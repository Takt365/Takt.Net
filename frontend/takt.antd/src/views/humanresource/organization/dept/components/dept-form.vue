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
              <a-form-item label="部门名称" name="deptName">
                <a-input v-model:value="formState.deptName" placeholder="请输入部门名称" show-count :maxlength="50" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item label="部门编码" name="deptCode">
                <a-input v-model:value="formState.deptCode" placeholder="请输入部门编码" show-count :maxlength="50" :disabled="!!formData?.deptId" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item label="上级部门" name="parentId" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <TaktTreeSelect
                  v-model:value="formState.parentId"
                  api-url="/api/TaktDepts/tree-options"
                  placeholder="请选择上级部门（不选为根）"
                  allow-clear
                  :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item label="部门类型" name="deptType" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <TaktSelect
                  v-model:value="formState.deptType"
                  api-url="/api/TaktDictDatas/options?dictTypeCode=sys_dept_type"
                  placeholder="请选择"
                  :field-names="{ label: 'dictLabel', value: 'extLabel' }"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item label="部门负责人" name="deptHead">
                <a-input v-model:value="formState.deptHead" placeholder="请输入部门负责人" show-count :maxlength="50" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item label="部门电话" name="deptPhone">
                <a-input v-model:value="formState.deptPhone" placeholder="请输入部门电话" show-count :maxlength="50" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item label="部门邮箱" name="deptMail">
                <a-input v-model:value="formState.deptMail" placeholder="请输入部门邮箱" show-count :maxlength="100" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item label="部门地址" name="deptAddr">
                <a-input v-model:value="formState.deptAddr" placeholder="请输入部门地址" show-count :maxlength="200" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item label="排序号" name="orderNum">
                <a-input-number v-model:value="formState.orderNum" placeholder="越小越靠前" :min="0" style="width: 100%" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('common.entity.remark')" name="remark">
                <a-textarea v-model:value="formState.remark" :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })" :rows="2" show-count :maxlength="500" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item label="数据范围" name="dataScope" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-select v-model:value="formState.dataScope" placeholder="请选择" :options="dataScopeOptions" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item label="部门状态" name="deptStatus" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-select v-model:value="formState.deptStatus" placeholder="请选择" :options="deptStatusOptions" />
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
import type { Dept, DeptCreate } from '@/types/humanresource/organization/dept'

const { t } = useI18n()

const dataScopeOptions = [
  { label: '全部数据', value: 0 },
  { label: '本部门数据', value: 1 },
  { label: '本部门及以下数据', value: 2 },
  { label: '仅本人数据', value: 3 },
  { label: '自定义数据范围', value: 4 }
]
const deptStatusOptions = [
  { label: '启用', value: 0 },
  { label: '禁用', value: 1 }
]

interface Props {
  formData?: Partial<Dept>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const activeTab = ref('basic')
/** 表单总字段数（用于内容区高度：>=30 为 10 行，<30 为 5 行） */
const TOTAL_FIELDS = 12
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  deptName?: string
  deptCode?: string
  parentId?: string | number
  deptHead?: string
  deptType?: number
  deptPhone?: string
  deptMail?: string
  deptAddr?: string
  orderNum?: number
  dataScope?: number
  deptStatus?: number
  remark?: string
}

const formState = reactive<FormState>({
  deptName: '',
  deptCode: '',
  parentId: '0',
  deptHead: '',
  deptType: 1,
  deptPhone: '',
  deptMail: '',
  deptAddr: '',
  orderNum: 0,
  dataScope: 0,
  deptStatus: 0,
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  deptName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.dept.deptName') }), trigger: 'blur' }],
  deptCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.dept.deptCode') }), trigger: 'blur' }],
  deptType: [{ required: true, message: t('common.form.placeholder.select', { field: t('entity.dept.deptType') }), trigger: 'change' }],
  dataScope: [{ required: true, message: t('common.form.placeholder.select', { field: t('entity.dept.dataScope') }), trigger: 'change' }]
}))

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      deptName: newData.deptName ?? '',
      deptCode: newData.deptCode ?? '',
      parentId: newData.parentId !== undefined && newData.parentId !== null ? String(newData.parentId) : '0',
      deptHead: newData.deptHead ?? '',
      deptType: newData.deptType ?? 1,
      deptPhone: newData.deptPhone ?? '',
      deptMail: newData.deptMail ?? '',
      deptAddr: newData.deptAddr ?? '',
      orderNum: newData.orderNum ?? 0,
      dataScope: newData.dataScope ?? 0,
      deptStatus: newData.deptStatus ?? 0,
      remark: newData.remark ?? ''
    })
  } else {
    Object.assign(formState, {
      deptName: '',
      deptCode: '',
      parentId: '0',
      deptHead: '',
      deptType: 1,
      deptPhone: '',
      deptMail: '',
      deptAddr: '',
      orderNum: 0,
      dataScope: 0,
      deptStatus: 0,
      remark: ''
    })
  }
  activeTab.value = 'basic'
}, { immediate: true, deep: true })

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): DeptCreate => {
  const p = formState.parentId
  const parentId = p === '' || p === undefined ? '0' : String(p)
  return {
    deptName: formState.deptName ?? '',
    deptCode: formState.deptCode ?? '',
    parentId: parentId === '' ? '0' : parentId,
    deptHead: formState.deptHead || undefined,
    deptType: formState.deptType ?? 1,
    deptPhone: formState.deptPhone || undefined,
    deptMail: formState.deptMail || undefined,
    deptAddr: formState.deptAddr || undefined,
    orderNum: formState.orderNum ?? 0,
    dataScope: formState.dataScope ?? 0,
    deptStatus: formState.deptStatus ?? 0,
    remark: formState.remark || undefined
  }
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    deptName: '',
    deptCode: '',
    parentId: '0',
    deptHead: '',
    deptType: 1,
    deptPhone: '',
    deptMail: '',
    deptAddr: '',
    orderNum: 0,
    dataScope: 0,
    deptStatus: 0,
    remark: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
