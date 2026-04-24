<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    layout="horizontal"
    label-align="right"
  >
    <a-form-item
      :label="t('routine.setting.form.settingKey')"
      name="settingKey"
    >
      <a-input
        v-model:value="formState.settingKey"
        :placeholder="t('routine.setting.form.placeholderSettingKey')"
        :disabled="!!formData?.settingId"
      />
    </a-form-item>

    <a-form-item
      :label="t('routine.setting.form.settingValue')"
      name="settingValue"
    >
      <a-input
        v-model:value="formState.settingValue"
        :placeholder="t('routine.setting.form.placeholderSettingValue')"
        allow-clear
      />
    </a-form-item>

    <a-form-item
      :label="t('routine.setting.form.settingName')"
      name="settingName"
    >
      <a-input
        v-model:value="formState.settingName"
        :placeholder="t('routine.setting.form.placeholderSettingName')"
        allow-clear
      />
    </a-form-item>

    <a-form-item
      :label="t('routine.setting.form.settingGroup')"
      name="settingGroup"
    >
      <TaktSelect
        v-model:value="formState.settingGroup"
        dict-type="sys_setting_group"
        :placeholder="t('routine.setting.form.placeholderSettingGroup')"
        allow-clear
      />
    </a-form-item>

    <a-form-item
      :label="t('routine.setting.form.isBuiltIn')"
      name="isBuiltIn"
    >
      <TaktSelect
        v-model:value="formState.isBuiltIn"
        dict-type="sys_yes_no"
        :placeholder="t('routine.setting.form.placeholderIsBuiltIn')"
      />
    </a-form-item>

    <a-form-item
      :label="t('routine.setting.form.isEncrypted')"
      name="isEncrypted"
    >
      <TaktSelect
        v-model:value="formState.isEncrypted"
        dict-type="sys_yes_no"
        :placeholder="t('routine.setting.form.placeholderIsEncrypted')"
      />
    </a-form-item>

    <a-form-item
      :label="t('routine.setting.form.orderNum')"
      name="orderNum"
    >
      <a-input-number
        v-model:value="formState.orderNum"
        :min="0"
        :placeholder="t('routine.setting.form.placeholderOrderNum')"
        style="width: 100%"
      />
    </a-form-item>

    <a-form-item
      :label="t('routine.setting.form.remark')"
      name="remark"
    >
      <a-textarea
        v-model:value="formState.remark"
        :placeholder="t('routine.setting.form.placeholderRemark')"
        :rows="3"
      />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import type { Settings, SettingsCreate, SettingsUpdate } from '@/types/routine/tasks/setting/settings'

/** 与输入控件绑定：可选文本用 string，避免 undefined 与 v-model 不兼容。 */
type SettingFormModel = {
  settingId?: string
  settingKey: string
  settingValue: string
  settingName: string
  settingGroup: string | number | undefined
  isBuiltIn: number
  isEncrypted: number
  orderNum: number
  remark: string
}

const props = withDefaults(
  defineProps<{
    formData?: Partial<Settings> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const { t } = useI18n()

const formRef = ref<FormInstance>()

const emptyModel = (): SettingFormModel => ({
  settingKey: '',
  settingValue: '',
  settingName: '',
  settingGroup: undefined,
  isBuiltIn: 1,
  isEncrypted: 1,
  orderNum: 0,
  remark: ''
})

const formState = ref<SettingFormModel>(emptyModel())

const rules = computed<Record<string, Rule[]>>(() => ({
  settingKey: [{ required: true, message: t('routine.setting.validation.settingKey') }]
}))

watch(
  () => props.formData,
  val => {
    if (val) {
      const next: SettingFormModel = {
        ...emptyModel(),
        settingKey: val.settingKey ?? '',
        settingValue: val.settingValue ?? '',
        settingName: val.settingName ?? '',
        settingGroup: val.settingGroup === undefined || val.settingGroup === null || val.settingGroup === ''
          ? undefined
          : val.settingGroup,
        isBuiltIn: val.isBuiltIn ?? 1,
        isEncrypted: val.isEncrypted ?? 1,
        orderNum: val.orderNum ?? 0,
        remark: val.remark ?? ''
      }
      if (val.settingId) {
        next.settingId = val.settingId
      }
      formState.value = next
    } else {
      formState.value = emptyModel()
    }
  },
  { immediate: true }
)

function optionalTrimmed(s: string): string | undefined {
  const x = s.trim()
  return x.length > 0 ? x : undefined
}

function optionalGroup(v: string | number | undefined): string | undefined {
  if (v === undefined || v === null || v === '') return undefined
  return typeof v === 'number' ? String(v) : v
}

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): SettingsCreate | SettingsUpdate => {
  const s = formState.value
  const settingValue = optionalTrimmed(s.settingValue)
  const settingName = optionalTrimmed(s.settingName)
  const settingGroup = optionalGroup(s.settingGroup)
  const remark = optionalTrimmed(s.remark)
  const base: SettingsCreate = {
    settingKey: s.settingKey,
    isBuiltIn: s.isBuiltIn ?? 1,
    isEncrypted: s.isEncrypted ?? 1,
    orderNum: s.orderNum ?? 0,
    ...(settingValue !== undefined ? { settingValue } : {}),
    ...(settingName !== undefined ? { settingName } : {}),
    ...(settingGroup !== undefined ? { settingGroup } : {}),
    ...(remark !== undefined ? { remark } : {})
  }
  if (s.settingId) {
    return { ...base, settingId: s.settingId }
  }
  return base
}

defineExpose({ validate, getValues })
</script>

<style scoped lang="less">
// 与项目其他表单一致
</style>
