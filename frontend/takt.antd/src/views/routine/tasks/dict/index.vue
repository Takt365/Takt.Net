<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/dict/type -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：字典类型管理页面，包含字典类型列表、查询、新增、编辑、删除等功能（主子表） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-dict-type">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="
        t('common.form.placeholder.search', {
          keyword:
            t('entity.dicttype.code') +
            t('common.action.or') +
            t('entity.dicttype.name')
        })
      "
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="routine:tasks:dict:create"
      update-permission="routine:tasks:dict:update"
      delete-permission="routine:tasks:dict:delete"
      export-permission="routine:tasks:dict:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :update-disabled="updateDisabled"
      :delete-disabled="deleteDisabled"
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
      :row-key="getDictTypeId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      :expanded-row-keys="expandedRowKeys"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
      @expand="handleExpand"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'dictTypeCode'">
          <a
            style="color: #1890ff; cursor: pointer"
            @click.stop="handleOpenDictDataWindow(record)"
          >
            {{ record.dictTypeCode }}
          </a>
        </template>
        <template v-else-if="column.key === 'dictTypeStatus'">
          <a-switch
            :checked="record.dictTypeStatus === 0"
            :checked-children="t('common.button.enable')"
            :un-checked-children="t('common.button.disable')"
            @change="(checked: any) => handleStatusChange(record, !!checked)"
          />
        </template>
        <template v-else-if="column.key === 'dataSource'">
          <TaktDictTag
            :value="getDictTypeField(record, 'dataSource')"
            dict-type="sys_data_source"
          />
        </template>
        <template v-else-if="column.key === 'isBuiltIn'">
          <TaktDictTag
            :value="getDictTypeField(record, 'isBuiltIn')"
            dict-type="sys_yes_no"
          />
        </template>
      </template>
      <!-- 展开行渲染 -->
      <template #expandedRowRender="{ record }">
        <div style="padding: 16px">
          <a-table
            v-if="(dataSource.find(item => item.dictTypeId === record.dictTypeId)?.dictDataList || []).length > 0"
            :columns="dictDataColumns"
            :data-source="dataSource.find(item => item.dictTypeId === record.dictTypeId)?.dictDataList || []"
            :row-key="(r: DictData) => r.dictDataId || ''"
            :pagination="false"
            size="small"
            bordered
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
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <DictTypeForm
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
      <a-form-item :label="t('entity.dicttype.code')">
        <a-input v-model:value="advancedQueryForm.dictTypeCode" />
      </a-form-item>
      <a-form-item :label="t('entity.dicttype.name')">
        <a-input v-model:value="advancedQueryForm.dictTypeName" />
      </a-form-item>
      <a-form-item :label="t('entity.dicttype.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.dictTypeStatus"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_status"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.dicttype.status') })"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 列设置抽屉 -->
    <!-- 审计字段统一在 TaktColumnDrawer 中处理 -->
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'id'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />

    <!-- 字典数据子表窗口 -->
    <DictDataWindow
      v-model:visible="dictDataWindowVisible"
      :dict-type="currentDictType"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { FilterValue } from 'ant-design-vue/es/table/interface'
import { useI18n } from 'vue-i18n'
import { mergeDefaultColumns } from '@/utils/table-columns'
import DictTypeForm from './components/dict-type-form.vue'
import DictDataWindow from './components/dict-data-window.vue'
import * as dictTypeApi from '@/api/routine/tasks/dict/dicttype'
import * as dictDataApi from '@/api/routine/tasks/dict/dictdata'
import type { DictType, DictTypeQuery, DictTypeCreate, DictTypeUpdate } from '@/types/routine/tasks/dict/dict-type'
import type { DictData } from '@/types/routine/tasks/dict/dict-data'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

/** 与 `identity/user/index.vue` 的 `getUserField` 一致：供表格单元格（如 TaktDictTag）安全取行字段 */
const getDictTypeField = (record: unknown, field: string): any =>
  (record as Record<string, unknown> | null | undefined)?.[field]

