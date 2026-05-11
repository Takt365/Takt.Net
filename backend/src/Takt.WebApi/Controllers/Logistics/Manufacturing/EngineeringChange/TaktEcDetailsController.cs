// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDetailsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：设变明细表控制器，提供EcDetail管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变明细表控制器
/// </summary>
[Route("api/[controller]", Name = "设变明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:engineeringchange:ecdetail", "设变明细表管理")]
public class TaktEcDetailsController : TaktControllerBase
{
    private readonly ITaktEcDetailService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDetailsController(
        ITaktEcDetailService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取设变明细表(EcDetail)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:list", "查询设变明细表(EcDetail)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEcDetailDto>>> GetEcDetailListAsync([FromQuery] TaktEcDetailQueryDto queryDto)
    {
        var result = await _service.GetEcDetailListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取设变明细表(EcDetail)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:query", "查询设变明细表(EcDetail)详情")]
    public async Task<ActionResult<TaktEcDetailDto>> GetEcDetailByIdAsync(long id)
    {
        var item = await _service.GetEcDetailByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取设变明细表(EcDetail)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:query", "查询设变明细表(EcDetail)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEcDetailOptionsAsync()
    {
        var result = await _service.GetEcDetailOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建设变明细表(EcDetail)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:create", "创建设变明细表(EcDetail)")]
    public async Task<ActionResult<TaktEcDetailDto>> CreateEcDetailAsync([FromBody] TaktEcDetailCreateDto dto)
    {
        var result = await _service.CreateEcDetailAsync(dto);
        return CreatedAtAction(nameof(GetEcDetailByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新设变明细表(EcDetail)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:update", "更新设变明细表(EcDetail)")]
    public async Task<ActionResult<TaktEcDetailDto>> UpdateEcDetailAsync(long id, [FromBody] TaktEcDetailUpdateDto dto)
    {
        var result = await _service.UpdateEcDetailAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除设变明细表(EcDetail)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:delete", "删除设变明细表(EcDetail)")]
    public async Task<ActionResult> DeleteEcDetailByIdAsync(long id)
    {
        await _service.DeleteEcDetailByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除设变明细表(EcDetail)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:delete", "批量删除设变明细表(EcDetail)")]
    public async Task<ActionResult> DeleteEcDetailBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEcDetailBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取设变明细表(EcDetail)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:import", "获取设变明细表(EcDetail)导入模板")]
    public async Task<IActionResult> GetEcDetailTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEcDetailTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入设变明细表(EcDetail)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:import", "导入设变明细表(EcDetail)")]
    public async Task<ActionResult<object>> ImportEcDetailAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEcDetailAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出设变明细表(EcDetail)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecdetail:export", "导出设变明细表(EcDetail)")]
    public async Task<IActionResult> ExportEcDetailAsync([FromBody] TaktEcDetailQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEcDetailAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
