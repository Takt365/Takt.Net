// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktEmployeePostsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工岗位关联表控制器，提供EmployeePost管理的RESTful API接口
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
/// 员工岗位关联表控制器
/// </summary>
[Route("api/[controller]", Name = "员工岗位关联表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:employeepost", "员工岗位关联表管理")]
public class TaktEmployeePostsController : TaktControllerBase
{
    private readonly ITaktEmployeePostService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeePostsController(
        ITaktEmployeePostService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工岗位关联表(EmployeePost)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:employeepost:list", "查询员工岗位关联表(EmployeePost)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeePostDto>>> GetEmployeePostListAsync([FromQuery] TaktEmployeePostQueryDto queryDto)
    {
        var result = await _service.GetEmployeePostListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工岗位关联表(EmployeePost)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:employeepost:query", "查询员工岗位关联表(EmployeePost)详情")]
    public async Task<ActionResult<TaktEmployeePostDto>> GetEmployeePostByIdAsync(long id)
    {
        var item = await _service.GetEmployeePostByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工岗位关联表(EmployeePost)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:employeepost:query", "查询员工岗位关联表(EmployeePost)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeePostOptionsAsync()
    {
        var result = await _service.GetEmployeePostOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工岗位关联表(EmployeePost)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:employeepost:create", "创建员工岗位关联表(EmployeePost)")]
    public async Task<ActionResult<TaktEmployeePostDto>> CreateEmployeePostAsync([FromBody] TaktEmployeePostCreateDto dto)
    {
        var result = await _service.CreateEmployeePostAsync(dto);
        return CreatedAtAction(nameof(GetEmployeePostByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工岗位关联表(EmployeePost)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:employeepost:update", "更新员工岗位关联表(EmployeePost)")]
    public async Task<ActionResult<TaktEmployeePostDto>> UpdateEmployeePostAsync(long id, [FromBody] TaktEmployeePostUpdateDto dto)
    {
        var result = await _service.UpdateEmployeePostAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工岗位关联表(EmployeePost)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:employeepost:delete", "删除员工岗位关联表(EmployeePost)")]
    public async Task<ActionResult> DeleteEmployeePostByIdAsync(long id)
    {
        await _service.DeleteEmployeePostByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工岗位关联表(EmployeePost)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:employeepost:delete", "批量删除员工岗位关联表(EmployeePost)")]
    public async Task<ActionResult> DeleteEmployeePostBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeePostBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取员工岗位关联表(EmployeePost)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:employeepost:import", "获取员工岗位关联表(EmployeePost)导入模板")]
    public async Task<IActionResult> GetEmployeePostTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeePostTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工岗位关联表(EmployeePost)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:employeepost:import", "导入员工岗位关联表(EmployeePost)")]
    public async Task<ActionResult<object>> ImportEmployeePostAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeePostAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工岗位关联表(EmployeePost)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:employeepost:export", "导出员工岗位关联表(EmployeePost)")]
    public async Task<IActionResult> ExportEmployeePostAsync([FromBody] TaktEmployeePostQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeePostAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
