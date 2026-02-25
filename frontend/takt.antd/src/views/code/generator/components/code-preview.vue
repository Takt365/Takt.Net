<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/code/generator/components -->
<!-- 文件名称：code-preview.vue -->
<!-- 功能描述：代码预览展示由后端根据当前表配置与模板实时渲染生成的内容（非磁盘已生成文件）；修改配置或模板后重新预览即可看到更新 -->
<!-- ======================================== -->

<template>
  <TaktModal
    v-model:open="open"
    :title="t('code.generator.previewTitle')"
    width="85%"
    :centered="true"
    :body-style="{ height: '78vh', overflow: 'hidden', display: 'flex', flexDirection: 'column' }"
    :footer="null"
    destroy-on-close
    @cancel="handleCancel"
  >
    <div v-if="!files || files.length === 0" class="code-preview-empty">
      <a-empty :description="t('code.generator.previewEmpty')">
        <template #description>
          <span>{{ t('code.generator.previewHint') }}</span>
          <span>{{ t('code.generator.previewHintDetail') }}</span>
        </template>
      </a-empty>
    </div>
    <div v-else class="code-preview-body">
      <a-tabs v-model:activeKey="activeTabKey" type="card" class="preview-tabs">
        <a-tab-pane
          v-for="tab in visibleTabs"
          :key="tab.key"
          :tab="tab.label"
        >
          <div class="tab-pane-inner">
            <div class="file-list">
              <div
                v-for="f in tab.files"
                :key="f.name"
                class="file-item"
                :class="{ active: selectedFileInTab === f.name }"
                @click="selectFileInTab(tab.key, f)"
              >
                {{ getFileShortName(f.name) }}
              </div>
            </div>
            <div class="file-content">
              <highlightjs
                v-if="displayContent"
                :language="highlightLanguage"
                :code="displayContent"
                class="code-preview-highlight"
              />
              <div v-else-if="selectedFileInTab" class="code-preview-placeholder">{{ t('code.generator.previewFileEmpty') }}</div>
            </div>
          </div>
        </a-tab-pane>
      </a-tabs>
    </div>
  </TaktModal>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import TaktModal from '@/components/business/takt-modal/index.vue'
import { logger } from '@/utils/logger'
import 'highlight.js/lib/common'
import 'highlight.js/styles/github.min.css'
import hljsVuePlugin from '@highlightjs/vue-plugin'

const highlightjs = hljsVuePlugin.component
const { t } = useI18n()

export interface PreviewFile {
  name: string
  content: string
}

/** 分类键与显示顺序（标签从 code.generator.previewTabs 取） */
const TAB_ORDER: { key: string }[] = [
  { key: 'entity' },
  { key: 'dto' },
  { key: 'service' },
  { key: 'controller' },
  { key: 'types' },
  { key: 'api' },
  { key: 'i18n' },
  { key: 'view' },
  { key: 'form' },
  { key: 'sql' }
]

/** 根据文件路径（name）推断分类 */
function getCategoryKey(name: string): string {
  const p = name.replace(/\\/g, '/').toLowerCase()
  if (p.endsWith('.sql')) return 'sql'
  if (/\/entities\/.+\.cs$/i.test(p) && !p.includes('dtos')) return 'entity'
  if (p.includes('/dtos/') || p.endsWith('dtos.cs')) return 'dto'
  if (p.includes('iservice') && p.endsWith('.cs')) return 'service'
  if (p.includes('/services/') && p.endsWith('service.cs')) return 'service'
  if (p.includes('/controllers/') && p.endsWith('controller.cs')) return 'controller'
  if (p.endsWith('.d.ts')) return 'types'
  if (p.includes('/api/') && p.endsWith('.ts') && !p.endsWith('.d.ts')) return 'api'
  if (p.includes('/locales/')) return 'i18n'
  if (p.includes('/views/') && p.endsWith('/index.vue')) return 'view'
  if (p.includes('form') && p.endsWith('.vue')) return 'form'
  return 'other'
}

