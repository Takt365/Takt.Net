// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputDetailsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA日报明细表控制器，提供PcbaOutputDetail管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Application.Services.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Output;

/// <summary>
/// PCBA日报明细表控制器
/// </summary>
[Route("api/[controller]", Name = "PCBA日报明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:pcbaoutputdetail", "PCBA日报明细表管理")]
public class TaktPcbaOutputDetailsController : TaktControllerBase
{
    private readonly ITaktPcbaOutputDetailService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailsController(
        ITaktPcbaOutputDetailService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取PCBA日报明细表(PcbaOutputDetail)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:list", "查询PCBA日报明细表(PcbaOutputDetail)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPcbaOutputDetailDto>>> GetPcbaOutputDetailListAsync([FromQuery] TaktPcbaOutputDetailQueryDto queryDto)
    {
        var result = await _service.GetPcbaOutputDetailListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:query", "查询PCBA日报明细表(PcbaOutputDetail)详情")]
    public async Task<ActionResult<TaktPcbaOutputDetailDto>> GetPcbaOutputDetailByIdAsync(long id)
    {
        var item = await _service.GetPcbaOutputDetailByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取PCBA日报明细表(PcbaOutputDetail)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:query", "查询PCBA日报明细表(PcbaOutputDetail)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPcbaOutputDetailOptionsAsync()
    {
        var result = await _service.GetPcbaOutputDetailOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:create", "创建PCBA日报明细表(PcbaOutputDetail)")]
    public async Task<ActionResult<TaktPcbaOutputDetailDto>> CreatePcbaOutputDetailAsync([FromBody] TaktPcbaOutputDetailCreateDto dto)
    {
        var result = await _service.CreatePcbaOutputDetailAsync(dto);
        return CreatedAtAction(nameof(GetPcbaOutputDetailByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:update", "更新PCBA日报明细表(PcbaOutputDetail)")]
    public async Task<ActionResult<TaktPcbaOutputDetailDto>> UpdatePcbaOutputDetailAsync(long id, [FromBody] TaktPcbaOutputDetailUpdateDto dto)
    {
        var result = await _service.UpdatePcbaOutputDetailAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:delete", "删除PCBA日报明细表(PcbaOutputDetail)")]
    public async Task<ActionResult> DeletePcbaOutputDetailByIdAsync(long id)
    {
        await _service.DeletePcbaOutputDetailByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:delete", "批量删除PCBA日报明细表(PcbaOutputDetail)")]
    public async Task<ActionResult> DeletePcbaOutputDetailBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePcbaOutputDetailBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新PCBA日报明细表(PcbaOutputDetail)Completed
    /// </summary>
    [HttpPut("status-completed")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:update", "更新PCBA日报明细表(PcbaOutputDetail)Completed")]
    public async Task<ActionResult<TaktPcbaOutputDetailDto>> UpdatePcbaOutputDetailCompletedStatusAsync([FromBody] TaktPcbaOutputDetailCompletedStatusDto dto)
    {
        var result = await _service.UpdatePcbaOutputDetailCompletedStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取PCBA日报明细表(PcbaOutputDetail)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:import", "获取PCBA日报明细表(PcbaOutputDetail)导入模板")]
    public async Task<IActionResult> GetPcbaOutputDetailTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPcbaOutputDetailTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:import", "导入PCBA日报明细表(PcbaOutputDetail)")]
    public async Task<ActionResult<object>> ImportPcbaOutputDetailAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPcbaOutputDetailAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutputdetail:export", "导出PCBA日报明细表(PcbaOutputDetail)")]
    public async Task<IActionResult> ExportPcbaOutputDetailAsync([FromBody] TaktPcbaOutputDetailQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPcbaOutputDetailAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
