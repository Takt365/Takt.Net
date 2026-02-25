<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/scheme/components -->
<!-- 文件名称：bpmn-modeler.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：BPMN 2.0 流程设计器（基于 bpmn-js），流程绘制、工具栏（缩放/适应/清空/导出）、属性面板（节点与连线名称，与后端 TaktBpmnParser 一致），参照 OpenAuth 重新绘制与下载流程图 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="bpmn-modeler-wrap">
    <div class="bpmn-modeler-main">
      <div class="bpmn-toolbar">
        <a-space :size="8">
          <a-button size="small" @click="zoomIn">{{ t('workflow.scheme.zoomIn') }}</a-button>
          <a-button size="small" @click="zoomOut">{{ t('workflow.scheme.zoomOut') }}</a-button>
          <a-button size="small" @click="zoomFit">{{ t('workflow.scheme.zoomFit') }}</a-button>
          <a-button size="small" :disabled="!canUndo" @click="undo">{{ t('workflow.scheme.undo') }}</a-button>
          <a-button size="small" :disabled="!canRedo" @click="redo">{{ t('workflow.scheme.redo') }}</a-button>
          <a-button size="small" @click="handleClearCanvas">{{ t('workflow.scheme.clearCanvas') }}</a-button>
          <a-button size="small" @click="handleExportSvg">{{ t('workflow.scheme.exportSvg') }}</a-button>
        </a-space>
      </div>
      <div ref="containerRef" class="bpmn-modeler-container" />
    </div>
    <div class="bpmn-properties-panel">
      <div class="bpmn-properties-title">{{ t('workflow.scheme.elementProperties') }}</div>
      <div v-if="selectedElement" class="bpmn-properties-body">
        <div class="bpmn-property-item">
          <label>{{ t('workflow.scheme.elementId') }}</label>
          <input
            :value="selectedId"
            class="bpmn-property-input bpmn-property-input-readonly"
            readonly
            disabled
          />
        </div>
        <div class="bpmn-property-item">
          <label>{{ t('workflow.scheme.elementName') }}</label>
          <input
            :value="selectedName"
            class="bpmn-property-input"
            :placeholder="t('workflow.scheme.elementNamePlaceholder')"
            @input="onNameInput"
          />
        </div>
        <p v-if="selectedElementType" class="bpmn-property-type">{{ selectedElementType }}</p>
        <p v-if="isSequenceFlow" class="bpmn-property-hint">{{ t('workflow.scheme.flowNameHint') }}</p>
        <template v-if="isSequenceFlow">
          <div class="bpmn-property-item">
            <label>{{ t('workflow.scheme.conditionExpression') }}</label>
            <input
              :value="selectedConditionExpression"
              class="bpmn-property-input"
              :placeholder="t('workflow.scheme.conditionExpressionPlaceholder')"
              @input="onConditionExpressionInput"
            />
          </div>
        </template>
      </div>
      <div v-else class="bpmn-properties-empty">
        {{ t('workflow.scheme.selectElementHint') }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { Modal } from 'ant-design-vue'
// 官方 walkthrough — Add Stylesheets
import 'bpmn-js/dist/assets/diagram-js.css'
import 'bpmn-js/dist/assets/bpmn-font/css/bpmn.css'
import BpmnModeler from 'bpmn-js/lib/Modeler'

/** bpmn-js 图形元素（含 businessObject，与后端 TaktBpmnParser 使用的 id/name/conditionExpression 一致） */
interface BpmnShapeLike {
  businessObject?: { id?: string; name?: string; $type?: string; conditionExpression?: { body?: string } }
}

/** bpmn-js canvas 服务（zoom 等） */
interface CanvasLike {
  zoom(level?: number | 'fit-viewport'): number
}

const { t } = useI18n()

const props = withDefaults(
  defineProps<{
    /** 初始 BPMN XML，空则使用内置空白图 */
    xml?: string
  }>(),
  { xml: '' }
)

const emit = defineEmits<{
  (e: 'save', xml: string): void
}>()

const containerRef = ref<HTMLElement | null>(null)
const selectedElement = ref<BpmnShapeLike | null>(null)
/** 元素 ID，与后端 TaktBpmnParser / TaktFlowNode.Id、TaktFlowLine sourceRef/targetRef 一致 */
const selectedId = ref('')
const selectedName = ref('')
const selectedElementType = ref('')
/** 仅当选中顺序流时有值，与后端 TaktBpmnParser 使用的 conditionExpression.body 一致 */
const selectedConditionExpression = ref('')
const canUndo = ref(false)
const canRedo = ref(false)

let modeler: InstanceType<typeof BpmnModeler> | null = null

const isSequenceFlow = computed(() => {
  const bo = selectedElement.value?.businessObject
  return bo?.$type === 'bpmn:SequenceFlow'
})

/** 与后端 TaktBpmnParser 支持的节点类型对应 */
function getElementType(businessObject: { $type?: string } | undefined): string {
  if (!businessObject) return ''
  const local = businessObject.$type ?? ''
  const map: Record<string, string> = {
    'bpmn:StartEvent': t('workflow.scheme.typeStartEvent'),
    'bpmn:UserTask': t('workflow.scheme.typeUserTask'),
    'bpmn:EndEvent': t('workflow.scheme.typeEndEvent'),
    'bpmn:ExclusiveGateway': t('workflow.scheme.typeExclusiveGateway'),
    'bpmn:SequenceFlow': t('workflow.scheme.typeSequenceFlow')
  }
  return map[local] ?? local
}

function bindSelection() {
  if (!modeler) return
  const eventBus = modeler.get('eventBus') as { on: (event: string, handler: (e: { newSelection?: BpmnShapeLike[] }) => void) => void }
  eventBus.on('selection.changed', (e: { newSelection?: BpmnShapeLike[] }) => {
    const newElement = e.newSelection?.[0] ?? null
    selectedElement.value = newElement
    if (newElement?.businessObject) {
      const bo = newElement.businessObject
      selectedId.value = bo.id ?? ''
      selectedName.value = bo.name ?? ''
      selectedElementType.value = getElementType(bo)
      selectedConditionExpression.value =
        bo.$type === 'bpmn:SequenceFlow' && bo.conditionExpression
          ? (bo.conditionExpression.body ?? '')
          : ''
    } else {
      selectedId.value = ''
      selectedName.value = ''
      selectedElementType.value = ''
      selectedConditionExpression.value = ''
    }
  })
}

function onNameInput(ev: Event) {
  const value = (ev.target as HTMLInputElement).value
  selectedName.value = value
  if (!modeler || !selectedElement.value) return
  const modeling = modeler.get('modeling') as {
    updateLabel: (el: BpmnShapeLike, text: string) => void
    updateProperties: (el: BpmnShapeLike, props: Record<string, unknown>) => void
  }
  modeling.updateLabel(selectedElement.value, value)
  const bo = selectedElement.value.businessObject
  if (bo && 'name' in bo) {
    modeling.updateProperties(selectedElement.value, { name: value })
  }
}

/** 顺序流条件表达式（写入 BPMN conditionExpression，与后端 InferCondition 一致） */
function onConditionExpressionInput(ev: Event) {
  const value = (ev.target as HTMLInputElement).value.trim()
  selectedConditionExpression.value = value
  if (!modeler || !selectedElement.value) return
  const bo = selectedElement.value.businessObject
  if (!bo || bo.$type !== 'bpmn:SequenceFlow') return
  const moddle = modeler.get('moddle') as {
    create: (type: string, attrs: { body: string }) => { body: string }
  }
  const modeling = modeler.get('modeling') as {
    updateProperties: (el: BpmnShapeLike, props: Record<string, unknown>) => void
  }
  const conditionExpression = value
    ? moddle.create('bpmn:FormalExpression', { body: value })
    : undefined
  modeling.updateProperties(selectedElement.value, { conditionExpression })
}

function getCanvas(): CanvasLike | null {
  if (!modeler) return null
  return modeler.get('canvas') as CanvasLike
}

function zoomIn() {
  const canvas = getCanvas()
  if (!canvas) return
  const zoom = canvas.zoom()
  canvas.zoom(zoom + 0.25)
}

function zoomOut() {
  const canvas = getCanvas()
  if (!canvas) return
  const zoom = canvas.zoom()
  canvas.zoom(Math.max(0.2, zoom - 0.25))
}

function zoomFit() {
  getCanvas()?.zoom('fit-viewport')
}

function updateUndoRedoState() {
  if (!modeler) return
  const commandStack = modeler.get('commandStack') as { canUndo: () => boolean; canRedo: () => boolean } | undefined
  if (commandStack) {
    canUndo.value = commandStack.canUndo()
    canRedo.value = commandStack.canRedo()
  }
}

function undo() {
  (modeler?.get('commandStack') as { undo: () => void } | undefined)?.undo()
  updateUndoRedoState()
}

function redo() {
  (modeler?.get('commandStack') as { redo: () => void } | undefined)?.redo()
  updateUndoRedoState()
}

/** 清空画布并重新绘制（参照 OpenAuth 重新绘制），需用户确认 */
function handleClearCanvas() {
  if (!modeler) return
  Modal.confirm({
    title: t('workflow.scheme.clearCanvas'),
    content: t('workflow.scheme.clearCanvasConfirm'),
    okText: t('workflow.scheme.confirm'),
    cancelText: t('common.cancel'),
    onOk: async () => {
      selectedElement.value = null
      selectedId.value = ''
      selectedName.value = ''
      selectedElementType.value = ''
      selectedConditionExpression.value = ''
      await modeler!.createDiagram()
      await nextTick()
      getCanvas()?.zoom('fit-viewport')
    }
  })
}

/** 导出当前画布为 SVG 文件（参照 OpenAuth 下载流程图） */
function handleExportSvg() {
  const container = containerRef.value
  if (!container) return
  const svg = container.querySelector('svg')
  if (!svg) return
  const serializer = new XMLSerializer()
  let str = serializer.serializeToString(svg)
  if (!str.includes('xmlns=')) {
    str = str.replace('<svg ', '<svg xmlns="http://www.w3.org/2000/svg" ')
  }
  const blob = new Blob([str], { type: 'image/svg+xml;charset=utf-8' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = `${t('workflow.scheme.exportSvgFilename')}_${Date.now()}.svg`
  a.click()
  URL.revokeObjectURL(url)
}

async function initModeler() {
  if (!containerRef.value) return
  modeler = new BpmnModeler({ container: containerRef.value })
  const xmlToLoad = props.xml?.trim()
  if (xmlToLoad) {
    try {
      await modeler.importXML(xmlToLoad)
    } catch {
      await modeler.createDiagram()
    }
  } else {
    await modeler.createDiagram()
  }
  await nextTick()
  bindSelection()
  getCanvas()?.zoom('fit-viewport')
  const eventBus = modeler.get('eventBus') as { on: (e: string, fn: () => void) => void }
  eventBus.on('commandStack.changed', updateUndoRedoState)
  updateUndoRedoState()
}

watch(
  () => props.xml,
  async (newXml) => {
    if (!modeler || !containerRef.value) return
    selectedElement.value = null
    selectedId.value = ''
    selectedName.value = ''
    selectedElementType.value = ''
    selectedConditionExpression.value = ''
    const xmlToLoad = newXml?.trim()
    if (xmlToLoad) {
      try {
        await modeler.importXML(xmlToLoad)
      } catch {
        await modeler.createDiagram()
      }
    } else {
      await modeler.createDiagram()
    }
    getCanvas()?.zoom('fit-viewport')
  }
)

onMounted(() => {
  initModeler()
})

onBeforeUnmount(() => {
  modeler?.destroy()
  modeler = null
})

/**
 * 获取当前图的 BPMN XML，供父组件保存（与后端 TaktFlowScheme.BpmnXml 一致）
 */
async function getXml(): Promise<string> {
  if (!modeler) return ''
  const result = await modeler.saveXML({ format: true })
  return result.xml ?? ''
}

defineExpose({ getXml })
</script>

<style scoped>
.bpmn-modeler-wrap {
  display: flex;
  width: 100%;
  height: 100%;
  min-height: 400px;
}
.bpmn-modeler-main {
  flex: 1;
  min-width: 0;
  display: flex;
  flex-direction: column;
  min-height: 400px;
}
.bpmn-toolbar {
  flex-shrink: 0;
  padding: 8px 12px;
  border-bottom: 1px solid var(--ant-color-border);
  background: var(--ant-color-bg-container);
}
.bpmn-modeler-container {
  flex: 1;
  min-height: 0;
}
.bpmn-properties-panel {
  width: 280px;
  flex-shrink: 0;
  border-left: 1px solid var(--ant-color-border);
  background: var(--ant-color-bg-container);
  display: flex;
  flex-direction: column;
}
.bpmn-properties-title {
  padding: 12px;
  font-weight: 600;
  border-bottom: 1px solid var(--ant-color-border);
  color: var(--ant-color-text);
}
.bpmn-properties-body {
  padding: 12px;
}
.bpmn-properties-empty {
  padding: 24px 12px;
  font-size: 12px;
  text-align: center;
  color: var(--ant-color-text-tertiary);
}
.bpmn-property-item {
  margin-bottom: 12px;
}
.bpmn-property-item label {
  display: block;
  margin-bottom: 4px;
  font-size: 12px;
  color: var(--ant-color-text-secondary);
}
.bpmn-property-input {
  width: 100%;
  padding: 6px 8px;
  border: 1px solid var(--ant-color-border);
  border-radius: 4px;
  box-sizing: border-box;
  background: var(--ant-color-bg-container);
  color: var(--ant-color-text);
}
.bpmn-property-input-readonly {
  cursor: default;
  background: var(--ant-color-fill-tertiary);
  color: var(--ant-color-text-secondary);
}
.bpmn-property-type {
  margin: 8px 0 0;
  font-size: 12px;
  color: var(--ant-color-text-tertiary);
}
.bpmn-property-hint {
  margin: 8px 0 0;
  font-size: 12px;
  color: var(--ant-color-primary);
}
</style>
