<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/common/takt-import-file -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：文件导入组件，支持模板下载、上传、预览和导入结果展示 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="takt-import-file">
    <!-- 下载模板按钮 -->
    <a-button
      v-if="showTemplateButton"
      :loading="templateLoading"
      :disabled="disabled"
      @click="handleDownloadTemplate"
      class="takt-import-file-template"
    >
      <template #icon>
        <download-outlined />
      </template>
      {{ templateTextDisplay }}
    </a-button>

    <!-- 上传区域 -->
    <a-upload-dragger
      v-if="showUploadArea"
      v-model:fileList="fileList"
      :name="name"
      :accept="accept"
      :maxCount="1"
      :disabled="disabled"
      :beforeUpload="handleBeforeUpload"
      :customRequest="handleCustomRequest"
      :showUploadList="showUploadList"
      @change="handleChange"
      @preview="handlePreview"
      @drop="handleDrop"
      v-bind="$attrs"
      class="takt-import-file-upload"
    >
      <p class="ant-upload-drag-icon">
        <slot name="icon">
          <inbox-outlined />
        </slot>
      </p>
      <p class="ant-upload-text">
        <slot name="text">{{ uploadTextDisplay }}</slot>
      </p>
      <p class="ant-upload-hint" v-if="hintDisplay">
        <slot name="hint">{{ hintDisplay }}</slot>
      </p>
    </a-upload-dragger>

    <!-- 上传数据预览 -->
    <a-modal
      v-model:open="previewVisible"
      :title="previewTitle"
      :footer="null"
      width="90%"
      centered
      @cancel="handleCancelPreview"
      class="takt-import-file-preview"
    >
      <div v-if="previewType === 'xlsx' || previewType === 'csv' || previewType === 'txt'" class="takt-import-file-preview-file">
        <a-alert
          :message="getFileTypeText() + ' ' + t('common.button.preview')"
          :description="getPreviewDescription()"
          type="info"
          show-icon
          class="takt-import-file-preview-tip"
        />
        <div class="takt-import-file-preview-info">
          <p><strong>{{ t('components.common.import.fileName') }}</strong>{{ previewFileName }}</p>
          <p><strong>{{ t('components.common.import.fileSize') }}</strong>{{ previewFileSize }}</p>
          <p><strong>{{ t('components.common.import.fileType') }}</strong>{{ previewFileType }}</p>
        </div>
        <a-button type="primary" @click="handleDownloadPreviewFile" class="takt-import-file-preview-download">
          <template #icon>
            <download-outlined />
          </template>
          {{ t('components.common.import.downloadPreviewFile') }}
        </a-button>
      </div>
      <div v-else class="takt-import-file-preview-error">
        <a-result
          status="error"
          :title="t('components.common.import.cannotPreview')"
          :sub-title="t('components.common.import.previewExcelOnly')"
        />
      </div>
    </a-modal>

    <!-- 导入结果 -->
    <a-alert
      v-if="importResult"
      :type="importResult.fail > 0 ? 'warning' : 'success'"
      :message="getResultMessage()"
      :description="getResultDescription()"
      show-icon
      closable
      @close="handleCloseResult"
      class="takt-import-file-result"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { InboxOutlined, DownloadOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import type { UploadChangeParam, UploadFile, UploadProps } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { usePermissionStore } from '@/stores/identity/permission'

const { t } = useI18n()
const permissionStore = usePermissionStore()

type FileType = 'txt' | 'csv' | 'xlsx'

interface Props {
  /** 文件类型（只能选择一种） */
  fileType: FileType
  /** 是否显示下载模板按钮 */
  showTemplate?: boolean
  /** 下载模板的函数（仅传名称不含后缀；返回 Blob，下载文件名在视图中用 名称_时间戳.xlsx） */
  downloadTemplate?: (sheetName?: string, fileName?: string) => Promise<Blob>
  /** 导入文件的函数 */
  importFile?: (file: File, sheetName?: string) => Promise<{ success: number; fail: number; errors: string[] }>
  /** 上传的文件字段名 */
  name?: string
  /** 是否禁用 */
  disabled?: boolean
  /** 是否显示上传列表 */
  showUploadList?: boolean | UploadProps['showUploadList']
  /** 模板下载文本 */
  templateText?: string
  /** 上传文本 */
  uploadText?: string
  /** 提示说明 */
  hint?: string
  /** 工作表名称（仅 xlsx 格式需要） */
  sheetName?: string
  /** 模板文件名（仅名称，不含 .xlsx；后端自动拼接 名称_时间戳.xlsx） */
  templateFileName?: string
  /** 文件大小限制（MB） */
  maxSize?: number
  /** 最大记录数限制（默认 1000 条） */
  maxRows?: number
  /** 模板权限标识（如：identity:user:template；传入时仅在有该权限时显示下载模板按钮） */
  templatePermission?: string
  /** 导入权限标识（如：identity:user:import；传入时仅在有该权限时显示上传区域） */
  importPermission?: string
}

const props = withDefaults(defineProps<Props>(), {
  fileType: 'xlsx',
  showTemplate: true,
  downloadTemplate: undefined,
  importFile: undefined,
  name: 'file',
  disabled: false,
  showUploadList: true,
  templateText: undefined,
  uploadText: undefined,
  hint: undefined,
  sheetName: undefined,
  templateFileName: undefined,
  maxSize: 10,
  maxRows: 1000
})

const templateTextDisplay = computed(() => props.templateText ?? t('components.common.import.downloadTemplate'))
const uploadTextDisplay = computed(() => props.uploadText ?? t('components.common.import.uploadText'))
const hintDisplay = computed(() => props.hint ?? t('components.common.import.hint'))

/** 是否显示下载模板按钮：未传 templatePermission 时按 showTemplate；传入则需同时有权限 */
const showTemplateButton = computed(() => {
  if (!props.showTemplate) return false
  if (!props.templatePermission) return true
  return permissionStore.hasPermission(props.templatePermission)
})
/** 是否显示上传区域：未传 importPermission 时显示；传入则需有权限 */
const showUploadArea = computed(() => {
  if (!props.importPermission) return true
  return permissionStore.hasPermission(props.importPermission)
})

// 根据文件类型获取 accept 属性
const accept = computed(() => {
  switch (props.fileType) {
    case 'txt':
      return '.txt'
    case 'csv':
      return '.csv'
    case 'xlsx':
      return '.xlsx'
    default:
      return '.txt,.csv,.xlsx'
  }
})

const emit = defineEmits<{
  'update:fileList': [fileList: UploadFile[]]
  'change': [info: UploadChangeParam]
  'preview': [file: UploadFile]
  'drop': [e: DragEvent]
  'success': [result: { success: number; fail: number; errors: string[] }]
  'error': [error: Error]
}>()

const fileList = ref<UploadFile[]>([])
const templateLoading = ref(false)
const uploading = ref(false)
const importResult = ref<{ success: number; fail: number; errors: string[] } | null>(null)
const previewVisible = ref(false)
const previewFile = ref<File | null>(null)
const previewTitle = ref('')
const previewFileName = ref('')
const previewFileSize = ref('')
const previewFileType = ref('')

// 预览文件类型
const previewType = computed(() => {
  if (!previewFile.value) {
    return ''
  }
  const fileName = previewFile.value.name || ''
  if (fileName.endsWith('.xlsx')) {
    return 'xlsx'
  }
  if (fileName.endsWith('.csv')) {
    return 'csv'
  }
  if (fileName.endsWith('.txt')) {
    return 'txt'
  }
  return 'unknown'
})

// 获取预览描述
const getPreviewDescription = (): string => {
  switch (previewType.value) {
    case 'xlsx':
      return t('components.common.import.openWithExcel')
    case 'csv':
      return t('components.common.import.openWithCsv')
    case 'txt':
      return t('components.common.import.openWithEditor')
    default:
      return t('components.common.import.openWithApp')
  }
}

// 格式化文件大小
const formatFileSize = (bytes: number): string => {
  if (bytes === 0) {
    return '0 B'
  }
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return Math.round(bytes / Math.pow(k, i) * 100) / 100 + ' ' + sizes[i]
}

// 时间戳格式：yyyyMMddHHmmss，与后端一致
const formatTimestamp = () => {
  const d = new Date()
  const y = d.getFullYear()
  const m = String(d.getMonth() + 1).padStart(2, '0')
  const day = String(d.getDate()).padStart(2, '0')
  const h = String(d.getHours()).padStart(2, '0')
  const min = String(d.getMinutes()).padStart(2, '0')
  const s = String(d.getSeconds()).padStart(2, '0')
  return `${y}${m}${day}${h}${min}${s}`
}

// 下载模板（视图中用 名称_时间戳.xlsx 作为下载文件名）
const handleDownloadTemplate = async () => {
  if (!props.downloadTemplate) {
    message.warning(t('components.common.import.noTemplateFn'))
    return
  }

  try {
    templateLoading.value = true
    const blob = await props.downloadTemplate(props.sheetName, props.templateFileName)
    const baseName = (props.templateFileName && !props.templateFileName.endsWith('.xlsx') ? props.templateFileName : undefined) || t('components.common.import.template')
    const fileName = `${baseName}_${formatTimestamp()}.xlsx`

    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)

    message.success(t('components.common.import.templateDownloadSuccess'))
  } catch (error: any) {
    console.error('[TaktImportFile] 下载模板失败:', error)
    message.error(error?.message || t('components.common.import.templateDownloadFail'))
    emit('error', error instanceof Error ? error : new Error(String(error)))
  } finally {
    templateLoading.value = false
  }
}

