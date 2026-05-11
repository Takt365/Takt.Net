// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：物料清单明细表控制器，提供BillOfMaterialItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Application.Services.Logistics.Manufacturing.Bom;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料清单明细表控制器
/// </summary>
[Route("api/[controller]", Name = "物料清单明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:bom:billofmaterialitem", "物料清单明细表管理")]
public class TaktBillOfMaterialItemsController : TaktControllerBase
{
    private readonly ITaktBillOfMaterialItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemsController(
        ITaktBillOfMaterialItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取物料清单明细表(BillOfMaterialItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:list", "查询物料清单明细表(BillOfMaterialItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktBillOfMaterialItemDto>>> GetBillOfMaterialItemListAsync([FromQuery] TaktBillOfMaterialItemQueryDto queryDto)
    {
        var result = await _service.GetBillOfMaterialItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取物料清单明细表(BillOfMaterialItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:query", "查询物料清单明细表(BillOfMaterialItem)详情")]
    public async Task<ActionResult<TaktBillOfMaterialItemDto>> GetBillOfMaterialItemByIdAsync(long id)
    {
        var item = await _service.GetBillOfMaterialItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取物料清单明细表(BillOfMaterialItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:query", "查询物料清单明细表(BillOfMaterialItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetBillOfMaterialItemOptionsAsync()
    {
        var result = await _service.GetBillOfMaterialItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建物料清单明细表(BillOfMaterialItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:create", "创建物料清单明细表(BillOfMaterialItem)")]
    public async Task<ActionResult<TaktBillOfMaterialItemDto>> CreateBillOfMaterialItemAsync([FromBody] TaktBillOfMaterialItemCreateDto dto)
    {
        var result = await _service.CreateBillOfMaterialItemAsync(dto);
        return CreatedAtAction(nameof(GetBillOfMaterialItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新物料清单明细表(BillOfMaterialItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:update", "更新物料清单明细表(BillOfMaterialItem)")]
    public async Task<ActionResult<TaktBillOfMaterialItemDto>> UpdateBillOfMaterialItemAsync(long id, [FromBody] TaktBillOfMaterialItemUpdateDto dto)
    {
        var result = await _service.UpdateBillOfMaterialItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除物料清单明细表(BillOfMaterialItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:delete", "删除物料清单明细表(BillOfMaterialItem)")]
    public async Task<ActionResult> DeleteBillOfMaterialItemByIdAsync(long id)
    {
        await _service.DeleteBillOfMaterialItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除物料清单明细表(BillOfMaterialItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:delete", "批量删除物料清单明细表(BillOfMaterialItem)")]
    public async Task<ActionResult> DeleteBillOfMaterialItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteBillOfMaterialItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取物料清单明细表(BillOfMaterialItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:import", "获取物料清单明细表(BillOfMaterialItem)导入模板")]
    public async Task<IActionResult> GetBillOfMaterialItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetBillOfMaterialItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入物料清单明细表(BillOfMaterialItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:import", "导入物料清单明细表(BillOfMaterialItem)")]
    public async Task<ActionResult<object>> ImportBillOfMaterialItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportBillOfMaterialItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出物料清单明细表(BillOfMaterialItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialitem:export", "导出物料清单明细表(BillOfMaterialItem)")]
    public async Task<IActionResult> ExportBillOfMaterialItemAsync([FromBody] TaktBillOfMaterialItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportBillOfMaterialItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
