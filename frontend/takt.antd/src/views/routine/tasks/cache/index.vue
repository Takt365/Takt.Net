<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/tasks/cache -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2026-01-28 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：缓存管理页面，展示缓存配置、统计信息及键值操作 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2026 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-cache">
    <a-card>
      <template #title>
        <a-space>
          <CloudServerOutlined />
          <span>{{ t('routine.tasks.cache.page.title') }}</span>
        </a-space>
      </template>

      <!-- 缓存配置信息 -->
      <a-descriptions
        :title="t('routine.tasks.cache.descriptions.configTitle')"
        :column="1"
        bordered
        size="small"
        style="margin-bottom: 24px"
      >
        <a-descriptions-item :label="t('routine.tasks.cache.labels.provider')">
          {{ cacheInfo?.provider ?? '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('routine.tasks.cache.labels.defaultExpirationMinutes')">
          {{ cacheInfo?.defaultExpirationMinutes ?? '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('routine.tasks.cache.labels.slidingExpiration')">
          {{ cacheInfo?.enableSlidingExpiration ? t('common.page.button.yes') : t('common.page.button.no') }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('routine.tasks.cache.labels.multiLevelCache')">
          {{ cacheInfo?.enableMultiLevelCache ? t('common.page.button.yes') : t('common.page.button.no') }}
        </a-descriptions-item>
        <a-descriptions-item
          v-if="cacheInfo?.provider === 'Redis'"
          :label="t('routine.tasks.cache.labels.redisInstancePrefix')"
        >
          {{ cacheInfo?.redisInstanceName || '-' }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 缓存统计（仅 Memory 支持） -->
      <a-descriptions
        :title="t('routine.tasks.cache.descriptions.statsTitle')"
        :column="1"
        bordered
        size="small"
        style="margin-bottom: 24px"
      >
        <template v-if="!statistics?.supported || statistics?.message">
          <a-descriptions-item :label="t('routine.tasks.cache.labels.note')">
            {{ statistics?.message ?? t('routine.tasks.cache.labels.loadingHint') }}
          </a-descriptions-item>
        </template>
        <template v-else>
          <a-descriptions-item :label="t('routine.tasks.cache.labels.currentEntryCount')">
            {{ statistics?.currentEntryCount ?? '-' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('routine.tasks.cache.labels.totalHits')">
            {{ statistics?.totalHits ?? '-' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('routine.tasks.cache.labels.totalMisses')">
            {{ statistics?.totalMisses ?? '-' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('routine.tasks.cache.labels.hitRate')">
            {{ statistics?.hitRate != null ? (Math.round(statistics.hitRate * 10000) / 100 + '%') : '-' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('routine.tasks.cache.labels.estimatedSizeBytes')">
            {{ statistics?.currentEstimatedSizeBytes != null ? statistics.currentEstimatedSizeBytes : '-' }}
          </a-descriptions-item>
        </template>
      </a-descriptions>

      <a-divider />

      <!-- 按键操作 -->
      <h4>{{ t('routine.tasks.cache.labels.keyOps') }}</h4>
      <a-form
        layout="inline"
        :model="keyForm"
        style="margin-bottom: 16px"
        @finish="handleCheckExists"
      >
        <a-form-item
          :label="t('routine.tasks.cache.labels.cacheKey')"
          name="key"
          :rules="keyFormRules"
        >
          <a-input
            v-model:value="keyForm.key"
            :placeholder="t('routine.tasks.cache.placeholders.cacheKey')"
            style="width: 280px"
            allow-clear
          />
        </a-form-item>
        <a-form-item>
          <a-button
            type="primary"
            html-type="submit"
            :loading="existsLoading"
          >
            <template #icon>
              <SearchOutlined />
            </template>
            {{ t('routine.tasks.cache.buttons.checkExists') }}
          </a-button>
          <a-button
            danger
            style="margin-left: 8px"
            :loading="removeLoading"
            :disabled="!keyForm.key?.trim()"
            @click="handleRemove"
          >
            <template #icon>
              <DeleteOutlined />
            </template>
            {{ t('routine.tasks.cache.buttons.remove') }}
          </a-button>
        </a-form-item>
      </a-form>

      <a-alert
        v-if="existsResult !== null"
        :type="existsResult ? 'success' : 'warning'"
        :message="existsResult ? t('routine.tasks.cache.alerts.keyExists') : t('routine.tasks.cache.alerts.keyNotExists')"
        show-icon
        style="margin-top: 8px"
      />
    </a-card>
  </div>
</template>

<script setup lang="ts">
/**
 * 缓存管理页面
 * 展示缓存配置、统计信息及键值操作
 */
import { ref, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { CloudServerOutlined, SearchOutlined, DeleteOutlined } from '@ant-design/icons-vue'
import { getCacheInfo, getCacheStatistics, existsCacheKey, removeCacheKey, type TaktCacheInfoDto, type TaktCacheStatisticsDto } from '@/api/routine/tasks/cache/cache'
import type { Rule } from 'ant-design-vue/es/form'

const { t } = useI18n()

/** 缓存配置信息 */
const cacheInfo = ref<TaktCacheInfoDto | null>(null)
/** 缓存统计信息 */
const statistics = ref<TaktCacheStatisticsDto | null>(null)
/** 信息加载状态 */
const infoLoading = ref(false)
/** 键值表单 */
const keyForm = ref({ key: '' })
/** 检查存在加载状态 */
const existsLoading = ref(false)
/** 移除加载状态 */
const removeLoading = ref(false)
/** 存在检查结果 */
const existsResult = ref<boolean | null>(null)

/** 键值表单校验规则 */
const keyFormRules = computed<Rule[]>(() => [
  { required: true, message: t('routine.tasks.cache.rules.cacheKeyRequired') }
])

/** 加载缓存信息 */
async function loadCacheInfo() {
  infoLoading.value = true
  existsResult.value = null
  try {
    const [infoRes, statsRes] = await Promise.all([getCacheInfo(), getCacheStatistics()])
    const infoData = (infoRes as { data?: { success?: boolean; data?: TaktCacheInfoDto } })?.data
    if (infoData?.success !== false && infoData?.data) {
      cacheInfo.value = infoData.data
    } else {
      cacheInfo.value = null
    }
    const statsData = (statsRes as { data?: { success?: boolean; data?: TaktCacheStatisticsDto } })?.data
    if (statsData?.success !== false && statsData?.data) {
      statistics.value = statsData.data
    } else {
      statistics.value = null
    }
  } catch {
    cacheInfo.value = null
    statistics.value = null
    message.error(t('routine.tasks.cache.messages.loadInfoFail'))
  } finally {
    infoLoading.value = false
  }
}

/** 检查缓存键是否存在 */
async function handleCheckExists() {
  const key = keyForm.value.key?.trim()
  if (!key) return
  existsLoading.value = true
  existsResult.value = null
  try {
    const res = await existsCacheKey(key)
    const data = (res as { data?: { success?: boolean; data?: { exists: boolean }; message?: string } })?.data
    if (data?.success !== false && data?.data) {
      existsResult.value = data.data.exists
    } else {
      message.error(data?.message || t('routine.tasks.cache.messages.checkFail'))
    }
  } catch {
    message.error(t('routine.tasks.cache.messages.checkFail'))
  } finally {
    existsLoading.value = false
  }
}

/** 移除缓存键 */
async function handleRemove() {
  const key = keyForm.value.key?.trim()
  if (!key) return
  removeLoading.value = true
  existsResult.value = null
  try {
    await removeCacheKey(key)
    message.success(t('routine.tasks.cache.messages.removeSuccess'))
    existsResult.value = false
  } catch {
    message.error(t('routine.tasks.cache.messages.removeFail'))
  } finally {
    removeLoading.value = false
  }
}

/** 初始化加载 */
onMounted(() => {
  loadCacheInfo()
})
</script>

<style scoped lang="less">
.routine-cache {
  padding: 0;
}
</style>
