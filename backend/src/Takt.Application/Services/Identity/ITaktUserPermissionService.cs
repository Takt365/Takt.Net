// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktUserPermissionService.cs
// 创建时间：2026-04-18
// 创建人：Takt365(Cursor AI)
// 功能描述：按用户角色菜单聚合权限标识，供权限中间件校验
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Identity;

/// <summary>
/// 用户菜单权限（RBAC：用户→角色→角色菜单→菜单 Permission）查询与匹配。
/// </summary>
public interface ITaktUserPermissionService
{
    /// <summary>
    /// 判断用户是否拥有指定权限标识（与菜单 <c>permission</c> 字段全等比较，忽略大小写）。
    /// </summary>
    Task<bool> HasMenuPermissionAsync(long userId, string permission, CancellationToken cancellationToken = default);
}
