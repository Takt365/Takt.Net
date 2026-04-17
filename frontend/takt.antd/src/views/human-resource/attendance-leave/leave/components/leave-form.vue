<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/leave/components -->
<!-- 文件名称：leave-form.vue -->
<!-- 功能描述：请假维护弹窗内嵌表单。由 leave/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。员工、请假类型（dict-type=sys_leave_category，与 TaktDictTypeSeedData 一致）、起止日期、事由、证明附件（TaktUploadFile + proofAttachmentsJson）。表单模型复用 `@/types/.../leave` 中 `LeaveCreate`。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 单 Tab：预留 activeTab 与 holiday-form 结构一致 -->
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicInfo')"
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
          <!-- 员工 ID、请假类型（leave_type 列，字典 sys_leave_category） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.leave.employeeid')"
                name="employeeId"
              >
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.leave.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.leave.leavetype')"
                name="leaveType"
              >
                <TaktSelect
                  v-model="formState.leaveType"
                  dict-type="sys_leave_category"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.leave.leavetype') })"
                  allow-clear
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 开始/结束日期（与 TaktLeave.start_date / end_date、value-format YYYY-MM-DD 一致） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.leave.startdate')"
                name="startDate"
              >
                <a-date-picker
                  v-model:value="formState.startDate"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.leave.startdate') })"
                  value-format="YYYY-MM-DD"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.leave.enddate')"
                name="endDate"
              >
                <a-date-picker
                  v-model:value="formState.endDate"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.leave.enddate') })"
                  value-format="YYYY-MM-DD"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 请假事由（选填） -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.leave.reason')"
                name="reason"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-textarea
                  v-model:value="formState.reason"
                  :placeholder="t('common.form.placeholder.optional', { field: t('entity.leave.reason') })"
                  :rows="3"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 证明附件：上传完成后序列化为 LeaveCreate.proofAttachmentsJson -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.leave.proofattachments')"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <TaktUploadFile
                  ref="proofUploadRef"
                  v-model:files-file-list="proofFilesList"
                  tabs-type="files"
                  :files-custom-request="handleProofUpload"
                  :files-max-size="20"
                  :files-max-count="10"
                  files-accept=".pdf,.jpg,.jpeg,.png,.doc,.docx,.xlsx"
                  :files-multiple="true"
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
/**
 * 请假表单脚本：与 `leave/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { UploadProps } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { UploadFile } from 'ant-design-vue'
import type { Dayjs } from 'dayjs'
import type { LeaveProofAttachment } from '@/types/common'
import type { Leave, LeaveCreate } from '@/types/human-resource/attendance-leave/leave'
import TaktSelect from '@/components/business/takt-select/index.vue'
import TaktUploadFile from '@/components/common/takt-upload-file/index.vue'
import { upload as uploadFileApi } from '@/api/routine/tasks/file'
import { getFileExtension, getFileCategoryByFileName } from '@/utils/file-type'

const { t } = useI18n()

/**
 * 组件入参：由父级列表页传入当前编辑行或空对象（新增）。
 */
interface Props {
  /**
   * 当前请假数据（编辑时含 `leaveId` 等），与 `@/types/human-resource/attendance-leave/leave` 中 `Leave` 一致。
   */
  formData?: Partial<Leave>
  /**
   * 提交中状态，由父级传入；本组件当前未据此禁用控件，预留与父级 loading 联动。
   */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * Ant Design Vue 表单实例，用于 `validate`、`resetFields`。
 */
const formRef = ref<FormInstance>()

/**
 * 证明附件上传组件实例，用于 `clearFiles` 与列表同步。
 */
const proofUploadRef = ref<InstanceType<typeof TaktUploadFile> | null>(null)

/**
 * 证明附件上传列表，与 `TaktUploadFile` 双向绑定；保存时经 `buildProofAttachmentsJson` 写入 `LeaveCreate.proofAttachmentsJson`。
 */
const proofFilesList = ref<UploadFile[]>([])

/**
 * 当前激活的 Tab 键；仅 `basic`。
 */
const activeTab = ref('basic')

/**
 * 估算表单项数量，用于 `takt-form-content-rows-*` 容器类名。
 */
const TOTAL_FIELDS = 6

/**
 * 表单外层纵向布局类名。
 */
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

/**
 * 新增态默认值，类型与 `LeaveCreate` 一致（对应后端 `TaktLeaveCreateDto`）。
 *
 * @returns {LeaveCreate} 空表单初始对象
 */
function createEmptyLeaveForm(): LeaveCreate {
  return {
    employeeId: '',
    leaveType: '',
    startDate: '',
    endDate: '',
    reason: '',
    proofAttachmentsJson: undefined
  }
}

/** 表单模型：直接使用 `LeaveCreate`，不在本组件重复定义字段 interface。 */
const formState = reactive<LeaveCreate>(createEmptyLeaveForm())

/**
 * 校验规则：`name` 与 `a-form-item` 的 `name` 一致。
 */
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.leave.employeeid') }), trigger: 'blur' }
  ],
  leaveType: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.leave.leavetype') }), trigger: 'change' }
  ],
  startDate: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.leave.startdate') }), trigger: 'change' }
  ],
  endDate: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.leave.enddate') }), trigger: 'change' }
  ]
}))

