<template>
  <div class="accounting-title">
    <a-card>
      <template #title>会计科目</template>
      <a-table :columns="columns" :data-source="dataSource" />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const columns = [
  {
    title: '科目编码',
    dataIndex: 'titleCode',
    key: 'titleCode'
  },
  {
    title: '科目名称',
    dataIndex: 'titleName',
    key: 'titleName'
  }
]

const dataSource = ref([])
</script>

<style scoped lang="less">
.accounting-title {
  // 会计科目样式
}
</style>
