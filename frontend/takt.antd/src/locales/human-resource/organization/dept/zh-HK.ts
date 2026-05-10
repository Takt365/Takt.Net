/**
 * 部門模塊 · 繁體中文
 */
export default {
  page: {
      fields: {
        deptname: {
          validation: {
            format: '部門名稱格式不正確，2-50個字符'
          }
        },
        deptcode: {
          validation: {
            format: '部門編碼格式不正確，僅允許字母、數字、下劃線，2-30位'
          }
        }
      }
  }
}
