// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeWorksController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工工作履历表控制器，提供EmployeeWork管理的RESTful API接口
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
/// 员工工作履历表控制器
/// </summary>
[Route("api/[controller]", Name = "员工工作履历表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeework", "员工工作履历表管理")]
public class TaktEmployeeWorksController : TaktControllerBase
{
    private readonly ITaktEmployeeWorkService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorksController(
        ITaktEmployeeWorkService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工工作履历表(EmployeeWork)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeework:list", "查询员工工作履历表(EmployeeWork)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeWorkDto>>> GetEmployeeWorkListAsync([FromQuery] TaktEmployeeWorkQueryDto queryDto)
    {
        var result = await _service.GetEmployeeWorkListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工工作履历表(EmployeeWork)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeework:query", "查询员工工作履历表(EmployeeWork)详情")]
    public async Task<ActionResult<TaktEmployeeWorkDto>> GetEmployeeWorkByIdAsync(long id)
    {
        var item = await _service.GetEmployeeWorkByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工工作履历表(EmployeeWork)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeework:query", "查询员工工作履历表(EmployeeWork)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeWorkOptionsAsync()
    {
        var result = await _service.GetEmployeeWorkOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工工作履历表(EmployeeWork)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeework:create", "创建员工工作履历表(EmployeeWork)")]
    public async Task<ActionResult<TaktEmployeeWorkDto>> CreateEmployeeWorkAsync([FromBody] TaktEmployeeWorkCreateDto dto)
    {
        var result = await _service.CreateEmployeeWorkAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeWorkByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工工作履历表(EmployeeWork)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeework:update", "更新员工工作履历表(EmployeeWork)")]
    public async Task<ActionResult<TaktEmployeeWorkDto>> UpdateEmployeeWorkAsync(long id, [FromBody] TaktEmployeeWorkUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeWorkAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工工作履历表(EmployeeWork)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeework:delete", "删除员工工作履历表(EmployeeWork)")]
    public async Task<ActionResult> DeleteEmployeeWorkByIdAsync(long id)
    {
        await _service.DeleteEmployeeWorkByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工工作履历表(EmployeeWork)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeework:delete", "批量删除员工工作履历表(EmployeeWork)")]
    public async Task<ActionResult> DeleteEmployeeWorkBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeWorkBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取员工工作履历表(EmployeeWork)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeework:import", "获取员工工作履历表(EmployeeWork)导入模板")]
    public async Task<IActionResult> GetEmployeeWorkTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeWorkTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工工作履历表(EmployeeWork)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeework:import", "导入员工工作履历表(EmployeeWork)")]
    public async Task<ActionResult<object>> ImportEmployeeWorkAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeWorkAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工工作履历表(EmployeeWork)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeework:export", "导出员工工作履历表(EmployeeWork)")]
    public async Task<IActionResult> ExportEmployeeWorkAsync([FromBody] TaktEmployeeWorkQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeWorkAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
