<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/form -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程表单管理，列表与新建/编辑 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="workflow-form">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('entity.flowform.formcode') + ' / ' + t('entity.flowform.formname')"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="workflow:form:create"
      update-permission="workflow:form:update"
      delete-permission="workflow:form:delete"
      export-permission="workflow:form:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
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
      @export="handleExport"
      @refresh="loadData"
    />
    <TaktSingleTable
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :row-key="(r: FlowForm) => r.formId"
      :custom-row="onClickRow"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'formStatus'">
          <a-tag :color="record.formStatus === 1 ? 'green' : record.formStatus === 2 ? 'red' : 'default'">
            {{ record.formStatus === 1 ? t('workflow.form.statusDeployed') : record.formStatus === 2 ? t('workflow.form.statusRetired') : t('workflow.form.statusDraft') }}
          </a-tag>
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
      @cancel="formVisible = false"
    >
      <FormForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { getList, create, update, remove, exportForms } from '@/api/workflow/form'
import type { FlowForm } from '@/types/workflow/form'
import FormForm from './components/form-form.vue'

const { t } = useI18n()

const formRef = ref<InstanceType<typeof FormForm> | null>(null)
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<FlowForm[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FlowForm | null>(null)
const selectedRows = ref<FlowForm[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<FlowForm> | null>(null)
const formLoading = ref(false)

const columns = computed<TableColumnsType>(() => [
  { title: 'ID', dataIndex: 'formId', key: 'formId', width: 80, ellipsis: true },
  { title: t('entity.flowform.formcode'), dataIndex: 'formCode', key: 'formCode', width: 120, ellipsis: true },
  { title: t('entity.flowform.formname'), dataIndex: 'formName', key: 'formName', width: 160, ellipsis: true },
  { title: t('entity.flowform.formcategory'), dataIndex: 'formCategory', key: 'formCategory', width: 100 },
  { title: t('entity.flowform.formtype'), dataIndex: 'formType', key: 'formType', width: 90 },
  { title: t('entity.flowform.formstatus'), dataIndex: 'formStatus', key: 'formStatus', width: 90 },
  { title: t('entity.flowform.ordernum'), dataIndex: 'orderNum', key: 'orderNum', width: 80 }
])

const onClickRow = (record: FlowForm) => ({
  onClick: () => {
    selectedRow.value = record
    selectedRows.value = selectedRow.value ? [selectedRow.value] : []
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
  formTitle.value = t('common.button.create') + t('entity.flowform._self')
  formData.value = null
  formVisible.value = true
}

function handleUpdate() {
  if (!selectedRow.value) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.flowform._self') }))
    return
  }
  formTitle.value = t('common.button.edit') + t('entity.flowform._self')
  formData.value = { ...selectedRow.value }
  formVisible.value = true
}

async function handleFormSubmit() {
  try {
    await formRef.value?.validate()
  } catch {
    return
  }
  const payload = formRef.value?.getValues()
  if (!payload?.formCode?.trim() || !payload?.formName?.trim()) {
    message.warning(t('common.form.placeholder.input', { field: t('entity.flowform.formcode') + '/' + t('entity.flowform.formname') }))
    return
  }
  try {
    formLoading.value = true
    if (formData.value?.formId) {
      await update(formData.value.formId, { ...payload, formId: formData.value.formId })
      message.success(t('common.msg.updateSuccess', { target: t('entity.flowform._self') }))
    } else {
      await create(payload)
      message.success(t('common.msg.createSuccess', { target: t('entity.flowform._self') }))
    }
    formVisible.value = false
    loadData()
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.operateFail'))
  } finally {
    formLoading.value = false
  }
}

async function handleExport() {
  try {
    loading.value = true
    const query = {
      pageIndex: 1,
      pageSize: 99999,
      key: queryKeyword.value || undefined
    }
    const blob = await exportForms(query, undefined, t('entity.flowform._self') + t('common.action.exportDataSuffix'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.flowform._self')}${t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.flowform._self') }))
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.exportFail', { target: t('entity.flowform._self') }))
  } finally {
    loading.value = false
  }
}

function handleDelete() {
  const rows = selectedRows.value.length ? selectedRows.value : selectedRow.value ? [selectedRow.value] : []
  if (rows.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.flowform._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: rows.length, entity: t('entity.flowform._self') }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(rows.map(r => remove(r.formId)))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.flowform._self') }))
        selectedRow.value = null
        selectedRows.value = []
        loadData()
      } catch (e: unknown) {
        message.error((e as Error)?.message ?? t('common.msg.deleteFail', { target: t('entity.flowform._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.workflow-form {
  padding: 0 16px;
}
</style>
