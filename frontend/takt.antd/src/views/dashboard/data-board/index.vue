<!--
  项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
  命名空间：@/views/dashboard/data-board
  文件名称：index.vue
  功能描述：数据看板页，可添加/移除/拖拽排序统计模块，风格与 workspace 一致
  版权信息：Copyright (c) 2025 Takt. All rights reserved.
-->
<template>
  <div class="dashboard-data-board">
    <a-row v-if="modules.length === 0" :gutter="[16, 16]">
      <a-col :span="24">
        <a-empty :description="t('dashboard.dataBoard.emptyTip')">
          <a-button type="primary" @click="openAddModal">
            <template #icon><RiAddLine /></template>
            {{ t('dashboard.dataBoard.addModule') }}
          </a-button>
        </a-empty>
      </a-col>
    </a-row>
    <a-row v-else ref="rowRef" :gutter="[16, 16]">
      <a-col
        v-for="item in modules"
        :key="item.id"
        :data-id="item.id"
        :span="item.span ?? 24"
      >
        <a-dropdown trigger="contextmenu">
          <div class="data-board-module-context-target">
            <DataBoardModuleCard
              :module="item"
              @remove="removeModule(item.id)"
              @change-span="updateModuleSpan"
            >
              <component
                :is="moduleComponents[item.moduleKey]"
                v-bind="getModuleProps(item)"
              />
            </DataBoardModuleCard>
          </div>
          <template #overlay>
            <a-menu @click="onModuleContextMenuClick(item, $event)">
              <a-menu-item key="add">
                <template #icon><RiAddLine /></template>
                {{ t('dashboard.dataBoard.addModule') }}
              </a-menu-item>
              <a-menu-item v-if="canRemoveModule(item)" key="remove" danger>
                <template #icon><RiDeleteBinLine /></template>
                {{ t('dashboard.dataBoard.removeModule') }}
              </a-menu-item>
            </a-menu>
          </template>
        </a-dropdown>
      </a-col>
    </a-row>

    <a-modal
      v-model:open="showAddModal"
      :title="t('dashboard.dataBoard.addModule')"
      :width="520"
      @ok="onAddModule"
    >
      <p class="data-board-add-tip">{{ t('dashboard.dataBoard.selectModuleType') }}</p>
      <a-checkbox-group v-model:value="addingKeys">
        <a-row :gutter="[8, 8]">
          <a-col v-for="m in DATA_BOARD_AVAILABLE_MODULES" :key="m.key" :span="8">
            <a-checkbox
              :value="m.key"
              :disabled="m.key !== 'custom' && addedModuleKeys.has(m.key)"
            >
              {{ t(m.titleKey) }}
            </a-checkbox>
          </a-col>
        </a-row>
      </a-checkbox-group>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, markRaw, watch, onMounted, onBeforeUnmount, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { RiAddLine, RiDeleteBinLine } from '@remixicon/vue'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import Sortable from 'sortablejs'
import DataBoardModuleCard from './components/DataBoardModuleCard.vue'
import AccountingStatsOverview from './modules/AccountingStatsOverview.vue'
import LogisticsStatsOverview from './modules/LogisticsStatsOverview.vue'
import WorkflowStatsOverview from './modules/WorkflowStatsOverview.vue'
import HumanResourceStatsOverview from './modules/HumanResourceStatsOverview.vue'
import RoutineStatsOverview from './modules/RoutineStatsOverview.vue'
import OnlineStatsOverview from './modules/OnlineStatsOverview.vue'
import CustomStatsOverview from './modules/CustomStatsOverview.vue'
import type { DataBoardModuleItem, DataBoardModuleKey } from '@/types/dashboard/dataBoard'
import {
  DATA_BOARD_STORAGE_KEY,
  DATA_BOARD_AVAILABLE_MODULES,
  getDefaultDataBoardModules,
  generateDataBoardModuleId
} from './config'

const { t } = useI18n()

const moduleComponents: Record<DataBoardModuleKey, ReturnType<typeof markRaw>> = {
  accounting: markRaw(AccountingStatsOverview),
  logistics: markRaw(LogisticsStatsOverview),
  workflow: markRaw(WorkflowStatsOverview),
  humanResource: markRaw(HumanResourceStatsOverview),
  routine: markRaw(RoutineStatsOverview),
  onlineUsers: markRaw(OnlineStatsOverview),
  custom: markRaw(CustomStatsOverview)
}

