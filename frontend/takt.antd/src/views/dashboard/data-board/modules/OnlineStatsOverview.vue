<!--
  项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
  命名空间：@/views/dashboard/data-board/modules
  文件名称：OnlineStatsOverview.vue
  功能描述：数据看板 · 在线用户数字卡片，实时在线人数（来自 SignalR Store）
  版权信息：Copyright (c) 2025 Takt. All rights reserved.
-->
<template>
  <div class="online-stats-overview">
    <div class="online-stats-header">
      <a-button type="text" size="small" :loading="loading" @click="refresh">
        <template #icon><RiRefreshLine /></template>
        {{ t('dashboard.dataBoard.refreshOnlineUsers') }}
      </a-button>
    </div>
    <div class="online-stats-body">
      <a-spin v-if="loading" size="large" />
      <a-statistic v-else :value="count" :value-style="valueStyle" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { RiRefreshLine } from '@remixicon/vue'
import { storeToRefs } from 'pinia'
import { useSignalRStore } from '@/stores/identity/signalr'

const { t } = useI18n()
const signalRStore = useSignalRStore()
const { onlineUsers } = storeToRefs(signalRStore)

/** 按用户去重后的在线人数（后端已修复同一用户一条记录，此处防护历史数据或多端同账号） */
const count = computed(() => {
  const list = onlineUsers.value ?? []
  const seen = new Set<string>()
  for (const u of list) {
    const key = u.userId != null ? String(u.userId) : (u.userName || '')
    seen.add(key)
  }
  return seen.size
})
const loading = ref(false)

const valueStyle = { color: 'var(--ant-color-primary)', fontSize: '36px', fontWeight: 700 }

async function refresh() {
  loading.value = true
  try {
    await signalRStore.refreshOnlineUsers()
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  if (signalRStore.isConnected) refresh()
})
</script>

<style scoped lang="less">
.online-stats-overview {
  padding: 12px 0;
  min-height: 128px;
}
.online-stats-header {
  display: flex;
  justify-content: flex-end;
  margin-bottom: 8px;
}
.online-stats-body {
  min-height: 100px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.online-stats-body :deep(.ant-statistic-content) {
  font-size: 36px;
}
</style>
