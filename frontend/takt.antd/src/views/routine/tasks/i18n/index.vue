<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/i18n -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：i18n 管理（主表翻译、子表语言）：翻译列表/转置、语言列表与配置 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-i18n">
    <a-tabs
      v-model:active-key="activeTab"
      type="card"
    >
      <!-- 主表：翻译 -->
      <a-tab-pane
        key="translation"
        tab="翻译（主）"
      >
        <TaktQueryBar
          v-model="translationQueryKeyword"
          placeholder="资源键、翻译值"
          :loading="translationLoading"
          @search="handleTranslationSearch"
          @reset="handleTranslationReset"
        />
        <TaktToolsBar
          create-permission="routine:tasks:i18n:create"
          update-permission="routine:tasks:i18n:update"
          delete-permission="routine:tasks:i18n:delete"
          export-permission="routine:tasks:i18n:export"
          :show-create="true"
          :show-update="true"
          :show-delete="true"
          :show-export="true"
          :show-advanced-query="true"
          :show-column-setting="true"
          :show-transpose="true"
          :show-refresh="true"
          :create-disabled="false"
          :update-disabled="!translationSelectedRow"
          :delete-disabled="translationSelectedRows.length === 0"
          :create-loading="translationLoading"
          :update-loading="translationLoading"
          :delete-loading="translationLoading"
          :export-loading="translationLoading"
          :refresh-loading="translationLoading"
          @create="handleTranslationCreate"
          @update="handleTranslationUpdate"
          @delete="handleTranslationDelete"
          @export="handleTranslationExport"
          @advanced-query="handleTranslationAdvancedQuery"
          @column-setting="handleTranslationColumnSetting"
          @transpose="handleTranslationTranspose"
          @refresh="handleTranslationRefresh"
        />
        <!-- 列表模式 -->
        <TaktSingleTable
          v-if="translationViewMode === 'list'"
          :columns="translationDisplayColumns"
          :data-source="translationDataSource"
          :loading="translationLoading"
          :stripe="true"
          :row-key="(r: any) => r.translationId || ''"
          :row-selection="translationRowSelection"
          :pagination="false"
          @change="() => {}"
        />
        <!-- 转置模式：表头=语言列，每行一资源键+操作列 -->
        <div
          v-else
          class="transposed-table-wrap"
        >
          <a-table
            :columns="transposedColumns"
            :data-source="transposedTableRows"
            :row-key="(r: any) => [r.resourceKey, r.resourceType, r.resourceGroup].filter(Boolean).join('|')"
            :loading="translationLoading"
            :pagination="false"
            size="small"
            bordered
            :scroll="{ x: 'max-content' }"
          />
        </div>
        <TaktPagination
          v-model:current="translationPage"
          v-model:page-size="translationPageSize"
          :total="translationTotal"
          @change="handleTranslationPaginationChange"
          @show-size-change="handleTranslationPageSizeChange"
        />
        <TaktModal
          v-model:open="translationFormVisible"
          :title="translationFormTitle"
          :width="640"
          :confirm-loading="translationFormLoading"
          @ok="handleTranslationFormSubmit"
          @cancel="handleTranslationFormCancel"
        >
          <TranslationForm
            ref="translationFormRef"
            :form-data="translationFormData"
            :loading="translationFormLoading"
          />
        </TaktModal>
        <TaktModal
          v-model:open="translationTransposedFormVisible"
          :title="translationTransposedFormTitle"
          :width="720"
          :confirm-loading="translationTransposedFormLoading"
          @ok="handleTranslationTransposedFormSubmit"
          @cancel="handleTranslationTransposedFormCancel"
        >
          <TranslationTransposedForm
            ref="translationTransposedFormRef"
            :form-data="translationTransposedFormData"
            :loading="translationTransposedFormLoading"
          />
        </TaktModal>
        <TaktQueryDrawer
          v-model:open="translationAdvancedVisible"
          :form-model="translationAdvancedForm"
          @submit="handleTranslationAdvancedSubmit"
          @reset="handleTranslationAdvancedReset"
        >
          <a-form-item label="资源键">
            <a-input
              v-model:value="translationAdvancedForm.resourceKey"
              placeholder="模糊"
            />
          </a-form-item>
          <a-form-item label="语言编码">
            <a-input
              v-model:value="translationAdvancedForm.cultureCode"
              placeholder="如 zh-CN"
            />
          </a-form-item>
          <a-form-item label="资源类型">
            <a-select
              v-model:value="translationAdvancedForm.resourceType"
              placeholder="Frontend/Backend"
              allow-clear
            >
              <a-select-option value="Frontend">
                Frontend
              </a-select-option>
              <a-select-option value="Backend">
                Backend
              </a-select-option>
            </a-select>
          </a-form-item>
          <a-form-item label="资源分组">
            <a-input
              v-model:value="translationAdvancedForm.resourceGroup"
              placeholder="如 Menu"
            />
          </a-form-item>
        </TaktQueryDrawer>
        <TaktColumnDrawer
          v-model:open="translationColumnDrawerVisible"
          :columns="translationListColumns"
          :checked-keys="translationVisibleColumnKeys"
          :id-column-key="'translationId'"
          :action-column-key="'action'"
          @update:checked-keys="handleTranslationColumnKeysChange"
          @reset="handleTranslationColumnSettingReset"
        />
      </a-tab-pane>

      <!-- 子表：语言 -->
      <a-tab-pane
        key="language"
        tab="语言（子）"
      >
        <!-- 查询栏 -->
        <TaktQueryBar
          v-model="queryKeyword"
          placeholder="请输入语言编码或名称"
          :loading="loading"
          @search="handleSearch"
          @reset="handleReset"
        />

        <!-- 工具栏 -->
        <TaktToolsBar
          create-permission="routine:tasks:i18n:create"
          update-permission="routine:tasks:i18n:update"
          delete-permission="routine:tasks:i18n:delete"
          export-permission="routine:tasks:i18n:export"
          :show-create="true"
          :show-update="true"
          :show-delete="true"
          :show-export="true"
          :show-advanced-query="true"
          :show-column-setting="true"
          :show-fullscreen="true"
          :show-refresh="true"
          :create-disabled="false"
          :update-disabled="!selectedRow"
          :delete-disabled="selectedRows.length === 0"
          :create-loading="loading"
          :update-loading="loading"
          :delete-loading="loading"
          :export-loading="loading"
          :refresh-loading="loading"
          @create="handleCreate"
          @update="handleUpdate"
          @delete="handleDelete"
          @export="handleExport"
          @advanced-query="handleAdvancedQuery"
          @column-setting="handleColumnSetting"
          @refresh="handleRefresh"
        />

        <!-- 表格 -->
        <TaktSingleTable
          :columns="displayColumns"
          :data-source="dataSource"
          :loading="loading"
          :stripe="true"
          :row-key="(record: any) => record.languageId || ''"
          :row-selection="rowSelection"
          :custom-row="onClickRow"
          :pagination="false"
          :expanded-row-keys="expandedRowKeys"
          @change="handleTableChange"
          @resize-column="handleResizeColumn"
          @expand="handleExpand"
        >
          <!-- 自定义列渲染 -->
          <template #bodyCell="{ column, record }">
            <template v-if="column.key === 'languageStatus'">
              <a-switch
                :checked="record.languageStatus === 0"
                checked-children="启用"
                un-checked-children="禁用"
                @change="(checked: any) => handleStatusChange(record, !!checked)"
              />
            </template>
            <template v-else-if="column.key === 'isDefault'">
              {{ record.isDefault === 0 ? '是' : '否' }}
            </template>
            <template v-else-if="column.key === 'isRtl'">
              {{ record.isRtl === 0 ? '是' : '否' }}
            </template>
          </template>
          <!-- 展开行渲染 -->
          <template #expandedRowRender="{ record }">
            <div style="padding: 16px">
              <TaktSingleTable
                v-if="(dataSource.find(item => item.languageId === record.languageId)?.translationList || []).length > 0"
                :columns="translationColumns"
                :data-source="dataSource.find(item => item.languageId === record.languageId)?.translationList || []"
                :row-key="(r: Translation) => r.translationId || ''"
                :pagination="false"
                :stripe="true"
                size="small"
              />
              <a-empty v-else />
            </div>
          </template>
        </TaktSingleTable>

        <!-- 分页组件 -->
        <TaktPagination
          v-model:current="currentPage"
          v-model:page-size="pageSize"
          :total="total"
          @change="handlePaginationChange"
          @show-size-change="handlePaginationSizeChange"
        />

        <!-- 新增/编辑对话框 -->
        <TaktModal
          v-model:open="formVisible"
          :title="formTitle"
          :width="1200"
          :confirm-loading="formLoading"
          @ok="handleFormSubmit"
          @cancel="handleFormCancel"
        >
          <LanguageForm
            ref="formRef"
            :form-data="formData"
            :loading="formLoading"
          />
        </TaktModal>

        <!-- 高级查询抽屉 -->
        <TaktQueryDrawer
          v-model:open="advancedQueryVisible"
          :form-model="advancedQueryForm"
          @submit="handleAdvancedQuerySubmit"
          @reset="handleAdvancedQueryReset"
        >
          <a-form-item label="语言编码">
            <a-input v-model:value="advancedQueryForm.cultureCode" />
          </a-form-item>
          <a-form-item label="语言状态">
            <TaktSelect
              v-model:value="advancedQueryForm.languageStatus"
              dict-type="sys_status"
              placeholder="请选择状态"
              allow-clear
            />
          </a-form-item>
        </TaktQueryDrawer>

        <!-- 列设置抽屉 -->
        <!-- 审计字段统一在 TaktColumnDrawer 中处理 -->
        <TaktColumnDrawer
          v-model:open="columnDrawerVisible"
          :columns="mergedColumns"
          :checked-keys="visibleColumnKeys"
          :id-column-key="'cultureCode'"
          :action-column-key="'action'"
          @update:checked-keys="handleColumnKeysChange"
          @reset="handleColumnSettingReset"
        />
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, watch, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { mergeDefaultColumns } from '@/utils/table-columns'
import LanguageForm from './components/language-form.vue'
import TranslationForm from './components/translation-form.vue'
import TranslationTransposedForm from './components/translation-transposed-form.vue'
import * as languageApi from '@/api/routine/tasks/i18n/language'
import {
  getTranslationList,
  getTranslationListTransposed,
  createTranslation,
  updateTranslation,
  deleteTranslationById,
  exportTranslationData
} from '@/api/routine/tasks/i18n/translation'
import type { Language, LanguageQuery, LanguageCreate, LanguageUpdate } from '@/types/routine/tasks/i18n/language'
import type { Translation, TranslationQuery, TranslationTransposed } from '@/types/routine/tasks/i18n/translation'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

