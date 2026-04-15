/**
 * Post module · English
 * - Page title: entity.post + common.action.management
 * - Required: common.form.placeholder.required/select + entity.post.xxx; here only format and non-composable
 */
export default {
  /** Format validation (required use common+entity) */
  fields: {
    postName: {
      validation: {
        format: 'Post name format invalid, 2-50 characters'
      }
    },
    postCode: {
      validation: {
        format: 'Post code format invalid, letters, numbers, underscore only, 2-30 chars'
      }
    }
  },

  assignUser: 'Assign Users',

  msg: {
    loadUserFail: 'Failed to load post users',
    postIdRequired: 'Post ID is required',
    postRequired: 'Post information is required'
  }
}
