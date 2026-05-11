// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktBenefitPlansController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：福利方案表控制器，提供BenefitPlan管理的RESTful API接口
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
/// 福利方案表控制器
/// </summary>
[Route("api/[controller]", Name = "福利方案表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:benefitplan", "福利方案表管理")]
public class TaktBenefitPlansController : TaktControllerBase
{
    private readonly ITaktBenefitPlanService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlansController(
        ITaktBenefitPlanService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取福利方案表(BenefitPlan)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:list", "查询福利方案表(BenefitPlan)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktBenefitPlanDto>>> GetBenefitPlanListAsync([FromQuery] TaktBenefitPlanQueryDto queryDto)
    {
        var result = await _service.GetBenefitPlanListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取福利方案表(BenefitPlan)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:query", "查询福利方案表(BenefitPlan)详情")]
    public async Task<ActionResult<TaktBenefitPlanDto>> GetBenefitPlanByIdAsync(long id)
    {
        var item = await _service.GetBenefitPlanByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取福利方案表(BenefitPlan)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:query", "查询福利方案表(BenefitPlan)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetBenefitPlanOptionsAsync()
    {
        var result = await _service.GetBenefitPlanOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建福利方案表(BenefitPlan)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:create", "创建福利方案表(BenefitPlan)")]
    public async Task<ActionResult<TaktBenefitPlanDto>> CreateBenefitPlanAsync([FromBody] TaktBenefitPlanCreateDto dto)
    {
        var result = await _service.CreateBenefitPlanAsync(dto);
        return CreatedAtAction(nameof(GetBenefitPlanByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新福利方案表(BenefitPlan)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:update", "更新福利方案表(BenefitPlan)")]
    public async Task<ActionResult<TaktBenefitPlanDto>> UpdateBenefitPlanAsync(long id, [FromBody] TaktBenefitPlanUpdateDto dto)
    {
        var result = await _service.UpdateBenefitPlanAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除福利方案表(BenefitPlan)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:delete", "删除福利方案表(BenefitPlan)")]
    public async Task<ActionResult> DeleteBenefitPlanByIdAsync(long id)
    {
        await _service.DeleteBenefitPlanByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除福利方案表(BenefitPlan)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:delete", "批量删除福利方案表(BenefitPlan)")]
    public async Task<ActionResult> DeleteBenefitPlanBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteBenefitPlanBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新福利方案表(BenefitPlan)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:update", "更新福利方案表(BenefitPlan)状态")]
    public async Task<ActionResult<TaktBenefitPlanDto>> UpdateBenefitPlanStatusAsync([FromBody] TaktBenefitPlanStatusDto dto)
    {
        var result = await _service.UpdateBenefitPlanStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取福利方案表(BenefitPlan)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:import", "获取福利方案表(BenefitPlan)导入模板")]
    public async Task<IActionResult> GetBenefitPlanTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetBenefitPlanTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入福利方案表(BenefitPlan)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:import", "导入福利方案表(BenefitPlan)")]
    public async Task<ActionResult<object>> ImportBenefitPlanAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportBenefitPlanAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出福利方案表(BenefitPlan)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:benefitplan:export", "导出福利方案表(BenefitPlan)")]
    public async Task<IActionResult> ExportBenefitPlanAsync([FromBody] TaktBenefitPlanQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportBenefitPlanAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
