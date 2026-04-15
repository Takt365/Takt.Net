/**
 * 校验流程方案 ProcessContent（与后端 TaktFlowSchemeService.ValidateProcessContentOrThrow 对齐）。
 * 调用点：① 流程方案列表编辑弹窗 getFlowSchemeById 回填后（scheme/index.vue handleEdit）；② 提交保存前（同文件 handleFormSubmit）。
 * 不在此做「全表种子批量校验」；条数由业务在各自拉取/保存路径逐条校验。
 */
export function validateProcessContentForSave(
  raw: string | undefined | null
): { ok: true } | { ok: false; message: string } {
  const s = raw?.trim()
  if (!s) return { ok: true }
  let o: Record<string, unknown>
  try {
    o = JSON.parse(s) as Record<string, unknown>
  } catch (e: unknown) {
    const msg = e instanceof Error ? e.message : String(e)
    return { ok: false, message: msg }
  }
  if (!o || typeof o !== 'object') return { ok: false, message: 'root' }
  if (!Array.isArray(o.nodes)) return { ok: false, message: 'nodes' }
  if (!Array.isArray(o.edges)) return { ok: false, message: 'edges' }
  const ft = o.flowTree ?? o.FlowTree
  if (ft != null && typeof ft === 'object') {
    const fo = ft as Record<string, unknown>
    const nt = fo.nodeType ?? fo.NodeType
    if (nt !== undefined && Number(nt) !== 1) return { ok: false, message: 'flowTreeRoot' }
  }
  return { ok: true }
}
