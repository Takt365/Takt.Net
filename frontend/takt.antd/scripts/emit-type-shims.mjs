/**
 * 根据 openapi 分组与 DTO 命名约定，生成 src/types 下各模块的薄类型别名（非手写 DTO 字段）。
 * 运行：node ./scripts/emit-type-shims.mjs
 * 全量契约：`npm run gen:contracts`（openapi-typescript → 本脚本），纯 Node，无需 Java。
 */
import fs from 'node:fs'
import path from 'node:path'
import { fileURLToPath } from 'node:url'

const __dirname = path.dirname(fileURLToPath(import.meta.url))
const root = path.resolve(__dirname, '..')
const typesDir = path.join(root, 'src/types')

/** @type {Record<string, string>} path -> OpenAPI 命名空间前缀（不含末尾类型名） */
const NS_BY_PREFIX = [
  ['human-resource/personnel/', 'Takt.Application.Dtos.HumanResource.Personnel.'],
  ['human-resource/organization/', 'Takt.Application.Dtos.HumanResource.Organization.'],
  ['human-resource/attendance-leave/', 'Takt.Application.Dtos.HumanResource.AttendanceLeave.'],
  ['identity/', 'Takt.Application.Dtos.Identity.'],
  ['generator/', 'Takt.Application.Dtos.Code.Generator.'],
  ['accounting/controlling/', 'Takt.Application.Dtos.Accounting.Controlling.'],
  ['accounting/financial/', 'Takt.Application.Dtos.Accounting.Financial.'],
  ['accounting/', 'Takt.Application.Dtos.Accounting.'],
  ['logistics/materials/', 'Takt.Application.Dtos.Logistics.Materials.'],
  ['logistics/material/', 'Takt.Application.Dtos.Logistics.Materials.'],
  ['logistics/maintenance/', 'Takt.Application.Dtos.Logistics.Maintenance.'],
  ['routine/tasks/dict/', 'Takt.Application.Dtos.Routine.Tasks.Dict.'],
  ['routine/tasks/i18n/', 'Takt.Application.Dtos.Routine.Tasks.I18n.'],
  ['routine/tasks/numbering-rule', 'Takt.Application.Dtos.Routine.Tasks.NumberingRule.'],
  ['routine/tasks/setting', 'Takt.Application.Dtos.Routine.Tasks.Setting.'],
  ['routine/tasks/file', 'Takt.Application.Dtos.Routine.Tasks.Files.'],
  ['routine/tasks/signalr/', 'Takt.Application.Dtos.Routine.Tasks.SignalR.'],
  ['routine/tasks/wordfilter', 'Takt.Application.Dtos.Routine.Tasks.WordFilter.'],
  ['routine/business/announcement', 'Takt.Application.Dtos.Routine.Business.Announcement.'],
  ['statistics/logging/', 'Takt.Application.Dtos.Statistics.Logging.'],
  ['workflow/', 'Takt.Application.Dtos.Workflow.']
]

/** @type {Record<string, 'AccS'|'GenS'|'IdS'|'LogS'|'LgsS'|'OrgS'|'RouS'|'WfS'>} */
const PICK = {
  accounting: 'AccS',
  generator: 'GenS',
  identity: 'IdS',
  'human-resource': 'OrgS',
  logistics: 'LgsS',
  routine: 'RouS',
  statistics: 'LogS',
  workflow: 'WfS'
}

function bundleFor(relPath) {
  const seg = relPath.split('/')[0]
  if (seg === 'human-resource') return 'OrgS'
  if (PICK[seg]) return PICK[seg]
  return 'OrgS'
}

function nsFor(relPath) {
  for (const [p, ns] of NS_BY_PREFIX) {
    if (relPath.startsWith(p)) return ns
  }
  throw new Error(`No NS rule for path: ${relPath}`)
}

