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
                label="岗位名称"
                name="postName"
              >
                <a-input
                  v-model:value="formState.postName"
                  placeholder="请输入岗位名称"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="岗位编码"
                name="postCode"
              >
                <a-input
                  v-model:value="formState.postCode"
                  placeholder="请输入岗位编码"
                  show-count
                  :maxlength="50"
                  :disabled="!!formData?.postId"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                label="所属部门"
                name="deptId"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <TaktTreeSelect
                  v-model:value="formState.deptId"
                  api-url="/api/TaktDepts/tree-options"
                  placeholder="请选择所属部门"
                  allow-clear
                  :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                label="岗位类别"
                name="postCategory"
              >
                <a-input
                  v-model:value="formState.postCategory"
                  placeholder="请输入岗位类别"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="岗位级别"
                name="postLevel"
              >
                <a-input-number
                  v-model:value="formState.postLevel"
                  placeholder="请输入岗位级别"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                label="岗位职责"
                name="postDuty"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-textarea
                  v-model:value="formState.postDuty"
                  placeholder="请输入岗位职责"
                  :rows="3"
                  show-count
                  :maxlength="500"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                label="排序号"
                name="orderNum"
              >
                <a-input-number
                  v-model:value="formState.orderNum"
                  placeholder="越小越靠前"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                label="数据范围"
                name="dataScope"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-select
                  v-model:value="formState.dataScope"
                  placeholder="请选择"
                  :options="dataScopeOptions"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row
            v-if="formState.dataScope === 4"
            :gutter="24"
          >
            <a-col :span="24">
              <a-form-item
                label="自定义范围"
                name="customScope"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-input
                  v-model:value="formState.customScope"
                  placeholder="部门ID列表，逗号分隔"
                  show-count
                  :maxlength="500"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                label="岗位状态"
                name="postStatus"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-select
                  v-model:value="formState.postStatus"
                  placeholder="请选择"
                  :options="postStatusOptions"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                label="备注"
                name="remark"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-textarea
                  v-model:value="formState.remark"
                  placeholder="请输入备注"
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
import type { Post, PostCreate } from '@/types/human-resource/organization/post'

const { t } = useI18n()

const dataScopeOptions = [
  { label: '全部数据', value: 0 },
  { label: '本部门数据', value: 1 },
  { label: '本部门及以下数据', value: 2 },
  { label: '仅本人数据', value: 3 },
  { label: '自定义数据范围', value: 4 }
]
const postStatusOptions = [
  { label: '启用', value: 0 },
  { label: '禁用', value: 1 }
]

interface Props {
  formData?: Partial<Post>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const activeTab = ref('basic')
/** 表单总字段数（用于内容区高度：>=30 为 10 行，<30 为 5 行） */
const TOTAL_FIELDS = 11
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  postName?: string
  postCode?: string
  deptId?: string
  postCategory?: string
  postLevel?: number
  postDuty?: string
  orderNum?: number
  dataScope?: number
  customScope?: string
  postStatus?: number
  remark?: string
}

const formState = reactive<FormState>({
  postName: '',
  postCode: '',
  deptId: '',
  postCategory: '',
  postLevel: 0,
  postDuty: '',
  orderNum: 0,
  dataScope: 0,
  customScope: '',
  postStatus: 0,
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  postName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.post.postName') }), trigger: 'blur' }],
  postCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.post.postCode') }), trigger: 'blur' }],
  deptId: [{ required: true, message: t('common.form.placeholder.select', { field: t('entity.post.deptId') }), trigger: 'change' }],
  postLevel: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.post.postLevel') }), trigger: 'blur' }],
  dataScope: [{ required: true, message: t('common.form.placeholder.select', { field: t('entity.post.dataScope') }), trigger: 'change' }]
}))

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      postName: newData.postName ?? '',
      postCode: newData.postCode ?? '',
      deptId: newData.deptId != null ? String(newData.deptId) : '',
      postCategory: newData.postCategory ?? '',
      postLevel: newData.postLevel ?? 0,
      postDuty: newData.postDuty ?? '',
      orderNum: newData.orderNum ?? 0,
      dataScope: newData.dataScope ?? 0,
      customScope: newData.customScope ?? '',
      postStatus: newData.postStatus ?? 0,
      remark: newData.remark ?? ''
    })
  } else {
    Object.assign(formState, {
      postName: '',
      postCode: '',
      deptId: '',
      postCategory: '',
      postLevel: 0,
      postDuty: '',
      orderNum: 0,
      dataScope: 0,
      customScope: '',
      postStatus: 0,
      remark: ''
    })
  }
  activeTab.value = 'basic'
}, { immediate: true, deep: true })

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): PostCreate & { postId?: string; postStatus?: number } => {
  const deptId = formState.deptId ?? ''
  const payload: PostCreate & { postId?: string; postStatus?: number } = {
    postName: formState.postName ?? '',
    postCode: formState.postCode ?? '',
    deptId: deptId || (props.formData?.deptId != null ? String(props.formData.deptId) : ''),
    postCategory: formState.postCategory || undefined,
    postLevel: formState.postLevel ?? 0,
    postDuty: formState.postDuty || undefined,
    orderNum: formState.orderNum ?? 0,
    dataScope: formState.dataScope ?? 0,
    customScope: formState.dataScope === 4 ? (formState.customScope || undefined) : undefined,
    remark: formState.remark || undefined
  }
  if (props.formData?.postId) {
    payload.postId = props.formData.postId
    payload.postStatus = formState.postStatus ?? 0
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    postName: '',
    postCode: '',
    deptId: '',
    postCategory: '',
    postLevel: 0,
    postDuty: '',
    orderNum: 0,
    dataScope: 0,
    customScope: '',
    postStatus: 0,
    remark: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
