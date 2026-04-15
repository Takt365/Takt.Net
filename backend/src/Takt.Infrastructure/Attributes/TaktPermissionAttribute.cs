// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Attributes
// 文件名称：TaktPermissionAttribute.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：权限特性，用于标记控制器或方法所需的权限标识
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using Microsoft.AspNetCore.Authorization;

namespace Takt.Infrastructure.Attributes;

/// <summary>
/// 权限特性，用于标记控制器或方法所需的权限标识
/// </summary>
/// <remarks>
/// 权限标识格式：业务领域:实体:操作
/// - 业务领域：模块名称，例如 identity（身份认证）、system（系统管理）等
/// - 实体：业务实体名称，例如 user（用户）、role（角色）等
/// - 操作：操作类型，例如 list（列表查询）、query（详情查询）、create（创建）、update（更新）、delete（删除）、import（导入）、export（导出）等
/// 
/// 使用方法：
/// 1. 在控制器类上标记：该控制器的所有方法都需要该权限
/// 2. 在方法上标记：仅该方法需要该权限（会覆盖控制器级别的权限）
/// 3. 配合 [Authorize] 特性使用，确保用户已认证
/// 
/// 示例：
/// [Authorize]
/// [TaktPermission("identity:user", "用户管理")]
/// public class TaktUsersController : ControllerBase
/// {
///     [TaktPermission("identity:user:list", "查询用户列表")]
///     [HttpGet("list")]
///     public async Task&lt;IActionResult&gt; GetList() { }
///     
///     [TaktPermission("identity:user:query", "查询用户详情")]
///     [HttpGet("{id}")]
///     public async Task&lt;IActionResult&gt; GetById(long id) { }
///     
///     [TaktPermission("identity:user:create", "创建用户")]
///     [HttpPost]
///     public async Task&lt;IActionResult&gt; Create() { }
///     
///     [TaktPermission("identity:user:update", "更新用户")]
///     [HttpPut("{id}")]
///     public async Task&lt;IActionResult&gt; Update(long id) { }
///     
///     [TaktPermission("identity:user:delete", "删除用户")]
///     [HttpDelete("{id}")]
///     public async Task&lt;IActionResult&gt; Delete(long id) { }
/// }
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class TaktPermissionAttribute : Attribute, IAuthorizeData
{
    /// <summary>
    /// 权限标识（必填）
    /// </summary>
    public string Permission { get; }

    /// <summary>
    /// 权限描述（可选）
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permission">权限标识，格式：业务领域:实体:操作，例如："identity:user:list", "identity:user:query", "identity:user:create", "identity:user:update", "identity:user:delete"</param>
    public TaktPermissionAttribute(string permission)
    {
        if (string.IsNullOrWhiteSpace(permission))
        {
            throw new ArgumentException("权限标识不能为空", nameof(permission));
        }

        Permission = permission;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permission">权限标识</param>
    /// <param name="description">权限描述</param>
    public TaktPermissionAttribute(string permission, string description) : this(permission)
    {
        Description = description;
    }

    // IAuthorizeData 接口实现（用于兼容 ASP.NET Core 授权系统）
    /// <summary>
    /// 授权策略名称（用于兼容 ASP.NET Core 授权）
    /// </summary>
    public string? Policy { get; set; }

    /// <summary>
    /// 角色（用于兼容 ASP.NET Core 授权）
    /// </summary>
    public string? Roles { get; set; }

    /// <summary>
    /// 身份验证方案（用于兼容 ASP.NET Core 授权）
    /// </summary>
    public string? AuthenticationSchemes { get; set; }
}
