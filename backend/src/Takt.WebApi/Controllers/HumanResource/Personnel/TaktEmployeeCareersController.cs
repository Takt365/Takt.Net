// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeCareersController.cs
// 功能描述：员工职业信息控制器（CRUD + 导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工职业信息控制器（权限前缀 humanresource:personnel:employeecareer）
/// </summary>
[Route("api/[controller]", Name = "员工职业信息")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeecareer:list", "员工职业信息管理")]
public class TaktEmployeeCareersController : TaktControllerBase
{
    private readonly ITaktEmployeeCareerService _service;

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
    /// 分页查询员工职业信息列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeecareer:list", "员工职业信息列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeCareerDto>>> GetEmployeeCareerListAsync([FromQuery] TaktEmployeeCareerQueryDto query)
    {
        var result = await _service.GetEmployeeCareerListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取员工职业信息详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeecareer:detail", "员工职业信息详情")]
    public async Task<ActionResult<TaktEmployeeCareerDto?>> GetEmployeeCareerByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeCareerByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建员工职业信息
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeecareer:create", "创建员工职业信息")]
    public async Task<ActionResult<TaktEmployeeCareerDto>> CreateEmployeeCareerAsync([FromBody] TaktEmployeeCareerCreateDto dto)
    {
        var created = await _service.CreateEmployeeCareerAsync(dto);
        return Ok(created);
    }

    /// <summary>
    /// 更新员工职业信息
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeecareer:update", "更新员工职业信息")]
    public async Task<ActionResult<TaktEmployeeCareerDto>> UpdateEmployeeCareerAsync(long id, [FromBody] TaktEmployeeCareerUpdateDto dto)
    {
        if (dto.EmployeeCareerId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        var updated = await _service.UpdateEmployeeCareerAsync(id, dto);
        return Ok(updated);
    }

    /// <summary>
    /// 删除员工职业信息（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeecareer:delete", "删除员工职业信息")]
    public async Task<IActionResult> DeleteEmployeeCareerByIdAsync(long id)
    {
        await _service.DeleteEmployeeCareerByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工职业信息
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeecareer:delete", "批量删除员工职业信息")]
    public async Task<IActionResult> DeleteEmployeeCareerBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeCareerBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeecareer:template", "员工职业信息导入模板")]
    public async Task<IActionResult> GetEmployeeCareerTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetEmployeeCareerTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入员工职业信息
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeecareer:import", "导入员工职业信息")]
    public async Task<ActionResult<object>> ImportEmployeeCareerAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出员工职业信息
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeecareer:export", "导出员工职业信息")]
    public async Task<IActionResult> ExportEmployeeCareerAsync([FromBody] TaktEmployeeCareerQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportEmployeeCareerAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
