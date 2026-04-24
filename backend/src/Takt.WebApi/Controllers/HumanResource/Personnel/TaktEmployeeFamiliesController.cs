// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeFamiliesController.cs
// 功能描述：员工家庭成员控制器（CRUD + 导入导出）
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
/// 员工家庭成员控制器（权限前缀 humanresource:personnel:employeefamily）
/// </summary>
[Route("api/[controller]", Name = "员工家庭成员")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeefamily:list", "员工家庭成员管理")]
public class TaktEmployeeFamiliesController : TaktControllerBase
{
    private readonly ITaktEmployeeFamilyService _service;

    public TaktEmployeeFamiliesController(
        ITaktEmployeeFamilyService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 分页查询员工家庭成员列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeefamily:list", "员工家庭成员列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeFamilyDto>>> GetEmployeeFamilyListAsync([FromQuery] TaktEmployeeFamilyQueryDto query)
        => Ok(await _service.GetEmployeeFamilyListAsync(query));

    /// <summary>
    /// 根据ID获取员工家庭成员详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeefamily:detail", "员工家庭成员详情")]
    public async Task<ActionResult<TaktEmployeeFamilyDto?>> GetEmployeeFamilyByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeFamilyByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    /// <summary>
    /// 创建员工家庭成员
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeefamily:create", "创建员工家庭成员")]
    public async Task<ActionResult<TaktEmployeeFamilyDto>> CreateEmployeeFamilyAsync([FromBody] TaktEmployeeFamilyCreateDto dto)
        => Ok(await _service.CreateEmployeeFamilyAsync(dto));

    /// <summary>
    /// 更新员工家庭成员
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeefamily:update", "更新员工家庭成员")]
    public async Task<ActionResult<TaktEmployeeFamilyDto>> UpdateEmployeeFamilyAsync(long id, [FromBody] TaktEmployeeFamilyUpdateDto dto)
    {
        if (dto.EmployeeFamilyId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        return Ok(await _service.UpdateEmployeeFamilyAsync(id, dto));
    }

    /// <summary>
    /// 删除员工家庭成员（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeefamily:delete", "删除员工家庭成员")]
    public async Task<IActionResult> DeleteEmployeeFamilyByIdAsync(long id)
    {
        await _service.DeleteEmployeeFamilyByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工家庭成员
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeefamily:delete", "批量删除员工家庭成员")]
    public async Task<IActionResult> DeleteEmployeeFamilyBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeFamilyBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeefamily:template", "员工家庭成员导入模板")]
    public async Task<IActionResult> GetEmployeeFamilyTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeFamilyTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }

    /// <summary>
    /// 导入员工家庭成员
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeefamily:import", "导入员工家庭成员")]
    public async Task<ActionResult<object>> ImportEmployeeFamilyAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportEmployeeFamilyAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

    /// <summary>
    /// 导出员工家庭成员
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeefamily:export", "导出员工家庭成员")]
    public async Task<IActionResult> ExportEmployeeFamilyAsync([FromBody] TaktEmployeeFamilyQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeFamilyAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }
}
