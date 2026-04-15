<template>
  <a-breadcrumb v-if="show">
    <a-breadcrumb-item v-for="(item, index) in breadcrumbItems" :key="index">
      <router-link v-if="item.path && index < breadcrumbItems.length - 1" :to="item.path" class="breadcrumb-link">
        <component v-if="item.icon" :is="item.icon" />
        <span class="breadcrumb-title">{{ item.title }}</span>
      </router-link>
      <span v-else class="breadcrumb-plain">
        <component v-if="item.icon" :is="item.icon" />
        <span class="breadcrumb-title">{{ item.title }}</span>
      </span>
    </a-breadcrumb-item>
  </a-breadcrumb>
</template>

<script setup lang="ts">
import { computed, ref, markRaw } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'
import { useMenuStore } from '@/stores/identity/menu'
import type { MenuTree } from '@/types/identity/menu'

interface BreadcrumbItem {
  title: string
  path?: string
  icon?: any
}

const route = useRoute()
const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)
const { t } = useI18n()
const menuStore = useMenuStore()

// 图标缓存
const iconCache = ref<Record<string, any>>({})

// 预加载图标（使用 @remixicon/vue 组件名，如 RiHomeLine）
const preloadIcon = async (iconName: string) => {
  if (iconCache.value[iconName]) {
    return
  }
  try {
    const module = await import('@remixicon/vue')
    const IconComponent = (module as any)[iconName]
    if (IconComponent) {
      iconCache.value[iconName] = markRaw(IconComponent)
    }
  } catch (error) {
    // 图标加载失败，忽略
  }
}


// 从菜单树中查找菜单项（通过 path 匹配，后端已统一转换为 camelCase）
const findMenuByPath = (menus: MenuTree[], path: string): MenuTree | null => {
  for (const menu of menus) {
    const menuPath = menu.path || (menu as any).extValue || ''
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
const getTranslatedTitle = (menu: MenuTree | null, routeMeta: any): string => {
  if (menu) {
    const menuL10nKey = menu.menuL10nKey || (menu as any).transKey
    if (menuL10nKey) {
      try {
        const translated = t(menuL10nKey)
        // 如果翻译结果有效且不等于 key，使用翻译结果
        if (translated && translated !== menuL10nKey) {
          return translated
        }
      } catch (error) {
        // 翻译失败，继续使用 menuName
      }
    }
    const menuName = menu.menuName || (menu as any).dictLabel || ''
    if (menuName) {
      return menuName
    }
  }
  // 如果没有找到菜单，使用路由 meta 中的 title（支持 i18n key）
  const titleKey = (routeMeta?.title || routeMeta?.titleKey || '') as string
  return titleKey ? t(titleKey) : ''
}

const show = computed(() => settingSafe.value.showBreadcrumb)

const breadcrumbItems = computed(() => {
  // 访问 iconCache 以建立响应式依赖
  void iconCache.value
  
  const items: BreadcrumbItem[] = []
  const matched = route.matched.filter(item => item.meta && item.meta.title)
  
  matched.forEach((item, index) => {
    const routePath = item.path
    // 从菜单 store 中查找对应的菜单项（后端已统一转换为 camelCase）
    const menu = routePath ? findMenuByPath(menuStore.menuList, routePath) : null
    
    // 获取图标
    const menuIcon = menu ? menu.menuIcon : (item.meta?.icon as string)
    
    // 预加载图标（异步）
    if (menuIcon) {
      preloadIcon(menuIcon)
    }
    
    // 获取图标组件（可能还未加载完成，但会在加载完成后自动更新）
    const iconComponent = menuIcon ? iconCache.value[menuIcon] : undefined
    
    // 获取翻译后的标题
    const title = getTranslatedTitle(menu, item.meta)
    
    items.push({
      title: title || item.name as string || '',
      path: index < matched.length - 1 ? routePath : undefined,
      icon: setting.value.breadcrumbIcon ? iconComponent : undefined
    })
  })
  
  return items
})
</script>

<style scoped lang="less">
// 图标与文本间隔统一 6px
:deep(.breadcrumb-link),
:deep(.breadcrumb-plain) {
  .breadcrumb-title {
    margin-left: 6px;
  }
}
</style>
