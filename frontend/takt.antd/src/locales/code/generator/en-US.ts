/**
 * Code Generator · English (US)
 * Convention: `export default { page: { … } }`, runtime key is code.generator.page.*.
 * Column names etc: entity.gentable.*; common: common.*
 */
export default {
  page: {
    importfromdb: 'Import tables from database',
    saveas: 'Save as',
    saveaspathhint: 'Enter generation path (will override path in table config):',
    saveaspathplaceholder: 'e.g. D:\\Projects\\Takt.Net',
    overwriteconfirmtitle: 'Overwrite confirmation',
    overwriteconfirmcontent: 'The following files already exist in the target path. Overwrite?',
    overwrite: 'Overwrite',
    saveascancel: 'Save as',
    notableidsync: 'This record has no table ID, cannot sync',
    notableidinit: 'This record has no table ID, cannot initialize',
    syncformhint: 'Please save in the edit dialog to refresh field config from data source; full sync requires backend API',
    clonesuccess: 'Copied as new, please modify table name and save',
    nodataexport: 'No data to export',
    codegenerateddownload: 'Code generated and downloaded',
    gensuccesscount: ', total {count} files',
    existingfilessuffix: 'etc. total {count} files',
    notableidpreview: 'This record has no table ID, cannot preview',
    importtable: {
      datatable: 'Data table'
    },
    form: {
      tab: {
        table: 'Table config',
        column: 'Field config',
        basic: 'Basic info',
        business: 'Business module',
        entitydto: 'Entity & DTO',
        service: 'Service & controller',
        generate: 'Generate',
        front: 'Frontend & style'
      },
      placeholder: {
        configid: 'Select data source',
        tablenamenew: 'Lowercase snake_case, e.g. xxx_xxx_xxx',
        tablenameedit: 'Select data source first',
        tablecomment: 'Table comment',
        gentemplatecategory: 'Select template type',
        indatabase: 'Select if DB table',
        subtablename: 'Select parent table',
        subtablefkname: 'Select FK column',
        treecode: 'Select tree code column',
        treeparentcode: 'Select parent code column',
        treename: 'Select tree name column',
        nameprefix: 'Project name prefix, default Takt; updates all namespaces',
        permsprefix: 'Auto from module + business, e.g. accounting:controlling:standard:wage:rate',
        genmodulename: 'Pick module path or type, e.g. Generator, HumanResource.Organization',
        genbusinessnamefromtable: 'Auto from table name',
        genbusinessnamemanual: 'For class names and API comments, e.g. Settings, Department',
        genfunctionname: 'From table description, read-only',
        autofrommodule: 'Auto from module name',
        autofrombusiness: 'Auto from business name',
        genmethod: 'Select output mode',
        genpath: '/',
        currentprojectloading: 'Loading…',
        currentprojectidle: 'Shown when output mode is current project',
        parentmenuid: 'Parent menu (empty = root)',
        sorttype: 'Select sort type',
        sortfield: 'Select sort field',
        frontui: 'Select frontend UI framework',
        frontformlayout: 'Select frontend form layout',
        frontbtnstyle: 'Select frontend button style',
        genauthor: 'Current user',
        columndbtype: 'DB type',
        columncsharptype: 'C# type',
        columnquerytype: 'Query mode',
        columnhtmltype: 'Display type',
        columndicttype: 'Dict type'
      },
      rules: {
        tablecomment: 'Enter table description',
        gentemplatecategory: 'Select template type',
        permsprefix: 'Enter permission prefix',
        menubuttongroup: 'Select menu button group',
        genmodulename: 'Select module name',
        genbusinessname: 'Enter business name',
        entityclassname: 'Enter entity class name',
        dtoclassname: 'Enter Dto class name',
        iserviceclassname: 'Enter service interface name',
        serviceclassname: 'Enter service class name',
        controllerclassname: 'Enter controller class name',
        isrepository: 'Select whether to generate repository',
        genmethod: 'Select output mode',
        isgentranslation: 'Select whether to generate translations',
        frontformlayout: 'Select frontend form layout',
        isusetabs: 'Select whether to use tabs',
        genauthor: 'Enter author',
        sorttype: 'Select sort type',
        sortfield: 'Select sort field',
        tablename: 'Select or enter table name',
        nameprefix: 'Enter namespace prefix',
        subtablename: 'Select parent table',
        subtablefkname: 'Select foreign key',
        treecode: 'Select tree code column',
        treename: 'Select tree name column',
        treeparentcode: 'Select parent code column',
        genpath: 'Enter output path',
        parentmenuid: 'Select parent menu',
        repositoryinterfacenamespace: 'Enter repository interface namespace',
        irepositoryclassname: 'Enter repository interface name',
        repositorynamespace: 'Enter repository namespace',
        repositoryclassname: 'Enter repository class name',
        tabsfieldcount: 'Enter tabs field count'
      },
      validation: {
        tablenameformat: 'Table name must be lowercase snake_case, e.g. xxx_xxx_xxx',
        nameprefixpascal: 'Must be PascalCase, e.g. Takt (letters and digits)',
        columnsnake:
          'Row {row}: column name must be snake_case (e.g. column_1, user_name); current: 「{value}」',
        columnpascal:
          'Row {row}: C# column name must be PascalCase (e.g. Column1, UserName); current: 「{value}」'
      },
      column: {
        addRow: 'Add row',
        delete: 'Delete',
        emptySaveFirst: 'Save table configuration first, then manage columns',
        emptyNoData: 'No column data',
        dragsort: 'Drag Sort'
      }
    },
    preview: {
      loading: 'Loading preview file list...',
      empty: 'No preview content',
      emptyhint: 'Current backend only returns file paths and overwrite info, source code content not yet returned.',
      validationissuetitle: 'Template validation found {count} issues',
      validationissuetoast: 'Template validation found {count} issues, please fix before generating',
      exists: 'Already exists',
      loadfail: 'Failed to load preview',
      pathcontent: 'Target path: {path}',
      tab: {
        backend: 'Backend',
        frontend: 'Frontend',
        script: 'Script'
      },
      category: {
        backend: {
          entity: 'Entity Entities',
          dto: 'DTO',
          service: 'Service interface/impl',
          controller: 'Controller',
          other: 'Other'
        },
        frontend: {
          api: 'API',
          type: 'Type definitions',
          view: 'List view',
          component: 'Sub-component',
          other: 'Other'
        },
        script: {
          translationsql: 'Translation SQL',
          menusql: 'Menu SQL',
          other: 'Other'
        }
      }
    }
  }
}
