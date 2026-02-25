/**
 * 权限模块 · 中文
 * - 页面标题用 entity.permission + common.action.management 拼接
 * - required 用 common.form.placeholder.required/select + entity.permission.xxx 拼接
 */
export default {
  fields: {
    permissionCode: {
      validation: {
        format: '权限标识格式不正确，建议使用 module:resource:action 形式，如 identity:permission:list'
      }
    }
  }
}
