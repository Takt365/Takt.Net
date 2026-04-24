<template>
  <div class="stats-production-module">
    <a-row :gutter="16">
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.production.output')"
          :value="productionStats.output"
        />
      </a-col>
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.production.yieldrate')"
          :value="productionStats.yieldRate"
          suffix="%"
          :precision="1"
        />
      </a-col>
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.production.wip')"
          :value="productionStats.wip"
        />
      </a-col>
    </a-row>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

const productionStats = ref({
  output: 8520,
  yieldRate: 98.6,
  wip: 326
})
</script>

<style scoped lang="less">
.stats-production-module {
  min-height: 80px;
}
</style>
