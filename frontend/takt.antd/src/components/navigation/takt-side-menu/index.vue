<template>
  <a-menu
    v-model:selectedKeys="selectedKeys"
    v-model:openKeys="openKeys"
    mode="inline"
    :theme="theme"
    :items="menuItems"
    :inline-collapsed="collapsed"
    @click="handleMenuClick"
    :class="['takt-side-menu', `menu-style-${setting.menuStyle}`]"
  >
  </a-menu>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import { storeToRefs } from 'pinia'
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
const { setting } = storeToRefs(useSettingStore())

const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])

const menuItems = computed(() => menuStore.menuItems)

/** 手风琴：只保留“当前展开分支”（最后一个 key 及其祖先），其它子菜单收起 */
function getAccordionOpenKeys(keys: string[]): string[] {
  if (keys.length === 0) return []
  const last = keys[keys.length - 1]
  const branch = keys.filter(k => last === k || last.startsWith(k + '/'))
  return branch.slice().sort((a, b) => a.length - b.length)
}

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

// 手风琴：开启时只允许一个子菜单分支展开（ant-design-vue Menu 无 accordion 属性，用 openKeys 控制）
watch(
  () => [...openKeys.value],
  (newVal) => {
    if (!setting.value.menuAccordion || newVal.length <= 1) return
    const next = getAccordionOpenKeys(newVal)
    if (next.length !== newVal.length || next.some((k, i) => k !== newVal[i])) {
      openKeys.value = next
    }
  },
  { deep: true }
)
</script>

<style scoped lang="less">
.takt-side-menu {
  border-right: 0;

  // 所有菜单项：图标与文本间隔 4px（antd 通过图标后的 span 的 marginInlineStart 控制间距，需覆盖该 span）
  :deep(.ant-menu-item .anticon + span),
  :deep(.ant-menu-item .ant-menu-item-icon + span),
  :deep(.ant-menu-submenu-title .anticon + span),
  :deep(.ant-menu-submenu-title .ant-menu-item-icon + span) {
    margin-inline-start: 4px !important;
  }

  // 按层级缩进：顶级 8px，每层级递进 8px（与菜单类型无关）
  :deep(.ant-menu-item),
  :deep(.ant-menu-submenu-title) {
    padding-inline-start: 8px !important; // 顶级
  }
  :deep(.ant-menu-sub .ant-menu-item),
  :deep(.ant-menu-sub .ant-menu-submenu-title) {
    padding-inline-start: 16px !important; // 一级
  }
  :deep(.ant-menu-sub .ant-menu-sub .ant-menu-item),
  :deep(.ant-menu-sub .ant-menu-sub .ant-menu-submenu-title) {
    padding-inline-start: 24px !important; // 二级
  }
  :deep(.ant-menu-sub .ant-menu-sub .ant-menu-sub .ant-menu-item),
  :deep(.ant-menu-sub .ant-menu-sub .ant-menu-sub .ant-menu-submenu-title) {
    padding-inline-start: 32px !important; // 三级
  }
  :deep(.ant-menu-sub .ant-menu-sub .ant-menu-sub .ant-menu-sub .ant-menu-item),
  :deep(.ant-menu-sub .ant-menu-sub .ant-menu-sub .ant-menu-sub .ant-menu-submenu-title) {
    padding-inline-start: 40px !important; // 四级
  }

  &.menu-style-rounded {
    :deep(.ant-menu-item) {
      border-radius: 4px;
      margin: 4px 4px;
    }
    :deep(.ant-menu-submenu-title) {
      border-radius: 4px;
      margin: 4px 4px;
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
