<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-flow-antflow-designer/node -->
<!-- 文件名称：takt-flow-node-wrap.vue -->
<!-- 创建时间：2026-04-07 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程树节点递归渲染（发起人/审批/抄送、条件网关、并行网关） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->
<template>
  <!-- 发起人、审核人、抄送人（显式排除 2、7，避免并行网关被当成普通块渲染） -->
  <template v-if="nodeConfig && (nodeConfig.nodeType === 1 || nodeConfig.nodeType === 4 || nodeConfig.nodeType === 6)">
    <div class="node-wrap">
      <div
        class="node-wrap-box"
        :class="{ 'start-node': nodeConfig.nodeType === 1 }"
      >
        <div class="title" :style="titleStyle">
          <template v-if="nodeConfig.nodeType === 1">{{ nodeConfig.nodeName }}</template>
          <template v-else>
            <span class="iconfont node-title-icon" aria-hidden="true">
              <RiShieldCheckLine v-if="nodeConfig.nodeType === 4" size="15" />
              <RiShareForwardLine v-else size="15" />
            </span>
            <span class="title-label">{{ nodeConfig.nodeName }}</span>
            <i v-if="!readonly" class="close" @click.stop="delNode">×</i>
          </template>
        </div>
        <div class="content" @click="openDrawerForNode">
          <div class="text">
            <span v-if="!showText" class="placeholder">{{ t('workflow.designer.pleaseSelect') }}{{ defaultText }}</span>
            {{ showText }}
          </div>
          <span class="arrow" aria-hidden="true"><RiArrowRightSLine size="14" /></span>
        </div>
      </div>
      <TaktFlowAddNode v-if="!readonly" :child-node-p="nodeConfig.childNode ?? null" @update:child-node-p="(v) => (nodeConfig!.childNode = v)" />
      <TaktFlowNodeWrap
        v-if="nodeConfig.childNode != null"
        :node-config="nodeConfig.childNode ?? null"
        @update:node-config="(v) => (nodeConfig!.childNode = v)"
        :drawer="drawer"
        :readonly="readonly"
      />
    </div>
  </template>

  <!-- 条件分支 nodeType === 2（模板内递归组件名 TaktFlowNodeWrap） -->
  <template v-else-if="nodeConfig && nodeConfig.nodeType === 2">
    <div class="branch-wrap">
      <div class="branch-box-wrap">
        <div class="branch-box">
          <button v-if="!readonly" type="button" class="add-branch" @click="addTerm">{{ t('workflow.designer.addCondition') }}</button>
            <div v-for="(item, index) in nodeConfig.conditionNodes" :key="item.nodeId" class="col-box">
              <div class="condition-node">
                <div class="condition-node-box">
                  <div class="auto-judge">
                    <div class="title-wrapper">
                      <span class="iconfont node-title-icon" aria-hidden="true">
                        <RiPulseLine v-if="nodeConfig.isDynamicCondition === true" size="15" />
                        <RiOrganizationChart v-else-if="nodeConfig.isParallel === true" size="15" />
                        <RiGitBranchLine v-else size="15" />
                      </span>
                      <span class="editable-title">{{ item.nodeName }}</span>
                      <span class="priority-title" @click="openConditionDrawer(item.priorityLevel ?? 0)">优先级{{ item.priorityLevel }}</span>
                      <i v-if="!readonly" class="close" @click.stop="delTerm(index)">×</i>
                    </div>
                    <div class="content condition-content" @click="openConditionDrawer(item.priorityLevel ?? 0)">
                      {{ conditionStrFor(index) }}
                    </div>
                  </div>
                  <TaktFlowAddNode v-if="!readonly" :child-node-p="item.childNode ?? null" @update:child-node-p="(v) => (item.childNode = v)" />
                </div>
              </div>
              <TaktFlowNodeWrap
                v-if="item.childNode != null"
                :node-config="item.childNode ?? null"
                @update:node-config="(v) => (item.childNode = v)"
                :drawer="drawer"
                :readonly="readonly"
              />
              <template v-if="index === 0">
                <div class="top-left-cover-line" />
                <div class="bottom-left-cover-line" />
              </template>
              <template v-if="index === (nodeConfig.conditionNodes?.length ?? 0) - 1">
                <div class="top-right-cover-line" />
                <div class="bottom-right-cover-line" />
              </template>
            </div>
        </div>
        <TaktFlowAddNode v-if="!readonly" :child-node-p="nodeConfig.childNode ?? null" @update:child-node-p="(v) => (nodeConfig!.childNode = v)" />
      </div>
    </div>
    <TaktFlowNodeWrap
      v-if="nodeConfig?.childNode != null"
      :node-config="(nodeConfig?.childNode) ?? null"
      @update:node-config="(v) => nodeConfig && (nodeConfig.childNode = v)"
      :drawer="drawer"
      :readonly="readonly"
    />
  </template>

  <!-- 并行审批 nodeType === 7（AntFlow nodeWrap 并行分支段） -->
  <template v-else-if="nodeConfig && nodeConfig.nodeType === 7">
    <div class="branch-wrap">
      <div class="branch-box-wrap">
        <div class="branch-box">
          <button v-if="!readonly" type="button" class="add-branch" @click="addTerm">{{ t('workflow.designer.addParallelApprover') }}</button>
            <div v-for="(item, index) in nodeConfig.parallelNodes" :key="item.nodeId" class="col-box">
              <div class="condition-node">
                <div class="condition-node-box">
                  <div class="node-wrap-box parallel-col-box">
                    <div class="title" :style="parallelTitleStyle">
                      <span class="iconfont node-title-icon" aria-hidden="true">
                        <RiShieldCheckLine size="15" />
                      </span>
                      <span class="title-label">{{ item.nodeName }}</span>
                      <i v-if="!readonly" class="close" @click.stop="delTerm(index)">×</i>
                    </div>
                    <div class="content" @click="openParallelDrawer(index)">
                      <div class="text">
                        <span v-if="!parallelShowText(item)" class="placeholder">{{ t('workflow.designer.pleaseSelect') }}{{ t('workflow.designer.approver') }}</span>
                        {{ parallelShowText(item) }}
                      </div>
                      <span class="arrow" aria-hidden="true"><RiArrowRightSLine size="14" /></span>
                    </div>
                  </div>
                  <TaktFlowAddNode v-if="!readonly" :child-node-p="item.childNode ?? null" @update:child-node-p="(v) => (item.childNode = v)" />
                </div>
              </div>
              <TaktFlowNodeWrap
                v-if="item.childNode != null"
                :node-config="item.childNode ?? null"
                @update:node-config="(v) => (item.childNode = v)"
                :drawer="drawer"
                :readonly="readonly"
              />
              <template v-if="index === 0">
                <div class="top-left-cover-line" />
                <div class="bottom-left-cover-line" />
              </template>
              <template v-if="index === (nodeConfig.parallelNodes?.length ?? 0) - 1">
                <div class="top-right-cover-line" />
                <div class="bottom-right-cover-line" />
              </template>
            </div>
        </div>
        <TaktFlowAddNode v-if="!readonly" :child-node-p="nodeConfig.childNode ?? null" @update:child-node-p="(v) => (nodeConfig!.childNode = v)" />
      </div>
    </div>
    <TaktFlowNodeWrap
      v-if="nodeConfig?.childNode != null"
      :node-config="(nodeConfig?.childNode) ?? null"
      @update:node-config="(v) => nodeConfig && (nodeConfig.childNode = v)"
      :drawer="drawer"
      :readonly="readonly"
    />
  </template>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import {
  RiArrowRightSLine,
  RiGitBranchLine,
  RiOrganizationChart,
  RiPulseLine,
  RiShareForwardLine,
  RiShieldCheckLine
} from '@remixicon/vue'
import TaktFlowAddNode from './takt-flow-add-node.vue'
import TaktFlowNodeWrap from './takt-flow-node-wrap.vue'
import { setApproverStr, copyerStr, conditionStr } from '../config/takt-flow-condition-str'
import { createConditionNode, createParallelBranchApproverNode } from '../config/takt-flow-tree'
import type { FlowTreeNode } from '../config/takt-flow-tree'

