<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-modal -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：Takt 对话框组件，封装 a-modal，统一设置中文按钮文本 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-modal
    v-model:open="internalOpen"
    v-bind="modalProps"
    @cancel="handleCancel"
  >
    <template v-if="$slots.default" #default>
      <slot />
    </template>
    <template v-if="$slots.title" #title>
      <slot name="title" />
    </template>
    <template #footer>
      <slot name="footer">
        <div class="takt-modal-footer-default">
          <a-button @click="handleCancel" class="takt-modal-btn-cancel">
            <template #icon>
              <RiCloseLine />
            </template>
            {{ cancelTextDisplay }}
          </a-button>
          <a-button type="primary" @click="handleOk" class="takt-modal-btn-ok">
            <template #icon>
              <RiCheckLine />
            </template>
            {{ okTextDisplay }}
          </a-button>
        </div>
      </slot>
    </template>
    <template v-if="$slots.closeIcon" #closeIcon>
      <slot name="closeIcon" />
    </template>
  </a-modal>
</template>

<script setup lang="ts">
import { computed, useAttrs } from 'vue'
import { RiCloseLine, RiCheckLine } from '@remixicon/vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

interface Props {
  /** 确定按钮文本，默认为"提交" */
  okText?: string
  /** 取消按钮文本，默认为"取消" */
  cancelText?: string
  /** 是否显示对话框 */
  open?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  okText: undefined,
  cancelText: undefined,
  open: false
})

const okTextDisplay = computed(() => props.okText ?? t('common.button.submit'))
const cancelTextDisplay = computed(() => props.cancelText ?? t('common.button.cancel'))

const emit = defineEmits<{
  'update:open': [open: boolean]
  'ok': [e: MouseEvent]
  'cancel': [e: MouseEvent]
}>()

const attrs = useAttrs()

// 内部 open 状态
const internalOpen = computed({
  get: () => props.open,
  set: (value: boolean) => {
    emit('update:open', value)
  }
})

// 计算 modal 的所有属性，排除已定义的 props；统一默认弹出大小：视口 70% 宽、85% 高；默认垂直水平居中（与 a-modal 的 centered 一致）
const modalProps = computed(() => {
  const { okText, cancelText, open, centered, ...rest } = attrs
  const wrapClassName = [rest.wrapClassName, 'takt-modal-viewport-size'].filter(Boolean).join(' ')
  const width = rest.width !== undefined && rest.width !== null ? rest.width : '70vw'
  return {
    ...rest,
    width: width as string | number,
    wrapClassName,
    centered: centered !== undefined && centered !== null ? centered : true
  }
})

// 处理确定按钮点击
const handleOk = (e: MouseEvent) => {
  emit('ok', e)
}

// 处理取消按钮点击
const handleCancel = (e: MouseEvent) => {
  emit('cancel', e)
  emit('update:open', false)
}
</script>

<style scoped lang="less">
.takt-modal-footer-default {
  display: flex;
  justify-content: flex-end;
  gap: 8px;

  :deep(.ant-btn) {
    display: inline-flex;
    align-items: center;
    gap: 4px;

    .anticon {
      margin-inline-end: 0 !important;
    }
  }
}
</style>

<!-- 不 scoped：弹窗 teleport 到 body；宽度由内层 .ant-modal 的内联 width 决定，content 须 100% 填满，勿再写 70vw，否则会宽于父级导致内容视觉上偏右 -->
<style lang="less">
.takt-modal-viewport-size.ant-modal-wrap {
  display: flex;
  align-items: center;
  justify-content: center;
}
.takt-modal-viewport-size.ant-modal-wrap .ant-modal {
  top: 0;
  margin: 0;
  padding-bottom: 0;
}
.takt-modal-viewport-size.ant-modal-wrap .ant-modal-content {
  width: 100%;
  max-width: 100%;
  box-sizing: border-box;
  height: 85vh;
  min-width: 0;
  min-height: 360px;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}
.takt-modal-viewport-size.ant-modal-wrap .ant-modal-body {
  flex: 1;
  min-height: 0;
  overflow: auto;
}
.takt-modal-viewport-size.ant-modal-wrap .ant-modal-footer {
  flex-shrink: 0;
}
</style>
