/**
 * مولد الكود · العربية
 * Form labels use backend entity.gentable / entity.gentablecolumn keys
 */
export default {
  tableConfig: 'إعداد الجدول',

  keyword: 'اسم الجدول أو فئة الكيان أو الاسم التجاري',

  importFromDb: 'استيراد الجداول من قاعدة البيانات',
  saveAs: 'حفظ باسم',
  saveAsPathHint: 'أدخل مسار الإخراج (يتجاوز المسار في إعداد الجدول):',
  saveAsPathPlaceholder: 'مثال: D:\\Projects\\Takt.Net',
  genPath: 'مسار الإخراج',
  advancedQuery: 'استعلام متقدم',
  searchKeywordLabel: 'اسم الجدول / الكيان / الاسم التجاري',
  placeholderFuzzy: 'بحث تقريبي',

  tableName: 'اسم الجدول',
  tableComment: 'وصف الجدول',
  entityClassName: 'فئة الكيان',
  genModuleName: 'الوحدة',
  genBusinessName: 'الاسم التجاري',
  genTemplate: 'القالب',

  generate: 'توليد',
  sync: 'مزامنة',
  initialize: 'تهيئة',

  overwriteConfirmTitle: 'تأكيد الاستبدال',
  overwriteConfirmContent: 'الملفات التالية موجودة. هل تستبدل؟',
  overwrite: 'استبدال',
  saveAsCancel: 'حفظ باسم',

  noTableIdSync: 'لا يوجد معرف جدول، لا يمكن المزامنة',
  noTableIdPreview: 'لا يوجد معرف جدول، لا يمكن المعاينة',
  noTableIdInit: 'لا يوجد معرف جدول، لا يمكن التهيئة',
  noPreviewData: 'لا توجد معاينة؛ تحقق من إعداد الجدول أو القوالب',
  syncFormHint: 'احفظ في نافذة التحرير لتحديث الحقول من مصدر البيانات.',
  cloneSuccess: 'تم النسخ كسجل جديد؛ غيّر اسم الجدول واحفظ.',
  noDataToExport: 'لا توجد بيانات للتصدير',
  exportFileName: 'إعداد جدول مولد الكود',
  codeGeneratedDownload: 'تم توليد الكود وتنزيله',
  genSuccessCount: '، {count} ملف(ات)',
  existingFilesSuffix: '... {count} ملف(ات) في المجموع',

  previewTitle: 'معاينة الكود',
  previewEmpty: 'لا يوجد محتوى للمعاينة',
  previewHint: 'اختر سجلاً ثم انقر معاينة.',
  previewHintDetail: 'المعاينة من إعداد الجدول والقوالب الحالية؛ أعد المعاينة بعد التعديلات.',
  previewFileEmpty: '(هذا الملف بدون محتوى)',
  previewTabs: {
    entity: 'الكيان',
    dto: 'DTO',
    service: 'الخدمة',
    controller: 'التحكم',
    types: 'أنواع الواجهة',
    api: 'API',
    i18n: 'الترجمة',
    view: 'العرض',
    form: 'النموذج',
    sql: 'القائمة والترجمة (SQL)',
    other: 'أخرى'
  },

  validation: {
    columnNameSnakeCase: 'الصف {rowNum}: اسم العمود يجب أن يكون snake_case (مثال column_1, user_name)، الحالي «{colName}»',
    csharpColumnPascalCase: 'الصف {rowNum}: اسم عمود C# يجب أن يكون PascalCase (مثال Column1, UserName)، الحالي «{csharpName}»'
  },

  form: {
    tabBusiness: 'الوحدة التجارية',
    tabEntity: 'الكيان و DTO',
    tabService: 'الخدمة والتحكم',
    tabGenerate: 'توليد',
    tabFront: 'الواجهة والأنماط',
    tabColumn: 'إعداد الحقول',
    labelCurrentProjectPath: 'مسار المشروع الحالي',
    placeholderDataTableRequired: 'اختر أو أدخل اسم الجدول',
    placeholderTableName: 'snake_case بأحرف صغيرة، مثال xxx_xxx_xxx',
    placeholderTableComment: 'تعليق الجدول',
    placeholderNamePrefix: 'اسم المشروع، افتراضي Takt',
    placeholderPermsPrefix: 'تلقائي من الوحدة+التجاري، مثال accounting:controlling:standard:wage:rate',
    placeholderModule: 'اختر الوحدة أو اكتب، مثال Generator، HumanResource.Organization',
    placeholderBusinessFromTable: 'تلقائي من اسم الجدول',
    placeholderBusinessManual: 'لأسماء الكيانات/الخدمات/التحكم، مثال Setting، Dept',
    placeholderFunctionName: 'تلقائي من وصف الجدول، للقراءة فقط',
    placeholderAutoByModule: 'تلقائي من الوحدة',
    placeholderAutoByBusiness: 'تلقائي من الاسم التجاري',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: 'جاري التحميل…',
    placeholderCurrentProjectPathHint: 'اختر "المشروع الحالي" للتحميل',
    placeholderParentMenu: 'اختر القائمة الأب (فارغ = الجذر)',
    placeholderAuthor: 'المستخدم الحالي',
    placeholderDbType: 'نوع DB',
    placeholderCsharpType: 'نوع C#',
    placeholderQueryType: 'نوع الاستعلام',
    placeholderHtmlType: 'نوع العرض',
    placeholderDictType: 'اختر نوع القاموس',
    emptySaveTableFirst: 'احفظ إعداد الجدول أولاً لإدارة الحقول',
    emptyNoColumnData: 'لا توجد بيانات حقول'
  }
}
