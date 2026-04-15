<template>
  <a-menu
    v-model:selectedKeys="selectedKeys"
    mode="horizontal"
    :theme="theme"
    :items="menuItems"
    @click="handleMenuClick"
    class="takt-top-menu"
  >
  </a-menu>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import type { MenuClickEventHandler } from 'ant-design-vue/es/menu/src/interface'
import { useThemeStore } from '@/stores/theme'
import { useMenuStore } from '@/stores/identity/menu'

interface Props {
  theme?: 'light' | 'dark'
}

const props = withDefaults(defineProps<Props>(), {
  theme: 'light'
})

const emit = defineEmits<{
  'click': [key: string]
}>()

const route = useRoute()
const router = useRouter()
const themeStore = useThemeStore()
const menuStore = useMenuStore()

const selectedKeys = ref<string[]>([])

const menuItems = computed(() => menuStore.menuItems)

const theme = computed(() => {
  if (props.theme === 'dark') return 'dark'
  return themeStore.themeMode === 'dark' ? 'dark' : 'light'
})

const handleMenuClick: MenuClickEventHandler = (info) => {
  const key = String(info.key)
  router.push(key)
  emit('click', key)
}

watch(
  () => route.path,
  (path) => {
    selectedKeys.value = [path]
  },
  { immediate: true }
)
</script>

<style scoped lang="less">
.takt-top-menu {
  border-bottom: none;
  flex: 1;
}
</style>
