<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/components/business/takt-flow-designer -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：BPMN 2.0 流程设计器（参考 vue-bpmn-flowable：自绘属性面板、selection.changed/element.changed、modeling.updateProperties；后端为 Takt 非 Flowable，仅标准 BPMN），布局：header 工具栏、左侧调色板、中间画布、右侧属性面板、footer 操作按钮 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="takt-flow-design">
    <input
      ref="fileInputRef"
      type="file"
      accept=".bpmn,application/xml,text/xml"
      class="takt-flow-design-file-input"
      @change="onBpmnFileChange"
    />
    <div v-if="initError" class="takt-flow-design-error">
      {{ t('workflow.scheme.designerLoadError') }}: {{ initError }}
    </div>
    <template v-else>
      <!-- header：工具栏 -->
      <header class="takt-flow-design-header">
        <a-space :size="8">
          <a-button size="small" @click="zoomIn">{{ t('workflow.scheme.zoomIn') }}</a-button>
          <a-button size="small" @click="zoomOut">{{ t('workflow.scheme.zoomOut') }}</a-button>
          <a-button size="small" @click="zoomFit">{{ t('workflow.scheme.zoomFit') }}</a-button>
          <a-button size="small" :disabled="!canUndo" @click="undo">{{ t('workflow.scheme.undo') }}</a-button>
          <a-button size="small" :disabled="!canRedo" @click="redo">{{ t('workflow.scheme.redo') }}</a-button>
          <a-button size="small" @click="handleClearCanvas">{{ t('workflow.scheme.clearCanvas') }}</a-button>
        </a-space>
      </header>
      <!-- body：左侧调色板+画布（bpmn-js 内）| 右侧 Camunda 属性面板 -->
      <div class="takt-flow-design-body">
        <div class="takt-flow-design-canvas-wrap">
          <div ref="containerRef" class="takt-flow-design-container djs-parent" />
        </div>
        <div
          ref="propertiesPanelRef"
          class="takt-flow-design-properties-panel"
          :class="{ 'takt-flow-design-panel-hide-root-header': isSelectedRootProcess }"
        />
      </div>
      <!-- footer：操作按钮（打开、下载 BPMN 2.0 文件、保存 SVG） -->
      <footer class="takt-flow-design-footer">
        <slot name="footer">
          <a-space :size="8">
            <a-button :title="t('workflow.scheme.openbpmn')" @click="handleOpenBpmn">
              <template #icon><FolderOpenOutlined /></template>
              {{ t('common.button.open') }}
            </a-button>
            <a-button :title="t('workflow.scheme.downloadBpmn')" @click="handleDownloadBpmn">
              <template #icon><DownloadOutlined /></template>
              {{ t('common.button.download') }}
            </a-button>
            <a-button :title="t('workflow.scheme.exportSvg')" @click="handleExportSvg">
              <template #icon><PictureOutlined /></template>
              {{ t('common.button.export') }}
            </a-button>
          </a-space>
        </slot>
      </footer>
    </template>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { useLocaleStore } from '@/stores/routine/localization/locale'
import { Modal, message } from 'ant-design-vue'
import { FolderOpenOutlined, DownloadOutlined, PictureOutlined } from '@ant-design/icons-vue'
import { logger } from '@/utils/logger'
import { DEFAULT_BPMN_XML, DEFAULT_MIN_HEIGHT, ELEMENT_TYPE_I18N_KEYS } from './config/config'
import BpmnModeler from 'bpmn-js/lib/Modeler'
import {
  BpmnPropertiesPanelModule,
  BpmnPropertiesProviderModule,
  CamundaPlatformPropertiesProviderModule
} from 'bpmn-js-properties-panel'
import camundaModdleDescriptor from 'camunda-bpmn-moddle/resources/camunda.json'
// 官方 walkthrough + 参照 bpmn-vue3：diagram-js / bpmn-font / bpmn-codes
import 'bpmn-js/dist/assets/diagram-js.css'
import 'bpmn-js/dist/assets/bpmn-font/css/bpmn.css'
import 'bpmn-js/dist/assets/bpmn-font/css/bpmn-codes.css'
import '@bpmn-io/properties-panel/assets/properties-panel.css'

/** bpmn-js 图形元素（含 businessObject，与后端 TaktBpmnParser 一致） */
interface BpmnShapeLike {
  businessObject?: {
    id?: string
    name?: string
    $type?: string
    conditionExpression?: { body?: string }
    isExecutable?: boolean
  }
}

const props = withDefaults(
  defineProps<{
    xml?: string
    minHeight?: number
  }>(),
  { xml: '', minHeight: DEFAULT_MIN_HEIGHT }
)

