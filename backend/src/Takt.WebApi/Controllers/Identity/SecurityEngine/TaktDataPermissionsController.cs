// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers
// 文件名称：TaktDataPermissionsController.cs
// 创建时间：2026-04-30
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktDataPermission控制器，提供DataPermission管理的RESTful API接口
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
/// TaktDataPermission控制器
/// </summary>
[Route("api/[controller]", Name = "DataPermission")]
[ApiModule("System", "系统管理")]
[TaktPermission("system:datapermission", "DataPermission管理")]
public class TaktDataPermissionsController : TaktControllerBase
{
    private readonly ITaktDataPermissionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDataPermissionsController(
        ITaktDataPermissionService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取用户在数据权限下允许访问的部门 Id 列表（多角色结果做并集）
    /// </summary>
    /// <param name="userId">用户主键</param>
    /// <returns>
    /// 可见部门 Id 列表。超级管理员或任一启用角色为「全部数据」时返回全部启用部门 Id；
    /// 用户不存在、无角色或无启用角色时返回空列表；「仅本人」角色不向列表写入部门 Id，业务需自行按创建人/用户 Id 过滤。
    /// </returns>
    [HttpGet("allowed-departments")]
    [TaktPermission("system:datapermission:query", "查询数据权限部门列表")]
    public async Task<ActionResult<List<long>>> GetAllowedDepartmentsAsync([FromQuery] long userId)
    {
        if (userId <= 0)
        {
            return BadRequest("用户ID必须大于0");
        }
        
        try
        {
            var result = await _service.GetAllowedDepartmentIdsAsync(userId);
            return Ok(result.ToList());
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

}
