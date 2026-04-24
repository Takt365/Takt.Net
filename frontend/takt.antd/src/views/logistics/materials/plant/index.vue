<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/logistics/materials/plant -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：工厂表管理页面，含查询、增删改，由 GenFunction/ControllerActions 驱动 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="logistics-materials-plant">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="searchPlaceholder"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="logistics:materials:plant:create"
      update-permission="logistics:materials:plant:update"
      delete-permission="logistics:materials:plant:delete"
      import-permission="logistics:materials:plant:import"
      export-permission="logistics:materials:plant:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="true"
      :show-export="true"
      :show-expand="false"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :create-disabled="false"
      :create-loading="loading"
      :update-disabled="updateDisabled"
      :update-loading="loading"
      :delete-disabled="deleteDisabled"
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
      :row-key="getPlantId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    />

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
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <PlantForm
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
      <a-form-item :label="t('entity.plant.plantcode')">
        <TaktSelect
          v-model:value="advancedQueryForm.plantCode"
          dict-type="hr_attendance_correction_approval"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.plant.plantcode') })"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.plant.plantname')">

        <a-input
          v-model:value="advancedQueryForm.plantName"
          :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantname') })"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.plant._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="excelNames.sheet"
        :template-file-name="excelNames.fileBase"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('entity.plant._self') })"
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
      :id-column-key="'plantId'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />
  </div>
</template>

<script setup lang="ts">
/**
 * 工厂表管理页 · 列表、查询、增删改由 GenFunction/ControllerActions 驱动
 * @module views/logistics/materials/plant
 */
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import PlantForm from './components/plant-form.vue'
import { getPlantList, createPlant, updatePlant, deletePlantById, deletePlantBatch, getPlantTemplate, importPlantData, exportPlantData } from '@/api/logistics/materials/plant'
import type { Plant, PlantQuery, PlantCreate, PlantUpdate} from '@/types/logistics/materials/plant'
import { logger } from '@/utils/logger'
import { resolveExportDownloadFileName } from '@/utils/export-download-name'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const excelNames = taktExcelEntityNames('TaktPlant')
const searchPlaceholder = computed(
  () => t('common.form.placeholder.required', { field: t('entity.plant._self') }) + t('common.button.query')
)

/** 关键词（查询栏） */
const queryKeyword = ref('')
/** 列表/表单加载中 */
const loading = ref(false)
/** 表格数据源 */
const dataSource = ref<Plant[]>([])
/** 当前页码 */
const currentPage = ref(1)
/** 每页条数 */
const pageSize = ref(20)
/** 总记录数 */
const total = ref(0)
/** 当前选中行（单行） */
const selectedRow = ref<Plant | null>(null)
/** 当前选中行列表（多选） */
const selectedRows = ref<Plant[]>([])
/** 当前选中行主键列表 */
const selectedRowKeys = ref<(string | number)[]>([])
/** 表单弹窗是否显示 */
const formVisible = ref(false)
/** 表单弹窗标题（新增/编辑） */
const formTitle = ref('')
/** 表单数据（新增为空对象，编辑为当前行） */
const formData = ref<Partial<Plant>>({})
/** 表单提交中 */
const formLoading = ref(false)
const formRef = ref()
/** 高级查询抽屉是否显示 */
const advancedQueryVisible = ref(false)
/** 高级查询表单模型（IsQuery 列） */
const advancedQueryForm = ref({
  plantCode: '',
  plantName: '',
})
/** 列设置抽屉是否显示 */
const columnSettingVisible = ref(false)
/** 导入弹窗是否显示 */
const importVisible = ref(false)
/** 当前可见列 key 列表（列设置勾选） */
const visibleColumnKeys = ref<string[]>([])

/** 主键字段名（与 types 一致，用于 getXxxId / 提交） */
const entityIdName = 'plantId'
const updateDisabled = computed(() => selectedRows.value.length !== 1)
const deleteDisabled = computed(() => selectedRows.value.length === 0)

onMounted(() => {
  loadData()
})

