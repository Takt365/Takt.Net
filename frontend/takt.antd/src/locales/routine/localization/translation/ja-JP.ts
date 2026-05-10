/**
 * Static strings for the Translation (main) tab on the i18n admin page.
 */
export default {
  page: {
    tabMain: 'Translations (main)',
  },
  placeholders: {
    listSearch: 'Resource key or translation text',
    resourceKeyFuzzy: 'Fuzzy match',
    cultureCodeExample: 'e.g. zh-CN',
    resourceTypeSelect: 'Frontend / Backend',
    resourceGroupExample: 'e.g. Menu',
    resourceKeyExample: 'e.g. UserNotFound, menu.home._self',
    resourceGroupOptional: 'e.g. Validation, Menu (optional)',
    orderNumHint: 'Smaller sorts first',
    translationValueInLanguage: 'Text for this language',
    remarkOptional: 'Optional',
    translationValueForLang: '{label} translation',
  },
  divider: {
    perCultureValues: 'Values per language',
  },
  form: {
    languageSub: 'Language (child)',
    formCreate: 'Add translation',
    formEdit: 'Edit translation',
    formCreateTransposed: 'Add translation (transposed)',
    formEditTransposed: 'Edit translation (transposed)',
  },
  rules: {
    resourceKeyRequired: 'Enter resource key',
    resourceTypeRequired: 'Select resource type',
    cultureCodeRequired: 'Select language',
    translationValueRequired: 'Enter translation value',
  },
  messages: {
    loadListFail: 'Failed to load translation list',
    loadTransposedFail: 'Failed to load transposed translations',
    selectTranslationEdit: 'Select a translation to edit',
    selectTranslationDelete: 'Select translations to delete',
    invalidTranslationId: 'Invalid translation ID',
    notFoundByResourceKey: 'No translations for this resource key',
    loadTranslationFail: 'Failed to load translations',
    loadTranslationDataFail: 'Failed to load translation data',
    savePartial: 'Saved: {success} ok, {fail} failed',
    saveAllSuccess: 'Saved {count} row(s)',
    checkForm: 'Please check the form',
    saveFail: 'Save failed',
    exportDataLabel: 'Translations',
  },
  options: {
    frontend: 'Frontend',
    backend: 'Backend',
  },
}