const getDictTypeId = (record: unknown): string =>
  String(getDictTypeField(record, 'dictTypeId') ?? '')

/** 高级查询中字典状态（下拉 `extLabel` 可能为 string）转为 `DictTypeQuery.dictTypeStatus`。 */
function coerceAdvancedDictTypeStatus(value: string | number | undefined): number | undefined {
  if (value === undefined || value === null || value === '') return undefined
  if (typeof value === 'number' && Number.isFinite(value)) return value
  const n = Number(value)
  return Number.isFinite(n) ? n : undefined
}

/** 与 `TaktSingleTable` 的 `@change` 签名一致（见 `takt-single-table/index.vue`） */
type TaktTableChangeSorter = {
  field?: string | number | readonly (string | number)[]
  order?: string
}
type TaktTableChangeFilters = Record<string, FilterValue | null>
type TaktTableChangePagination = { current?: number; pageSize?: number; total?: number }

/** 与 `TaktSingleTable` 的 `@resize-column` 第二参数一致（`ResizableColumn`） */
type TaktResizeColumn = { width?: string | number } & Record<string, unknown>

// ========================================
// 数据定义
// ========================================

const loading = ref(false)
const queryKeyword = ref('')
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const dataSource = ref<DictType[]>([])

// 行选择
const selectedRowKeys = ref<(string | number)[]>([])
const selectedRows = ref<DictType[]>([])
const selectedRow = ref<DictType | null>(null)

const updateDisabled = computed(() => selectedRows.value.length !== 1)
const deleteDisabled = computed(() => selectedRows.value.length === 0)

// 表单
const formVisible = ref(false)
const formTitle = ref('')
const formLoading = ref(false)
const formData = ref<DictType | null>(null)
const formRef = ref<InstanceType<typeof DictTypeForm> | null>(null)

// 高级查询（字段与列表查询一致；`loadData` / `handleExport` 合并为 `DictTypeQuery`）
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  dictTypeCode: string
  dictTypeName: string
  dictTypeStatus?: string | number
}>({
  dictTypeCode: '',
  dictTypeName: ''
})

// 列设置
const visibleColumnKeys = ref<string[]>([])
const columnSettingVisible = ref(false)

// 展开行
const expandedRowKeys = ref<(string | number)[]>([])

// 字典数据子表窗口
const dictDataWindowVisible = ref(false)
const currentDictType = ref<DictType | null>(null)

// 字典数据子表列定义（用于展开行显示，与 DictData 接口字段顺序一致）
const dictDataColumns = computed<TableColumnsType<DictData>>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'dictDataId',
    key: 'dictDataId',
    width: 120
  },
  {
    title: t('entity.dictdata.dicttypeid'),
    dataIndex: 'dictTypeId',
    key: 'dictTypeId',
    width: 120
  },
  {
    title: t('entity.dictdata.dicttypecode'),
    dataIndex: 'dictTypeCode',
    key: 'dictTypeCode',
    width: 150
  },
  {
    title: t('entity.dictdata.dictlabel'),
    dataIndex: 'dictLabel',
    key: 'dictLabel',
    width: 150
  },
  {
    title: t('entity.dictdata.dictl10nkey'),
    dataIndex: 'dictL10nKey',
    key: 'dictL10nKey',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.dictdata.dictvalue'),
    dataIndex: 'dictValue',
    key: 'dictValue',
    width: 150
  },
  {
    title: t('entity.dictdata.cssclass'),
    dataIndex: 'cssClass',
    key: 'cssClass',
    width: 100
  },
  {
    title: t('entity.dictdata.listclass'),
    dataIndex: 'listClass',
    key: 'listClass',
    width: 100
  },
  {
    title: t('entity.dictdata.extlabel'),
    dataIndex: 'extLabel',
    key: 'extLabel',
    width: 150,
    ellipsis: true
  },
  {
    title: t('entity.dictdata.extvalue'),
    dataIndex: 'extValue',
    key: 'extValue',
    width: 150,
    ellipsis: true
  },
  {
    title: t('entity.dictdata.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  }
])

