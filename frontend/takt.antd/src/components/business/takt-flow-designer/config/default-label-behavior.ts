// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/components/business/takt-flow-designer/config
// 文件名称：default-label-behavior.ts
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：bpmn-js 扩展：① 所有元素都要有默认文字标签；② 标签以元素中心为基点、下移 64px。
// 新建时在 shape.create/connection.create/elements.create 的 postExecute 中设置；导入图后（import.done）遍历补设并重定位。
// 仅依赖 eventBus / modeling / getDefaultLabel / elementRegistry，不 import diagram-js 或 bpmn-js 子路径，避免 ESM 下 require is not defined。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/** 标签相对元素中心点的垂直间距（px），以元素中心为唯一正确基点 */
const LABEL_OFFSET_FROM_CENTER_Y = 64

/** 默认标签尺寸（与 bpmn-js LabelUtil.DEFAULT_LABEL_SIZE 一致），用于无 bounds 时计算位置 */
const DEFAULT_LABEL_WIDTH = 90
const DEFAULT_LABEL_HEIGHT = 20

type AnyContext = Record<string, unknown> & {
  shape?: ElementLike
  connection?: ElementLike
  elements?: ElementLike[]
  hints?: { createElementsBehavior?: boolean }
}

type ElementLike = {
  type?: string
  businessObject?: { name?: string; $type?: string }
  label?: LabelLike
  x?: number
  y?: number
  width?: number
  height?: number
  waypoints?: Array<{ x: number; y: number }>
}

type LabelLike = {
  type?: string
  x?: number
  y?: number
  width?: number
  height?: number
}

/** diagram-js 中标签元素的 type 为 'label'，不依赖 bpmn-js LabelUtil */
function isLabelElement(el: { type?: string } | undefined): boolean {
  return el?.type === 'label'
}

function applyDefaultLabel(
  element: ElementLike | undefined,
  modeling: { updateLabel: (element: unknown, newLabel: string) => void },
  getDefaultLabel: (type: string) => string
): void {
  if (!element || isLabelElement(element)) return
  const bo = element.businessObject
  if (!bo) return
  const name = bo.name
  if (name != null && String(name).trim() !== '') return
  const bpmnType = bo.$type ?? ''
  const defaultName = getDefaultLabel(bpmnType)
  if (!defaultName) return
  bo.name = defaultName
  modeling.updateLabel(element, defaultName)
}

/**
 * 将外部标签定位到「以元素中心为基点、下移 LABEL_OFFSET_FROM_CENTER_Y px」。
 * 形状用中心 (x+width/2, y+height/2)；连线用 waypoints 中点。
 */
function applyLabelPositionFromCenter(
  element: ElementLike,
  modeling: { moveShape: (shape: unknown, delta: { x: number; y: number }) => void }
): void {
  const label = element.label
  if (!label || !isLabelElement(label)) return

  const lw = label.width ?? DEFAULT_LABEL_WIDTH
  const lh = label.height ?? DEFAULT_LABEL_HEIGHT
  let centerX: number
  let centerY: number

  if (element.waypoints && element.waypoints.length > 0) {
    const pts = element.waypoints
    centerX = pts.reduce((s, p) => s + p.x, 0) / pts.length
    centerY = pts.reduce((s, p) => s + p.y, 0) / pts.length
  } else if (
    typeof element.x === 'number' &&
    typeof element.y === 'number' &&
    typeof element.width === 'number' &&
    typeof element.height === 'number'
  ) {
    centerX = element.x + element.width / 2
    centerY = element.y + element.height / 2
  } else {
    return
  }

  const targetX = centerX - lw / 2
  const targetY = centerY + LABEL_OFFSET_FROM_CENTER_Y - lh / 2
  const currentX = label.x ?? 0
  const currentY = label.y ?? 0
  const delta = { x: targetX - currentX, y: targetY - currentY }
  if (delta.x === 0 && delta.y === 0) return
  modeling.moveShape(label, delta)
}

type ElementRegistryLike = { forEach: (fn: (element: unknown) => void) => void }

const PRIO = 500

/**
 * 默认文字标签行为：仅用 eventBus.on 监听 commandStack.*.postExecute 与 import.done，不继承 CommandInterceptor，避免依赖链中的 require。
 * 所有元素补默认名称，并将外部标签以元素中心为基点下移 64px。
 */
function DefaultLabelBehavior(
  eventBus: { on: (event: string, priority: number, fn: (e: { context: AnyContext }) => void) => void },
  modeling: { updateLabel: (element: unknown, newLabel: string) => void; moveShape: (shape: unknown, delta: { x: number; y: number }) => void },
  getDefaultLabel: (type: string) => string,
  elementRegistry: ElementRegistryLike
) {
  function onShapeOrConnection(e: { context: AnyContext }) {
    const ctx = e.context
    if ((ctx.hints as { createElementsBehavior?: boolean } | undefined)?.createElementsBehavior === false) return
    const element = ctx.shape ?? ctx.connection as ElementLike
    applyDefaultLabel(element, modeling, getDefaultLabel)
    setTimeout(() => applyLabelPositionFromCenter(element, modeling), 0)
  }

  function onElements(e: { context: AnyContext }) {
    const ctx = e.context
    if ((ctx.hints as { createElementsBehavior?: boolean } | undefined)?.createElementsBehavior === false) return
    const list = ctx.elements
    if (!Array.isArray(list)) return
    list.forEach((el) => {
      applyDefaultLabel(el as ElementLike, modeling, getDefaultLabel)
      setTimeout(() => applyLabelPositionFromCenter(el as ElementLike, modeling), 0)
    })
  }

  eventBus.on('commandStack.shape.create.postExecute', PRIO, onShapeOrConnection)
  eventBus.on('commandStack.connection.create.postExecute', PRIO, onShapeOrConnection)
  eventBus.on('commandStack.elements.create.postExecute', PRIO, onElements)

  eventBus.on('import.done', PRIO, function () {
    elementRegistry.forEach(function (element: unknown) {
      const el = element as ElementLike
      applyDefaultLabel(el, modeling, getDefaultLabel)
      setTimeout(() => applyLabelPositionFromCenter(el, modeling), 0)
    })
  })
}

const inject = [ 'eventBus', 'modeling', 'getDefaultLabel', 'elementRegistry' ] as const
;(DefaultLabelBehavior as unknown as { $inject: string[] }).$inject = [ ...inject ]

export default DefaultLabelBehavior
