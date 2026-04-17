<template>
  <div
    v-if="show"
    class="takt-tools-bar"
  >
    <!-- 左侧：常用按钮 -->
    <div class="tools-bar-left">
      <slot name="left">
        <a-space>
          <a-button
            v-if="canCreate"
            class="takt-button-create"
            :disabled="createDisabled"
            :loading="createLoading"
            @click="handleCreate"
          >
            <template #icon>
              <RiAddLine />
            </template>
            {{ t('common.button.create') }}
          </a-button>
          <a-button
            v-if="canStartFlow"
            class="takt-button-start"
            :disabled="startFlowDisabled"
            :loading="startFlowLoading"
            @click="handleStartFlow"
          >
            <template #icon>
              <RiSendPlane2Line />
            </template>
            {{ t('common.button.startFlow') }}
          </a-button>
          <a-button
            v-if="canSendMessage"
            class="takt-button-send"
            :disabled="sendMessageDisabled"
            :loading="sendMessageLoading"
            @click="handleSendMessage"
          >
            <template #icon>
              <RiMailSendLine />
            </template>
            {{ t('common.button.sendMessage') }}
          </a-button>
          <!-- 新增行按钮：需满足 showCreateRow 与 createRowPermission（与导入/导出逻辑一致） -->
          <a-button
            v-if="canCreateRow"
            class="takt-button-create-row"
            :disabled="createRowDisabled"
            :loading="createRowLoading"
            @click="handleCreateRow"
          >
            <template #icon>
              <RiInsertRowBottom />
            </template>
            {{ t('common.button.createRow') }}
          </a-button>
          <a-button
            v-if="canUpdate"
            class="takt-button-update"
            :disabled="updateDisabled"
            :loading="updateLoading"
            @click="handleUpdate"
          >
            <template #icon>
              <RiEditLine />
            </template>
            {{ t('common.button.update') }}
          </a-button>
          <a-button
            v-if="canDelete"
            class="takt-button-delete"
            :disabled="deleteDisabled"
            :loading="deleteLoading"
            @click="handleDelete"
          >
            <template #icon>
              <RiDeleteBinLine />
            </template>
            {{ t('common.button.delete') }}
          </a-button>
          <!-- 删除行按钮：需满足 showDeleteRow 与 deleteRowPermission（与导入/导出逻辑一致） -->
          <a-button
            v-if="canDeleteRow"
            class="takt-button-delete-row"
            :disabled="deleteRowDisabled"
            :loading="deleteRowLoading"
            @click="handleDeleteRow"
          >
            <template #icon>
              <RiDeleteRow />
            </template>
            {{ t('common.button.deleteRow') }}
          </a-button>
          <a-button
            v-if="canImport"
            class="takt-button-import"
            :disabled="importDisabled"
            :loading="importLoading"
            @click="handleImport"
          >
            <template #icon>
              <RiImportLine />
            </template>
            {{ t('common.button.import') }}
          </a-button>
          <a-button
            v-if="canExport"
            class="takt-button-export"
            :disabled="exportDisabled"
            :loading="exportLoading"
            @click="handleExport"
          >
            <template #icon>
              <RiExportLine />
            </template>
            {{ t('common.button.export') }}
          </a-button>
          <!-- 清空按钮（下拉：清空7天、清空30天、清空全部） -->
          <a-dropdown
            v-if="canEmpty"
            :trigger="['click']"
            placement="bottomLeft"
          >
            <a-button
              class="takt-button-empty"
              :disabled="emptyDisabled"
              :loading="emptyLoading"
            >
              <template #icon>
                <RiBrush2Line />
              </template>
              {{ t('common.button.empty') }}
              <RiArrowDownSLine class="takt-button-empty-arrow" />
            </a-button>
            <template #overlay>
              <a-menu @click="handleEmptyMenuClick">
                <a-menu-item key="7d">
                  {{ t('common.button.empty7d') }}
                </a-menu-item>
                <a-menu-item key="30d">
                  {{ t('common.button.empty30d') }}
                </a-menu-item>
                <a-menu-item key="all">
                  {{ t('common.button.emptyAll') }}
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
          <!-- 自定义左侧按钮 -->
          <a-button
            v-for="action in filteredLeftActions"
            :key="action.key"
            :class="[
              action.buttonClass || `takt-button-${action.key}`,
              action.shape === 'plain' ? 'takt-button-plain' : undefined,
              action.shape === 'plain' && !action.label ? 'takt-button-plain-icon-only' : undefined,
              action.shape === 'circle' ? 'takt-button-circle' : undefined
            ]"
            :disabled="action.disabled"
            :loading="action.loading"
            @click="handleAction(action)"
          >
            <template
              v-if="action.icon"
              #icon
            >
              <i
                v-if="typeof action.icon === 'string'"
                :class="action.icon"
              />
              <component
                :is="action.icon"
                v-else
              />
            </template>
            <template v-if="action.shape !== 'circle' && action.label">
              {{ action.label }}
            </template>
          </a-button>
        </a-space>
      </slot>
    </div>

    <!-- 右侧：工具按钮（原生按钮组 a-button-group + a-button，不需要权限验证） -->
    <div class="tools-bar-right">
      <slot name="right">
        <a-button-group
          v-if="hasRightTools"
          class="takt-tools-bar-right-group"
        >
          <!-- 展开/收缩 -->
          <a-tooltip
            v-if="showExpand"
            :title="isExpanded ? t('common.button.collapse') : t('common.button.expand')"
          >
            <a-button
              class="takt-button-expand takt-button-plain takt-button-plain-icon-only"
              :disabled="expandDisabled"
              @click="handleExpand"
            >
              <template #icon>
                <RiMenuFoldLine v-if="isExpanded" />
                <RiMenuUnfoldLine v-else />
              </template>
            </a-button>
          </a-tooltip>
          <!-- 高级查询 -->
          <a-tooltip
            v-if="showAdvancedQuery"
            :title="t('common.button.advancedQuery')"
          >
            <a-button
              class="takt-button-query takt-button-plain takt-button-plain-icon-only"
              :disabled="advancedQueryDisabled"
              @click="handleAdvancedQuery"
            >
              <template #icon>
                <RiFilterLine />
              </template>
            </a-button>
          </a-tooltip>
          <!-- 列设置 -->
          <a-tooltip
            v-if="showColumnSetting"
            :title="t('common.button.columnSetting')"
          >
            <a-button
              class="takt-button-config takt-button-plain takt-button-plain-icon-only"
              :disabled="columnSettingDisabled"
              @click="handleColumnSetting"
            >
              <template #icon>
                <RiSettingsLine />
              </template>
            </a-button>
          </a-tooltip>
          <!-- 全屏 -->
          <a-tooltip
            v-if="showFullscreen"
            :title="isFullscreen ? t('common.button.exitFullscreen') : t('common.button.fullscreen')"
          >
            <a-button
              class="takt-button-fullscreen takt-button-plain takt-button-plain-icon-only"
              :disabled="fullscreenDisabled"
              @click="handleFullscreen"
            >
              <template #icon>
                <RiFullscreenExitLine v-if="isFullscreen" />
                <RiFullscreenLine v-else />
              </template>
            </a-button>
          </a-tooltip>
          <!-- 转置 -->
          <a-tooltip
            v-if="showTranspose"
            :title="isTransposed ? t('common.button.toList') : t('common.button.toTranspose')"
          >
            <a-button
              class="takt-button-transpose takt-button-plain takt-button-plain-icon-only"
              :disabled="transposeDisabled"
              @click="handleTranspose"
            >
              <template #icon>
                <RiLayoutRowLine v-if="isTransposed" />
                <RiLayoutColumnLine v-else />
              </template>
            </a-button>
          </a-tooltip>
          <!-- 刷新 -->
          <a-tooltip
            v-if="showRefresh"
            :title="t('common.button.refresh')"
          >
            <a-button
              class="takt-button-refresh takt-button-plain takt-button-plain-icon-only"
              :disabled="refreshDisabled"
              :loading="refreshLoading"
              @click="handleRefresh"
            >
              <template #icon>
                <RiRefreshLine />
              </template>
            </a-button>
          </a-tooltip>
          <!-- 自定义右侧按钮 -->
          <a-tooltip
            v-for="action in filteredRightActions"
            :key="action.key"
            :title="action.tooltip ?? action.label ?? action.key"
          >
            <a-button
              :class="[
                action.buttonClass || `takt-button-${action.key}`,
                action.shape === 'plain' ? 'takt-button-plain' : undefined,
                action.shape === 'plain' && !action.label ? 'takt-button-plain-icon-only' : undefined,
                action.shape === 'circle' ? 'takt-button-circle' : undefined
              ]"
              :disabled="action.disabled"
              :loading="action.loading"
              @click="handleAction(action)"
            >
              <template
                v-if="action.icon"
                #icon
              >
                <i
                  v-if="typeof action.icon === 'string'"
                  :class="action.icon"
                />
                <component
                  :is="action.icon"
                  v-else
                />
              </template>
              <template v-if="action.shape !== 'circle' && action.label">
                {{ action.label }}
              </template>
            </a-button>
          </a-tooltip>
        </a-button-group>
      </slot>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted } from 'vue'
