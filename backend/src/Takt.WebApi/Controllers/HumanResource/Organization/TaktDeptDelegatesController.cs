// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktDeptDelegatesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：部门代理表控制器，提供DeptDelegate管理的RESTful API接口
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
/// 部门代理表控制器
/// </summary>
[Route("api/[controller]", Name = "部门代理表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:deptdelegate", "部门代理表管理")]
public class TaktDeptDelegatesController : TaktControllerBase
{
    private readonly ITaktDeptDelegateService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptDelegatesController(
        ITaktDeptDelegateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取部门代理表(DeptDelegate)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:deptdelegate:list", "查询部门代理表(DeptDelegate)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDeptDelegateDto>>> GetDeptDelegateListAsync([FromQuery] TaktDeptDelegateQueryDto queryDto)
    {
        var result = await _service.GetDeptDelegateListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取部门代理表(DeptDelegate)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:deptdelegate:query", "查询部门代理表(DeptDelegate)详情")]
    public async Task<ActionResult<TaktDeptDelegateDto>> GetDeptDelegateByIdAsync(long id)
    {
        var item = await _service.GetDeptDelegateByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取部门代理表(DeptDelegate)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:deptdelegate:query", "查询部门代理表(DeptDelegate)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetDeptDelegateOptionsAsync()
    {
        var result = await _service.GetDeptDelegateOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建部门代理表(DeptDelegate)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:deptdelegate:create", "创建部门代理表(DeptDelegate)")]
    public async Task<ActionResult<TaktDeptDelegateDto>> CreateDeptDelegateAsync([FromBody] TaktDeptDelegateCreateDto dto)
    {
        var result = await _service.CreateDeptDelegateAsync(dto);
        return CreatedAtAction(nameof(GetDeptDelegateByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新部门代理表(DeptDelegate)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:deptdelegate:update", "更新部门代理表(DeptDelegate)")]
    public async Task<ActionResult<TaktDeptDelegateDto>> UpdateDeptDelegateAsync(long id, [FromBody] TaktDeptDelegateUpdateDto dto)
    {
        var result = await _service.UpdateDeptDelegateAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除部门代理表(DeptDelegate)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:deptdelegate:delete", "删除部门代理表(DeptDelegate)")]
    public async Task<ActionResult> DeleteDeptDelegateByIdAsync(long id)
    {
        await _service.DeleteDeptDelegateByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除部门代理表(DeptDelegate)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:deptdelegate:delete", "批量删除部门代理表(DeptDelegate)")]
    public async Task<ActionResult> DeleteDeptDelegateBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteDeptDelegateBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新部门代理表(DeptDelegate)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:organization:deptdelegate:update", "更新部门代理表(DeptDelegate)排序")]
    public async Task<ActionResult<TaktDeptDelegateDto>> UpdateDeptDelegateSortAsync([FromBody] TaktDeptDelegateSortDto dto)
    {
        var result = await _service.UpdateDeptDelegateSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取部门代理表(DeptDelegate)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:deptdelegate:import", "获取部门代理表(DeptDelegate)导入模板")]
    public async Task<IActionResult> GetDeptDelegateTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetDeptDelegateTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入部门代理表(DeptDelegate)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:deptdelegate:import", "导入部门代理表(DeptDelegate)")]
    public async Task<ActionResult<object>> ImportDeptDelegateAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportDeptDelegateAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出部门代理表(DeptDelegate)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:deptdelegate:export", "导出部门代理表(DeptDelegate)")]
    public async Task<IActionResult> ExportDeptDelegateAsync([FromBody] TaktDeptDelegateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportDeptDelegateAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
