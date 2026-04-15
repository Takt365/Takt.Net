<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/todo -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：待办列表页面，包含查询、办结（通过/驳回）、转办、加签、导出等 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="workflow-todo">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.flowinstance.processname'), t('entity.flowinstance.processtitle')].join(t('common.action.or')) })"
      :loading="loading"
      @search="loadTodo"
      @reset="handleReset"
    />
    <TaktToolsBar
      export-permission="workflow:todo:export"
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
      @refresh="loadTodo"
      @export="handleExport"
    />
    <TaktSingleTable
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getTodoRowKey"
      @change="handleTableChange"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <a-space wrap>
            <a-button type="link" size="small" @click="handleApprove(asFlowTodoItem(record), true)">通过</a-button>
            <a-button type="link" size="small" danger @click="handleApprove(asFlowTodoItem(record), false)">驳回</a-button>
            <a-button type="link" size="small" @click="openTransfer(asFlowTodoItem(record))">{{ t('workflow.instance.transfer') }}</a-button>
            <a-button type="link" size="small" @click="openAddSign(asFlowTodoItem(record))">{{ t('workflow.instance.addSign') }}</a-button>
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
      v-model:open="modalVisible"
      :title="t('workflow.instance.approveTitle')"
      :confirm-loading="loading"
      :ok-text="t('common.button.submit')"
      :cancel-text="t('common.button.cancel')"
      width="720px"
      @ok="handleApproveOk"
      @cancel="closeApproveModal"
    >
      <div class="todo-modal__sections">
        <div class="todo-modal__section">
          <div class="todo-modal__section-title">{{ t('workflow.instance.taskFormContent') }}</div>
          <TaskFormContent :detail="taskDetail" />
          <FlowPendingAddApproversPanel
            :detail="taskDetail"
            :allow-reduce="!!taskDetail?.canVerify"
            @refresh="reloadTaskDetailInModal"
          />
        </div>
        <div v-if="taskDetail?.currentNodeName === CASHIER_ROUTE_NODE_ID" class="todo-modal__section">
          <div class="todo-modal__section-title">{{ t('workflow.instance.cashierPayoutMethod') }}</div>
          <a-select
            v-model:value="cashierPayoutChannel"
            :options="cashierPayoutOptions"
            allow-clear
            :placeholder="t('workflow.instance.cashierPayoutRequired')"
            style="width: 100%"
          />
        </div>
        <div class="todo-modal__section">
          <div class="todo-modal__section-title">{{ t('workflow.instance.taskApproveAction') }}</div>
          <ApproveForm ref="approveFormRef" :form="completeForm" />
        </div>
      </div>
    </TaktModal>
    <TaktModal
      v-model:open="transferVisible"
      :title="t('workflow.instance.transfer')"
      :confirm-loading="loading"
      :ok-text="t('common.button.submit')"
      :cancel-text="t('common.button.cancel')"
      width="720px"
      @ok="handleTransferOk"
      @cancel="closeTransferModal"
    >
      <div class="todo-modal__sections">
        <div class="todo-modal__section">
          <div class="todo-modal__section-title">{{ t('workflow.instance.taskFormContent') }}</div>
          <TaskFormContent :detail="taskDetail" />
          <FlowPendingAddApproversPanel
            :detail="taskDetail"
            :allow-reduce="!!taskDetail?.canVerify"
            @refresh="reloadTaskDetailInModal"
          />
        </div>
        <div class="todo-modal__section">
          <div class="todo-modal__section-title">{{ t('workflow.instance.taskApproveAction') }}</div>
          <TransferForm ref="transferFormRef" :form="transferForm" :user-options="userOptions" />
        </div>
      </div>
    </TaktModal>
    <TaktModal
      v-model:open="addSignVisible"
      :title="t('workflow.instance.addSign')"
      :confirm-loading="loading"
      :ok-text="t('common.button.submit')"
      :cancel-text="t('common.button.cancel')"
      width="720px"
      @ok="handleAddSignOk"
      @cancel="closeAddSignModal"
    >
      <div class="todo-modal__sections">
        <div class="todo-modal__section">
          <div class="todo-modal__section-title">{{ t('workflow.instance.taskFormContent') }}</div>
          <TaskFormContent :detail="taskDetail" />
          <FlowPendingAddApproversPanel
            :detail="taskDetail"
            :allow-reduce="!!taskDetail?.canVerify"
            @refresh="reloadTaskDetailInModal"
          />
        </div>
        <div class="todo-modal__section">
          <div class="todo-modal__section-title">{{ t('workflow.instance.taskApproveAction') }}</div>
          <AddSignForm ref="addSignFormRef" :form="addSignForm" :user-options="userOptions" />
        </div>
      </div>
    </TaktModal>
  </div>
