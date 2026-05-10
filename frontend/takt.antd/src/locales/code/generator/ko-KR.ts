/**
 * 코드 생성 · 한국어
 *约定：`export default { page: { … } }`，运行时键为 code.generator.page.*。
 * 列名等：entity.gentable.*；通用：common.*
 */
export default {
  page: {
    importfromdb: '데이터베이스에서 테이블 가져오기',
    saveas: '다른 이름으로 저장',
    saveaspathhint: '생성 경로를 입력하세요(테이블 설정의 경로를 덮어씁니다):',
    saveaspathplaceholder: '예: D:\\Projects\\Takt.Net',
    overwriteconfirmtitle: '덮어쓰기 확인',
    overwriteconfirmcontent: '대상 경로에 다음 파일이 이미 존재합니다. 덮어쓰시겠습니까?',
    overwrite: '덮어쓰기',
    saveascancel: '다른 이름으로 저장',
    notableidsync: '이 레코드에는 테이블 ID가 없어 동기화할 수 없습니다',
    notableidinit: '이 레코드에는 테이블 ID가 없어 초기화할 수 없습니다',
    syncformhint: '편집 대화상자에서 저장하여 데이터 소스에서 필드 설정을 새로 고침하십시오. 전체 동기화에는 백엔드 인터페이스가 필요합니다',
    clonesuccess: '새로 만들기로 복사되었습니다. 테이블 이름을 수정한 후 저장하세요',
    nodataexport: '내보낼 데이터가 없습니다',
    codegenerateddownload: '코드가 생성되어 다운로드되었습니다',
    gensuccesscount: ', 총 {count}개 파일',
    existingfilessuffix: '등 총 {count}개 파일',
    notableidpreview: '이 레코드에는 테이블 ID가 없어 미리볼 수 없습니다',
    importtable: {
      datatable: '데이터 테이블'
    },
    form: {
      tab: {
        table: '테이블 설정',
        column: '필드 설정',
        basic: '기본 정보',
        business: '비즈니스 모듈',
        entitydto: '엔티티 및 DTO',
        service: '서비스 및 컨트롤러',
        generate: '생성',
        front: '프론트엔드 및 스타일'
      },
      placeholder: {
        configid: '데이터 소스를 선택하세요',
        tablenamenew: '소문자 스네이크 케이스, 예: xxx_xxx_xxx',
        tablenameedit: '먼저 데이터 소스를 선택하세요',
        tablecomment: '테이블 주석',
        gentemplatecategory: '생성 템플릿 유형을 선택하세요',
        indatabase: 'DB 테이블 여부를 선택하세요',
        subtablename: '부모 테이블을 선택하세요',
        subtablefkname: '외래 키 열을 선택하세요',
        treecode: '트리 코드 열을 선택하세요',
        treeparentcode: '트리 부모 코드 열을 선택하세요',
        treename: '트리 이름 열을 선택하세요',
        nameprefix: '프로젝트 이름, 기본값 Takt, 수정 후 모든 네임스페이스 동기화',
        permsprefix: '모듈 이름+비즈니스 이름에서 자동 생성, 예: accounting:controlling:standard:wage:rate',
        genmodulename: '모듈(디렉토리)을 선택하거나 수동 입력, 예: Generator、HumanResource.Organization',
        genbusinessnamefromtable: '테이블 이름에서 자동 생성',
        genbusinessnamemanual:
          '엔티티/서비스/컨트롤러 등의 클래스 이름 및 인터페이스 주석에 사용, 예: 설정, 부서',
        genfunctionname: '테이블 설명에서 자동 가져오기, 읽기 전용',
        autofrommodule: '모듈 이름에서 자동 생성',
        autofrombusiness: '비즈니스 이름에서 자동 생성',
        genmethod: '생성 방법을 선택하세요',
        genpath: '/',
        currentprojectloading: '가져오는 중…',
        currentprojectidle: '생성 방법을 현재 프로젝트로 선택 후 자동 가져오기',
        parentmenuid: '상위 메뉴를 선택하세요(선택하지 않으면 루트)',
        sorttype: '정렬 유형을 선택하세요',
        sortfield: '정렬 필드를 선택하세요',
        frontui: '프론트엔드 UI 프레임워크를 선택하세요',
        frontformlayout: '프론트엔드 폼 레이아웃을 선택하세요',
        frontbtnstyle: '프론트엔드 버튼 스타일을 선택하세요',
        genauthor: '현재 로그인 사용자',
        columndbtype: 'DB 유형',
        columncsharptype: 'C# 유형',
        columnquerytype: '쿼리 방식',
        columnhtmltype: '표시 유형',
        columndicttype: '사전 유형 선택'
      },
      rules: {
        tablecomment: '테이블 설명을 입력하세요',
        gentemplatecategory: '생성 템플릿 유형을 선택하세요',
        permsprefix: '권한 접두사를 입력하세요',
        menubuttongroup: '메뉴 버튼 그룹을 선택하세요',
        genmodulename: '모듈 이름을 선택하세요',
        genbusinessname: '비즈니스 이름을 입력하세요',
        entityclassname: '엔티티 클래스 이름을 입력하세요',
        dtoclassname: 'Dto 클래스 이름을 입력하세요',
        iserviceclassname: '서비스 인터페이스 클래스 이름을 입력하세요',
        serviceclassname: '서비스 클래스 이름을 입력하세요',
        controllerclassname: '컨트롤러 클래스 이름을 입력하세요',
        isrepository: '저장소 생성 여부를 선택하세요',
        genmethod: '생성 방법을 선택하세요',
        isgentranslation: '번역 생성 여부를 선택하세요',
        frontformlayout: '프론트엔드 폼 레이아웃을 선택하세요',
        isusetabs: '탭 사용 여부를 선택하세요',
        genauthor: '작성자를 입력하세요',
        sorttype: '정렬 유형을 선택하세요',
        sortfield: '정렬 필드를 선택하세요',
        tablename: '데이터 테이블 이름을 선택하거나 입력하세요',
        nameprefix: '네임스페이스 접두사를 입력하세요',
        subtablename: '연관 부모 테이블을 선택하세요',
        subtablefkname: '연관 외래 키를 선택하세요',
        treecode: '트리 코드 필드를 선택하세요',
        treename: '트리 이름 필드를 선택하세요',
        treeparentcode: '트리 부모 코드를 선택하세요',
        genpath: '생성 경로를 입력하세요',
        parentmenuid: '상위 메뉴를 선택하세요',
        repositoryinterfacenamespace: '저장소 인터페이스 네임스페이스를 입력하세요',
        irepositoryclassname: '저장소 인터페이스 클래스 이름을 입력하세요',
        repositorynamespace: '저장소 네임스페이스를 입력하세요',
        repositoryclassname: '저장소 클래스 이름을 입력하세요',
        tabsfieldcount: '탭 필드 수를 입력하세요'
      },
      validation: {
        tablenameformat: '데이터 테이블 이름은 소문자 스네이크 케이스여야 합니다. 예: xxx_xxx_xxx',
        nameprefixpascal: '파스칼 표기법이어야 합니다. 예: Takt(첫 글자 대문자, 영문자 및 숫자만)',
        columnsnake: '필드 설정 {row}행: 열 이름은 스네이크 케이스여야 합니다(예: column_1, user_name), 현재: 「{value}」',
        columnpascal: '필드 설정 {row}행: C# 열 이름은 파스칼 케이스여야 합니다(예: Column1, UserName), 현재: 「{value}」'
      },
      column: {
        addRow: '행 추가',
        delete: '삭제',
        emptySaveFirst: '먼저 테이블 설정을 저장한 후 필드를 관리하세요',
        emptyNoData: '필드 데이터가 없습니다',
        dragsort: '드래그 정렬'
      }
    },
    preview: {
      loading: '미리보기 파일 목록을 로드하는 중...',
      empty: '미리보기 콘텐츠가 없습니다',
      emptyhint: '현재 백엔드는 생성 파일 경로 및 덮어쓰기 정보만 반환하며, 소스 코드 콘텐츠는 아직 반환되지 않았습니다.',
      validationissuetitle: '템플릿 검증에서 {count}개의 문제가 발견되었습니다',
      validationissuetoast: '템플릿 검증에서 {count}개의 문제가 발견되었습니다. 먼저 수정한 후 생성하세요',
      exists: '이미 존재',
      loadfail: '미리보기 로드 실패',
      pathcontent: '대상 경로: {path}',
      tab: {
        backend: '백엔드',
        frontend: '프론트엔드',
        script: '스크립트'
      },
      category: {
        backend: {
          entity: '엔티티 Entities',
          dto: 'DTO',
          service: '서비스 인터페이스/구현',
          controller: '컨트롤러',
          other: '기타'
        },
        frontend: {
          api: 'API',
          type: '타입 정의',
          view: '목록 보기',
          component: '하위 컴포넌트',
          other: '기타'
        },
        script: {
          translationsql: '번역 SQL',
          menusql: '메뉴 SQL',
          other: '기타'
        }
      }
    }
  }
}
