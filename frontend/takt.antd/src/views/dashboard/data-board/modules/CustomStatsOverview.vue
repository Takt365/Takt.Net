<!--
  项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
  命名空间：@/views/dashboard/data-board/modules
  文件名称：CustomStatsOverview.vue
  功能描述：数据看板 · 自定义仪表模块
  版权信息：Copyright (c) 2025 Takt. All rights reserved.
-->
<template>
  <div class="data-board-custom-module">
    <a-typography-paragraph v-if="customContent" class="custom-content">
      {{ customContent }}
    </a-typography-paragraph>
    <a-empty
      v-else
      :description="t('dashboard.dataBoard.customPlaceholder')"
      :image="Empty.PRESENTED_IMAGE_SIMPLE"
    />
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { Empty } from 'ant-design-vue'

defineProps<{
  customTitle?: string
  customContent?: string
}>()

const { t } = useI18n()
</script>

<style scoped lang="less">
.data-board-custom-module {
  padding: 8px 0;
  min-height: 80px;
  .custom-content {
    white-space: pre-wrap;
    margin-bottom: 0;
  }
}
</style>
