<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/controlling/cost-center -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：成本中心管理列表页。分页列表、关键字检索、新增/编辑弹窗、单条与批量删除、Excel 导入导出、列显示设置；表单组件为 `components/cost-center-form.vue`，权限字面值与 `TaktCostCentersController` 对齐。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="accounting-controlling-cost-center">
    <!-- 顶部：关键字检索 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.costcenter.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：权限字面值与后端控制器一致 -->
    <TaktToolsBar
      create-permission="accounting:controlling:costcenter:create"
      update-permission="accounting:controlling:costcenter:update"
      delete-permission="accounting:controlling:costcenter:delete"
      import-permission="accounting:controlling:costcenter:import"
      template-permission="accounting:controlling:costcenter:template"
      export-permission="accounting:controlling:costcenter:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="true"
      :show-export="true"
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
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />

    <!-- 主表 -->
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getCostCenterId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @resize-column="handleResizeColumn"
    />

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
      <CostCenterForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 导入弹窗 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.costcenter._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="excelNames.sheet"
        :template-file-name="excelNames.fileBase"
        template-permission="accounting:controlling:costcenter:template"
        import-permission="accounting:controlling:costcenter:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.costcenter._self') })"
        :upload-text="t('common.action.import.uploadText')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <!-- 列设置 -->
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
import { computed, onMounted, ref } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { RiDeleteBinLine, RiEditLine } from '@remixicon/vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { taktExcelEntityNames } from '@/utils/naming'
import type { CostCenter, CostCenterCreate, CostCenterQuery, CostCenterUpdate } from '@/types/accounting/controlling/cost-center'
import CostCenterForm from './components/cost-center-form.vue'
import {
  createCostCenter,
  deleteCostCenterById,
  exportCostCenterData,
  getCostCenterList,
  getCostCenterTemplate,
  importCostCenterData,
  updateCostCenter
} from '@/api/accounting/controlling/cost-center'

/**
 * 国际化函数。
 */
const { t } = useI18n()

/**
 * 导入导出默认 sheet/file 名。
 */
const excelNames = taktExcelEntityNames('TaktCostCenter')

/**
 * 顶部关键字查询。
 */
const queryKeyword = ref('')

/**
 * 页面加载态。
 */
const loading = ref(false)

/**
 * 列表数据源。
 */
const dataSource = ref<CostCenter[]>([])

/**
 * 当前页码。
 */
const currentPage = ref(1)

/**
 * 每页条数。
 */
const pageSize = ref(20)

/**
 * 总记录数。
 */
const total = ref(0)

/**
 * 当前单选行。
 */
const selectedRow = ref<CostCenter | null>(null)

/**
 * 当前多选行。
 */
const selectedRows = ref<CostCenter[]>([])

/**
 * 多选主键集合。
 */
const selectedRowKeys = ref<(string | number)[]>([])

/**
 * 编辑弹窗开关。
 */
const formVisible = ref(false)

/**
 * 编辑弹窗标题。
 */
const formTitle = ref('')

/**
 * 编辑弹窗初始化数据。
 */
const formData = ref<Partial<CostCenter>>({})

/**
 * 编辑提交 loading。
 */
const formLoading = ref(false)

/**
 * 子表单组件引用。
 */
const formRef = ref<InstanceType<typeof CostCenterForm> | null>(null)

/**
 * 导入弹窗开关。
 */
const importVisible = ref(false)

/**
 * 列设置抽屉开关。
 */
const columnSettingVisible = ref(false)

/**
 * 列设置：可见列 key 集合。
 */
const visibleColumnKeys = ref<string[]>([])

/**
 * 行主键字符串。
 *
 * @param {CostCenter} record
 * @returns {string}
 */
const getCostCenterId = (record: CostCenter): string => String(record.costCenterId ?? '')

/**
 * 读取错误消息。
 *
 * @param {unknown} error
 * @returns {string | undefined}
 */
