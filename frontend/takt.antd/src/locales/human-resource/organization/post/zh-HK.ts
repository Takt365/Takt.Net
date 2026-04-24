/**
 * 崗位模塊 · 繁體中文
 */
export default {
  page: {
      fields: {
        postname: {
          validation: {
            format: '崗位名稱格式不正確，2-50個字符'
          }
        },
        postcode: {
          validation: {
            format: '崗位編碼格式不正確，僅允許字母、數字、下劃線，2-30位'
          }
        }
      },
      assignuser: '分配用戶',
      msg: {
        loaduserfail: '加載崗位用戶失敗',
        postidrequired: '崗位ID不存在',
        postrequired: '崗位信息不存在'
      }
  }
}
