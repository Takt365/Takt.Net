/**
 * 代碼生成 · 繁體中文
 * 表單 label 使用後端 entity.gentable / entity.gentablecolumn 種子鍵
 */
export default {
  tableConfig: '表配置',

  keyword: '表名、實體類名或業務名',

  importFromDb: '從數據庫導入表',
  saveAs: '另存為',
  saveAsPathHint: '請輸入生成路徑（將覆蓋表配置中的路徑）：',
  saveAsPathPlaceholder: '如：D:\\Projects\\Takt.Net',
  genPath: '生成路徑',
  advancedQuery: '高級查詢',
  searchKeywordLabel: '表名/實體類名/業務名',
  placeholderFuzzy: '支持模糊搜索',

  tableName: '表名',
  tableComment: '表描述',
  entityClassName: '實體類名',
  genModuleName: '模塊名',
  genBusinessName: '業務名',
  genTemplate: '模板',

  generate: '生成',
  sync: '同步',
  initialize: '初始化',

  overwriteConfirmTitle: '覆蓋確認',
  overwriteConfirmContent: '目標路徑下已存在以下文件，是否覆蓋？',
  overwrite: '覆蓋',
  saveAsCancel: '另存為',

  noTableIdSync: '該記錄無表 ID，無法同步',
  noTableIdPreview: '該記錄無表 ID，無法預覽',
  noTableIdInit: '該記錄無表 ID，無法初始化',
  noPreviewData: '暫無預覽數據，請檢查表配置或模板',
  syncFormHint: '請在編輯彈窗中保存以從數據源刷新字段配置；完整同步需後端提供接口',
  cloneSuccess: '已複製為新建，請修改表名後保存',
  noDataToExport: '暫無數據可導出',
  exportFileName: '代碼生成表配置',
  codeGeneratedDownload: '代碼已生成並下載',
  genSuccessCount: '，共 {count} 個文件',
  existingFilesSuffix: '等共 {count} 個文件',

  previewTitle: '代碼預覽',
  previewEmpty: '暫無預覽內容',
  previewHint: '請選擇一條記錄後點擊「預覽」；',
  previewHintDetail: '預覽內容由當前表配置與模板實時生成，修改後可再次預覽查看更新。',
  previewFileEmpty: '（該文件暫無內容）',
  previewTabs: {
    entity: '實體',
    dto: 'DTO',
    service: '服務',
    controller: '控制器',
    types: '前端類型',
    api: 'API',
    i18n: '翻譯',
    view: '視圖',
    form: '表單',
    sql: '菜單與翻譯(SQL)',
    other: '其他'
  },

  validation: {
    columnNameSnakeCase: '字段配置第 {rowNum} 行：列名須為下劃線命名法（如 column_1、user_name），當前為「{colName}」',
    csharpColumnPascalCase: '字段配置第 {rowNum} 行：C#列名須為大駝峰命名法（如 Column1、UserName），當前為「{csharpName}」'
  },

  form: {
    tabBusiness: '業務模塊',
    tabEntity: '實體與傳輸對象',
    tabService: '服務與控制器',
    tabGenerate: '生成',
    tabFront: '前端與樣式',
    tabColumn: '字段配置',
    labelCurrentProjectPath: '當前項目路徑',
    placeholderDataTableRequired: '請選擇或輸入數據表名',
    placeholderTableName: '小寫下劃線格式，如：xxx_xxx_xxx',
    placeholderTableComment: '表註釋',
    placeholderNamePrefix: '項目名稱，默認 Takt，修改後所有命名空間同步',
    placeholderPermsPrefix: '由模塊名+業務名自動生成，如 accounting:controlling:standard:wage:rate',
    placeholderModule: '請選擇模塊（目錄）或手動輸入，如：Generator、HumanResource.Organization',
    placeholderBusinessFromTable: '由表名自動生成',
    placeholderBusinessManual: '用於生成實體/服務/控制器等類名及接口註釋，如：設置、部門',
    placeholderFunctionName: '由表描述自動帶出，僅讀',
    placeholderAutoByModule: '由模塊名自動生成',
    placeholderAutoByBusiness: '由業務名自動生成',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: '正在獲取…',
    placeholderCurrentProjectPathHint: '請選擇生成方式為當前項目後自動獲取',
    placeholderParentMenu: '請選擇上級菜單（不選為根）',
    placeholderAuthor: '當前登錄用戶',
    placeholderDbType: 'DB類型',
    placeholderCsharpType: 'C#類型',
    placeholderQueryType: '查詢方式',
    placeholderHtmlType: '顯示類型',
    placeholderDictType: '選擇字典類型',
    emptySaveTableFirst: '請先保存表配置後再管理字段',
    emptyNoColumnData: '暫無字段數據'
  }
}
