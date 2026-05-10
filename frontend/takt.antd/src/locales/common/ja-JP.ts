/**
 * Common translations · 日本語
 * Same structure and key order as zh-CN (alphabetical for button)
 */
export default {
  page: {
    /** <summary>1. app</summary> */
      app: {
        htmltitle: 'Takt Digital Factory',
        name: 'Takt Digital Factory (TDF) ',
        productcode: 'TDF-MES-PRO',
        slogan: '未来を駆動',
        tagline: '実用·シンプル·柔軟'
      },
    
      /** <summary>2. settings</summary> */
      settings: {
        color: {
          blue: '青',
          cyan: 'シアン',
          gold: 'ゴールド',
          green: '緑',
          orange: 'オレンジ',
          pink: 'ピンク',
          purple: '紫',
          red: '赤',
          switch: '色を切り替え',
          title: '色'
        },
        locale: {
          'ar-sa': 'العربية',
          'en-us': 'English',
          'es-es': 'Español',
          'fr-fr': 'Français',
          'ja-jp': '日本語',
          'ko-kr': '한국어',
          'ru-ru': 'Русский',
          'zh-cn': '簡体中文',
          'zh-tw': '繁体中文',
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
        advancedoptions: '詳細オプション',
        advancedquery: '詳細検索',
        advancedsettings: '詳細設定',
        allocate: '割り当て',
        approve: '承認',
        archive: 'アーカイブ',
        back: '戻る',
        cancel: 'キャンセル',
        changepwd: 'パスワード変更',
        change: '変更',
        checkall: 'すべて選択',
        clean: 'クリア',
        clone: 'クローン',
        collapse: '折りたたみ',
        columnsetting: '列設定',
        comment: 'コメント',
        config: '設定',
        confirm: '確認',
        copy: 'コピー',
        countersign: '合議',
        create: '新規',
        createrow: '行を追加',
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
        emptyall: 'すべてクリア',
        expand: '展開',
        exitfullscreen: '全画面終了',
        export: 'エクスポート',
        favorite: 'お気に入り',
        field: 'フィールド管理',
        fixed: '固定',
        forward: '転送',
        formdata: 'フォームデータ',
        fullscreen: '全画面',
        generate: '生成',
        history: '履歴',
        import: 'インポート',
        initialize: '初期化',
        like: 'いいね',
        logout: 'ログアウト',
        markread: '既読にする',
        more: 'もっと',
        no: 'いいえ',
        ok: 'OK',
        password: 'パスワード',
        permission: '権限設定',
        personalsettings: '個人設定',
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
        startflow: 'フロー開始',
        stop: '停止',
        sync: '同期',
        subsign: '减签',
        submit: '送信',
        suspend: '一時停止',
        template: 'テンプレート',
        terminate: '終了',
        theme: 'テーマ設定',
        tolist: 'リスト',
        totranspose: '転置',
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
        uncheckall: '選択解除',
        update: '更新',
        upload: 'アップロード',
        urge: '督促',
        validate: '検証',
        version: 'バージョン',
        yes: 'はい'
      },
    
      /** <summary>4. entity</summary> */
      entity: {
        configid: '設定ID',
        createby: '作成者',
        createtime: '作成日時',
        createdbyid: '作成者ID',
        deletedby: '削除者',
        deletedbyid: '削除者ID',
        deletedtime: '削除日時',
        extfieldjson: '拡張フィールドJSON',
        id: 'ID',
        isdeleted: '削除済み',
        remark: '備考',
        updateby: '更新者',
        updatetime: '更新日時',
        updatedbyid: '更新者ID'
      },
    
      /** <summary>5. msg</summary> */
      msg: {
        actionfail: '{action}に失敗しました',
        actionsuccess: '{action}しました',
        assignfail: '{target}の割り当てに失敗しました',
        assignsuccess: '{target}を割り当てました',
        createsuccess: '作成しました',
        deletefail: '削除に失敗しました',
        deletesuccess: '削除しました',
        entityidrequired: '{entity}IDが必要です',
        entitynotfound: '{entity}が見つかりません',
        exportfail: 'エクスポートに失敗しました',
        exportsuccess: 'エクスポートしました',
        loadfail: 'データの読み込みに失敗しました',
        loadoptionsfail: 'オプションの読み込みに失敗しました。しばらくして再試行してください',
        loadtargetfail: '{target}の読み込みに失敗しました',
        nosearchresult: '検索結果がありません',
        operatefail: '操作に失敗しました',
        updatesuccess: '更新しました'
      },
    
      /** <summary>6. action</summary> */
      action: {
        confirmaction: '{action}の確認',
        confirmdelete: '削除の確認',
        etc: 'など',
        exportdatasuffix: 'データ',
        import: {
          hint: 'Excel（.xlsx）のインポートに対応。1回のインポートは最大1000件。',
          sheetnametemplate: '{entity}インポートテンプレート',
          templatetext: '{entity}インポートテンプレートをダウンロード',
          uploadtext: 'クリックまたはExcelファイルをここにドラッグしてアップロード'
        },
        management: '管理',
        operation: '操作',
        or: 'または',
        superrole: 'スーパーロール',
        superuser: 'スーパーユーザー',
        tabtargetallocation: '{target}割り当て',
        thistarget: 'この{target}',
        transferassigned: '割り当て済み',
        transferunassigned: '未割り当て',
        warnaction: {},
        warnselecttoaction: '{action}する{entity}を選択してください',
        warnsubjectcannot: '{subject}は{action}できません',
        warnusercannot: 'ユーザー {name} は{action}できません'
      },
    
      /** <summary>7. form</summary> */
      form: {
        tabs: {
          basicinfo: '基本情報',
          announcementbody: '本文',
          announcementpublish: '公開設定',
          announcementother: 'その他'
        },
        placeholder: {
          copyright: '著作権情報を入力',
          keyword: 'キーワード',
          ordernumhint: '値が小さいほど前に表示',
          required: '{field}を入力してください',
          requiredagain: '{field}を再入力してください',
          search: '{keyword}を入力',
          searchkeyword: 'キーワードを入力して検索',
          searchmenu: 'メニュー・ページを検索...',
          select: '{field}を選択してください',
          selectfirst: '先に{field}を選択してください',
          selectonly: '選択してください',
          treekeyword: 'ツリーキーワード',
          watermark: '透かし文字を入力'
        }
      },
    
      /** <summary>8. confirm</summary> */
      confirm: {
        deletecountentity: '選択した{count}件の{entity}を削除してもよろしいですか？',
        deleteentity: '{entity}「{name}」を削除してもよろしいですか？',
        resetpwdcontent: '{entity}「{name}」のパスワードをデフォルトにリセットしてもよろしいですか？',
        unlockcontent: '{entity}「{name}」のロックを解除してもよろしいですか？'
      },
      api: {
        loginexpired: 'ログインの有効期限が切れました。再度ログインしてください',
        tokenrefreshfail: 'Token の更新に失敗しました',
        redirectingtologin: 'ログインページにリダイレクトしています',
        tokenrefreshfailonloginpage: 'Token の更新に失敗しました。既にログインページにいます',
        requestfail: 'リクエストに失敗しました',
        forbidden: 'アクセスが拒否されました',
        notfound: '要求されたリソースが見つかりません',
        servererror: 'サーバーエラー。しばらくしてから再試行してください',
        systemerror: 'システムエラー。しばらくしてから再試行してください',
        csrffail: 'セキュリティ検証に失敗しました。ページを更新してください',
        connectfail: '接続に失敗しました',
        connectfaildescription: 'サーバーに接続できません。以下を確認してください:\n1. バックエンドサービスが起動しているか\n2. ネットワーク接続\n3. APIのURL設定',
        requestconfigerror: 'リクエスト設定エラー'
      }
  }
}
