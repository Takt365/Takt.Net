<template>
  <a-menu
    v-model:selectedKeys="selectedKeys"
    v-model:openKeys="openKeys"
    mode="inline"
    :theme="theme"
    :items="menuItems"
    :inline-collapsed="collapsed"
    :accordion="setting.menuAccordion"
    @click="handleMenuClick"
    :class="['takt-mix-menu', `menu-style-${setting.menuStyle}`]"
  >
  </a-menu>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import { useThemeStore } from '@/stores/theme'
import { useMenuStore } from '@/stores/identity/menu'
import { useSettingStore } from '@/stores/setting'

interface Props {
  collapsed?: boolean
  theme?: 'light' | 'dark'
}

const props = withDefaults(defineProps<Props>(), {
  collapsed: false,
  theme: 'light'
})

const emit = defineEmits<{
  'click': [key: string]
}>()

const route = useRoute()
const router = useRouter()
const themeStore = useThemeStore()
const menuStore = useMenuStore()
const setting = useSettingStore().setting

const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])

const menuItems = computed(() => menuStore.menuItems)

const theme = computed(() => {
  if (props.theme === 'dark') return 'dark'
  return themeStore.themeMode === 'dark' ? 'dark' : 'light'
})

const handleMenuClick = (info: MenuInfo) => {
  const key = String(info.key)
  router.push(key)
  emit('click', key)
}

watch(
  () => route.path,
  (path) => {
    selectedKeys.value = [path]
    const pathParts = path.split('/').filter(Boolean)
    if (pathParts.length > 1) {
      openKeys.value = ['/' + pathParts[0]]
    }
  },
  { immediate: true }
)
</script>

<style scoped lang="less">
.takt-mix-menu {
  border-right: 0;

  &.menu-style-rounded {
    :deep(.ant-menu-item) {
      border-radius: 4px;
      margin: 4px 8px;
    }
    :deep(.ant-menu-submenu-title) {
      border-radius: 4px;
      margin: 4px 8px;
    }
  }

  &.menu-style-plain {
    :deep(.ant-menu-item) {
      border-radius: 0;
      margin: 0;
    }
    :deep(.ant-menu-submenu-title) {
      border-radius: 0;
      margin: 0;
    }
  }
}
</style>
