<template>
  <!-- 按钮样式 -->
  <a-tooltip v-if="type === 'button' && themeColorLocked" :title="$t('common.settings.color.locked')" placement="top">
    <span>
      <a-button
        v-if="type === 'button'"
        :type="buttonType"
        :size="size"
        disabled
        v-bind="$attrs"
      >
        <template #icon>
          <RiPaletteLine style="margin-right: 8px;" />
        </template>
        <slot>{{ $t('common.settings.color.switch') }}</slot>
      </a-button>
    </span>
  </a-tooltip>
  <a-button
    v-else-if="type === 'button'"
    :type="buttonType"
    :size="size"
    @click="handleToggle"
    v-bind="$attrs"
  >
    <template #icon>
      <RiPaletteLine style="margin-right: 8px;" />
    </template>
    <slot>{{ $t('common.settings.color.switch') }}</slot>
  </a-button>

  <!-- 图标按钮样式：默认收缩，滑过向左展开，选中后自动收缩；移出延迟收缩以便能移入颜色圈选中 -->
  <div
    v-else-if="type === 'icon'"
    class="color-toggle-group"
    :class="{ expanded: isExpanded }"
    @mouseenter="onGroupEnter"
    @mouseleave="onGroupLeave"
  >
    <div
      class="color-circles-container"
      :class="{ expanded: isExpanded }"
    >
      <a-tooltip
        v-for="(value, key) in themeColorMap"
        :key="key"
        :title="$t(`common.settings.color.${key}`)"
        placement="top"
      >
        <div
          class="color-circle-button"
          :class="{ active: currentThemeType === key }"
          :style="{ backgroundColor: value }"
          @click.stop="handleColorSelect(key)"
        >
          <RiCheckLine
            v-if="currentThemeType === key"
            style="color: white;"
          />
        </div>
      </a-tooltip>
    </div>
    <a-tooltip v-if="themeColorLocked" :title="$t('common.settings.color.locked')" placement="top">
      <a-button
        type="text"
        :size="size"
        class="color-palette-button"
        v-bind="$attrs"
      >
        <template #icon>
          <RiPaletteLine :style="{ color: themeColorValue }" />
        </template>
      </a-button>
    </a-tooltip>
    <a-button
      v-else
      type="text"
      :size="size"
      class="color-palette-button"
      v-bind="$attrs"
    >
      <template #icon>
        <RiPaletteLine :style="{ color: themeColorValue }" />
      </template>
    </a-button>
  </div>

  <!-- 下拉菜单样式 -->
  <a-tooltip v-if="type === 'dropdown' && themeColorLocked" :title="$t('common.settings.color.locked')" placement="top">
    <span>
      <a-dropdown
        v-if="type === 'dropdown'"
        :trigger="['click']"
        :disabled="themeColorLocked"
        v-bind="$attrs"
      >
        <a-button type="text" :size="size">
          <template #icon>
            <RiPaletteLine style="margin-right: 8px;" />
          </template>
          <slot>{{ $t('common.settings.color.title') }}</slot>
        </a-button>
        <template #overlay>
      <a-menu :selected-keys="[currentThemeType]" @click="handleMenuClick">
        <a-menu-item
          v-for="(value, key) in themeColorMap"
          :key="key"
        >
          <span class="color-item">
            <span
              class="color-dot"
              :style="{ backgroundColor: value }"
            ></span>
            {{ $t(`common.settings.color.${key}`) }}
          </span>
        </a-menu-item>
      </a-menu>
        </template>
      </a-dropdown>
    </span>
  </a-tooltip>
  <a-dropdown
    v-else-if="type === 'dropdown'"
    :trigger="['click']"
    v-bind="$attrs"
  >
    <a-button type="text" :size="size">
      <template #icon>
        <RiPaletteLine style="margin-right: 8px;" />
      </template>
      <slot>{{ $t('common.settings.color.title') }}</slot>
    </a-button>
    <template #overlay>
      <a-menu :selected-keys="[currentThemeType]" @click="handleMenuClick">
        <a-menu-item
          v-for="(value, key) in themeColorMap"
          :key="key"
        >
          <span class="color-item">
            <span
              class="color-dot"
              :style="{ backgroundColor: value }"
            ></span>
            {{ $t(`common.settings.color.${key}`) }}
          </span>
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>

  <!-- 单选按钮组样式 -->
  <a-tooltip v-else-if="type === 'radio' && themeColorLocked" :title="$t('common.settings.color.locked')" placement="top">
    <span>
      <a-radio-group
        v-if="type === 'radio'"
        :value="currentThemeType"
        :size="radioSize"
        disabled
        v-bind="$attrs"
      >
        <a-radio-button
          v-for="(value, key) in themeColorMap"
          :key="key"
          :value="key"
        >
          <span class="color-item">
            <span
              class="color-dot"
              :style="{ backgroundColor: value }"
            ></span>
            {{ $t(`common.settings.color.${key}`) }}
          </span>
        </a-radio-button>
      </a-radio-group>
    </span>
  </a-tooltip>
  <a-radio-group
    v-else-if="type === 'radio'"
    :value="currentThemeType"
    :size="radioSize"
    @change="handleRadioChange"
    v-bind="$attrs"
  >
    <a-radio-button
      v-for="(value, key) in themeColorMap"
      :key="key"
      :value="key"
    >
      <span class="color-item">
        <span
          class="color-dot"
          :style="{ backgroundColor: value }"
        ></span>
        {{ $t(`common.settings.color.${key}`) }}
      </span>
    </a-radio-button>
  </a-radio-group>
