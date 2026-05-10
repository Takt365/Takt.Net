// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工信息表控制器，提供Employee管理的RESTful API接口
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
/// 员工信息表控制器
/// </summary>
[Route("api/[controller]", Name = "员工信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employee", "员工信息表管理")]
public class TaktEmployeesController : TaktControllerBase
{
    private readonly ITaktEmployeeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeesController(
        ITaktEmployeeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工信息表(Employee)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employee:list", "查询员工信息表(Employee)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeDto>>> GetEmployeeListAsync([FromQuery] TaktEmployeeQueryDto queryDto)
    {
        var result = await _service.GetEmployeeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工信息表(Employee)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employee:query", "查询员工信息表(Employee)详情")]
    public async Task<ActionResult<TaktEmployeeDto>> GetEmployeeByIdAsync(long id)
    {
        var item = await _service.GetEmployeeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工信息表(Employee)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employee:query", "查询员工信息表(Employee)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeOptionsAsync()
    {
        var result = await _service.GetEmployeeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工信息表(Employee)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employee:create", "创建员工信息表(Employee)")]
    public async Task<ActionResult<TaktEmployeeDto>> CreateEmployeeAsync([FromBody] TaktEmployeeCreateDto dto)
    {
        var result = await _service.CreateEmployeeAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工信息表(Employee)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employee:update", "更新员工信息表(Employee)")]
    public async Task<ActionResult<TaktEmployeeDto>> UpdateEmployeeAsync(long id, [FromBody] TaktEmployeeUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工信息表(Employee)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employee:delete", "删除员工信息表(Employee)")]
    public async Task<ActionResult> DeleteEmployeeByIdAsync(long id)
    {
        await _service.DeleteEmployeeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工信息表(Employee)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employee:delete", "批量删除员工信息表(Employee)")]
    public async Task<ActionResult> DeleteEmployeeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新员工信息表(Employee)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:personnel:employee:update", "更新员工信息表(Employee)状态")]
    public async Task<ActionResult<TaktEmployeeDto>> UpdateEmployeeStatusAsync([FromBody] TaktEmployeeStatusDto dto)
    {
        var result = await _service.UpdateEmployeeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取员工信息表(Employee)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employee:import", "获取员工信息表(Employee)导入模板")]
    public async Task<IActionResult> GetEmployeeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工信息表(Employee)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employee:import", "导入员工信息表(Employee)")]
    public async Task<ActionResult<object>> ImportEmployeeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工信息表(Employee)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employee:export", "导出员工信息表(Employee)")]
    public async Task<IActionResult> ExportEmployeeAsync([FromBody] TaktEmployeeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
