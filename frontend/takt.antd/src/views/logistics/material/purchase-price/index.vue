<template>
  <div class="logistics-material-purchase-price">
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
    title: '?????',
    dataIndex: 'supplierCode',
    key: 'supplierCode'
  },
  {
    title: '????',
    dataIndex: 'priceType',
    key: 'priceType'
  }
]

const dataSource = ref([])
</script>

<style scoped lang="less">
.logistics-material-purchase-price {
  // ??????
}
</style>
