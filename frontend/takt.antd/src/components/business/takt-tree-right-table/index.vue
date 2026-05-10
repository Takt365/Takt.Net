<!-- ========================================
项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间:@/components/business/takt-tree-right-table
文件名称:index.vue
创建时间:2025-01-20
创建人:Takt365(Cursor AI)
功能描述:右表区域,用于树表布局右侧的表格

版权信息:Copyright (c) 2025 Takt  All rights reserved.
免责声明:此软件使用 MIT License,作者不承担任何使用风险。
======================================== -->

<template>
  <div class="takt-tree-right-table">
    <a-table
      class="ant-table-striped"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :pagination="false"
      :row-key="rowKey"
      :row-class-name="rowClassName"
      :scroll="scrollConfig"
      :virtual="virtual"
      :size="size"
      :bordered="bordered"
      v-bind="{ ...$attrs, ...(effectiveRowSelection ? { 'row-selection': effectiveRowSelection } : {}), 'expanded-row-keys': expandedRowKeys }"
      @update:expanded-row-keys="(keys) => emit('update:expandedRowKeys', keys)"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template
        v-for="(_, name) in $slots"
        #[name]="slotData"
      >
        <slot
          :name="name"
          v-bind="slotData"
        />
      </template>
      <template
        v-if="$slots.summary"
        #summary
      >
        <slot name="summary" />
      </template>
    </a-table>
  </div>
</template>

<script setup lang="ts">
import type { TableColumnsType, TableProps, TablePaginationConfig } from 'ant-design-vue'
import type { ColumnType, SorterResult, FilterValue, TableCurrentDataSource } from 'ant-design-vue/es/table/interface'

type TableRecord = Record<string, unknown>
type TableSorter = { field?: string | number | readonly (string | number)[]; order?: string }
type TableFilters = Record<string, unknown>
type TablePagination = { current?: number; pageSize?: number; total?: number }
type ResizableColumn = { width?: number | string }

interface Props {
  /** 表格列配置 */
  columns: TableColumnsType
  /** 数据源 */
  dataSource?: TableRecord[]
  /** 加载状态 */
  loading?: boolean
  /** 行键 */
  rowKey?: string | ((record: TableRecord) => string)
  /** 自定义行类名(斑马纹等) */
  rowClassName?: string | ((record: TableRecord, index: number) => string)
  /** 是否启用斑马纹 */
  stripe?: boolean
  /** 是否启用虚拟滚动 */
  virtual?: boolean
  /** 滚动配置 */
  scroll?: { x?: number | string | true; y?: number | string }
  /** 表格尺寸 */
  size?: TableProps['size']
  /** 是否显示边框 */
  bordered?: boolean
  /** 行选择配置 */
  rowSelection?: TableProps['rowSelection']
  /** 是否默认显示行选择列（默认 true） */
  showRowSelection?: boolean
  /** 展开的行 key 列表（树表展开用，v-model:expanded-row-keys） */
  expandedRowKeys?: (string | number)[]
  /** 是否默认省略 */
  defaultEllipsis?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  dataSource: () => [],
  loading: false,
  rowKey: 'id',
  stripe: true,
  virtual: false,
  size: 'middle',
  bordered: false,
  showRowSelection: true,
  expandedRowKeys: () => [],
  defaultEllipsis: true
})

const emit = defineEmits<{
  'change': [pagination: TablePagination, filters: TableFilters, sorter: TableSorter]
  'resize-column': [width: number, column: ResizableColumn]
  'update:expandedRowKeys': [keys: (string | number)[]]
}>()

/** 行选择：默认显示选择列（showRowSelection 为 true 且未传 rowSelection 时用空对象） */
const effectiveRowSelection = computed(() => {
  if (!props.showRowSelection) return undefined
  return props.rowSelection !== undefined && props.rowSelection !== null ? props.rowSelection : {}
})

const rowClassName = computed(() => {
  if (props.rowClassName) return props.rowClassName
  if (props.stripe) {
    return (_record: TableRecord, index: number) => (index % 2 === 1 ? 'table-striped' : '')
  }
  return ''
})

const displayColumns = computed<TableColumnsType>(() => {
  const cols = props.columns
  const visibleCount = cols.length
  return cols.map((column) => {
    const processedColumn = { ...column } as ColumnType<any>
    if (!processedColumn.width && visibleCount > 0) {
      const viewportWidth = window.innerWidth - 40
      processedColumn.width = Math.floor(viewportWidth / 9)
    }
    if (props.defaultEllipsis && column.ellipsis === undefined) {
      processedColumn.ellipsis = true
    }
    return processedColumn
  })
})

const scrollConfig = computed(() => {
  const config: { x?: number | string | true; y?: number | string } = { ...props.scroll }
  if (!config.x) {
    const totalWidth = displayColumns.value.reduce((sum: number, col) => {
      const width = (col as ResizableColumn).width
      return sum + (typeof width === 'number' ? width : 0)
    }, 0)
    config.x = totalWidth > 0 && displayColumns.value.every((col) => !!(col as ResizableColumn).width) ? totalWidth : 'max-content'
  }
  if (props.virtual && !config.y) config.y = 600
  return config
})

const handleTableChange = (pagination: TablePaginationConfig, filters: Record<string, FilterValue | null>, sorter: SorterResult<any> | SorterResult<any>[], _extra: TableCurrentDataSource<any>) => {
  const finalSorter = Array.isArray(sorter) ? sorter[0] : sorter
  emit('change', 
    { current: pagination.current, pageSize: pagination.pageSize, total: pagination.total } as TablePagination, 
    filters as TableFilters, 
    (finalSorter || {}) as TableSorter
  )
}

const handleResizeColumn = (w: number, col: ColumnType<any>) => {
  ;(col as any).width = w
  emit('resize-column', w, col as any)
}
</script>

<style scoped lang="less">
/* 右边距：用宽度 calc(80% - 4px) 在右侧留出 4px 空隙，不依赖 margin/padding，避免被 layout-content overflow-x 裁掉 */
.takt-tree-right-table {
  flex: 0 0 calc(80% );
  width: calc(80% );
  max-width: calc(80%);
  min-width: 0;
  //margin: 0 0 4px 0;
  padding: 4px;
  box-sizing: border-box;
  overflow: hidden;
}
</style>
