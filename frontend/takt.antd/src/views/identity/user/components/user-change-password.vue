<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：user-change-password.vue -->
<!-- 功能描述：修改密码弹窗内嵌表单。由 user/index.vue 引用；defineExpose 提供 validate、getValues、resetFields；表单模型为 `@/types/identity/user` 的 UserChangePasswordFormModel，getValues 映射为 UserChangePwd；旧/新/确认密码校验含 isValidPassword；loading 结束时 watch 重置表单。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 旧密码、新密码、确认密码；用户名为只读展示 -->
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 6 }"
    :wrapper-col="{ span: 18 }"
    layout="horizontal"
  >
    <a-form-item :label="t('entity.user.username')">
      <a-input
        :value="userName"
        disabled
      />
    </a-form-item>
    <a-form-item :label="t('identity.user.password.old.label')" name="oldPassword">
      <a-input-password
        v-model:value="formState.oldPassword"
        :placeholder="t('common.form.placeholder.required', { field: t('identity.user.password.old.label') })"
        :disabled="loading"
        show-count
        :maxlength="20"
      />
    </a-form-item>
    <a-form-item :label="t('identity.user.password.new.label')" name="newPassword">
      <a-input-password
        v-model:value="formState.newPassword"
        :placeholder="t('common.form.placeholder.required', { field: t('identity.user.password.new.label') })"
        :disabled="loading"
        show-count
        :maxlength="20"
      />
    </a-form-item>
    <a-form-item :label="t('identity.user.password.confirm.label')" name="confirmPassword">
      <a-input-password
        v-model:value="formState.confirmPassword"
        :placeholder="t('common.form.placeholder.requiredAgain', { field: t('identity.user.password.new.label') })"
        :disabled="loading"
        show-count
        :maxlength="20"
      />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
/**
 * 修改密码表单脚本：与 `user/index.vue` 弹窗 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成提交流程。
 */
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { UserChangePwd, UserChangePasswordFormModel } from '@/types/identity/user'
import { isValidPassword } from '@/utils/regex'

/**
 * 组件入参：嵌入用户管理页「修改密码」弹窗。
 */
interface Props {
  /** 当前用户登录名（只读展示） */
  userName: string
  /** 提交请求进行中时禁用输入 */
  loading?: boolean
}

const props = defineProps<Props>()

const emit = defineEmits<{
  submit: [values: UserChangePwd]
}>()

const { t } = useI18n()

/** Ant Design Vue 表单实例 */
const formRef = ref<FormInstance>()

function createEmptyChangePasswordForm(): UserChangePasswordFormModel {
  return {
    oldPassword: '',
    newPassword: '',
    confirmPassword: ''
  }
}

/** a-form 绑定模型（含确认密码，仅前端） */
const formState = reactive<UserChangePasswordFormModel>(createEmptyChangePasswordForm())

/** 确认密码：必填且须与 newPassword 一致 */
const validateConfirmPassword = (_rule: Rule, value: string) => {
  if (!value) {
    return Promise.reject(t('common.form.placeholder.requiredAgain', { field: t('identity.user.password.new.label') }))
  }
  if (value !== formState.newPassword) {
    return Promise.reject(t('identity.user.password.confirm.validation.mismatch'))
  }
  return Promise.resolve()
}

/** 校验规则，name 与 a-form-item 一致 */
const rules: Record<string, Rule[]> = {
  // oldPassword：必填
  oldPassword: [
    { required: true, message: t('common.form.placeholder.required', { field: t('identity.user.password.old.label') }), trigger: 'blur' }
  ],
  // newPassword：必填 + isValidPassword
  newPassword: [
    { required: true, message: t('common.form.placeholder.required', { field: t('identity.user.password.new.label') }), trigger: 'blur' },
    {
      validator: (_rule: Rule, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        if (!isValidPassword(value)) {
          return Promise.reject(t('identity.user.password.new.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  // confirmPassword：必填 + 与 newPassword 一致
  confirmPassword: [
    { required: true, message: t('common.form.placeholder.requiredAgain', { field: t('identity.user.password.new.label') }), trigger: 'blur' },
    { validator: validateConfirmPassword, trigger: 'blur' }
  ]
}

/** 执行 a-form 校验；父组件提交前 await */
const validate = async (): Promise<void> => {
  if (!formRef.value) {
    return Promise.reject(new Error(t('common.form.validation.notFound', { target: t('identity.user.fields.formRef.label') })))
  }
  await formRef.value.validate()
}

/** 返回后端 `UserChangePwd`（不含 confirmPassword） */
const getValues = (): UserChangePwd => {
  return {
    oldPassword: formState.oldPassword,
    newPassword: formState.newPassword
  }
}

/** 清空校验与本地字段 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyChangePasswordForm())
}

/** 监听 loading：由 true→false 时视为一次提交结束，重置表单避免残留 */
watch(
  () => props.loading,
  (newVal, oldVal) => {
    if (oldVal && !newVal) {
      resetFields()
    }
  }
)

/** 暴露给 `user/index.vue` 弹窗 ref：`validate`、`getValues`、`resetFields` */
defineExpose({
  validate,
  getValues,
  resetFields
})
</script>

<style scoped lang="less">
/* 本组件无局部样式；占位与项目其它表单子组件一致 */
</style>
