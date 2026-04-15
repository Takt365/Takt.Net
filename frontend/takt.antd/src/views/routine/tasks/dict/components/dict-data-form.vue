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
      :rules="formRules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
      layout="horizontal"
    >
      <!-- 表单字段顺序与 DictData 接口字段顺序一致 -->
      <a-form-item label="字典类型编码" name="dictTypeCode">
        <a-input
          v-model:value="formState.dictTypeCode"
          placeholder="字典类型编码"
          :disabled="true"
        />
      </a-form-item>

      <a-form-item label="字典标签" name="dictLabel">
        <a-input
          v-model:value="formState.dictLabel"
          placeholder="请输入字典标签（在同一个字典类型下唯一）"
        />
      </a-form-item>

      <a-form-item label="字典本地化键" name="dictL10nKey">
        <a-input
          v-model:value="formState.dictL10nKey"
          placeholder="请输入字典本地化键（可选，用于多语言翻译）"
        />
      </a-form-item>

      <a-form-item label="字典值" name="dictValue">
        <a-input
          v-model:value="formState.dictValue"
          placeholder="请输入字典值（显示值）"
        />
      </a-form-item>

      <a-form-item label="CSS类名" name="cssClass">
        <a-input-number
          v-model:value="formState.cssClass"
          :min="0"
          placeholder="请输入CSS类名"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item label="列表类名" name="listClass">
        <a-input-number
          v-model:value="formState.listClass"
          :min="0"
          placeholder="请输入列表类名"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item label="扩展标签" name="extLabel">
        <a-input
          v-model:value="formState.extLabel"
          placeholder="请输入扩展标签（可选）"
        />
      </a-form-item>

      <a-form-item label="扩展值" name="extValue">
        <a-input
          v-model:value="formState.extValue"
          placeholder="请输入扩展值（可选）"
        />
      </a-form-item>

      <a-form-item label="排序号" name="orderNum">
        <a-input-number
          v-model:value="formState.orderNum"
          :min="0"
          placeholder="请输入排序号（越小越靠前）"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          placeholder="请输入备注"
          :rows="3"
        />
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { DictData, DictDataCreate, DictDataUpdate } from '@/types/routine/tasks/dict/dictdata'

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
const formState = reactive<DictDataCreate & { dictDataId?: string }>({
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

const formRules: Record<string, Rule[]> = {
  dictLabel: [
    { required: true, message: '请输入字典标签', trigger: 'blur' }
  ],
  dictValue: [
    { required: true, message: '请输入字典值', trigger: 'blur' }
  ]
}

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
