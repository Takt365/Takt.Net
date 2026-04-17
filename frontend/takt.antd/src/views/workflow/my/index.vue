<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/my -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：我的流程列表页面，包含我发起的流程、发起流程、草稿、详情与导出 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="workflow-my">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.flowinstance.instancecode'), t('entity.flowinstance.processkey')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      export-permission="workflow:my:export"
      :show-create="false"
      :show-update="false"
      :show-delete="false"
      :show-start-flow="true"
      start-flow-permission="workflow:instance:start"
      :show-send-message="false"
      :show-refresh="true"
      :show-export="true"
      :show-fullscreen="true"
      :show-advanced-query="false"
      :show-column-setting="false"
      :refresh-loading="loading"
      :export-loading="exportLoading"
      @refresh="loadList"
      @export="handleExport"
      @start-flow="goStartFlow"
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
          <a-tag :color="statusColor((record as FlowInstance).instanceStatus)">
            {{ statusText((record as FlowInstance).instanceStatus) }}
          </a-tag>
        </template>
        <template v-else-if="column.key === 'action'">
          <a-space>
            <a-button
              type="link"
              size="small"
              @click="showDetail(asFlowInstance(record))"
            >
              {{ t('common.button.detail') }}
            </a-button>
            <a-button
              v-if="((record as FlowInstance).instanceStatus === 0 || (record as FlowInstance).instanceStatus === 5) && isStarter(asFlowInstance(record))"
              type="link"
              size="small"
              @click="handleEdit(asFlowInstance(record))"
            >
              {{ t('common.button.edit') }}
            </a-button>
            <a-button
              v-if="(record as FlowInstance).instanceStatus === 5 && isStarter(asFlowInstance(record))"
              type="link"
              size="small"
              @click="handleStartFromDraft(asFlowInstance(record))"
            >
              {{ t('workflow.instance.my.startFromDraft') }}
            </a-button>
            <a-button
              v-if="(record as FlowInstance).instanceStatus === 0 && isStarter(asFlowInstance(record))"
              type="link"
              size="small"
              danger
              @click="handleRevoke(asFlowInstance(record))"
            >
              {{ t('common.button.revoke') }}
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
      v-model:open="detailVisible"
      :title="t('workflow.instance.detailTitle')"
      width="640px"
      :footer="null"
      :cancel-text="t('common.button.cancel')"
      @cancel="detailVisible = false"
    >
      <FlowInstanceDetailForm
        :detail="detail"
        @refresh="reloadInstanceDetail"
      />
    </TaktModal>
    <TaktModal
      v-model:open="editVisible"
      :title="t('common.button.edit') + t('entity.flowinstance._self')"
      :confirm-loading="editLoading"
      :ok-text="t('common.button.ok')"
      :cancel-text="t('common.button.cancel')"
      @ok="handleEditSubmit"
      @cancel="editVisible = false"
    >
      <FlowInstanceEditForm
        ref="editFormRef"
        :form="editForm"
      />
    </TaktModal>
    <TaktModal
      v-model:open="startFlowVisible"
      :title="t('common.button.startFlow')"
      width="900px"
      @cancel="closeStartFlowModal"
    >
      <FlowStartForm
        :key="startFlowFormKey"
        ref="startFormRef"
        :form="startFlowForm"
        :scheme-options="schemeOptions"
        :scheme-loading="schemeLoading"
      />
      <template #footer>
        <a-button @click="closeStartFlowModal">
          {{ t('common.button.cancel') }}
        </a-button>
        <a-button
          :loading="startDraftLoading"
          @click="handleStartFlowDraft"
        >
          {{ t('workflow.instance.startFlowForm.saveDraft') }}
        </a-button>
        <a-button
          type="primary"
          :loading="startFlowLoading"
          @click="handleStartFlowSubmit"
        >
          {{ t('workflow.instance.startFlowForm.submit') }}
        </a-button>
      </template>
    </TaktModal>
  </div>
</template>

<script setup lang="ts">
/**
 * 我的流程页：我发起的实例列表、分页、发起流程、草稿、从草稿启动、详情、撤回、编辑、导出。
 */
import { ref, reactive, onMounted, computed } from 'vue'
import { message, Modal } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { getMyInstances, getFlowInstanceById, revoke, updateFlowInstance, startFromDraft, exportMy, start, createDraft } from '@/api/workflow/instance'
import { getFlowSchemeList } from '@/api/workflow/scheme'
import { useUserStore } from '@/stores/identity/user'
import FlowInstanceDetailForm from './components/flow-instance-detail-form.vue'
import FlowInstanceEditForm from './components/flow-instance-edit-form.vue'
import FlowStartForm from './components/flow-start-form.vue'
import type { FlowInstance, FlowInstanceDetail } from '@/types/workflow/instance'
import type { FlowScheme } from '@/types/workflow/scheme'
import type { FlowInstanceQuery } from '@/types/workflow/instance'
import type { TaktPagedResult } from '@/types/common'
const toErrorMessage = (error: unknown): string => (error instanceof Error ? error.message : String(error))

