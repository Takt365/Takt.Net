// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeTransfersController.cs
// 功能描述：员工调动控制器（转岗/调岗 CRUD + 状态更新 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工调动控制器（转岗/调岗，与流程审批关联；权限前缀 humanresource:personnel:employeetransfer）
/// </summary>
[Route("api/[controller]", Name = "员工调动")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeetransfer:list", "员工调动管理")]
public class TaktEmployeeTransfersController : TaktControllerBase
{
    private readonly ITaktEmployeeTransferService _service;

    public TaktEmployeeTransfersController(
        ITaktEmployeeTransferService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 分页查询员工调动列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeetransfer:list", "员工调动列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeTransferDto>>> GetEmployeeTransferListAsync([FromQuery] TaktEmployeeTransferQueryDto query)
    {
        var result = await _service.GetEmployeeTransferListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取员工调动详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeetransfer:detail", "员工调动详情")]
    public async Task<ActionResult<TaktEmployeeTransferDto?>> GetEmployeeTransferByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeTransferByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建员工调动（草稿，不发起流程）
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeetransfer:create", "创建员工调动")]
    public async Task<ActionResult<TaktEmployeeTransferDto>> CreateEmployeeTransferAsync([FromBody] TaktEmployeeTransferCreateDto dto)
    {
        var created = await _service.CreateEmployeeTransferAsync(dto);
        return Ok(created);
    }

    /// <summary>
    /// 更新员工调动
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeetransfer:update", "更新员工调动")]
    public async Task<ActionResult<TaktEmployeeTransferDto>> UpdateEmployeeTransferAsync(long id, [FromBody] TaktEmployeeTransferUpdateDto dto)
    {
        if (dto.EmployeeTransferId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        var updated = await _service.UpdateEmployeeTransferAsync(id, dto);
        return Ok(updated);
    }

    /// <summary>
    /// 删除员工调动（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeetransfer:delete", "删除员工调动")]
    public async Task<IActionResult> DeleteEmployeeTransferByIdAsync(long id)
    {
        await _service.DeleteEmployeeTransferByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工调动
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeetransfer:delete", "批量删除员工调动")]
    public async Task<IActionResult> DeleteEmployeeTransferBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeTransferBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 更新员工调动状态（流程回调时调用）
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:personnel:employeetransfer:update", "更新员工调动状态")]
    public async Task<ActionResult<TaktEmployeeTransferDto>> UpdateEmployeeTransferStatusAsync([FromBody] TaktEmployeeTransferStatusDto dto)
    {
        var updated = await _service.UpdateEmployeeTransferStatusAsync(dto);
        return Ok(updated);
    }

    /// <summary>
    /// 导出员工调动
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeetransfer:export", "导出员工调动")]
    public async Task<IActionResult> ExportEmployeeTransferAsync([FromBody] TaktEmployeeTransferQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportEmployeeTransferAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
