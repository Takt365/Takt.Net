// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Logging
// 文件名称：TaktOperLogsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt操作日志控制器，提供操作日志管理的RESTful API接口
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
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.Statistics.Logging;

/// <summary>
/// 操作日志控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[ApiController]
[Route("api/[controller]", Name = "操作日志")]
[ApiModule("Statistics", "统计看板")]
[Authorize]
[TaktPermission("statistics:operlog", "操作日志管理")]
public class TaktOperLogsController : TaktControllerBase
{
    private readonly ITaktOperLogService _operLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="operLogService">操作日志服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOperLogsController(
        ITaktOperLogService operLogService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _operLogService = operLogService;
    }

    /// <summary>
    /// 获取操作日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("statistics:operlog:list", "查询操作日志列表")]
    public async Task<ActionResult<TaktPagedResult<TaktOperLogDto>>> GetListAsync([FromQuery] TaktOperLogQueryDto queryDto)
    {
        try
        {
            var result = await _operLogService.GetListAsync(queryDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 根据ID获取操作日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>操作日志DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("statistics:operlog:query", "查询操作日志详情")]
    public async Task<ActionResult<TaktOperLogDto>> GetByIdAsync(long id)
    {
        try
        {
            var log = await _operLogService.GetByIdAsync(id);
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
    /// 创建操作日志
    /// </summary>
    /// <param name="dto">创建操作日志DTO</param>
    /// <returns>操作日志DTO</returns>
    [HttpPost]
    [TaktPermission("statistics:operlog:create", "创建操作日志")]
    public async Task<ActionResult<TaktOperLogDto>> CreateAsync([FromBody] TaktCreateOperLogDto dto)
    {
        try
        {
            var log = await _operLogService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = log.OperLogId }, log);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除操作日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:operlog:delete", "删除操作日志")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        try
        {
            await _operLogService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量删除操作日志
    /// </summary>
    /// <param name="ids">日志ID列表</param>
    /// <returns>任务</returns>
    [HttpDelete("batch")]
    [TaktPermission("statistics:operlog:delete", "批量删除操作日志")]
    public async Task<ActionResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        try
        {
            await _operLogService.DeleteBatchAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出操作日志
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpGet("export")]
    [TaktPermission("statistics:operlog:export", "导出操作日志")]
    public async Task<ActionResult> ExportAsync(
        [FromQuery] TaktOperLogQueryDto queryDto,
        [FromQuery] string? sheetName = null,
        [FromQuery] string? fileName = null)
    {
        try
        {
            var (exportFileName, content) = await _operLogService.ExportAsync(queryDto, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(exportFileName), exportFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