const { t } = useI18n()
const loading = ref(false)
const queryKeyword = ref('')
const dataSource = ref<FlowInstance[]>([])
const currentPage = ref(1)
const pageSize = ref(10)
const total = ref(0)
const detailVisible = ref(false)
const detail = ref<FlowInstanceDetail | null>(null)
const editVisible = ref(false)
const editFormRef = ref<InstanceType<typeof FlowInstanceEditForm> | null>(null)
const editForm = reactive({ processTitle: '', frmData: '' })
const editLoading = ref(false)
const currentEditRecord = ref<FlowInstance | null>(null)
const exportLoading = ref(false)
const startFlowVisible = ref(false)
const startFlowForm = reactive<{ processKey: string; processTitle?: string; frmData?: string }>({ processKey: '', processTitle: '', frmData: undefined })
const schemeOptions = ref<{ label: string; value: string }[]>([])
const schemeLoading = ref(false)
const startFormRef = ref<InstanceType<typeof FlowStartForm> | null>(null)
/** 每次打开发起弹窗递增，子组件重挂以便「申请人」重新默认当前登录用户 */
const startFlowFormKey = ref(0)
const startFlowLoading = ref(false)
const startDraftLoading = ref(false)

const userStore = useUserStore()
const currentUserId = computed(() => String(userStore.userInfo?.userId ?? ''))

const columns = [
  { title: '实例编码', dataIndex: 'instanceCode', key: 'instanceCode', width: 200 },
  { title: '流程名称', dataIndex: 'processName', key: 'processName', width: 120 },
  { title: '标题', dataIndex: 'processTitle', key: 'processTitle', ellipsis: true },
  { title: '状态', dataIndex: 'instanceStatus', key: 'instanceStatus', width: 90 },
  { title: '发起时间', dataIndex: 'startTime', key: 'startTime', width: 170 },
  { title: '操作', key: 'action', width: 140, fixed: 'right' as const }
]

/** 实例状态码转展示文案 */
function statusText(s: number) {
  return t(`workflow.instance.status.${s}`) || t('workflow.instance.status.unknown')
}

/** 实例状态对应 Tag 颜色 */
function statusColor(s: number) {
  const m: Record<number, string> = { 0: 'processing', 1: 'success', 2: 'error', 3: 'warning', 4: 'default', 5: 'default' }
  return m[s] ?? 'default'
}

/** 将 a-table bodyCell 的 record 断言为 FlowInstance */
function asFlowInstance(r: Record<string, unknown>): FlowInstance {
  return r as unknown as FlowInstance
}

/** 判断当前用户是否为该实例发起人 */
function isStarter(r: FlowInstance) {
  return String(r.startUserId) === String(currentUserId.value)
}

/** 打开发起流程弹窗并拉取已发布方案列表填充 schemeOptions */
async function goStartFlow() {
  startFlowFormKey.value += 1
  startFlowForm.processKey = ''
  startFlowForm.processTitle = ''
  startFlowForm.frmData = undefined
  startFlowVisible.value = true
  schemeLoading.value = true
  try {
    const res = await getFlowSchemeList({
      pageIndex: 1,
      pageSize: 100,
      processStatus: 1
    })
    const list = res.data ?? []
    schemeOptions.value = list.map((s: FlowScheme) => ({ label: `${s.processName}（${s.processKey}）`, value: s.processKey }))
    if (list.length === 1) startFlowForm.processKey = list[0].processKey
    if (list.length && !startFlowForm.processKey && list.some((s: FlowScheme) => s.processKey === 'Leave')) startFlowForm.processKey = 'Leave'
  } finally {
    schemeLoading.value = false
  }
}

/** 关闭发起流程弹窗 */
function closeStartFlowModal() {
  startFlowVisible.value = false
}

/** 发起流程提交：校验后调用 start 接口并刷新列表 */
async function handleStartFlowSubmit() {
  const ok = await startFormRef.value?.validate()
  if (!ok || !startFlowForm.processKey?.trim()) return
  startFlowLoading.value = true
  try {
    const res = await start({
      processKey: startFlowForm.processKey.trim(),
      processTitle: startFlowForm.processTitle?.trim() || undefined,
      frmData: startFlowForm.frmData?.trim() || undefined
    })
    message.success(t('workflow.instance.startFlowForm.submitSuccess', { code: res.instanceCode }))
    closeStartFlowModal()
    loadList()
  } catch (err: unknown) {
    message.error(toErrorMessage(err) || t('common.msg.operateFail'))
  } finally {
    startFlowLoading.value = false
  }
}

