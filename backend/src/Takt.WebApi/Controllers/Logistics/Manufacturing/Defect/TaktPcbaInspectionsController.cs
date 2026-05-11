// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaInspectionsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA检查日报表控制器，提供PcbaInspection管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Application.Services.Logistics.Manufacturing.Defect;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA检查日报表控制器
/// </summary>
[Route("api/[controller]", Name = "PCBA检查日报表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:defect:pcbainspection", "PCBA检查日报表管理")]
public class TaktPcbaInspectionsController : TaktControllerBase
{
    private readonly ITaktPcbaInspectionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaInspectionsController(
        ITaktPcbaInspectionService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取PCBA检查日报表(PcbaInspection)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:list", "查询PCBA检查日报表(PcbaInspection)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPcbaInspectionDto>>> GetPcbaInspectionListAsync([FromQuery] TaktPcbaInspectionQueryDto queryDto)
    {
        var result = await _service.GetPcbaInspectionListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取PCBA检查日报表(PcbaInspection)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:query", "查询PCBA检查日报表(PcbaInspection)详情")]
    public async Task<ActionResult<TaktPcbaInspectionDto>> GetPcbaInspectionByIdAsync(long id)
    {
        var item = await _service.GetPcbaInspectionByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取PCBA检查日报表(PcbaInspection)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:query", "查询PCBA检查日报表(PcbaInspection)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPcbaInspectionOptionsAsync()
    {
        var result = await _service.GetPcbaInspectionOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建PCBA检查日报表(PcbaInspection)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:create", "创建PCBA检查日报表(PcbaInspection)")]
    public async Task<ActionResult<TaktPcbaInspectionDto>> CreatePcbaInspectionAsync([FromBody] TaktPcbaInspectionCreateDto dto)
    {
        var result = await _service.CreatePcbaInspectionAsync(dto);
        return CreatedAtAction(nameof(GetPcbaInspectionByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新PCBA检查日报表(PcbaInspection)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:update", "更新PCBA检查日报表(PcbaInspection)")]
    public async Task<ActionResult<TaktPcbaInspectionDto>> UpdatePcbaInspectionAsync(long id, [FromBody] TaktPcbaInspectionUpdateDto dto)
    {
        var result = await _service.UpdatePcbaInspectionAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除PCBA检查日报表(PcbaInspection)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:delete", "删除PCBA检查日报表(PcbaInspection)")]
    public async Task<ActionResult> DeletePcbaInspectionByIdAsync(long id)
    {
        await _service.DeletePcbaInspectionByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除PCBA检查日报表(PcbaInspection)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:delete", "批量删除PCBA检查日报表(PcbaInspection)")]
    public async Task<ActionResult> DeletePcbaInspectionBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePcbaInspectionBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新PCBA检查日报表(PcbaInspection)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:update", "更新PCBA检查日报表(PcbaInspection)状态")]
    public async Task<ActionResult<TaktPcbaInspectionDto>> UpdatePcbaInspectionStatusAsync([FromBody] TaktPcbaInspectionStatusDto dto)
    {
        var result = await _service.UpdatePcbaInspectionStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取PCBA检查日报表(PcbaInspection)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:import", "获取PCBA检查日报表(PcbaInspection)导入模板")]
    public async Task<IActionResult> GetPcbaInspectionTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPcbaInspectionTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入PCBA检查日报表(PcbaInspection)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:import", "导入PCBA检查日报表(PcbaInspection)")]
    public async Task<ActionResult<object>> ImportPcbaInspectionAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPcbaInspectionAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出PCBA检查日报表(PcbaInspection)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspection:export", "导出PCBA检查日报表(PcbaInspection)")]
    public async Task<IActionResult> ExportPcbaInspectionAsync([FromBody] TaktPcbaInspectionQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPcbaInspectionAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