const { t } = useI18n()

const props = withDefaults(
  defineProps<{
    nodeConfig: FlowTreeNode | null
    drawer: { open: (type: 'promoter' | 'approver' | 'copyer' | 'condition', config: FlowTreeNode | null, conditionIndex: number | null, onSave: (u: FlowTreeNode) => void) => void }
    readonly?: boolean
  }>(),
  { readonly: false }
)

const emit = defineEmits<{ 'update:nodeConfig': [value: FlowTreeNode | null] }>()

const bgColors: Record<number, string> = {
  1: '64, 158, 255',
  4: '250, 140, 22',
  6: '82, 196, 26'
}

const titleStyle = computed(() => {
  if (!props.nodeConfig) return {}
  const rgb = bgColors[props.nodeConfig.nodeType] ?? '128, 128, 128'
  return { background: `rgb(${rgb})` }
})

const parallelTitleStyle = computed(() => ({ background: `rgb(${bgColors[4]})` }))

const defaultText = computed(() => {
  if (!props.nodeConfig) return ''
  const map: Record<number, string> = { 1: t('workflow.designer.promoter'), 4: t('workflow.designer.approver'), 6: t('workflow.designer.copyer') }
  return map[props.nodeConfig.nodeType] ?? ''
})

const showText = computed(() => {
  const n = props.nodeConfig
  if (!n) return ''
  if (n.nodeType === 1) return (n.nodeApproveList?.length ? n.nodeApproveList.map((x) => x.name).join('、') : '') || '所有人'
  if (n.nodeType === 4) return setApproverStr(n)
  if (n.nodeType === 6) return copyerStr(n)
  return ''
})

