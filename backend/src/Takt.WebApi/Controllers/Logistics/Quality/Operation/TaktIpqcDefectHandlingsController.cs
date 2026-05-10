// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIpqcDefectHandlingsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：制程检验不良处理记录表控制器，提供IpqcDefectHandling管理的RESTful API接口
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
/// 制程检验不良处理记录表控制器
/// </summary>
[Route("api/[controller]", Name = "制程检验不良处理记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:ipqcdefecthandling", "制程检验不良处理记录表管理")]
public class TaktIpqcDefectHandlingsController : TaktControllerBase
{
    private readonly ITaktIpqcDefectHandlingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingsController(
        ITaktIpqcDefectHandlingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取制程检验不良处理记录表(IpqcDefectHandling)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:list", "查询制程检验不良处理记录表(IpqcDefectHandling)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIpqcDefectHandlingDto>>> GetIpqcDefectHandlingListAsync([FromQuery] TaktIpqcDefectHandlingQueryDto queryDto)
    {
        var result = await _service.GetIpqcDefectHandlingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取制程检验不良处理记录表(IpqcDefectHandling)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:query", "查询制程检验不良处理记录表(IpqcDefectHandling)详情")]
    public async Task<ActionResult<TaktIpqcDefectHandlingDto>> GetIpqcDefectHandlingByIdAsync(long id)
    {
        var item = await _service.GetIpqcDefectHandlingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取制程检验不良处理记录表(IpqcDefectHandling)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:query", "查询制程检验不良处理记录表(IpqcDefectHandling)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIpqcDefectHandlingOptionsAsync()
    {
        var result = await _service.GetIpqcDefectHandlingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建制程检验不良处理记录表(IpqcDefectHandling)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:create", "创建制程检验不良处理记录表(IpqcDefectHandling)")]
    public async Task<ActionResult<TaktIpqcDefectHandlingDto>> CreateIpqcDefectHandlingAsync([FromBody] TaktIpqcDefectHandlingCreateDto dto)
    {
        var result = await _service.CreateIpqcDefectHandlingAsync(dto);
        return CreatedAtAction(nameof(GetIpqcDefectHandlingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新制程检验不良处理记录表(IpqcDefectHandling)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:update", "更新制程检验不良处理记录表(IpqcDefectHandling)")]
    public async Task<ActionResult<TaktIpqcDefectHandlingDto>> UpdateIpqcDefectHandlingAsync(long id, [FromBody] TaktIpqcDefectHandlingUpdateDto dto)
    {
        var result = await _service.UpdateIpqcDefectHandlingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除制程检验不良处理记录表(IpqcDefectHandling)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:delete", "删除制程检验不良处理记录表(IpqcDefectHandling)")]
    public async Task<ActionResult> DeleteIpqcDefectHandlingByIdAsync(long id)
    {
        await _service.DeleteIpqcDefectHandlingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除制程检验不良处理记录表(IpqcDefectHandling)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:delete", "批量删除制程检验不良处理记录表(IpqcDefectHandling)")]
    public async Task<ActionResult> DeleteIpqcDefectHandlingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIpqcDefectHandlingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新制程检验不良处理记录表(IpqcDefectHandling)Handling
    /// </summary>
    [HttpPut("status-handling")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:update", "更新制程检验不良处理记录表(IpqcDefectHandling)Handling")]
    public async Task<ActionResult<TaktIpqcDefectHandlingDto>> UpdateIpqcDefectHandlingHandlingStatusAsync([FromBody] TaktIpqcDefectHandlingHandlingStatusDto dto)
    {
        var result = await _service.UpdateIpqcDefectHandlingHandlingStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取制程检验不良处理记录表(IpqcDefectHandling)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:import", "获取制程检验不良处理记录表(IpqcDefectHandling)导入模板")]
    public async Task<IActionResult> GetIpqcDefectHandlingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIpqcDefectHandlingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入制程检验不良处理记录表(IpqcDefectHandling)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:import", "导入制程检验不良处理记录表(IpqcDefectHandling)")]
    public async Task<ActionResult<object>> ImportIpqcDefectHandlingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIpqcDefectHandlingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出制程检验不良处理记录表(IpqcDefectHandling)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:ipqcdefecthandling:export", "导出制程检验不良处理记录表(IpqcDefectHandling)")]
    public async Task<IActionResult> ExportIpqcDefectHandlingAsync([FromBody] TaktIpqcDefectHandlingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIpqcDefectHandlingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