/** 全量 schema 键覆盖（path::exportName 或仅 exportName） */
const SCHEMA_OVERRIDE = {
  // routine setting：后端为 TaktSettings* 而非 TaktSetting*
  'routine/tasks/setting::Setting': 'Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsDto',
  'routine/tasks/setting::SettingCreate': 'Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsCreateDto',
  'routine/tasks/setting::SettingUpdate': 'Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsUpdateDto',
  'routine/tasks/setting::SettingQuery': 'Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsQueryDto',
  'routine/tasks/setting::SettingStatus': 'Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsStatusDto',
  // workflow
  'workflow/instance::FlowStartRequest': 'Takt.Application.Dtos.Workflow.TaktFlowStartDto',
  'workflow/scheme::FlowSchemeCreateOrUpdate': 'Takt.Application.Dtos.Workflow.TaktFlowSchemeUpdateDto',
  'workflow/form::FlowFormStatusUpdate': 'Takt.Application.Dtos.Workflow.TaktFlowFormStatusDto',
  'workflow/scheme::FlowSchemeStatusUpdate': 'Takt.Application.Dtos.Workflow.TaktFlowSchemeStatusDto',
  // translation 转置
  'routine/tasks/i18n/translation::TranslationTransposed':
    'Takt.Application.Dtos.Routine.Tasks.I18n.TaktTranslationTransposedDto',
  'routine/tasks/i18n/translation::TranslationTransposedResult':
    'Takt.Application.Dtos.Routine.Tasks.I18n.TaktTranslationTransposedResult'
}

function defaultSchemaKey(relPath, exportName) {
  if (relPath === 'routine/tasks/wordfilter') {
    return `${nsFor(relPath)}${exportName}`
  }
  if (exportName.endsWith('Dto')) {
    return `${nsFor(relPath)}Takt${exportName}`
  }
  return `${nsFor(relPath)}Takt${exportName}Dto`
}

function schemaKey(relPath, exportName) {
  const k1 = `${relPath}::${exportName}`
  if (SCHEMA_OVERRIDE[k1]) return SCHEMA_OVERRIDE[k1]
  if (SCHEMA_OVERRIDE[exportName]) return SCHEMA_OVERRIDE[exportName]
  return defaultSchemaKey(relPath, exportName)
}