</template>

<script setup lang="ts">
/**
 * 待办列表页：拉取待办、分页、办结（通过/驳回）、转办、加签；弹窗内展示任务内容与审批/转办/加签表单。
 */
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { getMyTodo, getFlowInstanceById, complete, exportTodo, transfer, addApprovers } from '@/api/workflow/instance'
import { getUserOptions } from '@/api/identity/user'
import ApproveForm from './components/flow-approve-form.vue'
import TransferForm from './components/flow-transfer-form.vue'
import AddSignForm from './components/flow-add-sign-form.vue'
import TaskFormContent from './components/flow-task-form-content.vue'
import FlowPendingAddApproversPanel from '@/views/workflow/components/flow-pending-add-approvers-panel.vue'
import type { FlowTodoItem, FlowTodoQuery, FlowInstanceDetail } from '@/types/workflow/instance'
import type { FlowAddApproverItem } from '@/types/workflow/instance'
import type { TaktPagedResult } from '@/types/common'
import type { TaktSelectOption } from '@/types/common'

const { t } = useI18n()

/** 与种子 ProcessContent 节点 id 一致：出纳确认付款方式 */
const CASHIER_ROUTE_NODE_ID = 'cashier_route'

const cashierPayoutChannel = ref<number | undefined>(undefined)
const cashierPayoutOptions = computed(() => [
  { value: 1, label: t('workflow.instance.cashierPayoutBank') },
  { value: 2, label: t('workflow.instance.cashierPayoutCash') },
  { value: 3, label: t('workflow.instance.cashierPayoutRepay') }
])

const loading = ref(false)
const exportLoading = ref(false)
const queryKeyword = ref('')
const dataSource = ref<FlowTodoItem[]>([])
const currentPage = ref(1)
const pageSize = ref(10)
const total = ref(0)

const columns = [
  { title: '流程名称', dataIndex: 'processName', key: 'processName', width: 120 },
  { title: '标题', dataIndex: 'processTitle', key: 'processTitle', ellipsis: true },
  { title: '当前节点', dataIndex: 'nodeName', key: 'nodeName', width: 100 },
  { title: '发起人', dataIndex: 'startUserName', key: 'startUserName', width: 90 },
  { title: '发起时间', dataIndex: 'startTime', key: 'startTime', width: 170 },
  { title: '操作', key: 'action', width: 260, fixed: 'right' as const }
]

/** 将 a-table bodyCell 的 record 断言为 FlowTodoItem */
function asFlowTodoItem(r: Record<string, unknown>): FlowTodoItem {
  return r as unknown as FlowTodoItem
}

/** 待办行 key：取 instanceId 字符串 */
function getTodoRowKey(record: FlowTodoItem): string {
  return record?.instanceId != null ? String(record.instanceId) : ''
}

const modalVisible = ref(false)
const currentTask = ref<FlowTodoItem | null>(null)
const taskDetail = ref<FlowInstanceDetail | null>(null)
const approveFormRef = ref<InstanceType<typeof ApproveForm> | null>(null)
const completeForm = reactive({
  comment: '',
  approved: true,
  nodeRejectStep: undefined as string | undefined
})

const userOptions = ref<TaktSelectOption[]>([])
const transferVisible = ref(false)
const addSignVisible = ref(false)
const currentTransferTask = ref<FlowTodoItem | null>(null)
const currentAddSignTask = ref<FlowTodoItem | null>(null)
const transferFormRef = ref<InstanceType<typeof TransferForm> | null>(null)
const transferForm = reactive({
  toUserId: undefined as string | undefined,
  toUserName: '',
  comment: undefined as string | undefined
})
const addSignFormRef = ref<InstanceType<typeof AddSignForm> | null>(null)
const addSignForm = reactive({
  approverIds: [] as string[],
  approveType: 'sequential',
  reason: '',
  returnToSignNode: false
})

