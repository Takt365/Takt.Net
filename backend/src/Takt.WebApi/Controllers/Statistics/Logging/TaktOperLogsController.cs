// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Logging
// 文件名称：TaktOperLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：操作日志表控制器，提供OperLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Statistics.Logging;

/// <summary>
/// 操作日志表控制器
/// </summary>
[Route("api/[controller]", Name = "操作日志表")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:logging:operlog", "操作日志表管理")]
public class TaktOperLogsController : TaktControllerBase
{
    private readonly ITaktOperLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogsController(
        ITaktOperLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取操作日志表(OperLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("statistics:logging:operlog:list", "查询操作日志表(OperLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktOperLogDto>>> GetOperLogListAsync([FromQuery] TaktOperLogQueryDto queryDto)
    {
        var result = await _service.GetOperLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取操作日志表(OperLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("statistics:logging:operlog:query", "查询操作日志表(OperLog)详情")]
    public async Task<ActionResult<TaktOperLogDto>> GetOperLogByIdAsync(long id)
    {
        var item = await _service.GetOperLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取操作日志表(OperLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("statistics:logging:operlog:query", "查询操作日志表(OperLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOperLogOptionsAsync()
    {
        var result = await _service.GetOperLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建操作日志表(OperLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("statistics:logging:operlog:create", "创建操作日志表(OperLog)")]
    public async Task<ActionResult<TaktOperLogDto>> CreateOperLogAsync([FromBody] TaktOperLogCreateDto dto)
    {
        var result = await _service.CreateOperLogAsync(dto);
        return CreatedAtAction(nameof(GetOperLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新操作日志表(OperLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("statistics:logging:operlog:update", "更新操作日志表(OperLog)")]
    public async Task<ActionResult<TaktOperLogDto>> UpdateOperLogAsync(long id, [FromBody] TaktOperLogUpdateDto dto)
    {
        var result = await _service.UpdateOperLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除操作日志表(OperLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:logging:operlog:delete", "删除操作日志表(OperLog)")]
    public async Task<ActionResult> DeleteOperLogByIdAsync(long id)
    {
        await _service.DeleteOperLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除操作日志表(OperLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("statistics:logging:operlog:delete", "批量删除操作日志表(OperLog)")]
    public async Task<ActionResult> DeleteOperLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteOperLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新操作日志表(OperLog)Oper
    /// </summary>
    [HttpPut("status-oper")]
    [TaktPermission("statistics:logging:operlog:update", "更新操作日志表(OperLog)Oper")]
    public async Task<ActionResult<TaktOperLogDto>> UpdateOperLogOperStatusAsync([FromBody] TaktOperLogOperStatusDto dto)
    {
        var result = await _service.UpdateOperLogOperStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取操作日志表(OperLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("statistics:logging:operlog:import", "获取操作日志表(OperLog)导入模板")]
    public async Task<IActionResult> GetOperLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetOperLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入操作日志表(OperLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("statistics:logging:operlog:import", "导入操作日志表(OperLog)")]
    public async Task<ActionResult<object>> ImportOperLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportOperLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出操作日志表(OperLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("statistics:logging:operlog:export", "导出操作日志表(OperLog)")]
    public async Task<IActionResult> ExportOperLogAsync([FromBody] TaktOperLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportOperLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