/** 自 collect-type-imports 汇总（export 名） */
const MODULE_EXPORTS = {
  'accounting/controlling/cost-center': [
    'CostCenter',
    'CostCenterQuery',
    'CostCenterCreate',
    'CostCenterUpdate',
    'CostCenterStatus'
  ],
  'accounting/financial/title': [
    'AccountingTitle',
    'AccountingTitleTree',
    'AccountingTitleQuery',
    'AccountingTitleCreate',
    'AccountingTitleUpdate',
    'AccountingTitleStatus'
  ],
  'generator/table': ['GenTable', 'GenTableCreate', 'GenTableQuery', 'GenTableUpdate'],
  'generator/table-column': ['GenTableColumn', 'GenTableColumnCreate', 'GenTableColumnQuery', 'GenTableColumnUpdate'],
  'human-resource/attendance-leave/attendance-correction': [
    'AttendanceCorrection',
    'AttendanceCorrectionCreate',
    'AttendanceCorrectionQuery',
    'AttendanceCorrectionUpdate'
  ],
  'human-resource/attendance-leave/attendance-device': [
    'AttendanceDevice',
    'AttendanceDeviceCreate',
    'AttendanceDeviceQuery',
    'AttendanceDeviceUpdate'
  ],
  'human-resource/attendance-leave/attendance-exception': [
    'AttendanceException',
    'AttendanceExceptionCreate',
    'AttendanceExceptionQuery',
    'AttendanceExceptionUpdate'
  ],
  'human-resource/attendance-leave/attendance-punch': [
    'AttendancePunch',
    'AttendancePunchCreate',
    'AttendancePunchQuery',
    'AttendancePunchUpdate'
  ],
  'human-resource/attendance-leave/attendance-result': [
    'AttendanceResult',
    'AttendanceResultCreate',
    'AttendanceResultQuery',
    'AttendanceResultUpdate'
  ],
  'human-resource/attendance-leave/attendance-setting': [
    'AttendanceSetting',
    'AttendanceSettingCreate',
    'AttendanceSettingQuery',
    'AttendanceSettingUpdate'
  ],
  'human-resource/attendance-leave/attendance-source': [
    'AttendanceSource',
    'AttendanceSourceCreate',
    'AttendanceSourceQuery',
    'AttendanceSourceUpdate'
  ],
  'human-resource/attendance-leave/holiday': ['Holiday', 'HolidayCreate', 'HolidayQuery', 'HolidayUpdate'],
  'human-resource/attendance-leave/leave': ['Leave', 'LeaveCreate', 'LeaveQuery', 'LeaveStatus', 'LeaveUpdate'],
  'human-resource/attendance-leave/overtime': ['Overtime', 'OvertimeCreate', 'OvertimeQuery', 'OvertimeUpdate'],
  'human-resource/attendance-leave/shift-schedule': [
    'ShiftSchedule',
    'ShiftScheduleCreate',
    'ShiftScheduleQuery',
    'ShiftScheduleUpdate'
  ],
  'human-resource/attendance-leave/work-shift': [
    'WorkShift',
    'WorkShiftCreate',
    'WorkShiftQuery',
    'WorkShiftUpdate'
  ],
  'human-resource/organization/dept': ['Dept', 'DeptCreate', 'DeptQuery', 'DeptStatus', 'DeptUpdate'],
  'human-resource/organization/post': ['Post', 'PostCreate', 'PostQuery', 'PostStatus', 'PostUpdate'],
  'human-resource/organization/role-dept': ['RoleDept'],
  'human-resource/organization/user-dept': ['UserDept'],
  'human-resource/organization/user-post': ['UserPost'],
  'human-resource/personnel/employee': ['Employee', 'EmployeeCreate', 'EmployeeQuery', 'EmployeeUpdate'],
  'human-resource/personnel/employee-attachment': [
    'EmployeeAttachment',
    'EmployeeAttachmentCreate',
    'EmployeeAttachmentQuery',
    'EmployeeAttachmentUpdate'
  ],
  'human-resource/personnel/employee-career': [
    'EmployeeCareer',
    'EmployeeCareerCreate',
    'EmployeeCareerQuery',
    'EmployeeCareerUpdate'
  ],
  'human-resource/personnel/employee-contract': [
    'EmployeeContract',
    'EmployeeContractCreate',
    'EmployeeContractQuery',
    'EmployeeContractUpdate'
  ],
  'human-resource/personnel/employee-education': [
    'EmployeeEducation',
    'EmployeeEducationCreate',
    'EmployeeEducationQuery',
    'EmployeeEducationUpdate'
  ],
  'human-resource/personnel/employee-family': [
    'EmployeeFamily',
    'EmployeeFamilyCreate',
    'EmployeeFamilyQuery',
    'EmployeeFamilyUpdate'
  ],
  'human-resource/personnel/employee-skill': [
    'EmployeeSkill',
    'EmployeeSkillCreate',
    'EmployeeSkillQuery',
    'EmployeeSkillUpdate'
  ],
  'human-resource/personnel/employee-transfer': [
    'EmployeeTransfer',
    'EmployeeTransferCreate',
    'EmployeeTransferQuery',
    'EmployeeTransferStatus',
    'EmployeeTransferUpdate'
  ],
  'human-resource/personnel/employee-work': [
    'EmployeeWork',
    'EmployeeWorkCreate',
    'EmployeeWorkQuery',
    'EmployeeWorkUpdate'
  ],
  'identity/health': ['HealthCheck', 'HealthCheckDetailed', 'HealthCheckSignalR'],
  'identity/role': ['Role', 'RoleCreate', 'RoleQuery', 'RoleStatus', 'RoleUpdate'],
  'identity/role-menu': ['RoleMenu'],
  'identity/tenant': ['Tenant', 'TenantCreate', 'TenantQuery', 'TenantStatus', 'TenantUpdate'],
  'identity/user-role': ['UserRole'],
  'identity/user-tenant': ['UserTenant'],
  'logistics/maintenance/equipment': ['Equipment', 'EquipmentQuery'],
  'logistics/material/purchase-order': ['PurchaseOrder', 'PurchaseOrderQuery'],
  'logistics/material/purchase-price': ['PurchasePrice', 'PurchasePriceQuery'],
  'routine/business/announcement': [
    'Announcement',
    'AnnouncementCreate',
    'AnnouncementQuery',
    'AnnouncementStatus',
    'AnnouncementUpdate'
  ],
  'routine/tasks/dict/dictdata': ['DictData', 'DictDataCreate', 'DictDataQuery', 'DictDataUpdate'],
  'routine/tasks/dict/dicttype': ['DictType', 'DictTypeCreate', 'DictTypeQuery', 'DictTypeStatus', 'DictTypeUpdate'],
  'routine/tasks/file': [
    'File',
    'FileChange',
    'FileCreate',
    'FileIncrementDownloadCount',
    'FileQuery',
    'FileStatus',
    'FileUpdate'
  ],
  'routine/tasks/i18n/language': ['Language', 'LanguageCreate', 'LanguageQuery', 'LanguageStatus', 'LanguageUpdate'],
  'routine/tasks/i18n/translation': [
    'Translation',
    'TranslationCreate',
    'TranslationQuery',
    'TranslationTransposed',
    'TranslationTransposedResult',
    'TranslationUpdate'
  ],
  'routine/tasks/numbering-rule': [
    'NumberingRule',
    'NumberingRuleCreate',
    'NumberingRuleQuery',
    'NumberingRuleStatus',
    'NumberingRuleUpdate'
  ],
  'routine/tasks/setting': ['Setting', 'SettingCreate', 'SettingQuery', 'SettingStatus', 'SettingUpdate'],
  'routine/tasks/signalr/online': ['Online', 'OnlineQuery'],
  'routine/tasks/signalr/signalr': [
    'BroadcastMessage',
    'MessageReadEvent',
    'MessageSentEvent',
    'OnlineMessageEvent',
    'OnlineUser',
    'SignalRErrorEvent',
    'SignalRMessage',
    'UserConnectedEvent',
    'UserDisconnectedEvent'
  ],
  'routine/tasks/wordfilter': [
    'AddWordsDto',
    'AddWordsResultDto',
    'CheckTextDto',
    'CheckTextResultDto',
    'FindWordsDto',
    'FindWordsResultDto',
    'HighlightWordsDto',
    'HighlightWordsResultDto',
    'IllegalWordDetailDto',
    'RemoveWordsDto',
    'RemoveWordsResultDto',
    'ReplaceWordsDto',
    'ReplaceWordsResultDto',
    'WordFilterStatsDto',
    'WordLibraryFileDto'
  ],
  'statistics/logging/aop-log': ['AopLog', 'AopLogQuery'],
  'statistics/logging/login-log': ['LoginLog', 'LoginLogQuery'],
  'statistics/logging/oper-log': ['OperLog', 'OperLogQuery'],
  'statistics/logging/quartz-log': ['QuartzLog', 'QuartzLogQuery'],
  'workflow/form': [
    'FlowForm',
    'FlowFormCreate',
    'FlowFormQuery',
    'FlowFormStatusUpdate',
    'FlowFormUpdate'
  ],
  'workflow/scheme': ['FlowScheme', 'FlowSchemeCreateOrUpdate', 'FlowSchemeQuery', 'FlowSchemeStatusUpdate']
}

