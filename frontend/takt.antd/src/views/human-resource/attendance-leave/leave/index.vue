<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/leave -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：请假管理标准列表页。分页列表、关键字检索、高级查询（员工/请假类型字典/请假状态）、新增与编辑弹窗、克隆、单条与批量删除、Excel 导入与导出（工作表名与 TaktLeave 实体命名一致）、列显示设置、表格排序占位与列宽拖拽；列表「请假类型」「请假状态」列与用户视图 `@/views/identity/user` 一致，使用 `TaktSingleTable` 的 `#bodyCell` + `TaktDictTag`（`sys_leave_category` / `hr_leave_status`，与 TaktDictTypeSeedData 一致）。高级查询仍为 `TaktSelect`。权限字面值与 TaktLeavesController 声明一致。 -->
<!-- 创建时间：2026-04-14 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-leave">
    <!-- 顶部：关键字检索（entity.leave.keyword） -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.leave.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：RBAC 与 TaktLeavesController 权限码一致 -->
    <TaktToolsBar
      create-permission="humanresource:attendanceleave:leave:create"
      update-permission="humanresource:attendanceleave:leave:update"
      delete-permission="humanresource:attendanceleave:leave:delete"
      import-permission="humanresource:attendanceleave:leave:import"
      template-permission="humanresource:attendanceleave:leave:template"
      export-permission="humanresource:attendanceleave:leave:export"
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

    <!-- 主表：leaveType / leaveStatus 与用户页 userStatus、userType 相同，bodyCell + TaktDictTag -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getLeaveId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'leaveType'">
          <TaktDictTag
            :value="getLeaveField(record, 'leaveType')"
            dict-type="sys_leave_category"
          />
        </template>
        <template v-else-if="column.key === 'leaveStatus'">
          <TaktDictTag
            :value="getLeaveField(record, 'leaveStatus')"
            dict-type="hr_leave_status"
          />
        </template>
      </template>
    </TaktSingleTable>

    <!-- 底部分页 -->
    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />

    <!-- 新增 / 编辑 / 克隆：LeaveForm，宽度 50% -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <LeaveForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询：条件并入列表请求 TaktLeaveQueryDto -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.leave.employeeid')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.leave.leavetype')">
        <TaktSelect
          v-model="advancedQueryForm.leaveType"
          dict-type="sys_leave_category"
          placeholder="请选择"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.leave.leavestatus')">
        <TaktSelect
          v-model="advancedQueryForm.leaveStatus"
          dict-type="hr_leave_status"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.leave.leavestatus') })"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入：sheet / fileBase 与 taktExcelEntityNames('TaktLeave') 一致 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.leave._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="leaveExcelNames.sheet"
        :template-file-name="leaveExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:leave:template"
        import-permission="humanresource:attendanceleave:leave:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.leave._self') })"
        :upload-text="t('common.action.import.uploadText')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <!-- 列设置：与 mergeDefaultColumns 联动 -->
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
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import LeaveForm from './components/leave-form.vue'
import TaktSelect from '@/components/business/takt-select/index.vue'
import {
  getLeaveList,
  createLeave,
  updateLeave,
  deleteLeaveById,
  deleteLeaveBatch,
  getLeaveTemplate,
  importLeaveData,
  exportLeaveData
} from '@/api/human-resource/attendance-leave/leave'
import type { Leave, LeaveCreate, LeaveQuery, LeaveUpdate } from '@/types/human-resource/attendance-leave/leave'
import type { TaktPagedResult } from '@/types/common'
import { logger } from '@/utils/logger'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine, RiFileCopyLine } from '@remixicon/vue'

/**
 * 国际化文案函数（`vue-i18n`），供模板与脚本中的 `t('...')` 使用。
 */
const { t } = useI18n()

/**
 * 与后端 Excel 导入导出约定一致的工作表英文名与文件名前缀。
 * 对应实体类名 `TaktLeave`。
 */
const leaveExcelNames = taktExcelEntityNames('TaktLeave')

/**
 * 表格列单项类型，取自 Ant Design Vue `TableColumnsType` 的元素类型。
 * 用于列宽拖拽回调与列键解析（与 `TaktColumnDrawer` 联动）。
 */
type LeaveTableColumn = TableColumnsType[number]

/**
 * `a-table` 的 `change` 事件第三参数中，与排序相关的最小可用字段集合。
 * 本页未把排序传给后端，仅在有 `order` 时写调试日志。
 */
