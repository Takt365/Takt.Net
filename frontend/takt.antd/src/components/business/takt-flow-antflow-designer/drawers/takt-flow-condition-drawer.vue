<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-flow-antflow-designer/drawers -->
<!-- 文件名称：takt-flow-condition-drawer.vue -->
<!-- 创建时间：2026-04-07 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：条件分支条件表达式编辑抽屉 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->
<template>
  <a-drawer
    v-model:open="visible"
    title="条件设置"
    width="480"
    :footer-style="{ textAlign: 'right' }"
    @close="handleClose"
  >
    <template v-if="config">
      <a-form layout="vertical">
        <a-form-item label="条件名称">
          <a-input
            v-model:value="form.nodeName"
            placeholder="如：条件1"
          />
        </a-form-item>
        <a-form-item :label="t('workflow.designer.edgeCondition')">
          <a-textarea
            v-model:value="form.conditionExpr"
            :placeholder="t('workflow.designer.placeholderEdgeCondition')"
            :rows="3"
          />
        </a-form-item>
      </a-form>
    </template>
    <template #footer>
      <a-button
        style="margin-right: 8px"
        @click="handleClose"
      >
        取消
      </a-button>
      <a-button
        type="primary"
        @click="handleSave"
      >
        确定
      </a-button>
    </template>
  </a-drawer>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useFlowDrawer } from '../config/takt-flow-use-flow-drawer'
import type { FlowTreeNode } from '../config/takt-flow-tree'

const { t } = useI18n()
const { state, save: saveDrawer, close } = useFlowDrawer()

const visible = computed({
  get: () => !!(state.visible && state.type === 'condition'),
  set: (v) => { if (!v) close() }
})
const config = ref<FlowTreeNode | null>(null)
const form = ref({ nodeName: '', conditionExpr: '' })

function conditionListFromExpr(expr: string): { showName?: string; optType?: string; zdy1?: string }[] {
  if (!expr?.trim()) return []
  const m = expr.trim().match(/^(\S+)\s*(<|>|<=|>=|==)\s*(.+)$/)
  if (!m) return []
  const [, key, op, val] = m
  const optMap: Record<string, string> = { '<': '1', '>': '2', '>=': '4', '<=': '5', '==': '3' }
  return [{ showName: key?.replace(/_/g, ' '), optType: optMap[op ?? ''] ?? '3', zdy1: val?.trim() }]
}

watch(
  () => state.visible && state.type === 'condition',
  (v) => {
    if (v && state.config) {
      config.value = state.config
      const list = state.config.conditionList ?? []
      const first = list[0]
      const expr = first?.zdy1 != null && first?.showName
        ? `${first.showName.replace(/\s/g, '_')} ${(first.optType ?? '') === '1' ? '<' : (first.optType ?? '') === '2' ? '>' : (first.optType ?? '') === '4' ? '>=' : (first.optType ?? '') === '5' ? '<=' : '=='} ${first.zdy1}`
        : ''
      form.value = { nodeName: state.config.nodeName ?? '', conditionExpr: expr }
    } else {
      config.value = null
    }
  },
  { immediate: true }
)

function handleSave() {
  if (!config.value) return
  const conditionList = conditionListFromExpr(form.value.conditionExpr)
  const updated: FlowTreeNode = {
    ...config.value,
    nodeName: form.value.nodeName || config.value.nodeName,
    conditionList: conditionList.length ? conditionList : (config.value.conditionList ?? [])
  }
  saveDrawer(updated)
}

function handleClose() {
  close()
}
</script>
