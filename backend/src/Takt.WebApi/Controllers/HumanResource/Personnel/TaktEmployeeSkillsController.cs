// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeSkillsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：员工业务技能表控制器，提供EmployeeSkill管理的RESTful API接口
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
/// 员工业务技能表控制器
/// </summary>
[Route("api/[controller]", Name = "员工业务技能表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeeskill", "员工业务技能表管理")]
public class TaktEmployeeSkillsController : TaktControllerBase
{
    private readonly ITaktEmployeeSkillService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
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
    /// 获取员工业务技能表(EmployeeSkill)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeeskill:list", "查询员工业务技能表(EmployeeSkill)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeSkillDto>>> GetEmployeeSkillListAsync([FromQuery] TaktEmployeeSkillQueryDto queryDto)
    {
        var result = await _service.GetEmployeeSkillListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工业务技能表(EmployeeSkill)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeeskill:query", "查询员工业务技能表(EmployeeSkill)详情")]
    public async Task<ActionResult<TaktEmployeeSkillDto>> GetEmployeeSkillByIdAsync(long id)
    {
        var item = await _service.GetEmployeeSkillByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工业务技能表(EmployeeSkill)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeeskill:query", "查询员工业务技能表(EmployeeSkill)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeSkillOptionsAsync()
    {
        var result = await _service.GetEmployeeSkillOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工业务技能表(EmployeeSkill)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeeskill:create", "创建员工业务技能表(EmployeeSkill)")]
    public async Task<ActionResult<TaktEmployeeSkillDto>> CreateEmployeeSkillAsync([FromBody] TaktEmployeeSkillCreateDto dto)
    {
        var result = await _service.CreateEmployeeSkillAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeSkillByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工业务技能表(EmployeeSkill)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeeskill:update", "更新员工业务技能表(EmployeeSkill)")]
    public async Task<ActionResult<TaktEmployeeSkillDto>> UpdateEmployeeSkillAsync(long id, [FromBody] TaktEmployeeSkillUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeSkillAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工业务技能表(EmployeeSkill)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeeskill:delete", "删除员工业务技能表(EmployeeSkill)")]
    public async Task<ActionResult> DeleteEmployeeSkillByIdAsync(long id)
    {
        await _service.DeleteEmployeeSkillByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工业务技能表(EmployeeSkill)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeeskill:delete", "批量删除员工业务技能表(EmployeeSkill)")]
    public async Task<ActionResult> DeleteEmployeeSkillBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeSkillBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取员工业务技能表(EmployeeSkill)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeeskill:import", "获取员工业务技能表(EmployeeSkill)导入模板")]
    public async Task<IActionResult> GetEmployeeSkillTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeSkillTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工业务技能表(EmployeeSkill)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeeskill:import", "导入员工业务技能表(EmployeeSkill)")]
    public async Task<ActionResult<object>> ImportEmployeeSkillAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeSkillAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工业务技能表(EmployeeSkill)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeeskill:export", "导出员工业务技能表(EmployeeSkill)")]
    public async Task<IActionResult> ExportEmployeeSkillAsync([FromBody] TaktEmployeeSkillQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeSkillAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
