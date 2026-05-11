// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Logging
// 文件名称：TaktAopLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：差异日志表控制器，提供AopLog管理的RESTful API接口
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
/// 差异日志表控制器
/// </summary>
[Route("api/[controller]", Name = "差异日志表")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:logging:aoplog", "差异日志表管理")]
public class TaktAopLogsController : TaktControllerBase
{
    private readonly ITaktAopLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogsController(
        ITaktAopLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取差异日志表(AopLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("statistics:logging:aoplog:list", "查询差异日志表(AopLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAopLogDto>>> GetAopLogListAsync([FromQuery] TaktAopLogQueryDto queryDto)
    {
        var result = await _service.GetAopLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取差异日志表(AopLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("statistics:logging:aoplog:query", "查询差异日志表(AopLog)详情")]
    public async Task<ActionResult<TaktAopLogDto>> GetAopLogByIdAsync(long id)
    {
        var item = await _service.GetAopLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取差异日志表(AopLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("statistics:logging:aoplog:query", "查询差异日志表(AopLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAopLogOptionsAsync()
    {
        var result = await _service.GetAopLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建差异日志表(AopLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("statistics:logging:aoplog:create", "创建差异日志表(AopLog)")]
    public async Task<ActionResult<TaktAopLogDto>> CreateAopLogAsync([FromBody] TaktAopLogCreateDto dto)
    {
        var result = await _service.CreateAopLogAsync(dto);
        return CreatedAtAction(nameof(GetAopLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新差异日志表(AopLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("statistics:logging:aoplog:update", "更新差异日志表(AopLog)")]
    public async Task<ActionResult<TaktAopLogDto>> UpdateAopLogAsync(long id, [FromBody] TaktAopLogUpdateDto dto)
    {
        var result = await _service.UpdateAopLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除差异日志表(AopLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:logging:aoplog:delete", "删除差异日志表(AopLog)")]
    public async Task<ActionResult> DeleteAopLogByIdAsync(long id)
    {
        await _service.DeleteAopLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除差异日志表(AopLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("statistics:logging:aoplog:delete", "批量删除差异日志表(AopLog)")]
    public async Task<ActionResult> DeleteAopLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAopLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取差异日志表(AopLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("statistics:logging:aoplog:import", "获取差异日志表(AopLog)导入模板")]
    public async Task<IActionResult> GetAopLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAopLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入差异日志表(AopLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("statistics:logging:aoplog:import", "导入差异日志表(AopLog)")]
    public async Task<ActionResult<object>> ImportAopLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAopLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出差异日志表(AopLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("statistics:logging:aoplog:export", "导出差异日志表(AopLog)")]
    public async Task<IActionResult> ExportAopLogAsync([FromBody] TaktAopLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAopLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
