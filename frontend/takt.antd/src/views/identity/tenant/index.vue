<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/tenant -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-27 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：租户管理页面；列表查询、新增/编辑弹窗、删除、启用/禁用、高级查询、列设置、导入/导出。 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="identity-tenant">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.required', { field: [t('entity.tenant.tenantname'), t('entity.tenant.tenantcode')].join('、') }) + t('common.button.query')"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="identity:tenant:create"
      update-permission="identity:tenant:update"
      delete-permission="identity:tenant:delete"
      import-permission="identity:tenant:import"
      export-permission="identity:tenant:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="true"
      :show-export="true"
      :show-expand="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :create-disabled="false"
      :update-disabled="updateDisabled"
      :delete-disabled="deleteDisabled"
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
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getTenantId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="7"
      :small-screen-column-count="4"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <!-- 自定义列渲染：租户状态（字典 sys_normal_disable） -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'tenantStatus'">
          <TaktDictTag
            :value="getTenantField(record, 'tenantStatus')"
            dict-type="sys_normal_disable"
          />
        </template>
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

    <!-- 新增/编辑对话框：视口宽 50%，可拖拽调整宽高（wrap-class-name） -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <TenantForm
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
      <a-form-item :label="t('entity.tenant.tenantname')">
        <a-input v-model:value="advancedQueryForm.TenantName" />
      </a-form-item>
      <a-form-item :label="t('entity.tenant.tenantcode')">
        <a-input v-model:value="advancedQueryForm.TenantCode" />
      </a-form-item>
      <a-form-item :label="t('entity.tenant.tenantstatus')">
        <TaktSelect
          v-model:value="advancedQueryForm.TenantStatus"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_normal_disable"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.tenant.tenantstatus') })"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.tenant._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="tenantExcelNames.sheet"
        :template-file-name="tenantExcelNames.fileBase"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.tenant._self') })"
        :upload-text="t('common.action.import.uploadText')"
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
 * 租户列表页脚本模块。
 * - API：@/api/identity/tenant；表单子组件为 ./components/tenant-form.vue。
 * - 与同仓库列表页一致：TaktQueryBar、TaktToolsBar、TaktSingleTable、TaktPagination 及高级查询/列设置/导入导出。
 */
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import TenantForm from './components/tenant-form.vue'
import {
  getTenantList,
  createTenant,
  updateTenant,
  deleteTenantById,
  updateTenantStatus,
  getTenantTemplate,
  importTenantData,
  exportTenantData
} from '@/api/identity/tenant'
import type { Tenant, TenantQuery } from '@/types/identity/tenant'
import { logger } from '@/utils/logger'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine, RiCheckLine, RiCloseLine } from '@remixicon/vue'

const { t } = useI18n()

// 导入/导出 Excel 工作表名与文件名前缀（与服务端实体名 TaktTenant 一致）
const tenantExcelNames = taktExcelEntityNames('TaktTenant')

// 顶栏查询关键字
const queryKeyword = ref('')
// 表格加载中
const loading = ref(false)
// 表格数据
const dataSource = ref<Tenant[]>([])
// 分页：当前页、每页条数、总条数
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
// 行选择：当前单行、多选行、勾选 key
const selectedRow = ref<Tenant | null>(null)
const selectedRows = ref<Tenant[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
// 新增/编辑表单弹窗
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Tenant>>({})
const formLoading = ref(false)
const formRef = ref()
// 高级查询抽屉（字段名与请求 DTO 一致，使用 PascalCase）
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  TenantName?: string
  TenantCode?: string
  TenantStatus?: number
}>({
  TenantName: '',
  TenantCode: '',
  TenantStatus: undefined
})
// 导入弹窗
const importVisible = ref(false)
// 列设置抽屉
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

// 初始化时加载数据
onMounted(() => {
  loadData()
})

// 表格列配置（computed 以便列标题与操作列 label 随 locale 更新）
// 业务列顺序与后端 TaktTenant、TaktTenantDto、TaktTenantEntitiesSeedData 展示字段对齐（不含操作列共 6 列）
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'tenantId',
    key: 'tenantId',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getTenantField(record, 'tenantId') || ''
  },
  {
    title: t('entity.tenant.name'),
    dataIndex: 'tenantName',
    key: 'tenantName',
    width: 140,
    resizable: true,
    ellipsis: true,
    sorter: (a: any, b: any) => {
      const aName = getTenantField(a, 'tenantName') || ''
      const bName = getTenantField(b, 'tenantName') || ''
      return aName.localeCompare(bName)
    }
  },
  {
    title: t('entity.tenant.code'),
    dataIndex: 'tenantCode',
    key: 'tenantCode',
    width: 120,
    resizable: true,
    ellipsis: true,
    sorter: (a: any, b: any) => {
      const aCode = getTenantField(a, 'tenantCode') || ''
      const bCode = getTenantField(b, 'tenantCode') || ''
      return aCode.localeCompare(bCode)
    }
  },
  {
    title: t('entity.tenant.starttime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.tenant.endtime'),
    dataIndex: 'endTime',
    key: 'endTime',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.tenant.status'),
    dataIndex: 'tenantStatus',
    key: 'tenantStatus',
    width: 100
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'identity:tenant:update',
        onClick: (record: Tenant) => handleEdit(record)
      },
      {
        key: 'disable',
        label: t('common.button.disable'),
        shape: 'plain',
        icon: RiCloseLine,
        permission: 'identity:tenant:update',
        visible: (record: Tenant) => getTenantField(record, 'tenantStatus') === 1,
        onClick: (record: Tenant) => handleToggleStatus(record)
      },
      {
        key: 'enable',
        label: t('common.button.enable'),
        shape: 'plain',
        icon: RiCheckLine,
        permission: 'identity:tenant:update',
        visible: (record: Tenant) => getTenantField(record, 'tenantStatus') !== 0,
        onClick: (record: Tenant) => handleToggleStatus(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'identity:tenant:delete',
        onClick: (record: Tenant) => handleDeleteOne(record)
      }
    ]
  })
])

