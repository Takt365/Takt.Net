// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktMenusController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：菜单信息表控制器，提供Menu管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 菜单信息表控制器
/// </summary>
[Route("api/[controller]", Name = "菜单信息表")]
[ApiModule("System", "系统管理")]
[TaktPermission("identity:menu", "菜单信息表管理")]
public class TaktMenusController : TaktControllerBase
{
    private readonly ITaktMenuService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenusController(
        ITaktMenuService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取菜单信息表(Menu)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("identity:menu:list", "查询菜单信息表(Menu)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMenuDto>>> GetMenuListAsync([FromQuery] TaktMenuQueryDto queryDto)
    {
        var result = await _service.GetMenuListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取菜单信息表(Menu)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("identity:menu:query", "查询菜单信息表(Menu)详情")]
    public async Task<ActionResult<TaktMenuDto>> GetMenuByIdAsync(long id)
    {
        var item = await _service.GetMenuByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取菜单信息表(Menu)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("identity:menu:query", "查询菜单信息表(Menu)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetMenuOptionsAsync()
    {
        var result = await _service.GetMenuOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取菜单信息表(Menu)树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("identity:menu:query", "查询菜单信息表(Menu)树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetMenuTreeOptionsAsync()
    {
        var result = await _service.GetMenuTreeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取菜单信息表(Menu)树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("identity:menu:query", "查询菜单信息表(Menu)树形")]
    public async Task<ActionResult<List<TaktMenuTreeDto>>> GetMenuTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetMenuTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 获取菜单信息表(Menu)子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("identity:menu:query", "查询菜单信息表(Menu)子节点")]
    public async Task<ActionResult<List<TaktMenuDto>>> GetMenuChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetMenuChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 创建菜单信息表(Menu)
    /// </summary>
    [HttpPost]
    [TaktPermission("identity:menu:create", "创建菜单信息表(Menu)")]
    public async Task<ActionResult<TaktMenuDto>> CreateMenuAsync([FromBody] TaktMenuCreateDto dto)
    {
        var result = await _service.CreateMenuAsync(dto);
        return CreatedAtAction(nameof(GetMenuByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新菜单信息表(Menu)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("identity:menu:update", "更新菜单信息表(Menu)")]
    public async Task<ActionResult<TaktMenuDto>> UpdateMenuAsync(long id, [FromBody] TaktMenuUpdateDto dto)
    {
        var result = await _service.UpdateMenuAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除菜单信息表(Menu)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("identity:menu:delete", "删除菜单信息表(Menu)")]
    public async Task<ActionResult> DeleteMenuByIdAsync(long id)
    {
        await _service.DeleteMenuByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除菜单信息表(Menu)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("identity:menu:delete", "批量删除菜单信息表(Menu)")]
    public async Task<ActionResult> DeleteMenuBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteMenuBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新菜单信息表(Menu)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("identity:menu:update", "更新菜单信息表(Menu)状态")]
    public async Task<ActionResult<TaktMenuDto>> UpdateMenuStatusAsync([FromBody] TaktMenuStatusDto dto)
    {
        var result = await _service.UpdateMenuStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新菜单信息表(Menu)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("identity:menu:update", "更新菜单信息表(Menu)排序")]
    public async Task<ActionResult<TaktMenuDto>> UpdateMenuSortAsync([FromBody] TaktMenuSortDto dto)
    {
        var result = await _service.UpdateMenuSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取菜单信息表(Menu)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("identity:menu:import", "获取菜单信息表(Menu)导入模板")]
    public async Task<IActionResult> GetMenuTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetMenuTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入菜单信息表(Menu)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("identity:menu:import", "导入菜单信息表(Menu)")]
    public async Task<ActionResult<object>> ImportMenuAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportMenuAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出菜单信息表(Menu)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("identity:menu:export", "导出菜单信息表(Menu)")]
    public async Task<IActionResult> ExportMenuAsync([FromBody] TaktMenuQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportMenuAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