/**
 * `emitSpecial` 写入的路径（相对 `src/types`）：与 OpenAPI schema 非一一对应的手拼层，无统一 MARKER。
 * 须与下方 `emitSpecial` 内 `writeFileSync` 目标保持一致。
 */
export const SHIM_EMIT_SPECIAL_REL = [
  'identity/auth.ts',
  'identity/user.ts',
  'identity/menu.ts',
  'identity/health.ts',
  'dashboard/data-board.ts',
  'dashboard/workspace.ts',
  'logistics/materials/plant.ts',
  'workflow/instance.ts',
  'common.ts',
  'global-setting.ts'
]

/**
 * 与 `emit-type-shims` 实际会写入的 `.ts` 一致的路径（相对 frontend 根，POSIX），供 `check-contracts` 做 `git diff`。
 * 其中 `MODULE_EXPORTS` 的键即各 openapi 分组下的薄封装模块；`SHIM_EMIT_SPECIAL_REL` 为手写补充。
 */
export function getShimGitDiffPathsFromRepoRoot() {
  const set = new Set()
  for (const mod of Object.keys(MODULE_EXPORTS)) {
    set.add(`src/types/${mod}.ts`)
  }
  for (const f of SHIM_EMIT_SPECIAL_REL) {
    set.add(`src/types/${f.replace(/\\/g, '/')}`)
  }
  return [...set].sort()
}

