<!-- ========================================
项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间:@/components/navigation/takt-header-user
文件名称:index.vue
创建时间:2025-01-20
创建人:Takt365(Cursor AI)
功能描述:用户菜单组件,显示头像和个人信息下拉菜单

版权信息:Copyright (c) 2025 Takt  All rights reserved.
免责声明:此软件使用 MIT License,作者不承担任何使用风险。
======================================== -->
<template>
  <a-dropdown
    :trigger="['click']"
    placement="bottomRight"
  >
    <a-button type="text">
      <template #icon>
        <a-avatar 
          :size="20" 
          v-bind="avatarUrl ? { src: avatarUrl } : {}"
        >
          <template v-if="!avatarUrl">
            <RiUserLine />
          </template>
        </a-avatar>
      </template>
    </a-button>
    <template #overlay>
      <a-menu>
        <a-menu-item @click="handleProfile">
          <UserOutlined />
          {{ $t('common.page.button.profile') }}
        </a-menu-item>
        <a-menu-item @click="handleSettings">
          <SettingOutlined />
          {{ $t('common.page.button.personalsettings') }}
        </a-menu-item>
        <a-menu-divider />
        <a-menu-item @click="handleLogout">
          <LogoutOutlined />
          {{ $t('common.page.button.logout') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { Modal } from 'ant-design-vue'
import {
  UserOutlined,
  SettingOutlined,
  LogoutOutlined
} from '@ant-design/icons-vue'
import { RiUserLine } from '@remixicon/vue'
import { useUserStore } from '@/stores/identity/user'
//import defaultAvatar from '@/assets/images/takt-def.svg?url'

const emit = defineEmits<{
  'profile': []
  'settings': []
  'logout': []
}>()

const router = useRouter()
const userStore = useUserStore()

// 计算头像 URL：如果有用户头像则使用，否则返回 undefined 显示默认图标
const avatarUrl = computed(() => {
  const avatar = userStore.userInfo?.avatar
  if (avatar && avatar.trim()) {
    return avatar
  }
  return undefined
})

const handleProfile = () => {
  emit('profile')
  router.push('/profile')
}

const handleSettings = () => {
  emit('settings')
  router.push('/settings')
}

const { t } = useI18n()
const handleLogout = () => {
  Modal.confirm({
    title: t('components.navigation.page.systemsetting.confirmlogout'),
    content: t('components.navigation.page.systemsetting.logoutcontent'),
    centered: true,
    okText: t('common.page.button.ok'),
    cancelText: t('common.page.button.cancel'),
    onOk: async () => {
      emit('logout')
      await userStore.logout()
      router.push('/login')
    }
  })
}
</script>

<style scoped lang="less">

</style>
