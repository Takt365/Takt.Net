<template>
  <div class="stats-custom-module">
    <a-typography-paragraph class="stats-placeholder">
      {{ t('dashboard.data-board.customPlaceholder') }}
    </a-typography-paragraph>
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
</script>

<style scoped lang="less">
.stats-custom-module {
  min-height: 80px;
}
.stats-placeholder {
  color: var(--ant-color-text-tertiary);
  margin-bottom: 0;
}
</style>
