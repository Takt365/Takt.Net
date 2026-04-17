<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/human-resource/personnel/employee -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：员工管理页面，包含员工列表、查询、新增、编辑、删除、导入、模板下载、导出等功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-personnel-employee">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="
        t('common.form.placeholder.required', {
          field: [t('entity.employee.code'), t('entity.employee.realname'), t('entity.employee.phone')].join('、')
        }) + t('common.button.query')
      "
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="humanresource:personnel:employee:create"
      update-permission="humanresource:personnel:employee:update"
      delete-permission="humanresource:personnel:employee:delete"
      import-permission="humanresource:personnel:employee:import"
      export-permission="humanresource:personnel:employee:export"
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
      :row-key="getEmployeeId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'gender'">
          <TaktDictTag
            :value="getEmployeeField(record, 'gender')"
            dict-type="sys_user_gender"
          />
        </template>
        <template v-else-if="column.key === 'politicalStatus'">
          <TaktDictTag
            :value="getEmployeeField(record, 'politicalStatus')"
            dict-type="hr_political_status"
          />
        </template>
        <template v-else-if="column.key === 'maritalStatus'">
          <TaktDictTag
            :value="getEmployeeField(record, 'maritalStatus')"
            dict-type="hr_marital_status"
          />
        </template>
        <template v-else-if="column.key === 'employeeStatus'">
          <TaktDictTag
            :value="getEmployeeField(record, 'employeeStatus')"
            dict-type="hr_employee_status"
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

    <!-- 新增/编辑对话框：视口宽 50%、高 75vh，可拖拽调整宽高 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <EmployeeForm
        ref="formRef"
        :form-data="formData"
        :is-edit="!!formData?.employeeId"
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
      <a-form-item :label="t('entity.employee.code')">
        <a-input v-model:value="advancedQueryForm.employeeCode" />
      </a-form-item>
      <a-form-item :label="t('entity.employee.realname')">
        <a-input v-model:value="advancedQueryForm.realName" />
      </a-form-item>
      <a-form-item :label="t('entity.employee.phone')">
        <a-input v-model:value="advancedQueryForm.phone" />
      </a-form-item>
      <a-form-item :label="t('entity.employee.employeestatus')">
        <TaktSelect
          v-model:value="advancedQueryForm.employeeStatus"
          dict-type="hr_employee_status"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.employee.employeestatus') })"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.employee._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="employeeExcelNames.sheet"
        :template-file-name="employeeExcelNames.fileBase"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.employee._self') })"
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
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import EmployeeForm from './components/employee-form.vue'
import {
  getEmployeeList,
  getEmployeeById,
  createEmployee,
  updateEmployee,
  deleteEmployeeById,
  deleteEmployeeBatch,
  exportEmployeeData,
  getEmployeeTemplate,
  importEmployeeData
} from '@/api/human-resource/personnel/employee'
import type { Employee } from '@/types/human-resource/personnel/employee'
import { logger } from '@/utils/logger'
import { resolveExportDownloadFileName } from '@/utils/export-download-name'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

// 导入/导出 Excel 名称（与服务端实体名一致）
const employeeExcelNames = taktExcelEntityNames('TaktEmployee')

