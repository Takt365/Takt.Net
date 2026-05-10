/**
 * 角色模塊 · 繁體中文
 */
export default {
  page: {
      fields: {
        name: {
          validation: {
            format: '角色名稱格式不正確，2-50個字符'
          }
        },
        code: {
          validation: {
            format: '角色編碼格式不正確，僅允許字母、數字、下劃線，2-30位'
          }
        }
      },
      placeholder: {
        customscopehint: '數據範圍為自定義時填寫部門ID'
      },
      datascope: {
        all: '全部數據',
        dept: '本部門數據',
        deptandbelow: '本部門及以下數據',
        self: '僅本人數據',
        custom: '自定義數據範圍'
      }
  }
}
