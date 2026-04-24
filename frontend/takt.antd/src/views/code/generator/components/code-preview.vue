<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/code/generator/components -->
<!-- 文件名称：code-preview.vue -->
<!-- 功能描述：生成代码预览（文件列表 + 内容） -->
<!-- ======================================== -->

<template>
  <TaktModal
    v-model:open="open"
    :title="t('common.button.preview')"
    width="80%"
    :centered="true"
    :body-style="{ height: '75vh', overflow: 'auto' }"
    :footer="null"
    destroy-on-close
    @cancel="handleCancel"
  >
    <div
      v-if="loading"
      class="code-preview-loading"
    >
      <a-spin :tip="t('code.generator.page.preview.loading')" />
    </div>
    <div
      v-else-if="!files || files.length === 0"
      class="code-preview-empty"
    >
      <a-empty :description="t('code.generator.page.preview.empty')">
        <template #description>
          <span>{{ t('code.generator.page.preview.emptyhint') }}</span>
        </template>
      </a-empty>
    </div>
    <div
      v-else
      class="code-preview-tabs-wrap"
    >
      <a-alert
        v-if="(validationIssues?.length ?? 0) > 0"
        type="warning"
        show-icon
        :message="t('code.generator.page.preview.validationissuetitle', { count: validationIssues.length })"
        class="preview-validation-alert"
      >
        <template #description>
          <div class="preview-validation-issues">
            <div
              v-for="(issue, idx) in validationIssues"
              :key="`${issue.templateKey}_${idx}`"
              class="preview-validation-issue-item"
            >
              <div class="issue-template">
                {{ issue.templateKey }}
              </div>
              <div
                v-if="issue.targetPath"
                class="issue-path"
              >
                {{ issue.targetPath }}
              </div>
              <div class="issue-message">
                {{ issue.message }}
              </div>
            </div>
          </div>
        </template>
      </a-alert>
      <a-tabs
        v-model:active-key="activeTab"
        class="code-preview-tabs"
      >
        <a-tab-pane
          key="backend"
          :tab="t('code.generator.page.preview.tab.backend')"
        />
        <a-tab-pane
          key="frontend"
          :tab="t('code.generator.page.preview.tab.frontend')"
        />
        <a-tab-pane
          key="script"
          :tab="t('code.generator.page.preview.tab.script')"
        />
      </a-tabs>
      <div class="code-preview-body">
        <div class="file-list">
          <template
            v-for="group in visibleCategoryGroups"
            :key="group.key"
          >
            <div class="file-group-title">
              {{ group.label }}
            </div>
            <div
              v-for="f in group.files"
              :key="f.name"
              class="file-item"
              :class="{ active: selectedFileName === f.name }"
              @click="selectedFileName = f.name"
            >
              <span class="file-item-name">{{ f.name }}</span>
              <a-tag
                v-if="f.isExisting"
                color="orange"
              >
                {{ t('code.generator.page.preview.exists') }}
              </a-tag>
            </div>
          </template>
        </div>
        <div class="file-content">
          <pre class="code-preview-pre"><code
            class="hljs"
            v-html="highlightedHtml"
          /></pre>
        </div>
      </div>
    </div>
  </TaktModal>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import hljs from 'highlight.js'
import 'highlight.js/styles/github.css'
import TaktModal from '@/components/business/takt-modal/index.vue'

/** 代码预览自动识别时限制在生成器常见语言，减轻误判与体积相关开销 */
const PREVIEW_HIGHLIGHT_AUTO_SUBSET = [
  'csharp',
  'typescript',
  'javascript',
  'xml',
  'json',
  'sql',
  'less',
  'css',
  'scss',
  'yaml',
  'markdown',
  'bash',
  'plaintext'
] as const

function escapeHtmlPlain(text: string): string {
  return text
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;')
}

function resolveHighlightLanguage(fileName: string): string | null {
  const base = fileName.split(/[/\\]/).pop() ?? fileName
  const dot = base.lastIndexOf('.')
  if (dot < 0) return null
  const ext = base.slice(dot + 1).toLowerCase()
  const byExt: Record<string, string> = {
    cs: 'csharp',
    ts: 'typescript',
    tsx: 'typescript',
    js: 'javascript',
    jsx: 'javascript',
    mjs: 'javascript',
    cjs: 'javascript',
    vue: 'xml',
    json: 'json',
    sql: 'sql',
    less: 'less',
    css: 'css',
    scss: 'scss',
    sass: 'scss',
    md: 'markdown',
    markdown: 'markdown',
    xml: 'xml',
    html: 'xml',
    htm: 'xml',
    yaml: 'yaml',
    yml: 'yaml',
    csproj: 'xml',
    props: 'xml',
    targets: 'xml',
    sln: 'plaintext'
  }
  return byExt[ext] ?? null
}

