<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/logistics/materials/plant/components -->
<!-- 文件名称：plant-form.vue -->
<!-- 功能描述：工厂表表单组件，由 DtoCategory/IsCreate 列驱动 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 6 }"
    :wrapper-col="{ span: 18 }"
    layout="horizontal"
  >
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-item label="工厂代码" name="plantCode">
          <a-input
            v-model:value="formState.plantCode"
            placeholder="请输入工厂代码"
            :maxlength="4"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="工厂名称" name="plantName">
          <a-input
            v-model:value="formState.plantName"
            placeholder="请输入工厂名称"
            :maxlength="200"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="工厂简称" name="plantShortName">
          <a-input
            v-model:value="formState.plantShortName"
            placeholder="请输入工厂简称"
            :maxlength="100"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="注册地址" name="registrationAddress">
          <a-input
            v-model:value="formState.registrationAddress"
            placeholder="请输入注册地址"
            :maxlength="500"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="注册地区-国家" name="registrationRegion">
          <a-input
            v-model:value="formState.registrationRegion"
            placeholder="请输入注册地区-国家"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="注册地区-省" name="registrationProvince">
          <a-input
            v-model:value="formState.registrationProvince"
            placeholder="请输入注册地区-省"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="注册地区-市" name="registrationCity">
          <a-input
            v-model:value="formState.registrationCity"
            placeholder="请输入注册地区-市"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="经营地区-国家" name="businessRegion">
          <a-input
            v-model:value="formState.businessRegion"
            placeholder="请输入经营地区-国家"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="经营地区-省" name="businessProvince">
          <a-input
            v-model:value="formState.businessProvince"
            placeholder="请输入经营地区-省"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="经营地区-市" name="businessCity">
          <a-input
            v-model:value="formState.businessCity"
            placeholder="请输入经营地区-市"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="经营地址" name="businessAddress">
          <a-input
            v-model:value="formState.businessAddress"
            placeholder="请输入经营地址"
            :maxlength="500"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="工厂地址" name="plantAddress">
          <a-input
            v-model:value="formState.plantAddress"
            placeholder="请输入工厂地址"
            :maxlength="500"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="工厂电话" name="plantPhone">
          <a-input
            v-model:value="formState.plantPhone"
            placeholder="请输入工厂电话"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="工厂邮箱" name="plantEmail">
          <a-input
            v-model:value="formState.plantEmail"
            placeholder="请输入工厂邮箱"
            :maxlength="100"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="工厂负责人" name="plantManager">
          <a-input
            v-model:value="formState.plantManager"
            placeholder="请输入工厂负责人"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="企业性质" name="enterpriseNature">
          <a-input
            v-model:value="formState.enterpriseNature"
            placeholder="请输入企业性质"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="行业属性" name="industryAttribute">
          <a-input
            v-model:value="formState.industryAttribute"
            placeholder="请输入行业属性"
            :maxlength="100"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="企业规模" name="enterpriseScale">
          <a-input
            v-model:value="formState.enterpriseScale"
            placeholder="请输入企业规模"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="经营范围" name="businessScope">
          <a-input
            v-model:value="formState.businessScope"
            placeholder="请输入经营范围"
            :maxlength="2000"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="关联公司" name="relatedCompany">
          <a-input
            v-model:value="formState.relatedCompany"
            placeholder="请输入关联公司"
            :maxlength="50"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="工厂状态" name="plantStatus">
          <a-input
            v-model:value="formState.plantStatus"
            placeholder="请输入工厂状态"
            :maxlength="10"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="排序号" name="orderNum">
          <a-input
            v-model:value="formState.orderNum"
            placeholder="请输入排序号"
            :maxlength="10"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="扩展字段JSON" name="extFieldJson">
          <a-input
            v-model:value="formState.extFieldJson"
            placeholder="请输入扩展字段JSON"
            :maxlength="4000"
          />
        </a-form-item>
      </a-col>
      <a-col :span="12">
        <a-form-item label="备注" name="remark">
          <a-input
            v-model:value="formState.remark"
            placeholder="请输入备注"
            :maxlength="500"
          />
        </a-form-item>
      </a-col>
    </a-row>
  </a-form>
</template>

<script setup lang="ts">
/**
 * 工厂表表单组件 · 表单项由 DtoCategory/IsCreate 列驱动
 * @module views/logistics/materials/plant/components
 */
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { Plant } from '@/types/logistics/materials/plant'

const { t } = useI18n()
/** 当前模块 locale 命名空间，用于校验文案 t(localeNs + '.required_xxx') */
const localeNs = 'logistics.materials.plant'

/**
 * @param formData 表单数据（新增为空对象，编辑为当前行）
 * @param loading 提交中状态
 */
const props = defineProps<{
  formData?: Partial<Plant> | null
  loading?: boolean
}>()

/** 表单实例（用于校验与提交） */
const formRef = ref<FormInstance>()
/** 表单数据（双向同步自 formData，用于 v-model 绑定） */
const formState = ref<Record<string, any>>({})

/** 监听 formData 变化，同步到 formState */
watch(() => props.formData, (val) => {
  formState.value = val ? { ...val } : {}
}, { immediate: true, deep: true })

/** 校验规则：IsCreate 且 IsRequired 的列必填，提示文案来自 locale 的 required_xxx */
const rules = computed(() => ({
  plantCode: [{ required: true, message: t(`${localeNs}.required_plantCode`), trigger: 'blur' }],
  plantName: [{ required: true, message: t(`${localeNs}.required_plantName`), trigger: 'blur' }],
  plantStatus: [{ required: true, message: t(`${localeNs}.required_plantStatus`), trigger: 'blur' }],
  orderNum: [{ required: true, message: t(`${localeNs}.required_orderNum`), trigger: 'blur' }],
}))

/**
 * 校验表单并返回当前表单值
 * @returns 校验通过后返回 formState
 */
async function validate() {
  await formRef.value?.validate()
  return formState.value
}

/** 暴露给父组件：validate、formState */
defineExpose({ validate, formState })
</script>