interface TableSorterLike {
  /**
   * 当前排序列标识，对应列配置上的 `dataIndex` 或 `key`。
   */
  readonly field?: string | string[]

  /**
   * 排序方向：`ascend` 升序、`descend` 降序；未排序时常为 `null` 或缺省。
   */
  readonly order?: 'ascend' | 'descend' | null
}

/**
 * 从 `catch` 的未知错误中取出可读消息。
 *
 * @param {unknown} err - 捕获值
 * @returns {string | undefined} 无可用消息时返回 `undefined`
 */
function getErrorMessage(err: unknown): string | undefined {
  if (err instanceof Error) return err.message
  if (typeof err === 'object' && err !== null && 'message' in err) {
    const m = (err as { message?: unknown }).message
    return typeof m === 'string' ? m : undefined
  }
  return undefined
}

/**
 * 列配置的稳定键（与 `TaktColumnDrawer` 勾选键一致）。
 *
 * @param {LeaveTableColumn} col - 列定义
 * @returns {string} 无可用键时返回空串
 */
function getLeaveColumnKey(col: LeaveTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key
  const dataIndex = c.dataIndex
  const title = c.title
  return key || dataIndex || title ? String(key ?? dataIndex ?? title) : ''
}

/** 顶部查询栏绑定的关键字，映射列表请求 `LeaveQuery.keyWords`。 */
const queryKeyword = ref('')

/** 列表请求、删除、导出等异步操作进行中的互斥标志。 */
const loading = ref(false)

/** 当前页表格数据源，对应 `getLeaveList` 返回的 `data` 数组。 */
const dataSource = ref<Leave[]>([])

/** 分页当前页码（从 1 开始），绑定 `TaktPagination` 与 `LeaveQuery.pageIndex`。 */
const currentPage = ref(1)

/** 每页条数，绑定 `LeaveQuery.pageSize`。 */
const pageSize = ref(20)

/** 总记录数，来自 `TaktPagedResult<Leave>.total`。 */
const total = ref(0)

/**
 * 当前「单行」语义下的选中行：仅在勾选一行的场景由 `rowSelection` / 点击行逻辑设为该行；
 * 多选或无选时为 `null`。用于工具栏「编辑」依赖的 `selectedRow`。
 */
const selectedRow = ref<Leave | null>(null)

/** 表格多选当前已选中的行集合。 */
const selectedRows = ref<Leave[]>([])

/** 与 `selectedRows` 对应的 `row-key` 列表（值为 `getLeaveId` 的字符串结果）。 */
const selectedRowKeys = ref<(string | number)[]>([])

/** 新建/编辑/克隆弹窗是否可见。 */
const formVisible = ref(false)

/** 弹窗标题文案（创建、编辑、克隆前缀 + 实体名）。 */
const formTitle = ref('')

/** 传入 `LeaveForm` 的初始数据；空对象表示新建。 */
const formData = ref<Partial<Leave>>({})

/** 弹窗确定提交时表单校验与 API 请求进行中的标志。 */
const formLoading = ref(false)

/**
 * 子组件 `LeaveForm` 通过 `defineExpose` 暴露的实例类型（校验、取数、重置）。
 */
type LeaveFormExposed = InstanceType<typeof LeaveForm>

/** 请假表单组件实例引用，用于 `validate` / `getValues` / `resetFields`。 */
const formRef = ref<LeaveFormExposed | null>(null)

/** `TaktSingleTable` 实例引用，供 `defineExpose` 给父级或调试使用。 */
const tableRef = ref()

/** 高级查询抽屉是否打开。 */
const advancedQueryVisible = ref(false)

/**
 * 高级查询表单模型，提交时合并进 `LeaveQuery`（员工、类型、状态）。
 */
const advancedQueryForm = ref<{ employeeId?: string; leaveType?: string; leaveStatus?: string | number }>({
  employeeId: '',
  leaveType: undefined,
  leaveStatus: undefined
})

/** Excel 导入弹窗是否可见。 */
const importVisible = ref(false)

/** 列显示设置抽屉是否可见。 */
const columnSettingVisible = ref(false)

/**
 * `TaktColumnDrawer` 勾选的列键集合；空数组表示不筛选、展示全部合并列。
 */
const visibleColumnKeys = ref<string[]>([])

/** 挂载后拉取第一页列表数据。 */
onMounted(() => {
  loadData()
})

/**
 * 从表格行解析主键字符串（`row-key`、删除、导出）。
 *
 * @param {Leave} record - 列表行
 * @returns {string} 优先 `leaveId`，否则兼容历史 JSON 中的 `id`
 */