// 顶栏查询关键字
const queryKeyword = ref('')
// 表格加载中
const loading = ref(false)
// 表格数据
const dataSource = ref<Employee[]>([])
// 分页：当前页、每页条数、总条数
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
// 行选择：单选行、多选行、勾选 key
const selectedRow = ref<Employee | null>(null)
const selectedRows = ref<Employee[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
// 新增/编辑表单弹窗
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Employee>>({})
const formLoading = ref(false)
const formRef = ref()
// 导入弹窗
const importVisible = ref(false)
// 高级查询抽屉
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref({
  employeeCode: '',
  realName: '',
  phone: '',
  employeeStatus: undefined as number | undefined
})
// 列设置抽屉
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

// 初始化时加载数据
onMounted(() => {
  loadData()
})

// 表格列配置（computed 以便列标题与操作列 label 随 locale 更新）
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'employeeId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => {
      return getEmployeeField(record, 'employeeId') ?? getEmployeeField(record, 'id') ?? ''
    }
  },
  {
    title: t('entity.employee.code'),
    dataIndex: 'employeeCode',
    key: 'employeeCode',
    width: 120,
    resizable: true,
    ellipsis: true,
    sorter: (a: any, b: any) => {
      const ac = getEmployeeField(a, 'employeeCode') || ''
      const bc = getEmployeeField(b, 'employeeCode') || ''
      return String(ac).localeCompare(String(bc))
    }
  },
  {
    title: t('entity.employee.realname'),
    dataIndex: 'realName',
    key: 'realName',
    width: 100,
    resizable: true,
    ellipsis: true,
    sorter: (a: any, b: any) => {
      const an = getEmployeeField(a, 'realName') || ''
      const bn = getEmployeeField(b, 'realName') || ''
      return String(an).localeCompare(String(bn))
    }
  },
  {
    title: t('entity.employee.formername'),
    dataIndex: 'formerName',
    key: 'formerName',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.fullname'),
    dataIndex: 'fullName',
    key: 'fullName',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.nativename'),
    dataIndex: 'nativeName',
    key: 'nativeName',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.displayname'),
    dataIndex: 'displayName',
    key: 'displayName',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.gender'),
    dataIndex: 'gender',
    key: 'gender',
    width: 80
  },
  {
    title: t('entity.employee.birthdate'),
    dataIndex: 'birthDate',
    key: 'birthDate',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.age'),
    dataIndex: 'age',
    key: 'age',
    width: 80,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.idcard'),
    dataIndex: 'idCard',
    key: 'idCard',
    width: 180,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.phone'),
    dataIndex: 'phone',
    key: 'phone',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.email'),
    dataIndex: 'email',
    key: 'email',
    width: 160,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.avatar'),
    dataIndex: 'avatar',
    key: 'avatar',
    width: 180,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.nationality'),
    dataIndex: 'nationality',
    key: 'nationality',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.politicalstatus'),
    dataIndex: 'politicalStatus',
    key: 'politicalStatus',
    width: 120
  },
  {
    title: t('entity.employee.maritalstatus'),
    dataIndex: 'maritalStatus',
    key: 'maritalStatus',
    width: 120
  },
  {
    title: t('entity.employee.nativeplace'),
    dataIndex: 'nativePlace',
    key: 'nativePlace',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.currentaddress'),
    dataIndex: 'currentAddress',
    key: 'currentAddress',
    width: 180,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.registeredaddress'),
    dataIndex: 'registeredAddress',
    key: 'registeredAddress',
    width: 180,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.employee.employeestatus'),
    dataIndex: 'employeeStatus',
    key: 'employeeStatus',
    width: 100
  },
  {
    title: t('common.entity.createTime'),
    dataIndex: 'createdAt',
    key: 'createdAt',
    width: 160,
    ellipsis: true
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:personnel:employee:update',
        onClick: (record: Employee) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:personnel:employee:delete',
        onClick: (record: Employee) => handleDeleteOne(record)
      }
    ]
  })
])

// 辅助函数：获取员工ID
const getEmployeeId = (employee: any): string => {
  if (employee?.employeeId != null) return String(employee.employeeId)
  if (employee?.id != null) return String(employee.id)
  return ''
}

// 辅助函数：获取字段值
const getEmployeeField = (employee: any, field: string): any => {
  return employee?.[field]
}

// 员工管理无「受保护管理员用户」逻辑，不设 isAdminUser；编辑/删除不作账号级限制。

// 更新/删除按钮禁用状态（表格无选中时自动灰色；基于 selectedRows 保证与表格勾选一致）
// 更新：仅当恰好选中 1 行时可点
// 删除：仅当至少选中 1 行时可点
const updateDisabled = computed(() => selectedRows.value.length !== 1)
const deleteDisabled = computed(() => selectedRows.value.length === 0)

// 合并列配置（包含审计字段）- 父组件自己处理，不依赖 TaktColumnDrawer
// 使用 any 避免 TypeScript「类型实例化过深，且可能无限」错误（mergeDefaultColumns + TableColumnsType 递归类型）
const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))

// 根据可见列过滤显示的列 - 保持原始列的顺序
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []

  // 如果 keys 为空，返回原始列配置（等待 TaktColumnDrawer 初始化）
  if (keys.length === 0) {
    return columns.value
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

// 行选择配置（官方标准方式）
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Employee[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Employee, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (getEmployeeId(selectedRow.value) === getEmployeeId(record)) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: Employee[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? selectedRowsData[0] : null
    } else {
      selectedRow.value = null
    }
  }
}))