/** 拉取待办列表（分页），结果写入 dataSource 与 total */
async function loadTodo() {
  loading.value = true
  try {
    const params: FlowTodoQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    const res = await getMyTodo(params) as TaktPagedResult<FlowTodoItem>
    dataSource.value = res.data ?? []
    total.value = res.total ?? 0
  } finally {
    loading.value = false
  }
}

/** 重置关键词、页码并重新拉取待办 */
function handleReset() {
  queryKeyword.value = ''
  currentPage.value = 1
  loadTodo()
}

/** 表格变化占位（分页由 TaktPagination 处理） */
function handleTableChange(_pag: { current?: number; pageSize?: number }) {
  // 分页由 TaktPagination 处理
}

/** 分页变化时更新页码、每页条数并拉取 */
function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadTodo()
}

/** 每页条数变化时更新并拉取 */
function handlePaginationSizeChange(current: number, size: number) {
  currentPage.value = current
  pageSize.value = size
  loadTodo()
}

/** 减签后刷新弹窗内实例详情并刷新待办列表 */
async function reloadTaskDetailInModal() {
  const id = taskDetail.value?.instanceId
  if (id == null) return
  try {
    taskDetail.value = await getFlowInstanceById(id)
    await loadTodo()
  } catch {
    message.error(t('common.msg.loadFail'))
  }
}

/** 关闭审批弹窗并清空任务详情 */
async function closeApproveModal() {
  modalVisible.value = false
  taskDetail.value = null
}

/** 打开审批弹窗：设置当前任务、办结表单、拉取实例详情 */
async function handleApprove(record: FlowTodoItem, pass: boolean) {
  currentTask.value = record
  completeForm.comment = ''
  completeForm.approved = pass
  completeForm.nodeRejectStep = undefined
  cashierPayoutChannel.value = undefined
  taskDetail.value = null
  try {
    taskDetail.value = await getFlowInstanceById(record.instanceId)
    const fd = taskDetail.value?.frmData?.trim()
    if (fd) {
      try {
        const j = JSON.parse(fd) as { payoutChannel?: number }
        if (typeof j.payoutChannel === 'number' && [1, 2, 3].includes(j.payoutChannel)) {
          cashierPayoutChannel.value = j.payoutChannel
        }
      } catch {
        /* ignore */
      }
    }
  } catch {
    taskDetail.value = null
  }
  modalVisible.value = true
}

/** 审批提交：校验表单、调用 complete 接口后关闭弹窗并刷新待办 */
async function handleApproveOk() {
  const ok = await approveFormRef.value?.validate()
  if (!ok || !currentTask.value) return
  if (
    completeForm.approved &&
    taskDetail.value?.currentNodeName === CASHIER_ROUTE_NODE_ID &&
    cashierPayoutChannel.value == null
  ) {
    message.warning(t('workflow.instance.cashierPayoutRequired'))
    return
  }
  let frmDataPayload: string | undefined
  if (completeForm.approved && taskDetail.value?.currentNodeName === CASHIER_ROUTE_NODE_ID) {
    try {
      const base = taskDetail.value.frmData?.trim()
        ? (JSON.parse(taskDetail.value.frmData) as Record<string, unknown>)
        : {}
      base.payoutChannel = cashierPayoutChannel.value as number
      frmDataPayload = JSON.stringify(base)
    } catch {
      message.error(t('common.msg.loadFail'))
      return
    }
  }
  loading.value = true
  try {
    await complete({
      flowInstanceId: currentTask.value.instanceId,
      instanceCode: currentTask.value.instanceCode,
      comment: completeForm.comment || undefined,
      approved: completeForm.approved,
      nodeRejectStep: completeForm.nodeRejectStep || undefined,
      frmData: frmDataPayload
    })
    message.success('已提交')
    modalVisible.value = false
    taskDetail.value = null
    loadTodo()
  } catch (error: any) {
    message.error(error?.message || '提交失败')
  } finally {
    loading.value = false
  }
}

