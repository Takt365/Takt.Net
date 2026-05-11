// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Bom
// 文件名称：TaktStandardOperationTimesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：标准工序时间表控制器，提供StandardOperationTime管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Application.Services.Logistics.Manufacturing.Bom;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Bom;

/// <summary>
/// 标准工序时间表控制器
/// </summary>
[Route("api/[controller]", Name = "标准工序时间表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:bom:standardoperationtime", "标准工序时间表管理")]
public class TaktStandardOperationTimesController : TaktControllerBase
{
    private readonly ITaktStandardOperationTimeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimesController(
        ITaktStandardOperationTimeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取标准工序时间表(StandardOperationTime)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:list", "查询标准工序时间表(StandardOperationTime)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktStandardOperationTimeDto>>> GetStandardOperationTimeListAsync([FromQuery] TaktStandardOperationTimeQueryDto queryDto)
    {
        var result = await _service.GetStandardOperationTimeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取标准工序时间表(StandardOperationTime)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:query", "查询标准工序时间表(StandardOperationTime)详情")]
    public async Task<ActionResult<TaktStandardOperationTimeDto>> GetStandardOperationTimeByIdAsync(long id)
    {
        var item = await _service.GetStandardOperationTimeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取标准工序时间表(StandardOperationTime)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:query", "查询标准工序时间表(StandardOperationTime)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetStandardOperationTimeOptionsAsync()
    {
        var result = await _service.GetStandardOperationTimeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建标准工序时间表(StandardOperationTime)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:create", "创建标准工序时间表(StandardOperationTime)")]
    public async Task<ActionResult<TaktStandardOperationTimeDto>> CreateStandardOperationTimeAsync([FromBody] TaktStandardOperationTimeCreateDto dto)
    {
        var result = await _service.CreateStandardOperationTimeAsync(dto);
        return CreatedAtAction(nameof(GetStandardOperationTimeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新标准工序时间表(StandardOperationTime)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:update", "更新标准工序时间表(StandardOperationTime)")]
    public async Task<ActionResult<TaktStandardOperationTimeDto>> UpdateStandardOperationTimeAsync(long id, [FromBody] TaktStandardOperationTimeUpdateDto dto)
    {
        var result = await _service.UpdateStandardOperationTimeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除标准工序时间表(StandardOperationTime)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:delete", "删除标准工序时间表(StandardOperationTime)")]
    public async Task<ActionResult> DeleteStandardOperationTimeByIdAsync(long id)
    {
        await _service.DeleteStandardOperationTimeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除标准工序时间表(StandardOperationTime)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:delete", "批量删除标准工序时间表(StandardOperationTime)")]
    public async Task<ActionResult> DeleteStandardOperationTimeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteStandardOperationTimeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新标准工序时间表(StandardOperationTime)Approval
    /// </summary>
    [HttpPut("status-approval")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:update", "更新标准工序时间表(StandardOperationTime)Approval")]
    public async Task<ActionResult<TaktStandardOperationTimeDto>> UpdateStandardOperationTimeApprovalStatusAsync([FromBody] TaktStandardOperationTimeApprovalStatusDto dto)
    {
        var result = await _service.UpdateStandardOperationTimeApprovalStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取标准工序时间表(StandardOperationTime)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:import", "获取标准工序时间表(StandardOperationTime)导入模板")]
    public async Task<IActionResult> GetStandardOperationTimeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetStandardOperationTimeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入标准工序时间表(StandardOperationTime)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:import", "导入标准工序时间表(StandardOperationTime)")]
    public async Task<ActionResult<object>> ImportStandardOperationTimeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportStandardOperationTimeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出标准工序时间表(StandardOperationTime)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:bom:standardoperationtime:export", "导出标准工序时间表(StandardOperationTime)")]
    public async Task<IActionResult> ExportStandardOperationTimeAsync([FromBody] TaktStandardOperationTimeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportStandardOperationTimeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
