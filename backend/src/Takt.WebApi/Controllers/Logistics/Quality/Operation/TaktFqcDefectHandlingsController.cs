// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktFqcDefectHandlingsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：出货检验不良处理记录表控制器，提供FqcDefectHandling管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Operation;

/// <summary>
/// 出货检验不良处理记录表控制器
/// </summary>
[Route("api/[controller]", Name = "出货检验不良处理记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:fqcdefecthandling", "出货检验不良处理记录表管理")]
public class TaktFqcDefectHandlingsController : TaktControllerBase
{
    private readonly ITaktFqcDefectHandlingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingsController(
        ITaktFqcDefectHandlingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取出货检验不良处理记录表(FqcDefectHandling)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:list", "查询出货检验不良处理记录表(FqcDefectHandling)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFqcDefectHandlingDto>>> GetFqcDefectHandlingListAsync([FromQuery] TaktFqcDefectHandlingQueryDto queryDto)
    {
        var result = await _service.GetFqcDefectHandlingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取出货检验不良处理记录表(FqcDefectHandling)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:query", "查询出货检验不良处理记录表(FqcDefectHandling)详情")]
    public async Task<ActionResult<TaktFqcDefectHandlingDto>> GetFqcDefectHandlingByIdAsync(long id)
    {
        var item = await _service.GetFqcDefectHandlingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取出货检验不良处理记录表(FqcDefectHandling)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:query", "查询出货检验不良处理记录表(FqcDefectHandling)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFqcDefectHandlingOptionsAsync()
    {
        var result = await _service.GetFqcDefectHandlingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建出货检验不良处理记录表(FqcDefectHandling)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:create", "创建出货检验不良处理记录表(FqcDefectHandling)")]
    public async Task<ActionResult<TaktFqcDefectHandlingDto>> CreateFqcDefectHandlingAsync([FromBody] TaktFqcDefectHandlingCreateDto dto)
    {
        var result = await _service.CreateFqcDefectHandlingAsync(dto);
        return CreatedAtAction(nameof(GetFqcDefectHandlingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新出货检验不良处理记录表(FqcDefectHandling)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:update", "更新出货检验不良处理记录表(FqcDefectHandling)")]
    public async Task<ActionResult<TaktFqcDefectHandlingDto>> UpdateFqcDefectHandlingAsync(long id, [FromBody] TaktFqcDefectHandlingUpdateDto dto)
    {
        var result = await _service.UpdateFqcDefectHandlingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除出货检验不良处理记录表(FqcDefectHandling)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:delete", "删除出货检验不良处理记录表(FqcDefectHandling)")]
    public async Task<ActionResult> DeleteFqcDefectHandlingByIdAsync(long id)
    {
        await _service.DeleteFqcDefectHandlingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除出货检验不良处理记录表(FqcDefectHandling)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:delete", "批量删除出货检验不良处理记录表(FqcDefectHandling)")]
    public async Task<ActionResult> DeleteFqcDefectHandlingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFqcDefectHandlingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新出货检验不良处理记录表(FqcDefectHandling)Handling
    /// </summary>
    [HttpPut("status-handling")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:update", "更新出货检验不良处理记录表(FqcDefectHandling)Handling")]
    public async Task<ActionResult<TaktFqcDefectHandlingDto>> UpdateFqcDefectHandlingHandlingStatusAsync([FromBody] TaktFqcDefectHandlingHandlingStatusDto dto)
    {
        var result = await _service.UpdateFqcDefectHandlingHandlingStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取出货检验不良处理记录表(FqcDefectHandling)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:import", "获取出货检验不良处理记录表(FqcDefectHandling)导入模板")]
    public async Task<IActionResult> GetFqcDefectHandlingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFqcDefectHandlingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入出货检验不良处理记录表(FqcDefectHandling)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:import", "导入出货检验不良处理记录表(FqcDefectHandling)")]
    public async Task<ActionResult<object>> ImportFqcDefectHandlingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFqcDefectHandlingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出出货检验不良处理记录表(FqcDefectHandling)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:fqcdefecthandling:export", "导出出货检验不良处理记录表(FqcDefectHandling)")]
    public async Task<IActionResult> ExportFqcDefectHandlingAsync([FromBody] TaktFqcDefectHandlingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFqcDefectHandlingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