/** 导出待办为 Excel 并触发下载 */
async function handleExport() {
  try {
    exportLoading.value = true
    const blob = await exportTodo({ pageIndex: 1, pageSize: 99999 })
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `待办_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
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

/** 若用户选项为空则拉取用户列表供转办/加签下拉使用 */
async function ensureUserOptions() {
  if (userOptions.value.length === 0) {
    try {
      userOptions.value = await getUserOptions()
    } catch {
      message.error('加载用户列表失败')
    }
  }
}

/** 关闭转办弹窗并清空任务详情 */
function closeTransferModal() {
  transferVisible.value = false
  taskDetail.value = null
}

/** 关闭加签弹窗并清空任务详情 */
function closeAddSignModal() {
  addSignVisible.value = false
  taskDetail.value = null
}

/** 打开转办弹窗：设置当前任务、拉取实例详情、拉取用户选项 */
async function openTransfer(record: FlowTodoItem) {
  currentTransferTask.value = record
  transferForm.toUserId = undefined
  transferForm.toUserName = ''
  transferForm.comment = undefined
  taskDetail.value = null
  try {
    taskDetail.value = await getFlowInstanceById(record.instanceId)
  } catch {
    taskDetail.value = null
  }
  ensureUserOptions()
  transferVisible.value = true
}

/** 转办提交：校验表单、调用 transfer 接口后关闭弹窗并刷新待办 */
async function handleTransferOk() {
  const ok = await transferFormRef.value?.validate()
  if (!ok || !currentTransferTask.value || !transferForm.toUserId || !transferForm.toUserName) return
  loading.value = true
  try {
    await transfer({
      flowInstanceId: currentTransferTask.value.instanceId,
      instanceCode: currentTransferTask.value.instanceCode,
      toUserId: transferForm.toUserId,
      toUserName: transferForm.toUserName,
      comment: transferForm.comment || undefined
    })
    message.success('转办成功')
    transferVisible.value = false
    currentTransferTask.value = null
    taskDetail.value = null
    loadTodo()
  } catch (error: any) {
    message.error(error?.message || '转办失败')
  } finally {
    loading.value = false
  }
}

/** 打开加签弹窗：设置当前任务、拉取实例详情、拉取用户选项 */
async function openAddSign(record: FlowTodoItem) {
  currentAddSignTask.value = record
  addSignForm.approverIds = []
  addSignForm.approveType = 'sequential'
  addSignForm.reason = ''
  addSignForm.returnToSignNode = false
  taskDetail.value = null
  try {
    taskDetail.value = await getFlowInstanceById(record.instanceId)
  } catch {
    taskDetail.value = null
  }
  ensureUserOptions()
  addSignVisible.value = true
}

/** 加签提交：校验表单、调用 addApprovers 接口后关闭弹窗并刷新待办 */
async function handleAddSignOk() {
  const ok = await addSignFormRef.value?.validate()
  if (!ok || !currentAddSignTask.value || !addSignForm.approverIds?.length) return
  const approvers: FlowAddApproverItem[] = addSignForm.approverIds.map(id => {
    const opt = userOptions.value.find(o => String(o.dictValue) === String(id))
    return { approverUserId: id, approverUserName: opt?.dictLabel ?? id }
  })
  loading.value = true
  try {
    await addApprovers({
      flowInstanceId: currentAddSignTask.value.instanceId,
      instanceCode: currentAddSignTask.value.instanceCode,
      approvers,
      approveType: addSignForm.approveType,
      reason: addSignForm.reason || undefined,
      returnToSignNode: addSignForm.returnToSignNode
    })
    message.success('加签成功')
    addSignVisible.value = false
    currentAddSignTask.value = null
    taskDetail.value = null
    loadTodo()
  } catch (error: any) {
    message.error(error?.message || '加签失败')
  } finally {
    loading.value = false
  }
}

onMounted(() => loadTodo())
</script>

<style scoped lang="less">
.workflow-todo {
  padding: 16px;
}
.workflow-todo__toolbar {
  margin-bottom: 12px;
}

.todo-modal__sections {
  display: flex;
  flex-direction: column;
  gap: 20px;
}
.todo-modal__section-title {
  font-weight: 600;
  margin-bottom: 8px;
  padding-bottom: 6px;
  border-bottom: 1px solid var(--ant-color-border-secondary);
  color: var(--ant-color-text);
}
</style>
