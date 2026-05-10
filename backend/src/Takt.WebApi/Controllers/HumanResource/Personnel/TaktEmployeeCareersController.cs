// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeCareersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工职业表控制器，提供EmployeeCareer管理的RESTful API接口
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
/// 员工职业表控制器
/// </summary>
[Route("api/[controller]", Name = "员工职业表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeecareer", "员工职业表管理")]
public class TaktEmployeeCareersController : TaktControllerBase
{
    private readonly ITaktEmployeeCareerService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareersController(
        ITaktEmployeeCareerService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工职业表(EmployeeCareer)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeecareer:list", "查询员工职业表(EmployeeCareer)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeCareerDto>>> GetEmployeeCareerListAsync([FromQuery] TaktEmployeeCareerQueryDto queryDto)
    {
        var result = await _service.GetEmployeeCareerListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工职业表(EmployeeCareer)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeecareer:query", "查询员工职业表(EmployeeCareer)详情")]
    public async Task<ActionResult<TaktEmployeeCareerDto>> GetEmployeeCareerByIdAsync(long id)
    {
        var item = await _service.GetEmployeeCareerByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工职业表(EmployeeCareer)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeecareer:query", "查询员工职业表(EmployeeCareer)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeCareerOptionsAsync()
    {
        var result = await _service.GetEmployeeCareerOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工职业表(EmployeeCareer)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeecareer:create", "创建员工职业表(EmployeeCareer)")]
    public async Task<ActionResult<TaktEmployeeCareerDto>> CreateEmployeeCareerAsync([FromBody] TaktEmployeeCareerCreateDto dto)
    {
        var result = await _service.CreateEmployeeCareerAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeCareerByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工职业表(EmployeeCareer)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeecareer:update", "更新员工职业表(EmployeeCareer)")]
    public async Task<ActionResult<TaktEmployeeCareerDto>> UpdateEmployeeCareerAsync(long id, [FromBody] TaktEmployeeCareerUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeCareerAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工职业表(EmployeeCareer)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeecareer:delete", "删除员工职业表(EmployeeCareer)")]
    public async Task<ActionResult> DeleteEmployeeCareerByIdAsync(long id)
    {
        await _service.DeleteEmployeeCareerByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工职业表(EmployeeCareer)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeecareer:delete", "批量删除员工职业表(EmployeeCareer)")]
    public async Task<ActionResult> DeleteEmployeeCareerBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeCareerBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取员工职业表(EmployeeCareer)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeecareer:import", "获取员工职业表(EmployeeCareer)导入模板")]
    public async Task<IActionResult> GetEmployeeCareerTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeCareerTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工职业表(EmployeeCareer)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeecareer:import", "导入员工职业表(EmployeeCareer)")]
    public async Task<ActionResult<object>> ImportEmployeeCareerAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeCareerAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工职业表(EmployeeCareer)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeecareer:export", "导出员工职业表(EmployeeCareer)")]
    public async Task<IActionResult> ExportEmployeeCareerAsync([FromBody] TaktEmployeeCareerQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeCareerAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
