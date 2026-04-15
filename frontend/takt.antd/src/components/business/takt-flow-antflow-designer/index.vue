<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-flow-antflow-designer -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2026-04-07 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：AntFlow 风格流程设计器入口（画布缩放、流程树、流程结束、发起人/审批/抄送/条件抽屉） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->
<template>
  <div class="takt-flow-antflow-designer" :class="{ 'takt-flow-antflow-designer--dark': isDark }">
    <section class="dingflow-design" ref="dingflowRef">
      <div class="zoom-bar">
        <a-button type="link" size="small" class="zoom-bar__validate" @click="runValidateDesign">
          {{ t('workflow.designer.validateFlow') }}
        </a-button>
        <span class="zoom-bar__sep" aria-hidden="true" />
        <span class="zoom-out" title="缩小" @click="zoomOut" />
        <span class="zoom-val">{{ nowVal }}%</span>
        <span class="zoom-in" title="放大" @click="zoomIn" />
        <span class="zoom-reset" title="还原缩放" @click="zoomReset">↺</span>
      </div>
      <div class="box-scale" ref="boxScaleRef">
        <div v-if="nodeConfig" class="flow-canvas-chain">
          <TaktFlowNodeWrap
            v-model:node-config="nodeConfig"
            :drawer="drawer"
          />
          <div class="end-connector" aria-hidden="true" />
          <div class="end-node">
            <div class="end-node-block">
              <span class="end-node-text">{{ t('workflow.designer.endNode') }}</span>
            </div>
          </div>
        </div>
        <div v-else class="start-empty">
          <a-button type="primary" @click="initStart">从发起人开始</a-button>
        </div>
      </div>
    </section>
    <TaktFlowPromoterDrawer />
    <TaktFlowApproverDrawer />
    <TaktFlowCopyerDrawer />
    <TaktFlowConditionDrawer />
    <TaktFlowErrorDialog v-model:open="errorDialogOpen" :rows="errorDialogRows" />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { useThemeStore } from '@/stores/theme'
import TaktFlowNodeWrap from './node/takt-flow-node-wrap.vue'
import TaktFlowPromoterDrawer from './drawers/takt-flow-promoter-drawer.vue'
import TaktFlowApproverDrawer from './drawers/takt-flow-approver-drawer.vue'
import TaktFlowCopyerDrawer from './drawers/takt-flow-copyer-drawer.vue'
import TaktFlowConditionDrawer from './drawers/takt-flow-condition-drawer.vue'
import TaktFlowErrorDialog from './dialog/takt-flow-error-dialog.vue'
import type { TaktFlowErrorRow } from './dialog/takt-flow-error-dialog.vue'
import { collectFlowDesignErrors } from './config/takt-flow-design-validate'
import { useFlowDrawer } from './config/takt-flow-use-flow-drawer'
import { createStartNode } from './config/takt-flow-tree'
import { treeToGraph, graphToTree } from './config/takt-flow-tree-graph-convert'
import { zoomInit, resetImage, wheelZoomFunc } from './config/takt-flow-zoom'
import type { GraphNode, GraphEdge } from './config/takt-flow-tree-graph-convert'
import type { FlowTreeNode } from './config/takt-flow-tree'

/** 深拷贝流程树，避免与父级 modelValue 共享引用导致联动覆盖 */
function cloneFlowTree(tree: FlowTreeNode): FlowTreeNode {
  return JSON.parse(JSON.stringify(tree)) as FlowTreeNode
}

const { t } = useI18n()
const themeStore = useThemeStore()
const isDark = computed(() => themeStore.themeMode === 'dark')
const { state: drawerState, open: openDrawer, close: closeDrawer, save: saveDrawer } = useFlowDrawer()

const props = withDefaults(
  defineProps<{ modelValue?: string; readonly?: boolean }>(),
  { modelValue: '', readonly: false }
)
const emit = defineEmits<{ 'update:modelValue': [value: string] }>()

const dingflowRef = ref<HTMLElement | null>(null)
const boxScaleRef = ref<HTMLElement | null>(null)
const nodeConfig = ref<FlowTreeNode | null>(null)
const nowVal = ref(100)

const errorDialogOpen = ref(false)
const errorDialogRows = ref<TaktFlowErrorRow[]>([])

function runValidateDesign() {
  validateDesign()
}

/**
 * 校验流程树配置；无问题返回 true，有问题时打开错误弹窗并返回 false。
 * @param options.silent 为 true 时不弹出成功提示（供方案保存前校验）
 */
function validateDesign(options?: { silent?: boolean }): boolean {
  if (!nodeConfig.value) {
    message.warning(t('workflow.designer.validateNoCanvas'))
    return false
  }
  const rows = collectFlowDesignErrors(nodeConfig.value, t)
  if (!rows.length) {
    if (!options?.silent) message.success(t('workflow.designer.validateFlowOk'))
    return true
  }
  errorDialogRows.value = rows
  errorDialogOpen.value = true
  return false
}

const drawer = {
  open: openDrawer,
  close: closeDrawer,
  save: saveDrawer,
  state: drawerState
}