function getFileShortName(name: string): string {
  const parts = name.replace(/\\/g, '/').split('/')
  return parts[parts.length - 1] || name
}

/** 根据文件名推断 highlight.js 语言（供 @highlightjs/vue-plugin 的 language 使用） */
function getLanguageFromFileName(name: string): string {
  const p = (name ?? '').replace(/\\/g, '/').toLowerCase()
  if (p.endsWith('.cs')) return 'csharp'
  if (p.endsWith('.ts') || p.endsWith('.d.ts')) return 'typescript'
  if (p.endsWith('.vue')) return 'xml'
  if (p.endsWith('.sql')) return 'sql'
  if (p.endsWith('.js')) return 'javascript'
  if (p.endsWith('.json')) return 'json'
  if (p.endsWith('.css')) return 'css'
  if (p.endsWith('.less') || p.endsWith('.scss')) return 'scss'
  if (p.endsWith('.html')) return 'xml'
  return 'plaintext'
}

const props = withDefaults(
  defineProps<{
    modelValue?: boolean
    files?: PreviewFile[]
  }>(),
  { modelValue: false, files: () => [] }
)

const emit = defineEmits<{
  (e: 'update:modelValue', value: boolean): void
}>()

const open = computed({
  get: () => props.modelValue,
  set: (v) => emit('update:modelValue', v)
})

/** 按分类分组后的 tab 列表（仅包含有文件的分类，保持顺序；标签本地化） */
const visibleTabs = computed(() => {
  const list = props.files ?? []
  const byKey = new Map<string, PreviewFile[]>()
  for (const f of list) {
    const key = getCategoryKey(f.name)
    if (!byKey.has(key)) byKey.set(key, [])
    byKey.get(key)!.push(f)
  }
  const result: { key: string; label: string; files: PreviewFile[] }[] = []
  for (const tab of TAB_ORDER) {
    const files = byKey.get(tab.key)
    if (files?.length) result.push({ key: tab.key, label: t('code.generator.previewTabs.' + tab.key), files })
  }
  const other = byKey.get('other')
  if (other?.length) result.push({ key: 'other', label: t('code.generator.previewTabs.other'), files: other })
  return result
})

const activeTabKey = ref<string>('')
const selectedFileInTab = ref<string>('')

/** 路径规范化后比较，避免 \ 与 / 导致查找失败 */
function pathEqual(a: string, b: string): boolean {
  if (a === b) return true
  const na = (a ?? '').replace(/\\/g, '/')
  const nb = (b ?? '').replace(/\\/g, '/')
  return na === nb
}

/** 从文件项取内容，兼容 content / Content */
function getFileContent(f: PreviewFile | { name: string; content?: string; Content?: string }): string {
  const raw = (f as PreviewFile).content ?? (f as { Content?: string }).Content
  if (raw == null) return ''
  return typeof raw !== 'string' ? String(raw) : raw
}

/** 根据当前选中的文件名从 files 中取 content，保证与列表同源、响应式更新 */
const displayContent = computed(() => {
  const name = selectedFileInTab.value
  if (!name) return ''
  const list = props.files ?? []
  const file = list.find((f) => pathEqual(f.name, name))
  return file ? getFileContent(file) : ''
})

/** 当前选中文件对应的 highlight.js 语言（供 <highlightjs> 使用） */
const highlightLanguage = computed(() => getLanguageFromFileName(selectedFileInTab.value ?? ''))

/** 输出当前展示的具体内容日志：选中文件、内容长度、内容前若干字符 */
watch(
  displayContent,
  (content, _oldContent) => {
    const name = selectedFileInTab.value
    const len = (content ?? '').length
    const preview = (content ?? '').slice(0, 100).replace(/\n/g, '↵')
    logger.debug('[CodePreview] 当前展示: selectedFile=', name, 'contentLength=', len, '内容前100字=', preview + (len > 100 ? '...' : ''))
  },
  { immediate: true }
)

