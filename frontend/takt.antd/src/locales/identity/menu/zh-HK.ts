/**
 * 菜單模塊 · 繁體中文
 */
export default {
  page: {
      fields: {
        menuname: {
          validation: {
            format: '菜單名稱格式不正確，2-50個字符'
          }
        },
        menucode: {
          validation: {
            format: '菜單編碼格式不正確，僅允許字母、數字、下劃線，2-50位'
          }
        }
      },
      placeholder: {
        parentmenuhint: '請選擇父菜單（不選為根）',
        l10nhint: '用於多語言'
      },
      menutype: {
        dir: '目錄',
        menu: '菜單',
        button: '按鈕'
      },
      menustatus: {
        enable: '啟用',
        disable: '禁用',
        lock: '鎖定'
      },
      msg: {
        orderupdated: '排序/父級已更新'
      },
  }
}
