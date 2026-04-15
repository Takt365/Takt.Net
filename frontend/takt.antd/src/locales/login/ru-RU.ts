/**
 * Страница входа · Русский
 * - Структура: fields, login, forgot, sign (как в zh-CN)
 */
export default {
  /** Поля: label, placeholder, validation — общие для трёх страниц */
  fields: {
    username: {
      label: 'Имя пользователя',
      placeholder: 'Введите имя пользователя',
      validation: {
        required: 'Введите имя пользователя',
        min: 'Не менее 3 символов',
        max: 'Не более 20 символов'
      }
    },
    password: {
      label: 'Пароль',
      placeholder: 'Введите пароль',
      validation: {
        required: 'Введите пароль',
        min: 'Пароль не менее 6 символов',
        max: 'Пароль не более 50 символов'
      }
    },
    confirmPassword: {
      label: 'Подтверждение пароля',
      placeholder: 'Введите пароль ещё раз',
      validation: {
        required: 'Введите пароль ещё раз',
        mismatch: 'Пароли не совпадают'
      }
    },
    userEmail: {
      label: 'Email',
      placeholder: 'Введите email',
      placeholderForgot: 'Введите адрес email',
      validation: {
        required: 'Введите email',
        format: 'Введите корректный email'
      }
    },
    realName: {
      label: 'Полное имя',
      placeholder: 'Введите полное имя',
      validation: {
        required: 'Введите полное имя',
        max: 'Не более 50 символов'
      }
    },
    userPhone: {
      label: 'Телефон',
      placeholder: 'Введите номер телефона',
      validation: {
        required: 'Введите номер телефона',
        format: 'Введите корректный номер телефона'
      }
    },
    captcha: {
      validation: {
        required: 'Пройдите проверку',
        typeRequired: 'Сервер не вернул тип капчи, проверьте настройки бэкенда'
      }
    }
  },

  /** Главная страница входа */
  login: {
    title: 'Добро пожаловать',
    rememberMe: 'Запомнить меня',
    login: 'Войти',
    logout: 'Выйти',
    noAccountRegister: 'Впервые в Takt365? {register}'
  },

  /** Забыли пароль */
  forgot: {
    title: 'Забыли пароль',
    subtitle: 'Введите email, мы отправим ссылку для сброса пароля',
    submit: 'Сбросить пароль',
    backToLogin: 'Вернуться к входу',
    success: 'Письмо отправлено. Проверьте почту',
    fail: 'Не удалось отправить письмо. Проверьте email',
    emailNotRegistered: 'Этот email не зарегистрирован. Подтвердите, пожалуйста',
    protectedUser: 'Восстановление пароля для этой учётной записи не разрешено'
  },

  /** Регистрация (title — заголовок, кнопка и {register} в noAccountRegister) */
  sign: {
    title: 'Регистрация',
    subtitle: 'Создайте аккаунт для начала работы',
    hasAccount: 'Уже есть аккаунт? Войти',
    success: 'Регистрация успешна. Войдите в систему',
    successInitialPassword: 'Регистрация успешна. Проверьте почту для получения начального пароля.',
    fail: 'Ошибка регистрации'
  }
}
