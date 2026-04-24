<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/file -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：文件管理页面，包含文件列表、查询、上传、下载、删除等功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-file">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('routine.tasks.files.page.listSearch')"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="routine:tasks:file:create"
      update-permission="routine:tasks:file:update"
      delete-permission="routine:tasks:file:delete"
      export-permission="routine:tasks:file:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :create-disabled="false"
      :update-disabled="!selectedRow"
      :delete-disabled="selectedRows.length === 0"
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
      :row-key="getFileRowKey"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'accessUrl'">
          <a
            v-if="record.accessUrl"
            :href="record.accessUrl"
            target="_blank"
            rel="noopener noreferrer"
            style="color: #1890ff; cursor: pointer"
          >
            {{ record.fileName || record.accessUrl }}
          </a>
          <span v-else>-</span>
        </template>
        <template v-else-if="column.key === 'fileStatus'">
          <a-switch
            :checked="record.fileStatus === 0"
            :checked-children="t('routine.tasks.files.switches.fileStatusOn')"
            :un-checked-children="t('routine.tasks.files.switches.fileStatusOff')"
            @change="(checked) => handleFileStatusChange(record, checked)"
          />
        </template>
        <template v-else-if="column.key === 'fileCategory'">
          {{ getCategoryText(record.fileCategory) }}
        </template>
        <template v-else-if="column.key === 'storageType'">
          {{ getStorageTypeText(record.storageType) }}
        </template>
        <template v-else-if="column.key === 'fileSize'">
          {{ formatFileSize(record.fileSize) }}
        </template>
        <template v-else-if="column.key === 'isPublic'">
          <a-switch
            :checked="record.isPublic === 0"
            :checked-children="t('routine.tasks.files.switches.publicOn')"
            :un-checked-children="t('routine.tasks.files.switches.publicOff')"
            :disabled="record.fileStatus !== 0"
            @change="(checked) => handleIsPublicChange(record, checked)"
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

    <!-- 新增/编辑对话框 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      :width="800"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <FileForm
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
      <a-form-item :label="t('entity.file.code')">
        <a-input v-model:value="advancedQueryForm.fileCode" />
      </a-form-item>
      <a-form-item :label="t('entity.file.name')">
        <a-input v-model:value="advancedQueryForm.fileName" />
      </a-form-item>
      <a-form-item :label="t('entity.file.category')">
        <TaktSelect
          v-model:value="advancedQueryForm.fileCategory"
          dict-type="sys_file_category"
          :placeholder="t('routine.tasks.files.placeholders.selectFileCategory')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.file.storagetype')">
        <TaktSelect
          v-model:value="advancedQueryForm.storageType"
          dict-type="sys_storage_type"
          :placeholder="t('routine.tasks.files.placeholders.selectStorageType')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.file.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.fileStatus"
          dict-type="sys_file_status"
          :placeholder="t('routine.tasks.files.placeholders.selectFileStatus')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.file.ispublic')">
        <TaktSelect
          v-model:value="advancedQueryForm.isPublic"
          dict-type="sys_is_public"
          :placeholder="t('routine.tasks.files.placeholders.selectIsPublic')"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 列设置抽屉 -->
    <!-- 审计字段统一在 TaktColumnDrawer 中处理 -->
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'fileId'"
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
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import FileForm from './components/file-form.vue'
import { getFileList, createFile, updateFile, deleteFile, updateFileStatus, download, changeIsPublic, exportFiles } from '@/api/routine/tasks/file'
import type { File, FileCreate } from '@/types/routine/tasks/files/file'
import { RiEditLine, RiDeleteBinLine, RiDownloadLine,  } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'

const { t } = useI18n()
const queryKeyword = ref('')