// ========================================
// 数据定义
// ========================================

const loading = ref(false)
const queryKeyword = ref('')
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const dataSource = ref<Language[]>([])

// 行选择
const selectedRowKeys = ref<(string | number)[]>([])
const selectedRows = ref<Language[]>([])
const selectedRow = ref<Language | null>(null)

// 表单
const formVisible = ref(false)
const formTitle = ref('新增语言')
const formLoading = ref(false)
const formData = ref<Language | null>(null)
const formRef = ref<InstanceType<typeof LanguageForm> | null>(null)

// 高级查询
const advancedQueryVisible = ref(false)
const advancedQueryForm = reactive<LanguageQuery>({
  pageIndex: 1,
  pageSize: 20,
  keyWords: '',
  cultureCode: '',
  languageStatus: undefined,
  isDefault: undefined
})

// 列设置
const visibleColumnKeys = ref<string[]>([])
const columnDrawerVisible = ref(false)

// 展开行
const expandedRowKeys = ref<(string | number)[]>([])

// ----------------------------------------
// 翻译（主）Tab 状态与数据
// ----------------------------------------
const activeTab = ref<'translation' | 'language'>('translation')
const translationViewMode = ref<'list' | 'transposed'>('list') // 主视图默认列表，非转置
const translationLoading = ref(false)
const translationQueryKeyword = ref('')
const translationPage = ref(1)
const translationPageSize = ref(20)
const translationTotal = ref(0)
const translationDataSource = ref<Translation[]>([])
const translationSelectedRowKeys = ref<(string | number)[]>([])
const translationSelectedRows = ref<Translation[]>([])
const translationSelectedRow = ref<Translation | null>(null)
const translationFormVisible = ref(false)
const translationFormTitle = ref('新增翻译')
const translationFormLoading = ref(false)
const translationFormData = ref<Translation | null>(null)
const translationFormRef = ref<InstanceType<typeof TranslationForm> | null>(null)
// 转置表单
const translationTransposedFormVisible = ref(false)
const translationTransposedFormTitle = ref('新增翻译（转置）')
const translationTransposedFormLoading = ref(false)
const translationTransposedFormData = ref<Translation[] | null>(null)
const translationTransposedFormRef = ref<InstanceType<typeof TranslationTransposedForm> | null>(null)
const translationAdvancedVisible = ref(false)
const translationAdvancedForm = reactive<TranslationQuery & { keyWords?: string }>({
  pageIndex: 1,
  pageSize: 20,
  keyWords: '',
  resourceKey: '',
  cultureCode: '',
  resourceType: '',
  resourceGroup: ''
})
const translationColumnDrawerVisible = ref(false)
const translationVisibleColumnKeys = ref<string[]>([])
const transposedResult = ref<{ paged: { data: TranslationTransposed[]; total: number }; cultureCodeOrder: string[] } | null>(null)

