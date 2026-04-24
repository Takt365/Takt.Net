<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/i18n/language/components -->
<!-- 文件名称：language-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：语言表单组件，包含主表和子表（翻译） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="language-form">
    <a-tabs v-model:active-key="activeTab">
      <!-- 主表：语言信息 -->
      <a-tab-pane
        key="main"
        :tab="t('routine.localization.language.tabs.main')"
      >
        <a-form
          ref="mainFormRef"
          :model="mainFormState"
          :rules="mainFormRules"
          :label-col="{ span: 4 }"
          :wrapper-col="{ span: 20 }"
          layout="horizontal"
        >
          <!-- 表单字段顺序与 Language 接口字段顺序一致 -->
          <a-form-item
            :label="t('entity.language.name')"
            name="languageName"
          >
            <a-input
              v-model:value="mainFormState.languageName"
              :placeholder="t('routine.localization.language.placeholders.languageName')"
            />
          </a-form-item>

          <a-form-item
            :label="t('entity.language.culturecode')"
            name="cultureCode"
          >
            <a-input
              v-model:value="mainFormState.cultureCode"
              :placeholder="t('routine.localization.language.placeholders.cultureCode')"
              :disabled="!!props.formData?.languageId"
            />
          </a-form-item>

          <a-form-item
            :label="t('entity.language.nativename')"
            name="nativeName"
          >
            <a-input
              v-model:value="mainFormState.nativeName"
              :placeholder="t('routine.localization.language.placeholders.nativeName')"
            />
          </a-form-item>

          <a-form-item
            :label="t('entity.language.icon')"
            name="languageIcon"
          >
            <a-input
              v-model:value="mainFormState.languageIcon"
              :placeholder="t('routine.localization.language.placeholders.languageIcon')"
            />
          </a-form-item>

          <a-form-item
            :label="t('entity.language.ordernum')"
            name="orderNum"
          >
            <a-input-number
              v-model:value="mainFormState.orderNum"
              :min="0"
              :placeholder="t('routine.localization.language.placeholders.orderNum')"
              style="width: 100%"
            />
          </a-form-item>

          <a-form-item
            :label="t('entity.language.status')"
            name="languageStatus"
          >
            <TaktSelect
              v-model:value="mainFormState.languageStatus"
              api-url="/api/TaktDictDatas/options?dictTypeCode=sys_status"
              :field-names="{ label: 'dictLabel', value: 'extLabel' }"
              allow-clear
              :placeholder="t('common.form.placeholder.select', { field: t('entity.language.status') })"
            />
          </a-form-item>

          <a-form-item
            :label="t('entity.language.isdefault')"
            name="isDefault"
          >
            <TaktSelect
              v-model:value="mainFormState.isDefault"
              api-url="/api/TaktDictDatas/options?dictTypeCode=sys_yes_no"
              :field-names="{ label: 'dictLabel', value: 'extLabel' }"
              allow-clear
              :placeholder="t('common.form.placeholder.select', { field: t('entity.language.isdefault') })"
            />
          </a-form-item>

          <a-form-item
            :label="t('entity.language.isrtl')"
            name="isRtl"
          >
            <TaktSelect
              v-model:value="mainFormState.isRtl"
              api-url="/api/TaktDictDatas/options?dictTypeCode=sys_yes_no"
              :field-names="{ label: 'dictLabel', value: 'extLabel' }"
              allow-clear
              :placeholder="t('routine.localization.language.placeholders.isRtlSelect')"
            />
          </a-form-item>

          <a-form-item
            :label="t('common.entity.remark')"
            name="remark"
          >
            <a-textarea
              v-model:value="mainFormState.remark"
              :placeholder="t('routine.localization.language.placeholders.remark')"
              :rows="3"
            />
          </a-form-item>
        </a-form>
      </a-tab-pane>

      <!-- 子表：翻译列表 -->
      <a-tab-pane
        key="translation"
        :tab="t('routine.localization.language.tabs.translation')"
      >
        <div class="translation-toolbar">
          <a-button
            type="primary"
            @click="handleAddTranslation"
          >
            <template #icon>
              <PlusOutlined />
            </template>
            {{ t('routine.localization.language.typeForm.addTranslation') }}
          </a-button>
        </div>

        <a-table
          :columns="translationColumns"
          :data-source="translationList"
          :pagination="false"
          row-key="translationId"
          size="small"
        >
          <template #bodyCell="{ column, record, index }">
            <template v-if="column.key === 'resourceKey'">
              <a-input
                v-model:value="record.resourceKey"
                :placeholder="t('routine.localization.language.placeholders.translationResourceKey')"
                size="small"
              />
            </template>
            <template v-else-if="column.key === 'cultureCode'">
              <a-input
                v-model:value="record.cultureCode"
                :placeholder="t('routine.localization.language.placeholders.translationCultureCode')"
                size="small"
                :disabled="true"
              />
            </template>
            <template v-else-if="column.key === 'translationValue'">
              <a-input
                v-model:value="record.translationValue"
                :placeholder="t('routine.localization.language.placeholders.translationValue')"
                size="small"
              />
            </template>
            <template v-else-if="column.key === 'resourceType'">
              <a-input
                v-model:value="record.resourceType"
                :placeholder="t('routine.localization.language.placeholders.translationResourceType')"
                size="small"
              />
            </template>
            <template v-else-if="column.key === 'resourceGroup'">
              <a-input
                v-model:value="record.resourceGroup"
                :placeholder="t('routine.localization.language.placeholders.translationResourceGroup')"
                size="small"
              />
            </template>
            <template v-else-if="column.key === 'orderNum'">
              <a-input-number
                v-model:value="record.orderNum"
                :min="0"
                size="small"
                style="width: 100%"
              />
            </template>
            <template v-else-if="column.key === 'action'">
              <a-button
                type="link"
                danger
                size="small"
                @click="handleRemoveTranslation(index)"
              >
                {{ t('common.button.delete') }}
              </a-button>
            </template>
          </template>
        </a-table>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TableColumnsType } from 'ant-design-vue'
