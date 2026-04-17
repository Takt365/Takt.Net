<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/holiday/components -->
<!-- 文件名称：holiday-form.vue -->
<!-- 功能描述：假日维护弹窗内嵌表单。由 holiday/index.vue 引用；通过 defineExpose 提供 validate、getValues、resetFields。基本信息：地区/名称、TaktSelect（dict-type=hr_holiday_type / hr_holiday_is_working_day）、日期、问候/引用、主题色块（themeColorMap）、备注。表单模型复用 `@/types/.../holiday` 中 `HolidayCreate`，不重复定义 interface。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 单 Tab 结构：预留 activeTab 便于后续扩展「其它」分页 -->
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicInfo')"
    >
      <div :class="formContentClass">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
          label-align="right"
        >
          <!-- 地区、假日名称（与 TaktHoliday.region / holiday_name 对应） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.holiday.region')"
                name="region"
              >
                <a-input
                  v-model:value="formState.region"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.holiday.region') })"
                  show-count
                  :maxlength="10"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.holiday.holidayname')"
                name="holidayName"
              >
                <a-input
                  v-model:value="formState.holidayName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.holiday.holidayname') })"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 假日类型、是否工作日（字典与实体 holiday_type / is_working_day 一致） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.holiday.holidaytype')"
                name="holidayType"
              >
                <TaktSelect
                  v-model="formState.holidayType"
                  dict-type="hr_holiday_type"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.holidaytype') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.holiday.isworkingday')"
                name="isWorkingDay"
              >
                <TaktSelect
                  v-model="formState.isWorkingDay"
                  dict-type="hr_holiday_is_working_day"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.isworkingday') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 开始/结束日期（date 列，value-format 与接口 YYYY-MM-DD 一致） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.holiday.startdate')"
                name="startDate"
              >
                <a-date-picker
                  v-model:value="formState.startDate"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.startdate') })"
                  value-format="YYYY-MM-DD"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.holiday.enddate')"
                name="endDate"
              >
                <a-date-picker
                  v-model:value="formState.endDate"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.enddate') })"
                  value-format="YYYY-MM-DD"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 问候语（holiday_greeting） -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.holiday.holidaygreeting')"
                name="holidayGreeting"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-input
                  v-model:value="formState.holidayGreeting"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.holiday.holidaygreeting') })"
                  allow-clear
                  show-count
                  :maxlength="200"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 引用/诗句（holiday_quote，选填） -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.holiday.holidayquote')"
                name="holidayQuote"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-textarea
                  v-model:value="formState.holidayQuote"
                  :placeholder="t('common.form.placeholder.optional', { field: t('entity.holiday.holidayquote') })"
                  :rows="2"
                  show-count
                  :maxlength="500"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 假日主题：写入 holiday_theme，值为 setting 中 themeColorMap 的 key -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.holiday.holidaytheme')"
                name="holidayTheme"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <div class="theme-color-picker">
                  <a-tooltip
                    v-for="(color, key) in themeColorMap"
                    :key="key"
                    :title="t(`common.settings.color.${key}`)"
                    placement="top"
                  >
                    <div
                      class="color-item"
                      :class="{ active: formState.holidayTheme === key }"
                      :style="{ backgroundColor: color }"
                      @click="formState.holidayTheme = key"
                    >
                      <RiCheckLine
                        v-if="formState.holidayTheme === key"
                        class="color-item-check"
                      />
                    </div>
                  </a-tooltip>
                </div>
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 备注（实体基类 remark） -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('common.entity.remark')"
                name="remark"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
                  :rows="2"
                  show-count
                  :maxlength="500"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </div>
    </a-tab-pane>
  </a-tabs>
</template>

<script setup lang="ts">
/**
 * 假日表单脚本：与 `holiday/index.vue` 中 `HolidayForm` 实例配合；
 * 父组件通过 `formRef.value?.validate()`、`getValues()`、`resetFields()` 完成弹窗提交流程。
 */
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Holiday, HolidayCreate } from '@/types/human-resource/attendance-leave/holiday'
import type { Dayjs } from 'dayjs'
import { RiCheckLine } from '@remixicon/vue'
import { themeColorMap } from '@/stores/setting'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

/**
 * 组件入参：由父级列表页传入当前编辑行或空对象（新增）。
 */