/**
 * 解析上传接口返回体，兼容 `data` / `result` / 根级字段。
 *
 * @param {unknown} res - `uploadFileApi` 原始返回
 * @returns {Record<string, unknown> | null} 含 `fileCode` 的对象或 null
 */
function unwrapUploadResult(res: unknown): Record<string, unknown> | null {
  if (!res || typeof res !== 'object') return null
  const r = res as Record<string, unknown>
  const data = r.data
  const result = r.result
  if (data && typeof data === 'object' && (data as Record<string, unknown>).fileCode) return data as Record<string, unknown>
  if (result && typeof result === 'object' && (result as Record<string, unknown>).fileCode) return result as Record<string, unknown>
  if (r.fileCode) return r
  return null
}

/**
 * 将日期统一为 `YYYY-MM-DD`，供 `a-date-picker` 的 `value-format` 使用。
 *
 * @param {string | Dayjs | undefined} v - 接口日期串或 Dayjs
 * @returns {string} 日期串，空则 `''`
 */
function toDateStr(v: string | Dayjs | undefined): string {
  if (!v) return ''
  if (typeof v === 'string') {
    const s = v.trim()
    if (s.length >= 10) return s.slice(0, 10)
    return s
  }
  return v.format('YYYY-MM-DD')
}

/**
 * 根据 `proofAttachmentsJson` 还原上传列表（编辑进入时调用）。
 *
 * @param {string | undefined} json - 实体上的 JSON 字符串
 * @returns {void}
 */
function syncProofFilesFromJson(json?: string) {
  proofFilesList.value = []
  proofUploadRef.value?.clearFiles()
  if (!json?.trim()) return
  try {
    const arr = JSON.parse(json) as Record<string, unknown>[]
    if (!Array.isArray(arr)) return
    proofFilesList.value = arr.map((item, i) => {
      const fileOriginalName = String(
        item.fileOriginalName ?? item.FileOriginalName ?? item.fileName ?? item.FileName ?? 'file'
      )
      const fileCode = String(item.fileCode ?? item.FileCode ?? `existing-${i}`)
      const url = (item.accessUrl ?? item.AccessUrl) as string | undefined
      return {
        uid: `proof-${fileCode}-${i}`,
        name: fileOriginalName,
        status: 'done',
        url: url || undefined,
        response: item
      } as UploadFile
    })
  } catch {
    proofFilesList.value = []
  }
}

/**
 * 将当前上传列表序列化为 `LeaveCreate.proofAttachmentsJson`（仅 `status === 'done'` 且能解析出 `fileCode` 的项）。
 *
 * @param {UploadFile[]} list - 上传控件文件列表
 * @returns {string | undefined} JSON 字符串；无有效项时 `undefined`
 */
function buildProofAttachmentsJson(list: UploadFile[]): string | undefined {
  const items: LeaveProofAttachment[] = []
  let order = 0
  for (const f of list) {
    if (f.status !== 'done') continue
    const raw = unwrapUploadResult(f.response) ?? (f.response as Record<string, unknown> | undefined)
    if (!raw) continue
    const fileCode = String(raw.fileCode ?? raw.FileCode ?? '')
    if (!fileCode) continue
    items.push({
      fileId: raw.fileId != null || raw.FileId != null ? String(raw.fileId ?? raw.FileId) : undefined,
      fileCode,
      fileName: String(raw.fileName ?? raw.FileName ?? ''),
      fileOriginalName: String(raw.fileOriginalName ?? raw.FileOriginalName ?? f.name ?? ''),
      filePath: String(raw.filePath ?? raw.FilePath ?? ''),
      fileSize: Number(raw.fileSize ?? raw.FileSize ?? 0),
      fileType: raw.fileType != null ? String(raw.fileType) : raw.FileType != null ? String(raw.FileType) : undefined,
      fileExtension: raw.fileExtension != null ? String(raw.fileExtension) : raw.FileExtension != null ? String(raw.FileExtension) : undefined,
      fileHash: raw.fileHash != null ? String(raw.fileHash) : raw.FileHash != null ? String(raw.FileHash) : undefined,
      fileCategory:
        raw.fileCategory != null ? Number(raw.fileCategory) : raw.FileCategory != null ? Number(raw.FileCategory) : undefined,
      accessUrl: raw.accessUrl != null ? String(raw.accessUrl) : raw.AccessUrl != null ? String(raw.AccessUrl) : undefined,
      orderNum: order++
    })
  }
  if (items.length === 0) return undefined
  return JSON.stringify(items)
}

