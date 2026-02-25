/**
 * Code generator · English
 * - Page title, delete confirm, success/fail: use common.confirm, common.msg, common.button + this module entity
 * - Here: only entity name, search keyword, modal titles, column names, module-specific actions
 */
export default {
  tableConfig: 'Table Config',

  keyword: 'Table name, entity class or business name',

  importFromDb: 'Import tables from database',
  saveAs: 'Save As',
  saveAsPathHint: 'Enter output path (overrides path in table config):',
  saveAsPathPlaceholder: 'e.g. D:\\Projects\\Takt.Net',
  genPath: 'Output path',
  advancedQuery: 'Advanced query',
  searchKeywordLabel: 'Table / entity / business name',
  placeholderFuzzy: 'Fuzzy search',

  tableName: 'Table name',
  tableComment: 'Table comment',
  entityClassName: 'Entity class',
  genModuleName: 'Module',
  genBusinessName: 'Business name',
  genTemplate: 'Template',

  generate: 'Generate',
  sync: 'Sync',
  initialize: 'Initialize',

  overwriteConfirmTitle: 'Overwrite confirm',
  overwriteConfirmContent: 'The following files already exist. Overwrite?',
  overwrite: 'Overwrite',
  saveAsCancel: 'Save As',

  noTableIdSync: 'No table ID, cannot sync',
  noTableIdPreview: 'No table ID, cannot preview',
  noTableIdInit: 'No table ID, cannot initialize',
  noPreviewData: 'No preview data; check table config or templates',
  syncFormHint: 'Save in the edit dialog to refresh fields from data source.',
  cloneSuccess: 'Cloned as new; change table name and save.',
  noDataToExport: 'No data to export',
  exportFileName: 'Code generator table config',
  codeGeneratedDownload: 'Code generated and downloaded',
  genSuccessCount: ', {count} file(s)',
  existingFilesSuffix: '... {count} file(s) in total',

  previewTitle: 'Code preview',
  previewEmpty: 'No preview content',
  previewHint: 'Select a record and click Preview.',
  previewHintDetail: 'Preview is generated from current table config and templates; re-preview after changes.',
  previewFileEmpty: '(This file has no content)',
  previewTabs: {
    entity: 'Entity',
    dto: 'DTO',
    service: 'Service',
    controller: 'Controller',
    types: 'Frontend types',
    api: 'API',
    i18n: 'I18n',
    view: 'View',
    form: 'Form',
    sql: 'Menu & translation (SQL)',
    other: 'Other'
  },

  validation: {
    columnNameSnakeCase: 'Row {rowNum}: column name must be snake_case (e.g. column_1, user_name), got 「{colName}」',
    csharpColumnPascalCase: 'Row {rowNum}: C# column name must be PascalCase (e.g. Column1, UserName), got 「{csharpName}」'
  },

  /** gen-form: tabs, placeholders, empty state, current project path; form labels use backend entity.gentable / entity.gentablecolumn keys */
  form: {
    tabBusiness: 'Business Module',
    tabEntity: 'Entity & DTO',
    tabService: 'Service & Controller',
    tabGenerate: 'Generate',
    tabFront: 'Frontend & Style',
    tabColumn: 'Field Config',
    labelCurrentProjectPath: 'Current project path',
    placeholderDataTableRequired: 'Select or enter table name',
    placeholderTableName: 'Lower snake_case, e.g. xxx_xxx_xxx',
    placeholderTableComment: 'Table comment',
    placeholderNamePrefix: 'Project name, default Takt',
    placeholderPermsPrefix: 'Auto by module+business, e.g. accounting:controlling:standard:wage:rate',
    placeholderModule: 'Select module or type, e.g. Generator, HumanResource.Organization',
    placeholderBusinessFromTable: 'Auto from table name',
    placeholderBusinessManual: 'For entity/service/controller names, e.g. Setting, Dept',
    placeholderFunctionName: 'Auto from table comment, read-only',
    placeholderAutoByModule: 'Auto from module',
    placeholderAutoByBusiness: 'Auto from business name',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: 'Loading…',
    placeholderCurrentProjectPathHint: 'Select "Current project" to load',
    placeholderParentMenu: 'Select parent menu (empty = root)',
    placeholderAuthor: 'Current user',
    placeholderDbType: 'DB type',
    placeholderCsharpType: 'C# type',
    placeholderQueryType: 'Query type',
    placeholderHtmlType: 'Display type',
    placeholderDictType: 'Select dict type',
    emptySaveTableFirst: 'Save table config first to manage fields',
    emptyNoColumnData: 'No field data'
  }
}
