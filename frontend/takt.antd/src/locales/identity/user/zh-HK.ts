/**
 * 用戶模塊 · 繁體中文
 */
export default {
  page: {
      profile: '個人信息',
      changepasswordtitle: '修改密碼',
      password: {
        old: {
          label: '舊密碼',
          placeholder: '請輸入舊密碼'
        },
        new: {
          label: '新密碼',
          placeholder: '請輸入新密碼',
          validation: {
            format: '密碼必須為8-20位，且包含字母和數字'
          }
        },
        confirm: {
          label: '確認新密碼',
          placeholder: '請再次輸入新密碼',
          validation: {
            mismatch: '兩次輸入的密碼不一致'
          }
        }
      },
      tabs: {
        employeeinfo: '員工信息',
        userinfo: '用戶信息',
        basicinfo: '基本資料',
        accountandremark: '賬號與備註',
        permission: '權限',
        avatar: '頭像設置'
      },
      fields: {
        employee: {
          label: '關聯員工'
        },
        employeesnapshot: {
          hint: '選擇後下方展示該員工在下拉選項中的姓名與員工編碼（來自人事 options 接口）。'
        },
        employeeoptionmissing: '已綁定員工，但當前選項列表中未找到該項（可能未加載完成或員工已非在職可選範圍）。',
        employeelink: {
          label: '人事檔案',
          existing: '已有檔案',
          createnew: '新建檔案',
          createnewhint: '保存用戶時將先調用員工創建接口建立人事檔案，再創建用戶並關聯該員工（與後端 CreateUser 須先有 EmployeeId 一致）。'
        },
        employeeid: {
          placeholder: '請從員工選項列表選擇關聯員工（必填）'
        },
        nicknameplaceholder: '選填；展示用，與員工檔案姓名無關',
        usernamewhennewemployee: '新建人事檔案時，登錄名將使用保存後系統生成的員工編碼，此處無需填寫（已禁用）。',
        password: {
          label: '密碼'
        },
        formref: {
          label: '表單引用'
        },
        username: {
          validation: {
            format: '用戶名必須以小寫字母開頭，允許小寫字母和數字，不允許特殊符號，4-20位'
          }
        },
        realname: {
          validation: {
            format: '真實姓名必須為2-10個漢字'
          }
        },
        lastname: {
          validation: {
            format: '姓僅允許英文與數字，首字母必須大寫，數字不能在首位，1-100位'
          }
        },
        firstname: {
          validation: {
            format: '名僅允許英文與數字，首字母必須大寫，數字不能在首位，1-100位'
          }
        },
        nickname: {
          validation: {
            format: '暱稱格式不正確：允許中文、英文、數字、下劃線、橫線、點，1-200 位（與登錄用戶表校驗一致）'
          }
        }
      }
  }
}
