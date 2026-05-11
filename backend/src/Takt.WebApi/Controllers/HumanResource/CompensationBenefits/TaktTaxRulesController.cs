// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktTaxRulesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：税务规则表控制器，提供TaxRule管理的RESTful API接口
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
/// 税务规则表控制器
/// </summary>
[Route("api/[controller]", Name = "税务规则表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:taxrule", "税务规则表管理")]
public class TaktTaxRulesController : TaktControllerBase
{
    private readonly ITaktTaxRuleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRulesController(
        ITaktTaxRuleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取税务规则表(TaxRule)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:list", "查询税务规则表(TaxRule)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTaxRuleDto>>> GetTaxRuleListAsync([FromQuery] TaktTaxRuleQueryDto queryDto)
    {
        var result = await _service.GetTaxRuleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取税务规则表(TaxRule)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:query", "查询税务规则表(TaxRule)详情")]
    public async Task<ActionResult<TaktTaxRuleDto>> GetTaxRuleByIdAsync(long id)
    {
        var item = await _service.GetTaxRuleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取税务规则表(TaxRule)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:query", "查询税务规则表(TaxRule)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTaxRuleOptionsAsync()
    {
        var result = await _service.GetTaxRuleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建税务规则表(TaxRule)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:taxrule:create", "创建税务规则表(TaxRule)")]
    public async Task<ActionResult<TaktTaxRuleDto>> CreateTaxRuleAsync([FromBody] TaktTaxRuleCreateDto dto)
    {
        var result = await _service.CreateTaxRuleAsync(dto);
        return CreatedAtAction(nameof(GetTaxRuleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新税务规则表(TaxRule)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:update", "更新税务规则表(TaxRule)")]
    public async Task<ActionResult<TaktTaxRuleDto>> UpdateTaxRuleAsync(long id, [FromBody] TaktTaxRuleUpdateDto dto)
    {
        var result = await _service.UpdateTaxRuleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除税务规则表(TaxRule)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:delete", "删除税务规则表(TaxRule)")]
    public async Task<ActionResult> DeleteTaxRuleByIdAsync(long id)
    {
        await _service.DeleteTaxRuleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除税务规则表(TaxRule)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:delete", "批量删除税务规则表(TaxRule)")]
    public async Task<ActionResult> DeleteTaxRuleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTaxRuleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新税务规则表(TaxRule)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:update", "更新税务规则表(TaxRule)状态")]
    public async Task<ActionResult<TaktTaxRuleDto>> UpdateTaxRuleStatusAsync([FromBody] TaktTaxRuleStatusDto dto)
    {
        var result = await _service.UpdateTaxRuleStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取税务规则表(TaxRule)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:import", "获取税务规则表(TaxRule)导入模板")]
    public async Task<IActionResult> GetTaxRuleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTaxRuleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入税务规则表(TaxRule)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:import", "导入税务规则表(TaxRule)")]
    public async Task<ActionResult<object>> ImportTaxRuleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTaxRuleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出税务规则表(TaxRule)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:taxrule:export", "导出税务规则表(TaxRule)")]
    public async Task<IActionResult> ExportTaxRuleAsync([FromBody] TaktTaxRuleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTaxRuleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
