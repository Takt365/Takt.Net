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
        tab="语言信息"
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
            label="语言名称"
            name="languageName"
          >
            <a-input
              v-model:value="mainFormState.languageName"
              placeholder="请输入语言名称（中文名称，如：简体中文）"
            />
          </a-form-item>

          <a-form-item
            label="语言编码"
            name="cultureCode"
          >
            <a-input
              v-model:value="mainFormState.cultureCode"
              placeholder="请输入语言编码（如：zh-CN、en-US）"
              :disabled="!!props.formData?.languageId"
            />
          </a-form-item>

          <a-form-item
            label="本地化名称"
            name="nativeName"
          >
            <a-input
              v-model:value="mainFormState.nativeName"
              placeholder="请输入本地化名称（该语言下的名称，如：简体中文、English）"
            />
          </a-form-item>

          <a-form-item
            label="语言图标"
            name="languageIcon"
          >
            <a-input
              v-model:value="mainFormState.languageIcon"
              placeholder="请输入语言图标URL（可选）"
            />
          </a-form-item>

          <a-form-item
            label="排序号"
            name="orderNum"
          >
            <a-input-number
              v-model:value="mainFormState.orderNum"
              :min="0"
              placeholder="请输入排序号"
              style="width: 100%"
            />
          </a-form-item>

          <a-form-item
            label="语言状态"
            name="languageStatus"
          >
            <TaktSelect
              v-model:value="mainFormState.languageStatus"
              dict-type="sys_status"
              placeholder="请选择语言状态"
            />
          </a-form-item>

          <a-form-item
            label="是否默认"
            name="isDefault"
          >
            <TaktSelect
              v-model:value="mainFormState.isDefault"
              dict-type="sys_yes_no"
              placeholder="请选择是否默认语言"
            />
          </a-form-item>

          <a-form-item
            label="是否RTL"
            name="isRtl"
          >
            <TaktSelect
              v-model:value="mainFormState.isRtl"
              dict-type="sys_yes_no"
              placeholder="请选择是否启用RTL（从右到左）"
            />
          </a-form-item>

          <a-form-item
            label="备注"
            name="remark"
          >
            <a-textarea
              v-model:value="mainFormState.remark"
              placeholder="请输入备注"
              :rows="3"
            />
          </a-form-item>
        </a-form>
      </a-tab-pane>

      <!-- 子表：翻译列表 -->
      <a-tab-pane
        key="translation"
        tab="翻译列表"
      >
        <div class="translation-toolbar">
          <a-button
            type="primary"
            @click="handleAddTranslation"
          >
            <template #icon>
              <PlusOutlined />
            </template>
            新增翻译
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
                placeholder="请输入资源键"
                size="small"
              />
            </template>
            <template v-else-if="column.key === 'cultureCode'">
              <a-input
                v-model:value="record.cultureCode"
                placeholder="语言编码"
                size="small"
                :disabled="true"
              />
            </template>
            <template v-else-if="column.key === 'translationValue'">
              <a-input
                v-model:value="record.translationValue"
                placeholder="请输入翻译值"
                size="small"
              />
            </template>
            <template v-else-if="column.key === 'resourceType'">
              <a-input
                v-model:value="record.resourceType"
                placeholder="资源类型（Frontend/Backend）"
                size="small"
              />
            </template>
            <template v-else-if="column.key === 'resourceGroup'">
              <a-input
                v-model:value="record.resourceGroup"
                placeholder="请输入资源分组（可选）"
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
                删除
              </a-button>
            </template>
          </template>
        </a-table>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TableColumnsType } from 'ant-design-vue'
import type { Language, LanguageCreate, LanguageUpdate } from '@/types/routine/tasks/i18n/language'
import type { Translation } from '@/types/routine/tasks/i18n/translation'

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

// ========================================
// 数据定义
// ========================================

const activeTab = ref('main')
const mainFormRef = ref()
const translationList = ref<Translation[]>([])

// 表单状态（与 Language 接口字段顺序一致）
const mainFormState = reactive<LanguageCreate & { languageId?: string }>({
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

const mainFormRules: Record<string, Rule[]> = {
  cultureCode: [
    { required: true, message: '请输入语言编码', trigger: 'blur' }
  ],
  languageName: [
    { required: true, message: '请输入语言名称', trigger: 'blur' }
  ],
  nativeName: [
    { required: true, message: '请输入本地化名称', trigger: 'blur' }
  ]
}

// 翻译子表列定义（与 Translation 接口字段顺序一致）
const translationColumns: TableColumnsType = [
  {
    title: '资源键',
    dataIndex: 'resourceKey',
    key: 'resourceKey',
    width: 200
  },
  {
    title: '语言编码',
    dataIndex: 'cultureCode',
    key: 'cultureCode',
    width: 150
  },
  {
    title: '翻译值',
    dataIndex: 'translationValue',
    key: 'translationValue',
    width: 300
  },
  {
    title: '资源类型',
    dataIndex: 'resourceType',
    key: 'resourceType',
    width: 120
  },
  {
    title: '资源分组',
    dataIndex: 'resourceGroup',
    key: 'resourceGroup',
    width: 150,
    ellipsis: true
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: '操作',
    key: 'action',
    width: 80,
    fixed: 'right'
  }
]

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
      translationList.value = newData.translationList
        ? newData.translationList.map(item => ({ ...item }))
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
    isApproved: 1,
    approveBy: '',
    approveTime: '',
    orderNum: translationList.value.length
  } as Translation)
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
    if (!item.resourceKey || !item.translationValue) {
      throw new Error(`第 ${i + 1} 行翻译：资源键和翻译值不能为空`)
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
