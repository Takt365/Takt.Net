/**
 * 通用翻译 · 中文
 * 仅保留通用节点：app | settings | button | entity | msg | action | form | confirm
 * captcha/import/upload/download → components/common；tabs/systemSetting → components/navigation；session → layouts
 */
export default {
  /** <summary>1. app 类</summary> 应用名称、htmlTitle、版本号、口号、标语 */
  app: {
    htmlTitle: 'Takt Digital Factory',
    name: 'Takt Digital Factory (TDF) ',
    productcode: 'TDF-MES-PRO',
    slogan: '驱动未来',
    tagline: '实用·简洁·灵活'
  },

  /** <summary>2. 设置类</summary> 主题、语言、颜色 */
  settings: {
    color: {
      blue: '蓝色',
      brown: '棕色',
      cyan: '青色',
      gray: '灰色',
      green: '绿色',
      indigo: '靛蓝',
      orange: '橙色',
      pink: '粉色',
      purple: '紫色',
      red: '红色',
      yellow: '黄色',
      switch: '切换颜色',
      title: '颜色'
    },
    locale: {
      'ar-SA': 'العربية',
      'en-US': 'English',
      'es-ES': 'Español',
      'fr-FR': 'Français',
      'ja-JP': '日本語',
      'ko-KR': '한국어',
      'ru-RU': 'Русский',
      'zh-CN': '简体中文',
      'zh-TW': '繁体中文',
      switch: '切换语言',
      title: '语言'
    },
    theme: {
      dark: '暗色',
      light: '亮色',
      switch: '切换主题',
      title: '主题'
    }
  },

  /** 布局位置（通用组件如布局切换等） */
  layout: {
    position: {
      left: '居左',
      center: '居中',
      right: '居右'
    }
  },

  /** <summary>3. 按钮类</summary> 各类按钮文案 */
  button: {
    addsign: '加签',
    advancedQuery: '高级查询',
    allocate: '分配',
    approve: '审批',
    archive: '归档',
    back: '返回',
    cancel: '取消',
    changepwd: '修改密码',
    checkAll: '全选',
    clean: '清理',
    clone: '克隆',
    collapse: '收缩',
    columnSetting: '列设置',
    comment: '评论',
    config: '配置',
    confirm: '确认',
    copy: '复制',
    create: '新增',
    createRow: '新增行',
    datasource: '数据源',
    delegate: '委托',
    delete: '删除',
    design: '设计',
    detail: '详情',
    disable: '停用',
    download: '下载',
    draft: '草稿',
    edit: '编辑',
    enable: '启用',
    empty: '清空',
    empty30d: '清空30天',
    empty7d: '清空7天',
    emptyAll: '清空全部',
    expand: '展开',
    exitFullscreen: '退出全屏',
    export: '导出',
    favorite: '收藏',
    field: '字段管理',
    fixed: '固定',
    forward: '转发',
    formData: '表单数据',
    fullscreen: '全屏',
    history: '历史',
    import: '导入',
    like: '点赞',
    logout: '退出登录',
    markRead: '标记已读',
    more: '更多',
    no: '否',
    ok: '确定',
    password: '密码',
    permission: '权限设置',
    personalSettings: '个人设置',
    preview: '预览',
    print: '打印',
    preferences: '偏好',
    profile: '个人中心',
    progress: '进度',
    publish: '发布',
    query: '查询',
    read: '已读',
    refresh: '刷新',
    reply: '回复',
    reset: '重置',
    resume: '恢复',
    return: '退回',
    revoke: '撤销',
    run: '运行',
    send: '发送',
    sendMessage: '发送消息',
    share: '分享',
    sign: '签收',
    start: '启动',
    startFlow: '发起流程',
    stop: '停止',
    subsign: '减签',
    submit: '提交',
    suspend: '暂停',
    template: '模板',
    terminate: '终止',
    theme: '主题设置',
    toList: '列表',
    toTranspose: '转置',
    transfer: '转办',
    truncate: '截断',
    unfavorite: '取消收藏',
    uncomment: '取消评论',
    unflagging: '取消举报',
    unfollow: '取消关注',
    unshare: '取消分享',
    unlike: '取消点赞',
    unlock: '解除锁定',
    unread: '未读',
    uncheckAll: '反选',
    update: '修改',
    upload: '上传',
    urge: '催办',
    validate: '验证',
    version: '版本',
    yes: '是'
  },

  /** <summary>4. 实体基类</summary> 与后端 TaktEntityBase 一致（id/configId/createdById/createdBy/createdAt/updatedById/updatedBy/updatedAt/isDeleted/deletedById/deletedBy/deletedAt） */
  entity: {
    configId: '租户配置ID',
    createBy: '创建人',
    createTime: '创建时间',
    createdById: '创建人ID',
    deletedBy: '删除人',
    deletedById: '删除人ID',
    deletedTime: '删除时间',
    extFieldJson: '扩展字段JSON',
    id: 'ID',
    isDeleted: '是否删除',
    remark: '备注',
    updateBy: '更新人',
    updateTime: '更新时间',
    updatedById: '更新人ID'
  },

  /** <summary>5. 消息类</summary> 加载/删除/操作结果等提示 */
  msg: {
    actionFail: '{action}失败',
    actionSuccess: '{action}成功',
    assignFail: '分配{target}失败',
    assignSuccess: '分配{target}成功',
    createSuccess: '创建成功',
    deleteFail: '删除失败',
    deleteSuccess: '删除成功',
    entityIdRequired: '{entity}ID不存在',
    entityNotFound: '{entity}信息不存在',
    exportFail: '导出失败',
    exportSuccess: '导出成功',
    loadFail: '加载数据失败',
    loadOptionsFail: '加载选项数据失败，请稍后重试',
    loadTargetFail: '加载{target}失败',
    noSearchResult: '暂无搜索结果',
    operateFail: '操作失败',
    updateSuccess: '更新成功'
  },

  /** <summary>6. 操作类</summary> 弹窗、页面标题、穿梭框、导入、分配/警告等 */
  action: {
    confirmAction: '确认{action}',
    confirmDelete: '确认删除',
    etc: '等',
    exportDataSuffix: '数据',
    import: {
      hint: '支持Excel文件（.xlsx）导入。单次导入最多1000条记录（不限制数据表总条数）。',
      sheetNameTemplate: '{entity}导入模板',
      templateText: '下载{entity}导入模板',
      uploadText: '点击或拖拽Excel文件到此区域上传'
    },
    management: '管理',
    operation: '操作',
    or: '或',
    superRole: '超级角色',
    superUser: '超级用户',
    tabTargetAllocation: '{target}分配',
    thisTarget: '该{target}',
    transferAssigned: '已分配',
    transferUnassigned: '未分配',
    warnAction: {},
    warnSelectToAction: '请选择要{action}的{entity}',
    warnSubjectCannot: '{subject}不允许{action}',
    warnUserCannot: '用户 {name} 不允许{action}'
  },

  /** <summary>7. 验证类</summary> placeholder、validation、表单 Tab */
  form: {
    tabs: {
      basicInfo: '基本信息'
    },
    placeholder: {
      copyright: '请输入版权信息',
      orderNumHint: '越小越靠前',
      required: '请输入{field}',
      requiredAgain: '请再次输入{field}',
      search: '请输入{keyword}',
      searchKeyword: '请输入关键字查询',
      searchMenu: '搜索菜单、页面...',
      select: '请选择{field}',
      selectFirst: '请先选择{field}',
      selectOnly: '请选择',
      treeKeyword: '树关键字',
      watermark: '请输入水印内容'
    },
    validation: {
      enterValid: '请输入正确的{field}',
      notFound: '{target}不存在'
    }
  },

  /** <summary>8. 确认类</summary> 删除/重置密码/解锁等确认文案 */
  confirm: {
    deleteCountEntity: '确定要删除选中的 {count} 个{entity}吗？',
    deleteEntity: '确定要删除{entity} "{name}" 吗？',
    resetPwdContent: '确定要重置{entity} "{name}" 的密码为默认密码吗？',
    unlockContent: '确定要解除锁定{entity} "{name}" 吗？'
  },

  /** <summary>9. API 请求类</summary> request 拦截器/401/网络错误等提示（request.ts） */
  api: {
    loginExpired: '登录已过期，请重新登录',
    tokenRefreshFail: 'Token 刷新失败',
    redirectingToLogin: '正在跳转登录页',
    tokenRefreshFailOnLoginPage: 'Token 刷新失败，已在登录页',
    requestFail: '请求失败',
    forbidden: '无权访问',
    notFound: '请求的资源不存在',
    serverError: '服务器错误，请稍后重试',
    systemError: '系统内部错误，请稍后重试',
    csrfFail: '安全验证失败，请刷新页面重试',
    connectFail: '连接失败',
    connectFailDescription: '无法连接到服务器，请检查：\n1. 后端服务是否已启动\n2. 网络连接是否正常\n3. API 地址配置是否正确',
    requestConfigError: '请求配置错误'
  }
}
