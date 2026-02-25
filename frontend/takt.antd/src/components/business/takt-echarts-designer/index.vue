<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/components/business/takt-echarts-designer -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：ECharts 图表设计器：左侧画布预览、右侧选项面板（简版表单 + JSON），支持图表类型切换、导出/加载 option、响应式 resize -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="takt-echarts-designer">
    <header class="takt-echarts-designer-header">
      <a-space :size="8">
        <a-select
          v-model:value="currentChartType"
          :options="chartTypeOptions"
          :disabled="readonly"
          style="width: 120px"
          @change="onChartTypeChange"
        />
        <a-button size="small" :disabled="readonly" @click="handleRefresh">
          {{ t('components.echarts-designer.refresh') }}
        </a-button>
        <a-button size="small" :disabled="readonly" @click="handleExportJson">
          {{ t('components.echarts-designer.exportJson') }}
        </a-button>
        <a-button size="small" :disabled="readonly" @click="handleLoadJsonClick">
          {{ t('components.echarts-designer.loadJson') }}
        </a-button>
        <input
          ref="fileInputRef"
          type="file"
          accept=".json,application/json"
          class="takt-echarts-designer-file-input"
          @change="onJsonFileChange"
        />
      </a-space>
    </header>
    <div class="takt-echarts-designer-body">
      <div class="takt-echarts-designer-canvas-wrap" :style="{ minHeight: `${chartHeight}px` }">
        <div ref="chartRef" class="takt-echarts-designer-chart" />
      </div>
      <aside v-if="!hideOptionPanel" class="takt-echarts-designer-panel" :style="{ width: `${optionPanelWidth}px` }">
        <a-tabs v-model:activeKey="panelTab" size="small">
          <a-tab-pane :key="'simple'" :tab="t('components.echarts-designer.tabSimple')">
            <div class="takt-echarts-designer-form">
              <a-form layout="vertical" :disabled="readonly">
                <a-form-item :label="t('components.echarts-designer.chartTypeLabel')">
                  <a-select
                    v-model:value="currentChartType"
                    :options="chartTypeOptions"
                    @change="onChartTypeChange"
                  />
                </a-form-item>
                <a-form-item :label="t('components.echarts-designer.chartTitle')">
                  <a-input
                    v-model:value="chartTitle"
                    :placeholder="t('components.echarts-designer.chartTitlePlaceholder')"
                    @change="applyTitleToOption"
                  />
                </a-form-item>
              </a-form>
            </div>
          </a-tab-pane>
          <a-tab-pane :key="'json'" :tab="t('components.echarts-designer.tabJson')">
            <div class="takt-echarts-designer-json-wrap">
              <a-textarea
                v-model:value="jsonText"
                :placeholder="t('components.echarts-designer.jsonPlaceholder')"
                :readonly="readonly"
                :rows="14"
                class="takt-echarts-designer-json"
                @blur="onJsonBlur"
              />
              <a-button
                v-if="!readonly"
                type="primary"
                size="small"
                class="takt-echarts-designer-json-apply"
                @click="applyJsonOption"
              >
                {{ t('components.echarts-designer.applyJson') }}
              </a-button>
            </div>
          </a-tab-pane>
        </a-tabs>
      </aside>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onMounted, onBeforeUnmount, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import * as echarts from 'echarts'
import type { EChartsOption } from 'echarts'
import { message } from 'ant-design-vue'
import {
  DEFAULT_CHART_HEIGHT,
  OPTION_PANEL_WIDTH,
  CHART_TYPES,
  getPresetOption,
  type ChartTypeValue
} from './config/config'

const props = withDefaults(
  defineProps<{
    /** 当前图表配置（受控或初始值） */
    option?: EChartsOption
    /** 画布高度（px） */
    height?: number
    /** 是否隐藏右侧选项面板 */
    hideOptionPanel?: boolean
    /** 右侧面板宽度（px） */
    optionPanelWidth?: number
    /** 是否只读（不展示编辑与导入导出） */
    readonly?: boolean
  }>(),
  {
    option: () => ({}),
    height: DEFAULT_CHART_HEIGHT,
    hideOptionPanel: false,
    optionPanelWidth: OPTION_PANEL_WIDTH,
    readonly: false
  }
)

const emit = defineEmits<{
  (e: 'update:option', value: EChartsOption): void
  (e: 'change', value: EChartsOption): void
}>()

const { t } = useI18n()

const chartRef = ref<HTMLElement | null>(null)
const fileInputRef = ref<HTMLInputElement | null>(null)
let chartInstance: echarts.ECharts | null = null
let resizeObserver: ResizeObserver | null = null

const chartHeight = computed(() => props.height)
const panelTab = ref<'simple' | 'json'>('simple')
const currentChartType = ref<ChartTypeValue>('bar')
const chartTitle = ref('')
const jsonText = ref('')

const chartTypeOptions = computed(() =>
  CHART_TYPES.map((item) => ({ label: t(item.labelKey), value: item.value }))
)

function getOptionTitle(opt: EChartsOption): string {
  const title = opt?.title
  if (title && typeof title === 'object' && 'text' in title) {
    return String((title as { text?: string }).text ?? '')
  }
  if (Array.isArray(title) && title[0] && typeof title[0] === 'object' && 'text' in title[0]) {
    return String((title[0] as { text?: string }).text ?? '')
  }
  return ''
}

