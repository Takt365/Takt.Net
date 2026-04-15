<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/word-filter -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-22 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：敏感词过滤管理页面，包含文本检查、查找、替换、高亮、敏感词管理等功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-word-filter">
    <a-card>
      <template #title>
        <a-space>
          <SecurityScanOutlined />
          <span>敏感词过滤管理</span>
        </a-space>
      </template>

      <a-tabs v-model:activeKey="activeTab" type="card">
        <!-- 文本检查标签页 -->
        <a-tab-pane key="check" tab="文本检查">
          <a-card>
            <a-form layout="vertical" :model="checkForm" @finish="handleCheck">
              <a-form-item label="待检查文本" required>
                <a-textarea
                  v-model:value="checkForm.text"
                  :rows="6"
                  placeholder="请输入要检查的文本"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item>
                <a-button type="primary" html-type="submit" :loading="checkLoading">
                  <template #icon><SearchOutlined /></template>
                  检查
                </a-button>
                <a-button style="margin-left: 8px" @click="handleCheckReset">重置</a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="checkResult">
              <a-alert
                :type="checkResult.containsIllegalWords ? 'error' : 'success'"
                :message="checkResult.containsIllegalWords ? '检测到敏感词' : '未检测到敏感词'"
                :description="checkResult.containsIllegalWords ? `发现 ${checkResult.illegalWords.length} 个敏感词` : '文本安全'"
                show-icon
                style="margin-bottom: 16px"
              />
              <div v-if="checkResult.illegalWords.length > 0">
                <h4>敏感词列表：</h4>
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

        <!-- 查找敏感词标签页 -->
        <a-tab-pane key="find" tab="查找敏感词">
          <a-card>
            <a-form layout="vertical" :model="findForm" @finish="handleFind">
              <a-form-item label="待查找文本" required>
                <a-textarea
                  v-model:value="findForm.text"
                  :rows="6"
                  placeholder="请输入要查找敏感词的文本"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item>
                <a-checkbox v-model:checked="findForm.includeDetails">包含位置信息</a-checkbox>
              </a-form-item>
              <a-form-item>
                <a-button type="primary" html-type="submit" :loading="findLoading">
                  <template #icon><SearchOutlined /></template>
                  查找
                </a-button>
                <a-button style="margin-left: 8px" @click="handleFindReset">重置</a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="findResult">
              <a-alert
                type="info"
                :message="`找到 ${findResult.illegalWords.length} 个敏感词`"
                show-icon
                style="margin-bottom: 16px"
              />
              <div v-if="findResult.illegalWords.length > 0">
                <h4>敏感词列表：</h4>
                <a-tag
                  v-for="(word, index) in findResult.illegalWords"
                  :key="index"
                  color="orange"
                  style="margin: 4px"
                >
                  {{ word }}
                </a-tag>
              </div>
              <div v-if="findResult.illegalWordDetails && findResult.illegalWordDetails.length > 0" style="margin-top: 16px">
                <h4>位置信息：</h4>
                <a-table
                  :columns="detailColumns"
                  :data-source="findResult.illegalWordDetails"
                  :pagination="false"
                  size="small"
                  :row-key="(record, index) => `${record.keyword}-${index}`"
                >
                  <template #bodyCell="{ column, record }">
                    <template v-if="column.key === 'position'">
                      <span>第 {{ record.start }} - {{ record.end }} 个字符</span>
                    </template>
                  </template>
                </a-table>
              </div>
            </div>
          </a-card>
        </a-tab-pane>

        <!-- 替换敏感词标签页 -->
        <a-tab-pane key="replace" tab="替换敏感词">
          <a-card>
            <a-form layout="vertical" :model="replaceForm" @finish="handleReplace">
              <a-form-item label="待替换文本" required>
                <a-textarea
                  v-model:value="replaceForm.text"
                  :rows="6"
                  placeholder="请输入要替换敏感词的文本"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item label="替换方式">
                <a-radio-group v-model:value="replaceForm.replaceMode">
                  <a-radio value="char">使用字符替换</a-radio>
                  <a-radio value="text">使用文本替换</a-radio>
                </a-radio-group>
              </a-form-item>
              <a-form-item v-if="replaceForm.replaceMode === 'char'" label="替换字符">
                <a-input
                  v-model:value="replaceForm.replaceChar"
                  placeholder="请输入替换字符（如：*）"
                  :maxlength="1"
                  style="width: 200px"
                />
              </a-form-item>
              <a-form-item v-if="replaceForm.replaceMode === 'text'" label="替换文本">
                <a-input
                  v-model:value="replaceForm.replaceText"
                  placeholder="请输入替换文本（如：***）"
                  style="width: 200px"
                />
              </a-form-item>
              <a-form-item>
                <a-button type="primary" html-type="submit" :loading="replaceLoading">
                  <template #icon><EditOutlined /></template>
                  替换
                </a-button>
                <a-button style="margin-left: 8px" @click="handleReplaceReset">重置</a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="replaceResult">
              <a-alert
                type="success"
                :message="`已替换 ${replaceResult.replacedCount} 个敏感词`"
                show-icon
                style="margin-bottom: 16px"
              />
              <a-form-item label="原始文本">
                <a-textarea :value="replaceResult.originalText" :rows="4" readonly />
              </a-form-item>
              <a-form-item label="替换后文本">
                <a-textarea :value="replaceResult.replacedText" :rows="4" readonly />
              </a-form-item>
            </div>
          </a-card>
        </a-tab-pane>

        <!-- 高亮敏感词标签页 -->
        <a-tab-pane key="highlight" tab="高亮敏感词">
          <a-card>
            <a-form layout="vertical" :model="highlightForm" @finish="handleHighlight">
              <a-form-item label="待高亮文本" required>
                <a-textarea
                  v-model:value="highlightForm.text"
                  :rows="6"
                  placeholder="请输入要高亮敏感词的文本"
                  :maxlength="5000"
                  show-count
                />
              </a-form-item>
              <a-form-item label="高亮样式类名">
                <a-input
                  v-model:value="highlightForm.highlightClass"
                  placeholder="默认：illegal-word"
                  style="width: 200px"
                />
              </a-form-item>
              <a-form-item>
                <a-button type="primary" html-type="submit" :loading="highlightLoading">
                  <template #icon><EditOutlined /></template>
                  高亮
                </a-button>
                <a-button style="margin-left: 8px" @click="handleHighlightReset">重置</a-button>
              </a-form-item>
            </a-form>

            <a-divider />

            <div v-if="highlightResult">
              <a-alert
                type="success"
                :message="`已高亮 ${highlightResult.highlightedCount} 个敏感词`"
                show-icon
                style="margin-bottom: 16px"
              />
              <a-form-item label="原始文本">
                <a-textarea :value="highlightResult.originalText" :rows="4" readonly />
              </a-form-item>
              <a-form-item label="高亮后文本（HTML）">
                <div
                  class="highlight-preview"
                  v-html="highlightResult.highlightedText"
                />
              </a-form-item>
            </div>
          </a-card>
        </a-tab-pane>

        <!-- 敏感词管理标签页 -->
        <a-tab-pane key="manage" tab="敏感词管理">
          <a-card>
            <a-space style="margin-bottom: 16px">
              <a-button type="primary" @click="handleLoadWords" :loading="wordsLoading">
                <template #icon><ReloadOutlined /></template>
                刷新列表
              </a-button>
              <a-button type="primary" @click="handleAddWords" :loading="addLoading">
                <template #icon><PlusOutlined /></template>
                添加敏感词
              </a-button>
              <a-button
                danger
                :disabled="selectedWords.length === 0"
                @click="handleRemoveWords"
                :loading="removeLoading"
              >
                <template #icon><DeleteOutlined /></template>
                移除选中
              </a-button>
              <a-button
                danger
                @click="handleClearWords"
                :loading="clearLoading"
              >
                <template #icon><DeleteOutlined /></template>
                清空词库
              </a-button>
              <a-button @click="handleLoadStats" :loading="statsLoading">
                <template #icon><InfoCircleOutlined /></template>
                查看统计
              </a-button>
            </a-space>

            <a-alert
              v-if="stats"
              type="info"
              :message="`敏感词总数：${stats.totalCount}，状态：${stats.isInitialized ? '已初始化' : '未初始化'}`"
              show-icon
              style="margin-bottom: 16px"
            />

            <a-table
              :columns="wordsColumns"
              :data-source="wordsList"
              :loading="wordsLoading"
              :pagination="wordsPagination"
              :row-selection="wordsRowSelection"
              @change="handleWordsTableChange"
              :row-key="(record, index) => record || index"
            >
              <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'word'">
                  <a-tag color="red">{{ record }}</a-tag>
                </template>
              </template>
            </a-table>
          </a-card>
        </a-tab-pane>
      </a-tabs>
    </a-card>

    <!-- 添加敏感词对话框 -->
    <a-modal
      v-model:open="addWordsVisible"
      title="添加敏感词"
      :width="600"
      :confirm-loading="addLoading"
      @ok="handleAddWordsSubmit"
      @cancel="handleAddWordsCancel"
    >
      <a-form layout="vertical">
        <a-form-item label="词库组（可选）">
          <a-select
            v-model:value="selectedGroup"
            placeholder="选择词库组（不选择则只添加到内存词库）"
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
              {{ group.displayName }} ({{ group.wordCount }} 词)
            </a-select-option>
          </a-select>
          <div style="margin-top: 8px; color: #999; font-size: 12px">
            选择词库组后，敏感词将同时添加到内存词库和对应的词库文件中
          </div>
        </a-form-item>
        <a-form-item label="敏感词（每行一个）" required>
          <a-textarea
            v-model:value="addWordsText"
            :rows="10"
            placeholder="请输入敏感词，每行一个"
          />
        </a-form-item>
        <a-form-item>
          <a-alert
            type="info"
            message="提示"
            description="可以一次添加多个敏感词，每行一个。重复的敏感词会被自动忽略。"
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
import type { TableColumnsType } from 'ant-design-vue'
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
} from '@/types/routine/tasks/wordfilter'
import { logger } from '@/utils/logger'