/** 与后端 camelCase / 少数场景 PascalCase、数字或字符串 1 兼容，用于识别设计器根「发起人」节点 */
function flowTreeRootType(ft: unknown): number | undefined {
  if (ft == null || typeof ft !== 'object') return undefined
  const o = ft as Record<string, unknown>
  const v = o.nodeType ?? o.NodeType
  if (typeof v === 'number' && !Number.isNaN(v)) return v
  if (typeof v === 'string' && v.trim() !== '') {
    const n = Number(v)
    return Number.isNaN(n) ? undefined : n
  }
  return undefined
}

function isDesignerFlowTreeRoot(ft: unknown): ft is FlowTreeNode {
  return flowTreeRootType(ft) === 1
}

/**
 * 解析 ProcessContent 字符串。若库中误存两段 JSON 直连（...}{...，中间无逗号），整段非法；
 * 此时只取第一段解析，至少能还原前一条流程（根因应在入库侧避免拼接，见种子/保存逻辑）。
 */
function parseLenientProcessContentJson(val: string): unknown | null {
  const trimmed = val.trim()
  try {
    return JSON.parse(trimmed)
  } catch {
    const cut = trimmed.indexOf('}{')
    if (cut >= 0) {
      try {
        return JSON.parse(trimmed.slice(0, cut + 1))
      } catch {
        return null
      }
    }
    return null
  }
}

/**
 * 将 ProcessContent 解析为画布数据；解析异常时返回空图（与业务保存前校验分离）。
 * 合法性应与后端一致：写入/回填前用 `@/utils/workflow/validate-process-content`（流程方案见 scheme/index.vue）。
 */
function parseProcessContent(val: string): { nodes: GraphNode[]; edges: GraphEdge[]; flowTree?: FlowTreeNode | null } {
  if (!val?.trim()) return { nodes: [], edges: [] }
  try {
    let parsed: unknown = parseLenientProcessContentJson(val)
    if (parsed == null) return { nodes: [], edges: [] }
    if (typeof parsed === 'string' && parsed.trim()) {
      const inner = parseLenientProcessContentJson(parsed)
      if (inner != null) parsed = inner
    }
    if (parsed == null || typeof parsed !== 'object') return { nodes: [], edges: [] }
    const obj = parsed as { nodes?: unknown[]; edges?: unknown[]; flowTree?: unknown; FlowTree?: unknown }
    const nodes = (Array.isArray(obj.nodes) ? obj.nodes : []) as GraphNode[]
    const edges = (Array.isArray(obj.edges) ? obj.edges : []) as GraphEdge[]
    const rawFt = obj.flowTree ?? obj.FlowTree
    const flowTree =
      rawFt != null && typeof rawFt === 'object' && isDesignerFlowTreeRoot(rawFt) ? (rawFt as FlowTreeNode) : undefined
    return { nodes, edges, flowTree }
  } catch {
    return { nodes: [], edges: [] }
  }
}

/**
 * 输出仍含 nodes/edges 供引擎使用；另存 flowTree 作为设计器权威结构。
 * 网关经纯 nodes/edges 往返会丢失空分支/汇流子树（见 takt-flow-tree-graph-convert.graphToTree）。
 */
function toProcessContent(tree: FlowTreeNode | null): string {
  if (!tree) return JSON.stringify({ nodes: [], edges: [] })
  const { nodes, edges } = treeToGraph(tree)
  return JSON.stringify({ nodes, edges, flowTree: tree })
}

function initStart() {
  nodeConfig.value = createStartNode(null)
  emitUpdate()
}

function emitUpdate() {
  if (nodeConfig.value) emit('update:modelValue', toProcessContent(nodeConfig.value))
}

let fromModelValue = false
watch(
  () => props.modelValue,
  (val) => {
    fromModelValue = true
    const { nodes, edges, flowTree } = parseProcessContent(val ?? '')
    if (flowTree != null && isDesignerFlowTreeRoot(flowTree)) {
      nodeConfig.value = cloneFlowTree(flowTree)
      return
    }
    const tree = graphToTree(nodes, edges)
    if (tree) nodeConfig.value = tree
    else if (!nodeConfig.value) nodeConfig.value = null
  },
  { immediate: true }
)

watch(
  nodeConfig,
  () => {
    if (fromModelValue) {
      fromModelValue = false
      return
    }
    emitUpdate()
  },
  { deep: true }
)

function zoomIn() {
  wheelZoomFunc({ scaleFactor: parseInt(String(nowVal.value), 10) / 100 + 0.1, isExternalCall: true })
}
function zoomOut() {
  wheelZoomFunc({ scaleFactor: parseInt(String(nowVal.value), 10) / 100 - 0.1, isExternalCall: true })
}
function zoomReset() {
  resetImage()
  nowVal.value = 100
}

defineExpose({ validateDesign })

onMounted(() => {
  if (!nodeConfig.value && props.modelValue?.trim()) {
    const { nodes, edges, flowTree } = parseProcessContent(props.modelValue)
    if (flowTree != null && isDesignerFlowTreeRoot(flowTree)) {
      nodeConfig.value = cloneFlowTree(flowTree)
    } else {
      const tree = graphToTree(nodes, edges)
      if (tree) nodeConfig.value = tree
    }
  }
  zoomInit(dingflowRef, boxScaleRef, (val) => {
    nowVal.value = typeof val === 'number' ? val : parseInt(String(val), 10)
  })
})
</script>

<style lang="less">
@import './styles/workflow.less';
</style>
