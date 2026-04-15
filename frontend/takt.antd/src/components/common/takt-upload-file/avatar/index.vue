<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/common/takt-upload-file/avatar -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：头像上传组件，支持图片裁剪（1:1比例）和预览 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="takt-upload-avatar">
    <div class="takt-upload-avatar-preview">
      <a-avatar :size="avatarSize" :src="avatarUrl" class="takt-upload-avatar-img">
        <template #icon>
          <user-outlined />
        </template>
      </a-avatar>
      <div class="takt-upload-avatar-actions">
        <a-upload
          :showUploadList="false"
          :beforeUpload="handleBeforeUpload"
          :customRequest="handleCustomRequest"
          accept="image/*"
          v-bind="$attrs"
        >
          <a-button type="primary" :loading="uploading">
            <template #icon>
              <upload-outlined />
            </template>
            {{ uploading ? t('components.common.upload.avatarUploading') : t('components.common.upload.avatarUpload') }}
          </a-button>
        </a-upload>
        <a-button v-if="avatarUrl" danger @click="handleRemove">
          <template #icon>
            <delete-outlined />
          </template>
          {{ t('common.button.delete') }}
        </a-button>
      </div>
    </div>

    <!-- 裁剪弹窗 -->
    <a-modal
      v-model:open="cropperVisible"
      :title="t('components.common.upload.cropAvatar')"
      :width="700"
      :okText="t('components.common.upload.ok')"
      :cancelText="t('components.common.upload.cancel')"
      :confirmLoading="cropping"
      @ok="handleCropConfirm"
      @cancel="handleCropCancel"
      class="takt-upload-avatar-cropper-modal"
    >
      <takt-cropper
        ref="cropperRef"
        :imageSrc="cropperImage"
        :aspectRatio="1"
        :width="600"
        :height="600"
        :cropWidth="cropSize"
        :cropHeight="cropSize"
        @crop="handleCropperCrop"
        @error="handleCropperError"
      />
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { UserOutlined, UploadOutlined, DeleteOutlined } from '@ant-design/icons-vue'
import { Avatar, Button, Upload, Modal, message } from 'ant-design-vue'
import type { UploadChangeParam, UploadFile, UploadProps } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import TaktCropper from '../cropper/index.vue'

const { t } = useI18n()

interface Props {
  /** 头像地址 */
  modelValue?: string
  /** 头像尺寸（默认 120） */
  avatarSize?: number
  /** 裁剪后图片尺寸（默认 200，用于上传） */
  cropSize?: number
  /** 文件大小限制（MB，默认 2） */
  maxSize?: number
  /** 自定义上传请求 */
  customRequest?: UploadProps['customRequest']
  /** 上传地址 */
  action?: string
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: '',
  avatarSize: 120,
  cropSize: 200,
  maxSize: 2,
  action: '',
  customRequest: undefined
})

const emit = defineEmits<{
  'update:modelValue': [url: string]
  /** 上传成功事件 */
  'success': [url: string, file: File]
  /** 上传失败事件 */
  'error': [error: Error]
  /** 删除事件 */
  'remove': []
}>()

const avatarUrl = ref(props.modelValue)
const uploading = ref(false)
const cropperVisible = ref(false)
const cropperImage = ref('')
const cropping = ref(false)
const cropperRef = ref<InstanceType<typeof TaktCropper> | null>(null)
const currentFile = ref<File | null>(null)

// 监听 modelValue 变化
watch(() => props.modelValue, (newValue) => {
  avatarUrl.value = newValue
}, { immediate: true })

// 监听 avatarUrl 变化
watch(avatarUrl, (newValue) => {
  emit('update:modelValue', newValue)
})

// 上传前的钩子
const handleBeforeUpload = (file: UploadFile | File) => {
  // 获取原生 File 对象
  const originFile = (file as UploadFile).originFileObj || (file as File)
  
  // 检查是否为图片文件
  if (!originFile.type?.startsWith('image/')) {
    message.error(t('components.common.upload.imageOnly'))
    return false
  }

  // 检查文件大小
  if (props.maxSize && originFile.size) {
    const fileSizeMB = originFile.size / 1024 / 1024
    if (fileSizeMB > props.maxSize) {
      message.error(t('components.common.upload.imageSizeExceed', { max: props.maxSize }))
      return false
    }
  }

  // 读取图片并显示裁剪弹窗
  const reader = new FileReader()
  reader.onload = (e) => {
    cropperImage.value = e.target?.result as string
    currentFile.value = originFile
    cropperVisible.value = true
  }
  reader.onerror = () => {
    message.error(t('components.common.upload.imageReadFail'))
  }
  reader.readAsDataURL(originFile)

  // 阻止自动上传，等待裁剪完成
  return false
}

