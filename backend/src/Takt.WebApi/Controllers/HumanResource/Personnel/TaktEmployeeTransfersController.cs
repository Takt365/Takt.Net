// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeTransfersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工调动表控制器，提供EmployeeTransfer管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工调动表控制器
/// </summary>
[Route("api/[controller]", Name = "员工调动表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeetransfer", "员工调动表管理")]
public class TaktEmployeeTransfersController : TaktControllerBase
{
    private readonly ITaktEmployeeTransferService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
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
    /// 获取员工调动表(EmployeeTransfer)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeetransfer:list", "查询员工调动表(EmployeeTransfer)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeTransferDto>>> GetEmployeeTransferListAsync([FromQuery] TaktEmployeeTransferQueryDto queryDto)
    {
        var result = await _service.GetEmployeeTransferListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工调动表(EmployeeTransfer)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeetransfer:query", "查询员工调动表(EmployeeTransfer)详情")]
    public async Task<ActionResult<TaktEmployeeTransferDto>> GetEmployeeTransferByIdAsync(long id)
    {
        var item = await _service.GetEmployeeTransferByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工调动表(EmployeeTransfer)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeetransfer:query", "查询员工调动表(EmployeeTransfer)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeTransferOptionsAsync()
    {
        var result = await _service.GetEmployeeTransferOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工调动表(EmployeeTransfer)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeetransfer:create", "创建员工调动表(EmployeeTransfer)")]
    public async Task<ActionResult<TaktEmployeeTransferDto>> CreateEmployeeTransferAsync([FromBody] TaktEmployeeTransferCreateDto dto)
    {
        var result = await _service.CreateEmployeeTransferAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeTransferByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工调动表(EmployeeTransfer)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeetransfer:update", "更新员工调动表(EmployeeTransfer)")]
    public async Task<ActionResult<TaktEmployeeTransferDto>> UpdateEmployeeTransferAsync(long id, [FromBody] TaktEmployeeTransferUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeTransferAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工调动表(EmployeeTransfer)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeetransfer:delete", "删除员工调动表(EmployeeTransfer)")]
    public async Task<ActionResult> DeleteEmployeeTransferByIdAsync(long id)
    {
        await _service.DeleteEmployeeTransferByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工调动表(EmployeeTransfer)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeetransfer:delete", "批量删除员工调动表(EmployeeTransfer)")]
    public async Task<ActionResult> DeleteEmployeeTransferBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeTransferBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新员工调动表(EmployeeTransfer)Transfer
    /// </summary>
    [HttpPut("status-transfer")]
    [TaktPermission("humanresource:personnel:employeetransfer:update", "更新员工调动表(EmployeeTransfer)Transfer")]
    public async Task<ActionResult<TaktEmployeeTransferDto>> UpdateEmployeeTransferTransferStatusAsync([FromBody] TaktEmployeeTransferTransferStatusDto dto)
    {
        var result = await _service.UpdateEmployeeTransferTransferStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取员工调动表(EmployeeTransfer)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeetransfer:import", "获取员工调动表(EmployeeTransfer)导入模板")]
    public async Task<IActionResult> GetEmployeeTransferTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeTransferTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工调动表(EmployeeTransfer)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeetransfer:import", "导入员工调动表(EmployeeTransfer)")]
    public async Task<ActionResult<object>> ImportEmployeeTransferAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeTransferAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工调动表(EmployeeTransfer)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeetransfer:export", "导出员工调动表(EmployeeTransfer)")]
    public async Task<IActionResult> ExportEmployeeTransferAsync([FromBody] TaktEmployeeTransferQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeTransferAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
