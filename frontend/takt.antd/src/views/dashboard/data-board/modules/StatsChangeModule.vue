<template>
  <div class="stats-change-module">
    <a-row :gutter="16">
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.change.total')"
          :value="changeData.total"
        />
      </a-col>
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.change.inprogress')"
          :value="changeData.inProgress"
        />
      </a-col>
      <a-col :span="8">
        <a-statistic
          :title="t('dashboard.data-board.change.completed')"
          :value="changeData.completed"
        />
      </a-col>
    </a-row>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

const changeData = ref({
  total: 0,
  inProgress: 0,
  completed: 0
})
</script>

<style scoped lang="less">
.stats-change-module {
  min-height: 80px;
}
</style>
