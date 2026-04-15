<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/identity/user -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：用户管理页面，包含用户列表、查询、新增、编辑、删除等功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="identity-user">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.required', { field: [t('entity.user.name'), t('entity.user.email'), t('entity.user.phone')].join('、') }) + t('common.button.query')"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="identity:user:create"
      update-permission="identity:user:update"
      delete-permission="identity:user:delete"
      import-permission="identity:user:import"
      export-permission="identity:user:export"
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
      :row-key="getUserId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'userStatus'">
          <TaktDictTag
            :value="getUserField(record, 'userStatus')"
            dict-type="sys_normal_disable"
          />
        </template>
        <template v-else-if="column.key === 'userType'">
          <TaktDictTag
            :value="getUserField(record, 'userType')"
            dict-type="sys_user_type"
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
      <UserForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 修改密码对话框 -->
    <TaktModal
      v-model:open="changePasswordVisible"
      :title="t('common.button.changepwd')"
      :width="'33.333vw'"
      :confirm-loading="changePasswordLoading"
      @ok="handleChangePasswordSubmit"
      @cancel="handleChangePasswordCancel"
    >
      <UserChangePassword
        v-if="currentChangePasswordUser"
        ref="changePasswordFormRef"
        :user-name="getUserField(currentChangePasswordUser, 'userName') || ''"
        :loading="changePasswordLoading"
      />
    </TaktModal>

    <!-- 高级查询抽屉 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.user.name')">
        <a-input v-model:value="advancedQueryForm.UserName" />
      </a-form-item>
      <a-form-item :label="t('entity.user.email')">
        <a-input v-model:value="advancedQueryForm.UserEmail" />
      </a-form-item>
      <a-form-item :label="t('entity.user.phone')">
        <a-input v-model:value="advancedQueryForm.UserPhone" />
      </a-form-item>
      <a-form-item :label="t('entity.user.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.UserStatus"
          dict-type="sys_normal_disable"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.user.status') })"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.user._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="userExcelNames.sheet"
        :template-file-name="userExcelNames.fileBase"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.user._self') })"
        :upload-text="t('common.action.import.uploadText')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <!-- 分配角色对话框 -->
    <AssignRoleUsers
      v-model:open="assignRoleVisible"
      :user="currentAssignUser"
      @success="handleAssignSuccess"
    />

    <!-- 分配部门对话框 -->
    <AssignDeptUsers
      v-model:open="assignDeptVisible"
      :user="currentAssignUser"
      @success="handleAssignSuccess"
    />

    <!-- 分配岗位对话框 -->
    <AssignPostUsers
      v-model:open="assignPostVisible"
      :user="currentAssignUser"
      @success="handleAssignSuccess"
    />

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

    <!-- 分配租户对话框 -->
    <AssignUserTenants
      v-model:open="assignTenantVisible"
      :user="currentAssignUser"
      @success="handleAssignSuccess"
    />

    <!-- 分配角色菜单对话框 -->
    <AssignRoleMenus
      v-model:open="assignRoleMenuVisible"
      :user="currentAssignUser"
      @success="handleAssignSuccess"
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
import UserForm from './components/user-form.vue'
import AssignRoleUsers from './components/assign-role-users.vue'
import AssignDeptUsers from './components/assign-dept-users.vue'
import AssignPostUsers from './components/assign-post-users.vue'
import AssignUserTenants from './components/assign-user-tenants.vue'
import AssignRoleMenus from './components/assign-role-menus.vue'
import UserChangePassword from './components/user-change-password.vue'
import { getUserList, createUser, updateUser, deleteUserById, deleteUserBatch, unlock, exportUserData, getUserTemplate, importUserData, resetPassword, changePassword } from '@/api/identity/user'
import type { User, UserCreate, UserFormValues } from '@/types/identity/user'
import { logger } from '@/utils/logger'
import { resolveExportDownloadFileName } from '@/utils/export-download-name'
import { taktExcelEntityNames } from '@/utils/naming'
import {
  RiEditLine,
  RiDeleteBinLine,
  RiShieldUserLine,
  RiUserSettingsLine,
  RiBuildingLine,
  RiBriefcaseLine,
  RiCommunityLine,
  RiLockUnlockLine,
  RiLockPasswordLine,
  RiRestartLine
} from '@remixicon/vue'

const { t } = useI18n()

// 导入/导出 Excel 名称（与服务端实体名一致）
const userExcelNames = taktExcelEntityNames('TaktUser')

