<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/form/components -->
<!-- 文件名称：form-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程表单新增/编辑表单组件，含步骤与表单设计 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-form
    ref="formRef"
    layout="vertical"
    :model="form"
    :rules="formRules"
  >
    <a-steps
      :current="currentStep"
      :items="stepItems"
      class="form-steps"
    />
    <div class="steps-content">
      <!-- 第一步：表单信息 -->
      <div
        v-show="currentStep === 0"
        class="step-content"
      >
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.flowform.formcode')"
              name="formCode"
              required
            >
              <a-input
                v-model:value="form.formCode"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.flowform.formcode') })"
                :disabled="!!form.formId"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.flowform.formname')"
              name="formName"
              required
            >
              <a-input
                v-model:value="form.formName"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.flowform.formname') })"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowform.formcategory')">
              <TaktSelect
                v-model="form.formCategory"
                dict-type="sys_form_category"
                style="width: 100%"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.flowform.formcategory') })"
                allow-clear
                :show-search="true"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowform.formtype')">
              <TaktSelect
                v-model="form.formType"
                dict-type="sys_form_type"
                style="width: 100%"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.flowform.formtype') })"
                allow-clear
                :show-search="true"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowform.formversion')">
              <a-input
                v-model:value="form.formVersion"
                :placeholder="t('workflow.form.versionPlaceholder')"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowform.formstatus')">
              <TaktSelect
                v-model="form.formStatus"
                dict-type="sys_scheme_status"
                style="width: 100%"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.flowform.formstatus') })"
                allow-clear
                :show-search="true"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowform.ordernum')">
              <a-input-number
                v-model:value="form.orderNum"
                :min="0"
                :step="1"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </div>
      <!-- 第二步（新增/编辑一致）：数据源 + 数据表 + 字段网格 -->
      <div
        v-show="currentStep === 1"
        class="step-content"
      >
        <a-row
          :gutter="8"
          class="form-form__ds-row"
        >
          <a-col :span="12">
            <a-form-item :label="t('workflow.form.step.dataSource')">
              <TaktSelect
                v-model="form.relatedDataBaseName"
                :options="databaseConfigOptions"
                :placeholder="t('workflow.form.dataSourcePlaceholder')"
                :allow-clear="true"
                :show-search="true"
                :filter-option="filterDataSourceOption"
                :loading="databaseConfigLoading"
                style="width: 100%"
                @focus="loadDatabaseConfigs"
                @change="onDataSourceChange"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('workflow.form.step.dataTableList')">
              <TaktSelect
                v-model="form.relatedTableName"
                :options="databaseTableOptions"
                :placeholder="t('workflow.form.dataTablePlaceholder')"
                :allow-clear="true"
                :show-search="true"
                :filter-option="filterDataTableOption"
                :loading="databaseTableLoading"
                :disabled="!form.relatedDataBaseName"
                style="width: 100%"
                @focus="loadDatabaseTables"
                @change="onDataTableChange"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <TaktSingleTable
          class="form-form__field-grid"
          :columns="dataTableColumns"
          :data-source="tableColumnList"
          :pagination="false"
          :stripe="true"
          row-key="dbColumnName"
          :large-screen-column-count="10"
          :small-screen-column-count="5"
        >
          <template #bodyCell="{ column, record }">
            <!-- C#类型：只读显示（由 DB 类型自动映射） -->
            <template v-if="column.key === 'csharpType'">
              <span>{{ record.csharpType }}</span>
            </template>
            <!-- C#列名：只读显示（由列名自动生成 Pascal 大驼峰） -->
            <template v-else-if="column.key === 'csharpColumnName'">
              <span>{{ record.csharpColumnName }}</span>
            </template>
            <!-- 必填：开关，1=是，0=否 -->
            <template v-else-if="column.key === 'isRequired'">
              <a-switch
                :checked="record.isRequired === 0 || record.isRequired === '0'"
                checked-children="是"
                un-checked-children="否"
                @change="(checked) => { record.isRequired = checked ? 0 : 1 }"
              />
            </template>
            <!-- 显示类型：sys_display_type 字典 -->
            <template v-else-if="column.key === 'displayType'">
              <TaktSelect
                v-model="record.displayType"
                dict-type="sys_display_type"
                placeholder="显示类型"
                allow-clear
                size="small"
                style="width: 100%"
                @change="(v: unknown) => { const t = Array.isArray(v) ? v[0] : v; if (!['select','checkbox','radio'].includes(String(t))) record.dictTypeCode = '' }"
              />
            </template>
            <!-- 字典：选择绑定的字典类型编码 -->
            <template v-else-if="column.key === 'dictTypeCode'">
              <TaktSelect
                v-model="record.dictTypeCode"
                :options="dictTypeOptions"
                :field-names="{ label: 'dictLabel', value: 'extLabel' }"
                placeholder="选择字典类型"
                allow-clear
                size="small"
                style="width: 100%"
                :show-search="true"
                :filter-option="filterDictTypeOption"
              />
            </template>
          </template>
        </TaktSingleTable>
        <div class="form-form__entity-hint">
          {{ t('workflow.form.entityTableHint') }}
        </div>
      </div>
      <!-- 第三步（新增/编辑一致）：表单设计 -->
      <div
        v-show="currentStep === 2"
        class="step-content"
      >
        <a-form-item :label="t('entity.flowform.formconfig')">
          <TaktFormDesigner
            :key="'form-designer-' + (form.formId ?? 'new')"
            ref="designerRef"
            v-model="form.formConfig"
            height="480px"
            :designer-config="formDesignerConfig"
            :sfc-download-basename="sfcDownloadBasename"
          />
        </a-form-item>
      </div>
    </div>
    <div class="steps-action">
      <a-button
        v-if="currentStep > 0"
        style="margin-right: 8px"
        @click="prev"
      >
        {{ t('workflow.form.step.prev') }}
      </a-button>
      <a-button
        v-if="currentStep < steps.length - 1"
        type="primary"
        @click="next"
      >
        {{ t('workflow.form.step.next') }}
      </a-button>
      <a-button
        v-if="currentStep === steps.length - 1"
        type="primary"
        @click="handleDone"
      >
        {{ t('workflow.form.step.done') }}
      </a-button>
    </div>
  </a-form>
