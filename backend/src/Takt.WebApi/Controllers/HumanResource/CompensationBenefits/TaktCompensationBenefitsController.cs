// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationBenefitsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：薪酬福利表控制器，提供CompensationBenefit管理的RESTful API接口
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
/// 薪酬福利表控制器
/// </summary>
[Route("api/[controller]", Name = "薪酬福利表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:compensationbenefit", "薪酬福利表管理")]
public class TaktCompensationBenefitsController : TaktControllerBase
{
    private readonly ITaktCompensationBenefitService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitsController(
        ITaktCompensationBenefitService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取薪酬福利表(CompensationBenefit)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:list", "查询薪酬福利表(CompensationBenefit)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCompensationBenefitDto>>> GetCompensationBenefitListAsync([FromQuery] TaktCompensationBenefitQueryDto queryDto)
    {
        var result = await _service.GetCompensationBenefitListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取薪酬福利表(CompensationBenefit)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:query", "查询薪酬福利表(CompensationBenefit)详情")]
    public async Task<ActionResult<TaktCompensationBenefitDto>> GetCompensationBenefitByIdAsync(long id)
    {
        var item = await _service.GetCompensationBenefitByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取薪酬福利表(CompensationBenefit)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:query", "查询薪酬福利表(CompensationBenefit)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCompensationBenefitOptionsAsync()
    {
        var result = await _service.GetCompensationBenefitOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建薪酬福利表(CompensationBenefit)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:create", "创建薪酬福利表(CompensationBenefit)")]
    public async Task<ActionResult<TaktCompensationBenefitDto>> CreateCompensationBenefitAsync([FromBody] TaktCompensationBenefitCreateDto dto)
    {
        var result = await _service.CreateCompensationBenefitAsync(dto);
        return CreatedAtAction(nameof(GetCompensationBenefitByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新薪酬福利表(CompensationBenefit)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:update", "更新薪酬福利表(CompensationBenefit)")]
    public async Task<ActionResult<TaktCompensationBenefitDto>> UpdateCompensationBenefitAsync(long id, [FromBody] TaktCompensationBenefitUpdateDto dto)
    {
        var result = await _service.UpdateCompensationBenefitAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除薪酬福利表(CompensationBenefit)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:delete", "删除薪酬福利表(CompensationBenefit)")]
    public async Task<ActionResult> DeleteCompensationBenefitByIdAsync(long id)
    {
        await _service.DeleteCompensationBenefitByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除薪酬福利表(CompensationBenefit)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:delete", "批量删除薪酬福利表(CompensationBenefit)")]
    public async Task<ActionResult> DeleteCompensationBenefitBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCompensationBenefitBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新薪酬福利表(CompensationBenefit)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:update", "更新薪酬福利表(CompensationBenefit)状态")]
    public async Task<ActionResult<TaktCompensationBenefitDto>> UpdateCompensationBenefitStatusAsync([FromBody] TaktCompensationBenefitStatusDto dto)
    {
        var result = await _service.UpdateCompensationBenefitStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取薪酬福利表(CompensationBenefit)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:import", "获取薪酬福利表(CompensationBenefit)导入模板")]
    public async Task<IActionResult> GetCompensationBenefitTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCompensationBenefitTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入薪酬福利表(CompensationBenefit)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:import", "导入薪酬福利表(CompensationBenefit)")]
    public async Task<ActionResult<object>> ImportCompensationBenefitAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCompensationBenefitAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出薪酬福利表(CompensationBenefit)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:compensationbenefit:export", "导出薪酬福利表(CompensationBenefit)")]
    public async Task<IActionResult> ExportCompensationBenefitAsync([FromBody] TaktCompensationBenefitQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCompensationBenefitAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
