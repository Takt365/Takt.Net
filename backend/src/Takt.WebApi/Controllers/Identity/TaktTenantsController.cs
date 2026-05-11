// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktTenantsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：租户信息表控制器，提供Tenant管理的RESTful API接口
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
/// 租户信息表控制器
/// </summary>
[Route("api/[controller]", Name = "租户信息表")]
[ApiModule("System", "系统管理")]
[TaktPermission("identity:tenant", "租户信息表管理")]
public class TaktTenantsController : TaktControllerBase
{
    private readonly ITaktTenantService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantsController(
        ITaktTenantService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取租户信息表(Tenant)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("identity:tenant:list", "查询租户信息表(Tenant)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTenantDto>>> GetTenantListAsync([FromQuery] TaktTenantQueryDto queryDto)
    {
        var result = await _service.GetTenantListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取租户信息表(Tenant)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("identity:tenant:query", "查询租户信息表(Tenant)详情")]
    public async Task<ActionResult<TaktTenantDto>> GetTenantByIdAsync(long id)
    {
        var item = await _service.GetTenantByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取租户信息表(Tenant)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("identity:tenant:query", "查询租户信息表(Tenant)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTenantOptionsAsync()
    {
        var result = await _service.GetTenantOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建租户信息表(Tenant)
    /// </summary>
    [HttpPost]
    [TaktPermission("identity:tenant:create", "创建租户信息表(Tenant)")]
    public async Task<ActionResult<TaktTenantDto>> CreateTenantAsync([FromBody] TaktTenantCreateDto dto)
    {
        var result = await _service.CreateTenantAsync(dto);
        return CreatedAtAction(nameof(GetTenantByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新租户信息表(Tenant)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("identity:tenant:update", "更新租户信息表(Tenant)")]
    public async Task<ActionResult<TaktTenantDto>> UpdateTenantAsync(long id, [FromBody] TaktTenantUpdateDto dto)
    {
        var result = await _service.UpdateTenantAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除租户信息表(Tenant)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("identity:tenant:delete", "删除租户信息表(Tenant)")]
    public async Task<ActionResult> DeleteTenantByIdAsync(long id)
    {
        await _service.DeleteTenantByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除租户信息表(Tenant)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("identity:tenant:delete", "批量删除租户信息表(Tenant)")]
    public async Task<ActionResult> DeleteTenantBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTenantBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新租户信息表(Tenant)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("identity:tenant:update", "更新租户信息表(Tenant)状态")]
    public async Task<ActionResult<TaktTenantDto>> UpdateTenantStatusAsync([FromBody] TaktTenantStatusDto dto)
    {
        var result = await _service.UpdateTenantStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取租户信息表(Tenant)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("identity:tenant:import", "获取租户信息表(Tenant)导入模板")]
    public async Task<IActionResult> GetTenantTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTenantTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入租户信息表(Tenant)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("identity:tenant:import", "导入租户信息表(Tenant)")]
    public async Task<ActionResult<object>> ImportTenantAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTenantAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出租户信息表(Tenant)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("identity:tenant:export", "导出租户信息表(Tenant)")]
    public async Task<IActionResult> ExportTenantAsync([FromBody] TaktTenantQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTenantAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
