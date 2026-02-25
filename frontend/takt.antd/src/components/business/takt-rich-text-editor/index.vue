<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-rich-text-editor -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：Takt 富文本编辑器，基于 WangEditor，支持 v-model 绑定 HTML、主题与多语言与 App 一致 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div
    class="takt-rich-text-editor"
    :class="{ 'takt-rich-text-editor--dark': isDark }"
  >
    <Toolbar
      :editor="editorRef"
      :default-config="toolbarConfig"
      mode="default"
      class="takt-rich-text-editor__toolbar"
    />
    <Editor
      v-model="innerValue"
      :default-config="editorConfig"
      mode="default"
      :style="editorStyle"
      @on-created="handleCreated"
    />
  </div>
</template>

<script setup lang="ts">
import { computed, inject, onBeforeUnmount, shallowRef, watch } from 'vue'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue'
import { i18nChangeLanguage } from '@wangeditor/editor'
import type { IDomEditor } from '@wangeditor/editor'
import '@wangeditor/editor/dist/css/style.css'

/** 语言与暗色由 App.vue 与 themeStore/localeStore 同源 provide */
const wangEditorLocale = inject<ReturnType<typeof computed<string>>>('taktWangEditorLocale')
const wangEditorDark = inject<ReturnType<typeof computed<boolean>>>('taktWangEditorDark')
const isDark = computed(() => wangEditorDark?.value ?? false)

interface Props {
  /** 富文本 HTML 内容（v-model） */
  modelValue?: string
  /** 是否禁用 */
  disabled?: boolean
  /** 占位符 */
  placeholder?: string
  /** 编辑器高度（像素或带单位字符串，如 320 或 '20rem'） */
  height?: number | string
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: '',
  disabled: false,
  placeholder: '',
  height: 320
})

const emit = defineEmits<{
  'update:modelValue': [value: string]
}>()

const editorRef = shallowRef()
const innerValue = computed({
  get: () => props.modelValue ?? '',
  set: (value: string) => emit('update:modelValue', value)
})

const editorStyle = computed(() => {
  const h = props.height
  const height = typeof h === 'number' ? `${h}px` : h
  return { height, overflowY: 'hidden' as const }
})

const toolbarConfig = {}
const editorConfig = computed(() => ({
  placeholder: props.placeholder || undefined,
  readOnly: props.disabled
}))

function handleCreated(editor: IDomEditor) {
  editorRef.value = editor
  if (props.disabled) editor.disable()
}

watch(
  () => props.disabled,
  (disabled) => {
    const editor = editorRef.value
    if (!editor) return
    if (disabled) editor.disable()
    else editor.enable()
  }
)

watch(
  () => wangEditorLocale?.value,
  (lang) => {
    if (lang) i18nChangeLanguage(lang)
  },
  { immediate: true }
)

onBeforeUnmount(() => {
  const editor = editorRef.value
  if (editor) editor.destroy()
  editorRef.value = undefined
})
</script>

<style scoped lang="less">
.takt-rich-text-editor {
  border: 1px solid var(--ant-color-border, #d9d9d9);
  border-radius: 6px;
  overflow: hidden;

  &__toolbar {
    border-bottom: 1px solid var(--ant-color-border, #d9d9d9);
  }

  :deep(.w-e-text-container) {
    background-color: var(--w-e-textarea-bg-color);
  }

  &--dark {
    border-color: #434343;
    .takt-rich-text-editor__toolbar {
      border-bottom-color: #434343;
    }
    /* WangEditor 暗色：与官方 theme 文档一致，通过 CSS 变量覆盖 */
    --w-e-textarea-bg-color: #1f1f1f;
    --w-e-textarea-color: #e8e8e8;
  }
}
</style>
