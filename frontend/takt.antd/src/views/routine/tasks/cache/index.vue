<template>
  <div class="routine-cache">
    <a-card>
      <template #title>
        <a-space>
          <CloudServerOutlined />
          <span>缓存管理</span>
        </a-space>
      </template>

      <!-- 缓存配置信息 -->
      <a-descriptions title="缓存配置" :column="1" bordered size="small" style="margin-bottom: 24px">
        <a-descriptions-item label="提供者">
          {{ cacheInfo?.provider ?? '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="默认过期时间（分钟）">
          {{ cacheInfo?.defaultExpirationMinutes ?? '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="滑动过期">
          {{ cacheInfo?.enableSlidingExpiration ? '是' : '否' }}
        </a-descriptions-item>
        <a-descriptions-item label="多级缓存">
          {{ cacheInfo?.enableMultiLevelCache ? '是' : '否' }}
        </a-descriptions-item>
        <a-descriptions-item v-if="cacheInfo?.provider === 'Redis'" label="Redis 实例前缀">
          {{ cacheInfo?.redisInstanceName || '-' }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 缓存统计（仅 Memory 支持） -->
      <a-descriptions title="缓存统计" :column="1" bordered size="small" style="margin-bottom: 24px">
        <template v-if="!statistics?.supported || statistics?.message">
          <a-descriptions-item label="说明">
            {{ statistics?.message ?? '加载中…' }}
          </a-descriptions-item>
        </template>
        <template v-else>
          <a-descriptions-item label="当前缓存项数量">
            {{ statistics?.currentEntryCount ?? '-' }}
          </a-descriptions-item>
          <a-descriptions-item label="命中次数">
            {{ statistics?.totalHits ?? '-' }}
          </a-descriptions-item>
          <a-descriptions-item label="未命中次数">
            {{ statistics?.totalMisses ?? '-' }}
          </a-descriptions-item>
          <a-descriptions-item label="命中率">
            {{ statistics?.hitRate != null ? (Math.round(statistics.hitRate * 10000) / 100 + '%') : '-' }}
          </a-descriptions-item>
          <a-descriptions-item label="当前估算大小（字节）">
            {{ statistics?.currentEstimatedSizeBytes != null ? statistics.currentEstimatedSizeBytes : '-' }}
          </a-descriptions-item>
        </template>
      </a-descriptions>

      <a-divider />

      <!-- 按键操作 -->
      <h4>按键操作</h4>
      <a-form layout="inline" :model="keyForm" @finish="handleCheckExists" style="margin-bottom: 16px">
        <a-form-item label="缓存键" name="key" :rules="[{ required: true, message: '请输入缓存键' }]">
          <a-input
            v-model:value="keyForm.key"
            placeholder="请输入缓存键"
            style="width: 280px"
            allow-clear
          />
        </a-form-item>
        <a-form-item>
          <a-button type="primary" html-type="submit" :loading="existsLoading">
            <template #icon><SearchOutlined /></template>
            检查是否存在
          </a-button>
          <a-button
            danger
            style="margin-left: 8px"
            :loading="removeLoading"
            :disabled="!keyForm.key?.trim()"
            @click="handleRemove"
          >
            <template #icon><DeleteOutlined /></template>
            移除
          </a-button>
        </a-form-item>
      </a-form>

      <a-alert
        v-if="existsResult !== null"
        :type="existsResult ? 'success' : 'warning'"
        :message="existsResult ? '该键存在' : '该键不存在'"
        show-icon
        style="margin-top: 8px"
      />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { CloudServerOutlined, SearchOutlined, DeleteOutlined } from '@ant-design/icons-vue'
import { getCacheInfo, getCacheStatistics, existsCacheKey, removeCacheKey, type CacheInfoDto, type CacheStatisticsDto } from '@/api/routine/tasks/cache'

const cacheInfo = ref<CacheInfoDto | null>(null)
const statistics = ref<CacheStatisticsDto | null>(null)
const infoLoading = ref(false)
const keyForm = ref({ key: '' })
const existsLoading = ref(false)
const removeLoading = ref(false)
const existsResult = ref<boolean | null>(null)

async function loadCacheInfo() {
  infoLoading.value = true
  existsResult.value = null
  try {
    const [infoRes, statsRes] = await Promise.all([getCacheInfo(), getCacheStatistics()])
    const infoData = (infoRes as any)?.data
    if (infoData?.success !== false && infoData?.data) {
      cacheInfo.value = infoData.data
    } else {
      cacheInfo.value = null
    }
    const statsData = (statsRes as any)?.data
    if (statsData?.success !== false && statsData?.data) {
      statistics.value = statsData.data
    } else {
      statistics.value = null
    }
  } catch (e) {
    cacheInfo.value = null
    statistics.value = null
    message.error('获取缓存信息失败')
  } finally {
    infoLoading.value = false
  }
}

async function handleCheckExists() {
  const key = keyForm.value.key?.trim()
  if (!key) return
  existsLoading.value = true
  existsResult.value = null
  try {
    const res = await existsCacheKey(key)
    const data = (res as any)?.data
    if (data?.success !== false && data?.data) {
      existsResult.value = data.data.exists
    } else {
      message.error(data?.message || '检查失败')
    }
  } catch (e) {
    message.error('检查失败')
  } finally {
    existsLoading.value = false
  }
}

async function handleRemove() {
  const key = keyForm.value.key?.trim()
  if (!key) return
  removeLoading.value = true
  existsResult.value = null
  try {
    await removeCacheKey(key)
    message.success('已移除')
    existsResult.value = false
  } catch (e) {
    message.error('移除失败')
  } finally {
    removeLoading.value = false
  }
}

onMounted(() => {
  loadCacheInfo()
})
</script>

<style scoped lang="less">
.routine-cache {
  padding: 0;
}
</style>
