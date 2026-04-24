<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/logistics/materials/plant/components -->
<!-- 文件名称：plant-form.vue -->
<!-- 功能描述：工厂表维护弹窗内嵌表单。由同目录上级 index.vue 引用；defineExpose 提供 validate、getValues、resetFields、setServerValidationErrors。a-form 内 a-tabs：每标签最多 10 个 IsCreate 字段；布局由表配置 front_style 决定——与实体 TaktGenTable.FrontStyle 及字典 gen_frontend_style 一致：12=一行一列（普通字段与 textarea 均为 a-col span 24），24=一行两列（普通字段 span 12，textarea span 24）。与 identity/user/components/user-form.vue 的表单骨架风格一致。 -->
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
    <a-tabs
      v-model:active-key="activeTab"
      class="plant-form-tabs"
    >
      <!-- 标签 1：本页字段序号区间 [0, 9]（每标签最多 10 项） -->
      <a-tab-pane
        key="tab-0"
        :tab="t('common.form.tabs.basicinfo') + ' (1/3)'"
        force-render
      >
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantcode')"
                name="plantCode"
              >
                <TaktSelect
                  v-model:value="formState.plantCode"
                  dict-type="hr_attendance_correction_approval"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.plant.plantcode') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantname')"
                name="plantName"
              >
                <a-input
                  v-model:value="formState.plantName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantname') })"
                  show-count
                                    :maxlength="200"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantshortname')"
                name="plantShortName"
              >
                <a-input
                  v-model:value="formState.plantShortName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantshortname') })"
                  show-count
                                    :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.registrationaddress')"
                name="registrationAddress"
              >
                <a-input
                  v-model:value="formState.registrationAddress"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.registrationaddress') })"
                  show-count
                                    :maxlength="500"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.registrationregion')"
                name="registrationRegion"
              >
                <a-input
                  v-model:value="formState.registrationRegion"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.registrationregion') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.registrationprovince')"
                name="registrationProvince"
              >
                <a-input
                  v-model:value="formState.registrationProvince"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.registrationprovince') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.registrationcity')"
                name="registrationCity"
              >
                <a-input
                  v-model:value="formState.registrationCity"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.registrationcity') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.businessregion')"
                name="businessRegion"
              >
                <a-input
                  v-model:value="formState.businessRegion"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.businessregion') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.businessprovince')"
                name="businessProvince"
              >
                <a-input
                  v-model:value="formState.businessProvince"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.businessprovince') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.businesscity')"
                name="businessCity"
              >
                <a-input
                  v-model:value="formState.businessCity"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.businesscity') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </div>
      </a-tab-pane>
      <!-- 标签 2：本页字段序号区间 [10, 19]（每标签最多 10 项） -->
      <a-tab-pane
        key="tab-1"
        :tab="t('common.form.tabs.basicinfo') + ' (2/3)'"
        force-render
      >
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.businessaddress')"
                name="businessAddress"
              >
                <a-input
                  v-model:value="formState.businessAddress"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.businessaddress') })"
                  show-count
                                    :maxlength="500"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantaddress')"
                name="plantAddress"
              >
                <a-input
                  v-model:value="formState.plantAddress"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantaddress') })"
                  show-count
                                    :maxlength="500"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantphone')"
                name="plantPhone"
              >
                <a-input
                  v-model:value="formState.plantPhone"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantphone') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantemail')"
                name="plantEmail"
              >
                <a-input
                  v-model:value="formState.plantEmail"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantemail') })"
                  show-count
                                    :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantmanager')"
                name="plantManager"
              >
                <a-input
                  v-model:value="formState.plantManager"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantmanager') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.enterprisenature')"
                name="enterpriseNature"
              >
                <a-input
                  v-model:value="formState.enterpriseNature"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.enterprisenature') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.industryattribute')"
                name="industryAttribute"
              >
                <a-input
                  v-model:value="formState.industryAttribute"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.industryattribute') })"
                  show-count
                                    :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.enterprisescale')"
                name="enterpriseScale"
              >
                <a-input
                  v-model:value="formState.enterpriseScale"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.enterprisescale') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.businessscope')"
                name="businessScope"
              >
                <a-input
                  v-model:value="formState.businessScope"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.businessscope') })"
                  show-count
                                    :maxlength="2000"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.relatedcompany')"
                name="relatedCompany"
              >
                <a-input
                  v-model:value="formState.relatedCompany"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.relatedcompany') })"
                  show-count
                                    :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </div>
      </a-tab-pane>
      <!-- 标签 3：本页字段序号区间 [20, 29]（每标签最多 10 项） -->
      <a-tab-pane
        key="tab-2"
        :tab="t('common.form.tabs.basicinfo') + ' (3/3)'"
        force-render
      >
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.plantstatus')"
                name="plantStatus"
              >
                <a-input
                  v-model:value="formState.plantStatus"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.plantstatus') })"
                  show-count
                                    :maxlength="10"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.ordernum')"
                name="orderNum"
              >
                <a-input
                  v-model:value="formState.orderNum"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.ordernum') })"
                  show-count
                                    :maxlength="10"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.extfieldjson')"
                name="extFieldJson"
              >
                <a-input
                  v-model:value="formState.extFieldJson"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.extfieldjson') })"
                  show-count
                                    :maxlength="4000"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.plant.remark')"
                name="remark"
              >
                <a-input
                  v-model:value="formState.remark"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.plant.remark') })"
                  show-count
                                    :maxlength="500"
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
 * 工厂表维护表单脚本：与上级 `index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields`、`setServerValidationErrors` 完成弹窗提交流程（与 identity/user/components/user-form.vue 暴露能力一致）。
 * 布局：每标签最多 10 个 IsCreate 字段；`front_style===12` 为一行一列（栅格 24），`front_style===24` 为一行两列（普通 12、textarea 24），与 `TaktGenTable.FrontStyle` / `gen_frontend_style` 一致。
 * @module views/logistics/materials/plant/components
 */
