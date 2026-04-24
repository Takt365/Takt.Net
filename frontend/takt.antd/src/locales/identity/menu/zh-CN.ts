/**
 * 菜单模块 · 中文
 * - 页面标题用 entity.menu + common.action.management 拼接
 * - required 用 common.form.placeholder.required/select + entity.menu.xxx 拼接；此处仅保留占位、format、类型/状态选项、消息
 */
export default {
  page: {
      /** 本模块 format 校验文案（required 用 common+entity 拼接） */
      fields: {
        menuname: {
          validation: {
            format: '菜单名称格式不正确，2-50个字符'
          }
        },
        menucode: {
          validation: {
            format: '菜单编码格式不正确，仅允许字母、数字、下划线，2-50位'
          }
        }
      },
    
      /** 占位符/提示 */
      placeholder: {
        parentmenuhint: '请选择父菜单（不选为根）',
        l10nhint: '用于多语言'
      },
    
      /** 菜单类型选项（与后端枚举对应） */
      menutype: {
        dir: '目录',
        menu: '菜单',
        button: '按钮'
      },
    
      /** 菜单状态选项 */
      menustatus: {
        enable: '启用',
        disable: '禁用',
        lock: '锁定'
      },
    
      /** 操作结果消息 */
      msg: {
        orderupdated: '排序/父级已更新'
      },
  }
}
