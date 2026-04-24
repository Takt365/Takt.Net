<!-- 敏感词过滤管理：文本检查 / 查找 / 替换 / 高亮 / 词库管理 -->
<template>
  <div class="routine-word-filter">
    <a-card>
      <template #title>
        <a-space>
          <SecurityScanOutlined />
          <span>{{ t('routine.wordfilter.page.title') }}</span>
        </a-space>
      </template>

      <a-tabs
        v-model:active-key="activeTab"
        type="card"
      >
        <a-tab-pane
          key="check"
          :tab="t('routine.wordfilter.tabs.check')"
        >
          <a-card>
            <a-form
              layout="vertical"
              :model="checkForm"
              @finish="handleCheck"
            >
              <a-form-item
                :label="t('routine.wordfilter.check.textLabel')"
                required
              >
                <a-textarea
                  v-model:value="checkForm.text"
                  :rows="6"
                  :placeholder="t('routine.wordfilter.check.placeholder')"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item>
                <a-button
                  type="primary"
                  html-type="submit"
                  :loading="checkLoading"
                >
                  <template #icon>
                    <SearchOutlined />
                  </template>
                  {{ t('routine.wordfilter.check.button') }}
                </a-button>
                <a-button
                  style="margin-left: 8px"
                  @click="handleCheckReset"
                >
                  {{ t('routine.wordfilter.check.reset') }}
                </a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="checkResult">
              <a-alert
                :type="checkResult.containsIllegalWords ? 'error' : 'success'"
                :message="checkResult.containsIllegalWords ? t('routine.wordfilter.check.alertContains') : t('routine.wordfilter.check.alertNotContains')"
                :description="
                  checkResult.containsIllegalWords
                    ? t('routine.wordfilter.check.foundDescription', { count: checkResult.illegalWords.length })
                    : t('routine.wordfilter.check.safeDescription')
                "
                show-icon
                style="margin-bottom: 16px"
              />
              <div v-if="checkResult.illegalWords.length > 0">
                <h4>{{ t('routine.wordfilter.check.illegalWordsTitle') }}</h4>
                <a-tag
                  v-for="(word, index) in checkResult.illegalWords"
                  :key="index"
                  color="red"
                  style="margin: 4px"
                >
                  {{ word }}
                </a-tag>
              </div>
            </div>
          </a-card>
        </a-tab-pane>

        <a-tab-pane
          key="find"
          :tab="t('routine.wordfilter.tabs.find')"
        >
          <a-card>
            <a-form
              layout="vertical"
              :model="findForm"
              @finish="handleFind"
            >
              <a-form-item
                :label="t('routine.wordfilter.find.textLabel')"
                required
              >
                <a-textarea
                  v-model:value="findForm.text"
                  :rows="6"
                  :placeholder="t('routine.wordfilter.find.placeholder')"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item>
                <a-checkbox v-model:checked="findForm.includeDetails">
                  {{ t('routine.wordfilter.find.includeDetails') }}
                </a-checkbox>
              </a-form-item>
              <a-form-item>
                <a-button
                  type="primary"
                  html-type="submit"
                  :loading="findLoading"
                >
                  <template #icon>
                    <SearchOutlined />
                  </template>
                  {{ t('routine.wordfilter.find.button') }}
                </a-button>
                <a-button
                  style="margin-left: 8px"
                  @click="handleFindReset"
                >
                  {{ t('routine.wordfilter.find.reset') }}
                </a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="findResult">
              <a-alert
                type="info"
                :message="t('routine.wordfilter.find.foundMessage', { count: findResult.illegalWords.length })"
                show-icon
                style="margin-bottom: 16px"
              />
              <div v-if="findResult.illegalWords.length > 0">
                <h4>{{ t('routine.wordfilter.find.illegalWordsTitle') }}</h4>
                <a-tag
                  v-for="(word, index) in findResult.illegalWords"
                  :key="index"
                  color="orange"
                  style="margin: 4px"
                >
                  {{ word }}
                </a-tag>
              </div>
              <div
                v-if="findResult.illegalWordDetails && findResult.illegalWordDetails.length > 0"
                style="margin-top: 16px"
              >
                <h4>{{ t('routine.wordfilter.find.detailsTitle') }}</h4>
                <a-table
                  :columns="detailColumns"
                  :data-source="findResult.illegalWordDetails"
                  :pagination="false"
                  size="small"
                  :row-key="detailRowKey"
                >
                  <template #bodyCell="{ column, record }">
                    <template v-if="column.key === 'position'">
                      <span>{{ t('routine.wordfilter.find.positionRange', { start: record.start, end: record.end }) }}</span>
                    </template>
                  </template>
                </a-table>
              </div>
            </div>
          </a-card>
        </a-tab-pane>

        <a-tab-pane
          key="replace"
          :tab="t('routine.wordfilter.tabs.replace')"
        >
          <a-card>
            <a-form
              layout="vertical"
              :model="replaceForm"
              @finish="handleReplace"
            >
              <a-form-item
                :label="t('routine.wordfilter.replace.textLabel')"
                required
              >
                <a-textarea
                  v-model:value="replaceForm.text"
                  :rows="6"
                  :placeholder="t('routine.wordfilter.replace.placeholder')"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item :label="t('routine.wordfilter.replace.modeLabel')">
                <a-radio-group v-model:value="replaceForm.replaceMode">
                  <a-radio value="char">
                    {{ t('routine.wordfilter.replace.modeChar') }}
                  </a-radio>
                  <a-radio value="text">
                    {{ t('routine.wordfilter.replace.modeText') }}
                  </a-radio>
                </a-radio-group>
              </a-form-item>
              <a-form-item
                v-if="replaceForm.replaceMode === 'char'"
                :label="t('routine.wordfilter.replace.charLabel')"
              >
                <a-input
                  v-model:value="replaceForm.replaceChar"
                  :placeholder="t('routine.wordfilter.replace.charPlaceholder')"
                  :maxlength="1"
                  style="width: 200px"
                />
              </a-form-item>
              <a-form-item
                v-if="replaceForm.replaceMode === 'text'"
                :label="t('routine.wordfilter.replace.textLabelValue')"
              >
                <a-input
                  v-model:value="replaceForm.replaceText"
                  :placeholder="t('routine.wordfilter.replace.textPlaceholder')"
                  style="width: 200px"
                />
              </a-form-item>
              <a-form-item>
                <a-button
                  type="primary"
                  html-type="submit"
                  :loading="replaceLoading"
                >
                  <template #icon>
                    <EditOutlined />
                  </template>
                  {{ t('routine.wordfilter.replace.button') }}
                </a-button>
                <a-button
                  style="margin-left: 8px"
                  @click="handleReplaceReset"
                >
                  {{ t('routine.wordfilter.replace.reset') }}
                </a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="replaceResult">
              <a-alert
                type="success"
                :message="t('routine.wordfilter.replace.replacedMessage', { count: replaceResult.replacedCount })"
                show-icon
                style="margin-bottom: 16px"
              />
              <a-form-item :label="t('routine.wordfilter.replace.originalLabel')">
                <a-textarea
                  :value="replaceResult.originalText"
                  :rows="4"
                  readonly
                />
              </a-form-item>
              <a-form-item :label="t('routine.wordfilter.replace.replacedLabel')">
                <a-textarea
                  :value="replaceResult.replacedText"
                  :rows="4"
                  readonly
                />
              </a-form-item>
            </div>
          </a-card>
        </a-tab-pane>

        <a-tab-pane
          key="highlight"
          :tab="t('routine.wordfilter.tabs.highlight')"
        >
          <a-card>
            <a-form
              layout="vertical"
              :model="highlightForm"
              @finish="handleHighlight"
            >
              <a-form-item
                :label="t('routine.wordfilter.highlight.textLabel')"
                required
              >
                <a-textarea
                  v-model:value="highlightForm.text"
                  :rows="6"
                  :placeholder="t('routine.wordfilter.highlight.placeholder')"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item :label="t('routine.wordfilter.highlight.classLabel')">
                <a-input
                  v-model:value="highlightForm.highlightClass"
                  :placeholder="t('routine.wordfilter.highlight.classPlaceholder')"
                  style="width: 200px"
                />
              </a-form-item>
              <a-form-item>
                <a-button
                  type="primary"
                  html-type="submit"
                  :loading="highlightLoading"
                >
                  <template #icon>
                    <EditOutlined />
                  </template>
                  {{ t('routine.wordfilter.highlight.button') }}
                </a-button>
                <a-button
                  style="margin-left: 8px"
                  @click="handleHighlightReset"
                >
                  {{ t('routine.wordfilter.highlight.reset') }}
                </a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="highlightResult">
              <a-alert
                type="success"
                :message="t('routine.wordfilter.highlight.highlightedMessage', { count: highlightResult.highlightedCount })"
                show-icon
                style="margin-bottom: 16px"
              />
              <a-form-item :label="t('routine.wordfilter.highlight.originalLabel')">
                <a-textarea
                  :value="highlightResult.originalText"
                  :rows="4"
                  readonly
                />
              </a-form-item>
              <a-form-item :label="t('routine.wordfilter.highlight.resultLabel')">
                <div
                  class="highlight-preview"
                  v-html="highlightResult.highlightedText"
                />
              </a-form-item>
            </div>
          </a-card>
        </a-tab-pane>

        <a-tab-pane
          key="manage"
          :tab="t('routine.wordfilter.tabs.manage')"
        >
          <a-card>
            <a-space style="margin-bottom: 16px">
              <a-button
                type="primary"
                :loading="wordsLoading"
                @click="handleLoadWords"
              >
                <template #icon>
                  <ReloadOutlined />
                </template>
                {{ t('routine.wordfilter.manage.refresh') }}
              </a-button>
              <a-button
                type="primary"
                :loading="addLoading"
                @click="handleAddWords"
              >
                <template #icon>
                  <PlusOutlined />
                </template>
                {{ t('routine.wordfilter.manage.add') }}
              </a-button>
              <a-button
                danger
                :disabled="selectedWords.length === 0"
                :loading="removeLoading"
                @click="handleRemoveWords"
              >
                <template #icon>
                  <DeleteOutlined />
                </template>
                {{ t('routine.wordfilter.manage.remove') }}
              </a-button>
              <a-button
                danger
                :loading="clearLoading"
                @click="handleClearWords"
              >
                <template #icon>
                  <DeleteOutlined />
                </template>
                {{ t('routine.wordfilter.manage.clear') }}
              </a-button>
              <a-button
                :loading="statsLoading"
                @click="handleLoadStats"
              >
                <template #icon>
                  <InfoCircleOutlined />
                </template>
                {{ t('routine.wordfilter.manage.stats') }}
              </a-button>
            </a-space>

            <a-alert
              v-if="stats"
              type="info"
              :message="
                t('routine.wordfilter.manage.statsMessage', {
                  total: stats.totalCount,
                  status: stats.isInitialized
                    ? t('routine.wordfilter.manage.statusInitialized')
                    : t('routine.wordfilter.manage.statusNotInitialized')
                })
              "
              show-icon
              style="margin-bottom: 16px"
            />

            <a-table
              :columns="wordsColumns"
              :data-source="wordsList"
              :loading="wordsLoading"
              :pagination="wordsPagination"
              :row-selection="wordsRowSelection"
              :row-key="wordsRowKey"
              @change="handleWordsTableChange"
            >
              <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'word'">
                  <a-tag color="red">
                    {{ record }}
                  </a-tag>
                </template>
              </template>
            </a-table>
          </a-card>
        </a-tab-pane>
      </a-tabs>
    </a-card>

    <a-modal
      v-model:open="addWordsVisible"
      :title="t('routine.wordfilter.manage.modalAddTitle')"
      :width="600"
      :confirm-loading="addLoading"
      @ok="handleAddWordsSubmit"
      @cancel="handleAddWordsCancel"
    >
      <a-form layout="vertical">
        <a-form-item :label="t('routine.wordfilter.manage.groupLabel')">
          <a-select
            v-model:value="selectedGroup"
            :placeholder="t('routine.wordfilter.manage.groupPlaceholder')"
            allow-clear
            :loading="groupsLoading"
            show-search
            :filter-option="filterGroupOption"
          >
            <a-select-option
              v-for="group in wordLibraryGroups"
              :key="group.fileName"
              :value="group.fileName"
            >
              {{ t('routine.wordfilter.manage.optionDisplay', { name: group.displayName, count: group.wordCount }) }}
            </a-select-option>
          </a-select>
          <div style="margin-top: 8px; color: #999; font-size: 12px">
            {{ t('routine.wordfilter.manage.groupHint') }}
          </div>
        </a-form-item>
        <a-form-item
          :label="t('routine.wordfilter.manage.addWordsLabel')"
          required
        >
          <a-textarea
            v-model:value="addWordsText"
            :rows="10"
            :placeholder="t('routine.wordfilter.manage.addWordsPlaceholder')"
          />
        </a-form-item>
        <a-form-item>
          <a-alert
            type="info"
            :message="t('routine.wordfilter.manage.tipTitle')"
            :description="t('routine.wordfilter.manage.tipDescription')"
            show-icon
          />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { DefaultOptionType } from 'ant-design-vue/es/select'
