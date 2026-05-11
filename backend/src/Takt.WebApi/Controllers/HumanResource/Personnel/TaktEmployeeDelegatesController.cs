// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeDelegatesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：员工代理表控制器，提供EmployeeDelegate管理的RESTful API接口
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
/// 员工代理表控制器
/// </summary>
[Route("api/[controller]", Name = "员工代理表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeedelegate", "员工代理表管理")]
public class TaktEmployeeDelegatesController : TaktControllerBase
{
    private readonly ITaktEmployeeDelegateService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeDelegatesController(
        ITaktEmployeeDelegateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工代理表(EmployeeDelegate)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeedelegate:list", "查询员工代理表(EmployeeDelegate)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeDelegateDto>>> GetEmployeeDelegateListAsync([FromQuery] TaktEmployeeDelegateQueryDto queryDto)
    {
        var result = await _service.GetEmployeeDelegateListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工代理表(EmployeeDelegate)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeedelegate:query", "查询员工代理表(EmployeeDelegate)详情")]
    public async Task<ActionResult<TaktEmployeeDelegateDto>> GetEmployeeDelegateByIdAsync(long id)
    {
        var item = await _service.GetEmployeeDelegateByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工代理表(EmployeeDelegate)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeedelegate:query", "查询员工代理表(EmployeeDelegate)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeDelegateOptionsAsync()
    {
        var result = await _service.GetEmployeeDelegateOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工代理表(EmployeeDelegate)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeedelegate:create", "创建员工代理表(EmployeeDelegate)")]
    public async Task<ActionResult<TaktEmployeeDelegateDto>> CreateEmployeeDelegateAsync([FromBody] TaktEmployeeDelegateCreateDto dto)
    {
        var result = await _service.CreateEmployeeDelegateAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeDelegateByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工代理表(EmployeeDelegate)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeedelegate:update", "更新员工代理表(EmployeeDelegate)")]
    public async Task<ActionResult<TaktEmployeeDelegateDto>> UpdateEmployeeDelegateAsync(long id, [FromBody] TaktEmployeeDelegateUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeDelegateAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工代理表(EmployeeDelegate)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeedelegate:delete", "删除员工代理表(EmployeeDelegate)")]
    public async Task<ActionResult> DeleteEmployeeDelegateByIdAsync(long id)
    {
        await _service.DeleteEmployeeDelegateByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工代理表(EmployeeDelegate)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeedelegate:delete", "批量删除员工代理表(EmployeeDelegate)")]
    public async Task<ActionResult> DeleteEmployeeDelegateBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeDelegateBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新员工代理表(EmployeeDelegate)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:personnel:employeedelegate:update", "更新员工代理表(EmployeeDelegate)排序")]
    public async Task<ActionResult<TaktEmployeeDelegateDto>> UpdateEmployeeDelegateSortAsync([FromBody] TaktEmployeeDelegateSortDto dto)
    {
        var result = await _service.UpdateEmployeeDelegateSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取员工代理表(EmployeeDelegate)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeedelegate:import", "获取员工代理表(EmployeeDelegate)导入模板")]
    public async Task<IActionResult> GetEmployeeDelegateTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeDelegateTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工代理表(EmployeeDelegate)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeedelegate:import", "导入员工代理表(EmployeeDelegate)")]
    public async Task<ActionResult<object>> ImportEmployeeDelegateAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeDelegateAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工代理表(EmployeeDelegate)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeedelegate:export", "导出员工代理表(EmployeeDelegate)")]
    public async Task<IActionResult> ExportEmployeeDelegateAsync([FromBody] TaktEmployeeDelegateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeDelegateAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
