// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveysController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客户满意度调查表控制器，提供CustomerSatisfactionSurvey管理的RESTful API接口
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
/// 客户满意度调查表控制器
/// </summary>
[Route("api/[controller]", Name = "客户满意度调查表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:complaint:customersatisfactionsurvey", "客户满意度调查表管理")]
public class TaktCustomerSatisfactionSurveysController : TaktControllerBase
{
    private readonly ITaktCustomerSatisfactionSurveyService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveysController(
        ITaktCustomerSatisfactionSurveyService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取客户满意度调查表(CustomerSatisfactionSurvey)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:list", "查询客户满意度调查表(CustomerSatisfactionSurvey)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCustomerSatisfactionSurveyDto>>> GetCustomerSatisfactionSurveyListAsync([FromQuery] TaktCustomerSatisfactionSurveyQueryDto queryDto)
    {
        var result = await _service.GetCustomerSatisfactionSurveyListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:query", "查询客户满意度调查表(CustomerSatisfactionSurvey)详情")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyDto>> GetCustomerSatisfactionSurveyByIdAsync(long id)
    {
        var item = await _service.GetCustomerSatisfactionSurveyByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取客户满意度调查表(CustomerSatisfactionSurvey)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:query", "查询客户满意度调查表(CustomerSatisfactionSurvey)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCustomerSatisfactionSurveyOptionsAsync()
    {
        var result = await _service.GetCustomerSatisfactionSurveyOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:create", "创建客户满意度调查表(CustomerSatisfactionSurvey)")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyDto>> CreateCustomerSatisfactionSurveyAsync([FromBody] TaktCustomerSatisfactionSurveyCreateDto dto)
    {
        var result = await _service.CreateCustomerSatisfactionSurveyAsync(dto);
        return CreatedAtAction(nameof(GetCustomerSatisfactionSurveyByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:update", "更新客户满意度调查表(CustomerSatisfactionSurvey)")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyDto>> UpdateCustomerSatisfactionSurveyAsync(long id, [FromBody] TaktCustomerSatisfactionSurveyUpdateDto dto)
    {
        var result = await _service.UpdateCustomerSatisfactionSurveyAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:delete", "删除客户满意度调查表(CustomerSatisfactionSurvey)")]
    public async Task<ActionResult> DeleteCustomerSatisfactionSurveyByIdAsync(long id)
    {
        await _service.DeleteCustomerSatisfactionSurveyByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:delete", "批量删除客户满意度调查表(CustomerSatisfactionSurvey)")]
    public async Task<ActionResult> DeleteCustomerSatisfactionSurveyBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCustomerSatisfactionSurveyBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)Survey
    /// </summary>
    [HttpPut("status-survey")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:update", "更新客户满意度调查表(CustomerSatisfactionSurvey)Survey")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyDto>> UpdateCustomerSatisfactionSurveySurveyStatusAsync([FromBody] TaktCustomerSatisfactionSurveySurveyStatusDto dto)
    {
        var result = await _service.UpdateCustomerSatisfactionSurveySurveyStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)FollowUp
    /// </summary>
    [HttpPut("status-followup")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:update", "更新客户满意度调查表(CustomerSatisfactionSurvey)FollowUp")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyDto>> UpdateCustomerSatisfactionSurveyFollowUpStatusAsync([FromBody] TaktCustomerSatisfactionSurveyFollowUpStatusDto dto)
    {
        var result = await _service.UpdateCustomerSatisfactionSurveyFollowUpStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:update", "更新客户满意度调查表(CustomerSatisfactionSurvey)排序")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyDto>> UpdateCustomerSatisfactionSurveySortAsync([FromBody] TaktCustomerSatisfactionSurveySortDto dto)
    {
        var result = await _service.UpdateCustomerSatisfactionSurveySortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取客户满意度调查表(CustomerSatisfactionSurvey)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:import", "获取客户满意度调查表(CustomerSatisfactionSurvey)导入模板")]
    public async Task<IActionResult> GetCustomerSatisfactionSurveyTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCustomerSatisfactionSurveyTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:import", "导入客户满意度调查表(CustomerSatisfactionSurvey)")]
    public async Task<ActionResult<object>> ImportCustomerSatisfactionSurveyAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCustomerSatisfactionSurveyAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurvey:export", "导出客户满意度调查表(CustomerSatisfactionSurvey)")]
    public async Task<IActionResult> ExportCustomerSatisfactionSurveyAsync([FromBody] TaktCustomerSatisfactionSurveyQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCustomerSatisfactionSurveyAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
