<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/statistics/logging/server-monitor -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2026-05-06 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：服务器监控页面，展示应用状态、硬件信息（CPU/内存/磁盘/网络）等 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2026 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="server-monitor-container">
    <!-- 页面头部 -->
    <a-card :bordered="false" class="header-card">
      <div class="header-content">
        <div class="header-left">
          <h2>{{ t('statistics.logging.serverMonitor.page.title') }}</h2>
          <p class="description">{{ t('statistics.logging.serverMonitor.page.description') }}</p>
        </div>
        <div class="header-right">
          <a-button type="primary" :loading="refreshing" @click="handleRefresh">
            <template #icon><ReloadOutlined /></template>
            {{ t('statistics.logging.serverMonitor.page.refreshCache') }}
          </a-button>
        </div>
      </div>
    </a-card>

    <!-- 应用状态 -->
    <a-card :title="t('servermonitor.object.appstatus')" :bordered="false" class="status-card">
      <a-descriptions bordered :column="2" size="small">
        <a-descriptions-item :label="t('servermonitor.appstatus.appname')">
          {{ appStatus.applicationName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.appversion')">
          <a-tag color="blue">{{ appStatus.applicationVersion }}</a-tag>
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.environment')">
          <a-tag :color="envColor">{{ appStatus.environment }}</a-tag>
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.machinename')">
          {{ appStatus.machineName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.dotnetversion')">
          {{ appStatus.dotNetVersion }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.processorcount')">
          {{ appStatus.processorCount }} 核
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.starttime')">
          {{ formatDateTime(appStatus.startTime) }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.uptime')">
          {{ formatUptime(appStatus.uptime) }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.appstatus.workingset')" :span="2">
          {{ formatBytes(appStatus.workingSet) }}
        </a-descriptions-item>
      </a-descriptions>
    </a-card>

    <!-- 操作系统信息 -->
    <a-card :title="t('servermonitor.object.oslanguageinfo')" :bordered="false" class="info-card">
      <a-descriptions bordered :column="2" size="small">
        <a-descriptions-item :label="t('servermonitor.hardware.os')" :span="2">
          {{ hardwareInfo.operatingSystem }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.os.osversion')">
          {{ hardwareInfo.operatingSystemLanguage.osVersion }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.os.currentculture')">
          {{ hardwareInfo.operatingSystemLanguage.currentCultureDisplayName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('servermonitor.os.currentculture')">
          {{ hardwareInfo.operatingSystemLanguage.currentUICultureDisplayName }}
        </a-descriptions-item>
      </a-descriptions>
    </a-card>

    <!-- CPU信息 -->
    <a-card :title="t('servermonitor.object.cpuinfo')" :bordered="false" class="info-card">
      <a-table
        :columns="cpuColumns"
        :data-source="hardwareInfo.cpuList"
        :pagination="false"
        size="small"
        row-key="processorId"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'numberOfCores'">
            {{ record.numberOfCores }} 核
          </template>
          <template v-if="column.key === 'numberOfLogicalProcessors'">
            {{ record.numberOfLogicalProcessors }} 线程
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 内存信息 -->
    <a-card :title="t('servermonitor.object.memoryinfo')" :bordered="false" class="info-card">
      <a-row :gutter="[16, 16]">
        <a-col :span="8">
          <a-statistic
            :title="t('servermonitor.memory.totalphysical')"
            :value="formatBytes(hardwareInfo.memory.totalPhysicalMemory)"
          />
        </a-col>
        <a-col :span="8">
          <a-statistic
            :title="t('servermonitor.memory.usedphysical')"
            :value="formatBytes(hardwareInfo.memory.usedPhysicalMemory)"
          />
        </a-col>
        <a-col :span="8">
          <a-statistic
            :title="t('servermonitor.memory.usagepercent')"
            :value="hardwareInfo.memory.memoryUsagePercent"
            suffix="%"
            :value-style="{ color: getMemoryUsageColor(hardwareInfo.memory.memoryUsagePercent) }"
          />
        </a-col>
      </a-row>
      <a-divider />
      <a-row :gutter="[16, 16]">
        <a-col :span="12">
          <a-progress
            :percent="hardwareInfo.memory.memoryUsagePercent"
            :stroke-color="getMemoryUsageColor(hardwareInfo.memory.memoryUsagePercent)"
          />
        </a-col>
      </a-row>
    </a-card>

    <!-- 磁盘信息 -->
    <a-card :title="t('servermonitor.object.driveinfo')" :bordered="false" class="info-card">
      <a-table
        :columns="driveColumns"
        :data-source="hardwareInfo.driveList"
        :pagination="false"
        size="small"
        row-key="name"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'totalSize'">
            {{ formatBytes(record.totalSize) }}
          </template>
          <template v-if="column.key === 'freeSpace'">
            {{ formatBytes(record.freeSpace) }}
          </template>
          <template v-if="column.key === 'usedSpace'">
            {{ formatBytes(record.usedSpace) }}
          </template>
          <template v-if="column.key === 'usagePercent'">
            <a-progress
              :percent="record.usagePercent"
              size="small"
              :stroke-color="getDriveUsageColor(record.usagePercent)"
            />
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 网络适配器信息 -->
    <a-card :title="t('servermonitor.object.networkadapterinfo')" :bordered="false" class="info-card">
      <a-table
        :columns="networkColumns"
        :data-source="hardwareInfo.networkAdapterList"
        :pagination="false"
        size="small"
        row-key="macAddress"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'speed'">
            {{ formatSpeed(record.speed) }}
          </template>
          <template v-if="column.key === 'status'">
            <a-tag :color="record.status === 'Up' ? 'green' : 'red'">
              {{ record.status }}
            </a-tag>
          </template>
        </template>
      </a-table>
    </a-card>
  </div>
</template>

<script setup lang="ts">
/**
 * 服务器监控页面
 * 展示应用运行状态和服务器硬件信息
 */
import { ref, onMounted, computed } from 'vue'
import { ReloadOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import dayjs from 'dayjs'
import duration from 'dayjs/plugin/duration'
import { getServerHardware, getAppStatus, refreshHardwareCache } from '@/api/statistics/logging/server-monitor'
import type { TaktServerHardwareDto, TaktAppStatusDto } from '@/types/statistics/logging/server-monitor'

dayjs.extend(duration)

const { t } = useI18n()

/** 服务器硬件信息 */
const hardwareInfo = ref<TaktServerHardwareDto>({
  operatingSystem: '',
  operatingSystemLanguage: {
    currentCulture: '',
    currentCultureDisplayName: '',
    currentCultureNativeName: '',
    currentUICulture: '',
    currentUICultureDisplayName: '',
    currentUICultureNativeName: '',
    systemDefaultLanguage: '',
    osVersion: '',
    installedLanguages: []
  },
  cpuList: [],
  memory: {
    totalPhysicalMemory: 0,
    availablePhysicalMemory: 0,
    usedPhysicalMemory: 0,
    totalVirtualMemory: 0,
    availableVirtualMemory: 0,
    usedVirtualMemory: 0,
    memoryUsagePercent: 0
  },
  driveList: [],
  networkAdapterList: []
})

/** 应用运行状态 */
const appStatus = ref<TaktAppStatusDto>({
  applicationName: '',
  applicationVersion: '',
  environment: '',
  machineName: '',
  startTime: '',
  uptime: '',
  dotNetVersion: '',
  workingSet: 0,
  processorCount: 0
})

/** 刷新加载状态 */
const refreshing = ref(false)

/** 环境标签颜色 */
const envColor = computed(() => {
  switch (appStatus.value.environment) {
    case 'Development': return 'green'
    case 'Staging': return 'orange'
    case 'Production': return 'red'
    default: return 'default'
  }
})

/** CPU 表格列定义 */
const cpuColumns = [
  { title: t('servermonitor.cpu.name'), dataIndex: 'name', key: 'name' },
  { title: t('servermonitor.cpu.manufacturer'), dataIndex: 'manufacturer', key: 'manufacturer' },
  { title: t('servermonitor.cpu.cores'), dataIndex: 'numberOfCores', key: 'numberOfCores' },
  { title: t('servermonitor.cpu.logicalprocessors'), dataIndex: 'numberOfLogicalProcessors', key: 'numberOfLogicalProcessors' },
  { title: t('servermonitor.cpu.processorid'), dataIndex: 'processorId', key: 'processorId' }
]

/** 磁盘表格列定义 */
const driveColumns = [
  { title: t('servermonitor.drive.name'), dataIndex: 'name', key: 'name' },
  { title: t('servermonitor.drive.type'), dataIndex: 'driveType', key: 'driveType' },
  { title: t('servermonitor.drive.filesystem'), dataIndex: 'fileSystem', key: 'fileSystem' },
  { title: t('servermonitor.drive.totalsize'), dataIndex: 'totalSize', key: 'totalSize' },
  { title: t('servermonitor.drive.freespace'), dataIndex: 'freeSpace', key: 'freeSpace' },
  { title: t('servermonitor.drive.usedspace'), dataIndex: 'usedSpace', key: 'usedSpace' },
  { title: t('servermonitor.drive.usagepercent'), dataIndex: 'usagePercent', key: 'usagePercent', width: 200 }
]

/** 网络适配器表格列定义 */
const networkColumns = [
  { title: t('servermonitor.network.name'), dataIndex: 'name', key: 'name' },
  { title: t('servermonitor.network.description'), dataIndex: 'description', key: 'description' },
  { title: t('servermonitor.network.macaddress'), dataIndex: 'macAddress', key: 'macAddress' },
  { title: t('servermonitor.network.speed'), dataIndex: 'speed', key: 'speed' },
  { title: t('servermonitor.network.status'), dataIndex: 'status', key: 'status' }
]

/**
 * 格式化字节数
 * @param bytes 字节数
 * @returns 格式化后的字符串
 */
function formatBytes(bytes: number): string {
  if (bytes === 0) return '0 B'
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB', 'TB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i]
}

/**
 * 格式化网速
 * @param bytesPerSecond 字节/秒
 * @returns 格式化后的字符串
 */
function formatSpeed(bytesPerSecond: number): string {
  return formatBytes(bytesPerSecond) + '/s'
}

/**
 * 格式化日期时间
 * @param dateTime 日期时间字符串
 * @returns 格式化后的字符串
 */
function formatDateTime(dateTime: string): string {
  return dayjs(dateTime).format('YYYY-MM-DD HH:mm:ss')
}

/**
 * 格式化运行时长
 * @param uptime TimeSpan 字符串（格式：d.hh:mm:ss.fffffff）
 * @returns 格式化后的字符串
 */
function formatUptime(uptime: string): string {
  // TimeSpan 格式：d.hh:mm:ss.fffffff
  if (!uptime) return uptime
  
  const parts = uptime.split('.')
  if (parts.length >= 2) {
    const days = parseInt(parts[0] || '0')
    const time = parts[1]?.split(':')
    if (!time || time.length < 2) return uptime
    
    const hours = parseInt(time[0] || '0')
    const minutes = parseInt(time[1] || '0')
    
    if (days > 0) {
      return `${days} 天 ${hours} 小时 ${minutes} 分钟`
    }
    return `${hours} 小时 ${minutes} 分钟`
  }
  return uptime
}

/**
 * 获取内存使用率颜色
 * @param usage 使用率百分比
 * @returns 颜色值
 */
function getMemoryUsageColor(usage: number): string {
  if (usage >= 90) return '#ff4d4f'
  if (usage >= 70) return '#faad14'
  return '#52c41a'
}

/**
 * 获取磁盘使用率颜色
 * @param usage 使用率百分比
 * @returns 颜色值
 */
function getDriveUsageColor(usage: number): string {
  if (usage >= 90) return '#ff4d4f'
  if (usage >= 70) return '#faad14'
  return '#1677ff'
}

/** 加载数据 */
async function loadData() {
  try {
    const [hardware, status] = await Promise.all([
      getServerHardware(),
      getAppStatus()
    ])
    hardwareInfo.value = hardware
    appStatus.value = status
  } catch (error: any) {
    message.error(error?.message || t('common.page.api.requestfail'))
  }
}

/** 刷新缓存 */
async function handleRefresh() {
  refreshing.value = true
  try {
    await refreshHardwareCache()
    message.success(t('statistics.logging.serverMonitor.page.refreshSuccess'))
    await loadData()
  } catch (error: any) {
    message.error(error?.message || t('common.page.api.requestfail'))
  } finally {
    refreshing.value = false
  }
}

/** 初始化加载 */
onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.server-monitor-container {
  padding: 16px;
  
  .header-card {
    margin-bottom: 16px;
    
    .header-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
      
      .header-left {
        h2 {
          margin: 0 0 8px 0;
          font-size: 20px;
          font-weight: 600;
        }
        
        .description {
          margin: 0;
          color: #666;
          font-size: 14px;
        }
      }
      
      .header-right {
        flex-shrink: 0;
      }
    }
  }
  
  .status-card,
  .info-card {
    margin-bottom: 16px;
  }
}
</style>
