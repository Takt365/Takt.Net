// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktRolesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：角色信息表控制器，提供Role管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 角色信息表控制器
/// </summary>
[Route("api/[controller]", Name = "角色信息表")]
[ApiModule("System", "系统管理")]
[TaktPermission("identity:role", "角色信息表管理")]
public class TaktRolesController : TaktControllerBase
{
    private readonly ITaktRoleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRolesController(
        ITaktRoleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取角色信息表(Role)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("identity:role:list", "查询角色信息表(Role)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktRoleDto>>> GetRoleListAsync([FromQuery] TaktRoleQueryDto queryDto)
    {
        var result = await _service.GetRoleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取角色信息表(Role)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("identity:role:query", "查询角色信息表(Role)详情")]
    public async Task<ActionResult<TaktRoleDto>> GetRoleByIdAsync(long id)
    {
        var item = await _service.GetRoleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取角色信息表(Role)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("identity:role:query", "查询角色信息表(Role)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetRoleOptionsAsync()
    {
        var result = await _service.GetRoleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建角色信息表(Role)
    /// </summary>
    [HttpPost]
    [TaktPermission("identity:role:create", "创建角色信息表(Role)")]
    public async Task<ActionResult<TaktRoleDto>> CreateRoleAsync([FromBody] TaktRoleCreateDto dto)
    {
        var result = await _service.CreateRoleAsync(dto);
        return CreatedAtAction(nameof(GetRoleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新角色信息表(Role)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("identity:role:update", "更新角色信息表(Role)")]
    public async Task<ActionResult<TaktRoleDto>> UpdateRoleAsync(long id, [FromBody] TaktRoleUpdateDto dto)
    {
        var result = await _service.UpdateRoleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除角色信息表(Role)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("identity:role:delete", "删除角色信息表(Role)")]
    public async Task<ActionResult> DeleteRoleByIdAsync(long id)
    {
        await _service.DeleteRoleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除角色信息表(Role)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("identity:role:delete", "批量删除角色信息表(Role)")]
    public async Task<ActionResult> DeleteRoleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteRoleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新角色信息表(Role)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("identity:role:update", "更新角色信息表(Role)状态")]
    public async Task<ActionResult<TaktRoleDto>> UpdateRoleStatusAsync([FromBody] TaktRoleStatusDto dto)
    {
        var result = await _service.UpdateRoleStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新角色信息表(Role)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("identity:role:update", "更新角色信息表(Role)排序")]
    public async Task<ActionResult<TaktRoleDto>> UpdateRoleSortAsync([FromBody] TaktRoleSortDto dto)
    {
        var result = await _service.UpdateRoleSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取角色信息表(Role)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("identity:role:import", "获取角色信息表(Role)导入模板")]
    public async Task<IActionResult> GetRoleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetRoleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入角色信息表(Role)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("identity:role:import", "导入角色信息表(Role)")]
    public async Task<ActionResult<object>> ImportRoleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportRoleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出角色信息表(Role)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("identity:role:export", "导出角色信息表(Role)")]
    public async Task<IActionResult> ExportRoleAsync([FromBody] TaktRoleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportRoleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
