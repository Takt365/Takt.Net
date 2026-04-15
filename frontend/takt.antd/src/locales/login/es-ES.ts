/**
 * Página de inicio de sesión · Español
 * - Estructura: fields, login, forgot, sign (igual que zh-CN)
 */
export default {
  /** Campos: label, placeholder, validation — compartidos por las tres páginas */
  fields: {
    username: {
      label: 'Usuario',
      placeholder: 'Introduzca el usuario',
      validation: {
        required: 'Introduzca el usuario',
        min: 'Mínimo 3 caracteres',
        max: 'Máximo 20 caracteres'
      }
    },
    password: {
      label: 'Contraseña',
      placeholder: 'Introduzca la contraseña',
      validation: {
        required: 'Introduzca la contraseña',
        min: 'Mínimo 6 caracteres',
        max: 'Máximo 50 caracteres'
      }
    },
    confirmPassword: {
      label: 'Confirmar contraseña',
      placeholder: 'Introduzca la contraseña de nuevo',
      validation: {
        required: 'Introduzca la contraseña de nuevo',
        mismatch: 'Las contraseñas no coinciden'
      }
    },
    userEmail: {
      label: 'Correo electrónico',
      placeholder: 'Introduzca el correo',
      placeholderForgot: 'Introduzca su dirección de correo',
      validation: {
        required: 'Introduzca el correo',
        format: 'Introduzca un correo válido'
      }
    },
    realName: {
      label: 'Nombre completo',
      placeholder: 'Introduzca el nombre completo',
      validation: {
        required: 'Introduzca el nombre completo',
        max: 'Máximo 50 caracteres'
      }
    },
    userPhone: {
      label: 'Teléfono',
      placeholder: 'Introduzca el número de teléfono',
      validation: {
        required: 'Introduzca el número de teléfono',
        format: 'Introduzca un teléfono válido'
      }
    },
    captcha: {
      validation: {
        required: 'Complete la verificación',
        typeRequired: 'El servidor no devolvió el tipo de captcha, compruebe la configuración del backend'
      }
    }
  },

  /** Página principal de inicio de sesión */
  login: {
    title: 'Bienvenido',
    rememberMe: 'Recordarme',
    login: 'Iniciar sesión',
    logout: 'Cerrar sesión',
    noAccountRegister: '¿Nuevo en Takt365? {register}'
  },

  /** Olvidó contraseña */
  forgot: {
    title: 'Olvidó su contraseña',
    subtitle: 'Introduzca su correo y le enviaremos un enlace para restablecerla',
    submit: 'Restablecer contraseña',
    backToLogin: 'Volver a iniciar sesión',
    success: 'Correo enviado. Revise su bandeja de entrada',
    fail: 'Error al enviar. Verifique su correo',
    emailNotRegistered: 'Este correo no está registrado. Por favor, confirme',
    protectedUser: 'No se permite recuperar la contraseña para esta cuenta'
  },
  
  /** Registro (title = título·botón·{register} en noAccountRegister) */
  sign: {
    title: 'Registrarse',
    subtitle: 'Cree su cuenta para comenzar',
    hasAccount: '¿Ya tiene cuenta? Iniciar sesión',
    success: 'Registro correcto. Inicie sesión',
    successInitialPassword: 'Registro correcto. Revise su correo para la contraseña inicial.',
    fail: 'Error en el registro'
  }
}
