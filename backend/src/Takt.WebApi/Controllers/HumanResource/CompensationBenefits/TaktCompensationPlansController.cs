// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationPlansController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：薪酬方案表控制器，提供CompensationPlan管理的RESTful API接口
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
/// 薪酬方案表控制器
/// </summary>
[Route("api/[controller]", Name = "薪酬方案表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:compensationplan", "薪酬方案表管理")]
public class TaktCompensationPlansController : TaktControllerBase
{
    private readonly ITaktCompensationPlanService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationPlansController(
        ITaktCompensationPlanService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取薪酬方案表(CompensationPlan)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:list", "查询薪酬方案表(CompensationPlan)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCompensationPlanDto>>> GetCompensationPlanListAsync([FromQuery] TaktCompensationPlanQueryDto queryDto)
    {
        var result = await _service.GetCompensationPlanListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取薪酬方案表(CompensationPlan)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:query", "查询薪酬方案表(CompensationPlan)详情")]
    public async Task<ActionResult<TaktCompensationPlanDto>> GetCompensationPlanByIdAsync(long id)
    {
        var item = await _service.GetCompensationPlanByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取薪酬方案表(CompensationPlan)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:query", "查询薪酬方案表(CompensationPlan)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCompensationPlanOptionsAsync()
    {
        var result = await _service.GetCompensationPlanOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建薪酬方案表(CompensationPlan)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:create", "创建薪酬方案表(CompensationPlan)")]
    public async Task<ActionResult<TaktCompensationPlanDto>> CreateCompensationPlanAsync([FromBody] TaktCompensationPlanCreateDto dto)
    {
        var result = await _service.CreateCompensationPlanAsync(dto);
        return CreatedAtAction(nameof(GetCompensationPlanByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新薪酬方案表(CompensationPlan)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:update", "更新薪酬方案表(CompensationPlan)")]
    public async Task<ActionResult<TaktCompensationPlanDto>> UpdateCompensationPlanAsync(long id, [FromBody] TaktCompensationPlanUpdateDto dto)
    {
        var result = await _service.UpdateCompensationPlanAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除薪酬方案表(CompensationPlan)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:delete", "删除薪酬方案表(CompensationPlan)")]
    public async Task<ActionResult> DeleteCompensationPlanByIdAsync(long id)
    {
        await _service.DeleteCompensationPlanByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除薪酬方案表(CompensationPlan)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:delete", "批量删除薪酬方案表(CompensationPlan)")]
    public async Task<ActionResult> DeleteCompensationPlanBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCompensationPlanBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新薪酬方案表(CompensationPlan)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:update", "更新薪酬方案表(CompensationPlan)状态")]
    public async Task<ActionResult<TaktCompensationPlanDto>> UpdateCompensationPlanStatusAsync([FromBody] TaktCompensationPlanStatusDto dto)
    {
        var result = await _service.UpdateCompensationPlanStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取薪酬方案表(CompensationPlan)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:import", "获取薪酬方案表(CompensationPlan)导入模板")]
    public async Task<IActionResult> GetCompensationPlanTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCompensationPlanTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入薪酬方案表(CompensationPlan)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:import", "导入薪酬方案表(CompensationPlan)")]
    public async Task<ActionResult<object>> ImportCompensationPlanAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCompensationPlanAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出薪酬方案表(CompensationPlan)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:compensationplan:export", "导出薪酬方案表(CompensationPlan)")]
    public async Task<IActionResult> ExportCompensationPlanAsync([FromBody] TaktCompensationPlanQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCompensationPlanAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
