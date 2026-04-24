<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/human-resource/personnel/employee-career -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：员工职业信息管理页面，包含列表、查询、新增、编辑、删除、导入、导出功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-personnel-employee-career">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.employeecareer.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="humanresource:personnel:employeecareer:create"
      update-permission="humanresource:personnel:employeecareer:update"
      delete-permission="humanresource:personnel:employeecareer:delete"
      import-permission="humanresource:personnel:employeecareer:import"
      template-permission="humanresource:personnel:employeecareer:template"
      export-permission="humanresource:personnel:employeecareer:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :update-disabled="!selectedRow"
      :delete-disabled="!selectedRow && selectedRows.length === 0"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @import="handleImport"
      @export="handleExport"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />

    <!-- 表格 -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getRowId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="8"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'workNature'">
          {{ getWorkNatureLabel(getField(record, 'workNature')) }}
        </template>
        <template v-else-if="column.key === 'employmentType'">
          {{ getEmploymentTypeLabel(getField(record, 'employmentType')) }}
        </template>
        <template v-else-if="column.key === 'isPrimary'">
          {{ getField(record, 'isPrimary') === 1 ? t('common.button.yes') : t('common.button.no') }}
        </template>
      </template>
    </TaktSingleTable>

    <!-- 分页 -->
    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />

    <!-- 新增/编辑弹窗 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <EmployeeCareerForm
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
      <a-form-item :label="t('entity.employeecareer.employeeId')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.employeecareer.isPrimary')">
        <a-select
          v-model:value="advancedQueryForm.isPrimary"
          :options="isPrimaryOptions"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入弹窗 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.employeecareer._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetnametemplate', { entity: t('entity.employeecareer._self') })"
        :template-file-name="t('common.action.import.sheetnametemplate', { entity: t('entity.employeecareer._self') })"
        template-permission="humanresource:personnel:employeecareer:template"
        import-permission="humanresource:personnel:employeecareer:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('entity.employeecareer._self') })"
        :upload-text="t('common.action.import.uploadtext')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <!-- 列设置抽屉 -->
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'id'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />
  </div>
</template>

<script setup lang="ts">
/**
 * 员工职业信息页面脚本：与 `career-form.vue`、`TaktImportFile`、`TaktColumnDrawer` 联动；
 * 负责查询、分页、表单弹窗、导入导出与列设置。
 */
import { computed, onMounted, ref } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { RiDeleteBinLine, RiEditLine } from '@remixicon/vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { logger } from '@/utils/logger'
import EmployeeCareerForm from './components/career-form.vue'
import {
  createEmployeeCareer,
  deleteEmployeeCareerBatch,
  deleteEmployeeCareerById,
  exportEmployeeCareerData,
  getEmployeeCareerById,
  getEmployeeCareerList,
  getEmployeeCareerTemplate,
  importEmployeeCareerData,
  updateEmployeeCareer
} from '@/api/human-resource/personnel/employee-career'
import type { EmployeeCareer, EmployeeCareerQuery } from '@/types/human-resource/personnel/employee-career'

const { t } = useI18n()
const entityKey = 'entity.employeecareer'

/** 是否主职筛选选项 */
const isPrimaryOptions = [
  { label: t('common.button.yes'), value: 1 },
  { label: t('common.button.no'), value: 0 }
]

/** 查询关键字 */
const queryKeyword = ref('')
/** 列表加载状态 */
const loading = ref(false)
/** 表格数据源 */
const dataSource = ref<EmployeeCareer[]>([])
/** 当前页码 */
const currentPage = ref(1)
/** 每页条数 */
const pageSize = ref(20)
/** 总记录数 */
const total = ref(0)
/** 单选中行 */
const selectedRow = ref<EmployeeCareer | null>(null)
/** 多选中行 */
const selectedRows = ref<EmployeeCareer[]>([])
/** 多选 key 集合 */
const selectedRowKeys = ref<(string | number)[]>([])
/** 表单弹窗开关 */
const formVisible = ref(false)
/** 表单弹窗标题 */
const formTitle = ref('')
/** 当前编辑数据 */
const formData = ref<Partial<EmployeeCareer>>({})
/** 表单提交状态 */
const formLoading = ref(false)
/** 表单实例 */
const formRef = ref()
/** 表格实例 */
const tableRef = ref()
/** 高级查询开关 */
const advancedQueryVisible = ref(false)
/** 高级查询模型 */
const advancedQueryForm = ref<{ employeeId?: string; isPrimary?: number }>({
  employeeId: '',
  isPrimary: undefined
})
/** 导入弹窗开关 */
const importVisible = ref(false)
/** 列设置开关 */
const columnSettingVisible = ref(false)
/** 当前可见列 key */
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

/** 获取行主键 */
const getRowId = (row: any): string =>
  row?.careerId != null ? String(row.careerId) : row?.id != null ? String(row.id) : ''

