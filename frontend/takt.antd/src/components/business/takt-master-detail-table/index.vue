<template>
  <div class="takt-master-detail-table">
    <!-- 主表 -->
    <a-table
      class="ant-table-striped"
      :columns="displayMasterColumns"
      :data-source="masterDataSource"
      :loading="masterLoading"
      :pagination="masterPaginationConfig"
      :row-key="masterRowKey"
      :row-class-name="(_record: any, index: number) => (index % 2 === 1 ? 'table-striped' : null) as any"
      :scroll="masterScrollConfig"
      :virtual="masterVirtual"
      :size="size"
      :bordered="bordered"
      :row-selection="props.masterRowSelection"
      :expanded-row-keys="expandedRowKeys"
      @change="handleMasterTableChange"
      @expand="handleExpand"
      @resizeColumn="handleMasterResizeColumn"
      v-bind="$attrs"
    >
      <template v-for="(_, name) in $slots" #[name]="slotData">
        <slot :name="name" v-bind="slotData" />
      </template>
      <!-- 主表总结栏插槽
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
    
    <TaktPagination
      v-if="showMasterPagination"
      v-model:current="masterCurrentPage"
      v-model:page-size="masterPageSize"
      :total="masterTotal"
      :show-size-changer="showSizeChanger"
      :show-jumper="showQuickJumper"
      :show-total="computedShowTotal"
      :page-size-options="pageSizeOptions"
      :size="paginationSize"
      :simple="simplePagination"
      @change="handleMasterPaginationChange"
      @show-size-change="handleMasterPaginationSizeChange"
    />

    <!-- 从表展开内容 -->
    <a-drawer
      v-if="detailDrawerVisible"
      v-model:open="detailDrawerVisible"
      :title="detailDrawerTitleDisplay"
      :width="detailDrawerWidth"
      placement="right"
      class="takt-master-detail-table-drawer"
    >
      <a-table
        class="ant-table-striped"
        :columns="displayDetailColumns"
        :data-source="detailDataSource"
        :loading="detailLoading"
        :pagination="detailPaginationConfig"
        :row-key="detailRowKey"
        :row-class-name="(_record: any, index: number) => (index % 2 === 1 ? 'table-striped' : null) as any"
        :scroll="detailScrollConfig"
        :virtual="detailVirtual"
        :size="size"
        :bordered="bordered"
        @change="handleDetailTableChange"
      @resizeColumn="handleDetailResizeColumn"
      >
        <template
          v-for="(_, name) in detailSlotNames"
          :key="name"
          #[name]="slotData"
        >
          <slot :name="name" v-bind="slotData" />
        </template>
        <!-- 从表总结栏插槽
         * 使用方式：在组件中使用 <template #detail-summary> 插槽
         * 示例：
         * <template #detail-summary>
         *   <a-table-summary fixed>
         *     <a-table-summary-row>
         *       <a-table-summary-cell :index="0">小计</a-table-summary-cell>
         *       <a-table-summary-cell :index="1">{{ detailTotalAmount }}</a-table-summary-cell>
         *     </a-table-summary-row>
         *   </a-table-summary>
         * </template>
         -->
        <template v-if="$slots['detail-summary']" #summary>
          <slot name="detail-summary" />
        </template>
      </a-table>
      
      <TaktPagination
        v-if="showDetailPagination"
        v-model:current="detailCurrentPage"
        v-model:page-size="detailPageSize"
        :total="detailTotal"
        :show-size-changer="showSizeChanger"
        :show-jumper="showQuickJumper"
        :show-total="computedShowTotal"
        :page-size-options="pageSizeOptions"
        :size="paginationSize"
        :simple="simplePagination"
        @change="handleDetailPaginationChange"
        @show-size-change="handleDetailPaginationSizeChange"
      />
    </a-drawer>
    
    <!-- 主表列设置抽屉 -->
    <TaktColumnDrawer
      v-model:open="masterColumnSettingVisible"
      :columns="mergedMasterColumns"
      :checked-keys="masterVisibleColumnKeys"
      :id-column-key="masterIdColumnKey"
      :action-column-key="masterActionColumnKey"
      @update:checked-keys="handleMasterColumnKeysChange"
      @reset="handleMasterColumnSettingReset"
    />
    
    <!-- 从表列设置抽屉 -->
    <TaktColumnDrawer
      v-model:open="detailColumnSettingVisible"
      :columns="mergedDetailColumns"
      :checked-keys="detailVisibleColumnKeys"
      :id-column-key="detailIdColumnKey"
      :action-column-key="detailActionColumnKey"
      @update:checked-keys="handleDetailColumnKeysChange"
      @reset="handleDetailColumnSettingReset"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted, onUnmounted } from 'vue'
