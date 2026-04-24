<template>
  <div class="routine-announcement">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('common.form.placeholder.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="routine:business:announcement:create"
      update-permission="routine:business:announcement:update"
      delete-permission="routine:business:announcement:delete"
      export-permission="routine:business:announcement:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
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
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getAnnouncementRowKey"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'announcementType'">
          <TaktDictTag
            :value="getAnnouncementDictTagValue(record, 'announcementType')"
            dict-type="sys_notice_type"
          />
        </template>
        <template v-else-if="column.key === 'announcementStatus'">
          <TaktDictTag
            :value="getAnnouncementDictTagValue(record, 'announcementStatus')"
            dict-type="sys_notice_status"
          />
        </template>
        <template v-else-if="column.key === 'isTop'">
          <TaktDictTag
            :value="getAnnouncementDictTagValue(record, 'isTop')"
            dict-type="sys_yes_no"
          />
        </template>
        <template v-else-if="column.key === 'isUrgent'">
          <TaktDictTag
            :value="getAnnouncementDictTagValue(record, 'isUrgent')"
            dict-type="sys_urgency_level"
          />
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
      :width="640"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <AnnouncementForm
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
      <a-form-item :label="t('entity.announcement.announcementcode')">
        <a-input
          v-model:value="advancedQueryForm.announcementCode"
          :placeholder="t('common.form.placeholder.required', { field: t('entity.announcement.announcementcode') })"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.announcement.announcementtitle')">
        <a-input
          v-model:value="advancedQueryForm.announcementTitle"
          :placeholder="t('common.form.placeholder.required', { field: t('entity.announcement.announcementtitle') })"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.announcement.announcementtype')">
        <a-select
          v-model:value="advancedQueryForm.announcementType"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.announcement.announcementtype') })"
          allow-clear
          :options="announcementTypeSelectOptions"
        />
      </a-form-item>
      <a-form-item :label="t('entity.announcement.announcementstatus')">
        <a-select
          v-model:value="advancedQueryForm.announcementStatus"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.announcement.announcementstatus') })"
          allow-clear
          :options="announcementStatusSelectOptions"
        />
      </a-form-item>
    </TaktQueryDrawer>
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'announcementId'"
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
import type { FilterValue } from 'ant-design-vue/es/table/interface'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import AnnouncementForm from './components/announcement-form.vue'
import {
  getAnnouncementList,
  createAnnouncement,
  updateAnnouncement,
  deleteAnnouncement,
  deleteAnnouncementBatch,
  exportAnnouncements
} from '@/api/routine/business/announcement'
import type { Announcement, AnnouncementQuery } from '@/types/routine/business/announcement/announcement'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'

const { t } = useI18n()
const dictDataStore = useDictDataStore()

type AnnouncementTableColumn = TableColumnsType[number]

type TaktTableChangeSorter = {
  field?: string | number | readonly (string | number)[]
  order?: string
}
type TaktTableChangeFilters = Record<string, FilterValue | null>
type TaktTableChangePagination = { current?: number; pageSize?: number; total?: number }

/** 高级查询：编码/标题为 string；类型/状态未选时不出现键（exactOptionalPropertyTypes）。 */
type AnnouncementAdvancedQueryForm = {
  announcementCode: string
  announcementTitle: string
  announcementType?: number
  announcementStatus?: number
}

function emptyAdvancedQueryForm(): AnnouncementAdvancedQueryForm {
  return { announcementCode: '', announcementTitle: '' }
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Announcement[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Announcement | null>(null)
const selectedRows = ref<Announcement[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Announcement>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<AnnouncementAdvancedQueryForm>(emptyAdvancedQueryForm())
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

function announcementDictSelectOptions(dictType: string) {
  return dictDataStore.getDictOptions(dictType).map((o) => ({
    label: o.label,
    value: Number(o.value)
  }))
}

const announcementTypeSelectOptions = computed(() => announcementDictSelectOptions('sys_notice_type'))

const announcementStatusSelectOptions = computed(() => announcementDictSelectOptions('sys_notice_status'))

function getAnnouncementRowKey(record: unknown): string {
  if (record == null || typeof record !== 'object') return ''
  const r = record as Record<string, unknown>
  const id = r['announcementId']
  return id != null && String(id) !== '' ? String(id) : ''
}

function readField(record: unknown, field: string): unknown {
  return record != null && typeof record === 'object' ? (record as Record<string, unknown>)[field] : undefined
}

/** 供 `TaktDictTag` 的 `:value`（与假日列表 `getHolidayDictTagValue` 一致）。 */
function getAnnouncementDictTagValue(record: unknown, field: string): string | number {
  const v = readField(record, field)
  if (typeof v === 'number' && Number.isFinite(v)) return v
  if (typeof v === 'string') return v
  return String(v ?? '')
}

function getAnnouncementColumnKey(col: AnnouncementTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key ?? c.dataIndex ?? c.title
  return key != null && String(key) !== '' ? String(key) : ''
}

const columns = computed<TableColumnsType>(() => [
  {
    title: t('entity.announcement.announcementid'),
    dataIndex: 'announcementId',
    key: 'announcementId',
    width: 120,
    fixed: 'left'
  },
  {
    title: t('entity.announcement.announcementcode'),
    dataIndex: 'announcementCode',
    key: 'announcementCode',
    width: 140,
    ellipsis: true
  },
  {
    title: t('entity.announcement.announcementtitle'),
    dataIndex: 'announcementTitle',
    key: 'announcementTitle',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.announcement.announcementtype'),
    dataIndex: 'announcementType',
    key: 'announcementType',
    width: 90
  },
  {
    title: t('entity.announcement.publishername'),
    dataIndex: 'publisherName',
    key: 'publisherName',
    width: 100
  },
  {
    title: t('entity.announcement.announcementstatus'),
    dataIndex: 'announcementStatus',
    key: 'announcementStatus',
    width: 90
  },
  {
    title: t('entity.announcement.istop'),
    dataIndex: 'isTop',
    key: 'isTop',
    width: 80
  },
  {
    title: t('entity.announcement.isurgent'),
    dataIndex: 'isUrgent',
    key: 'isUrgent',
    width: 90
  },
  {
    title: t('entity.announcement.readcount'),
    dataIndex: 'readCount',
    key: 'readCount',
    width: 90
  },
  {
    title: t('entity.announcement.publishtime'),
    dataIndex: 'publishTime',
    key: 'publishTime',
    width: 160
  },
  {
    title: t('common.entity.createtime'),
    dataIndex: 'createdAt',
    key: 'createdAt',
    width: 160
  },
  CreateActionColumn<Announcement>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:business:announcement:update',
        onClick: (r: Announcement) => handleEdit(r)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:business:announcement:delete',
        onClick: (r: Announcement) => handleDeleteOne(r)
      }
    ]
  })
])