import { useI18n } from 'vue-i18n'
import {
  SecurityScanOutlined,
  SearchOutlined,
  EditOutlined,
  ReloadOutlined,
  PlusOutlined,
  DeleteOutlined,
  InfoCircleOutlined
} from '@ant-design/icons-vue'
import {
  checkText,
  findWords,
  replaceWords,
  highlightWords,
  getStats,
  getAllWords,
  addWords,
  removeWords,
  clearWords,
  getWordLibraryFiles
} from '@/api/routine/tasks/wordfilter'
import type {
  CheckTextDto,
  CheckTextResultDto,
  FindWordsDto,
  FindWordsResultDto,
  ReplaceWordsDto,
  ReplaceWordsResultDto,
  HighlightWordsDto,
  HighlightWordsResultDto,
  AddWordsDto,
  RemoveWordsResultDto,
  WordFilterStatsDto,
  IllegalWordDetailDto,
  WordLibraryFileDto
} from '@/types/routine/tasks/wordfilter/word-filter'
import { logger } from '@/utils/logger'

const { t } = useI18n()

function pickErrorMessage(err: unknown, fallback: string): string {
  if (err !== null && typeof err === 'object' && 'message' in err) {
    const m = (err as { message?: unknown }).message
    if (typeof m === 'string' && m.length > 0) {
      return m
    }
  }
  return fallback
}

