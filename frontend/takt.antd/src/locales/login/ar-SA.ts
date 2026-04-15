/**
 * صفحة تسجيل الدخول · العربية
 * - الهيكل: fields, login, forgot, sign (مطابق zh-CN)
 */
export default {
  /** الحقول: label, placeholder, validation — مشتركة بين الصفحات الثلاث */
  fields: {
    username: {
      label: 'اسم المستخدم',
      placeholder: 'أدخل اسم المستخدم',
      validation: {
        required: 'أدخل اسم المستخدم',
        min: '3 أحرف على الأقل',
        max: '20 حرفًا كحد أقصى'
      }
    },
    password: {
      label: 'كلمة المرور',
      placeholder: 'أدخل كلمة المرور',
      validation: {
        required: 'أدخل كلمة المرور',
        min: '6 أحرف على الأقل',
        max: '50 حرفًا كحد أقصى'
      }
    },
    confirmPassword: {
      label: 'تأكيد كلمة المرور',
      placeholder: 'أدخل كلمة المرور مرة أخرى',
      validation: {
        required: 'أدخل كلمة المرور مرة أخرى',
        mismatch: 'كلمات المرور غير متطابقة'
      }
    },
    userEmail: {
      label: 'البريد الإلكتروني',
      placeholder: 'أدخل البريد الإلكتروني',
      placeholderForgot: 'أدخل عنوان البريد الإلكتروني',
      validation: {
        required: 'أدخل البريد الإلكتروني',
        format: 'أدخل بريدًا إلكترونيًا صالحًا'
      }
    },
    realName: {
      label: 'الاسم الكامل',
      placeholder: 'أدخل الاسم الكامل',
      validation: {
        required: 'أدخل الاسم الكامل',
        max: '50 حرفًا كحد أقصى'
      }
    },
    userPhone: {
      label: 'رقم الهاتف',
      placeholder: 'أدخل رقم الهاتف',
      validation: {
        required: 'أدخل رقم الهاتف',
        format: 'أدخل رقم هاتف صالح'
      }
    },
    captcha: {
      validation: {
        required: 'أكمل التحقق',
        typeRequired: 'لم يُرجع الخادم نوع التحقق، يرجى التحقق من إعدادات الخلفية'
      }
    }
  },

  /** الصفحة الرئيسية لتسجيل الدخول */
  login: {
    title: 'تسجيل الدخول',
    rememberMe: 'تذكرني',
    login: 'تسجيل الدخول',
    logout: 'تسجيل الخروج',
    noAccountRegister: 'جديد على Takt365؟ {register}'
  },

  /** نسيت كلمة المرور */
  forgot: {
    title: 'نسيت كلمة المرور',
    subtitle: 'أدخل بريدك وسنرسل لك رابط إعادة التعيين',
    submit: 'إعادة تعيين كلمة المرور',
    backToLogin: 'العودة لتسجيل الدخول',
    success: 'تم إرسال البريد. تحقق من صندوق الوارد',
    fail: 'فشل الإرسال. تحقق من البريد',
    emailNotRegistered: 'هذا البريد غير مسجل. يرجى التأكد',
    protectedUser: 'استعادة كلمة المرور غير مسموحة لهذا الحساب'
  },

  /** التسجيل (title = العنوان·الزر·{register} في noAccountRegister) */
  sign: {
    title: 'تسجيل حساب',
    subtitle: 'أنشئ حسابك للبدء',
    hasAccount: 'لديك حساب؟ تسجيل الدخول',
    success: 'تم التسجيل. سجّل الدخول',
    successInitialPassword: 'تم التسجيل بنجاح. يرجى التحقق من بريدك لاستلام كلمة المرور الأولية.',
    fail: 'فشل التسجيل'
  }
}