// 行点击处理（点击行选中/取消选中 - 官方标准方式）
const onClickRow = (record: Employee) => {
  return {
    onClick: () => {
      // 根据业务需求判断是否可选
      // if (record.disabled) return;

      const key = getEmployeeId(record)
      const index = selectedRowKeys.value.indexOf(key)

      if (index > -1) {
        // 已选中，则取消
        selectedRowKeys.value.splice(index, 1)
      } else {
        // 未选中，则添加
        selectedRowKeys.value.push(key)
      }

      // 注意：此处需手动同步 selectedRows
      selectedRows.value = dataSource.value.filter(item =>
        selectedRowKeys.value.includes(getEmployeeId(item))
      )

      // 更新 selectedRow
      if (selectedRowKeys.value.length === 1) {
        selectedRow.value = selectedRows.value[0]
      } else {
        selectedRow.value = null
      }

      // 触发 rowSelection.onChange 以同步 checkbox 状态
      if (rowSelection.value.onChange) {
        rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
      }
    }
  }
}

// 加载数据
const loadData = async () => {
  try {
    loading.value = true
    const params: any = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }

    // 关键字查询
    if (queryKeyword.value) {
      params.employeeCode = queryKeyword.value
      params.realName = queryKeyword.value
      params.phone = queryKeyword.value
    }

    // 高级查询
    if (advancedQueryForm.value.employeeCode) {
      params.employeeCode = advancedQueryForm.value.employeeCode
    }
    if (advancedQueryForm.value.realName) {
      params.realName = advancedQueryForm.value.realName
    }
    if (advancedQueryForm.value.phone) {
      params.phone = advancedQueryForm.value.phone
    }
    if (
      advancedQueryForm.value.employeeStatus !== undefined &&
      advancedQueryForm.value.employeeStatus !== null
    ) {
      params.employeeStatus = advancedQueryForm.value.employeeStatus
    }

    const response = await getEmployeeList(params)

    // 处理响应数据：response 是 TaktPagedResult<Employee>，包含 data 和 total
    // 兼容后端可能返回的 PascalCase 格式
    const responseAny = response as any
    const items = response?.data || responseAny?.Data || []
    const totalCount = response?.total || responseAny?.Total || 0

    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Employee Management] 加载数据失败:', error)
    message.error(error.message || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 查询
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

// 重置
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = {
    employeeCode: '',
    realName: '',
    phone: '',
    employeeStatus: undefined
  }
  currentPage.value = 1
  loadData()
}

// 表格变化（仅处理排序，分页由 TaktPagination 处理）
const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter && sorter.order) {
    logger.debug('[Employee Management] 排序字段:', sorter.field, '排序方向:', sorter.order)
  }
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

// 列宽调整处理
const handleResizeColumn = (w: number, col: any) => {
  // 更新对应列的宽度
  const column = columns.value.find((c: any) => {
    const colKey = col.key || col.dataIndex || col.title
    const cKey = c.key || c.dataIndex || c.title
    return colKey && cKey && String(colKey) === String(cKey)
  })
  if (column) {
    column.width = w
  }
}


// 新增
const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.employee._self')
  formData.value = {}
  formVisible.value = true
}

// 编辑
const handleEdit = async (record: Employee) => {
  formTitle.value = t('common.button.edit') + t('entity.employee._self')
  try {
    formLoading.value = true
    const detail = await getEmployeeById(getEmployeeId(record))
    formData.value = { ...detail }
    formVisible.value = true
  } catch (error: any) {
    message.error(error.message || t('common.msg.loadFail'))
  } finally {
    formLoading.value = false
  }
}

// 更新（工具栏按钮）
const handleUpdate = () => {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
  } else {
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.edit'),
        entity: t('entity.employee._self')
      })
    )
  }
}

// 删除单个
const handleDeleteOne = (record: Employee) => {
  const name = getEmployeeField(record, 'realName') || getEmployeeId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.employee._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteEmployeeById(getEmployeeId(record))
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

// 删除（工具栏按钮）
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.delete'),
        entity: t('entity.employee._self')
      })
    )
    return
  }

  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      entity: t('entity.employee._self'),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteEmployeeById(getEmployeeId(selectedRows.value[0]))
        } else {
          await deleteEmployeeBatch(selectedRows.value.map((r) => getEmployeeId(r)))
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

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 下载导入模板（优先服务端 Content-Disposition，与导出一致）
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getEmployeeTemplate(sheetName, fileName)
}

