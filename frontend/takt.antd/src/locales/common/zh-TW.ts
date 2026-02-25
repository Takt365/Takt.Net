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
    slogan: '驅動未來',
    tagline: '實用·簡潔·靈活'
  },

  /** <summary>2. 设置类</summary> 主题、语言、颜色 */
  settings: {
    /** 十大著名色彩，與 color-base.less 一致 */
    color: {
      blue: '克萊因藍',
      brown: '凡戴克棕',
      cyan: '蒂芙尼藍',
      gray: '紀念灰',
      green: '馬爾斯綠',
      indigo: '普魯士藍',
      orange: '提香紅',
      pink: '波爾多紅',
      purple: '勃艮第紅',
      red: '中國紅',
      yellow: '申布倫黃',
      switch: '切換顏色',
      title: '顏色',
      locked: '今日主題色固定，不可變更'
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
    open: '打開',
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
    share: '分享',
    sign: '签收',
    start: '启动',
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

  /** <summary>4. 实体基类</summary> 租户、审计等通用字段，顺序与 TaktEntityBase 一致 */
  entity: {
    configId: '租户配置ID',
    extFieldJson: '扩展字段JSON',
    remark: '备注',
    createId: '创建人ID',
    createBy: '创建人',
    createTime: '创建时间',
    updateId: '更新人ID',
    updateBy: '更新人',
    updateTime: '更新时间',
    isDeleted: '是否删除',
    deleteId: '删除人ID',
    deletedBy: '删除人',
    deletedTime: '删除时间'
  },

  /** <summary>5. 消息类</summary> 加载/删除/操作结果等提示 */
  msg: {
    actionFail: '{action}失败',
    actionSuccess: '{action}成功',
    assignFail: '分配{target}失败',
    assignSuccess: '分配{target}成功',
    createSuccess: '創建{target}成功',
    deleteFail: '刪除{target}失敗',
    deleteSuccess: '刪除{target}成功',
    entityIdRequired: '{entity}ID不存在',
    entityNotFound: '{entity}信息不存在',
    exportFail: '導出{target}失敗',
    exportSuccess: '導出{target}成功',
    loadFail: '加载数据失败',
    loadOptionsFail: '加載{target}選項失敗，請稍後重試',
    loadListFail: '加載{target}列表數據失敗，\n請檢查伺服器並重試',
    loadTargetFail: '加载{target}失败',
    noSearchResult: '暂无搜索结果',
    operateFail: '{action}失敗',
    updateSuccess: '更新{target}成功'
  },

  /** <summary>6. 操作类</summary> 弹窗、页面标题、穿梭框、导入、分配/警告等 */
  action: {
    cancel: '取消',
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
      watermark: '请输入水印内容',
      lengthExact: '{field}必须为{length}位'
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
  /** PWA 更新提示 */
  pwa: {
    offlineReady: '應用已就緒，可離線使用',
    needRefresh: '發現新版本，點擊重新整理以更新',
    reload: '重新整理',
    close: '關閉'
  },

  api: {
    loginExpired: '登入已過期，請重新登入',
    tokenRefreshFail: 'Token 重新整理失敗',
    redirectingToLogin: '正在跳轉登入頁',
    tokenRefreshFailOnLoginPage: 'Token 重新整理失敗，已在登入頁',
    requestFail: '請求失敗',
    forbidden: '無權存取',
    notFound: '請求的資源不存在',
    serverError: '伺服器錯誤，請稍後重試',
    systemError: '系統內部錯誤，請稍後重試',
    csrfFail: '安全驗證失敗，請重新整理頁面',
    connectFail: '連線失敗',
    connectFailDescription: '無法連線至伺服器，請檢查：\n1. 後端服務是否已啟動\n2. 網路連線是否正常\n3. API 位址設定是否正確',
    requestConfigError: '請求設定錯誤'
  }
}
