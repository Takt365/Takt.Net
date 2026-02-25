<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/components/business/takt-form-designer -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：基于 @form-create/antd-designer 的表单设计器业务组件，用于流程表单可视化设计；与流程方案通过 formCode 关联，formTemplate 存设计结果供发起/审批渲染 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="takt-form-design" :class="{ 'takt-form-design--dark': isDark }" :style="wrapStyle">
    <fc-designer ref="designerRef" :height="heightPx" :locale="fcDesignerLocale" />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted, nextTick, inject } from 'vue'
import { storeToRefs } from 'pinia'
import formCreate from '@form-create/ant-design-vue'
import { useThemeStore } from '@/stores/theme'

/** 设计器实例方法（与 FcDesigner 文档一致） */
interface FcDesignerInstance {
  getRule: () => unknown[]
  getJson: () => string
  getOption: () => Record<string, unknown>
  getOptionsJson: () => string
  setRule: (rule: string | unknown[]) => void
  setOption: (opt: Record<string, unknown>) => void
  getFormData: () => Record<string, unknown>
  setFormData: (formData: Record<string, unknown>) => void
  clearDragRule: () => void
  openPreview: () => void
  fields: () => string[]
}

const props = withDefaults(
  defineProps<{
    /** 表单规则 JSON 字符串（FcDesigner 的 rule，与 formTemplate 中 rule 一致） */
    ruleJson?: string
    /** 表单配置 JSON 字符串（FcDesigner 的 option，与 formTemplate 中 option 一致） */
    optionJson?: string
    /** 设计器高度，如 '100%'、'600px'、500 */
    height?: string | number
  }>(),
  { ruleJson: '', optionJson: '', height: '100%' }
)

const { themeMode } = storeToRefs(useThemeStore())
const isDark = computed(() => themeMode.value === 'dark')

/** FcDesigner 语言包：由 App.vue 与 antdVueLocale/dayjs 同源 provide，此处 inject 使用 */
const fcDesignerLocale = inject<ReturnType<typeof computed<{ name: string; [key: string]: unknown }>>>('taktFcDesignerLocale')

/** fc-designer 组件实例（Options API，方法直接挂在实例上） */
const designerRef = ref<FcDesignerInstance | null>(null)

const heightPx = computed(() => {
  const h = props.height
  if (typeof h === 'number') return `${h}px`
  return h
})

const wrapStyle = computed(() => ({
  height: heightPx.value,
  minHeight: '400px'
}))

function getDesigner(): FcDesignerInstance | null {
  return designerRef.value ?? null
}

/** 设置设计器初始 rule/option（由 JSON 解析），需在组件挂载后调用 */
function applyInitial() {
  const designer = getDesigner()
  if (!designer) return
  const ruleStr = props.ruleJson?.trim()
  const optionStr = props.optionJson?.trim()
  if (ruleStr) {
    try {
      designer.setRule(formCreate.parseJson(ruleStr))
    } catch {
      // 忽略解析失败，保留空白设计器
    }
  }
  if (optionStr) {
    try {
      designer.setOption(formCreate.parseJson(optionStr) as unknown as Record<string, unknown>)
    } catch {
      // 忽略
    }
  }
}

onMounted(() => {
  // FcDesigner 需完成初始化后方可调用 setRule/setOption
  nextTick(() => {
    setTimeout(applyInitial, 100)
  })
})

watch(
  () => [props.ruleJson, props.optionJson],
  () => {
    nextTick(() => applyInitial())
  },
  { flush: 'post' }
)

/**
 * 获取当前表单规则（数组）
 */
function getRule(): unknown[] {
  return getDesigner()?.getRule() ?? []
}

/**
 * 获取当前表单规则 JSON 字符串（用于保存到 formTemplate.rule）
 */
function getJson(): string {
  return getDesigner()?.getJson() ?? '[]'
}

/**
 * 获取当前表单配置对象
 */
function getOption(): Record<string, unknown> {
  return getDesigner()?.getOption() ?? {}
}

/**
 * 获取当前表单配置 JSON 字符串（用于保存到 formTemplate.option）
 */
function getOptionsJson(): string {
  return getDesigner()?.getOptionsJson() ?? '{}'
}

/**
 * 设置表单规则（支持 JSON 字符串或规则数组）
 */
