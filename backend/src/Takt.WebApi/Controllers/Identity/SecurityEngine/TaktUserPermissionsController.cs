// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers
// 文件名称：TaktUserPermissionsController.cs
// 创建时间：2026-04-28
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktUserPermission控制器，提供UserPermission管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos;
using Takt.Application.Services;
using Takt.Application.Services.Identity.SecurityEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers;

/// <summary>
/// TaktUserPermission控制器
/// </summary>
[Route("api/[controller]", Name = "UserPermission")]
[ApiModule("System", "系统管理")]
[TaktPermission("system:userpermission", "UserPermission管理")]
public class TaktUserPermissionsController : TaktControllerBase
{
    private readonly ITaktUserPermissionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserPermissionsController(
        ITaktUserPermissionService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 判断用户是否拥有指定权限标识
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="permission">权限标识</param>
    /// <returns>是否拥有权限</returns>
    [HttpGet("has-permission")]
    [TaktPermission("system:userpermission:haspermission", "检查用户权限")]
    public async Task<ActionResult<bool>> HasMenuPermissionAsync(
        [FromQuery] long userId,
        [FromQuery] string permission)
    {
        if (userId <= 0)
        {
            return BadRequest("用户ID必须大于0");
        }

        if (string.IsNullOrWhiteSpace(permission))
        {
            return BadRequest("权限标识不能为空");
        }

        try
        {
            var result = await _service.HasMenuPermissionAsync(userId, permission);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
