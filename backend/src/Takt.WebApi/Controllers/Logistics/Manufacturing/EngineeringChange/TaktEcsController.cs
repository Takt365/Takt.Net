// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：设变主表控制器，提供Ec管理的RESTful API接口
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
/// 设变主表控制器
/// </summary>
[Route("api/[controller]", Name = "设变主表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:engineeringchange:ec", "设变主表管理")]
public class TaktEcsController : TaktControllerBase
{
    private readonly ITaktEcService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcsController(
        ITaktEcService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取设变主表(Ec)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:list", "查询设变主表(Ec)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEcDto>>> GetEcListAsync([FromQuery] TaktEcQueryDto queryDto)
    {
        var result = await _service.GetEcListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取设变主表(Ec)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:query", "查询设变主表(Ec)详情")]
    public async Task<ActionResult<TaktEcDto>> GetEcByIdAsync(long id)
    {
        var item = await _service.GetEcByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取设变主表(Ec)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:query", "查询设变主表(Ec)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEcOptionsAsync()
    {
        var result = await _service.GetEcOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建设变主表(Ec)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:create", "创建设变主表(Ec)")]
    public async Task<ActionResult<TaktEcDto>> CreateEcAsync([FromBody] TaktEcCreateDto dto)
    {
        var result = await _service.CreateEcAsync(dto);
        return CreatedAtAction(nameof(GetEcByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新设变主表(Ec)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:update", "更新设变主表(Ec)")]
    public async Task<ActionResult<TaktEcDto>> UpdateEcAsync(long id, [FromBody] TaktEcUpdateDto dto)
    {
        var result = await _service.UpdateEcAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除设变主表(Ec)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:delete", "删除设变主表(Ec)")]
    public async Task<ActionResult> DeleteEcByIdAsync(long id)
    {
        await _service.DeleteEcByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除设变主表(Ec)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:delete", "批量删除设变主表(Ec)")]
    public async Task<ActionResult> DeleteEcBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEcBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新设变主表(Ec)Change
    /// </summary>
    [HttpPut("status-change")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:update", "更新设变主表(Ec)Change")]
    public async Task<ActionResult<TaktEcDto>> UpdateEcChangeStatusAsync([FromBody] TaktEcChangeStatusDto dto)
    {
        var result = await _service.UpdateEcChangeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新设变主表(Ec)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:update", "更新设变主表(Ec)状态")]
    public async Task<ActionResult<TaktEcDto>> UpdateEcStatusAsync([FromBody] TaktEcStatusDto dto)
    {
        var result = await _service.UpdateEcStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取设变主表(Ec)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:import", "获取设变主表(Ec)导入模板")]
    public async Task<IActionResult> GetEcTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEcTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入设变主表(Ec)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:import", "导入设变主表(Ec)")]
    public async Task<ActionResult<object>> ImportEcAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEcAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出设变主表(Ec)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ec:export", "导出设变主表(Ec)")]
    public async Task<IActionResult> ExportEcAsync([FromBody] TaktEcQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEcAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
