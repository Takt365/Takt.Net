// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktProfitCentersController.cs
// 功能描述：Takt利润中心控制器，提供利润中心管理的RESTful API接口
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 利润中心控制器
/// </summary>
[Route("api/[controller]", Name = "利润中心")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:profitcenter", "利润中心管理")]
public class TaktProfitCentersController : TaktControllerBase
{
    private readonly ITaktProfitCenterService _profitCenterService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCentersController(
        ITaktProfitCenterService profitCenterService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _profitCenterService = profitCenterService;
    }

    /// <summary>
    /// 获取利润中心列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:profitcenter:list", "查询利润中心列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProfitCenterDto>>> GetListAsync([FromQuery] TaktProfitCenterQueryDto queryDto)
    {
        var result = await _profitCenterService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取利润中心
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:profitcenter:query", "查询利润中心详情")]
    public async Task<ActionResult<TaktProfitCenterDto>> GetByIdAsync(long id)
    {
        var entity = await _profitCenterService.GetByIdAsync(id);
        if (entity == null)
            return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// 获取利润中心选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:profitcenter:list", "查询利润中心选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _profitCenterService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建利润中心
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:profitcenter:create", "创建利润中心")]
    public async Task<ActionResult<TaktProfitCenterDto>> CreateAsync([FromBody] TaktProfitCenterCreateDto dto)
    {
        try
        {
            var entity = await _profitCenterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.ProfitCenterId }, entity);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新利润中心
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:profitcenter:update", "更新利润中心")]
    public async Task<ActionResult<TaktProfitCenterDto>> UpdateAsync(long id, [FromBody] TaktProfitCenterUpdateDto dto)
    {
        try
        {
            var entity = await _profitCenterService.UpdateAsync(id, dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除利润中心
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:profitcenter:delete", "删除利润中心")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _profitCenterService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新利润中心状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:controlling:profitcenter:status", "更新利润中心状态")]
    public async Task<ActionResult<TaktProfitCenterDto>> UpdateStatusAsync([FromBody] TaktProfitCenterStatusDto dto)
    {
        try
        {
            var entity = await _profitCenterService.UpdateStatusAsync(dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件流</returns>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:profitcenter:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _profitCenterService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导入利润中心
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>导入结果（成功数、失败数、错误信息）</returns>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:profitcenter:import", "导入利润中心")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的Excel文件");
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _profitCenterService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出利润中心
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件流</returns>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:profitcenter:export", "导出利润中心")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktProfitCenterQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _profitCenterService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
