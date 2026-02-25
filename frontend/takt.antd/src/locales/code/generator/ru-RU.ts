/**
 * Генератор кода · Русский
 * Form labels use backend entity.gentable / entity.gentablecolumn keys
 */
export default {
  tableConfig: 'Настройка таблицы',

  keyword: 'Имя таблицы, класс сущности или название модуля',

  importFromDb: 'Импорт таблиц из БД',
  saveAs: 'Сохранить как',
  saveAsPathHint: 'Введите путь вывода (перезаписывает путь в настройках таблицы):',
  saveAsPathPlaceholder: 'напр. D:\\Projects\\Takt.Net',
  genPath: 'Путь вывода',
  advancedQuery: 'Расширенный поиск',
  searchKeywordLabel: 'Таблица / сущность / название',
  placeholderFuzzy: 'Нечёткий поиск',

  tableName: 'Имя таблицы',
  tableComment: 'Комментарий таблицы',
  entityClassName: 'Класс сущности',
  genModuleName: 'Модуль',
  genBusinessName: 'Название',
  genTemplate: 'Шаблон',

  generate: 'Сгенерировать',
  sync: 'Синхронизация',
  initialize: 'Инициализация',

  overwriteConfirmTitle: 'Подтверждение перезаписи',
  overwriteConfirmContent: 'Следующие файлы уже существуют. Перезаписать?',
  overwrite: 'Перезаписать',
  saveAsCancel: 'Сохранить как',

  noTableIdSync: 'Нет ID таблицы, синхронизация невозможна',
  noTableIdPreview: 'Нет ID таблицы, предпросмотр невозможен',
  noTableIdInit: 'Нет ID таблицы, инициализация невозможна',
  noPreviewData: 'Нет предпросмотра; проверьте настройки или шаблоны',
  syncFormHint: 'Сохраните в диалоге редактирования, чтобы обновить поля из источника.',
  cloneSuccess: 'Склонировано как новое; измените имя таблицы и сохраните.',
  noDataToExport: 'Нет данных для экспорта',
  exportFileName: 'Настройки таблицы генератора',
  codeGeneratedDownload: 'Код сгенерирован и загружен',
  genSuccessCount: ', {count} файл(ов)',
  existingFilesSuffix: '... всего {count} файл(ов)',

  previewTitle: 'Предпросмотр кода',
  previewEmpty: 'Нет содержимого для предпросмотра',
  previewHint: 'Выберите запись и нажмите «Предпросмотр».',
  previewHintDetail: 'Предпросмотр формируется из текущей настройки таблицы и шаблонов; обновите после изменений.',
  previewFileEmpty: '(В этом файле нет содержимого)',
  previewTabs: {
    entity: 'Сущность',
    dto: 'DTO',
    service: 'Сервис',
    controller: 'Контроллер',
    types: 'Типы фронта',
    api: 'API',
    i18n: 'Перевод',
    view: 'Представление',
    form: 'Форма',
    sql: 'Меню и перевод (SQL)',
    other: 'Прочее'
  },

  validation: {
    columnNameSnakeCase: 'Строка {rowNum}: имя столбца должно быть в snake_case (напр. column_1, user_name), получено «{colName}»',
    csharpColumnPascalCase: 'Строка {rowNum}: имя столбца C# должно быть в PascalCase (напр. Column1, UserName), получено «{csharpName}»'
  },

  form: {
    tabBusiness: 'Бизнес-модуль',
    tabEntity: 'Сущность и DTO',
    tabService: 'Сервис и контроллер',
    tabGenerate: 'Генерация',
    tabFront: 'Фронт и стили',
    tabColumn: 'Настройка полей',
    labelCurrentProjectPath: 'Путь текущего проекта',
    placeholderDataTableRequired: 'Выберите или введите имя таблицы',
    placeholderTableName: 'snake_case в нижнем регистре, напр. xxx_xxx_xxx',
    placeholderTableComment: 'Комментарий таблицы',
    placeholderNamePrefix: 'Имя проекта, по умолчанию Takt',
    placeholderPermsPrefix: 'Авто по модулю+названию, напр. accounting:controlling:standard:wage:rate',
    placeholderModule: 'Выберите модуль или введите, напр. Generator, HumanResource.Organization',
    placeholderBusinessFromTable: 'Авто из имени таблицы',
    placeholderBusinessManual: 'Для имён сущность/сервис/контроллер, напр. Setting, Dept',
    placeholderFunctionName: 'Авто из комментария таблицы, только чтение',
    placeholderAutoByModule: 'Авто из модуля',
    placeholderAutoByBusiness: 'Авто из названия',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: 'Загрузка…',
    placeholderCurrentProjectPathHint: 'Выберите «Текущий проект» для загрузки',
    placeholderParentMenu: 'Выберите родительское меню (пусто = корень)',
    placeholderAuthor: 'Текущий пользователь',
    placeholderDbType: 'Тип БД',
    placeholderCsharpType: 'Тип C#',
    placeholderQueryType: 'Тип запроса',
    placeholderHtmlType: 'Тип отображения',
    placeholderDictType: 'Выберите тип словаря',
    emptySaveTableFirst: 'Сначала сохраните настройки таблицы для управления полями',
    emptyNoColumnData: 'Нет данных полей'
  }
}