import type { Component } from 'vue'
import { useI18n } from 'vue-i18n'
import { usePermissionStore } from '@/stores/identity/permission'
import {
  RiAddLine,
  RiEditLine,
  RiDeleteBinLine,
  RiImportLine,
  RiExportLine,
  RiSendPlane2Line,
  RiMailSendLine,
  RiFilterLine,
  RiSettingsLine,
  RiFullscreenLine,
  RiFullscreenExitLine,
  RiRefreshLine,
  RiMenuFoldLine,
  RiMenuUnfoldLine,
  RiLayoutColumnLine,
  RiLayoutRowLine,
  RiInsertRowBottom,
  RiDeleteRow,
  RiBrush2Line,
  RiArrowDownSLine
} from '@remixicon/vue'

const { t } = useI18n()

export interface ToolBarAction {
  key: string
  label?: string
  /** 悬停提示文案（右侧图标按钮建议设置） */
  tooltip?: string
  /** 按钮形状：standard（标准，图标+文本）、plain（透明背景，图标或图标+文本）、circle（圆形，只显示图标） */
  shape?: 'standard' | 'plain' | 'circle'
  disabled?: boolean
  loading?: boolean
  /** 图标组件或 CSS 类名（如 'ri-edit-line'） */
  icon?: Component | string
  permission?: string
  /** 按钮样式类名（如：takt-button-detail） */
  buttonClass?: string
  onClick?: (action: ToolBarAction) => void
}

