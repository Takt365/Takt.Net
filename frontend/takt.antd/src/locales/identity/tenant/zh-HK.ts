/**
 * 租戶模塊 · 繁體中文
 */
export default {
  page: {
      fields: {
        tenantname: {
          validation: {
            format: '租戶名稱格式不正確，2-50個字符'
          }
        },
        tenantcode: {
          validation: {
            format: '租戶編碼格式不正確，僅允許字母、數字、下劃線，2-30位'
          }
        }
      }
  }
}
