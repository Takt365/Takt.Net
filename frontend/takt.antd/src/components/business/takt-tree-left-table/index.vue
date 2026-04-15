<!-- ======================================== -->
<!-- 命名空间：@/components/business/takt-tree-left-table -->
<!-- 功能描述：左树区域，用于树表布局左侧的树，宽度为视口比例（如 1/4） -->
<!-- ======================================== -->

<template>
  <div ref="containerRef" class="takt-tree-left-table" :style="leftStyle">
    <a-spin :spinning="loading" class="takt-tree-left-table__spin">
      <a-tree
        class="takt-tree-left-table__tree"
        :class="{ 'draggable-tree': draggable }"
        :tree-data="treeData"
        :field-names="fieldNames"
        v-model:expanded-keys="expandedKeys"
        v-model:selected-keys="selectedKeys"
        :block-node="blockNode"
        :show-line="showLine"
        :selectable="selectable"
        :draggable="draggable"
        :virtual="virtual"
        :height="virtual ? computedVirtualHeight : undefined"
        :item-height="itemHeight"
        @select="handleSelect"
        @dragenter="onDragEnter"
        @drop="onDrop"
      >
        <template v-if="$slots.title" #title="{ title, key, dataRef }">
          <slot name="title" :title="title" :key="key" :data-ref="dataRef" />
        </template>
      </a-tree>
    </a-spin>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount, nextTick } from 'vue'
import { logger } from '@/utils/logger'

export interface TreeFieldNames {
  title?: string
  key?: string
  children?: string
}

interface Props {
  /** 树数据 */
  treeData?: any[]
  /** 树字段映射 */
  treeFieldNames?: TreeFieldNames
  /** 当前展开的节点 key 列表（v-model:expanded-keys） */
  expandedKeys?: (string | number)[]
  /** 当前选中的节点 key 列表（v-model:selected-keys） */
  selectedKeys?: (string | number)[]
  /** 左侧宽度比例（相对内容视口），如 0.2 表示内容视口的 1/5，即 20% */
  treeWidthRatio?: number
  /** 加载状态 */
  loading?: boolean
  /** 是否节点占满一行 */
  blockNode?: boolean
  /** 是否显示连接线 */
  showLine?: boolean
  /** 是否可选 */
  selectable?: boolean
  /** 是否开启虚拟滚动（由页面控制，大数据展开时建议开启） */
  virtual?: boolean
  /** 虚拟滚动列表高度（px），不传则按容器高度计算 */
  height?: number
  /** 虚拟滚动单项高度（px），不传则使用组件默认 */
  itemHeight?: number
  /** 是否开启拖拽排序/变更父节点（由页面控制，与 virtual 独立） */
  draggable?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  treeData: () => [],
  treeFieldNames: () => ({ title: 'title', key: 'key', children: 'children' }),
  expandedKeys: () => [],
  selectedKeys: () => [],
  treeWidthRatio: 0.2,
  loading: false,
  blockNode: true,
  showLine: false,
  selectable: true,
  virtual: true,
  height: undefined,
  itemHeight: undefined,
  draggable: false
})

/** 根节点 ref，用于按视口计算虚拟滚动高度 */
const containerRef = ref<HTMLElement | null>(null)
/** 虚拟滚动高度 = 从本组件内容区顶部到视口底部的可见高度，与树收缩/展开无关 */
const measuredHeight = ref(0)

const computedVirtualHeight = computed(() => {
  if (!props.virtual) return undefined
  const h = measuredHeight.value > 0 ? measuredHeight.value : (props.height ?? 400)
  return h > 0 ? h : 400
})

/** 按视口动态计算：从本组件内容区顶部到视口底部的距离作为虚拟列表高度，不依赖父级或树内容高度 */
function doUpdateHeight() {
  const el = containerRef.value
  if (!el) return
  const rect = el.getBoundingClientRect()
  const style = getComputedStyle(el)
  const marginTop = parseFloat(style.marginTop) || 0
  const marginBottom = parseFloat(style.marginBottom) || 0
  const paddingTop = parseFloat(style.paddingTop) || 0
  const paddingBottom = parseFloat(style.paddingBottom) || 0
  const contentTop = rect.top + marginTop + paddingTop
  const viewportHeight = window.innerHeight
  const available = viewportHeight - contentTop - paddingBottom - marginBottom
  const nextHeight = Math.max(0, Math.floor(available))
  const prevHeight = measuredHeight.value
  measuredHeight.value = nextHeight
  if (prevHeight !== nextHeight) {
    logger.debug('[TaktTreeLeftTable] 视口高度动态计算', {
      rectTop: rect.top,
      contentTop,
      viewportHeight,
      available: nextHeight,
      measuredHeight: nextHeight
    })
  }
}

/** 节流：将多次 updateHeight 合并到下一帧执行，避免 HMR/ResizeObserver 风暴 */
let rafId: number | null = null
function updateHeight() {
  if (rafId !== null) return
  rafId = requestAnimationFrame(() => {
    rafId = null
    doUpdateHeight()
  })
}

let resizeObserver: ResizeObserver | null = null
let windowResizeHandler: (() => void) | null = null
onMounted(() => {
  nextTick(() => {
    doUpdateHeight()
    const el = containerRef.value
    if (el && typeof ResizeObserver !== 'undefined') {
      resizeObserver = new ResizeObserver(() => updateHeight())
      resizeObserver.observe(el)
    }
    windowResizeHandler = () => updateHeight()
    window.addEventListener('resize', windowResizeHandler)
  })
})
onBeforeUnmount(() => {
  if (rafId !== null) {
    cancelAnimationFrame(rafId)
    rafId = null
  }
  if (resizeObserver && containerRef.value) {
    resizeObserver.disconnect()
    resizeObserver = null
  }
  if (windowResizeHandler) {
    window.removeEventListener('resize', windowResizeHandler)
    windowResizeHandler = null
  }
})

