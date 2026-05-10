// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktUserTenantsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：用户租户关联表控制器，提供UserTenant管理的RESTful API接口
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
/// 用户租户关联表控制器
/// </summary>
[Route("api/[controller]", Name = "用户租户关联表")]
[ApiModule("System", "系统管理")]
[TaktPermission("identity:usertenant", "用户租户关联表管理")]
public class TaktUserTenantsController : TaktControllerBase
{
    private readonly ITaktUserTenantService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserTenantsController(
        ITaktUserTenantService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取用户租户关联表(UserTenant)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("identity:usertenant:list", "查询用户租户关联表(UserTenant)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktUserTenantDto>>> GetUserTenantListAsync([FromQuery] TaktUserTenantQueryDto queryDto)
    {
        var result = await _service.GetUserTenantListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取用户租户关联表(UserTenant)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("identity:usertenant:query", "查询用户租户关联表(UserTenant)详情")]
    public async Task<ActionResult<TaktUserTenantDto>> GetUserTenantByIdAsync(long id)
    {
        var item = await _service.GetUserTenantByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取用户租户关联表(UserTenant)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("identity:usertenant:query", "查询用户租户关联表(UserTenant)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetUserTenantOptionsAsync()
    {
        var result = await _service.GetUserTenantOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建用户租户关联表(UserTenant)
    /// </summary>
    [HttpPost]
    [TaktPermission("identity:usertenant:create", "创建用户租户关联表(UserTenant)")]
    public async Task<ActionResult<TaktUserTenantDto>> CreateUserTenantAsync([FromBody] TaktUserTenantCreateDto dto)
    {
        var result = await _service.CreateUserTenantAsync(dto);
        return CreatedAtAction(nameof(GetUserTenantByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新用户租户关联表(UserTenant)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("identity:usertenant:update", "更新用户租户关联表(UserTenant)")]
    public async Task<ActionResult<TaktUserTenantDto>> UpdateUserTenantAsync(long id, [FromBody] TaktUserTenantUpdateDto dto)
    {
        var result = await _service.UpdateUserTenantAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除用户租户关联表(UserTenant)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("identity:usertenant:delete", "删除用户租户关联表(UserTenant)")]
    public async Task<ActionResult> DeleteUserTenantByIdAsync(long id)
    {
        await _service.DeleteUserTenantByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除用户租户关联表(UserTenant)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("identity:usertenant:delete", "批量删除用户租户关联表(UserTenant)")]
    public async Task<ActionResult> DeleteUserTenantBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteUserTenantBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取用户租户关联表(UserTenant)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("identity:usertenant:import", "获取用户租户关联表(UserTenant)导入模板")]
    public async Task<IActionResult> GetUserTenantTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetUserTenantTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入用户租户关联表(UserTenant)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("identity:usertenant:import", "导入用户租户关联表(UserTenant)")]
    public async Task<ActionResult<object>> ImportUserTenantAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportUserTenantAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出用户租户关联表(UserTenant)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("identity:usertenant:export", "导出用户租户关联表(UserTenant)")]
    public async Task<IActionResult> ExportUserTenantAsync([FromBody] TaktUserTenantQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportUserTenantAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