</template>

<script setup lang="ts">
/**
 * 流程表单新增/编辑：步骤 1 基本信息、步骤 2 表单设计（takt-form-designer）；对外暴露 validate、getFormData。
 */
import { ref, computed, watch } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { TaktSelectOption } from '@/types/common'
import {
  getDatabaseConfigs,
  getDatabaseTables,
  getTableColumns,
  getFormConfigFromTable,
  type DatabaseConfigItem,
  type DatabaseTableItem,
  type TableColumnItem
} from '@/api/workflow/form'
import { getDictTypeOptions } from '@/api/routine/tasks/dict/dicttype'
import type { FlowFormCreate } from '@/types/workflow/form'
const getStringValue = (obj: unknown, key: string): string | undefined => {
  if (!obj || typeof obj !== 'object') return undefined
  const value = (obj as Record<string, unknown>)[key]
  return value == null ? undefined : String(value)
}
const getBooleanValue = (obj: unknown, key: string): boolean | undefined => {
  if (!obj || typeof obj !== 'object') return undefined
  const value = (obj as Record<string, unknown>)[key]
  return typeof value === 'boolean' ? value : undefined
}
const pickString = (obj: unknown, ...keys: string[]): string => {
  for (const key of keys) {
    const value = getStringValue(obj, key)
    if (value != null && value !== '') return value
  }
  return ''
}
const toErrorMessage = (error: unknown): string => (error instanceof Error ? error.message : String(error))

/** 将字符串转为下载文件名用 basename：全英文小写，仅保留 a-z0-9 与连字符 */
function toSlug(s: string): string {
  const v = String(s ?? '').trim().toLowerCase().replace(/[^a-z0-9]+/g, '-').replace(/^-|-$/g, '')
  return v
}

