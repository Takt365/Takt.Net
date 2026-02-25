<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/settings/components -->
<!-- 文件名称：setting-form.vue -->
<!-- 功能描述：设置表单组件，包含基本信息 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:activeKey="activeTab">
    <a-tab-pane key="basic" :tab="t('common.form.tabs.basicInfo')">
      <div :class="formContentClass">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 8 }"
          :wrapper-col="{ span: 16 }"
          layout="horizontal"
          label-align="right"
        >
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.settings.key')" name="settingKey">
                <a-input
                  v-model:value="formState.settingKey"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.settings.key') })"
                  :disabled="!!formData?.settingId"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.settings.value')" name="settingValue">
                <a-input
                  v-model:value="formState.settingValue"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.settings.value') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.settings.name')" name="settingName">
                <a-input
                  v-model:value="formState.settingName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.settings.name') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.settings.group')" name="settingGroup">
                <TaktSelect
                  v-model:value="formState.settingGroup"
                  dict-type="sys_setting_group"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.settings.group') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.settings.isbuiltin')" name="isBuiltIn">
                <TaktSelect
                  v-model:value="formState.isBuiltIn"
                  dict-type="sys_yes_no"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.settings.isbuiltin') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.settings.isencrypted')" name="isEncrypted">
                <TaktSelect
                  v-model:value="formState.isEncrypted"
                  dict-type="sys_yes_no"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.settings.isencrypted') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.settings.ordernum')" name="orderNum">
                <a-input-number
                  v-model:value="formState.orderNum"
                  :min="0"
                  :placeholder="t('common.form.placeholder.orderNumHint')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('common.entity.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
                  :rows="3"
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
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Setting, SettingCreate, SettingUpdate } from '@/types/routine/setting'

const props = withDefaults(
  defineProps<{
    formData?: Partial<Setting> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const { t } = useI18n()
const formRef = ref<FormInstance>()
const activeTab = ref('basic')
const TOTAL_FIELDS = 9
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

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

const rules = computed<Record<string, Rule[]>>(() => ({
  settingKey: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.settings.key') }), trigger: 'blur' }]
}))

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