import type { TableColumnsType, TableProps } from 'ant-design-vue'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import TaktPagination from '@/components/business/takt-pagination/index.vue'
import TaktColumnDrawer from '@/components/business/takt-column-drawer/index.vue'

const { t } = useI18n()

interface Props {
  /** 主表列配置
   * 支持功能：
   * - 列宽自定义：在 columns 中设置 width 属性（如：{ width: 200 }）
   * - 排序功能：在 columns 中设置 sorter: true/false 或 sorter 函数（如：{ sorter: (a, b) => a.age - b.age }）
   * - 文本省略：在 columns 中设置 ellipsis: true/false 或 { ellipsis: { showTitle: true } }
   * - 列宽调整：在 columns 中设置 resizable: true/false，启用后可通过拖拽调整列宽，会触发 @master-resize-column 事件
   */
  masterColumns: TableColumnsType
  /** 主表数据源 */
  masterDataSource?: any[]
  /** 主表加载状态 */
  masterLoading?: boolean
  /** 主表行键 */
  masterRowKey?: string | ((record: any) => string)
  /** 主表自定义行类名 */
  masterRowClassName?: string | ((record: any, index: number) => string)
  /** 主表是否启用斑马纹 */
  masterStripe?: boolean
  /** 是否显示主表分页 */
  showMasterPagination?: boolean
  /** 主表分页配置（已废弃，不再使用 a-table 内置分页，始终使用 takt-pagination） */
  masterPagination?: false | TableProps['pagination']
  /** 主表当前页码 */
  masterCurrent?: number
  /** 主表每页条数 */
  masterPageSize?: number
  /** 主表数据总数 */
  masterTotal?: number
  /** 从表列配置
   * 支持功能：
   * - 列宽自定义：在 columns 中设置 width 属性（如：{ width: 200 }）
   * - 排序功能：在 columns 中设置 sorter: true/false 或 sorter 函数（如：{ sorter: (a, b) => a.age - b.age }）
   * - 文本省略：在 columns 中设置 ellipsis: true/false 或 { ellipsis: { showTitle: true } }
   * - 列宽调整：在 columns 中设置 resizable: true/false，启用后可通过拖拽调整列宽，会触发 @detail-resize-column 事件
   */
  detailColumns: TableColumnsType
  /** 从表数据源 */
  detailDataSource?: any[]
  /** 从表加载状态 */
  detailLoading?: boolean
  /** 从表行键 */
  detailRowKey?: string | ((record: any) => string)
  /** 从表自定义行类名 */
  detailRowClassName?: string | ((record: any, index: number) => string)
  /** 从表是否启用斑马纹 */
  detailStripe?: boolean
  /** 是否显示从表分页 */
  showDetailPagination?: boolean
  /** 从表分页配置（已废弃，不再使用 a-table 内置分页，始终使用 takt-pagination） */
  detailPagination?: false | TableProps['pagination']
  /** 从表当前页码 */
  detailCurrent?: number
  /** 从表每页条数 */
  detailPageSize?: number
  /** 从表数据总数 */
  detailTotal?: number
  /** 是否显示每页条数选择器 */
  showSizeChanger?: boolean
  /** 是否显示快速跳转 */
  showQuickJumper?: boolean
  /** 是否显示总数 */
  showTotal?: boolean | ((total: number, range: [number, number]) => string)
  /** 每页条数选项 */
  pageSizeOptions?: string[]
  /** 分页尺寸 */
  paginationSize?: 'default' | 'small'
  /** 简单分页 */
  simplePagination?: boolean
  /** 主表是否启用虚拟滚动 */
  masterVirtual?: boolean
  /** 从表是否启用虚拟滚动 */
  detailVirtual?: boolean
  /** 主表滚动配置 */
  masterScroll?: { x?: number | string | true; y?: number | string }
  /** 从表滚动配置 */
  detailScroll?: { x?: number | string | true; y?: number | string }
  /** 表格尺寸 */
  size?: TableProps['size']
  /** 是否显示边框 */
  bordered?: boolean
  /** 主表行选择配置 */
  masterRowSelection?: TableProps['rowSelection']
  /** 从表抽屉标题 */
  detailDrawerTitle?: string
  /** 从表抽屉宽度 */
  detailDrawerWidth?: number | string
  /** 从表数据加载函数 */
  loadDetailData?: (masterRecord: any, page: number, pageSize: number) => Promise<any[]>
  /** 是否默认启用所有列的文本省略（默认 true，会自动为没有设置 ellipsis 的列添加 ellipsis: true） */
  defaultEllipsis?: boolean
  /** 主表是否默认启用所有列的文本省略（默认 true，会自动为没有设置 ellipsis 的列添加 ellipsis: true） */
  masterDefaultEllipsis?: boolean
  /** 从表是否默认启用所有列的文本省略（默认 true，会自动为没有设置 ellipsis 的列添加 ellipsis: true） */
  detailDefaultEllipsis?: boolean
  /** 是否显示主表列设置抽屉 */
  showMasterColumnSetting?: boolean
  /** 主表ID列的键（用于固定显示，默认 'id'） */
  masterIdColumnKey?: string | number
  /** 主表操作列的键（用于固定显示，默认 'action'） */
  masterActionColumnKey?: string | number
  /** 主表大屏默认显示的列数（不包括ID列和操作列，默认 9） */
  masterLargeScreenColumnCount?: number
  /** 主表小屏默认显示的列数（不包括ID列和操作列，默认 5） */
  masterSmallScreenColumnCount?: number
  /** 是否显示从表列设置抽屉 */
  showDetailColumnSetting?: boolean
  /** 从表ID列的键（用于固定显示，默认 'id'） */
  detailIdColumnKey?: string | number
  /** 从表操作列的键（用于固定显示，默认 'action'） */
  detailActionColumnKey?: string | number
  /** 从表大屏默认显示的列数（不包括ID列和操作列，默认 9） */
  detailLargeScreenColumnCount?: number
  /** 从表小屏默认显示的列数（不包括ID列和操作列，默认 5） */
  detailSmallScreenColumnCount?: number
  /** 大屏断点（默认 1200px） */
  largeScreenBreakpoint?: number
}

