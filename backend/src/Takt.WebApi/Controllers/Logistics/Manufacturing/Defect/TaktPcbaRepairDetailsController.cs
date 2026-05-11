// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairDetailsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA改修明细表控制器，提供PcbaRepairDetail管理的RESTful API接口
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
/// PCBA改修明细表控制器
/// </summary>
[Route("api/[controller]", Name = "PCBA改修明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:defect:pcbarepairdetail", "PCBA改修明细表管理")]
public class TaktPcbaRepairDetailsController : TaktControllerBase
{
    private readonly ITaktPcbaRepairDetailService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaRepairDetailsController(
        ITaktPcbaRepairDetailService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取PCBA改修明细表(PcbaRepairDetail)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:list", "查询PCBA改修明细表(PcbaRepairDetail)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPcbaRepairDetailDto>>> GetPcbaRepairDetailListAsync([FromQuery] TaktPcbaRepairDetailQueryDto queryDto)
    {
        var result = await _service.GetPcbaRepairDetailListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:query", "查询PCBA改修明细表(PcbaRepairDetail)详情")]
    public async Task<ActionResult<TaktPcbaRepairDetailDto>> GetPcbaRepairDetailByIdAsync(long id)
    {
        var item = await _service.GetPcbaRepairDetailByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取PCBA改修明细表(PcbaRepairDetail)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:query", "查询PCBA改修明细表(PcbaRepairDetail)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPcbaRepairDetailOptionsAsync()
    {
        var result = await _service.GetPcbaRepairDetailOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:create", "创建PCBA改修明细表(PcbaRepairDetail)")]
    public async Task<ActionResult<TaktPcbaRepairDetailDto>> CreatePcbaRepairDetailAsync([FromBody] TaktPcbaRepairDetailCreateDto dto)
    {
        var result = await _service.CreatePcbaRepairDetailAsync(dto);
        return CreatedAtAction(nameof(GetPcbaRepairDetailByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:update", "更新PCBA改修明细表(PcbaRepairDetail)")]
    public async Task<ActionResult<TaktPcbaRepairDetailDto>> UpdatePcbaRepairDetailAsync(long id, [FromBody] TaktPcbaRepairDetailUpdateDto dto)
    {
        var result = await _service.UpdatePcbaRepairDetailAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:delete", "删除PCBA改修明细表(PcbaRepairDetail)")]
    public async Task<ActionResult> DeletePcbaRepairDetailByIdAsync(long id)
    {
        await _service.DeletePcbaRepairDetailByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:delete", "批量删除PCBA改修明细表(PcbaRepairDetail)")]
    public async Task<ActionResult> DeletePcbaRepairDetailBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePcbaRepairDetailBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取PCBA改修明细表(PcbaRepairDetail)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:import", "获取PCBA改修明细表(PcbaRepairDetail)导入模板")]
    public async Task<IActionResult> GetPcbaRepairDetailTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPcbaRepairDetailTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:import", "导入PCBA改修明细表(PcbaRepairDetail)")]
    public async Task<ActionResult<object>> ImportPcbaRepairDetailAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPcbaRepairDetailAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:defect:pcbarepairdetail:export", "导出PCBA改修明细表(PcbaRepairDetail)")]
    public async Task<IActionResult> ExportPcbaRepairDetailAsync([FromBody] TaktPcbaRepairDetailQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPcbaRepairDetailAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
