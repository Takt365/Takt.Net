<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/user/components -->
<!-- 文件名称：dept-tree-transfer.vue -->
<!-- 功能描述：部门树形穿梭（无模态框）。由 user-form.vue 引用；v-model 部门 id 数组；双树 Transfer + getDeptTreeOptions；internalTargetKeys 与 modelValue 双向同步（防循环比较 JSON）。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-transfer
    v-model:target-keys="internalTargetKeys"
    class="tree-transfer"
    :data-source="transferDataSource"
    :list-style="{
      width: '13.333vw',
      height: '20vh',
    }"
    :render="item => item.title"
    :show-select-all="false"
    :loading="loading"
  >
    <template #children="{ direction, selectedKeys, onItemSelect }">
      <!-- 左侧树形选择 -->
      <a-tree
        v-if="direction === 'left'"
        block-node
        checkable
        :check-strictly="false"
        default-expand-all
        :checked-keys="[...selectedKeys.map((k: string | number) => String(k)), ...internalTargetKeys]"
        :tree-data="treeData"
        :field-names="{ title: 'dictLabel', key: 'dictValue', children: 'children' }"
        @check="(checked: any, props: any) => {
          if (props) {
            // a-tree 默认会处理父子关联选中，checked 已经包含所有选中的节点（包括子节点）
            const checkedKeys = Array.isArray(checked) ? checked : checked.checked || []
            const allKeys = [...selectedKeys.map((k: string | number) => String(k)), ...internalTargetKeys]
            
            // 找出新增和移除的节点
            const newKeys = checkedKeys.filter((k: string | number) => !allKeys.includes(String(k)))
            const removedKeys = allKeys.filter((k: string) => !checkedKeys.map((ck: string | number) => String(ck)).includes(k))
            
            logger.debug('[DeptTreeTransfer] 左侧树选中变化:', { 
              checkedKeys, 
              allKeys, 
              newKeys, 
              removedKeys,
              selectedKeys,
              internalTargetKeys: [...internalTargetKeys]
            })
            
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
        :checked-keys="internalTargetKeys"
        :tree-data="rightTreeData"
        :field-names="{ title: 'dictLabel', key: 'dictValue', children: 'children' }"
        @check="handleRightTreeCheck"
      />
    </template>
  </a-transfer>
</template>

<script setup lang="ts">
/**
 * 部门树穿梭：供用户表单权限 Tab 绑定 deptIds；挂载时加载部门树。
 */
import { ref, watch, computed, onMounted } from 'vue'
import type { TransferProps } from 'ant-design-vue'
import { getDeptTreeOptions } from '@/api/human-resource/organization/dept'
import type { TaktTreeSelectOption } from '@/types/common'
import { logger } from '@/utils/logger'

/**
 * 组件入参：与父级 v-model:model-value 对应。
 */
interface Props {
  /** 已选部门 id 列表 */
  modelValue?: string[]
  /** 外部传入的加载态（与 Transfer loading 绑定） */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: () => [],
  loading: false
})

const emit = defineEmits<{
  'update:modelValue': [value: string[]]
}>()

interface TransferItem {
  key: string
  title: string
  dictValue: string | number
  dictLabel?: string
}

interface DeptTreeNode {
  key: string
  title: string
  dictValue: string | number
  dictLabel?: string
  disabled?: boolean
  children?: DeptTreeNode[]
}

interface TreeCheckResult {
  checked?: Array<string | number>
}

function normalizeCheckedKeys(input: unknown): string[] {
  if (Array.isArray(input)) {
    return input.map((k) => String(k))
  }
  if (typeof input === 'object' && input !== null && 'checked' in input) {
    const checked = (input as TreeCheckResult).checked
    if (Array.isArray(checked)) {
      return checked.map((k) => String(k))
    }
  }
  return []
}

/** 部门树源数据 */
const allOptions = ref<TaktTreeSelectOption[]>([])
/** Transfer 展平行 */
const transferDataSource = ref<TransferItem[]>([])

/** Transfer 内部右侧 keys，与 props.modelValue 双向同步 */
const internalTargetKeys = ref<string[]>([])

