<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/financial/title -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：会计科目管理列表页。分页列表、关键字检索、新增/编辑弹窗、单条与批量删除、Excel 导入导出、列显示设置；表单组件为 `components/accounting-title-form.vue`，权限字面值与 `TaktAccountingTitlesController` 对齐。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="accounting-financial-title">
    <!-- 顶部：关键字检索 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.accountingtitle.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：权限与后端控制器一致 -->
    <TaktToolsBar
      create-permission="accounting:financial:title:create"
      update-permission="accounting:financial:title:update"
      delete-permission="accounting:financial:title:delete"
      import-permission="accounting:financial:title:import"
      template-permission="accounting:financial:title:template"
      export-permission="accounting:financial:title:export"
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
      :row-key="getTitleId"
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
      width="58%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <AccountingTitleForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 导入弹窗 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.accountingtitle._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="excelNames.sheet"
        :template-file-name="excelNames.fileBase"
        template-permission="accounting:financial:title:template"
        import-permission="accounting:financial:title:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('entity.accountingtitle._self') })"
        :upload-text="t('common.action.import.uploadtext')"
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
import type {
  AccountingTitle,
  AccountingTitleCreate,
  AccountingTitleQuery,
  AccountingTitleUpdate
} from '@/types/accounting/financial/title'
import AccountingTitleForm from './components/accounting-title-form.vue'
import {
  createAccountingTitle,
  deleteAccountingTitleById,
  exportAccountingTitleData,
  getAccountingTitleList,
  getAccountingTitleTemplate,
  importAccountingTitleData,
  updateAccountingTitle
} from '@/api/accounting/financial/title'

/**
 * 国际化函数。
 */
const { t } = useI18n()

/**
 * 导入导出默认 sheet/file 名（与后端命名规则一致）。
 */
const excelNames = taktExcelEntityNames('TaktAccountingTitle')

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
const dataSource = ref<AccountingTitle[]>([])

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
const selectedRow = ref<AccountingTitle | null>(null)

/**
 * 当前多选行。
 */
const selectedRows = ref<AccountingTitle[]>([])

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
const formData = ref<Partial<AccountingTitle>>({})

/**
 * 编辑提交 loading。
 */
const formLoading = ref(false)

/**
 * 子表单组件引用。
 */
const formRef = ref<InstanceType<typeof AccountingTitleForm> | null>(null)

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
 * @param {AccountingTitle} record
 * @returns {string}
 */
const getTitleId = (record: AccountingTitle): string => String(record.titleId ?? '')

/**
 * 0/1 转是/否文案。
 *
 * @param {unknown} value
 * @returns {string}
 */
function toYesNoText(value: unknown): string {
  return Number(value) === 1 ? t('common.button.yes') : t('common.button.no')
}

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
 * 列配置（会计科目）。
 */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    key: 'id',
    dataIndex: 'titleId',
    width: 90,
    fixed: 'left'
  },
  {
    title: t('entity.accountingtitle.titlecode'),
    key: 'titleCode',
    dataIndex: 'titleCode',
    width: 140
  },
  {
    title: t('entity.accountingtitle.titlename'),
    key: 'titleName',
    dataIndex: 'titleName',
    width: 180
  },
  {
    title: t('entity.accountingtitle.parentid'),
    key: 'parentId',
    dataIndex: 'parentId',
    width: 120
  },
  {
    title: t('entity.accountingtitle.titletype'),
    key: 'titleType',
    dataIndex: 'titleType',
    width: 120,
    customRender: ({ record }: { record: AccountingTitle }) => t(`accounting.title.page.titletype${record.titleType ?? 0}`)
  },
  {
    title: t('entity.accountingtitle.balancedirection'),
    key: 'balanceDirection',
    dataIndex: 'balanceDirection',
    width: 110,
    customRender: ({ record }: { record: AccountingTitle }) =>
      t(`accounting.title.page.balancedirection${record.balanceDirection ?? 0}`)
  },
  {
    title: t('entity.accountingtitle.isleaf'),
    key: 'isLeaf',
    dataIndex: 'isLeaf',
    width: 90,
    customRender: ({ record }: { record: AccountingTitle }) => toYesNoText(record.isLeaf)
  },
  {
    title: t('entity.accountingtitle.titlestatus'),
    key: 'titleStatus',
    dataIndex: 'titleStatus',
    width: 100,
    customRender: ({ record }: { record: AccountingTitle }) =>
      record.titleStatus === 0 ? t('common.button.enable') : t('common.button.disable')
  },
  {
    title: t('entity.accountingtitle.ordernum'),
    key: 'orderNum',
    dataIndex: 'orderNum',
    width: 100
  },
  CreateActionColumn<AccountingTitle>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'accounting:financial:title:update',
        onClick: (record: Record<string, unknown>) => {
          handleEdit(record as unknown as AccountingTitle)
        }
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'accounting:financial:title:delete',
        onClick: (record: Record<string, unknown>) => {
          handleDeleteOne(record as unknown as AccountingTitle)
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
  onChange: (keys: (string | number)[], rows: AccountingTitle[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  }
}))

