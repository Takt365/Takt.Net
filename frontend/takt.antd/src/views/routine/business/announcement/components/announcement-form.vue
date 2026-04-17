<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 4 }"
    :wrapper-col="{ span: 20 }"
    layout="horizontal"
  >
    <a-form-item
      label="公告编码"
      name="announcementCode"
    >
      <a-input
        v-model:value="formState.announcementCode"
        placeholder="请输入公告编码（唯一）"
        :disabled="!!formData?.announcementId"
      />
    </a-form-item>
    <a-form-item
      label="公告标题"
      name="announcementTitle"
    >
      <a-input
        v-model:value="formState.announcementTitle"
        placeholder="请输入公告标题"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      label="公告内容"
      name="announcementContent"
    >
      <a-textarea
        v-model:value="formState.announcementContent"
        placeholder="请输入公告内容"
        :rows="6"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      label="公告类型"
      name="announcementType"
    >
      <a-select
        v-model:value="formState.announcementType"
        placeholder="请选择公告类型"
        :options="[
          { label: '通知', value: 0 },
          { label: '公告', value: 1 },
          { label: '新闻', value: 2 },
          { label: '活动', value: 3 }
        ]"
      />
    </a-form-item>
    <a-form-item
      label="发布范围"
      name="publishScope"
    >
      <a-select
        v-model:value="formState.publishScope"
        placeholder="请选择发布范围"
        :options="[
          { label: '全部', value: 0 },
          { label: '指定部门', value: 1 },
          { label: '指定用户', value: 2 },
          { label: '指定角色', value: 3 }
        ]"
      />
    </a-form-item>
    <a-form-item
      label="是否置顶"
      name="isTop"
    >
      <a-radio-group v-model:value="formState.isTop">
        <a-radio :value="0">
          否
        </a-radio>
        <a-radio :value="1">
          是
        </a-radio>
      </a-radio-group>
    </a-form-item>
    <a-form-item
      label="是否紧急"
      name="isUrgent"
    >
      <a-radio-group v-model:value="formState.isUrgent">
        <a-radio :value="0">
          一般
        </a-radio>
        <a-radio :value="1">
          紧急
        </a-radio>
        <a-radio :value="2">
          非常紧急
        </a-radio>
      </a-radio-group>
    </a-form-item>
    <a-form-item
      label="生效时间"
      name="effectiveTime"
    >
      <a-date-picker
        v-model:value="formState.effectiveTime"
        show-time
        format="YYYY-MM-DD HH:mm:ss"
        value-format="YYYY-MM-DD HH:mm:ss"
        placeholder="请选择生效时间"
        style="width: 100%"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      label="失效时间"
      name="expireTime"
    >
      <a-date-picker
        v-model:value="formState.expireTime"
        show-time
        format="YYYY-MM-DD HH:mm:ss"
        value-format="YYYY-MM-DD HH:mm:ss"
        placeholder="请选择失效时间"
        style="width: 100%"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      label="排序号"
      name="orderNum"
    >
      <a-input-number
        v-model:value="formState.orderNum"
        :min="0"
        placeholder="排序号"
        style="width: 100%"
      />
    </a-form-item>
    <a-form-item
      label="备注"
      name="remark"
    >
      <a-textarea
        v-model:value="formState.remark"
        placeholder="请输入备注"
        :rows="2"
        allow-clear
      />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import dayjs from 'dayjs'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Announcement, AnnouncementCreate, AnnouncementUpdate } from '@/types/routine/business/announcement'

const props = withDefaults(
  defineProps<{
    formData?: Partial<Announcement> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const formRef = ref<FormInstance>()
const formState = ref<AnnouncementCreate & { announcementId?: string }>({
  announcementCode: '',
  announcementTitle: '',
  announcementContent: '',
  announcementType: 0,
  publishScope: 0,
  isTop: 0,
  isUrgent: 0,
  orderNum: 0,
  remark: undefined,
  effectiveTime: undefined,
  expireTime: undefined
})

const rules: Record<string, Rule[]> = {
  announcementCode: [{ required: true, message: '请输入公告编码' }],
  announcementTitle: [{ required: true, message: '请输入公告标题' }]
}

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = {
        announcementCode: val.announcementCode ?? '',
        announcementTitle: val.announcementTitle ?? '',
        announcementContent: val.announcementContent ?? '',
        announcementType: val.announcementType ?? 0,
        publishScope: val.publishScope ?? 0,
        isTop: val.isTop ?? 0,
        isUrgent: val.isUrgent ?? 0,
        orderNum: val.orderNum ?? 0,
        remark: val.remark ?? undefined,
        effectiveTime: val.effectiveTime ? dayjs(val.effectiveTime).format('YYYY-MM-DD HH:mm:ss') : undefined,
        expireTime: val.expireTime ? dayjs(val.expireTime).format('YYYY-MM-DD HH:mm:ss') : undefined
      }
      if (val.announcementId) (formState.value as AnnouncementUpdate).announcementId = val.announcementId
    } else {
      formState.value = {
        announcementCode: '',
        announcementTitle: '',
        announcementContent: '',
        announcementType: 0,
        publishScope: 0,
        isTop: 0,
        isUrgent: 0,
        orderNum: 0,
        remark: undefined,
        effectiveTime: undefined,
        expireTime: undefined
      }
    }
  },
  { immediate: true }
)

function validate() {
  return formRef.value?.validate()
}

function getValues() {
  return formState.value
}

defineExpose({ validate, getValues })
</script>
