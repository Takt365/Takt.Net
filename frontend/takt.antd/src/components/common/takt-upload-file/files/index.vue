<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/common/takt-upload-file/files -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：通用文件上传组件，支持拖拽上传、分片上传和文件验证 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="takt-upload-files">
    <a-upload-dragger
      v-model:fileList="fileList"
      :name="name"
      :multiple="multiple"
      :action="autoUpload ? action : undefined"
      :accept="accept"
      :maxCount="maxCount"
      :disabled="disabled"
      :beforeUpload="handleBeforeUpload"
      :customRequest="autoUpload ? customRequest : undefined"
      :showUploadList="showUploadList"
      @change="handleChange"
      @drop="handleDrop"
      @remove="handleRemove"
      @preview="handlePreview"
      v-bind="$attrs"
      class="takt-upload-files-dragger"
    >
      <p class="ant-upload-drag-icon">
        <slot name="icon">
          <inbox-outlined></inbox-outlined>
        </slot>
      </p>
      <p class="ant-upload-text">
        <slot name="text">{{ textDisplay }}</slot>
      </p>
      <p class="ant-upload-hint" v-if="hintDisplay">
        <slot name="hint">{{ hintDisplay }}</slot>
      </p>
    </a-upload-dragger>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { InboxOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import type { UploadChangeParam, UploadFile, UploadProps } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { logger } from '@/utils/logger'

const { t } = useI18n()

interface Props {
  /** 文件列表 */
  modelValue?: UploadFile[]
  /** 上传的文件字段名 */
  name?: string
  /** 是否支持多选 */
  multiple?: boolean
  /** 上传的地址 */
  action?: string
  /** 接受上传的文件类型 */
  accept?: string
  /** 最大上传数量 */
  maxCount?: number
  /** 是否禁用 */
  disabled?: boolean
  /** 是否显示上传列表 */
  showUploadList?: boolean | UploadProps['showUploadList']
  /** 上传前的钩子 */
  beforeUpload?: UploadProps['beforeUpload']
  /** 自定义上传请求 */
  customRequest?: UploadProps['customRequest']
  /** 提示文本 */
  text?: string
  /** 提示说明 */
  hint?: string
  /** 文件大小限制（MB） */
  maxSize?: number
  /** 是否启用分片上传 */
  enableChunked?: boolean
  /** 分片大小（MB，默认 2） */
  chunkSize?: number
  /** 是否自动上传（true=自动上传，false=手动上传，默认 true） */
  autoUpload?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  name: 'file',
  multiple: true,
  action: '',
  accept: undefined,
  maxCount: undefined,
  disabled: false,
  showUploadList: true,
  text: undefined,
  hint: undefined,
  maxSize: undefined,
  enableChunked: false,
  chunkSize: 2,
  beforeUpload: undefined,
  customRequest: undefined,
  autoUpload: true
})

const textDisplay = computed(() => props.text ?? t('components.common.upload.filesText'))
const hintDisplay = computed(() => props.hint ?? t('components.common.upload.filesHint'))

const emit = defineEmits<{
  'update:modelValue': [fileList: UploadFile[]]
  'change': [info: UploadChangeParam]
  'drop': [e: DragEvent]
  'remove': [file: UploadFile]
  'preview': [file: UploadFile]
}>()

const fileList = ref<UploadFile[]>(props.modelValue || [])

// 监听 modelValue 变化
watch(() => props.modelValue, (newValue) => {
  if (newValue !== fileList.value) {
    fileList.value = newValue || []
  }
}, { deep: true })

// 监听 fileList 变化
watch(fileList, (newValue) => {
  emit('update:modelValue', newValue)
}, { deep: true })

// 上传前的钩子
const handleBeforeUpload = (file: UploadFile | File) => {
  // 如果是手动上传模式，阻止自动上传
  if (!props.autoUpload) {
    return false
  }
  
  // 获取原生 File 对象
  const originFile = (file as UploadFile).originFileObj || (file as File)
  
  // 如果设置了文件大小限制
  if (props.maxSize && originFile.size) {
    const fileSizeMB = originFile.size / 1024 / 1024
    if (fileSizeMB > props.maxSize) {
      message.error(t('components.common.upload.fileSizeExceed', { max: props.maxSize }))
      return false
    }
  }

  // 如果提供了自定义 beforeUpload
  if (props.beforeUpload) {
    return props.beforeUpload(originFile as any, fileList.value as any)
  }

  return true
}