/** 表格列配置（ID + IsList 列 + 操作列）；用 computed 与 identity/user/index.vue 一致，列标题与操作列 label 随 locale 变化；列 key 与表单 label 同源 entity.{模块实体}.{字段小写} */
const columns = computed<TableColumnsType>(() => [
  {
    title: t('entity.plant.plantid'),
    dataIndex: 'plantId',
    key: 'entity.plant.plantid',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantId') ?? ''
  },
  {
    title: t('entity.plant.plantcode'),
    dataIndex: 'plantCode',
    key: 'entity.plant.plantcode',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantCode') ?? ''
  },
  {
    title: t('entity.plant.plantname'),
    dataIndex: 'plantName',
    key: 'entity.plant.plantname',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantName') ?? ''
  },
  {
    title: t('entity.plant.plantshortname'),
    dataIndex: 'plantShortName',
    key: 'entity.plant.plantshortname',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantShortName') ?? ''
  },
  {
    title: t('entity.plant.registrationaddress'),
    dataIndex: 'registrationAddress',
    key: 'entity.plant.registrationaddress',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationAddress') ?? ''
  },
  {
    title: t('entity.plant.registrationregion'),
    dataIndex: 'registrationRegion',
    key: 'entity.plant.registrationregion',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationRegion') ?? ''
  },
  {
    title: t('entity.plant.registrationprovince'),
    dataIndex: 'registrationProvince',
    key: 'entity.plant.registrationprovince',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationProvince') ?? ''
  },
  {
    title: t('entity.plant.registrationcity'),
    dataIndex: 'registrationCity',
    key: 'entity.plant.registrationcity',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationCity') ?? ''
  },
  {
    title: t('entity.plant.businessregion'),
    dataIndex: 'businessRegion',
    key: 'entity.plant.businessregion',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessRegion') ?? ''
  },
  {
    title: t('entity.plant.businessprovince'),
    dataIndex: 'businessProvince',
    key: 'entity.plant.businessprovince',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessProvince') ?? ''
  },
  {
    title: t('entity.plant.businesscity'),
    dataIndex: 'businessCity',
    key: 'entity.plant.businesscity',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessCity') ?? ''
  },
  {
    title: t('entity.plant.businessaddress'),
    dataIndex: 'businessAddress',
    key: 'entity.plant.businessaddress',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessAddress') ?? ''
  },
  {
    title: t('entity.plant.plantaddress'),
    dataIndex: 'plantAddress',
    key: 'entity.plant.plantaddress',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantAddress') ?? ''
  },
  {
    title: t('entity.plant.plantphone'),
    dataIndex: 'plantPhone',
    key: 'entity.plant.plantphone',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantPhone') ?? ''
  },
  {
    title: t('entity.plant.plantemail'),
    dataIndex: 'plantEmail',
    key: 'entity.plant.plantemail',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantEmail') ?? ''
  },
  {
    title: t('entity.plant.plantmanager'),
    dataIndex: 'plantManager',
    key: 'entity.plant.plantmanager',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantManager') ?? ''
  },
  {
    title: t('entity.plant.enterprisenature'),
    dataIndex: 'enterpriseNature',
    key: 'entity.plant.enterprisenature',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'enterpriseNature') ?? ''
  },
  {
    title: t('entity.plant.industryattribute'),
    dataIndex: 'industryAttribute',
    key: 'entity.plant.industryattribute',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'industryAttribute') ?? ''
  },
  {
    title: t('entity.plant.enterprisescale'),
    dataIndex: 'enterpriseScale',
    key: 'entity.plant.enterprisescale',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'enterpriseScale') ?? ''
  },
  {
    title: t('entity.plant.businessscope'),
    dataIndex: 'businessScope',
    key: 'entity.plant.businessscope',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessScope') ?? ''
  },
  {
    title: t('entity.plant.relatedcompany'),
    dataIndex: 'relatedCompany',
    key: 'entity.plant.relatedcompany',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'relatedCompany') ?? ''
  },
  {
    title: t('entity.plant.plantstatus'),
    dataIndex: 'plantStatus',
    key: 'entity.plant.plantstatus',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantStatus') ?? ''
  },
  {
    title: t('entity.plant.ordernum'),
    dataIndex: 'orderNum',
    key: 'entity.plant.ordernum',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'orderNum') ?? ''
  },
  {
    title: t('entity.plant.extfieldjson'),
    dataIndex: 'extFieldJson',
    key: 'entity.plant.extfieldjson',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'extFieldJson') ?? ''
  },
  {
    title: t('entity.plant.remark'),
    dataIndex: 'remark',
    key: 'entity.plant.remark',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'remark') ?? ''
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'standard',
        icon: RiEditLine,
        permission: 'logistics:materials:plant:update',
        onClick: (record: Plant) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'standard',
        icon: RiDeleteBinLine,
        permission: 'logistics:materials:plant:delete',
        onClick: (record: Plant) => handleDeleteOne(record)
      }
    ]
  })
])

