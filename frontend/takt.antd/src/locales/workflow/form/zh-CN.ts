/**
 * 流程表单 · 中文
 * 表单分类/类型选项等（实体字段用 entity.flowform.*）
 */
export default {
  flowSelectorLabel: '流程',
  flowSelectorPlaceholder: '请选择流程',
  /** 步骤3 单选：关联流程 / 新建流程 */
  linkFlowOptionLink: '关联流程',
  linkFlowOptionNew: '新建流程',
  linkedSchemes: '关联的流程',
  noSchemeHint: '暂无流程方案，请到「流程方案」中新建流程。',
  linkedSchemesNewHint: '请先保存表单并填写表单编码，再到「流程方案」中设计流程并选择本表单。',
  goDesignFlow: '去设计流程',
  category: {
    general: '通用表单',
    business: '业务表单',
    system: '系统表单'
  },
  type: {
    dynamic: '动态表单（拖拽设计）',
    static: '静态表单（模板渲染）',
    custom: '自定义表单（自定义页面）'
  },
  versionPlaceholder: '如 v1.0.0',
  dataSourcePlaceholder: '请选择数据源（数据库）',
  dataTablePlaceholder: '请选择数据表',
  formFieldPlaceholder: '请选择要显示在表单中的字段',
  entityTablePlaceholder: '请先选择数据源，再选择数据表',
  entityTableHint: '选中列出所有数据列项，以便第四步还原表单',
  batchDelete: '批量删除',
  loadDetailFailed: '加载表单详情失败',
  step: {
    /** 第一步：表单信息 */
    formInfo: '表单信息',
    /** 第二步：数据源 */
    dataSource: '数据源',
    /** 第三步：数据表清单 */
    dataTableList: '数据表清单',
    /** 第四步：表单设计 */
    formDesign: '表单设计',
    /** 步骤3：关联流程/流程设计 */
    linkedFlow: '关联流程/流程设计',
    basicInfo: '基本信息',
    formDesignEdit: '表单设计（编辑）',
    next: '下一步',
    prev: '上一步',
    done: '完成',
    completeRequired: '请完成所有步骤并通过校验后再提交',
    validateFail: '请先完成第 {step} 步'
  }
}
