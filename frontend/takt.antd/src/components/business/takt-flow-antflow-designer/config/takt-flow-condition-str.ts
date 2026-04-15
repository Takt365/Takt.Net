// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/components/business/takt-flow-antflow-designer/config
// 文件名称：takt-flow-condition-str.ts
// 创建时间：2026-04-07
// 创建人：Takt365(Cursor AI)
// 功能描述：条件分支/审批人/抄送节点展示文案（AntFlow conditionStr 语义）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { FlowTreeNode } from './takt-flow-tree'

export function setApproverStr(node: FlowTreeNode): string {
  if (!node || node.nodeType !== 4) return ''
  const list = node.nodeApproveList ?? []
  const names = list.map((x) => x.name).join('、')
  if (node.setType === 1) {
    if (list.length === 0) return ''
    if (list.length === 1) return list[0].name ?? ''
    return node.signType === 2 ? `${list.length}人(${names})或签` : names
  }
  if (node.setType === 2) {
    const level = node.directorLevel === 1 ? '直接主管' : `第${node.directorLevel}级主管`
    return node.signType === 2 ? `${level}或签` : level
  }
  if (node.setType === 3) return list.length ? `指定(${names})角色` : ''
  if (node.setType === 4) return '指定部门'
  if (node.setType === 5) return '发起人自选'
  if (node.setType === 6) return `层层审批：直到发起人的第${node.directorLevel ?? 1}级主管`
  return names || ''
}

export function copyerStr(node: FlowTreeNode): string {
  if (!node || node.nodeType !== 6) return ''
  const list = node.nodeApproveList ?? []
  if (list.length) return list.map((x) => x.name).join('、')
  if (node.ccFlag === 1) return '发起人自选'
  return ''
}

export function conditionStr(nodeConfig: FlowTreeNode, index: number): string {
  const nodes = nodeConfig.conditionNodes
  if (!nodes?.length || index < 0 || index >= nodes.length) return '请设置条件'
  const item = nodes[index]
  const list = item.conditionList ?? []
  if (list.length === 0) {
    const isLast = index === nodes.length - 1
    return isLast ? '其他条件进入此流程' : '请设置条件'
  }
  const parts = list
    .filter((c) => c.zdy1 != null)
    .map((c) => {
      const op = (c.optType ?? '') === '1' ? '<' : (c.optType ?? '') === '2' ? '>' : (c.optType ?? '') === '4' ? '>=' : (c.optType ?? '') === '5' ? '<=' : '=='
      return `${c.showName ?? ''} ${op} ${c.zdy1 ?? ''}`
    })
  return parts.join(' 并且 ') || '请设置条件'
}
