<template>
  <!-- 与 identity/user-form 一致：单 a-form 包裹 a-tabs，validate 校验全部 Tab（force-render 保证未选中 Tab 内控件已挂载） -->
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 4 }"
    :wrapper-col="{ span: 20 }"
    layout="horizontal"
  >
    <a-tabs v-model:active-key="activeTab">
      <a-tab-pane
        key="basic"
        :tab="t('common.form.tabs.basicinfo')"
        force-render
      >
        <div class="announcement-form-pane">
          <a-form-item
            :label="t('entity.announcement.announcementcode')"
            name="announcementCode"
          >
            <a-input
              v-model:value="formState.announcementCode"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.announcement.announcementcode') })"
              :disabled="!!formData?.announcementId"
            />
          </a-form-item>
          <a-form-item
            :label="t('entity.announcement.announcementtitle')"
            name="announcementTitle"
          >
            <a-input
              v-model:value="formState.announcementTitle"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.announcement.announcementtitle') })"
              allow-clear
            />
          </a-form-item>
          <a-form-item
            :label="t('entity.announcement.announcementtype')"
            name="announcementType"
          >
            <a-select
              v-model:value="formState.announcementType"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.announcement.announcementtype') })"
              :options="announcementTypeOptions"
            />
          </a-form-item>
        </div>
      </a-tab-pane>

      <a-tab-pane
        key="body"
        :tab="t('common.form.tabs.announcementbody')"
        force-render
      >
        <div class="announcement-form-pane">
          <a-form-item
            :label="t('entity.announcement.announcementcontent')"
            name="announcementContent"
            :label-col="{ span: 4 }"
            :wrapper-col="{ span: 20 }"
          >
            <a-textarea
              v-model:value="formState.announcementContent"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.announcement.announcementcontent') })"
              :rows="10"
              allow-clear
            />
          </a-form-item>
        </div>
      </a-tab-pane>

      <a-tab-pane
        key="publish"
        :tab="t('common.form.tabs.announcementpublish')"
        force-render
      >
        <div class="announcement-form-pane">
          <a-form-item
            :label="t('entity.announcement.publishscope')"
            name="publishScope"
          >
            <a-select
              v-model:value="formState.publishScope"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.announcement.publishscope') })"
              :options="publishScopeOptions"
            />
          </a-form-item>
          <a-form-item
            :label="t('entity.announcement.istop')"
            name="isTop"
          >
            <a-radio-group v-model:value="formState.isTop">
              <a-radio v-for="o in isTopOptions" :key="o.value" :value="o.value">
                {{ o.label }}
              </a-radio>
            </a-radio-group>
          </a-form-item>
          <a-form-item
            :label="t('entity.announcement.isurgent')"
            name="isUrgent"
          >
            <a-radio-group v-model:value="formState.isUrgent">
              <a-radio v-for="o in urgencyLevelOptions" :key="o.value" :value="o.value">
                {{ o.label }}
              </a-radio>
            </a-radio-group>
          </a-form-item>
          <a-form-item
            :label="t('entity.announcement.effectivetime')"
            name="effectiveTime"
          >
            <a-date-picker
              v-model:value="formState.effectiveTime"
              show-time
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.announcement.effectivetime') })"
              style="width: 100%"
              allow-clear
            />
          </a-form-item>
          <a-form-item
            :label="t('entity.announcement.expiretime')"
            name="expireTime"
          >
            <a-date-picker
              v-model:value="formState.expireTime"
              show-time
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.announcement.expiretime') })"
              style="width: 100%"
              allow-clear
            />
          </a-form-item>
          <a-form-item
            :label="t('entity.announcement.ordernum')"
            name="orderNum"
          >
            <a-input-number
              v-model:value="formState.orderNum"
              :min="0"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.announcement.ordernum') })"
              style="width: 100%"
            />
          </a-form-item>
        </div>
      </a-tab-pane>

      <a-tab-pane
        key="other"
        :tab="t('common.form.tabs.announcementother')"
        force-render
      >
        <div class="announcement-form-pane">
          <a-form-item
            :label="t('common.entity.remark')"
            name="remark"
          >
            <a-textarea
              v-model:value="formState.remark"
              :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
              :rows="4"
              allow-clear
            />
          </a-form-item>
        </div>
      </a-tab-pane>
    </a-tabs>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch, computed, onMounted } from 'vue'