// 验证文件类型
const validateFileType = (fileName: string): boolean => {
  const fileExtension = fileName.substring(fileName.lastIndexOf('.')).toLowerCase()
  const expectedExtension = `.${props.fileType}`
  return fileExtension === expectedExtension
}

// 获取文件类型文本
const getFileTypeText = (): string => {
  switch (props.fileType) {
    case 'txt':
      return t('components.common.import.fileTypeTxt')
    case 'csv':
      return t('components.common.import.fileTypeCsv')
    case 'xlsx':
      return t('components.common.import.fileTypeXlsx')
    default:
      return t('components.common.import.fileTypeFile')
  }
}

// 读取文本文件行数
const countTextFileRows = async (file: File): Promise<number> => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.onload = (e) => {
      try {
        const text = e.target?.result as string
        const lines = text.split(/\r?\n/).filter(line => line.trim().length > 0)
        resolve(lines.length)
      } catch (error) {
        reject(error)
      }
    }
    reader.onerror = reject
    reader.readAsText(file, 'UTF-8')
  })
}

// 读取 CSV 文件行数
const countCsvFileRows = async (file: File): Promise<number> => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.onload = (e) => {
      try {
        const text = e.target?.result as string
        const lines = text.split(/\r?\n/).filter(line => line.trim().length > 0)
        // CSV 第一行可能是标题行，所以实际数据行数可能减1
        const dataRows = lines.length > 0 ? lines.length - 1 : 0
        resolve(dataRows)
      } catch (error) {
        reject(error)
      }
    }
    reader.onerror = reject
    reader.readAsText(file, 'UTF-8')
  })
}


