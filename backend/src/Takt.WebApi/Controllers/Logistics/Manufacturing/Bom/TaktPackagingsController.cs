// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Bom
// 文件名称：TaktPackagingsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：物料包装信息表控制器，提供Packaging管理的RESTful API接口
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
/// 物料包装信息表控制器
/// </summary>
[Route("api/[controller]", Name = "物料包装信息表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:bom:packaging", "物料包装信息表管理")]
public class TaktPackagingsController : TaktControllerBase
{
    private readonly ITaktPackagingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingsController(
        ITaktPackagingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取物料包装信息表(Packaging)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:bom:packaging:list", "查询物料包装信息表(Packaging)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPackagingDto>>> GetPackagingListAsync([FromQuery] TaktPackagingQueryDto queryDto)
    {
        var result = await _service.GetPackagingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取物料包装信息表(Packaging)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:bom:packaging:query", "查询物料包装信息表(Packaging)详情")]
    public async Task<ActionResult<TaktPackagingDto>> GetPackagingByIdAsync(long id)
    {
        var item = await _service.GetPackagingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取物料包装信息表(Packaging)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:bom:packaging:query", "查询物料包装信息表(Packaging)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPackagingOptionsAsync()
    {
        var result = await _service.GetPackagingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建物料包装信息表(Packaging)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:bom:packaging:create", "创建物料包装信息表(Packaging)")]
    public async Task<ActionResult<TaktPackagingDto>> CreatePackagingAsync([FromBody] TaktPackagingCreateDto dto)
    {
        var result = await _service.CreatePackagingAsync(dto);
        return CreatedAtAction(nameof(GetPackagingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新物料包装信息表(Packaging)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:bom:packaging:update", "更新物料包装信息表(Packaging)")]
    public async Task<ActionResult<TaktPackagingDto>> UpdatePackagingAsync(long id, [FromBody] TaktPackagingUpdateDto dto)
    {
        var result = await _service.UpdatePackagingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除物料包装信息表(Packaging)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:bom:packaging:delete", "删除物料包装信息表(Packaging)")]
    public async Task<ActionResult> DeletePackagingByIdAsync(long id)
    {
        await _service.DeletePackagingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除物料包装信息表(Packaging)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:bom:packaging:delete", "批量删除物料包装信息表(Packaging)")]
    public async Task<ActionResult> DeletePackagingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePackagingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新物料包装信息表(Packaging)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:manufacturing:bom:packaging:update", "更新物料包装信息表(Packaging)排序")]
    public async Task<ActionResult<TaktPackagingDto>> UpdatePackagingSortAsync([FromBody] TaktPackagingSortDto dto)
    {
        var result = await _service.UpdatePackagingSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取物料包装信息表(Packaging)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:bom:packaging:import", "获取物料包装信息表(Packaging)导入模板")]
    public async Task<IActionResult> GetPackagingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPackagingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入物料包装信息表(Packaging)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:bom:packaging:import", "导入物料包装信息表(Packaging)")]
    public async Task<ActionResult<object>> ImportPackagingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPackagingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出物料包装信息表(Packaging)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:bom:packaging:export", "导出物料包装信息表(Packaging)")]
    public async Task<IActionResult> ExportPackagingAsync([FromBody] TaktPackagingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPackagingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
