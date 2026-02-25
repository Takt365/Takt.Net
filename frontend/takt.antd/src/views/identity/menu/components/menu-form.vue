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
              <a-form-item :label="t('entity.menu.name')" name="menuName">
                <a-input v-model:value="formState.menuName" :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.name') })" show-count :maxlength="50" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.code')" name="menuCode">
                <a-input v-model:value="formState.menuCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.code') })" show-count :maxlength="50" :disabled="!!formData?.menuId" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.menu.parentid')" name="parentId" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <TaktTreeSelect
                  v-model:value="formState.parentId"
                  api-url="/api/TaktMenus/tree-options"
                  :placeholder="t('identity.menu.placeholder.parentMenuHint')"
                  allow-clear
                  :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.path')" name="path">
                <a-input v-model:value="formState.path" :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.path') })" show-count :maxlength="200" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.component')" name="component">
                <a-input v-model:value="formState.component" :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.component') })" show-count :maxlength="200" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.icon')" name="menuIcon">
                <a-input v-model:value="formState.menuIcon" :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.icon') })" show-count :maxlength="100" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.ordernum')" name="orderNum">
                <a-input-number v-model:value="formState.orderNum" :placeholder="t('common.form.placeholder.orderNumHint')" :min="0" style="width: 100%" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.menu.type')" name="menuType" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-select v-model:value="formState.menuType" :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.type') })" :options="menuTypeOptions" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.menu.status')" name="menuStatus" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-select v-model:value="formState.menuStatus" :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.status') })" :options="menuStatusOptions" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.menu.permission')" name="permission" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-input v-model:value="formState.permission" :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.permission') })" show-count :maxlength="100" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.isVisible')" name="isVisible">
                <a-radio-group v-model:value="formState.isVisible">
                  <a-radio :value="0">{{ t('common.button.yes') }}</a-radio>
                  <a-radio :value="1">{{ t('common.button.no') }}</a-radio>
                </a-radio-group>
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.isCache')" name="isCache">
                <a-radio-group v-model:value="formState.isCache">
                  <a-radio :value="0">{{ t('common.button.yes') }}</a-radio>
                  <a-radio :value="1">{{ t('common.button.no') }}</a-radio>
                </a-radio-group>
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.isExternal')" name="isExternal">
                <a-radio-group v-model:value="formState.isExternal">
                  <a-radio :value="0">{{ t('common.button.yes') }}</a-radio>
                  <a-radio :value="1">{{ t('common.button.no') }}</a-radio>
                </a-radio-group>
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.linkUrl')" name="linkUrl">
                <a-input v-model:value="formState.linkUrl" :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.linkUrl') })" show-count :maxlength="500" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.menu.l10nkey')" name="menuL10nKey">
                <a-input v-model:value="formState.menuL10nKey" :placeholder="t('identity.menu.placeholder.l10nHint')" show-count :maxlength="100" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('common.entity.remark')" name="remark">
                <a-textarea v-model:value="formState.remark" :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })" :rows="2" show-count :maxlength="500" />
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
import type { Menu, MenuCreate } from '@/types/identity/menu'

const { t } = useI18n()

const menuTypeOptions = computed(() => [
  { label: t('identity.menu.menuType.dir'), value: 0 },
  { label: t('identity.menu.menuType.menu'), value: 1 }
])
const menuStatusOptions = computed(() => [
  { label: t('identity.menu.menuStatus.enable'), value: 0 },
  { label: t('identity.menu.menuStatus.disable'), value: 1 },
  { label: t('identity.menu.menuStatus.lock'), value: 3 }
])

interface Props {
  formData?: Partial<Menu>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const activeTab = ref('basic')
/** 表单总字段数（用于内容区高度：>=30 为 10 行，<30 为 5 行） */
const TOTAL_FIELDS = 16
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  menuName?: string
  menuCode?: string
  menuL10nKey?: string
  parentId?: string | number
  path?: string
  component?: string
  menuIcon?: string
  orderNum?: number
  menuType?: number
  permission?: string
  menuStatus?: number
  isVisible?: number
  isCache?: number
  isExternal?: number
  linkUrl?: string
  remark?: string
}

const formState = reactive<FormState>({
  menuName: '',
  menuCode: '',
  menuL10nKey: '',
  parentId: 0,
  path: '',
  component: '',
  menuIcon: '',
  orderNum: 0,
  menuType: 0,
  menuStatus: 0,
  permission: '',
  isVisible: 0,
  isCache: 1,
  isExternal: 1,
  linkUrl: '',
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  menuName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.menu.name') }), trigger: 'blur' }],
  menuCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.menu.code') }), trigger: 'blur' }],
  menuType: [{ required: true, message: t('common.form.placeholder.select', { field: t('entity.menu.type') }), trigger: 'change' }]
}))

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      menuName: newData.menuName ?? '',
      menuCode: newData.menuCode ?? '',
      menuL10nKey: newData.menuL10nKey ?? '',
      parentId: newData.parentId !== undefined && newData.parentId !== null ? String(newData.parentId) : 0,
      path: newData.path ?? '',
      component: newData.component ?? '',
      menuIcon: newData.menuIcon ?? '',
      orderNum: newData.orderNum ?? 0,
      menuType: newData.menuType ?? 0,
      menuStatus: newData.menuStatus ?? 0,
      permission: newData.permission ?? '',
      isVisible: newData.isVisible ?? 0,
      isCache: newData.isCache ?? 1,
      isExternal: newData.isExternal ?? 1,
      linkUrl: newData.linkUrl ?? '',
      remark: newData.remark ?? ''
    })
  } else {
    Object.assign(formState, {
      menuName: '',
      menuCode: '',
      menuL10nKey: '',
      parentId: 0,
      path: '',
      component: '',
      menuIcon: '',
      orderNum: 0,
      menuType: 0,
      menuStatus: 0,
      permission: '',
      isVisible: 0,
      isCache: 1,
      isExternal: 1,
      linkUrl: '',
      remark: ''
    })
  }
  activeTab.value = 'basic'
}, { immediate: true, deep: true })

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): MenuCreate => {
  const p = formState.parentId
  let parentId: number
  if (p === '' || p === undefined) {
    parentId = 0
  } else if (typeof p === 'string') {
    parentId = p === '' ? 0 : Number(p)
  } else {
    parentId = p
  }
  return {
    menuName: formState.menuName ?? '',
    menuCode: formState.menuCode ?? '',
    menuL10nKey: formState.menuL10nKey || undefined,
    parentId: Number(parentId) || 0,
    path: formState.path || undefined,
    component: formState.component || undefined,
    menuIcon: formState.menuIcon || undefined,
    orderNum: formState.orderNum ?? 0,
    menuType: formState.menuType ?? 0,
    menuStatus: formState.menuStatus ?? 0,
    permission: formState.permission || undefined,
    isVisible: formState.isVisible ?? 0,
    isCache: formState.isCache ?? 1,
    isExternal: formState.isExternal ?? 1,
    linkUrl: formState.linkUrl || undefined,
    remark: formState.remark || undefined
  }
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    menuName: '',
    menuCode: '',
    menuL10nKey: '',
    parentId: 0,
    path: '',
    component: '',
    menuIcon: '',
    orderNum: 0,
    menuType: 0,
    menuStatus: 0,
    permission: '',
    isVisible: 0,
    isCache: 1,
    isExternal: 1,
    linkUrl: '',
    remark: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