/** SFC 下载文件名：优先表单名称（转英文小写），无则用表单编码 */
const sfcDownloadBasename = computed(() =>
  toSlug(form.formName ?? '') || toSlug(form.formCode ?? '') || 'form-component'
)

const { t } = useI18n()

/** 父组件传入的表单数据（含 formId 表示编辑） */
interface Props {
  form: FlowFormCreate & { formId?: string }
}

const props = defineProps<Props>()

const form = props.form

/** 当前步骤（0=表单信息，1=数据源+字段网格，2=表单设计），提前声明避免 watch 注册时 TDZ */
const currentStep = ref(0)

/** 数据库源下拉：选项与加载状态 */
const databaseConfigOptions = ref<{ value: string; label: string }[]>([])
const databaseConfigLoading = ref(false)
function loadDatabaseConfigs() {
  if (databaseConfigOptions.value.length > 0) return
  databaseConfigLoading.value = true
  getDatabaseConfigs()
    .then((list) => {
      databaseConfigOptions.value = (list ?? []).map((item: DatabaseConfigItem) => {
        const dbName = pickString(item, 'dataBaseName', 'configId', 'ConfigId')
        const display = pickString(item, 'displayName', 'DisplayName') || dbName
        return { value: dbName, label: display }
      })
    })
    .catch(() => { databaseConfigOptions.value = [] })
    .finally(() => { databaseConfigLoading.value = false })
}
function filterDataSourceOption(input: string, option?: unknown) {
  const label = String((option as { label?: string })?.label ?? '').toLowerCase()
  return label.includes((input ?? '').trim().toLowerCase())
}

/** 数据表下拉：选项与加载状态（依赖 form.relatedDataBaseName）；仅显示带 flow_instance_id 的表 */
const databaseTableOptions = ref<{ value: string; label: string }[]>([])
const databaseTableLoading = ref(false)
function loadDatabaseTables() {
  const configId = form.relatedDataBaseName?.trim()
  if (!configId) { databaseTableOptions.value = []; return }
  databaseTableLoading.value = true
  getDatabaseTables(configId, { requiredColumn: 'flow_instance_id' })
    .then((list) => {
      databaseTableOptions.value = (list ?? []).map((item: DatabaseTableItem) => ({
        value: pickString(item, 'tableName', 'TableName'),
        label: pickString(item, 'tableComment', 'TableComment')
          ? `${pickString(item, 'tableName', 'TableName')} - ${pickString(item, 'tableComment', 'TableComment')}`
          : pickString(item, 'tableName', 'TableName')
      }))
    })
    .catch(() => { databaseTableOptions.value = [] })
    .finally(() => { databaseTableLoading.value = false })
}
function filterDataTableOption(input: string, option?: unknown) {
  const label = String((option as { label?: string })?.label ?? '').toLowerCase()
  return label.includes((input ?? '').trim().toLowerCase())
}

/** 表列多选：选项与加载状态（依赖 form.relatedDataBaseName + form.relatedTableName） */
const tableColumnOptions = ref<{ value: string; label: string }[]>([])
const tableColumnLoading = ref(false)
/** 表列网格数据：用于第二步展示字段列表 */
const tableColumnList = ref<TableColumnItem[]>([])
/** 字典类型选项：供「字典」列选择绑定的字典类型编码 */
const dictTypeOptions = ref<TaktSelectOption[]>([])

/** 审计字段 / 基类通用字段 / 实例字段：在字段网格中隐藏 */
/** 可选显隐的审计/通用列（在字段网格中隐藏）；ext_field_json、remark 必须显示，不在此集合中 */
const AUDIT_DB_COLUMNS = new Set([
  'id',
  'config_id',
  'created_id',
  'created_by',
  'created_at',
  'updated_id',
  'updated_by',
  'updated_at',
  'is_deleted',
  'deleted_id',
  'deleted_by',
  'deleted_at',
  'flow_instance_id'
])

