// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Organization
// 文件名称：TaktDeptsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门控制器，提供部门管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Organization;

/// <summary>
/// 部门控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "部门")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:dept:list", "部门管理")]
public class TaktDeptsController : TaktControllerBase
{
    private readonly ITaktDeptService _deptService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deptService">部门服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDeptsController(
        ITaktDeptService deptService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _deptService = deptService;
    }

    /// <summary>
    /// 获取部门列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:dept:list", "查询部门列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDeptDto>>> GetListAsync([FromQuery] TaktDeptQueryDto queryDto)
    {
        var result = await _deptService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>部门DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:dept:detail", "查询部门详情")]
    public async Task<ActionResult<TaktDeptDto>> GetByIdAsync(long id)
    {
        var dept = await _deptService.GetByIdAsync(id);
        if (dept == null)
            return NotFound();
        return Ok(dept);
    }

    /// <summary>
    /// 获取部门树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>部门树形选项列表</returns>
    [HttpGet("tree-options")]
    [TaktPermission("humanresource:organization:dept:list", "查询部门树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetTreeOptionsAsync()
    {
        var options = await _deptService.GetTreeOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建部门
    /// </summary>
    /// <param name="dto">创建部门DTO</param>
    /// <returns>部门DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:organization:dept:create", "创建部门")]
    public async Task<ActionResult<TaktDeptDto>> CreateAsync([FromBody] TaktDeptCreateDto dto)
    {
        var dept = await _deptService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dept.DeptId }, dept);
    }

    /// <summary>
    /// 更新部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <param name="dto">更新部门DTO</param>
    /// <returns>部门DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:dept:update", "更新部门")]
    public async Task<ActionResult<TaktDeptDto>> UpdateAsync(long id, [FromBody] TaktDeptUpdateDto dto)
    {
        try
        {
            var dept = await _deptService.UpdateAsync(id, dto);
            return Ok(dept);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:dept:delete", "删除部门")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _deptService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新部门状态
    /// </summary>
    /// <param name="dto">部门状态DTO</param>
    /// <returns>部门DTO</returns>
    [HttpPut("status")]
    [TaktPermission("humanresource:organization:dept:update", "更新部门状态")]
    public async Task<ActionResult<TaktDeptDto>> UpdateStatusAsync([FromBody] TaktDeptStatusDto dto)
    {
        try
        {
            var dept = await _deptService.UpdateStatusAsync(dto);
            return Ok(dept);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取部门用户列表
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <returns>部门用户列表</returns>
    [HttpGet("{deptId}/users")]
    [TaktPermission("humanresource:organization:dept:query", "查询部门用户")]
    public async Task<ActionResult<List<TaktUserDeptDto>>> GetUserDeptIdsAsync(long deptId)
    {
        var users = await _deptService.GetUserDeptIdsAsync(deptId);
        return Ok(users);
    }

    /// <summary>
    /// 分配部门用户
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="userIds">用户ID数组</param>
    /// <returns>操作结果</returns>
    [HttpPut("{deptId}/users")]
    [TaktPermission("humanresource:organization:dept:update", "分配部门用户")]
    public async Task<IActionResult> AssignUserDeptsAsync(long deptId, [FromBody] long[] userIds)
    {
        try
        {
            var result = await _deptService.AssignUserDeptsAsync(deptId, userIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取部门角色列表
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>部门角色分页列表</returns>
    [HttpPost("{deptId}/roles")]
    [TaktPermission("humanresource:organization:dept:query", "查询部门角色")]
    public async Task<ActionResult<TaktPagedResult<TaktRoleDeptDto>>> GetRoleDeptIdsAsync(long deptId, [FromBody] TaktRoleQueryDto query)
    {
        var roles = await _deptService.GetRoleDeptIdsAsync(deptId, query);
        return Ok(roles);
    }

    /// <summary>
    /// 分配部门角色
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="roleIds">角色ID数组</param>
    /// <returns>操作结果</returns>
    [HttpPut("{deptId}/roles")]
    [TaktPermission("humanresource:organization:dept:update", "分配部门角色")]
    public async Task<IActionResult> AssignRoleDeptsAsync(long deptId, [FromBody] long[] roleIds)
    {
        try
        {
            var result = await _deptService.AssignRoleDeptsAsync(deptId, roleIds);
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
    [TaktPermission("humanresource:organization:dept:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _deptService.GetTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入部门
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:dept:import", "导入部门")]
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
            var (success, fail, errors) = await _deptService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出部门
    /// </summary>
    /// <param name="query">部门查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:dept:export", "导出部门")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktDeptQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _deptService.ExportAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}