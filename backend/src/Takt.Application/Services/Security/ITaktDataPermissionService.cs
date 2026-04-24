// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Security
// 文件名称：ITaktDataPermissionService.cs
// 创建时间：2026-04-18
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 数据权限服务接口，定义按用户角色 DataScope、CustomScope 与 RBAC 部门关联（TaktUserDept）
//           计算「允许访问的部门 Id」列表的契约，供各业务服务在列表/统计查询中拼接行级数据范围过滤。
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Security;

/// <summary>
/// Takt 数据权限服务接口：按用户角色 <c>DataScope</c>、<c>CustomScope</c> 与 RBAC 用户部门关联计算可见部门范围。
/// </summary>
public interface ITaktDataPermissionService
{
    /// <summary>
    /// 获取用户在数据权限下允许访问的部门 Id 列表（多角色结果做并集）。
    /// </summary>
    /// <param name="userId">用户主键</param>
    /// <param name="cancellationToken">取消标记</param>
    /// <returns>
    /// 可见部门 Id 列表。超级管理员或任一启用角色为「全部数据」时返回全部启用部门 Id；
    /// 用户不存在、无角色或无启用角色时返回空列表；「仅本人」角色不向列表写入部门 Id，业务需自行按创建人/用户 Id 过滤。
    /// </returns>
    Task<IReadOnlyList<long>> GetAllowedDepartmentIdsAsync(long userId, CancellationToken cancellationToken = default);
}
