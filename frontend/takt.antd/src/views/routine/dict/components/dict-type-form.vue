<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/dict/type/components -->
<!-- 文件名称：dict-type-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：字典类型表单组件，包含主表和子表（字典数据） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="dict-type-form">
    <a-tabs v-model:activeKey="activeTab">
      <!-- 主表：字典类型信息 -->
      <a-tab-pane key="main" tab="字典类型信息">
        <a-form
          ref="mainFormRef"
          :model="mainFormState"
          :rules="mainFormRules"
          :label-col="{ span: 4 }"
          :wrapper-col="{ span: 20 }"
          layout="horizontal"
        >
          <a-form-item label="字典类型编码" name="dictTypeCode">
            <a-input
              v-model:value="mainFormState.dictTypeCode"
              placeholder="请输入字典类型编码"
              :disabled="!!props.formData?.dictTypeId"
            />
          </a-form-item>

          <a-form-item label="字典类型名称" name="dictTypeName">
            <a-input
              v-model:value="mainFormState.dictTypeName"
              placeholder="请输入字典类型名称"
            />
          </a-form-item>

          <a-form-item label="数据源" name="dataSource">
            <TaktSelect
              v-model:value="mainFormState.dataSource"
              dict-type="sys_data_source"
              placeholder="请选择数据源"
            />
          </a-form-item>

          <a-form-item
            v-if="mainFormState.dataSource === 1"
            label="数据库配置ID"
            name="dataConfigId"
          >
            <a-input
              v-model:value="mainFormState.dataConfigId"
              placeholder="请输入数据库配置ID（当数据源为SQL查询时）"
            />
          </a-form-item>

          <a-form-item
            v-if="mainFormState.dataSource === 1"
            label="SQL脚本"
            name="sqlScript"
          >
            <a-textarea
              v-model:value="mainFormState.sqlScript"
              placeholder="请输入SQL脚本"
              :rows="4"
            />
          </a-form-item>

          <a-form-item label="是否内置" name="isBuiltIn">
            <TaktSelect
              v-model:value="mainFormState.isBuiltIn"
              dict-type="sys_yes_no"
              placeholder="请选择是否内置"
            />
          </a-form-item>

          <a-form-item label="排序号" name="orderNum">
            <a-input-number
              v-model:value="mainFormState.orderNum"
              :min="0"
              placeholder="请输入排序号"
              style="width: 100%"
            />
          </a-form-item>

          <a-form-item label="类型状态" name="dictTypeStatus">
            <TaktSelect
              v-model:value="mainFormState.dictTypeStatus"
              dict-type="sys_status"
              placeholder="请选择类型状态"
            />
          </a-form-item>

          <a-form-item :label="t('common.entity.remark')" name="remark">
            <a-textarea
              v-model:value="mainFormState.remark"
              :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
              :rows="3"
            />
          </a-form-item>
        </a-form>
      </a-tab-pane>

      <!-- 子表：字典数据列表 -->
      <a-tab-pane key="data" tab="字典数据">
        <div class="dict-data-toolbar">
          <a-button type="primary" @click="handleAddDictData">
            <template #icon><PlusOutlined /></template>
            新增字典数据
          </a-button>
        </div>

        <a-table
          :columns="dictDataColumns"
          :data-source="dictDataList"
          :pagination="false"
          row-key="dictDataId"
          size="small"
        >
          <template #bodyCell="{ column, record, index }">
            <!-- 字典类型ID - 只读 -->
            <template v-if="column.key === 'dictTypeId'">
              <span style="color: #999">{{ record.dictTypeId || '-' }}</span>
            </template>
            <!-- 字典类型编码 - 只读 -->
            <template v-else-if="column.key === 'dictTypeCode'">
              <span>{{ record.dictTypeCode }}</span>
            </template>
            <!-- 字典标签 - 可编辑 -->
            <template v-else-if="column.key === 'dictLabel'">
              <a-input
                v-if="editingKey === `${record.dictDataId || index}-dictLabel`"
                v-model:value="editingRecord.dictLabel"
                size="small"
                @blur="handleSaveCell(record, index, 'dictLabel')"
                @pressEnter="handleSaveCell(record, index, 'dictLabel')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'dictLabel')"
              >
                {{ record.dictLabel || '-' }}
              </span>
            </template>
            <!-- 字典本地化键 - 可编辑 -->
            <template v-else-if="column.key === 'dictL10nKey'">
              <a-input
                v-if="editingKey === `${record.dictDataId || index}-dictL10nKey`"
                v-model:value="editingRecord.dictL10nKey"
                size="small"
                @blur="handleSaveCell(record, index, 'dictL10nKey')"
                @pressEnter="handleSaveCell(record, index, 'dictL10nKey')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'dictL10nKey')"
              >
                {{ record.dictL10nKey || '-' }}
              </span>
            </template>
            <!-- 字典值 - 可编辑 -->
            <template v-else-if="column.key === 'dictValue'">
              <a-input
                v-if="editingKey === `${record.dictDataId || index}-dictValue`"
                v-model:value="editingRecord.dictValue"
                size="small"
                @blur="handleSaveCell(record, index, 'dictValue')"
                @pressEnter="handleSaveCell(record, index, 'dictValue')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'dictValue')"
              >
                {{ record.dictValue || '-' }}
              </span>
            </template>
            <!-- CSS类名 - 可编辑 -->
            <template v-else-if="column.key === 'cssClass'">
              <a-input-number
                v-if="editingKey === `${record.dictDataId || index}-cssClass`"
                v-model:value="editingRecord.cssClass"
                :min="0"
                size="small"
                style="width: 100%"
                @blur="handleSaveCell(record, index, 'cssClass')"
                @pressEnter="handleSaveCell(record, index, 'cssClass')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'cssClass')"
              >
                {{ record.cssClass ?? 0 }}
              </span>
            </template>
            <!-- 列表类名 - 可编辑 -->
            <template v-else-if="column.key === 'listClass'">
              <a-input-number
                v-if="editingKey === `${record.dictDataId || index}-listClass`"
                v-model:value="editingRecord.listClass"
                :min="0"
                size="small"
                style="width: 100%"
                @blur="handleSaveCell(record, index, 'listClass')"
                @pressEnter="handleSaveCell(record, index, 'listClass')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'listClass')"
              >
                {{ record.listClass ?? 0 }}
              </span>
            </template>
            <!-- 扩展标签 - 可编辑 -->
            <template v-else-if="column.key === 'extLabel'">
              <a-input
                v-if="editingKey === `${record.dictDataId || index}-extLabel`"
                v-model:value="editingRecord.extLabel"
                size="small"
                @blur="handleSaveCell(record, index, 'extLabel')"
                @pressEnter="handleSaveCell(record, index, 'extLabel')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'extLabel')"
              >
                {{ record.extLabel || '-' }}
              </span>
            </template>
            <!-- 扩展值 - 可编辑 -->
            <template v-else-if="column.key === 'extValue'">
              <a-input
                v-if="editingKey === `${record.dictDataId || index}-extValue`"
                v-model:value="editingRecord.extValue"
                size="small"
                @blur="handleSaveCell(record, index, 'extValue')"
                @pressEnter="handleSaveCell(record, index, 'extValue')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'extValue')"
              >
                {{ record.extValue || '-' }}
              </span>
            </template>
            <!-- 排序号 - 可编辑 -->
            <template v-else-if="column.key === 'orderNum'">
              <a-input-number
                v-if="editingKey === `${record.dictDataId || index}-orderNum`"
                v-model:value="editingRecord.orderNum"
                :min="0"
                size="small"
                style="width: 100%"
                @blur="handleSaveCell(record, index, 'orderNum')"
                @pressEnter="handleSaveCell(record, index, 'orderNum')"
                @keydown.esc="handleCancelEdit"
              />
              <span
                v-else
                style="cursor: pointer; padding: 4px 8px; display: inline-block; min-height: 24px; width: 100%"
                @click="handleStartEdit(record, index, 'orderNum')"
              >
                {{ record.orderNum ?? 0 }}
              </span>
            </template>
            <!-- 操作列 -->
            <template v-else-if="column.key === 'action'">
              <a-button
                type="link"
                danger
                size="small"
                @click="handleRemoveDictData(index)"
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
import { useI18n } from 'vue-i18n'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TableColumnsType } from 'ant-design-vue'
import type { DictType, DictTypeCreate, DictTypeUpdate } from '@/types/routine/dict/dicttype'
import type { DictData } from '@/types/routine/dict/dictdata'

