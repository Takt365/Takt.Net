<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/overtime/components -->
<!-- 文件名称：overtime-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：加班维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致。字典 hr_overtime_status。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    layout="horizontal"
    label-align="right"
  >
    <a-tabs v-model:active-key="activeTab">
      <a-tab-pane
        key="basic"
        :tab="t('common.page.form.tabs.basicinfo')"
        force-render
      >
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.overtime.employeeid')"
                name="employeeId"
              >
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.page.form.placeholder.required', { field: t('entity.overtime.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.overtime.overtimedate')"
                name="overtimeDate"
              >
                <a-date-picker
                  v-model:value="formState.overtimeDate"
                  value-format="YYYY-MM-DD"
                  :placeholder="t('common.page.form.placeholder.select', { field: t('entity.overtime.overtimedate') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.overtime.plannedhours')"
                name="plannedHours"
              >
                <a-input-number
                  v-model:value="formState.plannedHours"
                  :min="0"
                  :step="0.5"
                  :placeholder="t('common.page.form.placeholder.required', { field: t('entity.overtime.plannedhours') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.overtime.actualhours')"
                name="actualHours"
              >
                <a-input-number
                  v-model:value="formState.actualHours"
                  :min="0"
                  :step="0.5"
                  allow-clear
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.overtime.overtimestatus')"
                name="overtimeStatus"
              >
                <TaktSelect
                  v-model:value="formState.overtimeStatus"
                  dict-type="hr_overtime_status"
                  :placeholder="t('common.page.form.placeholder.select', { field: t('entity.overtime.overtimestatus') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.overtime.reason')"
                name="reason"
              >
                <a-input
                  v-model:value="formState.reason"
                  :placeholder="t('common.page.form.placeholder.required', { field: t('entity.overtime.reason') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('common.page.entity.remark')"
                name="remark"
              >
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('common.page.form.placeholder.required', { field: t('common.page.entity.remark') })"
                  :rows="4"
                  show-count
                  :maxlength="500"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </div>
      </a-tab-pane>
    </a-tabs>
  </a-form>
</template>

<script setup lang="ts">
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Overtime, OvertimeCreate } from '@/types/human-resource/attendance-leave/overtime'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<Overtime>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

function createEmptyOvertimeForm(): OvertimeCreate {
  return {
    employeeId: '',
    overtimeDate: '',
    plannedHours: 0,
    actualHours: undefined,
    reason: '',
    overtimeStatus: 0,
    remark: ''
  }
}

