// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaInspectionDetailsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA检查明细表控制器，提供PcbaInspectionDetail管理的RESTful API接口
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
/// PCBA检查明细表控制器
/// </summary>
[Route("api/[controller]", Name = "PCBA检查明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail", "PCBA检查明细表管理")]
public class TaktPcbaInspectionDetailsController : TaktControllerBase
{
    private readonly ITaktPcbaInspectionDetailService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaInspectionDetailsController(
        ITaktPcbaInspectionDetailService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取PCBA检查明细表(PcbaInspectionDetail)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:list", "查询PCBA检查明细表(PcbaInspectionDetail)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPcbaInspectionDetailDto>>> GetPcbaInspectionDetailListAsync([FromQuery] TaktPcbaInspectionDetailQueryDto queryDto)
    {
        var result = await _service.GetPcbaInspectionDetailListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:query", "查询PCBA检查明细表(PcbaInspectionDetail)详情")]
    public async Task<ActionResult<TaktPcbaInspectionDetailDto>> GetPcbaInspectionDetailByIdAsync(long id)
    {
        var item = await _service.GetPcbaInspectionDetailByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取PCBA检查明细表(PcbaInspectionDetail)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:query", "查询PCBA检查明细表(PcbaInspectionDetail)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPcbaInspectionDetailOptionsAsync()
    {
        var result = await _service.GetPcbaInspectionDetailOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:create", "创建PCBA检查明细表(PcbaInspectionDetail)")]
    public async Task<ActionResult<TaktPcbaInspectionDetailDto>> CreatePcbaInspectionDetailAsync([FromBody] TaktPcbaInspectionDetailCreateDto dto)
    {
        var result = await _service.CreatePcbaInspectionDetailAsync(dto);
        return CreatedAtAction(nameof(GetPcbaInspectionDetailByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:update", "更新PCBA检查明细表(PcbaInspectionDetail)")]
    public async Task<ActionResult<TaktPcbaInspectionDetailDto>> UpdatePcbaInspectionDetailAsync(long id, [FromBody] TaktPcbaInspectionDetailUpdateDto dto)
    {
        var result = await _service.UpdatePcbaInspectionDetailAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:delete", "删除PCBA检查明细表(PcbaInspectionDetail)")]
    public async Task<ActionResult> DeletePcbaInspectionDetailByIdAsync(long id)
    {
        await _service.DeletePcbaInspectionDetailByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:delete", "批量删除PCBA检查明细表(PcbaInspectionDetail)")]
    public async Task<ActionResult> DeletePcbaInspectionDetailBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePcbaInspectionDetailBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新PCBA检查明细表(PcbaInspectionDetail)Inspection
    /// </summary>
    [HttpPut("status-inspection")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:update", "更新PCBA检查明细表(PcbaInspectionDetail)Inspection")]
    public async Task<ActionResult<TaktPcbaInspectionDetailDto>> UpdatePcbaInspectionDetailInspectionStatusAsync([FromBody] TaktPcbaInspectionDetailInspectionStatusDto dto)
    {
        var result = await _service.UpdatePcbaInspectionDetailInspectionStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取PCBA检查明细表(PcbaInspectionDetail)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:import", "获取PCBA检查明细表(PcbaInspectionDetail)导入模板")]
    public async Task<IActionResult> GetPcbaInspectionDetailTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPcbaInspectionDetailTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:import", "导入PCBA检查明细表(PcbaInspectionDetail)")]
    public async Task<ActionResult<object>> ImportPcbaInspectionDetailAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPcbaInspectionDetailAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:defect:pcbainspectiondetail:export", "导出PCBA检查明细表(PcbaInspectionDetail)")]
    public async Task<IActionResult> ExportPcbaInspectionDetailAsync([FromBody] TaktPcbaInspectionDetailQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPcbaInspectionDetailAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
