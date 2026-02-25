// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Identity
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

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 租户控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "租户")]
[ApiModule("Tenant", "租户管理")]
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
    public async Task<ActionResult<TaktPagedResult<TaktTenantDto>>> GetListAsync([FromQuery] TaktTenantQueryDto queryDto)
    {
        var result = await _tenantService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("identity:tenant:query", "查询租户详情")]
    public async Task<ActionResult<TaktTenantDto>> GetByIdAsync(long id)
    {
        var tenant = await _tenantService.GetByIdAsync(id);
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
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _tenantService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="dto">创建租户DTO</param>
    /// <returns>租户DTO</returns>
    [HttpPost]
    [TaktPermission("identity:tenant:create", "创建租户")]
    public async Task<ActionResult<TaktTenantDto>> CreateAsync([FromBody] TaktTenantCreateDto dto)
    {
        var tenant = await _tenantService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = tenant.TenantId }, tenant);
    }

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="dto">更新租户DTO</param>
    /// <returns>租户DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("identity:tenant:update", "更新租户")]
    public async Task<ActionResult<TaktTenantDto>> UpdateAsync(long id, [FromBody] TaktTenantUpdateDto dto)
    {
        try
        {
            var tenant = await _tenantService.UpdateAsync(id, dto);
            return Ok(tenant);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("identity:tenant:delete", "删除租户")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _tenantService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="dto">租户状态DTO</param>
    /// <returns>租户DTO</returns>
    [HttpPut("status")]
    [TaktPermission("identity:tenant:status", "更新租户状态")]
    public async Task<ActionResult<TaktTenantDto>> UpdateStatusAsync([FromBody] TaktTenantStatusDto dto)
    {
        try
        {
            var tenant = await _tenantService.UpdateStatusAsync(dto);
            return Ok(tenant);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
    [TaktPermission("identity:tenant:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _tenantService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
                return BadRequest("请选择要导入的Excel文件");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _tenantService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出租户
    /// </summary>
    /// <param name="query">租户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("identity:tenant:export", "导出租户")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktTenantQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _tenantService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