/** DB 类型 -> C# 类型 级联映射（与代码生成器保持一致的精简版） */
const DB_TYPE_TO_CSHARP: Record<string, string> = {
  bigint: 'long',
  bit: 'bool',
  datetime: 'DateTime',
  decimal: 'decimal',
  int: 'int',
  ntext: 'string',
  nvarchar: 'string',
  text: 'string',
  uniqueidentifier: 'Guid',
  varchar: 'string'
}

/** 下划线命名转 Pascal 大驼峰：user_name -> UserName */
function toPascalCase(name: string | undefined): string {
  if (!name) return ''
  return name
    .split(/[_\s]+/)
    .filter(Boolean)
    .map(part => part.charAt(0).toUpperCase() + part.slice(1))
    .join('')
}

function loadTableColumns() {
  const configId = form.relatedDataBaseName?.trim()
  const tableName = form.relatedTableName?.trim()
  if (!configId || !tableName) { tableColumnOptions.value = []; tableColumnList.value = []; return }
  tableColumnLoading.value = true
  getTableColumns(configId, tableName)
    .then((list) => {
      // 编辑且已有 form.relatedFormField 字段元数据时，不覆盖（由进入第二步时从 form.relatedFormField 还原）
      if (form.formId && form.relatedFormField?.trim()) {
        try {
          const parsed = JSON.parse(form.relatedFormField) as unknown
          if (Array.isArray(parsed) && parsed.length > 0) {
            const first = parsed[0] as Record<string, unknown>
            if (first && typeof first === 'object' && (first.dbColumnName != null || first.csharpColumnName != null)) return
          }
        } catch { /* ignore */ }
      }
      const raw = (list ?? []).filter((item) => {
        const name = pickString(item, 'dbColumnName', 'DbColumnName').toLowerCase()
        // 1) 显式排除的审计/实例字段
        if (AUDIT_DB_COLUMNS.has(name)) return false
        // 2) 所有状态字段（status / *_status）统一在网格中隐藏
        if (name === 'status' || name.endsWith('_status')) return false
        return true
      })
      const cols = raw.map((item) => {
        const dbType = pickString(item, 'dataType', 'DataType').toLowerCase()
        const mappedCsharp = DB_TYPE_TO_CSHARP[dbType] ?? 'string'
        const dbName = pickString(item, 'dbColumnName', 'DbColumnName')
        const csharpName = item.csharpColumnName && item.csharpColumnName.trim()
          ? item.csharpColumnName
          : toPascalCase(dbName)
        const notNullable = getBooleanValue(item, 'isNullable') === false
        const isRequired = notNullable ? 0 : 1
        return {
          ...item,
          csharpType: item.csharpType ?? mappedCsharp,
          csharpColumnName: csharpName,
          isRequired: item.isRequired ?? isRequired,
          displayType: item.displayType ?? (mappedCsharp === 'DateTime'
            ? 'date'
            : (mappedCsharp === 'int' || mappedCsharp === 'long' || mappedCsharp === 'decimal')
              ? 'InputNumber'
              : 'input'),
          dictTypeCode: item.dictTypeCode ?? ''
        } as TableColumnItem
      })
      tableColumnList.value = cols
      tableColumnOptions.value = cols.map((item: TableColumnItem) => ({
        value: pickString(item, 'dbColumnName', 'DbColumnName'),
        label: pickString(item, 'columnDescription', 'ColumnDescription')
          ? `${pickString(item, 'dbColumnName', 'DbColumnName')} - ${pickString(item, 'columnDescription', 'ColumnDescription')}`
          : pickString(item, 'dbColumnName', 'DbColumnName')
      }))
    })
    .catch(() => { tableColumnOptions.value = []; tableColumnList.value = [] })
    .finally(() => { tableColumnLoading.value = false })
}
watch(() => form.isDatasource, (v) => {
  if (v !== 1) {
    form.relatedDataBaseName = ''
    form.relatedTableName = ''
    form.relatedFormField = ''
    databaseTableOptions.value = []
    tableColumnOptions.value = []
    tableColumnList.value = []
  }
})
const previousDataSource = ref('')
watch(() => form.relatedDataBaseName, (configId) => {
  if (!configId) {
    form.relatedTableName = ''
    form.relatedFormField = ''
    previousDataSource.value = ''
    databaseTableOptions.value = []
    tableColumnOptions.value = []
    tableColumnList.value = []
    return
  }
  if (previousDataSource.value && previousDataSource.value !== configId) {
    form.relatedTableName = ''
    form.relatedFormField = ''
  }
  previousDataSource.value = configId
  databaseTableOptions.value = []
  tableColumnOptions.value = []
  loadDatabaseTables()
}, { immediate: true })
watch(() => form.relatedTableName, (tableName) => {
  if (!tableName || !form.relatedDataBaseName) {
    tableColumnOptions.value = []
    tableColumnList.value = []
    return
  }
  tableColumnOptions.value = []
  loadTableColumns()
}, { immediate: true })

