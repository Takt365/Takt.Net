// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：供应商评价考核表控制器，提供SupplierEvaluation管理的RESTful API接口
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
/// 供应商评价考核表控制器
/// </summary>
[Route("api/[controller]", Name = "供应商评价考核表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:complaint:supplierevaluation", "供应商评价考核表管理")]
public class TaktSupplierEvaluationsController : TaktControllerBase
{
    private readonly ITaktSupplierEvaluationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationsController(
        ITaktSupplierEvaluationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取供应商评价考核表(SupplierEvaluation)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:list", "查询供应商评价考核表(SupplierEvaluation)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSupplierEvaluationDto>>> GetSupplierEvaluationListAsync([FromQuery] TaktSupplierEvaluationQueryDto queryDto)
    {
        var result = await _service.GetSupplierEvaluationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取供应商评价考核表(SupplierEvaluation)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:query", "查询供应商评价考核表(SupplierEvaluation)详情")]
    public async Task<ActionResult<TaktSupplierEvaluationDto>> GetSupplierEvaluationByIdAsync(long id)
    {
        var item = await _service.GetSupplierEvaluationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取供应商评价考核表(SupplierEvaluation)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:query", "查询供应商评价考核表(SupplierEvaluation)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSupplierEvaluationOptionsAsync()
    {
        var result = await _service.GetSupplierEvaluationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建供应商评价考核表(SupplierEvaluation)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:create", "创建供应商评价考核表(SupplierEvaluation)")]
    public async Task<ActionResult<TaktSupplierEvaluationDto>> CreateSupplierEvaluationAsync([FromBody] TaktSupplierEvaluationCreateDto dto)
    {
        var result = await _service.CreateSupplierEvaluationAsync(dto);
        return CreatedAtAction(nameof(GetSupplierEvaluationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:update", "更新供应商评价考核表(SupplierEvaluation)")]
    public async Task<ActionResult<TaktSupplierEvaluationDto>> UpdateSupplierEvaluationAsync(long id, [FromBody] TaktSupplierEvaluationUpdateDto dto)
    {
        var result = await _service.UpdateSupplierEvaluationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除供应商评价考核表(SupplierEvaluation)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:delete", "删除供应商评价考核表(SupplierEvaluation)")]
    public async Task<ActionResult> DeleteSupplierEvaluationByIdAsync(long id)
    {
        await _service.DeleteSupplierEvaluationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除供应商评价考核表(SupplierEvaluation)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:delete", "批量删除供应商评价考核表(SupplierEvaluation)")]
    public async Task<ActionResult> DeleteSupplierEvaluationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSupplierEvaluationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)Evaluation
    /// </summary>
    [HttpPut("status-evaluation")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:update", "更新供应商评价考核表(SupplierEvaluation)Evaluation")]
    public async Task<ActionResult<TaktSupplierEvaluationDto>> UpdateSupplierEvaluationEvaluationStatusAsync([FromBody] TaktSupplierEvaluationEvaluationStatusDto dto)
    {
        var result = await _service.UpdateSupplierEvaluationEvaluationStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)Rectification
    /// </summary>
    [HttpPut("status-rectification")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:update", "更新供应商评价考核表(SupplierEvaluation)Rectification")]
    public async Task<ActionResult<TaktSupplierEvaluationDto>> UpdateSupplierEvaluationRectificationStatusAsync([FromBody] TaktSupplierEvaluationRectificationStatusDto dto)
    {
        var result = await _service.UpdateSupplierEvaluationRectificationStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:update", "更新供应商评价考核表(SupplierEvaluation)排序")]
    public async Task<ActionResult<TaktSupplierEvaluationDto>> UpdateSupplierEvaluationSortAsync([FromBody] TaktSupplierEvaluationSortDto dto)
    {
        var result = await _service.UpdateSupplierEvaluationSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取供应商评价考核表(SupplierEvaluation)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:import", "获取供应商评价考核表(SupplierEvaluation)导入模板")]
    public async Task<IActionResult> GetSupplierEvaluationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSupplierEvaluationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入供应商评价考核表(SupplierEvaluation)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:import", "导入供应商评价考核表(SupplierEvaluation)")]
    public async Task<ActionResult<object>> ImportSupplierEvaluationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSupplierEvaluationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出供应商评价考核表(SupplierEvaluation)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:complaint:supplierevaluation:export", "导出供应商评价考核表(SupplierEvaluation)")]
    public async Task<IActionResult> ExportSupplierEvaluationAsync([FromBody] TaktSupplierEvaluationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSupplierEvaluationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
