/**
 * User module · English
 * - Page title: entity.user + common.action.management
 * - Here: only profile, change password, tabs, validation messages
 */
export default {
  profile: 'Profile',
  changePasswordTitle: 'Change Password',

  /** Password (change password dialog) */
  password: {
    old: {
      label: 'Old password',
      placeholder: 'Enter old password',
      validation: {
        required: 'Old password is required'
      }
    },
    new: {
      label: 'New password',
      placeholder: 'Enter new password',
      validation: {
        required: 'New password is required',
        format: 'Password must be 8-20 characters with letters and numbers'
      }
    },
    confirm: {
      label: 'Confirm new password',
      placeholder: 'Enter new password again',
      validation: {
        required: 'Confirm password is required',
        mismatch: 'Passwords do not match'
      }
    }
  },

  /** Tab labels (user form tabs) */
  tabs: {
    employeeInfo: 'Employee',
    userInfo: 'User',
    basicInfo: 'Basic Info',
    accountAndRemark: 'Account & Remark',
    permission: 'Permission',
    avatar: 'Avatar'
  },

  /** Module-specific fields (entity fields use entity.user.xxx) */
  fields: {
    employee: {
      label: 'Associated Employee'
    },
    employeeSnapshot: {
      hint: 'After selection, name and employee code from the HR options list are shown below.'
    },
    employeeOptionMissing: 'An employee is bound, but it was not found in the current options list (still loading or employee not in the selectable on-job set).',
    employeeLink: {
      label: 'HR profile',
      existing: 'Existing',
      createNew: 'Create new',
      createNewHint: 'Saving will create the employee record first, then the user (backend requires an EmployeeId before creating a user).'
    },
    employeeId: {
      placeholder: 'Select an employee from the option list (required)'
    },
    nicknamePlaceholder: 'Optional; display nickname on the user record, independent of HR employee name',
    userNameWhenNewEmployee: 'When creating a new HR profile, the login name will be the system-generated employee code; this field is disabled.',
    password: {
      label: 'Password'
    },
    formRef: {
      label: 'Form reference'
    },
    userName: {
      validation: {
        format: 'Username must start with lowercase letter, 4-20 chars, letters and numbers only'
      }
    },
    realName: {
      validation: {
        format: 'Real name must be 2-10 Chinese characters'
      }
    },
    lastName: {
      validation: {
        format: 'Last name: English and digits only, first letter uppercase, digits not at start, 1-100 chars'
      }
    },
    firstName: {
      validation: {
        format: 'First name: English and digits only, first letter uppercase, digits not at start, 1-100 chars'
      }
    },
    nickName: {
      validation: {
        format: 'Invalid nickname: Chinese, English, digits, underscore, hyphen, dot, 1-200 characters (same as user table validation).'
      }
    }
  }
}
