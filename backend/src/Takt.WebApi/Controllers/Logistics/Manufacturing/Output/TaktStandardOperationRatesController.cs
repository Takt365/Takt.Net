// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktStandardOperationRatesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：标准生产稼动率表控制器，提供StandardOperationRate管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Application.Services.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Output;

/// <summary>
/// 标准生产稼动率表控制器
/// </summary>
[Route("api/[controller]", Name = "标准生产稼动率表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:standardoperationrate", "标准生产稼动率表管理")]
public class TaktStandardOperationRatesController : TaktControllerBase
{
    private readonly ITaktStandardOperationRateService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRatesController(
        ITaktStandardOperationRateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取标准生产稼动率表(StandardOperationRate)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:list", "查询标准生产稼动率表(StandardOperationRate)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktStandardOperationRateDto>>> GetStandardOperationRateListAsync([FromQuery] TaktStandardOperationRateQueryDto queryDto)
    {
        var result = await _service.GetStandardOperationRateListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取标准生产稼动率表(StandardOperationRate)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:query", "查询标准生产稼动率表(StandardOperationRate)详情")]
    public async Task<ActionResult<TaktStandardOperationRateDto>> GetStandardOperationRateByIdAsync(long id)
    {
        var item = await _service.GetStandardOperationRateByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取标准生产稼动率表(StandardOperationRate)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:query", "查询标准生产稼动率表(StandardOperationRate)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetStandardOperationRateOptionsAsync()
    {
        var result = await _service.GetStandardOperationRateOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建标准生产稼动率表(StandardOperationRate)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:create", "创建标准生产稼动率表(StandardOperationRate)")]
    public async Task<ActionResult<TaktStandardOperationRateDto>> CreateStandardOperationRateAsync([FromBody] TaktStandardOperationRateCreateDto dto)
    {
        var result = await _service.CreateStandardOperationRateAsync(dto);
        return CreatedAtAction(nameof(GetStandardOperationRateByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新标准生产稼动率表(StandardOperationRate)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:update", "更新标准生产稼动率表(StandardOperationRate)")]
    public async Task<ActionResult<TaktStandardOperationRateDto>> UpdateStandardOperationRateAsync(long id, [FromBody] TaktStandardOperationRateUpdateDto dto)
    {
        var result = await _service.UpdateStandardOperationRateAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除标准生产稼动率表(StandardOperationRate)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:delete", "删除标准生产稼动率表(StandardOperationRate)")]
    public async Task<ActionResult> DeleteStandardOperationRateByIdAsync(long id)
    {
        await _service.DeleteStandardOperationRateByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除标准生产稼动率表(StandardOperationRate)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:delete", "批量删除标准生产稼动率表(StandardOperationRate)")]
    public async Task<ActionResult> DeleteStandardOperationRateBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteStandardOperationRateBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新标准生产稼动率表(StandardOperationRate)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:update", "更新标准生产稼动率表(StandardOperationRate)状态")]
    public async Task<ActionResult<TaktStandardOperationRateDto>> UpdateStandardOperationRateStatusAsync([FromBody] TaktStandardOperationRateStatusDto dto)
    {
        var result = await _service.UpdateStandardOperationRateStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取标准生产稼动率表(StandardOperationRate)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:import", "获取标准生产稼动率表(StandardOperationRate)导入模板")]
    public async Task<IActionResult> GetStandardOperationRateTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetStandardOperationRateTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入标准生产稼动率表(StandardOperationRate)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:import", "导入标准生产稼动率表(StandardOperationRate)")]
    public async Task<ActionResult<object>> ImportStandardOperationRateAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportStandardOperationRateAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出标准生产稼动率表(StandardOperationRate)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:standardoperationrate:export", "导出标准生产稼动率表(StandardOperationRate)")]
    public async Task<IActionResult> ExportStandardOperationRateAsync([FromBody] TaktStandardOperationRateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportStandardOperationRateAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
