// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktProfitCentersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：利润中心表控制器，提供ProfitCenter管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 利润中心表控制器
/// </summary>
[Route("api/[controller]", Name = "利润中心表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:profitcenter", "利润中心表管理")]
public class TaktProfitCentersController : TaktControllerBase
{
    private readonly ITaktProfitCenterService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCentersController(
        ITaktProfitCenterService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:profitcenter:list", "查询利润中心表(ProfitCenter)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProfitCenterDto>>> GetProfitCenterListAsync([FromQuery] TaktProfitCenterQueryDto queryDto)
    {
        var result = await _service.GetProfitCenterListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取利润中心表(ProfitCenter)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:profitcenter:query", "查询利润中心表(ProfitCenter)详情")]
    public async Task<ActionResult<TaktProfitCenterDto>> GetProfitCenterByIdAsync(long id)
    {
        var item = await _service.GetProfitCenterByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:profitcenter:query", "查询利润中心表(ProfitCenter)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProfitCenterOptionsAsync()
    {
        var result = await _service.GetProfitCenterOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("accounting:controlling:profitcenter:query", "查询利润中心表(ProfitCenter)树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetProfitCenterTreeOptionsAsync()
    {
        var result = await _service.GetProfitCenterTreeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("accounting:controlling:profitcenter:query", "查询利润中心表(ProfitCenter)树形")]
    public async Task<ActionResult<List<TaktProfitCenterTreeDto>>> GetProfitCenterTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetProfitCenterTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("accounting:controlling:profitcenter:query", "查询利润中心表(ProfitCenter)子节点")]
    public async Task<ActionResult<List<TaktProfitCenterDto>>> GetProfitCenterChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetProfitCenterChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 创建利润中心表(ProfitCenter)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:profitcenter:create", "创建利润中心表(ProfitCenter)")]
    public async Task<ActionResult<TaktProfitCenterDto>> CreateProfitCenterAsync([FromBody] TaktProfitCenterCreateDto dto)
    {
        var result = await _service.CreateProfitCenterAsync(dto);
        return CreatedAtAction(nameof(GetProfitCenterByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新利润中心表(ProfitCenter)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:profitcenter:update", "更新利润中心表(ProfitCenter)")]
    public async Task<ActionResult<TaktProfitCenterDto>> UpdateProfitCenterAsync(long id, [FromBody] TaktProfitCenterUpdateDto dto)
    {
        var result = await _service.UpdateProfitCenterAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除利润中心表(ProfitCenter)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:profitcenter:delete", "删除利润中心表(ProfitCenter)")]
    public async Task<ActionResult> DeleteProfitCenterByIdAsync(long id)
    {
        await _service.DeleteProfitCenterByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除利润中心表(ProfitCenter)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:controlling:profitcenter:delete", "批量删除利润中心表(ProfitCenter)")]
    public async Task<ActionResult> DeleteProfitCenterBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProfitCenterBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新利润中心表(ProfitCenter)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:controlling:profitcenter:update", "更新利润中心表(ProfitCenter)状态")]
    public async Task<ActionResult<TaktProfitCenterDto>> UpdateProfitCenterStatusAsync([FromBody] TaktProfitCenterStatusDto dto)
    {
        var result = await _service.UpdateProfitCenterStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新利润中心表(ProfitCenter)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("accounting:controlling:profitcenter:update", "更新利润中心表(ProfitCenter)排序")]
    public async Task<ActionResult<TaktProfitCenterDto>> UpdateProfitCenterSortAsync([FromBody] TaktProfitCenterSortDto dto)
    {
        var result = await _service.UpdateProfitCenterSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:profitcenter:import", "获取利润中心表(ProfitCenter)导入模板")]
    public async Task<IActionResult> GetProfitCenterTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProfitCenterTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入利润中心表(ProfitCenter)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:profitcenter:import", "导入利润中心表(ProfitCenter)")]
    public async Task<ActionResult<object>> ImportProfitCenterAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProfitCenterAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出利润中心表(ProfitCenter)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:profitcenter:export", "导出利润中心表(ProfitCenter)")]
    public async Task<IActionResult> ExportProfitCenterAsync([FromBody] TaktProfitCenterQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProfitCenterAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
