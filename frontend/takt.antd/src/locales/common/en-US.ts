/**
 * Common translations · English
 * Same structure and key order as zh-CN (alphabetical for button)
 */
export default {
  page: {
    /** <summary>1. app</summary> */
      app: {
        htmltitle: 'Takt Digital Factory',
        name: 'Takt Digital Factory (TDF) ',
        productcode: 'TDF-MES-PRO',
        slogan: 'Driving the Future',
        tagline: 'Practical·Simple·Flexible'
      },
    
      /** <summary>2. settings</summary> */
      settings: {
        color: {
          blue: 'Blue',
          brown: 'Brown',
          cyan: 'Cyan',
          gray: 'Gray',
          green: 'Green',
          indigo: 'Indigo',
          orange: 'Orange',
          pink: 'Pink',
          purple: 'Purple',
          red: 'Red',
          yellow: 'Yellow',
          switch: 'Switch Color',
          title: 'Color'
        },
        locale: {
          'ar-sa': 'العربية',
          'en-us': 'English',
          'es-es': 'Español',
          'fr-fr': 'Français',
          'ja-jp': 'Japanese',
          'ko-kr': 'Korean',
          'ru-ru': 'Russian',
          'zh-cn': 'Chinese (Simplified)',
          'zh-tw': 'Chinese (Traditional)',
          switch: 'Switch Language',
          title: 'Language'
        },
        theme: {
          dark: 'Dark',
          light: 'Light',
          switch: 'Switch Theme',
          title: 'Theme'
        }
      },
    
      /** Layout position */
      layout: {
        position: {
          left: 'Left',
          center: 'Center',
          right: 'Right'
        }
      },
    
      /** <summary>3. button</summary> Alphabetical order as zh-CN */
      button: {
        addsign: 'Add Sign',
        advancedoptions: 'Advanced options',
        advancedquery: 'Advanced query',
        advancedsettings: 'Advanced settings',
        allocate: 'Allocate',
        approve: 'Approve',
        archive: 'Archive',
        back: 'Back',
        cancel: 'Cancel',
        changepwd: 'Change Password',
        change: 'Change',
        checkall: 'Check All',
        clean: 'Clean',
        clone: 'Clone',
        collapse: 'Collapse',
        columnsetting: 'Column Setting',
        comment: 'Comment',
        config: 'Config',
        confirm: 'Confirm',
        copy: 'Copy',
        countersign: 'Countersign',
        create: 'Create',
        createrow: 'Add Row',
        datasource: 'Data Source',
        delegate: 'Delegate',
        delete: 'Delete',
        design: 'Design',
        detail: 'Detail',
        disable: 'Disable',
        download: 'Download',
        draft: 'Draft',
        edit: 'Edit',
        enable: 'Enable',
        empty: 'Empty',
        empty30d: 'Clear 30 Days',
        empty7d: 'Clear 7 Days',
        emptyall: 'Clear All',
        expand: 'Expand',
        exitfullscreen: 'Exit Fullscreen',
        export: 'Export',
        favorite: 'Favorite',
        field: 'Field Management',
        fixed: 'Fixed',
        forward: 'Forward',
        formdata: 'Form Data',
        fullscreen: 'Fullscreen',
        generate: 'Generate',
        history: 'History',
        import: 'Import',
        like: 'Like',
        logout: 'Logout',
        markread: 'Mark Read',
        more: 'More',
        no: 'No',
        ok: 'OK',
        password: 'Password',
        permission: 'Permission Settings',
        personalsettings: 'Personal Settings',
        preview: 'Preview',
        print: 'Print',
        preferences: 'Preferences',
        profile: 'Profile',
        progress: 'Progress',
        publish: 'Publish',
        query: 'Query',
        read: 'Read',
        refresh: 'Refresh',
        reply: 'Reply',
        reset: 'Reset',
        resume: 'Resume',
        return: 'Return',
        revoke: 'Revoke',
        run: 'Run',
        send: 'Send',
        sendmessage: 'Send Message',
        share: 'Share',
        sign: 'Sign',
        start: 'Start',
        startflow: 'Start Flow',
        stop: 'Stop',
        sync: 'Sync',
        subsign: 'Remove Sign',
        submit: 'Submit',
        suspend: 'Suspend',
        template: 'Template',
        terminate: 'Terminate',
        theme: 'Theme Settings',
        tolist: 'List',
        totranspose: 'Transpose',
        transfer: 'Transfer',
        truncate: 'Truncate',
        unfavorite: 'Unfavorite',
        uncomment: 'Uncomment',
        unflagging: 'Unreport',
        unfollow: 'Unfollow',
        unshare: 'Unshare',
        unlike: 'Unlike',
        unlock: 'Unlock',
        unread: 'Unread',
        uncheckall: 'Uncheck All',
        update: 'Update',
        upload: 'Upload',
        urge: 'Urge',
        validate: 'Validate',
        version: 'Version',
        yes: 'Yes'
      },
    
      /** <summary>4. entity (TaktEntityBase)</summary> */
      entity: {
        configid: 'Config ID',
        createby: 'Created By',
        createtime: 'Create Time',
        createdbyid: 'Created By ID',
        deletedby: 'Deleted By',
        deletedbyid: 'Deleted By ID',
        deletedtime: 'Deleted Time',
        extfieldjson: 'Extended Field JSON',
        id: 'ID',
        isdeleted: 'Is Deleted',
        remark: 'Remark',
        updateby: 'Updated By',
        updatetime: 'Update Time',
        updatedbyid: 'Updated By ID'
      },
    
      /** <summary>5. msg</summary> */
      msg: {
        actionfail: '{action} failed',
        actionsuccess: '{action} successfully',
        assignfail: 'Assign {target} failed',
        assignsuccess: '{target} assigned successfully',
        createsuccess: 'Created successfully',
        deletefail: 'Delete failed',
        deletesuccess: 'Deleted successfully',
        entityidrequired: '{entity} ID is required',
        entitynotfound: '{entity} not found',
        exportfail: 'Export failed',
        exportsuccess: 'Export successful',
        loadfail: 'Failed to load data',
        loadoptionsfail: 'Failed to load options, please try again later',
        loadtargetfail: 'Failed to load {target}',
        nosearchresult: 'No search results',
        operatefail: 'Operation failed',
        updatesuccess: 'Updated successfully'
      },
    
      /** <summary>6. action</summary> */
      action: {
        confirmaction: 'Confirm {action}',
        confirmdelete: 'Confirm Delete',
        etc: 'etc.',
        exportdatasuffix: ' Data',
        import: {
          hint: 'Excel (.xlsx) import supported. Max 1000 records per import.',
          sheetnametemplate: '{entity} Import Template',
          templatetext: 'Download {entity} Import Template',
          uploadtext: 'Click or drag Excel file to this area to upload'
        },
        management: 'Management',
        operation: 'Operation',
        or: 'or',
        superrole: 'Super role',
        superuser: 'Super user',
        tabtargetallocation: '{target} Allocation',
        thistarget: 'This {target}',
        transferassigned: 'Assigned',
        transferunassigned: 'Unassigned',
        warnaction: {},
        warnselecttoaction: 'Please select {entity} to {action}',
        warnsubjectcannot: '{subject} cannot {action}',
        warnusercannot: 'User {name} cannot {action}'
      },
    
      /** <summary>7. form</summary> placeholder & tabs; validation messages from backend seeds (validation.*) */
      form: {
        tabs: {
          basicinfo: 'Basic Info',
          announcementbody: 'Body',
          announcementpublish: 'Publishing',
          announcementother: 'Other'
        },
        placeholder: {
          copyright: 'Enter copyright info',
          keyword: 'Keyword',
          ordernumhint: 'Lower value = higher position',
          required: 'Enter {field}',
          requiredagain: 'Enter {field} again',
          search: 'Enter {keyword}',
          searchkeyword: 'Enter keyword to search',
          searchmenu: 'Search menu, pages...',
          select: 'Select {field}',
          selectfirst: 'Please select {field} first',
          selectonly: 'Please select',
          treekeyword: 'Tree keyword',
          watermark: 'Enter watermark content'
        }
      },
    
      /** <summary>8. confirm</summary> */
      confirm: {
        deletecountentity: 'Are you sure to delete the selected {count} {entity}(s)?',
        deleteentity: 'Are you sure to delete {entity} "{name}"?',
        resetpwdcontent: 'Are you sure to reset {entity} "{name}" password to default?',
        unlockcontent: 'Are you sure to unlock {entity} "{name}"?'
      },
    
      /** <summary>9. API request</summary> request interceptor / 401 / network errors (request.ts) */
      api: {
        loginexpired: 'Login expired, please sign in again',
        tokenrefreshfail: 'Token refresh failed',
        redirectingtologin: 'Redirecting to login',
        tokenrefreshfailonloginpage: 'Token refresh failed, already on login page',
        requestfail: 'Request failed',
        forbidden: 'Access denied',
        notfound: 'The requested resource was not found',
        servererror: 'Server error, please try again later',
        systemerror: 'System error, please try again later',
        csrffail: 'Security verification failed, please refresh the page',
        connectfail: 'Connection failed',
        connectfaildescription: 'Unable to connect to the server. Please check:\n1. Whether the backend service is running\n2. Network connection\n3. API base URL configuration',
        requestconfigerror: 'Request configuration error'
      }
  }
}
