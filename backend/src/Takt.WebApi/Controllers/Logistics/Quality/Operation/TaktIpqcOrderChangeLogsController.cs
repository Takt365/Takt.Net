// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderChangeLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：制程检验单变更日志表控制器，提供IpqcOrderChangeLog管理的RESTful API接口
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
/// 制程检验单变更日志表控制器
/// </summary>
[Route("api/[controller]", Name = "制程检验单变更日志表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:ipqcorderchangelog", "制程检验单变更日志表管理")]
public class TaktIpqcOrderChangeLogsController : TaktControllerBase
{
    private readonly ITaktIpqcOrderChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderChangeLogsController(
        ITaktIpqcOrderChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取制程检验单变更日志表(IpqcOrderChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:list", "查询制程检验单变更日志表(IpqcOrderChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIpqcOrderChangeLogDto>>> GetIpqcOrderChangeLogListAsync([FromQuery] TaktIpqcOrderChangeLogQueryDto queryDto)
    {
        var result = await _service.GetIpqcOrderChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:query", "查询制程检验单变更日志表(IpqcOrderChangeLog)详情")]
    public async Task<ActionResult<TaktIpqcOrderChangeLogDto>> GetIpqcOrderChangeLogByIdAsync(long id)
    {
        var item = await _service.GetIpqcOrderChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取制程检验单变更日志表(IpqcOrderChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:query", "查询制程检验单变更日志表(IpqcOrderChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIpqcOrderChangeLogOptionsAsync()
    {
        var result = await _service.GetIpqcOrderChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:create", "创建制程检验单变更日志表(IpqcOrderChangeLog)")]
    public async Task<ActionResult<TaktIpqcOrderChangeLogDto>> CreateIpqcOrderChangeLogAsync([FromBody] TaktIpqcOrderChangeLogCreateDto dto)
    {
        var result = await _service.CreateIpqcOrderChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetIpqcOrderChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:update", "更新制程检验单变更日志表(IpqcOrderChangeLog)")]
    public async Task<ActionResult<TaktIpqcOrderChangeLogDto>> UpdateIpqcOrderChangeLogAsync(long id, [FromBody] TaktIpqcOrderChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateIpqcOrderChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:delete", "删除制程检验单变更日志表(IpqcOrderChangeLog)")]
    public async Task<ActionResult> DeleteIpqcOrderChangeLogByIdAsync(long id)
    {
        await _service.DeleteIpqcOrderChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:delete", "批量删除制程检验单变更日志表(IpqcOrderChangeLog)")]
    public async Task<ActionResult> DeleteIpqcOrderChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIpqcOrderChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取制程检验单变更日志表(IpqcOrderChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:import", "获取制程检验单变更日志表(IpqcOrderChangeLog)导入模板")]
    public async Task<IActionResult> GetIpqcOrderChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIpqcOrderChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:import", "导入制程检验单变更日志表(IpqcOrderChangeLog)")]
    public async Task<ActionResult<object>> ImportIpqcOrderChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIpqcOrderChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:ipqcorderchangelog:export", "导出制程检验单变更日志表(IpqcOrderChangeLog)")]
    public async Task<IActionResult> ExportIpqcOrderChangeLogAsync([FromBody] TaktIpqcOrderChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIpqcOrderChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
