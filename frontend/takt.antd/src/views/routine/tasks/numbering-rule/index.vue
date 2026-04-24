<template>
  <div class="routine-numbering-rule">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('routine.tasks.numbering-rule.page.listSearchPlaceholder')"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="routine:tasks:numberingrule:create"
      update-permission="routine:tasks:numberingrule:update"
      delete-permission="routine:tasks:numberingrule:delete"
      export-permission="routine:tasks:numberingrule:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
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
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getNumberingRuleRowKey"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="() => {}"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'ruleStatus'">
          <a-switch
            :checked="record.ruleStatus === 0"
            :checked-children="t('common.button.enable')"
            :un-checked-children="t('common.button.disable')"
            @change="(checked: unknown) => handleStatusChange(record, Boolean(checked))"
          />
        </template>
      </template>
    </TaktSingleTable>
    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      :width="560"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <NumberingRuleForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('routine.tasks.numbering-rule.advanced.ruleCode')">
        <a-input
          v-model:value="advancedQueryForm.ruleCode"
          :placeholder="t('routine.tasks.numbering-rule.advanced.placeholderRuleCode')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('routine.tasks.numbering-rule.advanced.ruleName')">
        <a-input
          v-model:value="advancedQueryForm.ruleName"
          :placeholder="t('routine.tasks.numbering-rule.advanced.placeholderRuleName')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('routine.tasks.numbering-rule.advanced.companyCode')">
        <a-input
          v-model:value="advancedQueryForm.companyCode"
          :placeholder="t('routine.tasks.numbering-rule.advanced.placeholderCompanyCode')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('routine.tasks.numbering-rule.advanced.deptCode')">
        <a-input
          v-model:value="advancedQueryForm.deptCode"
          :placeholder="t('routine.tasks.numbering-rule.advanced.placeholderDeptCode')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('routine.tasks.numbering-rule.advanced.ruleStatus')">
        <a-select
          v-model:value="advancedQueryForm.ruleStatus"
          :placeholder="t('common.form.placeholder.selectonly')"
          allow-clear
          :options="ruleStatusSelectOptions"
        />
      </a-form-item>
    </TaktQueryDrawer>
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'numberingRuleId'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import NumberingRuleForm from './components/numbering-rule-form.vue'
import {
  getNumberingRuleList,
  createNumberingRule,
  updateNumberingRule,
  deleteNumberingRule,
  deleteNumberingRuleBatch,
  updateNumberingRuleStatus,
  exportNumberingRules
} from '@/api/routine/tasks/numbering-rule'
import type { NumberingRule, NumberingRuleQuery, NumberingRuleCreate, NumberingRuleUpdate } from '@/types/routine/tasks/numbering-rule/numbering-rule'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'

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

/** TaktSingleTable 的 rowKey 入参为 TableRecord，按 unknown 解析 numberingRuleId。 */
const getNumberingRuleRowKey = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const r = record as Record<string, unknown>
  const id = r['numberingRuleId']
  return id != null && String(id) !== '' ? String(id) : ''
}

/** Ant Design Vue `TableColumnsType` 的元素类型，用于列显隐过滤与列键解析。 */
type NumberingRuleTableColumn = TableColumnsType[number]

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<NumberingRule[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<NumberingRule | null>(null)
const selectedRows = ref<NumberingRule[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<NumberingRule>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref({
  ruleCode: '',
  ruleName: '',
  companyCode: '',
  deptCode: '',
  ruleStatus: undefined as number | undefined
})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const ruleStatusSelectOptions = computed(() => [
  { label: t('common.button.enable'), value: 0 },
  { label: t('common.button.disable'), value: 1 }
])

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'numberingRuleId',
    key: 'numberingRuleId',
    width: 120,
    fixed: 'left'
  },
  {
    title: t('routine.tasks.numbering-rule.columns.ruleCode'),
    dataIndex: 'ruleCode',
    key: 'ruleCode',
    width: 140,
    ellipsis: true
  },
  {
    title: t('routine.tasks.numbering-rule.columns.ruleName'),
    dataIndex: 'ruleName',
    key: 'ruleName',
    width: 160,
    ellipsis: true
  },
  {
    title: t('routine.tasks.numbering-rule.columns.companyCode'),
    dataIndex: 'companyCode',
    key: 'companyCode',
    width: 100
  },
  {
    title: t('routine.tasks.numbering-rule.columns.deptCode'),
    dataIndex: 'deptCode',
    key: 'deptCode',
    width: 100
  },
  {
    title: t('routine.tasks.numbering-rule.columns.prefix'),
    dataIndex: 'prefix',
    key: 'prefix',
    width: 80
  },
  {
    title: t('routine.tasks.numbering-rule.columns.dateFormat'),
    dataIndex: 'dateFormat',
    key: 'dateFormat',
    width: 100
  },
  {
    title: t('routine.tasks.numbering-rule.columns.numberLength'),
    dataIndex: 'numberLength',
    key: 'numberLength',
    width: 90
  },
  {
    title: t('routine.tasks.numbering-rule.columns.suffix'),
    dataIndex: 'suffix',
    key: 'suffix',
    width: 80
  },
  {
    title: t('routine.tasks.numbering-rule.columns.currentNumber'),
    dataIndex: 'currentNumber',
    key: 'currentNumber',
    width: 100
  },
  {
    title: t('routine.tasks.numbering-rule.columns.step'),
    dataIndex: 'step',
    key: 'step',
    width: 70
  },
  {
    title: t('routine.tasks.numbering-rule.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('routine.tasks.numbering-rule.columns.ruleStatus'),
    dataIndex: 'ruleStatus',
    key: 'ruleStatus',
    width: 100
  },
  {
    title: t('common.entity.createtime'),
    dataIndex: 'createdAt',
    key: 'createdAt',
    width: 160
  },
  CreateActionColumn<NumberingRule>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:numberingrule:update',
        onClick: (r: NumberingRule) => handleEdit(r)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:numberingrule:delete',
        onClick: (r: NumberingRule) => handleDeleteOne(r)
      }
    ]
  })
])

