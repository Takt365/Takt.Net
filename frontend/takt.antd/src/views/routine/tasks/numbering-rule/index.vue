<template>
  <div class="routine-numbering-rule">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入规则编码或名称"
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
      :row-key="(r: any) => r.numberingRuleId || ''"
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
            checked-children="启用"
            un-checked-children="禁用"
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
      <a-form-item label="规则编码">
        <a-input
          v-model:value="advancedQueryForm.ruleCode"
          placeholder="规则编码"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="规则名称">
        <a-input
          v-model:value="advancedQueryForm.ruleName"
          placeholder="规则名称"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="公司编码">
        <a-input
          v-model:value="advancedQueryForm.companyCode"
          placeholder="公司编码"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="部门编码">
        <a-input
          v-model:value="advancedQueryForm.deptCode"
          placeholder="部门编码"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="规则状态">
        <a-select
          v-model:value="advancedQueryForm.ruleStatus"
          placeholder="请选择"
          allow-clear
          :options="[
            { label: '启用', value: 0 },
            { label: '禁用', value: 1 }
          ]"
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
import type { NumberingRule, NumberingRuleQuery } from '@/types/routine/tasks/numbering-rule'
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
const formTitle = ref('新增编码规则')
const formData = ref<Partial<NumberingRule>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  ruleCode?: string
  ruleName?: string
  companyCode?: string
  deptCode?: string
  ruleStatus?: number
}>({})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const columns = ref<TableColumnsType>([
  { title: '规则ID', dataIndex: 'numberingRuleId', key: 'numberingRuleId', width: 120, fixed: 'left' },
  { title: '规则编码', dataIndex: 'ruleCode', key: 'ruleCode', width: 140, ellipsis: true },
  { title: '规则名称', dataIndex: 'ruleName', key: 'ruleName', width: 160, ellipsis: true },
  { title: '公司编码', dataIndex: 'companyCode', key: 'companyCode', width: 100 },
  { title: '部门编码', dataIndex: 'deptCode', key: 'deptCode', width: 100 },
  { title: '前缀', dataIndex: 'prefix', key: 'prefix', width: 80 },
  { title: '日期格式', dataIndex: 'dateFormat', key: 'dateFormat', width: 100 },
  { title: '序号长度', dataIndex: 'numberLength', key: 'numberLength', width: 90 },
  { title: '后缀', dataIndex: 'suffix', key: 'suffix', width: 80 },
  { title: '当前序号', dataIndex: 'currentNumber', key: 'currentNumber', width: 100 },
  { title: '步长', dataIndex: 'step', key: 'step', width: 70 },
  { title: '排序号', dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  { title: '规则状态', dataIndex: 'ruleStatus', key: 'ruleStatus', width: 100 },
  { title: '创建时间', dataIndex: 'createdAt', key: 'createdAt', width: 160 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'routine:tasks:numberingrule:update', onClick: (r: NumberingRule) => handleEdit(r) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:tasks:numberingrule:delete', onClick: (r: NumberingRule) => handleDeleteOne(r) }
    ]
  })
])

const mergedColumns = computed(() => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = col.key || col.dataIndex || col.title
    return colKey && keysSet.has(String(colKey))
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: NumberingRule[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: NumberingRule) => ({
  onClick: () => {
    const key = record.numberingRuleId || ''
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.numberingRuleId || ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: NumberingRuleQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      keyWords: queryKeyword.value || undefined,
      ruleCode: advancedQueryForm.value.ruleCode,
      ruleName: advancedQueryForm.value.ruleName,
      companyCode: advancedQueryForm.value.companyCode,
      deptCode: advancedQueryForm.value.deptCode,
      ruleStatus: advancedQueryForm.value.ruleStatus
    }
    const res = await getNumberingRuleList(params) as any
    dataSource.value = res?.data ?? []
    total.value = res?.total ?? 0
  } catch (e: any) {
    logger.error('[NumberingRule] loadData error', e)
    message.error(e?.message || '加载失败')
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
  advancedQueryForm.value = {}
  currentPage.value = 1
  loadData()
}

function handleResizeColumn(_w: number, _col: any) {}

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
  formTitle.value = '新增编码规则'
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: NumberingRule) {
  formTitle.value = '编辑编码规则'
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning('请选择一条记录')
}

async function handleStatusChange(record: NumberingRule, checked: boolean) {
  const newStatus = checked ? 0 : 1
  const oldStatus = record.ruleStatus
  const idx = dataSource.value.findIndex(r => r.numberingRuleId === record.numberingRuleId)
  if (idx !== -1) dataSource.value[idx].ruleStatus = newStatus
  try {
    await updateNumberingRuleStatus({ numberingRuleId: record.numberingRuleId, ruleStatus: newStatus })
    message.success(checked ? '已启用' : '已禁用')
  } catch (e: any) {
    if (idx !== -1) dataSource.value[idx].ruleStatus = oldStatus
    message.error(e?.message || '操作失败')
  }
}

function handleDeleteOne(record: NumberingRule) {
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除编码规则「${record.ruleName}」吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await deleteNumberingRule(String(record.numberingRuleId))
        message.success('删除成功')
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的记录')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 条编码规则吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await deleteNumberingRuleBatch(selectedRows.value.map(r => String(r.numberingRuleId)))
        message.success('删除成功')
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
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
  advancedQueryForm.value = {}
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

async function handleExport() {
  try {
    loading.value = true
    const query: NumberingRuleQuery = {
      pageIndex: 1,
      pageSize: 99999,
      keyWords: queryKeyword.value || undefined,
      ...advancedQueryForm.value
    }
    const blob = await exportNumberingRules(query)
    const name = `编码规则_${Date.now()}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = name
    link.click()
    window.URL.revokeObjectURL(url)
    message.success('导出成功')
  } catch (e: any) {
    message.error(e?.message || '导出失败')
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
      await updateNumberingRule(values.numberingRuleId, values)
      message.success('更新成功')
    } else {
      await createNumberingRule(values)
      message.success('创建成功')
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: any) {
    if (e?.errorFields) return
    message.error(e?.message || '保存失败')
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
