<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-flow-antflow-designer/dialog -->
<!-- 文件名称：takt-flow-select-role-dialog.vue -->
<!-- 创建时间：2026-04-07 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：分页选择角色弹窗（对接 getRoleList，与 AntFlow selectRoleDialog 职责对齐） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->
<template>
  <a-modal
    v-model:open="openProxy"
    :title="t('workflow.designer.selectRoleDialogTitle')"
    width="800px"
    :confirm-loading="loading"
    destroy-on-close
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-input-search
      v-model:value="keyWords"
      style="width: 280px; margin-bottom: 12px"
      :placeholder="t('workflow.designer.selectRoleSearchPlaceholder')"
      allow-clear
      @search="onSearch"
    />
    <a-table
      size="small"
      :row-selection="{ selectedRowKeys, onChange: onSelectChange }"
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :pagination="pagination"
      row-key="roleId"
      @change="onTableChange"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { getRoleList } from '@/api/identity/role'
import type { Role } from '@/types/identity/role'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  selectedIds: string[]
}>()

const emit = defineEmits<{
  'update:open': [open: boolean]
  confirm: [payload: { ids: string[]; items: { targetId: string; name: string }[] }]
}>()

const openProxy = computed({
  get: () => props.open,
  set: (v: boolean) => emit('update:open', v)
})

const loading = ref(false)
const keyWords = ref('')
const pageIndex = ref(1)
const pageSize = ref(10)
const total = ref(0)
const dataSource = ref<Role[]>([])
const selectedRowKeys = ref<string[]>([])
const nameById = ref<Record<string, string>>({})

const columns = computed(() => [
  { title: t('workflow.designer.colRoleName'), dataIndex: 'roleName', ellipsis: true },
  { title: t('workflow.designer.colRoleCode'), dataIndex: 'roleCode', ellipsis: true }
])

const pagination = computed(() => ({
  current: pageIndex.value,
  pageSize: pageSize.value,
  total: total.value,
  showSizeChanger: true,
  showTotal: (n: number) => t('workflow.designer.pageTotal', { total: n })
}))

function onSelectChange(keys: (string | number)[], rows: Role[]) {
  const ks = keys.map(String)
  const prev = new Set(selectedRowKeys.value)
  const next = new Set(ks)
  for (const id of prev) {
    if (!next.has(id)) delete nameById.value[id]
  }
  for (const row of rows) {
    nameById.value[String(row.roleId)] = row.roleName ?? String(row.roleId)
  }
  selectedRowKeys.value = ks
}

async function fetchList() {
  loading.value = true
  try {
    const res: TaktPagedResult<Role> = await getRoleList({
      pageIndex: pageIndex.value,
      pageSize: pageSize.value,
      keyWords: keyWords.value?.trim() || undefined
    })
    dataSource.value = res.data ?? []
    total.value = res.total ?? 0
  } finally {
    loading.value = false
  }
}

function onSearch() {
  pageIndex.value = 1
  fetchList()
}

function onTableChange(pag: { current?: number; pageSize?: number }) {
  if (pag.current != null) pageIndex.value = pag.current
  if (pag.pageSize != null) pageSize.value = pag.pageSize
  fetchList()
}

watch(
  () => props.open,
  (v) => {
    if (v) {
      nameById.value = {}
      selectedRowKeys.value = [...(props.selectedIds ?? [])]
      keyWords.value = ''
      pageIndex.value = 1
      fetchList().then(() => {
        for (const row of dataSource.value) {
          const id = String(row.roleId)
          if (selectedRowKeys.value.includes(id)) {
            nameById.value[id] = row.roleName ?? id
          }
        }
      })
    }
  }
)

function handleOk() {
  const ids = selectedRowKeys.value
  const items = ids.map((id) => ({ targetId: id, name: nameById.value[id] ?? id }))
  emit('confirm', { ids, items })
  emit('update:open', false)
}

function handleCancel() {
  emit('update:open', false)
}
</script>
