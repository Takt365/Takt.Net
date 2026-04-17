<template>
  <div class="other-settings">
    <a-space
      direction="vertical"
      size="large"
      style="width: 100%"
    >
      <a-form-item :label="$t('components.navigation.systemSetting.showFooter')">
        <a-switch
          v-model:checked="setting.showFooter"
          @change="handleChange"
        />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.copyright')">
        <a-input
          v-model:value="setting.copyright"
          :placeholder="$t('common.form.placeholder.copyright')"
          @change="handleChange"
        />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.watermark')">
        <a-switch
          v-model:checked="setting.watermark"
          @change="handleChange"
        />
      </a-form-item>

      <a-form-item
        v-if="setting.watermark"
        :label="$t('components.navigation.systemSetting.watermarkContent')"
      >
        <a-input
          v-model:value="setting.watermarkContent"
          :placeholder="$t('common.form.placeholder.watermark')"
          @change="handleChange"
        />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.demoSwitch')">
        <a-switch
          v-model:checked="setting.demo"
          @change="handleChange"
        />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.showForgotPassword')">
        <a-switch
          v-model:checked="setting.showForgotPassword"
          @change="handleChange"
        />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.showRegister')">
        <a-switch
          v-model:checked="setting.showRegister"
          @change="handleChange"
        />
      </a-form-item>
    </a-space>
  </div>
</template>

<script setup lang="ts">
import { inject } from 'vue'
import type { AppSetting } from '@/types/global-setting'

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
