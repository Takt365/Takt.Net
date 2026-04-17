// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Tenant
// 文件名称：TaktTenantsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户控制器，提供租户管理的RESTful API接口
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

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 租户控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "租户")]
[ApiModule("Identity", "身份认证")]
[TaktPermission("identity:tenant", "租户管理")]
public class TaktTenantsController : TaktControllerBase
{
    private readonly ITaktTenantService _tenantService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tenantService">租户服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTenantsController(
        ITaktTenantService tenantService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _tenantService = tenantService;
    }

    /// <summary>
    /// 获取租户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("identity:tenant:list", "查询租户列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTenantDto>>> GetTenantListAsync([FromQuery] TaktTenantQueryDto queryDto)
    {
        var result = await _tenantService.GetTenantListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("identity:tenant:query", "查询租户详情")]
    public async Task<ActionResult<TaktTenantDto>> GetTenantByIdAsync(long id)
    {
        var tenant = await _tenantService.GetTenantByIdAsync(id);
        if (tenant == null)
            return NotFound();
        return Ok(tenant);
    }

    /// <summary>
    /// 获取租户选项列表（用于下拉框等）
    /// </summary>
    /// <returns>租户选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("identity:tenant:list", "查询租户选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTenantOptionsAsync()
    {
        var options = await _tenantService.GetTenantOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="dto">创建租户DTO</param>
    /// <returns>租户DTO</returns>
    [HttpPost]
    [TaktPermission("identity:tenant:create", "创建租户")]
    public async Task<ActionResult<TaktTenantDto>> CreateTenantAsync([FromBody] TaktTenantCreateDto dto)
    {
        var tenant = await _tenantService.CreateTenantAsync(dto);
        return CreatedAtAction(nameof(GetTenantByIdAsync), new { id = tenant.TenantId }, tenant);
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="dto">更新租户DTO</param>
    /// <returns>租户DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("identity:tenant:update", "更新租户")]
    public async Task<ActionResult<TaktTenantDto>> UpdateTenantAsync(long id, [FromBody] TaktTenantUpdateDto dto)
    {
        try
        {
            var tenant = await _tenantService.UpdateTenantAsync(id, dto);
            return Ok(tenant);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("identity:tenant:delete", "删除租户")]
    public async Task<IActionResult> DeleteTenantByIdAsync(long id)
    {
        await _tenantService.DeleteTenantByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="dto">租户状态DTO</param>
    /// <returns>租户DTO</returns>
    [HttpPut("status")]
    [TaktPermission("identity:tenant:status", "更新租户状态")]
    public async Task<ActionResult<TaktTenantDto>> UpdateTenantStatusAsync([FromBody] TaktTenantStatusDto dto)
    {
        try
        {
            var tenant = await _tenantService.UpdateTenantStatusAsync(dto);
            return Ok(tenant);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取租户用户列表
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <returns>租户用户列表</returns>
    [HttpGet("{tenantId}/users")]
    [TaktPermission("identity:tenant:query", "查询租户用户")]
    public async Task<ActionResult<List<TaktUserTenantDto>>> GetUserTenantIdsAsync(long tenantId)
    {
        var users = await _tenantService.GetUserTenantIdsAsync(tenantId);
        return Ok(users);
    }

    /// <summary>
    /// 分配租户用户
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="userIds">用户ID数组</param>
    /// <returns>操作结果</returns>
    [HttpPut("{tenantId}/users")]
    [TaktPermission("identity:tenant:update", "分配租户用户")]
    public async Task<IActionResult> AssignUserTenantsAsync(long tenantId, [FromBody] long[] userIds)
    {
        try
        {
            var result = await _tenantService.AssignUserTenantsAsync(tenantId, userIds);
            return Ok(new { success = result });
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
    [TaktPermission("identity:tenant:import", "获取导入模板")]
    public async Task<IActionResult> GetTenantTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _tenantService.GetTenantTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入租户
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("identity:tenant:import", "导入租户")]
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
            var (success, fail, errors) = await _tenantService.ImportTenantAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出租户
    /// </summary>
    /// <param name="query">租户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("identity:tenant:export", "导出租户")]
    public async Task<IActionResult> ExportTenantAsync([FromBody] TaktTenantQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _tenantService.ExportTenantAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
