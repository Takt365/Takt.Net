// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.SignalR
// 文件名称：TaktOnlinesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线用户控制器，提供在线用户管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Application.Services.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.SignalR;

/// <summary>
/// 在线用户控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[ApiController]
[Route("api/[controller]", Name = "在线用户")]
[ApiModule("Routine", "在线用户")]
[Authorize]
[TaktPermission("routine:tasks:online", "在线用户管理")]
public class TaktOnlinesController : TaktControllerBase
{
    private readonly ITaktOnlineService _onlineService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineService">在线用户服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOnlinesController(
        ITaktOnlineService onlineService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _onlineService = onlineService;
    }

    /// <summary>
    /// 获取在线用户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:online:list", "查询在线用户列表")]
    public async Task<ActionResult<TaktPagedResult<TaktOnlineDto>>> GetListAsync([FromQuery] TaktOnlineQueryDto queryDto)
    {
        try
        {
            var result = await _onlineService.GetListAsync(queryDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 根据ID获取在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>在线用户DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:online:query", "查询在线用户详情")]
    public async Task<ActionResult<TaktOnlineDto>> GetByIdAsync(long id)
    {
        try
        {
            var online = await _onlineService.GetByIdAsync(id);
            if (online == null)
                return NotFound();
            return Ok(online);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 根据连接ID获取在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>在线用户DTO</returns>
    [HttpGet("connection/{connectionId}")]
    [TaktPermission("routine:tasks:online:query", "根据连接ID查询在线用户")]
    public async Task<ActionResult<TaktOnlineDto>> GetByConnectionIdAsync(string connectionId)
    {
        try
        {
            var online = await _onlineService.GetByConnectionIdAsync(connectionId);
            if (online == null)
                return NotFound();
            return Ok(online);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 创建在线用户
    /// </summary>
    /// <param name="dto">创建在线用户DTO</param>
    /// <returns>在线用户DTO</returns>
    [HttpPost]
    [TaktPermission("routine:tasks:online:create", "创建在线用户")]
    public async Task<ActionResult<TaktOnlineDto>> CreateAsync([FromBody] TaktOnlineCreateDto dto)
    {
        try
        {
            var online = await _onlineService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = online.OnlineId }, online);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>任务</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:online:delete", "删除在线用户")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        try
        {
            await _onlineService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 根据连接ID删除在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>任务</returns>
    [HttpDelete("connection/{connectionId}")]
    [TaktPermission("routine:tasks:online:delete", "根据连接ID删除在线用户")]
    public async Task<ActionResult> DeleteByConnectionIdAsync(string connectionId)
    {
        try
        {
            await _onlineService.DeleteByConnectionIdAsync(connectionId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量删除在线用户
    /// </summary>
    /// <param name="ids">在线用户ID列表</param>
    /// <returns>任务</returns>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:online:delete", "批量删除在线用户")]
    public async Task<ActionResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        try
        {
            await _onlineService.DeleteBatchAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新最后活动时间
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>任务</returns>
    [HttpPut("active/{connectionId}")]
    [TaktPermission("routine:tasks:online:update", "更新最后活动时间")]
    public async Task<ActionResult> UpdateLastActiveTimeAsync(string connectionId)
    {
        try
        {
            await _onlineService.UpdateLastActiveTimeAsync(connectionId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出在线用户
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpGet("export")]
    [TaktPermission("routine:tasks:online:export", "导出在线用户")]
    public async Task<ActionResult> ExportAsync(
        [FromQuery] TaktOnlineQueryDto queryDto,
        [FromQuery] string? sheetName = null,
        [FromQuery] string? fileName = null)
    {
        try
        {
            var (exportFileName, content) = await _onlineService.ExportAsync(queryDto, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(exportFileName), exportFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