// ========================================
// Props & Emits
// ========================================

interface Props {
  formData?: DictType | null
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
const dictDataList = ref<DictData[]>([])

// 行内编辑状态
const editingKey = ref<string>('')
const editingRecord = ref<Partial<DictData>>({})
const editingIndex = ref<number>(-1)

const mainFormState = reactive<DictTypeCreate & { dictTypeId?: string; dictTypeStatus?: number }>({
  dictTypeCode: '',
  dictTypeName: '',
  dataSource: 0,
  dataConfigId: '',
  sqlScript: '',
  isBuiltIn: 1,
  orderNum: 0,
  dictTypeStatus: 0,
  remark: ''
})

const mainFormRules: Record<string, Rule[]> = {
  dictTypeCode: [
    { required: true, message: '请输入字典类型编码', trigger: 'blur' }
  ],
  dictTypeName: [
    { required: true, message: '请输入字典类型名称', trigger: 'blur' }
  ],
  dataSource: [
    { required: true, message: '请选择数据源', trigger: 'change' }
  ],
  sqlScript: [
    {
      validator: (_rule, value) => {
        if (mainFormState.dataSource === 1 && !value) {
          return Promise.reject('数据源为SQL查询时，SQL脚本不能为空')
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ]
}

// 字典数据子表列定义（与 DictData 接口字段顺序一致）
const dictDataColumns: TableColumnsType = [
  {
    title: '字典类型ID',
    dataIndex: 'dictTypeId',
    key: 'dictTypeId',
    width: 120
  },
  {
    title: '字典类型编码',
    dataIndex: 'dictTypeCode',
    key: 'dictTypeCode',
    width: 150
  },
  {
    title: '字典标签',
    dataIndex: 'dictLabel',
    key: 'dictLabel',
    width: 150
  },
  {
    title: '字典本地化键',
    dataIndex: 'dictL10nKey',
    key: 'dictL10nKey',
    width: 200,
    ellipsis: true
  },
  {
    title: '字典值',
    dataIndex: 'dictValue',
    key: 'dictValue',
    width: 150
  },
  {
    title: 'CSS类名',
    dataIndex: 'cssClass',
    key: 'cssClass',
    width: 100
  },
  {
    title: '列表类名',
    dataIndex: 'listClass',
    key: 'listClass',
    width: 100
  },
  {
    title: '扩展标签',
    dataIndex: 'extLabel',
    key: 'extLabel',
    width: 150,
    ellipsis: true
  },
  {
    title: '扩展值',
    dataIndex: 'extValue',
    key: 'extValue',
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
      // 编辑模式：填充主表数据（按 DictType 接口字段顺序）
      Object.assign(mainFormState, {
        dictTypeId: newData.dictTypeId,
        dictTypeCode: newData.dictTypeCode || '',
        dictTypeName: newData.dictTypeName || '',
        dataSource: newData.dataSource ?? 0,
        dataConfigId: newData.dataConfigId || '',
        sqlScript: newData.sqlScript || '',
        isBuiltIn: newData.isBuiltIn ?? 1,
        orderNum: newData.orderNum ?? 0,
        dictTypeStatus: newData.dictTypeStatus ?? 0,
        remark: newData.remark || ''
      })

      // 填充子表数据
      dictDataList.value = newData.dictDataList
        ? newData.dictDataList.map(item => ({ ...item }))
        : []
    } else {
      // 新增模式：重置表单（按 DictType 接口字段顺序）
      Object.assign(mainFormState, {
        dictTypeId: undefined,
        dictTypeCode: '',
        dictTypeName: '',
        dataSource: 0,
        dataConfigId: '',
        sqlScript: '',
        isBuiltIn: 1,
        orderNum: 0,
        dictTypeStatus: 0,
        remark: ''
      })
      dictDataList.value = []
    }
  },
  { immediate: true, deep: true }
)

// 新增字典数据（按 DictData 接口字段顺序）
const handleAddDictData = () => {
  dictDataList.value.push({
    dictDataId: `temp_${Date.now()}_${Math.random()}`,
    dictTypeId: '',
    dictTypeCode: mainFormState.dictTypeCode || '',
    dictLabel: '',
    dictL10nKey: '',
    dictValue: '',
    cssClass: 0,
    listClass: 0,
    extLabel: '',
    extValue: '',
    orderNum: dictDataList.value.length
  } as DictData)
}

// 删除字典数据
const handleRemoveDictData = (index: number) => {
  dictDataList.value.splice(index, 1)
}

// 表单验证
const validate = async () => {
  await mainFormRef.value?.validate()
  
  // 验证子表数据
  for (let i = 0; i < dictDataList.value.length; i++) {
    const item = dictDataList.value[i]
    if (!item.dictLabel || !item.dictValue) {
      throw new Error(`第 ${i + 1} 行字典数据：字典标签和字典值不能为空`)
    }
  }
}

// 获取表单数据（按 DictType 和 DictData 接口字段顺序）
const getFormData = (): DictTypeCreate | DictTypeUpdate => {
  const baseData: any = {
    dictTypeCode: mainFormState.dictTypeCode,
    dictTypeName: mainFormState.dictTypeName,
    dataSource: mainFormState.dataSource,
    dataConfigId: mainFormState.dataConfigId || undefined,
    sqlScript: mainFormState.sqlScript || undefined,
    isBuiltIn: mainFormState.isBuiltIn,
    orderNum: mainFormState.orderNum,
    remark: mainFormState.remark || undefined,
    dictDataList: dictDataList.value
      .filter(item => item.dictLabel && item.dictValue) // 过滤空数据
      .map(item => ({
        dictTypeId: item.dictTypeId || '',
        dictTypeCode: mainFormState.dictTypeCode,
        dictLabel: item.dictLabel,
        dictL10nKey: item.dictL10nKey || undefined,
        dictValue: item.dictValue,
        cssClass: item.cssClass ?? 0,
        listClass: item.listClass ?? 0,
        extLabel: item.extLabel || undefined,
        extValue: item.extValue || undefined,
        orderNum: item.orderNum ?? 0,
        remark: item.remark || undefined
      }))
  }
  
  if (mainFormState.dictTypeId) {
    baseData.dictTypeId = mainFormState.dictTypeId
  }
  
  return baseData
}

// ========================================
// 行内编辑方法
// ========================================

// 开始编辑单元格
const handleStartEdit = (record: any, index: number, field: keyof DictData) => {
  const key = `${record.dictDataId || index}-${field}`
  editingKey.value = key
  editingIndex.value = index
  editingRecord.value = {
    [field]: record[field]
  }
}

// 保存单元格（表单中的子表，直接更新本地数据，不需要调用API）
const handleSaveCell = (record: any, index: number, field: keyof DictData) => {
  const key = `${record.dictDataId || index}-${field}`
  if (editingKey.value !== key) {
    return
  }
  
  const newValue = editingRecord.value[field]
  
  // 验证必填字段
  if ((field === 'dictLabel' || field === 'dictValue') && !newValue) {
    // 恢复原值
    editingRecord.value[field] = record[field]
    handleCancelEdit()
    return
  }
  
  // 更新本地数据
  if (index >= 0 && index < dictDataList.value.length) {
    dictDataList.value[index] = {
      ...dictDataList.value[index],
      [field]: newValue
    } as DictData
  }
  
  // 清除编辑状态
  handleCancelEdit()
}

// 取消编辑
const handleCancelEdit = () => {
  if (editingIndex.value >= 0 && editingIndex.value < dictDataList.value.length) {
    // 如果取消编辑，恢复原值（如果需要的话，这里直接清除编辑状态即可）
    // 因为 editingRecord 只是临时值，不会影响原数据
  }
  editingKey.value = ''
  editingRecord.value = {}
  editingIndex.value = -1
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
.dict-type-form {
  .dict-data-toolbar {
    margin-bottom: 16px;
  }
}
</style>