function setRule(rule: string | unknown[]): void {
  const designer = getDesigner()
  if (!designer) return
  if (typeof rule === 'string') {
    try {
      designer.setRule(formCreate.parseJson(rule))
    } catch {
      designer.setRule([])
    }
  } else {
    designer.setRule(rule)
  }
}

/**
 * 设置表单配置（支持 JSON 字符串或配置对象）
 */
function setOption(opt: string | Record<string, unknown>): void {
  const designer = getDesigner()
  if (!designer) return
  if (typeof opt === 'string') {
    try {
      designer.setOption(formCreate.parseJson(opt) as unknown as Record<string, unknown>)
    } catch {
      designer.setOption({})
    }
  } else {
    designer.setOption(opt)
  }
}

/**
 * 获取表单数据（预览/录入用）
 */
function getFormData(): Record<string, unknown> {
  return getDesigner()?.getFormData() ?? {}
}

/**
 * 设置表单数据（预填）
 */
function setFormData(formData: Record<string, unknown>): void {
  getDesigner()?.setFormData(formData)
}

/**
 * 清空设计器内所有表单项
 */
function clearDragRule(): void {
  getDesigner()?.clearDragRule()
}

/**
 * 打开预览
 */
function openPreview(): void {
  getDesigner()?.openPreview()
}

/**
 * 获取所有字段名列表
 */
function fields(): string[] {
  return getDesigner()?.fields() ?? []
}

defineExpose({
  getRule,
  getJson,
  getOption,
  getOptionsJson,
  setRule,
  setOption,
  getFormData,
  setFormData,
  clearDragRule,
  openPreview,
  fields
})
</script>

<style scoped>
.takt-form-design {
  width: 100%;
}
</style>

