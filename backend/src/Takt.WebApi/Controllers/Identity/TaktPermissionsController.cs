// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktPermissionsController.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt权限控制器，提供权限管理的RESTful API接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 权限控制器
/// </summary>
[Route("api/[controller]", Name = "权限管理")]
[ApiModule("Identity", "身份认证")]
[TaktPermission("identity:permission", "权限管理")]
public class TaktPermissionsController : TaktControllerBase
{
    private readonly ITaktPermissionService _permissionService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permissionService">权限服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPermissionsController(
        ITaktPermissionService permissionService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _permissionService = permissionService;
    }

    /// <summary>
    /// 获取权限列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("identity:permission:list", "查询权限列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPermissionDto>>> GetListAsync([FromQuery] TaktPermissionQueryDto queryDto)
    {
        var result = await _permissionService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <returns>权限DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("identity:permission:query", "查询权限详情")]
    public async Task<ActionResult<TaktPermissionDto>> GetByIdAsync(long id)
    {
        var permission = await _permissionService.GetByIdAsync(id);
        if (permission == null)
            return NotFound();
        return Ok(permission);
    }

    /// <summary>
    /// 获取权限选项列表（用于下拉框等）
    /// </summary>
    /// <returns>权限选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("identity:permission:list", "查询权限选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _permissionService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建权限
    /// </summary>
    /// <param name="dto">创建权限DTO</param>
    /// <returns>权限DTO</returns>
    [HttpPost]
    [TaktPermission("identity:permission:create", "创建权限")]
    public async Task<ActionResult<TaktPermissionDto>> CreateAsync([FromBody] TaktPermissionCreateDto dto)
    {
        var permission = await _permissionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = permission.PermissionId }, permission);
    }

    /// <summary>
    /// 更新权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <param name="dto">更新权限DTO</param>
    /// <returns>权限DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("identity:permission:update", "更新权限")]
    public async Task<ActionResult<TaktPermissionDto>> UpdateAsync(long id, [FromBody] TaktPermissionUpdateDto dto)
    {
        try
        {
            var permission = await _permissionService.UpdateAsync(id, dto);
            return Ok(permission);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("identity:permission:delete", "删除权限")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _permissionService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除权限
    /// </summary>
    /// <param name="ids">权限ID列表</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    [TaktPermission("identity:permission:delete", "批量删除权限")]
    public async Task<IActionResult> DeleteAsync([FromBody] IEnumerable<long> ids)
    {
        try
        {
            await _permissionService.DeleteAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新权限状态
    /// </summary>
    /// <param name="dto">权限状态DTO</param>
    /// <returns>权限DTO</returns>
    [HttpPut("status")]
    [TaktPermission("identity:permission:status", "更新权限状态")]
    public async Task<ActionResult<TaktPermissionDto>> UpdateStatusAsync([FromBody] TaktPermissionStatusDto dto)
    {
        try
        {
            var permission = await _permissionService.UpdateStatusAsync(dto);
            return Ok(permission);
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
    [TaktPermission("identity:permission:template", "获取权限导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _permissionService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入权限
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("identity:permission:import", "导入权限")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的Excel文件");
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _permissionService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出权限
    /// </summary>
    /// <param name="query">权限查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("identity:permission:export", "导出权限")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktPermissionQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _permissionService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
