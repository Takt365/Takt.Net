/**
 * 租户模块 · 中文
 * - 页面标题用 entity.tenant + common.action.management 拼接
 * - required 用 common.form.placeholder.required/select + entity.tenant.xxx 拼接；此处仅保留 format 等不可拼接的校验
 */
export default {
  page: {
      /** 本模块 format 校验文案（required 用 common+entity 拼接） */
      fields: {
        tenantname: {
          validation: {
            format: '租户名称格式不正确，2-50个字符'
          }
        },
        tenantcode: {
          validation: {
            format: '租户编码格式不正确，仅允许字母、数字、下划线，2-30位'
          }
        }
      }
  }
}
