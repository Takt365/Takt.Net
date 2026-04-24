/**
 * 代码生成 · 日语
 * 约定：`export default { page: { … } }`，运行时键为 code.generator.page.*。
 * 列名等：entity.gentable.*；通用：common.*
 */
export default {
  page: {
    importfromdb: 'データベースからテーブルをインポート',
    saveas: '別名で保存',
    saveaspathhint: '生成パスを入力してください（表設定のパスを上書きします）：',
    saveaspathplaceholder: '例：D:\\Projects\\Takt.Net',
    overwriteconfirmtitle: '上書き確認',
    overwriteconfirmcontent: 'ターゲットパスに以下のファイルが既に存在します。上書きしますか？',
    overwrite: '上書き',
    saveascancel: '別名で保存',
    notableidsync: 'このレコードにはテーブルIDがないため、同期できません',
    notableidinit: 'このレコードにはテーブルIDがないため、初期化できません',
    syncformhint: '編集ダイアログで保存してデータソースからフィールド設定を更新してください。完全な同期にはバックエンドインターフェースが必要です',
    clonesuccess: '新規作成としてコピーされました。テーブル名を変更して保存してください',
    nodataexport: 'エクスポートできるデータがありません',
    codegenerateddownload: 'コードが生成されダウンロードされました',
    gensuccesscount: '、合計 {count} ファイル',
    existingfilessuffix: 'など合計 {count} ファイル',
    notableidpreview: 'このレコードにはテーブルIDがないため、プレビューできません',
    importtable: {
      datatable: 'データテーブル'
    },
    form: {
      tab: {
        table: 'テーブル設定',
        column: 'フィールド設定',
        basic: '基本情報',
        business: 'ビジネスモジュール',
        entitydto: 'エンティティとDTO',
        service: 'サービスとコントローラー',
        generate: '生成',
        front: 'フロントエンドとスタイル'
      },
      placeholder: {
        configid: 'データソースを選択してください',
        tablenamenew: '小文字スネークケース、例：xxx_xxx_xxx',
        tablenameedit: 'まずデータソースを選択してください',
        tablecomment: 'テーブルコメント',
        gentemplatecategory: '生成テンプレートタイプを選択してください',
        indatabase: 'DBテーブルかどうかを選択してください',
        subtablename: '親テーブルを選択してください',
        subtablefkname: '外部キー列を選択してください',
        treecode: 'ツリーコード列を選択してください',
        treeparentcode: 'ツリー親コード列を選択してください',
        treename: 'ツリー名列を選択してください',
        nameprefix: 'プロジェクト名、デフォルトは Takt、変更後はすべての名前空間が同期されます',
        permsprefix: 'モジュール名+ビジネス名から自動生成、例：accounting:controlling:standard:wage:rate',
        genmodulename: 'モジュール（ディレクトリ）を選択するか手動入力、例：Generator、HumanResource.Organization',
        genbusinessnamefromtable: 'テーブル名から自動生成',
        genbusinessnamemanual:
          'エンティティ/サービス/コントローラーなどのクラス名およびインターフェースコメントに使用、例：設定、部門',
        genfunctionname: 'テーブル説明から自動取得、読み取り専用',
        autofrommodule: 'モジュール名から自動生成',
        autofrombusiness: 'ビジネス名から自動生成',
        genmethod: '生成方法を選択してください',
        genpath: '/',
        currentprojectloading: '取得中…',
        currentprojectidle: '生成方法を現在のプロジェクトに選択後、自動取得されます',
        parentmenuid: '親メニューを選択してください（未選択の場合はルート）',
        sorttype: 'ソートタイプを選択してください',
        sortfield: 'ソートフィールドを選択してください',
        frontui: 'フロントエンドUIフレームワークを選択してください',
        frontformlayout: 'フロントエンドフォームレイアウトを選択してください',
        frontbtnstyle: 'フロントエンドボタンスタイルを選択してください',
        genauthor: '現在のログインユーザー',
        columndbtype: 'DBタイプ',
        columncsharptype: 'C#タイプ',
        columnquerytype: 'クエリ方法',
        columnhtmltype: '表示タイプ',
        columndicttype: '辞書タイプを選択'
      },
      rules: {
        tablecomment: 'テーブル説明を入力してください',
        gentemplatecategory: '生成テンプレートタイプを選択してください',
        permsprefix: '権限プレフィックスを入力してください',
        menubuttongroup: 'メニュー権限グループを選択してください',
        genmodulename: 'モジュール名を選択してください',
        genbusinessname: 'ビジネス名を入力してください',
        entityclassname: 'エンティティクラス名を入力してください',
        dtoclassname: 'Dto クラス名を入力してください',
        iserviceclassname: 'サービスインターフェースクラス名を入力してください',
        serviceclassname: 'サービスクラス名を入力してください',
        controllerclassname: 'コントローラークラス名を入力してください',
        isrepository: 'リポジトリを生成するか選択してください',
        genmethod: '生成方法を選択してください',
        isgentranslation: '翻訳を生成するか選択してください',
        frontformlayout: 'フロントエンドフォームレイアウトを選択してください',
        isusetabs: 'タブを使用するか選択してください',
        genauthor: '作者を入力してください',
        sorttype: 'ソートタイプを選択してください',
        sortfield: 'ソートフィールドを選択してください',
        tablename: 'データテーブル名を選択または入力してください',
        nameprefix: '名前空間プレフィックスを入力してください',
        subtablename: '関連親テーブルを選択してください',
        subtablefkname: '関連外部キーを選択してください',
        treecode: 'ツリーコードフィールドを選択してください',
        treename: 'ツリー名フィールドを選択してください',
        treeparentcode: 'ツリー親コードを選択してください',
        genpath: '生成パスを入力してください',
        parentmenuid: '親メニューを選択してください',
        repositoryinterfacenamespace: 'リポジトリインターフェース名前空間を入力してください',
        irepositoryclassname: 'リポジトリインターフェースクラス名を入力してください',
        repositorynamespace: 'リポジトリ名前空間を入力してください',
        repositoryclassname: 'リポジトリクラス名を入力してください',
        tabsfieldcount: 'タブフィールド数を入力してください'
      },
      validation: {
        tablenameformat: 'データテーブル名は小文字スネークケースである必要があります。例：xxx_xxx_xxx',
        nameprefixpascal: 'パスカル命名である必要があります。例：Takt（最初の文字は大文字、英数字のみ）',
        columnsnake: 'フィールド設定第 {row} 行：列名はスネークケースである必要があります（例：column_1、user_name）、現在は「{value}」',
        columnpascal: 'フィールド設定第 {row} 行：C#列名はパスカルケースである必要があります（例：Column1、UserName）、現在は「{value}」'
      },
      column: {
        addRow: '行を追加',
        delete: '削除',
        emptySaveFirst: 'まずテーブル設定を保存してからフィールドを管理してください',
        emptyNoData: 'フィールドデータがありません',
        dragsort: 'ドラッグ並べ替え'
      }
    },
    preview: {
      loading: 'プレビューファイルリストを読み込んでいます...',
      empty: 'プレビューコンテンツがありません',
      emptyhint: '現在のバックエンドは生成ファイルパスと上書き情報のみを返し、ソースコードコンテンツはまだ返されていません。',
      validationissuetitle: 'テンプレート検証で {count} 件の問題が見つかりました',
      validationissuetoast: 'テンプレート検証で {count} 件の問題が見つかりました。まず修復してから生成してください',
      exists: '既に存在',
      loadfail: 'プレビューの読み込みに失敗しました',
      pathcontent: 'ターゲットパス：{path}',
      tab: {
        backend: 'バックエンド',
        frontend: 'フロントエンド',
        script: 'スクリプト'
      },
      category: {
        backend: {
          entity: 'エンティティ Entities',
          dto: 'DTO',
          service: 'サービスインターフェース/実装',
          controller: 'コントローラー',
          other: 'その他'
        },
        frontend: {
          api: 'API',
          type: '型定義',
          view: 'リストビュー',
          component: 'サブコンポーネント',
          other: 'その他'
        },
        script: {
          translationsql: '翻訳 SQL',
          menusql: 'メニュー SQL',
          other: 'その他'
        }
      }
    }
  }
}
