<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/processed -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：已办列表页面，包含我已处理的流程查询与导出（可与我的流程共用接口，后续可扩展“我参与办结的”接口） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="workflow-processed">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.flowinstance.instancecode'), t('entity.flowinstance.processkey')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      export-permission="workflow:processed:export"
      :show-create="false"
      :show-update="false"
      :show-delete="false"
      :show-refresh="true"
      :show-export="true"
      :show-fullscreen="true"
      :show-advanced-query="false"
      :show-column-setting="false"
      :refresh-loading="loading"
      :export-loading="exportLoading"
      @refresh="loadList"
      @export="handleExport"
    />
    <TaktSingleTable
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getInstanceId"
      @change="handleTableChange"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'instanceStatus'">
          <a-tag :color="statusColor((record as FlowInstance).instanceStatus)">{{ statusText((record as FlowInstance).instanceStatus) }}</a-tag>
        </template>
        <template v-else-if="column.key === 'action'">
          <a-button type="link" size="small" @click="showDetail(asFlowInstance(record))">{{ t('common.button.detail') }}</a-button>
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
      v-model:open="detailVisible"
      :title="t('workflow.instance.detailTitle')"
      width="640px"
      :footer="null"
      :cancel-text="t('common.button.cancel')"
      @cancel="detailVisible = false"
    >
      <FlowInstanceDetailForm :detail="detail" @refresh="reloadInstanceDetail" />
    </TaktModal>
  </div>
</template>

<script setup lang="ts">
/**
 * 已办列表页：我已处理的流程实例列表、分页、详情、导出。
 */
import { ref, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { getMyProcessed, getFlowInstanceById, exportProcessed } from '@/api/workflow/instance'
import FlowInstanceDetailForm from '@/views/workflow/processed/components/flow-instance-detail-form.vue'
import type { FlowInstance, FlowInstanceDetail, FlowInstanceQuery } from '@/types/workflow/instance'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()
const loading = ref(false)
const exportLoading = ref(false)
const queryKeyword = ref('')
const dataSource = ref<FlowInstance[]>([])
const currentPage = ref(1)
const pageSize = ref(10)
const total = ref(0)
const detailVisible = ref(false)
const detail = ref<FlowInstanceDetail | null>(null)

const columns = [
  { title: '实例编码', dataIndex: 'instanceCode', key: 'instanceCode', width: 200 },
  { title: '流程名称', dataIndex: 'processName', key: 'processName', width: 120 },
  { title: '标题', dataIndex: 'processTitle', key: 'processTitle', ellipsis: true },
  { title: '状态', dataIndex: 'instanceStatus', key: 'instanceStatus', width: 90 },
  { title: '发起人', dataIndex: 'startUserName', key: 'startUserName', width: 90 },
  { title: '发起时间', dataIndex: 'startTime', key: 'startTime', width: 170 },
  { title: '操作', key: 'action', width: 80 }
]

/** 实例状态码转展示文案 */
function statusText(s: number) {
  const m: Record<number, string> = { 0: '运行中', 1: '已完成', 2: '已终止', 3: '已挂起', 4: '已撤回' }
  return m[s] ?? '未知'
}

/** 实例状态对应 Tag 颜色 */
function statusColor(s: number) {
  const m: Record<number, string> = { 0: 'processing', 1: 'success', 2: 'error', 3: 'warning', 4: 'default' }
  return m[s] ?? 'default'
}

/** 将 a-table bodyCell 的 record 断言为 FlowInstance */
function asFlowInstance(r: Record<string, unknown>): FlowInstance {
  return r as unknown as FlowInstance
}

/** 实例行 key：取 instanceId 字符串 */
function getInstanceId(record: FlowInstance): string {
  return record?.instanceId != null ? String(record.instanceId) : ''
}

/** 拉取已办列表（分页），结果写入 dataSource 与 total */
async function loadList() {
  loading.value = true
  try {
    const params: FlowInstanceQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value?.trim()) {
      params.processKey = queryKeyword.value.trim()
      params.instanceCode = queryKeyword.value.trim()
    }
    const res = await getMyProcessed(params) as TaktPagedResult<FlowInstance>
    dataSource.value = res.data ?? []
    total.value = res.total ?? 0
  } finally {
    loading.value = false
  }
}

/** 查询：页码置 1 并重新拉取 */
function handleSearch() {
  currentPage.value = 1
  loadList()
}

/** 重置关键词、页码并重新拉取 */
function handleReset() {
  queryKeyword.value = ''
  currentPage.value = 1
  loadList()
}

/** 表格变化占位（分页由 TaktPagination 处理） */
function handleTableChange(_pag: { current?: number; pageSize?: number }) {
  // 分页由 TaktPagination 处理
}

/** 分页变化时更新并拉取 */
function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadList()
}

/** 每页条数变化时更新并拉取 */
function handlePaginationSizeChange(current: number, size: number) {
  currentPage.value = current
  pageSize.value = size
  loadList()
}

/** 拉取实例详情并打开详情弹窗 */
async function showDetail(record: FlowInstance) {
  try {
    detail.value = await getFlowInstanceById(record.instanceId)
    detailVisible.value = true
  } catch {
    message.error('加载详情失败')
  }
}

/** 加签/减签后刷新当前详情（保持弹窗打开） */
async function reloadInstanceDetail() {
  if (!detail.value?.instanceId) return
  try {
    detail.value = await getFlowInstanceById(detail.value.instanceId)
  } catch {
    message.error(t('common.msg.loadFail'))
  }
}

/** 导出已办为 Excel 并触发下载 */
async function handleExport() {
  try {
    exportLoading.value = true
    const params: FlowInstanceQuery = { pageIndex: 1, pageSize: 99999 }
    if (queryKeyword.value?.trim()) {
      params.processKey = queryKeyword.value.trim()
      params.instanceCode = queryKeyword.value.trim()
    }
    const blob = await exportProcessed(params)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `已办_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
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
    exportLoading.value = false
  }
}

onMounted(() => loadList())
</script>

<style scoped lang="less">
.workflow-processed {
  padding: 16px;
}
.history-item {
  font-size: 12px;
  margin-bottom: 4px;
}
</style>
