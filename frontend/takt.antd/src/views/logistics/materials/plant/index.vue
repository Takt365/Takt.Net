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
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入关键词"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="logistics_materials:plant:create"
      update-permission="logistics_materials:plant:update"
      delete-permission="logistics_materials:plant:delete"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-advanced-query="true"
      :show-column-setting="true"
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
      :row-key="getPlantId"
      :row-selection="rowSelection"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    />

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
      :width="'33.333vw'"
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

    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item label="工厂代码">
        <a-input v-model:value="advancedQueryForm.plantCode" />
      </a-form-item>
      <a-form-item label="工厂名称">
        <a-input v-model:value="advancedQueryForm.plantName" />
      </a-form-item>
      <a-form-item label="工厂简称">
        <a-input v-model:value="advancedQueryForm.plantShortName" />
      </a-form-item>
    </TaktQueryDrawer>

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
import { getPlantList, getPlantById, createPlant, updatePlant, deletePlantById, deletePlantBatch, getPlantTemplate, importPlantData, exportPlantData } from '@/api/logistics/materials/plant'
import type { Plant, PlantQuery, PlantCreate, PlantUpdate, PlantTemplate, PlantImport, PlantExport } from '@/types/logistics/materials/plant'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

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
const tableRef = ref()
/** 高级查询抽屉是否显示 */
const advancedQueryVisible = ref(false)
/** 高级查询表单模型（IsQuery 列） */
const advancedQueryForm = ref({
  plantCode: '',
  plantName: '',
  plantShortName: '',
})
/** 列设置抽屉是否显示 */
const columnSettingVisible = ref(false)
/** 当前可见列 key 列表（列设置勾选） */
const visibleColumnKeys = ref<string[]>([])
/** 排序方向（由表配置 SortType 驱动） */
const sortOrder = ref<'asc' | 'desc'>('asc')
/** 排序字段（由表配置 SortField 驱动，驼峰） */
const sortField = ref('plant_code')

/** 主键字段名（与 types 一致，用于 getXxxId / 提交） */
const entityIdName = 'plantId'

onMounted(() => {
  loadData()
})

