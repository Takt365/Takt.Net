// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.Setting
// 文件名称：TaktSettingsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt设置控制器，提供设置管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Setting;
using Takt.Application.Services.Routine.Setting;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Setting;

/// <summary>
/// 设置控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "设置")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:setting", "设置管理")]
public class TaktSettingsController : TaktControllerBase
{
    private readonly ITaktSettingsService _settingsService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="settingsService">设置服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSettingsController(
        ITaktSettingsService settingsService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _settingsService = settingsService;
    }

    /// <summary>
    /// 获取设置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:setting:list", "查询设置列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSettingsDto>>> GetListAsync([FromQuery] TaktSettingsQueryDto queryDto)
    {
        var result = await _settingsService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <returns>设置DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:setting:query", "查询设置详情")]
    public async Task<ActionResult<TaktSettingsDto>> GetByIdAsync(long id)
    {
        var settings = await _settingsService.GetByIdAsync(id);
        if (settings == null)
            return NotFound();
        return Ok(settings);
    }

    /// <summary>
    /// 根据设置键获取设置
    /// </summary>
    /// <param name="settingKey">设置键</param>
    /// <returns>设置DTO</returns>
    [HttpGet("key/{settingKey}")]
    [TaktPermission("routine:setting:query", "根据键查询设置")]
    public async Task<ActionResult<TaktSettingsDto>> GetByKeyAsync(string settingKey)
    {
        var settings = await _settingsService.GetByKeyAsync(settingKey);
        if (settings == null)
            return NotFound();
        return Ok(settings);
    }

    /// <summary>
    /// 获取设置选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设置选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("routine:setting:list", "查询设置选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _settingsService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建设置
    /// </summary>
    /// <param name="dto">创建设置DTO</param>
    /// <returns>设置DTO</returns>
    [HttpPost]
    [TaktPermission("routine:setting:create", "创建设置")]
    public async Task<ActionResult<TaktSettingsDto>> CreateAsync([FromBody] TaktSettingsCreateDto dto)
    {
        var settings = await _settingsService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = settings.SettingId }, settings);
    }

    /// <summary>
    /// 更新设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <param name="dto">更新设置DTO</param>
    /// <returns>设置DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:setting:update", "更新设置")]
    public async Task<ActionResult<TaktSettingsDto>> UpdateAsync(long id, [FromBody] TaktSettingsUpdateDto dto)
    {
        try
        {
            var settings = await _settingsService.UpdateAsync(id, dto);
            return Ok(settings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:setting:delete", "删除设置")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _settingsService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新设置状态
    /// </summary>
    /// <param name="dto">设置状态DTO</param>
    /// <returns>设置DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:setting:status", "更新设置状态")]
    public async Task<ActionResult<TaktSettingsDto>> UpdateStatusAsync([FromBody] TaktSettingsStatusDto dto)
    {
        try
        {
            var settings = await _settingsService.UpdateStatusAsync(dto);
            return Ok(settings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("routine:setting:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _settingsService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入设置
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("routine:setting:import", "导入设置")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("请选择要导入的Excel文件");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _settingsService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出设置
    /// </summary>
    /// <param name="query">设置查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("routine:setting:export", "导出设置")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktSettingsQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _settingsService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}