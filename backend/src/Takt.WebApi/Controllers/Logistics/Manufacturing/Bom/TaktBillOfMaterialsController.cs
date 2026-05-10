// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：物料清单表控制器，提供BillOfMaterial管理的RESTful API接口
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
/// 物料清单表控制器
/// </summary>
[Route("api/[controller]", Name = "物料清单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:bom:billofmaterial", "物料清单表管理")]
public class TaktBillOfMaterialsController : TaktControllerBase
{
    private readonly ITaktBillOfMaterialService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialsController(
        ITaktBillOfMaterialService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取物料清单表(BillOfMaterial)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:list", "查询物料清单表(BillOfMaterial)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktBillOfMaterialDto>>> GetBillOfMaterialListAsync([FromQuery] TaktBillOfMaterialQueryDto queryDto)
    {
        var result = await _service.GetBillOfMaterialListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取物料清单表(BillOfMaterial)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:query", "查询物料清单表(BillOfMaterial)详情")]
    public async Task<ActionResult<TaktBillOfMaterialDto>> GetBillOfMaterialByIdAsync(long id)
    {
        var item = await _service.GetBillOfMaterialByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取物料清单表(BillOfMaterial)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:query", "查询物料清单表(BillOfMaterial)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetBillOfMaterialOptionsAsync()
    {
        var result = await _service.GetBillOfMaterialOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建物料清单表(BillOfMaterial)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:create", "创建物料清单表(BillOfMaterial)")]
    public async Task<ActionResult<TaktBillOfMaterialDto>> CreateBillOfMaterialAsync([FromBody] TaktBillOfMaterialCreateDto dto)
    {
        var result = await _service.CreateBillOfMaterialAsync(dto);
        return CreatedAtAction(nameof(GetBillOfMaterialByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新物料清单表(BillOfMaterial)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:update", "更新物料清单表(BillOfMaterial)")]
    public async Task<ActionResult<TaktBillOfMaterialDto>> UpdateBillOfMaterialAsync(long id, [FromBody] TaktBillOfMaterialUpdateDto dto)
    {
        var result = await _service.UpdateBillOfMaterialAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除物料清单表(BillOfMaterial)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:delete", "删除物料清单表(BillOfMaterial)")]
    public async Task<ActionResult> DeleteBillOfMaterialByIdAsync(long id)
    {
        await _service.DeleteBillOfMaterialByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除物料清单表(BillOfMaterial)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:delete", "批量删除物料清单表(BillOfMaterial)")]
    public async Task<ActionResult> DeleteBillOfMaterialBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteBillOfMaterialBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新物料清单表(BillOfMaterial)Bom
    /// </summary>
    [HttpPut("status-bom")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:update", "更新物料清单表(BillOfMaterial)Bom")]
    public async Task<ActionResult<TaktBillOfMaterialDto>> UpdateBillOfMaterialBomStatusAsync([FromBody] TaktBillOfMaterialBomStatusDto dto)
    {
        var result = await _service.UpdateBillOfMaterialBomStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新物料清单表(BillOfMaterial)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:update", "更新物料清单表(BillOfMaterial)排序")]
    public async Task<ActionResult<TaktBillOfMaterialDto>> UpdateBillOfMaterialSortAsync([FromBody] TaktBillOfMaterialSortDto dto)
    {
        var result = await _service.UpdateBillOfMaterialSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取物料清单表(BillOfMaterial)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:import", "获取物料清单表(BillOfMaterial)导入模板")]
    public async Task<IActionResult> GetBillOfMaterialTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetBillOfMaterialTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入物料清单表(BillOfMaterial)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:import", "导入物料清单表(BillOfMaterial)")]
    public async Task<ActionResult<object>> ImportBillOfMaterialAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportBillOfMaterialAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出物料清单表(BillOfMaterial)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterial:export", "导出物料清单表(BillOfMaterial)")]
    public async Task<IActionResult> ExportBillOfMaterialAsync([FromBody] TaktBillOfMaterialQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportBillOfMaterialAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
