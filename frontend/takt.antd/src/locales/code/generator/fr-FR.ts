/**
 * Générateur de code · Français
 * Form labels use backend entity.gentable / entity.gentablecolumn keys
 */
export default {
  tableConfig: 'Configuration de table',

  keyword: 'Nom de table, classe d\'entité ou nom métier',

  importFromDb: 'Importer les tables depuis la base',
  saveAs: 'Enregistrer sous',
  saveAsPathHint: 'Saisissez le chemin de sortie (remplace celui de la config) :',
  saveAsPathPlaceholder: 'ex. D:\\Projects\\Takt.Net',
  genPath: 'Chemin de sortie',
  advancedQuery: 'Recherche avancée',
  searchKeywordLabel: 'Table / entité / nom métier',
  placeholderFuzzy: 'Recherche floue',

  tableName: 'Nom de table',
  tableComment: 'Commentaire de table',
  entityClassName: 'Classe d\'entité',
  genModuleName: 'Module',
  genBusinessName: 'Nom métier',
  genTemplate: 'Modèle',

  generate: 'Générer',
  sync: 'Synchroniser',
  initialize: 'Initialiser',

  overwriteConfirmTitle: 'Confirmer l\'écrasement',
  overwriteConfirmContent: 'Les fichiers suivants existent déjà. Écraser ?',
  overwrite: 'Écraser',
  saveAsCancel: 'Enregistrer sous',

  noTableIdSync: 'Pas d\'ID de table, impossible de synchroniser',
  noTableIdPreview: 'Pas d\'ID de table, impossible d\'afficher l\'aperçu',
  noTableIdInit: 'Pas d\'ID de table, impossible d\'initialiser',
  noPreviewData: 'Pas d\'aperçu ; vérifiez la config ou les modèles',
  syncFormHint: 'Enregistrez dans la boîte d\'édition pour rafraîchir les champs.',
  cloneSuccess: 'Cloné comme nouveau ; changez le nom de table et enregistrez.',
  noDataToExport: 'Aucune donnée à exporter',
  exportFileName: 'Config table du générateur',
  codeGeneratedDownload: 'Code généré et téléchargé',
  genSuccessCount: ', {count} fichier(s)',
  existingFilesSuffix: '... {count} fichier(s) au total',

  previewTitle: 'Aperçu du code',
  previewEmpty: 'Aucun contenu à prévisualiser',
  previewHint: 'Sélectionnez un enregistrement et cliquez sur Aperçu.',
  previewHintDetail: 'L\'aperçu est généré à partir de la config et des modèles ; ré-aperçu après modification.',
  previewFileEmpty: '(Ce fichier n\'a pas de contenu)',
  previewTabs: {
    entity: 'Entité',
    dto: 'DTO',
    service: 'Service',
    controller: 'Contrôleur',
    types: 'Types frontend',
    api: 'API',
    i18n: 'I18n',
    view: 'Vue',
    form: 'Formulaire',
    sql: 'Menu et traduction (SQL)',
    other: 'Autres'
  },

  validation: {
    columnNameSnakeCase: 'Ligne {rowNum} : le nom de colonne doit être en snake_case (ex. column_1, user_name), actuel « {colName} »',
    csharpColumnPascalCase: 'Ligne {rowNum} : le nom de colonne C# doit être en PascalCase (ex. Column1, UserName), actuel « {csharpName} »'
  },

  form: {
    tabBusiness: 'Module métier',
    tabEntity: 'Entité et DTO',
    tabService: 'Service et contrôleur',
    tabGenerate: 'Générer',
    tabFront: 'Frontend et style',
    tabColumn: 'Config des champs',
    labelCurrentProjectPath: 'Chemin du projet actuel',
    placeholderDataTableRequired: 'Sélectionnez ou saisissez le nom de table',
    placeholderTableName: 'snake_case en minuscules, ex. xxx_xxx_xxx',
    placeholderTableComment: 'Commentaire de table',
    placeholderNamePrefix: 'Nom du projet, défaut Takt',
    placeholderPermsPrefix: 'Auto par module+métier, ex. accounting:controlling:standard:wage:rate',
    placeholderModule: 'Sélectionnez le module ou saisissez, ex. Generator, HumanResource.Organization',
    placeholderBusinessFromTable: 'Auto depuis le nom de table',
    placeholderBusinessManual: 'Pour noms entité/service/contrôleur, ex. Setting, Dept',
    placeholderFunctionName: 'Auto depuis le commentaire de table, lecture seule',
    placeholderAutoByModule: 'Auto depuis le module',
    placeholderAutoByBusiness: 'Auto depuis le nom métier',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: 'Chargement…',
    placeholderCurrentProjectPathHint: 'Sélectionnez « Projet actuel » pour charger',
    placeholderParentMenu: 'Sélectionnez le menu parent (vide = racine)',
    placeholderAuthor: 'Utilisateur actuel',
    placeholderDbType: 'Type DB',
    placeholderCsharpType: 'Type C#',
    placeholderQueryType: 'Type de requête',
    placeholderHtmlType: 'Type d\'affichage',
    placeholderDictType: 'Sélectionnez le type de dictionnaire',
    emptySaveTableFirst: 'Enregistrez d\'abord la config de table pour gérer les champs',
    emptyNoColumnData: 'Aucune donnée de champs'
  }
}
