<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/routine/numbering-rule -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：单据编码规则管理页面，列表、查询、新增、编辑、删除、状态、批量删除 -->
<!-- ======================================== -->

<template>
  <div class="routine-numbering-rule">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.numberingrule.rulecode') + t('common.action.or') + t('entity.numberingrule.rulename') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="routine:numbering:rule:create"
      update-permission="routine:numbering:rule:update"
      delete-permission="routine:numbering:rule:delete"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :update-disabled="!selectedRow"
      :delete-disabled="selectedRows.length === 0"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />

    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getRuleId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'ruleStatus'">
          <a-switch
            :checked="record.ruleStatus === 0"
            :checked-children="t('common.button.enable')"
            :un-checked-children="t('common.button.disable')"
            @change="(checked: any) => handleStatusChange(record, !!checked)"
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
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
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
      <a-form-item :label="t('entity.numberingrule.rulecode')">
        <a-input
          v-model:value="advancedQueryForm.ruleCode"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.numberingrule.rulename')">
        <a-input
          v-model:value="advancedQueryForm.ruleName"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.numberingrule.documenttype')">
        <a-input
          v-model:value="advancedQueryForm.documentType"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.numberingrule.rulestatus')">
        <TaktSelect
          v-model:value="advancedQueryForm.ruleStatus"
          dict-type="sys_status"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'ruleId'"
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
  updateNumberingRuleStatus
} from '@/api/routine/numberingRule'
import type { NumberingRule, NumberingRuleQuery } from '@/types/routine/numberingRule'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'

const { t } = useI18n()
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
const advancedQueryForm = ref<{
  ruleCode: string
  ruleName: string
  documentType: string
  ruleStatus?: number
}>({
  ruleCode: '',
  ruleName: '',
  documentType: '',
  ruleStatus: undefined
})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const columns = computed<TableColumnsType>(() => [
  { title: 'ID', dataIndex: 'ruleId', key: 'id', width: 120, fixed: 'left', resizable: true, ellipsis: true },
  { title: t('entity.numberingrule.rulecode'), dataIndex: 'ruleCode', key: 'ruleCode', width: 140, ellipsis: true, resizable: true },
  { title: t('entity.numberingrule.rulename'), dataIndex: 'ruleName', key: 'ruleName', width: 140, ellipsis: true, resizable: true },
  { title: t('entity.numberingrule.documenttype'), dataIndex: 'documentType', key: 'documentType', width: 120, ellipsis: true },
  { title: t('entity.numberingrule.prefix'), dataIndex: 'prefix', key: 'prefix', width: 80 },
  { title: t('entity.numberingrule.dateformat'), dataIndex: 'dateFormat', key: 'dateFormat', width: 100 },
  { title: t('entity.numberingrule.seriallength'), dataIndex: 'serialLength', key: 'serialLength', width: 100 },
  { title: t('entity.numberingrule.currentvalue'), dataIndex: 'currentValue', key: 'currentValue', width: 110 },
  { title: t('entity.numberingrule.resetcycle'), dataIndex: 'resetCycle', key: 'resetCycle', width: 90 },
  { title: t('entity.numberingrule.ordernum'), dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  { title: t('entity.numberingrule.rulestatus'), dataIndex: 'ruleStatus', key: 'ruleStatus', width: 100 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.edit'), shape: 'plain', icon: RiEditLine, permission: 'routine:numbering:rule:update', onClick: (r: NumberingRule) => handleEdit(r) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:numbering:rule:delete', onClick: (r: NumberingRule) => handleDeleteOne(r) }
    ]
  })
])

const getRuleId = (record: any): string => String(record?.ruleId ?? '')

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getColumnKey = (col: any): string => (col.key || col.dataIndex || col.title) ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: NumberingRule[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: NumberingRule, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.ruleId === record?.ruleId) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, rows: NumberingRule[]) => {
    selectedRow.value = selected && rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: NumberingRule) => ({
  onClick: () => {
    const key = record.ruleId || ''
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.ruleId || ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
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
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.ruleCode) params.ruleCode = advancedQueryForm.value.ruleCode
    if (advancedQueryForm.value.ruleName) params.ruleName = advancedQueryForm.value.ruleName
    if (advancedQueryForm.value.documentType) params.documentType = advancedQueryForm.value.documentType
    if (advancedQueryForm.value.ruleStatus !== undefined) params.ruleStatus = advancedQueryForm.value.ruleStatus

    const response = await getNumberingRuleList(params) as any
    const items = response?.data ?? []
    const totalCount = response?.total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[NumberingRule] 加载失败:', error)
    message.error(error?.message || t('common.msg.loadFail'))
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
  advancedQueryForm.value = { ruleCode: '', ruleName: '', documentType: '', ruleStatus: undefined }
  currentPage.value = 1
  loadData()
}

function handleTableChange(_pagination: any, _filters: any, _sorter: any) {}
function handleResizeColumn(_w: number, _col: any) {}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(current: number, size: number) {
  currentPage.value = current
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = t('common.button.create') + t('entity.numberingrule._self')
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: NumberingRule) {
  formTitle.value = t('common.button.edit') + t('entity.numberingrule._self')
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.update'), entity: t('entity.numberingrule._self') }))
}

async function handleStatusChange(record: NumberingRule, checked: boolean) {
  const newStatus = checked ? 0 : 1
  const oldStatus = record.ruleStatus
  const idx = dataSource.value.findIndex(r => r.ruleId === record.ruleId)
  if (idx !== -1) dataSource.value[idx].ruleStatus = newStatus
  try {
    await updateNumberingRuleStatus({ ruleId: record.ruleId, ruleStatus: newStatus })
    message.success(checked ? t('common.button.enable') : t('common.button.disable'))
  } catch (e: any) {
    if (idx !== -1) dataSource.value[idx].ruleStatus = oldStatus
    message.error(e?.message || t('common.msg.operateFail', { action: t('common.action.operation') }))
  }
}

function handleDeleteOne(record: NumberingRule) {
  Modal.confirm({
    title: t('common.confirm.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.numberingrule._self'), name: record.ruleName }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteNumberingRule(record.ruleId)
        message.success(t('common.msg.deleteSuccess', { target: t('entity.numberingrule._self') }))
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail', { target: t('entity.numberingrule._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.numberingrule._self') }))
    return
  }
  Modal.confirm({
    title: t('common.confirm.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('entity.numberingrule._self') }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteNumberingRuleBatch(selectedRows.value.map(r => r.ruleId))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.numberingrule._self') }))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail', { target: t('entity.numberingrule._self') }))
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
  advancedQueryForm.value = { ruleCode: '', ruleName: '', documentType: '', ruleStatus: undefined }
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

async function handleFormSubmit() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if ('ruleId' in values && values.ruleId) {
      await updateNumberingRule(values.ruleId, values)
      message.success(t('common.msg.updateSuccess', { target: t('entity.numberingrule._self') }))
    } else {
      await createNumberingRule(values)
      message.success(t('common.msg.createSuccess', { target: t('entity.numberingrule._self') }))
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: any) {
    if (e?.errorFields) return
    message.error(e?.message || t('common.msg.operateFail', { action: t('common.action.operation') }))
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
