/**
 * Permission module · English
 * - Page title: entity.permission + common.action.management
 * - Required: common.form.placeholder.required/select + entity.permission.xxx
 */
export default {
  fields: {
    permissionCode: {
      validation: {
        format: 'Invalid permission code format, use module:resource:action e.g. identity:permission:list'
      }
    }
  }
}
