// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktCostCentersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：成本中心表控制器，提供CostCenter管理的RESTful API接口
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
/// 成本中心表控制器
/// </summary>
[Route("api/[controller]", Name = "成本中心表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:costcenter", "成本中心表管理")]
public class TaktCostCentersController : TaktControllerBase
{
    private readonly ITaktCostCenterService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCentersController(
        ITaktCostCenterService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取成本中心表(CostCenter)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:costcenter:list", "查询成本中心表(CostCenter)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCostCenterDto>>> GetCostCenterListAsync([FromQuery] TaktCostCenterQueryDto queryDto)
    {
        var result = await _service.GetCostCenterListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取成本中心表(CostCenter)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:costcenter:query", "查询成本中心表(CostCenter)详情")]
    public async Task<ActionResult<TaktCostCenterDto>> GetCostCenterByIdAsync(long id)
    {
        var item = await _service.GetCostCenterByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取成本中心表(CostCenter)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:costcenter:query", "查询成本中心表(CostCenter)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCostCenterOptionsAsync()
    {
        var result = await _service.GetCostCenterOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取成本中心表(CostCenter)树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("accounting:controlling:costcenter:query", "查询成本中心表(CostCenter)树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetCostCenterTreeOptionsAsync()
    {
        var result = await _service.GetCostCenterTreeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取成本中心表(CostCenter)树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("accounting:controlling:costcenter:query", "查询成本中心表(CostCenter)树形")]
    public async Task<ActionResult<List<TaktCostCenterTreeDto>>> GetCostCenterTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetCostCenterTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 获取成本中心表(CostCenter)子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("accounting:controlling:costcenter:query", "查询成本中心表(CostCenter)子节点")]
    public async Task<ActionResult<List<TaktCostCenterDto>>> GetCostCenterChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetCostCenterChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 创建成本中心表(CostCenter)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:costcenter:create", "创建成本中心表(CostCenter)")]
    public async Task<ActionResult<TaktCostCenterDto>> CreateCostCenterAsync([FromBody] TaktCostCenterCreateDto dto)
    {
        var result = await _service.CreateCostCenterAsync(dto);
        return CreatedAtAction(nameof(GetCostCenterByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新成本中心表(CostCenter)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:costcenter:update", "更新成本中心表(CostCenter)")]
    public async Task<ActionResult<TaktCostCenterDto>> UpdateCostCenterAsync(long id, [FromBody] TaktCostCenterUpdateDto dto)
    {
        var result = await _service.UpdateCostCenterAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除成本中心表(CostCenter)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:costcenter:delete", "删除成本中心表(CostCenter)")]
    public async Task<ActionResult> DeleteCostCenterByIdAsync(long id)
    {
        await _service.DeleteCostCenterByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除成本中心表(CostCenter)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:controlling:costcenter:delete", "批量删除成本中心表(CostCenter)")]
    public async Task<ActionResult> DeleteCostCenterBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCostCenterBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新成本中心表(CostCenter)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:controlling:costcenter:update", "更新成本中心表(CostCenter)状态")]
    public async Task<ActionResult<TaktCostCenterDto>> UpdateCostCenterStatusAsync([FromBody] TaktCostCenterStatusDto dto)
    {
        var result = await _service.UpdateCostCenterStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新成本中心表(CostCenter)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("accounting:controlling:costcenter:update", "更新成本中心表(CostCenter)排序")]
    public async Task<ActionResult<TaktCostCenterDto>> UpdateCostCenterSortAsync([FromBody] TaktCostCenterSortDto dto)
    {
        var result = await _service.UpdateCostCenterSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取成本中心表(CostCenter)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:costcenter:import", "获取成本中心表(CostCenter)导入模板")]
    public async Task<IActionResult> GetCostCenterTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCostCenterTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入成本中心表(CostCenter)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:costcenter:import", "导入成本中心表(CostCenter)")]
    public async Task<ActionResult<object>> ImportCostCenterAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCostCenterAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出成本中心表(CostCenter)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:costcenter:export", "导出成本中心表(CostCenter)")]
    public async Task<IActionResult> ExportCostCenterAsync([FromBody] TaktCostCenterQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCostCenterAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
