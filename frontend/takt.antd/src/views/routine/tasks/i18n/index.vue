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
        :tab="t('routine.localization.translation.page.tabMain')"
      >
        <TaktQueryBar
          v-model="translationQueryKeyword"
          :placeholder="t('routine.localization.translation.placeholders.listSearch')"
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
          :row-key="getTranslationListRowKey"
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
            :row-key="getTransposedTableRowKey"
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
          <a-form-item :label="t('entity.translation.resourcekey')">
            <a-input
              v-model:value="translationAdvancedForm.resourceKey"
              :placeholder="t('routine.localization.translation.placeholders.resourceKeyFuzzy')"
            />
          </a-form-item>
          <a-form-item :label="t('entity.translation.culturecode')">
            <a-input
              v-model:value="translationAdvancedForm.cultureCode"
              :placeholder="t('routine.localization.translation.placeholders.cultureCodeExample')"
            />
          </a-form-item>
          <a-form-item :label="t('entity.translation.resourcetype')">
            <a-select
              v-model:value="translationAdvancedForm.resourceType"
              :placeholder="t('routine.localization.translation.placeholders.resourceTypeSelect')"
              allow-clear
            >
              <a-select-option value="Frontend">
                {{ t('routine.localization.translation.options.frontend') }}
              </a-select-option>
              <a-select-option value="Backend">
                {{ t('routine.localization.translation.options.backend') }}
              </a-select-option>
            </a-select>
          </a-form-item>
          <a-form-item :label="t('entity.translation.resourcegroup')">
            <a-input
              v-model:value="translationAdvancedForm.resourceGroup"
              :placeholder="t('routine.localization.translation.placeholders.resourceGroupExample')"
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
        :tab="t('routine.localization.language.page.tabInI18n')"
      >
        <!-- 查询栏 -->
        <TaktQueryBar
          v-model="queryKeyword"
          :placeholder="t('routine.localization.language.placeholders.listSearch')"
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
          :row-key="getLanguageListRowKey"
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
                :checked-children="t('common.button.enable')"
                :un-checked-children="t('common.button.disable')"
                @change="(checked) => handleStatusChange(record, checked)"
              />
            </template>
            <template v-else-if="column.key === 'isDefault'">
              {{ record.isDefault === 0 ? t('common.button.yes') : t('common.button.no') }}
            </template>
            <template v-else-if="column.key === 'isRtl'">
              {{ record.isRtl === 0 ? t('common.button.yes') : t('common.button.no') }}
            </template>
          </template>
          <!-- 展开行渲染 -->
          <template #expandedRowRender="{ record }">
            <div style="padding: 16px">
              <TaktSingleTable
                v-if="((dataSource.find((item) => item.languageId === record.languageId)?.translationList ?? []) as Translation[]).length > 0"
                :columns="translationColumnsNested"
                :data-source="(dataSource.find((item) => item.languageId === record.languageId)?.translationList ?? []) as Record<string, unknown>[]"
                :row-key="getTranslationListRowKey"
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
          <a-form-item :label="t('entity.language.culturecode')">
            <a-input v-model:value="advancedQueryForm.cultureCode" />
          </a-form-item>
          <a-form-item :label="t('entity.language.status')">
            <TaktSelect
              v-model:value="advancedQueryForm.languageStatus"
              api-url="/api/TaktDictDatas/options?dictTypeCode=sys_status"
              :field-names="{ label: 'dictLabel', value: 'extLabel' }"
              allow-clear
              :placeholder="t('common.form.placeholder.select', { field: t('entity.language.status') })"
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
import type {
  Translation,
  TranslationQuery,
  TranslationTransposed,
  TranslationCreate,
  TranslationUpdate
} from '@/types/routine/tasks/i18n/translation'
import { logger } from '@/utils/logger'
import { CreateActionColumn, type ActionRecord } from '@/components/business/takt-action-column/index'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const getTranslationListRowKey = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const id = (record as Record<string, unknown>)['translationId']
  return id != null && String(id) !== '' ? String(id) : ''
}

