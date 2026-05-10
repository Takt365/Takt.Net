// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktDeptsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：部门信息表控制器，提供Dept管理的RESTful API接口
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
/// 部门信息表控制器
/// </summary>
[Route("api/[controller]", Name = "部门信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:dept", "部门信息表管理")]
public class TaktDeptsController : TaktControllerBase
{
    private readonly ITaktDeptService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptsController(
        ITaktDeptService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取部门信息表(Dept)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:dept:list", "查询部门信息表(Dept)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDeptDto>>> GetDeptListAsync([FromQuery] TaktDeptQueryDto queryDto)
    {
        var result = await _service.GetDeptListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取部门信息表(Dept)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:dept:query", "查询部门信息表(Dept)详情")]
    public async Task<ActionResult<TaktDeptDto>> GetDeptByIdAsync(long id)
    {
        var item = await _service.GetDeptByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取部门信息表(Dept)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:dept:query", "查询部门信息表(Dept)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetDeptOptionsAsync()
    {
        var result = await _service.GetDeptOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取部门信息表(Dept)树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("humanresource:organization:dept:query", "查询部门信息表(Dept)树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetDeptTreeOptionsAsync()
    {
        var result = await _service.GetDeptTreeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取部门信息表(Dept)树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("humanresource:organization:dept:query", "查询部门信息表(Dept)树形")]
    public async Task<ActionResult<List<TaktDeptTreeDto>>> GetDeptTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetDeptTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 获取部门信息表(Dept)子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("humanresource:organization:dept:query", "查询部门信息表(Dept)子节点")]
    public async Task<ActionResult<List<TaktDeptDto>>> GetDeptChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetDeptChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 创建部门信息表(Dept)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:dept:create", "创建部门信息表(Dept)")]
    public async Task<ActionResult<TaktDeptDto>> CreateDeptAsync([FromBody] TaktDeptCreateDto dto)
    {
        var result = await _service.CreateDeptAsync(dto);
        return CreatedAtAction(nameof(GetDeptByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新部门信息表(Dept)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:dept:update", "更新部门信息表(Dept)")]
    public async Task<ActionResult<TaktDeptDto>> UpdateDeptAsync(long id, [FromBody] TaktDeptUpdateDto dto)
    {
        var result = await _service.UpdateDeptAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除部门信息表(Dept)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:dept:delete", "删除部门信息表(Dept)")]
    public async Task<ActionResult> DeleteDeptByIdAsync(long id)
    {
        await _service.DeleteDeptByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除部门信息表(Dept)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:dept:delete", "批量删除部门信息表(Dept)")]
    public async Task<ActionResult> DeleteDeptBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteDeptBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新部门信息表(Dept)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:organization:dept:update", "更新部门信息表(Dept)状态")]
    public async Task<ActionResult<TaktDeptDto>> UpdateDeptStatusAsync([FromBody] TaktDeptStatusDto dto)
    {
        var result = await _service.UpdateDeptStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新部门信息表(Dept)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:organization:dept:update", "更新部门信息表(Dept)排序")]
    public async Task<ActionResult<TaktDeptDto>> UpdateDeptSortAsync([FromBody] TaktDeptSortDto dto)
    {
        var result = await _service.UpdateDeptSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取部门信息表(Dept)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:dept:import", "获取部门信息表(Dept)导入模板")]
    public async Task<IActionResult> GetDeptTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetDeptTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入部门信息表(Dept)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:dept:import", "导入部门信息表(Dept)")]
    public async Task<ActionResult<object>> ImportDeptAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportDeptAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出部门信息表(Dept)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:dept:export", "导出部门信息表(Dept)")]
    public async Task<IActionResult> ExportDeptAsync([FromBody] TaktDeptQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportDeptAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