// 翻译列表列（主视图 = 列表，列与 Translation 一致 + 操作列）
const translationListColumns: any[] = [
  { title: '翻译ID', dataIndex: 'translationId', key: 'translationId', width: 120 },
  { title: '资源键', dataIndex: 'resourceKey', key: 'resourceKey', width: 200 },
  { title: '语言ID', dataIndex: 'languageId', key: 'languageId', width: 120 },
  { title: '语言编码', dataIndex: 'cultureCode', key: 'cultureCode', width: 120 },
  { title: '翻译值', dataIndex: 'translationValue', key: 'translationValue', width: 240, ellipsis: true },
  { title: '资源类型', dataIndex: 'resourceType', key: 'resourceType', width: 100 },
  { title: '资源分组', dataIndex: 'resourceGroup', key: 'resourceGroup', width: 120, ellipsis: true },
  { title: '排序号', dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: '编辑',
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:i18n:update',
        onClick: (record: Translation) => handleTranslationEditOne(record)
      },
      {
        key: 'delete',
        label: '删除',
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:i18n:delete',
        onClick: (record: Translation) => handleTranslationDeleteOne(record)
      }
    ]
  })
]

// 转置表列：固定列 + 语言列 + 操作列
const transposedColumns = computed(() => {
  const order = transposedResult.value?.cultureCodeOrder ?? []
  const base: any[] = [
    { title: '资源键', dataIndex: 'resourceKey', key: 'resourceKey', width: 180, fixed: 'left' as const },
    { title: '资源类型', dataIndex: 'resourceType', key: 'resourceType', width: 90, fixed: 'left' as const },
    { title: '资源分组', dataIndex: 'resourceGroup', key: 'resourceGroup', width: 100, fixed: 'left' as const }
  ]
  order.forEach((c) => {
    base.push({ title: c, dataIndex: `translations.${c}`, key: `lang_${c}`, width: 120, ellipsis: true })
  })
  base.push(
    CreateActionColumn({
      actions: [
        {
          key: 'update',
          label: '编辑',
          shape: 'plain',
          icon: RiEditLine,
          permission: 'routine:tasks:i18n:update',
          onClick: (record: any) => handleTransposedEdit(record)
        },
        {
          key: 'delete',
          label: '删除',
          shape: 'plain',
          icon: RiDeleteBinLine,
          permission: 'routine:tasks:i18n:delete',
          onClick: (record: any) => handleTransposedDelete(record)
        }
      ]
    })
  )
  return base
})