const props = withDefaults(defineProps<Props>(), {
  masterDataSource: () => [],
  masterLoading: false,
  masterRowKey: 'id',
  masterRowClassName: undefined,
  masterStripe: true,
  showMasterPagination: true,
  masterCurrent: 1,
  masterPageSize: 10,
  masterTotal: 0,
  detailColumns: () => [],
  detailDataSource: () => [],
  detailLoading: false,
  detailRowKey: 'id',
  detailRowClassName: undefined,
  detailStripe: true,
  showDetailPagination: true,
  detailCurrent: 1,
  detailPageSize: 10,
  detailTotal: 0,
  showSizeChanger: true,
  showQuickJumper: false,
  showTotal: true,
  pageSizeOptions: () => ['10', '20', '50', '100'],
  paginationSize: 'default',
  simplePagination: false,
  masterVirtual: false,
  detailVirtual: false,
  size: 'middle',
  bordered: false,
  detailDrawerTitle: undefined,
  detailDrawerWidth: 800,
  defaultEllipsis: true,
  masterDefaultEllipsis: true,
  detailDefaultEllipsis: true,
  showMasterColumnSetting: false,
  masterIdColumnKey: 'id',
  masterActionColumnKey: 'action',
  masterLargeScreenColumnCount: 9,
  masterSmallScreenColumnCount: 5,
  showDetailColumnSetting: false,
  detailIdColumnKey: 'id',
  detailActionColumnKey: 'action',
  detailLargeScreenColumnCount: 9,
  detailSmallScreenColumnCount: 5,
  largeScreenBreakpoint: 1200
})

