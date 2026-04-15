<template>
  <div class="stats-online-module">
    <a-spin :spinning="loading">
      <a-row :gutter="16">
        <a-col :span="8">
          <a-statistic
            :title="t('dashboard.data-board.online.users')"
            :value="onlineStats.users"
          />
        </a-col>
        <a-col :span="8">
          <a-statistic
            :title="t('dashboard.data-board.online.todayVisits')"
            :value="onlineStats.todayVisits"
          />
        </a-col>
        <a-col :span="8">
          <a-statistic
            :title="t('dashboard.data-board.online.sessions')"
            :value="onlineStats.sessions"
          />
        </a-col>
      </a-row>
    </a-spin>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { getOnlineList } from '@/api/routine/tasks/signalr/online'

const { t } = useI18n()

const loading = ref(false)
const onlineStats = ref({
  users: 0,
  todayVisits: 0,
  sessions: 0
})

async function fetchOnlineStats() {
  loading.value = true
  try {
    const res = await getOnlineList({
      pageIndex: 1,
      pageSize: 1,
      onlineStatus: 0
    })
    const total = res?.total ?? 0
    onlineStats.value = {
      users: total,
      todayVisits: 0,
      sessions: total
    }
  } catch {
    onlineStats.value = { users: 0, todayVisits: 0, sessions: 0 }
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchOnlineStats()
})
</script>

<style scoped lang="less">
.stats-online-module {
  min-height: 80px;
}
</style>
