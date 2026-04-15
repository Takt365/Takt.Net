<template>
  <div class="dashboard-data-board">
    <a-row ref="rowRef" :gutter="[16, 16]">
      <a-col
        v-for="item in modules"
        :key="item.id"
        :data-id="item.id"
        :span="item.span ?? 24"
      >
        <a-dropdown trigger="contextmenu">
          <div class="databoard-module-context-target">
            <DataBoardModuleCard
              :module="item"
              @remove="removeModule(item.id)"
              @change-span="updateModuleSpan"
            >
              <component :is="moduleComponents[item.moduleKey]" />
            </DataBoardModuleCard>
          </div>
          <template #overlay>
            <a-menu @click="onModuleContextMenuClick(item, $event)">
              <a-menu-item key="add">
                <template #icon><RiAddLine /></template>
                {{ t('dashboard.data-board.addModule') }}
              </a-menu-item>
              <a-menu-item key="remove" danger>
                <template #icon><RiDeleteBinLine /></template>
                {{ t('dashboard.data-board.removeModule') }}
              </a-menu-item>
            </a-menu>
          </template>
        </a-dropdown>
      </a-col>
    </a-row>

    <a-modal
      v-model:open="showAddModal"
      :title="t('dashboard.data-board.addModule')"
      :width="520"
      @ok="onAddModule"
    >
      <p class="databoard-add-tip">{{ t('dashboard.data-board.selectModuleType') }}</p>
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
import { ref, computed, markRaw, onMounted, onBeforeUnmount, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { RiAddLine, RiDeleteBinLine } from '@remixicon/vue'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import Sortable from 'sortablejs'
import DataBoardModuleCard from './components/DataBoardModuleCard.vue'
import StatsOverviewModule from './modules/StatsOverviewModule.vue'
import StatsChangeModule from './modules/StatsChangeModule.vue'
import StatsOnlineModule from './modules/StatsOnlineModule.vue'
import StatsSalesModule from './modules/StatsSalesModule.vue'
import StatsProductionModule from './modules/StatsProductionModule.vue'
import StatsCustomModule from './modules/StatsCustomModule.vue'
import type { DataBoardModuleItem, DataBoardModuleKey } from '@/types/dashboard/data-board'
import {
  DATA_BOARD_STORAGE_KEY,
  DATA_BOARD_AVAILABLE_MODULES,
  getDefaultDataBoardModules,
  generateModuleId
} from './config'

const { t } = useI18n()

const moduleComponents: Record<DataBoardModuleKey, any> = {
  overview: markRaw(StatsOverviewModule),
  change: markRaw(StatsChangeModule),
  online: markRaw(StatsOnlineModule),
  sales: markRaw(StatsSalesModule),
  production: markRaw(StatsProductionModule),
  custom: markRaw(StatsCustomModule)
}

function loadModules(): DataBoardModuleItem[] {
  try {
    const raw = localStorage.getItem(DATA_BOARD_STORAGE_KEY)
    if (raw) {
      const parsed = JSON.parse(raw) as DataBoardModuleItem[]
      if (Array.isArray(parsed) && parsed.length > 0) return parsed
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

onMounted(() => {
  nextTick(() => {
    const el = rowRef.value?.$el
    if (!el || !(el instanceof HTMLElement)) return
    sortableInstance = Sortable.create(el, {
      handle: '.databoard-module-card-drag-handle',
      animation: 150,
      ghostClass: 'databoard-module-card-ghost',
      onEnd(evt: Sortable.SortableEvent) {
        reorderModulesByDomOrder(evt.from as HTMLElement)
      }
    })
  })
})

onBeforeUnmount(() => {
  sortableInstance?.destroy()
})

function addModule(key: DataBoardModuleKey) {
  if (key !== 'custom' && addedModuleKeys.value.has(key)) return
  const meta = DATA_BOARD_AVAILABLE_MODULES.find(m => m.key === key)
  const span = meta?.defaultSpan ?? 24
  const newItem: DataBoardModuleItem = {
    id: generateModuleId(key),
    moduleKey: key,
    span
  }
  modules.value = [...modules.value, newItem]
  saveModules(modules.value)
  message.success(t('dashboard.data-board.addSuccess'))
}

function removeModule(id: string) {
  modules.value = modules.value.filter(m => m.id !== id)
  saveModules(modules.value)
  message.success(t('dashboard.data-board.removeSuccess'))
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
  padding: 24px 0;
}
.databoard-module-context-target {
  height: 100%;
}
.databoard-add-tip {
  margin-bottom: 8px;
  color: var(--ant-color-text-secondary);
  font-size: 12px;
}
:deep(.databoard-module-card-ghost) {
  opacity: 0.5;
  background: var(--ant-color-fill-quaternary);
}
</style>
