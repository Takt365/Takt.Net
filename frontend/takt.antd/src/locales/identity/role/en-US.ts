/**
 * Role module · English
 * - Page title: entity.role + common.action.management
 * - Required: common.form.placeholder.required/select + entity.role.xxx; here only format, placeholders, data scope
 */
export default {
  page: {
      /** Format validation (required use common+entity) */
      fields: {
        name: {
          validation: {
            format: 'Role name format invalid, 2-50 characters'
          }
        },
        code: {
          validation: {
            format: 'Role code format invalid, letters, numbers, underscore only, 2-30 chars'
          }
        }
      },
    
      /** Placeholders / hints */
      placeholder: {
        customscopehint: 'Enter department ID(s) when data scope is custom'
      },
    
      /** Data scope options (match backend enum) */
      datascope: {
        all: 'All data',
        dept: 'Department data',
        deptandbelow: 'Department and below',
        self: 'Self only',
        custom: 'Custom data scope'
      }
  }
}