// 顶栏查询关键字
const queryKeyword = ref('')
// 表格加载中
const loading = ref(false)
// 表格数据
const dataSource = ref<User[]>([])
// 分页：当前页、每页条数、总条数
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
// 行选择：单选行、多选行、勾选 key
const selectedRow = ref<User | null>(null)
const selectedRows = ref<User[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
// 新增/编辑表单弹窗
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<User>>({})
const formLoading = ref(false)
const formRef = ref()
// 修改密码弹窗
const changePasswordFormRef = ref()
const changePasswordVisible = ref(false)
const changePasswordLoading = ref(false)
const currentChangePasswordUser = ref<User | null>(null)
// 高级查询抽屉
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref({
  UserName: '',
  UserEmail: '',
  UserPhone: '',
  UserStatus: undefined as number | undefined
})
// 导入弹窗
const importVisible = ref(false)
// 分配角色/部门/岗位/租户/角色菜单（弹窗与当前操作用户）
const assignRoleVisible = ref(false)
const assignDeptVisible = ref(false)
const assignPostVisible = ref(false)
const assignTenantVisible = ref(false)
const assignRoleMenuVisible = ref(false)
const currentAssignUser = ref<User | null>(null)
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
    dataIndex: 'userId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => {
      return getUserField(record, 'userId') || ''
    }
  },
  {
    title: t('entity.user.employeeid'),
    dataIndex: 'employeeId',
    key: 'employeeId',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.user.name'),
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    resizable: true,
    ellipsis: true,
    sorter: (a: any, b: any) => {
      const aName = getUserField(a, 'userName') || ''
      const bName = getUserField(b, 'userName') || ''
      return aName.localeCompare(bName)
    }
  },
  {
    title: t('entity.user.nickname'),
    dataIndex: 'nickName',
    key: 'nickName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.user.email'),
    dataIndex: 'userEmail',
    key: 'userEmail',
    width: 180,
    resizable: true,
    ellipsis: true,
  },
  {
    title: t('entity.user.phone'),
    dataIndex: 'userPhone',
    key: 'userPhone',
    width: 130
  },
  {
    title: t('entity.user.type'),
    dataIndex: 'userType',
    key: 'userType',
    width: 100
  },
  {
    title: t('entity.user.status'),
    dataIndex: 'userStatus',
    key: 'userStatus',
    width: 100
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'identity:user:update',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleEdit(record)
      },
      {
        key: 'changepwd',
        label: t('common.button.changepwd'),
        shape: 'plain',
        icon: RiLockPasswordLine,
        permission: 'identity:user:changepwd',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleUpdatePassword(record)
      },
      {
        key: 'resetpwd',
        label: t('common.button.reset') + ' ' + t('common.button.password'),
        shape: 'plain',
        icon: RiRestartLine,
        permission: 'identity:user:resetpwd',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleResetPassword(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'identity:user:delete',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleDeleteOne(record)
      },
      {
        key: 'allocate-user-role',
        label: t('common.button.allocate') + t('entity.role._self'),
        shape: 'plain',
        icon: RiUserSettingsLine,
        permission: 'identity:user:allocate',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleRole(record)
      },
      {
        key: 'allocate-role-menu',
        label: t('common.button.allocate') + t('entity.rolemenu._self'),
        shape: 'plain',
        icon: RiShieldUserLine,
        permission: 'identity:user:authorize',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleRoleMenu(record)
      },
      {
        key: 'allocate-dept-user',
        label: t('common.button.allocate') + t('entity.dept._self'),
        shape: 'plain',
        icon: RiBuildingLine,
        permission: 'identity:user:allocate',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleDept(record)
      },
      {
        key: 'allocate-post-user',
        label: t('common.button.allocate') + t('entity.post._self'),
        shape: 'plain',
        icon: RiBriefcaseLine,
        permission: 'identity:user:allocate',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handlePost(record)
      },
      {
        key: 'allocate-tenant-user',
        label: t('common.button.allocate') + t('entity.tenant._self'),
        shape: 'plain',
        icon: RiCommunityLine,
        permission: 'identity:user:allocate',
        visible: (record: User) => !isAdminUser(record),
        onClick: (record: User) => handleTenant(record)
      },
      {
        key: 'unlock',
        label: t('common.button.unlock'),
        shape: 'plain',
        icon: RiLockUnlockLine,
        permission: 'identity:user:unlock',
        visible: (record: User) => {
          if (isAdminUser(record)) return false
          return record.userStatus === 2
        },
        onClick: (record: User) => handleUnlock(record)
      }
    ]
  })
])