function highlightPreviewCode(code: string, fileName: string): string {
  if (!code) return ''
  const lang = resolveHighlightLanguage(fileName)
  if (lang && hljs.getLanguage(lang)) {
    try {
      return hljs.highlight(code, { language: lang, ignoreIllegals: true }).value
    } catch {
      /* 指定语言高亮失败时回退自动检测 */
    }
  }
  try {
    return hljs.highlightAuto(code, [...PREVIEW_HIGHLIGHT_AUTO_SUBSET]).value
  } catch {
    return escapeHtmlPlain(code)
  }
}

export interface PreviewFile {
  name: string
  content: string
  isExisting?: boolean
}
export interface PreviewValidationIssue {
  templateKey: string
  targetPath?: string
  message: string
}
type PreviewTab = 'backend' | 'frontend' | 'script'
type PreviewCategory =
  | 'backendEntity'
  | 'backendDto'
  | 'backendService'
  | 'backendController'
  | 'backendOther'
  | 'frontendApi'
  | 'frontendType'
  | 'frontendView'
  | 'frontendComponent'
  | 'frontendOther'
  | 'scriptTranslationSql'
  | 'scriptMenuSql'
  | 'scriptOther'

const props = withDefaults(
  defineProps<{
    modelValue?: boolean
    files?: PreviewFile[]
    loading?: boolean
    validationIssues?: PreviewValidationIssue[]
  }>(),
  { modelValue: false, files: () => [], loading: false, validationIssues: () => [] }
)

const emit = defineEmits<{
  (e: 'update:modelValue', value: boolean): void
}>()
const { t } = useI18n()

const open = computed({
  get: () => props.modelValue,
  set: (v) => emit('update:modelValue', v)
})

const activeTab = ref<PreviewTab>('backend')
const selectedFileName = ref('')

interface PreviewCategoryGroup {
  key: PreviewCategory
  label: string
  files: PreviewFile[]
}

function normalizePath(path: string): string {
  return path.replace(/\\/g, '/').toLowerCase()
}

function resolveFileTab(path: string): PreviewTab {
  const normalized = normalizePath(path)
  if (normalized.startsWith('frontend/')) return 'frontend'
  if (normalized.endsWith('.sql') || normalized.startsWith('backend/sql/')) return 'script'
  return 'backend'
}

function resolveFileCategory(path: string): PreviewCategory {
  const normalized = normalizePath(path)
  if (normalized.startsWith('frontend/')) {
    if (normalized.includes('/src/api/')) return 'frontendApi'
    if (normalized.includes('/src/types/')) return 'frontendType'
    if (normalized.includes('/src/views/') && normalized.includes('/components/')) return 'frontendComponent'
    if (normalized.includes('/src/views/')) return 'frontendView'
    return 'frontendOther'
  }

  if (normalized.endsWith('.sql') || normalized.startsWith('backend/sql/')) {
    if (normalized.includes('translation')) return 'scriptTranslationSql'
    if (normalized.includes('menu')) return 'scriptMenuSql'
    return 'scriptOther'
  }

  if (normalized.startsWith('backend/src/')) {
    if (normalized.includes('/entities/')) return 'backendEntity'
    if (normalized.includes('/dtos/')) return 'backendDto'
    if (normalized.includes('/controllers/')) return 'backendController'
    if (normalized.includes('/services/')) return 'backendService'
  }
  return 'backendOther'
}

function categoryLabel(category: PreviewCategory): string {
  switch (category) {
    case 'backendEntity':
      return t('code.generator.page.preview.category.backend.entity')
    case 'backendDto':
      return t('code.generator.page.preview.category.backend.dto')
    case 'backendService':
      return t('code.generator.page.preview.category.backend.service')
    case 'backendController':
      return t('code.generator.page.preview.category.backend.controller')
    case 'backendOther':
      return t('code.generator.page.preview.category.backend.other')
    case 'frontendApi':
      return t('code.generator.page.preview.category.frontend.api')
    case 'frontendType':
      return t('code.generator.page.preview.category.frontend.type')
    case 'frontendView':
      return t('code.generator.page.preview.category.frontend.view')
    case 'frontendComponent':
      return t('code.generator.page.preview.category.frontend.component')
    case 'frontendOther':
      return t('code.generator.page.preview.category.frontend.other')
    case 'scriptTranslationSql':
      return t('code.generator.page.preview.category.script.translationsql')
    case 'scriptMenuSql':
      return t('code.generator.page.preview.category.script.menusql')
    case 'scriptOther':
      return t('code.generator.page.preview.category.script.other')
  }
}

const tabFilesMap = computed<Record<PreviewTab, PreviewFile[]>>(() => {
  const map: Record<PreviewTab, PreviewFile[]> = { backend: [], frontend: [], script: [] }
  for (const file of props.files ?? []) {
    map[resolveFileTab(file.name)].push(file)
  }
  return map
})

const visibleCategoryGroups = computed<PreviewCategoryGroup[]>(() => {
  const groups = new Map<PreviewCategory, PreviewFile[]>()
  for (const file of tabFilesMap.value[activeTab.value]) {
    const category = resolveFileCategory(file.name)
    const list = groups.get(category) ?? []
    list.push(file)
    groups.set(category, list)
  }
  return Array.from(groups.entries()).map(([key, files]) => ({
    key,
    label: categoryLabel(key),
    files
  }))
})