// 验证文件记录数
const validateFileRows = async (file: File): Promise<boolean> => {
  try {
    let rowCount = 0

    switch (props.fileType) {
      case 'txt':
        rowCount = await countTextFileRows(file)
        if (rowCount > props.maxRows!) {
          message.error(t('components.common.import.rowCountExceed', { count: rowCount, max: props.maxRows }))
          return false
        }
        if (rowCount === 0) {
          message.warning(t('components.common.import.fileEmpty'))
          return false
        }
        break
      case 'csv':
        rowCount = await countCsvFileRows(file)
        if (rowCount > props.maxRows!) {
          message.error(t('components.common.import.rowCountExceed', { count: rowCount, max: props.maxRows }))
          return false
        }
        if (rowCount === 0) {
          message.warning(t('components.common.import.fileEmpty'))
          return false
        }
        break
      case 'xlsx':
        // 对于 xlsx 文件，前端只做基本验证，详细的行数验证在服务端进行
        // 检查文件大小，如果文件太大可能超过限制
        const fileSizeKB = file.size / 1024
        if (fileSizeKB > 500) {
          message.warning(t('components.common.import.fileLargeHint', { size: Math.round(fileSizeKB), max: props.maxRows }))
        }
        // xlsx 文件的行数验证在服务端进行，这里只通过文件类型验证
        return true
      default:
        message.error(t('components.common.import.unsupportedFileType', { type: props.fileType }))
        return false
    }

    return true
  } catch (error: any) {
    console.error('[TaktImportFile] 验证文件记录数失败:', error)
    message.error(error?.message || t('components.common.import.validateRowCountFail'))
    return false
  }
}

// 上传前的钩子
const handleBeforeUpload = async (file: UploadFile | File) => {
  // 获取原生 File 对象
  const originFile = (file as UploadFile).originFileObj || (file as File)
  
  // 如果设置了文件大小限制
  if (props.maxSize && originFile.size) {
    const fileSizeMB = originFile.size / 1024 / 1024
    if (fileSizeMB > props.maxSize) {
      message.error(t('components.common.import.fileSizeExceed', { max: props.maxSize }))
      return false
    }
  }

  // 检查文件类型
  const fileName = originFile.name || ''
  if (!validateFileType(fileName)) {
    message.error(t('components.common.import.onlySupportType', { type: getFileTypeText() }))
    return false
  }

  // 验证文件记录数
  const isValidRows = await validateFileRows(originFile)
  if (!isValidRows) {
    return false
  }

  return true
}