const emit = defineEmits<{
  'update:masterCurrent': [page: number]
  'update:masterPageSize': [pageSize: number]
  'update:detailCurrent': [page: number]
  'update:detailPageSize': [pageSize: number]
  'master-change': [pagination: any, filters: any, sorter: any]
  'detail-change': [pagination: any, filters: any, sorter: any]
  'expand': [expanded: boolean, record: any]
  'master-pagination-change': [page: number, pageSize: number]
  'detail-pagination-change': [page: number, pageSize: number]
  'master-column-setting-change': [keys: string[]]
  'detail-column-setting-change': [keys: string[]]
  'master-resize-column': [width: number, column: any]
  'detail-resize-column': [width: number, column: any]
}>()

const masterCurrentPage = ref(props.masterCurrent)
const masterPageSize = ref(props.masterPageSize)
const detailCurrentPage = ref(props.detailCurrent)
const detailPageSize = ref(props.detailPageSize)
const expandedRowKeys = ref<string[]>([])
const detailDrawerVisible = ref(false)
const currentMasterRecord = ref<any>(null)

const detailDrawerTitleDisplay = computed(() => props.detailDrawerTitle ?? t('common.button.detail'))

// 从表插槽名称列表（过滤出以 'detail-' 开头的插槽）
const slots = useSlots()
const detailSlotNames = computed(() => {
  return Object.keys(slots).filter(
    (name) => typeof name === 'string' && name.startsWith('detail-')
  ) as string[]
})

// 监听 props 变化
watch(() => props.masterCurrent, (val) => {
  masterCurrentPage.value = val
})

watch(() => props.masterPageSize, (val) => {
  masterPageSize.value = val
})

watch(() => props.detailCurrent, (val) => {
  detailCurrentPage.value = val
})

watch(() => props.detailPageSize, (val) => {
  detailPageSize.value = val
})

// 主表分页配置 - 始终禁用 a-table 的分页，只使用 takt-pagination
const masterPaginationConfig = computed<false>(() => {
  // 始终返回 false，禁用 a-table 的内置分页
  return false
})

// 从表分页配置 - 始终禁用 a-table 的分页，只使用 takt-pagination
const detailPaginationConfig = computed<false>(() => {
  // 始终返回 false，禁用 a-table 的内置分页
  return false
})

// 处理 showTotal：将 boolean 转换为 Function 或 undefined
const computedShowTotal = computed(() => {
  if (typeof props.showTotal === 'function') {
    return props.showTotal
  }
  if (props.showTotal === true) {
    // 默认显示总数函数
    return (total: number, range: [number, number]) => t('components.navigation.systemSetting.totalCount', { total })
  }
  // showTotal 为 false 或 undefined 时，返回 undefined（不显示总数）
  return undefined
})

// 响应式检测（大屏/小屏）
const isLargeScreen = ref(window.innerWidth >= props.largeScreenBreakpoint)

// 监听窗口大小变化
const handleResize = () => {
  isLargeScreen.value = window.innerWidth >= props.largeScreenBreakpoint
}

onMounted(() => {
  window.addEventListener('resize', handleResize)
})

onUnmounted(() => {
  window.removeEventListener('resize', handleResize)
})

// 主表列设置相关
const masterColumnSettingVisible = ref(false)
const masterVisibleColumnKeys = ref<string[]>([])

// 从表列设置相关
const detailColumnSettingVisible = ref(false)
const detailVisibleColumnKeys = ref<string[]>([])

// 获取列键（统一转换为字符串）
const getColumnKey = (column: any): string => {
  const key = column.key || column.dataIndex || column.title
  return String(key)
}

