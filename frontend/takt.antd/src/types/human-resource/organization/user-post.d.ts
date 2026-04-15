// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/organization/user-post
// 文件名称：user-post.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户岗位关联类型定义，对应后端 Takt.Application.Dtos.Organization.TaktUserPostDto
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase } from '@/types/common'

/**
 * 用户岗位关联类型（对应后端 Takt.Application.Dtos.Organization.TaktUserPostDto）
 * 用于获取岗位用户列表，即根据岗位ID获取该岗位下的用户列表
 */
export interface UserPost extends TaktEntityBase {
  /** 用户岗位关联ID（对应后端 UserPostId，序列化为string以避免Javascript精度问题） */
  userPostId: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 用户真实姓名（对应后端 RealName） */
  realName: string
  /** 岗位ID（对应后端 PostId，序列化为string以避免Javascript精度问题） */
  postId: string
  /** 岗位名称（对应后端 PostName） */
  postName: string
  /** 岗位编码（对应后端 PostCode） */
  postCode: string
}
