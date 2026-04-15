<template>
  <a-dropdown :trigger="['click']" placement="bottomRight">
    <a-button type="text">
      <template #icon>
        <RiLayoutLeftLine v-if="currentPosition === 'left'" />
        <RiLayoutColumnLine v-else-if="currentPosition === 'center'" />
        <RiLayoutRightLine v-else />
      </template>
    </a-button>
    <template #overlay>
      <a-menu :selected-keys="[currentPosition]" @click="handleMenuClick">
        <a-menu-item key="left">
          <span style="display: inline-flex; align-items: center;">
            <RiLayoutLeftLine style="margin-right: 8px;" />
            {{ $t('common.layout.position.left') }}
          </span>
        </a-menu-item>
        <a-menu-item key="center">
          <span style="display: inline-flex; align-items: center;">
            <RiLayoutColumnLine style="margin-right: 8px;" />
            {{ $t('common.layout.position.center') }}
          </span>
        </a-menu-item>
        <a-menu-item key="right">
          <span style="display: inline-flex; align-items: center;">
            <RiLayoutRightLine style="margin-right: 8px;" />
            {{ $t('common.layout.position.right') }}
          </span>
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { RiLayoutLeftLine, RiLayoutColumnLine, RiLayoutRightLine } from '@remixicon/vue'

interface Props {
  /** 登录表单在页面中的位置：left（左对齐）| center（居中）| right（右对齐） */
  position?: 'left' | 'center' | 'right'
}

const props = withDefaults(defineProps<Props>(), {
  position: 'right'
})

const emit = defineEmits<{
  'update:position': [value: 'left' | 'center' | 'right']
}>()

// 从 localStorage 读取保存的登录表单位置，如果没有则使用 props.position（默认为 right）
// 注意：这只是控制登录表单在页面中的位置（左、中、右），不是全局布局
const savedPosition = localStorage.getItem('loginLayoutPosition') as
  | 'left'
  | 'center'
  | 'right'
  | null
const currentPosition = ref<'left' | 'center' | 'right'>(
  savedPosition || props.position
)

// 监听外部 position 变化
watch(
  () => props.position,
  (newPosition) => {
    currentPosition.value = newPosition
  }
)

// 监听内部 position 变化，同步到外部
watch(currentPosition, (newPosition) => {
  emit('update:position', newPosition)
  localStorage.setItem('loginLayoutPosition', newPosition)
})

// 处理菜单点击
const handleMenuClick = ({ key }: { key: string | number }) => {
  currentPosition.value = String(key) as 'left' | 'center' | 'right'
}
</script>

<style scoped lang="less">
// 让 Remix Icon 的 SVG 适配主题颜色
:deep(svg) {
  font-size: 16px !important;
  width: 16px !important;
  height: 16px !important;
  display: inline-flex !important;
  align-items: center !important;
  justify-content: center !important;
  vertical-align: middle !important;
  color: var(--ant-color-text);
  fill: currentColor;
}

// 菜单项：确保图标和文本水平对齐
:deep(.ant-menu-item) {
  display: flex !important;
  flex-direction: row !important;
  align-items: center !important;
  white-space: nowrap !important;

  svg {
    font-size: 16px !important;
    width: 16px !important;
    height: 16px !important;
    flex-shrink: 0 !important;
    margin-right: 8px !important;
    vertical-align: middle !important;
    display: inline-flex !important;
    align-items: center !important;
    justify-content: center !important;
  }
}

// 确保组件本身和按钮居中
:deep(.ant-dropdown) {
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-btn) {
  padding: 2px !important;
  margin: 0 !important;
  display: inline-flex !important;
  align-items: center !important;
  justify-content: center !important;
  
  // 确保图标居中
  .anticon,
  svg {
    font-size: 16px !important;
    width: 16px !important;
    height: 16px !important;
    display: inline-flex !important;
    align-items: center !important;
    justify-content: center !important;
    margin: 0;
    vertical-align: middle !important;
  }
}
</style>
