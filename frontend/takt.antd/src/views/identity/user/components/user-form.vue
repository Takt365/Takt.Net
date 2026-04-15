<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：user-form.vue -->
<!-- 功能描述：用户维护弹窗内嵌表单。由 user/index.vue 引用；defineExpose 提供 validate、getValues、resetFields、setServerValidationErrors。员工/用户信息/权限多标签；TaktSelect 字典 sys_user_type、sys_normal_disable；模型为 `@/types/identity/user` 的 UserFormModel、UserFormPermissionModel；新增密码由父组件映射为 UserCreate.passwordHash。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 根：a-form 包裹 a-tabs；formRef 供父级 validate、resetFields -->
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    layout="horizontal"
    label-align="right"
  >
    <a-tabs v-model:activeKey="activeTab">
      <!-- 标签1：员工信息（编辑=已绑定员工只读+描述；新增=下拉选员工，可跳转人事员工页建档，变更时带出默认 userName） -->
      <a-tab-pane key="employee" :tab="t('identity.user.tabs.employeeInfo')" force-render>
        <div :class="formContentClass">
          <!-- 编辑态 -->
          <template v-if="formData?.userId">
            <a-row :gutter="24">
              <a-col :span="24">
                <a-form-item :label="t('entity.user.employeeid')" name="employeeId">
                  <TaktSelect
                    v-model:value="formState.employeeId"
                    :options="employeeOptions"
                    disabled
                  />
                </a-form-item>
              </a-col>
            </a-row>
            <a-descriptions v-if="boundEmployeeOption" bordered size="middle" :column="2">
              <a-descriptions-item :label="t('entity.employee.code')">
                {{ displayStr(boundEmployeeOption.extLabel) }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('entity.employee.realname')">
                {{ displayStr(boundEmployeeOption.dictLabel) }}
              </a-descriptions-item>
            </a-descriptions>
            <a-alert
              v-else
              type="warning"
              show-icon
              :message="t('identity.user.fields.employeeOptionMissing')"
            />
          </template>
          <!-- 新增态 -->
          <template v-else>
            <a-row :gutter="24">
              <a-col :span="24">
                <a-form-item :label="t('entity.user.employeeid')" name="employeeId">
                  <TaktSelect
                    v-model:value="formState.employeeId"
                    :options="employeeOptions"
                    :placeholder="t('identity.user.fields.employeeId.placeholder')"
                    show-search
                    :filter-option="filterOption"
                    @change="onEmployeeChange"
                  />
                </a-form-item>
              </a-col>
            </a-row>
            <a-alert
              type="info"
              show-icon
              :message="t('identity.user.fields.employeeSnapshot.hint')"
              style="margin-bottom: 16px"
            />
            <a-space style="margin-bottom: 16px">
              <span>{{ t('identity.user.fields.employeeLink.createNewHint') }}</span>
              <a-button type="link" @click="goToEmployeeCreate">
                {{ t('common.button.create') }}{{ t('entity.employee._self') }}
              </a-button>
            </a-space>
            <a-descriptions v-if="selectedEmployeeOption" bordered size="middle" :column="2">
              <a-descriptions-item :label="t('entity.employee.code')">
                {{ displayStr(selectedEmployeeOption.extLabel) }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('entity.employee.realname')">
                {{ displayStr(selectedEmployeeOption.dictLabel) }}
              </a-descriptions-item>
            </a-descriptions>
            <a-alert
              v-else-if="formState.employeeId"
              type="warning"
              show-icon
              :message="t('identity.user.fields.employeeOptionMissing')"
            />
          </template>
        </div>
      </a-tab-pane>

      <!-- 标签2：用户信息（账号、联系方式、类型/状态、备注；新增时含密码） -->
      <a-tab-pane key="user" :tab="t('identity.user.tabs.userInfo')" force-render>
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="24">
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
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.user.nickname')" name="nickName">
                <a-input
                  v-model:value="formState.nickName"
                  :placeholder="t('identity.user.fields.nicknamePlaceholder')"
                  show-count
                  :maxlength="200"
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
            <a-col :span="12">
              <a-form-item :label="t('entity.user.type')" name="userType">
                <TaktSelect
                  v-model:value="formState.userType"
                  dict-type="sys_user_type"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.user.type') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.user.status')" name="userStatus">
                <TaktSelect
                  v-model:value="formState.userStatus"
                  dict-type="sys_normal_disable"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.user.status') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 仅新增展示密码；编辑不展示，提交时由父级不传 passwordHash -->
          <a-row :gutter="24" v-if="!formData?.userId">
            <a-col :span="24">
              <a-form-item :label="t('entity.user.password')" name="password">
                <a-input-password
                  v-model:value="formState.password"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.user.password') })"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('common.entity.remark')" name="remark">
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
        </div>
      </a-tab-pane>

      <!-- 标签3：权限分配（角色、租户、部门树、岗位；绑定 permissionState，随 getValues 返回） -->
      <a-tab-pane key="permission" :tab="t('common.action.tabTargetAllocation', { target: t('identity.user.tabs.permission') })" force-render>
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.role._self')">
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
              <a-form-item :label="t('entity.tenant._self')">
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
              <a-form-item :label="t('entity.dept._self')">
                <DeptTreeTransfer
                  v-model:model-value="permissionState.deptIds"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.post._self')">
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
        </div>
      </a-tab-pane>

    </a-tabs>
  </a-form>
</template>

<script setup lang="ts">
/**
 * 用户维护表单脚本：与 `user/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields`、`setServerValidationErrors` 完成弹窗提交流程。
 */
import { reactive, watch, ref, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import type { Rule } from 'ant-design-vue/es/form'
import { message } from 'ant-design-vue'
import type { User, UserFormModel, UserFormPermissionModel, UserFormValues } from '@/types/identity/user'
import type { TaktSelectOption } from '@/types/common'
import { getRoleOptions } from '@/api/identity/role'
import { getPostOptions } from '@/api/human-resource/organization/post'
import { getTenantOptions } from '@/api/identity/tenant'
import { getEmployeeOptions } from '@/api/human-resource/personnel/employee'
import DeptTreeTransfer from './dept-tree-transfer.vue'
import { logger } from '@/utils/logger'
import { useUserStore } from '@/stores/identity/user'
import {
  isValidUsername,
  isValidUserNickName,
  isValidEmail,
  isValidPhone,
  isValidPassword
} from '@/utils/regex'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()

/** 当前激活 Tab：employee | user | permission */
const activeTab = ref('employee')
/** 表单内容区纵向布局类名（与弹窗内其它表单一致） */
const formContentClass = 'takt-form-content-rows-10'

/**
 * 组件入参：由列表页传入当前编辑用户或空对象（新增）。
 */
interface Props {
  /** 当前用户数据（编辑时含 userId），与 `@/types/identity/user` 中 `User` 一致 */
  formData?: Partial<User>
  /** 提交中状态，由父级传入 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/** Ant Design Vue 表单实例，用于 validate、resetFields、setFields */
const formRef = ref()

/** 与 `UserFormModel` 一致的初始值 */
function createEmptyUserFormModel(): UserFormModel {
  return {
    employeeId: '',
    userName: '',
    nickName: '',
    userType: 0,
    userEmail: '',
    userPhone: '',
    password: '',
    userStatus: 1,
    remark: ''
  }
}

/** a-form 绑定模型（UserFormModel） */
const formState = reactive<UserFormModel>(createEmptyUserFormModel())

/** 与 `UserFormPermissionModel` 一致的初始值 */
function createEmptyUserFormPermission(): UserFormPermissionModel {
  return {
    roleIds: [],
    deptIds: [],
    postIds: [],
    tenantIds: []
  }
}

/** 权限标签页模型（UserFormPermissionModel），由 getValues 与 formState 合并为 UserFormValues */
const permissionState = reactive<UserFormPermissionModel>(createEmptyUserFormPermission())

/** 角色 / 岗位 / 租户 / 员工 下拉选项 */
const roleOptions = ref<TaktSelectOption[]>([])
/** 岗位下拉 */
const postOptions = ref<TaktSelectOption[]>([])
/** 租户下拉 */
const tenantOptions = ref<TaktSelectOption[]>([])
/** 员工下拉（新增与编辑拉取策略由 loadBusinessOptions 区分） */
const employeeOptions = ref<TaktSelectOption[]>([])

/** 当前登录用户是否为管理员类型（userType 1 或 2） */
const isAdmin = computed(() => {
  const ut = userStore.userInfo?.userType
  return ut === 1 || ut === 2
})

/** 根据 formState.employeeId 在员工选项中解析当前项 */
const selectedEmployeeOption = computed(() => {
  const id = formState.employeeId
  if (id === undefined || id === null || String(id).trim() === '') return undefined
  return employeeOptions.value.find((o) => String(o.dictValue) === String(id))
})

/** 编辑态下与已绑定员工对应的选项（依赖 selectedEmployeeOption） */
const boundEmployeeOption = computed(() => {
  if (!props.formData?.userId) return undefined
  return selectedEmployeeOption.value
})

/** descriptions 展示用，空值显示为 `-` */
const displayStr = (v: unknown) => {
  if (v === undefined || v === null || String(v).trim() === '') return '-'
  return String(v)
}

/** 跳转人事员工建档页 */
const goToEmployeeCreate = () => {
  router.push('/human-resource/personnel/employee')
}

/** TaktSelect 搜索：按 label / extLabel 模糊匹配 */
const filterOption = (input: string, option: any) => {
  const label = (option?.label ?? option?.dictLabel ?? '') as string
  const ext = (option?.extLabel ?? '') as string
  return (label + ' ' + ext).toLowerCase().includes((input || '').toLowerCase())
}

/** 员工变更：新增时按选项带出默认 userName */
const onEmployeeChange = (value: string | number | (string | number)[] | undefined) => {
  const v = value == null ? '' : Array.isArray(value) ? value[0] : value
  const opt = employeeOptions.value.find((o) => String(o.dictValue) === String(v))
  if (opt && !props.formData?.userId) {
    formState.userName = (opt.extLabel ?? opt.dictLabel ?? '') as string
  }
}

/** 并行加载角色、岗位、租户、员工选项 */
const loadBusinessOptions = async () => {
  try {
    const [roles, posts, tenants, employees] = await Promise.all([
      getRoleOptions(),
      getPostOptions(),
      getTenantOptions(),
      getEmployeeOptions(!props.formData?.userId)
    ])
    roleOptions.value = roles
    postOptions.value = posts
    tenantOptions.value = tenants
    employeeOptions.value = employees
  } catch (error: any) {
    logger.error('[User Form] 加载业务选项数据失败:', {
      error,
      message: error?.message || t('common.msg.loadOptionsFail'),
      stack: error?.stack
    })
    const errorMessage = error?.response?.data?.message || error?.message || t('common.msg.loadOptionsFail')
    message.error({ content: errorMessage, duration: 5 })
  }
}

/** 挂载时拉取角色/岗位/租户/员工选项 */
onMounted(() => {
  loadBusinessOptions()
})

/** 校验规则：`name` 与 `a-form-item` 的 `name` 一致 */
const rules = computed<Record<string, Rule[]>>(() => {
  /** 对应 name=employeeId：新增必填；编辑已绑定则跳过 */
  const employeeIdRules: Rule[] = [
    {
      validator: (_rule: any, value: string) => {
        if (props.formData?.userId) return Promise.resolve()
        if (!value) {
          return Promise.reject(t('identity.user.fields.employeeId.placeholder'))
        }
        return Promise.resolve()
      },
      trigger: 'change'
    }
  ]

  /** 对应 name=userName：新增必填 + 用户名格式；编辑禁用输入故跳过 */
  const userNameRules: Rule[] = [
    {
      validator: (_rule: any, value: string) => {
        if (props.formData?.userId) return Promise.resolve()
        if (!value) {
          return Promise.reject(t('common.form.placeholder.required', { field: t('entity.user.name') }))
        }
        if (!isValidUsername(value)) {
          return Promise.reject(t('identity.user.fields.userName.validation.format'))
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ]

  return {
    employeeId: employeeIdRules,
    userName: userNameRules,
    nickName: [
      {
        validator: (_rule: any, value: string) => {
          if (!value || !String(value).trim()) return Promise.resolve()
          if (!isValidUserNickName(String(value))) {
            return Promise.reject(t('identity.user.fields.nickName.validation.format'))
          }
          return Promise.resolve()
        },
        trigger: 'blur'
      }
    ],
    // userEmail：必填 + 邮箱格式
    userEmail: [
      {
        required: true,
        message: t('common.form.placeholder.required', { field: t('entity.user.email') }),
        trigger: 'blur'
      },
      {
        validator: (_rule: any, value: string) => {
          if (!value) return Promise.resolve()
          if (!isValidEmail(value)) {
            return Promise.reject(t('validation.formatInvalid', [t('entity.user.email')]))
          }
          return Promise.resolve()
        },
        trigger: 'blur'
      }
    ],
    // userPhone：必填 + 手机格式
    userPhone: [
      {
        required: true,
        message: t('common.form.placeholder.required', { field: t('entity.user.phone') }),
        trigger: 'blur'
      },
      {
        validator: (_rule: any, value: string) => {
          if (!value) return Promise.resolve()
          if (!isValidPhone(value)) {
            return Promise.reject(t('validation.formatInvalid', [t('entity.user.phone')]))
          }
          return Promise.resolve()
        },
        trigger: 'blur'
      }
    ],
    // password：仅新增必填 + 密码策略；编辑不展示该项
    password: [
      {
        required: !props.formData?.userId,
        message: t('common.form.placeholder.required', { field: t('entity.user.password') }),
        trigger: 'blur'
      },
      {
        validator: (_rule: any, value: string) => {
          if (!value) return Promise.resolve()
          if (!isValidPassword(value)) {
            return Promise.reject(t('identity.user.password.new.validation.format'))
          }
          return Promise.resolve()
        },
        trigger: 'blur'
      }
    ],
    // userType：字典 sys_user_type，必选
    userType: [
      {
        required: true,
        message: t('common.form.placeholder.select', { field: t('entity.user.type') }),
        trigger: 'change'
      }
    ],
    // userStatus：字典 sys_normal_disable，必选
    userStatus: [
      {
        required: true,
        message: t('common.form.placeholder.select', { field: t('entity.user.status') }),
        trigger: 'change'
      }
    ]
  }
})

/** 监听 formData：编辑回填表单与权限 ID；新增重置为空；immediate + deep 与父级异步赋行对齐 */
watch(
  () => props.formData,
  (newData) => {
    if (newData?.userId) {
      Object.assign(formState, {
        employeeId: newData.employeeId != null ? String(newData.employeeId) : '',
        userName: newData.userName || '',
        nickName: newData.nickName ?? '',
        userType: newData.userType !== undefined && newData.userType !== null ? newData.userType : 0,
        userEmail: newData.userEmail || '',
        userPhone: newData.userPhone || '',
        password: '',
        userStatus: newData.userStatus !== undefined && newData.userStatus !== null ? newData.userStatus : 1,
        remark: newData.remark || ''
      })
      permissionState.roleIds = newData.roleIds?.map((id) => String(id)) || []
      permissionState.deptIds = newData.deptIds?.map((id) => String(id)) || []
      permissionState.postIds = newData.postIds?.map((id) => String(id)) || []
      permissionState.tenantIds = newData.tenantIds?.map((id) => String(id)) || []
    } else {
      Object.assign(formState, createEmptyUserFormModel())
      Object.assign(permissionState, createEmptyUserFormPermission())
    }
  },
  { immediate: true, deep: true }
)

/** 预留：可结合当前用户类型与编辑态做权限类 UI 控制（当前仅占位） */
watch(
  () => [userStore.userInfo?.userType, props.formData?.userId] as const,
  () => {
    if (props.formData?.userId || !isAdmin.value) return
  }
)

/** 执行 a-form 校验；父组件在提交前 await 本方法 */
const validate = async () => {
  await formRef.value?.validate()
}

/** 合并基础字段与权限 ID，供父级组装 UserCreate / 更新 DTO */
const getValues = (): UserFormValues => {
  return {
    employeeId: formState.employeeId ? String(formState.employeeId) : '',
    userName: formState.userName || '',
    nickName: formState.nickName?.trim() ?? '',
    userType: formState.userType ?? 0,
    userEmail: formState.userEmail || '',
    userPhone: formState.userPhone || '',
    password: formState.password || '',
    userStatus: formState.userStatus ?? 1,
    remark: formState.remark || '',
    roleIds: permissionState.roleIds ?? [],
    deptIds: permissionState.deptIds ?? [],
    postIds: permissionState.postIds ?? [],
    tenantIds: permissionState.tenantIds ?? []
  }
}

/** 重置 Ant 校验与本地 model，Tab 回到员工 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyUserFormModel())
  Object.assign(permissionState, createEmptyUserFormPermission())
  activeTab.value = 'employee'
}

/** 将后端校验错误写入对应表单项 */
const setServerValidationErrors = (errors: Array<{ field?: string; message?: string }>) => {
  if (!Array.isArray(errors) || errors.length === 0) return
  const fields = errors
    .filter((e) => e?.field && e?.message)
    .map((e) => ({ name: e.field as string, errors: [e.message as string] }))
  if (fields.length > 0) {
    formRef.value?.setFields(fields)
  }
}

/** 暴露给 `user/index.vue` 弹窗 ref：`validate`、`getValues`、`resetFields`、`setServerValidationErrors` */
defineExpose({
  validate,
  getValues,
  resetFields,
  setServerValidationErrors
})
</script>

<style scoped lang="less">
/* 标签页内容区最小高度，与列表页弹窗内表单一致 */
:deep(.ant-tabs-content-holder) {
  min-height: 50vh;
}

:deep(.ant-tabs-tabpane) {
  min-height: 50vh;
}
</style>
