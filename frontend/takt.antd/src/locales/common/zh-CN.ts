/**
 * 通用翻译 · 中文
 * 仅保留通用节点：app | settings | button | entity | msg | action | form | confirm
 * captcha/import/upload/download → components/common；tabs/systemSetting → components/navigation；session → layouts
 */
export default {
  page: {
    /** <summary>1. app 类</summary> 应用名称、htmlTitle、版本号、口号、标语 */
      app: {
        htmltitle: 'Takt Digital Factory',
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
          'ar-sa': 'العربية',
          'en-us': 'English',
          'es-es': 'Español',
          'fr-fr': 'Français',
          'ja-jp': '日本語',
          'ko-kr': '한국어',
          'ru-ru': 'Русский',
          'zh-cn': '简体中文',
          'zh-tw': '繁体中文',
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
        advancedoptions: '高级选项',
        advancedquery: '高级查询',
        advancedsettings: '高级设置',
        allocate: '分配',
        approve: '审批',
        archive: '归档',
        back: '返回',
        cancel: '取消',
        changepwd: '修改密码',
        change: '变更',
        checkall: '全选',
        clean: '清理',
        clone: '克隆',
        collapse: '收缩',
        columnsetting: '列设置',
        comment: '评论',
        config: '配置',
        confirm: '确认',
        copy: '复制',
        countersign: '会签',
        create: '新增',
        createrow: '新增行',
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
        emptyall: '清空全部',
        expand: '展开',
        exitfullscreen: '退出全屏',
        export: '导出',
        favorite: '收藏',
        field: '字段管理',
        fixed: '固定',
        forward: '转发',
        formdata: '表单数据',
        fullscreen: '全屏',
        generate: '生成',
        history: '历史',
        import: '导入',
        initialize: '初始化',
        like: '点赞',
        logout: '退出登录',
        markread: '标记已读',
        more: '更多',
        no: '否',
        ok: '确定',
        password: '密码',
        permission: '权限设置',
        personalsettings: '个人设置',
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
        sendmessage: '发送消息',
        share: '分享',
        sign: '签收',
        start: '启动',
        startflow: '发起流程',
        stop: '停止',
        sync: '同步',
        subsign: '减签',
        submit: '提交',
        suspend: '暂停',
        template: '模板',
        terminate: '终止',
        theme: '主题设置',
        tolist: '列表',
        totranspose: '转置',
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
        uncheckall: '反选',
        update: '修改',
        upload: '上传',
        urge: '催办',
        validate: '验证',
        version: '版本',
        yes: '是'
      },
    
      /** <summary>4. 实体基类</summary> 与后端 TaktEntityBase 一致（id/configId/createdById/createdBy/createdAt/updatedById/updatedBy/updatedAt/isDeleted/deletedById/deletedBy/deletedAt） */
      entity: {
        configid: '租户配置ID',
        createby: '创建人',
        createtime: '创建时间',
        createdbyid: '创建人ID',
        deletedby: '删除人',
        deletedbyid: '删除人ID',
        deletedtime: '删除时间',
        extfieldjson: '扩展字段JSON',
        id: 'ID',
        isdeleted: '是否删除',
        remark: '备注',
        updateby: '更新人',
        updatetime: '更新时间',
        updatedbyid: '更新人ID'
      },
    
      /** <summary>5. 消息类</summary> 加载/删除/操作结果等提示 */
      msg: {
        actionfail: '{action}失败',
        actionsuccess: '{action}成功',
        assignfail: '分配{target}失败',
        assignsuccess: '分配{target}成功',
        createsuccess: '创建成功',
        deletefail: '删除失败',
        deletesuccess: '删除成功',
        entityidrequired: '{entity}ID不存在',
        entitynotfound: '{entity}信息不存在',
        exportfail: '导出失败',
        exportsuccess: '导出成功',
        loadfail: '加载数据失败',
        loadoptionsfail: '加载选项数据失败，请稍后重试',
        loadtargetfail: '加载{target}失败',
        nosearchresult: '暂无搜索结果',
        operatefail: '操作失败',
        updatesuccess: '更新成功'
      },
    
      /** <summary>6. 操作类</summary> 弹窗、页面标题、穿梭框、导入、分配/警告等 */
      action: {
        confirmaction: '确认{action}',
        confirmdelete: '确认删除',
        etc: '等',
        exportdatasuffix: '数据',
        import: {
          hint: '支持Excel文件（.xlsx）导入。单次导入最多1000条记录（不限制数据表总条数）。',
          sheetnametemplate: '{entity}导入模板',
          templatetext: '下载{entity}导入模板',
          uploadtext: '点击或拖拽Excel文件到此区域上传'
        },
        management: '管理',
        operation: '操作',
        or: '或',
        superrole: '超级角色',
        superuser: '超级用户',
        tabtargetallocation: '{target}分配',
        thistarget: '该{target}',
        transferassigned: '已分配',
        transferunassigned: '未分配',
        warnaction: {},
        warnselecttoaction: '请选择要{action}的{entity}',
        warnsubjectcannot: '{subject}不允许{action}',
        warnusercannot: '用户 {name} 不允许{action}'
      },
    
      /** <summary>7. 表单类</summary> placeholder、表单 Tab（通用验证文案见后端种子 validation.*，由 i18n 合并） */
      form: {
        tabs: {
          basicinfo: '基本信息',
          announcementbody: '公告正文',
          announcementpublish: '发布设置',
          announcementother: '其它'
        },
        placeholder: {
          copyright: '请输入版权信息',
          keyword: '关键字',
          ordernumhint: '越小越靠前',
          required: '请输入{field}',
          requiredagain: '请再次输入{field}',
          search: '请输入{keyword}',
          searchkeyword: '请输入关键字查询',
          searchmenu: '搜索菜单、页面...',
          select: '请选择{field}',
          selectfirst: '请先选择{field}',
          selectonly: '请选择',
          treekeyword: '树关键字',
          watermark: '请输入水印内容'
        }
      },
    
      /** <summary>8. 确认类</summary> 删除/重置密码/解锁等确认文案 */
      confirm: {
        deletecountentity: '确定要删除选中的 {count} 个{entity}吗？',
        deleteentity: '确定要删除{entity} "{name}" 吗？',
        resetpwdcontent: '确定要重置{entity} "{name}" 的密码为默认密码吗？',
        unlockcontent: '确定要解除锁定{entity} "{name}" 吗？'
      },
    
      /** <summary>9. API 请求类</summary> request 拦截器/401/网络错误等提示（request.ts） */
      api: {
        loginexpired: '登录已过期，请重新登录',
        tokenrefreshfail: 'Token 刷新失败',
        redirectingtologin: '正在跳转登录页',
        tokenrefreshfailonloginpage: 'Token 刷新失败，已在登录页',
        requestfail: '请求失败',
        forbidden: '无权访问',
        notfound: '请求的资源不存在',
        servererror: '服务器错误，请稍后重试',
        systemerror: '系统内部错误，请稍后重试',
        csrffail: '安全验证失败，请刷新页面重试',
        connectfail: '连接失败',
        connectfaildescription: '无法连接到服务器，请检查：\n1. 后端服务是否已启动\n2. 网络连接是否正常\n3. API 地址配置是否正确',
        requestconfigerror: '请求配置错误'
      }
  }
}
