<template>
  <div class="takt-single-table">
    <a-table
      class="ant-table-striped"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :pagination="false"
      :row-key="rowKey"
      :row-class-name="(_record, index) => (index % 2 === 1 ? 'table-striped' : null) as any"
      :scroll="scrollConfig"
      :virtual="virtual"
      :size="size"
      :bordered="bordered"
      :row-selection="effectiveRowSelection"
      @change="handleTableChange"
      @resizeColumn="handleResizeColumn"
      v-bind="$attrs"
    >
      <template v-for="(_, name) in $slots" #[name]="slotData">
        <slot :name="name" v-bind="slotData" />
      </template>
      <!-- 总结栏插槽
       * 使用方式：在组件中使用 <template #summary> 插槽
       * 示例：
       * <template #summary>
       *   <a-table-summary fixed>
       *     <a-table-summary-row>
       *       <a-table-summary-cell :index="0">总计</a-table-summary-cell>
       *       <a-table-summary-cell :index="1">{{ totalAmount }}</a-table-summary-cell>
       *     </a-table-summary-row>
       *   </a-table-summary>
       * </template>
       -->
      <template v-if="$slots.summary" #summary>
        <slot name="summary" />
      </template>
    </a-table>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import type { TableColumnsType, TableProps } from 'ant-design-vue'

interface Props {
  /** 表格列配置
   * 支持功能：
   * - 列宽自定义：在 columns 中设置 width 属性（如：{ width: 200 }）
   * - 排序功能：在 columns 中设置 sorter: true/false 或 sorter 函数（如：{ sorter: (a, b) => a.age - b.age }）
   * - 文本省略：在 columns 中设置 ellipsis: true/false 或 { ellipsis: { showTitle: true } }
   * - 列宽调整：在 columns 中设置 resizable: true/false，启用后可通过拖拽调整列宽，会触发 @resize-column 事件
   */
  columns: TableColumnsType
  /** 数据源 */
  dataSource?: any[]
  /** 加载状态 */
  loading?: boolean
  /** 行键 */
  rowKey?: string | ((record: any) => string)
  /** 自定义行类名（用于自定义带斑马纹的表格）
   * 如果提供函数，函数接收 (record, index) 参数，返回类名字符串
   * 如果不提供且 stripe 为 true，则自动应用斑马纹样式
   */
  rowClassName?: string | ((record: any, index: number) => string)
  /** 是否启用斑马纹（默认 true，会自动为奇数行添加 takt-table-row-stripe 类） */
  stripe?: boolean
  /** 是否启用虚拟滚动（大数据量时建议开启）
   * 启用虚拟滚动时，必须设置 scroll.y 指定滚动高度，否则虚拟滚动不会生效
   */
  virtual?: boolean
  /** 滚动配置（支持虚拟滚动）
   * - x: 横向滚动，可设置为 true（自动计算）或具体数值
   * - y: 纵向滚动，启用虚拟滚动时必须设置此值（如：{ y: 600 }）
   */
  scroll?: { x?: number | string | true; y?: number | string }
  /** 表格尺寸 */
  size?: TableProps['size']
  /** 是否显示边框 */
  bordered?: boolean
  /** 行选择配置 */
  rowSelection?: TableProps['rowSelection']
  /** 是否默认显示行选择列（默认 true） */
  showRowSelection?: boolean
  /** 是否默认启用所有列的文本省略（默认 true，会自动为没有设置 ellipsis 的列添加 ellipsis: true） */
  defaultEllipsis?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  dataSource: () => [],
  loading: false,
  rowKey: 'id',
  rowClassName: undefined,
  stripe: true,
  virtual: false,
  size: 'middle',
  bordered: false,
  showRowSelection: true,
  defaultEllipsis: true
})

const emit = defineEmits<{
  'change': [pagination: any, filters: any, sorter: any]
  'resize-column': [width: number, column: any]
}>()

/** 行选择：默认显示选择列（showRowSelection 为 true 且未传 rowSelection 时用空对象） */
const effectiveRowSelection = computed(() => {
  if (!props.showRowSelection) return undefined
  return props.rowSelection !== undefined && props.rowSelection !== null ? props.rowSelection : {}
})

// 处理列宽调整（使用 Ant Design Vue 原生支持）
// 注意：直接修改 col.width 以立即更新显示，同时触发事件让父组件更新原始数据
const handleResizeColumn = (w: number, col: any) => {
  // 更新当前列的宽度（用于立即显示）
  col.width = w
  // 触发事件，让父组件更新原始数据源
  emit('resize-column', w, col)
}

// 直接使用传入的 columns，不再进行任何合并处理
// 所有列的合并和过滤都由 TaktColumnDrawer 统一处理
const displayColumns = computed(() => {
  const visibleCount = props.columns.length
  
  return props.columns.map((column: any) => {
    const processedColumn: any = { ...column }
    
    // 如果列没有设置 width，默认设置为视口的 1/9（假设显示9个字段）
    if (!processedColumn.width && visibleCount > 0) {
      // 计算视口宽度（减去表格的 padding 和边框，大约减去 40px）
      const viewportWidth = window.innerWidth - 40
      processedColumn.width = Math.floor(viewportWidth / 9)
    }
    
    // 处理 ellipsis
    if (props.defaultEllipsis && column.ellipsis === undefined) {
      processedColumn.ellipsis = true
    }
    
    // resizable 属性直接传递给 a-table，Ant Design Vue 原生支持
    
    return processedColumn
  })
})



// 滚动配置（支持虚拟滚动）
// 注意：启用虚拟滚动时，必须设置 scroll.y，否则虚拟滚动不会生效
// 注意：必须设置 scroll.x 才能让列宽按照配置的 width 显示，否则会按内容自动调整
const scrollConfig = computed(() => {
  const config: { x?: number | string | true; y?: number | string } = {
    ...props.scroll
  }
  
  // 如果没有设置 scroll.x，自动计算所有列的宽度总和，确保列宽按照配置显示
  if (!config.x) {
    const totalWidth = displayColumns.value.reduce((sum: number, col: any) => {
      return sum + (col.width || 0)
    }, 0)
    
    // 如果所有列都有 width 配置，使用总和；否则使用 'max-content' 让表格自动计算
    if (totalWidth > 0 && displayColumns.value.every((col: any) => col.width)) {
      config.x = totalWidth
    } else {
      // 如果有些列没有 width，使用 'max-content' 确保表格能正确显示
      config.x = 'max-content'
    }
  }
  
  // 如果启用虚拟滚动，必须设置 y 轴滚动高度
  // 如果没有提供 scroll.y，使用默认高度 600px
  if (props.virtual) {
    if (!config.y) {
      config.y = 600 // 默认高度
    }
  }
  
  return config
})

// 表格变化处理
const handleTableChange = (pagination: any, filters: any, sorter: any) => {
  emit('change', pagination, filters, sorter)
}
</script>

<style scoped lang="less">
.takt-single-table {
  margin: 0 4px 4px 4px;
  width: 100%;
  overflow: hidden;
}

</style>
