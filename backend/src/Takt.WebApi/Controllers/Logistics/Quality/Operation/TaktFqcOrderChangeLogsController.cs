// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderChangeLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：出货检验单变更日志表控制器，提供FqcOrderChangeLog管理的RESTful API接口
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
/// 出货检验单变更日志表控制器
/// </summary>
[Route("api/[controller]", Name = "出货检验单变更日志表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:fqcorderchangelog", "出货检验单变更日志表管理")]
public class TaktFqcOrderChangeLogsController : TaktControllerBase
{
    private readonly ITaktFqcOrderChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderChangeLogsController(
        ITaktFqcOrderChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取出货检验单变更日志表(FqcOrderChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:list", "查询出货检验单变更日志表(FqcOrderChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFqcOrderChangeLogDto>>> GetFqcOrderChangeLogListAsync([FromQuery] TaktFqcOrderChangeLogQueryDto queryDto)
    {
        var result = await _service.GetFqcOrderChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:query", "查询出货检验单变更日志表(FqcOrderChangeLog)详情")]
    public async Task<ActionResult<TaktFqcOrderChangeLogDto>> GetFqcOrderChangeLogByIdAsync(long id)
    {
        var item = await _service.GetFqcOrderChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取出货检验单变更日志表(FqcOrderChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:query", "查询出货检验单变更日志表(FqcOrderChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFqcOrderChangeLogOptionsAsync()
    {
        var result = await _service.GetFqcOrderChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:create", "创建出货检验单变更日志表(FqcOrderChangeLog)")]
    public async Task<ActionResult<TaktFqcOrderChangeLogDto>> CreateFqcOrderChangeLogAsync([FromBody] TaktFqcOrderChangeLogCreateDto dto)
    {
        var result = await _service.CreateFqcOrderChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetFqcOrderChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:update", "更新出货检验单变更日志表(FqcOrderChangeLog)")]
    public async Task<ActionResult<TaktFqcOrderChangeLogDto>> UpdateFqcOrderChangeLogAsync(long id, [FromBody] TaktFqcOrderChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateFqcOrderChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:delete", "删除出货检验单变更日志表(FqcOrderChangeLog)")]
    public async Task<ActionResult> DeleteFqcOrderChangeLogByIdAsync(long id)
    {
        await _service.DeleteFqcOrderChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:delete", "批量删除出货检验单变更日志表(FqcOrderChangeLog)")]
    public async Task<ActionResult> DeleteFqcOrderChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFqcOrderChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取出货检验单变更日志表(FqcOrderChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:import", "获取出货检验单变更日志表(FqcOrderChangeLog)导入模板")]
    public async Task<IActionResult> GetFqcOrderChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFqcOrderChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:import", "导入出货检验单变更日志表(FqcOrderChangeLog)")]
    public async Task<ActionResult<object>> ImportFqcOrderChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFqcOrderChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:fqcorderchangelog:export", "导出出货检验单变更日志表(FqcOrderChangeLog)")]
    public async Task<IActionResult> ExportFqcOrderChangeLogAsync([FromBody] TaktFqcOrderChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFqcOrderChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
