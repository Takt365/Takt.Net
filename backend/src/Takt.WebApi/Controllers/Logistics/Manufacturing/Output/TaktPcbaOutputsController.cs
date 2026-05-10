// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA日报表控制器，提供PcbaOutput管理的RESTful API接口
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
/// PCBA日报表控制器
/// </summary>
[Route("api/[controller]", Name = "PCBA日报表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:pcbaoutput", "PCBA日报表管理")]
public class TaktPcbaOutputsController : TaktControllerBase
{
    private readonly ITaktPcbaOutputService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputsController(
        ITaktPcbaOutputService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取PCBA日报表(PcbaOutput)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:list", "查询PCBA日报表(PcbaOutput)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPcbaOutputDto>>> GetPcbaOutputListAsync([FromQuery] TaktPcbaOutputQueryDto queryDto)
    {
        var result = await _service.GetPcbaOutputListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取PCBA日报表(PcbaOutput)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:query", "查询PCBA日报表(PcbaOutput)详情")]
    public async Task<ActionResult<TaktPcbaOutputDto>> GetPcbaOutputByIdAsync(long id)
    {
        var item = await _service.GetPcbaOutputByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取PCBA日报表(PcbaOutput)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:query", "查询PCBA日报表(PcbaOutput)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPcbaOutputOptionsAsync()
    {
        var result = await _service.GetPcbaOutputOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建PCBA日报表(PcbaOutput)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:create", "创建PCBA日报表(PcbaOutput)")]
    public async Task<ActionResult<TaktPcbaOutputDto>> CreatePcbaOutputAsync([FromBody] TaktPcbaOutputCreateDto dto)
    {
        var result = await _service.CreatePcbaOutputAsync(dto);
        return CreatedAtAction(nameof(GetPcbaOutputByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新PCBA日报表(PcbaOutput)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:update", "更新PCBA日报表(PcbaOutput)")]
    public async Task<ActionResult<TaktPcbaOutputDto>> UpdatePcbaOutputAsync(long id, [FromBody] TaktPcbaOutputUpdateDto dto)
    {
        var result = await _service.UpdatePcbaOutputAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除PCBA日报表(PcbaOutput)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:delete", "删除PCBA日报表(PcbaOutput)")]
    public async Task<ActionResult> DeletePcbaOutputByIdAsync(long id)
    {
        await _service.DeletePcbaOutputByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除PCBA日报表(PcbaOutput)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:delete", "批量删除PCBA日报表(PcbaOutput)")]
    public async Task<ActionResult> DeletePcbaOutputBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePcbaOutputBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取PCBA日报表(PcbaOutput)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:import", "获取PCBA日报表(PcbaOutput)导入模板")]
    public async Task<IActionResult> GetPcbaOutputTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPcbaOutputTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入PCBA日报表(PcbaOutput)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:import", "导入PCBA日报表(PcbaOutput)")]
    public async Task<ActionResult<object>> ImportPcbaOutputAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPcbaOutputAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出PCBA日报表(PcbaOutput)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:pcbaoutput:export", "导出PCBA日报表(PcbaOutput)")]
    public async Task<IActionResult> ExportPcbaOutputAsync([FromBody] TaktPcbaOutputQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPcbaOutputAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