// ========================================
// 列定义
// ========================================

const columns = computed<TableColumnsType<DictType>>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'dictTypeId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: DictType }) =>
      String(getDictTypeField(record, 'dictTypeId') ?? '')
  },
  {
    title: t('entity.dicttype.code'),
    dataIndex: 'dictTypeCode',
    key: 'dictTypeCode',
    width: 150,
    fixed: 'left'
  },
  {
    title: t('entity.dicttype.name'),
    dataIndex: 'dictTypeName',
    key: 'dictTypeName',
    width: 200
  },
  {
    title: t('entity.dicttype.datasource'),
    dataIndex: 'dataSource',
    key: 'dataSource',
    width: 100
  },
  {
    title: t('entity.dicttype.dataconfigid'),
    dataIndex: 'dataConfigId',
    key: 'dataConfigId',
    width: 150,
    ellipsis: true
  },
  {
    title: t('entity.dicttype.sqlscript'),
    dataIndex: 'sqlScript',
    key: 'sqlScript',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.dicttype.isbuiltin'),
    dataIndex: 'isBuiltIn',
    key: 'isBuiltIn',
    width: 100
  },
  {
    title: t('entity.dicttype.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('entity.dicttype.status'),
    dataIndex: 'dictTypeStatus',
    key: 'dictTypeStatus',
    width: 100
  },
  CreateActionColumn<DictType>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:dict:update',
        onClick: (record: DictType) => handleEditOne(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:dict:delete',
        onClick: (record: DictType) => handleDeleteOne(record)
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

// 加载数据
const loadData = async () => {
  try {
    loading.value = true
    const query: DictTypeQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) query.KeyWords = queryKeyword.value
    const adv = advancedQueryForm.value
    if (adv.dictTypeCode) query.dictTypeCode = adv.dictTypeCode
    if (adv.dictTypeName) query.dictTypeName = adv.dictTypeName
    const advStatus = coerceAdvancedDictTypeStatus(adv.dictTypeStatus)
    if (advStatus !== undefined) query.dictTypeStatus = advStatus

    const result = await dictTypeApi.getDictTypeList(query)
    dataSource.value = result.data || []
    total.value = result.total || 0
    
    // 字典数据按需加载（点击展开时加载），不再一次性加载所有数据
  } catch (error) {
    logger.error('[DictType] 加载数据失败', error)
    message.error(t('common.msg.loadtargetfail', { target: t('entity.dicttype._self') }))
  } finally {
    loading.value = false
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
  advancedQueryForm.value = { dictTypeCode: '', dictTypeName: '' }
  currentPage.value = 1
  loadData()
}

// 新增
const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.dicttype._self')
  formData.value = null
  formVisible.value = true
}

// 编辑
const handleUpdate = () => {
  if (!selectedRow.value) {
    message.warning(
      t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: t('entity.dicttype._self') })
    )
    return
  }
  
  formTitle.value = t('common.button.edit') + t('entity.dicttype._self')
  formData.value = { ...selectedRow.value }
  formVisible.value = true
}

// 编辑单条记录（操作列使用）
const handleEditOne = (record: DictType) => {
  selectedRow.value = record
  formTitle.value = t('common.button.edit') + t('entity.dicttype._self')
  formData.value = { ...record }
  formVisible.value = true
}

// 删除
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('entity.dicttype._self') })
    )
    return
  }
  
  const ids = selectedRows.value.map(row => row.dictTypeId).filter(Boolean)
  if (ids.length === 0) {
    message.warning(t('common.msg.entityidrequired', { entity: t('entity.dicttype._self') }))
    return
  }

  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', {
      entity: t('entity.dicttype._self'),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (ids.length === 1) {
          await dictTypeApi.deleteDictTypeById(ids[0]!)
        } else {
          await dictTypeApi.deleteDictTypeBatch(ids as string[])
        }
        message.success(t('common.msg.deletesuccess'))
        await loadData()
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
      } catch (error) {
        logger.error('[DictType] 删除失败', error)
        message.error(t('common.msg.deletefail'))
      } finally {
        loading.value = false
      }
    }
  })
}