// 转置表行：每个资源键一行，语言列为译文；行结构含 translations.xxx 以匹配列 dataIndex
const transposedTableRows = computed(() => {
  const list = transposedResult.value?.paged?.data ?? []
  const order = transposedResult.value?.cultureCodeOrder ?? []
  const rows: Record<string, unknown>[] = []
  list.forEach((item) => {
    const row: Record<string, unknown> = {
      resourceKey: item.resourceKey,
      resourceType: item.resourceType,
      resourceGroup: item.resourceGroup ?? ''
    }
    order.forEach((c) => {
      row[`translations.${c}`] = (item.translations ?? {})[c] ?? ''
    })
    rows.push(row)
  })
  return rows
})

const translationRowSelection = computed(() => ({
  selectedRowKeys: translationSelectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Translation[]) => {
    translationSelectedRowKeys.value = keys
    translationSelectedRows.value = rows
    translationSelectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

// 翻译列表可见列（列设置过滤）
const translationDisplayColumns = computed((): any[] => {
  const keys = translationVisibleColumnKeys.value || []
  const cols: any[] = translationListColumns
  if (keys.length === 0) return cols
  const getColumnKey = (col: any): string => {
    const k = col.key || col.dataIndex || col.title
    return k ? String(k) : ''
  }
  const keysSet = new Set(keys.map((k) => String(k)))
  return cols.filter((col: any) => {
    const ck = getColumnKey(col)
    return ck && keysSet.has(ck)
  })
})

// 翻译子表列定义（主表翻译，子表语言；与 Translation 接口一致）
const translationColumns = [
  { title: '翻译ID', dataIndex: 'translationId', key: 'translationId', width: 120 },
  { title: '资源键', dataIndex: 'resourceKey', key: 'resourceKey', width: 200 },
  { title: '语言ID', dataIndex: 'languageId', key: 'languageId', width: 120 },
  { title: '语言编码', dataIndex: 'cultureCode', key: 'cultureCode', width: 150 },
  { title: '翻译值', dataIndex: 'translationValue', key: 'translationValue', width: 300 },
  { title: '资源类型', dataIndex: 'resourceType', key: 'resourceType', width: 120 },
  { title: '资源分组', dataIndex: 'resourceGroup', key: 'resourceGroup', width: 150, ellipsis: true },
  { title: '排序号', dataIndex: 'orderNum', key: 'orderNum', width: 100 }
]

// ========================================
// 列定义
// ========================================

// 表格列定义（与 Language 接口字段顺序一致）
const columns = computed<TableColumnsType<Language>>(() => [
  {
    title: '语言ID',
    dataIndex: 'languageId',
    key: 'languageId',
    width: 120,
    fixed: 'left'
  },
  {
    title: '语言名称',
    dataIndex: 'languageName',
    key: 'languageName',
    width: 150,
    fixed: 'left'
  },
  {
    title: '语言编码',
    dataIndex: 'cultureCode',
    key: 'cultureCode',
    width: 150
  },
  {
    title: '本地化名称',
    dataIndex: 'nativeName',
    key: 'nativeName',
    width: 150
  },
  {
    title: '语言图标',
    dataIndex: 'languageIcon',
    key: 'languageIcon',
    width: 200,
    ellipsis: true
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: '语言状态',
    dataIndex: 'languageStatus',
    key: 'languageStatus',
    width: 100
  },
  {
    title: '是否默认',
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 100
  },
  {
    title: '是否RTL',
    dataIndex: 'isRtl',
    key: 'isRtl',
    width: 100
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: '编辑',
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:i18n:update',
        onClick: (record: Language) => handleEditOne(record)
      },
      {
        key: 'delete',
        label: '删除',
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:i18n:delete',
        onClick: (record: Language) => handleDeleteOne(record)
      }
    ]
  })
])

// 审计字段统一在 TaktColumnDrawer 中处理

// 合并列配置（包含审计字段）- 参照 file/index.vue 的实现
// 注意：CreateActionColumn 内部使用 h 函数返回 VNode，导致 TypeScript 类型推断过深
// 这是 TypeScript 在处理复杂递归类型时的已知限制，使用类型断言是合理的解决方案
const mergedColumns = computed((): any => {
  return mergeDefaultColumns(columns.value as any, t, true)
})

