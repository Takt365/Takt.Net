<!-- ========================================
 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 命名空间：@/views/logistics/maintenance/work-order
 文件名称：index.vue
 创建时间：2025-02-21
 创建人：Takt365(Cursor AI)
 功能描述：工厂维护-维护工单
 版权信息：Copyright (c) 2025 Takt  All rights reserved.
 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
 ======================================== -->
<template>
  <div class="logistics-maintenance-work-order">
    <a-card>
      <template #title>{{ $t('menu.logistics.maintenance.workorder') }}</template>
      <a-empty :description="$t('common.msg.noData')" />
    </a-card>
  </div>
</template>

<script setup lang="ts">
</script>

<style scoped lang="less">
.logistics-maintenance-work-order {
}
</style>
