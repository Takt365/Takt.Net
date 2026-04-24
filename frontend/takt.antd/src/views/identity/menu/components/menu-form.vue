<template>
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicinfo')"
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
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.name')"
                name="menuName"
              >
                <a-input
                  v-model:value="formState.menuName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.name') })"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.code')"
                name="menuCode"
              >
                <a-input
                  v-model:value="formState.menuCode"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.code') })"
                  show-count
                  :maxlength="200"
                  :disabled="!!formData?.menuId"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.menu.parentid')"
                name="parentId"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <TaktTreeSelect
                  v-model:value="formState.parentId"
                  api-url="/api/TaktMenus/tree-options"
                  :placeholder="t('identity.menu.page.placeholder.parentmenuhint')"
                  allow-clear
                  :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.path')"
                name="path"
              >
                <a-input
                  v-model:value="formState.path"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.path') })"
                  show-count
                  :maxlength="200"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.component')"
                name="component"
              >
                <a-input
                  v-model:value="formState.component"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.component') })"
                  show-count
                  :maxlength="200"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.icon')"
                name="menuIcon"
              >
                <a-input
                  v-model:value="formState.menuIcon"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.icon') })"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.ordernum')"
                name="orderNum"
              >
                <a-input-number
                  v-model:value="formState.orderNum"
                  :placeholder="t('common.form.placeholder.ordernumhint')"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.menu.type')"
                name="menuType"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <TaktSelect
                  v-model:value="formState.menuType"
                  dict-type="sys_menu_type"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.type') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.menu.status')"
                name="menuStatus"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <TaktSelect
                  v-model:value="formState.menuStatus"
                  dict-type="sys_normal_disable"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.status') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.menu.permission')"
                name="permission"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-input
                  v-model:value="formState.permission"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.permission') })"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.isvisible')"
                name="isVisible"
              >
                <TaktSelect
                  v-model:value="formState.isVisible"
                  dict-type="sys_yes_no"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.isvisible') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.iscache')"
                name="isCache"
              >
                <TaktSelect
                  v-model:value="formState.isCache"
                  dict-type="sys_yes_no"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.iscache') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.isexternal')"
                name="isExternal"
              >
                <TaktSelect
                  v-model:value="formState.isExternal"
                  dict-type="sys_yes_no"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.isexternal') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.linkurl')"
                name="linkUrl"
              >
                <a-input
                  v-model:value="formState.linkUrl"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.menu.linkurl') })"
                  show-count
                  :maxlength="500"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.menu.l10nkey')"
                name="menuL10nKey"
              >
                <a-input
                  v-model:value="formState.menuL10nKey"
                  :placeholder="t('identity.menu.page.placeholder.l10nhint')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('common.entity.remark')"
                name="remark"
              >
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
                  :rows="2"
                  show-count
                  :maxlength="500"
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
import type { Menu, MenuCreate } from '@/types/identity/menu'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

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
  menuStatus: 1,
  permission: '',
  isVisible: 1,
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
      menuStatus: newData.menuStatus ?? 1,
      permission: newData.permission ?? '',
      isVisible: newData.isVisible ?? 1,
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
      menuStatus: 1,
      permission: '',
      isVisible: 1,
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
  const parentId = p === '' || p === undefined ? 0 : (typeof p === 'string' ? (p === '' ? 0 : Number(p)) : p)
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
    menuStatus: formState.menuStatus ?? 1,
    permission: formState.permission || undefined,
    isVisible: formState.isVisible ?? 1,
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
    menuStatus: 1,
    permission: '',
    isVisible: 1,
    isCache: 1,
    isExternal: 1,
    linkUrl: '',
    remark: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