/** TaktSingleTable 的 rowKey 入参为 TableRecord，与 File 标注不兼容，按 unknown 解析 fileId。 */
const getFileRowKey = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const r = record as Record<string, unknown>
  const id = r['fileId']
  return id != null && String(id) !== '' ? String(id) : ''
}
const loading = ref(false)
const dataSource = ref<File[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<File | null>(null)
const selectedRows = ref<File[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<File>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref({
  fileCode: '',
  fileName: '',
  fileCategory: undefined as number | undefined,
  storageType: undefined as number | undefined,
  fileStatus: undefined as number | undefined,
  isPublic: undefined as number | undefined
})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

// 表格列配置（与 File 接口字段一一对应，字段名为小驼峰）
const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'fileId',
    key: 'fileId',
    width: 120,
    fixed: 'left'
  },
  {
    title: t('entity.file.code'),
    dataIndex: 'fileCode',
    key: 'fileCode',
    width: 150,
    ellipsis: true,
    resizable: true,
    sorter: (a: File, b: File) => {
      const aCode = a.fileCode || ''
      const bCode = b.fileCode || ''
      return aCode.localeCompare(bCode)
    }
  },
  {
    title: t('entity.file.name'),
    dataIndex: 'fileName',
    key: 'fileName',
    width: 180,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.file.originalname'),
    dataIndex: 'fileOriginalName',
    key: 'fileOriginalName',
    width: 180,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.file.path'),
    dataIndex: 'filePath',
    key: 'filePath',
    width: 220,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.file.size'),
    dataIndex: 'fileSize',
    key: 'fileSize',
    width: 120
  },
  {
    title: t('entity.file.mimetype'),
    dataIndex: 'fileType',
    key: 'fileType',
    width: 140,
    ellipsis: true
  },
  {
    title: t('entity.file.extension'),
    dataIndex: 'fileExtension',
    key: 'fileExtension',
    width: 100
  },
  {
    title: t('entity.file.hash'),
    dataIndex: 'fileHash',
    key: 'fileHash',
    width: 220,
    ellipsis: true
  },
  {
    title: t('entity.file.category'),
    dataIndex: 'fileCategory',
    key: 'fileCategory',
    width: 120
  },
  {
    title: t('entity.file.storagetype'),
    dataIndex: 'storageType',
    key: 'storageType',
    width: 120
  },
  {
    title: t('entity.file.storageconfig'),
    dataIndex: 'storageConfig',
    key: 'storageConfig',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.file.accessurl'),
    dataIndex: 'accessUrl',
    key: 'accessUrl',
    width: 220,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.file.downloadcount'),
    dataIndex: 'downloadCount',
    key: 'downloadCount',
    width: 100
  },
  {
    title: t('entity.file.lastdownloadtime'),
    dataIndex: 'lastDownloadTime',
    key: 'lastDownloadTime',
    width: 180
  },
  {
    title: t('entity.file.status'),
    dataIndex: 'fileStatus',
    key: 'fileStatus',
    width: 120
  },
  {
    title: t('entity.file.ispublic'),
    dataIndex: 'isPublic',
    key: 'isPublic',
    width: 120
  },
  {
    title: t('entity.file.accesspermissionconfig'),
    dataIndex: 'accessPermissionConfig',
    key: 'accessPermissionConfig',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.file.description'),
    dataIndex: 'fileDescription',
    key: 'fileDescription',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.file.tags'),
    dataIndex: 'fileTags',
    key: 'fileTags',
    width: 160,
    ellipsis: true
  },
  {
    title: t('entity.file.ipaddress'),
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 160
  },
  {
    title: t('entity.file.location'),
    dataIndex: 'location',
    key: 'location',
    width: 180,
    ellipsis: true
  },
  CreateActionColumn<File>({
    actions: [
      {
        key: 'update',
        label: t('common.action.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:file:update',
        onClick: (record: File) => handleEdit(record)
      },
      {
        key: 'download',
        label: t('common.action.download'),
        shape: 'plain',
        icon: RiDownloadLine,
        permission: 'routine:tasks:file:download',
        onClick: (record: File) => handleDownload(record)
      },
      {
        key: 'delete',
        label: t('common.action.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:file:delete',
        onClick: (record: File) => handleDeleteOne(record)
      }
    ]
  })
])

// 审计字段统一在 TaktColumnDrawer 中处理

// 初始化可见列：TaktColumnDrawer 会自动计算默认的9个列（ID + 7个字段 + 操作列）
const initVisibleColumnKeys = () => {
  // 不需要手动初始化，TaktColumnDrawer 会自动处理
}

// 合并列配置（包含审计字段）- 父组件自己处理，不依赖 TaktColumnDrawer
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


// 行选择配置
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: File[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: File, selected: boolean) => {
    if (selected) {
      selectedRow.value = record
      } else if (selectedRow.value?.fileId === record?.fileId) {
      selectedRow.value = null
    }
  },
  onSelectAll: (selected: boolean, selectedRowsData: File[]) => {
    if (selected) {
      selectedRow.value = selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
    } else {
      selectedRow.value = null
    }
  }
}))

