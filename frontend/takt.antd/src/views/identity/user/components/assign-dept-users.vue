<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：assign-dept-users.vue -->
<!-- 功能描述：分配用户部门弹窗。由 user/index.vue 引用；v-model:open 与父级同步，emit success；树形 Transfer + getDeptTreeOptions / getUserDeptIds / assignUserDepts；左侧选部门右侧已选树，支持取消穿梭回左侧。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-modal
    v-model:open="visible"
    :title="t('common.button.allocate') + t('entity.dept._self')"
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
      <a-form-item :label="t('entity.dept._self')">
        <a-transfer
          v-model:target-keys="targetKeys"
          class="tree-transfer"
          :data-source="transferDataSource"
          :list-style="{
            width: '250px',
            height: '50vh',
          }"
          :render="item => item.title"
          :show-select-all="false"
          :loading="optionsLoading"
        >
          <template #children="{ direction, selectedKeys, onItemSelect }">
            <!-- 左侧树形选择 -->
            <a-tree
              v-if="direction === 'left'"
              block-node
              checkable
              :check-strictly="false"
              default-expand-all
              :checked-keys="[...selectedKeys.map((k: string | number) => String(k)), ...targetKeys]"
              :tree-data="treeData"
              :field-names="{ title: 'dictLabel', key: 'dictValue', children: 'children' }"
              @check="(checked: any, props: any) => {
                if (props) {
                  // a-tree 默认会处理父子关联选中，checked 已经包含所有选中的节点（包括子节点）
                  const checkedKeys = Array.isArray(checked) ? checked : checked.checked || []
                  const allKeys = [...selectedKeys.map((k: string | number) => String(k)), ...targetKeys]
                  
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
              :checked-keys="targetKeys"
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
 * 分配用户部门弹窗：open 与 visible 双向同步，加载部门树与用户已绑部门 ID，提交 assignUserDepts。
 */
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TransferProps } from 'ant-design-vue'
import { getDeptTreeOptions } from '@/api/human-resource/organization/dept'
import { getUserDeptIds, assignUserDepts } from '@/api/identity/user'
import type { User } from '@/types/identity/user'
import type { UserDept } from '@/types/human-resource/organization/user-dept'
import type { TaktTreeSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

/**
 * 组件入参：父级控制弹窗显隐与当前用户。
 */
interface Props {
  /** 是否显示对话框（与 v-model:open 对应） */
  open?: boolean
  /** 被分配部门的用户 */
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

/** 与 a-modal v-model:open 绑定，并向父 emit update:open */
const visible = ref(false)
/** 提交 assign 或初始加载用户部门时的整窗 loading */
const loading = ref(false)
/** 部门树选项加载中 */
const optionsLoading = ref(false)
/** Transfer 右侧已选部门 id 列表 */
const targetKeys = ref<string[]>([])
/** 部门树原始数据 */
const allOptions = ref<TaktTreeSelectOption[]>([])
/** Transfer 展平后的 dataSource */
const transferDataSource = ref<TransferProps['dataSource']>([])

/** 弹窗内只读展示：用户名（真实名） */
const userInfo = ref('')

/** 将部门树递归展平为 Transfer 行数据 */
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

/** 构造左侧树节点；不禁用已选节点以便左侧展示勾选状态 */
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

/** 右侧树：仅保留已选 id 及其祖先路径 */
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

/** 左侧部门树数据 */
const treeData = computed(() => {
  return handleTreeData(allOptions.value, targetKeys.value)
})

/** 右侧已选部门子树 */
const rightTreeData = computed(() => {
  if (targetKeys.value.length === 0 || allOptions.value.length === 0) {
    return []
  }
  try {
    return filterTreeDataBySelectedKeys(allOptions.value, targetKeys.value)
  } catch (error) {
    logger.error('[AssignDeptUsers] 生成右侧树形数据失败:', error)
    return []
  }
})

/** 右侧树取消勾选：从 targetKeys 移除并等价于穿梭回左侧 */
const handleRightTreeCheck = (checked: any) => {
  // 处理右侧树的取消选中操作
  const checkedKeys = Array.isArray(checked) ? checked : checked.checked || []
  // 找出被取消选中的节点（在 targetKeys 中但不在 checkedKeys 中）
  const removedKeys = targetKeys.value.filter((k: string) => !checkedKeys.map((ck: string | number) => String(ck)).includes(k))
  // 从 targetKeys 中移除取消选中的节点
  removedKeys.forEach((key: string) => {
    const index = targetKeys.value.indexOf(key)
    if (index > -1) {
      targetKeys.value.splice(index, 1)
    }
  })
}

/** 监听 props.open：打开时同步 visible 并加载用户已分配部门 */
watch(() => props.open, (val) => {
  visible.value = val
  if (val && props.user) {
    loadUserDepts()
  }
})

/** 监听 visible：与父组件 v-model:open 双向绑定 */
watch(visible, (val) => {
  emit('update:open', val)
})

/** 拉取全量部门树与用户当前部门 id，填充 targetKeys 与 transferDataSource */
const loadUserDepts = async () => {
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

    // 并行加载所有部门和用户已分配的部门
    const [allDepts, userDepts] = await Promise.all([
      getDeptTreeOptions(),
      getUserDeptIds(String(userId))
    ])
    
    allOptions.value = allDepts
    
    // 展平树形数据
    transferDataSource.value = []
    flatten(allDepts)
    
    // 提取已分配的部门ID
    const selectedIds = userDepts.map((dept: UserDept) => {
      return String(dept.deptId || (dept as any).DeptId || '')
    }).filter((id: string) => id)
    
    targetKeys.value = selectedIds

    logger.debug('[AssignDeptUsers] 加载用户部门成功:', {
      userId,
      userDepts,
      targetKeys: targetKeys.value
    })
  } catch (error: any) {
    logger.error('[AssignDeptUsers] 加载用户部门失败:', error)
    message.error(error.message || t('common.msg.loadTargetFail', { target: t('entity.user._self') + t('entity.dept._self') }))
  } finally {
    loading.value = false
    optionsLoading.value = false
  }
}

/** 弹窗确定：调用 assignUserDepts 后 emit success 并关闭 */
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
    await assignUserDepts(String(userId), targetKeys.value)
    
    message.success(t('common.msg.assignSuccess', { target: t('entity.dept._self') }))
    emit('success')
    handleCancel()
  } catch (error: any) {
    logger.error('[AssignDeptUsers] 分配部门失败:', error)
    message.error(error.message || t('common.msg.assignFail', { target: t('entity.dept._self') }))
  } finally {
    loading.value = false
  }
}

/** 关闭弹窗并清空本地选择状态 */
const handleCancel = () => {
  visible.value = false
  targetKeys.value = []
  transferDataSource.value = []
  allOptions.value = []
  userInfo.value = ''
}
</script>

<style scoped lang="less">
/* 左侧 Transfer 列宽：树形选择需要更宽区域 */
.tree-transfer :deep(.ant-transfer-list:first-child) {
  width: 50%;
  flex: none;
}
</style>