function parallelShowText(node: FlowTreeNode): string {
  return setApproverStr(node)
}

function conditionStrFor(index: number): string {
  return props.nodeConfig ? conditionStr(props.nodeConfig, index) : '请设置条件'
}

function openDrawerForNode() {
  if (props.readonly || !props.nodeConfig) return
  const n = props.nodeConfig
  const onSave = (updated: FlowTreeNode) => emit('update:nodeConfig', updated)
  if (n.nodeType === 1) props.drawer.open('promoter', props.nodeConfig, null, onSave)
  else if (n.nodeType === 4) props.drawer.open('approver', props.nodeConfig, null, onSave)
  else if (n.nodeType === 6) props.drawer.open('copyer', props.nodeConfig, null, onSave)
}

function openParallelDrawer(index: number) {
  if (props.readonly || !props.nodeConfig?.parallelNodes) return
  const item = props.nodeConfig.parallelNodes[index]
  if (!item) return
  const onSave = (updated: FlowTreeNode) => {
    if (props.nodeConfig?.parallelNodes?.[index]) {
      const list = [...props.nodeConfig.parallelNodes]
      list[index] = updated
      emit('update:nodeConfig', { ...props.nodeConfig, parallelNodes: list })
    }
  }
  props.drawer.open('approver', item, null, onSave)
}

function openConditionDrawer(priorityLevel: number) {
  if (props.readonly || !props.nodeConfig?.conditionNodes) return
  const idx = props.nodeConfig.conditionNodes.findIndex((c) => c.priorityLevel === priorityLevel)
  const onSave = (updated: FlowTreeNode) => {
    if (props.nodeConfig?.conditionNodes && idx >= 0) {
      const list = [...props.nodeConfig.conditionNodes]
      list[idx] = updated
      emit('update:nodeConfig', { ...props.nodeConfig, conditionNodes: list })
    }
  }
  const cond = idx >= 0 ? props.nodeConfig.conditionNodes[idx] : null
  props.drawer.open('condition', cond ?? null, idx >= 0 ? idx : null, onSave)
}

function delNode() {
  emit('update:nodeConfig', props.nodeConfig?.childNode ?? null)
}

function resetConditionNodesTitle(gateway: FlowTreeNode, index: number): string {
  if (gateway.isDynamicCondition === true) return `动态条件${index + 1}`
  if (gateway.isParallel === true) return `并行条件${index + 1}`
  return `条件${index + 1}`
}

function reData(data: FlowTreeNode, addData: FlowTreeNode): void {
  if (!data.childNode) {
    data.childNode = addData
  } else {
    reData(data.childNode, addData)
  }
}

function addTerm() {
  const nc = props.nodeConfig
  if (!nc) return
  if (nc.nodeType === 2 && nc.conditionNodes) {
    const len = nc.conditionNodes.length
    const n_name = resetConditionNodesTitle(nc, len)
    nc.conditionNodes.push(createConditionNode(n_name, null, len + 1, 0))
    emit('update:nodeConfig', { ...nc })
  } else if (nc.nodeType === 7 && nc.parallelNodes) {
    const len = nc.parallelNodes.length + 1
    const n_name = `并行审核人${len}`
    nc.parallelNodes.push(createParallelBranchApproverNode(n_name, null, len, 0))
    emit('update:nodeConfig', { ...nc })
  }
}

function delTerm(index: number) {
  const nc = props.nodeConfig
  if (!nc) return
  if (nc.nodeType === 2 && nc.conditionNodes) {
    nc.conditionNodes.splice(index, 1)
    nc.conditionNodes.forEach((item, i) => {
      item.priorityLevel = i + 1
      item.nodeName = resetConditionNodesTitle(nc, i)
    })
    if (nc.conditionNodes.length === 1) {
      const only = nc.conditionNodes[0]
      if (nc.childNode) {
        if (only.childNode) {
          reData(only.childNode, nc.childNode)
        } else {
          only.childNode = nc.childNode
        }
      }
      emit('update:nodeConfig', only.childNode ?? null)
      return
    }
    emit('update:nodeConfig', { ...nc })
  } else if (nc.nodeType === 7 && nc.parallelNodes) {
    nc.parallelNodes.splice(index, 1)
    nc.parallelNodes.forEach((item, i) => {
      item.priorityLevel = i + 1
      item.nodeName = `审批人${i + 1}`
    })
    if (nc.parallelNodes.length === 1) {
      const only = nc.parallelNodes[0]
      if (nc.childNode) {
        if (only.childNode) {
          reData(only.childNode, nc.childNode)
        } else {
          only.childNode = nc.childNode
        }
      }
      emit('update:nodeConfig', only.childNode ?? null)
      return
    }
    emit('update:nodeConfig', { ...nc })
  }
}
</script>
