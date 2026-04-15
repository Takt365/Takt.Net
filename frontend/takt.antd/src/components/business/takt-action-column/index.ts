import { h } from 'vue'
import type { Component, VNode } from 'vue'
import type { TableColumnsType } from 'ant-design-vue'
import { Button, Space, Tooltip, Dropdown, Menu, MenuItem } from 'ant-design-vue'
import { MoreOutlined } from '@ant-design/icons-vue'
import i18n from '@/locales'
import { usePermissionStore } from '@/stores/identity/permission'
import { logger } from '@/utils/logger'

const t = (key: string) => (i18n.global.t as (k: string) => string)(key)

export interface ActionColumnItem {
  key: string
  label?: string
  /** 按钮形状：standard（标准，图标+文本）、plain（透明背景，图标或图标+文本）、circle（圆形，只显示图标） */
  shape?: 'standard' | 'plain' | 'circle'
  size?: 'small' | 'middle' | 'large'
  disabled?: boolean | ((record: any, index: number) => boolean)
  disabledFn?: (record: any, index: number) => boolean
  loading?: boolean | ((record: any, index: number) => boolean)
  loadingFn?: (record: any, index: number) => boolean
  /** 是否显示按钮，可以是布尔值或函数（根据记录动态判断） */
  visible?: boolean | ((record: any, index: number) => boolean)
  /** 图标组件或 CSS 类名（如 'ri-edit-line'） */
  icon?: Component | string
  permission?: string
  /** 按钮样式类名（如：takt-button-detail） */
  buttonClass?: string
  onClick?: (record: any, index: number) => void
}

export interface ActionColumnOptions {
  title?: string
  width?: number | string
  fixed?: boolean | 'left' | 'right'
  align?: 'left' | 'right' | 'center'
  actions: ActionColumnItem[]
}

/**
 * 创建操作列配置
 * @param options 操作列选项
 * @returns 表格列配置
 */
