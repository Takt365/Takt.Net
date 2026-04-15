<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-select -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：Takt 下拉选择框组件，支持 API 动态加载、字典数据、业务选项等 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 字典数量 3 个及以下且非多选模式下，使用 Radio 单选 -->
  <a-radio-group
    v-if="shouldUseRadio && !multiple"
    :value="modelValue"
    :disabled="disabled"
    :size="radioSize"
    @change="handleRadioChange"
  >
    <a-radio
      v-for="option in radioOptions"
      :key="String(option.value)"
      :value="option.value"
    >
      {{ option.label }}
    </a-radio>
  </a-radio-group>
  
  <!-- 其他情况使用 Select 下拉选择框 -->
  <a-select
    v-else
    :value="modelValue"
    :options="options"
    :loading="loading"
    :placeholder="placeholderDisplay"
    :allow-clear="allowClear"
    :disabled="disabled"
    :multiple="multiple"
    :size="size"
    :show-search="showSearch"
    :filter-option="filterOption"
    :max-tag-count="effectiveMaxTagCount"
    :virtual="shouldUseVirtual"
    :list-height="listHeight"
    @change="handleChange"
    @search="handleSearch"
    v-bind="$attrs"
  >
    <template v-if="$slots.default" #default>
      <slot />
    </template>
  </a-select>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted, nextTick } from 'vue'
import type { SelectValue, DefaultOptionType, LabeledValue } from 'ant-design-vue/es/select'
import type { TaktSelectOption } from '@/types/common'
import request from '@/api/request'
import { logger } from '@/utils/logger'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

interface Props {
  /** 绑定值 */
  modelValue?: string | number | (string | number)[]
  /** 字典类型编码（用于加载字典数据，优先级高于 apiUrl） */
  dictType?: string
  /** API 端点（可选，如果提供了 dictType 或 options 则不需要） */
  apiUrl?: string
  /** 选项数据（可选，如果提供了则直接使用，不再通过 dictType 或 apiUrl 加载）。支持标准格式 { label, value } 或 TaktSelectOption 格式 { dictLabel, dictValue } */
  options?: TaktSelectOption[] | Array<{ label: string; value: string | number; [key: string]: any }>
  /** 占位符 */
  placeholder?: string
  /** 是否显示清除按钮 */
  allowClear?: boolean
  /** 是否禁用 */
  disabled?: boolean
  /** 是否多选 */
  multiple?: boolean
  /** 尺寸 */
  size?: 'small' | 'middle' | 'large'
  /** 是否支持搜索 */
  showSearch?: boolean
  /** 自定义过滤函数 */
  filterOption?: boolean | ((input: string, option: any) => boolean)
  /** 多选时最多显示多少个标签，超出部分以 +N 形式展示。支持数字或 'responsive'（响应式模式，但不推荐在大表单场景下使用，因为对性能有所消耗） */
  maxTagCount?: number | 'responsive'
  /** 是否开启虚拟滚动（大数据量时建议开启，可提升渲染性能）。如果不指定，当选项数量超过 100 条时会自动开启 */
  virtual?: boolean
  /** 虚拟滚动时列表高度（单位：px），默认 256px */
  listHeight?: number
  /** 字段映射配置（用于自定义 label 和 value 字段名） */
  fieldNames?: {
    label?: string
    value?: string
  }
}

const props = withDefaults(defineProps<Props>(), {
  placeholder: undefined,
  allowClear: true,
  disabled: false,
  multiple: false,
  size: 'middle',
  showSearch: true,
  filterOption: true,
  maxTagCount: undefined,
  virtual: false,
  listHeight: 256,
  fieldNames: () => ({
    label: 'label',
    value: 'value'
  })
})

const placeholderDisplay = computed(() => props.placeholder ?? t('common.form.placeholder.selectOnly'))

const emit = defineEmits<{
  'update:modelValue': [value: string | number | (string | number)[] | undefined]
  'change': [value: string | number | (string | number)[] | undefined, option: any]
  'search': [value: string]
}>()

const loading = ref(false)
const rawData = ref<TaktSelectOption[]>([])
const dictDataStore = useDictDataStore()

/**
 * 推断期望的值类型（根据 modelValue 的类型）
 * @param modelValue 绑定值
 * @returns 期望的值类型：'number' | 'string'
 */
function inferValueType(modelValue: string | number | (string | number)[] | undefined): 'number' | 'string' {
  if (modelValue === undefined || modelValue === null) {
    // 无法推断，默认返回 string
    return 'string'
  }
  
  // 如果是数组，检查第一个元素的类型
  if (Array.isArray(modelValue)) {
    if (modelValue.length === 0) {
      return 'string'
    }
    return typeof modelValue[0] === 'number' ? 'number' : 'string'
  }
  
  // 单个值，直接检查类型
  return typeof modelValue === 'number' ? 'number' : 'string'
}

/**
 * 判断值是否是数值类型（number 或可转换为 number 的 string）
 */
