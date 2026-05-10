/**
 * Common translations · 한국어
 * Same structure and key order as zh-CN (alphabetical for button)
 */
export default {
  page: {
    /** <summary>1. app</summary> */
      app: {
        htmltitle: 'Takt Digital Factory',
        name: 'Takt Digital Factory (TDF) ',
        productcode: 'TDF-MES-PRO',
        slogan: '미래를 이끌다',
        tagline: '실용·간결·유연'
      },
    
      /** <summary>2. settings</summary> */
      settings: {
        color: {
          blue: '파랑',
          cyan: '청록',
          gold: '금색',
          green: '초록',
          orange: '주황',
          pink: '분홍',
          purple: '보라',
          red: '빨강',
          switch: '색상 전환',
          title: '색상'
        },
        locale: {
          'ar-sa': 'العربية',
          'en-us': 'English',
          'es-es': 'Español',
          'fr-fr': 'Français',
          'ja-jp': '日本語',
          'ko-kr': '한국어',
          'ru-ru': 'Русский',
          'zh-cn': '간체 중국어',
          'zh-tw': '번체 중국어',
          switch: '언어 전환',
          title: '언어'
        },
        theme: {
          dark: '다크',
          light: '라이트',
          switch: '테마 전환',
          title: '테마'
        }
      },
    
      /** Layout position */
      layout: {
        position: {
          left: '왼쪽',
          center: '가운데',
          right: '오른쪽'
        }
      },
    
      /** <summary>3. button</summary> Alphabetical order as zh-CN */
      button: {
        addsign: '추가 서명',
        advancedoptions: '고급 옵션',
        advancedquery: '고급 검색',
        advancedsettings: '고급 설정',
        allocate: '할당',
        approve: '승인',
        archive: '보관',
        back: '뒤로',
        cancel: '취소',
        changepwd: '비밀번호 변경',
        change: '변경',
        checkall: '전체 선택',
        clean: '비우기',
        clone: '복제',
        collapse: '접기',
        columnsetting: '열 설정',
        comment: '댓글',
        config: '설정',
        confirm: '확인',
        copy: '복사',
        countersign: '합의',
        create: '추가',
        createrow: '행 추가',
        datasource: '데이터 소스',
        delegate: '위임',
        delete: '삭제',
        design: '설계',
        detail: '상세',
        disable: '비활성화',
        download: '다운로드',
        draft: '초안',
        edit: '수정',
        enable: '활성화',
        empty: '비우기',
        empty30d: '30일 비우기',
        empty7d: '7일 비우기',
        emptyall: '전체 비우기',
        expand: '펼치기',
        exitfullscreen: '전체 화면 나가기',
        export: '내보내기',
        favorite: '즐겨찾기',
        field: '필드 관리',
        fixed: '고정',
        forward: '전달',
        formdata: '폼 데이터',
        fullscreen: '전체 화면',
        generate: '생성',
        history: '이력',
        import: '가져오기',
        initialize: '초기화',
        like: '좋아요',
        logout: '로그아웃',
        markread: '읽음으로 표시',
        more: '더보기',
        no: '아니오',
        ok: '확인',
        password: '비밀번호',
        permission: '권한 설정',
        personalsettings: '개인 설정',
        preview: '미리보기',
        print: '인쇄',
        preferences: '선호',
        profile: '프로필',
        progress: '진행',
        publish: '게시',
        query: '조회',
        read: '읽음',
        refresh: '새로고침',
        reply: '답장',
        reset: '초기화',
        resume: '재개',
        return: '반환',
        revoke: '취소',
        run: '실행',
        send: '보내기',
        share: '공유',
        sign: '서명',
        start: '시작',
        startflow: '플로우 시작',
        stop: '중지',
        sync: '동기화',
        subsign: '서명 제거',
        submit: '제출',
        suspend: '일시 중지',
        template: '템플릿',
        terminate: '종료',
        theme: '테마 설정',
        tolist: '목록',
        totranspose: '전치',
        transfer: '이관',
        truncate: '잘라내기',
        unfavorite: '즐겨찾기 해제',
        uncomment: '댓글 취소',
        unflagging: '신고 취소',
        unfollow: '팔로우 취소',
        unshare: '공유 해제',
        unlike: '좋아요 취소',
        unlock: '잠금 해제',
        unread: '안 읽음',
        uncheckall: '선택 해제',
        update: '갱신',
        upload: '업로드',
        urge: '촉구',
        validate: '검증',
        version: '버전',
        yes: '예'
      },
    
      /** <summary>4. entity</summary> */
      entity: {
        configid: '설정 ID',
        createby: '생성자',
        createtime: '생성 일시',
        createdbyid: '생성자 ID',
        deletedby: '삭제자',
        deletedbyid: '삭제자 ID',
        deletedtime: '삭제 일시',
        extfieldjson: '확장 필드 JSON',
        id: 'ID',
        isdeleted: '삭제됨',
        remark: '비고',
        updateby: '수정자',
        updatetime: '수정 일시',
        updatedbyid: '수정자 ID'
      },
    
      /** <summary>5. msg</summary> */
      msg: {
        actionfail: '{action}에 실패했습니다',
        actionsuccess: '{action}되었습니다',
        assignfail: '{target} 할당에 실패했습니다',
        assignsuccess: '{target}이(가) 할당되었습니다',
        createsuccess: '생성되었습니다',
        deletefail: '삭제에 실패했습니다',
        deletesuccess: '삭제되었습니다',
        entityidrequired: '{entity} ID가 필요합니다',
        entitynotfound: '{entity}을(를) 찾을 수 없습니다',
        exportfail: '내보내기에 실패했습니다',
        exportsuccess: '내보내기되었습니다',
        loadfail: '데이터를 불러오지 못했습니다',
        loadoptionsfail: '옵션을 불러오지 못했습니다. 나중에 다시 시도하세요',
        loadtargetfail: '{target}을(를) 불러오지 못했습니다',
        nosearchresult: '검색 결과가 없습니다',
        operatefail: '작업에 실패했습니다',
        updatesuccess: '수정되었습니다'
      },
    
      /** <summary>6. action</summary> */
      action: {
        confirmaction: '{action} 확인',
        confirmdelete: '삭제 확인',
        etc: '등',
        exportdatasuffix: '데이터',
        import: {
          hint: 'Excel(.xlsx) 가져오기 지원. 가져오기당 최대 1000건.',
          sheetnametemplate: '{entity} 가져오기 템플릿',
          templatetext: '{entity} 가져오기 템플릿 다운로드',
          uploadtext: '클릭하거나 Excel 파일을 여기로 드래그하여 업로드'
        },
        management: '관리',
        operation: '작업',
        or: '또는',
        superrole: '슈퍼 역할',
        superuser: '슈퍼 사용자',
        tabtargetallocation: '{target} 할당',
        thistarget: '이 {target}',
        transferassigned: '할당됨',
        transferunassigned: '미할당',
        warnaction: {},
        warnselecttoaction: '{action}할 {entity}을(를) 선택하세요',
        warnsubjectcannot: '{subject}은(는) {action}(을)를 할 수 없습니다',
        warnusercannot: '사용자 {name}(은)는 {action}(을)를 할 수 없습니다'
      },
    
      /** <summary>7. form</summary> */
      form: {
        tabs: {
          basicinfo: '기본 정보',
          announcementbody: '본문',
          announcementpublish: '게시 설정',
          announcementother: '기타'
        },
        placeholder: {
          copyright: '저작권 정보 입력',
          keyword: '키워드',
          ordernumhint: '값이 작을수록 앞에 표시',
          required: '{field}을(를) 입력하세요',
          requiredagain: '{field}을(를) 다시 입력하세요',
          search: '{keyword} 입력',
          searchkeyword: '키워드로 검색하세요',
          searchmenu: '메뉴, 페이지 검색...',
          select: '{field}을(를) 선택하세요',
          selectfirst: '먼저 {field}을(를) 선택하세요',
          selectonly: '선택하세요',
          treekeyword: '트리 키워드',
          watermark: '워터마크 내용 입력'
        }
      },
    
      /** <summary>8. confirm</summary> */
      confirm: {
        deletecountentity: '선택한 {count}개의 {entity}(을)를 삭제하시겠습니까?',
        deleteentity: '{entity} "{name}"(을)를 삭제하시겠습니까?',
        resetpwdcontent: '{entity} "{name}"의 비밀번호를 기본값으로 재설정하시겠습니까?',
        unlockcontent: '{entity} "{name}"의 잠금을 해제하시겠습니까?'
      },
      api: {
        loginexpired: '로그인이 만료되었습니다. 다시 로그인해 주세요',
        tokenrefreshfail: '토큰 새로 고침 실패',
        redirectingtologin: '로그인 페이지로 이동 중',
        tokenrefreshfailonloginpage: '토큰 새로 고침 실패, 이미 로그인 페이지에 있습니다',
        requestfail: '요청에 실패했습니다',
        forbidden: '액세스가 거부되었습니다',
        notfound: '요청한 리소스를 찾을 수 없습니다',
        servererror: '서버 오류. 나중에 다시 시도해 주세요',
        systemerror: '시스템 오류. 나중에 다시 시도해 주세요',
        csrffail: '보안 검증에 실패했습니다. 페이지를 새로 고침해 주세요',
        connectfail: '연결에 실패했습니다',
        connectfaildescription: '서버에 연결할 수 없습니다. 다음을 확인하세요:\n1. 백엔드 서비스 실행 여부\n2. 네트워크 연결\n3. API 주소 설정',
        requestconfigerror: '요청 설정 오류'
      }
  }
}