function getErrorMessage(error: unknown): string | undefined {
  if (error instanceof Error) return error.message
  if (typeof error === 'object' && error !== null && 'message' in error) {
    const msg = (error as { message?: unknown }).message
    return typeof msg === 'string' ? msg : undefined
  }
  return undefined
}

/**
 * 列配置（成本中心）。
 */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    key: 'id',
    dataIndex: 'costCenterId',
    width: 90,
    fixed: 'left'
  },
  {
    title: t('entity.costcenter.costcentercode'),
    key: 'costCenterCode',
    dataIndex: 'costCenterCode',
    width: 140
  },
  {
    title: t('entity.costcenter.costcentername'),
    key: 'costCenterName',
    dataIndex: 'costCenterName',
    width: 180
  },
  {
    title: t('entity.costcenter.parentid'),
    key: 'parentId',
    dataIndex: 'parentId',
    width: 120
  },
  {
    title: t('entity.costcenter.costcentertype'),
    key: 'costCenterType',
    dataIndex: 'costCenterType',
    width: 120
  },
  {
    title: t('entity.costcenter.deptname'),
    key: 'deptName',
    dataIndex: 'deptName',
    width: 140
  },
  {
    title: t('entity.costcenter.managername'),
    key: 'managerName',
    dataIndex: 'managerName',
    width: 120
  },
  {
    title: t('entity.costcenter.ordernum'),
    key: 'orderNum',
    dataIndex: 'orderNum',
    width: 100
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'accounting:controlling:costcenter:update',
        onClick: (record: Record<string, unknown>) => {
          handleEdit(record as unknown as CostCenter)
        }
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'accounting:controlling:costcenter:delete',
        onClick: (record: Record<string, unknown>) => {
          handleDeleteOne(record as unknown as CostCenter)
        }
      }
    ]
  })
])

/**
 * 合并默认实体列后的列配置。
 */
const mergedColumns = computed(() => mergeDefaultColumns(columns.value, t, true))

/**
 * 当前可见列。
 */
const displayColumns = computed(() => {
  if (visibleColumnKeys.value.length === 0) return columns.value
  const keySet = new Set(visibleColumnKeys.value)
  return mergedColumns.value.filter((column: any) => {
    const key = String(column.key ?? column.dataIndex ?? column.title ?? '')
    return keySet.has(key)
  })
})

/**
 * 表格勾选配置。
 */
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: CostCenter[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

/**
 * 行点击切换选中。
 *
 * @param {CostCenter} record
 * @returns {{ onClick: () => void }}
 */
const onClickRow = (record: CostCenter) => ({
  onClick: () => {
    const key = getCostCenterId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) {
      selectedRowKeys.value.splice(index, 1)
    } else {
      selectedRowKeys.value.push(key)
    }
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getCostCenterId(item)))
    selectedRow.value = selectedRows.value.length === 1 ? selectedRows.value[0] : null
  }
})

/**
 * 加载列表数据。
 */
const loadData = async () => {
  try {
    loading.value = true
    const params: CostCenterQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) {
      params.keyWords = queryKeyword.value
    }

    const result = await getCostCenterList(params)
    dataSource.value = result?.data ?? []
    total.value = Number(result?.total ?? 0)
  } catch (error: unknown) {
    message.error(getErrorMessage(error) || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

/**
 * 搜索：重置到第一页。
 */
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

/**
 * 重置查询条件。
 */
const handleReset = () => {
  queryKeyword.value = ''
  currentPage.value = 1
  loadData()
}

/**
 * 页码变化。
 *
 * @param {number} page
 * @param {number} size
 */
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

/**
 * 每页条数变化。
 *
 * @param _current
 * @param {number} size
 */
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

/**
 * 列宽变更回写。
 *
 * @param {number} width
 * @param {any} column
 */
const handleResizeColumn = (width: number, column: any) => {
  const hit = columns.value.find((item: any) => String(item.key ?? item.dataIndex) === String(column.key ?? column.dataIndex))
  if (hit) {
    ;(hit as any).width = width
  }
}

/**
 * 打开新增弹窗。
 */
const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.costcenter._self')
  formData.value = {}
  formVisible.value = true
}

/**
 * 打开编辑弹窗。
 *
 * @param {CostCenter} record
 */
const handleEdit = (record: CostCenter) => {
  formTitle.value = t('common.button.edit') + t('entity.costcenter._self')
  formData.value = { ...record }
  formVisible.value = true
}

/**
 * 工具栏编辑入口。
 */
const handleUpdate = () => {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
  } else {
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.edit'),
        entity: t('entity.costcenter._self')
      })
    )
  }
}