import type { Language, LanguageCreate, LanguageUpdate } from '@/types/routine/tasks/i18n/language'

/** 子表行：仅编辑所需字段，不要求完整 Translation/TaktEntityBase */
export interface LanguageTranslationInlineRow {
  translationId: string
  resourceKey: string
  languageId: string
  cultureCode: string
  translationValue: string
  resourceType: string
  resourceGroup: string
  orderNum: number
  remark?: string
}

type MainFormState = Omit<LanguageCreate, 'languageIcon' | 'remark'> & {
  languageId?: string
  languageIcon: string
  remark: string
}

// ========================================
// Props & Emits
// ========================================

interface Props {
  formData?: Language | null
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: null,
  loading: false
})

const { t } = useI18n()

// ========================================
// 数据定义
// ========================================

const activeTab = ref('main')
const mainFormRef = ref()
const translationList = ref<LanguageTranslationInlineRow[]>([])

// 表单状态（与 Language 接口字段顺序一致）
const mainFormState = reactive<MainFormState>({
  languageName: '',
  cultureCode: '',
  nativeName: '',
  languageIcon: '',
  orderNum: 0,
  languageStatus: 0,
  isDefault: 1,
  isRtl: 1,
  remark: ''
})

const mainFormRules = computed<Record<string, Rule[]>>(() => ({
  cultureCode: [
    { required: true, message: t('routine.localization.language.rules.cultureCodeRequired'), trigger: 'blur' }
  ],
  languageName: [
    { required: true, message: t('routine.localization.language.rules.languageNameRequired'), trigger: 'blur' }
  ],
  nativeName: [
    { required: true, message: t('routine.localization.language.rules.nativeNameRequired'), trigger: 'blur' }
  ]
}))