watch(
  () => props.files,
  (list) => {
    const files = list ?? []
    const [first] = files
    activeTab.value = first ? resolveFileTab(first.name) : 'backend'
    selectedFileName.value = first?.name ?? ''
  },
  { immediate: true }
)

watch(
  [activeTab, tabFilesMap],
  () => {
    const currentFiles = tabFilesMap.value[activeTab.value]
    if (currentFiles.some((f) => f.name === selectedFileName.value)) return
    const [head] = currentFiles
    selectedFileName.value = head?.name ?? ''
  },
  { immediate: true }
)

const selectedContent = computed(() => {
  const f = (props.files ?? []).find((item) => item.name === selectedFileName.value)
  return f ? f.content : ''
})

const highlightedHtml = computed(() =>
  highlightPreviewCode(selectedContent.value, selectedFileName.value)
)

function handleCancel() {
  emit('update:modelValue', false)
}
</script>

<style scoped lang="less">
.code-preview-empty {
  padding: 24px 0;
  text-align: center;
}

.code-preview-loading {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 280px;
}

.code-preview-tabs-wrap {
  height: 100%;
}

.preview-validation-alert {
  margin-bottom: 8px;
}

.preview-validation-issues {
  max-height: 160px;
  overflow: auto;

  .preview-validation-issue-item {
    margin-bottom: 8px;
    padding-bottom: 8px;
    border-bottom: 1px dashed rgba(0, 0, 0, 0.08);

    .issue-template {
      font-weight: 600;
      color: var(--ant-color-text, #262626);
      word-break: break-all;
    }

    .issue-path {
      color: var(--ant-color-text-secondary, #595959);
      font-size: 12px;
      word-break: break-all;
    }

    .issue-message {
      color: var(--ant-color-error, #cf1322);
      word-break: break-all;
    }
  }
}

.code-preview-tabs {
  margin-bottom: 8px;
}

.code-preview-body {
  display: flex;
  height: 100%;
  min-height: 300px;
  border: 1px solid var(--ant-color-border-secondary, rgba(0, 0, 0, 0.06));
  border-radius: 4px;

  .file-list {
    width: 320px;
    border-right: 1px solid var(--ant-color-border-secondary, rgba(0, 0, 0, 0.06));
    overflow-y: auto;
    

    .file-group-title {
      padding: 8px 12px;
      color: var(--ant-color-text-tertiary, #8c8c8c);
      font-size: 12px;
      border-bottom: 1px dashed var(--ant-color-border-secondary, rgba(0, 0, 0, 0.06));
    }

    .file-item {
      padding: 8px 12px;
      font-size: 12px;
      cursor: pointer;
      word-break: break-all;
      display: flex;
      align-items: center;
      gap: 6px;

      .file-item-name {
        flex: 1;
      }


    }
  }

  .file-content {
    flex: 1;
    overflow: auto;
    padding: 12px;
    background: #808080;
    

    .code-preview-pre {
      margin: 0;
      font-size: 12px;
      line-height: 1.5;
      white-space: pre;
      word-break: normal;
      overflow: auto;
    }

    :deep(.hljs) {
      display: block;
      overflow: auto;
      white-space: pre;
      min-width: max-content;
      padding: 12px;
      font-family: 'Consolas', 'Monaco', monospace;
      background: #bce2e8 !important;
      border-radius: 4px;
      color: var(--ant-color-text, #262626);
    }

    // 让高亮语义色尽量贴近 Ant 主题（亮/暗主题都可读）
    :deep(.hljs-comment),
    :deep(.hljs-quote) {
      color: var(--ant-color-text-tertiary, #8c8c8c);
    }

    :deep(.hljs-keyword),
    :deep(.hljs-selector-tag),
    :deep(.hljs-literal),
    :deep(.hljs-section),
    :deep(.hljs-link) {
      color: var(--ant-color-primary, #1677ff);
    }

    :deep(.hljs-string),
    :deep(.hljs-attr),
    :deep(.hljs-symbol),
    :deep(.hljs-bullet),
    :deep(.hljs-addition) {
      color: var(--ant-color-success, #389e0d);
    }

    :deep(.hljs-number),
    :deep(.hljs-type),
    :deep(.hljs-built_in),
    :deep(.hljs-params),
    :deep(.hljs-meta),
    :deep(.hljs-variable),
    :deep(.hljs-template-variable) {
      color: var(--ant-color-warning, #d48806);
    }

    :deep(.hljs-title),
    :deep(.hljs-name),
    :deep(.hljs-selector-id),
    :deep(.hljs-selector-class),
    :deep(.hljs-function) {
      color: var(--ant-color-info, #1677ff);
    }

  }
}

// dark 主题下统一代码预览文字为浅色，避免 scoped 嵌套选择器失效
:global([data-doc-theme='dark']) .code-preview-body .file-content :deep(.hljs),
:global([data-doc-theme='dark']) .code-preview-body .file-content :deep(.hljs *) {
  color: var(--ant-color-text, #f0f0f0) !important;
}
</style>
