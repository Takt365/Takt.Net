<template>
  <div class="logistics-material-purchase-order">
    <a-card>
      <template #title>????</template>
      <a-table :columns="columns" :data-source="dataSource" />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const columns = [
  {
    title: '????',
    dataIndex: 'orderCode',
    key: 'orderCode'
  },
  {
    title: '???',
    dataIndex: 'supplierName',
    key: 'supplierName'
  },
  {
    title: '????',
    dataIndex: 'orderDate',
    key: 'orderDate'
  }
]

const dataSource = ref([])
</script>

<style scoped lang="less">
.logistics-material-purchase-order {
  // ??????
}
</style>