// 辅助函数：获取用户ID
const getUserId = (user: any): string => {
  return user?.userId || ''
}

// 辅助函数：获取字段值
const getUserField = (user: any, field: string): any => {
  return user?.[field]
}

// 辅助函数：判断是否为受保护的管理员用户（admin、guest）
const isAdminUser = (user: User | null): boolean => {
  if (!user) return false
  const userName = getUserField(user, 'userName') || ''
  const lowerUserName = userName.toLowerCase()
  return lowerUserName === 'admin' || lowerUserName === 'guest'
}

// 更新/删除按钮禁用状态（表格无选中时自动灰色；基于 selectedRows 保证与表格勾选一致）
// 更新：仅当恰好选中 1 行且非管理员时可点
// 删除：仅当至少选中 1 行且选中里无管理员时可点
const updateDisabled = computed(
  () =>
    selectedRows.value.length !== 1 || (selectedRows.value.length === 1 && isAdminUser(selectedRows.value[0]))
)
const deleteDisabled = computed(
  () => selectedRows.value.length === 0 || selectedRows.value.some((u) => isAdminUser(u))
)

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
  onChange: (keys: (string | number)[], rows: User[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: User, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
    } else if (getUserId(selectedRow.value) === getUserId(record)) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: User[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? selectedRowsData[0] : null
    } else {
      selectedRow.value = null
    }
  }
}))

// 行点击处理（点击行选中/取消选中 - 官方标准方式）
const onClickRow = (record: User) => {
  return {
    onClick: () => {
      // 根据业务需求判断是否可选
      // if (record.disabled) return;
      
      const key = getUserId(record)
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
        selectedRowKeys.value.includes(getUserId(item))
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
      PageIndex: currentPage.value,
      PageSize: pageSize.value
    }

    // 关键字查询
    if (queryKeyword.value) {
      params.KeyWords = queryKeyword.value
    }

    // 高级查询
    if (advancedQueryForm.value.UserName) {
      params.UserName = advancedQueryForm.value.UserName
    }
    if (advancedQueryForm.value.UserEmail) {
      params.UserEmail = advancedQueryForm.value.UserEmail
    }
    if (advancedQueryForm.value.UserPhone) {
      params.UserPhone = advancedQueryForm.value.UserPhone
    }
    if (advancedQueryForm.value.UserStatus !== undefined) {
      params.UserStatus = advancedQueryForm.value.UserStatus
    }

    const response = await getUserList(params)
    
    // 处理响应数据：response 是 TaktPagedResult<User>，包含 data 和 total
    // 兼容后端可能返回的 PascalCase 格式
    const responseAny = response as any
    const items = response?.data || responseAny?.Data || []
    const totalCount = response?.total || responseAny?.Total || 0

    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[User Management] 加载数据失败:', error)
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
    UserName: '',
    UserEmail: '',
    UserPhone: '',
    UserStatus: undefined
  }
  currentPage.value = 1
  loadData()
}

// 表格变化（仅处理排序，分页由 TaktPagination 处理）
const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter && sorter.order) {
    logger.debug('[User Management] 排序字段:', sorter.field, '排序方向:', sorter.order)
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
  formTitle.value = t('common.button.create') + t('entity.user._self')
  formData.value = {}
  formVisible.value = true
}

// 编辑
const handleEdit = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.update') }))
    return
  }
  formTitle.value = t('common.button.edit') + t('entity.user._self')
  formData.value = { ...record }
  formVisible.value = true
}

// 更新（工具栏按钮）
const handleUpdate = () => {
  if (selectedRow.value) {
    if (isAdminUser(selectedRow.value)) {
      message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.update') }))
      return
    }
    handleEdit(selectedRow.value)
  } else {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.user._self') }))
  }
}

// 删除单个
const handleDeleteOne = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.delete') }))
    return
  }
  const userName = getUserField(record, 'userName') || t('common.action.thisTarget', { target: t('entity.user._self') })
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.user._self'), name: userName }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteUserById(getUserId(record))
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
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.user._self') }))
    return
  }

  // 检查是否包含admin用户
  const adminUsers = selectedRows.value.filter(u => isAdminUser(u))
  if (adminUsers.length > 0) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.delete') }))
    return
  }

  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: t('entity.user._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteUserBatch(selectedRows.value.map(user => getUserId(user)))
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

// 分配角色菜单
const handleRoleMenu = (record: User) => {
  logger.debug('[User Management] handleRoleMenu 被调用:', { record })
  if (isAdminUser(record)) {
    logger.warn('[User Management] 超级管理员admin不允许分配角色菜单')
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.allocate') + t('entity.rolemenu._self') }))
    return
  }
  logger.debug('[User Management] 设置 currentAssignUser 和打开对话框')
  currentAssignUser.value = record
  assignRoleMenuVisible.value = true
  logger.debug('[User Management] assignRoleMenuVisible:', assignRoleMenuVisible.value)
}

// 分配角色
const handleRole = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.allocate') + t('entity.role._self') }))
    return
  }
  currentAssignUser.value = record
  assignRoleVisible.value = true
}

