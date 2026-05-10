/**
 * form-create 表单统一配置：与项目 Ant Design Vue 表单风格一致
 * - 标签在上、输入框在下（vertical），与「申请人」等表单项一致
 * - 无提交/重置按钮（由业务页自行提供）
 * 所有使用 form-create 的页面应基于此配置，保证风格统一。
 */
export const FORM_CREATE_DEFAULT_OPTION = {
  submitBtn: false,
  resetBtn: false,
  form: {
    layout: 'vertical' as const
  }
} as const

export type FormCreateOptionBase = typeof FORM_CREATE_DEFAULT_OPTION
