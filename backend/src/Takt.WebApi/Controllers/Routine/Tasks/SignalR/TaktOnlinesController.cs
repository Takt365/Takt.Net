// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.SignalR
// 文件名称：TaktOnlinesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：在线用户表控制器，提供Online管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Application.Services.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.SignalR;

/// <summary>
/// 在线用户表控制器
/// </summary>
[Route("api/[controller]", Name = "在线用户表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:signalr:online", "在线用户表管理")]
public class TaktOnlinesController : TaktControllerBase
{
    private readonly ITaktOnlineService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlinesController(
        ITaktOnlineService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取在线用户表(Online)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:signalr:online:list", "查询在线用户表(Online)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktOnlineDto>>> GetOnlineListAsync([FromQuery] TaktOnlineQueryDto queryDto)
    {
        var result = await _service.GetOnlineListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取在线用户表(Online)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:signalr:online:query", "查询在线用户表(Online)详情")]
    public async Task<ActionResult<TaktOnlineDto>> GetOnlineByIdAsync(long id)
    {
        var item = await _service.GetOnlineByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取在线用户表(Online)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:signalr:online:query", "查询在线用户表(Online)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOnlineOptionsAsync()
    {
        var result = await _service.GetOnlineOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建在线用户表(Online)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:signalr:online:create", "创建在线用户表(Online)")]
    public async Task<ActionResult<TaktOnlineDto>> CreateOnlineAsync([FromBody] TaktOnlineCreateDto dto)
    {
        var result = await _service.CreateOnlineAsync(dto);
        return CreatedAtAction(nameof(GetOnlineByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新在线用户表(Online)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:signalr:online:update", "更新在线用户表(Online)")]
    public async Task<ActionResult<TaktOnlineDto>> UpdateOnlineAsync(long id, [FromBody] TaktOnlineUpdateDto dto)
    {
        var result = await _service.UpdateOnlineAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除在线用户表(Online)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:signalr:online:delete", "删除在线用户表(Online)")]
    public async Task<ActionResult> DeleteOnlineByIdAsync(long id)
    {
        await _service.DeleteOnlineByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除在线用户表(Online)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:signalr:online:delete", "批量删除在线用户表(Online)")]
    public async Task<ActionResult> DeleteOnlineBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteOnlineBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新在线用户表(Online)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:signalr:online:update", "更新在线用户表(Online)状态")]
    public async Task<ActionResult<TaktOnlineDto>> UpdateOnlineStatusAsync([FromBody] TaktOnlineStatusDto dto)
    {
        var result = await _service.UpdateOnlineStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取在线用户表(Online)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:signalr:online:import", "获取在线用户表(Online)导入模板")]
    public async Task<IActionResult> GetOnlineTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetOnlineTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入在线用户表(Online)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:signalr:online:import", "导入在线用户表(Online)")]
    public async Task<ActionResult<object>> ImportOnlineAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportOnlineAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出在线用户表(Online)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:signalr:online:export", "导出在线用户表(Online)")]
    public async Task<IActionResult> ExportOnlineAsync([FromBody] TaktOnlineQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportOnlineAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