// 合并主表默认字段列
const mergedMasterColumns = computed(() => {
  return mergeDefaultColumns(props.masterColumns, t)
})

// 合并从表默认字段列
const mergedDetailColumns = computed(() => {
  return mergeDefaultColumns(props.detailColumns, t)
})

// 初始化主表可见列键
const initMasterVisibleColumnKeys = () => {
  const allKeys = mergedMasterColumns.value.map(col => getColumnKey(col))
  const idKey = String(props.masterIdColumnKey)
  const actionKey = String(props.masterActionColumnKey)
  
  const nonFixedColumns = mergedMasterColumns.value.filter(col => {
    const key = getColumnKey(col)
    return key !== idKey && key !== actionKey
  })
  
  const columnCount = isLargeScreen.value 
    ? props.masterLargeScreenColumnCount 
    : props.masterSmallScreenColumnCount
  
  const selectedNonFixedKeys = nonFixedColumns
    .slice(0, columnCount)
    .map(col => getColumnKey(col))
  
  const keys = [idKey, ...selectedNonFixedKeys, actionKey].filter(key => 
    allKeys.includes(key)
  )
  
  masterVisibleColumnKeys.value = keys
}

// 初始化从表可见列键
const initDetailVisibleColumnKeys = () => {
  const allKeys = mergedDetailColumns.value.map(col => getColumnKey(col))
  const idKey = String(props.detailIdColumnKey)
  const actionKey = String(props.detailActionColumnKey)
  
  const nonFixedColumns = mergedDetailColumns.value.filter(col => {
    const key = getColumnKey(col)
    return key !== idKey && key !== actionKey
  })
  
  const columnCount = isLargeScreen.value 
    ? props.detailLargeScreenColumnCount 
    : props.detailSmallScreenColumnCount
  
  const selectedNonFixedKeys = nonFixedColumns
    .slice(0, columnCount)
    .map(col => getColumnKey(col))
  
  const keys = [idKey, ...selectedNonFixedKeys, actionKey].filter(key => 
    allKeys.includes(key)
  )
  
  detailVisibleColumnKeys.value = keys
}

// 监听屏幕大小变化
watch(isLargeScreen, () => {
  if (!props.showMasterColumnSetting || masterVisibleColumnKeys.value.length === 0) {
    initMasterVisibleColumnKeys()
  }
  if (!props.showDetailColumnSetting || detailVisibleColumnKeys.value.length === 0) {
    initDetailVisibleColumnKeys()
  }
}, { immediate: true })

// 监听列变化
watch(() => props.masterColumns, () => {
  if (!props.showMasterColumnSetting || masterVisibleColumnKeys.value.length === 0) {
    initMasterVisibleColumnKeys()
  }
}, { immediate: true, deep: true })

watch(() => props.detailColumns, () => {
  if (!props.showDetailColumnSetting || detailVisibleColumnKeys.value.length === 0) {
    initDetailVisibleColumnKeys()
  }
}, { immediate: true, deep: true })

// 监听列设置开关变化
watch(() => props.showMasterColumnSetting, (val) => {
  if (val && masterVisibleColumnKeys.value.length === 0) {
    initMasterVisibleColumnKeys()
  }
}, { immediate: true })

watch(() => props.showDetailColumnSetting, (val) => {
  if (val && detailVisibleColumnKeys.value.length === 0) {
    initDetailVisibleColumnKeys()
  }
}, { immediate: true })

// 主表列设置处理
const handleMasterColumnKeysChange = (keys: string[]) => {
  masterVisibleColumnKeys.value = keys
  emit('master-column-setting-change', keys)
}

const handleMasterColumnSettingReset = () => {
  initMasterVisibleColumnKeys()
}

// 从表列设置处理
const handleDetailColumnKeysChange = (keys: string[]) => {
  detailVisibleColumnKeys.value = keys
  emit('detail-column-setting-change', keys)
}

const handleDetailColumnSettingReset = () => {
  initDetailVisibleColumnKeys()
}

