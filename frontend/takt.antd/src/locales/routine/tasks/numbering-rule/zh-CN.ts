/**
 * 编码规则列表/表单静态文案（vue-i18n）；与后端 TaktTranslation 无对应 entity 字段时放此处。
 */
export default {
  page: {
    listSearchPlaceholder: '请输入规则编码或名称',
    formCreate: '新增编码规则',
    formEdit: '编辑编码规则',
    exportDataLabel: '编码规则',
    entityName: '编码规则'
  },
  advanced: {
    ruleCode: '规则编码',
    ruleName: '规则名称',
    companyCode: '公司编码',
    deptCode: '部门编码',
    ruleStatus: '规则状态',
    placeholderRuleCode: '规则编码',
    placeholderRuleName: '规则名称',
    placeholderCompanyCode: '公司编码',
    placeholderDeptCode: '部门编码'
  },
  columns: {
    ruleCode: '规则编码',
    ruleName: '规则名称',
    companyCode: '公司编码',
    deptCode: '部门编码',
    prefix: '前缀',
    dateFormat: '日期格式',
    numberLength: '序号长度',
    suffix: '后缀',
    currentNumber: '当前序号',
    step: '步长',
    orderNum: '排序号',
    ruleStatus: '规则状态'
  },
  form: {
    ruleCode: '规则编码',
    ruleName: '规则名称',
    companyCode: '公司编码',
    deptCode: '部门编码',
    prefix: '前缀',
    dateFormat: '日期格式',
    numberLength: '序号长度',
    suffix: '后缀',
    step: '步长',
    orderNum: '排序号',
    remark: '备注',
    placeholderRuleCode: '如 ANNOUNCEMENT、PO',
    placeholderRuleName: '请输入规则名称',
    placeholderCompanyCode: '可选，用于匹配规则',
    placeholderDeptCode: '可选，用于匹配规则',
    placeholderPrefix: '如 ANN-',
    placeholderDateFormat: '如 yyyyMMdd',
    placeholderNumberLength: '如 5',
    placeholderSuffix: '可选',
    placeholderStep: '如 1',
    placeholderRemark: '请输入备注'
  },
  messages: {
    loadFail: '加载编码规则失败',
    selectOne: '请选择一条记录',
    selectDelete: '请选择要删除的记录',
    statusEnabled: '已启用',
    statusDisabled: '已禁用'
  },
  validation: {
    ruleCode: '请输入规则编码',
    ruleName: '请输入规则名称',
    numberLength: '请输入序号长度'
  }
}
