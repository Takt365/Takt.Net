// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.HelpDesk
// 文件名称：TaktSelfServicesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：自助服务表控制器，提供SelfService管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Application.Services.Routine.Business.HelpDesk;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Business.HelpDesk;

/// <summary>
/// 自助服务表控制器
/// </summary>
[Route("api/[controller]", Name = "自助服务表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:helpdesk:selfservice", "自助服务表管理")]
public class TaktSelfServicesController : TaktControllerBase
{
    private readonly ITaktSelfServiceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServicesController(
        ITaktSelfServiceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取自助服务表(SelfService)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:helpdesk:selfservice:list", "查询自助服务表(SelfService)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSelfServiceDto>>> GetSelfServiceListAsync([FromQuery] TaktSelfServiceQueryDto queryDto)
    {
        var result = await _service.GetSelfServiceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取自助服务表(SelfService)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:helpdesk:selfservice:query", "查询自助服务表(SelfService)详情")]
    public async Task<ActionResult<TaktSelfServiceDto>> GetSelfServiceByIdAsync(long id)
    {
        var item = await _service.GetSelfServiceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取自助服务表(SelfService)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:helpdesk:selfservice:query", "查询自助服务表(SelfService)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSelfServiceOptionsAsync()
    {
        var result = await _service.GetSelfServiceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建自助服务表(SelfService)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:helpdesk:selfservice:create", "创建自助服务表(SelfService)")]
    public async Task<ActionResult<TaktSelfServiceDto>> CreateSelfServiceAsync([FromBody] TaktSelfServiceCreateDto dto)
    {
        var result = await _service.CreateSelfServiceAsync(dto);
        return CreatedAtAction(nameof(GetSelfServiceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新自助服务表(SelfService)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:helpdesk:selfservice:update", "更新自助服务表(SelfService)")]
    public async Task<ActionResult<TaktSelfServiceDto>> UpdateSelfServiceAsync(long id, [FromBody] TaktSelfServiceUpdateDto dto)
    {
        var result = await _service.UpdateSelfServiceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除自助服务表(SelfService)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:helpdesk:selfservice:delete", "删除自助服务表(SelfService)")]
    public async Task<ActionResult> DeleteSelfServiceByIdAsync(long id)
    {
        await _service.DeleteSelfServiceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除自助服务表(SelfService)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:helpdesk:selfservice:delete", "批量删除自助服务表(SelfService)")]
    public async Task<ActionResult> DeleteSelfServiceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSelfServiceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新自助服务表(SelfService)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:business:helpdesk:selfservice:update", "更新自助服务表(SelfService)状态")]
    public async Task<ActionResult<TaktSelfServiceDto>> UpdateSelfServiceStatusAsync([FromBody] TaktSelfServiceStatusDto dto)
    {
        var result = await _service.UpdateSelfServiceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新自助服务表(SelfService)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:business:helpdesk:selfservice:update", "更新自助服务表(SelfService)排序")]
    public async Task<ActionResult<TaktSelfServiceDto>> UpdateSelfServiceSortAsync([FromBody] TaktSelfServiceSortDto dto)
    {
        var result = await _service.UpdateSelfServiceSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取自助服务表(SelfService)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:helpdesk:selfservice:import", "获取自助服务表(SelfService)导入模板")]
    public async Task<IActionResult> GetSelfServiceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSelfServiceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入自助服务表(SelfService)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:helpdesk:selfservice:import", "导入自助服务表(SelfService)")]
    public async Task<ActionResult<object>> ImportSelfServiceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSelfServiceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出自助服务表(SelfService)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:helpdesk:selfservice:export", "导出自助服务表(SelfService)")]
    public async Task<IActionResult> ExportSelfServiceAsync([FromBody] TaktSelfServiceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSelfServiceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
