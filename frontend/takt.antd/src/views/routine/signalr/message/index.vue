<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/signalr/message -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：在线消息列表，查询、新增、编辑、删除、导出 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-signalr-message">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="发送者、接收者、标题、内容"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="routine:message:create"
      update-permission="routine:message:update"
      delete-permission="routine:message:delete"
      export-permission="routine:message:export"
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
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="(r: any) => r.messageId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'readStatus'">
          <a-tag :color="record.readStatus === 1 ? 'green' : 'default'">
            {{ record.readStatus === 1 ? '已读' : '未读' }}
          </a-tag>
        </template>
        <template v-else-if="column.key === 'messageContent'">
          <span :title="record.messageContent" style="max-width: 200px; display: inline-block; overflow: hidden; text-overflow: ellipsis; white-space: nowrap">{{ record.messageContent }}</span>
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
      <a-form ref="formRef" :model="formState" :label-col="{ span: 6 }" :wrapper-col="{ span: 17 }">
        <a-form-item label="发送者用户名" name="fromUserName" :rules="[{ required: true, message: '请输入发送者用户名' }]">
          <a-input v-model:value="formState.fromUserName" placeholder="发送者用户名" allow-clear />
        </a-form-item>
        <a-form-item label="接收者用户名" name="toUserName" :rules="[{ required: true, message: '请输入接收者用户名' }]">
          <a-input v-model:value="formState.toUserName" placeholder="接收者用户名" allow-clear />
        </a-form-item>
        <a-form-item label="消息标题" name="messageTitle">
          <a-input v-model:value="formState.messageTitle" placeholder="消息标题" allow-clear />
        </a-form-item>
        <a-form-item label="消息内容" name="messageContent" :rules="[{ required: true, message: '请输入消息内容' }]">
          <a-textarea v-model:value="formState.messageContent" :rows="3" placeholder="消息内容" allow-clear />
        </a-form-item>
        <a-form-item label="消息类型" name="messageType" :rules="[{ required: true, message: '请选择消息类型' }]">
          <a-select v-model:value="formState.messageType" placeholder="请选择" allow-clear style="width: 100%">
            <a-select-option value="Text">文本</a-select-option>
            <a-select-option value="Image">图片</a-select-option>
            <a-select-option value="File">文件</a-select-option>
            <a-select-option value="System">系统消息</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="消息分组" name="messageGroup">
          <a-select v-model:value="formState.messageGroup" placeholder="请选择" allow-clear style="width: 100%">
            <a-select-option value="Chat">聊天</a-select-option>
            <a-select-option value="Notification">通知</a-select-option>
            <a-select-option value="Alert">提醒</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="读取状态" name="readStatus">
          <a-radio-group v-model:value="formState.readStatus">
            <a-radio :value="0">未读</a-radio>
            <a-radio :value="1">已读</a-radio>
          </a-radio-group>
        </a-form-item>
        <a-form-item label="发送时间" name="sendTime">
          <a-date-picker v-model:value="formState.sendTime" show-time value-format="YYYY-MM-DD HH:mm:ss" style="width: 100%" />
        </a-form-item>
      </a-form>
    </TaktModal>
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item label="发送者用户名">
        <a-input v-model:value="advancedQueryForm.fromUserName" placeholder="发送者用户名" allow-clear />
      </a-form-item>
      <a-form-item label="接收者用户名">
        <a-input v-model:value="advancedQueryForm.toUserName" placeholder="接收者用户名" allow-clear />
      </a-form-item>
      <a-form-item label="消息类型">
        <a-select v-model:value="advancedQueryForm.messageType" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option value="Text">文本</a-select-option>
          <a-select-option value="Image">图片</a-select-option>
          <a-select-option value="File">文件</a-select-option>
          <a-select-option value="System">系统消息</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="消息分组">
        <a-select v-model:value="advancedQueryForm.messageGroup" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option value="Chat">聊天</a-select-option>
          <a-select-option value="Notification">通知</a-select-option>
          <a-select-option value="Alert">提醒</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="读取状态">
        <a-select v-model:value="advancedQueryForm.readStatus" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option :value="0">未读</a-select-option>
          <a-select-option :value="1">已读</a-select-option>
        </a-select>
      </a-form-item>
    </TaktQueryDrawer>
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'messageId'"
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
import type { FormInstance } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { getList, getById, create, update, remove, exportMessage } from '@/api/routine/signalr/message'
import type { Message, MessageQuery, MessageCreate, MessageUpdate } from '@/types/routine/signalr/message'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Message[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Message | null>(null)
const selectedRows = ref<Message[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('新增消息')
const formLoading = ref(false)
const formRef = ref<FormInstance>()
const formState = ref<Partial<MessageCreate & { messageId?: string }>>({
  fromUserName: '',
  toUserName: '',
  messageTitle: '',
  messageContent: '',
  messageType: 'Text',
  messageGroup: 'Notification',
  readStatus: 0,
  sendTime: undefined
})
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<Partial<MessageQuery>>({ pageIndex: 1, pageSize: 20 })
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const columns = ref<TableColumnsType>([
  { title: 'ID', dataIndex: 'messageId', key: 'messageId', width: 120, fixed: 'left' },
  { title: '发送者', dataIndex: 'fromUserName', key: 'fromUserName', width: 100 },
  { title: '接收者', dataIndex: 'toUserName', key: 'toUserName', width: 100 },
  { title: '消息标题', dataIndex: 'messageTitle', key: 'messageTitle', width: 140, ellipsis: true },
  { title: '消息内容', dataIndex: 'messageContent', key: 'messageContent', width: 200, ellipsis: true },
  { title: '消息类型', dataIndex: 'messageType', key: 'messageType', width: 90 },
  { title: '消息分组', dataIndex: 'messageGroup', key: 'messageGroup', width: 90 },
  { title: '读取状态', dataIndex: 'readStatus', key: 'readStatus', width: 90 },
  { title: '发送时间', dataIndex: 'sendTime', key: 'sendTime', width: 160 },
  { title: '读取时间', dataIndex: 'readTime', key: 'readTime', width: 160 },
  { title: '创建时间', dataIndex: 'createTime', key: 'createTime', width: 160 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'routine:message:update', onClick: (r: Message) => handleEdit(r) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:message:delete', onClick: (r: Message) => handleDeleteOne(r) }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getKey = (col: any) => String(col.key || col.dataIndex || col.title || '')
  return merged.filter((col: any) => new Set(keys.map(k => String(k))).has(getKey(col)))
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Message[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Message, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.messageId === record?.messageId) selectedRow.value = null
  },
  onSelectAll: (_: boolean, rows: Message[]) => { selectedRow.value = rows.length === 1 ? rows[0] : null }
}))

const onClickRow = (record: Message) => ({
  onClick: () => {
    const id = String(record?.messageId ?? '')
    const idx = selectedRowKeys.value.indexOf(id)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(id)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.messageId ?? ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: MessageQuery = { pageIndex: currentPage.value, pageSize: pageSize.value }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.fromUserName) params.fromUserName = advancedQueryForm.value.fromUserName
    if (advancedQueryForm.value.toUserName) params.toUserName = advancedQueryForm.value.toUserName
    if (advancedQueryForm.value.messageType) params.messageType = advancedQueryForm.value.messageType
    if (advancedQueryForm.value.messageGroup) params.messageGroup = advancedQueryForm.value.messageGroup
    if (advancedQueryForm.value.readStatus !== undefined) params.readStatus = advancedQueryForm.value.readStatus

    const res = await getList(params) as any
    const items = res?.data ?? res?.Data ?? []
    const totalCount = res?.total ?? res?.Total ?? 0
    dataSource.value = Array.isArray(items) ? items : []
    total.value = Number(totalCount)
  } catch (e: any) {
    logger.error('[Message] 加载失败:', e)
    message.error(e?.message || '加载失败')
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

function handleSearch() { currentPage.value = 1; loadData() }
function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = { pageIndex: 1, pageSize: pageSize.value }
  currentPage.value = 1
  loadData()
}
function handleTableChange(_p: any, _f: any, _s: any) {}
function handleResizeColumn(_w: number, _col: any) {}
function handlePaginationChange(page: number, size: number) { currentPage.value = page; pageSize.value = size; loadData() }
function handlePaginationSizeChange(_: number, size: number) { pageSize.value = size; loadData() }

function handleCreate() {
  formTitle.value = '新增消息'
  formState.value = {
    fromUserName: '',
    toUserName: '',
    messageTitle: '',
    messageContent: '',
    messageType: 'Text',
    messageGroup: 'Notification',
    readStatus: 0,
    sendTime: undefined
  }
  formVisible.value = true
}

function handleEdit(record: Message) {
  selectedRow.value = record
  handleUpdate()
}

async function handleUpdate() {
  if (!selectedRow.value?.messageId) {
    message.warning('请选择一条记录')
    return
  }
  try {
    formLoading.value = true
    const detail = await getById(selectedRow.value.messageId)
    formTitle.value = '编辑消息'
    formState.value = {
      messageId: detail.messageId,
      fromUserName: detail.fromUserName,
      toUserName: detail.toUserName,
      messageTitle: detail.messageTitle,
      messageContent: detail.messageContent,
      messageType: detail.messageType,
      messageGroup: detail.messageGroup,
      readStatus: detail.readStatus,
      sendTime: detail.sendTime
    }
    formVisible.value = true
  } catch (e: any) {
    message.error(e?.message || '获取详情失败')
  } finally {
    formLoading.value = false
  }
}

function handleFormSubmit() {
  formRef.value?.validate().then(async () => {
    try {
      formLoading.value = true
      if (formState.value.messageId) {
        await update(formState.value.messageId, {
          fromUserName: formState.value.fromUserName!,
          toUserName: formState.value.toUserName!,
          messageTitle: formState.value.messageTitle,
          messageContent: formState.value.messageContent!,
          messageType: formState.value.messageType!,
          messageGroup: formState.value.messageGroup,
          readStatus: formState.value.readStatus ?? 0,
          sendTime: formState.value.sendTime
        } as MessageUpdate)
        message.success('修改成功')
      } else {
        await create({
          fromUserName: formState.value.fromUserName!,
          toUserName: formState.value.toUserName!,
          messageTitle: formState.value.messageTitle,
          messageContent: formState.value.messageContent!,
          messageType: formState.value.messageType!,
          messageGroup: formState.value.messageGroup,
          readStatus: formState.value.readStatus ?? 0,
          sendTime: formState.value.sendTime
        } as MessageCreate)
        message.success('新增成功')
      }
      formVisible.value = false
      loadData()
    } catch (e: any) {
      message.error(e?.message || '保存失败')
    } finally {
      formLoading.value = false
    }
  }).catch(() => {})
}

function handleFormCancel() { formVisible.value = false }

function handleDeleteOne(record: Message) {
  Modal.confirm({
    title: '确认删除',
    content: '确定要删除该消息吗？',
    onOk: async () => {
      try {
        loading.value = true
        await remove(record.messageId)
        message.success('已删除')
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的记录')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 条消息吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(r => remove(r.messageId)))
        message.success('已删除')
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

async function handleExport() {
  try {
    loading.value = true
    const query: MessageQuery = { pageIndex: 1, pageSize: 99999 }
    if (queryKeyword.value) query.keyWords = queryKeyword.value
    if (advancedQueryForm.value.fromUserName) query.fromUserName = advancedQueryForm.value.fromUserName
    if (advancedQueryForm.value.toUserName) query.toUserName = advancedQueryForm.value.toUserName
    if (advancedQueryForm.value.messageType) query.messageType = advancedQueryForm.value.messageType
    if (advancedQueryForm.value.messageGroup) query.messageGroup = advancedQueryForm.value.messageGroup
    if (advancedQueryForm.value.readStatus !== undefined) query.readStatus = advancedQueryForm.value.readStatus

    const blob = await exportMessage(query, undefined, '在线消息')
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `在线消息_${new Date().toISOString().slice(0, 19).replace(/[-:T]/g, '')}.xlsx`
    link.click()
    window.URL.revokeObjectURL(url)
    message.success('导出成功')
  } catch (e: any) {
    logger.error('[Message] 导出失败:', e)
    message.error(e?.message || '导出失败')
  } finally {
    loading.value = false
  }
}

function handleAdvancedQuery() { advancedQueryVisible.value = true }
function handleAdvancedQuerySubmit() { currentPage.value = 1; loadData(); advancedQueryVisible.value = false }
function handleAdvancedQueryReset() { advancedQueryForm.value = { pageIndex: 1, pageSize: pageSize.value } }
function handleColumnSetting() { columnSettingVisible.value = true }
function handleColumnKeysChange(keys: (string | number)[]) { visibleColumnKeys.value = keys.map(k => String(k)) }
function handleColumnSettingReset() { visibleColumnKeys.value = [] }
function handleRefresh() { loadData() }

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-signalr-message {
  padding: 16px;
}
</style>
