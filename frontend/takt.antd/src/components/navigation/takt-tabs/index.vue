<template>
  <div
    v-if="show"
    class="takt-tabs-container"
  >
    <div class="tabs-wrapper">
      <a-tabs
        v-model:active-key="activeKey"
        :class="['takt-tabs', `tab-style-${settingSafe.tabStyle}`]"
        type="editable-card"
        hide-add
        @edit="handleEdit"
        @change="handleChange"
      >
        <a-tab-pane
          v-for="tab in displayTabs"
          :key="tab.key"
          :closable="tab.closable !== false"
        >
          <template #tab>
            <span class="tab-content">
              <component
                :is="tab.icon"
                v-if="tab.icon"
                class="tab-icon"
              />
              <span>{{ tab.title }}</span>
            </span>
          </template>
          <slot
            :name="tab.key"
            :tab="tab"
          >
            <router-view v-if="tab.component" />
          </slot>
        </a-tab-pane>
        <template #rightExtra>
          <div class="tabs-extra">
            <a-dropdown
              :trigger="['click']"
              placement="bottomRight"
            >
              <a-button
                type="text"
                class="tabs-dropdown-btn"
              >
                <template #icon>
                  <RiArrowDownSLine />
                </template>
              </a-button>
              <template #overlay>
                <a-menu @click="handleMenuClick">
                  <a-menu-item key="refresh">
                    {{ t('components.navigation.tabs.refreshCurrent') }}
                  </a-menu-item>
                  <a-menu-item
                    key="close-current"
                    :disabled="activeKey === '/dashboard/workspace' || displayTabs.length <= 1"
                  >
                    {{ t('components.navigation.tabs.closeCurrent') }}
                  </a-menu-item>
                  <a-menu-item
                    key="close-right"
                    :disabled="!hasRightTabs"
                  >
                    {{ t('components.navigation.tabs.closeRight') }}
                  </a-menu-item>
                  <a-menu-item
                    key="close-left"
                    :disabled="!hasLeftTabs"
                  >
                    {{ t('components.navigation.tabs.closeLeft') }}
                  </a-menu-item>
                  <a-menu-item
                    key="close-other"
                    :disabled="displayTabs.length <= 1"
                  >
                    {{ t('components.navigation.tabs.closeOthers') }}
                  </a-menu-item>
                  <a-menu-item
                    key="close-all"
                    :disabled="displayTabs.length <= 1"
                  >
                    {{ t('components.navigation.tabs.closeAll') }}
                  </a-menu-item>
                </a-menu>
              </template>
            </a-dropdown>
            <a-button
              type="text"
              class="tabs-fullscreen-btn"
              :title="isFullscreen ? t('common.button.exitFullscreen') : t('common.button.fullscreen')"
              @click="handleToggleFullscreen"
            >
              <template #icon>
                <RiFullscreenExitLine v-if="isFullscreen" />
                <RiFullscreenLine v-else />
              </template>
            </a-button>
          </div>
        </template>
      </a-tabs>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted, onUnmounted, nextTick, markRaw } from 'vue'
import type { Component } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import type { RouteMeta } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { RiArrowDownSLine, RiFullscreenLine, RiFullscreenExitLine } from '@remixicon/vue'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'
import { useMenuStore } from '@/stores/identity/menu'
import type { MenuTree } from '@/types/identity/menu'

interface Tab {
  key: string
  title: string
  path: string
  closable?: boolean
  component?: Component
  icon?: Component
}
type MenuTreeLegacy = MenuTree & {
  extValue?: string
  transKey?: string
  dictLabel?: string
}
type StoredTab = {
  key: string
  title: string
  path: string
  closable?: boolean
}

interface Props {
  tabs?: Tab[]
  maxTabs?: number
}

const props = withDefaults(defineProps<Props>(), {
  tabs: () => [],
  maxTabs: 10
})

const emit = defineEmits<{
  'update:activeKey': [key: string]
  'change': [key: string]
  'edit': [key: string, action: 'add' | 'remove']
  'close-current': [key: string]
  'close-right': [key: string]
  'close-left': [key: string]
  'close-other': [key: string]
  'close-all': []
  'refresh': [key: string]
}>()

const route = useRoute()
const router = useRouter()
const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)
const { t, locale: i18nLocale } = useI18n()
const menuStore = useMenuStore()