interface Props {
  /** 是否显示 */
  show?: boolean
  /** 新增权限标识（如：identity:user:create） */
  createPermission?: string
  /** 更新权限标识（如：identity:user:update） */
  updatePermission?: string
  /** 删除权限标识（如：identity:user:delete） */
  deletePermission?: string
  /** 导入权限标识（如：identity:user:import） */
  importPermission?: string
  /** 导出权限标识（如：identity:user:export） */
  exportPermission?: string
  /** 发起流程权限标识（如：workflow:instance:start） */
  startFlowPermission?: string
  /** 发送消息权限标识（如：signalr:message:send） */
  sendMessagePermission?: string
  /** 新增行权限标识（与 canImport/canExport 一致：必须显式传入才做权限校验并显示） */
  createRowPermission?: string
  /** 删除行权限标识（与 canImport/canExport 一致：必须显式传入才做权限校验并显示） */
  deleteRowPermission?: string
  /** 清空权限标识（与 canImport/canExport 一致：必须显式传入才做权限校验并显示） */
  emptyPermission?: string
  /** 显示新增按钮（必须同时满足此属性和权限检查） */
  showCreate?: boolean
  /** 显示更新按钮（必须同时满足此属性和权限检查） */
  showUpdate?: boolean
  /** 显示删除按钮（必须同时满足此属性和权限检查） */
  showDelete?: boolean
  /** 显示导入按钮（必须同时满足此属性和权限检查） */
  showImport?: boolean
  /** 显示导出按钮（必须同时满足此属性和权限检查） */
  showExport?: boolean
  /** 显示发起流程按钮（必须同时满足此属性和权限检查） */
  showStartFlow?: boolean
  /** 显示发送消息按钮（必须同时满足此属性和权限检查） */
  showSendMessage?: boolean
  /** 显示高级查询按钮 */
  showAdvancedQuery?: boolean
  /** 显示列设置按钮 */
  showColumnSetting?: boolean
  /** 显示全屏按钮 */
  showFullscreen?: boolean
  /** 显示转置按钮 */
  showTranspose?: boolean
  /** 显示刷新按钮 */
  showRefresh?: boolean
  /** 显示展开/收缩按钮 */
  showExpand?: boolean
  /** 显示新增行按钮（必须同时满足此属性和权限检查） */
  showCreateRow?: boolean
  /** 显示删除行按钮（必须同时满足此属性和权限检查） */
  showDeleteRow?: boolean
  /** 显示清空按钮（下拉：清空7天、清空30天、清空全部） */
  showEmpty?: boolean
  /** 创建按钮禁用 */
  createDisabled?: boolean
  /** 更新按钮禁用 */
  updateDisabled?: boolean
  /** 删除按钮禁用 */
  deleteDisabled?: boolean
  /** 导入按钮禁用 */
  importDisabled?: boolean
  /** 导出按钮禁用 */
  exportDisabled?: boolean
  /** 发起流程按钮禁用 */
  startFlowDisabled?: boolean
  /** 发送消息按钮禁用 */
  sendMessageDisabled?: boolean
  /** 高级查询按钮禁用 */
  advancedQueryDisabled?: boolean
  /** 列设置按钮禁用 */
  columnSettingDisabled?: boolean
  /** 全屏按钮禁用 */
  fullscreenDisabled?: boolean
  /** 转置按钮禁用 */
  transposeDisabled?: boolean
  /** 刷新按钮禁用 */
  refreshDisabled?: boolean
  /** 展开/收缩按钮禁用 */
  expandDisabled?: boolean
  /** 新增行按钮禁用 */
  createRowDisabled?: boolean
  /** 删除行按钮禁用 */
  deleteRowDisabled?: boolean
  /** 清空按钮禁用 */
  emptyDisabled?: boolean
  /** 创建按钮加载状态 */
  createLoading?: boolean
  /** 更新按钮加载状态 */
  updateLoading?: boolean
  /** 删除按钮加载状态 */
  deleteLoading?: boolean
  /** 导入按钮加载状态 */
  importLoading?: boolean
  /** 导出按钮加载状态 */
  exportLoading?: boolean
  /** 发起流程按钮加载状态 */
  startFlowLoading?: boolean
  /** 发送消息按钮加载状态 */
  sendMessageLoading?: boolean
  /** 刷新按钮加载状态 */
  refreshLoading?: boolean
  /** 新增行按钮加载状态 */
  createRowLoading?: boolean
  /** 删除行按钮加载状态 */
  deleteRowLoading?: boolean
  /** 清空按钮加载状态 */
  emptyLoading?: boolean
  /** 自定义左侧操作按钮 */
  leftActions?: ToolBarAction[]
  /** 自定义右侧操作按钮 */
  rightActions?: ToolBarAction[]
}