// 当前激活的标签页
const activeTab = ref('check')

// ========== 文本检查 ==========
const checkForm = ref<CheckTextDto>({
  text: ''
})
const checkLoading = ref(false)
const checkResult = ref<CheckTextResultDto | null>(null)

const handleCheck = async () => {
  if (!checkForm.value.text.trim()) {
    message.warning('请输入要检查的文本')
    return
  }

  try {
    checkLoading.value = true
    checkResult.value = await checkText(checkForm.value)
  } catch (error: any) {
    logger.error('[Word Filter] 检查文本失败:', error)
    message.error(error.message || '检查失败')
  } finally {
    checkLoading.value = false
  }
}

const handleCheckReset = () => {
  checkForm.value = { text: '' }
  checkResult.value = null
}

// ========== 查找敏感词 ==========
const findForm = ref<FindWordsDto>({
  text: '',
  includeDetails: false
})
const findLoading = ref(false)
const findResult = ref<FindWordsResultDto | null>(null)

const detailColumns: TableColumnsType = [
  {
    title: '敏感词',
    dataIndex: 'keyword',
    key: 'keyword'
  },
  {
    title: '位置',
    key: 'position',
    width: 200
  }
]

const handleFind = async () => {
  if (!findForm.value.text.trim()) {
    message.warning('请输入要查找的文本')
    return
  }

  try {
    findLoading.value = true
    findResult.value = await findWords(findForm.value)
  } catch (error: any) {
    logger.error('[Word Filter] 查找敏感词失败:', error)
    message.error(error.message || '查找失败')
  } finally {
    findLoading.value = false
  }
}

