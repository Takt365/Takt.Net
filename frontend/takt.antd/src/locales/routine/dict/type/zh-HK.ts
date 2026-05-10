/**
 * 字典類型模塊靜態補全 · 繁體中文
 */
export default {
  page: {
    tabmain: '字典類型信息',
    tabdata: '字典數據列表',
    datatitle: '字典數據',
  },
  placeholders: {
    dictTypeCode: '請輸入字典類型編碼',
    dictTypeName: '請輸入字典類型名稱',
    dataConfigIdSql: '請輸入數據庫配置ID（當數據源為SQL查詢時）',
    sqlScript: '請輸入SQL腳本',
    orderNum: '請輸入排序號',
    remark: '請輸入備註',
    dictLabelUnique: '請輸入字典標籤（在同一個字典類型下唯一）',
    dictL10nKey: '請輸入字典本地化鍵（可選，用於多語言翻譯）',
    dictValue: '請輸入字典值（顯示值）',
    cssClass: '請輸入CSS類名',
    listClass: '請輸入列表類名',
    extLabel: '請輸入擴展標籤（可選）',
    extValue: '請輸入擴展值（可選）',
    orderNumSort: '請輸入排序號（越小越靠前）',
    searchDictData: '請輸入字典標籤或字典值',
  },
  rules: {
    dictTypeCodeRequired: '請輸入字典類型編碼',
    dictTypeNameRequired: '請輸入字典類型名稱',
    dataSourceRequired: '請選擇數據源',
    sqlScriptRequired: '數據源為SQL查詢時，SQL腳本不能為空',
    dictLabelRequired: '請輸入字典標籤',
    dictValueRequired: '請輸入字典值',
    dictRowRequired: '第 {row} 行字典數據：字典標籤和字典值不能為空',
    dictLabelEmpty: '字典標籤不能為空',
    dictValueEmpty: '字典值不能為空',
  },
  typeForm: {
    addDictData: '新增字典數據',
  },
  dataWindow: {
    windowTitle: '字典數據列表 - {name} ({code})',
    windowTitleDefault: '字典數據列表',
    dictTypeLine: '字典類型：{name} ({code})',
    formCreate: '新增字典數據',
    formEdit: '編輯字典數據',
    exportFilePrefix: '字典數據',
  },
  messages: {
    checkForm: '請檢查表單輸入',
    cellSaveSuccess: '保存成功',
  },
}