const props = withDefaults(defineProps<Props>(), {
  show: true,
  createPermission: undefined,
  updatePermission: undefined,
  deletePermission: undefined,
  importPermission: undefined,
  exportPermission: undefined,
  startFlowPermission: undefined,
  sendMessagePermission: undefined,
  createRowPermission: undefined,
  deleteRowPermission: undefined,
  emptyPermission: undefined,
  showCreate: false,
  showUpdate: false,
  showDelete: false,
  showImport: false,
  showExport: false,
  showStartFlow: false,
  showSendMessage: false,
  showAdvancedQuery: true,
  showColumnSetting: true,
  showFullscreen: false,
  showTranspose: false,
  showRefresh: false,
  showCreateRow: true,
  showDeleteRow: true,
  showEmpty: false,
  createDisabled: false,
  updateDisabled: false,
  deleteDisabled: false,
  importDisabled: false,
  exportDisabled: false,
  startFlowDisabled: false,
  sendMessageDisabled: false,
  advancedQueryDisabled: false,
  columnSettingDisabled: false,
  fullscreenDisabled: false,
  transposeDisabled: false,
  refreshDisabled: false,
  expandDisabled: false,
  createRowDisabled: false,
  deleteRowDisabled: false,
  emptyDisabled: false,
  createLoading: false,
  updateLoading: false,
  deleteLoading: false,
  importLoading: false,
  exportLoading: false,
  startFlowLoading: false,
  sendMessageLoading: false,
  refreshLoading: false,
  createRowLoading: false,
  deleteRowLoading: false,
  emptyLoading: false,
  leftActions: () => [],
  rightActions: () => []
})

