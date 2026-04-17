<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 4 }"
    :wrapper-col="{ span: 20 }"
    layout="horizontal"
  >
    <a-form-item
      label="设置键"
      name="settingKey"
    >
      <a-input
        v-model:value="formState.settingKey"
        placeholder="请输入设置键（唯一）"
        :disabled="!!formData?.settingId"
      />
    </a-form-item>

    <a-form-item
      label="设置值"
      name="settingValue"
    >
      <a-input
        v-model:value="formState.settingValue"
        placeholder="请输入设置值"
        allow-clear
      />
    </a-form-item>

    <a-form-item
      label="设置名称"
      name="settingName"
    >
      <a-input
        v-model:value="formState.settingName"
        placeholder="请输入设置名称（描述）"
        allow-clear
      />
    </a-form-item>

    <a-form-item
      label="设置分组"
      name="settingGroup"
    >
      <TaktSelect
        v-model:value="formState.settingGroup"
        dict-type="sys_setting_group"
        placeholder="请选择设置分组"
        allow-clear
      />
    </a-form-item>

    <a-form-item
      label="是否内置"
      name="isBuiltIn"
    >
      <TaktSelect
        v-model:value="formState.isBuiltIn"
        dict-type="sys_yes_no"
        placeholder="请选择是否内置"
      />
    </a-form-item>

    <a-form-item
      label="是否加密"
      name="isEncrypted"
    >
      <TaktSelect
        v-model:value="formState.isEncrypted"
        dict-type="sys_yes_no"
        placeholder="请选择是否加密"
      />
    </a-form-item>

    <a-form-item
      label="排序号"
      name="orderNum"
    >
      <a-input-number
        v-model:value="formState.orderNum"
        :min="0"
        placeholder="请输入排序号（越小越靠前）"
        style="width: 100%"
      />
    </a-form-item>

    <a-form-item
      label="备注"
      name="remark"
    >
      <a-textarea
        v-model:value="formState.remark"
        placeholder="请输入备注"
        :rows="3"
      />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Setting, SettingCreate, SettingUpdate } from '@/types/routine/tasks/setting'

const props = withDefaults(
  defineProps<{
    formData?: Partial<Setting> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const formRef = ref<FormInstance>()
const formState = ref<SettingCreate & { settingId?: string }>({
  settingKey: '',
  settingValue: undefined,
  settingName: undefined,
  settingGroup: undefined,
  isBuiltIn: 1,
  isEncrypted: 1,
  orderNum: 0,
  remark: undefined
})

const rules: Record<string, Rule[]> = {
  settingKey: [{ required: true, message: '请输入设置键' }]
}

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = {
        settingKey: val.settingKey ?? '',
        settingValue: val.settingValue ?? undefined,
        settingName: val.settingName ?? undefined,
        settingGroup: val.settingGroup ?? undefined,
        isBuiltIn: val.isBuiltIn ?? 1,
        isEncrypted: val.isEncrypted ?? 1,
        orderNum: val.orderNum ?? 0,
        remark: val.remark ?? undefined
      }
      if (val.settingId) formState.value.settingId = val.settingId
    } else {
      formState.value = {
        settingKey: '',
        settingValue: undefined,
        settingName: undefined,
        settingGroup: undefined,
        isBuiltIn: 1,
        isEncrypted: 1,
        orderNum: 0,
        remark: undefined
      }
    }
  },
  { immediate: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): SettingCreate | SettingUpdate => {
  const base: SettingCreate = {
    settingKey: formState.value.settingKey,
    settingValue: formState.value.settingValue || undefined,
    settingName: formState.value.settingName || undefined,
    settingGroup: formState.value.settingGroup || undefined,
    isBuiltIn: formState.value.isBuiltIn ?? 1,
    isEncrypted: formState.value.isEncrypted ?? 1,
    orderNum: formState.value.orderNum ?? 0,
    remark: formState.value.remark || undefined
  }
  if (formState.value.settingId) {
    return { ...base, settingId: formState.value.settingId } as SettingUpdate
  }
  return base
}

defineExpose({ validate, getValues })
</script>

<style scoped lang="less">
// 与项目其他表单一致
</style>
