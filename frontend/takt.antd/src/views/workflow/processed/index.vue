<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/processed -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：已处理（已完成的流程实例列表），查看流转历史 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <div class="workflow-processed">
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
          <a-button type="link" size="small" @click="openHistory(record)">
            {{ t('workflow.instance.history') }}
          </a-button>
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
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { getList, getHistoryList } from '@/api/workflow/instance'
import type { FlowInstance } from '@/types/workflow/instance'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<FlowInstance[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FlowInstance | null>(null)

const historyVisible = ref(false)
const historyList = ref<Array<{ fromNodeName?: string; toNodeName: string; transitionUserName: string; transitionTime: string; transitionComment?: string; isFinish: number }>>([])

const columns = computed<TableColumnsType>(() => [
  { title: t('entity.flowinstance.instancecode'), dataIndex: 'instanceCode', key: 'instanceCode', width: 160, ellipsis: true },
  { title: t('entity.flowinstance.processtitle'), dataIndex: 'processTitle', key: 'processTitle', width: 160, ellipsis: true },
  { title: t('entity.flowscheme.processname'), dataIndex: 'processName', key: 'processName', width: 120, ellipsis: true },
  { title: t('entity.flowinstance.instancestatus'), dataIndex: 'instanceStatus', key: 'instanceStatus', width: 90 },
  { title: t('common.label.createTime'), dataIndex: 'createTime', key: 'createTime', width: 160 },
  { title: t('common.action.operation'), key: 'action', width: 120, fixed: 'right' }
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
      instanceStatus: 1,
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

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.workflow-processed {
  padding: 0 16px;
}
</style>
