<template>
  <a-badge 
    v-if="dot || count > 0"
    :dot="dot"
    :count="dot ? undefined : count"
    :overflow-count="overflowCount"
    :show-zero="false"
  >
    <a-button type="text" @click="handleClick" class="takt-header-notification">
      <template #icon>
        <RiNotificationLine />
      </template>
    </a-button>
  </a-badge>
  <a-button 
    v-else
    type="text" 
    @click="handleClick" 
    class="takt-header-notification"
  >
    <template #icon>
      <RiNotificationLine />
    </template>
  </a-button>
  <a-drawer
    v-model:open="visible"
    :title="$t('components.navigation.systemSetting.notification')"
    placement="right"
    :width="400"
    class="takt-header-notification-drawer"
  >
    <template #extra>
      <a-button type="text" @click="handleClearAll">{{ $t('common.button.empty') }}</a-button>
    </template>
    <a-list
      :data-source="notifications"
      :loading="loading"
      :pagination="pagination"
    >
      <template #renderItem="{ item }">
        <a-list-item>
          <a-list-item-meta>
            <template #title>
              <span :class="{ 'unread': !item.read }">{{ item.title }}</span>
            </template>
            <template #description>
              <div>{{ item.content }}</div>
              <div class="notification-time">{{ item.time }}</div>
            </template>
          </a-list-item-meta>
          <template #actions>
            <a-button type="text" size="small" @click="handleMarkRead(item.id)" v-if="!item.read">
              {{ $t('common.button.markRead') }}
            </a-button>
            <a-button type="text" size="small" danger @click="handleDelete(item.id)">
              {{ $t('common.button.delete') }}
            </a-button>
          </template>
        </a-list-item>
      </template>
    </a-list>
  </a-drawer>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { RiNotificationLine } from '@remixicon/vue'

const { t } = useI18n()

interface Notification {
  id: string
  title: string
  content: string
  time: string
  read: boolean
}

interface Props {
  notifications?: Notification[]
  dot?: boolean
  overflowCount?: number
}

const props = withDefaults(defineProps<Props>(), {
  notifications: () => [],
  dot: false,
  overflowCount: 99
})

const emit = defineEmits<{
  'click': []
  'read': [id: string]
  'delete': [id: string]
  'clear-all': []
}>()

const visible = ref(false)
const loading = ref(false)

const notifications = computed(() => props.notifications)

const count = computed(() => {
  return notifications.value.filter(n => !n.read).length
})

const pagination = computed(() => ({
  pageSize: 10,
  showSizeChanger: false,
  showTotal: (total: number) => t('components.navigation.systemSetting.totalCount', { total })
}))

const handleClick = () => {
  visible.value = true
  emit('click')
}

const handleMarkRead = (id: string) => {
  emit('read', id)
}

const handleDelete = (id: string) => {
  emit('delete', id)
}

const handleClearAll = () => {
  emit('clear-all')
}
</script>

<style scoped lang="less">
.takt-header-notification-drawer {
  :deep(.ant-drawer-content-wrapper) {
    right: 16px;
  }
}
</style>
