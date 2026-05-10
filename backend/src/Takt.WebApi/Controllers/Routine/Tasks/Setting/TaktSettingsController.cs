// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.Setting
// 文件名称：TaktSettingsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：系统设置表控制器，提供Setting管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.Setting;
using Takt.Application.Services.Routine.Tasks.Setting;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Setting;

/// <summary>
/// 系统设置表控制器
/// </summary>
[Route("api/[controller]", Name = "系统设置表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:setting", "系统设置表管理")]
public class TaktSettingsController : TaktControllerBase
{
    private readonly ITaktSettingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsController(
        ITaktSettingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取系统设置表(Setting)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:setting:list", "查询系统设置表(Setting)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSettingDto>>> GetSettingListAsync([FromQuery] TaktSettingQueryDto queryDto)
    {
        var result = await _service.GetSettingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取系统设置表(Setting)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:setting:query", "查询系统设置表(Setting)详情")]
    public async Task<ActionResult<TaktSettingDto>> GetSettingByIdAsync(long id)
    {
        var item = await _service.GetSettingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取系统设置表(Setting)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:setting:query", "查询系统设置表(Setting)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSettingOptionsAsync()
    {
        var result = await _service.GetSettingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建系统设置表(Setting)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:setting:create", "创建系统设置表(Setting)")]
    public async Task<ActionResult<TaktSettingDto>> CreateSettingAsync([FromBody] TaktSettingCreateDto dto)
    {
        var result = await _service.CreateSettingAsync(dto);
        return CreatedAtAction(nameof(GetSettingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新系统设置表(Setting)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:setting:update", "更新系统设置表(Setting)")]
    public async Task<ActionResult<TaktSettingDto>> UpdateSettingAsync(long id, [FromBody] TaktSettingUpdateDto dto)
    {
        var result = await _service.UpdateSettingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除系统设置表(Setting)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:setting:delete", "删除系统设置表(Setting)")]
    public async Task<ActionResult> DeleteSettingByIdAsync(long id)
    {
        await _service.DeleteSettingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除系统设置表(Setting)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:setting:delete", "批量删除系统设置表(Setting)")]
    public async Task<ActionResult> DeleteSettingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSettingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新系统设置表(Setting)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:setting:update", "更新系统设置表(Setting)状态")]
    public async Task<ActionResult<TaktSettingDto>> UpdateSettingStatusAsync([FromBody] TaktSettingStatusDto dto)
    {
        var result = await _service.UpdateSettingStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新系统设置表(Setting)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:tasks:setting:update", "更新系统设置表(Setting)排序")]
    public async Task<ActionResult<TaktSettingDto>> UpdateSettingSortAsync([FromBody] TaktSettingSortDto dto)
    {
        var result = await _service.UpdateSettingSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取系统设置表(Setting)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:setting:import", "获取系统设置表(Setting)导入模板")]
    public async Task<IActionResult> GetSettingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSettingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入系统设置表(Setting)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:setting:import", "导入系统设置表(Setting)")]
    public async Task<ActionResult<object>> ImportSettingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSettingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出系统设置表(Setting)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:setting:export", "导出系统设置表(Setting)")]
    public async Task<IActionResult> ExportSettingAsync([FromBody] TaktSettingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSettingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