// 辅助函数：行主键（与后端 TenantId 序列化一致，表格 row-key 使用）
const getTenantId = (record: any): string => record?.tenantId ?? ''

// 辅助函数：安全读取行字段（兼容接口返回结构差异）
const getTenantField = (record: any, field: string): any => record?.[field]

// 更新/删除按钮禁用状态（与表格勾选同步）
// 更新：仅当选中恰好 1 行时可点；删除：至少选中 1 行时可点
const updateDisabled = computed(() => selectedRows.value.length !== 1)
const deleteDisabled = computed(() => selectedRows.value.length === 0)

// 合并列配置（含默认审计列等）；使用 any 避免 mergeDefaultColumns 与 TableColumnsType 递归过深
const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))

// 根据列设置抽屉勾选的 key 过滤展示列，保持 mergedColumns 中的原始顺序
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []

  // 未勾选任何列时展示全部业务列（等待 TaktColumnDrawer 初始化或用户未改列）
  if (keys.length === 0) {
    return columns.value
  }

  const getColumnKey = (col: any): string => {
    const key = col.key || col.dataIndex || col.title
    return key ? String(key) : ''
  }

  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

// 行选择配置（与 Ant Design Vue Table rowSelection 一致）
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Tenant[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Tenant, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (getTenantId(selectedRow.value) === getTenantId(record)) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: Tenant[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? selectedRowsData[0] : null
    } else {
      selectedRow.value = null
    }
  }
}))

// 行点击：切换当前行在勾选集合中的选中状态，并同步 selectedRows / selectedRow
const onClickRow = (record: Tenant) => ({
  onClick: () => {
    const key = getTenantId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) {
      selectedRowKeys.value.splice(index, 1)
    } else {
      selectedRowKeys.value.push(key)
    }

    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getTenantId(item)))
    selectedRow.value = selectedRows.value.length === 1 ? selectedRows.value[0] : null

    if (rowSelection.value.onChange) {
      rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
    }
  }
})

// 加载分页列表（顶栏关键字 + 高级查询条件一并传入，请求参数与后端 TaktTenantQueryDto 对齐）
const loadData = async () => {
  try {
    loading.value = true
    const params: any = {
      PageIndex: currentPage.value,
      PageSize: pageSize.value
    }

    if (queryKeyword.value) {
      params.KeyWords = queryKeyword.value
    }
    if (advancedQueryForm.value.TenantName) {
      params.TenantName = advancedQueryForm.value.TenantName
    }
    if (advancedQueryForm.value.TenantCode) {
      params.TenantCode = advancedQueryForm.value.TenantCode
    }
    if (advancedQueryForm.value.TenantStatus !== undefined) {
      params.TenantStatus = advancedQueryForm.value.TenantStatus
    }

    const response = await getTenantList(params as TenantQuery)
    // 兼容 TaktPagedResult 与后端可能返回的 PascalCase（Data / Total）
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Tenant Management] 加载数据失败:', error)
    message.error(error.message || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 查询（顶栏搜索：回到第一页并拉取）
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

// 重置（清空关键字与高级查询，回到第一页）
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = { TenantName: '', TenantCode: '', TenantStatus: undefined }
  currentPage.value = 1
  loadData()
}

// 表格变化（当前仅记录排序；分页由 TaktPagination 独立触发）
const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) {
    logger.debug('[Tenant Management] 排序:', sorter.field, sorter.order)
  }
}

// 分页：页码或每页条数变化
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

// 分页：仅每页条数变化（回到第一页）
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

// 列宽拖拽调整：写回 columns 中对应列的 width
const handleResizeColumn = (w: number, col: any) => {
  const column = (columns.value as any[]).find((c: any) => {
    const colKey = col.key || col.dataIndex || col.title
    const cKey = c.key || c.dataIndex || c.title
    return colKey && cKey && String(colKey) === String(cKey)
  })
  if (column) column.width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.tenant._self')
  formData.value = {}
  formVisible.value = true
}

