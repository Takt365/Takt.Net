// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA改修日报表控制器，提供PcbaRepair管理的RESTful API接口
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
/// PCBA改修日报表控制器
/// </summary>
[Route("api/[controller]", Name = "PCBA改修日报表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:defect:pcbarepair", "PCBA改修日报表管理")]
public class TaktPcbaRepairsController : TaktControllerBase
{
    private readonly ITaktPcbaRepairService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaRepairsController(
        ITaktPcbaRepairService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取PCBA改修日报表(PcbaRepair)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:list", "查询PCBA改修日报表(PcbaRepair)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPcbaRepairDto>>> GetPcbaRepairListAsync([FromQuery] TaktPcbaRepairQueryDto queryDto)
    {
        var result = await _service.GetPcbaRepairListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取PCBA改修日报表(PcbaRepair)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:query", "查询PCBA改修日报表(PcbaRepair)详情")]
    public async Task<ActionResult<TaktPcbaRepairDto>> GetPcbaRepairByIdAsync(long id)
    {
        var item = await _service.GetPcbaRepairByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取PCBA改修日报表(PcbaRepair)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:query", "查询PCBA改修日报表(PcbaRepair)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPcbaRepairOptionsAsync()
    {
        var result = await _service.GetPcbaRepairOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建PCBA改修日报表(PcbaRepair)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:create", "创建PCBA改修日报表(PcbaRepair)")]
    public async Task<ActionResult<TaktPcbaRepairDto>> CreatePcbaRepairAsync([FromBody] TaktPcbaRepairCreateDto dto)
    {
        var result = await _service.CreatePcbaRepairAsync(dto);
        return CreatedAtAction(nameof(GetPcbaRepairByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新PCBA改修日报表(PcbaRepair)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:update", "更新PCBA改修日报表(PcbaRepair)")]
    public async Task<ActionResult<TaktPcbaRepairDto>> UpdatePcbaRepairAsync(long id, [FromBody] TaktPcbaRepairUpdateDto dto)
    {
        var result = await _service.UpdatePcbaRepairAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除PCBA改修日报表(PcbaRepair)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:delete", "删除PCBA改修日报表(PcbaRepair)")]
    public async Task<ActionResult> DeletePcbaRepairByIdAsync(long id)
    {
        await _service.DeletePcbaRepairByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除PCBA改修日报表(PcbaRepair)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:delete", "批量删除PCBA改修日报表(PcbaRepair)")]
    public async Task<ActionResult> DeletePcbaRepairBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePcbaRepairBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新PCBA改修日报表(PcbaRepair)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:update", "更新PCBA改修日报表(PcbaRepair)状态")]
    public async Task<ActionResult<TaktPcbaRepairDto>> UpdatePcbaRepairStatusAsync([FromBody] TaktPcbaRepairStatusDto dto)
    {
        var result = await _service.UpdatePcbaRepairStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取PCBA改修日报表(PcbaRepair)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:import", "获取PCBA改修日报表(PcbaRepair)导入模板")]
    public async Task<IActionResult> GetPcbaRepairTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPcbaRepairTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入PCBA改修日报表(PcbaRepair)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:import", "导入PCBA改修日报表(PcbaRepair)")]
    public async Task<ActionResult<object>> ImportPcbaRepairAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPcbaRepairAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出PCBA改修日报表(PcbaRepair)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepair:export", "导出PCBA改修日报表(PcbaRepair)")]
    public async Task<IActionResult> ExportPcbaRepairAsync([FromBody] TaktPcbaRepairQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPcbaRepairAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