// 根据可见列过滤显示的列 - 保持原始列的顺序
// 注意：由于 mergedColumns 包含 VNode 返回类型，需要类型断言避免类型推断过深
// 使用 any 类型避免 TypeScript 类型推断过深的问题
const displayColumns = computed((): any => {
  const keys = visibleColumnKeys.value || []
  // 直接对 mergedColumns.value 进行类型断言，避免类型推断
  const merged: any = mergedColumns.value || []
  
  // 如果 keys 为空，返回原始列配置（等待 TaktColumnDrawer 初始化）
  if (keys.length === 0) {
    const cols: any = columns.value
    return cols
  }
  
  // 根据选中的 keys 过滤列，但保持原始列的顺序
  const getColumnKey = (col: any): string => {
    const key = col.key || col.dataIndex || col.title
    return key ? String(key) : ''
  }
  
  // 将 keys 转换为 Set 以便快速查找
  const keysSet = new Set(keys.map(k => String(k)))
  
  // 按照 merged 的原始顺序过滤，只保留选中的列
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

// ========================================
// 方法定义
// ========================================

// 加载数据（语言 tab）
const loadData = async () => {
  try {
    loading.value = true
    const query: LanguageQuery = {
      ...advancedQueryForm,
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      keyWords: queryKeyword.value || undefined
    }
    const result = await languageApi.getLanguageList(query)
    dataSource.value = result.data || []
    total.value = result.total || 0
  } catch (error) {
    logger.error('[Language] 加载数据失败', error)
    message.error('加载数据失败')
  } finally {
    loading.value = false
  }
}

// 翻译 tab：加载列表（主视图默认）
const loadTranslationList = async () => {
  try {
    translationLoading.value = true
    const { pageIndex: _pi, pageSize: _ps, ...rest } = translationAdvancedForm
    const query: TranslationQuery = {
      ...rest,
      pageIndex: translationPage.value,
      pageSize: translationPageSize.value,
      keyWords: translationQueryKeyword.value || undefined
    }
    const result = await getTranslationList(query)
    translationDataSource.value = result?.data ?? []
    translationTotal.value = result?.total ?? 0
  } catch (e) {
    logger.error('[Translation] 加载列表失败', e)
    message.error('加载翻译列表失败')
  } finally {
    translationLoading.value = false
  }
}

// 翻译 tab：加载转置（仅当切换为转置时）
const loadTranslationTransposed = async () => {
  try {
    translationLoading.value = true
    const { pageIndex: _pi, pageSize: _ps, ...rest } = translationAdvancedForm
    const query: TranslationQuery = {
      ...rest,
      pageIndex: translationPage.value,
      pageSize: translationPageSize.value,
      keyWords: translationQueryKeyword.value || undefined
    }
    const result = await getTranslationListTransposed(query)
    transposedResult.value = { paged: result.paged, cultureCodeOrder: result.cultureCodeOrder ?? [] }
    translationTotal.value = result.paged?.total ?? 0
  } catch (e) {
    logger.error('[Translation] 加载转置失败', e)
    message.error('加载翻译转置失败')
  } finally {
    translationLoading.value = false
  }
}

const handleTranslationViewModeChange = () => {
  translationPage.value = 1
  if (translationViewMode.value === 'list') loadTranslationList()
  else loadTranslationTransposed()
}

const handleTranslationTranspose = (isTransposed: boolean) => {
  translationViewMode.value = isTransposed ? 'transposed' : 'list'
  handleTranslationViewModeChange()
}

const handleTranslationSearch = () => {
  translationPage.value = 1
  if (translationViewMode.value === 'list') loadTranslationList()
  else loadTranslationTransposed()
}

const handleTranslationReset = () => {
  translationQueryKeyword.value = ''
  translationPage.value = 1
  Object.assign(translationAdvancedForm, {
    pageIndex: 1,
    pageSize: translationPageSize.value,
    keyWords: '',
    resourceKey: '',
    cultureCode: '',
    resourceType: '',
    resourceGroup: ''
  })
  if (translationViewMode.value === 'list') loadTranslationList()
  else loadTranslationTransposed()
}

const handleTranslationRefresh = () => {
  if (translationViewMode.value === 'list') loadTranslationList()
  else loadTranslationTransposed()
}

const handleTranslationAdvancedQuery = () => { translationAdvancedVisible.value = true }
const handleTranslationColumnSetting = () => { translationColumnDrawerVisible.value = true }
const handleTranslationColumnKeysChange = (keys: (string | number)[]) => {
  translationVisibleColumnKeys.value = keys.map((k) => String(k))
}
const handleTranslationColumnSettingReset = () => { translationVisibleColumnKeys.value = [] }
const handleTranslationAdvancedSubmit = () => {
  translationAdvancedVisible.value = false
  translationPage.value = 1
  if (translationViewMode.value === 'list') loadTranslationList()
  else loadTranslationTransposed()
}
const handleTranslationAdvancedReset = () => {
  Object.assign(translationAdvancedForm, {
    resourceKey: '',
    cultureCode: '',
    resourceType: '',
    resourceGroup: ''
  })
}

const handleTranslationPaginationChange = (page: number) => {
  translationPage.value = page
  if (translationViewMode.value === 'list') loadTranslationList()
  else loadTranslationTransposed()
}
const handleTranslationPageSizeChange = (_current: number, size: number) => {
  translationPage.value = 1
  translationPageSize.value = size
  if (translationViewMode.value === 'list') loadTranslationList()
  else loadTranslationTransposed()
}

const handleTranslationCreate = () => {
  if (translationViewMode.value === 'transposed') {
    // 转置模式：使用转置表单
    translationTransposedFormTitle.value = '新增翻译（转置）'
    translationTransposedFormData.value = null
    translationTransposedFormVisible.value = true
  } else {
    // 列表模式：使用单条表单
    translationFormTitle.value = '新增翻译'
    translationFormData.value = null
    translationFormVisible.value = true
  }
}

const handleTranslationUpdate = () => {
  if (!translationSelectedRow.value) {
    message.warning('请选择要编辑的翻译')
    return
  }
  translationFormTitle.value = '编辑翻译'
  translationFormData.value = { ...translationSelectedRow.value }
  translationFormVisible.value = true
}

const handleTranslationEditOne = (record: Translation) => {
  translationFormTitle.value = '编辑翻译'
  translationFormData.value = { ...record }
  translationFormVisible.value = true
}

const handleTranslationDeleteOne = async (record: Translation) => {
  if (!record?.translationId) {
    message.warning('没有有效的翻译ID')
    return
  }
  try {
    translationLoading.value = true
    await deleteTranslationById(record.translationId)
    message.success('删除成功')
    if (translationViewMode.value === 'list') loadTranslationList()
    else loadTranslationTransposed()
    const k = record.translationId
    translationSelectedRowKeys.value = translationSelectedRowKeys.value.filter((x) => x !== k)
    translationSelectedRows.value = translationSelectedRows.value.filter((r) => r.translationId !== k)
    if (translationSelectedRow.value?.translationId === k) translationSelectedRow.value = null
  } catch (e) {
    logger.error('[Translation] 删除失败', e)
    message.error('删除失败')
  } finally {
    translationLoading.value = false
  }
}

const handleTranslationDelete = async () => {
  if (translationSelectedRows.value.length === 0) {
    message.warning('请选择要删除的翻译')
    return
  }
  const ids = translationSelectedRows.value.map((r) => r.translationId).filter(Boolean) as string[]
  try {
    translationLoading.value = true
    for (const id of ids) await deleteTranslationById(id)
    message.success('删除成功')
    if (translationViewMode.value === 'list') loadTranslationList()
    else loadTranslationTransposed()
    translationSelectedRowKeys.value = []
    translationSelectedRows.value = []
    translationSelectedRow.value = null
  } catch (e) {
    logger.error('[Translation] 删除失败', e)
    message.error('删除失败')
  } finally {
    translationLoading.value = false
  }
}

const handleTranslationExport = async () => {
  try {
    translationLoading.value = true
    const query: TranslationQuery = {
      ...translationAdvancedForm,
      pageIndex: 1,
      pageSize: 100000,
      keyWords: translationQueryKeyword.value || undefined
    }
    const blob = await exportTranslationData(query, undefined, '翻译')
    const ts = new Date(); const t = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `翻译_${ts.getFullYear()}${t(ts.getMonth()+1)}${t(ts.getDate())}${t(ts.getHours())}${t(ts.getMinutes())}${t(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const a = document.createElement('a')
    a.href = url
    a.download = fileName
    document.body.appendChild(a)
    a.click()
    document.body.removeChild(a)
    window.URL.revokeObjectURL(url)
    message.success('导出成功')
  } catch (e) {
    logger.error('[Translation] 导出失败', e)
    message.error('导出失败')
  } finally {
    translationLoading.value = false
  }
}

const handleTranslationFormSubmit = async () => {
  if (!translationFormRef.value) return
  try {
    await translationFormRef.value.validate()
    translationFormLoading.value = true
    const d = translationFormRef.value.getFormData()
    if ('translationId' in d && d.translationId) {
      await updateTranslation(String(d.translationId), d)
      message.success('更新成功')
    } else {
      await createTranslation(d)
      message.success('新增成功')
    }
    translationFormVisible.value = false
    if (translationViewMode.value === 'list') loadTranslationList()
    else loadTranslationTransposed()
  } catch (err: any) {
    if (err?.errorFields) message.warning('请检查表单')
    else {
      logger.error('[Translation] 保存失败', err)
      message.error('保存失败')
    }
  } finally {
    translationFormLoading.value = false
  }
}

const handleTranslationFormCancel = () => {
  translationFormVisible.value = false
  translationFormData.value = null
}

const handleTranslationTransposedFormSubmit = async () => {
  if (!translationTransposedFormRef.value) return
  try {
    await translationTransposedFormRef.value.validate()
    translationTransposedFormLoading.value = true
    const formData = translationTransposedFormRef.value.getFormData()
    const { resourceKey, resourceType, resourceGroup, orderNum, remark, translations, translationIds, languageIds } = formData
    // 遍历所有语言，逐个创建/更新
    const results: { success: number; fail: number } = { success: 0, fail: 0 }
    for (const [cultureCode, translationValue] of Object.entries(translations)) {
      if (!translationValue) continue // 跳过空值
      const existingId = translationIds[cultureCode]
      const languageId = languageIds[cultureCode]
      const payload = {
        resourceKey,
        cultureCode,
        translationValue,
        resourceType,
        resourceGroup: resourceGroup || undefined,
        orderNum,
        remark: remark || undefined,
        languageId
      }
      try {
        if (existingId) {
          await updateTranslation(existingId, { ...payload, translationId: existingId })
        } else {
          await createTranslation(payload)
        }
        results.success++
      } catch {
        results.fail++
      }
    }
    if (results.fail > 0) {
      message.warning(`保存完成，成功 ${results.success} 条，失败 ${results.fail} 条`)
    } else {
      message.success(`保存成功，共 ${results.success} 条`)
    }
    translationTransposedFormVisible.value = false
    translationTransposedFormData.value = null
    loadTranslationTransposed()
  } catch (err: any) {
    if (err?.errorFields) message.warning('请检查表单')
    else {
      logger.error('[Translation] 保存失败', err)
      message.error('保存失败')
    }
  } finally {
    translationTransposedFormLoading.value = false
  }
}

const handleTranslationTransposedFormCancel = () => {
  translationTransposedFormVisible.value = false
  translationTransposedFormData.value = null
}

type TransposedRow = { resourceKey: string; resourceType: string; resourceGroup?: string }

const handleTransposedEdit = async (row: TransposedRow) => {
  if (!row?.resourceKey) return
  try {
    translationLoading.value = true
    // 获取该资源键下的所有翻译
    const { data } = await getTranslationList({
      pageIndex: 1,
      pageSize: 10000,
      resourceKey: row.resourceKey,
      resourceType: row.resourceType || undefined,
      resourceGroup: row.resourceGroup || undefined
    })
    if (!data || data.length === 0) {
      message.warning('未找到该资源键的翻译')
      return
    }
    // 使用转置表单
    translationTransposedFormTitle.value = '编辑翻译（转置）'
    translationTransposedFormData.value = data
    translationTransposedFormVisible.value = true
  } catch (e) {
    logger.error('[Translation] 获取翻译失败', e)
    message.error('获取翻译失败')
  } finally {
    translationLoading.value = false
  }
}

const handleTransposedDelete = async (row: TransposedRow) => {
  if (!row?.resourceKey) return
  try {
    translationLoading.value = true
    const { data } = await getTranslationList({
      pageIndex: 1,
      pageSize: 10000,
      resourceKey: row.resourceKey,
      resourceType: row.resourceType || undefined,
      resourceGroup: row.resourceGroup || undefined
    })
    const ids = (data ?? []).map((r: Translation) => r.translationId).filter(Boolean) as string[]
    if (ids.length === 0) {
      message.warning('未找到该资源键的翻译')
      return
    }
    for (const id of ids) await deleteTranslationById(id)
    message.success('删除成功')
    loadTranslationTransposed()
  } catch (e) {
    logger.error('[Translation] 删除失败', e)
    message.error('删除失败')
  } finally {
    translationLoading.value = false
  }
}

// 加载翻译数据 - 根据 languageId 动态获取
const loadTranslationData = async (record: Language) => {
  if (!record.languageId) return
  
  try {
    // 使用 getTranslationList 根据 languageId 查询翻译数据
    const result = await getTranslationList({
      pageIndex: 1,
      pageSize: 10000, // 获取所有数据
      languageId: record.languageId
    })
    
    if (result && result.data) {
      // 更新 dataSource 中对应的记录，确保响应式更新
      const index = dataSource.value.findIndex(item => item.languageId === record.languageId)
      if (index !== -1) {
        // 使用 Vue 的响应式更新方式
        dataSource.value[index] = {
          ...dataSource.value[index],
          translationList: result.data
        }
      }
      return result.data
    }
    return []
  } catch (error) {
    logger.error('[Language] 加载翻译数据失败', error)
    message.error('加载翻译数据失败')
    return []
  }
}

// 搜索
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

// 重置
const handleReset = () => {
  queryKeyword.value = ''
  currentPage.value = 1
  Object.assign(advancedQueryForm, {
    keyWords: '',
    cultureCode: '',
    languageStatus: undefined,
    isDefault: undefined
  })
  loadData()
}

// 新增
const handleCreate = () => {
  formTitle.value = '新增语言'
  formData.value = null
  formVisible.value = true
}

// 编辑
const handleUpdate = () => {
  if (!selectedRow.value) {
    message.warning('请选择要编辑的记录')
    return
  }
  
  formTitle.value = '编辑语言'
  formData.value = { ...selectedRow.value }
  formVisible.value = true
}

// 编辑单条记录（操作列使用）
const handleEditOne = (record: Language) => {
  selectedRow.value = record
  formTitle.value = '编辑语言'
  formData.value = { ...record }
  formVisible.value = true
}

// 删除
const handleDelete = async () => {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的记录')
    return
  }
  
  const ids = selectedRows.value.map(row => row.languageId).filter(Boolean)
  if (ids.length === 0) {
    message.warning('没有有效的记录ID')
    return
  }
  
  try {
    loading.value = true
    if (ids.length === 1) {
      await languageApi.deleteLanguageById(ids[0])
    } else {
      await languageApi.deleteLanguageBatch(ids)
    }
    message.success('删除成功')
    await loadData()
    selectedRowKeys.value = []
    selectedRows.value = []
    selectedRow.value = null
  } catch (error) {
    logger.error('[Language] 删除失败', error)
    message.error('删除失败')
  } finally {
    loading.value = false
  }
}

// 删除单条记录（操作列使用）
const handleDeleteOne = async (record: Language) => {
  if (!record.languageId) {
    message.warning('没有有效的记录ID')
    return
  }
  
  try {
    loading.value = true
    await languageApi.deleteLanguageById(record.languageId)
    message.success('删除成功')
    await loadData()
    // 如果删除的是当前选中的行，清除选中状态
    if (selectedRow.value?.languageId === record.languageId) {
      selectedRow.value = null
    }
    selectedRowKeys.value = selectedRowKeys.value.filter(k => k !== record.languageId)
    selectedRows.value = selectedRows.value.filter(r => r.languageId !== record.languageId)
  } catch (error) {
    logger.error('[Language] 删除失败', error)
    message.error('删除失败')
  } finally {
    loading.value = false
  }
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    const query: LanguageQuery = {
      ...advancedQueryForm,
      pageIndex: 1,
      pageSize: 10000,
      keyWords: queryKeyword.value || undefined
    }
    
    const blob = await languageApi.exportLanguageData(query, undefined, '语言')
    const ts = new Date(); const t = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `语言_${ts.getFullYear()}${t(ts.getMonth()+1)}${t(ts.getDate())}${t(ts.getHours())}${t(ts.getMinutes())}${t(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success('导出成功')
  } catch (error) {
    logger.error('[Language] 导出失败', error)
    message.error('导出失败')
  } finally {
    loading.value = false
  }
}

// 状态切换
const handleStatusChange = async (record: Language, checked: boolean) => {
  try {
    await languageApi.updateLanguageStatus({
      languageId: record.languageId,
      languageStatus: checked ? 0 : 1
    })
    message.success('状态更新成功')
    await loadData()
  } catch (error) {
    logger.error('[Language] 状态更新失败', error)
    message.error('状态更新失败')
  }
}

// 高级查询
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

const handleAdvancedQuerySubmit = () => {
  advancedQueryVisible.value = false
  currentPage.value = 1
  loadData()
}

const handleAdvancedQueryReset = () => {
  Object.assign(advancedQueryForm, {
    keyWords: '',
    cultureCode: '',
    languageStatus: undefined,
    isDefault: undefined
  })
}

// 列设置
const handleColumnSetting = () => {
  columnDrawerVisible.value = true
}

// 列设置变化 - TaktColumnDrawer 传递选中的 keys，更新 visibleColumnKeys
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}

// 列设置重置：TaktColumnDrawer 会自动重置为默认值
const handleColumnSettingReset = () => {
  // TaktColumnDrawer 组件内部会自动处理重置逻辑
  // 这里只需要清空，让组件使用默认值
  visibleColumnKeys.value = []
}

// 刷新
const handleRefresh = () => {
  loadData()
}

// 表单提交
const handleFormSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
    formLoading.value = true
    
    const formData = formRef.value.getFormData()
    if ('languageId' in formData && formData.languageId) {
      // 更新
      await languageApi.updateLanguage(formData.languageId, formData as LanguageUpdate)
      message.success('更新成功')
    } else {
      // 新增
      await languageApi.createLanguage(formData as LanguageCreate)
      message.success('新增成功')
    }
    
    formVisible.value = false
    await loadData()
  } catch (error: any) {
    if (error?.errorFields) {
      message.warning('请检查表单输入')
    } else {
      logger.error('[Language] 保存失败', error)
      message.error('保存失败')
    }
  } finally {
    formLoading.value = false
  }
}

// 表单取消
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = null
}

