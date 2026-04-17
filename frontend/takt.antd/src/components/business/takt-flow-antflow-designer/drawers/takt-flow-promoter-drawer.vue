<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-flow-antflow-designer/drawers -->
<!-- 文件名称：takt-flow-promoter-drawer.vue -->
<!-- 创建时间：2026-04-07 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：发起人节点属性编辑抽屉 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->
<template>
  <a-drawer
    v-model:open="visible"
    :title="t('workflow.designer.promoter')"
    width="480"
    :footer-style="{ textAlign: 'right' }"
    @close="handleClose"
  >
    <template v-if="config && config.nodeType === 1">
      <a-form layout="vertical">
        <a-form-item label="发起人权限">
          <a-radio-group v-model:value="form.allPerson">
            <a-radio :value="true">
              所有人可发起
            </a-radio>
            <a-radio :value="false">
              指定成员/角色可发起
            </a-radio>
          </a-radio-group>
        </a-form-item>
        <a-form-item
          v-if="!form.allPerson"
          label="指定成员"
        >
          <a-select
            v-model:value="form.selectedUserIds"
            mode="multiple"
            show-search
            :filter-option="filterOption"
            :options="userOptions"
            placeholder="选择可发起人员"
            style="width: 100%"
            :field-names="{ label: 'dictLabel', value: 'dictValue' }"
          />
        </a-form-item>
      </a-form>
    </template>
    <template #footer>
      <a-button
        style="margin-right: 8px"
        @click="handleClose"
      >
        取消
      </a-button>
      <a-button
        type="primary"
        @click="handleSave"
      >
        确定
      </a-button>
    </template>
  </a-drawer>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { useFlowDrawer } from '../config/takt-flow-use-flow-drawer'
import { getUserOptions } from '@/api/identity/user'
import type { TaktSelectOption } from '@/types/common'
import type { FlowTreeNode } from '../config/takt-flow-tree'

const { t } = useI18n()
const { state, save: saveDrawer, close } = useFlowDrawer()

const visible = computed({
  get: () => !!(state.visible && state.type === 'promoter'),
  set: (v) => { if (!v) close() }
})
const config = ref<FlowTreeNode | null>(null)
const form = ref({ allPerson: true, selectedUserIds: [] as string[] })
const userOptions = ref<TaktSelectOption[]>([])

function filterOption(
  input: string,
  option?: { label?: string; dictLabel?: string }
) {
  const label = option?.dictLabel ?? option?.label ?? ''
  return String(label).toLowerCase().includes((input ?? '').toLowerCase())
}

watch(
  () => state.visible && state.type === 'promoter',
  (v) => {
    if (v && state.config) {
      config.value = state.config
      const list = state.config.nodeApproveList ?? []
      form.value = {
        allPerson: list.length === 0,
        selectedUserIds: list.map((x) => x.targetId)
      }
    } else {
      config.value = null
    }
  },
  { immediate: true }
)

onMounted(() => {
  getUserOptions().then((list) => { userOptions.value = list ?? [] })
})

function handleSave() {
  if (!config.value) return
  const list = form.value.allPerson
    ? []
    : (form.value.selectedUserIds ?? []).map((id) => {
        const o = userOptions.value.find((u) => String(u.dictValue) === id)
        return { targetId: id, name: o?.dictLabel ?? id }
      })
  const updated: FlowTreeNode = { ...config.value, nodeApproveList: list }
  saveDrawer(updated)
}

function handleClose() {
  close()
}
</script>