const emit = defineEmits<{
  (e: 'open'): void
  (e: 'loaded', xml: string): void
}>()

const { t } = useI18n()
const { locale } = storeToRefs(useLocaleStore())

const containerRef = ref<HTMLElement | null>(null)
const propertiesPanelRef = ref<HTMLElement | null>(null)
const fileInputRef = ref<HTMLInputElement | null>(null)
/** 选中根流程或未选中时隐藏属性面板顶部「• Process_1」标题，避免多余；默认 true 以应对首次加载显示根的情况 */
const isSelectedRootProcess = ref(true)
const initError = ref<string | null>(null)
const minHeightPx = computed(() => `${props.minHeight}px`)
const canUndo = ref(false)
const canRedo = ref(false)
/** 当前选中元素仅存于模块变量，不放入 ref，避免 Vue 代理 diagram-js 只读属性（如 labels）导致报错 */
let currentSelectedElement: BpmnShapeLike | null = null
const hasSelectedElement = ref(false)
const selectedBpmnType = ref('')
const selectedId = ref('')
const selectedName = ref('')
const selectedElementType = ref('')
const selectedConditionExpression = ref('')
const selectedExecutable = ref(false)
let modeler: InstanceType<typeof BpmnModeler> | null = null
let resizeObserver: ResizeObserver | null = null
let initDone = false

function getElementType(bo: { $type?: string } | undefined): string {
  if (!bo) return ''
  try {
    const key = ELEMENT_TYPE_I18N_KEYS[bo.$type ?? '']
    return (key ? (t(key) || bo.$type) : bo.$type) ?? ''
  } catch {
    return bo.$type ?? ''
  }
}

/** 根据当前选中元素同步属性面板（参考 vue-bpmn-flowable：selection.changed + element.changed 双监听） */
function syncPanelFromElement(el: BpmnShapeLike | null) {
  currentSelectedElement = el
  hasSelectedElement.value = !!el
  selectedBpmnType.value = el?.businessObject?.$type ?? ''
  if (el?.businessObject) {
    const bo = el.businessObject
    selectedId.value = bo.id ?? ''
    selectedName.value = bo.name ?? ''
    selectedElementType.value = getElementType(bo)
    selectedConditionExpression.value =
      bo.$type === 'bpmn:SequenceFlow' && bo.conditionExpression ? (bo.conditionExpression.body ?? '') : ''
    selectedExecutable.value = bo.$type === 'bpmn:Process' ? !!bo.isExecutable : false
  } else {
    selectedId.value = ''
    selectedName.value = ''
    selectedElementType.value = ''
    selectedConditionExpression.value = ''
    selectedExecutable.value = false
  }
}

function bindSelection() {
  if (!modeler) return
  const eventBus = modeler.get('eventBus') as {
    on: (event: string, handler: (e: { newSelection?: BpmnShapeLike[]; element?: BpmnShapeLike }) => void) => void
  }
  const canvas = modeler.get('canvas') as { getRootElement: () => BpmnShapeLike } | undefined
  eventBus.on('selection.changed', (e: { newSelection?: BpmnShapeLike[] }) => {
    try {
      const selected = e.newSelection?.[0] ?? null
      syncPanelFromElement(selected)
      const root = canvas?.getRootElement?.()
      isSelectedRootProcess.value = !!(root && (selected === root || !selected))
    } catch (err) {
      if (import.meta.env.DEV) logger.warn('[TaktFlowDesigner] selection.changed handler error (e.g. after HMR):', err)
    }
  })
  /** 参考 vue-bpmn-flowable：element.changed 时若为当前选中元素则刷新面板（如撤销/重做后） */
  eventBus.on('element.changed', (e: { element?: BpmnShapeLike }) => {
    try {
      const el = e.element
      const current = currentSelectedElement
      if (el?.businessObject && current?.businessObject && el.businessObject.id === current.businessObject.id) syncPanelFromElement(el)
    } catch (err) {
      if (import.meta.env.DEV) logger.warn('[TaktFlowDesigner] element.changed handler error:', err)
    }
  })
}

/** 打开：弹出文件选择，选中 .bpmn 后读入并导入画布，并 emit loaded 供父组件同步 */
function handleOpenBpmn() {
  fileInputRef.value?.click()
}