// 裁剪器裁剪事件
const handleCropperCrop = (blob: Blob, dataUrl: string) => {
  // 裁剪完成，这里可以预览裁剪后的图片
  // dataUrl 可以用于预览，blob 用于实际上传
}

// 裁剪器错误事件
const handleCropperError = (error: Error) => {
  message.error(t('components.common.upload.cropFail') + '：' + error.message)
}

// 确认裁剪并上传
const handleCropConfirm = async () => {
  if (!cropperRef.value) {
    message.error(t('components.common.upload.cropperNotInit'))
    return
  }

  try {
    cropping.value = true

    // 获取裁剪后的图片
    const { blob, dataUrl } = await cropperRef.value.getCroppedImage()

    // 创建 File 对象
    const croppedFile = new File(
      [blob],
      currentFile.value?.name || `avatar-${Date.now()}.png`,
      { type: 'image/png' }
    )

    // 如果有自定义上传请求，使用自定义请求
    if (props.customRequest) {
      await new Promise<void>((resolve, reject) => {
        uploading.value = true

        props.customRequest!({
          file: croppedFile,
          onSuccess: (result: any) => {
            // 假设返回结果中有 url 字段
            const url = result?.url || result?.data?.url || dataUrl
            avatarUrl.value = url
            uploading.value = false
            cropperVisible.value = false
            cropperImage.value = ''
            currentFile.value = null
            message.success(t('components.common.upload.uploadSuccess'))
            emit('success', url, croppedFile)
            resolve()
          },
          onError: (error: any) => {
            uploading.value = false
            const err = error instanceof Error ? error : new Error(String(error))
            message.error(t('components.common.upload.uploadFail') + '：' + err.message)
            emit('error', err)
            reject(err)
          },
          onProgress: () => {
            // 进度处理
          }
        } as any)
      })
    } else if (props.action) {
      // 使用默认上传请求
      uploading.value = true

      const formData = new FormData()
      formData.append('file', croppedFile)

      try {
        const response = await fetch(props.action, {
          method: 'POST',
          body: formData
        })

        if (!response.ok) {
          throw new Error(`上传失败：${response.statusText}`)
        }

        const result = await response.json()
        const url = result?.url || result?.data?.url || dataUrl
        avatarUrl.value = url
        uploading.value = false
        cropperVisible.value = false
        cropperImage.value = ''
        currentFile.value = null
        message.success(t('components.common.upload.uploadSuccess'))
        emit('success', url, croppedFile)
      } catch (error: any) {
        uploading.value = false
        const err = error instanceof Error ? error : new Error(String(error))
        message.error(t('components.common.upload.uploadFail') + '：' + err.message)
        emit('error', err)
      }
    } else {
      // 没有上传配置，直接使用 base64
      avatarUrl.value = dataUrl
      cropperVisible.value = false
      cropperImage.value = ''
      currentFile.value = null
      message.success(t('components.common.upload.cropSuccess'))
      emit('success', dataUrl, croppedFile)
    }
  } catch (error: any) {
    console.error('[TaktUploadAvatar] 裁剪上传失败:', error)
    const err = error instanceof Error ? error : new Error(String(error))
    message.error(t('components.common.upload.operationFail') + '：' + err.message)
    emit('error', err)
  } finally {
    cropping.value = false
  }
}

// 取消裁剪
const handleCropCancel = () => {
  cropperVisible.value = false
  cropperImage.value = ''
  currentFile.value = null
}

// 自定义上传请求（如果不提供，使用默认的）
const handleCustomRequest = (options: any) => {
  // 这个函数实际上不会被调用，因为我们在 beforeUpload 中返回了 false
  // 但为了完整性，这里提供一个实现
  if (props.customRequest) {
    props.customRequest(options)
  }
}

// 删除头像
const handleRemove = () => {
  Modal.confirm({
    title: t('components.common.upload.confirmDeleteAvatar'),
    content: t('components.common.upload.confirmDeleteAvatarContent'),
    okText: t('components.common.upload.ok'),
    cancelText: t('components.common.upload.cancel'),
    onOk: () => {
      avatarUrl.value = ''
      message.success(t('components.common.upload.deleteSuccess'))
      emit('remove')
    }
  })
}
</script>

<style scoped lang="less">
.takt-upload-avatar {
  .takt-upload-avatar-preview {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 16px;
    
    .takt-upload-avatar-img {
      border: 2px solid #d9d9d9;
      cursor: pointer;
      transition: all 0.3s;
      
      &:hover {
        border-color: #40a9ff;
      }
    }
    
    .takt-upload-avatar-actions {
      display: flex;
      gap: 8px;
    }
  }
}

.takt-upload-avatar-cropper-modal {
  :deep(.ant-modal-body) {
    padding: 24px;
  }
}
</style>