// 暴露列设置方法
defineExpose({
  openMasterColumnSetting: () => {
    masterColumnSettingVisible.value = true
  },
  openDetailColumnSetting: () => {
    detailColumnSettingVisible.value = true
  }
})

// 处理主表列宽调整（使用 Ant Design Vue 原生支持）
const handleMasterResizeColumn = (w: number, col: any) => {
  col.width = w
  emit('master-resize-column', w, col)
}

// 处理从表列宽调整（使用 Ant Design Vue 原生支持）
const handleDetailResizeColumn = (w: number, col: any) => {
  col.width = w
  emit('detail-resize-column', w, col)
}

// 处理主表列配置，默认启用 ellipsis，支持 resizable，设置默认列宽
const processedMasterColumns = computed(() => {
  const shouldEllipsis = props.masterDefaultEllipsis ?? props.defaultEllipsis ?? true
  // 计算可见列数（用于设置默认列宽）
  const visibleCount = masterVisibleColumnKeys.value.length || mergedMasterColumns.value.length
  
  return mergedMasterColumns.value.map((column: any) => {
    const processedColumn: any = { ...column }
    
    // 如果列没有设置 width，默认设置为视口的 1/9（假设显示9个字段）
    if (!processedColumn.width && visibleCount > 0) {
      // 计算视口宽度（减去表格的 padding 和边框，大约减去 40px）
      const viewportWidth = window.innerWidth - 40
      processedColumn.width = Math.floor(viewportWidth / 9)
    }
    
    // 处理 ellipsis
    if (shouldEllipsis && column.ellipsis === undefined) {
      processedColumn.ellipsis = true
    }
    
    // resizable 属性直接传递给 a-table，Ant Design Vue 原生支持
    
    return processedColumn
  })
})

// 主表显示列（根据可见列键过滤）
const displayMasterColumns = computed(() => {
  if (!props.showMasterColumnSetting && masterVisibleColumnKeys.value.length === 0) {
    // 如果未启用列设置，使用响应式默认显示
    const idKey = String(props.masterIdColumnKey)
    const actionKey = String(props.masterActionColumnKey)
    const nonFixedColumns = processedMasterColumns.value.filter((col: any) => {
      const key = getColumnKey(col)
      return key !== idKey && key !== actionKey
    })
    
    const columnCount = isLargeScreen.value 
      ? props.masterLargeScreenColumnCount 
      : props.masterSmallScreenColumnCount
    
    const selectedNonFixed = nonFixedColumns.slice(0, columnCount)
    const idColumn = processedMasterColumns.value.find((col: any) => getColumnKey(col) === idKey)
    const actionColumn = processedMasterColumns.value.find((col: any) => getColumnKey(col) === actionKey)
    
    const result: any[] = []
    if (idColumn) result.push(idColumn)
    result.push(...selectedNonFixed)
    if (actionColumn) result.push(actionColumn)
    
    return result
  }
  
  // 如果启用了列设置，根据 visibleColumnKeys 过滤
  // 如果 visibleColumnKeys 为空，返回空数组（等待初始化）
  if (masterVisibleColumnKeys.value.length === 0) {
    return []
  }
  
  return processedMasterColumns.value.filter((column: any) => {
    const key = getColumnKey(column)
    return masterVisibleColumnKeys.value.includes(key)
  })
})

// 处理从表列配置，默认启用 ellipsis，支持 resizable，设置默认列宽
const processedDetailColumns = computed(() => {
  const shouldEllipsis = props.detailDefaultEllipsis ?? props.defaultEllipsis ?? true
  // 计算可见列数（用于设置默认列宽）
  const visibleCount = detailVisibleColumnKeys.value.length || mergedDetailColumns.value.length
  
  return mergedDetailColumns.value.map((column: any) => {
    const processedColumn: any = { ...column }
    
    // 如果列没有设置 width，默认设置为视口的 1/9（假设显示9个字段）
    if (!processedColumn.width && visibleCount > 0) {
      // 计算视口宽度（减去表格的 padding 和边框，大约减去 40px）
      const viewportWidth = window.innerWidth - 40
      processedColumn.width = Math.floor(viewportWidth / 9)
    }
    
    // 处理 ellipsis
    if (shouldEllipsis && column.ellipsis === undefined) {
      processedColumn.ellipsis = true
    }
    
    // resizable 属性直接传递给 a-table，Ant Design Vue 原生支持
    
    return processedColumn
  })
})