const formState = reactive<OvertimeCreate>(createEmptyOvertimeForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.page.form.placeholder.required', { field: t('entity.overtime.employeeid') }), trigger: 'blur' }
  ],
  overtimeDate: [
    { required: true, message: t('common.page.form.placeholder.required', { field: t('entity.overtime.overtimedate') }), trigger: 'change' }
  ],
  plannedHours: [
    { required: true, message: t('common.page.form.placeholder.required', { field: t('entity.overtime.plannedhours') }), trigger: 'change' }
  ],
  reason: [{ required: true, message: t('common.page.form.placeholder.required', { field: t('entity.overtime.reason') }), trigger: 'blur' }],
  overtimeStatus: [
    { required: true, message: t('common.page.form.placeholder.select', { field: t('entity.overtime.overtimestatus') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        employeeId: String(newData.employeeId ?? ''),
        overtimeDate: newData.overtimeDate ? String(newData.overtimeDate).slice(0, 10) : '',
        plannedHours: Number(newData.plannedHours ?? 0),
        actualHours: newData.actualHours != null ? Number(newData.actualHours) : undefined,
        reason: String(newData.reason ?? ''),
        overtimeStatus: newData.overtimeStatus != null ? Number(newData.overtimeStatus) : 0,
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyOvertimeForm())
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): OvertimeCreate & { overtimeId?: string } => {
  const payload: OvertimeCreate & { overtimeId?: string } = {
    employeeId: formState.employeeId,
    overtimeDate: formState.overtimeDate ? `${formState.overtimeDate}T00:00:00` : '',
    plannedHours: Number(formState.plannedHours ?? 0),
    actualHours: formState.actualHours != null ? Number(formState.actualHours) : undefined,
    reason: formState.reason,
    overtimeStatus: Number(formState.overtimeStatus ?? 0),
    remark: formState.remark || undefined
  }
  if (props.formData?.overtimeId != null && String(props.formData.overtimeId).length > 0) {
    payload.overtimeId = String(props.formData.overtimeId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyOvertimeForm())
  activeTab.value = 'basic'
}

const setServerValidationErrors = (errors: Array<{ field?: string; message?: string }>) => {
  if (!Array.isArray(errors) || errors.length === 0) return
  const fields = errors
    .filter((e) => e?.field && e?.message)
    .map((e) => ({ name: e.field as string, errors: [e.message as string] }))
  if (fields.length > 0) {
    formRef.value?.setFields(fields)
  }
}

defineExpose({
  validate,
  getValues,
  resetFields,
  setServerValidationErrors
})
</script>

<style scoped lang="less">
:deep(.ant-tabs-content-holder) {
  min-height: 50vh;
}

:deep(.ant-tabs-tabpane) {
  min-height: 50vh;
}
</style>
�误中取出可读消息（兼容 `Error` 与 Axios 风格对象）。
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

/** 顶部查询关键字，对应列表请求中的 `keyWords`。 */
const queryKeyword = ref('')

/** 列表与删除、导出等异步请求的 loading。 */
const loading = ref(false)

/** 当前页表格数据。 */
const dataSource = ref<Overtime[]>([])

/** 当前页码（从 1 开始）。 */
const currentPage = ref(1)

/** 每页条数。 */
const pageSize = ref(20)

/** 总条数。 */
const total = ref(0)

/** 单行选中语义下的当前行（用于工具栏编辑）。 */
const selectedRow = ref<Overtime | null>(null)

/** 多选已选行。 */
const selectedRows = ref<Overtime[]>([])

/** 多选对应的 `row-key` 列表。 */
const selectedRowKeys = ref<(string | number)[]>([])

/** 表单弹窗可见性。 */
const formVisible = ref(false)

/** 弹窗标题。 */
const formTitle = ref('')

/** 传入 `OvertimeForm` 的初始数据。 */
const formData = ref<Partial<Overtime>>({})

/** 表单提交 loading。 */
const formLoading = ref(false)

/**
 * `OvertimeForm` 暴露实例类型。
 */
type OvertimeFormExposed = InstanceType<typeof OvertimeForm>

/** 加班表单 ref。 */
const formRef = ref<OvertimeFormExposed | null>(null)

/** 表格组件 ref（`defineExpose`）。 */
const tableRef = ref()

/** 高级查询抽屉可见性。 */
const advancedQueryVisible = ref(false)

/**
 * 高级查询：员工 ID 字符串（可解析为数字后传给接口）、状态、加班日期起止（`YYYY-MM-DD`）。
 */
const advancedQueryForm = ref<{
  employeeId: string
  overtimeStatus?: string | number
  from: string
  to: string
}>({
  employeeId: '',
  overtimeStatus: undefined,
  from: '',
  to: ''
})

/** 导入弹窗可见性。 */
const importVisible = ref(false)

/** 列设置抽屉可见性。 */
const columnSettingVisible = ref(false)

/** 列设置勾选的列键；空表示展示全部。 */
const visibleColumnKeys = ref<string[]>([])

/** 首屏加载列表。 */
onMounted(() => {
  loadData()
})

/**
 * 从表格行解析主键字符串，供 `row-key`、删除与导出使用。
 *
 * @param {Overtime} record - 列表行数据
 * @returns {string} 优先 `overtimeId`，否则兼容 `id`；均无时返回空串
 */
const getOvertimeId = (record: Overtime): string => {
  if (record?.overtimeId != null && String(record.overtimeId) !== '') return String(record.overtimeId)
  const legacyId = getOvertimeField(record, 'id')
  if (legacyId != null && legacyId !== '') return String(legacyId)
  return ''
}

/**
 * 读取行字段（与用户页 `getUserField` 一致，供 `TaktDictTag` 的 `:value`）。
 *
 * @param {Overtime} record - 列表行
 * @param {string} field - 字段名（与 DTO 字段一致）
 * @returns {unknown} 字段原始值
 */
const getOvertimeField = (record: Overtime, field: string): unknown =>
  (record as unknown as Record<string, unknown>)[field]

/**
 * 高级查询「加班状态」绑定值（字典可能为 string）转为接口所需 number。
 */
function coerceAdvancedOvertimeStatus(value: string | number | undefined | null): number | undefined {
  if (value === undefined || value === null || value === '') return undefined
  if (typeof value === 'number' && Number.isFinite(value)) return value
  const n = Number(value)
  return Number.isFinite(n) ? n : undefined
}

/**
 * 列配置的稳定键，用于列设置勾选与列宽匹配。
 *
 * @param {OvertimeTableColumn} col - 列定义
 * @returns {string} `key` / `dataIndex` / `title` 字符串化，均无则空串
 */
function getOvertimeColumnKey(col: OvertimeTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key
  const dataIndex = c.dataIndex
  const title = c.title
  return key || dataIndex || title ? String(key ?? dataIndex ?? title) : ''
}

/**
 * 列表主列（含操作列）；`overtimeStatus` 由 `#bodyCell` + `TaktDictTag` 展示。
 */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'overtimeId',
    key: 'id',
    width: 88,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: Overtime }) =>
      getOvertimeField(record, 'overtimeId') ?? getOvertimeField(record, 'id') ?? ''
  },
  {
    title: t('entity.overtime.employeeid'),
    dataIndex: 'employeeId',
    key: 'employeeId',
    width: 100,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.overtime.overtimedate'),
    dataIndex: 'overtimeDate',
    key: 'overtimeDate',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.overtime.plannedhours'),
    dataIndex: 'plannedHours',
    key: 'plannedHours',
    width: 96,
    resizable: true
  },
  {
    title: t('entity.overtime.actualhours'),
    dataIndex: 'actualHours',
    key: 'actualHours',
    width: 96,
    resizable: true
  },
  {
    title: t('entity.overtime.reason'),
    dataIndex: 'reason',
    key: 'reason',
    width: 180,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.overtime.overtimestatus'),
    dataIndex: 'overtimeStatus',
    key: 'overtimeStatus',
    width: 100
  },
  CreateActionColumn<Overtime>({
    actions: [
      {
        key: 'update',
        label: t('common.page.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:overtime:update',
        onClick: (record: Overtime) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.page.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:overtime:delete',
        onClick: (record: Overtime) => handleDeleteOne(record)
      }
    ]
  })
])

