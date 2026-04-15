<template>
  <div v-if="show" class="takt-query-bar">
    <a-input
      v-model:value="keyword"
      :placeholder="placeholderDisplay"
      :size="size"
      :allow-clear="allowClear"
      :max-length="maxLength"
      @press-enter="handleSearch"
      @change="handleChange"
    >
      <template #prefix>
        <RiSearchLine />
      </template>
    </a-input>
    <a-space class="query-actions">
      <a-button
        class="takt-button-query"
        :loading="loading"
        @click="handleSearch"
      >
        <template #icon>
          <RiSearchLine />
        </template>
        {{ t('common.button.query') }}
      </a-button>
      <a-button
        class="takt-button-reset"
        @click="handleReset"
      >
        <template #icon>
          <RiRefreshLine />
        </template>
        {{ t('common.button.reset') }}
      </a-button>
    </a-space>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { RiSearchLine, RiRefreshLine } from '@remixicon/vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

interface Props {
  /** 是否显示 */
  show?: boolean
  /** 关键字值（v-model） */
  modelValue?: string
  /** 占位符 */
  placeholder?: string
  /** 输入框尺寸 */
  size?: 'small' | 'middle' | 'large'
  /** 是否显示清除按钮 */
  allowClear?: boolean
  /** 加载状态 */
  loading?: boolean
  /** 最大长度 */
  maxLength?: number
}

const props = withDefaults(defineProps<Props>(), {
  show: true,
  modelValue: '',
  placeholder: undefined,
  size: 'middle',
  allowClear: true,
  loading: false,
  maxLength: undefined
})

const placeholderDisplay = computed(() => props.placeholder ?? t('common.form.placeholder.searchKeyword'))

const emit = defineEmits<{
  'update:modelValue': [value: string]
  'search': [keyword: string]
  'reset': []
  'change': [value: string]
}>()

const keyword = ref(props.modelValue)

// 监听 props 变化
watch(() => props.modelValue, (val) => {
  keyword.value = val
})

// 监听内部值变化
watch(keyword, (val) => {
  emit('update:modelValue', val)
})

const handleSearch = () => {
  emit('search', keyword.value)
}

const handleReset = () => {
  keyword.value = ''
  emit('update:modelValue', '')
  emit('reset')
}

const handleChange = (e: Event) => {
  const value = (e.target as HTMLInputElement).value
  emit('change', value)
}

// 暴露方法供父组件调用
defineExpose({
  keyword,
  clear: handleReset,
  search: handleSearch
})
</script>

<style scoped lang="less">
/* 宽度：内容视口的 4/5（80%）；右边距 0（左侧栏已有 4px 右外边距） */
.takt-query-bar {
  flex: 0 0 80%;
  width: 80%;
  min-width: 200px;
  max-width: 80%;
  //margin: 4px 0 4px 4px;
  padding: 4px;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  gap: 8px;
  /* 输入框宽度：当前栏（内容视口 4/5）的 1/3 */
  :deep(.ant-input-affix-wrapper) {
    flex: 0 0 33.333%;
    width: 33.333%;
    min-width: 0;

    .ant-input-prefix {
      margin-inline-end: 4px;

      svg {
        color: var(--ant-color-text-secondary);
        fill: currentColor;
      }
    }
  }

  .query-actions {
    flex-shrink: 0;

    :deep(.ant-btn) {
      display: inline-flex;
      align-items: center;
      gap: 4px;

      .anticon {
        margin-inline-end: 0 !important;
      }
    }
  }
}
</style>
