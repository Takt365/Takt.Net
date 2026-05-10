// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization.SpecificEngine
// 文件名称：TaktOrganizationsController.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：组织架构专用控制器，提供部门树等树形API
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services.HumanResource.Organization.SpecificEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.HumanResource.Organization.SpecificEngine;

/// <summary>
/// 组织架构专用控制器
/// </summary>
[Route("api/organizations", Name = "组织架构专用")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organizations:dept", "组织架构专用管理")]
public class TaktOrganizationsController : TaktControllerBase
{
    private readonly ITaktOrganizationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOrganizationsController(
        ITaktOrganizationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    #region 部门树形选项

    /// <summary>
    /// 获取部门树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>部门树形选项列表</returns>
    [HttpGet("tree-options")]
    [TaktPermission("humanresource:organizations:dept:options", "部门树形选项查询")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetTreeOptionsAsync()
    {
        try
        {
            var result = await _service.GetTreeOptionsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    #endregion

    #region 部门树形

    /// <summary>
    /// 获取部门树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门树形列表</returns>
    [HttpGet("tree")]
    [TaktPermission("humanresource:organizations:dept:tree", "部门树查询")]
    public async Task<ActionResult<List<TaktDeptTreeDto>>> GetTreeAsync(
        [FromQuery] long parentId = 0,
        [FromQuery] bool includeDisabled = false)
    {
        try
        {
            var result = await _service.GetTreeAsync(parentId, includeDisabled);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取部门子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门子节点列表</returns>
    [HttpGet("children")]
    [TaktPermission("humanresource:organizations:dept:children", "部门子节点查询")]
    public async Task<ActionResult<List<TaktDeptDto>>> GetChildrenAsync(
        [FromQuery] long parentId,
        [FromQuery] bool includeDisabled = false)
    {
        try
        {
            var result = await _service.GetChildrenAsync(parentId, includeDisabled);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    #endregion
}