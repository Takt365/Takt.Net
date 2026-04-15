<template>
  <div class="navigation-settings">
    <a-space direction="vertical" size="large" style="width: 100%">
      <a-form-item :label="$t('components.navigation.systemSetting.menuAccordion')">
        <a-switch v-model:checked="setting.menuAccordion" @change="handleChange" />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.menuStyle')">
        <a-radio-group v-model:value="setting.menuStyle" @change="handleChange">
          <a-radio-button value="plain">{{ $t('components.navigation.systemSetting.plain') }}</a-radio-button>
          <a-radio-button value="rounded">{{ $t('components.navigation.systemSetting.rounded') }}</a-radio-button>
        </a-radio-group>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.showBreadcrumb')">
        <a-switch v-model:checked="setting.showBreadcrumb" @change="handleChange" />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.showBreadcrumbIcon')">
        <a-switch v-model:checked="setting.breadcrumbIcon" @change="handleChange" />
      </a-form-item>
    </a-space>
  </div>
</template>

<script setup lang="ts">
import { inject } from 'vue'
import type { AppSetting } from '@/stores/setting'

const setting = inject<AppSetting>('setting')!

const emit = defineEmits<{
  'change': []
}>()

const handleChange = () => {
  emit('change')
}
</script>

<style scoped lang="less">
.setting-label-tip {
  margin-left: 4px;
  color: var(--ant-color-text-tertiary);
  cursor: help;
  font-size: 14px;
}
</style>