/** 监听 modelValue：排序后比较再写入 internal，避免与子 watch 循环 */
watch(() => props.modelValue, (newValue) => {
  const newKeys = newValue || []
  const currentKeys = internalTargetKeys.value || []
  // 只有当值真正不同时才更新，避免循环
  const currentStr = JSON.stringify([...currentKeys].sort())
  const newStr = JSON.stringify([...newKeys].sort())
  if (currentStr !== newStr) {
    logger.debug('[DeptTreeTransfer] modelValue 变化，同步到 internalTargetKeys:', { newKeys, currentKeys })
    internalTargetKeys.value = [...newKeys]
  }
}, { immediate: true })

/** 监听 internalTargetKeys：变更时 emit update:modelValue（排序比较防循环） */
watch(internalTargetKeys, (newValue) => {
  const newKeys = newValue || []
  const propsKeys = props.modelValue || []
  // 只有当值真正不同时才更新，避免循环
  const newStr = JSON.stringify([...newKeys].sort())
  const propsStr = JSON.stringify([...propsKeys].sort())
  if (newStr !== propsStr) {
    logger.debug('[DeptTreeTransfer] internalTargetKeys 变化，同步到 modelValue:', { newKeys, propsKeys })
    emit('update:modelValue', [...newKeys])
  }
}, { deep: true })

/** 展平部门树 */
function flatten(list: TaktTreeSelectOption[] = []) {
  list.forEach(item => {
    const key = String(item.dictValue)
    const title = item.dictLabel || ''
    transferDataSource.value.push({
      key,
      title,
      dictValue: item.dictValue,
      dictLabel: item.dictLabel
    })
    if (item.children?.length) {
      flatten(item.children)
    }
  })
}

/** 左侧树节点 */
function handleTreeData(treeNodes: TaktTreeSelectOption[], targetKeys: string[] = []): DeptTreeNode[] {
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

/** 右侧已选子树 */
function filterTreeDataBySelectedKeys(treeNodes: TaktTreeSelectOption[], selectedKeys: string[]): DeptTreeNode[] {
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
  }).filter((node): node is DeptTreeNode => node !== null)
}

/** 左侧部门树 */
const treeData = computed(() => {
  return handleTreeData(allOptions.value, internalTargetKeys.value)
})

/** 右侧已选部门树 */
const rightTreeData = computed(() => {
  if (internalTargetKeys.value.length === 0 || allOptions.value.length === 0) {
    return []
  }
  try {
    return filterTreeDataBySelectedKeys(allOptions.value, internalTargetKeys.value)
  } catch (error) {
    logger.error('[DeptTreeTransfer] 生成右侧树形数据失败:', error)
    return []
  }
})

/** 右侧取消勾选：更新 internal 并 emit */
const handleRightTreeCheck = (checked: unknown) => {
  // 处理右侧树的取消选中操作
  const checkedKeys = normalizeCheckedKeys(checked)
  // 找出被取消选中的节点（在 internalTargetKeys 中但不在 checkedKeys 中）
  const removedKeys = internalTargetKeys.value.filter((k: string) => !checkedKeys.includes(k))
  // 从 internalTargetKeys 中移除取消选中的节点
  const newKeys = internalTargetKeys.value.filter((k: string) => !removedKeys.includes(k))
  internalTargetKeys.value = newKeys
  // 同步更新到父组件
  emit('update:modelValue', [...newKeys])
}

/** 拉取部门树并展平到 transferDataSource */
const loadDeptOptions = async () => {
  try {
    const depts = await getDeptTreeOptions()
    allOptions.value = depts
    
    // 展平树形数据
    transferDataSource.value = []
    flatten(depts)
  } catch (error: unknown) {
    logger.error('[DeptTreeTransfer] 加载部门数据失败:', error)
  }
}

/** 挂载加载部门选项 */
onMounted(() => {
  loadDeptOptions()
})
</script>

<style scoped lang="less">
/* 左侧列宽适配树 */
.tree-transfer :deep(.ant-transfer-list:first-child) {
  width: 50%;
  flex: none;
}
</style>
