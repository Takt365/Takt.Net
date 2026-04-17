// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/utils/table-columns
// 文件名称：table-columns.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：表格列工具函数，用于生成默认字段列
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TableColumnsType } from 'ant-design-vue'
import type { ColumnGroupType, ColumnType } from 'ant-design-vue/es/table'

/** 表格行占位（默认实体列排序/过滤用） */
type RowRecord = Record<string, unknown>

type ColumnItem = ColumnType<RowRecord> | ColumnGroupType<RowRecord>

function getColumnKey(col: ColumnItem | Record<string, unknown>): string | undefined {
  const c = col as { key?: string | number; dataIndex?: string | number }
  const k = c.key ?? c.dataIndex
  return k != null && k !== '' ? String(k) : undefined
}

/**
 * 审计字段列表（如果 includeAuditFields 为 false，这些字段会被过滤掉）
 * 与后端 TaktEntityBase 的 camelCase 字段一致
 */
const AUDIT_FIELDS = [
  'createdById',
  'createdBy',
  'createdAt',
  'updatedById',
  'updatedBy',
  'updatedAt',
  'isDeleted',
  'deletedById',
  'deletedBy',
  'deletedAt'
] as const

type AuditFieldName = (typeof AUDIT_FIELDS)[number]

/**
 * 获取默认字段列配置（对应 TaktEntityBase）
 * 这些字段会自动添加到表格中，如果用户已定义同名字段则使用用户定义
 * @param t 翻译函数
 * @param includeAuditFields 是否包含审计字段（默认 true，默认显示所有字段）
 */
export function getDefaultEntityColumns(t: (key: string) => string, includeAuditFields: boolean = true): TableColumnsType {
  const allColumns = [
    {
      key: 'configId',
      dataIndex: 'configId',
      title: t('common.entity.configId'),
      width: 120,
      ellipsis: true
    },
    {
      key: 'extFieldJson',
      dataIndex: 'extFieldJson',
      title: t('common.entity.extFieldJson'),
      width: 150,
      ellipsis: true
    },
    {
      key: 'remark',
      dataIndex: 'remark',
      title: t('common.entity.remark'),
      width: 150,
      ellipsis: true
    },
    {
      key: 'createdById',
      dataIndex: 'createdById',
      title: t('common.entity.createdById'),
      width: 120,
      ellipsis: true
    },
    {
      key: 'createdBy',
      dataIndex: 'createdBy',
      title: t('common.entity.createBy'),
      width: 120,
      ellipsis: true
    },
    {
      key: 'createdAt',
      dataIndex: 'createdAt',
      title: t('common.entity.createTime'),
      width: 180,
      ellipsis: true,
      sorter: (a: RowRecord, b: RowRecord) => {
        const aTime = a.createdAt ? new Date(String(a.createdAt)).getTime() : 0
        const bTime = b.createdAt ? new Date(String(b.createdAt)).getTime() : 0
        return aTime - bTime
      }
    },
    {
      key: 'updatedById',
      dataIndex: 'updatedById',
      title: t('common.entity.updatedById'),
      width: 120,
      ellipsis: true
    },
    {
      key: 'updatedBy',
      dataIndex: 'updatedBy',
      title: t('common.entity.updateBy'),
      width: 120,
      ellipsis: true
    },
    {
      key: 'updatedAt',
      dataIndex: 'updatedAt',
      title: t('common.entity.updateTime'),
      width: 180,
      ellipsis: true,
      sorter: (a: RowRecord, b: RowRecord) => {
        const aTime = a.updatedAt ? new Date(String(a.updatedAt)).getTime() : 0
        const bTime = b.updatedAt ? new Date(String(b.updatedAt)).getTime() : 0
        return aTime - bTime
      }
    },
    {
      key: 'isDeleted',
      dataIndex: 'isDeleted',
      title: t('common.entity.isDeleted'),
      width: 100,
      ellipsis: true,
      sorter: (a: RowRecord, b: RowRecord) => {
        const aValue = Number(a.isDeleted ?? 0)
        const bValue = Number(b.isDeleted ?? 0)
        return aValue - bValue
      }
    },
    {
      key: 'deletedById',
      dataIndex: 'deletedById',
      title: t('common.entity.deletedById'),
      width: 120,
      ellipsis: true
    },
    {
      key: 'deletedBy',
      dataIndex: 'deletedBy',
      title: t('common.entity.deletedBy'),
      width: 120,
      ellipsis: true
    },
    {
      key: 'deletedAt',
      dataIndex: 'deletedAt',
      title: t('common.entity.deletedTime'),
      width: 180,
      ellipsis: true,
      sorter: (a: RowRecord, b: RowRecord) => {
        const aTime = a.deletedAt ? new Date(String(a.deletedAt)).getTime() : 0
        const bTime = b.deletedAt ? new Date(String(b.deletedAt)).getTime() : 0
        return aTime - bTime
      }
    }
  ]
  
  // 如果不需要包含审计字段，则过滤掉
  if (!includeAuditFields) {
    return allColumns.filter(col => {
      const key = getColumnKey(col as ColumnItem)
      return key != null && !AUDIT_FIELDS.includes(key as AuditFieldName)
    })
  }
  
  return allColumns
}

