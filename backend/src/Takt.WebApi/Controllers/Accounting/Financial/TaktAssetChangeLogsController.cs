// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktAssetChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：资产变更记录表控制器，提供AssetChangeLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 资产变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "资产变更记录表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:financial:assetchangelog", "资产变更记录表管理")]
public class TaktAssetChangeLogsController : TaktControllerBase
{
    private readonly ITaktAssetChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetChangeLogsController(
        ITaktAssetChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取资产变更记录表(AssetChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:assetchangelog:list", "查询资产变更记录表(AssetChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAssetChangeLogDto>>> GetAssetChangeLogListAsync([FromQuery] TaktAssetChangeLogQueryDto queryDto)
    {
        var result = await _service.GetAssetChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取资产变更记录表(AssetChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:assetchangelog:query", "查询资产变更记录表(AssetChangeLog)详情")]
    public async Task<ActionResult<TaktAssetChangeLogDto>> GetAssetChangeLogByIdAsync(long id)
    {
        var item = await _service.GetAssetChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取资产变更记录表(AssetChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:assetchangelog:query", "查询资产变更记录表(AssetChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAssetChangeLogOptionsAsync()
    {
        var result = await _service.GetAssetChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建资产变更记录表(AssetChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:assetchangelog:create", "创建资产变更记录表(AssetChangeLog)")]
    public async Task<ActionResult<TaktAssetChangeLogDto>> CreateAssetChangeLogAsync([FromBody] TaktAssetChangeLogCreateDto dto)
    {
        var result = await _service.CreateAssetChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetAssetChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新资产变更记录表(AssetChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:assetchangelog:update", "更新资产变更记录表(AssetChangeLog)")]
    public async Task<ActionResult<TaktAssetChangeLogDto>> UpdateAssetChangeLogAsync(long id, [FromBody] TaktAssetChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateAssetChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除资产变更记录表(AssetChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:assetchangelog:delete", "删除资产变更记录表(AssetChangeLog)")]
    public async Task<ActionResult> DeleteAssetChangeLogByIdAsync(long id)
    {
        await _service.DeleteAssetChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除资产变更记录表(AssetChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:financial:assetchangelog:delete", "批量删除资产变更记录表(AssetChangeLog)")]
    public async Task<ActionResult> DeleteAssetChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAssetChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取资产变更记录表(AssetChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:assetchangelog:import", "获取资产变更记录表(AssetChangeLog)导入模板")]
    public async Task<IActionResult> GetAssetChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAssetChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入资产变更记录表(AssetChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:assetchangelog:import", "导入资产变更记录表(AssetChangeLog)")]
    public async Task<ActionResult<object>> ImportAssetChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAssetChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出资产变更记录表(AssetChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:assetchangelog:export", "导出资产变更记录表(AssetChangeLog)")]
    public async Task<IActionResult> ExportAssetChangeLogAsync([FromBody] TaktAssetChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAssetChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
