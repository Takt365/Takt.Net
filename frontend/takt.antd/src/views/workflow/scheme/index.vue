<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/scheme -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程方案管理（流程建模），列表与新建/编辑，含 BPMN/ProcessJson 编辑 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="workflow-scheme">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('entity.flowscheme.processkey') + ' / ' + t('entity.flowscheme.processname')"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="workflow:scheme:create"
      update-permission="workflow:scheme:update"
      delete-permission="workflow:scheme:delete"
      export-permission="workflow:scheme:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-refresh="true"
      :update-disabled="!selectedRow"
      :delete-disabled="!selectedRow"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @export="handleExport"
      @refresh="loadData"
    />
    <TaktSingleTable
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :row-key="(r: FlowScheme) => r.schemeId"
      :custom-row="onClickRow"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'processStatus'">
          <a-tag :color="getStatusColor(record.processStatus)">
            {{ getStatusText(record.processStatus) }}
          </a-tag>
        </template>
        <template v-else-if="column.key === 'action'">
          <a-space v-if="record.processStatus === 0">
            <a-button type="link" size="small" :loading="statusLoading === record.schemeId" @click.stop="handleDeploy(record)">
              {{ t('workflow.scheme.deploy') }}
            </a-button>
          </a-space>
          <a-space v-else-if="record.processStatus === 1">
            <a-button type="link" size="small" danger :loading="statusLoading === record.schemeId" @click.stop="handleRetire(record)">
              {{ t('workflow.scheme.retire') }}
            </a-button>
          </a-space>
          <span v-else>—</span>
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
      width="720px"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <SchemeForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import SchemeForm from './components/scheme-form.vue'
import { message, Modal } from 'ant-design-vue'
import { getList, create, update, getById, remove, exportSchemes, updateStatus } from '@/api/workflow/scheme'
import type { FlowScheme } from '@/types/workflow/scheme'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<FlowScheme[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FlowScheme | null>(null)
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<FlowScheme> | null>(null)
const formLoading = ref(false)
const formRef = ref<InstanceType<typeof SchemeForm>>()
const statusLoading = ref<string | null>(null)

const columns = computed<TableColumnsType>(() => [
  { title: 'ID', dataIndex: 'schemeId', key: 'schemeId', width: 80, ellipsis: true },
  { title: t('entity.flowscheme.processkey'), dataIndex: 'processKey', key: 'processKey', width: 140, ellipsis: true },
  { title: t('entity.flowscheme.processname'), dataIndex: 'processName', key: 'processName', width: 160, ellipsis: true },
  { title: t('entity.flowscheme.processcategory'), dataIndex: 'processCategory', key: 'processCategory', width: 100 },
  { title: t('entity.flowscheme.processversion'), dataIndex: 'processVersion', key: 'processVersion', width: 90 },
  { title: t('entity.flowscheme.processstatus'), dataIndex: 'processStatus', key: 'processStatus', width: 90 },
  { title: t('entity.flowscheme.ordernum'), dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  { title: t('common.action.operation'), key: 'action', width: 100, fixed: 'right' }
])

function getStatusColor(status: number): string {
  const map: Record<number, string> = { 0: 'default', 1: 'green', 2: 'red' }
  return map[status] ?? 'default'
}

function getStatusText(status: number): string {
  const map: Record<number, string> = {
    0: t('workflow.scheme.statusDraft'),
    1: t('workflow.scheme.statusDeployed'),
    2: t('workflow.scheme.statusRetired')
  }
  return map[status] ?? String(status)
}

const onClickRow = (record: FlowScheme) => ({
  onClick: () => {
    selectedRow.value = record
  }
})

async function loadData() {
  try {
    loading.value = true
    const res = await getList({
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      key: queryKeyword.value || undefined
    })
    dataSource.value = res?.data ?? []
    total.value = res?.total ?? 0
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.loadFail'))
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
  currentPage.value = 1
  loadData()
}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(_current: number, size: number) {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = t('common.button.create') + t('entity.flowscheme._self')
  formData.value = null
  formVisible.value = true
}

async function handleUpdate() {
  if (!selectedRow.value) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.flowscheme._self') }))
    return
  }
  try {
    const detail = await getById(selectedRow.value.schemeId)
    formTitle.value = t('common.button.edit') + t('entity.flowscheme._self')
    formData.value = detail ?? selectedRow.value
    formVisible.value = true
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.loadFail'))
  }
}

async function handleFormSubmit() {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.schemeId) {
      await update(formData.value.schemeId, { ...values, schemeId: formData.value.schemeId })
      message.success(t('common.msg.updateSuccess', { target: t('entity.flowscheme._self') }))
    } else {
      await create(values)
      message.success(t('common.msg.createSuccess', { target: t('entity.flowscheme._self') }))
    }
    formVisible.value = false
    loadData()
  } catch (e: unknown) {
    if ((e as Error)?.message) message.error((e as Error).message)
  } finally {
    formLoading.value = false
  }
}

function handleFormCancel() {
  formVisible.value = false
}

/** 发布流程方案（草稿 → 已发布） */
async function handleDeploy(row: FlowScheme) {
  try {
    statusLoading.value = row.schemeId
    await updateStatus({ schemeId: row.schemeId, processStatus: 1 })
    message.success(t('workflow.scheme.deploySuccess'))
    loadData()
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.operateFail'))
  } finally {
    statusLoading.value = null
  }
}

/** 停用流程方案（已发布 → 已停用） */
async function handleRetire(row: FlowScheme) {
  Modal.confirm({
    title: t('workflow.scheme.retireConfirmTitle'),
    content: t('workflow.scheme.retireConfirmContent', { name: row.processName || row.processKey }),
    okText: t('common.button.confirm'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        statusLoading.value = row.schemeId
        await updateStatus({ schemeId: row.schemeId, processStatus: 2 })
        message.success(t('workflow.scheme.retireSuccess'))
        loadData()
      } catch (e: unknown) {
        message.error((e as Error)?.message ?? t('common.msg.operateFail'))
      } finally {
        statusLoading.value = null
      }
    }
  })
}

function handleDelete() {
  if (!selectedRow.value) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.flowscheme._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.flowscheme._self'), name: selectedRow.value?.processName || selectedRow.value?.processKey || '' }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await remove(selectedRow.value!.schemeId)
        message.success(t('common.msg.deleteSuccess', { target: t('entity.flowscheme._self') }))
        selectedRow.value = null
        loadData()
      } catch (e: unknown) {
        message.error((e as Error)?.message ?? t('common.msg.deleteFail', { target: t('entity.flowscheme._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

async function handleExport() {
  try {
    loading.value = true
    const query = {
      pageIndex: 1,
      pageSize: 99999,
      key: queryKeyword.value || undefined
    }
    const blob = await exportSchemes(query, undefined, t('entity.flowscheme._self') + t('common.action.exportDataSuffix'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.flowscheme._self')}${t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.flowscheme._self') }))
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.exportFail', { target: t('entity.flowscheme._self') }))
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.workflow-scheme {
  padding: 0 16px;
}
</style>
