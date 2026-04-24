/**
 * Static fallback for dictionary type module when backend TaktTranslation (Frontend) is missing.
 * Prefer t('entity.dicttype.*'), t('entity.dictdata.*'), t('common.*') for field/entity labels; this file only holds page-specific copy not seeded in DB.
 */
export default {
  page: {
    tabmain: 'Dictionary type',
    tabdata: 'Dictionary data list',
    datatitle: 'Dictionary data',
  },
  placeholders: {
    dictTypeCode: 'Enter dictionary type code',
    dictTypeName: 'Enter dictionary type name',
    dataConfigIdSql: 'Enter data config ID (when data source is SQL)',
    sqlScript: 'Enter SQL script',
    orderNum: 'Enter order number',
    remark: 'Enter remark',
    dictLabelUnique: 'Enter dictionary label (unique within the same dictionary type)',
    dictL10nKey: 'Enter localization key (optional, for i18n)',
    dictValue: 'Enter dictionary value (display value)',
    cssClass: 'Enter CSS class',
    listClass: 'Enter list class',
    extLabel: 'Enter extension label (optional)',
    extValue: 'Enter extension value (optional)',
    orderNumSort: 'Enter order number (smaller sorts first)',
    searchDictData: 'Enter dictionary label or value',
  },
  rules: {
    dictTypeCodeRequired: 'Dictionary type code is required',
    dictTypeNameRequired: 'Dictionary type name is required',
    dataSourceRequired: 'Please select a data source',
    sqlScriptRequired: 'SQL script is required when data source is SQL',
    dictLabelRequired: 'Dictionary label is required',
    dictValueRequired: 'Dictionary value is required',
    dictRowRequired: 'Row {row}: dictionary label and value cannot be empty',
    dictLabelEmpty: 'Dictionary label cannot be empty',
    dictValueEmpty: 'Dictionary value cannot be empty',
  },
  typeForm: {
    addDictData: 'Add dictionary data',
  },
  dataWindow: {
    windowTitle: 'Dictionary data - {name} ({code})',
    windowTitleDefault: 'Dictionary data',
    dictTypeLine: 'Dictionary type: {name} ({code})',
    formCreate: 'Add dictionary data',
    formEdit: 'Edit dictionary data',
    exportFilePrefix: 'Dictionary data',
  },
  messages: {
    checkForm: 'Please check the form input',
    cellSaveSuccess: 'Saved',
  },
}