// 翻译子表列定义（与 Translation 接口字段顺序一致；列标题走后端种子 entity.translation.*）
const translationColumns = computed<TableColumnsType<LanguageTranslationInlineRow>>(() => [
  {
    title: t('entity.translation.resourcekey'),
    dataIndex: 'resourceKey',
    key: 'resourceKey',
    width: 200
  },
  {
    title: t('entity.translation.culturecode'),
    dataIndex: 'cultureCode',
    key: 'cultureCode',
    width: 150
  },
  {
    title: t('entity.translation.translationvalue'),
    dataIndex: 'translationValue',
    key: 'translationValue',
    width: 300
  },
  {
    title: t('entity.translation.resourcetype'),
    dataIndex: 'resourceType',
    key: 'resourceType',
    width: 120
  },
  {
    title: t('entity.translation.resourcegroup'),
    dataIndex: 'resourceGroup',
    key: 'resourceGroup',
    width: 150,
    ellipsis: true
  },
  {
    title: t('entity.translation.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('common.action.operation'),
    key: 'action',
    width: 80,
    fixed: 'right'
  }
])

// ========================================
// 方法定义
// ========================================

// 监听 formData 变化
watch(
  () => props.formData,
  (newData) => {
    if (newData) {
      // 编辑模式：填充主表数据（按 Language 接口字段顺序）
      Object.assign(mainFormState, {
        languageId: newData.languageId,
        languageName: newData.languageName || '',
        cultureCode: newData.cultureCode || '',
        nativeName: newData.nativeName || '',
        languageIcon: newData.languageIcon || '',
        orderNum: newData.orderNum ?? 0,
        languageStatus: newData.languageStatus ?? 0,
        isDefault: newData.isDefault ?? 1,
        isRtl: newData.isRtl ?? 1,
        remark: newData.remark || ''
      })

      // 填充子表数据
      translationList.value = Array.isArray(newData.translationList)
        ? newData.translationList.map((item: unknown) => {
            const o = item as Record<string, unknown>
            const row: LanguageTranslationInlineRow = {
              translationId: String(o.translationId ?? ''),
              resourceKey: String(o.resourceKey ?? ''),
              languageId: String(o.languageId ?? ''),
              cultureCode: String(o.cultureCode ?? ''),
              translationValue: String(o.translationValue ?? ''),
              resourceType: String(o.resourceType ?? 'Frontend'),
              resourceGroup: String(o.resourceGroup ?? ''),
              orderNum: typeof o.orderNum === 'number' ? o.orderNum : Number(o.orderNum ?? 0)
            }
            if (o.remark != null && o.remark !== '') {
              row.remark = String(o.remark)
            }
            return row
          })
        : []
    } else {
      // 新增模式：重置表单（按 Language 接口字段顺序）
      Object.assign(mainFormState, {
        languageId: undefined,
        languageName: '',
        cultureCode: '',
        nativeName: '',
        languageIcon: '',
        orderNum: 0,
        languageStatus: 0,
        isDefault: 1,
        isRtl: 1,
        remark: ''
      })
      translationList.value = []
    }
  },
  { immediate: true, deep: true }
)

// 新增翻译（按 Translation 接口字段顺序）
const handleAddTranslation = () => {
  translationList.value.push({
    translationId: `temp_${Date.now()}_${Math.random()}`,
    resourceKey: '',
    languageId: '',
    cultureCode: mainFormState.cultureCode || '',
    translationValue: '',
    resourceType: 'Frontend',
    resourceGroup: '',
    orderNum: translationList.value.length
  })
}

// 删除翻译
const handleRemoveTranslation = (index: number) => {
  translationList.value.splice(index, 1)
}

// 表单验证
const validate = async () => {
  await mainFormRef.value?.validate()
  
  // 验证子表数据
  for (let i = 0; i < translationList.value.length; i++) {
    const item = translationList.value[i]
    if (!item) continue
    if (!item.resourceKey || !item.translationValue) {
      throw new Error(t('routine.localization.language.messages.translationRowInvalid', { row: i + 1 }))
    }
  }
}

// 获取表单数据（按 Language 和 Translation 接口字段顺序）
const getFormData = (): LanguageCreate | LanguageUpdate => {
  const baseData: any = {
    languageName: mainFormState.languageName,
    cultureCode: mainFormState.cultureCode,
    nativeName: mainFormState.nativeName,
    languageIcon: mainFormState.languageIcon || undefined,
    orderNum: mainFormState.orderNum,
    languageStatus: mainFormState.languageStatus,
    isDefault: mainFormState.isDefault,
    isRtl: mainFormState.isRtl,
    remark: mainFormState.remark || undefined,
    translationList: translationList.value
      .filter(item => item.resourceKey && item.translationValue) // 过滤空数据
      .map(item => ({
        resourceKey: item.resourceKey,
        languageId: item.languageId || '',
        cultureCode: mainFormState.cultureCode,
        translationValue: item.translationValue,
        resourceType: item.resourceType || 'Frontend',
        resourceGroup: item.resourceGroup || undefined,
        orderNum: item.orderNum ?? 0,
        remark: item.remark || undefined
      }))
  }
  
  if (mainFormState.languageId) {
    baseData.languageId = mainFormState.languageId
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
.language-form {
  .translation-toolbar {
    margin-bottom: 16px;
  }
}
</style>