/**
 * 合并默认字段到用户定义的列中
 * 如果用户已定义同名字段（通过 key 或 dataIndex），则使用用户定义
 * @param userColumns 用户定义的列
 * @param t 翻译函数
 * @param includeAuditFields 是否包含审计字段（默认 true，默认显示所有字段）
 * @returns 合并后的列配置
 */
export function mergeDefaultColumns(userColumns: TableColumnsType, t: (key: string) => string, includeAuditFields: boolean = true): TableColumnsType {
  const defaultColumns = getDefaultEntityColumns(t, includeAuditFields)
  
  // 获取用户已定义的列的键（key 或 dataIndex）
  const userColumnKeys = new Set<string>()
  userColumns.forEach(col => {
    // 检查是否为 ColumnGroupType（有 children 属性）
    if ('children' in col && col.children) {
      // 如果是分组列，递归处理子列
      col.children.forEach((childCol: ColumnType<RowRecord>) => {
        const key = getColumnKey(childCol)
        if (key) {
          userColumnKeys.add(key)
        }
      })
    } else {
      // 普通列
      const key = getColumnKey(col as ColumnItem)
      if (key) {
        userColumnKeys.add(key)
      }
    }
  })
  
  // 过滤出用户未定义的默认列
  const missingDefaultColumns = defaultColumns.filter(col => {
    const key = getColumnKey(col as ColumnItem)
    return key != null && !userColumnKeys.has(key)
  })
  
  // 分离操作列（key='action' 或 fixed='right' 的列）
  // 保持操作列的相对顺序
  const actionColumns: TableColumnsType = []
  const otherUserColumns: TableColumnsType = []
  
  userColumns.forEach((col) => {
    const item = col as ColumnItem
    const key = getColumnKey(item)
    // 如果是操作列（key='action'）或固定右侧的列，放到操作列数组
    if (key === 'action' || item.fixed === 'right') {
      actionColumns.push(col)
    } else {
      otherUserColumns.push(col)
    }
  })
  
  // 合并：其他用户列 + 缺失的默认列 + 操作列（确保操作列在最后）
  const mergedColumns = [...otherUserColumns, ...missingDefaultColumns, ...actionColumns]
  
  // 如果不包含审计字段，过滤掉用户定义的审计字段列
  if (!includeAuditFields) {
    return mergedColumns.map((col) => {
      const item = col as ColumnItem
      const key = getColumnKey(item)
      // 如果是分组列，检查子列
      if ('children' in item && item.children) {
        // 过滤子列中的审计字段
        const filteredChildren = item.children.filter((childCol: ColumnItem) => {
          const childKey = getColumnKey(childCol)
          return childKey != null && !AUDIT_FIELDS.includes(childKey as AuditFieldName)
        })
        // 如果所有子列都被过滤掉了，返回 null（后续会被过滤掉）
        if (filteredChildren.length === 0) {
          return null
        }
        // 创建新的分组列对象，包含过滤后的子列
        return {
          ...item,
          children: filteredChildren
        }
      }
      // 普通列：如果是审计字段，返回 null（后续会被过滤掉）
      if (key != null && AUDIT_FIELDS.includes(key as AuditFieldName)) {
        return null
      }
      return col
    }).filter((col): col is ColumnItem => col !== null) as TableColumnsType
  }
  
  return mergedColumns
}
