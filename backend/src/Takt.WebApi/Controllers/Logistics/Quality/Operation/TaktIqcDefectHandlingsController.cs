// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIqcDefectHandlingsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验不良处理记录表控制器，提供IqcDefectHandling管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Operation;

/// <summary>
/// 进货检验不良处理记录表控制器
/// </summary>
[Route("api/[controller]", Name = "进货检验不良处理记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:iqcdefecthandling", "进货检验不良处理记录表管理")]
public class TaktIqcDefectHandlingsController : TaktControllerBase
{
    private readonly ITaktIqcDefectHandlingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIqcDefectHandlingsController(
        ITaktIqcDefectHandlingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取进货检验不良处理记录表(IqcDefectHandling)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:list", "查询进货检验不良处理记录表(IqcDefectHandling)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIqcDefectHandlingDto>>> GetIqcDefectHandlingListAsync([FromQuery] TaktIqcDefectHandlingQueryDto queryDto)
    {
        var result = await _service.GetIqcDefectHandlingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:query", "查询进货检验不良处理记录表(IqcDefectHandling)详情")]
    public async Task<ActionResult<TaktIqcDefectHandlingDto>> GetIqcDefectHandlingByIdAsync(long id)
    {
        var item = await _service.GetIqcDefectHandlingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取进货检验不良处理记录表(IqcDefectHandling)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:query", "查询进货检验不良处理记录表(IqcDefectHandling)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIqcDefectHandlingOptionsAsync()
    {
        var result = await _service.GetIqcDefectHandlingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:create", "创建进货检验不良处理记录表(IqcDefectHandling)")]
    public async Task<ActionResult<TaktIqcDefectHandlingDto>> CreateIqcDefectHandlingAsync([FromBody] TaktIqcDefectHandlingCreateDto dto)
    {
        var result = await _service.CreateIqcDefectHandlingAsync(dto);
        return CreatedAtAction(nameof(GetIqcDefectHandlingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:update", "更新进货检验不良处理记录表(IqcDefectHandling)")]
    public async Task<ActionResult<TaktIqcDefectHandlingDto>> UpdateIqcDefectHandlingAsync(long id, [FromBody] TaktIqcDefectHandlingUpdateDto dto)
    {
        var result = await _service.UpdateIqcDefectHandlingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:delete", "删除进货检验不良处理记录表(IqcDefectHandling)")]
    public async Task<ActionResult> DeleteIqcDefectHandlingByIdAsync(long id)
    {
        await _service.DeleteIqcDefectHandlingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:delete", "批量删除进货检验不良处理记录表(IqcDefectHandling)")]
    public async Task<ActionResult> DeleteIqcDefectHandlingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIqcDefectHandlingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新进货检验不良处理记录表(IqcDefectHandling)Handling
    /// </summary>
    [HttpPut("status-handling")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:update", "更新进货检验不良处理记录表(IqcDefectHandling)Handling")]
    public async Task<ActionResult<TaktIqcDefectHandlingDto>> UpdateIqcDefectHandlingHandlingStatusAsync([FromBody] TaktIqcDefectHandlingHandlingStatusDto dto)
    {
        var result = await _service.UpdateIqcDefectHandlingHandlingStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取进货检验不良处理记录表(IqcDefectHandling)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:import", "获取进货检验不良处理记录表(IqcDefectHandling)导入模板")]
    public async Task<IActionResult> GetIqcDefectHandlingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIqcDefectHandlingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:import", "导入进货检验不良处理记录表(IqcDefectHandling)")]
    public async Task<ActionResult<object>> ImportIqcDefectHandlingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIqcDefectHandlingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:iqcdefecthandling:export", "导出进货检验不良处理记录表(IqcDefectHandling)")]
    public async Task<IActionResult> ExportIqcDefectHandlingAsync([FromBody] TaktIqcDefectHandlingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIqcDefectHandlingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