const getTransposedTableRowKey = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const r = record as Record<string, unknown>
  return [r['resourceKey'], r['resourceType'], r['resourceGroup']].filter((x) => x != null && String(x) !== '').join('|')
}

const getLanguageListRowKey = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const id = (record as Record<string, unknown>)['languageId']
  return id != null && String(id) !== '' ? String(id) : ''
}

/** 翻译高级查询抽屉：绑定控件须为必填 string，避免 TranslationQuery 可选字段在 EOPT 下与 a-input value 不兼容 */
type TranslationAdvancedQueryFormState = Pick<TranslationQuery, 'pageIndex' | 'pageSize'> & {
  KeyWords: string
  resourceKey: string
  cultureCode: string
  resourceType: string
  resourceGroup: string
}

/** 语言高级查询：KeyWords / cultureCode 与 a-input 绑定为必填 string（TaktPagedQuery 上为可选） */
type LanguageAdvancedQueryFormState = Omit<LanguageQuery, 'KeyWords' | 'cultureCode' | 'languageStatus' | 'isDefault'> & {
  KeyWords: string
  cultureCode: string
  languageStatus?: number
  isDefault?: number
}

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
const formTitle = ref(t('routine.localization.language.messages.formCreate'))
const formLoading = ref(false)
const formData = ref<Language | null>(null)
const formRef = ref<InstanceType<typeof LanguageForm> | null>(null)

// 高级查询
const advancedQueryVisible = ref(false)
const advancedQueryForm = reactive<LanguageAdvancedQueryFormState>({
  pageIndex: 1,
  pageSize: 20,
  KeyWords: '',
  cultureCode: ''
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
const translationFormTitle = ref(t('routine.localization.translation.form.formCreate'))
const translationFormLoading = ref(false)
const translationFormData = ref<Translation | null>(null)
const translationFormRef = ref<InstanceType<typeof TranslationForm> | null>(null)
// 转置表单
const translationTransposedFormVisible = ref(false)
const translationTransposedFormTitle = ref(t('routine.localization.translation.form.formCreateTransposed'))
const translationTransposedFormLoading = ref(false)
const translationTransposedFormData = ref<Translation[] | null>(null)
const translationTransposedFormRef = ref<InstanceType<typeof TranslationTransposedForm> | null>(null)
const translationAdvancedVisible = ref(false)
const translationAdvancedForm = reactive<TranslationAdvancedQueryFormState>({
  pageIndex: 1,
  pageSize: 20,
  KeyWords: '',
  resourceKey: '',
  cultureCode: '',
  resourceType: '',
  resourceGroup: ''
})
const translationColumnDrawerVisible = ref(false)
const translationVisibleColumnKeys = ref<string[]>([])
const transposedResult = ref<{ paged: { data: TranslationTransposed[]; total: number }; cultureCodeOrder: string[] } | null>(null)

type TransposedRow = { resourceKey: string; resourceType: string; resourceGroup?: string }

// 翻译列表列（主视图 = 列表，列与 Translation 一致 + 操作列）
const translationListColumns = computed(() => [
  { title: t('routine.localization.language.columns.translationId'), dataIndex: 'translationId', key: 'translationId', width: 120 },
  { title: t('entity.translation.resourcekey'), dataIndex: 'resourceKey', key: 'resourceKey', width: 200 },
  { title: t('entity.translation.languageid'), dataIndex: 'languageId', key: 'languageId', width: 120 },
  { title: t('entity.translation.culturecode'), dataIndex: 'cultureCode', key: 'cultureCode', width: 120 },
  { title: t('entity.translation.translationvalue'), dataIndex: 'translationValue', key: 'translationValue', width: 240, ellipsis: true },
  { title: t('entity.translation.resourcetype'), dataIndex: 'resourceType', key: 'resourceType', width: 100 },
  { title: t('entity.translation.resourcegroup'), dataIndex: 'resourceGroup', key: 'resourceGroup', width: 120, ellipsis: true },
  { title: t('entity.translation.ordernum'), dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  CreateActionColumn<Translation>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:i18n:update',
        onClick: (record: Translation) => handleTranslationEditOne(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:i18n:delete',
        onClick: (record: Translation) => handleTranslationDeleteOne(record)
      }
    ]
  })
])