const emit = defineEmits<{
  'action': [action: ToolBarAction]
  'create': []
  'update': []
  'delete': []
  'import': []
  'export': []
  'start-flow': []
  'send-message': []
  'advanced-query': []
  'column-setting': []
  'fullscreen': [isFullscreen: boolean]
  'transpose': [isTransposed: boolean]
  'refresh': []
  'expand': [isExpanded: boolean]
  'create-row': []
  'delete-row': []
  'empty-7d': []
  'empty-30d': []
  'empty-all': []
}>()

const permissionStore = usePermissionStore()
const isFullscreen = ref(false)
const isExpanded = ref(false)
const isTransposed = ref(false)

// 权限检查（严格检查：必须同时满足 show-* 和权限）
const canCreate = computed(() => {
  if (!props.showCreate) return false
  if (!props.createPermission) return false
  const hasPerm = permissionStore.hasPermission(props.createPermission)
  if (import.meta.env.DEV && !hasPerm) {
    logger.debug('[TaktToolsBar] 创建按钮权限检查失败:', {
      permission: props.createPermission,
      permissionsList: permissionStore.permissions,
      hasPermission: hasPerm
    })
  }
  return hasPerm
})

const canStartFlow = computed(() => {
  if (!props.showStartFlow) return false
  if (!props.startFlowPermission) return false
  return permissionStore.hasPermission(props.startFlowPermission)
})

const canSendMessage = computed(() => {
  if (!props.showSendMessage) return false
  if (!props.sendMessagePermission) return false
  return permissionStore.hasPermission(props.sendMessagePermission)
})

// 与 canImport/canExport 一致：仅当显式传入对应 permission 时才校验并显示
const canCreateRow = computed(() => {
  if (!props.showCreateRow) return false
  if (!props.createRowPermission) return false
  return permissionStore.hasPermission(props.createRowPermission)
})

const canUpdate = computed(() => {
  if (!props.showUpdate) return false
  if (!props.updatePermission) return false
  return permissionStore.hasPermission(props.updatePermission)
})

const canDelete = computed(() => {
  if (!props.showDelete) return false
  if (!props.deletePermission) return false
  return permissionStore.hasPermission(props.deletePermission)
})

const canDeleteRow = computed(() => {
  if (!props.showDeleteRow) return false
  if (!props.deleteRowPermission) return false
  return permissionStore.hasPermission(props.deleteRowPermission)
})

const canImport = computed(() => {
  if (!props.showImport) return false
  if (!props.importPermission) return false
  return permissionStore.hasPermission(props.importPermission)
})

const canExport = computed(() => {
  if (!props.showExport) return false
  if (!props.exportPermission) return false
  return permissionStore.hasPermission(props.exportPermission)
})

const canEmpty = computed(() => {
  if (!props.showEmpty) return false
  if (!props.emptyPermission) return false
  return permissionStore.hasPermission(props.emptyPermission)
})

// 过滤自定义按钮（根据权限）
const filteredLeftActions = computed(() => {
  return props.leftActions.filter(action => {
    if (action.permission) {
      return permissionStore.hasPermission(action.permission)
    }
    return true
  })
})

const filteredRightActions = computed(() => {
  return props.rightActions.filter(action => {
    if (action.permission) {
      return permissionStore.hasPermission(action.permission)
    }
    return true
  })
})

