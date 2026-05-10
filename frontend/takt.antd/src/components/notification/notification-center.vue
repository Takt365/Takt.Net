<!-- ========================================
项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间:@/components/notification/notification-center
文件名称:notification-center.vue
创建时间:2025-01-20
创建人:Takt365(Cursor AI)
功能描述:通知中心组件,管理和显示系统通知

版权信息:Copyright (c) 2025 Takt  All rights reserved.
免责声明:此软件使用 MIT License,作者不承担任何使用风险。
======================================== -->
<template>
  <div class="notification-center">
    <!-- 通知图标 -->
    <a-badge :count="unreadCount" :overflow-count="99">
      <a-button type="text" @click="toggleVisible">
        <template #icon>
          <BellOutlined />
        </template>
      </a-button>
    </a-badge>

    <!-- 通知面板 -->
    <a-drawer
      v-model:open="visible"
      title="通知中心"
      placement="right"
      :width="400"
    >
      <a-list :data-source="notifications" item-layout="horizontal">
        <template #renderItem="{ item }">
          <a-list-item>
            <a-list-item-meta
              :title="item.title"
              :description="item.content"
            >
              <template #avatar>
                <a-avatar :style="{ backgroundColor: getNotificationColor(item.type) }">
                  <template #icon>
                    <component :is="getNotificationIcon(item.type)" />
                  </template>
                </a-avatar>
              </template>
            </a-list-item-meta>
            <template #actions>
              <a-tag :color="getNotificationColor(item.type)">
                {{ item.type }}
              </a-tag>
            </template>
          </a-list-item>
        </template>
      </a-list>

      <template #footer>
        <a-space>
          <a-button @click="clearAll">清空全部</a-button>
          <a-button type="primary" @click="markAllRead">全部已读</a-button>
        </a-space>
      </template>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { BellOutlined, InfoCircleOutlined, CheckCircleOutlined, WarningOutlined, CloseCircleOutlined } from '@ant-design/icons-vue'
import { notification } from 'ant-design-vue'
import { NotificationEvents, useEventBus } from '@/utils/eventBus'
import type { NotificationPayload } from '@/utils/eventBus'

interface NotificationItem extends NotificationPayload {
  id: string
  timestamp: number
  read: boolean
}

const visible = ref(false)
const notifications = ref<NotificationItem[]>([])
const unreadCount = ref(0)

// ========== 使用组合式函数（自动清理） ==========
const { on, emit } = useEventBus()

/**
 * 收到通知
 */
const handleNotificationReceived = (payload: NotificationPayload) => {
  const item: NotificationItem = {
    id: `notif_${Date.now()}`,
    title: payload.title || '通知',
    content: payload.content,
    type: payload.type || 'info',
    data: payload.data,
    timestamp: payload.timestamp || Date.now(),
    read: false
  }

  notifications.value.unshift(item)
  unreadCount.value++

  // 发布数量更新事件
  emit(NotificationEvents.CountUpdate, unreadCount.value)

  // 显示桌面通知
  notification[payload.type || 'info']({
    message: item.title,
    description: item.content,
    duration: 5
  })
}

/**
 * 收到广播
 */
const handleBroadcastReceived = (payload: NotificationPayload) => {
  handleNotificationReceived({
    ...payload,
    type: 'warning'
  })
}

/**
 * 清空通知
 */
const handleClear = () => {
  notifications.value = []
  unreadCount.value = 0
  emit(NotificationEvents.CountUpdate, 0)
}

/**
 * 切换面板显示
 */
const toggleVisible = () => {
  visible.value = !visible.value
}

/**
 * 清空全部
 */
const clearAll = () => {
  notifications.value = []
  unreadCount.value = 0
  emit(NotificationEvents.CountUpdate, 0)
  emit(NotificationEvents.Clear)
}

/**
 * 全部已读
 */
const markAllRead = () => {
  notifications.value.forEach(item => {
    item.read = true
  })
  unreadCount.value = 0
  emit(NotificationEvents.CountUpdate, 0)
}

/**
 * 获取通知颜色
 */
const getNotificationColor = (type?: string) => {
  const colors: Record<string, string> = {
    info: '#1890ff',
    success: '#52c41a',
    warning: '#faad14',
    error: '#ff4d4f'
  }
  return colors[type || 'info'] || '#1890ff'
}

/**
 * 获取通知图标
 */
const getNotificationIcon = (type?: string) => {
  const icons: Record<string, any> = {
    info: InfoCircleOutlined,
    success: CheckCircleOutlined,
    warning: WarningOutlined,
    error: CloseCircleOutlined
  }
  return icons[type || 'info'] || InfoCircleOutlined
}

// ========== 注册事件监听 ==========

// 使用组合式函数，自动清理
on(NotificationEvents.Received, handleNotificationReceived)
on(NotificationEvents.Broadcast, handleBroadcastReceived)
on(NotificationEvents.Clear, handleClear)
</script>

<style scoped>
.notification-center {
  display: inline-block;
}
</style>
