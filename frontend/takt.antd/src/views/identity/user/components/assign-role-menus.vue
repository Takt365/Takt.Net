<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：assign-role-menus.vue -->
<!-- 功能描述：分配角色菜单弹窗。由 user/index.vue 引用；先选用户已有角色再树形 Transfer 分配菜单；getUserRoleIds / getMenuTreeOptionsWithButtons / getRoleMenuIds / assignRoleMenus；v-model:open 与 emit success。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-modal
    v-model:open="visible"
    :title="t('common.button.allocate') + t('entity.rolemenu._self')"
    :width="'33.333vw'"
    :confirm-loading="loading"
    :centered="true"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
      layout="horizontal"
    >
      <a-form-item :label="t('entity.user._self')">
        <a-input
          :value="userInfo"
          disabled
        />
      </a-form-item>
      <a-form-item :label="t('entity.role._self')">
        <TaktSelect
          v-model:value="selectedRoleId"
          api-url="/api/TaktRoles/options"
          :placeholder="t('common.form.placeholder.selectfirst', { field: t('entity.role._self') })"
          :loading="roleOptionsLoading"
          @change="(value) => handleRoleChange(value as string | number | undefined)"
        />
      </a-form-item>
      <a-form-item :label="t('entity.menu._self')">
        <a-transfer
          v-model:target-keys="selectedMenuIds"
          class="tree-transfer"
          :data-source="transferDataSource"
          :list-style="{
            width: '250px',
            height: '50vh',
          }"
          :render="item => item.title"
          :show-select-all="false"
          :loading="menuOptionsLoading"
          :disabled="!selectedRoleId"
        >
          <template #children="{ direction, selectedKeys, onItemSelect }">
            <!-- 左侧树形选择 -->
            <a-tree
              v-if="direction === 'left'"
              block-node
              checkable
              :check-strictly="false"
              default-expand-all
              :checked-keys="[...selectedKeys.map((k: string | number) => String(k)), ...selectedMenuIds]"
              :tree-data="treeData"
              :field-names="{ title: 'dictLabel', key: 'dictValue', children: 'children' }"
              @check="(checked: any, props: any) => {
                if (props) {
                  // a-tree 默认会处理父子关联选中，checked 已经包含所有选中的节点（包括子节点）
                  const checkedKeys = Array.isArray(checked) ? checked : checked.checked || []
                  const allKeys = [...selectedKeys.map((k: string | number) => String(k)), ...selectedMenuIds]
                  
                  // 找出新增和移除的节点
                  const newKeys = checkedKeys.filter((k: string | number) => !allKeys.includes(String(k)))
                  const removedKeys = allKeys.filter((k: string) => !checkedKeys.map((ck: string | number) => String(ck)).includes(k))
                  
                  // 添加新选中的节点（包括子节点）
                  newKeys.forEach((key: string | number) => {
                    onItemSelect(String(key), true)
                  })
                  
                  // 移除取消选中的节点（包括子节点）
                  removedKeys.forEach((key: string) => {
                    onItemSelect(key, false)
                  })
                }
              }"
            />
            <!-- 右侧树形显示（可交互，保持树形结构，支持取消选中穿梭回左侧） -->
            <a-tree
              v-else-if="direction === 'right'"
              block-node
              checkable
              :check-strictly="false"
              default-expand-all
              :checked-keys="selectedMenuIds"
              :tree-data="rightTreeData"
              :field-names="{ title: 'dictLabel', key: 'dictValue', children: 'children' }"
              @check="handleRightTreeCheck"
            />
          </template>
        </a-transfer>
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
/**
 * 分配角色菜单弹窗：依赖用户已有角色，选角色后加载菜单树并提交 assignRoleMenus。
 */
