/**
 * 代碼生成 · 繁體中文（香港）
 * 約定：`export default { page: { … } }`，運行時鍵為 code.generator.page.*。
 * 列名等：entity.gentable.*；通用：common.*
 */
export default {
  page: {
    importfromdb: '從資料庫匯入表',
    saveas: '另存為',
    saveaspathhint: '請輸入生成路徑（將覆蓋表配置中的路徑）：',
    saveaspathplaceholder: '如：D:\\Projects\\Takt.Net',
    overwriteconfirmtitle: '覆蓋確認',
    overwriteconfirmcontent: '目標路徑下已存在以下檔案，是否覆蓋？',
    overwrite: '覆蓋',
    saveascancel: '另存為',
    notableidsync: '此記錄無表 ID，無法同步',
    notableidinit: '此記錄無表 ID，無法初始化',
    syncformhint: '請在編輯對話框中儲存以從資料來源刷新欄位配置；完整同步需後端提供介面',
    clonesuccess: '已複製為新建，請修改表名後儲存',
    nodataexport: '暫無資料可匯出',
    codegenerateddownload: '代碼已生成並下載',
    gensuccesscount: '，共 {count} 個檔案',
    existingfilessuffix: '等共 {count} 個檔案',
    notableidpreview: '此記錄無表 ID，無法預覽',
    importtable: {
      datatable: '資料表'
    },
    form: {
      tab: {
        table: '表配置',
        column: '欄位配置',
        basic: '基本資訊',
        business: '業務模組',
        entitydto: '實體與傳輸物件',
        service: '服務與控制器',
        generate: '生成',
        front: '前端與樣式'
      },
      placeholder: {
        configid: '請選擇資料來源',
        tablenamenew: '小寫底線格式，如：xxx_xxx_xxx',
        tablenameedit: '請先選擇資料來源',
        tablecomment: '表註解',
        gentemplatecategory: '請選擇生成模板類型',
        indatabase: '請選擇是否庫表',
        subtablename: '請選擇父表',
        subtablefkname: '請選擇外鍵欄',
        treecode: '請選擇樹編碼欄',
        treeparentcode: '請選擇樹父編碼欄',
        treename: '請選擇樹名稱欄',
        nameprefix: '專案名稱，預設 Takt，修改後所有命名空間同步',
        permsprefix: '由模組名+業務名自動生成，如 accounting:controlling:standard:wage:rate',
        genmodulename: '請選擇模組（目錄）或手動輸入，如：Generator、HumanResource.Organization',
        genbusinessnamefromtable: '由表名自動生成',
        genbusinessnamemanual: '用於生成實體/服務/控制器等類別名稱及介面註解，如：設定、部門',
        genfunctionname: '由表描述自動帶出，唯讀',
        autofrommodule: '由模組名自動生成',
        autofrombusiness: '由業務名自動生成',
        genmethod: '請選擇生成方式',
        genpath: '/',
        currentprojectloading: '正在取得…',
        currentprojectidle: '請選擇生成方式為當前專案後自動取得',
        parentmenuid: '請選擇上層選單（不選為根）',
        sorttype: '請選擇排序類型',
        sortfield: '請選擇排序欄位',
        frontui: '請選擇前端UI框架',
        frontformlayout: '請選擇前端表單佈局',
        frontbtnstyle: '請選擇前端按鈕樣式',
        genauthor: '當前登入使用者',
        columndbtype: 'DB類型',
        columncsharptype: 'C#類型',
        columnquerytype: '查詢方式',
        columnhtmltype: '顯示類型',
        columndicttype: '選擇字典類型'
      },
      rules: {
        tablecomment: '請填寫表描述',
        gentemplatecategory: '請選擇生成模板類型',
        permsprefix: '請填寫權限前綴',
        menubuttongroup: '請選擇選單權限組',
        genmodulename: '請選擇模組名',
        genbusinessname: '請填寫業務名',
        entityclassname: '請輸入實體類別名稱',
        dtoclassname: '請填寫 Dto 類別名稱',
        iserviceclassname: '請填寫服務介面類別名稱',
        serviceclassname: '請填寫服務類別名稱',
        controllerclassname: '請填寫控制器類別名稱',
        isrepository: '請選擇是否生成儲存庫',
        genmethod: '請選擇生成方式',
        isgentranslation: '請選擇是否生成翻譯',
        frontformlayout: '請選擇前端表單佈局',
        isusetabs: '請選擇是否使用 Tabs',
        genauthor: '請填寫作者',
        sorttype: '請選擇排序類型',
        sortfield: '請選擇排序欄位',
        tablename: '請選擇或輸入資料表名',
        nameprefix: '請填寫命名空間前綴',
        subtablename: '請選擇關聯父表',
        subtablefkname: '請選擇關聯外鍵',
        treecode: '請選擇樹編碼欄位',
        treename: '請選擇樹名稱欄位',
        treeparentcode: '請選擇樹父編碼',
        genpath: '請填寫生成路徑',
        parentmenuid: '請選擇上層選單',
        repositoryinterfacenamespace: '請填寫儲存庫介面命名空間',
        irepositoryclassname: '請填寫儲存庫介面類別名稱',
        repositorynamespace: '請填寫儲存庫命名空間',
        repositoryclassname: '請填寫儲存庫類別名稱',
        tabsfieldcount: '請填寫 Tabs 欄位數'
      },
      validation: {
        tablenameformat: '資料表名須為小寫底線格式，如：xxx_xxx_xxx',
        nameprefixpascal: '須為帕斯卡命名，如 Takt（首字母大寫，僅字母數字）',
        columnsnake: '欄位配置第 {row} 行：欄位名須為底線命名法（如 column_1、user_name），目前為「{value}」',
        columnpascal: '欄位配置第 {row} 行：C#欄位名須為大駝峰命名法（如 Column1、UserName），目前為「{value}」'
      },
      column: {
        addRow: '新增行',
        delete: '刪除',
        emptySaveFirst: '請先儲存表配置後再管理欄位',
        emptyNoData: '暫無欄位資料',
        dragsort: '拖動排序'
      }
    },
    preview: {
      loading: '正在載入預覽檔案列表...',
      empty: '暫無預覽內容',
      emptyhint: '當前後端僅回傳將生成檔案路徑與覆蓋資訊，尚未回傳原始碼內容。',
      validationissuetitle: '模板校驗發現 {count} 個問題',
      validationissuetoast: '模板校驗發現 {count} 個問題，請先修復後再生成',
      exists: '已存在',
      loadfail: '載入預覽失敗',
      pathcontent: '目標路徑：{path}',
      tab: {
        backend: '後端',
        frontend: '前端',
        script: '腳本'
      },
      category: {
        backend: {
          entity: '實體 Entities',
          dto: 'DTO',
          service: '服務介面/實作',
          controller: '控制器',
          other: '其他'
        },
        frontend: {
          api: 'API',
          type: '型別定義',
          view: '清單檢視',
          component: '子元件',
          other: '其他'
        },
        script: {
          translationsql: '翻譯 SQL',
          menusql: '選單 SQL',
          other: '其他'
        }
      }
    }
  }
}