function isNumericValue(value: string | number | (string | number)[] | undefined): boolean {
  if (value == null) return false
  
  if (Array.isArray(value)) {
    if (value.length === 0) return false
    const first = value[0]
    return typeof first === 'number' || (typeof first === 'string' && first !== '' && !isNaN(Number(first)))
  }
  
  return typeof value === 'number' || (typeof value === 'string' && value !== '' && !isNaN(Number(value)))
}

/**
 * 判断字符串是否是纯数字字符串（用于字典值的类型转换）
 */
function isNumericString(str: string): boolean {
  if (!str || str.trim() === '') return false
  const trimmed = String(str).trim()
  return /^-?\d+(\.\d+)?$/.test(trimmed) && !isNaN(Number(trimmed))
}

/**
 * 转换值类型（根据期望的类型转换字典数据的值）
 */
function convertValueType(value: string | number, expectedType: 'number' | 'string', dictType: string): string | number {
  if (typeof value === expectedType) {
    return value
  }
  
  if (expectedType === 'number' && typeof value === 'string') {
    if (!value || value.trim() === '') {
      logger.warn(`[TaktSelect] 字典类型 ${dictType} 的值 "${value}" 为空，无法转换为 number，返回 0`)
      return 0
    }
    
    if (!isNumericString(value)) {
      logger.error(`[TaktSelect] 字典类型 ${dictType} 的值 "${value}" 不是数值字符串，无法转换为 number`)
      throw new Error(`字典数据值类型转换失败：字典类型 ${dictType} 的值 "${value}" 不是数值字符串，无法转换为 number 类型`)
    }
    
    const numValue = Number(value)
    if (isNaN(numValue)) {
      logger.error(`[TaktSelect] 字典类型 ${dictType} 的值 "${value}" 无法转换为 number`)
      throw new Error(`字典数据值类型转换失败：字典类型 ${dictType} 的值 "${value}" 无法转换为 number 类型`)
    }
    
    return numValue
  }
  
  if (expectedType === 'string' && typeof value === 'number') {
    return String(value)
  }
  
  return value
}

// 将后端数据转换为 Select 组件需要的格式
const options = computed(() => {
  const expectedValueType = inferValueType(props.modelValue)
  
  // 如果直接提供了 options，需要转换字段名和值类型
  if (props.options?.length) {
    const labelField = props.fieldNames?.label ?? 'label'
    const valueField = props.fieldNames?.value ?? 'value'
    
    return props.options.map(item => {
      const itemObj = item as Record<string, any>
      const rawValue = 'value' in item ? item.value : (itemObj[valueField] ?? itemObj.dictValue ?? itemObj.extLabel ?? itemObj.extValue ?? '')
      const label = 'label' in item ? item.label : (itemObj[labelField] ?? itemObj.dictLabel ?? itemObj.extLabel ?? '')
      const convertedValue = convertValueType(rawValue, expectedValueType, props.dictType || 'custom')
      
      return {
        ...itemObj,
        [labelField]: label,
        [valueField]: convertedValue,
        label,
        value: convertedValue
      }
    })
  }
  
  // 如果提供了 dictType，从字典 store 加载数据
  if (props.dictType) {
    const labelField = props.fieldNames?.label
    const valueField = props.fieldNames?.value
    
    // 转换 fieldNames 为 getDictOptions 需要的格式
    const dictLabelField: 'dictLabel' | 'extLabel' = (labelField === 'extLabel' ? 'extLabel' : 'dictLabel')
    const dictValueField: 'dictValue' | 'extLabel' | 'extValue' = 
      (valueField === 'extLabel' ? 'extLabel' : valueField === 'extValue' ? 'extValue' : 'dictValue')
    
    const dictOptions = dictDataStore.getDictOptions(props.dictType, {
      labelField: dictLabelField,
      valueField: dictValueField
    })
    
    // 根据 modelValue 的类型推断期望的值类型
    let expectedValueType = inferValueType(props.modelValue)
    
    // 如果 modelValue 是 undefined/null，但所有字典选项的值都是数值字符串，则推断为 number 类型
    if (expectedValueType === 'string' && props.modelValue == null) {
      if (dictOptions.every(option => isNumericValue(option.value))) {
        expectedValueType = 'number'
      }
    }
    
    return dictOptions.map(option => ({
      ...option,
      value: convertValueType(option.value, expectedValueType, props.dictType || '')
    }))
  }
  
  // 否则使用从 API 加载的数据
  const labelField = props.fieldNames?.label ?? 'label'
  const valueField = props.fieldNames?.value ?? 'value'
  
  return rawData.value.map(item => {
    const itemAny = item as any
    const label = labelField === 'extLabel' 
      ? String(itemAny.extLabel ?? itemAny.dictLabel ?? '')
      : (itemAny.dictLabel ?? '')
    
    const rawValue = valueField === 'extLabel'
      ? (itemAny.extLabel ?? itemAny.dictValue ?? '')
      : valueField === 'extValue'
      ? (itemAny.extValue ?? itemAny.dictValue ?? '')
      : (itemAny.dictValue ?? '')
    
    const convertedValue = convertValueType(rawValue, expectedValueType, props.dictType || 'api')
    
    return {
      ...item,
      [labelField]: label,
      [valueField]: convertedValue,
      label,
      value: convertedValue
    }
  })
})

