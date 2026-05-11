// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrdersController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：制程检验单表控制器，提供IpqcOrder管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Operation;

/// <summary>
/// 制程检验单表控制器
/// </summary>
[Route("api/[controller]", Name = "制程检验单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:ipqcorder", "制程检验单表管理")]
public class TaktIpqcOrdersController : TaktControllerBase
{
    private readonly ITaktIpqcOrderService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrdersController(
        ITaktIpqcOrderService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取制程检验单表(IpqcOrder)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:ipqcorder:list", "查询制程检验单表(IpqcOrder)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIpqcOrderDto>>> GetIpqcOrderListAsync([FromQuery] TaktIpqcOrderQueryDto queryDto)
    {
        var result = await _service.GetIpqcOrderListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取制程检验单表(IpqcOrder)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorder:query", "查询制程检验单表(IpqcOrder)详情")]
    public async Task<ActionResult<TaktIpqcOrderDto>> GetIpqcOrderByIdAsync(long id)
    {
        var item = await _service.GetIpqcOrderByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取制程检验单表(IpqcOrder)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:ipqcorder:query", "查询制程检验单表(IpqcOrder)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIpqcOrderOptionsAsync()
    {
        var result = await _service.GetIpqcOrderOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建制程检验单表(IpqcOrder)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:ipqcorder:create", "创建制程检验单表(IpqcOrder)")]
    public async Task<ActionResult<TaktIpqcOrderDto>> CreateIpqcOrderAsync([FromBody] TaktIpqcOrderCreateDto dto)
    {
        var result = await _service.CreateIpqcOrderAsync(dto);
        return CreatedAtAction(nameof(GetIpqcOrderByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新制程检验单表(IpqcOrder)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorder:update", "更新制程检验单表(IpqcOrder)")]
    public async Task<ActionResult<TaktIpqcOrderDto>> UpdateIpqcOrderAsync(long id, [FromBody] TaktIpqcOrderUpdateDto dto)
    {
        var result = await _service.UpdateIpqcOrderAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除制程检验单表(IpqcOrder)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorder:delete", "删除制程检验单表(IpqcOrder)")]
    public async Task<ActionResult> DeleteIpqcOrderByIdAsync(long id)
    {
        await _service.DeleteIpqcOrderByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除制程检验单表(IpqcOrder)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:ipqcorder:delete", "批量删除制程检验单表(IpqcOrder)")]
    public async Task<ActionResult> DeleteIpqcOrderBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIpqcOrderBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新制程检验单表(IpqcOrder)Judge
    /// </summary>
    [HttpPut("status-judge")]
    [TaktPermission("logistics:quality:operation:ipqcorder:update", "更新制程检验单表(IpqcOrder)Judge")]
    public async Task<ActionResult<TaktIpqcOrderDto>> UpdateIpqcOrderJudgeStatusAsync([FromBody] TaktIpqcOrderJudgeStatusDto dto)
    {
        var result = await _service.UpdateIpqcOrderJudgeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取制程检验单表(IpqcOrder)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:ipqcorder:import", "获取制程检验单表(IpqcOrder)导入模板")]
    public async Task<IActionResult> GetIpqcOrderTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIpqcOrderTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入制程检验单表(IpqcOrder)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:ipqcorder:import", "导入制程检验单表(IpqcOrder)")]
    public async Task<ActionResult<object>> ImportIpqcOrderAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIpqcOrderAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出制程检验单表(IpqcOrder)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:ipqcorder:export", "导出制程检验单表(IpqcOrder)")]
    public async Task<IActionResult> ExportIpqcOrderAsync([FromBody] TaktIpqcOrderQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIpqcOrderAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
