/**
 * Tenant module · English
 * - Page title: entity.tenant + common.action.management
 * - Required: common.form.placeholder.required/select + entity.tenant.xxx; here only format etc.
 */
export default {
  /** Format validation (required use common+entity) */
  fields: {
    tenantName: {
      validation: {
        format: 'Tenant name format invalid, 2-50 characters'
      }
    },
    tenantCode: {
      validation: {
        format: 'Tenant code format invalid, letters, numbers, underscore only, 2-30 chars'
      }
    }
  }
}