// 显示标签页：showTabs 或 multiTab 任一为 true 时显示
const show = computed(() => settingSafe.value.showTabs || settingSafe.value.multiTab)
const activeKey = ref<string>('')
const tabsList = ref<Tab[]>([])
const isFullscreen = ref(false)

// 图标缓存
const iconCache = ref<Record<string, Component>>({})

// 预加载图标（使用 @remixicon/vue 组件名，如 RiHomeLine）
const preloadIcon = async (iconName: string) => {
  if (iconCache.value[iconName]) {
    return
  }
  try {
    const module = await import('@remixicon/vue')
    const iconModule = module as Record<string, unknown>
    const IconComponent = iconModule[iconName]
    if (IconComponent) {
      iconCache.value[iconName] = markRaw(IconComponent as Component)
    }
  } catch {
    // 图标加载失败，忽略
  }
}

// 从菜单树中查找菜单项（通过 path 匹配，后端已统一转换为 camelCase）
const findMenuByPath = (menus: MenuTree[], path: string): MenuTree | null => {
  for (const menu of menus) {
    const menuLegacy = menu as MenuTreeLegacy
    const menuPath = menu.path || menuLegacy.extValue || ''
    if (menuPath === path) {
      return menu
    }
    if (menu.children) {
      const found = findMenuByPath(menu.children, path)
      if (found) {
        return found
      }
    }
  }
  return null
}

// 获取翻译文本（后端已统一转换为 camelCase）
const getTranslatedTitle = (menu: MenuTree | null, routeMeta: RouteMeta | null): string => {
  if (menu) {
    const menuLegacy = menu as MenuTreeLegacy
    const menuL10nKey = menu.menuL10nKey || menuLegacy.transKey
    if (menuL10nKey) {
      try {
        const translated = t(menuL10nKey)
        // 如果翻译结果有效且不等于 key，使用翻译结果
        if (translated && translated !== menuL10nKey) {
          return translated
        }
      } catch {
        // 翻译失败，继续使用 menuName
      }
    }
    const menuName = menu.menuName || menuLegacy.dictLabel || ''
    if (menuName) {
      return menuName
    }
  }
  // 如果没有找到菜单，使用路由 meta 中的 title
  return routeMeta?.title as string || routeMeta?.titleKey as string || ''
}

// 管理标签列表
const manageTabs = async () => {
  const currentPath = route.path
  const matchedRoute = route.matched[route.matched.length - 1]
  const routeMeta = matchedRoute?.meta
  
  // 从菜单 store 中查找对应的菜单项（后端已统一转换为 camelCase）
  const menu = findMenuByPath(menuStore.menuList, currentPath)
  const menuIcon = menu ? menu.menuIcon : (routeMeta?.icon as string)
  
  const homePath = '/dashboard/workspace'
  const homeMenu = findMenuByPath(menuStore.menuList, homePath)
  const homeMenuIcon = homeMenu ? homeMenu.menuIcon : undefined
  
  // 先等待所需图标加载完成，再读写 tabs，避免首次不显示
  const toPreload = [menuIcon, homeMenuIcon].filter(Boolean) as string[]
  const unique = [...new Set(toPreload)]
  await Promise.all(unique.map((name) => preloadIcon(name)))
  
  const iconComponent = menuIcon ? iconCache.value[menuIcon] : undefined
  const homeIconComponent = homeMenuIcon ? iconCache.value[homeMenuIcon] : undefined
  
  // 获取翻译后的标题
  const currentTitle = getTranslatedTitle(menu, routeMeta) || routeMeta?.title as string || route.name as string || t('components.navigation.tabs.unnamed')
  const homeTitle = getTranslatedTitle(homeMenu, null) || t('components.navigation.tabs.home')
  
  const homeTab = tabsList.value.find(t => t.key === homePath)
  
  if (!homeTab) {
    // 如果首页标签不存在，添加到第一个位置
    tabsList.value.unshift({
      key: homePath,
      title: homeTitle,
      path: homePath,
      closable: false,
      icon: homeIconComponent
    })
  } else {
    // 确保首页标签在第一个位置
    const homeIndex = tabsList.value.findIndex(t => t.key === homePath)
    if (homeIndex > 0) {
      tabsList.value.splice(homeIndex, 1)
      tabsList.value.unshift(homeTab)
    }
    // 确保首页标签不可关闭，并更新标题和图标
    homeTab.closable = false
    homeTab.title = homeTitle
    homeTab.icon = homeIconComponent
  }
  
  // 检查当前路由标签是否已存在（排除首页）
  const existingTab = tabsList.value.find(t => t.key === currentPath && t.key !== homePath)
  
  if (!existingTab && currentPath !== homePath) {
    // 如果标签数量达到上限，移除最后一个标签（但保留首页）
    const maxTabsValue = props.maxTabs || settingSafe.value.maxTabs || 10
    if (tabsList.value.length >= maxTabsValue) {
      const lastTab = tabsList.value[tabsList.value.length - 1]
      if (lastTab.key !== homePath) {
        tabsList.value.pop()
      } else {
        // 如果最后一个是首页，移除倒数第二个
        tabsList.value.splice(tabsList.value.length - 2, 1)
      }
    }
    
    // 添加新标签（在首页之后）
    tabsList.value.push({
      key: currentPath,
      title: currentTitle,
      path: currentPath,
      closable: true,
      icon: iconComponent
    })
  } else if (existingTab) {
    // 如果标签已存在，更新标题和图标
    existingTab.title = currentTitle
    existingTab.icon = iconComponent
  }
  
  // 更新当前活动标签
  activeKey.value = currentPath
}