function emitGenericModule(relPath, exports) {
  const pick = bundleFor(relPath)
  const lines = [
    '/** 由 scripts/emit-type-shims.mjs 生成：从 OpenAPI components.schemas 映射别名，请勿手改字段。 */',
    `import type { ${pick} } from '@/types/internal/openapi-pick'`,
    ''
  ]
  for (const name of exports) {
    const key = schemaKey(relPath, name)
    lines.push(`export type ${name} = ${pick}<'${key}'>`)
  }
  lines.push('')
  const out = path.join(typesDir, ...relPath.split('/')) + '.ts'
  fs.mkdirSync(path.dirname(out), { recursive: true })
  fs.writeFileSync(out, lines.join('\n'), 'utf8')
}

function emitSpecial() {
  fs.mkdirSync(path.join(typesDir, 'identity'), { recursive: true })

  const auth = `import type { IdS } from '@/types/internal/openapi-pick'

export type UserInfo = IdS<'Takt.Application.Dtos.Identity.TaktUserInfoDto'>

/** OAuth 密码模式 + userinfo 组合结果（非单一 swagger schema）。 */
export interface LoginResponse {
  token: string
  refreshToken?: string
  tokenType?: string
  expiresIn?: number
  userInfo: UserInfo
}

/** 登录表单与 OpenAPI TaktLoginDto 字段名不完全一致（username），此处为前端入参约定。 */
export interface LoginParams {
  username: string
  password: string
  rememberMe?: boolean
}
`
  fs.writeFileSync(path.join(typesDir, 'identity/auth.ts'), auth, 'utf8')

  const user = `import type { IdS } from '@/types/internal/openapi-pick'

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
`
  fs.writeFileSync(path.join(typesDir, 'identity/user.ts'), user, 'utf8')

  const menuTypes = `import type { IdS } from '@/types/internal/openapi-pick'

type TaktMenuTree = IdS<'Takt.Application.Dtos.Identity.TaktMenuTreeDto'>

/** 与后端菜单树 DTO 一致，并保留历史/树选择器兼容字段（dict*，小驼峰）。 */
export type MenuTree = TaktMenuTree & {
  dictLabel?: string | null
  dictValue?: string | number | null
  extLabel?: string | null
  extValue?: string | number | null
  transKey?: string
  children?: MenuTree[] | null
}

export type Menu = IdS<'Takt.Application.Dtos.Identity.TaktMenuDto'>
export type MenuCreate = IdS<'Takt.Application.Dtos.Identity.TaktMenuCreateDto'>
export type MenuUpdate = IdS<'Takt.Application.Dtos.Identity.TaktMenuUpdateDto'>
export type MenuQuery = IdS<'Takt.Application.Dtos.Identity.TaktMenuQueryDto'>
export type MenuStatusDto = IdS<'Takt.Application.Dtos.Identity.TaktMenuStatusDto'>
`
  fs.writeFileSync(path.join(typesDir, 'identity/menu.ts'), menuTypes, 'utf8')

  const health = `/** 健康检查端点当前 swagger 无 response schema，使用宽松类型占位。 */
export type HealthCheck = Record<string, unknown>
export type HealthCheckDetailed = Record<string, unknown>
export type HealthCheckSignalR = Record<string, unknown>
`
  fs.writeFileSync(path.join(typesDir, 'identity/health.ts'), health, 'utf8')

  const sr = `import type { RouS } from '@/types/internal/openapi-pick'

export type OnlineUser = RouS<'Takt.Application.Dtos.Routine.Tasks.SignalR.TaktOnlineDto'>
export type SignalRMessage = RouS<'Takt.Application.Dtos.Routine.Tasks.SignalR.TaktMessageDto'>
export type BroadcastMessage = RouS<'Takt.Application.Dtos.Routine.Tasks.SignalR.TaktMessageDto'>

/** Hub 推送载荷：无 OpenAPI schema，与后端 Hub 方法参数约定一致。 */
export type UserConnectedEvent = Record<string, unknown>
export type UserDisconnectedEvent = Record<string, unknown>
export type MessageSentEvent = Record<string, unknown>
export type MessageReadEvent = Record<string, unknown>
export type SignalRErrorEvent = Record<string, unknown>
export type OnlineMessageEvent = Record<string, unknown>
`
  fs.mkdirSync(path.join(typesDir, 'routine/tasks/signalr'), { recursive: true })
  fs.writeFileSync(path.join(typesDir, 'routine/tasks/signalr/signalr.ts'), sr, 'utf8')

  const dashBoard = `export type DataBoardModuleKey =
  | 'overview'
  | 'change'
  | 'online'
  | 'sales'
  | 'production'
  | 'custom'

export interface DataBoardModuleMeta {
  key: DataBoardModuleKey
  titleKey: string
  defaultSpan: number
}

export interface DataBoardModuleItem {
  id: string
  moduleKey: DataBoardModuleKey
  span: number
}
`
  fs.mkdirSync(path.join(typesDir, 'dashboard'), { recursive: true })
  fs.writeFileSync(path.join(typesDir, 'dashboard/data-board.ts'), dashBoard, 'utf8')

  const ws = `export type WorkspaceModuleKey =
  | 'welcome'
  | 'shortcut'
  | 'todo'
  | 'notice'
  | 'custom'

export interface WorkspaceModuleMeta {
  key: WorkspaceModuleKey
  titleKey: string
  defaultSpan: number
}

export interface WorkspaceModuleItem {
  id: string
  moduleKey: WorkspaceModuleKey
  span: number
}
`
  fs.writeFileSync(path.join(typesDir, 'dashboard/workspace.ts'), ws, 'utf8')

  const plant = `import type { LgsS } from '@/types/internal/openapi-pick'

export type Plant = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantDto'>
export type PlantQuery = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantQueryDto'>
export type PlantCreate = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantCreateDto'>
export type PlantUpdate = LgsS<'Takt.Application.Dtos.Logistics.Materials.TaktPlantUpdateDto'>
/** 导入/导出/模板接口在 swagger 中无独立 schema，占位为宽松对象。 */
export type PlantTemplate = Record<string, unknown>
export type PlantImport = Record<string, unknown>
export type PlantExport = Record<string, unknown>
`
  fs.mkdirSync(path.join(typesDir, 'logistics/materials'), { recursive: true })
  fs.writeFileSync(path.join(typesDir, 'logistics/materials/plant.ts'), plant, 'utf8')

  const flowInstance = `import type { WfS } from '@/types/internal/openapi-pick'

export type FlowStartRequest = WfS<'Takt.Application.Dtos.Workflow.TaktFlowStartDto'>
export type FlowStartResult = WfS<'Takt.Application.Dtos.Workflow.TaktFlowStartResultDto'>
export type FlowInstance = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceDto'>
export type FlowInstanceDetail = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceDetailDto'>
export type FlowInstanceQuery = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceQueryDto'>
export type FlowTodoItem = WfS<'Takt.Application.Dtos.Workflow.TaktFlowTodoItemDto'>
/** 待办列表查询参数在 swagger 中为匿名 query，占位。 */
export type FlowTodoQuery = Record<string, unknown>
export type FlowCompleteRequest = WfS<'Takt.Application.Dtos.Workflow.TaktFlowCompleteDto'>
export type FlowOperateBase = Pick<
  WfS<'Takt.WebApi.Controllers.Workflow.TaktFlowInstancesController+RevokeRequest'>,
  'instanceCode' | 'flowInstanceId'
>
export type FlowInstanceUpdate = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceUpdateDto'>
export type FlowUndoVerification = WfS<'Takt.Application.Dtos.Workflow.TaktFlowUndoVerificationDto'>
export type FlowSuspend = WfS<'Takt.Application.Dtos.Workflow.TaktFlowSuspendDto'>
export type FlowResume = WfS<'Takt.Application.Dtos.Workflow.TaktFlowResumeDto'>
export type FlowTerminate = WfS<'Takt.Application.Dtos.Workflow.TaktFlowTerminateDto'>
export type FlowTransfer = WfS<'Takt.Application.Dtos.Workflow.TaktFlowTransferDto'>
export type FlowAddApprovers = WfS<'Takt.Application.Dtos.Workflow.TaktFlowAddApproversDto'>
export type FlowAddApproverItem = WfS<'Takt.Application.Dtos.Workflow.TaktFlowAddApproverItemDto'>
export type FlowReduceApproval = WfS<'Takt.Application.Dtos.Workflow.TaktFlowReduceApprovalDto'>
export type FlowOperationHistoryItem = WfS<'Takt.Application.Dtos.Workflow.TaktFlowOperationHistoryItemDto'>
`
  fs.mkdirSync(path.join(typesDir, 'workflow'), { recursive: true })
  fs.writeFileSync(path.join(typesDir, 'workflow/instance.ts'), flowInstance, 'utf8')

  const common = `import type { IdentityComponents } from '@/types/generated'
import type { IdS, OrgS, RouS } from '@/types/internal/openapi-pick'

type UserPaged =
  IdentityComponents['schemas']["Takt.Shared.Models.TaktPagedResult\`1[[Takt.Application.Dtos.Identity.TaktUserDto, Takt.Application, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null]]"]

export type TaktPagedResult<T> = Omit<UserPaged, 'data'> & { data: T[] }

type ApiObj =
  IdentityComponents['schemas']["Takt.Shared.Models.TaktApiResult\`1[[System.Object, System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]"]

export type TaktApiResult<T> = Omit<ApiObj, 'data'> & { data: T | null }

export type TaktPagedQuery = Pick<
  OrgS<'Takt.Application.Dtos.HumanResource.Organization.TaktDeptQueryDto'>,
  'pageIndex' | 'pageSize' | 'keyWords'
> & { KeyWords?: string }

type RawSelect = IdS<'Takt.Shared.Models.TaktSelectOption'>
export type TaktSelectOption = Omit<RawSelect, 'dictValue' | 'extValue'> & {
  dictValue?: string | number
  extValue?: string | number
}

type RawTree = IdS<'Takt.Shared.Models.TaktTreeSelectOption'>
export type TaktTreeSelectOption = Omit<RawTree, 'dictValue' | 'extValue' | 'children'> & {
  dictValue?: string | number
  extValue?: string | number
  children?: TaktTreeSelectOption[]
}

export type LeaveProofAttachment = RouS<'Takt.Application.Dtos.Routine.Tasks.Files.TaktFileDto'>

export type { TaktResultCode } from '@/utils/enum'
`
  fs.writeFileSync(path.join(typesDir, 'common.ts'), common, 'utf8')

  const gs = `export type ThemeColor =
  | 'blue'
  | 'green'
  | 'red'
  | 'orange'
  | 'purple'
  | 'cyan'
  | 'pink'
  | 'yellow'
  | 'indigo'
  | 'brown'
  | 'gray'
  | 'custom'

export interface ThemeColorConfig {
  type: ThemeColor
  customColor?: string
}

export interface AppSetting {
  layout: 'side' | 'top' | 'mix' | 'content'
  theme: 'light' | 'dark'
  themeColor: ThemeColorConfig
  borderRadius: 0 | 5 | 10 | 15 | 20
  fontSize: number
  colorWeak: boolean
  grayscale: boolean
  fixedHeader: boolean
  fixedSider: boolean
  showLogo: boolean
  siderWidth: number
  siderCollapsedWidth: number
  showBreadcrumb: boolean
  breadcrumbIcon: boolean
  showTabs: boolean
  tabStyle: 'google' | 'card'
  persistTabs: boolean
  maxTabs: number
  showFooter: boolean
  copyright: string
  contentWidth: 'fluid' | 'fixed'
  multiTab: boolean
  watermark: boolean
  watermarkContent: string
  demo: boolean
  menuAccordion: boolean
  menuStyle: 'rounded' | 'plain'
  defaultLocale: string
  logo: string
  logoText: string
  logoCollapsedText: string
  showForgotPassword: boolean
  showRegister: boolean
}
`
  fs.writeFileSync(path.join(typesDir, 'global-setting.ts'), gs, 'utf8')
}

function main() {
  emitSpecial()
  for (const [rel, exports] of Object.entries(MODULE_EXPORTS)) {
    emitGenericModule(rel, exports)
  }
  for (const f of ['common.d.ts', 'global-setting.d.ts']) {
    const p = path.join(typesDir, f)
    if (fs.existsSync(p)) fs.unlinkSync(p)
  }
  console.log('[emit-type-shims] wrote modules under', typesDir)
}

const _emitThisFile = path.resolve(fileURLToPath(import.meta.url))
const _emitEntry = process.argv[1] ? path.resolve(process.argv[1]) : ''
if (_emitEntry !== '' && _emitThisFile === _emitEntry) {
  main()
}