const mergedColumns = computed((): TableColumnsType => {
  return mergeDefaultColumns(columns.value as TableColumnsType, t, true) as TableColumnsType
})

const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) {
    return columns.value
  }
  const keysSet = new Set(keys.map(k => String(k)))
  const getColumnKey = (col: NumberingRuleTableColumn): string => {
    const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
    const resolved = c.key ?? c.dataIndex ?? c.title
    return resolved != null && String(resolved) !== '' ? String(resolved) : ''
  }
  return merged.filter((col: NumberingRuleTableColumn) => {
    const colKey = getColumnKey(col)
    return colKey.length > 0 && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: NumberingRule[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: NumberingRule, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (selectedRow.value?.numberingRuleId === record.numberingRuleId) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: NumberingRule[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
    } else {
      selectedRow.value = null
    }
  }
}))

const onClickRow = (record: NumberingRule) => ({
  onClick: () => {
    const key = record.numberingRuleId || ''
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) {
      selectedRowKeys.value.splice(idx, 1)
    } else {
      selectedRowKeys.value.push(key)
    }
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.numberingRuleId || ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: NumberingRuleQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) {
      params.KeyWords = queryKeyword.value
    }
    const adv = advancedQueryForm.value
    if (adv.ruleCode) params.ruleCode = adv.ruleCode
    if (adv.ruleName) params.ruleName = adv.ruleName
    if (adv.companyCode) params.companyCode = adv.companyCode
    if (adv.deptCode) params.deptCode = adv.deptCode
    if (adv.ruleStatus !== undefined) {
      params.ruleStatus = adv.ruleStatus
    }
    const res = await getNumberingRuleList(params)
    dataSource.value = res?.data ?? []
    total.value = res?.total ?? 0
  } catch (e: unknown) {
    logger.error('[NumberingRule] loadData error', e)
    message.error(pickErrorMessage(e, t('routine.tasks.numbering-rule.messages.loadFail')))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

function handleSearch() {
  currentPage.value = 1
  loadData()
}

function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = {
    ruleCode: '',
    ruleName: '',
    companyCode: '',
    deptCode: '',
    ruleStatus: undefined
  }
  currentPage.value = 1
  loadData()
}

function handleResizeColumn(w: number, col: NumberingRuleTableColumn) {
  const resolveColPart = (x: NumberingRuleTableColumn) => {
    const c = x as { key?: unknown; dataIndex?: unknown; title?: unknown }
    return c.key ?? c.dataIndex ?? c.title
  }
  const colKey = resolveColPart(col)
  const column = columns.value.find((c: NumberingRuleTableColumn) => {
    const cKey = resolveColPart(c)
    return colKey != null && cKey != null && String(colKey) === String(cKey)
  }) as { width?: number } | undefined
  if (column) {
    column.width = w
  }
}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(_current: number, size: number) {
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = t('routine.tasks.numbering-rule.page.formCreate')
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: NumberingRule) {
  formTitle.value = t('routine.tasks.numbering-rule.page.formEdit')
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
  } else {
    message.warning(t('routine.tasks.numbering-rule.messages.selectOne'))
  }
}

