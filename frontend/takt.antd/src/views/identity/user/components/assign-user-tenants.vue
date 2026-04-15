<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：assign-user-tenants.vue -->
<!-- 功能描述：分配用户租户弹窗。由 user/index.vue 引用；Transfer + getTenantOptions / getUserTenantIds / assignUserTenants；v-model:open 与 emit success。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-modal
    v-model:open="visible"
    :title="t('common.button.allocate') + t('entity.tenant._self')"
    :width="'33.333vw'"
    :confirm-loading="loading"
    :centered="true"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
      layout="horizontal"
    >
      <a-form-item :label="t('entity.user._self')">
        <a-input :value="userInfo" disabled />
      </a-form-item>
      <a-form-item :label="t('entity.tenant._self')">
        <a-transfer
          v-model:target-keys="targetKeys"
          :data-source="dataSource"
          :list-style="{
            width: '250px',
            height: '50vh',
          }"
          :titles="[t('common.action.transferUnassigned'), t('common.action.transferAssigned')]"
          show-search
          :loading="optionsLoading"
          :render="item => item.title"
          @change="handleTransferChange"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
/**
 * 分配用户租户弹窗：租户 Transfer，提交 assignUserTenants。
 */
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getTenantOptions } from '@/api/identity/tenant'
import { getUserTenantIds, assignUserTenants } from '@/api/identity/user'
import type { User } from '@/types/identity/user'
import type { TaktSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

/**
 * 组件入参。
 */
interface Props {
  /** 是否显示对话框 */
  open?: boolean
  /** 被分配租户的用户 */
  user?: User | null
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  user: null
})

const emit = defineEmits<{
  'update:open': [value: boolean]
  'success': []
}>()

const { t } = useI18n()

/** 弹窗显隐 */
const visible = ref(false)
/** 提交/加载 */
const loading = ref(false)
/** 租户选项 loading */
const optionsLoading = ref(false)
/** 已选租户 id */
const targetKeys = ref<string[]>([])
/** 全量租户 */
const allOptions = ref<TaktSelectOption[]>([])

/** 用户只读展示 */
const userInfo = ref('')

/** Transfer 数据源 */
const dataSource = computed(() => 
  allOptions.value.map(item => ({
    key: String((item as any).value ?? item.dictValue),
    title: (item as any).label ?? item.dictLabel ?? ''
  }))
)

/** 监听 props.open：打开时加载用户租户 */
watch(() => props.open, (val) => {
  visible.value = val
  if (val && props.user) {
    loadUserTenants()
  }
})

/** 监听 visible：emit update:open */
watch(visible, (val) => {
  emit('update:open', val)
})

/** 加载租户选项与用户已绑租户 id */
const loadUserTenants = async () => {
  if (!props.user) return

  try {
    loading.value = true
    optionsLoading.value = true

    const u = props.user as User & { UserId?: string; UserName?: string; NickName?: string }
    const userId = u.userId || u.UserId || ''
    if (!userId) {
      message.error(t('common.msg.entityIdRequired', { entity: t('entity.user._self') }))
      return
    }

    userInfo.value = `${u.userName || u.UserName || ''}（${u.nickName || u.NickName || ''}）`

    // 并行加载所有租户和用户已分配的租户
    const [allTenants, tenantIds] = await Promise.all([
      getTenantOptions(),
      getUserTenantIds(String(userId))
    ])
    
    allOptions.value = allTenants
    targetKeys.value = tenantIds.map(id => String(id))

    logger.debug('[AssignUserTenants] 加载用户租户成功:', {
      userId,
      tenantIds,
      targetKeys: targetKeys.value
    })
  } catch (error: any) {
    logger.error('[AssignUserTenants] 加载用户租户失败:', error)
    message.error(error.message || t('common.msg.loadTargetFail', { target: t('entity.user._self') + t('entity.tenant._self') }))
  } finally {
    loading.value = false
    optionsLoading.value = false
  }
}

/** Transfer 变更 */
const handleTransferChange = (keys: string[], direction: string, moveKeys: string[]) => {
  targetKeys.value = keys
}

/** 提交 assignUserTenants */
const handleSubmit = async () => {
  if (!props.user) {
    message.error(t('common.msg.entityNotFound', { entity: t('entity.user._self') }))
    return
  }

  try {
    loading.value = true

    // 获取用户ID
    const userId = props.user.userId || (props.user as any).UserId || ''
    if (!userId) {
      message.error(t('common.msg.entityIdRequired', { entity: t('entity.user._self') }))
      return
    }

    // 调用分配API
    await assignUserTenants(String(userId), targetKeys.value)
    
    message.success(t('common.msg.assignSuccess', { target: t('entity.tenant._self') }))
    emit('success')
    handleCancel()
  } catch (error: any) {
    logger.error('[AssignUserTenants] 分配租户失败:', error)
    message.error(error.message || t('common.msg.assignFail', { target: t('entity.tenant._self') }))
  } finally {
    loading.value = false
  }
}

/** 关闭并重置 */
const handleCancel = () => {
  visible.value = false
  targetKeys.value = []
  allOptions.value = []
  userInfo.value = ''
}
</script>

<style scoped lang="less">
/* Transfer 滚动条由全局 index.less 统一配置 */
</style>
