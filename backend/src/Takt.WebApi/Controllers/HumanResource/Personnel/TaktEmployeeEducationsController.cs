// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationsController.cs
// 功能描述：员工教育经历控制器（CRUD + 导入导出）
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
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工教育经历控制器（权限前缀 humanresource:personnel:employeeeducation）
/// </summary>
[Route("api/[controller]", Name = "员工教育经历")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeeeducation:list", "员工教育经历管理")]
public class TaktEmployeeEducationsController : TaktControllerBase
{
    private readonly ITaktEmployeeEducationService _service;

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
    /// 分页查询员工教育经历列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeeeducation:list", "员工教育经历列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeEducationDto>>> GetEmployeeEducationListAsync([FromQuery] TaktEmployeeEducationQueryDto query)
    {
        var result = await _service.GetEmployeeEducationListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取员工教育经历详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeeducation:detail", "员工教育经历详情")]
    public async Task<ActionResult<TaktEmployeeEducationDto?>> GetEmployeeEducationByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeEducationByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建员工教育经历
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeeeducation:create", "创建员工教育经历")]
    public async Task<ActionResult<TaktEmployeeEducationDto>> CreateEmployeeEducationAsync([FromBody] TaktEmployeeEducationCreateDto dto)
    {
        var created = await _service.CreateEmployeeEducationAsync(dto);
        return Ok(created);
    }

    /// <summary>
    /// 更新员工教育经历
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeeducation:update", "更新员工教育经历")]
    public async Task<ActionResult<TaktEmployeeEducationDto>> UpdateEmployeeEducationAsync(long id, [FromBody] TaktEmployeeEducationUpdateDto dto)
    {
        if (dto.EmployeeEducationId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        var updated = await _service.UpdateEmployeeEducationAsync(id, dto);
        return Ok(updated);
    }

    /// <summary>
    /// 删除员工教育经历（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeeducation:delete", "删除员工教育经历")]
    public async Task<IActionResult> DeleteEmployeeEducationByIdAsync(long id)
    {
        await _service.DeleteEmployeeEducationByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工教育经历
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeeeducation:delete", "批量删除员工教育经历")]
    public async Task<IActionResult> DeleteEmployeeEducationBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeEducationBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeeeducation:template", "员工教育经历导入模板")]
    public async Task<IActionResult> GetEmployeeEducationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeEducationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
    }

    /// <summary>
    /// 导入员工教育经历
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeeeducation:import", "导入员工教育经历")]
    public async Task<ActionResult<object>> ImportEmployeeEducationAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));

        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportEmployeeEducationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

    /// <summary>
    /// 导出员工教育经历
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeeeducation:export", "导出员工教育经历")]
    public async Task<IActionResult> ExportEmployeeEducationAsync([FromBody] TaktEmployeeEducationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeEducationAsync(query, sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
    }
}