import dayjs from 'dayjs'
import { useI18n } from 'vue-i18n'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Announcement, AnnouncementCreate } from '@/types/routine/business/announcement/announcement'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'

const { t } = useI18n()
const dictDataStore = useDictDataStore()

const props = withDefaults(
  defineProps<{
    formData?: Partial<Announcement> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

/** 与控件绑定：可选 DTO 字段用空串表示未填，避免 exactOptionalPropertyTypes 下出现 undefined。 */
type AnnouncementFormModel = Omit<AnnouncementCreate, 'remark' | 'effectiveTime' | 'expireTime'> & {
  remark: string
  effectiveTime: string
  expireTime: string
  announcementId?: string
}

const formRef = ref<FormInstance>()
const activeTab = ref('basic')

function emptyForm(): AnnouncementFormModel {
  return {
    announcementCode: '',
    announcementTitle: '',
    announcementContent: '',
    announcementType: 0,
    publishScope: 0,
    isTop: 0,
    isUrgent: 0,
    orderNum: 0,
    remark: '',
    effectiveTime: '',
    expireTime: ''
  }
}

const formState = ref<AnnouncementFormModel>(emptyForm())

function dictOptionsAsNumber(dictType: string) {
  return dictDataStore.getDictOptions(dictType).map((o) => ({
    label: o.label,
    value: Number(o.value)
  }))
}

const announcementTypeOptions = computed(() => dictOptionsAsNumber('sys_notice_type'))

const publishScopeOptions = computed(() => dictOptionsAsNumber('sys_publish_scope'))

const isTopOptions = computed(() =>
  [...dictOptionsAsNumber('sys_yes_no')].sort((a, b) => a.value - b.value)
)

const urgencyLevelOptions = computed(() => dictOptionsAsNumber('sys_urgency_level'))

onMounted(() => {
  void dictDataStore.loadAllDictData()
})

const rules = computed<Record<string, Rule[]>>(() => ({
  announcementCode: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.announcement.announcementcode') }),
      trigger: 'blur'
    }
  ],
  announcementTitle: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.announcement.announcementtitle') }),
      trigger: 'blur'
    }
  ]
}))

watch(
  () => props.formData,
  (val) => {
    if (val && Object.keys(val).length > 0) {
      const next: AnnouncementFormModel = {
        ...emptyForm(),
        announcementCode: val.announcementCode ?? '',
        announcementTitle: val.announcementTitle ?? '',
        announcementContent: val.announcementContent ?? '',
        announcementType: val.announcementType ?? 0,
        publishScope: val.publishScope ?? 0,
        isTop: val.isTop ?? 0,
        isUrgent: val.isUrgent ?? 0,
        orderNum: val.orderNum ?? 0,
        remark: val.remark ?? '',
        effectiveTime: val.effectiveTime ? dayjs(val.effectiveTime).format('YYYY-MM-DD HH:mm:ss') : '',
        expireTime: val.expireTime ? dayjs(val.expireTime).format('YYYY-MM-DD HH:mm:ss') : ''
      }
      if (val.announcementId) next.announcementId = String(val.announcementId)
      formState.value = next
    } else {
      formState.value = emptyForm()
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

function validate() {
  return formRef.value?.validate()
}

function getValues(): AnnouncementCreate & { announcementId?: string } {
  const s = formState.value
  const remark = (s.remark ?? '').trim()
  const effectiveTime = (s.effectiveTime ?? '').trim()
  const expireTime = (s.expireTime ?? '').trim()
  const base: AnnouncementCreate & { announcementId?: string } = {
    announcementCode: s.announcementCode ?? '',
    announcementTitle: s.announcementTitle ?? '',
    announcementContent: s.announcementContent ?? '',
    announcementType: s.announcementType ?? 0,
    publishScope: s.publishScope ?? 0,
    isTop: s.isTop ?? 0,
    isUrgent: s.isUrgent ?? 0,
    orderNum: s.orderNum ?? 0,
    ...(remark.length > 0 ? { remark } : {}),
    ...(effectiveTime.length > 0 ? { effectiveTime } : {}),
    ...(expireTime.length > 0 ? { expireTime } : {})
  }
  if (s.announcementId) {
    base.announcementId = s.announcementId
  }
  return base
}

defineExpose({ validate, getValues })
</script>

<style scoped lang="less">
.announcement-form-pane {
  padding-top: 8px;
}
</style>
