// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types
// 文件名称：bpmn-js-properties-panel.d.ts
// 功能描述：bpmn-js-properties-panel / camunda-bpmn-moddle 无官方 .d.ts 时的模块声明，避免隐式 any
// ========================================

declare module 'bpmn-js-properties-panel' {
  /** diagram-js/bpmn-js 模块定义（无 @types/didi 时用 unknown） */
  const BpmnPropertiesPanelModule: unknown
  const BpmnPropertiesProviderModule: unknown
  const CamundaPlatformPropertiesProviderModule: unknown
  const CamundaPlatformTooltipProvider: Record<string, unknown>
  const ZeebePropertiesProviderModule: unknown
  const ZeebeTooltipProvider: Record<string, unknown>
  export { BpmnPropertiesPanelModule, BpmnPropertiesProviderModule, CamundaPlatformPropertiesProviderModule, CamundaPlatformTooltipProvider, ZeebePropertiesProviderModule, ZeebeTooltipProvider }
}

declare module 'camunda-bpmn-moddle/resources/camunda.json' {
  const descriptor: Record<string, unknown>
  export default descriptor
}
