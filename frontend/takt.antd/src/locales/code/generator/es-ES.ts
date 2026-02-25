/**
 * Generador de código · Español
 * Form labels use backend entity.gentable / entity.gentablecolumn keys
 */
export default {
  tableConfig: 'Configuración de tabla',

  keyword: 'Nombre de tabla, clase de entidad o nombre de negocio',

  importFromDb: 'Importar tablas desde la base de datos',
  saveAs: 'Guardar como',
  saveAsPathHint: 'Introduzca la ruta de salida (sobrescribe la del config de tabla):',
  saveAsPathPlaceholder: 'ej. D:\\Projects\\Takt.Net',
  genPath: 'Ruta de salida',
  advancedQuery: 'Búsqueda avanzada',
  searchKeywordLabel: 'Tabla / entidad / nombre de negocio',
  placeholderFuzzy: 'Búsqueda aproximada',

  tableName: 'Nombre de tabla',
  tableComment: 'Comentario de tabla',
  entityClassName: 'Clase de entidad',
  genModuleName: 'Módulo',
  genBusinessName: 'Nombre de negocio',
  genTemplate: 'Plantilla',

  generate: 'Generar',
  sync: 'Sincronizar',
  initialize: 'Inicializar',

  overwriteConfirmTitle: 'Confirmar sobrescritura',
  overwriteConfirmContent: 'Los siguientes archivos ya existen. ¿Sobrescribir?',
  overwrite: 'Sobrescribir',
  saveAsCancel: 'Guardar como',

  noTableIdSync: 'Sin ID de tabla, no se puede sincronizar',
  noTableIdPreview: 'Sin ID de tabla, no se puede previsualizar',
  noTableIdInit: 'Sin ID de tabla, no se puede inicializar',
  noPreviewData: 'Sin vista previa; revise la config de tabla o plantillas',
  syncFormHint: 'Guarde en el diálogo de edición para actualizar campos desde el origen.',
  cloneSuccess: 'Clonado como nuevo; cambie el nombre de tabla y guarde.',
  noDataToExport: 'No hay datos para exportar',
  exportFileName: 'Config de tabla del generador',
  codeGeneratedDownload: 'Código generado y descargado',
  genSuccessCount: ', {count} archivo(s)',
  existingFilesSuffix: '... {count} archivo(s) en total',

  previewTitle: 'Vista previa de código',
  previewEmpty: 'Sin contenido para previsualizar',
  previewHint: 'Seleccione un registro y haga clic en Vista previa.',
  previewHintDetail: 'La vista previa se genera desde la config de tabla y plantillas; vuelva a previsualizar tras cambios.',
  previewFileEmpty: '(Este archivo no tiene contenido)',
  previewTabs: {
    entity: 'Entidad',
    dto: 'DTO',
    service: 'Servicio',
    controller: 'Controlador',
    types: 'Tipos frontend',
    api: 'API',
    i18n: 'I18n',
    view: 'Vista',
    form: 'Formulario',
    sql: 'Menú y traducción (SQL)',
    other: 'Otros'
  },

  validation: {
    columnNameSnakeCase: 'Fila {rowNum}: el nombre de columna debe ser snake_case (ej. column_1, user_name), actual «{colName}»',
    csharpColumnPascalCase: 'Fila {rowNum}: el nombre de columna C# debe ser PascalCase (ej. Column1, UserName), actual «{csharpName}»'
  },

  form: {
    tabBusiness: 'Módulo de negocio',
    tabEntity: 'Entidad y DTO',
    tabService: 'Servicio y controlador',
    tabGenerate: 'Generar',
    tabFront: 'Frontend y estilo',
    tabColumn: 'Config de campos',
    labelCurrentProjectPath: 'Ruta del proyecto actual',
    placeholderDataTableRequired: 'Seleccione o introduzca el nombre de tabla',
    placeholderTableName: 'snake_case en minúsculas, ej. xxx_xxx_xxx',
    placeholderTableComment: 'Comentario de tabla',
    placeholderNamePrefix: 'Nombre del proyecto, por defecto Takt',
    placeholderPermsPrefix: 'Auto por módulo+negocio, ej. accounting:controlling:standard:wage:rate',
    placeholderModule: 'Seleccione módulo o escriba, ej. Generator, HumanResource.Organization',
    placeholderBusinessFromTable: 'Auto desde nombre de tabla',
    placeholderBusinessManual: 'Para nombres entidad/servicio/controlador, ej. Setting, Dept',
    placeholderFunctionName: 'Auto desde comentario de tabla, solo lectura',
    placeholderAutoByModule: 'Auto desde módulo',
    placeholderAutoByBusiness: 'Auto desde nombre de negocio',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: 'Cargando…',
    placeholderCurrentProjectPathHint: 'Seleccione "Proyecto actual" para cargar',
    placeholderParentMenu: 'Seleccione menú padre (vacío = raíz)',
    placeholderAuthor: 'Usuario actual',
    placeholderDbType: 'Tipo DB',
    placeholderCsharpType: 'Tipo C#',
    placeholderQueryType: 'Tipo de consulta',
    placeholderHtmlType: 'Tipo de visualización',
    placeholderDictType: 'Seleccione tipo de diccionario',
    emptySaveTableFirst: 'Guarde primero la config de tabla para gestionar campos',
    emptyNoColumnData: 'Sin datos de campos'
  }
}
