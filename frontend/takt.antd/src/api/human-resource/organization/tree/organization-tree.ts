// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/
// 文件名称：organization-tree.ts
// 功能描述：OrganizationTree API，对应后端 Takt.WebApi.Controllers.TaktOrganizationTrees
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type { OrganizationTree } from '@/types//organization-tree'

// ========================================
// OrganizationTree相关 API（按后端控制器顺序）
// ========================================
const organizationTreeUrl = 'api/TaktOrganizationTrees';