/**
 * 证明附件自定义上传：调用 `uploadFileApi`，补齐扩展名与 `fileCategory` 后交给 Upload `onSuccess`。
 * 签名与 `UploadProps['customRequest']` 一致，供 `TaktUploadFile` 绑定。
 *
 * @returns {Promise<void>}
 */
const handleProofUpload: NonNullable<UploadProps['customRequest']> = async (options) => {
  const { file, onSuccess, onError, onProgress } = options
  const uploadFile = file instanceof File ? file : null
  if (!uploadFile) {
    onError?.(new Error('invalid file'))
    return
  }
  try {
    const result = await uploadFileApi(
      uploadFile,
      2,
      undefined,
      onProgress
        ? (e: { percent: number }) => {
            onProgress({ percent: e.percent })
          }
        : undefined
    )
    const unwrapped = unwrapUploadResult(result)
    if (!unwrapped?.fileCode) {
      throw new Error(t('components.common.upload.fileInvalid', { name: uploadFile.name }))
    }
    const ext = getFileExtension(uploadFile.name)
    if (!unwrapped.fileExtension) unwrapped.fileExtension = ext
    if (unwrapped.fileCategory == null) {
      unwrapped.fileCategory = getFileCategoryByFileName(uploadFile.name)
    }
    onSuccess?.(unwrapped)
  } catch (err: unknown) {
    const e = err instanceof Error ? err : new Error(String(err))
    onError?.(e)
    message.error(e.message || t('components.common.upload.fileUploadFail', { name: uploadFile.name }))
  }
}

/**
 * 同步父组件 `formData`：有数据则填充并还原附件列表，否则重置为 `createEmptyLeaveForm()`。
 */
watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        employeeId: newData.employeeId ?? '',
        leaveType: newData.leaveType ?? '',
        startDate: newData.startDate ? toDateStr(newData.startDate) : '',
        endDate: newData.endDate ? toDateStr(newData.endDate) : '',
        reason: newData.reason ?? '',
        proofAttachmentsJson: newData.proofAttachmentsJson
      })
      syncProofFilesFromJson(newData.proofAttachmentsJson)
    } else {
      Object.assign(formState, createEmptyLeaveForm())
      syncProofFilesFromJson('')
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

/**
 * 校验表单；若证明附件仍有上传中项则提示并抛出（与父级保存逻辑约定）。
 *
 * @returns {Promise<void>}
 */
const validate = async () => {
  if (proofFilesList.value.some((f) => f.status === 'uploading')) {
    message.warning(t('entity.leave.waitproofupload'))
    throw Object.assign(new Error('proof-uploading'), { isProofUploading: true })
  }
  await formRef.value?.validate()
}

/**
 * 组装提交载荷；编辑时附带 `leaveId`。
 *
 * @returns {LeaveCreate & { leaveId?: string }} 提交体
 */
const getValues = (): LeaveCreate & { leaveId?: string } => {
  const payload: LeaveCreate & { leaveId?: string } = {
    employeeId: formState.employeeId ?? '',
    leaveType: formState.leaveType ?? '',
    startDate: formState.startDate ?? '',
    endDate: formState.endDate ?? '',
    reason: formState.reason || undefined,
    proofAttachmentsJson: buildProofAttachmentsJson(proofFilesList.value)
  }
  if (props.formData?.leaveId) {
    payload.leaveId = props.formData.leaveId
  }
  return payload
}

/**
 * 重置表单、`formState`、附件列表与 Tab。
 *
 * @returns {void}
 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyLeaveForm())
  proofUploadRef.value?.clearFiles()
  proofFilesList.value = []
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
/* 本表单无局部样式；占位与 holiday-form 的 style 结构一致 */
</style>
