/**
 * 登录页模块 · 中文
 * - 结构：fields（字段 label/placeholder/validation 统一一处）、login、forgot、sign（仅页面级文案）
 */
export default {
  /** 字段：label、placeholder、validation 三页复用，避免重复 */
  fields: {
    username: {
      label: '用户名',
      placeholder: '请输入用户名',
      validation: {
        required: '请输入用户名',
        min: '用户名长度不能少于3位',
        max: '用户名长度不能超过20位'
      }
    },
    password: {
      label: '密码',
      placeholder: '请输入密码',
      validation: {
        required: '请输入密码',
        min: '密码长度不能少于6位',
        max: '密码长度不能超过50位'
      }
    },
    confirmPassword: {
      label: '确认密码',
      placeholder: '请再次输入密码',
      validation: {
        required: '请再次输入密码',
        mismatch: '两次输入的密码不一致'
      }
    },
    userEmail: {
      label: '邮箱',
      placeholder: '请输入邮箱',
      placeholderForgot: '请输入邮箱地址',
      validation: {
        required: '请输入邮箱',
        format: '请输入正确的邮箱格式'
      }
    },
    realName: {
      label: '真实姓名',
      placeholder: '请输入真实姓名',
      validation: {
        required: '请输入真实姓名',
        max: '真实姓名长度不能超过50位'
      }
    },
    userPhone: {
      label: '手机号',
      placeholder: '请输入手机号',
      validation: {
        required: '请输入手机号',
        format: '请输入正确的手机号'
      }
    },
    captcha: {
      validation: {
        required: '请完成验证码验证',
        typeRequired: '验证码类型未返回，请检查后端配置'
      }
    }
  },

  /** 主登录页（仅页面级；“忘记密码”用 forgot.title，“注册”用 sign.title） */
  login: {
    title: '欢迎登录',
    rememberMe: '记住我',
    login: '登录',
    logout: '登出',
    noAccountRegister: '初次使用 Takt365？立即{register}'
  },

  /** 忘记密码页 */
  forgot: {
    title: '忘记密码',
    subtitle: '请输入您的邮箱地址，我们将向您发送密码重置邮件',
    submit: '重置密码',
    backToLogin: '返回登录',
    success: '密码重置邮件已发送，请查收您的邮箱',
    fail: '发送密码重置邮件失败，请检查邮箱地址是否正确',
    emailNotRegistered: '该邮件地址并没有注册，请确认！',
    protectedUser: '不允许找回密码'
  },

  /** 注册页（仅节点内：title 即「注册」，用于标题、按钮及主登录 noAccountRegister 的 {register}） */
  sign: {
    title: '注册',
    subtitle: '创建您的账户，开始使用系统',
    hasAccount: '已有账户？立即登录',
    success: '注册成功，请登录',
    successInitialPassword: '注册成功，请注意查收初始密码。',
    fail: '注册失败'
  }
}
