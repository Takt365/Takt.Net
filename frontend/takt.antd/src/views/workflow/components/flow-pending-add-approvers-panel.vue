<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/components -->
<!-- 文件名称：flow-pending-add-approvers-panel.vue -->
<!-- 功能描述：展示未处理加签列表；可选减签（与实例详情 pendingAddApprovers、reduce-sign API 对应） -->
<!-- ======================================== -->
<template>
  <div v-if="detail?.pendingAddApprovers?.length" class="flow-pending-add">
    <div class="flow-pending-add__title">{{ t('workflow.instance.pendingAddApproversTitle') }}</div>
    <div
      v-for="p in detail.pendingAddApprovers"
      :key="p.addApproverId"
      class="flow-pending-add__row"
    >
      <span class="flow-pending-add__name">{{ p.approverUserName }}</span>
      <a-button
        v-if="allowReduce"
        type="link"
        size="small"
        :loading="loadingId === p.addApproverId"
        @click="onReduce(p.addApproverId)"
      >
        {{ t('workflow.instance.reduceSign') }}
      </a-button>
    </div>
  </div>
</template>

<script setup lang="ts">
/**
 * 未处理加签列表；allowReduce 为 true 时显示减签（需 detail.canVerify 由父级控制）。
 */
import { ref } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { reduceApproval } from '@/api/workflow/instance'
import type { FlowInstanceDetail } from '@/types/workflow/instance'

const { t } = useI18n()

const props = withDefaults(
  defineProps<{ detail: FlowInstanceDetail | null; allowReduce?: boolean }>(),
  { allowReduce: false }
)
const emit = defineEmits<{ refresh: [] }>()

const loadingId = ref<string | null>(null)

async function onReduce(addApproverId: string) {
  const d = props.detail
  if (!d?.instanceId) return
  loadingId.value = addApproverId
  try {
    await reduceApproval({
      flowInstanceId: d.instanceId,
      instanceCode: d.instanceCode,
      addApproverId
    })
    message.success(t('workflow.instance.reduceSignSuccess'))
    emit('refresh')
  } catch (e: unknown) {
    const err = e as { message?: string }
    message.error(err?.message || t('workflow.instance.reduceSignFail'))
  } finally {
    loadingId.value = null
  }
}
</script>

<style scoped lang="less">
.flow-pending-add {
  margin-top: 12px;
  padding-top: 12px;
  border-top: 1px solid var(--ant-color-border-secondary);
}
.flow-pending-add__title {
  font-weight: 600;
  margin-bottom: 8px;
  color: var(--ant-color-text);
}
.flow-pending-add__row {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 4px;
  font-size: 13px;
}
.flow-pending-add__name {
  min-width: 0;
}
</style>
