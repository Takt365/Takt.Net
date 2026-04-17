import type { IdS } from '@/types/internal/openapi-pick'

export type User = IdS<'Takt.Application.Dtos.Identity.TaktUserDto'>
export type UserQuery = IdS<'Takt.Application.Dtos.Identity.TaktUserQueryDto'>
export type UserCreate = IdS<'Takt.Application.Dtos.Identity.TaktUserCreateDto'>
export type UserUpdate = IdS<'Takt.Application.Dtos.Identity.TaktUserUpdateDto'>
export type UserStatus = IdS<'Takt.Application.Dtos.Identity.TaktUserStatusDto'>
export type UserResetPwd = IdS<'Takt.Application.Dtos.Identity.TaktUserResetPwdDto'>
export type UserChangePwd = IdS<'Takt.Application.Dtos.Identity.TaktUserChangePwdDto'>
export type UserUnlock = IdS<'Takt.Application.Dtos.Identity.TaktUserUnlockDto'>
export type UserAvatarUpdate = IdS<'Takt.Application.Dtos.Identity.TaktUserAvatarUpdateDto'>
export type UserForgotPassword = IdS<'Takt.Application.Dtos.Identity.TaktUserForgotPasswordDto'>

export type UserFormPermissionModel = Pick<UserCreate, 'roleIds' | 'deptIds' | 'postIds' | 'tenantIds'>

export type UserFormModel = Required<
  Pick<UserCreate, 'userName' | 'nickName' | 'userType' | 'userEmail' | 'userPhone' | 'userStatus' | 'remark'>
> &
  Pick<UserCreate, 'employeeId'> & { password?: string }

export type UserFormValues = UserFormModel & UserFormPermissionModel

export type UserChangePasswordFormModel = UserChangePwd & { confirmPassword?: string }
