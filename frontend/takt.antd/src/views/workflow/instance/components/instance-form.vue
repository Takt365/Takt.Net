<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/instance/components -->
<!-- 文件名称：instance-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程实例详情展示组件，展示实例编码、状态、当前节点、流转历史等（只读） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div v-if="detail">
    <a-descriptions
      bordered
      size="small"
      :column="1"
    >
      <a-descriptions-item :label="t('entity.flowinstance.instancecode')">
        {{ detail.instanceCode }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('entity.flowinstance.processname')">
        {{ detail.processName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('entity.flowinstance.processtitle')">
        {{ detail.processTitle }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('entity.flowinstance.instancestatus')">
        {{ statusText(detail.instanceStatus) }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('entity.flowinstance.currentnodename')">
        {{ detail.currentNodeName || '-' }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('entity.flowinstance.startusername')">
        {{ detail.startUserName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('entity.flowinstance.starttime')">
        {{ detail.startTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('workflow.instance.transitionHistory')">
        <div
          v-for="(h, i) in historyItems"
          :key="i"
          class="history-item"
        >
          {{ h.fromNodeName }} -> {{ h.toNodeName }} ({{ h.transitionUserName }}, {{ h.transitionTime }})
          <span v-if="h.transitionComment">: {{ h.transitionComment }}</span>
        </div>
        <span v-if="!historyItems.length">{{ t('workflow.instance.noHistory') }}</span>
      </a-descriptions-item>
    </a-descriptions>
    <FlowPendingAddApproversPanel
      :detail="detail"
      :allow-reduce="!!detail?.canVerify"
      @refresh="$emit('refresh')"
    />
  </div>
  <div v-else>
    {{ t('workflow.instance.noData') }}
  </div>
</template>

<script setup lang="ts">
/**
 * 流程实例详情展示（只读）：实例编码、流程名、标题、状态、当前节点、发起人、发起时间、流转历史；未处理加签与减签。
 */
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FlowHistoryItem, FlowInstanceDetail } from '@/types/workflow/flow-instance'
import FlowPendingAddApproversPanel from '@/views/workflow/components/flow-pending-add-approvers-panel.vue'

const { t } = useI18n()
defineEmits<{ refresh: [] }>()

/** 父组件传入的实例详情，为 null 时显示“无数据” */
interface Props {
  detail: FlowInstanceDetail | null
}

const props = defineProps<Props>()
const historyItems = computed<FlowHistoryItem[]>(() => {
  const list = props.detail?.history
  if (!Array.isArray(list)) return []
  return list.filter((item): item is FlowHistoryItem => {
    if (!item || typeof item !== 'object') return false
    const row = item as Record<string, unknown>
    return typeof row.fromNodeName === 'string'
      && typeof row.toNodeName === 'string'
      && typeof row.transitionUserName === 'string'
      && typeof row.transitionTime === 'string'
  })
})

/** 实例状态码转展示文案（走 i18n workflow.instance.status.*） */
function statusText(s: number): string {
  return t(`workflow.instance.status.${s}`) || t('workflow.instance.status.unknown')
}
</script>

<style scoped lang="less">
.history-item {
  font-size: 12px;
  margin-bottom: 4px;
}
</style>