// 加载字典类型选项（用于「字典」列下拉）
getDictTypeOptions()
  .then(list => { dictTypeOptions.value = list ?? [] })
  .catch(() => { dictTypeOptions.value = [] })

function filterDictTypeOption(input: string, option?: unknown) {
  const label = String((option as { label?: string })?.label ?? '').toLowerCase()
  return label.includes((input ?? '').trim().toLowerCase())
}


/** 是否编辑（有 formId 为编辑，反之为新增）；编辑时表单设计器由 form.formConfig 还原 */
const isEdit = computed(() => !!form.formId)

/** 编辑时进入第二步：若 form.relatedFormField 为字段元数据数组(与 syncFieldsToFormModel 一致)，则还原到字段网格 */
watch(currentStep, (step) => {
  if (step !== 1 || !isEdit.value || !form.relatedFormField?.trim()) return
  try {
    const parsed = JSON.parse(form.relatedFormField) as unknown
    if (!Array.isArray(parsed) || parsed.length === 0) return
    const first = parsed[0] as Record<string, unknown>
    if (!first || typeof first !== 'object' || (first.dbColumnName == null && first.csharpColumnName == null)) return
    const list = parsed as TableColumnItem[]
    tableColumnList.value = list
    tableColumnOptions.value = list.map((item: TableColumnItem) => ({
      value: pickString(item, 'dbColumnName', 'DbColumnName'),
      label: pickString(item, 'columnDescription', 'ColumnDescription')
        ? `${pickString(item, 'dbColumnName', 'DbColumnName')} - ${pickString(item, 'columnDescription', 'ColumnDescription')}`
        : pickString(item, 'dbColumnName', 'DbColumnName')
    }))
  } catch {
    // 非字段元数据格式，保持由 form.relatedTableName watch 触发的 loadTableColumns 结果
  }
})
const formRef = ref()
const designerRef = ref<{ syncToModel?: () => void } | null>(null)

/** 第二步（新增）：数据源变更时清空数据表与字段列表 */
function onDataSourceChange() {
  form.relatedTableName = ''
  tableColumnList.value = []
}
/** 第二步（新增）：选中数据表后获取所有列项生成 FormConfig，并在网格中展示字段 */
async function onDataTableChange() {
  const configId = form.relatedDataBaseName?.trim()
  const tableName = form.relatedTableName?.trim()
  if (!configId || !tableName) return
  try {
    const raw = await getFormConfigFromTable(configId, tableName)
    const config =
      typeof raw === 'string'
        ? raw
        : Array.isArray(raw)
          ? JSON.stringify(raw)
          : raw != null
            ? String(raw)
            : ''
    form.formConfig = (config ?? '').trim() || ''
    form.isDatasource = 1
    await loadTableColumns()
    message.success(t('workflow.form.step.dataTableList') + '：已获取所有数据列项，下一步可还原表单')
  } catch (err: unknown) {
    const msg =
      (typeof (err as { response?: { data?: unknown } })?.response?.data === 'string' &&
      ((err as { response?: { data?: string } }).response?.data?.trim() ?? '') !== ''
        ? (err as { response?: { data?: string } }).response?.data?.trim()
        : null) ??
      ((err as { response?: { data?: { message?: string } } }).response?.data?.message) ??
      toErrorMessage(err) ??
      '获取表单配置失败'
    message.error(msg)
  }
}

