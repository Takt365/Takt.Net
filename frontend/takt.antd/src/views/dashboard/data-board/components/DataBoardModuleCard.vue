<template>
  <a-card
    class="databoard-module-card"
    :body-style="bodyStyle"
  >
    <template #title>
      <a-tooltip
        :title="dragTooltip"
        placement="right"
      >
        <span class="databoard-module-card-drag-handle">
          <RiDraggable />
        </span>
      </a-tooltip>
    </template>
    <template #extra>
      <a-space>
        <a-tooltip
          :title="displayTitle"
          placement="right"
        >
          <a-button
            type="text"
            size="small"
            class="databoard-module-card-title-trigger"
          >
            <template #icon>
              <RiInformationLine />
            </template>
          </a-button>
        </a-tooltip>
        <a-tooltip
          v-if="showLayoutSwitch"
          :title="layoutTooltip"
          placement="right"
        >
          <a-dropdown trigger="click">
            <a-button
              type="text"
              size="small"
            >
              <template #icon>
                <RiLayoutGridLine />
              </template>
            </a-button>
            <template #overlay>
              <a-menu
                :selected-keys="currentLayoutKey ? [currentLayoutKey] : []"
                @click="onLayoutMenuClick"
              >
                <a-menu-item
                  key="full"
                  :disabled="moduleSpan === 24"
                >
                  <template #icon>
                    <RiLayoutRowLine />
                  </template>
                  {{ t('dashboard.data-board.layoutfullrow') }}
                </a-menu-item>
                <a-menu-item
                  key="half"
                  :disabled="moduleSpan === 12"
                >
                  <template #icon>
                    <RiLayoutColumnLine />
                  </template>
                  {{ t('dashboard.data-board.layouthalfrow') }}
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
        </a-tooltip>
        <slot name="headActions" />
        <a-tooltip
          :title="t('dashboard.data-board.removemodule')"
          placement="right"
        >
          <a-button
            type="text"
            size="small"
            danger
            @click="onRemove"
          >
            <template #icon>
              <RiDeleteBinLine />
            </template>
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
import type { DataBoardModuleItem } from '@/types/dashboard/data-board'
import { DATA_BOARD_AVAILABLE_MODULES } from '../config'

const props = defineProps<{
  module: DataBoardModuleItem
}>()

const emit = defineEmits<{
  remove: []
  'change-span': [id: string, span: number]
}>()

const { t } = useI18n()

const displayTitle = computed(() => {
  const meta = DATA_BOARD_AVAILABLE_MODULES.find(m => m.key === props.module.moduleKey)
  return meta ? t(meta.titleKey) : props.module.moduleKey
})

const dragTooltip = computed(() => t('dashboard.data-board.dragtoreorder', { title: displayTitle.value }))

const moduleSpan = computed(() => props.module.span ?? 24)
const currentLayoutKey = computed(() => (moduleSpan.value === 24 ? 'full' : 'half'))
const currentLayoutLabel = computed(() =>
  moduleSpan.value === 24 ? t('dashboard.data-board.layoutfullrow') : t('dashboard.data-board.layouthalfrow')
)
const layoutTooltip = computed(() => `${t('dashboard.data-board.layoutlabel')}：${currentLayoutLabel.value}`)
const showLayoutSwitch = true

function onLayoutMenuClick(info: MenuInfo) {
  const key = String(info.key)
  const span = key === 'full' ? 24 : 12
  if (span !== moduleSpan.value) emit('change-span', props.module.id, span)
}

const bodyStyle = computed(() => ({
  minHeight: '128px',
  padding: '16px 24px'
}))

function onRemove() {
  emit('remove')
}
</script>

<style scoped lang="less">
.databoard-module-card {
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
.databoard-module-card-drag-handle {
  cursor: move;
  color: var(--ant-color-text-tertiary);
  display: inline-flex;
  user-select: none;
  &:hover {
    color: var(--ant-color-text-secondary);
  }
}
.databoard-module-card-title-trigger {
  color: var(--ant-color-text-tertiary);
}
</style>
