<template>
  <div class="logistics-maintenance-equipment">
    <a-card>
      <template #title>设备管理</template>
      <a-table :columns="columns" :data-source="dataSource" />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const columns = [
  {
    title: '设备名称',
    dataIndex: 'equipmentName',
    key: 'equipmentName'
  },
  {
    title: '设备编码',
    dataIndex: 'equipmentCode',
    key: 'equipmentCode'
  }
]

const dataSource = ref([])
</script>

<style scoped lang="less">
.logistics-maintenance-equipment {
  // 设备管理样式
}
</style>