watch(
  visibleTabs,
  (tabs) => {
    logger.debug('[CodePreview] visibleTabs 变化: tabsCount=', tabs.length, 'tabKeys=', tabs.map((t) => t.key))
    if (tabs.length) {
      const first = tabs[0]
      activeTabKey.value = first.key
      const f = first.files[0]
      if (f) {
        selectedFileInTab.value = f.name
        logger.debug('[CodePreview] 初始选中: tab=', first.key, 'file=', f.name, 'contentLength=', getFileContent(f).length)
      }
    } else {
      activeTabKey.value = ''
      selectedFileInTab.value = ''
    }
  },
  { immediate: true }
)

watch(activeTabKey, (key) => {
  const tab = visibleTabs.value.find((t) => t.key === key)
  if (tab?.files?.length) {
    const f = tab.files[0]
    selectedFileInTab.value = f.name
  }
})

function selectFileInTab(_tabKey: string, f: PreviewFile) {
  logger.debug('[CodePreview] 选中文件: name=', f.name, 'contentLength=', getFileContent(f).length)
  selectedFileInTab.value = f.name
}

watch(
  () => props.files,
  (list) => {
    const len = list?.length ?? 0
    logger.debug('[CodePreview] props.files 变化: count=', len, len ? '首项 name=' + list![0].name + ' contentLen=' + getFileContent(list![0]).length : '')
    const tabs = visibleTabs.value
    if (tabs.length && list?.length) {
      const cur = selectedFileInTab.value
      const stillExists = list.some((f) => pathEqual(f.name, cur))
      if (!stillExists && tabs[0]?.files?.[0]) {
        selectedFileInTab.value = tabs[0].files[0].name
      }
    }
  }
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

.code-preview-body {
  display: flex;
  flex-direction: column;
  height: 100%;
  min-height: 360px;
  border: 1px solid rgba(0, 0, 0, 0.06);
  border-radius: 4px;
  overflow: hidden;

  :deep(.preview-tabs) {
    display: flex;
    flex-direction: column;
    height: 100%;
    overflow: hidden;

    .ant-tabs-content {
      height: 100%;
      overflow: hidden;
    }
    .ant-tabs-tabpane {
      height: 100%;
      overflow: hidden;
    }
    .ant-tabs-card > .ant-tabs-nav .ant-tabs-tab {
      padding: 6px 12px;
    }
  }

  .tab-pane-inner {
    display: flex;
    height: 100%;
    min-height: 280px;
    overflow: hidden;
  }

  .file-list {
    width: 200px;
    flex-shrink: 0;
    border-right: 1px solid var(--ant-color-border, rgba(0, 0, 0, 0.06));
    overflow-y: auto;
    background: var(--ant-color-bg-layout, transparent);

    .file-item {
      padding: 8px 12px;
      font-size: 12px;
      cursor: pointer;
      word-break: break-all;

      &:hover {
        background: var(--ant-color-fill-quaternary, rgba(0, 0, 0, 0.04));
      }

      &.active {
        background: var(--ant-color-primary-bg, rgba(22, 119, 255, 0.08));
        color: var(--ant-color-primary);
      }
    }
  }

  .file-content {
    flex: 1;
    overflow: auto;
    padding: 12px;
    position: relative;
    background: #fef4f4;

    .code-preview-placeholder {
      color: var(--ant-color-text-tertiary, #999);
      font-size: 12px;
      padding: 8px 0;
    }

    :deep(.code-preview-highlight) {
      margin: 0;

      pre,
      pre.hljs {
        margin: 0;
        font-size: 12px;
        line-height: 1.5;
        white-space: pre-wrap;
        word-break: break-all;
        background: #fef4f4;
      }

      code,
      code.hljs {
        font-family: 'Consolas', 'Monaco', monospace;
        background: transparent;
      }
    }
  }
}
</style>
