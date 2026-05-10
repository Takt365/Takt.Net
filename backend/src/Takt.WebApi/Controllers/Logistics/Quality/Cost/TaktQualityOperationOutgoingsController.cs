// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationOutgoingsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务出货检验费用明细表控制器，提供QualityOperationOutgoing管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Application.Services.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Cost;

/// <summary>
/// 品质业务出货检验费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务出货检验费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperationoutgoing", "品质业务出货检验费用明细表管理")]
public class TaktQualityOperationOutgoingsController : TaktControllerBase
{
    private readonly ITaktQualityOperationOutgoingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationOutgoingsController(
        ITaktQualityOperationOutgoingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务出货检验费用明细表(QualityOperationOutgoing)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:list", "查询品质业务出货检验费用明细表(QualityOperationOutgoing)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationOutgoingDto>>> GetQualityOperationOutgoingListAsync([FromQuery] TaktQualityOperationOutgoingQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationOutgoingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务出货检验费用明细表(QualityOperationOutgoing)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:query", "查询品质业务出货检验费用明细表(QualityOperationOutgoing)详情")]
    public async Task<ActionResult<TaktQualityOperationOutgoingDto>> GetQualityOperationOutgoingByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationOutgoingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务出货检验费用明细表(QualityOperationOutgoing)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:query", "查询品质业务出货检验费用明细表(QualityOperationOutgoing)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationOutgoingOptionsAsync()
    {
        var result = await _service.GetQualityOperationOutgoingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务出货检验费用明细表(QualityOperationOutgoing)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:create", "创建品质业务出货检验费用明细表(QualityOperationOutgoing)")]
    public async Task<ActionResult<TaktQualityOperationOutgoingDto>> CreateQualityOperationOutgoingAsync([FromBody] TaktQualityOperationOutgoingCreateDto dto)
    {
        var result = await _service.CreateQualityOperationOutgoingAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationOutgoingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务出货检验费用明细表(QualityOperationOutgoing)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:update", "更新品质业务出货检验费用明细表(QualityOperationOutgoing)")]
    public async Task<ActionResult<TaktQualityOperationOutgoingDto>> UpdateQualityOperationOutgoingAsync(long id, [FromBody] TaktQualityOperationOutgoingUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationOutgoingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务出货检验费用明细表(QualityOperationOutgoing)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:delete", "删除品质业务出货检验费用明细表(QualityOperationOutgoing)")]
    public async Task<ActionResult> DeleteQualityOperationOutgoingByIdAsync(long id)
    {
        await _service.DeleteQualityOperationOutgoingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务出货检验费用明细表(QualityOperationOutgoing)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:delete", "批量删除品质业务出货检验费用明细表(QualityOperationOutgoing)")]
    public async Task<ActionResult> DeleteQualityOperationOutgoingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationOutgoingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务出货检验费用明细表(QualityOperationOutgoing)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:import", "获取品质业务出货检验费用明细表(QualityOperationOutgoing)导入模板")]
    public async Task<IActionResult> GetQualityOperationOutgoingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationOutgoingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务出货检验费用明细表(QualityOperationOutgoing)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:import", "导入品质业务出货检验费用明细表(QualityOperationOutgoing)")]
    public async Task<ActionResult<object>> ImportQualityOperationOutgoingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationOutgoingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务出货检验费用明细表(QualityOperationOutgoing)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperationoutgoing:export", "导出品质业务出货检验费用明细表(QualityOperationOutgoing)")]
    public async Task<IActionResult> ExportQualityOperationOutgoingAsync([FromBody] TaktQualityOperationOutgoingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationOutgoingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