// 转置表列：固定列 + 语言列 + 操作列
const transposedColumns = computed(() => {
  const order = transposedResult.value?.cultureCodeOrder ?? []
  const base: any[] = [
    { title: t('entity.translation.resourcekey'), dataIndex: 'resourceKey', key: 'resourceKey', width: 180, fixed: 'left' as const },
    { title: t('entity.translation.resourcetype'), dataIndex: 'resourceType', key: 'resourceType', width: 90, fixed: 'left' as const },
    { title: t('entity.translation.resourcegroup'), dataIndex: 'resourceGroup', key: 'resourceGroup', width: 100, fixed: 'left' as const }
  ]
  order.forEach((c) => {
    base.push({ title: c, dataIndex: `translations.${c}`, key: `lang_${c}`, width: 120, ellipsis: true })
  })
  base.push(
    CreateActionColumn<ActionRecord>({
      actions: [
        {
          key: 'update',
          label: t('common.button.edit'),
          shape: 'plain',
          icon: RiEditLine,
          permission: 'routine:tasks:i18n:update',
          onClick: (record: ActionRecord) => handleTransposedEdit(record as TransposedRow)
        },
        {
          key: 'delete',
          label: t('common.button.delete'),
          shape: 'plain',
          icon: RiDeleteBinLine,
          permission: 'routine:tasks:i18n:delete',
          onClick: (record: ActionRecord) => handleTransposedDelete(record as TransposedRow)
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
    const translations = (item as TranslationTransposed & { translations?: Record<string, string> }).translations
    const row: Record<string, unknown> = {
      resourceKey: item.resourceKey,
      resourceType: item.resourceType,
      resourceGroup: item.resourceGroup ?? ''
    }
    order.forEach((c) => {
      row[`translations.${c}`] = (translations ?? {})[c] ?? ''
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
    translationSelectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  }
}))

// 翻译列表可见列（列设置过滤）
const translationDisplayColumns = computed((): any[] => {
  const keys = translationVisibleColumnKeys.value || []
  const cols: any[] = translationListColumns.value
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

// 语言行展开：子表翻译列（与 Translation 接口一致；列标题优先 entity.translation.*）
const translationColumnsNested = computed<TableColumnsType>(() => [
  { title: t('routine.localization.language.columns.translationId'), dataIndex: 'translationId', key: 'translationId', width: 120 },
  { title: t('entity.translation.resourcekey'), dataIndex: 'resourceKey', key: 'resourceKey', width: 200 },
  { title: t('entity.translation.languageid'), dataIndex: 'languageId', key: 'languageId', width: 120 },
  { title: t('entity.translation.culturecode'), dataIndex: 'cultureCode', key: 'cultureCode', width: 150 },
  { title: t('entity.translation.translationvalue'), dataIndex: 'translationValue', key: 'translationValue', width: 300 },
  { title: t('entity.translation.resourcetype'), dataIndex: 'resourceType', key: 'resourceType', width: 120 },
  { title: t('entity.translation.resourcegroup'), dataIndex: 'resourceGroup', key: 'resourceGroup', width: 150, ellipsis: true },
  { title: t('entity.translation.ordernum'), dataIndex: 'orderNum', key: 'orderNum', width: 100 }
])

// ========================================
// 列定义
// ========================================

// 表格列定义（与 Language 接口字段顺序一致）
const columns = computed<TableColumnsType<Language>>(() => [
  {
    title: t('routine.localization.language.columns.languageId'),
    dataIndex: 'languageId',
    key: 'languageId',
    width: 120,
    fixed: 'left'
  },
  {
    title: t('entity.language.name'),
    dataIndex: 'languageName',
    key: 'languageName',
    width: 150,
    fixed: 'left'
  },
  {
    title: t('entity.language.culturecode'),
    dataIndex: 'cultureCode',
    key: 'cultureCode',
    width: 150
  },
  {
    title: t('entity.language.nativename'),
    dataIndex: 'nativeName',
    key: 'nativeName',
    width: 150
  },
  {
    title: t('entity.language.icon'),
    dataIndex: 'languageIcon',
    key: 'languageIcon',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.language.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('entity.language.status'),
    dataIndex: 'languageStatus',
    key: 'languageStatus',
    width: 100
  },
  {
    title: t('entity.language.isdefault'),
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 100
  },
  {
    title: t('entity.language.isrtl'),
    dataIndex: 'isRtl',
    key: 'isRtl',
    width: 100
  },
  CreateActionColumn<Language>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:i18n:update',
        onClick: (record: Language) => handleEditOne(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
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
    const query = {
      ...(advancedQueryForm as LanguageQuery),
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      KeyWords: queryKeyword.value || advancedQueryForm.KeyWords || undefined
    } as LanguageQuery
    const result = await languageApi.getLanguageList(query)
    dataSource.value = result.data || []
    total.value = result.total || 0
  } catch (error) {
    logger.error('[Language] 加载数据失败', error)
    message.error(t('common.msg.loadfail'))
  } finally {
    loading.value = false
  }
}

// 翻译 tab：加载列表（主视图默认）
const loadTranslationList = async () => {
  try {
    translationLoading.value = true
    const { pageIndex: _pi, pageSize: _ps, ...rest } = translationAdvancedForm
    const query = {
      ...(rest as TranslationQuery),
      pageIndex: translationPage.value,
      pageSize: translationPageSize.value,
      KeyWords: translationQueryKeyword.value || undefined
    } as TranslationQuery
    const result = await getTranslationList(query)
    translationDataSource.value = result?.data ?? []
    translationTotal.value = result?.total ?? 0
  } catch (e) {
    logger.error('[Translation] 加载列表失败', e)
    message.error(t('routine.localization.translation.messages.loadListFail'))
  } finally {
    translationLoading.value = false
  }
}

// 翻译 tab：加载转置（仅当切换为转置时）
const loadTranslationTransposed = async () => {
  try {
    translationLoading.value = true
    const { pageIndex: _pi, pageSize: _ps, ...rest } = translationAdvancedForm
    const query = {
      ...(rest as TranslationQuery),
      pageIndex: translationPage.value,
      pageSize: translationPageSize.value,
      KeyWords: translationQueryKeyword.value || undefined
    } as TranslationQuery
    const result = await getTranslationListTransposed(query)
    transposedResult.value = { paged: result.paged, cultureCodeOrder: result.cultureCodeOrder ?? [] }
    translationTotal.value = result.paged?.total ?? 0
  } catch (e) {
    logger.error('[Translation] 加载转置失败', e)
    message.error(t('routine.localization.translation.messages.loadTransposedFail'))
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
    KeyWords: '',
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
    translationTransposedFormTitle.value = t('routine.localization.translation.form.formCreateTransposed')
    translationTransposedFormData.value = null
    translationTransposedFormVisible.value = true
  } else {
    // 列表模式：使用单条表单
    translationFormTitle.value = t('routine.localization.translation.form.formCreate')
    translationFormData.value = null
    translationFormVisible.value = true
  }
}

const handleTranslationUpdate = () => {
  if (!translationSelectedRow.value) {
    message.warning(t('routine.localization.translation.messages.selectTranslationEdit'))
    return
  }
  translationFormTitle.value = t('routine.localization.translation.form.formEdit')
  translationFormData.value = { ...translationSelectedRow.value }
  translationFormVisible.value = true
}

const handleTranslationEditOne = (record: Translation) => {
  translationFormTitle.value = t('routine.localization.translation.form.formEdit')
  translationFormData.value = { ...record }
  translationFormVisible.value = true
}

const handleTranslationDeleteOne = async (record: Translation) => {
  if (!record?.translationId) {
    message.warning(t('routine.localization.translation.messages.invalidTranslationId'))
    return
  }
  try {
    translationLoading.value = true
    await deleteTranslationById(record.translationId)
    message.success(t('common.msg.deletesuccess'))
    if (translationViewMode.value === 'list') loadTranslationList()
    else loadTranslationTransposed()
    const k = record.translationId
    translationSelectedRowKeys.value = translationSelectedRowKeys.value.filter((x) => x !== k)
    translationSelectedRows.value = translationSelectedRows.value.filter((r) => r.translationId !== k)
    if (translationSelectedRow.value?.translationId === k) translationSelectedRow.value = null
  } catch (e) {
    logger.error('[Translation] 删除失败', e)
    message.error(t('common.msg.deletefail'))
  } finally {
    translationLoading.value = false
  }
}

const handleTranslationDelete = async () => {
  if (translationSelectedRows.value.length === 0) {
    message.warning(t('routine.localization.translation.messages.selectTranslationDelete'))
    return
  }
  const ids = translationSelectedRows.value.map((r) => r.translationId).filter(Boolean) as string[]
  try {
    translationLoading.value = true
    for (const id of ids) await deleteTranslationById(id)
    message.success(t('common.msg.deletesuccess'))
    if (translationViewMode.value === 'list') loadTranslationList()
    else loadTranslationTransposed()
    translationSelectedRowKeys.value = []
    translationSelectedRows.value = []
    translationSelectedRow.value = null
  } catch (e) {
    logger.error('[Translation] 删除失败', e)
    message.error(t('common.msg.deletefail'))
  } finally {
    translationLoading.value = false
  }
}

const handleTranslationExport = async () => {
  try {
    translationLoading.value = true
    const query = {
      ...(translationAdvancedForm as TranslationQuery),
      pageIndex: 1,
      pageSize: 100000,
      KeyWords: translationQueryKeyword.value || undefined
    } as TranslationQuery
    const exportLabel = t('routine.localization.translation.messages.exportDataLabel')
    const blob = await exportTranslationData(query, undefined, exportLabel)
    const ts = new Date()
    const padNum = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${exportLabel}_${ts.getFullYear()}${padNum(ts.getMonth() + 1)}${padNum(ts.getDate())}${padNum(ts.getHours())}${padNum(ts.getMinutes())}${padNum(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const a = document.createElement('a')
    a.href = url
    a.download = fileName
    document.body.appendChild(a)
    a.click()
    document.body.removeChild(a)
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.exportsuccess'))
  } catch (e) {
    logger.error('[Translation] 导出失败', e)
    message.error(t('common.msg.exportfail'))
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
      await updateTranslation(String(d.translationId), d as TranslationUpdate)
      message.success(t('common.msg.updatesuccess'))
    } else {
      await createTranslation(d as TranslationCreate)
      message.success(t('common.msg.createsuccess'))
    }
    translationFormVisible.value = false
    if (translationViewMode.value === 'list') loadTranslationList()
    else loadTranslationTransposed()
  } catch (err: any) {
    if (err?.errorFields) message.warning(t('routine.localization.translation.messages.checkForm'))
    else {
      logger.error('[Translation] 保存失败', err)
      message.error(t('routine.localization.translation.messages.saveFail'))
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
      const languageIdStr = String(languageIds[cultureCode] ?? '')
      const payload: TranslationCreate = {
        resourceKey,
        cultureCode,
        translationValue,
        resourceType,
        languageId: languageIdStr,
        orderNum
      }
      if (resourceGroup && resourceGroup.trim() !== '') {
        payload.resourceGroup = resourceGroup
      }
      if (remark && remark.trim() !== '') {
        payload.remark = remark
      }
      try {
        if (existingId) {
          await updateTranslation(existingId, { ...payload, translationId: existingId } as TranslationUpdate)
        } else {
          await createTranslation(payload)
        }
        results.success++
      } catch {
        results.fail++
      }
    }
    if (results.fail > 0) {
      message.warning(
        t('routine.localization.translation.messages.savePartial', {
          success: results.success,
          fail: results.fail
        })
      )
    } else {
      message.success(t('routine.localization.translation.messages.saveAllSuccess', { count: results.success }))
    }
    translationTransposedFormVisible.value = false
    translationTransposedFormData.value = null
    loadTranslationTransposed()
  } catch (err: any) {
    if (err?.errorFields) message.warning(t('routine.localization.translation.messages.checkForm'))
    else {
      logger.error('[Translation] 保存失败', err)
      message.error(t('routine.localization.translation.messages.saveFail'))
    }
  } finally {
    translationTransposedFormLoading.value = false
  }
}

const handleTranslationTransposedFormCancel = () => {
  translationTransposedFormVisible.value = false
  translationTransposedFormData.value = null
}

const handleTransposedEdit = async (row: TransposedRow) => {
  if (!row?.resourceKey) return
  try {
    translationLoading.value = true
    // 获取该资源键下的所有翻译
    const q: TranslationQuery = {
      pageIndex: 1,
      pageSize: 10000,
      resourceKey: row.resourceKey
    }
    if (row.resourceType && row.resourceType.trim() !== '') {
      q.resourceType = row.resourceType
    }
    if (row.resourceGroup && row.resourceGroup.trim() !== '') {
      q.resourceGroup = row.resourceGroup
    }
    const { data } = await getTranslationList(q)
    if (!data || data.length === 0) {
      message.warning(t('routine.localization.translation.messages.notFoundByResourceKey'))
      return
    }
    // 使用转置表单
    translationTransposedFormTitle.value = t('routine.localization.translation.form.formEditTransposed')
    translationTransposedFormData.value = data
    translationTransposedFormVisible.value = true
  } catch (e) {
    logger.error('[Translation] 获取翻译失败', e)
    message.error(t('routine.localization.translation.messages.loadTranslationFail'))
  } finally {
    translationLoading.value = false
  }
}

const handleTransposedDelete = async (row: TransposedRow) => {
  if (!row?.resourceKey) return
  try {
    translationLoading.value = true
    const q: TranslationQuery = {
      pageIndex: 1,
      pageSize: 10000,
      resourceKey: row.resourceKey
    }
    if (row.resourceType && row.resourceType.trim() !== '') {
      q.resourceType = row.resourceType
    }
    if (row.resourceGroup && row.resourceGroup.trim() !== '') {
      q.resourceGroup = row.resourceGroup
    }
    const { data } = await getTranslationList(q)
    const ids = (data ?? []).map((r: Translation) => r.translationId).filter(Boolean) as string[]
    if (ids.length === 0) {
      message.warning(t('routine.localization.translation.messages.notFoundByResourceKey'))
      return
    }
    for (const id of ids) await deleteTranslationById(id)
    message.success(t('common.msg.deletesuccess'))
    loadTranslationTransposed()
  } catch (e) {
    logger.error('[Translation] 删除失败', e)
    message.error(t('common.msg.deletefail'))
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
        const prev = dataSource.value[index]
        if (prev) {
          dataSource.value[index] = {
            ...prev,
            translationList: result.data
          }
        }
      }
      return result.data
    }
    return []
  } catch (error) {
    logger.error('[Language] 加载翻译数据失败', error)
    message.error(t('routine.localization.translation.messages.loadTranslationDataFail'))
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
    KeyWords: '',
    cultureCode: ''
  })
  delete advancedQueryForm.languageStatus
  delete advancedQueryForm.isDefault
  loadData()
}

// 新增
const handleCreate = () => {
  formTitle.value = t('routine.localization.language.messages.formCreate')
  formData.value = null
  formVisible.value = true
}

// 编辑
const handleUpdate = () => {
  if (!selectedRow.value) {
    message.warning(t('routine.localization.language.messages.selectEdit'))
    return
  }
  
  formTitle.value = t('routine.localization.language.messages.formEdit')
  formData.value = { ...selectedRow.value }
  formVisible.value = true
}

// 编辑单条记录（操作列使用）
const handleEditOne = (record: Language) => {
  selectedRow.value = record
  formTitle.value = t('routine.localization.language.messages.formEdit')
  formData.value = { ...record }
  formVisible.value = true
}

// 删除
const handleDelete = async () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('routine.localization.language.messages.selectDelete'))
    return
  }
  
  const ids = selectedRows.value.map((row) => row.languageId).filter((id): id is string => Boolean(id))
  if (ids.length === 0) {
    message.warning(t('routine.localization.language.messages.invalidRecordId'))
    return
  }
  
  try {
    loading.value = true
    if (ids.length === 1) {
      await languageApi.deleteLanguageById(ids[0]!)
    } else {
      await languageApi.deleteLanguageBatch(ids)
    }
    message.success(t('common.msg.deletesuccess'))
    await loadData()
    selectedRowKeys.value = []
    selectedRows.value = []
    selectedRow.value = null
  } catch (error) {
    logger.error('[Language] 删除失败', error)
    message.error(t('common.msg.deletefail'))
  } finally {
    loading.value = false
  }
}

// 删除单条记录（操作列使用）
const handleDeleteOne = async (record: Language) => {
  if (!record.languageId) {
    message.warning(t('routine.localization.language.messages.invalidRecordId'))
    return
  }
  
  try {
    loading.value = true
    await languageApi.deleteLanguageById(record.languageId)
    message.success(t('common.msg.deletesuccess'))
    await loadData()
    // 如果删除的是当前选中的行，清除选中状态
    if (selectedRow.value?.languageId === record.languageId) {
      selectedRow.value = null
    }
    selectedRowKeys.value = selectedRowKeys.value.filter(k => k !== record.languageId)
    selectedRows.value = selectedRows.value.filter(r => r.languageId !== record.languageId)
  } catch (error) {
    logger.error('[Language] 删除失败', error)
    message.error(t('common.msg.deletefail'))
  } finally {
    loading.value = false
  }
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    const query = {
      ...(advancedQueryForm as LanguageQuery),
      pageIndex: 1,
      pageSize: 10000,
      KeyWords: queryKeyword.value || undefined
    } as LanguageQuery
    
    const blob = await languageApi.exportLanguageData(
      query,
      undefined,
      t('routine.localization.language.messages.exportFilePrefix')
    )
    const ts = new Date()
    const padNum = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('routine.localization.language.messages.exportFilePrefix')}_${ts.getFullYear()}${padNum(ts.getMonth() + 1)}${padNum(ts.getDate())}${padNum(ts.getHours())}${padNum(ts.getMinutes())}${padNum(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.exportsuccess'))
  } catch (error) {
    logger.error('[Language] 导出失败', error)
    message.error(t('common.msg.exportfail'))
  } finally {
    loading.value = false
  }
}

// 状态切换
const handleStatusChange = async (record: Language, checked: unknown) => {
  const on = Boolean(checked)
  try {
    await languageApi.updateLanguageStatus({
      languageId: record.languageId,
      languageStatus: on ? 0 : 1
    })
    message.success(t('routine.localization.language.messages.statusUpdateSuccess'))
    await loadData()
  } catch (error) {
    logger.error('[Language] 状态更新失败', error)
    message.error(t('routine.localization.language.messages.statusUpdateFail'))
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
    KeyWords: '',
    cultureCode: ''
  })
  delete advancedQueryForm.languageStatus
  delete advancedQueryForm.isDefault
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
      message.success(t('common.msg.updatesuccess'))
    } else {
      // 新增
      await languageApi.createLanguage(formData as LanguageCreate)
      message.success(t('common.msg.createsuccess'))
    }
    
    formVisible.value = false
    await loadData()
  } catch (error: any) {
    if (error?.errorFields) {
      message.warning(t('routine.localization.language.messages.checkFormInput'))
    } else {
      logger.error('[Language] 保存失败', error)
      message.error(t('routine.localization.translation.messages.saveFail'))
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
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
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
      selectedRow.value = selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
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