// 导入文件
const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importEmployeeData(file, sheetName)
}

// 导入成功回调
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  logger.info('[Employee Management] 导入成功:', result)
  // 刷新数据列表
  loadData()
  // 如果全部成功，可以关闭导入对话框
  if (result.fail === 0) {
    setTimeout(() => {
      importVisible.value = false
    }, 2000)
  }
}

// 取消导入
const handleImportCancel = () => {
  importVisible.value = false
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true

    // 构建查询参数（使用当前查询条件）
    const queryParams: any = {
      pageIndex: 1,
      pageSize: total.value || 9999
    }

    // 关键字查询
    if (queryKeyword.value) {
      queryParams.employeeCode = queryKeyword.value
      queryParams.realName = queryKeyword.value
      queryParams.phone = queryKeyword.value
    }

    // 高级查询
    if (advancedQueryForm.value.employeeCode) {
      queryParams.employeeCode = advancedQueryForm.value.employeeCode
    }
    if (advancedQueryForm.value.realName) {
      queryParams.realName = advancedQueryForm.value.realName
    }
    if (advancedQueryForm.value.phone) {
      queryParams.phone = advancedQueryForm.value.phone
    }
    if (
      advancedQueryForm.value.employeeStatus !== undefined &&
      advancedQueryForm.value.employeeStatus !== null
    ) {
      queryParams.employeeStatus = advancedQueryForm.value.employeeStatus
    }

    // 调用导出 API；本地文件名仅按服务端 Content-Disposition / Content-Type 还原（是否 zip 由后端 TaktExcelHelper 决定）
    const exportMeta = await exportEmployeeData(
      queryParams,
      employeeExcelNames.sheet,
      employeeExcelNames.fileBase
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fallbackBase = `${employeeExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}`
    const fileName = resolveExportDownloadFileName({
      contentDisposition: exportMeta.contentDisposition,
      contentType: exportMeta.contentType,
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

    // 延迟清理 URL，确保下载完成
    setTimeout(() => {
      window.URL.revokeObjectURL(url)
    }, 100)

    message.success(t('common.msg.exportSuccess'))
  } catch (error: any) {
    logger.error('[Employee Management] 导出失败:', error)
    message.error(error.message || t('common.msg.exportFail'))
  } finally {
    loading.value = false
  }
}

// 高级查询
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

// 高级查询提交
const handleAdvancedQuerySubmit = (_values?: Record<string, any>) => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

// 高级查询重置
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = {
    employeeCode: '',
    realName: '',
    phone: '',
    employeeStatus: undefined
  }
}

// 列设置
// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化 - TaktColumnDrawer 传递选中的 keys，更新 visibleColumnKeys
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}

// 列设置重置：TaktColumnDrawer 会自动重置为默认的9个列（ID + 7个字段 + 操作列）
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
  try {
    if (!formRef.value) {
      return
    }

    await formRef.value.validate()
    const formValues = formRef.value.getValues()

    formLoading.value = true

    if (formData.value?.employeeId) {
      // 更新
      await updateEmployee(formData.value.employeeId, {
        ...formValues,
        employeeId: formData.value.employeeId
      })
      message.success(t('common.msg.updateSuccess'))
    } else {
      await createEmployee(formValues)
      message.success(t('common.msg.createSuccess'))
    }

    // 重置表单
    formRef.value?.resetFields()
    formData.value = {}

    formVisible.value = false
    loadData()
  } catch (error: any) {
    if (error.errorFields) {
      // 表单验证错误
      return
    }
    if (Array.isArray(error?.validationErrors) && error.validationErrors.length > 0) {
      formRef.value?.setServerValidationErrors?.(error.validationErrors)
      return
    }
    message.error(error.message || t('common.msg.operateFail'))
  } finally {
    formLoading.value = false
  }
}

// 表单取消
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  if (formRef.value) {
    formRef.value.resetFields()
  }
}
</script>

<style scoped lang="less">
.humanresource-personnel-employee {
  padding: 16px;
}
</style>
