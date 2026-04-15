<template>
  <a-dropdown :trigger="['click']" placement="bottomRight">
    <a-button type="text">
      <template #icon>
        <a-avatar 
          :size="20" 
          :src="avatarUrl || undefined"
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
          {{ $t('common.button.profile') }}
        </a-menu-item>
        <a-menu-item @click="handleSettings">
          <SettingOutlined />
          {{ $t('common.button.personalSettings') }}
        </a-menu-item>
        <a-menu-divider />
        <a-menu-item @click="handleLogout">
          <LogoutOutlined />
          {{ $t('common.button.logout') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { computed } from 'vue'
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

// 计算头像 URL：如果有用户头像则使用，否则返回 null 显示默认图标
const avatarUrl = computed(() => {
  const avatar = userStore.userInfo?.avatar
  if (avatar && avatar.trim()) {
    return avatar
  }
  return null
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
    title: t('components.navigation.systemSetting.confirmLogout'),
    content: t('components.navigation.systemSetting.logoutContent'),
    centered: true,
    okText: t('common.button.ok'),
    cancelText: t('common.button.cancel'),
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