export interface TreeDropPayload {
  newTreeData: any[]
  dragKey: string | number
  dropKey: string | number
  dropToGap: boolean
  dropPosition: number
  dragNode: any
  dropNode: any
}

const emit = defineEmits<{
  'update:expandedKeys': [keys: (string | number)[]]
  'update:selectedKeys': [keys: (string | number)[]]
  'tree-select': [selectedKeys: (string | number)[], e: any]
  'tree-drop': [payload: TreeDropPayload]
}>()

const expandedKeys = computed({
  get: () => props.expandedKeys ?? [],
  set: (val) => emit('update:expandedKeys', val)
})

const selectedKeys = computed({
  get: () => props.selectedKeys ?? [],
  set: (val) => emit('update:selectedKeys', val)
})

const fieldNames = computed(() => ({
  title: props.treeFieldNames?.title ?? 'title',
  key: props.treeFieldNames?.key ?? 'key',
  children: props.treeFieldNames?.children ?? 'children'
}))

/** 左侧宽度：内容视口的 1/5（treeWidthRatio 0.2 = 20%） */
const leftStyle = computed(() => {
  const ratio = (props.treeWidthRatio ?? 0.2) * 100
  return {
    flex: `0 0 ${ratio}%`,
    width: `${ratio}%`,
    maxWidth: `${ratio}%`
  }
})

const handleSelect = (keys: (string | number)[], e: any) => {
  emit('tree-select', keys, e)
}

/** 深拷贝树节点（保留 key/title/children 等字段） */
function deepCloneTree(arr: any[]): any[] {
  if (!arr?.length) return []
  return arr.map(item => {
    const next: any = { ...item }
    const ch = fieldNames.value.children
    if (next[ch]?.length) {
      next[ch] = deepCloneTree(next[ch])
    }
    return next
  })
}

/** 在树中查找节点并执行 callback，用于删除或插入；找到并执行后返回 true */
function loop(
  data: any[],
  key: string | number,
  callback: (item: any, index: number, arr: any[]) => void
): boolean {
  const keyF = fieldNames.value.key
  const chF = fieldNames.value.children
  for (let i = 0; i < data.length; i++) {
    if (String(data[i][keyF]) === String(key)) {
      callback(data[i], i, data)
      return true
    }
    if (data[i][chF]?.length && loop(data[i][chF], key, callback)) {
      return true
    }
  }
  return false
}

function onDragEnter(info: { expandedKeys: (string | number)[] }) {
  if (props.draggable && info?.expandedKeys?.length) {
    emit('update:expandedKeys', info.expandedKeys)
  }
}

function onDrop(info: {
  node: { key: string | number; pos?: string; children?: any[]; expanded?: boolean }
  dragNode: { key: string | number }
  dropPosition: number
  dropToGap?: boolean
}) {
  if (!props.draggable) return
  const dropKey = info.node.key
  const dragKey = info.dragNode.key
  const posStr = info.node.pos ?? ''
  const dropPos = posStr.split('-')
  const dropPosition = info.dropPosition - Number(dropPos[dropPos.length - 1] ?? 0)
  const chF = fieldNames.value.children

  const data = deepCloneTree(props.treeData ?? [])
  let dragObj: any = null

  loop(data, dragKey, (item: any, index: number, arr: any[]) => {
    arr.splice(index, 1)
    dragObj = item
  })

  if (dragObj == null) return

  if (!info.dropToGap) {
    loop(data, dropKey, (item: any) => {
      const children = item[chF] ?? []
      item[chF] = [dragObj, ...children]
    })
  } else if (
    (info.node.children ?? []).length > 0 &&
    info.node.expanded &&
    dropPosition === 1
  ) {
    loop(data, dropKey, (item: any) => {
      const children = item[chF] ?? []
      item[chF] = [dragObj, ...children]
    })
  } else {
    let ar: any[] = []
    let i = 0
    loop(data, dropKey, (_item: any, index: number, arr: any[]) => {
      ar = arr
      i = index
    })
    if (dropPosition === -1) {
      ar.splice(i, 0, dragObj)
    } else {
      ar.splice(i + 1, 0, dragObj)
    }
  }

  emit('tree-drop', {
    newTreeData: data,
    dragKey,
    dropKey,
    dropToGap: info.dropToGap ?? false,
    dropPosition: info.dropPosition,
    dragNode: info.dragNode,
    dropNode: info.node
  })
}
</script>

<style scoped lang="less">
/* 占满父级（树表 wrap）高度，虚拟滚动视口由父级高度计算，不随树收缩变化 */
.takt-tree-left-table {
  min-width: 160px;
  min-height: 0;
  align-self: stretch;
  margin: 40px 0px 0px 0px;
  overflow: hidden;
  padding: 4px;
  display: flex;
  flex-direction: column;

  .takt-tree-left-table__spin {
    flex: 1;
    min-height: 0;
    display: flex;
    flex-direction: column;

    /* 让 loading 遮罩铺满并具有高度，使内部 top:50% 能上下居中 */
    :deep(.ant-spin-nested-loading) {
      flex: 1;
      min-height: 0;
      position: relative;
    }
    :deep(.ant-spin-nested-loading > div:first-child) {
      position: absolute;
      inset: 0;
      z-index: 4;
    }

    :deep(.ant-spin-container) {
      flex: 1;
      min-height: 0;
      display: flex;
      flex-direction: column;
    }
  }

  :deep(.ant-tree) {
    flex: 1;
    min-height: 0;
    overflow: auto;
  }
}
</style>
