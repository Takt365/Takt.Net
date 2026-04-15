/**
 * 登入頁模組 · 繁體中文
 * - 結構：fields（欄位 label/placeholder/validation 統一一處）、login、forgot、sign（僅頁面級文案）
 */
export default {
  /** 欄位：label、placeholder、validation 三頁共用，避免重複 */
  fields: {
    username: {
      label: '使用者名稱',
      placeholder: '請輸入使用者名稱',
      validation: {
        required: '請輸入使用者名稱',
        min: '使用者名稱長度不能少於3位',
        max: '使用者名稱長度不能超過20位'
      }
    },
    password: {
      label: '密碼',
      placeholder: '請輸入密碼',
      validation: {
        required: '請輸入密碼',
        min: '密碼長度不能少於6位',
        max: '密碼長度不能超過50位'
      }
    },
    confirmPassword: {
      label: '確認密碼',
      placeholder: '請再次輸入密碼',
      validation: {
        required: '請再次輸入密碼',
        mismatch: '兩次輸入的密碼不一致'
      }
    },
    userEmail: {
      label: '信箱',
      placeholder: '請輸入信箱',
      placeholderForgot: '請輸入信箱地址',
      validation: {
        required: '請輸入信箱',
        format: '請輸入正確的信箱格式'
      }
    },
    realName: {
      label: '真實姓名',
      placeholder: '請輸入真實姓名',
      validation: {
        required: '請輸入真實姓名',
        max: '真實姓名長度不能超過50位'
      }
    },
    userPhone: {
      label: '手機號碼',
      placeholder: '請輸入手機號碼',
      validation: {
        required: '請輸入手機號碼',
        format: '請輸入正確的手機號碼'
      }
    },
    captcha: {
      validation: {
        required: '請完成驗證碼驗證',
        typeRequired: '驗證碼類型未返回，請檢查後端配置'
      }
    }
  },

  /** 主登入頁（僅頁面級；“忘記密碼”用 forgot.title，“註冊”用 sign.title） */
  login: {
    title: '歡迎登入',
    rememberMe: '記住我',
    login: '登入',
    logout: '登出',
    noAccountRegister: '初次使用 Takt365？立即{register}'
  },

  /** 忘記密碼頁 */
  forgot: {
    title: '忘記密碼',
    subtitle: '請輸入您的信箱地址，我們將向您發送密碼重設郵件',
    submit: '重設密碼',
    backToLogin: '返回登入',
    success: '密碼重設郵件已發送，請查收您的信箱',
    fail: '發送密碼重設郵件失敗，請檢查信箱地址是否正確',
    emailNotRegistered: '該郵件地址並沒有註冊，請確認！',
    protectedUser: '不允許找回密碼'
  },

  /** 註冊頁（僅節點內：title 即「註冊」，用於標題、按鈕及主登入 noAccountRegister 的 {register}） */
  sign: {
    title: '註冊',
    subtitle: '建立您的帳號，開始使用系統',
    hasAccount: '已有帳號？立即登入',
    success: '註冊成功，請登入',
    successInitialPassword: '註冊成功，請注意查收初始密碼。',
    fail: '註冊失敗'
  }
}
