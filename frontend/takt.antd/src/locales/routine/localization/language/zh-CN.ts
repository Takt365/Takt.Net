/**
 * 语言模块静态补全：当后端 TaktTranslation（Frontend）未命中时由 vue-i18n 使用。
 * 字段展示名请用 t('entity.language.*')、t('entity.translation.*')（见 SeedI18nData）；此处仅页面级标题、长占位、校验句、导出前缀等后端未单独提供的文案。
 */
export default {
  page: {
    title: '语言管理',
    /** 合并在「翻译 / i18n」页内的语言子 Tab 标题 */
    tabInI18n: '语言（子）',
  },
  /** 列表列标题：后端种子未提供 entity.language.languageid 时使用 */
  columns: {
    languageId: '语言ID',
    /** 展开行内翻译子表主键列（无 entity.translation.translationid 种子） */
    translationId: '翻译ID',
  },
  placeholders: {
    listSearch: '请输入语言编码或名称',
    remark: '请输入备注',
    languageName: '请输入语言名称（中文名称，如：简体中文）',
    cultureCode: '请输入语言编码（如：zh-CN、en-US）',
    nativeName: '请输入本地化名称（该语言下的名称，如：简体中文、English）',
    languageIcon: '请输入语言图标URL（可选）',
    orderNum: '请输入排序号',
    translationResourceKey: '请输入资源键',
    translationCultureCode: '语言编码',
    translationValue: '请输入翻译值',
    translationResourceType: '资源类型（Frontend/Backend）',
    translationResourceGroup: '请输入资源分组（可选）',
    isRtlSelect: '请选择是否启用 RTL（从右到左）',
  },
  tabs: {
    main: '语言信息',
    translation: '翻译列表',
  },
  typeForm: {
    addTranslation: '新增翻译',
  },
  rules: {
    cultureCodeRequired: '请输入语言编码',
    languageNameRequired: '请输入语言名称',
    nativeNameRequired: '请输入本地化名称',
  },
  messages: {
    formCreate: '新增语言',
    formEdit: '编辑语言',
    exportFilePrefix: '语言',
    translationRowInvalid: '第 {row} 行翻译：资源键和翻译值不能为空',
    selectEdit: '请选择要编辑的记录',
    selectDelete: '请选择要删除的记录',
    invalidRecordId: '没有有效的记录ID',
    checkFormInput: '请检查表单输入',
    statusUpdateSuccess: '状态更新成功',
    statusUpdateFail: '状态更新失败',
  },
}