// 行点击处理
const onClickRow = (record: File) => {
  return {
    onClick: () => {
      const key = record.fileId || ''
      const index = selectedRowKeys.value.indexOf(key)
      
      if (index > -1) {
        selectedRowKeys.value.splice(index, 1)
      } else {
        selectedRowKeys.value.push(key)
      }
      
      selectedRows.value = dataSource.value.filter((item: File) =>
        selectedRowKeys.value.includes(item.fileId || '')
      )
      
      if (selectedRowKeys.value.length === 1) {
        selectedRow.value = selectedRows.value[0] ?? null
      } else {
        selectedRow.value = null
      }
      
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
      params.keyWords = queryKeyword.value
    }

    // 高级查询
    if (advancedQueryForm.value.fileCode) {
      params.fileCode = advancedQueryForm.value.fileCode
    }
    if (advancedQueryForm.value.fileName) {
      params.fileName = advancedQueryForm.value.fileName
    }
    if (advancedQueryForm.value.fileCategory !== undefined) {
      params.fileCategory = advancedQueryForm.value.fileCategory
    }
    if (advancedQueryForm.value.storageType !== undefined) {
      params.storageType = advancedQueryForm.value.storageType
    }
    if (advancedQueryForm.value.fileStatus !== undefined) {
      params.fileStatus = advancedQueryForm.value.fileStatus
    }
    if (advancedQueryForm.value.isPublic !== undefined) {
      params.isPublic = advancedQueryForm.value.isPublic
    }

    const response = await getFileList(params)
    
    // 处理响应数据：兼容后端可能返回的 PascalCase 格式
    const responseAny = response as any
    const items = response?.data || responseAny?.Data || []
    const totalCount = response?.total || responseAny?.Total || 0

    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[File Management] 加载数据失败:', error)
    message.error(error.message || t('routine.tasks.files.messages.loadFail'))
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
    fileCode: '',
    fileName: '',
    fileCategory: undefined,
    storageType: undefined,
    fileStatus: undefined,
    isPublic: undefined
  }
  currentPage.value = 1
  loadData()
}

// 表格变化
const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter && sorter.order) {
    logger.debug('[File Management] 排序字段:', sorter.field, '排序方向:', sorter.order)
  }
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

// 分页变化
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

// 分页大小变化
const handlePaginationSizeChange = (current: number, size: number) => {
  currentPage.value = current
  pageSize.value = size
  loadData()
}

// 新增
const handleCreate = () => {
  formTitle.value = t('routine.tasks.files.page.formCreate')
  formData.value = {}
  formVisible.value = true
}

// 编辑
const handleEdit = (record: File) => {
  formTitle.value = t('routine.tasks.files.page.formEdit')
  formData.value = { ...record }
  formVisible.value = true
}

// 更新（工具栏按钮）
const handleUpdate = () => {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
  } else {
    message.warning(t('routine.tasks.files.messages.selectEdit'))
  }
}

// 下载
const handleDownload = async (record: File) => {
  const fileId = record.fileId
  
  if (!fileId) {
    message.warning(t('routine.tasks.files.messages.fileIdMissing'))
    return
  }
  
  try {
    loading.value = true
    
    const blob = await download(fileId)
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = record.fileOriginalName || record.fileName || t('routine.tasks.files.messages.downloadFallback')
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    
    // 延迟清理 URL，确保下载完成
    setTimeout(() => {
      window.URL.revokeObjectURL(url)
    }, 100)
    
    message.success(t('routine.tasks.files.messages.downloadSuccess'))
    // 刷新数据以更新下载次数
    loadData()
  } catch (error: any) {
    logger.error('[File Management] 下载失败:', error)
    message.error(error.message || t('routine.tasks.files.messages.downloadFail'))
  } finally {
    loading.value = false
  }
}

