// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeSkillsController.cs
// 功能描述：员工业务技能控制器（CRUD + 导入导出）
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
/// 员工业务技能控制器（权限前缀 humanresource:personnel:employeeskill）
/// </summary>
[Route("api/[controller]", Name = "员工业务技能")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeeskill:list", "员工业务技能管理")]
public class TaktEmployeeSkillsController : TaktControllerBase
{
    private readonly ITaktEmployeeSkillService _service;

    public TaktEmployeeSkillsController(
        ITaktEmployeeSkillService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 分页查询员工业务技能列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeeskill:list", "员工业务技能列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeSkillDto>>> GetEmployeeSkillListAsync([FromQuery] TaktEmployeeSkillQueryDto query)
        => Ok(await _service.GetEmployeeSkillListAsync(query));

    /// <summary>
    /// 根据ID获取员工业务技能详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeskill:detail", "员工业务技能详情")]
    public async Task<ActionResult<TaktEmployeeSkillDto?>> GetEmployeeSkillByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeSkillByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    /// <summary>
    /// 创建员工业务技能
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeeskill:create", "创建员工业务技能")]
    public async Task<ActionResult<TaktEmployeeSkillDto>> CreateEmployeeSkillAsync([FromBody] TaktEmployeeSkillCreateDto dto)
        => Ok(await _service.CreateEmployeeSkillAsync(dto));

    /// <summary>
    /// 更新员工业务技能
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeskill:update", "更新员工业务技能")]
    public async Task<ActionResult<TaktEmployeeSkillDto>> UpdateEmployeeSkillAsync(long id, [FromBody] TaktEmployeeSkillUpdateDto dto)
    {
        if (dto.EmployeeSkillId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        return Ok(await _service.UpdateEmployeeSkillAsync(id, dto));
    }

    /// <summary>
    /// 删除员工业务技能（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeskill:delete", "删除员工业务技能")]
    public async Task<IActionResult> DeleteEmployeeSkillByIdAsync(long id)
    {
        await _service.DeleteEmployeeSkillByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工业务技能
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeeskill:delete", "批量删除员工业务技能")]
    public async Task<IActionResult> DeleteEmployeeSkillBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeSkillBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeeskill:template", "员工业务技能导入模板")]
    public async Task<IActionResult> GetEmployeeSkillTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeSkillTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
    }

    /// <summary>
    /// 导入员工业务技能
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeeskill:import", "导入员工业务技能")]
    public async Task<ActionResult<object>> ImportEmployeeSkillAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportEmployeeSkillAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

    /// <summary>
    /// 导出员工业务技能
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeeskill:export", "导出员工业务技能")]
    public async Task<IActionResult> ExportEmployeeSkillAsync([FromBody] TaktEmployeeSkillQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeSkillAsync(query, sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
    }
}