function onBpmnFileChange(ev: Event) {
  const input = ev.target as HTMLInputElement
  const file = input.files?.[0]
  input.value = ''
  if (!file) return
  const reader = new FileReader()
  reader.onload = async (e) => {
    const xml = (e.target?.result as string) ?? ''
    if (!xml.trim()) {
      message.error(t('workflow.scheme.openBpmnEmpty'))
      return
    }
    try {
      if (modeler) {
        currentSelectedElement = null
        hasSelectedElement.value = false
        selectedBpmnType.value = ''
        selectedId.value = ''
        selectedName.value = ''
        selectedElementType.value = ''
        selectedConditionExpression.value = ''
        selectedExecutable.value = false
        await modeler.importXML(xml)
        await nextTick()
        getCanvas()?.zoom('fit-viewport')
        message.success(t('workflow.scheme.openBpmnSuccess'))
      }
      emit('loaded', xml)
    } catch (err) {
      logger.error('[TaktFlowDesigner] importXML failed:', err)
      message.error(t('workflow.scheme.openBpmnFail'))
    }
  }
  reader.onerror = () => {
    message.error(t('workflow.scheme.openBpmnReadError'))
  }
  reader.readAsText(file, 'UTF-8')
}


/** 下载 XML：将当前 BPMN 导出为 .bpmn 文件下载 */
/** 下载 BPMN 2.0 文件，后缀 .bpmn */
async function handleDownloadBpmn() {
  if (!modeler) return
  const xml = await getXml()
  const blob = new Blob([xml], { type: 'application/xml;charset=utf-8' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = `flow_${Date.now()}.bpmn`
  a.click()
  URL.revokeObjectURL(url)
}

/** 保存 SVG：将当前画布导出为 SVG 图片下载 */
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

function getCanvas() {
  return modeler?.get('canvas') as { zoom: (level?: number | 'fit-viewport') => number } | undefined
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
  try {
    if (!modeler) return
    const commandStack = modeler.get('commandStack') as { canUndo: () => boolean; canRedo: () => boolean } | undefined
    if (commandStack) {
      canUndo.value = commandStack.canUndo()
      canRedo.value = commandStack.canRedo()
    }
  } catch (err) {
    if (import.meta.env.DEV) logger.warn('[TaktFlowDesigner] updateUndoRedoState error (e.g. after HMR):', err)
  }
}

function undo() {
  const cmd = modeler?.get('commandStack') as { undo: () => void } | undefined
  cmd?.undo()
  updateUndoRedoState()
}

function redo() {
  const cmd = modeler?.get('commandStack') as { redo: () => void } | undefined
  cmd?.redo()
  updateUndoRedoState()
}

function handleClearCanvas() {
  if (!modeler) return
  Modal.confirm({
    title: t('workflow.scheme.clearCanvas'),
    content: t('workflow.scheme.clearCanvasConfirm'),
    okText: t('workflow.scheme.confirm'),
    cancelText: t('common.cancel'),
    onOk: async () => {
      currentSelectedElement = null
      hasSelectedElement.value = false
      selectedBpmnType.value = ''
      selectedId.value = ''
      selectedName.value = ''
      selectedElementType.value = ''
      selectedConditionExpression.value = ''
      selectedExecutable.value = false
      await modeler!.importXML(DEFAULT_BPMN_XML)
      await nextTick()
      getCanvas()?.zoom('fit-viewport')
    }
  })
}

/** 供父组件（如 scheme-form）获取当前 BPMN XML */
async function getXml(): Promise<string> {
  if (!modeler) return ''
  const result = await modeler.saveXML({ format: true })
  return result.xml ?? ''
}

async function initModeler() {
  initError.value = null
  const container = containerRef.value
  if (!container) return
  if (container.offsetWidth < 50 || container.offsetHeight < 50) return
  if (initDone) return
  initDone = true
  try {
    const { getAdditionalModules } = await import('./config/module')
    const additionalModules = await getAdditionalModules(locale.value, t)
    const panelParent = propertiesPanelRef.value ?? undefined
    modeler = new BpmnModeler({
      container,
      propertiesPanel: panelParent ? { parent: panelParent } : undefined,
      moddleExtensions: { camunda: camundaModdleDescriptor as Record<string, unknown> },
      // eslint-disable-next-line @typescript-eslint/no-explicit-any -- bpmn-js additionalModules 与 bpmn-js-properties-panel 类型兼容
      additionalModules: [
        ...(additionalModules as any[]),
        BpmnPropertiesPanelModule,
        BpmnPropertiesProviderModule,
        CamundaPlatformPropertiesProviderModule
      ] as any
    })
    const xmlToLoad = props.xml?.trim() || DEFAULT_BPMN_XML
    try {
      await modeler.importXML(xmlToLoad)
    } catch {
      await modeler.importXML(DEFAULT_BPMN_XML)
    }
    await nextTick()
    getCanvas()?.zoom('fit-viewport')
    ;(modeler.get('palette') as { open?: () => void } | undefined)?.open?.()
    bindSelection()
    const eventBus = modeler.get('eventBus') as { on: (e: string, fn: () => void) => void }
    eventBus.on('commandStack.changed', updateUndoRedoState)
    updateUndoRedoState()
  } catch (err: unknown) {
    initDone = false
    initError.value = err instanceof Error ? err.message : String(err)
    logger.error('[TaktFlowDesigner] initModeler failed:', err)
  }
}

function tryInit() {
  const container = containerRef.value
  if (!container || container.offsetWidth < 50 || container.offsetHeight < 50) return
  initModeler()
}

watch(
  () => props.xml,
  async (newXml) => {
    if (!modeler || !containerRef.value) return
    currentSelectedElement = null
    hasSelectedElement.value = false
    selectedBpmnType.value = ''
    selectedId.value = ''
    selectedName.value = ''
    selectedElementType.value = ''
    selectedConditionExpression.value = ''
    selectedExecutable.value = false
    const xmlToLoad = newXml?.trim() || DEFAULT_BPMN_XML
    try {
      await modeler.importXML(xmlToLoad)
    } catch {
      await modeler.importXML(DEFAULT_BPMN_XML)
    }
    getCanvas()?.zoom('fit-viewport')
  }
)

function onContainerResize() {
  if (modeler && containerRef.value) {
    getCanvas()?.zoom('fit-viewport')
  } else {
    tryInit()
  }
}

onMounted(() => {
  nextTick(() => {
    tryInit()
    const container = containerRef.value
    if (container) {
      resizeObserver = new ResizeObserver(onContainerResize)
      resizeObserver.observe(container)
    }
  })
})

onBeforeUnmount(() => {
  resizeObserver?.disconnect()
  resizeObserver = null
  modeler?.destroy()
  modeler = null
  initDone = false
})

defineExpose({ getXml })
</script>

<style scoped>
/* 填满父容器；header/footer 自适应高度，左中右按比例自适应 */
.takt-flow-design {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
  min-height: v-bind(minHeightPx);
  min-width: 0;
  box-sizing: border-box;
}
.takt-flow-design-header {
  flex-shrink: 0;
  min-height: 48px;
  display: flex;
  align-items: center;
  padding: 8px 12px;
  border-bottom: 1px solid var(--ant-color-border);
  background: var(--ant-color-bg-container);
}
/* 参照 bpmn-vue3：grid 布局 左画布区 + 右属性面板（100px 调色板在画布区内） */
.takt-flow-design-body {
  flex: 1;
  min-height: 0;
  display: grid;
  grid-template-columns: 1fr max-content;
}
.takt-flow-design-canvas-wrap {
  min-width: 0;
  position: relative;
  overflow: hidden;
  contain: layout style paint;
  transform: translateZ(0);
}
.takt-flow-design-container {
  position: absolute;
  inset: 0;
  overflow: hidden;
  contain: layout style paint;
  transform: translateZ(0);
}
/* 参照 bpmn-vue3：调色板固定 100px 宽，画布占剩余 */
.takt-flow-design-container :deep(.djs-container) {
  display: flex;
  flex-direction: row;
}
.takt-flow-design-container :deep(.djs-palette) {
  flex: 0 0 100px;
  width: 100px;
  min-width: 100px;
}
.takt-flow-design-container :deep(.djs-canvas) {
  flex: 1 1 auto;
  min-width: 0;
}
.takt-flow-design-properties-panel {
  width: 380px;
  min-width: 280px;
  border-left: 1px solid var(--ant-color-border);
  background: var(--ant-color-bg-container);
  overflow: auto;
  box-sizing: border-box;
}
/* 选中根流程或未选中时隐藏面板顶部「• Process_1」标题，避免多余 */
.takt-flow-design-properties-panel.takt-flow-design-panel-hide-root-header :deep(.bio-properties-panel-header) {
  display: none;
}
/* 画布上元素名称与间距由 bpmn-theme.less 的 .djs-parent + .djs-label 统一控制 */
.takt-flow-design-footer {
  flex-shrink: 0;
  min-height: 48px;
  display: flex;
  align-items: center;
  padding: 8px 12px;
  border-top: 1px solid var(--ant-color-border);
  background: var(--ant-color-bg-container);
}
.takt-flow-design-file-input {
  display: none;
}

/* 参照 bpmn-vue3：画布与属性面板区域滚动条样式 */
.takt-flow-design-body ::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}
.takt-flow-design-body ::-webkit-scrollbar-track-piece {
  background: var(--ant-color-fill-quaternary);
  border-radius: 4px;
}
.takt-flow-design-body ::-webkit-scrollbar-thumb {
  background: var(--ant-color-text-quaternary);
  border-radius: 4px;
}
.takt-flow-design-body ::-webkit-scrollbar-thumb:hover {
  background: var(--ant-color-text-tertiary);
}
</style>
