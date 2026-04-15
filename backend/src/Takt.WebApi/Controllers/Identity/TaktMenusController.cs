// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktMenusController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt菜单控制器，提供菜单管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 菜单控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "菜单管理")]
[ApiModule("Identity", "身份认证")]
[TaktPermission("identity:menu", "菜单管理")]
public class TaktMenusController : TaktControllerBase
{
    private readonly ITaktMenuService _menuService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="menuService">菜单服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMenusController(
        ITaktMenuService menuService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _menuService = menuService;
    }

    /// <summary>
    /// 获取菜单列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("identity:menu:list", "查询菜单列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMenuDto>>> GetListAsync([FromQuery] TaktMenuQueryDto queryDto)
    {
        var result = await _menuService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>菜单DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("identity:menu:query", "查询菜单详情")]
    public async Task<ActionResult<TaktMenuDto>> GetByIdAsync(long id)
    {
        var menu = await _menuService.GetByIdAsync(id);
        if (menu == null)
            return NotFound();
        return Ok(menu);
    }

    /// <summary>
    /// 获取菜单树形选项列表（用于业务组件：components/business/takt-tree-select 和 takt-select）
    /// </summary>
    /// <returns>菜单树形选项列表</returns>
    [HttpGet("tree-options")]
    [TaktPermission("identity:menu:list", "查询菜单树形选项")]
    public async Task<ActionResult<TaktApiResult<List<TaktTreeSelectOption>>>> GetTreeOptionsAsync()
    {
        var options = await _menuService.GetTreeOptionsAsync();
        return Ok(TaktApiResult<List<TaktTreeSelectOption>>.Ok(options));
    }

    /// <summary>
    /// 获取模块名称选项列表（仅 MenuType=0 目录），用于代码生成中的模块列表。返回树形 TaktMenuTreeDto（含 MenuName、Path 等）。
    /// </summary>
    /// <returns>目录级菜单树形列表</returns>
    [HttpGet("module-name-options")]
    [TaktPermission("identity:menu:list", "查询模块名称选项")]
    public async Task<ActionResult<TaktApiResult<List<TaktMenuTreeDto>>>> GetModuleNameOptionsAsync()
    {
        var options = await _menuService.GetModuleNameOptionsAsync();
        return Ok(TaktApiResult<List<TaktMenuTreeDto>>.Ok(options));
    }

    /// <summary>
    /// 获取当前用户的菜单树形列表（根据用户权限过滤）
    /// </summary>
    /// <returns>当前用户的菜单树形列表</returns>
    [HttpGet("current-tree")]
    public async Task<ActionResult<TaktApiResult<List<TaktMenuTreeDto>>>> GetCurrentTreeMenuAsync()
    {
        var menus = await _menuService.GetCurrentTreeMenuAsync();
        return Ok(TaktApiResult<List<TaktMenuTreeDto>>.Ok(menus));
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="dto">创建菜单DTO</param>
    /// <returns>菜单DTO</returns>
    [HttpPost]
    [TaktPermission("identity:menu:create", "创建菜单")]
    public async Task<ActionResult<TaktMenuDto>> CreateAsync([FromBody] TaktMenuCreateDto dto)
    {
        var menu = await _menuService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = menu.MenuId }, menu);
    }

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <param name="dto">更新菜单DTO</param>
    /// <returns>菜单DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("identity:menu:update", "更新菜单")]
    public async Task<ActionResult<TaktMenuDto>> UpdateAsync(long id, [FromBody] TaktMenuUpdateDto dto)
    {
        try
        {
            var menu = await _menuService.UpdateAsync(id, dto);
            return Ok(menu);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("identity:menu:delete", "删除菜单")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _menuService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    /// <param name="dto">菜单状态DTO</param>
    /// <returns>菜单DTO</returns>
    [HttpPut("status")]
    [TaktPermission("identity:menu:status", "更新菜单状态")]
    public async Task<ActionResult<TaktMenuDto>> UpdateStatusAsync([FromBody] TaktMenuStatusDto dto)
    {
        try
        {
            var menu = await _menuService.UpdateStatusAsync(dto);
            return Ok(menu);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("identity:menu:import", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _menuService.GetTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入菜单
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("identity:menu:import", "导入菜单")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
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
            var (success, fail, errors) = await _menuService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出菜单
    /// </summary>
    /// <param name="query">菜单查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("identity:menu:export", "导出菜单")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktMenuQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _menuService.ExportAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}