import { reactive, watch, computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Plant } from '@/types/logistics/materials/plant'

const { t } = useI18n()

/** 表单内容区纵向布局类名（与弹窗内其它表单一致，同 user-form.vue） */
const formContentClass = 'takt-form-content-rows-10'

/** 当前激活的标签 key（tab-0、tab-1…），与 a-tab-pane 的 key 一致 */
const activeTab = ref('tab-0')

/**
 * 组件入参：由列表页传入当前编辑行或空对象（新增）。
 */
interface Props {
  /** 当前实体数据（编辑时含主键字段），与 `@/types/.../plant` 中类型一致 */
  formData?: Partial<Plant> | null
  /** 提交中状态，由父级传入 */
  loading?: boolean
}

/**
 * 声明组件入参并设置默认值：与 `user-form.vue` 一致——新增时 `formData` 默认为空对象，`loading` 默认为 false，避免子组件对 undefined 分支处理不一致。
 */
const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * 服务端返回的字段级校验错误（供 `setServerValidationErrors` 映射为 Ant Design Vue `setFields` 入参）。
 */
interface ValidationErrorItem {
  /** 对应 `a-form-item` 的 `name`，须与表单字段名一致 */
  field?: string
  /** 展示在该表单项下的错误提示文案 */
  message?: string
}

/** Ant Design Vue 表单实例，用于 validate、resetFields、setFields */
const formRef = ref()

/** a-form 绑定模型（动态字段，由生成列驱动） */
const formState = reactive<Record<string, any>>({})

/** 监听 formData：编辑回填；新增重置；immediate + deep 与父级异步赋行对齐（同 user-form 策略） */
watch(
  () => props.formData,
  (val) => {
    const next = val ? { ...val } : {}
    Object.keys(formState).forEach((k) => delete formState[k])
    Object.assign(formState, next)
  },
  { immediate: true, deep: true }
)

/** 校验规则：IsCreate 且 IsRequired 的列必填；文案使用 common.form.placeholder.* + entity.*（与 user-form 一致） */
const rules = computed<Record<string, Rule[]>>(() => ({
  plantCode: [
    {
      required: true,
      message: t('common.form.placeholder.select', { field: t('entity.plant.plantcode') }),
      trigger: 'change'
    }
  ],
  plantName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.plant.plantname') }),
      trigger: 'blur'
    }
  ],
  plantStatus: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.plant.plantstatus') }),
      trigger: 'blur'
    }
  ],
  orderNum: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.plant.ordernum') }),
      trigger: 'blur'
    }
  ],
}))

/** 执行 a-form 校验；父组件在提交前 await 本方法；通过后返回当前 formState（便于与旧生成页兼容） */
async function validate() {
  await formRef.value?.validate()
  return formState
}

/** 返回当前表单字段快照，供父级组装 Create/Update DTO */
function getValues(): Record<string, any> {
  return { ...formState }
}

/** 重置 Ant 校验与本地 model；Tab 回到第一页（同 user-form 重置策略） */
function resetFields() {
  formRef.value?.resetFields()
  Object.keys(formState).forEach((k) => delete formState[k])
  activeTab.value = 'tab-0'
}

/** 将后端校验错误写入对应表单项 */
function setServerValidationErrors(errors: ValidationErrorItem[]) {
  if (!Array.isArray(errors) || errors.length === 0) return
  const fields = errors
    .filter((e) => e?.field && e?.message)
    .map((e) => ({ name: e.field as string, errors: [e.message as string] }))
  if (fields.length > 0) {
    formRef.value?.setFields(fields as any)
  }
}

/** 暴露给上级列表页弹窗 ref：validate、getValues、resetFields、setServerValidationErrors */
defineExpose({ validate, getValues, resetFields, setServerValidationErrors })
</script>

<style scoped lang="less">
/* 标签页内容区最小高度，与 user-form 弹窗内多 Tab 表单一致 */
:deep(.ant-tabs-content-holder) {
  min-height: 50vh;
}

:deep(.ant-tabs-tabpane) {
  min-height: 50vh;
}
</style>
