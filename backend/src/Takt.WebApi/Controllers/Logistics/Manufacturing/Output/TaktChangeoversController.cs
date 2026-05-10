// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktChangeoversController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：切换记录表控制器，提供Changeover管理的RESTful API接口
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
/// 切换记录表控制器
/// </summary>
[Route("api/[controller]", Name = "切换记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:changeover", "切换记录表管理")]
public class TaktChangeoversController : TaktControllerBase
{
    private readonly ITaktChangeoverService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktChangeoversController(
        ITaktChangeoverService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取切换记录表(Changeover)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:changeover:list", "查询切换记录表(Changeover)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktChangeoverDto>>> GetChangeoverListAsync([FromQuery] TaktChangeoverQueryDto queryDto)
    {
        var result = await _service.GetChangeoverListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取切换记录表(Changeover)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:changeover:query", "查询切换记录表(Changeover)详情")]
    public async Task<ActionResult<TaktChangeoverDto>> GetChangeoverByIdAsync(long id)
    {
        var item = await _service.GetChangeoverByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取切换记录表(Changeover)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:changeover:query", "查询切换记录表(Changeover)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetChangeoverOptionsAsync()
    {
        var result = await _service.GetChangeoverOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建切换记录表(Changeover)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:changeover:create", "创建切换记录表(Changeover)")]
    public async Task<ActionResult<TaktChangeoverDto>> CreateChangeoverAsync([FromBody] TaktChangeoverCreateDto dto)
    {
        var result = await _service.CreateChangeoverAsync(dto);
        return CreatedAtAction(nameof(GetChangeoverByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新切换记录表(Changeover)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:changeover:update", "更新切换记录表(Changeover)")]
    public async Task<ActionResult<TaktChangeoverDto>> UpdateChangeoverAsync(long id, [FromBody] TaktChangeoverUpdateDto dto)
    {
        var result = await _service.UpdateChangeoverAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除切换记录表(Changeover)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:changeover:delete", "删除切换记录表(Changeover)")]
    public async Task<ActionResult> DeleteChangeoverByIdAsync(long id)
    {
        await _service.DeleteChangeoverByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除切换记录表(Changeover)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:changeover:delete", "批量删除切换记录表(Changeover)")]
    public async Task<ActionResult> DeleteChangeoverBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteChangeoverBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取切换记录表(Changeover)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:changeover:import", "获取切换记录表(Changeover)导入模板")]
    public async Task<IActionResult> GetChangeoverTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetChangeoverTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入切换记录表(Changeover)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:changeover:import", "导入切换记录表(Changeover)")]
    public async Task<ActionResult<object>> ImportChangeoverAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportChangeoverAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出切换记录表(Changeover)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:changeover:export", "导出切换记录表(Changeover)")]
    public async Task<IActionResult> ExportChangeoverAsync([FromBody] TaktChangeoverQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportChangeoverAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
