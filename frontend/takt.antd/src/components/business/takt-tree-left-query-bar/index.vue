<!-- ======================================== -->
<!-- 命名空间：@/components/business/takt-tree-left-query-bar -->
<!-- 功能描述：左树查询栏，宽度为视口的 1/4，用于树表布局左侧的树关键字查询 -->
<!-- ======================================== -->

<template>
  <div v-if="show" class="takt-tree-left-query-bar">
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
  </div>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { RiSearchLine } from '@remixicon/vue'
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

const placeholderDisplay = computed(() => props.placeholder ?? t('common.form.placeholder.treeKeyword'))

const emit = defineEmits<{
  'update:modelValue': [value: string]
  'search': [keyword: string]
  'reset': []
  'change': [value: string]
}>()

const keyword = ref(props.modelValue)

watch(() => props.modelValue, (val) => {
  keyword.value = val
})

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

defineExpose({
  keyword,
  clear: handleReset,
  search: handleSearch
})
</script>

<style scoped lang="less">
/* 宽度：内容视口的 1/5（20%），与左侧树/工具栏统一 */
.takt-tree-left-query-bar {
  flex: 0 0 20%;
  width: 20%;
  min-width: 160px;
  max-width: 20%;
  //margin: 4px;
  padding: 0px;
  display: flex;
  align-items: center;
  gap: 8px;
  box-sizing: border-box;

  :deep(.ant-input-affix-wrapper) {
    flex: 1;
    min-width: 0;

    .ant-input-prefix {
      margin-inline-end: 4px;

      svg {
        color: var(--ant-color-text-secondary);
        fill: currentColor;
      }
    }
  }
}
</style>