/** 读取行字段值 */
const getField = (row: any, field: string): any => row?.[field]

/** 工作性质显示值 */
function getWorkNatureLabel(value: number | undefined): string {
  return value !== undefined && value !== null ? t(`${entityKey}.workNature${value}`) : '-'
}

/** 任职类型显示值 */
function getEmploymentTypeLabel(value: number | undefined): string {
  return value !== undefined && value !== null ? t(`${entityKey}.employmentType${value}`) : '-'
}

/** 原始列配置 */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'careerId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getField(record, 'careerId') ?? getField(record, 'id') ?? ''
  },
  { title: t(`${entityKey}.employeeId`), dataIndex: 'employeeId', key: 'employeeId', width: 120, ellipsis: true },
  { title: t(`${entityKey}.deptName`), dataIndex: 'deptName', key: 'deptName', width: 120, ellipsis: true },
  { title: t(`${entityKey}.postName`), dataIndex: 'postName', key: 'postName', width: 100, ellipsis: true },
  { title: t(`${entityKey}.jobTitle`), dataIndex: 'jobTitle', key: 'jobTitle', width: 100, ellipsis: true },
  { title: t(`${entityKey}.joinDate`), dataIndex: 'joinDate', key: 'joinDate', width: 120 },
  { title: t(`${entityKey}.workNature`), dataIndex: 'workNature', key: 'workNature', width: 80 },
  { title: t(`${entityKey}.isPrimary`), dataIndex: 'isPrimary', key: 'isPrimary', width: 80 },
  { title: t('common.entity.createtime'), dataIndex: 'createdAt', key: 'createdAt', width: 160, ellipsis: true },
  CreateActionColumn<EmployeeCareer>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:personnel:employeecareer:update',
        onClick: (record: EmployeeCareer) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:personnel:employeecareer:delete',
        onClick: (record: EmployeeCareer) => handleDeleteOne(record)
      }
    ]
  })
])

/** 合并审计列后的完整列 */
const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))

/** 当前显示列 */
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  if (keys.length === 0) return columns.value

  const keysSet = new Set(keys.map((key) => String(key)))
  const getColumnKey = (column: any) => String(column.key || column.dataIndex || column.title || '')

  return mergedColumns.value.filter((column: any) => keysSet.has(getColumnKey(column)))
})

/** 表格勾选配置 */
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: EmployeeCareer[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (row: EmployeeCareer, selected: boolean) => {
    if (selected) {
      selectedRow.value = row
      return
    }
    if (selectedRow.value && getRowId(selectedRow.value) === getRowId(row)) {
      selectedRow.value = null
    }
  },
  onSelectAll: (_selected: boolean, rows: EmployeeCareer[]) => {
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

/** 行点击切换勾选 */
const onClickRow = (record: EmployeeCareer) => ({
  onClick: () => {
    const key = getRowId(record)
    const index = selectedRowKeys.value.indexOf(key)

    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)

    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getRowId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

/** 加载列表数据 */
async function loadData() {
  try {
    loading.value = true

    const params: EmployeeCareerQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }

    if (queryKeyword.value) params.employeeId = queryKeyword.value
    if (advancedQueryForm.value.employeeId) params.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.isPrimary !== undefined && advancedQueryForm.value.isPrimary !== null) {
      params.isPrimary = advancedQueryForm.value.isPrimary
    }

    const response = await getEmployeeCareerList(params)
    const responseAny = response as any

    dataSource.value = responseAny?.data ?? responseAny?.Data ?? []
    total.value = responseAny?.total ?? responseAny?.Total ?? 0
  } catch (error: any) {
    logger.error('[EmployeeCareer] loadData:', error)
    message.error(error?.message || t('common.msg.loadfail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

/** 查询 */
function handleSearch() {
  currentPage.value = 1
  loadData()
}

/** 重置查询 */
function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = { employeeId: '', isPrimary: undefined }
  currentPage.value = 1
  loadData()
}

/** 表格排序变更（当前保留） */
function handleTableChange() {}

/** 页码变更 */
function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

/** 分页大小变更 */
function handlePaginationSizeChange(_current: number, size: number) {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

/** 列宽调整（当前保留） */
function handleResizeColumn() {}

/** 打开新增弹窗 */
function handleCreate() {
  formTitle.value = t('common.button.create') + t(`${entityKey}._self`)
  formData.value = {}
  formVisible.value = true
}

/** 打开编辑弹窗 */
async function handleEdit(record: EmployeeCareer) {
  formTitle.value = t('common.button.edit') + t(`${entityKey}._self`)
  try {
    formLoading.value = true
    formData.value = { ...(await getEmployeeCareerById(getRowId(record))) }
    formVisible.value = true
  } catch (error: any) {
    message.error(error?.message || t('common.msg.loadfail'))
  } finally {
    formLoading.value = false
  }
}

/** 工具栏编辑按钮 */
function handleUpdate() {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
    return
  }
  message.warning(
    t('common.action.warnselecttoaction', {
      action: t('common.button.edit'),
      entity: t(`${entityKey}._self`)
    })
  )
}

/** 删除单条 */
function handleDeleteOne(record: EmployeeCareer) {
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', {
      entity: t(`${entityKey}._self`),
      name: getField(record, 'deptName') || getRowId(record)
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteEmployeeCareerById(getRowId(record))
        message.success(t('common.msg.deletesuccess', { target: t(`${entityKey}._self`) }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deletefail', { target: t(`${entityKey}._self`) }))
      } finally {
        loading.value = false
      }
    }
  })
}

/** 批量删除 */
function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnselecttoaction', {
        action: t('common.button.delete'),
        entity: t(`${entityKey}._self`)
      })
    )
    return
  }

  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', {
      entity: t(`${entityKey}._self`),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteEmployeeCareerById(getRowId(selectedRows.value[0]))
        } else {
          await deleteEmployeeCareerBatch(selectedRows.value.map((row) => getRowId(row)))
        }
        message.success(t('common.msg.deletesuccess', { target: t(`${entityKey}._self`) }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deletefail', { target: t(`${entityKey}._self`) }))
      } finally {
        loading.value = false
      }
    }
  })
}

