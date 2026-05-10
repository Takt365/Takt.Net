/**
 * 用户模块 · 中文
 * - 页面标题用 entity.user + common.action.management 拼接
 * - 此处仅保留：个人信息/修改密码、Tab、校验文案等无法拼接的
 */
export default {
  page: {
      profile: '个人信息',
      changepasswordtitle: '修改密码',
    
      /** 密码相关（修改密码弹窗） */
      password: {
        old: {
          label: '旧密码',
          placeholder: '请输入旧密码'
        },
        new: {
          label: '新密码',
          placeholder: '请输入新密码',
          validation: {
            format: '密码必须为8-20位，且包含字母和数字'
          }
        },
        confirm: {
          label: '确认新密码',
          placeholder: '请再次输入新密码',
          validation: {
            mismatch: '两次输入的密码不一致'
          }
        }
      },
    
      /** Tab 标签（用户表单内 Tab） */
      tabs: {
        employeeinfo: '员工信息',
        userinfo: '用户信息',
        basicinfo: '基本资料',
        accountandremark: '账号与备注',
        permission: '权限',
        avatar: '头像设置'
      },
    
      /** 本模块专用字段/标签（实体字段用 entity.user.xxx） */
      fields: {
        employee: {
          label: '关联员工'
        },
        employeesnapshot: {
          hint: '选择后下方展示该员工在下拉选项中的姓名与员工编码（来自人事 options 接口）。'
        },
        employeeoptionmissing: '已绑定员工，但当前选项列表中未找到该项（可能未加载完成或员工已非在职可选范围）。',
        employeelink: {
          label: '人事档案',
          existing: '已有档案',
          createnew: '新建档案',
          createnewhint: '保存用户时将先调用员工创建接口建立人事档案，再创建用户并关联该员工（与后端 CreateUser 须先有 EmployeeId 一致）。'
        },
        employeeid: {
          placeholder: '请从员工选项列表选择关联员工（必填）'
        },
        /** 用户表昵称输入框占位（选填） */
        nicknameplaceholder: '选填；展示用，与员工档案姓名无关',
        usernamewhennewemployee: '新建人事档案时，登录名将使用保存后系统生成的员工编码，此处无需填写（已禁用）。',
        password: {
          label: '密码'
        },
        formref: {
          label: '表单引用'
        },
        username: {
          validation: {
            format: '用户名必须以小写字母开头，允许小写字母和数字，不允许特殊符号，4-20位'
          }
        },
        realname: {
          validation: {
            format: '真实姓名必须为2-10个汉字'
          }
        },
        lastname: {
          validation: {
            format: '姓仅允许英文与数字，首字母必须大写，数字不能在首位，1-100位'
          }
        },
        firstname: {
          validation: {
            format: '名仅允许英文与数字，首字母必须大写，数字不能在首位，1-100位'
          }
        },
        nickname: {
          validation: {
            format: '昵称格式不正确：允许中文、英文、数字、下划线、横线、点，1-200 位（与登录用户表校验一致）'
          }
        }
      }
  }
}