// 显示的标签（最多 maxTabs 个）
const displayTabs = computed(() => {
  const maxTabsValue = props.maxTabs || settingSafe.value.maxTabs || 10
  if (props.tabs.length > 0) {
    return props.tabs.slice(0, maxTabsValue)
  }
  return tabsList.value.slice(0, maxTabsValue)
})

// 判断当前标签右侧是否有其他标签（除了首页）
const hasRightTabs = computed(() => {
  const currentIndex = tabsList.value.findIndex(t => t.key === activeKey.value)
  if (currentIndex < 0) return false
  // 如果当前标签不是最后一个，且右侧还有标签（排除首页），则有右侧标签
  if (currentIndex < tabsList.value.length - 1) {
    // 检查右侧是否有非首页的标签
    for (let i = currentIndex + 1; i < tabsList.value.length; i++) {
      if (tabsList.value[i].key !== '/dashboard/workspace') {
        return true
      }
    }
  }
  return false
})

// 判断当前标签左侧是否有其他标签（除了首页）
const hasLeftTabs = computed(() => {
  const currentIndex = tabsList.value.findIndex(t => t.key === activeKey.value)
  if (currentIndex <= 0) return false // 索引0是首页，没有左侧标签
  // 检查左侧是否有非首页的标签
  for (let i = currentIndex - 1; i > 0; i--) {
    if (tabsList.value[i].key !== '/dashboard/workspace') {
      return true
    }
  }
  return false
})

// 持久化标签页到 localStorage
const saveTabsToStorage = () => {
  if (settingSafe.value.persistTabs) {
    const tabsData = tabsList.value.map(tab => ({
      key: tab.key,
      title: tab.title,
      path: tab.path,
      closable: tab.closable
    }))
    localStorage.setItem('takt-tabs', JSON.stringify({
      tabs: tabsData,
      activeKey: activeKey.value
    }))
  }
}

// 从 localStorage 恢复标签页
const loadTabsFromStorage = () => {
  if (settingSafe.value.persistTabs) {
    const stored = localStorage.getItem('takt-tabs')
    if (stored) {
      try {
        const data = JSON.parse(stored)
        if (data.tabs && Array.isArray(data.tabs)) {
          // 恢复标签列表（但需要重新加载图标）
          tabsList.value = data.tabs.map((tab: StoredTab) => ({
            key: tab.key,
            title: tab.title,
            path: tab.path,
            closable: tab.closable !== false,
            icon: undefined // 图标需要重新加载
          }))
          // 恢复活动标签
          if (data.activeKey && tabsList.value.find(t => t.key === data.activeKey)) {
            activeKey.value = data.activeKey
            router.push(data.activeKey)
          }
        }
      } catch {
        // 解析失败，忽略
      }
    }
  }
}

// 监听路由变化
watch(() => route.path, () => {
  manageTabs()
  saveTabsToStorage()
}, { immediate: true })

// 重新计算所有标签页的标题（用于语言切换后更新）
const refreshTabsTitles = () => {
  tabsList.value.forEach(tab => {
    const menu = findMenuByPath(menuStore.menuList, tab.path)
    // 从路由匹配中查找对应的路由 meta
    const matchedRoute = router.resolve(tab.path)
    const routeMeta = matchedRoute.matched[matchedRoute.matched.length - 1]?.meta
    const newTitle = getTranslatedTitle(menu, routeMeta) || tab.title
    tab.title = newTitle
  })
  saveTabsToStorage()
}

