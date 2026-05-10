/**
 * Post module · English
 * - Page title: entity.post + common.action.management
 * - Required: common.form.placeholder.required/select + entity.post.xxx; here only format and non-composable
 */
export default {
  page: {
      /** Format validation (required use common+entity) */
      fields: {
        postname: {
          validation: {
            format: 'Post name format invalid, 2-50 characters'
          }
        },
        postcode: {
          validation: {
            format: 'Post code format invalid, letters, numbers, underscore only, 2-30 chars'
          }
        }
      },
    
      assignuser: 'Assign Users',
    
      msg: {
        loaduserfail: 'Failed to load post users',
        postidrequired: 'Post ID is required',
        postrequired: 'Post information is required'
      }
  }
}