/** 表单绑定用必填 string，避免 ReplaceWordsDto 可选字段在 EOPT 下与 a-input 不兼容 */
type ReplaceFormState = {
  text: string
  replaceChar: string
  replaceText: string
  replaceMode: 'char' | 'text'
}

const activeTab = ref('check')

const checkForm = ref<CheckTextDto>({
  text: ''
})
const checkLoading = ref(false)
const checkResult = ref<CheckTextResultDto | null>(null)

const handleCheck = async () => {
  if (!checkForm.value.text.trim()) {
    message.warning(t('routine.wordfilter.messages.checkWarning'))
    return
  }

  try {
    checkLoading.value = true
    checkResult.value = await checkText(checkForm.value)
  } catch (error: unknown) {
    logger.error('[Word Filter] 检查文本失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.checkFail')))
  } finally {
    checkLoading.value = false
  }
}

const handleCheckReset = () => {
  checkForm.value = { text: '' }
  checkResult.value = null
}

const findForm = ref<FindWordsDto>({
  text: '',
  includeDetails: false
})
const findLoading = ref(false)
const findResult = ref<FindWordsResultDto | null>(null)

const detailColumns = computed<TableColumnsType>(() => [
  {
    title: t('routine.wordfilter.find.columnKeyword'),
    dataIndex: 'keyword',
    key: 'keyword'
  },
  {
    title: t('routine.wordfilter.find.columnPosition'),
    key: 'position',
    width: 200
  }
])