/** 表格列配置（ID + IsList 列 + 操作列） */
const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'plantId',
    key: 'plantId',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantId') ?? ''
  },
  {
    title: '工厂代码',
    dataIndex: 'plantCode',
    key: 'plantCode',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantCode') ?? ''
  },
  {
    title: '工厂名称',
    dataIndex: 'plantName',
    key: 'plantName',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantName') ?? ''
  },
  {
    title: '工厂简称',
    dataIndex: 'plantShortName',
    key: 'plantShortName',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantShortName') ?? ''
  },
  {
    title: '注册地址',
    dataIndex: 'registrationAddress',
    key: 'registrationAddress',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationAddress') ?? ''
  },
  {
    title: '注册地区-国家',
    dataIndex: 'registrationRegion',
    key: 'registrationRegion',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationRegion') ?? ''
  },
  {
    title: '注册地区-省',
    dataIndex: 'registrationProvince',
    key: 'registrationProvince',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationProvince') ?? ''
  },
  {
    title: '注册地区-市',
    dataIndex: 'registrationCity',
    key: 'registrationCity',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'registrationCity') ?? ''
  },
  {
    title: '经营地区-国家',
    dataIndex: 'businessRegion',
    key: 'businessRegion',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessRegion') ?? ''
  },
  {
    title: '经营地区-省',
    dataIndex: 'businessProvince',
    key: 'businessProvince',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessProvince') ?? ''
  },
  {
    title: '经营地区-市',
    dataIndex: 'businessCity',
    key: 'businessCity',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessCity') ?? ''
  },
  {
    title: '经营地址',
    dataIndex: 'businessAddress',
    key: 'businessAddress',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessAddress') ?? ''
  },
  {
    title: '工厂地址',
    dataIndex: 'plantAddress',
    key: 'plantAddress',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantAddress') ?? ''
  },
  {
    title: '工厂电话',
    dataIndex: 'plantPhone',
    key: 'plantPhone',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantPhone') ?? ''
  },
  {
    title: '工厂邮箱',
    dataIndex: 'plantEmail',
    key: 'plantEmail',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantEmail') ?? ''
  },
  {
    title: '工厂负责人',
    dataIndex: 'plantManager',
    key: 'plantManager',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantManager') ?? ''
  },
  {
    title: '企业性质',
    dataIndex: 'enterpriseNature',
    key: 'enterpriseNature',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'enterpriseNature') ?? ''
  },
  {
    title: '行业属性',
    dataIndex: 'industryAttribute',
    key: 'industryAttribute',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'industryAttribute') ?? ''
  },
  {
    title: '企业规模',
    dataIndex: 'enterpriseScale',
    key: 'enterpriseScale',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'enterpriseScale') ?? ''
  },
  {
    title: '经营范围',
    dataIndex: 'businessScope',
    key: 'businessScope',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'businessScope') ?? ''
  },
  {
    title: '关联公司',
    dataIndex: 'relatedCompany',
    key: 'relatedCompany',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'relatedCompany') ?? ''
  },
  {
    title: '工厂状态',
    dataIndex: 'plantStatus',
    key: 'plantStatus',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'plantStatus') ?? ''
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'orderNum') ?? ''
  },
  {
    title: '扩展字段JSON',
    dataIndex: 'extFieldJson',
    key: 'extFieldJson',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'extFieldJson') ?? ''
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    resizable: true,
    ellipsis: true,
    customRender: ({ record }: { record: any }) => getPlantField(record, 'remark') ?? ''
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: '编辑',
        shape: 'default',
        icon: RiEditLine,
        permission: 'logistics_materials:plant:update',
        onClick: (record: Plant) => handleEdit(record)
      },
      {
        key: 'delete',
        label: '删除',
        shape: 'default',
        icon: RiDeleteBinLine,
        permission: 'logistics_materials:plant:delete',
        onClick: (record: Plant) => handleDeleteOne(record)
      }
    ]
  })
])

/** 取当前行主键 ID（plantId） */
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
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

/** 加载列表数据（有列表接口时请求分页，否则清空） */
async function loadData() {
  loading.value = true
  try {
    const res = await getPlantList({
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      keyWords: queryKeyword.value || undefined,
      sortOrder: sortOrder.value || undefined,
      sortField: sortField.value || undefined,
      ...advancedQueryForm.value
    })
    dataSource.value = res.data ?? []
    total.value = res.total ?? 0
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
    plantShortName: '',
  }
  currentPage.value = 1
  loadData()
}

/** 打开新增表单 */
function handleCreate() {
  formTitle.value = '新增工厂表'
  formData.value = {}
  formVisible.value = true
}

/** 打开编辑表单并回填当前行 */
function handleEdit(record: Plant) {
  formTitle.value = '编辑工厂表'
  formData.value = { ...record }
  formVisible.value = true
}

/** 编辑当前选中行 */
function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
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
      message.success('更新成功')
    } else {
      await createPlant(formData.value as any)
      message.success('新增成功')
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

/** 单条删除（有 remove 时确认后调用接口） */
async function handleDeleteOne(record: Plant) {
  Modal.confirm({
    title: '确认删除',
    content: '确定要删除该条记录吗？',
    onOk: async () => {
      await deletePlantById((record as any)[entityIdName])
      message.success('删除成功')
      loadData()
    }
  })
}

/** 批量删除（有批量删除接口时确认后调用） */
async function handleDelete() {
  if (selectedRows.value.length === 0) return
  Modal.confirm({
    title: '确认批量删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 条记录吗？`,
    onOk: async () => {
      const ids = selectedRows.value.map((r: any) => r[entityIdName]).filter(Boolean)
      await deletePlantBatch(ids)
      message.success('删除成功')
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
    plantShortName: '',
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
