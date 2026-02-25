/**
 * Common translations · Русский
 * Same structure and key order as zh-CN (alphabetical for button)
 */
export default {
  /** <summary>1. app</summary> */
  app: {
    htmlTitle: 'Takt Digital Factory',
    name: 'Takt Digital Factory (TDF) ',
    productcode: 'TDF-MES-PRO',
    slogan: 'Ведём в будущее',
    tagline: 'Практично·Просто·Гибко'
  },

  /** <summary>2. settings</summary> */
  settings: {
    /** Десять знаменитых цветов, соответствует color-base.less */
    color: {
      blue: 'Краин-блю',
      brown: 'Ван Дейк браун',
      cyan: 'Тиффани-блю',
      gold: 'Золотой',
      gray: 'Мемориал-грей',
      green: 'Марс-грин',
      indigo: 'Прусский синий',
      orange: 'Красно-тициановский',
      pink: 'Бордо',
      purple: 'Бургундский',
      red: 'Китайский красный',
      yellow: 'Жёлтый Сеннелье',
      switch: 'Сменить цвет',
      title: 'Цвет',
      locked: 'Цвет темы на сегодня зафиксирован и не может быть изменён'
    },
    locale: {
      'ar-SA': 'العربية',
      'en-US': 'English',
      'es-ES': 'Español',
      'fr-FR': 'Français',
      'ja-JP': '日本語',
      'ko-KR': '한국어',
      'ru-RU': 'Русский',
      'zh-CN': 'Китайский (упрощ.)',
      'zh-TW': 'Китайский (традиц.)',
      switch: 'Сменить язык',
      title: 'Язык'
    },
    theme: {
      dark: 'Тёмная',
      light: 'Светлая',
      switch: 'Сменить тему',
      title: 'Тема'
    }
  },

  /** Layout position */
  layout: {
    position: {
      left: 'Слева',
      center: 'По центру',
      right: 'Справа'
    }
  },

  /** <summary>3. button</summary> Alphabetical order as zh-CN */
  button: {
    addsign: 'Добавить подпись',
    advancedQuery: 'Расширенный поиск',
    allocate: 'Назначить',
    approve: 'Утвердить',
    archive: 'Архив',
    back: 'Назад',
    cancel: 'Отмена',
    changepwd: 'Изменить пароль',
    checkAll: 'Выбрать все',
    clean: 'Очистить',
    clone: 'Клонировать',
    collapse: 'Свернуть',
    columnSetting: 'Настройка столбцов',
    comment: 'Комментировать',
    config: 'Настройки',
    confirm: 'Подтвердить',
    copy: 'Копировать',
    create: 'Создать',
    createRow: 'Добавить строку',
    datasource: 'Источник данных',
    delegate: 'Делегировать',
    delete: 'Удалить',
    design: 'Проектирование',
    detail: 'Подробнее',
    disable: 'Отключить',
    download: 'Скачать',
    draft: 'Черновик',
    edit: 'Изменить',
    enable: 'Включить',
    empty: 'Очистить',
    empty30d: 'Очистить 30 дней',
    empty7d: 'Очистить 7 дней',
    emptyAll: 'Очистить всё',
    expand: 'Развернуть',
    exitFullscreen: 'Выйти из полноэкранного режима',
    export: 'Экспорт',
    favorite: 'В избранное',
    field: 'Управление полями',
    fixed: 'Закрепить',
    forward: 'Переслать',
    formData: 'Данные формы',
    fullscreen: 'Полный экран',
    history: 'История',
    import: 'Импорт',
    like: 'Нравится',
    logout: 'Выйти',
    markRead: 'Отметить прочитанным',
    more: 'Ещё',
    no: 'Нет',
    ok: 'OK',
    open: 'Открыть',
    password: 'Пароль',
    permission: 'Права доступа',
    personalSettings: 'Личные настройки',
    preview: 'Предпросмотр',
    print: 'Печать',
    preferences: 'Настройки',
    profile: 'Профиль',
    progress: 'Прогресс',
    publish: 'Опубликовать',
    query: 'Поиск',
    read: 'Прочитано',
    refresh: 'Обновить',
    reply: 'Ответить',
    reset: 'Сбросить',
    resume: 'Возобновить',
    return: 'Вернуть',
    revoke: 'Отозвать',
    run: 'Запустить',
    send: 'Отправить',
    share: 'Поделиться',
    sign: 'Подписать',
    start: 'Запуск',
    stop: 'Остановить',
    subsign: 'Убрать подпись',
    submit: 'Отправить',
    suspend: 'Приостановить',
    template: 'Шаблон',
    terminate: 'Завершить',
    theme: 'Настройки темы',
    toList: 'Список',
    toTranspose: 'Транспонирование',
    transfer: 'Передать',
    truncate: 'Обрезать',
    unfavorite: 'Убрать из избранного',
    uncomment: 'Удалить комментарий',
    unflagging: 'Снять жалобу',
    unfollow: 'Отписаться',
    unshare: 'Отменить общий доступ',
    unlike: 'Больше не нравится',
    unlock: 'Разблокировать',
    unread: 'Не прочитано',
    uncheckAll: 'Снять выбор',
    update: 'Обновить',
    upload: 'Загрузить',
    urge: 'Напомнить',
    validate: 'Проверить',
    version: 'Версия',
    yes: 'Да'
  },

  /** <summary>4. entity</summary> 审计字段顺序与 TaktEntityBase 一致 */
  entity: {
    configId: 'ID конфигурации',
    extFieldJson: 'Расширенное поле JSON',
    remark: 'Примечание',
    createId: 'ID создателя',
    createBy: 'Создано',
    createTime: 'Время создания',
    updateId: 'ID обновившего',
    updateBy: 'Обновлено',
    updateTime: 'Время обновления',
    isDeleted: 'Удалено',
    deleteId: 'ID удалившего',
    deletedBy: 'Удалено пользователем',
    deletedTime: 'Время удаления'
  },

  /** <summary>5. msg</summary> */
  msg: {
    actionFail: 'Ошибка {action}',
    actionSuccess: '{action} выполнено',
    assignFail: 'Ошибка назначения {target}',
    assignSuccess: '{target} назначен(о)',
    createSuccess: '{target} создано',
    deleteFail: 'Ошибка удаления {target}',
    deleteSuccess: '{target} удалено',
    entityIdRequired: 'Требуется ID {entity}',
    entityNotFound: '{entity} не найден(о)',
    exportFail: 'Ошибка экспорта {target}',
    exportSuccess: '{target} экспортировано',
    loadFail: 'Не удалось загрузить данные',
    loadOptionsFail: 'Не удалось загрузить опции {target}. Попробуйте позже',
    loadListFail: 'Не удалось загрузить список {target}.\nПроверьте сервер и повторите попытку.',
    loadTargetFail: 'Не удалось загрузить {target}',
    noSearchResult: 'Нет результатов поиска',
    operateFail: 'Ошибка {action}',
    updateSuccess: '{target} обновлено'
  },

  /** <summary>6. action</summary> */
  action: {
    cancel: 'Отмена',
    confirmAction: 'Подтвердить {action}',
    confirmDelete: 'Подтвердить удаление',
    etc: 'и т.д.',
    exportDataSuffix: 'данные',
    import: {
      hint: 'Поддерживается импорт Excel (.xlsx). Не более 1000 записей за импорт.',
      sheetNameTemplate: 'Шаблон импорта {entity}',
      templateText: 'Скачать шаблон импорта {entity}',
      uploadText: 'Нажмите или перетащите файл Excel сюда для загрузки'
    },
    management: 'Управление',
    operation: 'Действие',
    or: 'или',
    superRole: 'Суперроль',
    superUser: 'Суперпользователь',
    tabTargetAllocation: 'Назначение {target}',
    thisTarget: 'Этот(а) {target}',
    transferAssigned: 'Назначено',
    transferUnassigned: 'Не назначено',
    warnAction: {},
    warnSelectToAction: 'Выберите {entity} для {action}',
    warnSubjectCannot: '{subject} не может {action}',
    warnUserCannot: 'Пользователь {name} не может {action}'
  },

  /** <summary>7. form</summary> */
  form: {
    tabs: {
      basicInfo: 'Основная информация'
    },
    placeholder: {
      copyright: 'Введите информацию об авторских правах',
      orderNumHint: 'Меньшее значение = выше в списке',
      required: 'Введите {field}',
      requiredAgain: 'Введите {field} снова',
      search: 'Введите {keyword}',
      searchKeyword: 'Введите ключевое слово для поиска',
      searchMenu: 'Поиск меню, страниц...',
      select: 'Выберите {field}',
      selectFirst: 'Сначала выберите {field}',
      selectOnly: 'Выберите',
      treeKeyword: 'Ключевое слово дерева',
      watermark: 'Введите содержимое водяного знака'
    },
    validation: {
      enterValid: 'Введите корректный(ую) {field}',
      notFound: '{target} не найден(о)'
    }
  },

  /** <summary>8. confirm</summary> */
  confirm: {
    deleteCountEntity: 'Удалить выбранные {count} {entity}?',
    deleteEntity: 'Удалить {entity} «{name}»?',
    resetPwdContent: 'Сбросить пароль {entity} «{name}» на пароль по умолчанию?',
    unlockContent: 'Разблокировать {entity} «{name}»?'
  },
  /** PWA уведомление об обновлении */
  pwa: {
    offlineReady: 'Приложение готово к работе в автономном режиме',
    needRefresh: 'Доступно новое содержимое, нажмите «Обновить» для обновления',
    reload: 'Обновить',
    close: 'Закрыть'
  },

  api: {
    loginExpired: 'Сессия истекла, войдите снова',
    tokenRefreshFail: 'Не удалось обновить токен',
    redirectingToLogin: 'Перенаправление на страницу входа',
    tokenRefreshFailOnLoginPage: 'Не удалось обновить токен, вы уже на странице входа',
    requestFail: 'Ошибка запроса',
    forbidden: 'Доступ запрещён',
    notFound: 'Запрашиваемый ресурс не найден',
    serverError: 'Ошибка сервера, попробуйте позже',
    systemError: 'Внутренняя ошибка системы, попробуйте позже',
    csrfFail: 'Ошибка проверки безопасности, обновите страницу',
    connectFail: 'Ошибка соединения',
    connectFailDescription: 'Не удалось подключиться к серверу. Проверьте:\n1. Запущен ли сервис\n2. Сетевое подключение\n3. Настройки URL API',
    requestConfigError: 'Ошибка настройки запроса'
  }
}