// 编辑（行内按钮：直接带入当前行数据打开弹窗）
const handleEdit = (record: Tenant) => {
  formTitle.value = t('common.button.edit') + t('entity.tenant._self')
  formData.value = { ...record }
  formVisible.value = true
}

// 更新（工具栏：依赖单行选中）
const handleUpdate = () => {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
  } else {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.tenant._self') }))
  }
}

// 删除单条
const handleDeleteOne = (record: Tenant) => {
  const name = getTenantField(record, 'tenantName') || t('common.action.thisTarget', { target: t('entity.tenant._self') })
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.tenant._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteTenantById(getTenantId(record))
        message.success(t('common.msg.deleteSuccess'))
        loadData()
      } catch (error: any) {
        message.error(error.message || t('common.msg.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

// 删除（工具栏：批量删除当前勾选）
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.tenant._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: t('entity.tenant._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        for (const row of selectedRows.value) {
          await deleteTenantById(getTenantId(row))
        }
        message.success(t('common.msg.deleteSuccess'))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error.message || t('common.msg.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

// 启用 / 禁用（调用 updateTenantStatus）
const handleToggleStatus = (record: Tenant) => {
  const id = getTenantId(record)
  const currentStatus = getTenantField(record, 'tenantStatus')
  const newStatus = currentStatus === 1 ? 0 : 1
  const action = newStatus === 1 ? t('common.button.enable') : t('common.button.disable')
  Modal.confirm({
    title: t('common.action.confirmAction', { action }),
    content: t('common.action.confirmAction', { action }) + '？',
    okText: t('common.button.ok'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await updateTenantStatus({ tenantId: id, tenantStatus: newStatus })
        message.success(t('common.msg.actionSuccess', { action }))
        loadData()
      } catch (error: any) {
        message.error(error.message || t('common.msg.actionFail', { action }))
      } finally {
        loading.value = false
      }
    }
  })
}

// 打开导入弹窗
const handleImport = () => {
  importVisible.value = true
}

// 下载导入模板
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getTenantTemplate(sheetName, fileName)
}

// 执行导入上传
const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importTenantData(file, sheetName)
}

// 导入完成回调
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  logger.info('[Tenant Management] 导入成功:', result)
  loadData()
  if (result.fail === 0) {
    setTimeout(() => { importVisible.value = false }, 2000)
  }
}

// 关闭导入弹窗
const handleImportCancel = () => {
  importVisible.value = false
}

// 导出当前查询条件下的列表为 xlsx
const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: any = {}
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.TenantName) queryParams.TenantName = advancedQueryForm.value.TenantName
    if (advancedQueryForm.value.TenantCode) queryParams.TenantCode = advancedQueryForm.value.TenantCode
    if (advancedQueryForm.value.TenantStatus !== undefined) queryParams.TenantStatus = advancedQueryForm.value.TenantStatus

    const blob = await exportTenantData(queryParams as TenantQuery, tenantExcelNames.sheet, tenantExcelNames.fileBase)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${tenantExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess'))
  } catch (error: any) {
    logger.error('[Tenant Management] 导出失败:', error)
    message.error(error.message || t('common.msg.exportFail'))
  } finally {
    loading.value = false
  }
}

// 打开高级查询抽屉
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

// 高级查询提交
const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

// 高级查询表单重置
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { TenantName: '', TenantCode: '', TenantStatus: undefined }
}

const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置：勾选列 key 变更
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}

// 列设置恢复默认（展示全部列）
const handleColumnSettingReset = () => {
  visibleColumnKeys.value = []
}

// 刷新列表
const handleRefresh = () => {
  loadData()
}

// 表单提交：有 tenantId 则更新，否则创建
const handleFormSubmit = async () => {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const formValues = formRef.value.getValues()

    formLoading.value = true

    if (formData.value.tenantId) {
      const updatePayload = {
        tenantId: formData.value.tenantId,
        tenantName: formValues.tenantName || '',
        tenantCode: formValues.tenantCode || '',
        configId: formValues.configId || '0',
        startTime: formValues.startTime,
        endTime: formValues.endTime,
        remark: formValues.remark || ''
      }
      await updateTenant(formData.value.tenantId, updatePayload)
      message.success(t('common.msg.updateSuccess'))
    } else {
      const createPayload = {
        tenantName: formValues.tenantName || '',
        tenantCode: formValues.tenantCode || '',
        configId: formValues.configId || '0',
        startTime: formValues.startTime,
        endTime: formValues.endTime,
        remark: formValues.remark || ''
      }
      await createTenant(createPayload)
      message.success(t('common.msg.createSuccess'))
    }

    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: any) {
    if (error.errorFields) return
    message.error(error.message || t('common.msg.operateFail'))
  } finally {
    formLoading.value = false
  }
}

// 取消表单：关闭弹窗并清空表单状态
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}
</script>

<style scoped lang="less">
/* 页面根容器内边距 */
.identity-tenant {
  padding: 16px;
}
</style>
