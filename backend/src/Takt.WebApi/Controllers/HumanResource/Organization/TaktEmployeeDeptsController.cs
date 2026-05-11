// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktEmployeeDeptsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：员工部门关联表控制器，提供EmployeeDept管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Organization;

/// <summary>
/// 员工部门关联表控制器
/// </summary>
[Route("api/[controller]", Name = "员工部门关联表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:employeedept", "员工部门关联表管理")]
public class TaktEmployeeDeptsController : TaktControllerBase
{
    private readonly ITaktEmployeeDeptService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeDeptsController(
        ITaktEmployeeDeptService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工部门关联表(EmployeeDept)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:employeedept:list", "查询员工部门关联表(EmployeeDept)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeDeptDto>>> GetEmployeeDeptListAsync([FromQuery] TaktEmployeeDeptQueryDto queryDto)
    {
        var result = await _service.GetEmployeeDeptListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工部门关联表(EmployeeDept)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:employeedept:query", "查询员工部门关联表(EmployeeDept)详情")]
    public async Task<ActionResult<TaktEmployeeDeptDto>> GetEmployeeDeptByIdAsync(long id)
    {
        var item = await _service.GetEmployeeDeptByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工部门关联表(EmployeeDept)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:employeedept:query", "查询员工部门关联表(EmployeeDept)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeDeptOptionsAsync()
    {
        var result = await _service.GetEmployeeDeptOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工部门关联表(EmployeeDept)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:employeedept:create", "创建员工部门关联表(EmployeeDept)")]
    public async Task<ActionResult<TaktEmployeeDeptDto>> CreateEmployeeDeptAsync([FromBody] TaktEmployeeDeptCreateDto dto)
    {
        var result = await _service.CreateEmployeeDeptAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeDeptByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工部门关联表(EmployeeDept)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:employeedept:update", "更新员工部门关联表(EmployeeDept)")]
    public async Task<ActionResult<TaktEmployeeDeptDto>> UpdateEmployeeDeptAsync(long id, [FromBody] TaktEmployeeDeptUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeDeptAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工部门关联表(EmployeeDept)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:employeedept:delete", "删除员工部门关联表(EmployeeDept)")]
    public async Task<ActionResult> DeleteEmployeeDeptByIdAsync(long id)
    {
        await _service.DeleteEmployeeDeptByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工部门关联表(EmployeeDept)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:employeedept:delete", "批量删除员工部门关联表(EmployeeDept)")]
    public async Task<ActionResult> DeleteEmployeeDeptBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeDeptBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取员工部门关联表(EmployeeDept)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:employeedept:import", "获取员工部门关联表(EmployeeDept)导入模板")]
    public async Task<IActionResult> GetEmployeeDeptTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeDeptTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工部门关联表(EmployeeDept)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:employeedept:import", "导入员工部门关联表(EmployeeDept)")]
    public async Task<ActionResult<object>> ImportEmployeeDeptAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeDeptAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工部门关联表(EmployeeDept)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:employeedept:export", "导出员工部门关联表(EmployeeDept)")]
    public async Task<IActionResult> ExportEmployeeDeptAsync([FromBody] TaktEmployeeDeptQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeDeptAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
