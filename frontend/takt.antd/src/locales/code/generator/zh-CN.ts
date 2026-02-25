/**
 * 代码生成 · 中文
 * - 页面标题、删除确认、成功/失败提示等用 common.confirm、common.msg、common.button + 本模块 entity 拼接
 * - 此处仅保留：实体名、搜索 keyword、弹窗标题、列名、本模块专有操作名等无法拼接的
 */
export default {
  /** 实体名（用于 common.confirm.deleteEntity、formTitle = common.button.create/edit + 此值） */
  tableConfig: '表配置',

  /** 搜索占位用 common.form.placeholder.search + keyword */
  keyword: '表名、实体类名或业务名',

  /** 弹窗/抽屉标题等 */
  importFromDb: '从数据库导入表',
  saveAs: '另存为',
  saveAsPathHint: '请输入生成路径（将覆盖表配置中的路径）：',
  saveAsPathPlaceholder: '如：D:\\Projects\\Takt.Net',
  genPath: '生成路径',
  advancedQuery: '高级查询',
  searchKeywordLabel: '表名/实体类名/业务名',
  placeholderFuzzy: '支持模糊搜索',

  /** 列头（表格/导出） */
  tableName: '表名',
  tableComment: '表描述',
  entityClassName: '实体类名',
  genModuleName: '模块名',
  genBusinessName: '业务名',
  genTemplate: '模板',

  /** 行内操作（common 无的） */
  generate: '生成',
  sync: '同步',
  initialize: '初始化',

  /** 覆盖确认 */
  overwriteConfirmTitle: '覆盖确认',
  overwriteConfirmContent: '目标路径下已存在以下文件，是否覆盖？',
  overwrite: '覆盖',
  saveAsCancel: '另存为',

  /** 无法用 common 拼接的提示 */
  noTableIdSync: '该记录无表 ID，无法同步',
  noTableIdPreview: '该记录无表 ID，无法预览',
  noTableIdInit: '该记录无表 ID，无法初始化',
  noPreviewData: '暂无预览数据，请检查表配置或模板',
  syncFormHint: '请在编辑弹窗中保存以从数据源刷新字段配置；完整同步需后端提供接口',
  cloneSuccess: '已复制为新建，请修改表名后保存',
  noDataToExport: '暂无数据可导出',
  exportFileName: '代码生成表配置',
  codeGeneratedDownload: '代码已生成并下载',
  genSuccessCount: '，共 {count} 个文件',
  existingFilesSuffix: '等共 {count} 个文件',

  /** 代码预览弹窗 */
  previewTitle: '代码预览',
  previewEmpty: '暂无预览内容',
  previewHint: '请选择一条记录后点击「预览」；',
  previewHintDetail: '预览内容由当前表配置与模板实时生成，修改后可再次预览查看更新。',
  previewFileEmpty: '（该文件暂无内容）',
  previewTabs: {
    entity: '实体',
    dto: 'DTO',
    service: '服务',
    controller: '控制器',
    types: '前端类型',
    api: 'API',
    i18n: '翻译',
    view: '视图',
    form: '表单',
    sql: '菜单与翻译(SQL)',
    other: '其他'
  },

  /** 字段配置校验错误信息 */
  validation: {
    columnNameSnakeCase: '字段配置第 {rowNum} 行：列名须为下划线命名法（如 column_1、user_name），当前为「{colName}」',
    csharpColumnPascalCase: '字段配置第 {rowNum} 行：C#列名须为大驼峰命名法（如 Column1、UserName），当前为「{csharpName}」'
  },

  /** 表配置表单 gen-form：仅 Tab、占位符、空态、当前项目路径；表单 label 用后端 entity.gentable / entity.gentablecolumn 种子键 */
  form: {
    tabBusiness: '业务模块',
    tabEntity: '实体与传输对象',
    tabService: '服务与控制器',
    tabGenerate: '生成',
    tabFront: '前端与样式',
    tabColumn: '字段配置',
    labelCurrentProjectPath: '当前项目路径',
    placeholderDataTableRequired: '请选择或输入数据表名',
    placeholderTableName: '小写下划线格式，如：xxx_xxx_xxx',
    placeholderTableComment: '表注释',
    placeholderNamePrefix: '项目名称，默认 Takt，修改后所有命名空间同步',
    placeholderPermsPrefix: '由模块名+业务名自动生成，如 accounting:controlling:standard:wage:rate',
    placeholderModule: '请选择模块（目录）或手动输入，如：Generator、HumanResource.Organization',
    placeholderBusinessFromTable: '由表名自动生成',
    placeholderBusinessManual: '用于生成实体/服务/控制器等类名及接口注释，如：设置、部门',
    placeholderFunctionName: '由表描述自动带出，仅读',
    placeholderAutoByModule: '由模块名自动生成',
    placeholderAutoByBusiness: '由业务名自动生成',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: '正在获取…',
    placeholderCurrentProjectPathHint: '请选择生成方式为当前项目后自动获取',
    placeholderParentMenu: '请选择上级菜单（不选为根）',
    placeholderAuthor: '当前登录用户',
    placeholderDbType: 'DB类型',
    placeholderCsharpType: 'C#类型',
    placeholderQueryType: '查询方式',
    placeholderHtmlType: '显示类型',
    placeholderDictType: '选择字典类型',
    emptySaveTableFirst: '请先保存表配置后再管理字段',
    emptyNoColumnData: '暂无字段数据'
  }
}