// 删除单条记录（操作列使用）
const handleDeleteOne = (record: DictType) => {
  if (!record.dictTypeId) {
    message.warning(t('common.msg.entityidrequired', { entity: t('entity.dicttype._self') }))
    return
  }

  const name = record.dictTypeCode || t('common.action.thistarget', { target: t('entity.dicttype._self') })
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.dicttype._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await dictTypeApi.deleteDictTypeById(record.dictTypeId)
        message.success(t('common.msg.deletesuccess'))
        await loadData()
        if (selectedRow.value?.dictTypeId === record.dictTypeId) {
          selectedRow.value = null
        }
        selectedRowKeys.value = selectedRowKeys.value.filter(k => k !== record.dictTypeId)
        selectedRows.value = selectedRows.value.filter(r => r.dictTypeId !== record.dictTypeId)
      } catch (error) {
        logger.error('[DictType] 删除失败', error)
        message.error(t('common.msg.deletefail'))
      } finally {
        loading.value = false
      }
    }
  })
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    const query: DictTypeQuery = {
      pageIndex: 1,
      pageSize: 10000
    }
    if (queryKeyword.value) query.KeyWords = queryKeyword.value
    const adv = advancedQueryForm.value
    if (adv.dictTypeCode) query.dictTypeCode = adv.dictTypeCode
    if (adv.dictTypeName) query.dictTypeName = adv.dictTypeName
    const advStatus = coerceAdvancedDictTypeStatus(adv.dictTypeStatus)
    if (advStatus !== undefined) query.dictTypeStatus = advStatus

    const blob = await dictTypeApi.exportDictTypeData(query, undefined, t('entity.dicttype._self'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.dicttype._self')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
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
    logger.error('[DictType] 导出失败', error)
    message.error(t('common.msg.exportfail'))
  } finally {
    loading.value = false
  }
}

// 状态切换
const handleStatusChange = async (record: DictType, checked: boolean) => {
  try {
    await dictTypeApi.updateDictTypeStatus({
      dictTypeId: record.dictTypeId,
      dictTypeStatus: checked ? 0 : 1
    })
    message.success(t('common.msg.updatesuccess'))
    await loadData()
  } catch (error) {
    logger.error('[DictType] 状态更新失败', error)
    message.error(t('common.msg.operatefail'))
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
  advancedQueryForm.value = { dictTypeCode: '', dictTypeName: '' }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
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
    if ('dictTypeId' in formData && formData.dictTypeId) {
      // 更新
      await dictTypeApi.updateDictType(formData.dictTypeId, formData as DictTypeUpdate)
      message.success(t('common.msg.updatesuccess'))
    } else {
      // 新增
      await dictTypeApi.createDictType(formData as DictTypeCreate)
      message.success(t('common.msg.createsuccess'))
    }
    
    formVisible.value = false
    await loadData()
  } catch (error: any) {
    if (error?.errorFields) {
      message.warning(t('common.msg.operatefail'))
    } else {
      logger.error('[DictType] 保存失败', error)
      message.error(t('common.msg.operatefail'))
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

// 表格变化（分页由 `TaktPagination` 处理；签名须与 `TaktSingleTable` 的 `change` 一致）
const handleTableChange = (
  _pagination: TaktTableChangePagination,
  _filters: TaktTableChangeFilters,
  sorter: TaktTableChangeSorter | TaktTableChangeSorter[]
) => {
  const one = Array.isArray(sorter) ? sorter[0] : sorter
  if (one?.order) logger.debug('[DictType] 排序:', one.field, one.order)
}

// 列宽拖拽（与 `TaktSingleTable` 的 `resize-column` 及 `identity/user` 页一致：`col`/`c` 用 `any` 避免列 `key: Key` 与窄对象在 EOPT 下不兼容）
const handleResizeColumn = (w: number, col: TaktResizeColumn) => {
  const colAny = col as Record<string, unknown>
  const colKey = colAny.key ?? colAny.dataIndex ?? colAny.title
  const column = columns.value.find((c: any) => {
    const cKey = c.key ?? c.dataIndex ?? c.title
    return colKey != null && cKey != null && String(colKey) === String(cKey)
  }) as { width?: number } | undefined
  if (column) column.width = w
}

// 行选择配置
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: DictType[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: DictType, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (selectedRow.value?.dictTypeId === record?.dictTypeId) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: DictType[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
    } else {
      selectedRow.value = null
    }
  }
}))

