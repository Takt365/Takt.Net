<template>
  <a-card class="workspace-module-card" :body-style="bodyStyle">
    <template #title>
      <a-tooltip :title="dragTooltip" placement="right">
        <span class="workspace-module-card-drag-handle">
          <RiDraggable />
        </span>
      </a-tooltip>
    </template>
    <template #extra>
      <a-space>
        <a-tooltip :title="displayTitle" placement="right">
          <a-button type="text" size="small" class="workspace-module-card-title-trigger">
            <template #icon><RiInformationLine /></template>
          </a-button>
        </a-tooltip>
        <a-tooltip v-if="showLayoutSwitch" :title="layoutTooltip" placement="right">
          <a-dropdown trigger="click">
            <a-button type="text" size="small">
              <template #icon><RiLayoutGridLine /></template>
            </a-button>
            <template #overlay>
            <a-menu :selected-keys="currentLayoutKey ? [currentLayoutKey] : []" @click="onLayoutMenuClick">
              <a-menu-item key="full" :disabled="moduleSpan === 24">
                <template #icon><RiLayoutRowLine /></template>
                {{ t('dashboard.workspace.layoutFullRow') }}
              </a-menu-item>
              <a-menu-item key="half" :disabled="moduleSpan === 12">
                <template #icon><RiLayoutColumnLine /></template>
                {{ t('dashboard.workspace.layoutHalfRow') }}
              </a-menu-item>
            </a-menu>
          </template>
          </a-dropdown>
        </a-tooltip>
        <slot name="headActions" />
        <a-tooltip v-if="showRemoveButton" :title="t('dashboard.workspace.removeModule')" placement="right">
          <a-button type="text" size="small" danger @click="onRemove">
            <template #icon><RiDeleteBinLine /></template>
          </a-button>
        </a-tooltip>
      </a-space>
    </template>
    <slot />
  </a-card>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import {
  RiDeleteBinLine,
  RiDraggable,
  RiInformationLine,
  RiLayoutColumnLine,
  RiLayoutGridLine,
  RiLayoutRowLine
} from '@remixicon/vue'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import type { WorkspaceModuleItem } from '@/types/dashboard/workspace'
import { WORKSPACE_AVAILABLE_MODULES } from '../config'

const props = defineProps<{
  module: WorkspaceModuleItem
}>()

const emit = defineEmits<{
  remove: []
  'change-span': [id: string, span: number]
}>()

const { t } = useI18n()

const displayTitle = computed(() => {
  if (props.module.moduleKey === 'custom' && props.module.customTitle) {
    return props.module.customTitle
  }
  const meta = WORKSPACE_AVAILABLE_MODULES.find(m => m.key === props.module.moduleKey)
  return meta ? t(meta.titleKey) : props.module.moduleKey
})

const dragTooltip = computed(() => t('dashboard.workspace.dragToReorder', { title: displayTitle.value }))

/** 布局切换：独占一行(24) / 半行(12)，未设置时按 24 */
const moduleSpan = computed(() => props.module.span ?? 24)
const currentLayoutKey = computed(() => (moduleSpan.value === 24 ? 'full' : 'half'))
const currentLayoutLabel = computed(() =>
  moduleSpan.value === 24 ? t('dashboard.workspace.layoutFullRow') : t('dashboard.workspace.layoutHalfRow')
)
const layoutTooltip = computed(() => `${t('dashboard.workspace.layoutLabel')}：${currentLayoutLabel.value}`)
const showLayoutSwitch = true

function onLayoutMenuClick(info: MenuInfo) {
  const key = String(info.key)
  const span = key === 'full' ? 24 : 12
  if (span !== moduleSpan.value) emit('change-span', props.module.id, span)
}

/** welcome、shortcut 不允许删除 */
const showRemoveButton = computed(() => {
  const key = props.module.moduleKey
  return key !== 'welcome' && key !== 'shortcut'
})

const bodyStyle = computed(() => ({
  minHeight: '128px',
  padding: '16px 24px'
}))

function onRemove() {
  emit('remove')
}
</script>

<style scoped lang="less">
.workspace-module-card {
  height: 100%;
  :deep(.ant-card-head) {
    min-height: 32px !important;
    height: 32px !important;
    padding: 0 16px;
    .ant-card-head-title,
    .ant-card-extra {
      padding: 0;
      line-height: 32px !important;
    }
    .ant-card-head-title {
      display: flex;
      align-items: center;
    }
  }
}
.workspace-module-card-drag-handle {
  cursor: move;
  color: var(--ant-color-text-tertiary);
  display: inline-flex;
  user-select: none;
  &:hover {
    color: var(--ant-color-text-secondary);
  }
}
.workspace-module-card-title-trigger {
  color: var(--ant-color-text-tertiary);
}
</style>
