// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉主表控制器，提供CustomerComplaint管理的RESTful API接口
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
/// 客诉主表控制器
/// </summary>
[Route("api/[controller]", Name = "客诉主表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:complaint:customercomplaint", "客诉主表管理")]
public class TaktCustomerComplaintsController : TaktControllerBase
{
    private readonly ITaktCustomerComplaintService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintsController(
        ITaktCustomerComplaintService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取客诉主表(CustomerComplaint)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:list", "查询客诉主表(CustomerComplaint)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCustomerComplaintDto>>> GetCustomerComplaintListAsync([FromQuery] TaktCustomerComplaintQueryDto queryDto)
    {
        var result = await _service.GetCustomerComplaintListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取客诉主表(CustomerComplaint)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:query", "查询客诉主表(CustomerComplaint)详情")]
    public async Task<ActionResult<TaktCustomerComplaintDto>> GetCustomerComplaintByIdAsync(long id)
    {
        var item = await _service.GetCustomerComplaintByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取客诉主表(CustomerComplaint)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:query", "查询客诉主表(CustomerComplaint)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCustomerComplaintOptionsAsync()
    {
        var result = await _service.GetCustomerComplaintOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建客诉主表(CustomerComplaint)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:complaint:customercomplaint:create", "创建客诉主表(CustomerComplaint)")]
    public async Task<ActionResult<TaktCustomerComplaintDto>> CreateCustomerComplaintAsync([FromBody] TaktCustomerComplaintCreateDto dto)
    {
        var result = await _service.CreateCustomerComplaintAsync(dto);
        return CreatedAtAction(nameof(GetCustomerComplaintByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新客诉主表(CustomerComplaint)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:update", "更新客诉主表(CustomerComplaint)")]
    public async Task<ActionResult<TaktCustomerComplaintDto>> UpdateCustomerComplaintAsync(long id, [FromBody] TaktCustomerComplaintUpdateDto dto)
    {
        var result = await _service.UpdateCustomerComplaintAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除客诉主表(CustomerComplaint)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:delete", "删除客诉主表(CustomerComplaint)")]
    public async Task<ActionResult> DeleteCustomerComplaintByIdAsync(long id)
    {
        await _service.DeleteCustomerComplaintByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除客诉主表(CustomerComplaint)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:delete", "批量删除客诉主表(CustomerComplaint)")]
    public async Task<ActionResult> DeleteCustomerComplaintBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCustomerComplaintBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新客诉主表(CustomerComplaint)Complaint
    /// </summary>
    [HttpPut("status-complaint")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:update", "更新客诉主表(CustomerComplaint)Complaint")]
    public async Task<ActionResult<TaktCustomerComplaintDto>> UpdateCustomerComplaintComplaintStatusAsync([FromBody] TaktCustomerComplaintComplaintStatusDto dto)
    {
        var result = await _service.UpdateCustomerComplaintComplaintStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新客诉主表(CustomerComplaint)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:update", "更新客诉主表(CustomerComplaint)排序")]
    public async Task<ActionResult<TaktCustomerComplaintDto>> UpdateCustomerComplaintSortAsync([FromBody] TaktCustomerComplaintSortDto dto)
    {
        var result = await _service.UpdateCustomerComplaintSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取客诉主表(CustomerComplaint)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:import", "获取客诉主表(CustomerComplaint)导入模板")]
    public async Task<IActionResult> GetCustomerComplaintTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCustomerComplaintTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入客诉主表(CustomerComplaint)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:import", "导入客诉主表(CustomerComplaint)")]
    public async Task<ActionResult<object>> ImportCustomerComplaintAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCustomerComplaintAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出客诉主表(CustomerComplaint)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:complaint:customercomplaint:export", "导出客诉主表(CustomerComplaint)")]
    public async Task<IActionResult> ExportCustomerComplaintAsync([FromBody] TaktCustomerComplaintQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCustomerComplaintAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
