<!-- ========================================
项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间:@/components/business/takt-query-bar
文件名称:index.vue
创建时间:2025-01-20
创建人:Takt365(Cursor AI)
功能描述:查询栏组件,提供关键字搜索、查询和重置功能

版权信息:Copyright (c) 2025 Takt  All rights reserved.
免责声明:此软件使用 MIT License,作者不承担任何使用风险。
======================================== -->
<template>
  <div
    v-if="show"
    class="takt-query-bar"
  >
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
        {{ t('common.page.button.query') }}
      </a-button>
      <a-button
        class="takt-button-reset"
        @click="handleReset"
      >
        <template #icon>
          <RiRefreshLine />
        </template>
        {{ t('common.page.button.reset') }}
      </a-button>
    </a-space>
  </div>
</template>

<script setup lang="ts">
import { RiSearchLine, RiRefreshLine } from '@remixicon/vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

interface Props {
  /** 是否显示 */
  show?: boolean
  /** 关键字值(v-model) */
  modelValue?: string
  /** 占位符 */
  placeholder?: string | undefined
  /** 输入框尺寸 */
  size?: 'small' | 'middle' | 'large'
  /** 是否显示清除按钮 */
  allowClear?: boolean
  /** 加载状态 */
  loading?: boolean
  /** 最大长度 */
  maxLength?: number | undefined
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

const placeholderDisplay = computed(() => props.placeholder ?? t('common.page.form.placeholder.searchkeyword'))

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
.takt-query-bar {
  margin: 4px;
  padding: 4px;
  display: flex;
  align-items: center;
  gap: 8px;
  width: 100%;
  box-sizing: border-box;

  :deep(.ant-input-affix-wrapper) {
    width: 50vw;
    flex: 0 0 50vw;
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
