// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Reception
// 文件名称：TaktCustomerReceptionsController.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：客户接待控制器，提供客户接待管理的 RESTful API
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Reception;
using Takt.Application.Services.Routine.Reception;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Reception;

/// <summary>
/// 客户接待控制器
/// </summary>
[Route("api/[controller]", Name = "客户接待")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:reception", "客户接待")]
public class TaktCustomerReceptionsController : TaktControllerBase
{
    private readonly ITaktCustomerReceptionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerReceptionsController(
        ITaktCustomerReceptionService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取客户接待列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:reception:list", "查询客户接待列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCustomerReceptionDto>>> GetListAsync([FromQuery] TaktCustomerReceptionQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据 ID 获取客户接待
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:reception:query", "查询客户接待详情")]
    public async Task<ActionResult<TaktCustomerReceptionDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 创建客户接待
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:reception:create", "创建客户接待")]
    public async Task<ActionResult<TaktCustomerReceptionDto>> CreateAsync([FromBody] TaktCustomerReceptionCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.ReceptionId }, item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新客户接待
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:reception:update", "更新客户接待")]
    public async Task<ActionResult<TaktCustomerReceptionDto>> UpdateAsync(long id, [FromBody] TaktCustomerReceptionUpdateDto dto)
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
    /// 删除客户接待
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:reception:delete", "删除客户接待")]
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
    /// 批量删除客户接待
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:reception:delete", "批量删除客户接待")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        try
        {
            await _service.DeleteAsync(ids ?? Array.Empty<long>());
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出客户接待
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:reception:export", "导出客户接待")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktCustomerReceptionQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