const detailRowKey = (record: IllegalWordDetailDto, index?: number): string =>
  `${record.keyword}-${index ?? 0}`

const handleFind = async () => {
  if (!findForm.value.text.trim()) {
    message.warning(t('routine.wordfilter.messages.findWarning'))
    return
  }

  try {
    findLoading.value = true
    findResult.value = await findWords(findForm.value)
  } catch (error: unknown) {
    logger.error('[Word Filter] 查找敏感词失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.findFail')))
  } finally {
    findLoading.value = false
  }
}

const handleFindReset = () => {
  findForm.value = { text: '', includeDetails: false }
  findResult.value = null
}

const replaceForm = ref<ReplaceFormState>({
  text: '',
  replaceChar: '*',
  replaceText: '',
  replaceMode: 'char'
})
const replaceLoading = ref(false)
const replaceResult = ref<ReplaceWordsResultDto | null>(null)

const handleReplace = async () => {
  if (!replaceForm.value.text.trim()) {
    message.warning(t('routine.wordfilter.messages.replaceWarning'))
    return
  }

  try {
    replaceLoading.value = true
    const requestData: ReplaceWordsDto = {
      text: replaceForm.value.text
    }

    if (replaceForm.value.replaceMode === 'text' && replaceForm.value.replaceText) {
      requestData.replaceText = replaceForm.value.replaceText
    } else if (replaceForm.value.replaceMode === 'char' && replaceForm.value.replaceChar) {
      requestData.replaceChar = replaceForm.value.replaceChar
    }

    replaceResult.value = await replaceWords(requestData)
  } catch (error: unknown) {
    logger.error('[Word Filter] 替换敏感词失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.replaceFail')))
  } finally {
    replaceLoading.value = false
  }
}