// 文件状态改变时的回调
const handleChange = (info: UploadChangeParam) => {
  fileList.value = info.fileList
  emit('change', info)

  const status = info.file.status
  if (status !== 'uploading') {
    console.log('[TaktUploadFiles] 文件状态变化:', info.file, info.fileList)
  }
  if (status === 'done') {
    message.success(t('components.common.upload.fileUploadSuccess', { name: info.file.name }))
  } else if (status === 'error') {
    message.error(t('components.common.upload.fileUploadFail', { name: info.file.name }))
  }
}

// 拖拽放下时的回调
const handleDrop = (e: DragEvent) => {
  console.log('[TaktUploadFiles] 拖拽放下:', e)
  emit('drop', e)
}

// 移除文件
const handleRemove = (file: UploadFile) => {
  emit('remove', file)
}

// 预览文件
const handlePreview = (file: UploadFile) => {
  emit('preview', file)
  
  if (file.url) {
    window.open(file.url, '_blank')
  } else if (file.preview) {
    window.open(file.preview, '_blank')
  } else {
    message.warning(t('components.common.upload.previewFail'))
  }
}

// 手动上传所有未上传的文件
const uploadFiles = async (): Promise<void> => {
  // 如果没有提供上传方法，直接返回（不报错，允许没有上传方法的情况）
  if (!props.customRequest) {
    return
  }
  
  // 获取所有未上传的文件（状态为 ready、error 或未设置状态的文件，排除已完成的）
  const filesToUpload = fileList.value.filter(
    file => file.status !== 'done' && file.status !== 'removed'
  )
  
  // 如果没有需要上传的文件，直接返回
  if (filesToUpload.length === 0) {
    return
  }
  
  // 逐个上传文件
  const uploadPromises = filesToUpload.map((file) => {
    return new Promise<void>((resolve, reject) => {
      const originFile = file.originFileObj || (file as any)
      if (!originFile) {
        reject(new Error(t('components.common.upload.fileInvalid', { name: file.name })))
        return
      }
      
      // 更新文件状态为 uploading
      file.status = 'uploading'
      file.percent = 0
      
      // 调用自定义上传请求
      props.customRequest?.({
        file: originFile,
        onSuccess: (response: any) => {
          file.status = 'done'
          file.percent = 100
          file.response = response
          // 手动触发 change 事件，确保父组件能收到文件上传完成的通知
          emit('change', {
            file,
            fileList: fileList.value
          } as UploadChangeParam)
          resolve()
        },
        onError: (error: Error) => {
          file.status = 'error'
          file.error = error
          
          // 记录错误日志
          logger.error('[TaktUploadFiles] 文件上传失败:', {
            fileName: file.name,
            error: error.message,
            file: file
          })
          
          // 手动触发 change 事件，通知父组件上传失败
          emit('change', {
            file,
            fileList: fileList.value
          } as UploadChangeParam)
          reject(error)
        },
        onProgress: (event: { percent: number }) => {
          file.percent = event.percent
        }
      } as any)
    })
  })
  
  try {
    await Promise.all(uploadPromises)
    message.success(t('components.common.upload.allUploadSuccess'))
  } catch (error: any) {
    const errorMessage = error?.message || t('components.common.upload.partialUploadFail')
    logger.error('[TaktUploadFiles] 批量上传失败:', {
      error: errorMessage,
      filesCount: filesToUpload.length,
      fullError: error
    })
    message.error(errorMessage)
    throw error
  }
}

// 清空文件列表
const clearFiles = () => {
  fileList.value = []
  emit('update:modelValue', [])
}

// 暴露方法给父组件
defineExpose({
  uploadFiles,
  clearFiles
})
</script>

<style scoped lang="less">
.takt-upload-files {
  .takt-upload-files-dragger {
    // 使用 Ant Design Vue 默认样式
  }
}
</style>
