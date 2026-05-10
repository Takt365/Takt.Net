// ========================================
// 命名空间：@/types/identity/user-form-view
// 功能描述：用户维护弹窗视图层类型（手写静态文件）。
// 勿合并进 user.d.ts：`npm run gen:contracts` / emit-type-shims 会覆盖同目录下的 user.d.ts。
// ========================================

/**
 * `user-form.vue` 中 `formState`；`password` 为前端明文，提交时由父组件映射为 `UserCreate.passwordHash`。
 */
export interface UserFormModel {
  employeeId: string
  userName: string
  nickName: string
  userType: number
  userEmail: string
  userPhone: string
  password: string
  userStatus: number
  remark: string
}

/** `user-form.vue` 中 `permissionState`。 */
export interface UserFormPermissionModel {
  roleIds: string[]
  deptIds: string[]
  postIds: string[]
  tenantIds: string[]
}

/** `UserForm.getValues()`：`UserFormModel` + `UserFormPermissionModel`。 */
export type UserFormValues = UserFormModel & UserFormPermissionModel