// 监听语言变化，重新计算所有标签页的标题
// 延迟更新以确保翻译数据已经加载完成
watch(() => i18nLocale.value, async (newLocale, oldLocale) => {
  // 如果语言没有变化，跳过
  if (newLocale === oldLocale) return
  
  // 先立即更新一次（使用静态翻译或 fallback）
  refreshTabsTitles()
  
  // 等待 Vue 的响应式更新完成
  await nextTick()
  
  // 延迟再次更新，确保后端翻译数据已加载完成
  // 使用多次延迟更新，确保在所有异步操作完成后更新
  setTimeout(() => {
    refreshTabsTitles()
  }, 300)
  
  // 再次延迟更新，处理可能的网络延迟
  setTimeout(() => {
    refreshTabsTitles()
  }, 1000)
  
  // 最后延迟更新，确保所有翻译数据都已加载
  setTimeout(() => {
    refreshTabsTitles()
  }, 2000)
})

// 监听设置变化，如果关闭持久化则清除存储
watch(() => settingSafe.value.persistTabs, (enabled) => {
  if (!enabled) {
    localStorage.removeItem('takt-tabs')
  } else {
    saveTabsToStorage()
  }
})

// 监听标签列表变化，保存到存储
watch(tabsList, () => {
  saveTabsToStorage()
}, { deep: true })

// 监听活动标签变化，保存到存储
watch(activeKey, () => {
  saveTabsToStorage()
})

// 组件挂载时恢复标签页
onMounted(() => {
  if (settingSafe.value.persistTabs) {
    loadTabsFromStorage()
  }
})

// 标签切换
const handleChange = (key: string | number) => {
  const keyStr = String(key)
  activeKey.value = keyStr
  const tab = displayTabs.value.find(t => t.key === keyStr)
  if (tab && tab.path) {
    router.push(tab.path)
  }
  emit('change', keyStr)
}

// 标签编辑（关闭）
const handleEdit = (targetKey: string | number | MouseEvent | KeyboardEvent, action: 'add' | 'remove') => {
  if (action === 'remove') {
    const keyStr = typeof targetKey === 'string' || typeof targetKey === 'number' ? String(targetKey) : ''
    if (keyStr) {
      // 不允许关闭首页标签
      if (keyStr === '/dashboard/workspace') {
        return
      }
      
      const index = tabsList.value.findIndex(t => t.key === keyStr)
      if (index > -1) {
        tabsList.value.splice(index, 1)
        
        // 如果关闭的是当前标签，切换到其他标签（优先切换到首页）
        if (keyStr === activeKey.value && tabsList.value.length > 0) {
          const homeTab = tabsList.value.find(t => t.key === '/dashboard/workspace')
          const nextTab = homeTab || tabsList.value[tabsList.value.length - 1]
          activeKey.value = nextTab.key
          router.push(nextTab.path)
        }
        
        emit('edit', keyStr, action)
      }
    }
  }
}

