/**
 * 流程方案 · 中文
 */
export default {
  designerLabel: '流程结构（可视化构建）',
  designerLabelEdit: '流程结构（编辑）',
  createButton: '新增流程方案',
  modalTitle: '流程方案',
  jsonPlaceholder: 'JSON 格式：nodes、edges 等',
  stepProcessJsonLabel: '流程结构（仅本项目工作流解析字段）',
  loadDetailFailed: '加载方案详情失败',
  /** ProcessContent 与后端校验一致：合法 JSON + nodes/edges；若有 flowTree 根须 nodeType=1 */
  invalidProcessContent: '流程内容无效：须为合法 JSON，且包含 nodes、edges；若有 flowTree，根须为发起人（nodeType=1）。请回到「流程设计」步骤保存。',
  linkForm: '关联表单',
  selectFormPlaceholder: '请选择关联表单（可选）',
  /** 步骤2 单选：关联表单 / 新建表单 */
  linkFormOptionLink: '关联表单',
  linkFormOptionNew: '新建表单',
  noFormHint: '暂无流程表单，请到「流程表单」中新建。',
  step: {
    /** 步骤1：流程信息 */
    step1FlowInfo: '流程信息',
    /** 步骤2：关联表单/表单设计 */
    step2SelectForm: '关联表单/表单设计',
    /** 步骤3：流程设计 */
    step3FlowDesign: '流程设计',
    step1BasicInfo: '方案基本信息',
    step4FlowPreview: '流程预览',
    basicInfo: '基本信息',
    initialData: '初始数据',
    processDesign: '流程设计',
    processDesignVisual: '流程结构（可视化构建）',
    processDesignJson: '流程结构（仅本项目工作流解析字段）',
    next: '下一步',
    prev: '上一步',
    done: '完成',
    completeRequired: '请完成所有步骤并通过校验后再提交',
    validateFail: '请先完成第 {step} 步'
  }
}
