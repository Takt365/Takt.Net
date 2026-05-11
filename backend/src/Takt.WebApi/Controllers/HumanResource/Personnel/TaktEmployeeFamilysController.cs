// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeFamilysController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：员工家庭成员表控制器，提供EmployeeFamily管理的RESTful API接口
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
/// 员工家庭成员表控制器
/// </summary>
[Route("api/[controller]", Name = "员工家庭成员表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeefamily", "员工家庭成员表管理")]
public class TaktEmployeeFamilysController : TaktControllerBase
{
    private readonly ITaktEmployeeFamilyService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilysController(
        ITaktEmployeeFamilyService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工家庭成员表(EmployeeFamily)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeefamily:list", "查询员工家庭成员表(EmployeeFamily)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeFamilyDto>>> GetEmployeeFamilyListAsync([FromQuery] TaktEmployeeFamilyQueryDto queryDto)
    {
        var result = await _service.GetEmployeeFamilyListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工家庭成员表(EmployeeFamily)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeefamily:query", "查询员工家庭成员表(EmployeeFamily)详情")]
    public async Task<ActionResult<TaktEmployeeFamilyDto>> GetEmployeeFamilyByIdAsync(long id)
    {
        var item = await _service.GetEmployeeFamilyByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工家庭成员表(EmployeeFamily)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeefamily:query", "查询员工家庭成员表(EmployeeFamily)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeFamilyOptionsAsync()
    {
        var result = await _service.GetEmployeeFamilyOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工家庭成员表(EmployeeFamily)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeefamily:create", "创建员工家庭成员表(EmployeeFamily)")]
    public async Task<ActionResult<TaktEmployeeFamilyDto>> CreateEmployeeFamilyAsync([FromBody] TaktEmployeeFamilyCreateDto dto)
    {
        var result = await _service.CreateEmployeeFamilyAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeFamilyByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工家庭成员表(EmployeeFamily)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeefamily:update", "更新员工家庭成员表(EmployeeFamily)")]
    public async Task<ActionResult<TaktEmployeeFamilyDto>> UpdateEmployeeFamilyAsync(long id, [FromBody] TaktEmployeeFamilyUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeFamilyAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工家庭成员表(EmployeeFamily)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeefamily:delete", "删除员工家庭成员表(EmployeeFamily)")]
    public async Task<ActionResult> DeleteEmployeeFamilyByIdAsync(long id)
    {
        await _service.DeleteEmployeeFamilyByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工家庭成员表(EmployeeFamily)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeefamily:delete", "批量删除员工家庭成员表(EmployeeFamily)")]
    public async Task<ActionResult> DeleteEmployeeFamilyBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeFamilyBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取员工家庭成员表(EmployeeFamily)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeefamily:import", "获取员工家庭成员表(EmployeeFamily)导入模板")]
    public async Task<IActionResult> GetEmployeeFamilyTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeFamilyTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工家庭成员表(EmployeeFamily)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeefamily:import", "导入员工家庭成员表(EmployeeFamily)")]
    public async Task<ActionResult<object>> ImportEmployeeFamilyAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeFamilyAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工家庭成员表(EmployeeFamily)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeefamily:export", "导出员工家庭成员表(EmployeeFamily)")]
    public async Task<IActionResult> ExportEmployeeFamilyAsync([FromBody] TaktEmployeeFamilyQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeFamilyAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