// 下拉菜单点击
const handleMenuClick = (info: MenuInfo) => {
  const key = String(info.key)
  const homePath = '/dashboard/workspace'
  switch (key) {
    case 'refresh':
      // 刷新当前标签
      emit('refresh', activeKey.value)
      // 重新加载当前路由
      router.go(0)
      break
    case 'close-current':
      // 关闭当前标签（不允许关闭首页）
      if (activeKey.value !== homePath) {
        handleEdit(activeKey.value, 'remove')
        emit('close-current', activeKey.value)
      }
      break
    case 'close-right': {
      // 关闭右侧标签
      const currentIndex = tabsList.value.findIndex(t => t.key === activeKey.value)
      if (currentIndex >= 0 && currentIndex < tabsList.value.length - 1) {
        // 从右侧开始删除，保留首页
        const tabsToRemove: string[] = []
        for (let i = tabsList.value.length - 1; i > currentIndex; i--) {
          const tab = tabsList.value[i]
          if (tab.key !== homePath) {
            tabsToRemove.push(tab.key)
            tabsList.value.splice(i, 1)
          }
        }
        emit('close-right', activeKey.value)
      }
      break
    }
    case 'close-left': {
      // 关闭左侧标签（保留首页和当前标签）
      const currentIndexLeft = tabsList.value.findIndex(t => t.key === activeKey.value)
      if (currentIndexLeft > 1) {
        // 从索引1开始删除到当前索引之前（索引0是首页，保留）
        const tabsToRemove: string[] = []
        for (let i = currentIndexLeft - 1; i > 0; i--) {
          const tab = tabsList.value[i]
          if (tab.key !== homePath) {
            tabsToRemove.push(tab.key)
            tabsList.value.splice(i, 1)
          }
        }
        emit('close-left', activeKey.value)
      }
      break
    }
    case 'close-other': {
      // 关闭其他标签，只保留当前标签和首页标签
      const currentTab = tabsList.value.find(t => t.key === activeKey.value)
      const homeTab = tabsList.value.find(t => t.key === homePath)
      if (currentTab && homeTab) {
        if (activeKey.value === homePath) {
          // 如果当前是首页，只保留首页
          tabsList.value.splice(0, tabsList.value.length, homeTab)
        } else {
          // 如果当前不是首页，保留首页和当前标签
          tabsList.value.splice(0, tabsList.value.length, homeTab, currentTab)
        }
        emit('close-other', activeKey.value)
      }
      break
    }
    case 'close-all': {
      // 关闭所有标签，但保留首页标签
      const homeTabOnly = tabsList.value.find(t => t.key === homePath)
      if (homeTabOnly) {
        tabsList.value.splice(0, tabsList.value.length, homeTabOnly)
        activeKey.value = homePath
        router.push(homePath)
      } else {
        // 如果首页标签不存在，创建它（后端已统一转换为 camelCase）
        const homeMenu = findMenuByPath(menuStore.menuList, homePath)
        const homeMenuIcon = homeMenu ? homeMenu.menuIcon : undefined
        
        if (homeMenuIcon) {
          preloadIcon(homeMenuIcon)
        }
        const homeIconComponent = homeMenuIcon ? iconCache.value[homeMenuIcon] : undefined
        const homeTitle = getTranslatedTitle(homeMenu, null) || t('components.navigation.tabs.home')
        
        tabsList.value.splice(0, tabsList.value.length, {
          key: homePath,
          title: homeTitle,
          path: homePath,
          closable: false,
          icon: homeIconComponent
        })
        activeKey.value = homePath
        router.push(homePath)
      }
      emit('close-all')
      break
    }
  }
}

// 全屏切换
const handleToggleFullscreen = () => {
  if (!document.fullscreenElement) {
    document.documentElement.requestFullscreen().then(() => {
      isFullscreen.value = true
    }).catch(() => {
      // 全屏失败，忽略
    })
  } else {
    document.exitFullscreen().then(() => {
      isFullscreen.value = false
    }).catch(() => {
      // 退出全屏失败，忽略
    })
  }
}

// 监听全屏状态变化
const handleFullscreenChange = () => {
  isFullscreen.value = !!document.fullscreenElement
}

onMounted(() => {
  document.addEventListener('fullscreenchange', handleFullscreenChange)
  manageTabs()
})

onUnmounted(() => {
  document.removeEventListener('fullscreenchange', handleFullscreenChange)
})
</script>

<style scoped lang="less">
.takt-tabs-container {
  height: 40px;
  display: flex;
  align-items: center;
  padding: 0 8px;
  transition: border-bottom-color 0.25s ease, background-color 0.25s ease;
}

/* 朴素/卡片/谷歌共用同一容器背景，仅底部分隔线不同，避免谷歌→朴素时整条变白出现白边 */
[data-doc-theme='light'] .takt-tabs-container {
  background: #f5f5f5;
  border-bottom: 1px solid #d9d9d9;
}

[data-doc-theme='dark'] .takt-tabs-container {
  background: #1f1f1f;
  border-bottom: 1px solid rgba(255, 255, 255, 0.12);
}

/* 谷歌风格：底边与背景同色，视觉上无分隔线 */
[data-doc-theme='light'] .takt-tabs-container:has(.takt-tabs.tab-style-google) {
  border-bottom-color: #f5f5f5;
}

[data-doc-theme='dark'] .takt-tabs-container:has(.takt-tabs.tab-style-google) {
  border-bottom-color: #1f1f1f;
}

.tabs-wrapper {
  flex: 1;
  display: flex;
  align-items: center;
  height: 40px;
  overflow: hidden;
}

