/**
 * 語言模塊靜態補全 · 繁體中文
 */
export default {
  page: {
    title: '語言管理',
    tabInI18n: '語言（子）',
  },
  columns: {
    languageId: '語言ID',
    translationId: '翻譯ID',
  },
  placeholders: {
    listSearch: '請輸入語言編碼或名稱',
    remark: '請輸入備註',
    languageName: '請輸入語言名稱（中文名稱，如：簡體中文）',
    cultureCode: '請輸入語言編碼（如：zh-CN、en-US）',
    nativeName: '請輸入本地化名稱（該語言下的名稱，如：简体中文、English）',
    languageIcon: '請輸入語言圖標URL（可選）',
    orderNum: '請輸入排序號',
    translationResourceKey: '請輸入資源鍵',
    translationCultureCode: '語言編碼',
    translationValue: '請輸入翻譯值',
    translationResourceType: '資源類型（Frontend/Backend）',
    translationResourceGroup: '請輸入資源分組（可選）',
    isRtlSelect: '請選擇是否啟用 RTL（從右到左）',
  },
  tabs: {
    main: '語言信息',
    translation: '翻譯列表',
  },
  typeForm: {
    addTranslation: '新增翻譯',
  },
  rules: {
    cultureCodeRequired: '請輸入語言編碼',
    languageNameRequired: '請輸入語言名稱',
    nativeNameRequired: '請輸入本地化名稱',
  },
  messages: {
    formCreate: '新增語言',
    formEdit: '編輯語言',
    exportFilePrefix: '語言',
    translationRowInvalid: '第 {row} 行翻譯：資源鍵和翻譯值不能為空',
    selectEdit: '請選擇要編輯的記錄',
    selectDelete: '請選擇要刪除的記錄',
    invalidRecordId: '沒有有效的記錄ID',
    checkFormInput: '請檢查表單輸入',
    statusUpdateSuccess: '狀態更新成功',
    statusUpdateFail: '狀態更新失敗',
  },
}
