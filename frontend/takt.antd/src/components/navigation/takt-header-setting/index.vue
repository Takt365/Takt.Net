<template>
  <a-button
    type="text"
    @click="handleClick"
    class="takt-header-setting"
    :title="$t('components.navigation.systemSetting.title')"
  >
    <template #icon>
      <RiTShirtLine />
    </template>
  </a-button>
  <a-drawer
    v-model:open="visible"
    :title="$t('components.navigation.systemSetting.systemTitle')"
    placement="right"
    :width="600"
    class="takt-header-setting-drawer"
  >
    <template #extra>
      <div class="drawer-extra-actions">
        <a-space>
          <a-button class="takt-button takt-button-copy" @click="handleCopy">
            <template #icon>
              <RiFileCopyLine />
            </template>
            {{ $t('common.button.copy') }} {{ $t('common.button.preferences') }} {{ $t('common.button.config') }}
          </a-button>
          <a-button class="takt-button takt-button-reset" @click="handleReset">
            <template #icon>
              <RiRefreshLine />
            </template>
            {{ $t('common.button.reset') }}
          </a-button>
        </a-space>
      </div>
    </template>
    <div class="takt-header-setting-drawer__body">
      <a-tabs v-model:activeKey="activeTab">
        <a-tab-pane key="layout" :tab="$t('components.navigation.systemSetting.layout')">
          <LayoutSettings @change="handleSettingChange" />
        </a-tab-pane>
        <a-tab-pane key="theme" :tab="$t('components.navigation.systemSetting.theme')">
          <ThemeSettings @change="handleSettingChange" />
        </a-tab-pane>
        <a-tab-pane key="navigation" :tab="$t('components.navigation.systemSetting.navigation')">
          <NavigationSettings @change="handleSettingChange" />
        </a-tab-pane>
        <a-tab-pane key="tabs" :tab="$t('components.navigation.systemSetting.tabs')">
          <TabsSettings @change="handleSettingChange" />
        </a-tab-pane>
        <a-tab-pane key="other" :tab="$t('components.navigation.systemSetting.other')">
          <OtherSettings @change="handleSettingChange" />
        </a-tab-pane>
      </a-tabs>
    </div>
  </a-drawer>
</template>

<script setup lang="ts">
import { ref, reactive, provide } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { RiTShirtLine, RiFileCopyLine, RiRefreshLine } from '@remixicon/vue'
import { getSetting, defaultSetting, useSettingStore, type AppSetting } from '@/stores/setting'
import { applySettings, notifySettingsChanged } from '@/utils/apply-settings'
import LayoutSettings from './tabs/LayoutSettings.vue'
import ThemeSettings from './tabs/ThemeSettings.vue'
import NavigationSettings from './tabs/NavigationSettings.vue'
import TabsSettings from './tabs/TabsSettings.vue'
import OtherSettings from './tabs/OtherSettings.vue'

const { t } = useI18n()

interface Props {
  /** 组件类型（用于兼容性，实际不使用） */
  type?: 'icon' | 'button' | 'dropdown'
}

// 定义 props 以接受 type 属性，避免 Vue 警告
defineProps<Props>()

const emit = defineEmits<{
  'change': [setting: Partial<AppSetting>]
}>()

const visible = ref(false)
const activeTab = ref('layout')

// 当前设置状态
const currentSetting = reactive<AppSetting>({ ...getSetting() })

// 通过 provide 传递设置对象给子组件
provide('setting', currentSetting)

const handleClick = () => {
  visible.value = true
  // 重新加载设置
  Object.assign(currentSetting, getSetting())
}

// 处理设置变化（实时保存并立即生效）
const handleSettingChange = () => {
  useSettingStore().setSetting(currentSetting)
  applySettings()
  notifySettingsChanged()
}

// 复制偏好设置到剪贴板
const handleCopy = async () => {
  try {
    const settingJson = JSON.stringify(currentSetting, null, 2)
    await navigator.clipboard.writeText(settingJson)
    message.success(t('components.navigation.systemSetting.preferencesCopied'))
  } catch (error) {
    console.error('[TaktHeaderSetting] copy failed:', error)
    message.error(t('components.navigation.systemSetting.copyFail'))
  }
}

// 重置设置为默认值
const handleReset = () => {
  Object.assign(currentSetting, defaultSetting)
  useSettingStore().resetSetting()
  applySettings()
  notifySettingsChanged()
  message.success(t('components.navigation.systemSetting.resetToDefault'))
}
</script>

<style scoped lang="less">
.takt-header-setting-drawer {
  :deep(.ant-drawer-content-wrapper) {
    right: 16px;
  }
}

.drawer-extra-actions {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
}
</style>

<!-- 非 scoped：Drawer 可能渲染到 body，内容区内边距与表单项样式统一在此设置，避免与各 tab 的 padding 冲突 -->
<style lang="less">
/* 本抽屉的 body 不保留组件库默认 padding，仅由下方 __body 提供 4px 8px，避免叠加 */
.ant-drawer-body:has(.takt-header-setting-drawer__body) {
  padding: 0;
}

.takt-header-setting-drawer__body {
  padding: 4px 8px; /* 上下 4px，左右 8px */
}

/* 各 tab 内 a-space 表单项间隔统一为 16px */
.takt-header-setting-drawer__body .ant-space-vertical {
  row-gap: 16px !important;
}
.takt-header-setting-drawer__body .ant-space-vertical .ant-space-item {
  margin-bottom: 0 !important;
}

.takt-header-setting-drawer__body .ant-form-item {
  width: 100%;
  margin-bottom: 0;

  .ant-form-item-row {
    display: flex;
    flex-wrap: nowrap;
    width: 100%;
    align-items: flex-start;
    gap: 8px;
  }

  .ant-form-item-label {
    flex: 0 0 30%;
    max-width: 30%;
    min-width: 0;
    text-align: left;
    padding-right: 0;

    > label {
      justify-content: flex-start;
    }
  }

  .ant-form-item-control {
    flex: 1 1 70%;
    min-width: 0;
    display: flex;
    justify-content: flex-start;

    .ant-form-item-control-input {
      flex: 1;
      min-width: 0;
      max-width: 100%;
    }

    .ant-form-item-control-input-content {
      display: flex;
      flex-wrap: wrap;
      justify-content: flex-start;
      align-items: center;
      width: 100%;
      min-width: 0;
    }

    /* switch、radio-group、input-number 控件区右对齐 */
    .ant-form-item-control-input-content:has(.ant-switch),
    .ant-form-item-control-input-content:has(.ant-radio-group),
    .ant-form-item-control-input-content:has(.ant-input-number) {
      justify-content: flex-end;
    }

    /* slider 占满控件区可用宽度，轨道正常显示 */
    .ant-form-item-control-input-content .ant-slider {
      flex: 1;
      min-width: 100px;
      max-width: 100%;
      margin-inline: 0 8px;
    }
    .ant-form-item-control-input-content .ant-slider .ant-slider-rail {
      width: 100%;
    }
  }
}
</style>
