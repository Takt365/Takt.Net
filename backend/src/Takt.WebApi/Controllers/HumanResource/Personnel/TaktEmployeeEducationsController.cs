// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：员工教育履历表控制器，提供EmployeeEducation管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工教育履历表控制器
/// </summary>
[Route("api/[controller]", Name = "员工教育履历表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeeeducation", "员工教育履历表管理")]
public class TaktEmployeeEducationsController : TaktControllerBase
{
    private readonly ITaktEmployeeEducationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationsController(
        ITaktEmployeeEducationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工教育履历表(EmployeeEducation)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeeeducation:list", "查询员工教育履历表(EmployeeEducation)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeEducationDto>>> GetEmployeeEducationListAsync([FromQuery] TaktEmployeeEducationQueryDto queryDto)
    {
        var result = await _service.GetEmployeeEducationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工教育履历表(EmployeeEducation)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeeeducation:query", "查询员工教育履历表(EmployeeEducation)详情")]
    public async Task<ActionResult<TaktEmployeeEducationDto>> GetEmployeeEducationByIdAsync(long id)
    {
        var item = await _service.GetEmployeeEducationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工教育履历表(EmployeeEducation)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeeeducation:query", "查询员工教育履历表(EmployeeEducation)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeEducationOptionsAsync()
    {
        var result = await _service.GetEmployeeEducationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工教育履历表(EmployeeEducation)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeeeducation:create", "创建员工教育履历表(EmployeeEducation)")]
    public async Task<ActionResult<TaktEmployeeEducationDto>> CreateEmployeeEducationAsync([FromBody] TaktEmployeeEducationCreateDto dto)
    {
        var result = await _service.CreateEmployeeEducationAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeEducationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工教育履历表(EmployeeEducation)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeeeducation:update", "更新员工教育履历表(EmployeeEducation)")]
    public async Task<ActionResult<TaktEmployeeEducationDto>> UpdateEmployeeEducationAsync(long id, [FromBody] TaktEmployeeEducationUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeEducationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工教育履历表(EmployeeEducation)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeeeducation:delete", "删除员工教育履历表(EmployeeEducation)")]
    public async Task<ActionResult> DeleteEmployeeEducationByIdAsync(long id)
    {
        await _service.DeleteEmployeeEducationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工教育履历表(EmployeeEducation)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeeeducation:delete", "批量删除员工教育履历表(EmployeeEducation)")]
    public async Task<ActionResult> DeleteEmployeeEducationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeEducationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取员工教育履历表(EmployeeEducation)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeeeducation:import", "获取员工教育履历表(EmployeeEducation)导入模板")]
    public async Task<IActionResult> GetEmployeeEducationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeEducationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工教育履历表(EmployeeEducation)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeeeducation:import", "导入员工教育履历表(EmployeeEducation)")]
    public async Task<ActionResult<object>> ImportEmployeeEducationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeEducationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工教育履历表(EmployeeEducation)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeeeducation:export", "导出员工教育履历表(EmployeeEducation)")]
    public async Task<IActionResult> ExportEmployeeEducationAsync([FromBody] TaktEmployeeEducationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeEducationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