// 切换文件状态（正常/禁用）
const handleFileStatusChange = async (record: File, checked: unknown) => {
  const on = Boolean(checked)
  const oldStatus = record.fileStatus
  const newStatus = on ? 0 : 1 // checked=true 表示正常(0)，checked=false 表示禁用(1)
  
  // 乐观更新：立即更新本地数据
  const fileIndex = dataSource.value.findIndex((f: File) => f.fileId === record.fileId)
  if (fileIndex !== -1) {
    const row = dataSource.value[fileIndex]
    if (row) row.fileStatus = newStatus
  }
  
  try {
    await updateFileStatus({
      fileId: record.fileId,
      fileStatus: newStatus
    })
    message.success(on ? t('routine.tasks.files.messages.statusNormal') : t('routine.tasks.files.messages.statusDisabled'))
    // 可选：刷新数据以确保数据一致性
    // loadData()
  } catch (error: any) {
    logger.error('[File Management] 切换文件状态失败:', error)
    message.error(error.message || t('routine.tasks.files.messages.toggleFail'))
    // 回滚：恢复原始状态
    if (fileIndex !== -1) {
      const row = dataSource.value[fileIndex]
      if (row) row.fileStatus = oldStatus
    }
  }
}

// 切换公开/私有状态
const handleIsPublicChange = async (record: File, checked: unknown) => {
  // 检查文件状态：只有正常状态（0）才允许切换公开/私有
  if (record.fileStatus !== 0) {
    message.warning(t('routine.tasks.files.messages.publicWhenDisabled'))
    return
  }
  
  const on = Boolean(checked)
  const oldIsPublic = record.isPublic
  const newIsPublic = on ? 0 : 1 // checked=true 表示公开(0)，checked=false 表示私有(1)
  
  // 乐观更新：立即更新本地数据
  const fileIndex = dataSource.value.findIndex((f: File) => f.fileId === record.fileId)
  if (fileIndex !== -1) {
    const row = dataSource.value[fileIndex]
    if (row) row.isPublic = newIsPublic
  }
  
  try {
    await changeIsPublic({
      fileId: record.fileId,
      isPublic: newIsPublic
    })
    message.success(on ? t('routine.tasks.files.messages.setPublic') : t('routine.tasks.files.messages.setPrivate'))
    // 可选：刷新数据以确保数据一致性
    // loadData()
  } catch (error: any) {
    logger.error('[File Management] 切换公开/私有状态失败:', error)
    message.error(error.message || t('routine.tasks.files.messages.toggleFail'))
    // 回滚：恢复原始状态
    if (fileIndex !== -1) {
      const row = dataSource.value[fileIndex]
      if (row) row.isPublic = oldIsPublic
    }
  }
}