.takt-tabs {
  flex: 1;
  height: 40px;

  /* 风格切换时 tab 与 nav 过渡统一，避免谷歌→朴素出现白边或闪烁 */
  :deep(.ant-tabs-nav) {
    transition: background-color 0.25s ease;
  }
  :deep(.ant-tabs-tab) {
    transition: background-color 0.25s ease, border-color 0.25s ease, border-radius 0.25s ease, margin 0.25s ease;
  }
  
  :deep(.ant-tabs-content-holder) {
    display: none;
  }
  
  /* 标签内图标与文本间隔 4px：用 .tab-icon margin 明确覆盖 Ant Design 对 [iconCls] 的 marginSM，避免 gap 被库样式干扰 */
  :deep(.ant-tabs-tab .ant-tabs-tab-btn) {
    .tab-content {
      display: inline-flex;
      align-items: center;

      .tab-icon {
        font-size: 14px;
        margin-right: 4px;
        margin-left: 0;
      }
    }
  }

  // 标签页风格：卡片（与容器同底色 + 边框，谷歌→卡片时伪元素消失不会白闪）
  &.tab-style-card {
    :deep(.ant-tabs-nav) {
      background: transparent;
    }
    :deep(.ant-tabs-tab) {
      border-radius: 5px 5px 0 0;
      border-bottom: none;
      margin-right: 4px;
      transition: background-color 0.25s ease, border-color 0.25s ease, border-radius 0.25s ease, margin 0.25s ease;
    }
  }

  // 标签页风格：谷歌（box-shadow + clip-path 实现底部反向圆角，参考 Svelte 官网 tab）
  &.tab-style-google {
    :deep(.ant-tabs-nav) {
      background: transparent;
      transition: background-color 0.25s ease;
    }
    :deep(.ant-tabs-ink-bar) {
      display: none;
    }
    :deep(.ant-tabs-tab) {
      position: relative;
      border-radius: 12px 12px 0 0;
      border: 1px solid transparent;
      margin-right: 2px;
      background-color: transparent;
      overflow: visible;
      transition: background-color 0.25s ease, border-color 0.25s ease, border-radius 0.25s ease, margin 0.25s ease;

      &::before,
      &::after {
        position: absolute;
        bottom: 0;
        content: '';
        width: 20px;
        height: 20px;
        border-radius: 100%;
        box-shadow: 0 0 0 40px transparent;
        transition: box-shadow 0.25s ease;
      }
      &::before {
        left: -20px;
        clip-path: inset(50% -10px 0 50%);
      }
      &::after {
        right: -20px;
        clip-path: inset(50% 50% 0 -10px);
      }
    }
  }
}

/* 卡片风格：透明背景 + 边框，与谷歌同底，切换无背景闪烁 */
[data-doc-theme='light'] .takt-tabs.tab-style-card :deep(.ant-tabs-tab) {
  border: 1px solid #d9d9d9;
  background: transparent;
  &.ant-tabs-tab-active {
    background: transparent;
  }
}

[data-doc-theme='dark'] .takt-tabs.tab-style-card :deep(.ant-tabs-tab) {
  border: 1px solid rgba(255, 255, 255, 0.12);
  background: transparent;
  &.ant-tabs-tab-active {
    background: transparent;
  }
}

/* 谷歌风格：边框与容器同色（视觉无框），与卡片切换时仅 border-color 过渡，无闪烁 */
[data-doc-theme='light'] .takt-tabs.tab-style-google :deep(.ant-tabs-tab) {
  border-color: #f5f5f5;
  &:hover {
    background-color: #f0f0f0;
    &::before,
    &::after {
      box-shadow: 0 0 0 30px #f0f0f0;
    }
  }
  &.ant-tabs-tab-active {
    background-color: #fff;
    &::before,
    &::after {
      box-shadow: 0 0 0 30px #fff;
    }
  }
}

[data-doc-theme='dark'] .takt-tabs.tab-style-google :deep(.ant-tabs-tab) {
  border-color: #1f1f1f;
  &:hover {
    background-color: #2a2a2a;
    &::before,
    &::after {
      box-shadow: 0 0 0 30px #2a2a2a;
    }
  }
  &.ant-tabs-tab-active {
    background-color: #262626;
    &::before,
    &::after {
      box-shadow: 0 0 0 30px #262626;
    }
  }
}

.tabs-extra {
  display: flex;
  align-items: center;
  height: 40px;
  gap: 4px;
  
  .tabs-dropdown-btn,
  .tabs-fullscreen-btn {
    height: 32px;
    width: 32px;
    padding: 0;
    display: inline-flex;
    align-items: center;
    justify-content: center;
  }
}
</style>
