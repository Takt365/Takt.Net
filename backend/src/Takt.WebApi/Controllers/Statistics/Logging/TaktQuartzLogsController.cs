// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Statistics.Logging
// 文件名称：TaktQuartzLogsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt任务日志控制器，提供任务日志管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services.Logging;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Statistics.Logging;

/// <summary>
/// 任务日志控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[ApiController]
[Route("api/[controller]", Name = "任务日志")]
[ApiModule("Logging", "日志管理")]
[Authorize]
[TaktPermission("statistics:logging:quartzlog", "任务日志管理")]
public class TaktQuartzLogsController : TaktControllerBase
{
    private readonly ITaktQuartzLogService _quartzLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="quartzLogService">任务日志服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQuartzLogsController(
        ITaktQuartzLogService quartzLogService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _quartzLogService = quartzLogService;
    }

    /// <summary>
    /// 获取任务日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("statistics:logging:quartzlog:list", "查询任务日志列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQuartzLogDto>>> GetListAsync([FromQuery] TaktQuartzLogQueryDto queryDto)
    {
        try
        {
            var result = await _quartzLogService.GetListAsync(queryDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 根据ID获取任务日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务日志DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("statistics:logging:quartzlog:query", "查询任务日志详情")]
    public async Task<ActionResult<TaktQuartzLogDto>> GetByIdAsync(long id)
    {
        try
        {
            var log = await _quartzLogService.GetByIdAsync(id);
            if (log == null)
                return NotFound();
            return Ok(log);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 创建任务日志
    /// </summary>
    /// <param name="dto">创建任务日志DTO</param>
    /// <returns>任务日志DTO</returns>
    [HttpPost]
    [TaktPermission("statistics:logging:quartzlog:create", "创建任务日志")]
    public async Task<ActionResult<TaktQuartzLogDto>> CreateAsync([FromBody] TaktCreateQuartzLogDto dto)
    {
        try
        {
            var log = await _quartzLogService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = log.QuartzLogId }, log);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除任务日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:logging:quartzlog:delete", "删除任务日志")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        try
        {
            await _quartzLogService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量删除任务日志
    /// </summary>
    /// <param name="ids">日志ID列表</param>
    /// <returns>任务</returns>
    [HttpDelete("batch")]
    [TaktPermission("statistics:logging:quartzlog:delete", "批量删除任务日志")]
    public async Task<ActionResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        try
        {
            await _quartzLogService.DeleteBatchAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出任务日志
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [TaktPermission("statistics:logging:quartzlog:export", "导出任务日志")]
    public async Task<ActionResult> ExportAsync(
        [FromQuery] TaktQuartzLogQueryDto queryDto,
        [FromQuery] string? sheetName = null,
        [FromQuery] string? fileName = null)
    {
        try
        {
            var (exportFileName, content) = await _quartzLogService.ExportAsync(queryDto, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", exportFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
