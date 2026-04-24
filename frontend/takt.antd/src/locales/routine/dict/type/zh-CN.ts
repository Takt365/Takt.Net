/**
 * 字典类型模块静态补全：当后端 TaktTranslation（Frontend）未返回或未执行种子时，vue-i18n 使用此处文案。
 * 字段展示名与实体名请优先使用 t('entity.dicttype.*')、t('entity.dictdata.*')、t('common.*')（与 SeedI18nData 对齐）；本文件仅保留后端未提供的页面/占位/校验/组合句。
 */
export default {
  page: {
    tabmain: '字典类型信息',
    tabdata: '字典数据列表',
    datatitle: '字典数据',
  },
  placeholders: {
    dictTypeCode: '请输入字典类型编码',
    dictTypeName: '请输入字典类型名称',
    dataConfigIdSql: '请输入数据库配置ID（当数据源为SQL查询时）',
    sqlScript: '请输入SQL脚本',
    orderNum: '请输入排序号',
    remark: '请输入备注',
    dictLabelUnique: '请输入字典标签（在同一个字典类型下唯一）',
    dictL10nKey: '请输入字典本地化键（可选，用于多语言翻译）',
    dictValue: '请输入字典值（显示值）',
    cssClass: '请输入CSS类名',
    listClass: '请输入列表类名',
    extLabel: '请输入扩展标签（可选）',
    extValue: '请输入扩展值（可选）',
    orderNumSort: '请输入排序号（越小越靠前）',
    searchDictData: '请输入字典标签或字典值',
  },
  rules: {
    dictTypeCodeRequired: '请输入字典类型编码',
    dictTypeNameRequired: '请输入字典类型名称',
    dataSourceRequired: '请选择数据源',
    sqlScriptRequired: '数据源为SQL查询时，SQL脚本不能为空',
    dictLabelRequired: '请输入字典标签',
    dictValueRequired: '请输入字典值',
    dictRowRequired: '第 {row} 行字典数据：字典标签和字典值不能为空',
    dictLabelEmpty: '字典标签不能为空',
    dictValueEmpty: '字典值不能为空',
  },
  typeForm: {
    addDictData: '新增字典数据',
  },
  dataWindow: {
    windowTitle: '字典数据列表 - {name} ({code})',
    windowTitleDefault: '字典数据列表',
    dictTypeLine: '字典类型：{name} ({code})',
    formCreate: '新增字典数据',
    formEdit: '编辑字典数据',
    exportFilePrefix: '字典数据',
  },
  /** 子窗口内无对应 common 键的提示 */
  messages: {
    checkForm: '请检查表单输入',
    cellSaveSuccess: '保存成功',
  },
}
