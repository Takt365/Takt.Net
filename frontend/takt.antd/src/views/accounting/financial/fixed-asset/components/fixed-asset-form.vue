<!-- 固定资产表单组件 -->
<template>
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
          <a-form-item :label="t('entity.fixedasset.code')" name="assetCode">
            <a-input
              v-model:value="formState.assetCode"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.fixedasset.code') })"
              show-count
              :maxlength="50"
              :disabled="!!formData?.fixedAssetsId"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.name')" name="assetName">
            <a-input
              v-model:value="formState.assetName"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.fixedasset.name') })"
              show-count
              :maxlength="200"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.category')" name="assetCategoryId">
            <a-input-number
              v-model:value="formState.assetCategoryId"
              :min="0"
              placeholder="资产类别ID"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.type')" name="assetType">
            <TaktSelect
              v-model:value="formState.assetType"
              dict-type="acct_asset_type"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.fixedasset.type') })"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.originalValue')" name="assetOriginalValue">
            <a-input-number
              v-model:value="formState.assetOriginalValue"
              :min="0"
              :precision="2"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.netValue')" name="assetNetValue">
            <a-input-number
              v-model:value="formState.assetNetValue"
              :min="0"
              :precision="2"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.accumulatedDepreciation')" name="accumulatedDepreciation">
            <a-input-number
              v-model:value="formState.accumulatedDepreciation"
              :min="0"
              :precision="2"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.monthlyDepreciation')" name="monthlyDepreciation">
            <a-input-number
              v-model:value="formState.monthlyDepreciation"
              :min="0"
              :precision="2"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.location')" name="assetLocation">
            <a-input v-model:value="formState.assetLocation" placeholder="" show-count :maxlength="200" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.status')" name="assetStatus">
            <TaktSelect
              v-model:value="formState.assetStatus"
              dict-type="acct_fixed_asset_status"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.fixedasset.status') })"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.purchaseDate')" name="purchaseDate">
            <a-date-picker
              v-model:value="formState.purchaseDate"
              value-format="YYYY-MM-DD"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.startDate')" name="startDate">
            <a-date-picker
              v-model:value="formState.startDate"
              value-format="YYYY-MM-DD"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.expectedLifeMonths')" name="expectedLifeMonths">
            <a-input-number
              v-model:value="formState.expectedLifeMonths"
              :min="0"
              placeholder="月"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.fixedasset.depreciationMethod')" name="depreciationMethod">
            <TaktSelect
              v-model:value="formState.depreciationMethod"
              dict-type="acct_depreciation_method"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.fixedasset.depreciationMethod') })"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('common.entity.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
            <a-textarea v-model:value="formState.remark" :rows="3" show-count :maxlength="500" />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { FixedAsset, FixedAssetCreate } from '@/types/accounting/financial/fixed-asset'

const { t } = useI18n()

interface Props {
  formData?: Partial<FixedAsset>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const TOTAL_FIELDS = 12
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  assetCode?: string
  assetName?: string
  assetCategoryId?: number
  assetType?: number
  assetOriginalValue?: number
  assetNetValue?: number
  accumulatedDepreciation?: number
  assetLocation?: string
  purchaseDate?: string
  startDate?: string
  expectedLifeMonths?: number
  depreciationMethod?: number
  monthlyDepreciation?: number
  assetStatus?: number
  remark?: string
}

const formState = reactive<FormState>({
  assetCode: '',
  assetName: '',
  assetCategoryId: 0,
  assetType: 0,
  assetOriginalValue: 0,
  assetNetValue: 0,
  accumulatedDepreciation: 0,
  assetLocation: '',
  purchaseDate: undefined,
  startDate: undefined,
  expectedLifeMonths: 0,
  depreciationMethod: 0,
  monthlyDepreciation: 0,
  assetStatus: 0,
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  assetCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.fixedasset.code') }), trigger: 'blur' }
  ],
  assetName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.fixedasset.name') }), trigger: 'blur' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        assetCode: newData.assetCode ?? '',
        assetName: newData.assetName ?? '',
        assetCategoryId: newData.assetCategoryId != null ? Number(newData.assetCategoryId) : 0,
        assetType: newData.assetType ?? 0,
        assetOriginalValue: newData.assetOriginalValue ?? 0,
        assetNetValue: newData.assetNetValue ?? 0,
        accumulatedDepreciation: newData.accumulatedDepreciation ?? 0,
        assetLocation: newData.assetLocation ?? '',
        purchaseDate: (newData.purchaseDate as string) ?? undefined,
        startDate: (newData.startDate as string) ?? undefined,
        expectedLifeMonths: newData.expectedLifeMonths ?? 0,
        depreciationMethod: newData.depreciationMethod ?? 0,
        monthlyDepreciation: newData.monthlyDepreciation ?? 0,
        assetStatus: newData.assetStatus ?? 0,
        remark: newData.remark ?? ''
      })
    } else {
      Object.assign(formState, {
        assetCode: '',
        assetName: '',
        assetCategoryId: 0,
        assetType: 0,
        assetOriginalValue: 0,
        assetNetValue: 0,
        accumulatedDepreciation: 0,
        assetLocation: '',
        purchaseDate: undefined,
        startDate: undefined,
        expectedLifeMonths: 0,
        depreciationMethod: 0,
        monthlyDepreciation: 0,
        assetStatus: 0,
        remark: ''
      })
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): FixedAssetCreate & { fixedAssetsId?: string } => ({
  assetCode: formState.assetCode ?? '',
  assetName: formState.assetName ?? '',
  assetCategoryId: String(formState.assetCategoryId ?? 0),
  assetType: formState.assetType ?? 0,
  assetOriginalValue: formState.assetOriginalValue ?? 0,
  assetNetValue: formState.assetNetValue ?? 0,
  accumulatedDepreciation: formState.accumulatedDepreciation ?? 0,
  assetLocation: formState.assetLocation || undefined,
  purchaseDate: formState.purchaseDate || undefined,
  startDate: formState.startDate || undefined,
  expectedLifeMonths: formState.expectedLifeMonths ?? 0,
  depreciationMethod: formState.depreciationMethod ?? 0,
  monthlyDepreciation: formState.monthlyDepreciation ?? 0,
  assetStatus: formState.assetStatus ?? 0,
  remark: formState.remark || undefined,
  ...(props.formData?.fixedAssetsId ? { fixedAssetsId: props.formData.fixedAssetsId } : {})
})

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    assetCode: '',
    assetName: '',
    assetCategoryId: 0,
    assetType: 0,
    assetOriginalValue: 0,
    assetNetValue: 0,
    accumulatedDepreciation: 0,
    assetLocation: '',
    purchaseDate: undefined,
    startDate: undefined,
    expectedLifeMonths: 0,
    depreciationMethod: 0,
    monthlyDepreciation: 0,
    assetStatus: 0,
    remark: ''
  })
}

defineExpose({ validate, getValues, resetFields })
</script>