// 自定义上传请求
const handleCustomRequest = async (options: any) => {
  const { file, onSuccess, onError, onProgress } = options

  if (!props.importFile) {
    const error = new Error(t('components.common.import.noImportFn'))
    onError(error)
    message.error(t('components.common.import.noImportFn'))
    return
  }

  try {
    uploading.value = true
    onProgress?.({ percent: 50 })

    const result = await props.importFile(file as File, props.sheetName)

    onProgress?.({ percent: 100 })
    onSuccess?.(result, file)

    // 保存导入结果
    importResult.value = result

    // 触发成功事件
    emit('success', result)

    // 显示成功消息
    if (result.fail > 0) {
      message.warning(t('components.common.import.importDoneSummary', { success: result.success, fail: result.fail }))
    } else {
      message.success(t('components.common.import.importSuccessTotal', { success: result.success }))
    }
  } catch (error: any) {
    console.error('[TaktImportFile] 导入失败:', error)
    const err = error instanceof Error ? error : new Error(String(error))
    onError?.(err)
    message.error(err.message || t('components.common.import.importFail'))
    emit('error', err)
  } finally {
    uploading.value = false
  }
}

// 文件状态改变时的回调
const handleChange = (info: UploadChangeParam) => {
  // 如果文件被移除，清空导入结果和预览
  if (info.file.status === 'removed') {
    importResult.value = null
    previewFile.value = null
  }

  fileList.value = info.fileList
  emit('change', info)
  emit('update:fileList', info.fileList)
}

// 预览文件
const handlePreview = (file: UploadFile) => {
  const originFile = file.originFileObj || (file as any)
  
  if (!originFile || !(originFile instanceof File)) {
    message.warning(t('components.common.import.cannotPreview'))
    return
  }

  previewFile.value = originFile
  previewTitle.value = t('components.common.import.previewTitle', { name: file.name || originFile.name || t('components.common.import.unknownFile') })
  previewFileName.value = file.name || originFile.name || t('components.common.import.unknownFile')
  previewFileSize.value = formatFileSize(originFile.size || 0)
  
  const fileName = previewFileName.value
  if (fileName.endsWith('.xlsx')) {
    previewFileType.value = t('components.common.import.excelWorkbook')
  } else if (fileName.endsWith('.csv')) {
    previewFileType.value = t('components.common.import.csvFile')
  } else if (fileName.endsWith('.txt')) {
    previewFileType.value = t('components.common.import.fileTypeTxt')
  } else {
    previewFileType.value = t('components.common.import.unknownType')
  }

  previewVisible.value = true
  emit('preview', file)
}

// 取消预览
const handleCancelPreview = () => {
  previewVisible.value = false
  previewFile.value = null
  previewTitle.value = ''
  previewFileName.value = ''
  previewFileSize.value = ''
  previewFileType.value = ''
}

// 下载预览文件
const handleDownloadPreviewFile = () => {
  if (!previewFile.value) {
    message.warning(t('components.common.import.previewNotExist'))
    return
  }

  const url = window.URL.createObjectURL(previewFile.value)
  const link = document.createElement('a')
  link.href = url
  link.download = previewFileName.value
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
  window.URL.revokeObjectURL(url)
  
  message.success(t('components.common.import.fileDownloadSuccess'))
}

// 拖拽放下时的回调
const handleDrop = (e: DragEvent) => {
  console.log('[TaktImportFile] 拖拽放下:', e)
  emit('drop', e)
}

// 关闭导入结果
const handleCloseResult = () => {
  importResult.value = null
}

// 获取结果消息
const getResultMessage = (): string => {
  if (!importResult.value) {
    return ''
  }
  const { success, fail } = importResult.value
  if (fail > 0) {
    return t('components.common.import.importDoneSummary', { success, fail })
  }
  return t('components.common.import.importSuccessTotal', { success })
}

// 获取结果描述
const getResultDescription = (): string => {
  if (!importResult.value || !importResult.value.errors || importResult.value.errors.length === 0) {
    return ''
  }
  const errorCount = importResult.value.errors.length
  const displayedErrors = importResult.value.errors.slice(0, 5)
  const remaining = errorCount > 5 ? t('components.common.import.errorsRemaining', { count: errorCount }) : ''
  return displayedErrors.join('；') + remaining
}
</script>

<style scoped lang="less">
.takt-import-file {
  display: flex;
  flex-direction: column;
  gap: 16px;

  .takt-import-file-template {
    align-self: flex-start;
  }

  .takt-import-file-upload {
    width: 100%;
  }

  .takt-import-file-preview {
    :deep(.ant-modal-body) {
      padding: 24px;
    }

    .takt-import-file-preview-file {
      .takt-import-file-preview-tip {
        margin-bottom: 16px;
      }

      .takt-import-file-preview-info {
        margin: 16px 0;
        padding: 16px;
        background: #f5f5f5;
        border-radius: 4px;

        p {
          margin: 8px 0;
          color: #666;

          strong {
            color: #333;
          }
        }
      }

      .takt-import-file-preview-download {
        width: 100%;
      }
    }

    .takt-import-file-preview-error {
      text-align: center;
    }
  }

  .takt-import-file-result {
    margin-top: 8px;
  }
}
</style>
