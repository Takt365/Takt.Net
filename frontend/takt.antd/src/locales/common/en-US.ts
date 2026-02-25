/**
 * Common translations · English
 * Same structure and key order as zh-CN (alphabetical for button)
 */
export default {
  /** <summary>1. app</summary> */
  app: {
    htmlTitle: 'Takt Digital Factory',
    name: 'Takt Digital Factory (TDF) ',
    productcode: 'TDF-MES-PRO',
    slogan: 'Driving the Future',
    tagline: 'Practical·Simple·Flexible'
  },

  /** <summary>2. settings</summary> */
  settings: {
    /** Ten famous colors, matches color-base.less */
    color: {
      blue: 'Klein Blue',
      brown: 'Van Dyke Brown',
      cyan: 'Tiffany Blue',
      gray: 'Memorial Gray',
      green: 'Mars Green',
      indigo: 'Prussian Blue',
      orange: 'Titian Red',
      pink: 'Bordeaux',
      purple: 'Burgundy',
      red: 'Chinese Red',
      yellow: 'Sennelier Yellow',
      switch: 'Switch Color',
      title: 'Color',
      locked: 'Today\'s theme color is fixed and cannot be changed'
    },
    locale: {
      'ar-SA': 'العربية',
      'en-US': 'English',
      'es-ES': 'Español',
      'fr-FR': 'Français',
      'ja-JP': 'Japanese',
      'ko-KR': 'Korean',
      'ru-RU': 'Russian',
      'zh-CN': 'Chinese (Simplified)',
      'zh-TW': 'Chinese (Traditional)',
      switch: 'Switch Language',
      title: 'Language'
    },
    theme: {
      dark: 'Dark',
      light: 'Light',
      switch: 'Switch Theme',
      title: 'Theme'
    }
  },

  /** Layout position */
  layout: {
    position: {
      left: 'Left',
      center: 'Center',
      right: 'Right'
    }
  },

  /** <summary>3. button</summary> Alphabetical order as zh-CN */
  button: {
    addsign: 'Add Sign',
    advancedQuery: 'Advanced Query',
    allocate: 'Allocate',
    approve: 'Approve',
    archive: 'Archive',
    back: 'Back',
    cancel: 'Cancel',
    changepwd: 'Change Password',
    checkAll: 'Check All',
    clean: 'Clean',
    clone: 'Clone',
    collapse: 'Collapse',
    columnSetting: 'Column Setting',
    comment: 'Comment',
    config: 'Config',
    confirm: 'Confirm',
    copy: 'Copy',
    create: 'Create',
    createRow: 'Add Row',
    datasource: 'Data Source',
    delegate: 'Delegate',
    delete: 'Delete',
    design: 'Design',
    detail: 'Detail',
    disable: 'Disable',
    download: 'Download',
    draft: 'Draft',
    edit: 'Edit',
    enable: 'Enable',
    empty: 'Empty',
    empty30d: 'Clear 30 Days',
    empty7d: 'Clear 7 Days',
    emptyAll: 'Clear All',
    expand: 'Expand',
    exitFullscreen: 'Exit Fullscreen',
    export: 'Export',
    favorite: 'Favorite',
    field: 'Field Management',
    fixed: 'Fixed',
    forward: 'Forward',
    formData: 'Form Data',
    fullscreen: 'Fullscreen',
    history: 'History',
    import: 'Import',
    like: 'Like',
    logout: 'Logout',
    markRead: 'Mark Read',
    more: 'More',
    no: 'No',
    ok: 'OK',
    open: 'Open',
    password: 'Password',
    permission: 'Permission Settings',
    personalSettings: 'Personal Settings',
    preview: 'Preview',
    print: 'Print',
    preferences: 'Preferences',
    profile: 'Profile',
    progress: 'Progress',
    publish: 'Publish',
    query: 'Query',
    read: 'Read',
    refresh: 'Refresh',
    reply: 'Reply',
    reset: 'Reset',
    resume: 'Resume',
    return: 'Return',
    revoke: 'Revoke',
    run: 'Run',
    send: 'Send',
    share: 'Share',
    sign: 'Sign',
    start: 'Start',
    stop: 'Stop',
    subsign: 'Remove Sign',
    submit: 'Submit',
    suspend: 'Suspend',
    template: 'Template',
    terminate: 'Terminate',
    theme: 'Theme Settings',
    toList: 'List',
    toTranspose: 'Transpose',
    transfer: 'Transfer',
    truncate: 'Truncate',
    unfavorite: 'Unfavorite',
    uncomment: 'Uncomment',
    unflagging: 'Unreport',
    unfollow: 'Unfollow',
    unshare: 'Unshare',
    unlike: 'Unlike',
    unlock: 'Unlock',
    unread: 'Unread',
    uncheckAll: 'Uncheck All',
    update: 'Update',
    upload: 'Upload',
    urge: 'Urge',
    validate: 'Validate',
    version: 'Version',
    yes: 'Yes'
  },

  /** <summary>4. entity</summary> 审计字段顺序与 TaktEntityBase 一致 */
  entity: {
    configId: 'Config ID',
    extFieldJson: 'Extended Field JSON',
    remark: 'Remark',
    createId: 'Created By ID',
    createBy: 'Created By',
    createTime: 'Create Time',
    updateId: 'Updated By ID',
    updateBy: 'Updated By',
    updateTime: 'Update Time',
    isDeleted: 'Is Deleted',
    deleteId: 'Deleted By ID',
    deletedBy: 'Deleted By',
    deletedTime: 'Deleted Time'
  },

  /** <summary>5. msg</summary> */
  msg: {
    actionFail: '{action} failed',
    actionSuccess: '{action} successfully',
    assignFail: 'Assign {target} failed',
    assignSuccess: '{target} assigned successfully',
    createSuccess: '{target} created successfully',
    deleteFail: 'Failed to delete {target}',
    deleteSuccess: '{target} deleted successfully',
    entityIdRequired: '{entity} ID is required',
    entityNotFound: '{entity} not found',
    exportFail: 'Failed to export {target}',
    exportSuccess: '{target} exported successfully',
    loadFail: 'Failed to load data',
    loadOptionsFail: 'Failed to load {target} options. Please try again later.',
    loadListFail: 'Failed to load {target} list.\nPlease check the server and try again.',
    loadTargetFail: 'Failed to load {target}',
    noSearchResult: 'No search results',
    operateFail: '{action} failed',
    updateSuccess: '{target} updated successfully'
  },

  /** <summary>6. action</summary> */
  action: {
    cancel: 'Cancel',
    confirmAction: 'Confirm {action}',
    confirmDelete: 'Confirm Delete',
    etc: 'etc.',
    exportDataSuffix: ' Data',
    import: {
      hint: 'Excel (.xlsx) import supported. Max 1000 records per import.',
      sheetNameTemplate: '{entity} Import Template',
      templateText: 'Download {entity} Import Template',
      uploadText: 'Click or drag Excel file to this area to upload'
    },
    management: 'Management',
    operation: 'Operation',
    or: 'or',
    superRole: 'Super role',
    superUser: 'Super user',
    tabTargetAllocation: '{target} Allocation',
    thisTarget: 'This {target}',
    transferAssigned: 'Assigned',
    transferUnassigned: 'Unassigned',
    warnAction: {},
    warnSelectToAction: 'Please select {entity} to {action}',
    warnSubjectCannot: '{subject} cannot {action}',
    warnUserCannot: 'User {name} cannot {action}'
  },

  /** <summary>7. form</summary> */
  form: {
    tabs: {
      basicInfo: 'Basic Info'
    },
    placeholder: {
      copyright: 'Enter copyright info',
      orderNumHint: 'Lower value = higher position',
      required: 'Enter {field}',
      requiredAgain: 'Enter {field} again',
      search: 'Enter {keyword}',
      searchKeyword: 'Enter keyword to search',
      searchMenu: 'Search menu, pages...',
      select: 'Select {field}',
      selectFirst: 'Please select {field} first',
      selectOnly: 'Please select',
      treeKeyword: 'Tree keyword',
      watermark: 'Enter watermark content',
      lengthExact: '{field} must be exactly {length} characters'
    },
    validation: {
      enterValid: 'Please enter a valid {field}',
      notFound: '{target} not found'
    }
  },

  /** <summary>8. confirm</summary> */
  confirm: {
    deleteCountEntity: 'Are you sure to delete the selected {count} {entity}(s)?',
    deleteEntity: 'Are you sure to delete {entity} "{name}"?',
    resetPwdContent: 'Are you sure to reset {entity} "{name}" password to default?',
    unlockContent: 'Are you sure to unlock {entity} "{name}"?'
  },

  /** <summary>9. PWA update prompt</summary> when Service Worker detects new version */
  pwa: {
    offlineReady: 'App ready to work offline',
    needRefresh: 'New content available, click reload to update',
    reload: 'Reload',
    close: 'Close'
  },

  /** <summary>10. API request</summary> request interceptor / 401 / network errors (request.ts) */
  api: {
    loginExpired: 'Login expired, please sign in again',
    tokenRefreshFail: 'Token refresh failed',
    redirectingToLogin: 'Redirecting to login',
    tokenRefreshFailOnLoginPage: 'Token refresh failed, already on login page',
    requestFail: 'Request failed',
    forbidden: 'Access denied',
    notFound: 'The requested resource was not found',
    serverError: 'Server error, please try again later',
    systemError: 'System error, please try again later',
    csrfFail: 'Security verification failed, please refresh the page',
    connectFail: 'Connection failed',
    connectFailDescription: 'Unable to connect to the server. Please check:\n1. Whether the backend service is running\n2. Network connection\n3. API base URL configuration',
    requestConfigError: 'Request configuration error'
  }
}
