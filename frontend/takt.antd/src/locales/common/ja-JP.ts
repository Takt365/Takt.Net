/**
 * Common translations · 日本語
 * Same structure and key order as zh-CN (alphabetical for button)
 */
export default {
  /** <summary>1. app</summary> */
  app: {
    htmlTitle: 'Takt Digital Factory',
    name: 'Takt Digital Factory (TDF) ',
    productcode: 'TDF-MES-PRO',
    slogan: '未来を駆動',
    tagline: '実用·シンプル·柔軟'
  },

  /** <summary>2. settings</summary> */
  settings: {
    /** 十大著名色彩、color-base.less に準拠 */
    color: {
      blue: 'クラインブルー',
      brown: 'バン・ダイク・ブラウン',
      cyan: 'ティファニーブルー',
      gold: 'ゴールド',
      gray: 'メモリアルグレー',
      green: 'マーズグリーン',
      indigo: 'プルシアンブルー',
      orange: 'ティツィアーノレッド',
      pink: 'ボルドー',
      purple: 'ブルゴーニュ',
      red: 'チャイニーズレッド',
      yellow: 'セネリエイエロー',
      switch: '色を切り替え',
      title: '色',
      locked: '本日のテーマ色は固定で変更できません'
    },
    locale: {
      'ar-SA': 'العربية',
      'en-US': 'English',
      'es-ES': 'Español',
      'fr-FR': 'Français',
      'ja-JP': '日本語',
      'ko-KR': '한국어',
      'ru-RU': 'Русский',
      'zh-CN': '簡体中文',
      'zh-TW': '繁体中文',
      switch: '言語を切り替え',
      title: '言語'
    },
    theme: {
      dark: 'ダーク',
      light: 'ライト',
      switch: 'テーマを切り替え',
      title: 'テーマ'
    }
  },

  /** Layout position */
  layout: {
    position: {
      left: '左',
      center: '中央',
      right: '右'
    }
  },

  /** <summary>3. button</summary> Alphabetical order as zh-CN */
  button: {
    addsign: '加签',
    advancedQuery: '詳細検索',
    allocate: '割り当て',
    approve: '承認',
    archive: 'アーカイブ',
    back: '戻る',
    cancel: 'キャンセル',
    changepwd: 'パスワード変更',
    checkAll: 'すべて選択',
    clean: 'クリア',
    clone: 'クローン',
    collapse: '折りたたみ',
    columnSetting: '列設定',
    comment: 'コメント',
    config: '設定',
    confirm: '確認',
    copy: 'コピー',
    create: '新規',
    createRow: '行を追加',
    datasource: 'データソース',
    delegate: '委任',
    delete: '削除',
    design: '設計',
    detail: '詳細',
    disable: '無効',
    download: 'ダウンロード',
    draft: '下書き',
    edit: '編集',
    enable: '有効',
    empty: 'クリア',
    empty30d: '30日間をクリア',
    empty7d: '7日間をクリア',
    emptyAll: 'すべてクリア',
    expand: '展開',
    exitFullscreen: '全画面終了',
    export: 'エクスポート',
    favorite: 'お気に入り',
    field: 'フィールド管理',
    fixed: '固定',
    forward: '転送',
    formData: 'フォームデータ',
    fullscreen: '全画面',
    history: '履歴',
    import: 'インポート',
    like: 'いいね',
    logout: 'ログアウト',
    markRead: '既読にする',
    more: 'もっと',
    no: 'いいえ',
    ok: 'OK',
    open: '開く',
    password: 'パスワード',
    permission: '権限設定',
    personalSettings: '個人設定',
    preview: 'プレビュー',
    print: '印刷',
    preferences: '環境設定',
    profile: 'プロフィール',
    progress: '進捗',
    publish: '公開',
    query: '検索',
    read: '既読',
    refresh: '更新',
    reply: '返信',
    reset: 'リセット',
    resume: '再開',
    return: '戻す',
    revoke: '取り消し',
    run: '実行',
    send: '送信',
    share: '共有',
    sign: '署名',
    start: '開始',
    stop: '停止',
    subsign: '减签',
    submit: '送信',
    suspend: '一時停止',
    template: 'テンプレート',
    terminate: '終了',
    theme: 'テーマ設定',
    toList: 'リスト',
    toTranspose: '転置',
    transfer: '転送',
    truncate: '切り詰め',
    unfavorite: 'お気に入り解除',
    uncomment: 'コメント解除',
    unflagging: '報告解除',
    unfollow: 'フォロー解除',
    unshare: '共有解除',
    unlike: 'いいね解除',
    unlock: 'ロック解除',
    unread: '未読',
    uncheckAll: '選択解除',
    update: '更新',
    upload: 'アップロード',
    urge: '督促',
    validate: '検証',
    version: 'バージョン',
    yes: 'はい'
  },

  /** <summary>4. entity</summary> 审计字段顺序与 TaktEntityBase 一致 */
  entity: {
    configId: '設定ID',
    extFieldJson: '拡張フィールドJSON',
    remark: '備考',
    createId: '作成者ID',
    createBy: '作成者',
    createTime: '作成日時',
    updateId: '更新者ID',
    updateBy: '更新者',
    updateTime: '更新日時',
    isDeleted: '削除済み',
    deleteId: '削除者ID',
    deletedBy: '削除者',
    deletedTime: '削除日時'
  },

  /** <summary>5. msg</summary> */
  msg: {
    actionFail: '{action}に失敗しました',
    actionSuccess: '{action}しました',
    assignFail: '{target}の割り当てに失敗しました',
    assignSuccess: '{target}を割り当てました',
    createSuccess: '{target}を作成しました',
    deleteFail: '{target}の削除に失敗しました',
    deleteSuccess: '{target}を削除しました',
    entityIdRequired: '{entity}IDが必要です',
    entityNotFound: '{entity}が見つかりません',
    exportFail: '{target}のエクスポートに失敗しました',
    exportSuccess: '{target}をエクスポートしました',
    loadFail: 'データの読み込みに失敗しました',
    loadOptionsFail: '{target}オプションの読み込みに失敗しました。しばらくして再試行してください',
    loadListFail: '{target}リストの読み込みに失敗しました。\nサーバーを確認して再試行してください。',
    loadTargetFail: '{target}の読み込みに失敗しました',
    noSearchResult: '検索結果がありません',
    operateFail: '{action}に失敗しました',
    updateSuccess: '{target}を更新しました'
  },

  /** <summary>6. action</summary> */
  action: {
    cancel: 'キャンセル',
    confirmAction: '{action}の確認',
    confirmDelete: '削除の確認',
    etc: 'など',
    exportDataSuffix: 'データ',
    import: {
      hint: 'Excel（.xlsx）のインポートに対応。1回のインポートは最大1000件。',
      sheetNameTemplate: '{entity}インポートテンプレート',
      templateText: '{entity}インポートテンプレートをダウンロード',
      uploadText: 'クリックまたはExcelファイルをここにドラッグしてアップロード'
    },
    management: '管理',
    operation: '操作',
    or: 'または',
    superRole: 'スーパーロール',
    superUser: 'スーパーユーザー',
    tabTargetAllocation: '{target}割り当て',
    thisTarget: 'この{target}',
    transferAssigned: '割り当て済み',
    transferUnassigned: '未割り当て',
    warnAction: {},
    warnSelectToAction: '{action}する{entity}を選択してください',
    warnSubjectCannot: '{subject}は{action}できません',
    warnUserCannot: 'ユーザー {name} は{action}できません'
  },

  /** <summary>7. form</summary> */
  form: {
    tabs: {
      basicInfo: '基本情報'
    },
    placeholder: {
      copyright: '著作権情報を入力',
      orderNumHint: '値が小さいほど前に表示',
      required: '{field}を入力してください',
      requiredAgain: '{field}を再入力してください',
      search: '{keyword}を入力',
      searchKeyword: 'キーワードを入力して検索',
      searchMenu: 'メニュー・ページを検索...',
      select: '{field}を選択してください',
      selectFirst: '先に{field}を選択してください',
      selectOnly: '選択してください',
      treeKeyword: 'ツリーキーワード',
      watermark: '透かし文字を入力',
      lengthExact: '{field}は{length}文字で入力してください'
    },
    validation: {
      enterValid: '正しい{field}を入力してください',
      notFound: '{target}が見つかりません'
    }
  },

  /** <summary>8. confirm</summary> */
  confirm: {
    deleteCountEntity: '選択した{count}件の{entity}を削除してもよろしいですか？',
    deleteEntity: '{entity}「{name}」を削除してもよろしいですか？',
    resetPwdContent: '{entity}「{name}」のパスワードをデフォルトにリセットしてもよろしいですか？',
    unlockContent: '{entity}「{name}」のロックを解除してもよろしいですか？'
  },
  /** PWA 更新通知 */
  pwa: {
    offlineReady: 'アプリはオフラインで使用できるようになりました',
    needRefresh: '新しいバージョンが利用可能です。再読み込みをクリックして更新してください',
    reload: '再読み込み',
    close: '閉じる'
  },

  api: {
    loginExpired: 'ログインの有効期限が切れました。再度ログインしてください',
    tokenRefreshFail: 'Token の更新に失敗しました',
    redirectingToLogin: 'ログインページにリダイレクトしています',
    tokenRefreshFailOnLoginPage: 'Token の更新に失敗しました。既にログインページにいます',
    requestFail: 'リクエストに失敗しました',
    forbidden: 'アクセスが拒否されました',
    notFound: '要求されたリソースが見つかりません',
    serverError: 'サーバーエラー。しばらくしてから再試行してください',
    systemError: 'システムエラー。しばらくしてから再試行してください',
    csrfFail: 'セキュリティ検証に失敗しました。ページを更新してください',
    connectFail: '接続に失敗しました',
    connectFailDescription: 'サーバーに接続できません。以下を確認してください:\n1. バックエンドサービスが起動しているか\n2. ネットワーク接続\n3. APIのURL設定',
    requestConfigError: 'リクエスト設定エラー'
  }
}
