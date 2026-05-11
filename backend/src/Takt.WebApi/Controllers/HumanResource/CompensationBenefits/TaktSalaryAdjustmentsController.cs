// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryAdjustmentsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资调整表控制器，提供SalaryAdjustment管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Application.Services.HumanResource.CompensationBenefits;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资调整表控制器
/// </summary>
[Route("api/[controller]", Name = "薪资调整表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:salaryadjustment", "薪资调整表管理")]
public class TaktSalaryAdjustmentsController : TaktControllerBase
{
    private readonly ITaktSalaryAdjustmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentsController(
        ITaktSalaryAdjustmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取薪资调整表(SalaryAdjustment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:list", "查询薪资调整表(SalaryAdjustment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalaryAdjustmentDto>>> GetSalaryAdjustmentListAsync([FromQuery] TaktSalaryAdjustmentQueryDto queryDto)
    {
        var result = await _service.GetSalaryAdjustmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取薪资调整表(SalaryAdjustment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:query", "查询薪资调整表(SalaryAdjustment)详情")]
    public async Task<ActionResult<TaktSalaryAdjustmentDto>> GetSalaryAdjustmentByIdAsync(long id)
    {
        var item = await _service.GetSalaryAdjustmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取薪资调整表(SalaryAdjustment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:query", "查询薪资调整表(SalaryAdjustment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalaryAdjustmentOptionsAsync()
    {
        var result = await _service.GetSalaryAdjustmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建薪资调整表(SalaryAdjustment)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:create", "创建薪资调整表(SalaryAdjustment)")]
    public async Task<ActionResult<TaktSalaryAdjustmentDto>> CreateSalaryAdjustmentAsync([FromBody] TaktSalaryAdjustmentCreateDto dto)
    {
        var result = await _service.CreateSalaryAdjustmentAsync(dto);
        return CreatedAtAction(nameof(GetSalaryAdjustmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新薪资调整表(SalaryAdjustment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:update", "更新薪资调整表(SalaryAdjustment)")]
    public async Task<ActionResult<TaktSalaryAdjustmentDto>> UpdateSalaryAdjustmentAsync(long id, [FromBody] TaktSalaryAdjustmentUpdateDto dto)
    {
        var result = await _service.UpdateSalaryAdjustmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除薪资调整表(SalaryAdjustment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:delete", "删除薪资调整表(SalaryAdjustment)")]
    public async Task<ActionResult> DeleteSalaryAdjustmentByIdAsync(long id)
    {
        await _service.DeleteSalaryAdjustmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除薪资调整表(SalaryAdjustment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:delete", "批量删除薪资调整表(SalaryAdjustment)")]
    public async Task<ActionResult> DeleteSalaryAdjustmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalaryAdjustmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新薪资调整表(SalaryAdjustment)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:update", "更新薪资调整表(SalaryAdjustment)状态")]
    public async Task<ActionResult<TaktSalaryAdjustmentDto>> UpdateSalaryAdjustmentStatusAsync([FromBody] TaktSalaryAdjustmentStatusDto dto)
    {
        var result = await _service.UpdateSalaryAdjustmentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取薪资调整表(SalaryAdjustment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:import", "获取薪资调整表(SalaryAdjustment)导入模板")]
    public async Task<IActionResult> GetSalaryAdjustmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalaryAdjustmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入薪资调整表(SalaryAdjustment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:import", "导入薪资调整表(SalaryAdjustment)")]
    public async Task<ActionResult<object>> ImportSalaryAdjustmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalaryAdjustmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出薪资调整表(SalaryAdjustment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:salaryadjustment:export", "导出薪资调整表(SalaryAdjustment)")]
    public async Task<IActionResult> ExportSalaryAdjustmentAsync([FromBody] TaktSalaryAdjustmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalaryAdjustmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