</template>

<script setup lang="ts">
/**
 * 颜色切换组件，引用 @/stores/setting 的 themeColorMap（与 @/assets/styles/color-base.less 十大著名色彩 完全一致）
 * Less → themeColorMap key: @mars-green→green, @tiffany-blue→cyan, @chinese-red→red, @titian-red→orange, @burgundy→purple, @bordeaux→pink, @klein-blue→blue, @van-dyke-brown→brown, @prussian-blue→indigo, @sennelier-yellow→yellow, @memorial-gray→gray 纪念灰
 */
import { ref, computed, onUnmounted } from 'vue'
import { storeToRefs } from 'pinia'
import type { RadioChangeEvent } from 'ant-design-vue'
import { useSettingStore, themeColorMap, getEffectiveThemeColorValue, getFixedThemeForDate, isThemeColorLocked, type ThemeColor } from '@/stores/setting'
import { applySettings } from '@/utils/apply-settings'
import { RiPaletteLine, RiCheckLine } from '@remixicon/vue'

type ThemePreset = Exclude<ThemeColor, 'custom'>

const themePresetKeys = Object.keys(themeColorMap) as ThemePreset[]

interface Props {
  type?: 'button' | 'icon' | 'dropdown' | 'radio'
  buttonType?: 'default' | 'primary' | 'dashed' | 'link' | 'text'
  size?: 'small' | 'middle' | 'large'
}

const props = withDefaults(defineProps<Props>(), {
  type: 'icon',
  buttonType: 'default',
  size: 'middle'
})

const store = useSettingStore()
const { setting } = storeToRefs(store)
const isExpanded = ref(false)
const leaveTimer = ref<ReturnType<typeof setTimeout> | null>(null)

const COLLAPSE_DELAY_MS = 200

function onGroupEnter() {
  if (themeColorLocked.value) return
  if (leaveTimer.value != null) {
    clearTimeout(leaveTimer.value)
    leaveTimer.value = null
  }
  isExpanded.value = true
}

function onGroupLeave() {
  if (leaveTimer.value != null) {
    clearTimeout(leaveTimer.value)
  }
  leaveTimer.value = setTimeout(() => {
    isExpanded.value = false
    leaveTimer.value = null
  }, COLLAPSE_DELAY_MS)
}

onUnmounted(() => {
  if (leaveTimer.value != null) {
    clearTimeout(leaveTimer.value)
  }
})

const themeColorLocked = computed(() => isThemeColorLocked())

const currentThemeType = computed(() => {
  const fixed = getFixedThemeForDate()
  if (fixed != null) return fixed
  const type = setting.value?.themeColor?.type
  return type && type !== 'custom' && type in themeColorMap ? type : 'blue'
})
const themeColorValue = computed(() => getEffectiveThemeColorValue(setting.value?.themeColor ?? { type: 'blue' }))

const radioSize = computed<'default' | 'small' | 'large' | undefined>(() =>
  props.size === 'middle' ? 'default' : props.size
)

function setThemeColor(type: ThemePreset) {
  if (themeColorLocked.value) return
  store.setSetting({ themeColor: { type } })
  applySettings()
}

const handleToggle = () => {
  const idx = themePresetKeys.indexOf(currentThemeType.value)
  const next = themePresetKeys[(idx + 1) % themePresetKeys.length]
  setThemeColor(next)
}

const handleMenuClick = ({ key }: { key: string | number }) => {
  setThemeColor(String(key) as ThemePreset)
}

const handleRadioChange = (e: RadioChangeEvent) => {
  const value = e.target?.value
  if (value != null && value in themeColorMap) {
    setThemeColor(value as ThemePreset)
  }
}

const handleColorSelect = (color: ThemePreset) => {
  setThemeColor(color)
  if (leaveTimer.value != null) {
    clearTimeout(leaveTimer.value)
    leaveTimer.value = null
  }
  isExpanded.value = false
}
</script>

<style scoped lang="less">
.color-item {
  display: inline-flex;
  align-items: center;
  gap: 8px;
}

.color-dot {
  display: inline-block;
}

.color-toggle-group {
  position: relative;
  display: inline-flex;
  align-items: center;
}

/* 展开时向左延伸感应区，使鼠标从按钮移入颜色圈时仍算在组内，可选中颜色；11 色 24px+8px 需约 360px */
.color-toggle-group.expanded::before {
  content: '';
  position: absolute;
  right: 100%;
  top: 0;
  bottom: 0;
  width: 360px;
  margin-right: 8px;
  z-index: 0;
}

.color-circles-container {
  position: absolute;
  right: 100%;
  top: 50%;
  transform: translateY(-50%);
  display: inline-flex;
  align-items: center;
  gap: 8px;
  margin-right: 8px;
  z-index: 10;
  max-width: 0;
  opacity: 0;
  overflow: hidden;
  pointer-events: none;
  transition: max-width 0.25s ease-out, opacity 0.2s ease-out;
}

.color-circles-container.expanded {
  max-width: 360px;
  opacity: 1;
  pointer-events: auto;
}

.color-circle-button {
  width: 24px;
  height: 24px;
  border-radius: 50%;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  cursor: pointer;
  transition: opacity 0.15s ease-out;
}

.color-circles-container:not(.expanded) .color-circle-button {
  opacity: 0;
}

.color-palette-button {
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

</style>