const getLeaveId = (record: Leave): string => {
  if (record?.leaveId != null && String(record.leaveId) !== '') return String(record.leaveId)
  const legacyId = getLeaveField(record, 'id')
  if (legacyId != null && legacyId !== '') return String(legacyId)
  return ''
}

/**
 * 读取行字段（与用户页 `getUserField` 一致，供 `#bodyCell`、`TaktDictTag` 的 `:value` 使用）。
 *
 * @param {Leave} record - 列表行
 * @param {string} field - 字段名
 * @returns {any} 原始值
 */
const getLeaveField = (record: Leave, field: string): any =>
  (record as unknown as Record<string, unknown>)[field]

/**
 * 高级查询中「请假状态」绑定值（字典可能为 string）转为 `LeaveQuery.leaveStatus`（number）。
 *
 * @param {string | number | undefined | null} value - 表单或字典绑定值
 * @returns {number | undefined} 无效或空时 `undefined`
 */
function coerceAdvancedLeaveStatus(value: string | number | undefined | null): number | undefined {
  if (value === undefined || value === null || value === '') return undefined
  if (typeof value === 'number' && Number.isFinite(value)) return value
  const n = Number(value)
  return Number.isFinite(n) ? n : undefined
}

/**
 * 列表主列定义（未合并审计列前）；含 ID 列、`CreateActionColumn` 操作列。
 */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'leaveId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: Leave }) =>
      getLeaveField(record, 'leaveId') ?? getLeaveField(record, 'id') ?? ''
  },
  {
    title: t('entity.leave.employeeid'),
    dataIndex: 'employeeId',
    key: 'employeeId',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.leave.leavetype'),
    dataIndex: 'leaveType',
    key: 'leaveType',
    width: 100,
    resizable: true
  },
  {
    title: t('entity.leave.startdate'),
    dataIndex: 'startDate',
    key: 'startDate',
    width: 120,
    ellipsis: true
  },
  {
    title: t('entity.leave.enddate'),
    dataIndex: 'endDate',
    key: 'endDate',
    width: 120,
    ellipsis: true
  },
  {
    title: t('entity.leave.reason'),
    dataIndex: 'reason',
    key: 'reason',
    width: 180,
    ellipsis: true
  },
  {
    title: t('entity.leave.proofattachments'),
    dataIndex: 'proofAttachmentsJson',
    key: 'proofAttachmentsJson',
    width: 180,
    ellipsis: true
  },
  {
    title: t('entity.leave.leavestatus'),
    dataIndex: 'leaveStatus',
    key: 'leaveStatus',
    width: 100
  },
  {
    title: t('entity.leave.flowinstanceid'),
    dataIndex: 'flowInstanceId',
    key: 'flowInstanceId',
    width: 140,
    ellipsis: true
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:leave:update',
        onClick: (record: Leave) => handleEdit(record)
      },
      {
        key: 'clone',
        label: t('common.button.clone'),
        shape: 'plain',
        icon: RiFileCopyLine,
        permission: 'humanresource:attendanceleave:leave:create',
        onClick: (record: Leave) => handleClone(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:leave:delete',
        onClick: (record: Leave) => handleDeleteOne(record)
      }
    ]
  })
])

/**
 * 在 `columns` 基础上合并默认列（如审计字段），与 `mergeDefaultColumns` 工具一致。
 */
const mergedColumns = computed((): TableColumnsType => mergeDefaultColumns(columns.value, t, true))

/**
 * 最终传给 `TaktSingleTable` 的列：若 `visibleColumnKeys` 非空则按勾选过滤 `mergedColumns`，否则用原始 `columns`。
 */
