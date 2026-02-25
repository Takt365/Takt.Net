<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/routine/event/components -->
<!-- 文件名称：event-form.vue -->
<!-- 创建时间：2025-02-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：活动组织新增/编辑表单，供 index.vue 弹窗使用 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="(rules as any)"
    :label-col="{ span: 5 }"
    :wrapper-col="{ span: 18 }"
  >
    <a-form-item label="活动名称" name="eventName" :rules="[{ required: true, message: '请输入活动名称' }]">
      <a-input v-model:value="formState.eventName" placeholder="请输入活动名称" allow-clear />
    </a-form-item>
    <a-form-item label="活动类型" name="eventType" :rules="[{ required: true, message: '请选择活动类型' }]">
      <a-select v-model:value="formState.eventType" placeholder="请选择" allow-clear style="width: 100%">
        <a-select-option :value="0">培训</a-select-option>
        <a-select-option :value="1">团建</a-select-option>
        <a-select-option :value="2">会议活动</a-select-option>
        <a-select-option :value="3">庆典</a-select-option>
        <a-select-option :value="4">其他</a-select-option>
      </a-select>
    </a-form-item>
    <a-form-item label="开始时间" name="startTime" :rules="[{ required: true, message: '请选择开始时间' }]">
      <a-date-picker
        v-model:value="formState.startTime"
        show-time
        value-format="YYYY-MM-DD HH:mm:ss"
        style="width: 100%"
        placeholder="请选择开始时间"
      />
    </a-form-item>
    <a-form-item label="结束时间" name="endTime" :rules="[{ required: true, message: '请选择结束时间' }]">
      <a-date-picker
        v-model:value="formState.endTime"
        show-time
        value-format="YYYY-MM-DD HH:mm:ss"
        style="width: 100%"
        placeholder="请选择结束时间"
      />
    </a-form-item>
    <a-form-item label="活动地点" name="location">
      <a-input v-model:value="formState.location" placeholder="请输入活动地点" allow-clear />
    </a-form-item>
    <a-form-item label="组织部门" name="deptId">
      <TaktTreeSelect
        v-model:value="formState.deptId"
        api-url="/api/TaktDepts/tree-options"
        placeholder="请选择组织部门"
        allow-clear
        :field-names="{ label: 'dictLabel', value: 'dictValue' }"
        @change="onDeptChange"
      />
    </a-form-item>
    <a-form-item label="部门名称" name="deptName">
      <a-input v-model:value="formState.deptName" placeholder="可选，与组织部门联动" allow-clear />
    </a-form-item>
    <a-form-item label="活动状态" name="eventStatus" :rules="[{ required: true, message: '请选择活动状态' }]">
      <a-select v-model:value="formState.eventStatus" placeholder="请选择" allow-clear style="width: 100%">
        <a-select-option :value="0">草稿</a-select-option>
        <a-select-option :value="1">已发布</a-select-option>
        <a-select-option :value="2">进行中</a-select-option>
        <a-select-option :value="3">已结束</a-select-option>
        <a-select-option :value="4">已取消</a-select-option>
      </a-select>
    </a-form-item>
    <a-form-item label="活动内容" name="eventContent">
      <a-textarea v-model:value="formState.eventContent" placeholder="请输入活动内容/描述" :rows="3" allow-clear />
    </a-form-item>
    <a-form-item label="参与人摘要" name="participantSummary">
      <a-input v-model:value="formState.participantSummary" placeholder="请输入参与人摘要" allow-clear />
    </a-form-item>
    <a-form-item label="排序号" name="orderNum">
      <a-input-number v-model:value="formState.orderNum" :min="0" style="width: 100%" />
    </a-form-item>
    <a-form-item label="备注" name="remark">
      <a-input v-model:value="formState.remark" placeholder="请输入备注" allow-clear />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { reactive, watch, ref } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Event, EventCreate } from '@/types/routine/event'
import { getById as getDeptById } from '@/api/humanresource/organization/dept'
import TaktTreeSelect from '@/components/business/takt-tree-select/index.vue'

interface Props {
  formData?: Partial<Event>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref<FormInstance>()

interface FormState {
  eventName: string
  eventType: number
  startTime: string
  endTime: string
  location?: string
  deptId?: string
  deptName?: string
  eventStatus: number
  eventContent?: string
  participantSummary?: string
  orderNum: number
  remark?: string
}

const formState = reactive<FormState>({
  eventName: '',
  eventType: 0,
  startTime: '',
  endTime: '',
  location: undefined,
  deptId: undefined,
  deptName: undefined,
  eventStatus: 0,
  eventContent: undefined,
  participantSummary: undefined,
  orderNum: 0,
  remark: undefined
})

const rules = {
  eventName: [{ required: true, message: '请输入活动名称', trigger: 'blur' }],
  eventType: [{ required: true, message: '请选择活动类型', trigger: 'change' }],
  startTime: [{ required: true, message: '请选择开始时间', trigger: 'change' }],
  endTime: [{ required: true, message: '请选择结束时间', trigger: 'change' }],
  eventStatus: [{ required: true, message: '请选择活动状态', trigger: 'change' }]
}

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      eventName: newData.eventName ?? '',
      eventType: newData.eventType ?? 0,
      startTime: newData.startTime ?? '',
      endTime: newData.endTime ?? '',
      location: newData.location,
      deptId: newData.deptId,
      deptName: newData.deptName,
      eventStatus: newData.eventStatus ?? 0,
      eventContent: newData.eventContent,
      participantSummary: newData.participantSummary,
      orderNum: newData.orderNum ?? 0,
      remark: newData.remark
    })
  } else {
    Object.assign(formState, {
      eventName: '',
      eventType: 0,
      startTime: '',
      endTime: '',
      location: undefined,
      deptId: undefined,
      deptName: undefined,
      eventStatus: 0,
      eventContent: undefined,
      participantSummary: undefined,
      orderNum: 0,
      remark: undefined
    })
  }
}, { immediate: true, deep: true })

async function onDeptChange(value: string | number | (string | number)[] | undefined, _option?: any) {
  const id = Array.isArray(value) ? value[0] : value
  if (id == null || id === '') {
    formState.deptName = undefined
    return
  }
  try {
    const dept = await getDeptById(String(id))
    formState.deptName = dept?.deptName ?? undefined
  } catch {
    formState.deptName = undefined
  }
}

async function validate() {
  await formRef.value?.validate()
}

function getValues(): EventCreate & { eventId?: string } {
  const payload: EventCreate & { eventId?: string } = {
    eventName: formState.eventName,
    eventType: formState.eventType,
    startTime: formState.startTime,
    endTime: formState.endTime,
    location: formState.location,
    deptId: formState.deptId,
    deptName: formState.deptName,
    eventStatus: formState.eventStatus,
    eventContent: formState.eventContent,
    participantSummary: formState.participantSummary,
    orderNum: formState.orderNum,
    remark: formState.remark
  }
  if (props.formData?.eventId) {
    payload.eventId = props.formData.eventId
  }
  return payload
}

function resetFields() {
  formRef.value?.resetFields()
  Object.assign(formState, {
    eventName: '',
    eventType: 0,
    startTime: '',
    endTime: '',
    location: undefined,
    deptId: undefined,
    deptName: undefined,
    eventStatus: 0,
    eventContent: undefined,
    participantSummary: undefined,
    orderNum: 0,
    remark: undefined
  })
}

defineExpose({ validate, getValues, resetFields })
</script>