// 行点击处理 - 切换展开状态（手风琴模式：只允许一个展开，保留行选择功能）
const onClickRow = (record: DictType) => {
  return {
    onClick: (event: MouseEvent) => {
      // 如果点击的是复选框或操作列，不处理展开
      const target = event.target as HTMLElement
      if (target.closest('.ant-checkbox-wrapper') || target.closest('.takt-action-column')) {
        return
      }
      
      const key = record.dictTypeId || ''
      // 手风琴模式：切换展开状态
      if (expandedRowKeys.value.includes(key)) {
        // 如果当前行已展开，则收起
        expandedRowKeys.value = []
      } else {
        // 如果当前行未展开，先关闭其他已展开的行，再展开当前行
        expandedRowKeys.value = []
        
        // 确保字典数据已加载，等待加载完成后再展开
        const item = dataSource.value.find(item => item.dictTypeId === record.dictTypeId)
        if (item && (!item.dictDataList || item.dictDataList.length === 0)) {
          // 先加载数据，等待完成后再展开
          loadDictData(record).then(() => {
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
const handleExpand = async (expanded: boolean, record: DictType) => {
  if (expanded && record.dictTypeId) {
    // 手风琴模式：先关闭其他已展开的行
    const currentKey = record.dictTypeId || ''
    if (expandedRowKeys.value.length > 0 && expandedRowKeys.value[0] !== currentKey) {
      expandedRowKeys.value = []
    }
    
    // 检查 dataSource 中是否有数据，如果没有则加载
    const item = dataSource.value.find(item => item.dictTypeId === record.dictTypeId)
    if (item && (!item.dictDataList || item.dictDataList.length === 0)) {
      await loadDictData(record)
    }
    
    // 设置当前行为唯一展开的行
    expandedRowKeys.value = [currentKey]
  } else {
    // 收起时清空
    expandedRowKeys.value = []
  }
}

// 加载字典数据 - 根据 dictTypeId 动态获取
const loadDictData = async (record: DictType) => {
  if (!record.dictTypeId) return
  
  try {
    // 使用 dictDataApi.getList 根据 dictTypeId 查询字典数据（dictTypeId 是唯一标识）
    const result = await dictDataApi.getDictDataList({
      pageIndex: 1,
      pageSize: 10000, // 获取所有数据
      dictTypeId: record.dictTypeId
    })
    
    if (result && result.data) {
      // 更新 dataSource 中对应的记录，确保响应式更新
      const index = dataSource.value.findIndex(item => item.dictTypeId === record.dictTypeId)
      if (index !== -1) {
        const row = dataSource.value[index]
        // 仅替换 dictDataList；展开推断在 EOPT 下会变「部分字段可选」，与 DictType 必选冲突，故断言为 DictType
        dataSource.value[index] = { ...row, dictDataList: result.data } as DictType
      }
      return result.data
    }
    return []
  } catch (error) {
    logger.error('[DictType] 加载字典数据失败', error)
    message.error(t('common.msg.loadtargetfail', { target: t('entity.dictdata._self') }))
    return []
  }
}

// 打开字典数据子表窗口
const handleOpenDictDataWindow = async (record: DictType) => {
  if (!record.dictTypeId) {
    message.warning(t('common.msg.entityidrequired', { entity: t('entity.dicttype._self') }))
    return
  }
  
  currentDictType.value = record
  dictDataWindowVisible.value = true
}




// 分页变化
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

// ========================================
// 生命周期
// ========================================

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.routine-dict-type {
  padding: 16px;
}
</style>
