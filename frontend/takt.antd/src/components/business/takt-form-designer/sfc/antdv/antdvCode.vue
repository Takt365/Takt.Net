<template>
  <div class="antdv-code">
    <div class="antdv-code__preview">
      <pre class="antdv-code__pre"><code
        ref="codeRef"
        class="hljs language-xml"
      /></pre>
    </div>
    <a-button
      v-if="showDownload"
      type="primary"
      class="antdv-code__download"
      @click="onDownload"
    >
      下载 .vue
    </a-button>
  </div>
</template>

<script setup lang="ts">
import { computed, nextTick, ref, watch } from 'vue'
import hljs from 'highlight.js'
import 'highlight.js/styles/github.min.css'

const props = withDefaults(
  defineProps<{
    content: string
    showDownload?: boolean
    downloadFilename?: string
    rows?: number
  }>(),
  { showDownload: true, downloadFilename: 'FormComponent.antd.vue', rows: 16 }
)

const codeRef = ref<HTMLElement | null>(null)
const highlightedContent = computed(() => {
  const code = props.content ?? ''
  return code.trim() ? code : ''
})

function renderHighlightedCode() {
  const element = codeRef.value
  if (!element) return
  element.textContent = highlightedContent.value
  hljs.highlightElement(element)
}

watch(highlightedContent, () => {
  nextTick(() => renderHighlightedCode())
}, { immediate: true })

function onDownload() {
  const blob = new Blob([props.content], { type: 'text/plain;charset=utf-8' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = props.downloadFilename
  a.click()
  URL.revokeObjectURL(url)
}
</script>

<style scoped lang="less">
.antdv-code {
  display: flex;
  flex-direction: column;
  flex: 1;
  min-width: 0;
  .antdv-code__preview {
    flex: 1;
    min-height: 200px;
    overflow: auto;
    border: 1px solid #d9d9d9;
    border-radius: 6px;
    background: #fafafa;
  }
  .antdv-code__pre {
    margin: 0;
    padding: 12px;
    font-size: 12px;
    line-height: 1.5;
    code {
      font-family: ui-monospace, 'SF Mono', Monaco, 'Cascadia Code', monospace;
    }
  }
  .antdv-code__download {
    margin-top: 8px;
  }
}
</style>

