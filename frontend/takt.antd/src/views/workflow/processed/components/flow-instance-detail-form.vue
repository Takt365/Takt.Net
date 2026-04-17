<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/processed/components -->
<!-- 文件名称：flow-instance-detail-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：已办列表中流程实例详情展示组件，展示实例编码、状态、当前节点、流转历史等（只读，由父级 TaktModal 包裹） -->
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
          v-for="(h, i) in detail.history"
          :key="i"
          class="history-item"
        >
          {{ h.fromNodeName }} → {{ h.toNodeName }}（{{ h.transitionUserName }}，{{ h.transitionTime }}）
          <span v-if="h.transitionComment">：{{ h.transitionComment }}</span>
        </div>
        <span v-if="!detail.history?.length">{{ t('workflow.instance.noHistory') }}</span>
      </a-descriptions-item>
    </a-descriptions>
    <FlowPendingAddApproversPanel
      :detail="detail"
      :allow-reduce="!!detail?.canVerify"
      @refresh="$emit('refresh')"
    />
  </div>
</template>

<script setup lang="ts">
/**
 * 已办·实例详情展示（只读）：实例编码、流程名、标题、状态、当前节点、发起人、发起时间、流转历史；未处理加签与减签。
 */
import { useI18n } from 'vue-i18n'
import type { FlowInstanceDetail } from '@/types/workflow/instance'
import FlowPendingAddApproversPanel from '@/views/workflow/components/flow-pending-add-approvers-panel.vue'

/** 父组件传入的实例详情 */
interface Props {
  detail: FlowInstanceDetail | null
}

defineProps<Props>()
defineEmits<{ refresh: [] }>()

const { t } = useI18n()

/** 实例状态码转展示文案（走 i18n workflow.instance.status.*） */
function statusText(s: number) {
  return t(`workflow.instance.status.${s}`) || t('workflow.instance.status.unknown')
}
</script>

<style scoped lang="less">
.history-item {
  font-size: 12px;
  margin-bottom: 4px;
}
</style>
