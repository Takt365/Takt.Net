<template>
  <div class="layout-settings">
    <a-space direction="vertical" size="large" style="width: 100%">
      <a-form-item :label="$t('components.navigation.systemSetting.layoutMode')">
        <a-radio-group v-model:value="setting.layout" @change="handleChange">
          <a-radio-button value="side">{{ $t('components.navigation.systemSetting.sideNav') }}</a-radio-button>
          <a-radio-button value="top">{{ $t('components.navigation.systemSetting.topNav') }}</a-radio-button>
          <a-radio-button value="mix">{{ $t('components.navigation.systemSetting.mixNav') }}</a-radio-button>
          <a-radio-button value="content">{{ $t('components.navigation.systemSetting.contentNav') }}</a-radio-button>
        </a-radio-group>
      </a-form-item>

      <a-form-item>
        <template #label>
          <span>{{ $t('components.navigation.systemSetting.titleLabel') }}</span>
          <a-tooltip :title="$t('components.navigation.systemSetting.titleHint')" placement="top">
            <span class="label-hint-icon"><RiQuestionLine /></span>
          </a-tooltip>
        </template>
        <a-input
          v-model:value="setting.logoText"
          placeholder="Takt Digital Factory (TDF) "
          @input="handleChange"
          @blur="handleChange"
        />
      </a-form-item>

      <a-form-item>
        <template #label>
          <span>{{ $t('components.navigation.systemSetting.collapseTitle') }}</span>
          <a-tooltip :title="$t('components.navigation.systemSetting.collapseTitleHint')" placement="top">
            <span class="label-hint-icon"><RiQuestionLine /></span>
          </a-tooltip>
        </template>
        <a-input
          v-model:value="setting.logoCollapsedText"
          placeholder="T"
          @input="handleChange"
          @blur="handleChange"
        />
      </a-form-item>

      <a-form-item>
        <template #label>
          <span>{{ $t('components.navigation.systemSetting.showLogo') }}</span>
          <a-tooltip :title="$t('components.navigation.systemSetting.showLogoHint')" placement="top">
            <span class="label-hint-icon"><RiQuestionLine /></span>
          </a-tooltip>
        </template>
        <a-switch v-model:checked="setting.showLogo" @change="handleChange" />
      </a-form-item>

      <a-form-item v-if="setting.showLogo">
        <template #label>
          <span>{{ $t('components.navigation.systemSetting.logoPath') }}</span>
          <a-tooltip :title="$t('components.navigation.systemSetting.logoPathHint')" placement="top">
            <span class="label-hint-icon"><RiQuestionLine /></span>
          </a-tooltip>
        </template>
        <a-input
          v-model:value="logoPath"
          placeholder="assets/images/takt.svg"
          addon-before="@/"
          allow-clear
        />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.fixedHeader')">
        <a-switch v-model:checked="setting.fixedHeader" @change="handleChange" />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.fixedSidebar')">
        <a-switch v-model:checked="setting.fixedSider" @change="handleChange" />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.sidebarWidth')">
        <a-slider
          v-model:value="setting.siderWidth"
          :min="160"
          :max="300"
          :step="10"
          @change="handleChange"
        />
        <span style="margin-left: 8px">{{ setting.siderWidth }}px</span>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.sidebarCollapsedWidth')">
        <a-slider
          v-model:value="setting.siderCollapsedWidth"
          :min="48"
          :max="80"
          :step="8"
          @change="handleChange"
        />
        <span style="margin-left: 8px">{{ setting.siderCollapsedWidth }}px</span>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.contentWidth')">
        <a-radio-group v-model:value="setting.contentWidth" @change="handleChange">
          <a-radio-button value="fluid">{{ $t('components.navigation.systemSetting.fluid') }}</a-radio-button>
          <a-radio-button value="fixed">{{ $t('components.navigation.systemSetting.fixed') }}</a-radio-button>
        </a-radio-group>
      </a-form-item>
    </a-space>
  </div>
</template>

<script setup lang="ts">
import { computed, inject } from 'vue'
import { RiQuestionLine } from '@remixicon/vue'
import type { AppSetting } from '@/stores/setting'

const setting = inject<AppSetting>('setting')!

const emit = defineEmits<{
  'change': []
}>()

const logoPath = computed({
  get: () => {
    const path = setting.logo || ''
    if (path.startsWith('@/')) return path.substring(2)
    if (path.startsWith('/src/')) return path.substring(5)
    return path
  },
  set: (value: string) => {
    const trimmedValue = value?.trim() || ''
    if (!trimmedValue) {
      setting.logo = ''
    } else {
      setting.logo = (trimmedValue.startsWith('@/') || trimmedValue.startsWith('/')) ? trimmedValue : `@/${trimmedValue}`
    }
    handleChange()
  }
})

const handleChange = () => {
  emit('change')
}
</script>

<style scoped lang="less">
.label-hint-icon {
  margin-left: 4px;
  display: inline-flex;
  align-items: center;
  color: var(--ant-color-text-tertiary);
  cursor: help;
  vertical-align: middle;
}
</style>
