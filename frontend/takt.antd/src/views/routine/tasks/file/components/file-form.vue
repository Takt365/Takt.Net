<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 3 }"
    :wrapper-col="{ span: 24 }"
    layout="horizontal"
  >
    <a-form-item
      :label="t('entity.file.storagetype')"
      name="storageType"
    >
      <TaktSelect
        v-model:value="formState.storageType"
        dict-type="sys_storage_type"
        :placeholder="t('routine.tasks.files.placeholders.selectStorageType')"
      />
    </a-form-item>

    <a-form-item
      v-if="formState.storageType === 0"
      :label="t('routine.tasks.files.labels.storageDirectory')"
      name="storageDirectory"
    >
      <TaktSelect
        v-model:value="formState.storageDirectory"
        dict-type="sys_storage_directory"
        :placeholder="t('routine.tasks.files.placeholders.selectStorageDirectory')"
      />
    </a-form-item>

    <!-- OSS对象存储配置 -->
    <template v-if="formState.storageType === 1">
      <a-form-item name="ossProvider">
        <template #label>
          <span>
            {{ t('routine.tasks.files.labels.ossProvider') }}
            <a-tooltip :title="getOssTooltipText(formState.ossProvider)">
              <RiQuestionFill style="margin-left: 4px; color: #1890ff; cursor: help;" />
            </a-tooltip>
          </span>
        </template>
        <TaktSelect
          v-model:value="formState.ossProvider"
          dict-type="sys_oss_provider"
          :placeholder="t('routine.tasks.files.placeholders.selectOssProvider')"
        />
      </a-form-item>
    </template>

    <!-- FTP配置 -->
    <template v-if="formState.storageType === 2">
      <a-form-item name="ftpType">
        <template #label>
          <span>
            {{ t('routine.tasks.files.labels.ftpProvider') }}
            <a-tooltip :title="getFtpTooltipText(formState.ftpType)">
              <RiQuestionFill style="margin-left: 4px; color: #1890ff; cursor: help;" />
            </a-tooltip>
          </span>
        </template>
        <TaktSelect
          v-model:value="formState.ftpType"
          dict-type="sys_ftp_provider"
          :placeholder="t('routine.tasks.files.placeholders.selectFtpProvider')"
        />
      </a-form-item>
    </template>

    <a-form-item
      :label="t('routine.tasks.files.labels.storageNaming')"
      name="storageNaming"
    >
      <TaktSelect
        v-model:value="formState.storageNaming"
        dict-type="sys_storage_naming"
        :placeholder="t('routine.tasks.files.placeholders.selectStorageNaming')"
      />
    </a-form-item>

    <a-form-item
      v-if="formState.storageNaming === 2"
      :label="t('routine.tasks.files.labels.storageNamingCustom')"
      name="storageNamingCustom"
    >
      <a-input
        v-model:value="formState.storageNamingCustom"
        :placeholder="t('routine.tasks.files.placeholders.namingCustom')"
      />
    </a-form-item>

    <a-form-item
      :label="t('entity.file.ispublic')"
      name="isPublic"
    >
      <TaktSelect
        v-model:value="formState.isPublic"
        dict-type="sys_yes_no"
        :placeholder="t('routine.tasks.files.placeholders.selectIsPublic')"
      />
    </a-form-item>

    <a-form-item
      :label="t('entity.file.status')"
      name="fileStatus"
    >
      <TaktSelect
        v-model:value="formState.fileStatus"
        dict-type="sys_file_status"
        :placeholder="t('routine.tasks.files.placeholders.selectFileStatus')"
      />
    </a-form-item>

    <a-form-item
      :label="t('entity.file.description')"
      name="fileDescription"
    >
      <a-textarea
        v-model:value="formState.fileDescription"
        :placeholder="t('routine.tasks.files.placeholders.fileDescription')"
        :rows="4"
      />
    </a-form-item>

    <a-form-item
      :label="t('common.entity.remark')"
      name="remark"
    >
      <a-textarea
        v-model:value="formState.remark"
        :placeholder="t('routine.tasks.files.placeholders.remark')"
        :rows="4"
      />
    </a-form-item>

    <a-form-item
      :label="t('entity.file.tags')"
      name="fileTags"
    >
      <template
        v-for="(tag, index) in fileTagsList"
        :key="tag"
      >
        <a-tooltip
          v-if="tag.length > 20"
          :title="tag"
        >
          <a-tag
            :closable="index !== 0"
            @close="() => handleRemoveTag(tag)"
          >
            {{ `${tag.slice(0, 20)}...` }}
          </a-tag>
        </a-tooltip>
        <a-tag
          v-else
          :closable="index !== 0"
          @close="() => handleRemoveTag(tag)"
        >
          {{ tag }}
        </a-tag>
      </template>
      <a-input
        v-if="tagInputVisible && fileTagsList.length < 7"
        ref="tagInputRef"
        v-model:value="tagInputValue"
        type="text"
        size="small"
        :style="{ width: '78px' }"
        @blur="handleTagInputConfirm"
        @keyup.enter="handleTagInputConfirm"
      />
      <a-tag
        v-else-if="fileTagsList.length < 7"
        @click="showTagInput"
      >
        <plus-outlined />
        {{ t('routine.tasks.files.labels.addTag') }}
      </a-tag>
    </a-form-item>

    <!-- 文件上传组件 - 放在最后 -->
    <a-form-item
      :label="t('routine.tasks.files.labels.fileUpload')"
      name="fileUpload"
      :required="true"
    >
      <TaktUploadFile
        ref="uploadFileRef"
        tabs-type="files"
        :files-auto-upload="false"
        :files-custom-request="handleFileUpload"
        :files-max-size="1024"
        :files-enable-chunked="true"
        :files-chunk-size="2"
        files-accept=".jpg,.jpeg,.pdf,.xlsx,.rar"
        :files-before-upload="validateFileType"
        @files:change="handleFilesChange"
      />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, reactive, computed, watch, nextTick, onMounted, getCurrentInstance } from 'vue'