// 从表显示列（根据可见列键过滤）
const displayDetailColumns = computed(() => {
  if (!props.showDetailColumnSetting && detailVisibleColumnKeys.value.length === 0) {
    // 如果未启用列设置，使用响应式默认显示
    const idKey = String(props.detailIdColumnKey)
    const actionKey = String(props.detailActionColumnKey)
    const nonFixedColumns = processedDetailColumns.value.filter((col: any) => {
      const key = getColumnKey(col)
      return key !== idKey && key !== actionKey
    })
    
    const columnCount = isLargeScreen.value 
      ? props.detailLargeScreenColumnCount 
      : props.detailSmallScreenColumnCount
    
    const selectedNonFixed = nonFixedColumns.slice(0, columnCount)
    const idColumn = processedDetailColumns.value.find((col: any) => getColumnKey(col) === idKey)
    const actionColumn = processedDetailColumns.value.find((col: any) => getColumnKey(col) === actionKey)
    
    const result: any[] = []
    if (idColumn) result.push(idColumn)
    result.push(...selectedNonFixed)
    if (actionColumn) result.push(actionColumn)
    
    return result
  }
  
  // 如果启用了列设置，根据 visibleColumnKeys 过滤
  // 如果 visibleColumnKeys 为空，返回空数组（等待初始化）
  if (detailVisibleColumnKeys.value.length === 0) {
    return []
  }
  
  return processedDetailColumns.value.filter((column: any) => {
    const key = getColumnKey(column)
    return detailVisibleColumnKeys.value.includes(key)
  })
})


// 主表滚动配置（支持虚拟滚动）
// 注意：启用虚拟滚动时，必须设置 scroll.y，否则虚拟滚动不会生效
// 注意：必须设置 scroll.x 才能让列宽按照配置的 width 显示，否则会按内容自动调整
const masterScrollConfig = computed(() => {
  const config: { x?: number | string | true; y?: number | string } = {
    ...props.masterScroll
  }
  
  // 如果没有设置 scroll.x，自动计算所有列的宽度总和，确保列宽按照配置显示
  if (!config.x) {
    const totalWidth = displayMasterColumns.value.reduce((sum: number, col: any) => {
      return sum + (col.width || 0)
    }, 0)
    
    // 如果所有列都有 width 配置，使用总和；否则使用 'max-content' 让表格自动计算
    if (totalWidth > 0 && displayMasterColumns.value.every((col: any) => col.width)) {
      config.x = totalWidth
    } else {
      // 如果有些列没有 width，使用 'max-content' 确保表格能正确显示
      config.x = 'max-content'
    }
  }
  
  // 如果启用虚拟滚动，必须设置 y 轴滚动高度
  if (props.masterVirtual && !config.y) {
    config.y = 600 // 默认高度
  }
  return config
})

// 从表滚动配置（支持虚拟滚动）
// 注意：启用虚拟滚动时，必须设置 scroll.y，否则虚拟滚动不会生效
// 注意：必须设置 scroll.x 才能让列宽按照配置的 width 显示，否则会按内容自动调整
const detailScrollConfig = computed(() => {
  const config: { x?: number | string | true; y?: number | string } = {
    ...props.detailScroll
  }
  
  // 如果没有设置 scroll.x，自动计算所有列的宽度总和，确保列宽按照配置显示
  if (!config.x) {
    const totalWidth = displayDetailColumns.value.reduce((sum: number, col: any) => {
      return sum + (col.width || 0)
    }, 0)
    
    // 如果所有列都有 width 配置，使用总和；否则使用 'max-content' 让表格自动计算
    if (totalWidth > 0 && displayDetailColumns.value.every((col: any) => col.width)) {
      config.x = totalWidth
    } else {
      // 如果有些列没有 width，使用 'max-content' 确保表格能正确显示
      config.x = 'max-content'
    }
  }
  
  // 如果启用虚拟滚动，必须设置 y 轴滚动高度
  if (props.detailVirtual && !config.y) {
    config.y = 400 // 默认高度（从表在抽屉中，高度较小）
  }
  return config
})

