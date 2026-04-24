<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/dict/components -->
<!-- 文件名称：dict-data-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：字典数据表单组件 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="dict-data-form">
    <a-form
      ref="formRef"
      :model="formState"
      :rules="formRulesComputed"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
      layout="horizontal"
    >
      <!-- 表单字段顺序与 DictData 接口字段顺序一致 -->
      <a-form-item
        :label="t('entity.dictdata.dicttypecode')"
        name="dictTypeCode"
      >
        <a-input
          v-model:value="formState.dictTypeCode"
          :placeholder="t('routine.dict.type.placeholders.dictTypeCode')"
          :disabled="true"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.dictlabel')"
        name="dictLabel"
      >
        <a-input
          v-model:value="formState.dictLabel"
          :placeholder="t('routine.dict.type.placeholders.dictLabelUnique')"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.dictl10nkey')"
        name="dictL10nKey"
      >
        <a-input
          v-model:value="formState.dictL10nKey"
          :placeholder="t('routine.dict.type.placeholders.dictL10nKey')"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.dictvalue')"
        name="dictValue"
      >
        <a-input
          v-model:value="formState.dictValue"
          :placeholder="t('routine.dict.type.placeholders.dictValue')"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.cssclass')"
        name="cssClass"
      >
        <a-input-number
          v-model:value="formState.cssClass"
          :min="0"
          :placeholder="t('routine.dict.type.placeholders.cssClass')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.listclass')"
        name="listClass"
      >
        <a-input-number
          v-model:value="formState.listClass"
          :min="0"
          :placeholder="t('routine.dict.type.placeholders.listClass')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.extlabel')"
        name="extLabel"
      >
        <a-input
          v-model:value="formState.extLabel"
          :placeholder="t('routine.dict.type.placeholders.extLabel')"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.extvalue')"
        name="extValue"
      >
        <a-input
          v-model:value="formState.extValue"
          :placeholder="t('routine.dict.type.placeholders.extValue')"
        />
      </a-form-item>

      <a-form-item
        :label="t('entity.dictdata.ordernum')"
        name="orderNum"
      >
        <a-input-number
          v-model:value="formState.orderNum"
          :min="0"
          :placeholder="t('routine.dict.type.placeholders.orderNumSort')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('common.entity.remark')"
        name="remark"
      >
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('routine.dict.type.placeholders.remark')"
          :rows="3"
        />
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { DictData, DictDataCreate, DictDataUpdate } from '@/types/routine/tasks/dict/dict-data'

const { t } = useI18n()

/** 弹窗表单：`DictDataCreate` 中 `dictL10nKey`/`extLabel`/`extValue`/`remark` 为可选时与 `a-input`/`a-textarea` 在 exactOptionalPropertyTypes 下不兼容，此处收窄为必填 `string`（空串表示未填）。 */
type DictDataFormState = Omit<DictDataCreate, 'dictL10nKey' | 'extLabel' | 'extValue' | 'remark'> & {
  dictL10nKey: string
  extLabel: string
  extValue: string
  remark: string
  dictDataId?: string
}

// ========================================
// Props & Emits
// ========================================

interface Props {
  formData?: DictData | null
  dictTypeCode?: string
  dictTypeId?: string
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: null,
  dictTypeCode: '',
  dictTypeId: '',
  loading: false
})

// ========================================
// 数据定义
// ========================================

const formRef = ref()

// 表单状态（与 DictData 接口字段顺序一致）
const formState = reactive<DictDataFormState>({
  dictTypeId: '',
  dictTypeCode: '',
  dictLabel: '',
  dictL10nKey: '',
  dictValue: '',
  cssClass: 0,
  listClass: 0,
  extLabel: '',
  extValue: '',
  orderNum: 0,
  remark: ''
})

const formRulesComputed = computed<Record<string, Rule[]>>(() => ({
  dictLabel: [
    { required: true, message: t('routine.dict.type.rules.dictLabelRequired'), trigger: 'blur' }
  ],
  dictValue: [
    { required: true, message: t('routine.dict.type.rules.dictValueRequired'), trigger: 'blur' }
  ]
}))

// ========================================
// 方法定义
// ========================================

// 监听 formData 变化
watch(
  () => props.formData,
  (newData) => {
    if (newData) {
      // 编辑模式：填充表单数据（按 DictData 接口字段顺序）
      Object.assign(formState, {
        dictDataId: newData.dictDataId,
        dictTypeId: newData.dictTypeId || props.dictTypeId,
        dictTypeCode: newData.dictTypeCode || props.dictTypeCode,
        dictLabel: newData.dictLabel || '',
        dictL10nKey: newData.dictL10nKey || '',
        dictValue: newData.dictValue || '',
        cssClass: newData.cssClass ?? 0,
        listClass: newData.listClass ?? 0,
        extLabel: newData.extLabel || '',
        extValue: newData.extValue || '',
        orderNum: newData.orderNum ?? 0,
        remark: newData.remark || ''
      })
    } else {
      // 新增模式：重置表单（按 DictData 接口字段顺序）
      Object.assign(formState, {
        dictDataId: undefined,
        dictTypeId: props.dictTypeId || '',
        dictTypeCode: props.dictTypeCode || '',
        dictLabel: '',
        dictL10nKey: '',
        dictValue: '',
        cssClass: 0,
        listClass: 0,
        extLabel: '',
        extValue: '',
        orderNum: 0,
        remark: ''
      })
    }
  },
  { immediate: true, deep: true }
)

// 监听 dictTypeCode 和 dictTypeId 变化
watch(
  () => [props.dictTypeCode, props.dictTypeId],
  ([newCode, newId]) => {
    if (!props.formData) {
      // 新增模式下，更新字典类型信息
      formState.dictTypeCode = newCode || ''
      formState.dictTypeId = newId || ''
    }
  },
  { immediate: true }
)

// 表单验证
const validate = async () => {
  await formRef.value?.validate()
}

// 获取表单数据（按 DictData 接口字段顺序）
const getFormData = (): DictDataCreate | DictDataUpdate => {
  const baseData: any = {
    dictTypeId: formState.dictTypeId,
    dictTypeCode: formState.dictTypeCode,
    dictLabel: formState.dictLabel,
    dictL10nKey: formState.dictL10nKey || undefined,
    dictValue: formState.dictValue,
    cssClass: formState.cssClass,
    listClass: formState.listClass,
    extLabel: formState.extLabel || undefined,
    extValue: formState.extValue || undefined,
    orderNum: formState.orderNum,
    remark: formState.remark || undefined
  }
  
  if (formState.dictDataId) {
    baseData.dictDataId = formState.dictDataId
  }
  
  return baseData
}

// ========================================
// 暴露方法
// ========================================

defineExpose({
  validate,
  getFormData
})
</script>

<style scoped lang="less">
.dict-data-form {
  padding: 16px 0;
}
</style>
