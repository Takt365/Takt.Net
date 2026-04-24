/**
 * i18n 管理页「翻译（主）」Tab 静态补全；字段列标题优先 t('entity.translation.*')。
 */
export default {
  page: {
    tabMain: '翻译（主）',
  },
  placeholders: {
    listSearch: '资源键、翻译值',
    resourceKeyFuzzy: '模糊',
    cultureCodeExample: '如 zh-CN',
    resourceTypeSelect: 'Frontend / Backend',
    resourceGroupExample: '如 Menu',
    resourceKeyExample: '如：UserNotFound、menu.home._self',
    resourceGroupOptional: '如：Validation、Menu（可选）',
    orderNumHint: '越小越靠前',
    translationValueInLanguage: '该语言下的文本内容',
    remarkOptional: '可选',
    translationValueForLang: '{label} 翻译值',
  },
  divider: {
    perCultureValues: '各语言翻译值',
  },
  form: {
    languageSub: '语言（子表）',
    formCreate: '新增翻译',
    formEdit: '编辑翻译',
    formCreateTransposed: '新增翻译（转置）',
    formEditTransposed: '编辑翻译（转置）',
  },
  rules: {
    resourceKeyRequired: '请输入资源键',
    resourceTypeRequired: '请选择资源类型',
    cultureCodeRequired: '请选择语言',
    translationValueRequired: '请输入翻译值',
  },
  messages: {
    loadListFail: '加载翻译列表失败',
    loadTransposedFail: '加载翻译转置失败',
    selectTranslationEdit: '请选择要编辑的翻译',
    selectTranslationDelete: '请选择要删除的翻译',
    invalidTranslationId: '没有有效的翻译ID',
    notFoundByResourceKey: '未找到该资源键的翻译',
    loadTranslationFail: '获取翻译失败',
    loadTranslationDataFail: '加载翻译数据失败',
    savePartial: '保存完成，成功 {success} 条，失败 {fail} 条',
    saveAllSuccess: '保存成功，共 {count} 条',
    checkForm: '请检查表单',
    saveFail: '保存失败',
    exportDataLabel: '翻译',
  },
  options: {
    frontend: 'Frontend',
    backend: 'Backend',
  },
}
