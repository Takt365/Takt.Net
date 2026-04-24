/**
 * 代码生成 · 中文
 * 约定：`export default { page: { … } }`，运行时键为 code.generator.page.*。
 * 列名等：entity.gentable.*；通用：common.*
 */
export default {
  page: {
    importfromdb: '从数据库导入表',
    saveas: '另存为',
    saveaspathhint: '请输入生成路径（将覆盖表配置中的路径）：',
    saveaspathplaceholder: '如：D:\\Projects\\Takt.Net',
    overwriteconfirmtitle: '覆盖确认',
    overwriteconfirmcontent: '目标路径下已存在以下文件，是否覆盖？',
    overwrite: '覆盖',
    saveascancel: '另存为',
    notableidsync: '该记录无表 ID，无法同步',
    notableidinit: '该记录无表 ID，无法初始化',
    syncformhint: '请在编辑弹窗中保存以从数据源刷新字段配置；完整同步需后端提供接口',
    clonesuccess: '已复制为新建，请修改表名后保存',
    nodataexport: '暂无数据可导出',
    codegenerateddownload: '代码已生成并下载',
    gensuccesscount: '，共 {count} 个文件',
    existingfilessuffix: '等共 {count} 个文件',
    notableidpreview: '该记录无表 ID，无法预览',
    importtable: {
      datatable: '数据表'
    },
    form: {
      tab: {
        table: '表配置',
        column: '字段配置',
        basic: '基本信息',
        business: '业务模块',
        entitydto: '实体与传输对象',
        service: '服务与控制器',
        generate: '生成',
        front: '前端与样式'
      },
      placeholder: {
        configid: '请选择数据源',
        tablenamenew: '小写下划线格式，如：xxx_xxx_xxx',
        tablenameedit: '请先选择数据源',
        tablecomment: '表注释',
        gentemplatecategory: '请选择生成模板类型',
        indatabase: '请选择是否库表',
        subtablename: '请选择父表',
        subtablefkname: '请选择外键列',
        treecode: '请选择树编码列',
        treeparentcode: '请选择树父编码列',
        treename: '请选择树名称列',
        nameprefix: '项目名称，默认 Takt，修改后所有命名空间同步',
        permsprefix: '由模块名+业务名自动生成，如 accounting:controlling:standard:wage:rate',
        genmodulename: '请选择模块（目录）或手动输入，如：Generator、HumanResource.Organization',
        genbusinessnamefromtable: '由表名自动生成',
        genbusinessnamemanual:
          '用于生成实体/服务/控制器等类名及接口注释，如：设置、部门',
        genfunctionname: '由表描述自动带出，仅读',
        autofrommodule: '由模块名自动生成',
        autofrombusiness: '由业务名自动生成',
        genmethod: '请选择生成方式',
        genpath: '/',
        currentprojectloading: '正在获取…',
        currentprojectidle: '请选择生成方式为当前项目后自动获取',
        parentmenuid: '请选择上级菜单（不选为根）',
        sorttype: '请选择排序类型',
        sortfield: '请选择排序字段',
        frontui: '请选择前端UI框架',
        frontformlayout: '请选择前端表单布局',
        frontbtnstyle: '请选择前端按钮样式',
        genauthor: '当前登录用户',
        columndbtype: 'DB类型',
        columncsharptype: 'C#类型',
        columnquerytype: '查询方式',
        columnhtmltype: '显示类型',
        columndicttype: '选择字典类型'
      },
      rules: {
        tablecomment: '请填写表描述',
        gentemplatecategory: '请选择生成模板类型',
        permsprefix: '请填写权限前缀',
        menubuttongroup: '请选择菜单权限组',
        genmodulename: '请选择模块名',
        genbusinessname: '请填写业务名',
        entityclassname: '请输入实体类名',
        dtoclassname: '请填写 Dto 类名',
        iserviceclassname: '请填写服务接口类名',
        serviceclassname: '请填写服务类名',
        controllerclassname: '请填写控制器类名',
        isrepository: '请选择是否生成仓储',
        genmethod: '请选择生成方式',
        isgentranslation: '请选择是否生成翻译',
        frontformlayout: '请选择前端表单布局',
        isusetabs: '请选择是否使用 Tabs',
        genauthor: '请填写作者',
        sorttype: '请选择排序类型',
        sortfield: '请选择排序字段',
        tablename: '请选择或输入数据表名',
        nameprefix: '请填写命名空间前缀',
        subtablename: '请选择关联父表',
        subtablefkname: '请选择关联外键',
        treecode: '请选择树编码字段',
        treename: '请选择树名称字段',
        treeparentcode: '请选择树父编码',
        genpath: '请填写生成路径',
        parentmenuid: '请选择上级菜单',
        repositoryinterfacenamespace: '请填写仓储接口命名空间',
        irepositoryclassname: '请填写仓储接口类名',
        repositorynamespace: '请填写仓储命名空间',
        repositoryclassname: '请填写仓储类名',
        tabsfieldcount: '请填写 Tabs 字段数'
      },
      validation: {
        tablenameformat: '数据表名须为小写下划线格式，如：xxx_xxx_xxx',
        nameprefixpascal: '须为帕斯卡命名，如 Takt（首字母大写，仅字母数字）',
        columnsnake: '字段配置第 {row} 行：列名须为下划线命名法（如 column_1、user_name），当前为「{value}」',
        columnpascal: '字段配置第 {row} 行：C#列名须为大驼峰命名法（如 Column1、UserName），当前为「{value}」'
      },
      column: {
        addRow: '新增行',
        delete: '删除',
        emptySaveFirst: '请先保存表配置后再管理字段',
        emptyNoData: '暂无字段数据',
        dragsort: '拖拽排序'
      }
    },
    preview: {
      loading: '正在加载预览文件列表...',
      empty: '暂无预览内容',
      emptyhint: '当前后端仅返回将生成文件路径与覆盖信息，尚未返回源码内容。',
      validationissuetitle: '模板校验发现 {count} 个问题',
      validationissuetoast: '模板校验发现 {count} 个问题，请先修复再生成',
      exists: '已存在',
      loadfail: '加载预览失败',
      pathcontent: '目标路径：{path}',
      tab: {
        backend: '后端',
        frontend: '前端',
        script: '脚本'
      },
      category: {
        backend: {
          entity: '实体 Entities',
          dto: 'DTO',
          service: '服务接口/实现',
          controller: '控制器',
          other: '其他'
        },
        frontend: {
          api: 'API',
          type: '类型定义',
          view: '列表视图',
          component: '子组件',
          other: '其他'
        },
        script: {
          translationsql: '翻译 SQL',
          menusql: '菜单 SQL',
          other: '其他'
        }
      }
    }
  }
}
