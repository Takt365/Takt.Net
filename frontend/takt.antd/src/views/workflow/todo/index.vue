<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/todo -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：待办事项（运行中且待当前用户处理的流程实例列表），审批、流转历史 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="workflow-todo">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('entity.flowinstance.instancecode') + ' / ' + t('entity.flowinstance.processtitle')"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      :show-refresh="true"
      :refresh-loading="loading"
      @refresh="loadData"
    />
    <TaktSingleTable
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :row-key="(r: FlowInstance) => r.instanceId"
      :custom-row="onClickRow"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'instanceStatus'">
          <a-tag :color="getStatusColor(record.instanceStatus)">
            {{ getStatusText(record.instanceStatus) }}
          </a-tag>
        </template>
        <template v-else-if="column.key === 'action'">
          <a-space>
            <a-button type="link" size="small" @click="openApprove(record, true)">
              {{ t('workflow.instance.pass') }}
            </a-button>
            <a-button type="link" size="small" danger @click="openApprove(record, false)">
              {{ t('workflow.instance.reject') }}
            </a-button>
            <a-button type="link" size="small" @click="openFlowDiagram(record)">
              {{ t('workflow.instance.viewDiagram') }}
            </a-button>
            <a-button type="link" size="small" @click="openHistory(record)">
              {{ t('workflow.instance.history') }}
            </a-button>
          </a-space>
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
      v-model:open="approveVisible"
      :title="approvePass ? t('workflow.instance.pass') : t('workflow.instance.reject')"
      :confirm-loading="approveLoading"
      :ok-text="approvePass ? t('workflow.instance.pass') : t('workflow.instance.reject')"
      :ok-button-props="approvePass ? {} : { danger: true }"
      @ok="handleApproveSubmit"
      @cancel="approveVisible = false"
    >
      <a-form layout="vertical">
        <a-form-item :label="t('workflow.instance.comment')">
          <a-textarea v-model:value="approveForm.comment" :rows="3" />
        </a-form-item>
      </a-form>
    </TaktModal>

    <a-drawer v-model:open="historyVisible" :title="t('workflow.instance.history')" width="520">
      <a-list v-if="historyList.length" :data-source="historyList" item-layout="vertical">
        <template #renderItem="{ item }">
          <a-list-item>
            <a-list-item-meta>
              <template #title>
                {{ item.fromNodeName }} → {{ item.toNodeName }}
                <a-tag v-if="item.isFinish === 1" color="green">结束</a-tag>
              </template>
              <template #description>
                {{ item.transitionUserName }} · {{ item.transitionTime }}
                <span v-if="item.transitionComment"> · {{ item.transitionComment }}</span>
              </template>
            </a-list-item-meta>
          </a-list-item>
        </template>
      </a-list>
      <a-empty v-else :description="t('common.msg.noData')" />
    </a-drawer>

    <a-drawer
      v-model:open="flowDiagramVisible"
      :title="t('workflow.instance.viewDiagram')"
      width="72%"
      destroy-on-close
    >
      <div v-if="flowDiagramXml" class="flow-diagram-box">
        <BpmnViewer
          :xml="flowDiagramXml"
          :highlight-node-ids="flowDiagramHighlightIds"
        />
      </div>
      <a-empty v-else-if="!flowDiagramLoading" :description="t('common.msg.noData')" />
      <a-spin v-else :spinning="true" />
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { getList, approve, getHistoryList } from '@/api/workflow/instance'
import { getById as getSchemeById } from '@/api/workflow/scheme'
import type { FlowInstance } from '@/types/workflow/instance'
import BpmnViewer from '../scheme/components/bpmn-viewer.vue'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<FlowInstance[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FlowInstance | null>(null)

const approveVisible = ref(false)
const approveLoading = ref(false)
const approvePass = ref(true)
const currentApproveInstance = ref<FlowInstance | null>(null)
const approveForm = ref<{ comment?: string }>({ comment: '' })

const historyVisible = ref(false)
const historyList = ref<Array<{ fromNodeName?: string; toNodeName: string; transitionUserName: string; transitionTime: string; transitionComment?: string; isFinish: number }>>([])

const flowDiagramVisible = ref(false)
const flowDiagramXml = ref('')
const flowDiagramHighlightIds = ref<string[]>([])
const flowDiagramLoading = ref(false)

const columns = computed<TableColumnsType>(() => [
  { title: t('entity.flowinstance.instancecode'), dataIndex: 'instanceCode', key: 'instanceCode', width: 160, ellipsis: true },
  { title: t('entity.flowinstance.processtitle'), dataIndex: 'processTitle', key: 'processTitle', width: 160, ellipsis: true },
  { title: t('entity.flowscheme.processname'), dataIndex: 'processName', key: 'processName', width: 120, ellipsis: true },
  { title: t('entity.flowinstance.currentnodename'), dataIndex: 'currentNodeName', key: 'currentNodeName', width: 100 },
  { title: t('common.label.createTime'), dataIndex: 'createTime', key: 'createTime', width: 160 },
  { title: t('common.action.operation'), key: 'action', width: 180, fixed: 'right' }
])

function getStatusColor(status: number): string {
  const map: Record<number, string> = { 0: 'processing', 1: 'success', 2: 'error', 3: 'warning', 4: 'default' }
  return map[status] ?? 'default'
}

function getStatusText(status: number): string {
  const map: Record<number, string> = {
    0: t('workflow.instance.statusRunning'),
    1: t('workflow.instance.statusCompleted'),
    2: t('workflow.instance.statusTerminated'),
    3: t('workflow.instance.statusSuspended'),
    4: t('workflow.instance.statusRecalled')
  }
  return map[status] ?? String(status)
}

const onClickRow = (record: FlowInstance) => ({
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
      instanceStatus: 0,
      processKey: queryKeyword.value || undefined
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

function openApprove(record: FlowInstance, pass: boolean) {
  currentApproveInstance.value = record
  approvePass.value = pass
  approveForm.value = { comment: '' }
  approveVisible.value = true
}

async function handleApproveSubmit() {
  if (!currentApproveInstance.value) return
  try {
    approveLoading.value = true
    await approve({
      instanceId: currentApproveInstance.value.instanceId,
      pass: approvePass.value,
      comment: approveForm.value.comment
    })
    message.success(t('workflow.instance.approve') + t('common.msg.success'))
    approveVisible.value = false
    loadData()
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.operateFail'))
  } finally {
    approveLoading.value = false
  }
}

async function openHistory(record: FlowInstance) {
  try {
    const res = await getHistoryList({
      instanceId: record.instanceId,
      pageIndex: 1,
      pageSize: 100
    })
    historyList.value = res?.data ?? []
    historyVisible.value = true
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.loadFail'))
  }
}

async function openFlowDiagram(record: FlowInstance) {
  flowDiagramXml.value = ''
  flowDiagramHighlightIds.value = record.currentNodeId ? [record.currentNodeId] : []
  flowDiagramVisible.value = true
  try {
    flowDiagramLoading.value = true
    const scheme = await getSchemeById(record.schemeId)
    flowDiagramXml.value = scheme?.bpmnXml ?? ''
  } catch (e: unknown) {
    message.error((e as Error)?.message ?? t('common.msg.loadFail'))
  } finally {
    flowDiagramLoading.value = false
  }
}

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.workflow-todo {
  padding: 0 16px;
}
.flow-diagram-box {
  height: 70vh;
  min-height: 400px;
}
</style>
