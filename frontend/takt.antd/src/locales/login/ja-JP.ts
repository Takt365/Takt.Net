/**
 * ログインページ · 日本語
 * - 構造: fields, login, forgot, sign (zh-CNと同一)
 */
export default {
  /** フィールド: label, placeholder, validation — 3ページ共通 */
  fields: {
    username: {
      label: 'ユーザー名',
      placeholder: 'ユーザー名を入力',
      validation: {
        required: 'ユーザー名を入力してください',
        min: 'ユーザー名は3文字以上',
        max: 'ユーザー名は20文字以内'
      }
    },
    password: {
      label: 'パスワード',
      placeholder: 'パスワードを入力',
      validation: {
        required: 'パスワードを入力してください',
        min: 'パスワードは6文字以上',
        max: 'パスワードは50文字以内'
      }
    },
    confirmPassword: {
      label: 'パスワード確認',
      placeholder: 'パスワードを再入力',
      validation: {
        required: 'パスワードを再入力してください',
        mismatch: 'パスワードが一致しません'
      }
    },
    userEmail: {
      label: 'メールアドレス',
      placeholder: 'メールアドレスを入力',
      placeholderForgot: 'メールアドレスを入力',
      validation: {
        required: 'メールアドレスを入力してください',
        format: '有効なメールアドレスを入力してください'
      }
    },
    realName: {
      label: '氏名',
      placeholder: '氏名を入力',
      validation: {
        required: '氏名を入力してください',
        max: '氏名は50文字以内'
      }
    },
    userPhone: {
      label: '電話番号',
      placeholder: '電話番号を入力',
      validation: {
        required: '電話番号を入力してください',
        format: '有効な電話番号を入力してください'
      }
    },
    captcha: {
      validation: {
        required: '認証を完了してください',
        typeRequired: 'サーバーから認証タイプが返されませんでした。バックエンド設定を確認してください'
      }
    }
  },

  /** メインログイン（パスワード忘れは forgot.title、新規登録は sign.title） */
  login: {
    title: 'ようこそ',
    rememberMe: 'ログイン状態を保持',
    login: 'サインイン',
    logout: 'サインアウト',
    noAccountRegister: 'Takt365は初めてですか？{register}'
  },

  /** パスワードリセット */
  forgot: {
    title: 'パスワードをお忘れですか',
    subtitle: 'メールアドレスを入力すると、パスワードリセット用のリンクをお送りします',
    submit: 'パスワードをリセット',
    backToLogin: 'ログインに戻る',
    success: 'リセット用メールを送信しました。受信トレイをご確認ください',
    fail: '送信に失敗しました。メールアドレスをご確認ください',
    emailNotRegistered: 'このメールアドレスは登録されていません。ご確認ください',
    protectedUser: 'このアカウントではパスワードの再取得はできません'
  },

  /** 新規登録（title = 見出し・ボタン・noAccountRegisterの{register}） */
  sign: {
    title: '新規登録',
    subtitle: 'アカウントを作成してご利用を開始',
    hasAccount: 'アカウントをお持ちですか？ログイン',
    success: '登録完了。ログインしてください',
    successInitialPassword: '登録が完了しました。初期パスワードのメールをご確認ください。',
    fail: '登録に失敗しました'
  }
}