/**
 * 删除单条。
 *
 * @param {CostCenter} record
 */
const handleDeleteOne = (record: CostCenter) => {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', {
      entity: t('entity.costcenter._self'),
      name: record.costCenterName ?? ''
    }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteCostCenterById(getCostCenterId(record))
        message.success(t('common.msg.deleteSuccess'))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 删除多条（逐条调用删除接口）。
 */
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    return
  }

  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      count: selectedRows.value.length,
      entity: t('entity.costcenter._self')
    }),
    onOk: async () => {
      try {
        loading.value = true
        for (const row of selectedRows.value) {
          await deleteCostCenterById(getCostCenterId(row))
        }
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        message.success(t('common.msg.deleteSuccess'))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 提交新增/编辑表单。
 */
const handleFormSubmit = async () => {
  try {
    if (!formRef.value) return

    await formRef.value.validate()
    const values = formRef.value.getValues() as CostCenterCreate & { costCenterId?: string }
    formLoading.value = true

    if (formData.value.costCenterId) {
      await updateCostCenter(String(formData.value.costCenterId), values as CostCenterUpdate)
      message.success(t('common.msg.updateSuccess'))
    } else {
      await createCostCenter(values)
      message.success(t('common.msg.createSuccess'))
    }

    formVisible.value = false
    formData.value = {}
    formRef.value.resetFields()
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    message.error(getErrorMessage(error) || t('common.msg.operateFail'))
  } finally {
    formLoading.value = false
  }
}

/**
 * 关闭编辑弹窗并重置状态。
 */
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

/**
 * 打开导入弹窗。
 */
const handleImport = () => {
  importVisible.value = true
}

/**
 * 下载导入模板。
 *
 * @param {string} [sheetName]
 * @param {string} [fileName]
 * @returns {Promise<import('@/api/request').BlobDownloadWithMeta>}
 */
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getCostCenterTemplate(sheetName, fileName)
}

/**
 * 导入文件上传。
 *
 * @param {File} file
 * @param {string} [sheetName]
 * @returns {Promise<{ success: number; fail: number; errors: string[] }>}
 */
const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importCostCenterData(file, sheetName)
}

/**
 * 导入成功回调。
 */
const handleImportSuccess = () => {
  loadData()
  setTimeout(() => {
    importVisible.value = false
  }, 1500)
}

/**
 * 关闭导入弹窗。
 */
const handleImportCancel = () => {
  importVisible.value = false
}

/**
 * 导出当前查询条件。
 */
const handleExport = async () => {
  try {
    loading.value = true
    const query: CostCenterQuery = {
      pageIndex: 1,
      pageSize: 100000
    }
    if (queryKeyword.value) query.keyWords = queryKeyword.value

    const blob = await exportCostCenterData(query, excelNames.sheet, excelNames.fileBase)
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `${excelNames.fileBase}_${Date.now()}.xlsx`
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess'))
  } catch (error: unknown) {
    message.error(getErrorMessage(error) || t('common.msg.exportFail'))
  } finally {
    loading.value = false
  }
}

/**
 * 打开列设置。
 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

/**
 * 列勾选变化。
 *
 * @param {(string | number)[]} keys
 */
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map((key) => String(key))
}

/**
 * 重置列设置。
 */
const handleColumnSettingReset = () => {
  visibleColumnKeys.value = []
}

/**
 * 刷新列表。
 */
const handleRefresh = () => {
  loadData()
}

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.accounting-controlling-cost-center {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
