// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktAssetsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：固定资产表控制器，提供Asset管理的RESTful API接口
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
/// 固定资产表控制器
/// </summary>
[Route("api/[controller]", Name = "固定资产表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:financial:asset", "固定资产表管理")]
public class TaktAssetsController : TaktControllerBase
{
    private readonly ITaktAssetService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetsController(
        ITaktAssetService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取固定资产表(Asset)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:asset:list", "查询固定资产表(Asset)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAssetDto>>> GetAssetListAsync([FromQuery] TaktAssetQueryDto queryDto)
    {
        var result = await _service.GetAssetListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取固定资产表(Asset)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:asset:query", "查询固定资产表(Asset)详情")]
    public async Task<ActionResult<TaktAssetDto>> GetAssetByIdAsync(long id)
    {
        var item = await _service.GetAssetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取固定资产表(Asset)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:asset:query", "查询固定资产表(Asset)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAssetOptionsAsync()
    {
        var result = await _service.GetAssetOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建固定资产表(Asset)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:asset:create", "创建固定资产表(Asset)")]
    public async Task<ActionResult<TaktAssetDto>> CreateAssetAsync([FromBody] TaktAssetCreateDto dto)
    {
        var result = await _service.CreateAssetAsync(dto);
        return CreatedAtAction(nameof(GetAssetByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新固定资产表(Asset)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产表(Asset)")]
    public async Task<ActionResult<TaktAssetDto>> UpdateAssetAsync(long id, [FromBody] TaktAssetUpdateDto dto)
    {
        var result = await _service.UpdateAssetAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除固定资产表(Asset)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:asset:delete", "删除固定资产表(Asset)")]
    public async Task<ActionResult> DeleteAssetByIdAsync(long id)
    {
        await _service.DeleteAssetByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除固定资产表(Asset)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:financial:asset:delete", "批量删除固定资产表(Asset)")]
    public async Task<ActionResult> DeleteAssetBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAssetBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新固定资产表(Asset)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产表(Asset)状态")]
    public async Task<ActionResult<TaktAssetDto>> UpdateAssetStatusAsync([FromBody] TaktAssetStatusDto dto)
    {
        var result = await _service.UpdateAssetStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取固定资产表(Asset)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:asset:import", "获取固定资产表(Asset)导入模板")]
    public async Task<IActionResult> GetAssetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAssetTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入固定资产表(Asset)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:asset:import", "导入固定资产表(Asset)")]
    public async Task<ActionResult<object>> ImportAssetAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAssetAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出固定资产表(Asset)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:asset:export", "导出固定资产表(Asset)")]
    public async Task<IActionResult> ExportAssetAsync([FromBody] TaktAssetQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAssetAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