const handleReplaceReset = () => {
  replaceForm.value = {
    text: '',
    replaceChar: '*',
    replaceText: '',
    replaceMode: 'char'
  }
  replaceResult.value = null
}

const highlightForm = ref<HighlightWordsDto>({
  text: '',
  highlightClass: 'illegal-word'
})
const highlightLoading = ref(false)
const highlightResult = ref<HighlightWordsResultDto | null>(null)

const handleHighlight = async () => {
  if (!highlightForm.value.text.trim()) {
    message.warning(t('routine.wordfilter.messages.highlightWarning'))
    return
  }

  try {
    highlightLoading.value = true
    highlightResult.value = await highlightWords(highlightForm.value)
  } catch (error: unknown) {
    logger.error('[Word Filter] 高亮敏感词失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.highlightFail')))
  } finally {
    highlightLoading.value = false
  }
}

const handleHighlightReset = () => {
  highlightForm.value = {
    text: '',
    highlightClass: 'illegal-word'
  }
  highlightResult.value = null
}

const wordsList = ref<string[]>([])
const wordsLoading = ref(false)
const stats = ref<WordFilterStatsDto | null>(null)
const statsLoading = ref(false)
const selectedWords = ref<string[]>([])
const addWordsVisible = ref(false)
const addWordsText = ref('')
const selectedGroup = ref<string | undefined>(undefined)
const wordLibraryGroups = ref<WordLibraryFileDto[]>([])
const groupsLoading = ref(false)
const addLoading = ref(false)
const removeLoading = ref(false)
const clearLoading = ref(false)

const wordsColumns = computed<TableColumnsType>(() => [
  {
    title: t('routine.wordfilter.manage.columnIndex'),
    key: 'index',
    width: 80,
    customRender: ({ index }: { index: number }) => {
      return (wordsPagination.value.current - 1) * wordsPagination.value.pageSize + index + 1
    }
  },
  {
    title: t('routine.wordfilter.manage.columnWord'),
    dataIndex: 'word',
    key: 'word'
  }
])

const wordsPagination = ref({
  current: 1,
  pageSize: 20,
  total: 0,
  showSizeChanger: true,
  showTotal: (total: number) => t('routine.wordfilter.manage.paginationTotal', { total })
})

/** 行键与选中项一致，为敏感词字符串本身（列表中词唯一），供 removeWords 使用。 */
const wordsRowKey = (record: unknown): string => (typeof record === 'string' ? record : String(record ?? ''))

const wordsRowSelection = computed(() => ({
  selectedRowKeys: selectedWords.value,
  onChange: (keys: (string | number)[]) => {
    selectedWords.value = keys.map(k => String(k))
  },
  onSelectAll: (selected: boolean, _selectedRows: readonly unknown[], changeRows: readonly unknown[]) => {
    const changed = changeRows.map(w => String(w))
    if (selected) {
      selectedWords.value = [...new Set([...selectedWords.value, ...changed])]
    } else {
      selectedWords.value = selectedWords.value.filter(word => !changed.includes(word))
    }
  }
}))

const handleLoadWords = async () => {
  try {
    wordsLoading.value = true
    const words = await getAllWords()
    wordsList.value = words
    wordsPagination.value.total = words.length
  } catch (error: unknown) {
    logger.error('[Word Filter] 加载敏感词列表失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.loadFail')))
  } finally {
    wordsLoading.value = false
  }
}

const handleLoadStats = async () => {
  try {
    statsLoading.value = true
    stats.value = await getStats()
    message.success(t('routine.wordfilter.messages.statsSuccess'))
  } catch (error: unknown) {
    logger.error('[Word Filter] 加载统计信息失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.loadFail')))
  } finally {
    statsLoading.value = false
  }
}

const handleAddWords = async () => {
  addWordsText.value = ''
  selectedGroup.value = undefined
  addWordsVisible.value = true

  if (wordLibraryGroups.value.length === 0) {
    await loadWordLibraryGroups()
  }
}

const loadWordLibraryGroups = async () => {
  try {
    groupsLoading.value = true
    wordLibraryGroups.value = await getWordLibraryFiles()
  } catch (error: unknown) {
    logger.error('[Word Filter] 加载词库组列表失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.loadGroupsFail')))
  } finally {
    groupsLoading.value = false
  }
}

const filterGroupOption = (input: string, option?: DefaultOptionType): boolean => {
  const q = input.trim().toLowerCase()
  const v = option?.value
  const fileName = v != null ? String(v) : ''
  const group = wordLibraryGroups.value.find(g => g.fileName === fileName)
  const label = group?.displayName ?? fileName
  return label.toLowerCase().includes(q)
}

