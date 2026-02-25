<!--
  项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
  命名空间：@/views/dashboard/data-board/modules
  文件名称：LogisticsStatsOverview.vue
  功能描述：数据看板 · 后勤管理仪表模块占位
  版权信息：Copyright (c) 2025 Takt. All rights reserved.
-->
<template>
  <div class="data-board-stat-module">
    <a-empty
      :description="t('dashboard.dataBoard.logisticsPlaceholder')"
      :image="Empty.PRESENTED_IMAGE_SIMPLE"
    />
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { Empty } from 'ant-design-vue'

const { t } = useI18n()
</script>

<style scoped lang="less">
.data-board-stat-module {
  padding: 16px 0;
  min-height: 128px;
}
</style>
