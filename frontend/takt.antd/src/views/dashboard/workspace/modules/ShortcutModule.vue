<template>
  <div class="shortcut-module">
    <a-row :gutter="[16, 16]">
      <a-col
        v-for="item in shortcuts"
        :key="item.path"
        :span="3"
      >
        <a-tooltip
          :title="item.title"
          placement="top"
        >
          <a
            class="shortcut-item"
            @click="go(item.path)"
          >
            <span
              v-if="item.icon"
              class="shortcut-icon"
              :style="item.iconColor ? { color: item.iconColor } : undefined"
            ><component :is="item.icon" /></span>
          </a>
        </a-tooltip>
      </a-col>
    </a-row>
    <a-modal
      v-model:open="manageVisible"
      :title="t('dashboard.workspace.manageshortcuts')"
      :width="520"
      @ok="handleManageOk"
      @cancel="handleManageCancel"
    >
      <p class="shortcut-tip">
        最多选择 16 个菜单作为快捷入口（两行，每行 8 个）。
      </p>
      <a-checkbox-group v-model:value="editingPaths">
        <a-row :gutter="[8, 8]">
          <a-col
            v-for="item in availableShortcuts"
            :key="item.path"
            :span="6"
          >
            <a-checkbox
              :value="item.path"
              :disabled="editingPaths.length >= 16 && !editingPaths.includes(item.path)"
            >
              {{ item.title }}
            </a-checkbox>
          </a-col>
        </a-row>
      </a-checkbox-group>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { useMenuStore } from '@/stores/identity/menu'
import { useWorkspaceShortcutStore } from '@/stores/dashboard/workspace-shortcut'

const router = useRouter()
const { t } = useI18n()
const menuStore = useMenuStore()
const shortcutStore = useWorkspaceShortcutStore()

const availableShortcuts = computed(() => menuStore.leafMenuItems || [])

const FAMOUS_COLORS = [
  '#2e8b57', '#00a0b0', '#FF0000', '#FF6347', '#990033',
  '#8c1515', '#002FA7', '#4c2b18', '#003153', '#F9DC24', '#808080'
]

function getColorForPath(path: string): string {
  let n = 0
  for (let i = 0; i < path.length; i++) n = (n * 31 + path.charCodeAt(i)) >>> 0
  return FAMOUS_COLORS[n % FAMOUS_COLORS.length]
}

watch(
  availableShortcuts,
  list => {
    if (list.length > 0) {
      const paths = list.map(x => x.path)
      shortcutStore.initDefaultsIfEmpty(paths)
    }
  },
  { immediate: true }
)

const shortcuts = computed(() => {
  const list = availableShortcuts.value
  const getIconRenderer = menuStore.getIconRenderer
  const selected = shortcutStore.selectedPaths
  return list
    .filter(x => selected.includes(x.path))
    .slice(0, 16)
    .map((x: { path: string; title: string; iconName?: string }) => ({
      path: x.path,
      title: x.title,
      icon: x.iconName ? getIconRenderer(x.iconName) : undefined,
      iconColor: getColorForPath(x.path)
    }))
})

const manageVisible = ref(false)
const editingPaths = ref<string[]>([])

function openManage() {
  editingPaths.value = [...shortcutStore.selectedPaths]
  manageVisible.value = true
}

function handleManageOk() {
  const unique = Array.from(new Set(editingPaths.value))
  const finalList = unique.slice(0, 16)
  shortcutStore.setPaths(finalList)
  manageVisible.value = false
}

function handleManageCancel() {
  manageVisible.value = false
}

function go(path: string) {
  router.push(path)
}

defineExpose({ openManage })
</script>

<style scoped lang="less">
.shortcut-module {
  padding: 16px 0;
  min-height: 140px;
}
.shortcut-item {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 44px;
  height: 44px;
  padding: 0;
  border-radius: 8px;
  color: var(--ant-color-text);
  text-decoration: none;
  transition: background-color 0.2s, color 0.2s, transform 0.2s ease;
  cursor: pointer;
  user-select: none;
  border: 1px solid transparent;
  perspective: 420px;
  &:hover {
    background: var(--ant-color-fill-tertiary);
    color: var(--ant-color-primary);
    .shortcut-icon {
      color: var(--ant-color-primary) !important;
    }
  }
  &:active {
    background: var(--ant-color-fill-secondary);
    transform: scale(0.98);
  }
  &:focus-visible {
    outline: 2px solid var(--ant-color-primary);
    outline-offset: 2px;
  }
  .shortcut-icon {
    font-size: 22px;
    display: inline-block;
    transform: rotateY(0deg);
    transition: color 0.2s ease, transform 0.3s ease;
    will-change: transform;
  }
  &:hover .shortcut-icon {
    transform: rotateY(180deg);
  }
  &:active .shortcut-icon {
    transform: rotateY(180deg) scale(0.95);
  }
}
.shortcut-tip {
  margin-bottom: 8px;
  color: var(--ant-color-text-secondary);
  font-size: 12px;
}
</style>