/**
 * 코드 생성 · 한국어
 * Form labels use backend entity.gentable / entity.gentablecolumn keys
 */
export default {
  tableConfig: '테이블 설정',

  keyword: '테이블명, 엔티티 클래스 또는 업무명',

  importFromDb: '데이터베이스에서 테이블 가져오기',
  saveAs: '다른 이름으로 저장',
  saveAsPathHint: '출력 경로 입력(테이블 설정의 경로를 덮어씀):',
  saveAsPathPlaceholder: '예: D:\\Projects\\Takt.Net',
  genPath: '출력 경로',
  advancedQuery: '고급 검색',
  searchKeywordLabel: '테이블 / 엔티티 / 업무명',
  placeholderFuzzy: '퍼지 검색',

  tableName: '테이블명',
  tableComment: '테이블 설명',
  entityClassName: '엔티티 클래스',
  genModuleName: '모듈',
  genBusinessName: '업무명',
  genTemplate: '템플릿',

  generate: '생성',
  sync: '동기화',
  initialize: '초기화',

  overwriteConfirmTitle: '덮어쓰기 확인',
  overwriteConfirmContent: '다음 파일이 이미 있습니다. 덮어쓸까요?',
  overwrite: '덮어쓰기',
  saveAsCancel: '다른 이름으로 저장',

  noTableIdSync: '테이블 ID 없음, 동기화 불가',
  noTableIdPreview: '테이블 ID 없음, 미리보기 불가',
  noTableIdInit: '테이블 ID 없음, 초기화 불가',
  noPreviewData: '미리보기 없음; 테이블 설정 또는 템플릿 확인',
  syncFormHint: '편집 대화상자에서 저장하면 데이터 소스에서 필드를 새로 고칩니다.',
  cloneSuccess: '새 항목으로 복제됨; 테이블명을 변경하고 저장하세요.',
  noDataToExport: '내보낼 데이터 없음',
  exportFileName: '코드 생성 테이블 설정',
  codeGeneratedDownload: '코드가 생성되어 다운로드됨',
  genSuccessCount: ', {count}개 파일',
  existingFilesSuffix: '... 총 {count}개 파일',

  previewTitle: '코드 미리보기',
  previewEmpty: '미리보기 내용 없음',
  previewHint: '레코드를 선택한 후 미리보기를 클릭하세요.',
  previewHintDetail: '미리보기는 현재 테이블 설정과 템플릿으로 생성됩니다. 변경 후 다시 미리보기하세요.',
  previewFileEmpty: '(이 파일에 내용이 없습니다)',
  previewTabs: {
    entity: '엔티티',
    dto: 'DTO',
    service: '서비스',
    controller: '컨트롤러',
    types: '프론트 타입',
    api: 'API',
    i18n: '번역',
    view: '뷰',
    form: '폼',
    sql: '메뉴 및 번역(SQL)',
    other: '기타'
  },

  validation: {
    columnNameSnakeCase: '제 {rowNum}행: 열 이름은 snake_case(예: column_1, user_name)여야 합니다. 현재「{colName}」',
    csharpColumnPascalCase: '제 {rowNum}행: C# 열 이름은 PascalCase(예: Column1, UserName)여야 합니다. 현재「{csharpName}」'
  },

  form: {
    tabBusiness: '업무 모듈',
    tabEntity: '엔티티 및 DTO',
    tabService: '서비스 및 컨트롤러',
    tabGenerate: '생성',
    tabFront: '프론트 및 스타일',
    tabColumn: '필드 설정',
    labelCurrentProjectPath: '현재 프로젝트 경로',
    placeholderDataTableRequired: '테이블명 선택 또는 입력',
    placeholderTableName: '소문자 snake_case, 예 xxx_xxx_xxx',
    placeholderTableComment: '테이블 설명',
    placeholderNamePrefix: '프로젝트명, 기본 Takt',
    placeholderPermsPrefix: '모듈+업무로 자동, 예 accounting:controlling:standard:wage:rate',
    placeholderModule: '모듈 선택 또는 입력, 예 Generator, HumanResource.Organization',
    placeholderBusinessFromTable: '테이블명에서 자동',
    placeholderBusinessManual: '엔티티/서비스/컨트롤러명용, 예 Setting, Dept',
    placeholderFunctionName: '테이블 설명에서 자동, 읽기 전용',
    placeholderAutoByModule: '모듈에서 자동',
    placeholderAutoByBusiness: '업무명에서 자동',
    placeholderGenPathSlash: '/',
    placeholderCurrentProjectPathLoading: '로딩 중…',
    placeholderCurrentProjectPathHint: '"현재 프로젝트" 선택하여 로드',
    placeholderParentMenu: '상위 메뉴 선택(비움=루트)',
    placeholderAuthor: '현재 사용자',
    placeholderDbType: 'DB 타입',
    placeholderCsharpType: 'C# 타입',
    placeholderQueryType: '쿼리 타입',
    placeholderHtmlType: '표시 타입',
    placeholderDictType: '사전 타입 선택',
    emptySaveTableFirst: '필드 관리를 위해 먼저 테이블 설정을 저장하세요',
    emptyNoColumnData: '필드 데이터 없음'
  }
}
