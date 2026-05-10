/**
 * Menu module · English
 * - Page title: entity.menu + common.action.management
 * - Required: common.form.placeholder.required/select + entity.menu.xxx; here only placeholders, format, type/status, messages
 */
export default {
  page: {
      /** Format validation (required use common+entity) */
      fields: {
        menuname: {
          validation: {
            format: 'Menu name format invalid, 2-50 characters'
          }
        },
        menucode: {
          validation: {
            format: 'Menu code format invalid, letters, numbers, underscore only, 2-50 chars'
          }
        }
      },
    
      /** Placeholders / hints */
      placeholder: {
        parentmenuhint: 'Select parent menu (leave empty for root)',
        l10nhint: 'For i18n'
      },
    
      /** Menu type options (match backend enum) */
      menutype: {
        dir: 'Directory',
        menu: 'Menu',
        button: 'Button'
      },
    
      /** Menu status options */
      menustatus: {
        enable: 'Enable',
        disable: 'Disable',
        lock: 'Lock'
      },
    
      /** Action result messages */
      msg: {
        orderupdated: 'Order/parent updated'
      },
  }
}