// 表格变化
const handleTableChange = () => {
  // 处理表格变化
}

// 列调整
const handleResizeColumn = () => {
  // 处理列调整
}

// 行选择配置
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Language[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Language, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (selectedRow.value?.languageId === record?.languageId) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: Language[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? selectedRowsData[0] : null
    } else {
      selectedRow.value = null
    }
  }
}))

// 行点击处理 - 切换展开状态（手风琴模式：只允许一个展开，保留行选择功能）
const onClickRow = (record: Language) => {
  return {
    onClick: (event: MouseEvent) => {
      // 如果点击的是复选框或操作列，不处理展开
      const target = event.target as HTMLElement
      if (target.closest('.ant-checkbox-wrapper') || target.closest('.takt-action-column')) {
        // 处理行选择
        const key = record.languageId || ''
        if (selectedRowKeys.value.includes(key)) {
          selectedRowKeys.value = selectedRowKeys.value.filter(k => k !== key)
          selectedRows.value = selectedRows.value.filter(r => r.languageId !== key)
          if (selectedRow.value?.languageId === key) {
            selectedRow.value = null
          }
        } else {
          selectedRowKeys.value = [key]
          selectedRows.value = [record]
          selectedRow.value = record
        }
        return
      }
      
      const key = record.languageId || ''
      // 手风琴模式：切换展开状态
      if (expandedRowKeys.value.includes(key)) {
        // 如果当前行已展开，则收起
        expandedRowKeys.value = []
      } else {
        // 如果当前行未展开，先关闭其他已展开的行，再展开当前行
        expandedRowKeys.value = []
        
        // 确保翻译数据已加载，等待加载完成后再展开
        const item = dataSource.value.find(item => item.languageId === record.languageId)
        if (item && (!item.translationList || item.translationList.length === 0)) {
          // 先加载数据，等待完成后再展开
          loadTranslationData(record).then(() => {
            expandedRowKeys.value = [key]
          })
        } else {
          expandedRowKeys.value = [key]
        }
      }
    }
  }
}

