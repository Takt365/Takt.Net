<template>
  <div class="custom-module">
    <a-typography-paragraph
      v-if="customContent"
      class="custom-content"
    >
      {{ customContent }}
    </a-typography-paragraph>
    <a-empty
      v-else
      :description="t('dashboard.workspace.customPlaceholder')"
      :image="Empty.PRESENTED_IMAGE_SIMPLE"
    />
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { Empty } from 'ant-design-vue'

defineProps<{
  customTitle?: string
  customContent?: string
}>()

const { t } = useI18n()
</script>

<style scoped lang="less">
.custom-module {
  padding: 8px 0;
  min-height: 80px;
  .custom-content {
    white-space: pre-wrap;
    margin-bottom: 0;
  }
}
</style>