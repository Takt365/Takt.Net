<template>
  <div class="todo-module">
    <a-empty :description="t('dashboard.workspace.todoPlaceholder')" :image="Empty.PRESENTED_IMAGE_SIMPLE" />
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { Empty } from 'ant-design-vue'

const { t } = useI18n()
</script>

<style scoped lang="less">
.todo-module {
  padding: 16px 0;
  min-height: 128px;
}
</style>
