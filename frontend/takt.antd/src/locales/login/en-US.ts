/**
 * Login page module · English
 * - Structure: fields (shared), login, forgot, sign (page-only)
 */
export default {
  /** Fields: label, placeholder, validation – shared across login / forgot / sign */
  fields: {
    username: {
      label: 'Username',
      placeholder: 'Enter username',
      validation: {
        required: 'Please enter username',
        min: 'Username must be at least 3 characters',
        max: 'Username cannot exceed 20 characters'
      }
    },
    password: {
      label: 'Password',
      placeholder: 'Enter password',
      validation: {
        required: 'Please enter password',
        min: 'Password must be at least 6 characters',
        max: 'Password cannot exceed 50 characters'
      }
    },
    confirmPassword: {
      label: 'Confirm password',
      placeholder: 'Enter password again',
      validation: {
        required: 'Please enter password again',
        mismatch: 'Passwords do not match'
      }
    },
    userEmail: {
      label: 'Email',
      placeholder: 'Enter email',
      placeholderForgot: 'Enter email address',
      validation: {
        required: 'Please enter email',
        format: 'Please enter a valid email address'
      }
    },
    realName: {
      label: 'Full name',
      placeholder: 'Enter full name',
      validation: {
        required: 'Please enter full name',
        max: 'Full name cannot exceed 50 characters'
      }
    },
    userPhone: {
      label: 'Phone',
      placeholder: 'Enter phone number',
      validation: {
        required: 'Please enter phone number',
        format: 'Please enter a valid phone number'
      }
    },
    captcha: {
      validation: {
        required: 'Please complete the captcha',
        typeRequired: 'Captcha type not returned from server, please check backend configuration'
      }
    }
  },

  /** Main login (page-only; use forgot.title for forgot link, sign.title for register) */
  login: {
    title: 'Welcome',
    rememberMe: 'Remember Me',
    login: 'Sign In',
    logout: 'Sign Out',
    noAccountRegister: 'New to Takt365? {register}'
  },

  /** Forgot password */
  forgot: {
    title: 'Forgot Password',
    subtitle: 'Enter your email address and we will send you a password reset link',
    submit: 'Reset Password',
    backToLogin: 'Back to Sign In',
    success: 'Password reset email sent. Please check your inbox',
    fail: 'Failed to send reset email. Please check your email address',
    emailNotRegistered: 'This email address is not registered. Please confirm!',
    protectedUser: 'Password recovery is not allowed for this account'
  },

  /** Sign up (title used for heading, button, and noAccountRegister {register}) */
  sign: {
    title: 'Sign up',
    subtitle: 'Create your account to get started',
    hasAccount: 'Already have an account? Sign in',
    success: 'Registration successful. Please sign in',
    successInitialPassword: 'Registration successful. Please check your email for the initial password.',
    fail: 'Registration failed'
  }
}