// 主表变化处理
const handleMasterTableChange = (pagination: any, filters: any, sorter: any) => {
  if (pagination) {
    masterCurrentPage.value = pagination.current
    masterPageSize.value = pagination.pageSize
    emit('update:masterCurrent', pagination.current)
    emit('update:masterPageSize', pagination.pageSize)
  }
  emit('master-change', pagination, filters, sorter)
}

// 从表变化处理
const handleDetailTableChange = (pagination: any, filters: any, sorter: any) => {
  if (pagination) {
    detailCurrentPage.value = pagination.current
    detailPageSize.value = pagination.pageSize
    emit('update:detailCurrent', pagination.current)
    emit('update:detailPageSize', pagination.pageSize)
  }
  emit('detail-change', pagination, filters, sorter)
}

// 展开处理
const handleExpand = async (expanded: boolean, record: any) => {
  if (expanded) {
    currentMasterRecord.value = record
    detailDrawerVisible.value = true
    
    // 如果有加载函数，调用加载从表数据
    if (props.loadDetailData) {
      try {
        const data = await props.loadDetailData(record, detailCurrentPage.value, detailPageSize.value)
        // 这里需要父组件处理数据更新
      } catch (error) {
        logger.error('[TaktMasterDetailTable] 加载从表数据失败:', error)
      }
    }
  } else {
    detailDrawerVisible.value = false
    currentMasterRecord.value = null
  }
  
  emit('expand', expanded, record)
}

// 主表分页变化处理
const handleMasterPaginationChange = (page: number, size: number) => {
  masterCurrentPage.value = page
  masterPageSize.value = size
  emit('update:masterCurrent', page)
  emit('update:masterPageSize', size)
  emit('master-pagination-change', page, size)
}

const handleMasterPaginationSizeChange = (current: number, size: number) => {
  masterCurrentPage.value = current
  masterPageSize.value = size
  emit('update:masterCurrent', current)
  emit('update:masterPageSize', size)
  emit('master-pagination-change', current, size)
}

// 从表分页变化处理
const handleDetailPaginationChange = async (page: number, size: number) => {
  detailCurrentPage.value = page
  detailPageSize.value = size
  emit('update:detailCurrent', page)
  emit('update:detailPageSize', size)
  emit('detail-pagination-change', page, size)
  
  // 如果展开状态且有加载函数，重新加载数据
  if (detailDrawerVisible.value && currentMasterRecord.value && props.loadDetailData) {
    try {
      const data = await props.loadDetailData(currentMasterRecord.value, page, size)
      // 这里需要父组件处理数据更新
    } catch (error) {
      logger.error('[TaktMasterDetailTable] 加载从表数据失败:', error)
    }
  }
}

const handleDetailPaginationSizeChange = async (current: number, size: number) => {
  detailCurrentPage.value = current
  detailPageSize.value = size
  emit('update:detailCurrent', current)
  emit('update:detailPageSize', size)
  emit('detail-pagination-change', current, size)
  
  // 如果展开状态且有加载函数，重新加载数据
  if (detailDrawerVisible.value && currentMasterRecord.value && props.loadDetailData) {
    try {
      const data = await props.loadDetailData(currentMasterRecord.value, current, size)
      // 这里需要父组件处理数据更新
    } catch (error) {
      logger.error('[TaktMasterDetailTable] 加载从表数据失败:', error)
    }
  }
}


</script>

<style scoped lang="less">
.takt-master-detail-table {
  width: 100%;
  overflow: hidden;
  
}

.takt-master-detail-table-drawer {
  :deep(.ant-drawer-body) {
    padding: 16px;
  }
}
</style>