import { useI18n } from 'vue-i18n'
import { PlusOutlined } from '@ant-design/icons-vue'
import { RiQuestionFill   } from '@remixicon/vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { UploadChangeParam } from 'ant-design-vue'
import type { File as TaktFile } from '@/types/routine/tasks/files/file'
import { useUserStore } from '@/stores/identity/user'
import { logger } from '@/utils/logger'
import { upload as uploadFileApi } from '@/api/routine/tasks/file'
import { generateFileIdentifier } from '@/utils/upload'
import { DateTimeHelper } from '@/utils/datetime'
import { getFileExtension, getFileCategoryByFileName } from '@/utils/file-type'
import { message } from 'ant-design-vue'

// 自定义上传请求选项类型
interface CustomUploadRequestOption {
  file: string | Blob | globalThis.File
  onSuccess?: (response: any, file: any) => void
  onError?: (error: Error) => void
  onProgress?: (event: { percent: number }) => void
}

interface Props {
  formData?: Partial<TaktFile>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const { t } = useI18n()

const emit = defineEmits<{
  'submit': [values: any]
  'cancel': []
}>()

const formRef = ref()
const tagInputRef = ref()
const tagInputVisible = ref(false)
const tagInputValue = ref('')
const uploadFileRef = ref()
const filesFileList = ref<any[]>([]) // 文件列表，用于验证
const userStore = useUserStore()

// 注意：字典数据在路由守卫中统一加载，组件中通过 TaktSelect 的 dict-type 属性直接使用即可

// 生成默认文件描述（文案见 routine.tasks.files.messages.defaultDescription）
const generateDefaultFileDescription = (): string => {
  const userName = userStore.userInfo?.userName || t('routine.tasks.files.messages.unknownUser')
  const instance = getCurrentInstance()
  const formatDateTime = instance?.appContext.config.globalProperties.formatDateTime as typeof DateTimeHelper.format | undefined

  const dateStr = formatDateTime?.(new Date(), 'YYYY-MM-DD HH:mm:ss') ||
    DateTimeHelper.format(new Date(), 'YYYY-MM-DD HH:mm:ss')

  return t('routine.tasks.files.messages.defaultDescription', { user: userName, time: dateStr })
}

interface FormState {
  // 隐藏字段（通过上传自动填充）
  fileCode?: string
  fileOriginalName?: string
  filePath?: string
  fileSize?: number
  fileType?: string
  fileExtension?: string
  fileHash?: string
  fileCategory?: number
  storageConfig?: string
  accessPermissionConfig?: string
  // 显示字段
  fileName?: string
  storageType?: number
  storageDirectory?: string
  // OSS配置
  ossProvider?: string
  // FTP配置
  ftpType?: string
  storageNaming?: number
  storageNamingCustom: string
  isPublic?: number
  fileDescription: string
  remark: string
  fileTags?: string
  fileStatus?: number
  fileUpload?: any[] // 文件上传列表，用于表单验证
}

const formState = reactive<FormState>({
  // 隐藏字段
  fileCode: '',
  fileOriginalName: '',
  filePath: '',
  fileSize: 0,
  fileType: '',
  fileExtension: '',
  fileHash: '',
  fileCategory: 5,
  storageConfig: '',
  accessPermissionConfig: '',
  // 显示字段
  fileName: '',
  storageType: 0,
  storageDirectory: 'default',
  // OSS配置
  ossProvider: '',
  // FTP配置
  ftpType: '',
  storageNaming: 0,
  storageNamingCustom: '',
  isPublic: 0,
  fileDescription: '',
  remark: '',
  fileTags: '',
  fileStatus: 0,
  fileUpload: [] // 文件上传列表，用于表单验证
})

// 文件标签列表（从逗号分隔的字符串转换为数组）
const fileTagsList = computed({
  get: () => {
    if (!formState.fileTags) return []
    return formState.fileTags.split(',').filter(tag => tag.trim() !== '')
  },
  set: (tags: string[]) => {
    formState.fileTags = tags.join(',')
  }
})

// 显示标签输入框
const showTagInput = () => {
  if (fileTagsList.value.length >= 7) {
    message.warning(t('routine.tasks.files.messages.tagMax'))
    return
  }
  tagInputVisible.value = true
  nextTick(() => {
    tagInputRef.value?.focus()
  })
}

// 确认添加标签
const handleTagInputConfirm = () => {
  const inputValue = tagInputValue.value.trim()
  let tags = fileTagsList.value
  
  if (inputValue && tags.indexOf(inputValue) === -1 && tags.length < 7) {
    tags = [...tags, inputValue]
    fileTagsList.value = tags
  } else if (tags.length >= 7) {
    message.warning(t('routine.tasks.files.messages.tagMax'))
  } else if (tags.indexOf(inputValue) !== -1) {
    message.warning(t('routine.tasks.files.messages.tagDuplicate'))
  }
  
  tagInputVisible.value = false
  tagInputValue.value = ''
}

// 删除标签
const handleRemoveTag = (removedTag: string) => {
  const tags = fileTagsList.value.filter(tag => tag !== removedTag)
  fileTagsList.value = tags
}

// 注意：字段现在始终显示，不再需要 hasUploadedFile 来控制显示

const rules = computed<Record<string, Rule[]>>(() => ({
  fileUpload: [
    {
      validator: (_rule: any, value: any) => {
        const fileList = (Array.isArray(value) ? value : filesFileList.value) || []
        if (fileList.length === 0) {
          return Promise.reject(t('routine.tasks.files.messages.selectFileFirst'))
        }
        return Promise.resolve()
      },
      trigger: 'change'
    }
  ],
  storageType: [
    { required: true, message: t('routine.tasks.files.messages.storageTypeRequired'), trigger: 'change' }
  ],
  storageDirectory: [
    {
      validator: (_rule: any, value: string) => {
        if (formState.storageType === 0 && !value) {
          return Promise.reject(t('routine.tasks.files.messages.storageDirRequired'))
        }
        return Promise.resolve()
      },
      trigger: 'change'
    }
  ],
  storageNamingCustom: [
    {
      validator: (_rule: any, value: string) => {
        if (formState.storageNaming === 2 && !value) {
          return Promise.reject(t('routine.tasks.files.messages.namingCustomRequired'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ]
}))

// 监听 formData 变化，更新 formState
watch(
  () => props.formData,
  (newData) => {
    // 判断是否是新增（空对象或未定义）
    const isNew = !newData || Object.keys(newData || {}).length === 0
    
    if (newData && !isNew) {
      // 从 storageConfig JSON 中解析配置
      let storageDirectory = ''
      let ossProvider = ''
      let ftpType = ''
      
      if (newData.storageConfig) {
        try {
          const config = JSON.parse(newData.storageConfig)
          storageDirectory = config.directory || ''
          // OSS配置
          ossProvider = config.provider || ''
          // FTP配置
          ftpType = config.type || ''
        } catch (e) {
          // 如果解析失败，忽略错误
        }
      }
      
      Object.assign(formState, {
        // 隐藏字段
        fileCode: newData.fileCode || '',
        fileOriginalName: newData.fileOriginalName || '',
        filePath: newData.filePath || '',
        fileSize: newData.fileSize || 0,
        fileType: newData.fileType || '',
        fileExtension: newData.fileExtension || '',
        fileHash: newData.fileHash || '',
        fileCategory: newData.fileCategory ?? 5,
        storageConfig: newData.storageConfig || '',
        accessPermissionConfig: newData.accessPermissionConfig || '',
        // 显示字段
        fileName: newData.fileName || '',
        storageType: newData.storageType ?? 0,
        storageDirectory: storageDirectory || (newData.storageType === 0 ? 'default' : ''),
        // OSS配置
        ossProvider: ossProvider,
        // FTP配置
        ftpType: ftpType,
        storageNaming: (newData as any).storageNaming ?? 0,
        storageNamingCustom: (newData as any).storageNamingCustom || '',
        isPublic: newData.isPublic ?? 0,
        fileDescription: newData.fileDescription || '',
        remark: (newData as any).remark || newData.fileDescription || '',
        fileTags: newData.fileTags || '',
        fileStatus: newData.fileStatus ?? 0,
        fileUpload: [] // 编辑时，文件上传列表为空（已有文件不需要重新上传）
      })
    } else {
      // 重置表单（新增场景）
      Object.assign(formState, {
        // 隐藏字段
        fileCode: '',
        fileOriginalName: '',
        filePath: '',
        fileSize: 0,
        fileType: '',
        fileExtension: '',
        fileHash: '',
        fileCategory: 5,
        storageConfig: '',
        accessPermissionConfig: '',
        // 显示字段
        fileName: '',
        storageType: 0,
        storageDirectory: 'default',
        // OSS配置
        ossProvider: '',
        // FTP配置
        ftpType: '',
        storageNaming: 0,
        storageNamingCustom: '',
        isPublic: 0,
        fileDescription: generateDefaultFileDescription(),
        remark: generateDefaultFileDescription(),
        fileTags: '',
        fileStatus: 0,
        fileUpload: [] // 新增时，文件上传列表为空
      })
      
      // 重置文件列表
      filesFileList.value = []
      
      // 新增时，延迟设置默认值，确保字典数据已加载
      // 使用多个延迟确保 TaktSelect 组件和字典数据都已准备好
      nextTick(() => {
        // 第一次设置
        if (formState.storageType === 0 && formState.storageDirectory !== 'default') {
          formState.storageDirectory = 'default'
        }
        // 延迟设置，确保字典数据已加载
        setTimeout(() => {
          if (formState.storageType === 0) {
            // 强制触发响应式更新，确保 TaktSelect 组件能正确显示
            const currentValue = formState.storageDirectory || 'default'
            formState.storageDirectory = ''
            nextTick(() => {
              formState.storageDirectory = currentValue
              // 再次延迟，确保字典数据已完全加载
              setTimeout(() => {
                if (formState.storageType === 0 && formState.storageDirectory !== 'default') {
                  formState.storageDirectory = 'default'
                }
              }, 300)
            })
          }
        }, 500)
      })
    }
  },
  { immediate: true, deep: true }
)

// 监听存储命名规则变化，如果不是自定义（2），清空自定义命名规则，并重新生成 fileName
watch(
  () => formState.storageNaming,
  (newValue) => {
    if (newValue !== 2) {
      formState.storageNamingCustom = ''
    }
    // 如果已有文件信息，根据新的命名规则重新生成 fileName
    if (formState.fileOriginalName) {
      if (newValue === 2 && formState.storageNamingCustom) {
        formState.fileName = formState.storageNamingCustom
      } else if (newValue === 1 && formState.fileCode) {
        const extension = formState.fileOriginalName.match(/\.[^/.]+$/)?.[0] || ''
        formState.fileName = `${formState.fileCode}${extension}`
      } else if (newValue === 0 && formState.fileHash) {
        const originalNameWithoutExt = formState.fileOriginalName.replace(/\.[^/.]+$/, '')
        const extension = formState.fileOriginalName.match(/\.[^/.]+$/)?.[0] || ''
        formState.fileName = `${originalNameWithoutExt}_${formState.fileHash}${extension}`
      }
    }
  }
)

// 监听存储方式变化，清空不相关的配置
watch(
  () => formState.storageType,
  (newValue) => {
    if (newValue !== 0) {
      formState.storageDirectory = ''
    } else if (newValue === 0) {
      // 当切换到本地存储时，如果存储目录为空，设置为默认值
      // 使用多个 nextTick 和 setTimeout 确保 TaktSelect 组件已渲染且字典数据已加载
      nextTick(() => {
        if (!formState.storageDirectory) {
          formState.storageDirectory = 'default'
        }
        // 延迟设置，确保字典数据已加载
        setTimeout(() => {
          if (formState.storageType === 0 && !formState.storageDirectory) {
            formState.storageDirectory = 'default'
          }
        }, 200)
      })
    }
    if (newValue !== 1) {
      // 清空OSS配置
      formState.ossProvider = ''
    }
    if (newValue !== 2) {
      // 清空FTP配置
      formState.ftpType = ''
    }
  },
  { immediate: true } // 立即执行一次，确保初始化时也设置默认值
)

// 监听文件描述变化，如果备注为空，则自动同步
watch(
  () => formState.fileDescription,
  (newDescription) => {
    // 如果备注为空或未设置，则自动设置为文件描述的值
    if (!formState.remark || formState.remark.trim() === '') {
      formState.remark = newDescription || ''
    }
  }
)

// 组件挂载后，确保初始化时如果存储方式为本地存储，存储目录有默认值
onMounted(() => {
  // 使用多个 nextTick 和 setTimeout 确保字典数据加载完成后再设置
  nextTick(() => {
    setTimeout(() => {
      if (formState.storageType === 0) {
        // 如果存储目录为空，设置为默认值
        if (!formState.storageDirectory) {
          formState.storageDirectory = 'default'
        }
        // 强制触发响应式更新，确保 TaktSelect 组件能正确显示
        const currentValue = formState.storageDirectory
        formState.storageDirectory = ''
        nextTick(() => {
          formState.storageDirectory = currentValue || 'default'
        })
      }
      if (!formState.fileDescription || formState.fileDescription.trim() === '') {
        formState.fileDescription = generateDefaultFileDescription()
        logger.debug('[FileForm] 在 onMounted 中生成文件描述:', formState.fileDescription)
      }
      // 备注：如果为空，则默认等于文件描述
      if (!formState.remark || formState.remark.trim() === '') {
        formState.remark = formState.fileDescription || ''
      }
    }, 300) // 延迟 300ms 确保字典数据已加载
  })
})

// 获取OSS配置提示文本（用于问号图标）
const getOssTooltipText = (provider?: string): string => {
  if (!provider) {
    return t('routine.tasks.files.tooltips.ossDefault')
  }

  const keys: Record<string, 'routine.tasks.files.tooltips.ossAliyun' | 'routine.tasks.files.tooltips.ossTencent' | 'routine.tasks.files.tooltips.ossHuawei' | 'routine.tasks.files.tooltips.ossAws'> = {
    aliyun: 'routine.tasks.files.tooltips.ossAliyun',
    tencent: 'routine.tasks.files.tooltips.ossTencent',
    huawei: 'routine.tasks.files.tooltips.ossHuawei',
    aws: 'routine.tasks.files.tooltips.ossAws'
  }

  const k = keys[provider]
  return k ? t(k) : t('routine.tasks.files.tooltips.ossDefault')
}

// 获取FTP配置提示文本（用于问号图标）
const getFtpTooltipText = (provider?: string): string => {
  if (!provider) {
    return t('routine.tasks.files.tooltips.ftpDefault')
  }

  const keys: Record<string, 'routine.tasks.files.tooltips.ftpTeacCn' | 'routine.tasks.files.tooltips.ftpTeacJp'> = {
    teac_cn: 'routine.tasks.files.tooltips.ftpTeacCn',
    teac_jp: 'routine.tasks.files.tooltips.ftpTeacJp'
  }

  const k = keys[provider]
  return k ? t(k) : t('routine.tasks.files.tooltips.ftpDefault')
}

// 文件类型验证：只允许 jpg, pdf, xlsx, rar
const validateFileType = (file: { originFileObj?: globalThis.File } | globalThis.File, _fileList?: unknown[]): boolean => {
  const originFile = (file as { originFileObj?: globalThis.File }).originFileObj || (file as globalThis.File)

  if (!originFile || !(originFile instanceof File)) {
    return false
  }

  const allowedExtensions = ['jpg', 'jpeg', 'pdf', 'xlsx', 'rar']
  const fileExtension = getFileExtension(originFile.name).toLowerCase()

  if (!allowedExtensions.includes(fileExtension)) {
    message.error(
      t('routine.tasks.files.messages.allowedTypes', {
        types: allowedExtensions.join(', ').toUpperCase()
      })
    )
    return false
  }

  return true
}

// 文件上传处理
const handleFileUpload = async (options: CustomUploadRequestOption) => {
  const { file, onSuccess, onError, onProgress } = options
  
  // 确保 file 是 File 类型
  const uploadFile: globalThis.File | null = file instanceof File ? file : (typeof file === 'string' ? null : file as globalThis.File)
  if (!uploadFile || !(uploadFile instanceof File)) {
    const err = new Error(t('routine.tasks.files.messages.invalidFile'))
    onError?.(err)
    message.error(t('routine.tasks.files.messages.fileTypeInvalid'))
    return
  }

  // 验证文件类型
  if (!validateFileType(uploadFile)) {
    const err = new Error(t('routine.tasks.files.messages.unsupportedFileType'))
    onError?.(err)
    return
  }
  
  try {
    // 根据 storageNaming 规则生成目标文件名（如果已设置命名规则）
    let targetFileName: string | undefined = undefined
    
    if (formState.storageNaming === 2 && formState.storageNamingCustom) {
      // 自定义命名：使用 storageNamingCustom
      targetFileName = formState.storageNamingCustom
    } else if (formState.storageNaming === 0) {
      // 原文件+哈希值：需要先计算文件哈希值
      try {
        const fileHash = await generateFileIdentifier(uploadFile)
        const originalNameWithoutExt = uploadFile.name.replace(/\.[^/.]+$/, '')
        const extension = uploadFile.name.match(/\.[^/.]+$/)?.[0] || ''
        targetFileName = `${originalNameWithoutExt}_${fileHash}${extension}`
        logger.debug('[FileUpload] 根据命名规则生成目标文件名:', { targetFileName, fileHash })
      } catch (hashError: any) {
        logger.warn('[FileUpload] 计算文件哈希值失败，将使用默认文件名:', hashError)
        // 如果计算哈希值失败，不传递 targetFileName，使用默认的 fileCode.扩展名
      }
    }
    // storageNaming === 1 时，使用默认的 fileCode.扩展名，不需要传递 targetFileName
    
    // 使用正确的 API 方法上传文件
    let uploadResult = await uploadFileApi(
      uploadFile,
      2, // File = 2
      targetFileName, // 传递目标文件名（如果已设置命名规则）
      onProgress ? (progressEvent: { percent: number }) => {
        onProgress(progressEvent)
      } : undefined
    )
    
    // 尝试解包可能的响应包装器（如 TaktApiResult）
    // TaktApiResult 格式：{ code: number, message: string, data: T, success: boolean }
    if (uploadResult && typeof uploadResult === 'object') {
      // 如果响应有 data 字段且 data 有 fileCode，说明被 TaktApiResult 包装了
      if (uploadResult.data && typeof uploadResult.data === 'object' && uploadResult.data.fileCode) {
        uploadResult = uploadResult.data
      }
      // 如果响应有 result 字段，说明被包装了
      else if (uploadResult.result && typeof uploadResult.result === 'object' && uploadResult.result.fileCode) {
        uploadResult = uploadResult.result
      }
      // 如果响应本身就有 fileCode，说明没有被包装，直接使用
      // 注意：这里不需要 else if，因为如果 uploadResult.fileCode 存在，说明已经是 FileUploadResult
    }
    
    // 确保 uploadResult 有 fileCode
    if (uploadResult && uploadResult.fileCode) {
      // 强制触发响应式更新
      formState.fileCode = uploadResult.fileCode || ''
      // 自动填充隐藏字段（这些字段不在界面显示）
      formState.fileCode = uploadResult.fileCode || ''
      formState.fileOriginalName = uploadResult.fileOriginalName || uploadFile.name
      formState.filePath = uploadResult.filePath || ''
      formState.fileSize = uploadResult.fileSize || uploadFile.size
      formState.fileType = uploadResult.fileType || uploadFile.type
      formState.fileExtension = uploadResult.fileExtension || getFileExtension(uploadFile.name)
      formState.fileHash = uploadResult.fileHash || ''
      formState.fileCategory = uploadResult.fileCategory ?? getFileCategoryByFileName(uploadFile.name)
      
      // 根据命名规则自动生成 fileName
      if (formState.storageNaming === 2 && formState.storageNamingCustom) {
        // 自定义命名：使用 storageNamingCustom
        formState.fileName = formState.storageNamingCustom
      } else if (formState.storageNaming === 1 && formState.fileCode) {
        // 自动生成：使用 fileCode（哈希值）
        const extension = formState.fileOriginalName?.match(/\.[^/.]+$/)?.[0] || ''
        formState.fileName = `${formState.fileCode}${extension}`
      } else if (formState.storageNaming === 0 && formState.fileOriginalName && formState.fileHash) {
        // 原文件+哈希值：格式为 原文件名_哈希值.扩展名
        const originalNameWithoutExt = formState.fileOriginalName.replace(/\.[^/.]+$/, '')
        const extension = formState.fileOriginalName.match(/\.[^/.]+$/)?.[0] || ''
        formState.fileName = `${originalNameWithoutExt}_${formState.fileHash}${extension}`
      } else {
        // 默认：使用原始文件名（去掉扩展名）
        formState.fileName = uploadFile.name.replace(/\.[^/.]+$/, '')
      }
      formState.storageType = uploadResult.storageType ?? 0 // 默认本地存储
      formState.isPublic = uploadResult.isPublic ?? 0 // 默认公开
      // 文件描述：FileUploadResult 不包含 fileDescription，所以总是使用默认值
      // 如果当前 fileDescription 为空，则生成默认值
      if (!formState.fileDescription || formState.fileDescription.trim() === '') {
        formState.fileDescription = generateDefaultFileDescription()
      }
      // 备注：如果为空，则默认等于文件描述
      if (!formState.remark || formState.remark.trim() === '') {
        formState.remark = formState.fileDescription || ''
      }
      formState.fileTags = uploadResult.fileTags || ''
      formState.fileStatus = uploadResult.fileStatus ?? 0 // 默认正常

      // 确保响应式更新，触发 hasUploadedFile 重新计算
      await nextTick()
      
      // 调试：检查设置后的 fileDescription
      logger.debug('[FileForm] 上传后设置的文件描述:', {
        fileDescription: formState.fileDescription,
        length: formState.fileDescription?.length
      })
      
      onSuccess?.(uploadResult, uploadFile)
      // 注意：不在这里显示成功消息，因为上传和创建是合并操作的，成功消息会在创建成功后统一显示
    } else {
      throw new Error(t('routine.tasks.files.messages.uploadResponseNoCode'))
    }
  } catch (error: any) {
    const err = error instanceof Error ? error : new Error(String(error))
    
    // 记录详细错误信息
    const errorMessage = error?.response?.data?.message || error?.message || t('routine.tasks.files.messages.uploadFailedGeneric')
    const errorStatus = error?.response?.status
    const errorStatusText = error?.response?.statusText
    
    logger.error('[FileUpload] 文件上传失败:', {
      fileName: uploadFile?.name,
      error: err.message,
      status: errorStatus,
      statusText: errorStatusText,
      response: error?.response?.data,
      fullError: error
    })
    
    onError?.(err)
    message.error(
      errorStatus === 403
        ? t('routine.tasks.files.messages.uploadRejected403')
        : errorMessage || t('routine.tasks.files.messages.uploadFailedGeneric')
    )
  }
}

// 文件列表变化处理
const handleFilesChange = (info: UploadChangeParam) => {
  // 更新文件列表（用于验证）
  const fileList = info.fileList || []
  filesFileList.value = fileList
  formState.fileUpload = fileList // 同步更新 formState，用于表单验证
  
  // 手动触发 fileUpload 字段的验证
  formRef.value?.validateFields(['fileUpload']).catch(() => {
    // 验证失败时忽略错误，因为会在提交时再次验证
  })
  
  const { file } = info
  if (file.status === 'done' && file.response) {
    const uploadResult = file.response
    if (uploadResult && uploadResult.fileCode) {
      // 自动填充所有字段（包括显示字段）
      formState.fileCode = uploadResult.fileCode || ''
      formState.fileOriginalName = uploadResult.fileOriginalName || file.name
      formState.filePath = uploadResult.filePath || ''
      formState.fileSize = uploadResult.fileSize || (file.originFileObj?.size || 0)
      formState.fileType = uploadResult.fileType || (file.originFileObj?.type || '')
      formState.fileExtension = uploadResult.fileExtension || getFileExtension(file.name)
      formState.fileHash = uploadResult.fileHash || ''
      formState.fileCategory = uploadResult.fileCategory ?? getFileCategoryByFileName(file.name)
      
      // 根据命名规则自动生成 fileName
      if (formState.storageNaming === 2 && formState.storageNamingCustom) {
        // 自定义命名：使用 storageNamingCustom
        formState.fileName = formState.storageNamingCustom
      } else if (formState.storageNaming === 1 && formState.fileCode) {
        // 自动生成：使用 fileCode（哈希值）
        const extension = formState.fileOriginalName?.match(/\.[^/.]+$/)?.[0] || ''
        formState.fileName = `${formState.fileCode}${extension}`
      } else if (formState.storageNaming === 0 && formState.fileOriginalName && formState.fileHash) {
        // 原文件+哈希值：格式为 原文件名_哈希值.扩展名
        const originalNameWithoutExt = formState.fileOriginalName.replace(/\.[^/.]+$/, '')
        const extension = formState.fileOriginalName.match(/\.[^/.]+$/)?.[0] || ''
        formState.fileName = `${originalNameWithoutExt}_${formState.fileHash}${extension}`
      } else {
        // 默认：使用原始文件名（去掉扩展名）
        formState.fileName = file.name.replace(/\.[^/.]+$/, '')
      }
      formState.storageType = uploadResult.storageType ?? 0 // 默认本地存储
      formState.isPublic = uploadResult.isPublic ?? 0 // 默认公开
      // 文件描述：FileUploadResult 不包含 fileDescription，所以总是使用默认值
      // 如果当前 fileDescription 为空，则生成默认值
      if (!formState.fileDescription || formState.fileDescription.trim() === '') {
        formState.fileDescription = generateDefaultFileDescription()
      }
      // 备注：如果为空，则默认等于文件描述
      if (!formState.remark || formState.remark.trim() === '') {
        formState.remark = formState.fileDescription || ''
      }
      formState.fileTags = uploadResult.fileTags || ''
      formState.fileStatus = uploadResult.fileStatus ?? 0 // 默认正常
    }
  } else if (file.status === 'removed') {
    // 文件移除时清空相关字段
    formState.fileCode = ''
    formState.fileOriginalName = ''
    formState.filePath = ''
    formState.fileSize = 0
    formState.fileType = ''
    formState.fileExtension = ''
    formState.fileHash = ''
    formState.fileCategory = 5
    formState.fileName = ''
    // 更新文件上传列表
    formState.fileUpload = filesFileList.value
  }
}

// 手动上传文件（在表单提交前调用）
const uploadFiles = async (): Promise<void> => {
  if (uploadFileRef.value) {
    await uploadFileRef.value.uploadFiles()
  }
}

// 清空上传组件
const clearUploadFiles = () => {
  if (uploadFileRef.value) {
    uploadFileRef.value.clearFiles()
  }
  filesFileList.value = []
  formState.fileUpload = []
}

// 暴露方法给父组件
defineExpose({
  validate: async () => {
    return await formRef.value?.validate()
  },
  validateFields: async (fields?: string[]) => {
    return await formRef.value?.validateFields(fields)
  },
  resetFields: () => {
    formRef.value?.resetFields()
  },
  uploadFiles,
  clearUploadFiles,
  getValues: () => {
    const values = { ...formState }
    
    // 根据命名规则设置 fileName
    if (values.storageNaming === 2 && values.storageNamingCustom) {
      // 自定义命名：使用 storageNamingCustom 作为 fileName
      values.fileName = values.storageNamingCustom
    } else if (values.storageNaming === 1 && values.fileCode) {
      // 自动生成：使用 fileCode（哈希值）作为 fileName
      const extension = values.fileOriginalName?.match(/\.[^/.]+$/)?.[0] || ''
      values.fileName = `${values.fileCode}${extension}`
    } else if (values.storageNaming === 0 && values.fileOriginalName && values.fileHash) {
      // 原文件+哈希值：格式为 原文件名_哈希值.扩展名
      // 例如：GPO_fs04.html -> GPO_fs04_fef6ce15838b47869ef99bc8f71c9eb0.html
      const originalNameWithoutExt = values.fileOriginalName.replace(/\.[^/.]+$/, '')
      const extension = values.fileOriginalName.match(/\.[^/.]+$/)?.[0] || ''
      values.fileName = `${originalNameWithoutExt}_${values.fileHash}${extension}`
    } else if (!values.fileName && values.fileOriginalName) {
      // 如果 fileName 为空，使用原始文件名（去掉扩展名）作为默认值
      values.fileName = values.fileOriginalName.replace(/\.[^/.]+$/, '')
    }
    
    // 将存储配置序列化到 storageConfig JSON 中
    try {
      let config: any = {}
      if (values.storageConfig) {
        try {
          config = JSON.parse(values.storageConfig)
        } catch (e) {
          // 如果解析失败，使用空对象
        }
      }
      
      // 根据存储方式设置相应的配置
      if (values.storageType === 0) {
        // 本地存储：存储目录
        if (values.storageDirectory) {
          config.directory = values.storageDirectory
        } else {
          delete config.directory
        }
        // 清空其他存储方式的配置
        delete config.provider
        delete config.host
        delete config.port
        delete config.username
        delete config.password
        delete config.basePath
      } else if (values.storageType === 1) {
        // OSS对象存储：提供商
        if (values.ossProvider) {
          config.provider = values.ossProvider
        } else {
          delete config.provider
        }
        // 清空其他存储方式的配置
        delete config.directory
        delete config.host
        delete config.port
        delete config.username
        delete config.password
        delete config.basePath
      } else if (values.storageType === 2) {
        // FTP
        if (values.ftpType) {
          config.type = values.ftpType
        } else {
          delete config.type
        }
        // 清空其他存储方式的配置
        delete config.directory
        delete config.provider
      } else {
        // 其他存储方式，清空所有配置
        delete config.directory
        delete config.provider
        delete config.type
      }
      
      // 如果配置对象为空，设置为空字符串，否则序列化为JSON
      values.storageConfig = Object.keys(config).length > 0 ? JSON.stringify(config) : ''
    } catch (e) {
      // 如果序列化失败，忽略错误
    }
    
    // 删除临时配置字段，因为它们不应该直接发送到后端
    delete (values as any).storageDirectory
    delete (values as any).ossProvider
    delete (values as any).ftpType
    delete (values as any).ftpHost
    delete (values as any).ftpPort
    delete (values as any).ftpUsername
    delete (values as any).ftpPassword
    delete (values as any).ftpBasePath
    
    return values
  }
})
</script>

<style scoped lang="less">

</style>
