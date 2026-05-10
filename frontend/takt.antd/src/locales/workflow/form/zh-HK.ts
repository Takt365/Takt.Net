/**
 * 流程表單 · 繁體中文
 */
export default {
  page: {
      flowselectorlabel: '流程',
      flowselectorplaceholder: '請選擇流程',
      linkflowoptionlink: '關聯流程',
      linkflowoptionnew: '新建流程',
      linkedschemes: '關聯的流程',
      noschemehint: '暫無流程方案，請到「流程方案」中新建流程。',
      linkedschemesnewhint: '請先保存表單並填寫表單編碼，再到「流程方案」中設計流程並選擇本表單。',
      godesignflow: '去設計流程',
      category: {
        general: '通用表單',
        business: '業務表單',
        system: '系統表單'
      },
      type: {
        dynamic: '動態表單（拖拽設計）',
        static: '靜態表單（模板渲染）',
        custom: '自定義表單（自定義頁面）'
      },
      versionplaceholder: '如 v1.0.0',
      datasourceplaceholder: '請選擇數據源（數據庫）',
      datatableplaceholder: '請選擇數據表',
      formfieldplaceholder: '請選擇要顯示在表單中的字段',
      entitytableplaceholder: '請先選擇數據源，再選擇數據表',
      entitytablehint: '選中列出所有數據列項，以便第四步還原表單',
      batchdelete: '批量刪除',
      loaddetailfailed: '加載表單詳情失敗',
      step: {
        forminfo: '表單信息',
        datasource: '數據源',
        datatablelist: '數據表清單',
        formdesign: '表單設計',
        linkedflow: '關聯流程/流程設計',
        basicinfo: '基本信息',
        formdesignedit: '表單設計（編輯）',
        next: '下一步',
        prev: '上一步',
        done: '完成',
        completerequired: '請完成所有步驟並通過校驗後再提交',
        validatefail: '請先完成第 {step} 步'
      }
  }
}