// 删除单个
const handleDeleteOne = (record: File) => {
  const fileName = record.fileName || t('routine.tasks.files.messages.thisFile')
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.file._self'), name: fileName }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteFile(record.fileId)
        message.success(t('routine.tasks.files.messages.deleteSuccess'))
        loadData()
      } catch (error: any) {
        message.error(error.message || t('routine.tasks.files.messages.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

// 删除（工具栏按钮）
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('routine.tasks.files.messages.selectDelete'))
    return
  }

  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', {
      count: selectedRows.value.length,
      entity: t('entity.file._self')
    }),
    onOk: async () => {
      try {
        loading.value = true
        const deletePromises = selectedRows.value.map((file: File) => deleteFile(file.fileId))
        await Promise.all(deletePromises)
        message.success(t('routine.tasks.files.messages.deleteSuccess'))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error.message || t('routine.tasks.files.messages.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

// 高级查询
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

// 高级查询提交
const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

// 高级查询重置
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = {
    fileCode: '',
    fileName: '',
    fileCategory: undefined,
    storageType: undefined,
    fileStatus: undefined,
    isPublic: undefined
  }
}

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

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    
    // 构建查询参数（使用当前查询条件）
    const queryParams: any = {
      pageIndex: 1,
      pageSize: 10000 // 导出时使用较大的页面大小，或者后端支持不分页导出
    }
    
    // 关键字查询
    if (queryKeyword.value) {
      queryParams.keyWords = queryKeyword.value
    }
    
    // 高级查询
    if (advancedQueryForm.value.fileCode) {
      queryParams.fileCode = advancedQueryForm.value.fileCode
    }
    if (advancedQueryForm.value.fileName) {
      queryParams.fileName = advancedQueryForm.value.fileName
    }
    if (advancedQueryForm.value.fileCategory !== undefined) {
      queryParams.fileCategory = advancedQueryForm.value.fileCategory
    }
    if (advancedQueryForm.value.storageType !== undefined) {
      queryParams.storageType = advancedQueryForm.value.storageType
    }
    if (advancedQueryForm.value.fileStatus !== undefined) {
      queryParams.fileStatus = advancedQueryForm.value.fileStatus
    }
    if (advancedQueryForm.value.isPublic !== undefined) {
      queryParams.isPublic = advancedQueryForm.value.isPublic
    }
    
    const exportLabel = t('routine.tasks.files.page.exportDataLabel')
    const blob = await exportFiles(queryParams, undefined, exportLabel)
    const ts = new Date()
    const padNum = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${exportLabel}_${ts.getFullYear()}${padNum(ts.getMonth() + 1)}${padNum(ts.getDate())}${padNum(ts.getHours())}${padNum(ts.getMinutes())}${padNum(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
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
    
    message.success(t('routine.tasks.files.messages.exportSuccess'))
  } catch (error: any) {
    logger.error('[File Management] 导出失败:', error)
    message.error(error.message || t('routine.tasks.files.messages.exportFail'))
  } finally {
    loading.value = false
  }
}

// 表单提交
const handleFormSubmit = async () => {
  try {
    if (!formRef.value) {
      return
    }

    // 先验证表单
    await formRef.value.validate()
    
    const formValues = formRef.value.getValues()
    const isEdit = !!formData.value.fileId
    
    // 检查是否有文件需要上传（新增时必须上传，编辑时如果有新文件也需要上传）
    const hasFileToUpload = formValues.fileUpload && formValues.fileUpload.length > 0
    
    // 新增时：必须先上传文件，获取 fileCode 等信息
    if (!isEdit && !hasFileToUpload) {
      message.error(t('routine.tasks.files.messages.selectUploadFirst'))
      return
    }
    
    // 如果有文件需要上传，先执行上传操作（upload）
    if (hasFileToUpload) {
      try {
        // 手动上传模式：上传文件到服务器目录
        await formRef.value.uploadFiles()
        // 上传完成后，等待一下确保文件信息已填充到 formState
        await new Promise(resolve => setTimeout(resolve, 300))
        
        // 重新获取表单值，确保包含上传后的 fileCode 等信息
        const updatedValues = formRef.value.getValues()
        
        // 验证文件信息是否已正确填充（新增时必须有 fileCode）
        if (!isEdit && !updatedValues.fileCode) {
          message.error(t('routine.tasks.files.messages.uploadMetaMissing'))
          return
        }
        
        // 使用更新后的表单值
        Object.assign(formValues, updatedValues)
        
        // 调试日志
        logger.debug('[FileSubmit] 文件上传完成，表单值:', {
          ...formValues,
          fileDescriptionValue: formValues.fileDescription,
          fileDescriptionLength: formValues.fileDescription?.length
        })
      } catch (error: any) {
        // 文件上传失败，阻止提交
        logger.error('[FileSubmit] 文件上传失败:', error)
        message.error(error?.message || t('routine.tasks.files.messages.uploadFailRetry'))
        return
      }
    }
    
    // 验证必需字段（新增时必须有的字段）
    if (!isEdit) {
      if (!formValues.fileCode) {
        message.error(t('routine.tasks.files.messages.fileCodeMissing'))
        return
      }
      if (!formValues.filePath) {
        message.error(t('routine.tasks.files.messages.filePathMissing'))
        return
      }
      if (!formValues.fileName) {
        message.error(t('routine.tasks.files.messages.fileNameEmpty'))
        return
      }
    }

    formLoading.value = true

    try {
      if (isEdit) {
        // 更新：使用 update API 更新数据库记录
        const fileId = formData.value.fileId
        if (!fileId) {
          message.error(t('routine.tasks.files.messages.fileIdMissingSubmit'))
          formLoading.value = false
          return
        }
        const updateData = { ...formValues, fileId }
        logger.debug('[FileSubmit] 开始更新文件记录:', updateData)
        await updateFile(String(fileId), updateData)
        message.success(t('routine.tasks.files.messages.updateSuccess'))
      } else {
        // 新增：使用 create API 创建数据库记录
        // 确保所有必需字段都已填充
        const createData = {
          fileCode: formValues.fileCode || '',
          fileName: formValues.fileName || '',
          fileOriginalName: formValues.fileOriginalName || '',
          filePath: formValues.filePath || '',
          fileSize: formValues.fileSize || 0,
          fileType: formValues.fileType,
          fileExtension: formValues.fileExtension,
          fileHash: formValues.fileHash,
          fileCategory: formValues.fileCategory ?? 5,
          storageType: formValues.storageType ?? 0,
          storageConfig: formValues.storageConfig,
          isPublic: formValues.isPublic ?? 0,
          accessPermissionConfig: formValues.accessPermissionConfig,
          fileDescription: formValues.fileDescription || '',
          fileTags: formValues.fileTags || '',
          remark: formValues.remark || ''
        } as FileCreate
        
        // 调试：检查 fileDescription 的值
        logger.debug('[FileSubmit] 开始创建文件记录:', {
          ...createData,
          fileDescriptionLength: createData.fileDescription?.length,
          fileDescriptionValue: createData.fileDescription
        })
        await createFile(createData)
        message.success(t('routine.tasks.files.messages.createSuccess'))
      }

      // 只有 create/update 成功后才关闭对话框并刷新列表
      logger.debug('[FileSubmit] 操作成功，关闭对话框并刷新列表')
      formVisible.value = false
      formData.value = {} // 重置表单数据
      if (formRef.value) {
        formRef.value.resetFields() // 重置表单字段
        formRef.value.clearUploadFiles() // 清空上传组件
      }
      loadData() // 刷新列表
    } catch (createUpdateError: any) {
      // create/update 失败，不关闭对话框，让用户修改后重试
      const responseData = createUpdateError?.response?.data
      const businessCode = createUpdateError?.businessCode
      const errorMessage = createUpdateError?.message
      
      logger.error('[FileSubmit] 保存失败:', {
        error: createUpdateError,
        message: errorMessage,
        businessCode: businessCode,
        response: createUpdateError?.response,
        responseData: responseData,
        responseDataCode: responseData?.code,
        responseDataMessage: responseData?.message,
        responseDataData: responseData?.data,
        status: createUpdateError?.response?.status,
        statusText: createUpdateError?.response?.statusText,
        fullError: JSON.stringify(createUpdateError, null, 2)
      })
      
      // 显示更详细的错误信息
      const displayMessage = responseData?.message || errorMessage || t('routine.tasks.files.messages.saveRetry')
      message.error(displayMessage)
    } finally {
      formLoading.value = false
    }
  } catch (error: any) {
    if (error.errorFields) {
      // 表单验证错误
      return
    }
    message.error(error.message || t('routine.tasks.files.messages.operateFail'))
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
    formRef.value.clearUploadFiles() // 清空上传组件
  }
}

// 文件分类展示（数值枚举文案在 routine.tasks.files.category）
const getCategoryText = (category: number) => {
  const key = `routine.tasks.files.category.${category}` as const
  const translated = t(key)
  return translated === key ? t('routine.tasks.files.category.unknown') : translated
}

// 存储方式展示
const getStorageTypeText = (storageType: number) => {
  const key = `routine.tasks.files.storage.${storageType}` as const
  const translated = t(key)
  return translated === key ? t('routine.tasks.files.storage.unknown') : translated
}

// 格式化文件大小
const formatFileSize = (bytes: number): string => {
  if (bytes === 0) return '0 B'
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB', 'TB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return Math.round(bytes / Math.pow(k, i) * 100) / 100 + ' ' + sizes[i]
}

// 初始化
onMounted(() => {
  initVisibleColumnKeys()
  loadData()
})
</script>

<style scoped lang="less">
.routine-file {
  padding: 16px;
}
</style>
