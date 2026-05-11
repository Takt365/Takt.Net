// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktEmployeeBenefitsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：员工福利表控制器，提供EmployeeBenefit管理的RESTful API接口
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
/// 员工福利表控制器
/// </summary>
[Route("api/[controller]", Name = "员工福利表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:employeebenefit", "员工福利表管理")]
public class TaktEmployeeBenefitsController : TaktControllerBase
{
    private readonly ITaktEmployeeBenefitService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitsController(
        ITaktEmployeeBenefitService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工福利表(EmployeeBenefit)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:list", "查询员工福利表(EmployeeBenefit)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeBenefitDto>>> GetEmployeeBenefitListAsync([FromQuery] TaktEmployeeBenefitQueryDto queryDto)
    {
        var result = await _service.GetEmployeeBenefitListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工福利表(EmployeeBenefit)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:query", "查询员工福利表(EmployeeBenefit)详情")]
    public async Task<ActionResult<TaktEmployeeBenefitDto>> GetEmployeeBenefitByIdAsync(long id)
    {
        var item = await _service.GetEmployeeBenefitByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工福利表(EmployeeBenefit)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:query", "查询员工福利表(EmployeeBenefit)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeBenefitOptionsAsync()
    {
        var result = await _service.GetEmployeeBenefitOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工福利表(EmployeeBenefit)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:create", "创建员工福利表(EmployeeBenefit)")]
    public async Task<ActionResult<TaktEmployeeBenefitDto>> CreateEmployeeBenefitAsync([FromBody] TaktEmployeeBenefitCreateDto dto)
    {
        var result = await _service.CreateEmployeeBenefitAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeBenefitByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工福利表(EmployeeBenefit)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:update", "更新员工福利表(EmployeeBenefit)")]
    public async Task<ActionResult<TaktEmployeeBenefitDto>> UpdateEmployeeBenefitAsync(long id, [FromBody] TaktEmployeeBenefitUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeBenefitAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工福利表(EmployeeBenefit)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:delete", "删除员工福利表(EmployeeBenefit)")]
    public async Task<ActionResult> DeleteEmployeeBenefitByIdAsync(long id)
    {
        await _service.DeleteEmployeeBenefitByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工福利表(EmployeeBenefit)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:delete", "批量删除员工福利表(EmployeeBenefit)")]
    public async Task<ActionResult> DeleteEmployeeBenefitBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeBenefitBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新员工福利表(EmployeeBenefit)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:update", "更新员工福利表(EmployeeBenefit)状态")]
    public async Task<ActionResult<TaktEmployeeBenefitDto>> UpdateEmployeeBenefitStatusAsync([FromBody] TaktEmployeeBenefitStatusDto dto)
    {
        var result = await _service.UpdateEmployeeBenefitStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取员工福利表(EmployeeBenefit)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:import", "获取员工福利表(EmployeeBenefit)导入模板")]
    public async Task<IActionResult> GetEmployeeBenefitTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeBenefitTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工福利表(EmployeeBenefit)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:import", "导入员工福利表(EmployeeBenefit)")]
    public async Task<ActionResult<object>> ImportEmployeeBenefitAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeBenefitAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工福利表(EmployeeBenefit)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:employeebenefit:export", "导出员工福利表(EmployeeBenefit)")]
    public async Task<IActionResult> ExportEmployeeBenefitAsync([FromBody] TaktEmployeeBenefitQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeBenefitAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