import { ref, watch, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TransferProps } from 'ant-design-vue'
import { getUserRoleIds } from '@/api/identity/user'
import { getRoleMenuIds, assignRoleMenus } from '@/api/identity/role'
import { getMenuTreeOptionsWithButtons } from '@/api/identity/menu'
import type { User } from '@/types/identity/user'
import type { TaktTreeSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

/**
 * 组件入参。
 */
interface Props {
  /** 是否显示对话框 */
  open?: boolean
  /** 目标用户（须已有角色方可分配菜单） */
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
/** 提交 assignRoleMenus loading */
const loading = ref(false)
/** 用户角色列表加载 */
const roleOptionsLoading = ref(false)
/** 菜单树加载 */
const menuOptionsLoading = ref(false)
/** 当前选中的角色 id（TaktSelect） */
const selectedRoleId = ref<string | number | undefined>(undefined)
/** Transfer 右侧已选菜单 id */
const selectedMenuIds = ref<string[]>([])
/** 全量菜单树 */
const allMenuOptions = ref<TaktTreeSelectOption[]>([])
/** Transfer 展平数据源 */
const transferDataSource = ref<TransferProps['dataSource']>([])

/** 用户只读展示 */
const userInfo = ref('')

/** 展平菜单树为 Transfer 行 */
function flatten(list: TaktTreeSelectOption[] = []) {
  if (!transferDataSource.value) {
    transferDataSource.value = []
  }
  list.forEach(item => {
    const key = String(item.dictValue)
    const title = item.dictLabel || ''
    transferDataSource.value!.push({
      key,
      title,
      dictValue: item.dictValue,
      dictLabel: item.dictLabel
    } as any)
    if (item.children?.length) {
      flatten(item.children)
    }
  })
}

/** 左侧菜单树节点（不禁用已选，便于展示勾选） */
function handleTreeData(treeNodes: TaktTreeSelectOption[], targetKeys: string[] = []): any[] {
  return treeNodes.map(({ children, ...props }) => {
    const key = String(props.dictValue)
    return {
      key,
      title: props.dictLabel || '',
      dictValue: props.dictValue,
      dictLabel: props.dictLabel,
      // 不将已选中的项标记为 disabled，这样左侧树可以正确显示选中状态
      disabled: false,
      children: handleTreeData(children ?? [], targetKeys)
    }
  })
}

/** 右侧已选菜单子树 */
function filterTreeDataBySelectedKeys(treeNodes: TaktTreeSelectOption[], selectedKeys: string[]): any[] {
  return treeNodes.map(({ children, ...props }) => {
    const key = String(props.dictValue)
    const filteredChildren = children?.length 
      ? filterTreeDataBySelectedKeys(children, selectedKeys)
      : []
    
    // 如果当前节点被选中，或者有子节点被选中，则保留该节点
    const isSelected = selectedKeys.includes(key)
    const hasSelectedChildren = filteredChildren.length > 0
    
    if (isSelected || hasSelectedChildren) {
      return {
        key,
        title: props.dictLabel || '',
        dictValue: props.dictValue,
        dictLabel: props.dictLabel,
        children: filteredChildren
      }
    }
    return null
  }).filter(Boolean) as any[]
}

/** 左侧菜单树 */
const treeData = computed(() => {
  return handleTreeData(allMenuOptions.value, selectedMenuIds.value)
})

/** 右侧已选菜单树 */
const rightTreeData = computed(() => {
  if (selectedMenuIds.value.length === 0 || allMenuOptions.value.length === 0) {
    return []
  }
  try {
    return filterTreeDataBySelectedKeys(allMenuOptions.value, selectedMenuIds.value)
  } catch (error) {
    logger.error('[AssignRoleMenus] 生成右侧树形数据失败:', error)
    return []
  }
})

/** 右侧树取消勾选：从 selectedMenuIds 移除 */
const handleRightTreeCheck = (checked: any) => {
  // 处理右侧树的取消选中操作
  const checkedKeys = Array.isArray(checked) ? checked : checked.checked || []
  // 找出被取消选中的节点（在 selectedMenuIds 中但不在 checkedKeys 中）
  const removedKeys = selectedMenuIds.value.filter((k: string) => !checkedKeys.map((ck: string | number) => String(ck)).includes(k))
  // 从 selectedMenuIds 中移除取消选中的节点
  removedKeys.forEach((key: string) => {
    const index = selectedMenuIds.value.indexOf(key)
    if (index > -1) {
      selectedMenuIds.value.splice(index, 1)
    }
  })
}

/** 挂载时若 props.open 已为 true，同步打开并加载用户角色 */
onMounted(() => {
  logger.debug('[AssignRoleMenus] 组件已挂载:', { 
    propsOpen: props.open, 
    user: props.user,
    visible: visible.value
  })
  // 如果 props.open 为 true，同步 visible
  if (props.open) {
    visible.value = true
    if (props.user) {
      loadUserRoles()
    }
  }
})

/** 监听 props.open：打开时加载用户角色；关闭时打日志 */
watch(() => props.open, (val, oldVal) => {
  logger.debug('[AssignRoleMenus] open 变化:', { 
    val, 
    oldVal, 
    user: props.user,
    visible: visible.value,
    propsOpen: props.open
  })
  visible.value = val
  if (val && props.user) {
    logger.debug('[AssignRoleMenus] 开始加载用户角色')
    loadUserRoles()
  } else if (!val) {
    logger.debug('[AssignRoleMenus] 对话框关闭，重置状态')
  }
})

/** 监听 visible：emit update:open */
watch(visible, (val, oldVal) => {
  logger.debug('[AssignRoleMenus] visible 变化:', { val, oldVal, propsOpen: props.open })
  emit('update:open', val)
})

/** 加载用户角色列表，默认选第一项并拉取其菜单 */
const loadUserRoles = async () => {
  if (!props.user) {
    logger.warn('[AssignRoleMenus] 用户信息不存在')
    return
  }

  try {
    logger.debug('[AssignRoleMenus] 开始加载用户角色')
    roleOptionsLoading.value = true

    const u = props.user as User & { UserId?: string; UserName?: string; NickName?: string }
    const userId = u.userId || u.UserId || ''
    if (!userId) {
      logger.error('[AssignRoleMenus] 用户ID不存在')
      message.error(t('common.msg.entityidrequired', { entity: t('entity.user._self') }))
      return
    }

    userInfo.value = `${u.userName || u.UserName || ''}（${u.nickName || u.NickName || ''}）`

    logger.debug('[AssignRoleMenus] 调用 getUserRoleIds:', userId)
    // 加载用户已分配的角色
    const userRoles = await getUserRoleIds(String(userId))
    
    logger.debug('[AssignRoleMenus] 获取到用户角色:', userRoles)
    
    if (userRoles && userRoles.length > 0) {
      // 如果有角色，默认选择第一个角色
      const firstRole = userRoles[0]
      selectedRoleId.value = firstRole.roleId || (firstRole as any).RoleId || ''
      logger.debug('[AssignRoleMenus] 选择第一个角色:', selectedRoleId.value)
      await loadRoleMenus(selectedRoleId.value as string)
    } else {
      logger.warn('[AssignRoleMenus] 用户没有分配角色')
      selectedRoleId.value = undefined
      selectedMenuIds.value = []
      transferDataSource.value = []
      allMenuOptions.value = []
    }

    logger.debug('[AssignRoleMenus] 加载用户角色成功:', {
      userId,
      userRoles,
      selectedRoleId: selectedRoleId.value
    })
  } catch (error: any) {
    logger.error('[AssignRoleMenus] 加载用户角色失败:', error)
    message.error(error.message || t('common.msg.loadtargetfail', { target: t('entity.user._self') + t('entity.role._self') }))
  } finally {
    roleOptionsLoading.value = false
  }
}

/** 角色下拉变更：重载该角色已分配菜单 */
const handleRoleChange = async (roleId: string | number | undefined) => {
  if (!roleId) {
    selectedMenuIds.value = []
    transferDataSource.value = []
    return
  }
  await loadRoleMenus(String(roleId))
}

/** 并行加载全量菜单树与角色已绑 menuId */
const loadRoleMenus = async (roleId: string) => {
  try {
    menuOptionsLoading.value = true

    // 并行加载所有菜单和角色已分配的菜单
    const [allMenusResponse, menuIds] = await Promise.all([
      getMenuTreeOptionsWithButtons(),
      getRoleMenuIds(roleId)
    ])
    
    // 提取响应数据（兼容 AxiosResponse 和直接返回数据两种情况）
    const allMenus = (allMenusResponse as any).data ?? allMenusResponse
    allMenuOptions.value = allMenus as TaktTreeSelectOption[]
    
    // 展平树形数据
    transferDataSource.value = []
    flatten(allMenus as TaktTreeSelectOption[])
    
    selectedMenuIds.value = menuIds.map(id => String(id))

    logger.debug('[AssignRoleMenus] 加载角色菜单成功:', {
      roleId,
      menuIds,
      selectedMenuIds: selectedMenuIds.value
    })
  } catch (error: any) {
    logger.error('[AssignRoleMenus] 加载角色菜单失败:', error)
    message.error(error.message || t('common.msg.loadtargetfail', { target: t('entity.rolemenu._self') }))
  } finally {
    menuOptionsLoading.value = false
  }
}

/** 提交 assignRoleMenus */
const handleSubmit = async () => {
  if (!props.user) {
    message.error(t('common.msg.entitynotfound', { entity: t('entity.user._self') }))
    return
  }

  if (!selectedRoleId.value) {
    message.warning(t('common.form.placeholder.selectfirst', { field: t('entity.role._self') }))
    return
  }

  try {
    loading.value = true

    // 调用分配API
    await assignRoleMenus(String(selectedRoleId.value), selectedMenuIds.value)
    
    message.success(t('common.msg.assignsuccess', { target: t('entity.rolemenu._self') }))
    emit('success')
    handleCancel()
  } catch (error: any) {
    logger.error('[AssignRoleMenus] 分配角色菜单失败:', error)
    message.error(error.message || t('common.msg.assignfail', { target: t('entity.rolemenu._self') }))
  } finally {
    loading.value = false
  }
}

/** 关闭并重置角色与菜单选择 */
const handleCancel = () => {
  visible.value = false
  selectedRoleId.value = undefined
  selectedMenuIds.value = []
  transferDataSource.value = []
  allMenuOptions.value = []
  userInfo.value = ''
}
</script>

<style scoped lang="less">
/* 左侧 Transfer 列宽 */
.tree-transfer :deep(.ant-transfer-list:first-child) {
  width: 50%;
  flex: none;
}
</style>
