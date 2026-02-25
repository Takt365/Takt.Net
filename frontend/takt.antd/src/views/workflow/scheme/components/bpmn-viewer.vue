<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/scheme/components -->
<!-- 文件名称：bpmn-viewer.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：BPMN 2.0 只读流程图查看器，支持高亮当前节点（流程执行时查看）、工具栏（缩放/适应）、空/加载/错误状态 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="bpmn-viewer-wrap">
    <div class="bpmn-viewer-main">
      <div class="bpmn-viewer-toolbar">
        <a-space>
          <a-button size="small" :disabled="!hasXml" @click="zoomIn">
            {{ t('workflow.scheme.zoomIn') }}
          </a-button>
          <a-button size="small" :disabled="!hasXml" @click="zoomOut">
            {{ t('workflow.scheme.zoomOut') }}
          </a-button>
          <a-button size="small" :disabled="!hasXml" @click="zoomFit">
            {{ t('workflow.scheme.zoomFit') }}
          </a-button>
        </a-space>
      </div>
      <div ref="containerRef" class="bpmn-viewer-container" />
      <div v-if="loading" class="bpmn-viewer-mask">
        <a-spin :spinning="true" />
      </div>
      <div v-else-if="loadError" class="bpmn-viewer-mask bpmn-viewer-mask-error">
        <a-result status="error" :title="t('workflow.scheme.diagramLoadFail')" />
      </div>
      <div v-else-if="!hasXml" class="bpmn-viewer-mask bpmn-viewer-mask-empty">
        <a-empty :description="t('workflow.scheme.diagramEmpty')" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted, onBeforeUnmount } from 'vue'
import { useI18n } from 'vue-i18n'
// 官方 walkthrough — Add Stylesheets
import 'bpmn-js/dist/assets/diagram-js.css'
import 'bpmn-js/dist/assets/bpmn-font/css/bpmn.css'
import BpmnViewer from 'bpmn-js/lib/Viewer'

const { t } = useI18n()

const props = withDefaults(
  defineProps<{
    /** BPMN 2.0 XML（与后端 TaktFlowScheme.BpmnXml 一致） */
    xml?: string
    /** 需要高亮的节点 ID（如当前活动节点 currentNodeId），对应 BPMN 元素 id */
    highlightNodeIds?: string[]
  }>(),
  { xml: '', highlightNodeIds: () => [] }
)

const hasXml = computed(() => !!props.xml?.trim())

const containerRef = ref<HTMLElement | null>(null)
const loading = ref(false)
const loadError = ref(false)

let viewer: InstanceType<typeof BpmnViewer> | null = null

function getCanvas(): { zoom: (v?: number | 'fit-viewport') => number; addMarker: (id: string, cls: string) => void; removeMarker: (id: string, cls: string) => void } | null {
  if (!viewer) return null
  return viewer.get('canvas') as { zoom: (v?: number | 'fit-viewport') => number; addMarker: (id: string, cls: string) => void; removeMarker: (id: string, cls: string) => void }
}

function applyHighlights() {
  const canvas = getCanvas()
  if (!canvas || !props.highlightNodeIds?.length) return
  const ids = props.highlightNodeIds
  ids.forEach((id) => {
    try {
      canvas.addMarker(id, 'bpmn-highlight-current')
    } catch {
      // 元素可能不存在（如已结束流程）
    }
  })
}

function clearHighlights() {
  const canvas = getCanvas()
  if (!canvas || !props.highlightNodeIds?.length) return
  props.highlightNodeIds.forEach((id) => {
    try {
      canvas.removeMarker(id, 'bpmn-highlight-current')
    } catch {
      // ignore
    }
  })
}

function zoomIn() {
  const canvas = getCanvas()
  if (!canvas) return
  const current = canvas.zoom()
  canvas.zoom(current + 0.25)
}

function zoomOut() {
  const canvas = getCanvas()
  if (!canvas) return
  const current = canvas.zoom()
  canvas.zoom(Math.max(0.2, current - 0.25))
}

function zoomFit() {
  const canvas = getCanvas()
  if (!canvas) return
  canvas.zoom('fit-viewport')
}

async function loadXml() {
  if (!containerRef.value || !viewer) return
  clearHighlights()
  const xml = props.xml?.trim()
  if (!xml) {
    loadError.value = false
    loading.value = false
    return
  }
  loading.value = true
  loadError.value = false
  try {
    await viewer.importXML(xml)
    getCanvas()?.zoom('fit-viewport')
    applyHighlights()
  } catch {
    loadError.value = true
  } finally {
    loading.value = false
  }
}

onMounted(async () => {
  if (!containerRef.value) return
  viewer = new BpmnViewer({ container: containerRef.value })
  await loadXml()
})

watch(
  () => [props.xml, props.highlightNodeIds],
  () => {
    loadXml()
  },
  { deep: true }
)

onBeforeUnmount(() => {
  viewer?.destroy()
  viewer = null
})
</script>

<style scoped>
.bpmn-viewer-wrap {
  width: 100%;
  height: 100%;
  min-height: 300px;
}
.bpmn-viewer-main {
  position: relative;
  width: 100%;
  height: 100%;
  min-height: 300px;
  display: flex;
  flex-direction: column;
}
.bpmn-viewer-toolbar {
  flex-shrink: 0;
  padding: 8px 12px;
  border-bottom: 1px solid #e8e8e8;
  background: #fafafa;
}
.bpmn-viewer-container {
  flex: 1;
  min-height: 0;
}
.bpmn-viewer-mask {
  position: absolute;
  inset: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(255, 255, 255, 0.7);
}
.bpmn-viewer-mask-error,
.bpmn-viewer-mask-empty {
  background: #fff;
}

/* 当前节点高亮（与后端 currentNodeId 对应，符合 BPMN 执行态展示） */
:deep(.bpmn-highlight-current:not(.djs-connection) .djs-visual > :nth-child(1)) {
  fill: #1890ff !important;
  stroke: #096dd9 !important;
  stroke-width: 2px;
}
:deep(.bpmn-highlight-current.djs-connection .djs-visual) {
  stroke: #1890ff !important;
  stroke-width: 3px;
}
</style>