function loadModules(): DataBoardModuleItem[] {
  try {
    const raw = localStorage.getItem(DATA_BOARD_STORAGE_KEY)
    if (raw) {
      const parsed = JSON.parse(raw) as DataBoardModuleItem[]
      if (Array.isArray(parsed) && parsed.length > 0) {
        // 兼容旧数据：overview -> accounting，chart -> onlineUsers
        return parsed.map((m) => {
          const key = m.moduleKey as string
          if (key === 'overview') return { ...m, moduleKey: 'accounting' }
          if (key === 'chart') return { ...m, moduleKey: 'onlineUsers' }
          return m
        }) as DataBoardModuleItem[]
      }
    }
  } catch (_) {}
  return getDefaultDataBoardModules()
}

function saveModules(list: DataBoardModuleItem[]) {
  localStorage.setItem(DATA_BOARD_STORAGE_KEY, JSON.stringify(list))
}

const modules = ref<DataBoardModuleItem[]>(loadModules())
const rowRef = ref<{ $el: HTMLElement } | null>(null)
let sortableInstance: Sortable | null = null

const showAddModal = ref(false)
const addingKeys = ref<DataBoardModuleKey[]>([])
const addedModuleKeys = computed(() => new Set(modules.value.map(m => m.moduleKey)))

function openAddModal() {
  showAddModal.value = true
  addingKeys.value = []
}

function reorderModulesByDomOrder(container: HTMLElement) {
  const ids = [...container.children]
    .map(el => el.getAttribute('data-id'))
    .filter((id): id is string => !!id)
  const idToItem = new Map(modules.value.map(m => [m.id, m]))
  const ordered = ids.map(id => idToItem.get(id)).filter((m): m is DataBoardModuleItem => !!m)
  if (ordered.length === modules.value.length) {
    modules.value = ordered
    saveModules(modules.value)
  }
}

function initSortable() {
  if (sortableInstance) return
  const el = rowRef.value?.$el
  if (!el || !(el instanceof HTMLElement)) return
  sortableInstance = Sortable.create(el, {
    handle: '.data-board-module-card-drag-handle',
    animation: 150,
    ghostClass: 'data-board-module-card-ghost',
    onEnd(evt: Sortable.SortableEvent) {
      reorderModulesByDomOrder(evt.from as HTMLElement)
    }
  })
}

onMounted(() => {
  nextTick(() => {
    if (modules.value.length > 0) initSortable()
  })
})

/** 从空到有模块时再挂载 rowRef，需延迟初始化 Sortable */
watch(
  () => modules.value.length,
  (len) => {
    if (len > 0) nextTick(initSortable)
  }
)

onBeforeUnmount(() => {
  sortableInstance?.destroy()
})

function getModuleProps(item: DataBoardModuleItem) {
  if (item.moduleKey === 'custom') {
    return { customTitle: item.customTitle, customContent: item.customContent }
  }
  return {}
}

function addModule(key: DataBoardModuleKey) {
  if (key !== 'custom' && addedModuleKeys.value.has(key)) return
  const meta = DATA_BOARD_AVAILABLE_MODULES.find(m => m.key === key)
  const span = meta?.defaultSpan ?? 24
  const newItem: DataBoardModuleItem = {
    id: generateDataBoardModuleId(key),
    moduleKey: key,
    span
  }
  if (key === 'custom') {
    newItem.customTitle = t('dashboard.dataBoard.modules.custom')
    newItem.customContent = ''
  }
  modules.value = [...modules.value, newItem]
  saveModules(modules.value)
  message.success(t('dashboard.dataBoard.addSuccess'))
}

/** 在线用户、工作流统计不可删除 */
function canRemoveModule(item: DataBoardModuleItem): boolean {
  return item.moduleKey !== 'onlineUsers' && item.moduleKey !== 'workflow'
}

function removeModule(id: string) {
  modules.value = modules.value.filter(m => m.id !== id)
  saveModules(modules.value)
  message.success(t('dashboard.dataBoard.removeSuccess'))
}

function updateModuleSpan(id: string, span: number) {
  const item = modules.value.find(m => m.id === id)
  if (item && (span === 24 || span === 12)) {
    item.span = span
    saveModules(modules.value)
  }
}

function onAddModule() {
  const added = addedModuleKeys.value
  for (const key of addingKeys.value) {
    if (key !== 'custom' && added.has(key)) continue
    addModule(key)
  }
  showAddModal.value = false
  addingKeys.value = []
}

function onModuleContextMenuClick(item: DataBoardModuleItem, ev: MenuInfo) {
  if (ev.key === 'add') openAddModal()
  else if (ev.key === 'remove') removeModule(item.id)
}
</script>

<style scoped lang="less">
.dashboard-data-board {
  padding: 0;
}
.data-board-module-context-target {
  height: 100%;
}
.data-board-add-tip {
  margin-bottom: 8px;
  color: var(--ant-color-text-secondary);
  font-size: 12px;
}
:deep(.data-board-module-card-ghost) {
  opacity: 0.5;
  background: var(--ant-color-fill-quaternary);
}
</style>