// 展开/收起处理（手风琴模式：只允许一个展开）
const handleExpand = async (expanded: boolean, record: Language) => {
  if (expanded && record.languageId) {
    // 手风琴模式：先关闭其他已展开的行
    const currentKey = record.languageId || ''
    if (expandedRowKeys.value.length > 0 && expandedRowKeys.value[0] !== currentKey) {
      expandedRowKeys.value = []
    }
    
    // 检查 dataSource 中是否有数据，如果没有则加载
    const item = dataSource.value.find(item => item.languageId === record.languageId)
    if (item && (!item.translationList || item.translationList.length === 0)) {
      await loadTranslationData(record)
    }
    
    // 设置当前行为唯一展开的行
    expandedRowKeys.value = [currentKey]
  } else {
    // 收起时清空
    expandedRowKeys.value = []
  }
}

// 分页变化
const handlePaginationChange = (page: number) => {
  currentPage.value = page
  loadData()
}

const handlePaginationSizeChange = (current: number, size: number) => {
  currentPage.value = current
  pageSize.value = size
  loadData()
}

// ========================================
// 生命周期
// ========================================

watch(activeTab, (tab) => {
  if (tab === 'translation') {
    if (translationViewMode.value === 'list') loadTranslationList()
    else loadTranslationTransposed()
  }
})

onMounted(() => {
  loadData()
  // 主视图默认列表（非转置）
  if (activeTab.value === 'translation' && translationViewMode.value === 'list') {
    loadTranslationList()
  }
})
</script>

<style scoped lang="less">
.routine-i18n {
  padding: 16px;
}
.transposed-table-wrap {
  margin-bottom: 16px;
}
</style>