<!-- 暗色主题：覆盖 @form-create/antd-designer 内部浅色样式，与应用 themeMode 一致 -->
<style lang="less">
.takt-form-design--dark {
  ._fc-designer,
  ._fc-designer .ant-layout-sider,
  ._fc-designer .ant-layout-header,
  ._fc-designer .ant-layout-content,
  ._fc-designer .ant-layout {
    background-color: var(--ant-color-bg-container) !important;
  }
  ._fc-designer .CodeMirror-gutters,
  ._fd-config-dialog .CodeMirror-gutters {
    background-color: var(--ant-color-fill-secondary) !important;
    border-right-color: var(--ant-color-border) !important;
  }
  ._fc-designer .CodeMirror-scroll,
  ._fd-config-dialog .CodeMirror-scroll {
    background-color: var(--ant-color-bg-layout) !important;
    color: var(--ant-color-text) !important;
    caret-color: var(--ant-color-text) !important;
  }
  ._fd-config-dialog .CodeMirror-scroll {
    background-color: var(--ant-color-bg-container) !important;
  }
  ._fc-l-menu {
    border-top-color: var(--ant-color-border);
    border-right-color: var(--ant-color-border);
  }
  ._fc-l-menu-form {
    border-bottom-color: var(--ant-color-border);
  }
  ._fc-l-label {
    color: var(--ant-color-text) !important;
  }
  ._fc-l-info {
    color: var(--ant-color-text-tertiary) !important;
  }
  ._fc-l-close,
  ._fc-r-close,
  ._fc-l-open,
  ._fc-r-open {
    background: var(--ant-color-bg-container) !important;
    border-color: var(--ant-color-border);
  }
  ._fc-l,
  ._fc-m,
  ._fc-r {
    border-top-color: var(--ant-color-border);
  }
  ._fc-r-title {
    color: var(--ant-color-text) !important;
  }
  ._fc-r-sub > ._fd-config-item > ._fd-ci-head:before {
    background-color: var(--ant-color-text-secondary);
  }
  ._fc-r-name-input .ant-input-group-addon {
    color: var(--ant-color-text-tertiary) !important;
  }
  ._fc-r-tools-close {
    color: var(--ant-color-text-secondary) !important;
  }
  ._fc-l-item,
  ._fc-l-item ._fc-l-name,
  ._fc-l-item ._fc-l-icon {
    background: var(--ant-color-bg-container) !important;
    color: var(--ant-color-text) !important;
  }
  ._fc-l-item:hover {
    background: var(--ant-color-primary) !important;
    color: #fff !important;
  }
  ._fc-l-group {
    border-color: var(--ant-color-border);
  }
  ._fc-l-tab,
  ._fc-r-tab {
    color: var(--ant-color-text) !important;
  }
  ._fc-l ._fc-l-tab.active,
  ._fc-r ._fc-r-tab.active {
    color: var(--ant-color-primary) !important;
    border-bottom-color: var(--ant-color-primary);
  }
  ._fc-m .form-create ._fc-l-item,
  ._fc-m .form-create ._fc-field-node {
    background: var(--ant-color-fill-tertiary) !important;
    color: var(--ant-color-text) !important;
    border-color: var(--ant-color-border);
  }
  ._fc-m-tools {
    border-color: var(--ant-color-border);
  }
  ._fc-m-tools .line {
    background: var(--ant-color-border);
  }
  ._fc-m-tools ._fd-m-extend {
    color: var(--ant-color-text-secondary);
    border-color: var(--ant-color-border);
    background-color: var(--ant-color-fill-secondary);
  }
  ._fc-m-tools-l .fc-icon.disabled {
    color: var(--ant-color-text-tertiary) !important;
  }
  ._fc-m ._fc-m-con {
    background: var(--ant-color-bg-layout) !important;
  }
  ._fc-m-input-handle {
    background: var(--ant-color-bg-container) !important;
    box-shadow: 0 2px 10px 0 rgba(0, 0, 0, 0.2);
  }
  ._fc-m-drag,
  .draggable-drag {
    background: var(--ant-color-bg-container) !important;
  }
  ._fd-drag-box,
  ._fc-child-empty,
  ._fd-aTooltip-drag.drag-holder,
  ._fd-fcInlineForm-drag.drag-holder,
  ._fd-fcDialog-drag.drag-holder,
  ._fd-fcDrawer-drag.drag-holder,
  ._fd-draggable-drag.drag-holder,
  ._fd-tableFormColumn-drag.drag-holder,
  ._fd-aTabPane-drag.drag-holder,
  ._fd-group-drag.drag-holder,
  ._fd-subForm-drag.drag-holder,
  ._fd-stepFormItem-drag.drag-holder,
  ._fd-aCard-drag.drag-holder,
  ._fd-aCollapsePanel-drag.drag-holder {
    background: var(--ant-color-fill-tertiary) !important;
    background-size: 0;
  }
  ._fc-child-empty:after,
  ._fd-aTooltip-drag.drag-holder:after,
  ._fd-fcInlineForm-drag.drag-holder:after,
  ._fd-fcDialog-drag.drag-holder:after,
  ._fd-fcDrawer-drag.drag-holder:after,
  ._fd-draggable-drag.drag-holder:after,
  ._fd-tableFormColumn-drag.drag-holder:after,
  ._fd-aTabPane-drag.drag-holder:after,
  ._fd-group-drag.drag-holder:after,
  ._fd-subForm-drag.drag-holder:after,
  ._fd-stepFormItem-drag.drag-holder:after,
  ._fd-aCard-drag.drag-holder:after,
  ._fd-aCollapsePanel-drag.drag-holder:after {
    color: var(--ant-color-text-tertiary) !important;
  }
  ._fd-draggable-drag.drag-holder {
    background-color: var(--ant-color-bg-container) !important;
  }
  .fc-configured {
    color: var(--ant-color-text-tertiary) !important;
  }
  ._fc-struct-tree {
    color: var(--ant-color-text) !important;
  }
  ._fc-manage-text,
  ._fd-preview-copy {
    color: var(--ant-color-primary) !important;
  }
  ._fd-preview-copy {
    background: var(--ant-color-primary-bg, rgba(22, 119, 255, 0.15));
  }
  ._fd-row-line {
    background: var(--ant-color-border);
  }
  ._fd-menu-item {
    border-color: var(--ant-color-border);
    border-bottom-color: var(--ant-color-border);
  }
  ._fd-menu-item.is-active {
    border-color: var(--ant-color-primary);
  }
  ._fd-menu-item.is-active > div > .fc-icon {
    color: var(--ant-color-primary) !important;
  }
  ._fc-designer ._fc-l-tabs,
  ._fc-designer ._fc-r-tabs {
    border-bottom-color: var(--ant-color-border);
  }
}
</style>
