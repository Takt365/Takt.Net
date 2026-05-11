// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Logging
// 文件名称：TaktLoginLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：登录日志表控制器，提供LoginLog管理的RESTful API接口
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
/// 登录日志表控制器
/// </summary>
[Route("api/[controller]", Name = "登录日志表")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:logging:loginlog", "登录日志表管理")]
public class TaktLoginLogsController : TaktControllerBase
{
    private readonly ITaktLoginLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogsController(
        ITaktLoginLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取登录日志表(LoginLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("statistics:logging:loginlog:list", "查询登录日志表(LoginLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktLoginLogDto>>> GetLoginLogListAsync([FromQuery] TaktLoginLogQueryDto queryDto)
    {
        var result = await _service.GetLoginLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取登录日志表(LoginLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("statistics:logging:loginlog:query", "查询登录日志表(LoginLog)详情")]
    public async Task<ActionResult<TaktLoginLogDto>> GetLoginLogByIdAsync(long id)
    {
        var item = await _service.GetLoginLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取登录日志表(LoginLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("statistics:logging:loginlog:query", "查询登录日志表(LoginLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetLoginLogOptionsAsync()
    {
        var result = await _service.GetLoginLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建登录日志表(LoginLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("statistics:logging:loginlog:create", "创建登录日志表(LoginLog)")]
    public async Task<ActionResult<TaktLoginLogDto>> CreateLoginLogAsync([FromBody] TaktLoginLogCreateDto dto)
    {
        var result = await _service.CreateLoginLogAsync(dto);
        return CreatedAtAction(nameof(GetLoginLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新登录日志表(LoginLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("statistics:logging:loginlog:update", "更新登录日志表(LoginLog)")]
    public async Task<ActionResult<TaktLoginLogDto>> UpdateLoginLogAsync(long id, [FromBody] TaktLoginLogUpdateDto dto)
    {
        var result = await _service.UpdateLoginLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除登录日志表(LoginLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:logging:loginlog:delete", "删除登录日志表(LoginLog)")]
    public async Task<ActionResult> DeleteLoginLogByIdAsync(long id)
    {
        await _service.DeleteLoginLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除登录日志表(LoginLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("statistics:logging:loginlog:delete", "批量删除登录日志表(LoginLog)")]
    public async Task<ActionResult> DeleteLoginLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteLoginLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新登录日志表(LoginLog)Login
    /// </summary>
    [HttpPut("status-login")]
    [TaktPermission("statistics:logging:loginlog:update", "更新登录日志表(LoginLog)Login")]
    public async Task<ActionResult<TaktLoginLogDto>> UpdateLoginLogLoginStatusAsync([FromBody] TaktLoginLogLoginStatusDto dto)
    {
        var result = await _service.UpdateLoginLogLoginStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取登录日志表(LoginLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("statistics:logging:loginlog:import", "获取登录日志表(LoginLog)导入模板")]
    public async Task<IActionResult> GetLoginLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetLoginLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入登录日志表(LoginLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("statistics:logging:loginlog:import", "导入登录日志表(LoginLog)")]
    public async Task<ActionResult<object>> ImportLoginLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportLoginLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出登录日志表(LoginLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("statistics:logging:loginlog:export", "导出登录日志表(LoginLog)")]
    public async Task<IActionResult> ExportLoginLogAsync([FromBody] TaktLoginLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportLoginLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