async function handleStatusChange(record: NumberingRule, checked: boolean) {
  const newStatus = checked ? 0 : 1
  const oldStatus = record.ruleStatus
  const idx = dataSource.value.findIndex(r => r.numberingRuleId === record.numberingRuleId)
  const row = idx !== -1 ? dataSource.value[idx] : undefined
  if (row) {
    row.ruleStatus = newStatus
  }
  try {
    await updateNumberingRuleStatus({ numberingRuleId: record.numberingRuleId, ruleStatus: newStatus })
    message.success(checked ? t('routine.tasks.numbering-rule.messages.statusEnabled') : t('routine.tasks.numbering-rule.messages.statusDisabled'))
  } catch (e: unknown) {
    if (row) {
      row.ruleStatus = oldStatus
    }
    message.error(pickErrorMessage(e, t('common.msg.operatefail')))
  }
}

function handleDeleteOne(record: NumberingRule) {
  const name = record.ruleName || record.ruleCode || ''
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', {
      entity: t('routine.tasks.numbering-rule.page.entityName'),
      name
    }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteNumberingRule(String(record.numberingRuleId))
        message.success(t('common.msg.deletesuccess'))
        loadData()
      } catch (e: unknown) {
        message.error(pickErrorMessage(e, t('common.msg.deletefail')))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('routine.tasks.numbering-rule.messages.selectDelete'))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', {
      count: selectedRows.value.length,
      entity: t('routine.tasks.numbering-rule.page.entityName')
    }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteNumberingRuleBatch(selectedRows.value.map(r => String(r.numberingRuleId)))
        message.success(t('common.msg.deletesuccess'))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: unknown) {
        message.error(pickErrorMessage(e, t('common.msg.deletefail')))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

function handleAdvancedQueryReset() {
  advancedQueryForm.value = {
    ruleCode: '',
    ruleName: '',
    companyCode: '',
    deptCode: '',
    ruleStatus: undefined
  }
}

function handleColumnSetting() {
  columnSettingVisible.value = true
}

function handleColumnKeysChange(keys: (string | number)[]) {
  visibleColumnKeys.value = keys.map(k => String(k))
}

function handleColumnSettingReset() {
  visibleColumnKeys.value = []
}

function handleRefresh() {
  loadData()
}

function padNum(n: number): string {
  return n < 10 ? `0${n}` : String(n)
}

async function handleExport() {
  try {
    loading.value = true
    const query: NumberingRuleQuery = {
      pageIndex: 1,
      pageSize: 99999
    }
    if (queryKeyword.value) {
      query.KeyWords = queryKeyword.value
    }
    const adv = advancedQueryForm.value
    if (adv.ruleCode) query.ruleCode = adv.ruleCode
    if (adv.ruleName) query.ruleName = adv.ruleName
    if (adv.companyCode) query.companyCode = adv.companyCode
    if (adv.deptCode) query.deptCode = adv.deptCode
    if (adv.ruleStatus !== undefined) {
      query.ruleStatus = adv.ruleStatus
    }
    const exportLabel = t('routine.tasks.numbering-rule.page.exportDataLabel')
    const blob = await exportNumberingRules(query, undefined, exportLabel)
    const ts = new Date()
    const fileName = `${exportLabel}_${ts.getFullYear()}${padNum(ts.getMonth() + 1)}${padNum(ts.getDate())}${padNum(ts.getHours())}${padNum(ts.getMinutes())}${padNum(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.exportsuccess'))
  } catch (e: unknown) {
    message.error(pickErrorMessage(e, t('common.msg.exportfail')))
  } finally {
    loading.value = false
  }
}

async function handleFormSubmit() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if (values.numberingRuleId) {
      const payload: NumberingRuleUpdate = {
        numberingRuleId: values.numberingRuleId,
        ruleCode: values.ruleCode,
        ruleName: values.ruleName,
        numberLength: values.numberLength,
        step: values.step,
        orderNum: values.orderNum,
        companyCode: values.companyCode,
        deptCode: values.deptCode,
        prefix: values.prefix,
        dateFormat: values.dateFormat,
        suffix: values.suffix,
        remark: values.remark
      }
      await updateNumberingRule(values.numberingRuleId, payload)
      message.success(t('common.msg.updatesuccess'))
    } else {
      const createPayload: NumberingRuleCreate = {
        ruleCode: values.ruleCode,
        ruleName: values.ruleName,
        numberLength: values.numberLength,
        step: values.step,
        orderNum: values.orderNum,
        companyCode: values.companyCode,
        deptCode: values.deptCode,
        prefix: values.prefix,
        dateFormat: values.dateFormat,
        suffix: values.suffix,
        remark: values.remark
      }
      await createNumberingRule(createPayload)
      message.success(t('common.msg.createsuccess'))
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: unknown) {
    if (e !== null && typeof e === 'object' && 'errorFields' in e) {
      return
    }
    message.error(pickErrorMessage(e, t('common.msg.operatefail')))
  } finally {
    formLoading.value = false
  }
}

function handleFormCancel() {
  formVisible.value = false
  formData.value = {}
}

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-numbering-rule {
  padding: 16px;
}
</style>
