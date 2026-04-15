/**
 * Menu module · English
 * - Page title: entity.menu + common.action.management
 * - Required: common.form.placeholder.required/select + entity.menu.xxx; here only placeholders, format, type/status, messages
 */
export default {
  /** Format validation (required use common+entity) */
  fields: {
    menuName: {
      validation: {
        format: 'Menu name format invalid, 2-50 characters'
      }
    },
    menuCode: {
      validation: {
        format: 'Menu code format invalid, letters, numbers, underscore only, 2-50 chars'
      }
    }
  },

  /** Placeholders / hints */
  placeholder: {
    parentMenuHint: 'Select parent menu (leave empty for root)',
    l10nHint: 'For i18n'
  },

  /** Menu type options (match backend enum) */
  menuType: {
    dir: 'Directory',
    menu: 'Menu',
    button: 'Button'
  },

  /** Menu status options */
  menuStatus: {
    enable: 'Enable',
    disable: 'Disable',
    lock: 'Lock'
  },

  /** Action result messages */
  msg: {
    orderUpdated: 'Order/parent updated'
  },

}
