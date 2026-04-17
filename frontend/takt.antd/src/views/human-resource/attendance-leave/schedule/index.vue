<!-- 排班管理：排班计划 + 班次定义（双页签） -->
<template>
  <div class="schedule-page">
    <a-tabs
      v-model:active-key="activeTab"
      destroy-inactive-tab-pane
    >
      <a-tab-pane
        key="plan"
        tab="排班计划"
      >
        <ShiftScheduleTab />
      </a-tab-pane>
      <a-tab-pane
        key="shift"
        tab="班次定义"
      >
        <WorkShiftTab />
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import ShiftScheduleTab from './components/shift-schedule-tab.vue'
import WorkShiftTab from './components/work-shift-tab.vue'

const activeTab = ref('plan')
</script>

<style scoped lang="less">
.schedule-page {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