// 分配部门
const handleDept = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.allocate') + t('entity.dept._self') }))
    return
  }
  currentAssignUser.value = record
  assignDeptVisible.value = true
}

// 分配岗位
const handlePost = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.allocate') + t('entity.post._self') }))
    return
  }
  currentAssignUser.value = record
  assignPostVisible.value = true
}

// 分配租户
const handleTenant = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.allocate') + t('entity.tenant._self') }))
    return
  }
  currentAssignUser.value = record
  assignTenantVisible.value = true
}

// 解锁用户
// 更新密码
const handleUpdatePassword = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.changepwd') }))
    return
  }
  currentChangePasswordUser.value = record
  changePasswordVisible.value = true
}

// 修改密码提交
const handleChangePasswordSubmit = async () => {
  try {
    if (!changePasswordFormRef.value) {
      return
    }
    
    await changePasswordFormRef.value.validate()
    const formValues = changePasswordFormRef.value.getValues()
    
    if (!currentChangePasswordUser.value) {
      message.error(t('common.msg.entityNotFound', { entity: t('entity.user._self') }))
      return
    }
    
    changePasswordLoading.value = true
    
    await changePassword(formValues)
    message.success(t('common.msg.actionSuccess', { action: t('common.button.changepwd') }))
    changePasswordVisible.value = false
    currentChangePasswordUser.value = null
    changePasswordFormRef.value?.resetFields()
  } catch (error: any) {
    if (error.errorFields) {
      // 表单验证错误
      return
    }
    message.error(error.message || t('common.msg.actionFail', { action: t('common.button.changepwd') }))
  } finally {
    changePasswordLoading.value = false
  }
}

// 修改密码取消
const handleChangePasswordCancel = () => {
  changePasswordVisible.value = false
  currentChangePasswordUser.value = null
  changePasswordFormRef.value?.resetFields()
}

// 重置密码
const handleResetPassword = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.reset') + ' ' + t('common.button.password') }))
    return
  }
  const userName = getUserField(record, 'userName') || t('common.action.thisTarget', { target: t('entity.user._self') })
  Modal.confirm({
    title: t('common.button.reset') + ' ' + t('common.button.password'),
    content: t('common.confirm.resetPwdContent', { entity: t('entity.user._self'), name: userName }),
    okText: t('common.button.ok'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await resetPassword({
          userId: getUserId(record)
          // 不传递 newPassword，后端会从配置中读取默认密码（PasswordPolicy:DefaultPassword）
        })
        message.success(t('common.msg.actionSuccess', { action: t('common.button.reset') + ' ' + t('common.button.password') }))
        loadData()
      } catch (error: any) {
        message.error(error.message || t('common.msg.actionFail', { action: t('common.button.reset') + ' ' + t('common.button.password') }))
      } finally {
        loading.value = false
      }
    }
  })
}

// 解除锁定
const handleUnlock = (record: User) => {
  if (isAdminUser(record)) {
    message.warning(t('common.action.warnSubjectCannot', { subject: t('common.action.superUser'), action: t('common.button.unlock') }))
    return
  }
  const userName = getUserField(record, 'userName') || t('common.action.thisTarget', { target: t('entity.user._self') })
  Modal.confirm({
    title: t('common.action.confirmAction', { action: t('common.button.unlock') }),
    content: t('common.confirm.unlockContent', { entity: t('entity.user._self'), name: userName }),
    okText: t('common.button.unlock'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await unlock({
          userId: getUserId(record),
          userStatus: 1
        })
        message.success(t('common.msg.actionSuccess', { action: t('common.button.unlock') }))
        loadData()
      } catch (error: any) {
        message.error(error.message || t('common.msg.actionFail', { action: t('common.button.unlock') }))
      } finally {
        loading.value = false
      }
    }
  })
}

// 分配成功回调
const handleAssignSuccess = () => {
  // 刷新数据列表
  loadData()
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 下载导入模板（优先服务端 Content-Disposition，与导出一致）
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getUserTemplate(sheetName, fileName)
}

