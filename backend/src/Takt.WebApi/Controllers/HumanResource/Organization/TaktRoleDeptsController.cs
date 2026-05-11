// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktRoleDeptsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：角色部门关联表控制器，提供RoleDept管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Organization;

/// <summary>
/// 角色部门关联表控制器
/// </summary>
[Route("api/[controller]", Name = "角色部门关联表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:roledept", "角色部门关联表管理")]
public class TaktRoleDeptsController : TaktControllerBase
{
    private readonly ITaktRoleDeptService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleDeptsController(
        ITaktRoleDeptService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取角色部门关联表(RoleDept)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:roledept:list", "查询角色部门关联表(RoleDept)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktRoleDeptDto>>> GetRoleDeptListAsync([FromQuery] TaktRoleDeptQueryDto queryDto)
    {
        var result = await _service.GetRoleDeptListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取角色部门关联表(RoleDept)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:roledept:query", "查询角色部门关联表(RoleDept)详情")]
    public async Task<ActionResult<TaktRoleDeptDto>> GetRoleDeptByIdAsync(long id)
    {
        var item = await _service.GetRoleDeptByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取角色部门关联表(RoleDept)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:roledept:query", "查询角色部门关联表(RoleDept)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetRoleDeptOptionsAsync()
    {
        var result = await _service.GetRoleDeptOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建角色部门关联表(RoleDept)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:roledept:create", "创建角色部门关联表(RoleDept)")]
    public async Task<ActionResult<TaktRoleDeptDto>> CreateRoleDeptAsync([FromBody] TaktRoleDeptCreateDto dto)
    {
        var result = await _service.CreateRoleDeptAsync(dto);
        return CreatedAtAction(nameof(GetRoleDeptByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新角色部门关联表(RoleDept)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:roledept:update", "更新角色部门关联表(RoleDept)")]
    public async Task<ActionResult<TaktRoleDeptDto>> UpdateRoleDeptAsync(long id, [FromBody] TaktRoleDeptUpdateDto dto)
    {
        var result = await _service.UpdateRoleDeptAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除角色部门关联表(RoleDept)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:roledept:delete", "删除角色部门关联表(RoleDept)")]
    public async Task<ActionResult> DeleteRoleDeptByIdAsync(long id)
    {
        await _service.DeleteRoleDeptByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除角色部门关联表(RoleDept)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:roledept:delete", "批量删除角色部门关联表(RoleDept)")]
    public async Task<ActionResult> DeleteRoleDeptBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteRoleDeptBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取角色部门关联表(RoleDept)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:roledept:import", "获取角色部门关联表(RoleDept)导入模板")]
    public async Task<IActionResult> GetRoleDeptTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetRoleDeptTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入角色部门关联表(RoleDept)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:roledept:import", "导入角色部门关联表(RoleDept)")]
    public async Task<ActionResult<object>> ImportRoleDeptAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        }

        if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
            !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));
        }

        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportRoleDeptAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出角色部门关联表(RoleDept)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:roledept:export", "导出角色部门关联表(RoleDept)")]
    public async Task<IActionResult> ExportRoleDeptAsync([FromBody] TaktRoleDeptQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportRoleDeptAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
