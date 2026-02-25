// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.DocsCenter
// 文件名称：TaktDocumentsController.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心文档控制器，提供文档管理的 RESTful API
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.DocsCenter;
using Takt.Application.Services.Routine.DocsCenter;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.DocsCenter;

/// <summary>
/// 文控中心文档控制器
/// </summary>
[Route("api/[controller]", Name = "文控文档")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:document", "文控文档")]
public class TaktDocumentsController : TaktControllerBase
{
    private readonly ITaktDocumentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">文档服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDocumentsController(
        ITaktDocumentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取文档列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:document:list", "查询文档列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDocumentDto>>> GetListAsync([FromQuery] TaktDocumentQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据 ID 获取文档
    /// </summary>
    /// <param name="id">文档 ID</param>
    /// <returns>文档 DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:document:query", "查询文档详情")]
    public async Task<ActionResult<TaktDocumentDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 获取文档选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("routine:document:list", "查询文档选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _service.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建文档
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>文档 DTO</returns>
    [HttpPost]
    [TaktPermission("routine:document:create", "创建文档")]
    public async Task<ActionResult<TaktDocumentDto>> CreateAsync([FromBody] TaktDocumentCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.DocumentId }, item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新文档
    /// </summary>
    /// <param name="id">文档 ID</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>文档 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:document:update", "更新文档")]
    public async Task<ActionResult<TaktDocumentDto>> UpdateAsync(long id, [FromBody] TaktDocumentUpdateDto dto)
    {
        try
        {
            var item = await _service.UpdateAsync(id, dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除文档
    /// </summary>
    /// <param name="id">文档 ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:document:delete", "删除文档")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除文档
    /// </summary>
    /// <param name="ids">文档 ID 列表</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    [TaktPermission("routine:document:delete", "批量删除文档")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        await _service.DeleteAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新文档状态
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>文档 DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:document:status", "更新文档状态")]
    public async Task<ActionResult<TaktDocumentDto>> UpdateStatusAsync([FromBody] TaktDocumentStatusDto dto)
    {
        try
        {
            var item = await _service.UpdateStatusAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