const handleAddWordsSubmit = async () => {
  if (!addWordsText.value.trim()) {
    message.warning(t('routine.wordfilter.messages.addWarning'))
    return
  }

  const words = addWordsText.value
    .split('\n')
    .map(w => w.trim())
    .filter(w => w.length > 0)

  if (words.length === 0) {
    message.warning(t('routine.wordfilter.messages.addWarningEmpty'))
    return
  }

  try {
    addLoading.value = true
    const requestData: AddWordsDto = { words }
    if (selectedGroup.value) {
      requestData.group = selectedGroup.value
    }

    const result = await addWords(requestData)
    const groupName = selectedGroup.value
      ? wordLibraryGroups.value.find(g => g.fileName === selectedGroup.value)?.displayName ?? selectedGroup.value
      : ''
    if (selectedGroup.value) {
      message.success(
        t('routine.wordfilter.messages.addSuccessGroup', {
          count: result.addedCount,
          total: result.totalCount,
          group: groupName
        })
      )
    } else {
      message.success(
        t('routine.wordfilter.messages.addSuccessMemory', {
          count: result.addedCount,
          total: result.totalCount
        })
      )
    }
    addWordsVisible.value = false
    addWordsText.value = ''
    selectedGroup.value = undefined
    await handleLoadWords()
    await handleLoadStats()
    await loadWordLibraryGroups()
  } catch (error: unknown) {
    logger.error('[Word Filter] 添加敏感词失败:', error)
    message.error(pickErrorMessage(error, t('routine.wordfilter.messages.addFail')))
  } finally {
    addLoading.value = false
  }
}

const handleAddWordsCancel = () => {
  addWordsVisible.value = false
  addWordsText.value = ''
  selectedGroup.value = undefined
}

const handleRemoveWords = () => {
  if (selectedWords.value.length === 0) {
    message.warning(t('routine.wordfilter.messages.removeWarning'))
    return
  }

  Modal.confirm({
    title: t('routine.wordfilter.manage.removeConfirmTitle'),
    content: t('routine.wordfilter.manage.removeConfirmContent', { count: selectedWords.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        removeLoading.value = true
        const result: RemoveWordsResultDto = await removeWords({ words: selectedWords.value })
        message.success(
          t('routine.wordfilter.messages.removeSuccess', {
            count: result.removedCount,
            remaining: result.remainingCount
          })
        )
        selectedWords.value = []
        await handleLoadWords()
        await handleLoadStats()
      } catch (error: unknown) {
        logger.error('[Word Filter] 移除敏感词失败:', error)
        message.error(pickErrorMessage(error, t('routine.wordfilter.messages.removeFail')))
      } finally {
        removeLoading.value = false
      }
    }
  })
}

const handleClearWords = () => {
  Modal.confirm({
    title: t('routine.wordfilter.manage.clearConfirmTitle'),
    content: t('routine.wordfilter.manage.clearConfirmContent'),
    okType: 'danger',
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        clearLoading.value = true
        await clearWords()
        message.success(t('routine.wordfilter.messages.clearSuccess'))
        wordsList.value = []
        wordsPagination.value.total = 0
        selectedWords.value = []
        await handleLoadStats()
      } catch (error: unknown) {
        logger.error('[Word Filter] 清空敏感词库失败:', error)
        message.error(pickErrorMessage(error, t('routine.wordfilter.messages.clearFail')))
      } finally {
        clearLoading.value = false
      }
    }
  })
}

const handleWordsTableChange = (pagination?: TablePaginationConfig) => {
  if (pagination == null) return
  if (pagination.current != null) {
    wordsPagination.value.current = pagination.current
  }
  if (pagination.pageSize != null) {
    wordsPagination.value.pageSize = pagination.pageSize
  }
}

onMounted(async () => {
  await handleLoadStats()
  await handleLoadWords()
  await loadWordLibraryGroups()
})
</script>

<style scoped lang="less">
.routine-word-filter {
  padding: 16px;

  .highlight-preview {
    padding: 12px;
    border: 1px solid #d9d9d9;
    border-radius: 4px;
    background-color: #fafafa;
    min-height: 100px;

    :deep(.illegal-word) {
      background-color: #ff4d4f;
      color: #fff;
      padding: 2px 4px;
      border-radius: 2px;
      font-weight: bold;
    }
  }
}
</style>
