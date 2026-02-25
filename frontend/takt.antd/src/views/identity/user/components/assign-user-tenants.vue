<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：assign-user-tenants.vue -->
<!-- 创建时间：2025-01-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：分配用户租户组件，用于给用户分配租户 -->
<!-- 
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
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getOptions } from '@/api/identity/tenant'
import { getUserTenantIds, assignUserTenants } from '@/api/identity/user'
import type { User } from '@/types/identity/user'
import type { TaktSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

interface Props {
  /** 是否显示对话框 */
  open?: boolean
  /** 用户信息 */
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
const visible = ref(false)
const loading = ref(false)
const optionsLoading = ref(false)
const targetKeys = ref<string[]>([])
const allOptions = ref<TaktSelectOption[]>([])

// 用户信息显示
const userInfo = ref('')

// Transfer 数据源
const dataSource = computed(() => 
  allOptions.value.map(item => ({
    key: String((item as any).value ?? item.dictValue),
    title: (item as any).label ?? item.dictLabel ?? ''
  }))
)

// 监听 open 变化
watch(() => props.open, (val) => {
  visible.value = val
  if (val && props.user) {
    loadUserTenants()
  }
})

// 监听 visible 变化，同步到父组件
watch(visible, (val) => {
  emit('update:open', val)
})

// 加载所有租户和用户已分配的租户
const loadUserTenants = async () => {
  if (!props.user) {
    return
  }

  try {
    loading.value = true
    optionsLoading.value = true

    // 获取用户ID
    const userId = props.user.userId || (props.user as any).UserId || ''
    if (!userId) {
      message.error(t('common.msg.entityIdRequired', { entity: t('entity.user._self') }))
      return
    }

    // 设置用户信息显示
    const userName = props.user.userName || (props.user as any).UserName || ''
    const realName = props.user.realName || (props.user as any).RealName || ''
    userInfo.value = `${userName}${realName ? `（${realName}）` : ''}`

    // 并行加载所有租户和用户已分配的租户
    const [allTenants, tenantIds] = await Promise.all([
      getOptions(),
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

// Transfer 变化处理
const handleTransferChange = (keys: string[], _direction: string, _moveKeys: string[]) => {
  targetKeys.value = keys
}

// 提交分配
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

// 取消
const handleCancel = () => {
  visible.value = false
  targetKeys.value = []
  allOptions.value = []
  userInfo.value = ''
}
</script>

<style scoped lang="less">
// 注意：Transfer 滚动条样式已在全局样式 index.less 中统一配置，无需重复设置
</style>
