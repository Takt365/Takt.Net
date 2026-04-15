<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：assign-role-users.vue -->
<!-- 功能描述：分配用户角色弹窗。由 user/index.vue 引用；Transfer + getRoleOptions / getUserRoleIds / assignUserRoles；v-model:open 与 emit success。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-modal
    v-model:open="visible"
    :title="t('common.button.allocate') + t('entity.role._self')"
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
      <a-form-item :label="t('entity.role._self')">
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
 * 分配用户角色弹窗：角色 Transfer，提交 assignUserRoles。
 */
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getRoleOptions } from '@/api/identity/role'
import { getUserRoleIds, assignUserRoles } from '@/api/identity/user'
import type { User } from '@/types/identity/user'
import type { UserRole } from '@/types/identity/user-role'
import type { TaktSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

/**
 * 组件入参。
 */
interface Props {
  /** 是否显示对话框 */
  open?: boolean
  /** 被分配角色的用户 */
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
/** 提交/加载 loading */
const loading = ref(false)
/** 角色选项 loading */
const optionsLoading = ref(false)
/** 已选角色 id */
const targetKeys = ref<string[]>([])
/** 全量角色 */
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

/** 监听 props.open：打开时加载用户角色 */
watch(() => props.open, (val) => {
  visible.value = val
  if (val && props.user) {
    loadUserRoles()
  }
})

/** 监听 visible：同步父级 open */
watch(visible, (val) => {
  emit('update:open', val)
})

/** 加载角色选项与用户已绑 roleId */
const loadUserRoles = async () => {
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

    // 并行加载所有角色和用户已分配的角色
    const [allRoles, userRoles] = await Promise.all([
      getRoleOptions(),
      getUserRoleIds(String(userId))
    ])
    
    allOptions.value = allRoles
    
    // 提取已分配的角色ID
    const selectedIds = userRoles.map((role: UserRole) => {
      return String(role.roleId || (role as any).RoleId || '')
    }).filter((id: string) => id)
    
    targetKeys.value = selectedIds

    logger.debug('[AssignRoleUsers] 加载用户角色成功:', {
      userId,
      userRoles,
      targetKeys: targetKeys.value
    })
  } catch (error: any) {
    logger.error('[AssignRoleUsers] 加载用户角色失败:', error)
    message.error(error.message || t('common.msg.loadTargetFail', { target: t('entity.user._self') + t('entity.role._self') }))
  } finally {
    loading.value = false
    optionsLoading.value = false
  }
}

/** Transfer 变更：写回 targetKeys */
const handleTransferChange = (keys: string[], direction: string, moveKeys: string[]) => {
  targetKeys.value = keys
}

/** 提交 assignUserRoles */
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
    await assignUserRoles(String(userId), targetKeys.value)
    
    message.success(t('common.msg.assignSuccess', { target: t('entity.role._self') }))
    emit('success')
    handleCancel()
  } catch (error: any) {
    logger.error('[AssignRoleUsers] 分配角色失败:', error)
    message.error(error.message || t('common.msg.assignFail', { target: t('entity.role._self') }))
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