/** 保存草稿：校验后调用 createDraft 接口并刷新列表 */
async function handleStartFlowDraft() {
  const ok = await startFormRef.value?.validate()
  if (!ok || !startFlowForm.processKey?.trim()) return
  startDraftLoading.value = true
  try {
    const res = await createDraft({
      processKey: startFlowForm.processKey.trim(),
      processTitle: startFlowForm.processTitle?.trim() || undefined,
      frmData: startFlowForm.frmData?.trim() || undefined
    })
    message.success(t('workflow.instance.startFlowForm.saveDraftSuccess', { code: res.instanceCode }))
    closeStartFlowModal()
    loadList()
  } catch (err: unknown) {
    message.error(toErrorMessage(err) || t('common.msg.operateFail'))
  } finally {
    startDraftLoading.value = false
  }
}

/** 实例行 key：取 instanceId 字符串 */
function getInstanceId(record: FlowInstance): string {
  return record?.instanceId != null ? String(record.instanceId) : ''
}

/** 拉取我发起的流程列表（分页），结果写入 dataSource 与 total */
async function loadList() {
  loading.value = true
  try {
    const params: FlowInstanceQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      myStartedOnly: true
    }
    if (queryKeyword.value?.trim()) {
      params.processKey = queryKeyword.value.trim()
      params.instanceCode = queryKeyword.value.trim()
    }
    const res = await getMyInstances(params)
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
  // TaktSingleTable 分页由 TaktPagination 处理，此处仅保留以兼容 @change
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
    message.error(t('common.msg.loadFail'))
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

/** 打开编辑弹窗：回填标题与 frmData，拉取最新详情 */
async function handleEdit(record: FlowInstance) {
  currentEditRecord.value = record
  editForm.processTitle = record.processTitle ?? ''
  editForm.frmData = record.frmData ?? ''
  try {
    const d = await getFlowInstanceById(record.instanceId)
    if (d) {
      editForm.processTitle = d.processTitle ?? ''
      editForm.frmData = d.frmData ?? ''
    }
  } catch {
    // 忽略详情回填失败，保留当前行数据进入编辑
  }
  editVisible.value = true
}

/** 编辑提交：校验后调用 update 接口并关闭弹窗、刷新列表 */
async function handleEditSubmit() {
  const ok = await editFormRef.value?.validate()
  if (!ok || !currentEditRecord.value) return
  try {
    editLoading.value = true
    await updateFlowInstance({
      id: currentEditRecord.value.instanceId,
      processTitle: editForm.processTitle?.trim() || undefined,
      frmData: editForm.frmData?.trim() || undefined
    })
    message.success(t('common.msg.updateSuccess'))
    editVisible.value = false
    currentEditRecord.value = null
    loadList()
  } catch (error: unknown) {
    message.error(toErrorMessage(error) || t('common.msg.operateFail'))
  } finally {
    editLoading.value = false
  }
}

/** 从草稿启动：二次确认后调用 startFromDraft 并刷新列表 */
function handleStartFromDraft(record: FlowInstance) {
  Modal.confirm({
    centered: true,
    title: t('workflow.my.startFromDraft'),
    content: t('workflow.instance.my.confirmStartFromDraft', { name: record.processTitle || record.instanceCode }),
    onOk: async () => {
      try {
        loading.value = true
        await startFromDraft(record.instanceId)
        message.success(t('common.msg.operateSuccess'))
        loadList()
      } catch (error: unknown) {
        message.error(toErrorMessage(error) || t('common.msg.operateFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

/** 导出我发起的流程为 Excel 并触发下载 */
async function handleExport() {
  try {
    exportLoading.value = true
    const query: FlowInstanceQuery = { pageIndex: 1, pageSize: 99999, myStartedOnly: true }
    if (queryKeyword.value?.trim()) {
      query.processKey = queryKeyword.value.trim()
      query.instanceCode = queryKeyword.value.trim()
    }
    const blob = await exportMy(query)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `我的流程_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess'))
  } catch (error: unknown) {
    message.error(toErrorMessage(error) || t('common.msg.exportFail'))
  } finally {
    exportLoading.value = false
  }
}

/** 撤回：二次确认后调用 revoke 接口并刷新列表 */
function handleRevoke(record: FlowInstance) {
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmAction', { action: t('common.button.revoke') }),
    content: t('workflow.instance.confirmRevoke', { name: record.processTitle || record.instanceCode }),
    onOk: async () => {
      try {
        loading.value = true
        await revoke(record.instanceCode)
        message.success(t('common.msg.actionSuccess', { action: t('common.button.revoke') }))
        loadList()
      } catch (error: unknown) {
        message.error(toErrorMessage(error) || t('common.msg.operateFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

onMounted(() => loadList())
</script>

<style scoped lang="less">
.workflow-my {
  padding: 16px;
}
.history-item {
  font-size: 12px;
  margin-bottom: 4px;
}
</style>