export function CreateActionColumn(options: ActionColumnOptions): TableColumnsType[0] {
  const {
    title = t('common.action.operation'),
    width = 148, // 4px + 24px×4 + 4px×3 + 4px = 116px
    fixed = 'right',
    align = 'center',
    actions
  } = options

  const permissionStore = usePermissionStore()

  return {
    key: 'action',
    title,
    width,
    fixed,
    align,
    className: 'takt-action-column',
    customRender: ({ record, index }: { record: any; index: number }) => {
      // 严格过滤：只有有权限且可见的操作才显示
      const filteredActions = actions.filter(action => {
        try {
          // 首先检查 visible 属性
          if (action.visible !== undefined) {
            const isVisible = typeof action.visible === 'function' 
              ? action.visible(record, index)
              : action.visible
            if (!isVisible) {
              return false
            }
          }
          
          // 然后检查权限
          if (action.permission) {
            const hasPerm = permissionStore.hasPermission(action.permission)
            if (!hasPerm) {
              return false
            }
          }
          // 如果没有指定 permission，默认显示（允许无权限要求的操作）
          return true
        } catch (error) {
          // 如果 visible 函数或权限检查抛出异常，记录错误但不显示该按钮，避免影响其他按钮
          logger.error('[TaktActionColumn] 操作按钮检查失败:', {
            action: action.key,
            error,
            record
          })
          return false
        }
      })

      // 创建按钮的辅助函数
      const createButton = (action: ActionColumnItem) => {
        const disabled = typeof action.disabled === 'function' 
          ? action.disabled(record, index)
          : (action.disabled || (action.disabledFn && action.disabledFn(record, index)))
        
        const loading = typeof action.loading === 'function'
          ? action.loading(record, index)
          : (action.loading || (action.loadingFn && action.loadingFn(record, index)))

        // 自动推断按钮类名（如果未指定 buttonClass）
        // 操作列中的 plain 按钮使用无边框版本
        const buttonClass = [
          action.buttonClass || (action.key ? `takt-button-${action.key}` : undefined),
          action.shape === 'plain' ? 'takt-button-plain-borderless' : undefined,
          action.shape === 'plain' && !action.label ? 'takt-button-plain-icon-only' : undefined,
          action.shape === 'circle' ? 'takt-button-circle' : undefined
        ].filter(Boolean).join(' ')
        
        const buttonProps = {
          class: buttonClass,
          size: action.size || 'small',
          disabled,
          loading,
                onClick: () => {
                  if (action.onClick) {
                    action.onClick(record, index)
                  }
                }
        }

        const buttonChildren: Array<VNode | string> = []
        if (action.icon) {
          // 支持 Component 或 CSS 类名字符串
          if (typeof action.icon === 'string') {
            // CSS 类名（如 'ri-edit-line'）
            buttonChildren.push(h('i', { class: action.icon }))
          } else {
            // Vue 组件
            buttonChildren.push(h(action.icon))
          }
        }
        // Plain 模式且只显示图标时，不显示文本（使用 tooltip）
        // Circle 模式不显示文本，其他模式显示文本（如果提供）
        if (action.shape !== 'circle' && action.shape !== 'plain' && action.label) {
          buttonChildren.push(action.label)
        }

        const button = h(Button, buttonProps, { default: () => buttonChildren })
        
        // Plain 模式且只显示图标时，使用 tooltip 显示提示文本
        // 注意：使用 render 函数而不是插槽，以减少 Vue 警告
        if (action.shape === 'plain' && action.label && action.icon) {
          return h(Tooltip, { 
            title: action.label,
            // 使用 getPopupContainer 确保 Tooltip 正确渲染
            getPopupContainer: (triggerNode: HTMLElement) => triggerNode.parentElement || document.body
          }, {
            default: () => button
          })
        }
        
        return button
      }

      // <=3 个按钮：显示所有按钮，不显示 More
      // >=4 个按钮：显示前 3 个按钮 + 1 个 More 按钮
      const maxVisibleButtons = 3
      
      // 调试日志
      logger.debug('[TaktActionColumn] 按钮过滤结果:', {
        totalActions: actions.length,
        filteredActionsCount: filteredActions.length,
        filteredActionKeys: filteredActions.map(a => a.key)
      })
      
      // 判断是否需要显示 More 按钮
      const shouldShowMore = filteredActions.length > maxVisibleButtons
      
      const visibleActions = shouldShowMore
        ? filteredActions.slice(0, maxVisibleButtons)
        : filteredActions
      const moreActions = shouldShowMore
        ? filteredActions.slice(maxVisibleButtons)
        : []

      // 调试日志
      logger.debug('[TaktActionColumn] 按钮分组结果:', {
        shouldShowMore,
        visibleActionsCount: visibleActions.length,
        moreActionsCount: moreActions.length,
        visibleActionKeys: visibleActions.map(a => a.key),
        moreActionKeys: moreActions.map(a => a.key)
      })

      const buttons: VNode[] = []

      // 渲染可见的按钮
      visibleActions.forEach(action => {
        buttons.push(createButton(action))
      })

      // 如果有超过3个按钮，创建"更多"下拉菜单
      // 使用 shouldShowMore 确保逻辑正确
      if (shouldShowMore && moreActions.length > 0) {
        // 创建菜单项
        const menuItems = moreActions.map(action => {
          const disabled = typeof action.disabled === 'function' 
            ? action.disabled(record, index)
            : (action.disabled || (action.disabledFn && action.disabledFn(record, index)))
          
          const loading = typeof action.loading === 'function'
            ? action.loading(record, index)
            : (action.loading || (action.loadingFn && action.loadingFn(record, index)))

          // 构建菜单项内容（图标 + 文本）
          const menuItemChildren: Array<VNode | string> = []
          if (action.icon) {
            if (typeof action.icon === 'string') {
              menuItemChildren.push(h('i', { class: action.icon, style: { marginRight: '8px' } }))
            } else {
              menuItemChildren.push(h(action.icon, { style: { marginRight: '8px' } }))
            }
          }
          if (action.label) {
            menuItemChildren.push(action.label)
          }

          // 为菜单项添加对应的按钮样式类，以便应用正确的颜色
          const menuItemClass = action.buttonClass || (action.key ? `takt-button-${action.key}` : undefined)

          return h(
            MenuItem,
            {
              key: action.key,
              class: menuItemClass,
              disabled: disabled || loading,
              onClick: () => {
                if (action.onClick && !disabled && !loading) {
                  action.onClick(record, index)
                }
              }
            },
            { default: () => menuItemChildren }
          )
        })
        
        // 创建菜单
        const menu = h(
          Menu,
          {
            class: 'takt-action-column-dropdown-menu',
            theme: 'light', // 使用浅色主题，但通过 CSS 覆盖背景
            selectable: false // 菜单项不可选中
          },
          { default: () => menuItems }
        )

        const moreButton = h(
          Button,
          {
            class: 'takt-button-more takt-button-plain-borderless takt-button-plain-icon-only',
            size: 'small',
            onClick: (e: Event) => {
              e.stopPropagation()
            }
          },
          {
            default: () => h(MoreOutlined)
          }
        )

        // 为"更多"按钮添加 Tooltip
        // 注意：使用 render 函数而不是插槽，以减少 Vue 警告
        const moreButtonWithTooltip = h(
          Tooltip,
          {
            title: t('common.button.more'),
            // 使用 getPopupContainer 确保 Tooltip 正确渲染
            getPopupContainer: (triggerNode: HTMLElement) => triggerNode.parentElement || document.body
          },
          {
            default: () => moreButton
          }
        )

        // 使用 overlay 插槽方式创建 Dropdown
        buttons.push(
          h(
            Dropdown,
            {
              trigger: ['click'],
              placement: 'bottomRight',
              getPopupContainer: () => {
                // 直接渲染到 body，确保不被表格容器遮挡
                return document.body
              },
              overlayStyle: {
                zIndex: 1060, // 使用更高的 z-index，确保在表格固定列和其他元素上方
                backgroundColor: 'transparent',
                background: 'transparent',
                boxShadow: 'none',
                padding: 0
              },
              overlayClassName: 'takt-action-column-dropdown'
            },
            {
              default: () => moreButtonWithTooltip,
              overlay: () => menu
            }
          )
        )
      }

      return h(Space, { wrap: false, size: 4 }, { default: () => buttons })
    }
  }
}
