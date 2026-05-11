// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktStandardWageRatesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：标准工资率表控制器，提供StandardWageRate管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 标准工资率表控制器
/// </summary>
[Route("api/[controller]", Name = "标准工资率表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:standardwagerate", "标准工资率表管理")]
public class TaktStandardWageRatesController : TaktControllerBase
{
    private readonly ITaktStandardWageRateService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRatesController(
        ITaktStandardWageRateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取标准工资率表(StandardWageRate)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:standardwagerate:list", "查询标准工资率表(StandardWageRate)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktStandardWageRateDto>>> GetStandardWageRateListAsync([FromQuery] TaktStandardWageRateQueryDto queryDto)
    {
        var result = await _service.GetStandardWageRateListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取标准工资率表(StandardWageRate)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:standardwagerate:query", "查询标准工资率表(StandardWageRate)详情")]
    public async Task<ActionResult<TaktStandardWageRateDto>> GetStandardWageRateByIdAsync(long id)
    {
        var item = await _service.GetStandardWageRateByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取标准工资率表(StandardWageRate)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:standardwagerate:query", "查询标准工资率表(StandardWageRate)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetStandardWageRateOptionsAsync()
    {
        var result = await _service.GetStandardWageRateOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建标准工资率表(StandardWageRate)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:standardwagerate:create", "创建标准工资率表(StandardWageRate)")]
    public async Task<ActionResult<TaktStandardWageRateDto>> CreateStandardWageRateAsync([FromBody] TaktStandardWageRateCreateDto dto)
    {
        var result = await _service.CreateStandardWageRateAsync(dto);
        return CreatedAtAction(nameof(GetStandardWageRateByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新标准工资率表(StandardWageRate)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:standardwagerate:update", "更新标准工资率表(StandardWageRate)")]
    public async Task<ActionResult<TaktStandardWageRateDto>> UpdateStandardWageRateAsync(long id, [FromBody] TaktStandardWageRateUpdateDto dto)
    {
        var result = await _service.UpdateStandardWageRateAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除标准工资率表(StandardWageRate)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:standardwagerate:delete", "删除标准工资率表(StandardWageRate)")]
    public async Task<ActionResult> DeleteStandardWageRateByIdAsync(long id)
    {
        await _service.DeleteStandardWageRateByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除标准工资率表(StandardWageRate)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:controlling:standardwagerate:delete", "批量删除标准工资率表(StandardWageRate)")]
    public async Task<ActionResult> DeleteStandardWageRateBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteStandardWageRateBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取标准工资率表(StandardWageRate)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:standardwagerate:import", "获取标准工资率表(StandardWageRate)导入模板")]
    public async Task<IActionResult> GetStandardWageRateTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetStandardWageRateTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入标准工资率表(StandardWageRate)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:standardwagerate:import", "导入标准工资率表(StandardWageRate)")]
    public async Task<ActionResult<object>> ImportStandardWageRateAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportStandardWageRateAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出标准工资率表(StandardWageRate)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:standardwagerate:export", "导出标准工资率表(StandardWageRate)")]
    public async Task<IActionResult> ExportStandardWageRateAsync([FromBody] TaktStandardWageRateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportStandardWageRateAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