// 导入文件
const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importUserData(file, sheetName)
}

// 导入成功回调
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  logger.info('[User Management] 导入成功:', result)
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
    const queryParams: any = {}
    
    // 关键字查询
    if (queryKeyword.value) {
      queryParams.KeyWords = queryKeyword.value
    }
    
    // 高级查询
    if (advancedQueryForm.value.UserName) {
      queryParams.UserName = advancedQueryForm.value.UserName
    }
    if (advancedQueryForm.value.UserEmail) {
      queryParams.UserEmail = advancedQueryForm.value.UserEmail
    }
    if (advancedQueryForm.value.UserPhone) {
      queryParams.UserPhone = advancedQueryForm.value.UserPhone
    }
    if (advancedQueryForm.value.UserStatus !== undefined) {
      queryParams.UserStatus = advancedQueryForm.value.UserStatus
    }
    
    // 调用导出 API；本地文件名仅按服务端 Content-Disposition / Content-Type 还原（是否 zip 由后端 TaktExcelHelper 决定）
    const exportMeta = await exportUserData(queryParams, userExcelNames.sheet, userExcelNames.fileBase)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fallbackBase = `${userExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}`
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
    logger.error('[User Management] 导出失败:', error)
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
    UserName: '',
    UserEmail: '',
    UserPhone: '',
    UserStatus: undefined
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

    if (formData.value.userId) {
      // 更新
      const currentUser = dataSource.value.find(u => getUserId(u) === getUserId(formData.value))
      if (currentUser && isAdminUser(currentUser)) {
        const userName = getUserField(currentUser, 'userName') || 'admin'
        message.warning(t('common.action.warnUserCannot', { name: userName, action: t('common.button.update') }))
        formLoading.value = false
        return
      }
      // 构建更新数据，确保所有必需字段都存在
      // 注意：后端使用 Mapster 的 Adapt 方法，如果 passwordHash 为空字符串会覆盖现有密码
      // 因此不包含 passwordHash 字段，让后端保留现有密码
      const updateData: any = {
        userId: formData.value.userId,
        // 雪花ID保持字符串，避免 Number 精度丢失导致后端查不到员工
        employeeId: formValues.employeeId ? String(formValues.employeeId) : '',
        userName: formValues.userName || '',
        nickName: formValues.nickName ?? '',
        userType: formValues.userType ?? 0,
        userEmail: formValues.userEmail || '',
        userPhone: formValues.userPhone || '',
        userStatus: formValues.userStatus ?? 1,
        remark: formValues.remark || '',
        // ID 数组转换为数字类型（后端期望 List<long>），空数组也保留
        roleIds: Array.isArray(formValues.roleIds)
          ? formValues.roleIds.map((id: string | number) => String(id))
          : [],
        deptIds: Array.isArray(formValues.deptIds)
          ? formValues.deptIds.map((id: string | number) => String(id))
          : [],
        postIds: Array.isArray(formValues.postIds)
          ? formValues.postIds.map((id: string | number) => String(id))
          : [],
        tenantIds: Array.isArray(formValues.tenantIds)
          ? formValues.tenantIds.map((id: string | number) => String(id))
          : []
      }
      // 注意：不包含 passwordHash 字段，后端 Mapster Adapt 会跳过不存在的字段，保留现有密码
      
      logger.debug('[User Management] 更新用户数据:', JSON.stringify(updateData, null, 2))
      await updateUser(formData.value.userId, updateData)
      message.success(t('common.msg.updateSuccess'))
    } else {
      const fv = formValues as UserFormValues
      const resolvedEmployeeId = String(fv.employeeId || '')
      const mapIds = (ids: string[] | undefined) => (Array.isArray(ids) ? ids.map((id) => String(id)) : [])
      const createData: UserCreate = {
        employeeId: resolvedEmployeeId,
        userName: fv.userName ?? '',
        nickName: fv.nickName ?? '',
        userType: fv.userType ?? 0,
        userEmail: fv.userEmail ?? '',
        userPhone: fv.userPhone ?? '',
        passwordHash: fv.password ?? '',
        userStatus: fv.userStatus ?? 1,
        remark: fv.remark ?? '',
        roleIds: mapIds(fv.roleIds),
        deptIds: mapIds(fv.deptIds),
        postIds: mapIds(fv.postIds),
        tenantIds: mapIds(fv.tenantIds)
      }
      await createUser(createData)
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
.identity-user {
  padding: 16px;
}
</style>