/** 取当前行主键 ID（plantId） */
const getPlantId = (record: any): string => record?.[entityIdName] ?? ''
/** 取当前行指定字段值 */
const getPlantField = (record: any, field: string): any => record?.[field]

/** 合并默认列（含审计列等） */
const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
/** 根据列设置过滤后的显示列 */
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map((k: any) => String(k)))
  return merged.filter((col: any) => {
    const colKey = col.key || col.dataIndex || col.title
    return colKey && keysSet.has(String(colKey))
  })
})

/** 行选择配置（单选/多选联动 selectedRow、selectedRows） */
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Plant[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: Plant, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (getPlantId(selectedRow.value) === getPlantId(record)) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: Plant[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
    } else {
      selectedRow.value = null
    }
  }
}))

/** 行点击处理：点击行切换选中/取消；同步 selectedRowKeys、selectedRows、selectedRow，并调用 rowSelection.onChange 以同步表格复选框（与 identity/user 列表页 custom-row 标准一致） */
const onClickRow = (record: Plant) => ({
  onClick: () => {
    const key = getPlantId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) {
      selectedRowKeys.value.splice(index, 1)
    } else {
      selectedRowKeys.value.push(key)
    }
    selectedRows.value = dataSource.value.filter(item =>
      selectedRowKeys.value.includes(getPlantId(item))
    )
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    if (rowSelection.value.onChange) {
      rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
    }
  }
})

/** 加载列表数据（有列表接口时请求分页，否则清空） */
async function loadData() {
  loading.value = true
  try {
    const kw = (queryKeyword.value ?? '').trim()
    const params: PlantQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      ...advancedQueryForm.value
    }
    if (kw.length > 0) {
      params.KeyWords = kw
    }
    const res = await getPlantList(params)
    dataSource.value = res.data ?? []
    total.value = res.total ?? 0
  } catch (error: any) {
    logger.error('[Plant Management] 加载数据失败:', error)
    message.error(error?.message || t('common.msg.loadfail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

/** 查询：重置页码并重新加载 */
function handleSearch() {
  currentPage.value = 1
  loadData()
}

/** 重置：清空关键词与高级查询条件并重新加载 */
function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = {
    plantCode: '',
    plantName: '',
  }
  currentPage.value = 1
  loadData()
}

/** 打开新增表单 */
function handleCreate() {
  formTitle.value = t('common.button.create') + t('entity.plant._self')
  formData.value = {}
  formVisible.value = true
}

/** 打开编辑表单并回填当前行 */
function handleEdit(record: Plant) {
  formTitle.value = t('common.button.edit') + t('entity.plant._self')
  formData.value = { ...record }
  formVisible.value = true
}

/** 编辑当前选中行 */
function handleUpdate() {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
  } else {
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: t('entity.plant._self') }))
  }
}

