// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDeptsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：设变部门表控制器，提供EcDept管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变部门表控制器
/// </summary>
[Route("api/[controller]", Name = "设变部门表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:engineeringchange:ecdept", "设变部门表管理")]
public class TaktEcDeptsController : TaktControllerBase
{
    private readonly ITaktEcDeptService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptsController(
        ITaktEcDeptService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取设变部门表(EcDept)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:list", "查询设变部门表(EcDept)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEcDeptDto>>> GetEcDeptListAsync([FromQuery] TaktEcDeptQueryDto queryDto)
    {
        var result = await _service.GetEcDeptListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取设变部门表(EcDept)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:query", "查询设变部门表(EcDept)详情")]
    public async Task<ActionResult<TaktEcDeptDto>> GetEcDeptByIdAsync(long id)
    {
        var item = await _service.GetEcDeptByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取设变部门表(EcDept)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:query", "查询设变部门表(EcDept)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEcDeptOptionsAsync()
    {
        var result = await _service.GetEcDeptOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建设变部门表(EcDept)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:create", "创建设变部门表(EcDept)")]
    public async Task<ActionResult<TaktEcDeptDto>> CreateEcDeptAsync([FromBody] TaktEcDeptCreateDto dto)
    {
        var result = await _service.CreateEcDeptAsync(dto);
        return CreatedAtAction(nameof(GetEcDeptByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新设变部门表(EcDept)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:update", "更新设变部门表(EcDept)")]
    public async Task<ActionResult<TaktEcDeptDto>> UpdateEcDeptAsync(long id, [FromBody] TaktEcDeptUpdateDto dto)
    {
        var result = await _service.UpdateEcDeptAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除设变部门表(EcDept)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:delete", "删除设变部门表(EcDept)")]
    public async Task<ActionResult> DeleteEcDeptByIdAsync(long id)
    {
        await _service.DeleteEcDeptByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除设变部门表(EcDept)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:delete", "批量删除设变部门表(EcDept)")]
    public async Task<ActionResult> DeleteEcDeptBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEcDeptBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取设变部门表(EcDept)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:import", "获取设变部门表(EcDept)导入模板")]
    public async Task<IActionResult> GetEcDeptTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEcDeptTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入设变部门表(EcDept)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:import", "导入设变部门表(EcDept)")]
    public async Task<ActionResult<object>> ImportEcDeptAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEcDeptAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出设变部门表(EcDept)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdept:export", "导出设变部门表(EcDept)")]
    public async Task<IActionResult> ExportEcDeptAsync([FromBody] TaktEcDeptQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEcDeptAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
