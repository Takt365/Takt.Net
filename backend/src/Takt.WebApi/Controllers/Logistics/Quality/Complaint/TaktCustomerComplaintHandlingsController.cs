// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintHandlingsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉处理记录表控制器，提供CustomerComplaintHandling管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Application.Services.Logistics.Quality.Complaint;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Complaint;

/// <summary>
/// 客诉处理记录表控制器
/// </summary>
[Route("api/[controller]", Name = "客诉处理记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:complaint:customercomplainthandling", "客诉处理记录表管理")]
public class TaktCustomerComplaintHandlingsController : TaktControllerBase
{
    private readonly ITaktCustomerComplaintHandlingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingsController(
        ITaktCustomerComplaintHandlingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取客诉处理记录表(CustomerComplaintHandling)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:list", "查询客诉处理记录表(CustomerComplaintHandling)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCustomerComplaintHandlingDto>>> GetCustomerComplaintHandlingListAsync([FromQuery] TaktCustomerComplaintHandlingQueryDto queryDto)
    {
        var result = await _service.GetCustomerComplaintHandlingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:query", "查询客诉处理记录表(CustomerComplaintHandling)详情")]
    public async Task<ActionResult<TaktCustomerComplaintHandlingDto>> GetCustomerComplaintHandlingByIdAsync(long id)
    {
        var item = await _service.GetCustomerComplaintHandlingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取客诉处理记录表(CustomerComplaintHandling)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:query", "查询客诉处理记录表(CustomerComplaintHandling)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCustomerComplaintHandlingOptionsAsync()
    {
        var result = await _service.GetCustomerComplaintHandlingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:create", "创建客诉处理记录表(CustomerComplaintHandling)")]
    public async Task<ActionResult<TaktCustomerComplaintHandlingDto>> CreateCustomerComplaintHandlingAsync([FromBody] TaktCustomerComplaintHandlingCreateDto dto)
    {
        var result = await _service.CreateCustomerComplaintHandlingAsync(dto);
        return CreatedAtAction(nameof(GetCustomerComplaintHandlingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:update", "更新客诉处理记录表(CustomerComplaintHandling)")]
    public async Task<ActionResult<TaktCustomerComplaintHandlingDto>> UpdateCustomerComplaintHandlingAsync(long id, [FromBody] TaktCustomerComplaintHandlingUpdateDto dto)
    {
        var result = await _service.UpdateCustomerComplaintHandlingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:delete", "删除客诉处理记录表(CustomerComplaintHandling)")]
    public async Task<ActionResult> DeleteCustomerComplaintHandlingByIdAsync(long id)
    {
        await _service.DeleteCustomerComplaintHandlingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:delete", "批量删除客诉处理记录表(CustomerComplaintHandling)")]
    public async Task<ActionResult> DeleteCustomerComplaintHandlingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCustomerComplaintHandlingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新客诉处理记录表(CustomerComplaintHandling)Handling
    /// </summary>
    [HttpPut("status-handling")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:update", "更新客诉处理记录表(CustomerComplaintHandling)Handling")]
    public async Task<ActionResult<TaktCustomerComplaintHandlingDto>> UpdateCustomerComplaintHandlingHandlingStatusAsync([FromBody] TaktCustomerComplaintHandlingHandlingStatusDto dto)
    {
        var result = await _service.UpdateCustomerComplaintHandlingHandlingStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取客诉处理记录表(CustomerComplaintHandling)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:import", "获取客诉处理记录表(CustomerComplaintHandling)导入模板")]
    public async Task<IActionResult> GetCustomerComplaintHandlingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCustomerComplaintHandlingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:import", "导入客诉处理记录表(CustomerComplaintHandling)")]
    public async Task<ActionResult<object>> ImportCustomerComplaintHandlingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCustomerComplaintHandlingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:complaint:customercomplainthandling:export", "导出客诉处理记录表(CustomerComplaintHandling)")]
    public async Task<IActionResult> ExportCustomerComplaintHandlingAsync([FromBody] TaktCustomerComplaintHandlingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCustomerComplaintHandlingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
