/**
 * i18n 管理頁「翻譯（主）」Tab 靜態補全 · 繁體中文
 */
export default {
  page: {
    tabMain: '翻譯（主）',
  },
  placeholders: {
    listSearch: '資源鍵、翻譯值',
    resourceKeyFuzzy: '模糊',
    cultureCodeExample: '如 zh-CN',
    resourceTypeSelect: 'Frontend / Backend',
    resourceGroupExample: '如 Menu',
    resourceKeyExample: '如：UserNotFound、menu.home._self',
    resourceGroupOptional: '如：Validation、Menu（可選）',
    orderNumHint: '越小越靠前',
    translationValueInLanguage: '該語言下的文本內容',
    remarkOptional: '可選',
    translationValueForLang: '{label} 翻譯值',
  },
  divider: {
    perCultureValues: '各語言翻譯值',
  },
  form: {
    languageSub: '語言（子表）',
    formCreate: '新增翻譯',
    formEdit: '編輯翻譯',
    formCreateTransposed: '新增翻譯（轉置）',
    formEditTransposed: '編輯翻譯（轉置）',
  },
  rules: {
    resourceKeyRequired: '請輸入資源鍵',
    resourceTypeRequired: '請選擇資源類型',
    cultureCodeRequired: '請選擇語言',
    translationValueRequired: '請輸入翻譯值',
  },
  messages: {
    loadListFail: '加載翻譯列表失敗',
    loadTransposedFail: '加載翻譯轉置失敗',
    selectTranslationEdit: '請選擇要編輯的翻譯',
    selectTranslationDelete: '請選擇要刪除的翻譯',
    invalidTranslationId: '沒有有效的翻譯ID',
    notFoundByResourceKey: '未找到該資源鍵的翻譯',
    loadTranslationFail: '獲取翻譯失敗',
    loadTranslationDataFail: '加載翻譯數據失敗',
    savePartial: '保存完成，成功 {success} 條，失敗 {fail} 條',
    saveAllSuccess: '保存成功，共 {count} 條',
    checkForm: '請檢查表單',
    saveFail: '保存失敗',
    exportDataLabel: '翻譯',
  },
  options: {
    frontend: 'Frontend',
    backend: 'Backend',
  },
}