/** 提交表单：有 ID 则更新，否则新增 */
async function handleFormSubmit() {
  const ref = formRef.value
  if (!ref?.validate) return
  try {
    await ref.validate()
  } catch {
    return
  }
  formLoading.value = true
  try {
    const id = (formData.value as any)?.[entityIdName]
    if (id) {
      await updatePlant(id, formData.value as any)
      message.success(t('common.msg.updatesuccess'))
    } else {
      await createPlant(formData.value as any)
      message.success(t('common.msg.createsuccess'))
    }
    formVisible.value = false
    loadData()
  } finally {
    formLoading.value = false
  }
}

/** 关闭表单弹窗 */
function handleFormCancel() {
  formVisible.value = false
}

/** 打开导入弹窗 */
function handleImport() {
  importVisible.value = true
}

/** 下载导入模板 */
async function handleDownloadTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  const res = await getPlantTemplate(sheetName, fileName)
  return (res as any)?.data ?? res
}

/** 执行导入 */
async function handleImportFile(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  return await importPlantData(file, sheetName)
}

/** 导入成功回调 */
function handleImportSuccess(result: { success: number; fail: number; errors: string[] }) {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}

/** 关闭导入弹窗 */
function handleImportCancel() {
  importVisible.value = false
}

/** 导出数据 */
async function handleExport() {
  try {
    loading.value = true
    const kw = (queryKeyword.value ?? '').trim()
    const exportQuery: PlantQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      ...advancedQueryForm.value
    }
    if (kw.length > 0) {
      exportQuery.KeyWords = kw
    }
    const exportMeta = await exportPlantData(exportQuery, excelNames.sheet, excelNames.fileBase)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fallbackBase = `${excelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}`
    const fileName = resolveExportDownloadFileName({
      contentDisposition: exportMeta.contentDisposition ?? null,
      contentType: exportMeta.contentType ?? null,
      fallbackBase
    })
    const url = window.URL.createObjectURL(exportMeta.blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportsuccess'))
  } catch (error: any) {
    logger.error('[Plant Management] 导出失败:', error)
    message.error(error?.message || t('common.msg.exportfail'))
  } finally {
    loading.value = false
  }
}

/** 单条删除（有 delete 时确认后调用接口） */
async function handleDeleteOne(record: Plant) {
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.plant._self'), name: t('common.action.thistarget', { target: t('entity.plant._self') }) }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      await deletePlantById((record as any)[entityIdName])
      message.success(t('common.msg.deletesuccess'))
      loadData()
    }
  })
}

/** 批量删除（有批量删除接口时确认后调用） */
async function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('entity.plant._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', { entity: t('entity.plant._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      const ids = selectedRows.value.map((r: any) => r[entityIdName]).filter(Boolean)
      await deletePlantBatch(ids)
      message.success(t('common.msg.deletesuccess'))
      loadData()
    }
  })
}

/** 打开高级查询抽屉 */
function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

/** 高级查询提交：关闭抽屉并重新加载 */
function handleAdvancedQuerySubmit() {
  advancedQueryVisible.value = false
  currentPage.value = 1
  loadData()
}

/** 高级查询重置：清空表单 */
function handleAdvancedQueryReset() {
  advancedQueryForm.value = {
    plantCode: '',
    plantName: '',
  }
}

/** 打开列设置抽屉 */
function handleColumnSetting() {
  columnSettingVisible.value = true
}

/** 列显示勾选变化 */
function handleColumnKeysChange(keys: string[]) {
  visibleColumnKeys.value = keys
}

/** 列设置重置为全部显示 */
function handleColumnSettingReset() {
  visibleColumnKeys.value = columns.value.map((c: any) => c.key || c.dataIndex).filter(Boolean)
}

/** 刷新列表 */
function handleRefresh() {
  loadData()
}

/** 表格 change 占位 */
function handleTableChange() {}
/** 列宽拖拽占位 */
function handleResizeColumn() {}
/** 分页页码变化 */
function handlePaginationChange(page: number) {
  currentPage.value = page
  loadData()
}
/** 分页每页条数变化 */
function handlePaginationSizeChange(_current: number, size: number) {
  pageSize.value = size
  currentPage.value = 1
  loadData()
}
</script>

<style scoped lang="less">
.logistics-materials-plant {
  padding: 0;
}
</style>