/**
 * 行点击切换选中。
 *
 * @param {AccountingTitle} record
 * @returns {{ onClick: () => void }}
 */
const onClickRow = (record: AccountingTitle) => ({
  onClick: () => {
    const key = getTitleId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) {
      selectedRowKeys.value.splice(index, 1)
    } else {
      selectedRowKeys.value.push(key)
    }
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getTitleId(item)))
    selectedRow.value = selectedRows.value.length === 1 ? (selectedRows.value[0] ?? null) : null
  }
})

/**
 * 加载列表数据。
 */
const loadData = async () => {
  try {
    loading.value = true
    const params: AccountingTitleQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) {
      params.keyWords = queryKeyword.value
    }

    const result = await getAccountingTitleList(params)
    dataSource.value = result?.data ?? []
    total.value = Number(result?.total ?? 0)
  } catch (error: unknown) {
    message.error(getErrorMessage(error) || t('common.msg.loadfail'))
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
  formTitle.value = t('common.button.create') + t('entity.accountingtitle._self')
  formData.value = {}
  formVisible.value = true
}

/**
 * 打开编辑弹窗。
 *
 * @param {AccountingTitle} record
 */
const handleEdit = (record: AccountingTitle) => {
  formTitle.value = t('common.button.edit') + t('entity.accountingtitle._self')
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
      t('common.action.warnselecttoaction', {
        action: t('common.button.edit'),
        entity: t('entity.accountingtitle._self')
      })
    )
  }
}

/**
 * 删除单条。
 *
 * @param {AccountingTitle} record
 */
const handleDeleteOne = (record: AccountingTitle) => {
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', {
      entity: t('entity.accountingtitle._self'),
      name: record.titleName ?? ''
    }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAccountingTitleById(getTitleId(record))
        message.success(t('common.msg.deletesuccess'))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail'))
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
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', {
      count: selectedRows.value.length,
      entity: t('entity.accountingtitle._self')
    }),
    onOk: async () => {
      try {
        loading.value = true
        for (const row of selectedRows.value) {
          await deleteAccountingTitleById(getTitleId(row))
        }
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        message.success(t('common.msg.deletesuccess'))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail'))
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
    const values = formRef.value.getValues() as AccountingTitleCreate & { titleId?: string }
    formLoading.value = true

    if (formData.value.titleId) {
      await updateAccountingTitle(String(formData.value.titleId), values as AccountingTitleUpdate)
      message.success(t('common.msg.updatesuccess'))
    } else {
      await createAccountingTitle(values)
      message.success(t('common.msg.createsuccess'))
    }

    formVisible.value = false
    formData.value = {}
    formRef.value.resetFields()
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    message.error(getErrorMessage(error) || t('common.msg.operatefail'))
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
  return await getAccountingTitleTemplate(sheetName, fileName)
}

/**
 * 导入文件上传。
 *
 * @param {File} file
 * @param {string} [sheetName]
 * @returns {Promise<{ success: number; fail: number; errors: string[] }>}
 */
const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAccountingTitleData(file, sheetName)
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
    const query: AccountingTitleQuery = {
      pageIndex: 1,
      pageSize: 100000
    }
    if (queryKeyword.value) query.keyWords = queryKeyword.value

    const blob = await exportAccountingTitleData(query, excelNames.sheet, excelNames.fileBase)
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `${excelNames.fileBase}_${Date.now()}.xlsx`
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportsuccess'))
  } catch (error: unknown) {
    message.error(getErrorMessage(error) || t('common.msg.exportfail'))
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
.accounting-financial-title {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
