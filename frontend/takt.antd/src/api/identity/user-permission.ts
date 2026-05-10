// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/
// 文件名称：user-permission.ts
// 功能描述：UserPermission API，对应后端 Takt.WebApi.Controllers.TaktUserPermissions
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type { UserPermission } from '@/types//user-permission'

// ========================================
// UserPermission相关 API（按后端控制器顺序）
// ========================================
const userPermissionUrl = 'api/TaktUserPermissions';

