<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-career/components -->
<!-- 文件名称：career-form.vue -->
<!-- 功能描述：员工职业信息维护弹窗内嵌表单。由 employee-career/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。表单模型复用 `@/types/human-resource/personnel/employee-career` 中 EmployeeCareerCreate。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicinfo')"
    >
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
              :label="t('entity.employeecareer.employeeId')"
              name="employeeId"
            >
              <a-input
                v-model:value="formState.employeeId"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.deptId')"
              name="deptId"
            >
              <a-input
                v-model:value="formState.deptId"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.deptName')"
              name="deptName"
            >
              <a-input
                v-model:value="formState.deptName"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.postName')"
              name="postName"
            >
              <a-input
                v-model:value="formState.postName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.jobTitle')"
              name="jobTitle"
            >
              <a-input
                v-model:value="formState.jobTitle"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.joinDate')"
              name="joinDate"
            >
              <a-date-picker
                v-model:value="formState.joinDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.workNature')"
              name="workNature"
            >
              <a-select
                v-model:value="formState.workNature"
                :options="workNatureOptions"
                allow-clear
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.employmentType')"
              name="employmentType"
            >
              <a-select
                v-model:value="formState.employmentType"
                :options="employmentTypeOptions"
                allow-clear
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.isPrimary')"
              name="isPrimary"
            >
              <a-select
                v-model:value="formState.isPrimary"
                :options="isPrimaryOptions"
                allow-clear
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecareer.directManagerName')"
              name="directManagerName"
            >
              <a-input
                v-model:value="formState.directManagerName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
    </a-tab-pane>
  </a-tabs>
</template>

<script setup lang="ts">
/**
 * 员工职业信息表单脚本：与 `employee-career/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { EmployeeCareer, EmployeeCareerCreate } from '@/types/human-resource/personnel/employee-career'

const { t } = useI18n()
const entityKey = 'entity.employeecareer'

interface Props {
  /** 当前编辑行数据，新增时为空对象 */
  formData?: Partial<EmployeeCareer>
  /** 提交中状态，由父组件传入 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/** Ant Design Vue 表单实例 */
const formRef = ref()
/** 当前激活的 Tab 键；仅 basic */
const activeTab = ref('basic')

/** 工作性质选项 */
const workNatureOptions = computed(() =>
  [0, 1, 2, 3, 4].map((v) => ({ label: t(`${entityKey}.workNature${v}`), value: v }))
)
/** 任职类型选项 */
const employmentTypeOptions = computed(() =>
  [0, 1, 2, 3].map((v) => ({ label: t(`${entityKey}.employmentType${v}`), value: v }))
)
/** 是否主职选项 */
const isPrimaryOptions = [
  { label: t('common.button.yes'), value: 1 },
  { label: t('common.button.no'), value: 0 }
]

interface FormState {
  employeeId?: string
  deptId?: string
  deptName?: string
  postId?: string
  postName?: string
  jobTitle?: string
  joinDate?: string
  workNature?: number
  employmentType?: number
  isPrimary?: number
  directManagerId?: string
  directManagerName?: string
}

/** 表单模型（对应 EmployeeCareerCreate） */
const formState = reactive<FormState>({
  employeeId: '',
  deptId: '',
  deptName: '',
  postName: '',
  jobTitle: '',
  joinDate: '',
  workNature: 0,
  employmentType: 0,
  isPrimary: 0,
  directManagerName: ''
})

/** 表单校验规则 */
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.employeeId`) }),
      trigger: 'blur'
    }
  ],
  deptId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.deptId`) }),
      trigger: 'blur'
    }
  ],
  deptName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.deptName`) }),
      trigger: 'blur'
    }
  ]
}))

/** 监听父级 formData，同步编辑态/新增态表单 */
watch(
  () => props.formData,
  (data) => {
    if (data && Object.keys(data).length > 0) {
      Object.assign(formState, {
        employeeId: data.employeeId ?? '',
        deptId: data.deptId ?? '',
        deptName: data.deptName ?? '',
        postId: data.postId ?? '',
        postName: data.postName ?? '',
        jobTitle: data.jobTitle ?? '',
        joinDate: data.joinDate ?? '',
        workNature: data.workNature ?? 0,
        employmentType: data.employmentType ?? 0,
        isPrimary: data.isPrimary ?? 0,
        directManagerId: data.directManagerId ?? '',
        directManagerName: data.directManagerName ?? ''
      })
    } else {
      Object.assign(formState, {
        employeeId: '',
        deptId: '',
        deptName: '',
        postName: '',
        jobTitle: '',
        joinDate: '',
        workNature: 0,
        employmentType: 0,
        isPrimary: 0,
        directManagerName: ''
      })
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

/** 执行表单校验 */
const validate = async () => {
  await formRef.value?.validate()
}

/** 组装提交载荷 */
const getValues = (): EmployeeCareerCreate => ({
  employeeId: formState.employeeId ?? '',
  deptId: formState.deptId ?? '',
  deptName: formState.deptName ?? '',
  postId: formState.postId,
  postName: formState.postName,
  jobTitle: formState.jobTitle,
  joinDate: formState.joinDate,
  workNature: formState.workNature ?? 0,
  employmentType: formState.employmentType ?? 0,
  isPrimary: formState.isPrimary ?? 0,
  directManagerId: formState.directManagerId,
  directManagerName: formState.directManagerName
})

/** 重置表单 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    employeeId: '',
    deptId: '',
    deptName: '',
    postName: '',
    jobTitle: '',
    joinDate: '',
    workNature: 0,
    employmentType: 0,
    isPrimary: 0,
    directManagerName: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
