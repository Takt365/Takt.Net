<!-- ======================================== -->
<!-- 命名空间：@/views/humanresource/organization/post -->
<!-- 功能描述：岗位管理页面，包含岗位列表、查询、新增、编辑、删除、导入、导出、分配用户等 -->
<!-- ======================================== -->

<template>
  <div class="organization-post">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.post.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="humanresource:organization:post:create"
      update-permission="humanresource:organization:post:update"
      delete-permission="humanresource:organization:post:delete"
      import-permission="humanresource:organization:post:import"
      export-permission="humanresource:organization:post:export"
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

    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getPostId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'postStatus'">
          <TaktDictTag
            :value="getPostField(record, 'postStatus')"
            dict-type="sys_normal_disable"
          />
        </template>
        <template v-else-if="column.key === 'dataScope'">
          {{ getDataScopeLabel(getPostField(record, 'dataScope')) }}
        </template>
      </template>
    </TaktSingleTable>

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
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <PostForm
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
      <a-form-item label="岗位名称">
        <a-input v-model:value="advancedQueryForm.postName" />
      </a-form-item>
      <a-form-item label="岗位编码">
        <a-input v-model:value="advancedQueryForm.postCode" />
      </a-form-item>
      <a-form-item label="岗位状态">
        <TaktSelect
          v-model:value="advancedQueryForm.postStatus"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_normal_disable"
          placeholder="请选择状态"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.post._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="postExcelNames.sheet"
        :template-file-name="postExcelNames.fileBase"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.post._self') })"
        :upload-text="t('common.action.import.uploadText')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <AssignUserToPost
      v-model:open="assignUserVisible"
      :post="currentAssignPost"
      @success="handleAssignSuccess"
    />

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
import PostForm from './components/post-form.vue'
import AssignUserToPost from './components/assign-user-to-post.vue'
import {
  getPostList,
  createPost,
  updatePost,
  deletePostById,
  getPostTemplate,
  importPostData,
  exportPostData
} from '@/api/human-resource/organization/post'
import type { Post } from '@/types/human-resource/organization/post'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine, RiUserLine } from '@remixicon/vue'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Post[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Post | null>(null)
const selectedRows = ref<Post[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('新增岗位')
const formData = ref<Partial<Post>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ postName: string; postCode: string; postStatus?: number }>({
  postName: '',
  postCode: '',
  postStatus: undefined
})
const importVisible = ref(false)
const assignUserVisible = ref(false)
const currentAssignPost = ref<Post | null>(null)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getPostId = (record: any): string => record?.postId != null ? String(record.postId) : (record?.id != null ? String(record.id) : '')
const getPostField = (record: any, field: string): any => record?.[field]

function getDataScopeLabel(v: number | undefined): string {
  const map: Record<number, string> = { 0: '全部数据', 1: '本部门', 2: '本部门及以下', 3: '仅本人', 4: '自定义' }
  return v !== undefined && v !== null ? (map[v] ?? '-') : '-'
}

const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'postId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getPostField(record, 'postId') ?? getPostField(record, 'id') ?? ''
  },
  {
    title: '岗位名称',
    dataIndex: 'postName',
    key: 'postName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: '岗位编码',
    dataIndex: 'postCode',
    key: 'postCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: '部门ID',
    dataIndex: 'deptId',
    key: 'deptId',
    width: 100,
    ellipsis: true
  },
  {
    title: '岗位类别',
    dataIndex: 'postCategory',
    key: 'postCategory',
    width: 100
  },
  {
    title: '岗位级别',
    dataIndex: 'postLevel',
    key: 'postLevel',
    width: 90
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 90
  },
  {
    title: '数据范围',
    dataIndex: 'dataScope',
    key: 'dataScope',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'postStatus',
    key: 'postStatus',
    width: 90
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 160,
    ellipsis: true
  },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'humanresource:organization:post:update', onClick: (record: Post) => handleEdit(record) },
      { key: 'assign-user', label: '分配用户', shape: 'plain', icon: RiUserLine, permission: 'humanresource:organization:post:allocate', onClick: (record: Post) => handleAssignUser(record) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'humanresource:organization:post:delete', onClick: (record: Post) => handleDeleteOne(record) }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getColumnKey = (col: any): string => (col.key || col.dataIndex || col.title) ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Post[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Post, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getPostId(selectedRow.value) === getPostId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Post[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: Post) => ({
  onClick: () => {
    const key = getPostId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getPostId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: any = {
      PageIndex: currentPage.value,
      PageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.postName) params.PostName = advancedQueryForm.value.postName
    if (advancedQueryForm.value.postCode) params.PostCode = advancedQueryForm.value.postCode
    if (advancedQueryForm.value.postStatus !== undefined) params.PostStatus = advancedQueryForm.value.postStatus

    const response = await getPostList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Post] 加载数据失败:', error)
    message.error(error?.message || '加载数据失败')
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

const handleSearch = () => { currentPage.value = 1; loadData() }
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = { postName: '', postCode: '', postStatus: undefined }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[Post] 排序:', sorter.field, sorter.order)
}

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

const handleResizeColumn = (w: number, col: any) => {
  const column = columns.value.find((c: any) => {
    const colKey = col.key || col.dataIndex || col.title
    const cKey = c.key || c.dataIndex || c.title
    return colKey && cKey && String(colKey) === String(cKey)
  })
  if (column) (column as any).width = w
}

const handleCreate = () => {
  formTitle.value = '新增岗位'
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: Post) => {
  formTitle.value = '编辑岗位'
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning('请选择要编辑的岗位')
}

const handleAssignUser = (record: Post) => {
  currentAssignPost.value = record
  assignUserVisible.value = true
}

const handleAssignSuccess = () => {
  loadData()
}

const handleDeleteOne = (record: Post) => {
  const name = getPostField(record, 'postName') || getPostId(record)
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除岗位 "${name}" 吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        await deletePostById(getPostId(record))
        message.success('删除成功')
        loadData()
      } catch (error: any) {
        message.error(error?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的岗位')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 个岗位吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(record => deletePostById(getPostId(record))))
        message.success('删除成功')
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

const handleFormSubmit = async () => {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const formValues = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.postId) {
      await updatePost(formData.value.postId, { ...formValues, postId: formData.value.postId })
      message.success('更新成功')
    } else {
      await createPost(formValues)
      message.success('创建成功')
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: any) {
    if (error?.errorFields) return
    message.error(error?.message || '操作失败')
  } finally {
    formLoading.value = false
  }
}

const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

const handleImport = () => { importVisible.value = true }

const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getPostTemplate(sheetName, fileName)
}

const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importPosts(file, sheetName)
}

const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}

const handleImportCancel = () => { importVisible.value = false }

const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: any = {}
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.postName) queryParams.PostName = advancedQueryForm.value.postName
    if (advancedQueryForm.value.postCode) queryParams.PostCode = advancedQueryForm.value.postCode
    if (advancedQueryForm.value.postStatus !== undefined) queryParams.PostStatus = advancedQueryForm.value.postStatus
    const blob = await exportPostData(queryParams, undefined, '岗位数据')
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `岗位数据_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success('导出成功')
  } catch (error: any) {
    message.error(error?.message || '导出失败')
  } finally {
    loading.value = false
  }
}

const handleAdvancedQuery = () => { advancedQueryVisible.value = true }
const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { postName: '', postCode: '', postStatus: undefined }
}

const handleColumnSetting = () => { columnSettingVisible.value = true }
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}
const handleColumnSettingReset = () => { visibleColumnKeys.value = [] }

const handleRefresh = () => { loadData() }
</script>

<style scoped lang="less">
.organization-post {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