function syncFromOption(opt: EChartsOption) {
  chartTitle.value = getOptionTitle(opt)
  try {
    jsonText.value = JSON.stringify(opt, null, 2)
  } catch {
    jsonText.value = '{}'
  }
}

function inferChartType(opt: EChartsOption): ChartTypeValue {
  const series = opt?.series
  if (Array.isArray(series) && series.length > 0) {
    const first = series[0]
    const type = first && typeof first === 'object' && 'type' in first ? (first as { type?: string }).type : undefined
    if (type === 'bar' || type === 'line' || type === 'pie' || type === 'scatter') {
      return type as ChartTypeValue
    }
  }
  return 'bar'
}

function applyOption(opt: EChartsOption) {
  if (chartInstance) {
    chartInstance.setOption(opt, { notMerge: true })
  }
  syncFromOption(opt)
  emit('update:option', opt)
  emit('change', opt)
}

function onChartTypeChange() {
  const preset = getPresetOption(currentChartType.value)
  applyOption(preset)
}

function applyTitleToOption() {
  const raw = props.option && Object.keys(props.option).length > 0 ? props.option : getPresetOption(currentChartType.value)
  const next: EChartsOption = {
    ...raw,
    title: typeof raw.title === 'object' && raw.title !== null && !Array.isArray(raw.title)
      ? { ...(raw.title as object), text: chartTitle.value }
      : { text: chartTitle.value, left: 'center' }
  }
  applyOption(next)
}

function applyJsonOption() {
  try {
    const parsed = JSON.parse(jsonText.value) as EChartsOption
    applyOption(parsed)
    currentChartType.value = inferChartType(parsed)
    message.success(t('components.echarts-designer.jsonApplySuccess'))
  } catch (e) {
    message.error(t('components.echarts-designer.jsonParseError'))
  }
}

function onJsonBlur() {
  // 可选：失焦时尝试应用，这里不自动应用，仅点击按钮应用
}

function handleRefresh() {
  const opt = props.option && Object.keys(props.option).length > 0 ? props.option : getPresetOption(currentChartType.value)
  if (chartInstance) {
    chartInstance.setOption(opt, { notMerge: true })
  }
  syncFromOption(opt)
}

function handleExportJson() {
  const opt = props.option && Object.keys(props.option).length > 0 ? props.option : getPresetOption(currentChartType.value)
  const str = JSON.stringify(opt, null, 2)
  const blob = new Blob([str], { type: 'application/json' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = 'echarts-option.json'
  a.click()
  URL.revokeObjectURL(url)
  message.success(t('components.echarts-designer.exportSuccess'))
}

function handleLoadJsonClick() {
  fileInputRef.value?.click()
}

function onJsonFileChange(ev: Event) {
  const input = ev.target as HTMLInputElement
  const file = input.files?.[0]
  if (!file) return
  const reader = new FileReader()
  reader.onload = () => {
    try {
      const text = reader.result as string
      const parsed = JSON.parse(text) as EChartsOption
      applyOption(parsed)
      currentChartType.value = inferChartType(parsed)
      message.success(t('components.echarts-designer.loadSuccess'))
    } catch {
      message.error(t('components.echarts-designer.jsonParseError'))
    }
    input.value = ''
  }
  reader.readAsText(file)
}

watch(
  () => props.option,
  (opt) => {
    if (!opt || Object.keys(opt).length === 0) return
    if (chartInstance) {
      chartInstance.setOption(opt, { notMerge: true })
    }
    syncFromOption(opt)
    currentChartType.value = inferChartType(opt)
  },
  { deep: true }
)

onMounted(() => {
  const el = chartRef.value
  if (!el) return
  chartInstance = echarts.init(el)
  const opt = props.option && Object.keys(props.option).length > 0 ? props.option : getPresetOption(currentChartType.value)
  chartInstance.setOption(opt, { notMerge: true })
  syncFromOption(opt)
  currentChartType.value = inferChartType(opt)

  resizeObserver = new ResizeObserver(() => {
    chartInstance?.resize()
  })
  resizeObserver.observe(el)
})

onBeforeUnmount(() => {
  resizeObserver?.disconnect()
  chartInstance?.dispose()
  chartInstance = null
})
</script>

<style scoped lang="less">
.takt-echarts-designer-file-input {
  position: absolute;
  width: 0;
  height: 0;
  opacity: 0;
  pointer-events: none;
}

.takt-echarts-designer {
  display: flex;
  flex-direction: column;
  height: 100%;
  min-height: 300px;
}

.takt-echarts-designer-header {
  flex-shrink: 0;
  padding: 8px 12px;
  border-bottom: 1px solid var(--ant-color-border-secondary, #f0f0f0);
}

.takt-echarts-designer-body {
  display: flex;
  flex: 1;
  min-height: 0;
}

.takt-echarts-designer-canvas-wrap {
  flex: 1;
  min-width: 0;
  padding: 12px;
}

.takt-echarts-designer-chart {
  width: 100%;
  height: 100%;
  min-height: 300px;
}

.takt-echarts-designer-panel {
  flex-shrink: 0;
  border-left: 1px solid var(--ant-color-border-secondary, #f0f0f0);
  padding: 8px;
  overflow-y: auto;
  background: var(--ant-color-fill-quaternary, #fafafa);
}

.takt-echarts-designer-form {
  padding: 4px 0;
}

.takt-echarts-designer-json-wrap {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.takt-echarts-designer-json {
  font-family: ui-monospace, monospace;
  font-size: 12px;
}

.takt-echarts-designer-json-apply {
  align-self: flex-end;
}
</style>
