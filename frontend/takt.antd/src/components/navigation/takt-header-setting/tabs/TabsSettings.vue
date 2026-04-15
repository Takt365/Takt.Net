<template>
  <div class="tabs-settings">
    <a-space direction="vertical" size="large" style="width: 100%">
      <a-form-item :label="$t('components.navigation.systemSetting.showTabs')">
        <a-switch v-model:checked="setting.showTabs" @change="handleChange" />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.tabStyle')">
        <a-radio-group v-model:value="setting.tabStyle" @change="handleChange">
          <a-radio-button value="card">{{ $t('components.navigation.systemSetting.card') }}</a-radio-button>
          <a-radio-button value="google">{{ $t('components.navigation.systemSetting.google') }}</a-radio-button>
        </a-radio-group>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.persistTabs')">
        <a-switch v-model:checked="setting.persistTabs" @change="handleChange" />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.maxTabs')">
        <a-input-number
          v-model:value="setting.maxTabs"
          :min="5"
          :max="50"
          @change="handleChange"
        />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.enableMultiTab')">
        <a-switch v-model:checked="setting.multiTab" @change="handleChange" />
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