const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map((k) => String(k)))
  return merged.filter((col: LeaveTableColumn) => {
    const colKey = getLeaveColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

/**
 * Ant Design Vue 表格 `row-selection` 配置：维护 `selectedRowKeys` / `selectedRows` / `selectedRow`。
 */
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Leave[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Leave, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getLeaveId(selectedRow.value) === getLeaveId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Leave[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

/**
 * 生成 `TaktSingleTable` 的 `custom-row`：点击行时切换该行在 `selectedRowKeys` 中的选中状态，并同步 `selectedRows`。
 *
 * @param {Leave} record - 被点击的行
 * @returns {{ onClick: () => void }} Ant Design Vue `customRow` 形状
 */
const onClickRow = (record: Leave) => ({
  onClick: () => {
    const key = getLeaveId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getLeaveId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

/**
 * 拉取请假分页列表（关键字 + 高级查询条件）。
 *
 * @returns {Promise<void>}
 */
const loadData = async () => {
  try {
    loading.value = true
    const params: LeaveQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.employeeId) params.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.leaveType) params.leaveType = advancedQueryForm.value.leaveType
    const advLeaveStatus = coerceAdvancedLeaveStatus(advancedQueryForm.value.leaveStatus)
    if (advLeaveStatus !== undefined) params.leaveStatus = advLeaveStatus

    const response: TaktPagedResult<Leave> = await getLeaveList(params)
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[Leave] 加载数据失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

/**
 * 查询栏搜索：重置到第一页并请求列表。
 */
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

/**
 * 查询栏重置：清空关键字与高级查询条件，回到第一页并重新加载。
 */
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = { employeeId: '', leaveType: undefined, leaveStatus: undefined }
  currentPage.value = 1
  loadData()
}

/**
 * 表格变更（分页/筛选/排序）；当前仅记录排序便于后续接后端排序。
 *
 * @param _pagination - 未使用
 * @param _filters - 未使用
 * @param {TableSorterLike} sorter - 排序状态
 */
const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[Leave] 排序:', sorter.field, sorter.order)
}

/**
 * 底部分页页码或每页条数变化（`TaktPagination` `@change`）。
 *
 * @param {number} page - 目标页码
 * @param {number} size - 每页条数
 */
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

/**
 * 仅每页条数变化时：回到第一页再加载（`show-size-change`）。
 *
 * @param _current - 当前页（未使用）
 * @param {number} size - 新的每页条数
 */
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

/**
 * 列宽拖拽结束，写回 `columns`。
 *
 * @param {number} w - 新宽度（px）
 * @param {LeaveTableColumn} col - 当前列
 */
const handleResizeColumn = (w: number, col: LeaveTableColumn) => {
  const column = columns.value.find((c: LeaveTableColumn) => {
    const colKey = getLeaveColumnKey(col)
    const cKey = getLeaveColumnKey(c)
    return colKey && cKey && colKey === cKey
  })
  if (column) (column as LeaveTableColumn & { width?: number }).width = w
}

/**
 * 工具栏「新建」：打开弹窗并清空表单数据。
 */
const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.leave._self')
  formData.value = {}
  formVisible.value = true
}

/**
 * 克隆：复制业务字段，去掉主键、流程与状态及最近审计时间戳。
 *
 * @param {Leave} record - 被克隆行
 */
const handleClone = (record: Leave) => {
  formTitle.value = t('common.button.clone') + t('entity.leave._self')
  const { leaveId: _leaveId, flowInstanceId: _flow, leaveStatus: _status, createdAt: _ca, updatedAt: _ua, ...rest } =
    record
  formData.value = { ...rest }
  formVisible.value = true
}

/**
 * 打开编辑弹窗并填充指定行数据。
 *
 * @param {Leave} record - 要编辑的行
 */
const handleEdit = (record: Leave) => {
  formTitle.value = t('common.button.edit') + t('entity.leave._self')
  formData.value = { ...record }
  formVisible.value = true
}

/**
 * 工具栏「编辑」：依赖单行选中 `selectedRow`，无选中时提示。
 */
const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.leave._self') }))
}

/**
 * 行内「删除」：确认后调用 `deleteLeaveById`。
 *
 * @param {Leave} record - 待删除行
 */
const handleDeleteOne = (record: Leave) => {
  const reason = getLeaveField(record, 'reason')
  const name =
    (typeof reason === 'string' && reason.trim() !== '' ? reason : null) ??
    (typeof reason === 'number' ? String(reason) : null) ??
    getLeaveId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.leave._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteLeaveById(getLeaveId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.leave._self') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.leave._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 工具栏「批量删除」：无选中提示；单条走 `deleteLeaveById`，多条走 `deleteLeaveBatch`。
 */
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.leave._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: t('entity.leave._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteLeaveById(getLeaveId(selectedRows.value[0]))
        } else {
          await deleteLeaveBatch(selectedRows.value.map((r) => getLeaveId(r)))
        }
        message.success(t('common.msg.deleteSuccess', { target: t('entity.leave._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.leave._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 弹窗确定：校验表单后调用 `createLeave` 或 `updateLeave`；捕获校验错误（`errorFields`）、证明上传中（`proof-uploading` / `isProofUploading`）时不提示通用失败。
 *
 * @returns {Promise<void>}
 */
const handleFormSubmit = async () => {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const formValues = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.leaveId) {
      const id = formData.value.leaveId
      const payload: LeaveUpdate = { ...(formValues as LeaveCreate), leaveId: id }
      await updateLeave(id, payload)
      message.success(t('common.msg.updateSuccess', { target: t('entity.leave._self') }))
    } else {
      await createLeave(formValues as LeaveCreate)
      message.success(t('common.msg.createSuccess', { target: t('entity.leave._self') }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    if (getErrorMessage(error) === 'proof-uploading') return
    if (typeof error === 'object' && error !== null && 'isProofUploading' in error) {
      const u = (error as { isProofUploading?: unknown }).isProofUploading
      if (u === true) return
    }
    message.error(getErrorMessage(error) || t('common.msg.operateFail', { action: t('common.action.operation') }))
  } finally {
    formLoading.value = false
  }
}

/**
 * 弹窗取消：关闭并清空表单与校验状态。
 */
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

/**
 * 工具栏「导入」：打开导入弹窗。
 */
const handleImport = () => {
  importVisible.value = true
}

/**
 * 委托给 `getLeaveTemplate`，供 `TaktImportFile` 下载模板。
 *
 * @param {string} [sheetName] - 工作表名
 * @param {string} [fileName] - 模板文件名
 * @returns {Promise<import('@/api/request').BlobDownloadWithMeta>} 与 `getLeaveTemplate` 一致
 */
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getLeaveTemplate(sheetName, fileName)
}

/**
 * 委托给 `importLeaveData`，由 `TaktImportFile` 在上传后调用。
 *
 * @param {File} file - 上传的 Excel 文件
 * @param {string} [sheetName] - 工作表名
 * @returns {Promise<{ success: number; fail: number; errors: string[] }>} 导入统计结果
 */
const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importLeaveData(file, sheetName)
}

/**
 * 导入成功回调：刷新列表；若全部成功则延时关闭导入弹窗。
 *
 * @param {{ success: number; fail: number; errors: string[] }} result - 导入结果
 */
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}

/**
 * 关闭导入弹窗。
 */
const handleImportCancel = () => {
  importVisible.value = false
}

/**
 * 按当前关键字与高级查询条件导出 Excel；`LeaveQuery` 中 `pageIndex`/`pageSize` 为满足类型与尽量覆盖全量导出的大页占位。
 *
 * @returns {Promise<void>}
 */
const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: LeaveQuery = {
      pageIndex: 1,
      pageSize: 100000
    }
    if (queryKeyword.value) queryParams.keyWords = queryKeyword.value
    if (advancedQueryForm.value.employeeId) queryParams.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.leaveType) queryParams.leaveType = advancedQueryForm.value.leaveType
    const advLeaveStatus = coerceAdvancedLeaveStatus(advancedQueryForm.value.leaveStatus)
    if (advLeaveStatus !== undefined) queryParams.leaveStatus = advLeaveStatus

    const blob = await exportLeaveData(
      queryParams,
      leaveExcelNames.sheet,
      leaveExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob as Blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${leaveExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.leave._self') }))
  } catch (error: unknown) {
    logger.error('[Leave] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportFail', { target: t('entity.leave._self') }))
  } finally {
    loading.value = false
  }
}

/**
 * 工具栏「高级查询」：打开抽屉。
 */
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

/**
 * 高级查询抽屉确定：回到第一页、请求列表并关闭抽屉。
 */
const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

/**
 * 高级查询抽屉重置：恢复表单默认值（不自动请求，由用户再次提交或关闭后自行刷新）。
 */
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { employeeId: '', leaveType: undefined, leaveStatus: undefined }
}

/**
 * 工具栏「列设置」：打开列显示抽屉。
 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

/**
 * `TaktColumnDrawer` 勾选变更：同步到 `visibleColumnKeys`（字符串键）。
 *
 * @param {(string | number)[]} keys - 勾选的列键
 */
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map((k) => String(k))
}

/**
 * 列设置重置：清空勾选，表格恢复展示全部列。
 */
const handleColumnSettingReset = () => {
  visibleColumnKeys.value = []
}

/**
 * 工具栏「刷新」：按当前分页与条件重新加载列表。
 */
const handleRefresh = () => {
  loadData()
}

/** 供父级或调试使用的表格 ref */
defineExpose({ tableRef })
</script>

<style scoped lang="less">
/* 根容器：内边距 + 纵向 flex，避免表格高度塌陷 */
.humanresource-attendance-leave-leave {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
