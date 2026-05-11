// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryComponentsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资组成表控制器，提供SalaryComponent管理的RESTful API接口
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
/// 薪资组成表控制器
/// </summary>
[Route("api/[controller]", Name = "薪资组成表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:salarycomponent", "薪资组成表管理")]
public class TaktSalaryComponentsController : TaktControllerBase
{
    private readonly ITaktSalaryComponentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentsController(
        ITaktSalaryComponentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取薪资组成表(SalaryComponent)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:list", "查询薪资组成表(SalaryComponent)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalaryComponentDto>>> GetSalaryComponentListAsync([FromQuery] TaktSalaryComponentQueryDto queryDto)
    {
        var result = await _service.GetSalaryComponentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取薪资组成表(SalaryComponent)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:query", "查询薪资组成表(SalaryComponent)详情")]
    public async Task<ActionResult<TaktSalaryComponentDto>> GetSalaryComponentByIdAsync(long id)
    {
        var item = await _service.GetSalaryComponentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取薪资组成表(SalaryComponent)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:query", "查询薪资组成表(SalaryComponent)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalaryComponentOptionsAsync()
    {
        var result = await _service.GetSalaryComponentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建薪资组成表(SalaryComponent)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:create", "创建薪资组成表(SalaryComponent)")]
    public async Task<ActionResult<TaktSalaryComponentDto>> CreateSalaryComponentAsync([FromBody] TaktSalaryComponentCreateDto dto)
    {
        var result = await _service.CreateSalaryComponentAsync(dto);
        return CreatedAtAction(nameof(GetSalaryComponentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新薪资组成表(SalaryComponent)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:update", "更新薪资组成表(SalaryComponent)")]
    public async Task<ActionResult<TaktSalaryComponentDto>> UpdateSalaryComponentAsync(long id, [FromBody] TaktSalaryComponentUpdateDto dto)
    {
        var result = await _service.UpdateSalaryComponentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除薪资组成表(SalaryComponent)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:delete", "删除薪资组成表(SalaryComponent)")]
    public async Task<ActionResult> DeleteSalaryComponentByIdAsync(long id)
    {
        await _service.DeleteSalaryComponentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除薪资组成表(SalaryComponent)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:delete", "批量删除薪资组成表(SalaryComponent)")]
    public async Task<ActionResult> DeleteSalaryComponentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalaryComponentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新薪资组成表(SalaryComponent)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:update", "更新薪资组成表(SalaryComponent)状态")]
    public async Task<ActionResult<TaktSalaryComponentDto>> UpdateSalaryComponentStatusAsync([FromBody] TaktSalaryComponentStatusDto dto)
    {
        var result = await _service.UpdateSalaryComponentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新薪资组成表(SalaryComponent)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:update", "更新薪资组成表(SalaryComponent)排序")]
    public async Task<ActionResult<TaktSalaryComponentDto>> UpdateSalaryComponentSortAsync([FromBody] TaktSalaryComponentSortDto dto)
    {
        var result = await _service.UpdateSalaryComponentSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取薪资组成表(SalaryComponent)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:import", "获取薪资组成表(SalaryComponent)导入模板")]
    public async Task<IActionResult> GetSalaryComponentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalaryComponentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入薪资组成表(SalaryComponent)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:import", "导入薪资组成表(SalaryComponent)")]
    public async Task<ActionResult<object>> ImportSalaryComponentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalaryComponentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出薪资组成表(SalaryComponent)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:salarycomponent:export", "导出薪资组成表(SalaryComponent)")]
    public async Task<IActionResult> ExportSalaryComponentAsync([FromBody] TaktSalaryComponentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalaryComponentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
