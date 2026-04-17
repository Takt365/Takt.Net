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
      <a-spin :tip="t('code.generator.previewLoading')" />
    </div>
    <div
      v-else-if="!files || files.length === 0"
      class="code-preview-empty"
    >
      <a-empty :description="t('code.generator.previewEmpty')">
        <template #description>
          <span>{{ t('code.generator.previewEmptyHint') }}</span>
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
        :message="t('code.generator.previewValidationIssueTitle', { count: validationIssues.length })"
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
          :tab="t('code.generator.previewTabBackend')"
        />
        <a-tab-pane
          key="frontend"
          :tab="t('code.generator.previewTabFrontend')"
        />
        <a-tab-pane
          key="script"
          :tab="t('code.generator.previewTabScript')"
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
                {{ t('code.generator.previewExists') }}
              </a-tag>
            </div>
          </template>
        </div>
        <div class="file-content">
          <pre><code>{{ selectedContent }}</code></pre>
        </div>
      </div>
    </div>
  </TaktModal>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import TaktModal from '@/components/business/takt-modal/index.vue'

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
      return t('code.generator.previewCategoryBackendEntity')
    case 'backendDto':
      return t('code.generator.previewCategoryBackendDto')
    case 'backendService':
      return t('code.generator.previewCategoryBackendService')
    case 'backendController':
      return t('code.generator.previewCategoryBackendController')
    case 'backendOther':
      return t('code.generator.previewCategoryOther')
    case 'frontendApi':
      return t('code.generator.previewCategoryFrontendApi')
    case 'frontendType':
      return t('code.generator.previewCategoryFrontendType')
    case 'frontendView':
      return t('code.generator.previewCategoryFrontendView')
    case 'frontendComponent':
      return t('code.generator.previewCategoryFrontendComponent')
    case 'frontendOther':
      return t('code.generator.previewCategoryOther')
    case 'scriptTranslationSql':
      return t('code.generator.previewCategoryScriptTranslationSql')
    case 'scriptMenuSql':
      return t('code.generator.previewCategoryScriptMenuSql')
    case 'scriptOther':
      return t('code.generator.previewCategoryOther')
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
    const preferredTab = files.length > 0 ? resolveFileTab(files[0].name) : 'backend'
    activeTab.value = preferredTab
    selectedFileName.value = files.length > 0 ? files[0].name : ''
  },
  { immediate: true }
)

watch(
  [activeTab, tabFilesMap],
  () => {
    const currentFiles = tabFilesMap.value[activeTab.value]
    if (currentFiles.some((f) => f.name === selectedFileName.value)) return
    selectedFileName.value = currentFiles.length > 0 ? currentFiles[0].name : ''
  },
  { immediate: true }
)

const selectedContent = computed(() => {
  const f = (props.files ?? []).find((item) => item.name === selectedFileName.value)
  return f ? f.content : ''
})

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
      color: #262626;
      word-break: break-all;
    }

    .issue-path {
      color: #595959;
      font-size: 12px;
      word-break: break-all;
    }

    .issue-message {
      color: #cf1322;
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
  border: 1px solid rgba(0, 0, 0, 0.06);
  border-radius: 4px;

  .file-list {
    width: 320px;
    border-right: 1px solid rgba(0, 0, 0, 0.06);
    overflow-y: auto;
    background: #fafafa;

    .file-group-title {
      padding: 8px 12px;
      color: #8c8c8c;
      font-size: 12px;
      border-bottom: 1px dashed rgba(0, 0, 0, 0.06);
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

      &:hover {
        background: #f0f0f0;
      }

      &.active {
        background: #e6f7ff;
        color: #1890ff;
      }
    }
  }

  .file-content {
    flex: 1;
    overflow: auto;
    padding: 12px;
    background: #fff;

    pre {
      margin: 0;
      font-size: 12px;
      line-height: 1.5;
      white-space: pre-wrap;
      word-break: break-all;
    }

    code {
      font-family: 'Consolas', 'Monaco', monospace;
    }
  }
}
</style>
