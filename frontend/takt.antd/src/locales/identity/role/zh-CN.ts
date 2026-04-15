/**
 * 角色模块 · 中文
 * - 页面标题用 entity.role + common.action.management 拼接
 * - required 用 common.form.placeholder.required/select + entity.role.xxx 拼接；此处仅保留 format、占位、数据范围等
 */
export default {
  /** 本模块 format 校验文案（required 用 common+entity 拼接） */
  fields: {
    name: {
      validation: {
        format: '角色名称格式不正确，2-50个字符'
      }
    },
    code: {
      validation: {
        format: '角色编码格式不正确，仅允许字母、数字、下划线，2-30位'
      }
    }
  },

  /** 占位符/提示 */
  placeholder: {
    customScopeHint: '数据范围为自定义时填写部门ID'
  },

  /** 数据范围选项（与字典/后端枚举对应） */
  dataScope: {
    all: '全部数据',
    dept: '本部门数据',
    deptAndBelow: '本部门及以下数据',
    self: '仅本人数据',
    custom: '自定义数据范围'
  }
}
