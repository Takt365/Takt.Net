/**
 * コード生成 · 日本語
 * Form labels use backend entity.gentable / entity.gentablecolumn keys
 */
export default {
  tableConfig: 'テーブル設定',

  keyword: 'テーブル名・エンティティクラス名・業務名',

  importFromDb: 'データベースからテーブルをインポート',
  saveAs: '名前を付けて保存',
  saveAsPathHint: '出力パスを入力（テーブル設定のパスを上書きします）：',
  saveAsPathPlaceholder: '例：D:\\Projects\\Takt.Net',
  genPath: '出力パス',
  advancedQuery: '詳細検索',
  searchKeywordLabel: 'テーブル名／エンティティ／業務名',
  placeholderFuzzy: 'あいまい検索',

  tableName: 'テーブル名',
  tableComment: 'テーブルコメント',
  entityClassName: 'エンティティクラス',
  genModuleName: 'モジュール',
  genBusinessName: '業務名',
  genTemplate: 'テンプレート',

  generate: '生成',
  sync: '同期',
  initialize: '初期化',

  overwriteConfirmTitle: '上書き確認',
  overwriteConfirmContent: '以下のファイルが既に存在します。上書きしますか？',
  overwrite: '上書き',
  saveAsCancel: '名前を付けて保存',

  noTableIdSync: 'テーブルIDがありません。同期できません',
  noTableIdPreview: 'テーブルIDがありません。プレビューできません',
  noTableIdInit: 'テーブルIDがありません。初期化できません',
  noPreviewData: 'プレビューなし。テーブル設定またはテンプレートを確認してください',
  syncFormHint: '編集ダイアログで保存するとデータソースからフィールドを更新します。',
  cloneSuccess: '新規として複製しました。テーブル名を変更して保存してください。',
  noDataToExport: 'エクスポートするデータがありません',
  exportFileName: 'コード生成テーブル設定',
  codeGeneratedDownload: 'コードを生成してダウンロードしました',
  genSuccessCount: '、{count} ファイル',
  existingFilesSuffix: '… 計 {count} ファイル',

  previewTitle: 'コードプレビュー',
  previewEmpty: 'プレビュー内容がありません',
  previewHint: 'レコードを選択して「プレビュー」をクリックしてください。',
  previewHintDetail: 'プレビューは現在のテーブル設定とテンプレートからリアルタイムで生成されます。変更後に再プレビューで更新を確認できます。',
  previewFileEmpty: '（このファイルに内容がありません）',
  previewTabs: {
    entity: 'エンティティ',
    dto: 'DTO',
    service: 'サービス',
    controller: 'コントローラ',
    types: 'フロント型',
    api: 'API',
    i18n: '翻訳',
    view: 'ビュー',
    form: 'フォーム',
    sql: 'メニューと翻訳(SQL)',
    other: 'その他'
  },

  validation: {
    columnNameSnakeCase: '第 {rowNum} 行：列名は snake_case（例 column_1、user_name）である必要があります。現在「{colName}」',
    csharpColumnPascalCase: '第 {rowNum} 行：C# 列名は PascalCase（例 Column1、UserName）である必要があります。現在「{csharpName}」'
  },

  form: {
    tabBusiness: '業務モジュール',
    tabEntity: 'エンティティとDTO',
    tabService: 'サービスとコントローラ',
    tabGenerate: '生成',
    tabFront: 'フロントとスタイル',
    tabColumn: 'フィールド設定',
    labelCurrentProjectPath: '現在のプロジェクトパス',
    placeholderDataTableRequired: 'テーブル名を選択または入力',
    placeholderTableName: '小文字 snake_case、例 xxx_xxx_xxx',
    placeholderTableComment: 'テーブルコメント',
    placeholderNamePrefix: 'プロジェクト名、既定 Takt',
    placeholderPermsPrefix: 'モジュール+業務で自動、例 accounting:controlling:standard:wage:rate',
    placeholderModule: 'モジュールを選択または入力、例 Generator、HumanResource.Organization',
    placeholderBusinessFromTable: 'テーブル名から自動',
    placeholderBusinessManual: 'エンティティ/サービス/コントローラ名用、例 Setting、Dept',
    placeholderFunctionName: 'テーブルコメントから自動、読み取り専用',
    placeholderAutoByModule: 'モジュールから自動',
    placeholderAutoByBusiness: '業務名から自動',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: '読み込み中…',
    placeholderCurrentProjectPathHint: '「現在のプロジェクト」を選択して読み込み',
    placeholderParentMenu: '親メニューを選択（空=ルート）',
    placeholderAuthor: '現在のユーザー',
    placeholderDbType: 'DB型',
    placeholderCsharpType: 'C#型',
    placeholderQueryType: 'クエリタイプ',
    placeholderHtmlType: '表示タイプ',
    placeholderDictType: '辞書タイプを選択',
    emptySaveTableFirst: 'フィールドを管理するには先にテーブル設定を保存してください',
    emptyNoColumnData: 'フィールドデータがありません'
  }
}