const mergedColumns = computed((): TableColumnsType => mergeDefaultColumns(columns.value, t, true) as TableColumnsType)

const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map((k) => String(k)))
  return merged.filter((col: AnnouncementTableColumn) => {
    const colKey = getAnnouncementColumnKey(col)
    return colKey.length > 0 && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Announcement[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  }
}))

const onClickRow = (record: Announcement) => ({
  onClick: () => {
    const key = getAnnouncementRowKey(record)
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item: Announcement) =>
      selectedRowKeys.value.includes(getAnnouncementRowKey(item))
    )
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

function handleTableChange(
  _pagination: TaktTableChangePagination,
  _filters: TaktTableChangeFilters,
  sorter: TaktTableChangeSorter | TaktTableChangeSorter[]
) {
  const one = Array.isArray(sorter) ? sorter[0] : sorter
  if (one?.order) logger.debug('[Announcement] sort', one.field, one.order)
}

function handleResizeColumn(_w: number, _col: AnnouncementTableColumn) {}

async function loadData() {
  try {
    loading.value = true
    const adv = advancedQueryForm.value
    const params: AnnouncementQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    const kw = (queryKeyword.value ?? '').trim()
    if (kw.length > 0) params.KeyWords = kw
    if (adv.announcementCode.trim().length > 0) params.announcementCode = adv.announcementCode.trim()
    if (adv.announcementTitle.trim().length > 0) params.announcementTitle = adv.announcementTitle.trim()
    if (adv.announcementType !== undefined) params.announcementType = adv.announcementType
    if (adv.announcementStatus !== undefined) params.announcementStatus = adv.announcementStatus

    const res = await getAnnouncementList(params)
    dataSource.value = res?.data ?? []
    total.value = res?.total ?? 0
  } catch (e: unknown) {
    logger.error('[Announcement] loadData error', e)
    const msg = e instanceof Error ? e.message : ''
    message.error(msg || t('routine.business.announcement.messages.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

function handleSearch() {
  currentPage.value = 1
  loadData()
}

function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = emptyAdvancedQueryForm()
  currentPage.value = 1
  loadData()
}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(_current: number, size: number) {
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = t('routine.business.announcement.page.formCreate')
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: Announcement) {
  formTitle.value = t('routine.business.announcement.page.formEdit')
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('routine.business.announcement.messages.selectOne'))
}

function handleDeleteOne(record: Announcement) {
  Modal.confirm({
    title: t('routine.business.announcement.confirm.deleteTitle'),
    content: t('routine.business.announcement.confirm.deleteOneContent', { name: record.announcementTitle }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAnnouncement(getAnnouncementRowKey(record))
        message.success(t('routine.business.announcement.messages.deleteOk'))
        loadData()
      } catch (e: unknown) {
        const msg = e instanceof Error ? e.message : ''
        message.error(msg || t('routine.business.announcement.messages.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('routine.business.announcement.messages.selectRows'))
    return
  }
  Modal.confirm({
    title: t('routine.business.announcement.confirm.deleteTitle'),
    content: t('routine.business.announcement.confirm.deleteBatchContent', {
      count: selectedRows.value.length
    }),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          const first = selectedRows.value[0]
          if (first) await deleteAnnouncement(getAnnouncementRowKey(first))
        } else {
          await deleteAnnouncementBatch(selectedRows.value.map((r) => getAnnouncementRowKey(r)))
        }
        message.success(t('routine.business.announcement.messages.deleteOk'))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: unknown) {
        const msg = e instanceof Error ? e.message : ''
        message.error(msg || t('routine.business.announcement.messages.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

function handleAdvancedQueryReset() {
  advancedQueryForm.value = emptyAdvancedQueryForm()
}

function handleColumnSetting() {
  columnSettingVisible.value = true
}

function handleColumnKeysChange(keys: (string | number)[]) {
  visibleColumnKeys.value = keys.map((k) => String(k))
}

function handleColumnSettingReset() {
  visibleColumnKeys.value = []
}

function handleRefresh() {
  loadData()
}

async function handleExport() {
  try {
    loading.value = true
    const adv = advancedQueryForm.value
    const query: AnnouncementQuery = {
      pageIndex: 1,
      pageSize: 99999
    }
    const kw = (queryKeyword.value ?? '').trim()
    if (kw.length > 0) query.KeyWords = kw
    if (adv.announcementCode.trim().length > 0) query.announcementCode = adv.announcementCode.trim()
    if (adv.announcementTitle.trim().length > 0) query.announcementTitle = adv.announcementTitle.trim()
    if (adv.announcementType !== undefined) query.announcementType = adv.announcementType
    if (adv.announcementStatus !== undefined) query.announcementStatus = adv.announcementStatus

    const blob = await exportAnnouncements(
      query,
      undefined,
      `${t('routine.business.announcement.page.exportFilePrefix')}.xlsx`
    )
    const name = `${t('routine.business.announcement.page.exportFilePrefix')}_${Date.now()}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = name
    link.click()
    window.URL.revokeObjectURL(url)
    message.success(t('routine.business.announcement.messages.exportOk'))
  } catch (e: unknown) {
    const msg = e instanceof Error ? e.message : ''
    message.error(msg || t('routine.business.announcement.messages.exportFail'))
  } finally {
    loading.value = false
  }
}

async function handleFormSubmit() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if (values.announcementId) {
      await updateAnnouncement(values.announcementId, values)
      message.success(t('routine.business.announcement.messages.updateOk'))
    } else {
      await createAnnouncement(values)
      message.success(t('routine.business.announcement.messages.createOk'))
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: unknown) {
    if (typeof e === 'object' && e !== null && 'errorFields' in e) return
    const msg = e instanceof Error ? e.message : ''
    message.error(msg || t('routine.business.announcement.messages.saveFail'))
  } finally {
    formLoading.value = false
  }
}

function handleFormCancel() {
  formVisible.value = false
  formData.value = {}
}

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-announcement {
  padding: 16px;
}
</style>
