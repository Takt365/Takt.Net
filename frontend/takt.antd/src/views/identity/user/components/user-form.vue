<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：user-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：用户表单组件，包含基本资料、分配权限和头像设置三个标签页 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:activeKey="activeTab">
    <!-- 基本资料 -->
    <a-tab-pane key="basic" :tab="t('identity.user.tabs.basicInfo')">
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
            <a-form-item :label="t('entity.user.name')" name="userName">
              <a-input
                v-model:value="formState.userName"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.user.name') })"
                :disabled="!!formData?.userId"
                show-count
                :maxlength="20"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.user.realname')" name="realName">
              <a-input
                v-model:value="formState.realName"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.user.realname') })"
                show-count
                :maxlength="50"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item :label="t('entity.user.fullname')" name="fullName">
              <a-input
                v-model:value="formState.fullName"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.user.fullname') })"
                show-count
                :maxlength="100"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.user.nickname')" name="nickName">
              <a-input
                v-model:value="formState.nickName"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.user.nickname') })"
                show-count
                :maxlength="50"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.user.englishname')" name="englishName" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <a-input
                v-model:value="formState.englishName"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.user.englishname') })"
                show-count
                :maxlength="100"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.user.gender')" name="gender" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <TaktSelect
                v-model:value="formState.gender"
                dict-type="sys_user_gender"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.user.gender') })"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.user.type')" name="userType" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <TaktSelect
                v-model:value="formState.userType"
                dict-type="sys_user_type"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.user.type') })"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item :label="t('entity.user.email')" name="userEmail">
              <a-input
                v-model:value="formState.userEmail"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.user.email') })"
                show-count
                :maxlength="100"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.user.phone')" name="userPhone">
              <a-input
                v-model:value="formState.userPhone"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.user.phone') })"
                show-count
                :maxlength="20"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24" v-if="!formData?.userId">
            <a-form-item :label="t('identity.user.fields.password.label')" name="password" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <a-input-password
                v-model:value="formState.password"
                :placeholder="t('common.form.placeholder.required', { field: t('identity.user.fields.password.label') })"
                show-count
                :maxlength="20"
              />
            </a-form-item>
          </a-col>
          <a-col :span="24">
            <a-form-item :label="t('entity.user.status')" name="userStatus" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <TaktSelect
                v-model:value="formState.userStatus"
                dict-type="sys_normal_disable"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.user.status') })"
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
                :rows="4"
                show-count
                :maxlength="500"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
      </div>
    </a-tab-pane>
    
    <!-- 权限分配 -->
    <a-tab-pane key="permission" :tab="t('common.action.tabTargetAllocation', { target: t('identity.user.tabs.permission') })">
      <div :class="formContentClass">
      <a-form
        :model="permissionState"
        :label-col="{ span: 8 }"
        :wrapper-col="{ span: 16 }"
        layout="horizontal"
        label-align="right"
      >
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.role._self')" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <TaktSelect
                v-model:value="permissionState.roleIds"
                :options="roleOptions"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.role._self') })"
                multiple
                :max-tag-count="'responsive'"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.tenant._self')" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <TaktSelect
                v-model:value="permissionState.tenantIds"
                :options="tenantOptions"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.tenant._self') })"
                multiple
                :max-tag-count="'responsive'"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.dept._self')" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <DeptTreeTransfer
                v-model:model-value="permissionState.deptIds"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.post._self')" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <TaktSelect
                v-model:value="permissionState.postIds"
                :options="postOptions"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.post._self') })"
                multiple
                :max-tag-count="'responsive'"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
      </div>
    </a-tab-pane>
    
    <!-- 头像设置 -->
    <a-tab-pane key="avatar" :tab="t('identity.user.tabs.avatar')">
      <div :class="formContentClass">
      <a-form
        :model="formState"
        :label-col="{ span: 8 }"
        :wrapper-col="{ span: 16 }"
        layout="horizontal"
        label-align="right"
      >
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item :label="t('entity.user.avatar')" name="avatar" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
              <TaktUploadAvatar
                v-model:modelValue="formState.avatar"
                :avatarSize="120"
                :cropSize="200"
                :maxSize="2"
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
import { reactive, watch, ref, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import { message } from 'ant-design-vue'
import type { User } from '@/types/identity/user'
import type { TaktSelectOption } from '@/types/common'
import { getOptions as getRoleOptions } from '@/api/identity/role'
import { getOptions as getPostOptions } from '@/api/humanresource/organization/post'
import { getOptions as getTenantOptions } from '@/api/identity/tenant'
import DeptTreeTransfer from './dept-tree-transfer.vue'
import { logger } from '@/utils/logger'
import { 
  isValidUsername, 
  isValidRealName, 
  isValidFullName, 
  isValidNickName, 
  isValidEnglishName,
  isValidEmail, 
  isValidPhone, 
  isValidPassword,
  toPascalCase
} from '@/utils/regex'

const { t } = useI18n()
const activeTab = ref('basic')
/** 表单总字段数（基本+权限+头像，用于内容区高度：>=30 为 10 行，<30 为 5 行） */
const TOTAL_FIELDS = 19
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface Props {
  formData?: Partial<User>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const emit = defineEmits<{
  'submit': [values: any]
  'cancel': []
}>()

const formRef = ref()

interface FormState {
  userName?: string
  realName?: string
  fullName?: string
  nickName?: string
  englishName?: string
  avatar?: string
  gender?: number
  userType?: number
  userEmail?: string
  userPhone?: string
  password?: string
  userStatus?: number
  remark?: string
}

const formState = reactive<FormState>({
  userName: '',
  realName: '',
  fullName: '',
  nickName: '',
  englishName: '',
  avatar: '',
  gender: 0,
  userType: 0,
  userEmail: '',
  userPhone: '',
  password: '',
  userStatus: 0,
  remark: ''
})


// 权限分配状态
interface PermissionState {
  roleIds?: string[]
  deptIds?: string[]
  postIds?: string[]
  tenantIds?: string[]
}

const permissionState = reactive<PermissionState>({
  roleIds: [],
  deptIds: [],
  postIds: [],
  tenantIds: []
})

// 业务选项数据
const roleOptions = ref<TaktSelectOption[]>([])
const postOptions = ref<TaktSelectOption[]>([])
const tenantOptions = ref<TaktSelectOption[]>([])

// 加载业务选项数据
const loadBusinessOptions = async () => {
  try {
    const [roles, posts, tenants] = await Promise.all([
      getRoleOptions(),
      getPostOptions(),
      getTenantOptions()
    ])
    roleOptions.value = roles
    postOptions.value = posts
    tenantOptions.value = tenants
  } catch (error: any) {
    // 记录错误日志
    const optionsFailText = t('common.msg.loadOptionsFail', { target: t('entity.user._self') })
    logger.error('[User Form] 加载业务选项数据失败:', {
      error,
      message: error?.message || optionsFailText,
      stack: error?.stack
    })
    
    // 显示用户友好的错误提示
    const errorMessage = error?.response?.data?.message || error?.message || optionsFailText
    message.error({
      content: errorMessage,
      duration: 5
    })
  }
}

// 组件挂载时加载业务选项
onMounted(() => {
  loadBusinessOptions()
})

const rules: Record<string, Rule[]> = {
  userName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.user.name') }), trigger: 'blur' },
    { 
      validator: (_rule: any, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        if (!isValidUsername(value)) {
          return Promise.reject(t('identity.user.fields.userName.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  realName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.user.realname') }), trigger: 'blur' },
    {
      validator: (_rule: any, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        if (!isValidRealName(value)) {
          return Promise.reject(t('identity.user.fields.realName.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  fullName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.user.fullname') }), trigger: 'blur' },
    {
      validator: (_rule: any, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        // 如果是英文，自动转换为帕斯卡命名
        if (!/[\u4e00-\u9fa5]/.test(value)) {
          const pascalValue = toPascalCase(value)
          if (pascalValue !== value) {
            formState.fullName = pascalValue
            return Promise.resolve()
          }
        }
        if (!isValidFullName(value)) {
          return Promise.reject(t('identity.user.fields.fullName.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  nickName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.user.nickname') }), trigger: 'blur' },
    {
      validator: (_rule: any, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        // 如果是英文，自动转换为帕斯卡命名
        if (!/[\u4e00-\u9fa5]/.test(value)) {
          const pascalValue = toPascalCase(value)
          if (pascalValue !== value) {
            formState.nickName = pascalValue
            return Promise.resolve()
          }
        }
        if (!isValidNickName(value)) {
          return Promise.reject(t('identity.user.fields.nickName.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  englishName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.user.englishname') }), trigger: 'blur' },
    {
      validator: (_rule: any, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        // 自动转换为帕斯卡命名
        const pascalValue = toPascalCase(value)
        if (pascalValue !== value) {
          formState.englishName = pascalValue
          return Promise.resolve()
        }
        if (!isValidEnglishName(value)) {
          return Promise.reject(t('identity.user.fields.englishName.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  userEmail: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.user.email') }), trigger: 'blur' },
    {
      validator: (_rule: any, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        if (!isValidEmail(value)) {
          return Promise.reject(t('common.form.validation.enterValid', { field: t('entity.user.email') }))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  userPhone: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.user.phone') }), trigger: 'blur' },
    {
      validator: (_rule: any, value: string) => {
        if (!value) {
          return Promise.resolve()
        }
        if (!isValidPhone(value)) {
          return Promise.reject(t('common.form.validation.enterValid', { field: t('entity.user.phone') }))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  password: [
    { required: !props.formData?.userId, message: t('common.form.placeholder.required', { field: t('identity.user.fields.password.label') }), trigger: 'blur' },
    {
      validator: (_rule: any, value: string) => {
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
  gender: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.user.gender') }), trigger: 'change' }
  ],
  userType: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.user.type') }), trigger: 'change' }
  ],
  userStatus: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.user.status') }), trigger: 'change' }
  ]
}


// 监听 formData 变化
watch(() => props.formData, (newData) => {
  if (newData) {
    Object.assign(formState, {
      userName: newData.userName || '',
      realName: newData.realName || '',
      fullName: newData.fullName || '',
      nickName: newData.nickName || '',
      englishName: newData.englishName || '',
      avatar: newData.avatar || '',
      // 性别、用户类型、用户状态确保有默认值（0 表示默认值：未知性别、普通用户、启用状态）
      gender: newData.gender !== undefined && newData.gender !== null ? newData.gender : 0,
      userType: newData.userType !== undefined && newData.userType !== null ? newData.userType : 0,
      userEmail: newData.userEmail || '',
      userPhone: newData.userPhone || '',
      password: '',
      userStatus: newData.userStatus !== undefined && newData.userStatus !== null ? newData.userStatus : 0,
      remark: newData.remark || ''
    })
    
    // 同步权限分配状态（确保是字符串数组）
    permissionState.roleIds = newData.roleIds?.map(id => String(id)) || []
    permissionState.deptIds = newData.deptIds?.map(id => String(id)) || []
    permissionState.postIds = newData.postIds?.map(id => String(id)) || []
    permissionState.tenantIds = newData.tenantIds?.map(id => String(id)) || []
  } else {
    // 如果没有 formData，确保使用默认值
    Object.assign(formState, {
      userName: '',
      realName: '',
      fullName: '',
      nickName: '',
      englishName: '',
      avatar: '',
      gender: 0,
      userType: 0,
      userEmail: '',
      userPhone: '',
      password: '',
      userStatus: 0,
      remark: ''
    })
    
    // 重置权限分配状态
    permissionState.roleIds = []
    permissionState.deptIds = []
    permissionState.postIds = []
    permissionState.tenantIds = []
  }
}, { immediate: true, deep: true })

// 监听用户名变化，自动填充全名、昵称、英文名称（转换为帕斯卡命名）
watch(() => formState.userName, (newUserName) => {
  // 只在新增用户时自动填充（编辑时不应该自动填充）
  if (!props.formData?.userId && newUserName && newUserName.length > 0) {
    const pascalCaseName = toPascalCase(newUserName)
    // 直接填充到全名、昵称、英文名称
    formState.fullName = pascalCaseName
    formState.nickName = pascalCaseName
    formState.englishName = pascalCaseName
  }
})

// 验证表单
const validate = async () => {
  await formRef.value?.validate()
}

// 获取表单值
const getValues = () => {
  return { 
    ...formState,
    roleIds: permissionState.roleIds,
    deptIds: permissionState.deptIds,
    postIds: permissionState.postIds,
    tenantIds: permissionState.tenantIds
  }
}

// 重置表单
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    userName: '',
    realName: '',
    fullName: '',
    nickName: '',
    englishName: '',
    avatar: '',
    gender: 0,
    userType: 0,
    userEmail: '',
    userPhone: '',
    password: '',
    userStatus: 0,
    remark: ''
  })
  Object.assign(permissionState, {
    roleIds: [],
    deptIds: [],
    postIds: [],
    tenantIds: []
  })
  activeTab.value = 'basic'
}

// 暴露方法
defineExpose({
  validate,
  getValues,
  resetFields
})
</script>

<style scoped lang="less">
// 表单样式
:deep(.ant-tabs-content-holder) {
  min-height: 50vh;
}

:deep(.ant-tabs-tabpane) {
  min-height: 50vh;
}
</style>
