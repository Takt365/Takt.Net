// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Logging
// 文件名称：TaktAopLogsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt差异日志控制器，提供差异日志管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Statistics.Logging;

/// <summary>
/// 差异日志控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[ApiController]
[Route("api/[controller]", Name = "差异日志")]
[ApiModule("Statistics", "统计看板")]
[Authorize]
[TaktPermission("statistics:logging:aoplog", "差异日志管理")]
public class TaktAopLogsController : TaktControllerBase
{
    private readonly ITaktAopLogService _aopLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="aopLogService">差异日志服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAopLogsController(
        ITaktAopLogService aopLogService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _aopLogService = aopLogService;
    }

    /// <summary>
    /// 获取差异日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("statistics:logging:aoplog:list", "查询差异日志列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAopLogDto>>> GetAopLogListAsync([FromQuery] TaktAopLogQueryDto queryDto)
    {
        try
        {
            var result = await _aopLogService.GetAopLogListAsync(queryDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 根据ID获取差异日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>差异日志DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("statistics:logging:aoplog:query", "查询差异日志详情")]
    public async Task<ActionResult<TaktAopLogDto>> GetAopLogByIdAsync(long id)
    {
        try
        {
            var log = await _aopLogService.GetAopLogByIdAsync(id);
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
    /// 创建差异日志
    /// </summary>
    /// <param name="dto">创建差异日志DTO</param>
    /// <returns>差异日志DTO</returns>
    [HttpPost]
    [TaktPermission("statistics:logging:aoplog:create", "创建差异日志")]
    public async Task<ActionResult<TaktAopLogDto>> CreateAopLogAsync([FromBody] TaktAopLogCreateDto dto)
    {
        try
        {
            var log = await _aopLogService.CreateAopLogAsync(dto);
            return CreatedAtAction(nameof(GetAopLogByIdAsync), new { id = log.AopLogId }, log);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除差异日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:logging:aoplog:delete", "删除差异日志")]
    public async Task<ActionResult> DeleteAopLogByIdAsync(long id)
    {
        try
        {
            await _aopLogService.DeleteAopLogByIdAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量删除差异日志
    /// </summary>
    /// <param name="ids">日志ID列表</param>
    /// <returns>任务</returns>
    [HttpDelete("batch")]
    [TaktPermission("statistics:logging:aoplog:delete", "批量删除差异日志")]
    public async Task<ActionResult> DeleteAopLogBatchAsync([FromBody] List<long> ids)
    {
        try
        {
            await _aopLogService.DeleteAopLogBatchAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出差异日志
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpGet("export")]
    [TaktPermission("statistics:logging:aoplog:export", "导出差异日志")]
    public async Task<ActionResult> ExportAopLogAsync(
        [FromQuery] TaktAopLogQueryDto queryDto,
        [FromQuery] string? sheetName = null,
        [FromQuery] string? fileName = null)
    {
        try
        {
            var (exportFileName, content) = await _aopLogService.ExportAopLogAsync(queryDto, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(exportFileName), exportFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