const handleFindReset = () => {
  findForm.value = { text: '', includeDetails: false }
  findResult.value = null
}

// ========== 替换敏感词 ==========
const replaceForm = ref<ReplaceWordsDto & { replaceMode: 'char' | 'text' }>({
  text: '',
  replaceChar: '*',
  replaceText: '',
  replaceMode: 'char'
})
const replaceLoading = ref(false)
const replaceResult = ref<ReplaceWordsResultDto | null>(null)

const handleReplace = async () => {
  if (!replaceForm.value.text.trim()) {
    message.warning('请输入要替换的文本')
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
  } catch (error: any) {
    logger.error('[Word Filter] 替换敏感词失败:', error)
    message.error(error.message || '替换失败')
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

// ========== 高亮敏感词 ==========
const highlightForm = ref<HighlightWordsDto>({
  text: '',
  highlightClass: 'illegal-word'
})
const highlightLoading = ref(false)
const highlightResult = ref<HighlightWordsResultDto | null>(null)

const handleHighlight = async () => {
  if (!highlightForm.value.text.trim()) {
    message.warning('请输入要高亮的文本')
    return
  }

  try {
    highlightLoading.value = true
    highlightResult.value = await highlightWords(highlightForm.value)
  } catch (error: any) {
    logger.error('[Word Filter] 高亮敏感词失败:', error)
    message.error(error.message || '高亮失败')
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

// ========== 敏感词管理 ==========
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

const wordsColumns: TableColumnsType = [
  {
    title: '序号',
    key: 'index',
    width: 80,
    customRender: ({ index }: { index: number }) => {
      return (wordsPagination.value.current - 1) * wordsPagination.value.pageSize + index + 1
    }
  },
  {
    title: '敏感词',
    dataIndex: 'word',
    key: 'word'
  }
]

const wordsPagination = ref({
  current: 1,
  pageSize: 20,
  total: 0,
  showSizeChanger: true,
  showTotal: (total: number) => `共 ${total} 条`
})

const wordsRowSelection = computed(() => ({
  selectedRowKeys: selectedWords.value,
  onChange: (keys: (string | number)[]) => {
    selectedWords.value = keys as string[]
  },
  onSelectAll: (selected: boolean, selectedRows: string[], changeRows: string[]) => {
    if (selected) {
      selectedWords.value = [...selectedWords.value, ...changeRows]
    } else {
      selectedWords.value = selectedWords.value.filter(word => !changeRows.includes(word))
    }
  }
}))

const handleLoadWords = async () => {
  try {
    wordsLoading.value = true
    const words = await getAllWords()
    wordsList.value = words
    wordsPagination.value.total = words.length
  } catch (error: any) {
    logger.error('[Word Filter] 加载敏感词列表失败:', error)
    message.error(error.message || '加载失败')
  } finally {
    wordsLoading.value = false
  }
}

const handleLoadStats = async () => {
  try {
    statsLoading.value = true
    stats.value = await getStats()
    message.success('统计信息已更新')
  } catch (error: any) {
    logger.error('[Word Filter] 加载统计信息失败:', error)
    message.error(error.message || '加载失败')
  } finally {
    statsLoading.value = false
  }
}

const handleAddWords = async () => {
  addWordsText.value = ''
  selectedGroup.value = undefined
  addWordsVisible.value = true
  
  // 加载词库组列表
  if (wordLibraryGroups.value.length === 0) {
    await loadWordLibraryGroups()
  }
}

const loadWordLibraryGroups = async () => {
  try {
    groupsLoading.value = true
    wordLibraryGroups.value = await getWordLibraryFiles()
  } catch (error: any) {
    logger.error('[Word Filter] 加载词库组列表失败:', error)
    message.error(error.message || '加载词库组列表失败')
  } finally {
    groupsLoading.value = false
  }
}

const filterGroupOption = (input: string, option: any) => {
  const displayName = option.children?.[0]?.children || option.label || ''
  return displayName.toLowerCase().includes(input.toLowerCase())
}

const handleAddWordsSubmit = async () => {
  if (!addWordsText.value.trim()) {
    message.warning('请输入敏感词')
    return
  }

  const words = addWordsText.value
    .split('\n')
    .map(w => w.trim())
    .filter(w => w.length > 0)

  if (words.length === 0) {
    message.warning('请输入至少一个敏感词')
    return
  }

  try {
    addLoading.value = true
    const requestData: AddWordsDto = { words }
    if (selectedGroup.value) {
      requestData.group = selectedGroup.value
    }
    
    const result = await addWords(requestData)
    const groupInfo = selectedGroup.value 
      ? `（已添加到词库组：${wordLibraryGroups.value.find(g => g.fileName === selectedGroup.value)?.displayName || selectedGroup.value}）`
      : '（仅添加到内存词库）'
    message.success(`成功添加 ${result.addedCount} 个敏感词，当前总数：${result.totalCount} ${groupInfo}`)
    addWordsVisible.value = false
    addWordsText.value = ''
    selectedGroup.value = undefined
    await handleLoadWords()
    await handleLoadStats()
    // 重新加载词库组列表以更新词数
    await loadWordLibraryGroups()
  } catch (error: any) {
    logger.error('[Word Filter] 添加敏感词失败:', error)
    message.error(error.message || '添加失败')
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
    message.warning('请选择要移除的敏感词')
    return
  }

  Modal.confirm({
    title: '确认移除',
    content: `确定要移除选中的 ${selectedWords.value.length} 个敏感词吗？`,
    onOk: async () => {
      try {
        removeLoading.value = true
        const result = await removeWords({ words: selectedWords.value })
        message.success(`成功移除 ${result.removedCount} 个敏感词，剩余：${result.remainingCount}`)
        selectedWords.value = []
        await handleLoadWords()
        await handleLoadStats()
      } catch (error: any) {
        logger.error('[Word Filter] 移除敏感词失败:', error)
        message.error(error.message || '移除失败')
      } finally {
        removeLoading.value = false
      }
    }
  })
}

const handleClearWords = () => {
  Modal.confirm({
    title: '确认清空',
    content: '确定要清空所有敏感词吗？此操作不可恢复！',
    okType: 'danger',
    onOk: async () => {
      try {
        clearLoading.value = true
        await clearWords()
        message.success('敏感词库已清空')
        wordsList.value = []
        wordsPagination.value.total = 0
        selectedWords.value = []
        await handleLoadStats()
      } catch (error: any) {
        logger.error('[Word Filter] 清空敏感词库失败:', error)
        message.error(error.message || '清空失败')
      } finally {
        clearLoading.value = false
      }
    }
  })
}

const handleWordsTableChange = (pagination: any) => {
  wordsPagination.value.current = pagination.current
  wordsPagination.value.pageSize = pagination.pageSize
}

// 初始化
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