/**
 * 合并默认列（审计等）后的完整列配置。
 */
const mergedColumns = computed((): TableColumnsType =>
  mergeDefaultColumns(columns.value, t, true)
)

/**
 * 按列设置过滤后的展示列。
 */
const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map((k) => String(k)))
  return merged.filter((col: OvertimeTableColumn) => {
    const colKey = getOvertimeColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

/**
 * 表格行选择配置。
 */
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Overtime[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Overtime, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getOvertimeId(selectedRow.value) === getOvertimeId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Overtime[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

/**
 * 点击行切换该行选中状态（与 `rowSelection` 联动）。
 *
 * @param {Overtime} record - 被点击行
 * @returns {{ onClick: () => void }}
 */
const onClickRow = (record: Overtime) => ({
  onClick: () => {
    const key = getOvertimeId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getOvertimeId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

/**
 * 拉取加班分页列表，合并关键字与高级查询条件后调用 `getOvertimeList`。
 *
 * @returns {Promise<void>}
 */
const loadData = async () => {
  try {
    loading.value = true
    const params: Record<string, unknown> = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) params.employeeId = n
    }
    const advOs = coerceAdvancedOvertimeStatus(advancedQueryForm.value.overtimeStatus)
    if (advOs !== undefined) params.overtimeStatus = advOs
    if (advancedQueryForm.value.from) params.overtimeDateFrom = advancedQueryForm.value.from
    if (advancedQueryForm.value.to) params.overtimeDateTo = advancedQueryForm.value.to
    const response: TaktPagedResult<Overtime> = await getOvertimeList(params)
    const items = response.data ?? []
    const totalCount = response.total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: unknown) {
    logger.error('[Overtime] 加载数据失败:', error)
    message.error(getErrorMessage(error) || t('common.page.msg.loadfail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

/**
 * 查询栏搜索：第一页并加载。
 */
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

/**
 * 查询栏重置：清空关键字与高级条件，第一页并加载。
 */
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = { employeeId: '', overtimeStatus: undefined, from: '', to: '' }
  currentPage.value = 1
  loadData()
}

/**
 * 表格变更回调（分页/筛选/排序）；当前仅记录排序字段，便于后续接后端排序。
 *
 * @param _pagination - 分页状态（未使用）
 * @param _filters - 筛选状态（未使用）
 * @param {TableSorterLike} sorter - 排序状态
 */
const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[Overtime] 排序:', sorter.field, sorter.order)
}

/**
 * 分页 `@change`：同步页码与每页条数后加载。
 *
 * @param {number} page - 页码
 * @param {number} size - 每页条数
 */
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

/**
 * 仅每页条数变化：回到第一页再加载。
 *
 * @param _current - 当前页（未使用）
 * @param {number} size - 新每页条数
 */
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

/**
 * 列宽拖拽结束：将新宽度写回 `columns` 中对应列配置。
 *
 * @param {number} w - 新宽度（px）
 * @param {OvertimeTableColumn} col - 当前拖拽的列
 */
const handleResizeColumn = (w: number, col: OvertimeTableColumn) => {
  const column = columns.value.find((c: OvertimeTableColumn) => {
    const colKey = getOvertimeColumnKey(col)
    const cKey = getOvertimeColumnKey(c)
    return colKey && cKey && colKey === cKey
  })
  if (column) (column as OvertimeTableColumn & { width?: number }).width = w
}

/**
 * 新建：打开弹窗并清空 `formData`。
 */
const handleCreate = () => {
  formTitle.value = t('common.page.button.create') + t('entity.overtime._self')
  formData.value = {}
  formVisible.value = true
}

/**
 * 编辑指定行。
 *
 * @param {Overtime} record - 行数据
 */
const handleEdit = (record: Overtime) => {
  formTitle.value = t('common.page.button.edit') + t('entity.overtime._self')
  formData.value = { ...record }
  formVisible.value = true
}

/**
 * 工具栏编辑：依赖 `selectedRow`。
 */
const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.page.action.warnselecttoaction', { action: t('common.page.button.edit'), entity: t('entity.overtime._self') }))
}

/**
 * 行内删除单条。
 *
 * @param {Overtime} record - 待删行
 */
const handleDeleteOne = (record: Overtime) => {
  const reason = getOvertimeField(record, 'reason')
  const name =
    (typeof reason === 'string' && reason.trim() !== '' ? reason : null) ??
    (typeof reason === 'number' ? String(reason) : null) ??
    getOvertimeId(record)
  Modal.confirm({
    title: t('common.page.action.confirmdelete'),
    content: t('common.page.confirm.deleteentity', { entity: t('entity.overtime._self'), name }),
    okText: t('common.page.button.delete'),
    cancelText: t('common.page.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteOvertimeById(getOvertimeId(record))
        message.success(t('common.page.msg.deletesuccess', { target: t('entity.overtime._self') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.page.msg.deletefail', { target: t('entity.overtime._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 批量删除：无选中提示；单条 `deleteOvertimeById`，多条 `deleteOvertimeBatch`。
 */
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.page.action.warnselecttoaction', { action: t('common.page.button.delete'), entity: t('entity.overtime._self') }))
    return
  }
  Modal.confirm({
    title: t('common.page.action.confirmdelete'),
    content: t('common.page.confirm.deletecountentity', { entity: t('entity.overtime._self'), count: selectedRows.value.length }),
    okText: t('common.page.button.delete'),
    cancelText: t('common.page.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteOvertimeById(getOvertimeId(selectedRows.value[0]))
        } else {
          await deleteOvertimeBatch(selectedRows.value.map((r) => getOvertimeId(r)))
        }
        message.success(t('common.page.msg.deletesuccess', { target: t('entity.overtime._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.page.msg.deletefail', { target: t('entity.overtime._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 弹窗提交：校验后 `createOvertime` 或 `updateOvertime`；表单校验失败（含 `errorFields`）时不弹通用错误。
 *
 * @returns {Promise<void>}
 */
const handleFormSubmit = async () => {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const formValues = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.overtimeId) {
      const id = String(formData.value.overtimeId)
      const payload: OvertimeUpdate = { ...formValues, overtimeId: id }
      await updateOvertime(id, payload)
      message.success(t('common.page.msg.updatesuccess', { target: t('entity.overtime._self') }))
    } else {
      const { overtimeId: _omitOvertimeId, ...createPayload } = formValues
      await createOvertime(createPayload as OvertimeCreate)
      message.success(t('common.page.msg.createsuccess', { target: t('entity.overtime._self') }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    message.error(getErrorMessage(error) || t('common.page.msg.operatefail', { action: t('common.page.action.operation') }))
  } finally {
    formLoading.value = false
  }
}

/**
 * 关闭弹窗并重置子表单。
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
 * 下载导入模板（转调 `getOvertimeTemplate`）。
 *
 * @param {string} [sheetName] - 工作表名
 * @param {string} [fileName] - 文件名
 * @returns {Promise<import('@/api/request').BlobDownloadWithMeta>}
 */
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getOvertimeTemplate(sheetName, fileName)
}

/**
 * 执行导入上传（转调 `importOvertimeData`）。
 *
 * @param {File} file - Excel 文件
 * @param {string} [sheetName] - 工作表名
 * @returns {Promise<{ success: number; fail: number; errors: string[] }>}
 */
const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importOvertimeData(file, sheetName)
}

/**
 * 导入成功：刷新列表；全部成功则延时关弹窗。
 *
 * @param {{ success: number; fail: number; errors: string[] }} result - 导入统计
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
 * 导出：请求体为当前关键字与高级查询条件（`Record<string, unknown>` 与 `getOvertimeList` 参数形状一致）；响应为 `Blob` 或带 `data` 的包装时均兼容。
 *
 * @returns {Promise<void>}
 */
const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: Record<string, unknown> = {}
    if (queryKeyword.value) queryParams.keyWords = queryKeyword.value
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) queryParams.employeeId = n
    }
    const advOs = coerceAdvancedOvertimeStatus(advancedQueryForm.value.overtimeStatus)
    if (advOs !== undefined) queryParams.overtimeStatus = advOs
    if (advancedQueryForm.value.from) queryParams.overtimeDateFrom = advancedQueryForm.value.from
    if (advancedQueryForm.value.to) queryParams.overtimeDateTo = advancedQueryForm.value.to
    const blob = await exportOvertimeData(
      queryParams,
      overtimeExcelNames.sheet,
      overtimeExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${overtimeExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.page.msg.exportsuccess', { target: t('entity.overtime._self') }))
  } catch (error: unknown) {
    logger.error('[Overtime] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.page.msg.exportfail', { target: t('entity.overtime._self') }))
  } finally {
    loading.value = false
  }
}

/**
 * 打开高级查询抽屉。
 */
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

/**
 * 高级查询提交：第一页加载并关抽屉。
 */
const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

/**
 * 重置高级查询表单字段为初始值。
 */
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { employeeId: '', overtimeStatus: undefined, from: '', to: '' }
}

/**
 * 打开列设置抽屉。
 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

/**
 * 同步列设置勾选结果。
 *
 * @param {(string | number)[]} keys - 勾选列键
 */
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map((k) => String(k))
}

/**
 * 清空列筛选，展示全部列。
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

/** 供父级或调试使用的表格 ref */
defineExpose({ tableRef })
</script>

<style scoped lang="less">
/* 与请假页相同：内边距 + 纵向 flex 填满，避免表格高度塌陷 */
.humanresource-attendance-leave-overtime {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
