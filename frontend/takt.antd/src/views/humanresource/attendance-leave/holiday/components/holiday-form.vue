<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/humanresource/attendanceleave/holiday/components -->
<!-- 文件名称：holiday-form.vue -->
<!-- 功能描述：假日表单组件，包含基本资料（以用户实体为标准，参照 post-form） -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:activeKey="activeTab">
    <a-tab-pane key="basic" :tab="t('common.form.tabs.basicInfo')">
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
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.holiday.region')" name="region">
                <a-input
                  v-model:value="formState.region"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.holiday.region') })"
                  show-count
                  :maxlength="10"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.holiday.holidayname')" name="holidayName">
                <a-input
                  v-model:value="formState.holidayName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.holiday.holidayname') })"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.holiday.holidaytype')" name="holidayType">
                <a-select
                  v-model:value="formState.holidayType"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.holidaytype') })"
                  :options="holidayTypeOptions"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.holiday.isworkingday')" name="isWorkingDay">
                <a-select
                  v-model:value="formState.isWorkingDay"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.isworkingday') })"
                  :options="isWorkingDayOptions"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.holiday.startdate')" name="startDate">
                <a-date-picker
                  v-model:value="formState.startDate"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.startdate') })"
                  value-format="YYYY-MM-DD"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.holiday.enddate')" name="endDate">
                <a-date-picker
                  v-model:value="formState.endDate"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.holiday.enddate') })"
                  value-format="YYYY-MM-DD"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.holiday.holidaygreeting')" name="holidayGreeting" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
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
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.holiday.holidayquote')" name="holidayQuote" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
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
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.holiday.holidaytheme')" name="holidayTheme" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
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
                      <RiCheckLine v-if="formState.holidayTheme === key" class="color-item-check" />
                    </div>
                  </a-tooltip>
                </div>
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('common.entity.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
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
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Holiday, HolidayCreate } from '@/types/humanresource/attendance-leave/holiday'
import type { Dayjs } from 'dayjs'
import { RiCheckLine } from '@remixicon/vue'
import { themeColorMap } from '@/stores/setting'

const { t } = useI18n()

const holidayTypeOptions = [
  { label: '法定', value: 0 },
  { label: '调休', value: 1 },
  { label: '公司', value: 2 }
]
const isWorkingDayOptions = [
  { label: '非工作日', value: 0 },
  { label: '工作日', value: 1 },
  { label: '半天等', value: 2 }
]

interface Props {
  formData?: Partial<Holiday>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const activeTab = ref('basic')
const TOTAL_FIELDS = 9
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  region?: string
  holidayName?: string
  holidayType?: number
  startDate?: string
  endDate?: string
  isWorkingDay?: number
  holidayGreeting?: string
  holidayQuote?: string
  holidayTheme?: string
  remark?: string
}

const formState = reactive<FormState>({
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
})

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

function toDateStr(v: string | Dayjs | undefined): string {
  if (!v) return ''
  if (typeof v === 'string') return v
  return v.format('YYYY-MM-DD')
}

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        region: newData.region ?? 'CN',
        holidayName: newData.holidayName ?? '',
        holidayType: newData.holidayType ?? 0,
        startDate: newData.startDate ? toDateStr(newData.startDate as string) : '',
        endDate: newData.endDate ? toDateStr(newData.endDate as string) : '',
        isWorkingDay: newData.isWorkingDay ?? 0,
        holidayGreeting: newData.holidayGreeting ?? '',
        holidayQuote: newData.holidayQuote ?? '',
        holidayTheme: newData.holidayTheme ?? '',
        remark: newData.remark ?? ''
      })
    } else {
      Object.assign(formState, {
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
      })
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

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

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
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
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
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
