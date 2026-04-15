<template>
  <div class="theme-settings">
    <a-space direction="vertical" class="theme-settings-space" style="width: 100%">
      <a-form-item :label="$t('components.navigation.systemSetting.themeMode')">
        <a-radio-group v-model:value="setting.theme" @change="handleChange">
          <a-radio-button value="light">{{ $t('components.navigation.systemSetting.light') }}</a-radio-button>
          <a-radio-button value="dark">{{ $t('components.navigation.systemSetting.dark') }}</a-radio-button>
        </a-radio-group>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.themeColor')">
        <div class="theme-color-picker">
          <a-tooltip
            v-for="(color, key) in themeColorMap"
            :key="key"
            :title="$t(`common.settings.color.${key}`)"
            placement="top"
          >
            <div
              class="color-item"
              :class="{ active: setting.themeColor.type === key }"
              :style="{ backgroundColor: color }"
              @click="handleColorSelect(key as ThemeColor)"
            >
              <RiCheckLine v-if="setting.themeColor.type === key" class="color-item-check" />
            </div>
          </a-tooltip>
          <a-tooltip :title="$t('components.navigation.systemSetting.custom')" placement="top">
            <div
              class="color-item custom"
              :class="{ active: setting.themeColor.type === 'custom' }"
              @click="handleColorSelect('custom')"
            >
              <RiCheckLine v-if="setting.themeColor.type === 'custom'" class="color-item-check custom-check" />
            </div>
          </a-tooltip>
        </div>
        <div v-if="setting.themeColor.type === 'custom'" style="margin-top: 8px; display: flex; align-items: center; gap: 8px;">
          <input
            type="color"
            :value="customColorValue"
            @input="handleCustomColorPickerInput"
            style="width: 40px; height: 32px; border: 1px solid #d9d9d9; border-radius: 4px; cursor: pointer;"
          />
          <a-input
            v-model:value="customColorValue"
            placeholder="#1890ff"
            style="flex: 1"
          />
        </div>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.borderRadius')">
        <a-radio-group v-model:value="setting.borderRadius" @change="handleChange">
          <a-radio-button :value="0">0</a-radio-button>
          <a-radio-button :value="5">5</a-radio-button>
          <a-radio-button :value="10">10</a-radio-button>
          <a-radio-button :value="15">15</a-radio-button>
          <a-radio-button :value="20">20</a-radio-button>
        </a-radio-group>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.fontSize')">
        <a-slider
          v-model:value="setting.fontSize"
          :min="15"
          :max="22"
          :step="1"
          @change="handleFontSizeChange"
        />
        <span style="margin-left: 8px">{{ setting.fontSize }}px</span>
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.colorWeak')">
        <a-switch v-model:checked="setting.colorWeak" @change="handleChange" />
      </a-form-item>

      <a-form-item :label="$t('components.navigation.systemSetting.grayMode')">
        <a-switch v-model:checked="setting.grayscale" @change="handleChange" />
      </a-form-item>
    </a-space>
  </div>
</template>

<script setup lang="ts">
import { computed, inject } from 'vue'
import { RiCheckLine } from '@remixicon/vue'
import type { AppSetting, ThemeColor } from '@/stores/setting'
import { themeColorMap, validateFontSize } from '@/stores/setting'

const setting = inject<AppSetting>('setting')!

const emit = defineEmits<{
  'change': []
}>()

const handleChange = () => {
  emit('change')
}

const customColorValue = computed({
  get: () => setting.themeColor.customColor || '#1890ff',
  set: (v) => {
    const val = (v && String(v).trim()) || '#1890ff'
    setting.themeColor.customColor = val
    handleChange()
  }
})

const handleColorSelect = (color: ThemeColor) => {
  setting.themeColor.type = color
  if (color !== 'custom') {
    setting.themeColor.customColor = undefined
  } else {
    setting.themeColor.customColor = customColorValue.value
  }
  handleChange()
}

const handleCustomColorPickerInput = (e: Event) => {
  const target = e.target as HTMLInputElement
  customColorValue.value = target.value
}

const handleFontSizeChange = () => {
  setting.fontSize = validateFontSize(setting.fontSize)
  handleChange()
}
</script>

<style scoped lang="less">
.theme-settings-space :deep(.ant-space-item) {
  margin-bottom: 4px;
}
.theme-settings-space :deep(.ant-space-item:last-child) {
  margin-bottom: 0;
}

.setting-label-tip {
  margin-left: 4px;
  color: var(--ant-color-text-tertiary);
  cursor: help;
  font-size: 14px;
}

.theme-color-picker {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;

  :deep(.ant-tooltip-trigger) {
    display: block;
  }

  .color-item {
    width: 32px;
    height: 32px;
    border-radius: 4px;
    cursor: pointer;
    border: 2px solid transparent;
    transition: all 0.2s;
    display: flex;
    align-items: center;
    justify-content: center;

    .color-item-check {
      color: white;
      font-size: 18px;
    }

    &.active {
      border-color: transparent;
      box-shadow: none;
    }

    &.custom {
      background: #f0f0f0;
      border: 1px dashed #d9d9d9;

      span {
        font-size: 12px;
        color: #666;
      }

      .color-item-check.custom-check {
        color: var(--ant-color-primary);
      }

      &.active {
        border-color: #d9d9d9;
        box-shadow: none;
      }
    }
  }
}
</style>
