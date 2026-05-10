// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Attributes
// 文件名称：TaktSkipPermissionAttribute.cs
// 创建时间：2026-04-18
// 创建人：Takt365(Cursor AI)
// 功能描述：标记在已认证前提下跳过 TaktPermission 权限串校验（仍受 [Authorize] 等约束）
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Infrastructure.Attributes;

/// <summary>
/// 跳过基于菜单权限标识（<see cref="TaktPermissionAttribute"/>）的校验，仅要求已通过认证。
/// 用于「当前用户菜单树」等接口：其权限过滤在业务层完成，且角色菜单权限码未必包含控制器级权限串。
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class TaktSkipPermissionAttribute : Attribute
{
}
