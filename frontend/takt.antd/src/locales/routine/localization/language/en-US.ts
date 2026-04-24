/**
 * Static fallback for language module when backend TaktTranslation (Frontend) is missing.
 * Use t('entity.language.*') and t('entity.translation.*') for field labels from seeds.
 */
export default {
  page: {
    title: 'Language management',
    tabInI18n: 'Languages',
  },
  columns: {
    languageId: 'Language ID',
    translationId: 'Translation ID',
  },
  placeholders: {
    listSearch: 'Enter culture code or language name',
    remark: 'Enter remark',
    languageName: 'Enter display name (e.g. Simplified Chinese)',
    cultureCode: 'Enter culture code (e.g. zh-CN, en-US)',
    nativeName: 'Enter native name in that locale (e.g. 简体中文, English)',
    languageIcon: 'Enter language icon URL (optional)',
    orderNum: 'Enter order number',
    translationResourceKey: 'Enter resource key',
    translationCultureCode: 'Culture code',
    translationValue: 'Enter translation text',
    translationResourceType: 'Resource type (Frontend/Backend)',
    translationResourceGroup: 'Enter resource group (optional)',
    isRtlSelect: 'Choose whether to enable RTL (right-to-left)',
  },
  tabs: {
    main: 'Language',
    translation: 'Translations',
  },
  typeForm: {
    addTranslation: 'Add translation',
  },
  rules: {
    cultureCodeRequired: 'Culture code is required',
    languageNameRequired: 'Language name is required',
    nativeNameRequired: 'Native name is required',
  },
  messages: {
    formCreate: 'Add language',
    formEdit: 'Edit language',
    exportFilePrefix: 'Language',
    translationRowInvalid: 'Row {row}: resource key and translation value are required',
    selectEdit: 'Select a record to edit',
    selectDelete: 'Select records to delete',
    invalidRecordId: 'Invalid record ID',
    checkFormInput: 'Please check the form',
    statusUpdateSuccess: 'Status updated',
    statusUpdateFail: 'Failed to update status',
  },
}