/** 提交表单 */
async function handleFormSubmit() {
  try {
    if (!formRef.value) return

    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true

    if (formData.value?.careerId) {
      await updateEmployeeCareer(formData.value.careerId, {
        ...values,
        careerId: formData.value.careerId
      })
      message.success(t('common.msg.updatesuccess', { target: t(`${entityKey}._self`) }))
    } else {
      await createEmployeeCareer(values)
      message.success(t('common.msg.createsuccess', { target: t(`${entityKey}._self`) }))
    }

    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: any) {
    if (error?.errorFields) return
    message.error(error?.message || t('common.msg.operatefail', { action: t('common.action.operation') }))
  } finally {
    formLoading.value = false
  }
}

/** 关闭表单弹窗 */
function handleFormCancel() {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

/** 打开导入弹窗 */
function handleImport() {
  importVisible.value = true
}

/** 关闭导入弹窗 */
function handleImportCancel() {
  importVisible.value = false
}

/** 下载导入模板 */
async function handleDownloadTemplate(sheetName?: string, fileName?: string) {
  return await getEmployeeCareerTemplate(sheetName, fileName)
}

/** 导入文件 */
async function handleImportFile(file: File, sheetName?: string) {
  return importEmployeeCareerData(file, sheetName)
}

/** 导入成功回调 */
function handleImportSuccess(result: { success: number; fail: number; errors: string[] }) {
  loadData()
  if (result.fail === 0) {
    setTimeout(() => {
      importVisible.value = false
    }, 2000)
  }
}

/** 导出数据 */
async function handleExport() {
  try {
    loading.value = true

    const queryParams: EmployeeCareerQuery = {
      pageIndex: 1,
      pageSize: total.value || 9999
    }
    if (queryKeyword.value) queryParams.employeeId = queryKeyword.value
    if (advancedQueryForm.value.employeeId) queryParams.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.isPrimary !== undefined && advancedQueryForm.value.isPrimary !== null) {
      queryParams.isPrimary = advancedQueryForm.value.isPrimary
    }

    const blob = await exportEmployeeCareerData(
      queryParams,
      undefined,
      t(`${entityKey}._self`) + t('common.action.exportdatasuffix')
    )
    const data = (blob as any)?.data ?? blob
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t(`${entityKey}._self`) + t('common.action.exportdatasuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`

    const url = window.URL.createObjectURL(data)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)

    message.success(t('common.msg.exportsuccess', { target: t(`${entityKey}._self`) }))
  } catch (error: any) {
    logger.error('[EmployeeCareer] export:', error)
    message.error(error?.message || t('common.msg.exportfail', { target: t(`${entityKey}._self`) }))
  } finally {
    loading.value = false
  }
}

/** 打开高级查询 */
function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

/** 提交高级查询 */
function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

/** 重置高级查询 */
function handleAdvancedQueryReset() {
  advancedQueryForm.value = { employeeId: '', isPrimary: undefined }
}

/** 打开列设置 */
function handleColumnSetting() {
  columnSettingVisible.value = true
}

/** 更新可见列 */
function handleColumnKeysChange(keys: (string | number)[]) {
  visibleColumnKeys.value = keys.map((key) => String(key))
}

/** 重置列设置 */
function handleColumnSettingReset() {
  visibleColumnKeys.value = []
}

/** 刷新 */
function handleRefresh() {
  loadData()
}

defineExpose({ tableRef })
</script>

<style scoped lang="less">
.humanresource-personnel-employee-career {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
