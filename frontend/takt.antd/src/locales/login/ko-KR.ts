/**
 * 로그인 페이지 · 한국어
 * - 구조: fields, login, forgot, sign (zh-CN과 동일)
 */
export default {
  /** 필드: label, placeholder, validation — 세 페이지 공용 */
  fields: {
    username: {
      label: '사용자 이름',
      placeholder: '사용자 이름 입력',
      validation: {
        required: '사용자 이름을 입력하세요',
        min: '사용자 이름은 3자 이상',
        max: '사용자 이름은 20자 이내'
      }
    },
    password: {
      label: '비밀번호',
      placeholder: '비밀번호 입력',
      validation: {
        required: '비밀번호를 입력하세요',
        min: '비밀번호는 6자 이상',
        max: '비밀번호는 50자 이내'
      }
    },
    confirmPassword: {
      label: '비밀번호 확인',
      placeholder: '비밀번호 다시 입력',
      validation: {
        required: '비밀번호를 다시 입력하세요',
        mismatch: '비밀번호가 일치하지 않습니다'
      }
    },
    userEmail: {
      label: '이메일',
      placeholder: '이메일 입력',
      placeholderForgot: '이메일 주소 입력',
      validation: {
        required: '이메일을 입력하세요',
        format: '올바른 이메일 형식을 입력하세요'
      }
    },
    realName: {
      label: '실명',
      placeholder: '실명 입력',
      validation: {
        required: '실명을 입력하세요',
        max: '실명은 50자 이내'
      }
    },
    userPhone: {
      label: '휴대폰 번호',
      placeholder: '휴대폰 번호 입력',
      validation: {
        required: '휴대폰 번호를 입력하세요',
        format: '올바른 휴대폰 번호를 입력하세요'
      }
    },
    captcha: {
      validation: {
        required: '보안 인증을 완료해 주세요',
        typeRequired: '서버에서 캡차 유형이 반환되지 않았습니다. 백엔드 구성을 확인하세요'
      }
    }
  },

  /** 메인 로그인 (비밀번호 찾기 링크는 forgot.title, 회원가입은 sign.title) */
  login: {
    title: '로그인',
    rememberMe: '로그인 상태 유지',
    login: '로그인',
    logout: '로그아웃',
    noAccountRegister: 'Takt365가 처음이신가요? {register}'
  },

  /** 비밀번호 찾기 */
  forgot: {
    title: '비밀번호 찾기',
    subtitle: '이메일 주소를 입력하시면 비밀번호 재설정 메일을 보내드립니다',
    submit: '비밀번호 재설정',
    backToLogin: '로그인으로 돌아가기',
    success: '재설정 메일을 보냈습니다. 받은편지함을 확인하세요',
    fail: '메일 발송에 실패했습니다. 이메일 주소를 확인하세요',
    emailNotRegistered: '해당 이메일 주소는 등록되어 있지 않습니다. 확인해 주세요',
    protectedUser: '이 계정은 비밀번호 찾기를 허용하지 않습니다'
  },

  /** 회원가입 (title = 제목·버튼·noAccountRegister의 {register}) */
  sign: {
    title: '회원가입',
    subtitle: '계정을 만들고 시작하세요',
    hasAccount: '이미 계정이 있으신가요? 로그인',
    success: '가입 완료. 로그인해 주세요',
    successInitialPassword: '회원가입 성공. 초기 비밀번호 메일을 확인해 주세요.',
    fail: '가입에 실패했습니다'
  }
}