// 是否有右侧工具（用于决定是否渲染 a-button-group）
const hasRightTools = computed(() => {
  return (
    props.showExpand ||
    props.showAdvancedQuery ||
    props.showColumnSetting ||
    props.showFullscreen ||
    props.showTranspose ||
    props.showRefresh ||
    filteredRightActions.value.length > 0
  )
})

// 全屏状态监听
const handleFullscreenChange = () => {
  isFullscreen.value = !!document.fullscreenElement
}

onMounted(() => {
  document.addEventListener('fullscreenchange', handleFullscreenChange)
})

onUnmounted(() => {
  document.removeEventListener('fullscreenchange', handleFullscreenChange)
})

// 常用按钮处理
const handleCreate = () => {
  emit('create')
}

const handleUpdate = () => {
  emit('update')
}

const handleDelete = () => {
  emit('delete')
}

const handleImport = () => {
  emit('import')
}

const handleExport = () => {
  emit('export')
}

const handleStartFlow = () => {
  emit('start-flow')
}

const handleSendMessage = () => {
  emit('send-message')
}

// 工具按钮处理
const handleAdvancedQuery = () => {
  emit('advanced-query')
}

const handleColumnSetting = () => {
  emit('column-setting')
}

const handleFullscreen = () => {
  if (!document.fullscreenElement) {
    document.documentElement.requestFullscreen().then(() => {
      isFullscreen.value = true
      emit('fullscreen', true)
    }).catch(() => {
      logger.error('[TaktToolsBar] 无法进入全屏模式')
    })
  } else {
    document.exitFullscreen().then(() => {
      isFullscreen.value = false
      emit('fullscreen', false)
    }).catch(() => {
      logger.error('[TaktToolsBar] 无法退出全屏模式')
    })
  }
}

const handleTranspose = () => {
  isTransposed.value = !isTransposed.value
  emit('transpose', isTransposed.value)
}

const handleRefresh = () => {
  emit('refresh')
}

const handleExpand = () => {
  isExpanded.value = !isExpanded.value
  emit('expand', isExpanded.value)
}

// 新增行/删除行按钮处理
const handleCreateRow = () => {
  emit('create-row')
}

const handleDeleteRow = () => {
  emit('delete-row')
}

const handleEmptyMenuClick = (info: { key: string | number }) => {
  const key = String(info.key)
  if (key === '7d') emit('empty-7d')
  else if (key === '30d') emit('empty-30d')
  else if (key === 'all') emit('empty-all')
}

// 自定义按钮处理
const handleAction = (action: ToolBarAction) => {
  if (action.onClick) {
    action.onClick(action)
  }
  emit('action', action)
}
</script>

<style scoped lang="less">
.takt-tools-bar {
  margin: 0 4px 4px 4px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 16px;

  .tools-bar-left {
    display: flex;
    align-items: center;
    flex: 1;

    :deep(.ant-btn) {
      display: inline-flex;
      align-items: center;
      gap: 4px;

      .anticon {
        margin-inline-end: 0 !important;
      }
    }
  }

  .tools-bar-right {
    display: flex;
    align-items: center;
    flex-shrink: 0;
  }
}

// 清空按钮及下拉箭头（确保有样式）
.takt-button-empty {
  display: inline-flex;
  align-items: center;
  gap: 4px;

  .takt-button-empty-arrow {
    margin-left: 2px;
    font-size: 12px;
    line-height: 1;
    display: inline-flex;
    align-items: center;
    opacity: 0.85;
  }
}

// 右侧原生按钮组：与左侧按钮同高（默认 middle），plain 图标风格
.takt-tools-bar-right-group {
  :deep(.ant-btn) {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    min-width: 32px;
    height: 32px;
    padding: 0 8px;
    line-height: 1;
  }
}

// 禁用样式由全局 button.less 统一提供（.ant-btn[class*="takt-button-"]:disabled），此处不再重复

@media (max-width: 768px) {
  .takt-tools-bar {
    flex-direction: column;
    align-items: stretch;

    .tools-bar-left,
    .tools-bar-right {
      justify-content: center;
    }
  }
}
</style>