/** 设计器 config（官方 props.config），按文档配置显隐：https://view.form-create.com/props */
const formDesignerConfig = {
  showSaveBtn: true,
  showPreviewBtn: true,
  showJsonPreview: true,
  showLanguage: true,
  showInputData: true
}

/** 步骤配置（新增/编辑一致）：第一步 表单信息 → 第二步 数据源+数据表+字段网格 → 第三步 表单设计 */
const steps = computed(() => [
  { title: t('workflow.form.step.formInfo'), content: 0 },
  { title: t('workflow.form.step.dataSource'), content: 1 },
  { title: t('workflow.form.step.formDesign'), content: 2 }
])
/** 供 a-steps 使用的 items（title 列表） */
const stepItems = computed(() => steps.value.map(item => ({ key: item.title, title: item.title })))

/** 第二步字段网格列配置：列名、描述、DB类型 等 */
const dataTableColumns = computed(() => [
  { title: '列名', dataIndex: 'dbColumnName', key: 'dbColumnName', width: 160 },
  { title: '描述', dataIndex: 'columnDescription', key: 'columnDescription', width: 200 },
  { title: 'DB类型', dataIndex: 'dataType', key: 'dataType', width: 120 },
  { title: 'C#类型', dataIndex: 'csharpType', key: 'csharpType', width: 120 },
  { title: 'C#列名', dataIndex: 'csharpColumnName', key: 'csharpColumnName', width: 160 },
  { title: '长度', dataIndex: 'length', key: 'length', width: 80 },
  { title: '精度', dataIndex: 'decimalDigits', key: 'decimalDigits', width: 80 },
  { title: '必填', dataIndex: 'isRequired', key: 'isRequired', width: 80 },
  { title: '显示类型', dataIndex: 'displayType', key: 'displayType', width: 120 },
  { title: '字典', dataIndex: 'dictTypeCode', key: 'dictTypeCode', width: 160 }
])

/** 当前步骤需要校验的字段名：第一步 表单信息 必填 formCode、formName */
const stepFieldNames = computed<Record<number, string[]>>(
  () => ({ 0: ['formCode', 'formName'], 1: [], 2: [] })
)

/** 表单校验规则：formCode、formName 必填 */
const formRules = computed(() => ({
  formCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.flowform.formcode') }) }],
  formName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.flowform.formname') }) }]
}))

/**
 * 将当前字段网格(tableColumnList)同步到 form.relatedFormField / form.formConfig：
 * - relatedFormField：保存完整字段元数据数组(JSON)
 * - formConfig：根据字段元数据生成第三步表单配置规则(JSON)，供设计器与发起流程表单使用
 */
function syncFieldsToFormModel() {
  if (!tableColumnList.value.length) return
  const fieldDefs = tableColumnList.value.map((col) => {
    return {
      dbColumnName: pickString(col, 'dbColumnName', 'DbColumnName'),
      columnDescription: pickString(col, 'columnDescription', 'ColumnDescription'),
      dataType: pickString(col, 'dataType', 'DataType'),
      length: col.length,
      decimalDigits: col.decimalDigits,
      isRequired: col.isRequired,
      displayType: col.displayType,
      dictTypeCode: col.dictTypeCode,
      csharpType: col.csharpType,
      csharpColumnName: col.csharpColumnName
    }
  })

  // 写入 RelatedFormField：字段元数据 JSON
  form.relatedFormField = JSON.stringify(fieldDefs)

  // 生成 FormConfig 规则：与 FlowStartForm / FlowTaskForm 使用的 FormConfigRule 结构兼容
  const formConfigRule = fieldDefs.map((f) => {
    const field = (f.csharpColumnName || f.dbColumnName || '').toString()
    const title = (f.columnDescription || field || '').toString()
    const dt = (f.dataType || '').toString().toLowerCase()
    const displayType = (f.displayType || '').toString()

    let type: string
    if (displayType === 'textarea') {
      type = 'textarea'
    } else if (displayType === 'select' || displayType === 'checkbox' || displayType === 'radio') {
      type = 'select'
    } else if (displayType === 'date' || dt === 'date' || dt === 'datetime' || dt === 'datetime2') {
      type = 'datePicker'
    } else {
      type = 'input'
    }

    const props: Record<string, unknown> = {}
    if (type === 'textarea') {
      props.rows = 3
    }

    return {
      field,
      title,
      type,
      props
    }
  })

  form.formConfig = JSON.stringify(formConfigRule)
}

