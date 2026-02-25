<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/routine/announcement/components -->
<!-- 文件名称：announcement-form.vue -->
<!-- 创建时间：2025-02-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：公告新增/编辑表单，供 index.vue 弹窗使用 -->
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
    <a-form-item label="公告标题" name="announcementTitle" :rules="[{ required: true, message: '请输入公告标题' }]">
      <a-input v-model:value="formState.announcementTitle" placeholder="请输入公告标题" allow-clear />
    </a-form-item>
    <a-form-item label="公告内容" name="announcementContent" :rules="[{ required: true, message: '请输入公告内容' }]">
      <TaktRichTextEditor
        v-model="formState.announcementContent"
        placeholder="请输入公告内容"
        :height="280"
      />
    </a-form-item>
    <a-form-item label="公告类型" name="announcementType">
      <a-select v-model:value="formState.announcementType" placeholder="请选择" allow-clear style="width: 100%">
        <a-select-option :value="0">通知</a-select-option>
        <a-select-option :value="1">公告</a-select-option>
        <a-select-option :value="2">新闻</a-select-option>
        <a-select-option :value="3">活动</a-select-option>
      </a-select>
    </a-form-item>
    <a-form-item label="发布范围" name="publishScope">
      <a-select v-model:value="formState.publishScope" placeholder="请选择" allow-clear style="width: 100%">
        <a-select-option :value="0">全部</a-select-option>
        <a-select-option :value="1">指定部门</a-select-option>
        <a-select-option :value="2">指定用户</a-select-option>
        <a-select-option :value="3">指定角色</a-select-option>
      </a-select>
    </a-form-item>
    <a-form-item label="是否置顶" name="isTop">
      <a-radio-group v-model:value="formState.isTop">
        <a-radio :value="0">是</a-radio>
        <a-radio :value="1">否</a-radio>
      </a-radio-group>
    </a-form-item>
    <a-form-item label="是否紧急" name="isUrgent">
      <a-radio-group v-model:value="formState.isUrgent">
        <a-radio :value="0">是</a-radio>
        <a-radio :value="1">否</a-radio>
      </a-radio-group>
    </a-form-item>
    <a-form-item label="生效时间" name="effectiveTime">
      <a-date-picker v-model:value="formState.effectiveTime" show-time value-format="YYYY-MM-DD HH:mm:ss" style="width: 100%" />
    </a-form-item>
    <a-form-item label="失效时间" name="expireTime">
      <a-date-picker v-model:value="formState.expireTime" show-time value-format="YYYY-MM-DD HH:mm:ss" style="width: 100%" />
    </a-form-item>
    <a-form-item label="排序号" name="orderNum">
      <a-input-number v-model:value="formState.orderNum" :min="0" style="width: 100%" />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { reactive, watch, ref } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Announcement, AnnouncementCreate } from '@/types/routine/announcement'
import TaktRichTextEditor from '@/components/business/takt-rich-text-editor/index.vue'

interface Props {
  formData?: Partial<Announcement>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref<FormInstance>()

interface FormState {
  announcementTitle: string
  announcementContent: string
  announcementType: number
  publishScope: number
  isTop: number
  isUrgent: number
  effectiveTime?: string
  expireTime?: string
  orderNum: number
}

const formState = reactive<FormState>({
  announcementTitle: '',
  announcementContent: '',
  announcementType: 0,
  publishScope: 0,
  isTop: 1,
  isUrgent: 1,
  effectiveTime: undefined,
  expireTime: undefined,
  orderNum: 0
})

const rules = {
  announcementTitle: [{ required: true, message: '请输入公告标题', trigger: 'blur' }],
  announcementContent: [{ required: true, message: '请输入公告内容', trigger: 'blur' }]
}

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      announcementTitle: newData.announcementTitle ?? '',
      announcementContent: newData.announcementContent ?? '',
      announcementType: newData.announcementType ?? 0,
      publishScope: newData.publishScope ?? 0,
      isTop: newData.isTop ?? 1,
      isUrgent: newData.isUrgent ?? 1,
      effectiveTime: newData.effectiveTime,
      expireTime: newData.expireTime,
      orderNum: newData.orderNum ?? 0
    })
  } else {
    Object.assign(formState, {
      announcementTitle: '',
      announcementContent: '',
      announcementType: 0,
      publishScope: 0,
      isTop: 1,
      isUrgent: 1,
      effectiveTime: undefined,
      expireTime: undefined,
      orderNum: 0
    })
  }
}, { immediate: true, deep: true })

async function validate() {
  await formRef.value?.validate()
}

function getValues(): AnnouncementCreate & { announcementId?: string } {
  const payload: AnnouncementCreate & { announcementId?: string } = {
    announcementTitle: formState.announcementTitle,
    announcementContent: formState.announcementContent,
    announcementType: formState.announcementType,
    publishScope: formState.publishScope,
    isTop: formState.isTop,
    isUrgent: formState.isUrgent,
    effectiveTime: formState.effectiveTime,
    expireTime: formState.expireTime,
    orderNum: formState.orderNum
  }
  if (props.formData?.announcementId) {
    payload.announcementId = props.formData.announcementId
  }
  return payload
}

function resetFields() {
  formRef.value?.resetFields()
  Object.assign(formState, {
    announcementTitle: '',
    announcementContent: '',
    announcementType: 0,
    publishScope: 0,
    isTop: 1,
    isUrgent: 1,
    effectiveTime: undefined,
    expireTime: undefined,
    orderNum: 0
  })
}

defineExpose({ validate, getValues, resetFields })
</script>