// 根据数据量自动决定是否开启虚拟滚动（超过 100 条自动开启）
const shouldUseVirtual = computed(() => {
  return props.virtual ?? options.value.length > 100
})

// 判断是否应该使用 Radio 单选（字典数量 3 个及以下且非多选模式，且值必须是数值类型）
const shouldUseRadio = computed(() => {
  if (!props.dictType || props.multiple || options.value.length === 0 || options.value.length > 3) {
    return false
  }
  
  const allOptionsAreNumeric = options.value.every(option => isNumericValue((option as any).value))
  if (!allOptionsAreNumeric) {
    return false
  }
  
  return props.modelValue == null || isNumericValue(props.modelValue)
})

// Radio 组件的尺寸（RadioGroup 不支持 'middle'，只支持 'small' | 'large' | 'default'）
const radioSize = computed(() => {
  return props.size === 'middle' ? 'default' : (props.size === 'small' ? 'small' : 'large')
})

// Radio 选项数据（options.value 已包含正确转换后的 label 和 value）
const radioOptions = computed(() => {
  return options.value.map(option => {
    const item = option as any
    return {
      label: item.label ?? item.dictLabel ?? '',
      value: item.value ?? ''
    }
  })
})

// 多选时，maxTagCount 只在 multiple 为 true 时生效
// 多选模式下，如果未设置 maxTagCount，默认显示 3 个标签
const effectiveMaxTagCount = computed(() => {
  return props.multiple ? (props.maxTagCount ?? 3) : undefined
})

// 加载数据
const loadData = async () => {
  // 如果提供了 options，直接使用，不需要加载
  if (props.options?.length) {
    return
  }
  
  // 如果提供了 dictType，从字典 store 加载，不需要 API 请求
  if (props.dictType) {
    return
  }
  
  // 如果提供了 apiUrl，通过 API 加载数据
  if (props.apiUrl) {
    try {
      loading.value = true
      const data = await request<TaktSelectOption[]>({
        url: props.apiUrl,
        method: 'get'
      })
      rawData.value = Array.isArray(data) ? data : []
    } catch (error) {
      logger.error('[TaktSelect] 加载数据失败:', error)
      rawData.value = []
    } finally {
      loading.value = false
    }
    return
  }
  
  // 如果 dictType、apiUrl 和 options 都未提供，才发出警告
  logger.warn('[TaktSelect] dictType、apiUrl 和 options 都未提供，无法加载数据')
}

// 辅助函数：从 SelectValue 中提取原始值
const extractRawValue = (value: SelectValue): string | number | (string | number)[] | undefined => {
  if (value === undefined || value === null) {
    return undefined
  }
  
  // 如果是数组
  if (Array.isArray(value)) {
    return value.map(v => {
      if (typeof v === 'object' && v !== null && 'value' in v) {
        return (v as LabeledValue).value
      }
      return v as string | number
    })
  }
  
  // 如果是对象（LabeledValue）
  if (typeof value === 'object' && 'value' in value) {
    return (value as LabeledValue).value
  }
  
  // 原始值
  return value as string | number
}

// 处理 Radio 值变化
const handleRadioChange = (event: any) => {
  const value = event?.target?.value ?? event
  if (value == null) return
  
  emit('update:modelValue', value)
  const option = radioOptions.value.find(opt => opt.value === value)
  emit('change', value, option ?? null)
}

// 处理 Select 值变化
const handleChange = (value: SelectValue, option: DefaultOptionType | DefaultOptionType[]) => {
  const rawValue = extractRawValue(value)
  emit('update:modelValue', rawValue)
  emit('change', rawValue, option)
}

// 处理搜索
const handleSearch = (value: string) => {
  emit('search', value)
}

// 监听 dictType、API URL 和 options 变化
watch(() => [props.dictType, props.apiUrl, props.options], () => {
  // 只有在使用 API 加载时才需要重新加载数据
  // 如果使用 dictType 或 options，数据会自动更新（通过 computed）
  if (!props.options?.length && !props.dictType && props.apiUrl) {
    loadData()
  }
})

onMounted(() => {
  // 使用 nextTick 确保 props 已经完全初始化（特别是在条件渲染的场景下）
  nextTick(() => {
    // 只有在使用 API 加载时才需要加载数据
    // 如果使用 dictType 或 options，数据会自动更新（通过 computed）
    // 只有在确认有 apiUrl 且没有 dictType 和 options 时才调用 loadData
    if (props.apiUrl && !props.dictType && !props.options?.length) {
      loadData()
    }
  })
})
</script>

<style scoped lang="less">
// 组件样式
</style>