/**
 * 校验当前步骤需要校验的字段（见 stepFieldNames）。
 * @returns 通过返回 true，否则返回 false
 */
async function validateCurrentStep(): Promise<boolean> {
  const fields = stepFieldNames.value[currentStep.value]
  if (!fields?.length) return true
  try {
    await formRef.value?.validateFields(fields)
    return true
  } catch {
    return false
  }
}

/**
 * 下一步：先校验当前步骤，通过则将 currentStep 加 1。
 */
async function next() {
  const ok = await validateCurrentStep()
  if (!ok) return
  // 从第二步(字段网格)进入第三步(表单设计)前，同步字段元数据到 RelatedFormField / FormConfig（新增/编辑一致）
  if (currentStep.value === 1) {
    syncFieldsToFormModel()
  }
  currentStep.value++
}

/**
 * 上一步：将 currentStep 减 1。
 */
function prev() {
  currentStep.value--
}

/**
 * 完成：校验当前步骤，通过则提示成功（父组件负责实际提交）。
 */
async function handleDone() {
  const ok = await validateCurrentStep()
  if (!ok) return
  message.success(t('workflow.form.step.done'))
}

/**
 * 校验所有步骤的必填字段；未通过时切换到对应步骤并提示，返回 false。
 * @returns 全部通过返回 true，否则 false
 */
async function validateAllSteps(): Promise<boolean> {
  for (let i = 0; i < steps.value.length; i++) {
    const fields = stepFieldNames.value[i]
    if (fields?.length) {
      try {
        await formRef.value?.validateFields(fields)
      } catch {
        currentStep.value = i
        message.warning(t('workflow.form.step.validateFail', { step: i + 1 }))
        return false
      }
    }
  }
  return true
}

/**
 * 将表单设计器当前数据同步到 form.formConfig（提交前由父组件调用）。
 */
function syncDesignerToModel() {
  designerRef.value?.syncToModel?.()
}

/**
 * 重置步骤与子组件内部状态。
 * 仅由父组件在「打开弹窗」时调用（新增或编辑打开后 nextTick 调用），用于：
 * - 步骤归零，从第一步开始；
 * - 清空数据源/数据表/字段列表的缓存与加载状态，避免上次操作残留。
 * 关闭弹窗时不要调用：下次打开为新增会 resetForm + 再 open + 再调本方法，为编辑会回填 form + open + 再调本方法。
 */
function resetSteps() {
  currentStep.value = 0
  tableColumnList.value = []
  tableColumnOptions.value = []
  databaseConfigOptions.value = []
  databaseTableOptions.value = []
  previousDataSource.value = ''
  databaseConfigLoading.value = false
  databaseTableLoading.value = false
  tableColumnLoading.value = false
}

defineExpose({
  syncDesignerToModel,
  currentStep,
  validateAllSteps,
  resetSteps
})
</script>

<style scoped lang="less">
.form-steps {
  margin-bottom: 16px;
}
.steps-content {
  margin-top: 16px;
  min-height: 200px;
}
.step-content {
  margin-top: 0;
}
.form-form__entity-hint {
  margin-top: 8px;
  color: var(--ant-color-text-tertiary);
  font-size: 12px;
}
.steps-action {
  margin-top: 24px;
}
</style>

