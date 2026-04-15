/**
 * Department module · English
 * - Page title: entity.dept + common.action.management
 * - Required: common.form.placeholder.required/select + entity.dept.xxx; here only format etc.
 */
export default {
  /** Format validation (required use common+entity) */
  fields: {
    deptName: {
      validation: {
        format: 'Department name format invalid, 2-50 characters'
      }
    },
    deptCode: {
      validation: {
        format: 'Department code format invalid, letters, numbers, underscore only, 2-30 chars'
      }
    }
  }
}
