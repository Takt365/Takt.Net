/**
 * Common translations · Español
 * Same structure and key order as zh-CN (alphabetical for button)
 */
export default {
  /** <summary>1. app</summary> */
  app: {
    htmlTitle: 'Takt Digital Factory',
    name: 'Takt Digital Factory (TDF) ',
    productcode: 'TDF-MES-PRO',
    slogan: 'Impulsando el futuro',
    tagline: 'Práctico·Simple·Flexible'
  },

  /** <summary>2. settings</summary> */
  settings: {
    /** Diez colores famosos, coinciden con color-base.less */
    color: {
      blue: 'Azul Klein',
      brown: 'Marrón Van Dyke',
      cyan: 'Azul Tiffany',
      gold: 'Dorado',
      gray: 'Gris Memorial',
      green: 'Verde Marte',
      indigo: 'Azul Prusia',
      orange: 'Rojo Tiziano',
      pink: 'Burdeos',
      purple: 'Borgoña',
      red: 'Rojo Chino',
      yellow: 'Amarillo Sennelier',
      switch: 'Cambiar color',
      title: 'Color',
      locked: 'El color del tema de hoy está fijo y no se puede cambiar'
    },
    locale: {
      'ar-SA': 'Árabe',
      'en-US': 'English',
      'es-ES': 'Español',
      'fr-FR': 'Francés',
      'ja-JP': 'Japanés',
      'ko-KR': 'Coreano',
      'ru-RU': 'Ruso',
      'zh-CN': 'Chino simplificado',
      'zh-TW': 'Chino tradicional',
      switch: 'Cambiar idioma',
      title: 'Idioma'
    },
    theme: {
      dark: 'Oscuro',
      light: 'Claro',
      switch: 'Cambiar tema',
      title: 'Tema'
    }
  },

  /** Layout position */
  layout: {
    position: {
      left: 'Izquierda',
      center: 'Centro',
      right: 'Derecha'
    }
  },

  /** <summary>3. button</summary> Alphabetical order as zh-CN */
  button: {
    addsign: 'Añadir firma',
    advancedQuery: 'Búsqueda avanzada',
    allocate: 'Asignar',
    approve: 'Aprobar',
    archive: 'Archivar',
    back: 'Volver',
    cancel: 'Cancelar',
    changepwd: 'Cambiar contraseña',
    checkAll: 'Seleccionar todo',
    clean: 'Limpiar',
    clone: 'Clonar',
    collapse: 'Contraer',
    columnSetting: 'Configurar columna',
    comment: 'Comentar',
    config: 'Configuración',
    confirm: 'Confirmar',
    copy: 'Copiar',
    create: 'Crear',
    createRow: 'Añadir fila',
    datasource: 'Origen de datos',
    delegate: 'Delegar',
    delete: 'Eliminar',
    design: 'Diseño',
    detail: 'Detalle',
    disable: 'Desactivar',
    download: 'Descargar',
    draft: 'Borrador',
    edit: 'Editar',
    enable: 'Activar',
    empty: 'Vaciar',
    empty30d: 'Vaciar 30 días',
    empty7d: 'Vaciar 7 días',
    emptyAll: 'Vaciar todo',
    expand: 'Expandir',
    exitFullscreen: 'Salir de pantalla completa',
    export: 'Exportar',
    favorite: 'Favorito',
    field: 'Gestión de campos',
    fixed: 'Fijo',
    forward: 'Reenviar',
    formData: 'Datos del formulario',
    fullscreen: 'Pantalla completa',
    history: 'Historial',
    import: 'Importar',
    like: 'Me gusta',
    logout: 'Cerrar sesión',
    markRead: 'Marcar leído',
    more: 'Más',
    no: 'No',
    ok: 'Aceptar',
    open: 'Abrir',
    password: 'Contraseña',
    permission: 'Permisos',
    personalSettings: 'Ajustes personales',
    preview: 'Vista previa',
    print: 'Imprimir',
    preferences: 'Preferencias',
    profile: 'Perfil',
    progress: 'Progreso',
    publish: 'Publicar',
    query: 'Consultar',
    read: 'Leído',
    refresh: 'Actualizar',
    reply: 'Responder',
    reset: 'Restablecer',
    resume: 'Reanudar',
    return: 'Devolver',
    revoke: 'Revocar',
    run: 'Ejecutar',
    send: 'Enviar',
    share: 'Compartir',
    sign: 'Firmar',
    start: 'Iniciar',
    stop: 'Detener',
    subsign: 'Quitar firma',
    submit: 'Enviar',
    suspend: 'Suspender',
    template: 'Plantilla',
    terminate: 'Terminar',
    theme: 'Configuración de tema',
    toList: 'Lista',
    toTranspose: 'Transpuesta',
    transfer: 'Transferir',
    truncate: 'Truncar',
    unfavorite: 'Quitar favorito',
    uncomment: 'Quitar comentario',
    unflagging: 'Quitar reporte',
    unfollow: 'Dejar de seguir',
    unshare: 'Dejar de compartir',
    unlike: 'Ya no me gusta',
    unlock: 'Desbloquear',
    unread: 'No leído',
    uncheckAll: 'Deseleccionar todo',
    update: 'Actualizar',
    upload: 'Subir',
    urge: 'Urgir',
    validate: 'Validar',
    version: 'Versión',
    yes: 'Sí'
  },

  /** <summary>4. entity</summary> 审计字段顺序与 TaktEntityBase 一致 */
  entity: {
    configId: 'ID de configuración',
    extFieldJson: 'Campo extendido JSON',
    remark: 'Observación',
    createId: 'ID del creador',
    createBy: 'Creado por',
    createTime: 'Fecha de creación',
    updateId: 'ID del actualizador',
    updateBy: 'Actualizado por',
    updateTime: 'Fecha de actualización',
    isDeleted: 'Eliminado',
    deleteId: 'ID del eliminador',
    deletedBy: 'Eliminado por',
    deletedTime: 'Fecha de eliminación'
  },

  /** <summary>5. msg</summary> */
  msg: {
    actionFail: 'Error en {action}',
    actionSuccess: '{action} correctamente',
    assignFail: 'Error al asignar {target}',
    assignSuccess: '{target} asignado correctamente',
    createSuccess: '{target} creado correctamente',
    deleteFail: 'Error al eliminar {target}',
    deleteSuccess: '{target} eliminado correctamente',
    entityIdRequired: 'ID de {entity} requerido',
    entityNotFound: '{entity} no encontrado',
    exportFail: 'Error al exportar {target}',
    exportSuccess: '{target} exportado correctamente',
    loadFail: 'Error al cargar datos',
    loadOptionsFail: 'Error al cargar opciones de {target}. Intente más tarde.',
    loadListFail: 'Error al cargar la lista de {target}.\nCompruebe el servidor e inténtelo de nuevo.',
    loadTargetFail: 'Error al cargar {target}',
    noSearchResult: 'Sin resultados de búsqueda',
    operateFail: '{action} fallido',
    updateSuccess: '{target} actualizado correctamente'
  },

  /** <summary>6. action</summary> */
  action: {
    cancel: 'Cancelar',
    confirmAction: 'Confirmar {action}',
    confirmDelete: 'Confirmar eliminación',
    etc: 'etc.',
    exportDataSuffix: 'datos',
    import: {
      hint: 'Se admite importación Excel (.xlsx). Máx. 1000 registros por importación.',
      sheetNameTemplate: 'Plantilla de importación {entity}',
      templateText: 'Descargar plantilla de importación {entity}',
      uploadText: 'Haga clic o arrastre el archivo Excel aquí para subir'
    },
    management: 'Gestión',
    operation: 'Operación',
    or: 'o',
    superRole: 'Rol superior',
    superUser: 'Superusuario',
    tabTargetAllocation: 'Asignación de {target}',
    thisTarget: 'Este {target}',
    transferAssigned: 'Asignado',
    transferUnassigned: 'No asignado',
    warnAction: {},
    warnSelectToAction: 'Seleccione {entity} para {action}',
    warnSubjectCannot: '{subject} no puede {action}',
    warnUserCannot: 'El usuario {name} no puede {action}'
  },

  /** <summary>7. form</summary> */
  form: {
    tabs: {
      basicInfo: 'Información básica'
    },
    placeholder: {
      copyright: 'Introduzca información de copyright',
      orderNumHint: 'Valor menor = posición superior',
      required: 'Introduzca {field}',
      requiredAgain: 'Introduzca {field} de nuevo',
      search: 'Introduzca {keyword}',
      searchKeyword: 'Introduzca palabra clave para buscar',
      searchMenu: 'Buscar menú, páginas...',
      select: 'Seleccione {field}',
      selectFirst: 'Seleccione primero {field}',
      selectOnly: 'Seleccione',
      treeKeyword: 'Palabra clave del árbol',
      watermark: 'Introduzca contenido de marca de agua'
    },
    validation: {
      enterValid: 'Introduzca un {field} válido',
      notFound: '{target} no encontrado'
    }
  },

  /** <summary>8. confirm</summary> */
  confirm: {
    deleteCountEntity: '¿Está seguro de eliminar los {count} {entity} seleccionados?',
    deleteEntity: '¿Está seguro de eliminar {entity} "{name}"?',
    resetPwdContent: '¿Está seguro de restablecer la contraseña de {entity} "{name}" por defecto?',
    unlockContent: '¿Está seguro de desbloquear {entity} "{name}"?'
  },
  /** PWA aviso de actualización */
  pwa: {
    offlineReady: 'La aplicación está lista para trabajar sin conexión',
    needRefresh: 'Nuevo contenido disponible, haga clic en recargar para actualizar',
    reload: 'Recargar',
    close: 'Cerrar'
  },

  api: {
    loginExpired: 'Sesión expirada, inicie sesión de nuevo',
    tokenRefreshFail: 'Error al actualizar el token',
    redirectingToLogin: 'Redirigiendo al inicio de sesión',
    tokenRefreshFailOnLoginPage: 'Error al actualizar el token, ya está en la página de inicio de sesión',
    requestFail: 'Error en la solicitud',
    forbidden: 'Acceso denegado',
    notFound: 'El recurso solicitado no existe',
    serverError: 'Error del servidor, intente más tarde',
    systemError: 'Error interno del sistema, intente más tarde',
    csrfFail: 'Error de verificación de seguridad, actualice la página',
    connectFail: 'Error de conexión',
    connectFailDescription: 'No se puede conectar al servidor. Compruebe:\n1. Si el servicio está en ejecución\n2. La conexión de red\n3. La configuración de la URL de la API',
    requestConfigError: 'Error de configuración de la solicitud'
  }
}