interface Props {
  /**
   * 当前假日数据（编辑时含 `holidayId` 等），与 `@/types/human-resource/attendance-leave/holiday` 中 `Holiday` 一致。
   */
  formData?: Partial<Holiday>
  /**
   * 提交中状态，由父级传入；本组件当前未据此禁用控件，预留与父级 loading 联动。
   */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * Ant Design Vue 表单实例，用于 `validate`、`resetFields`。
 */
const formRef = ref<FormInstance>()

/**
 * 当前激活的 Tab 键；仅 `basic`，与 `a-tab-pane` 的 key 一致。
 */
const activeTab = ref('basic')

/**
 * 估算的表单项数量，用于选择 `takt-form-content-rows-5` / `takt-form-content-rows-10` 容器密度（与字段行数大致对应）。
 */
const TOTAL_FIELDS = 9

/**
 * 表单外层纵向布局类名：字段较少时用 `rows-5`，达到阈值时用 `rows-10`。
 */
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

/**
 * 新增态默认值，类型与 `@/types/human-resource/attendance-leave/holiday` 中 `HolidayCreate` 一致（对应后端 `TaktHolidayCreateDto`）。
 *
 * @returns {HolidayCreate} 空表单可用的初始对象
 */
function createEmptyHolidayForm(): HolidayCreate {
  return {
    region: 'CN',
    holidayName: '',
    holidayType: 0,
    startDate: '',
    endDate: '',
    isWorkingDay: 0,
    holidayGreeting: '',
    holidayQuote: '',
    holidayTheme: '',
    remark: ''
  }
}

/** 表单模型：直接使用 `HolidayCreate`，不在本组件重复声明字段类型。 */
const formState = reactive<HolidayCreate>(createEmptyHolidayForm())

/**
 * 校验规则：`name` 与模板 `a-form-item` 的 `name` 一一对应，触发时机与占位文案走 i18n。
 */
const rules = computed<Record<string, Rule[]>>(() => ({
  region: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.holiday.region') }), trigger: 'blur' }
  ],
  holidayName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.holiday.holidayname') }), trigger: 'blur' }
  ],
  holidayType: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.holiday.holidaytype') }), trigger: 'change' }
  ],
  startDate: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.holiday.startdate') }), trigger: 'change' }
  ],
  endDate: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.holiday.enddate') }), trigger: 'change' }
  ],
  holidayTheme: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.holiday.holidaytheme') }), trigger: 'change' }
  ]
}))

/**
 * 将日期字段统一为 `YYYY-MM-DD` 字符串，供 `a-date-picker` 的 `value-format` 与接口一致。
 *
 * @param {string | Dayjs | undefined} v - 接口返回的 ISO 字符串、纯日期串或 Dayjs
 * @returns {string} 日期字符串，无效时为空串
 */
function toDateStr(v: string | Dayjs | undefined): string {
  if (!v) return ''
  if (typeof v === 'string') return v
  return v.format('YYYY-MM-DD')
}

/**
 * 同步父组件传入的 `formData`：有数据则填充编辑态，否则重置为新增默认值；每次切换后回到「基本信息」Tab。
 */
watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        region: newData.region ?? 'CN',
        holidayName: newData.holidayName ?? '',
        holidayType: newData.holidayType ?? 0,
        startDate: newData.startDate ? toDateStr(newData.startDate) : '',
        endDate: newData.endDate ? toDateStr(newData.endDate) : '',
        isWorkingDay: newData.isWorkingDay ?? 0,
        holidayGreeting: newData.holidayGreeting ?? '',
        holidayQuote: newData.holidayQuote ?? '',
        holidayTheme: newData.holidayTheme ?? '',
        remark: newData.remark ?? ''
      })
    } else {
      Object.assign(formState, createEmptyHolidayForm())
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

/**
 * 触发表单校验（执行 `a-form` 上已配置的 `rules`）。
 *
 * @returns {Promise<void>}
 */
const validate = async () => {
  await formRef.value?.validate()
}

/**
 * 组装提交载荷，字段与创建/更新 API 所需 `HolidayCreate` 一致；编辑时附带 `holidayId`。
 *
 * @returns {HolidayCreate & { holidayId?: string }} 提交体；含可选 `holidayId` 表示更新
 */
const getValues = (): HolidayCreate & { holidayId?: string } => {
  const payload: HolidayCreate & { holidayId?: string } = {
    region: formState.region ?? 'CN',
    holidayName: formState.holidayName ?? '',
    holidayType: formState.holidayType ?? 0,
    startDate: formState.startDate ?? '',
    endDate: formState.endDate ?? '',
    isWorkingDay: formState.isWorkingDay ?? 0,
    holidayGreeting: formState.holidayGreeting || undefined,
    holidayQuote: formState.holidayQuote || undefined,
    holidayTheme: formState.holidayTheme ?? '',
    remark: formState.remark || undefined
  }
  if (props.formData?.holidayId) {
    payload.holidayId = props.formData.holidayId
  }
  return payload
}

/**
 * 重置：调用 Ant Design 表单 `resetFields`，并将 `formState` 恢复为新增默认值、`activeTab` 回到 `basic`。
 *
 * @returns {void}
 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyHolidayForm())
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
/* 假日主题色块：与 themeColorMap 条目一一对应，选中态展示对勾 */
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
  }
}
</style>
