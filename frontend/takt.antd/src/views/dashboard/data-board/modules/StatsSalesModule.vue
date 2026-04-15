<template>
  <div class="stats-sales-module">
    <a-row :gutter="16">
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.sales.orders')"
          :value="salesStats.orders"
        />
      </a-col>
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.sales.amount')"
          :value="salesStats.amount"
          prefix="¥"
          :precision="2"
        />
      </a-col>
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.sales.yoy')"
          :value="salesStats.yoy"
          suffix="%"
          :precision="1"
        />
      </a-col>
    </a-row>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

const salesStats = ref({
  orders: 1256,
  amount: 1285600,
  yoy: 12.8
})
</script>

<style scoped lang="less">
.stats-sales-module {
  min-height: 80px;
}
</style>
