// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktUsersController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户控制器，提供用户管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 用户控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "用户")]
[ApiModule("Identity", "身份认证")]
[TaktPermission("identity:user", "用户管理")]
public class TaktUsersController : TaktControllerBase
{
    private readonly ITaktUserService _userService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userService">用户服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktUsersController(
        ITaktUserService userService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _userService = userService;
    }

    /// <summary>
    /// 获取用户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("identity:user:list", "查询用户列表")]
    public async Task<ActionResult<TaktPagedResult<TaktUserDto>>> GetUserListAsync([FromQuery] TaktUserQueryDto queryDto)
    {
        var result = await _userService.GetUserListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("identity:user:query", "查询用户详情")]
    public async Task<ActionResult<TaktUserDto>> GetUserByIdAsync(long id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// 获取用户选项列表（用于下拉框等）
    /// </summary>
    /// <returns>用户选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("identity:user:query", "获取用户选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetUserOptionsAsync()
    {
        var options = await _userService.GetUserOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="dto">创建用户DTO</param>
    /// <returns>用户DTO</returns>
    [HttpPost]
    [TaktPermission("identity:user:create", "创建用户")]
    public async Task<ActionResult<TaktUserDto>> CreateUserAsync([FromBody] TaktUserCreateDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return Ok(user);
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="dto">更新用户DTO</param>
    /// <returns>用户DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("identity:user:update", "更新用户")]
    public async Task<ActionResult<TaktUserDto>> UpdateUserAsync(long id, [FromBody] TaktUserUpdateDto dto)
    {
        try
        {
            var user = await _userService.UpdateUserAsync(id, dto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除用户（单条）
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("identity:user:delete", "删除用户")]
    public async Task<IActionResult> DeleteUserByIdAsync(long id)
    {
        await _userService.DeleteUserByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除用户
    /// </summary>
    /// <param name="ids">用户ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("delete")]
    [TaktPermission("identity:user:delete", "批量删除用户")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        if (ids == null || ids.Count == 0)
            return BadRequest(GetLocalizedString("validation.userIdsDeleteRequired", "Frontend"));
        await _userService.DeleteUserBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新用户状态
    /// </summary>
    /// <param name="dto">用户状态DTO</param>
    /// <returns>用户DTO</returns>
    [HttpPut("status")]
    [TaktPermission("identity:user:status", "更新用户状态")]
    public async Task<ActionResult<TaktUserDto>> UpdateUserStatusAsync([FromBody] TaktUserStatusDto dto)
    {
        try
        {
            var user = await _userService.UpdateUserStatusAsync(dto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="dto">重置密码DTO</param>
    /// <returns>操作结果</returns>
    [HttpPut("reset-password")]
    [TaktPermission("identity:user:resetpwd", "重置密码")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] TaktUserResetPwdDto dto)
    {
        try
        {
            await _userService.ResetPasswordAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="dto">修改密码DTO</param>
    /// <returns>操作结果</returns>
    [HttpPut("change-password")]
    [TaktPermission("identity:user:changepwd", "修改密码")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] TaktUserChangePwdDto dto)
    {
        try
        {
            await _userService.ChangePasswordAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 忘记密码（发送密码重置邮件）
    /// </summary>
    /// <param name="dto">忘记密码DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] TaktUserForgotPasswordDto dto)
    {
        try
        {
            var result = await _userService.ForgotPasswordAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 解锁用户
    /// </summary>
    /// <param name="dto">解锁用户DTO</param>
    /// <returns>用户DTO</returns>
    [HttpPut("unlock")]
    [TaktPermission("identity:user:unlock", "解锁用户")]
    public async Task<ActionResult<TaktUserDto>> UnlockAsync([FromBody] TaktUserUnlockDto dto)
    {
        try
        {
            var user = await _userService.UnlockAsync(dto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 更新头像
    /// </summary>
    /// <param name="dto">用户头像更新DTO</param>
    /// <returns>用户DTO</returns>
    [HttpPut("avatar")]
    [TaktPermission("identity:user:update", "更新用户头像")]
    public async Task<ActionResult<TaktUserDto>> UpdateAvatarAsync([FromBody] TaktUserAvatarUpdateDto dto)
    {
        try
        {
            var user = await _userService.UpdateAvatarAsync(dto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户角色列表</returns>
    [HttpGet("{userId}/roles")]
    [TaktPermission("identity:user:query", "查询用户角色")]
    public async Task<ActionResult<List<TaktUserRoleDto>>> GetUserRoleIdsAsync(long userId)
    {
        var roles = await _userService.GetUserRoleIdsAsync(userId);
        return Ok(roles);
    }

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户部门列表</returns>
    [HttpGet("{userId}/depts")]
    [TaktPermission("identity:user:query", "查询用户部门")]
    public async Task<ActionResult<List<TaktUserDeptDto>>> GetUserDeptIdsAsync(long userId)
    {
        var depts = await _userService.GetUserDeptIdsAsync(userId);
        return Ok(depts);
    }

    /// <summary>
    /// 获取用户岗位列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户岗位列表</returns>
    [HttpGet("{userId}/posts")]
    [TaktPermission("identity:user:query", "查询用户岗位")]
    public async Task<ActionResult<List<TaktUserPostDto>>> GetUserPostIdsAsync(long userId)
    {
        var posts = await _userService.GetUserPostIdsAsync(userId);
        return Ok(posts);
    }

    /// <summary>
    /// 分配用户角色
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleIds">角色ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{userId}/roles")]
    [TaktPermission("identity:user:update", "分配用户角色")]
    public async Task<IActionResult> AssignUserRolesAsync(long userId, [FromBody] long[] roleIds)
    {
        try
        {
            var result = await _userService.AssignUserRolesAsync(userId, roleIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 分配用户部门
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deptIds">部门ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{userId}/depts")]
    [TaktPermission("identity:user:update", "分配用户部门")]
    public async Task<IActionResult> AssignUserDeptsAsync(long userId, [FromBody] long[] deptIds)
    {
        try
        {
            var result = await _userService.AssignUserDeptsAsync(userId, deptIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 分配用户岗位
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="postIds">岗位ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{userId}/posts")]
    [TaktPermission("identity:user:update", "分配用户岗位")]
    public async Task<IActionResult> AssignUserPostsAsync(long userId, [FromBody] long[] postIds)
    {
        try
        {
            var result = await _userService.AssignUserPostsAsync(userId, postIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取用户租户列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户租户列表</returns>
    [HttpGet("{userId}/tenants")]
    [TaktPermission("identity:user:query", "查询用户租户")]
    public async Task<ActionResult<List<TaktUserTenantDto>>> GetUserTenantIdsAsync(long userId)
    {
        var tenants = await _userService.GetUserTenantIdsAsync(userId);
        return Ok(tenants);
    }

    /// <summary>
    /// 分配用户租户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="tenantIds">租户ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{userId}/tenants")]
    [TaktPermission("identity:user:update", "分配用户租户")]
    public async Task<IActionResult> AssignUserTenantsAsync(long userId, [FromBody] long[] tenantIds)
    {
        try
        {
            var result = await _userService.AssignUserTenantsAsync(userId, tenantIds);
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
    [TaktPermission("identity:user:import", "获取导入模板")]
    public async Task<IActionResult> GetUserTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _userService.GetUserTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入用户
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("identity:user:import", "导入用户")]
    public async Task<ActionResult<object>> ImportUserAsync(IFormFile file, [FromForm] string? sheetName = null)
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
            var (success, fail, errors) = await _userService.ImportUserAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出用户
    /// </summary>
    /// <param name="query">用户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("identity:user:export", "导出用户")]
    public async Task<IActionResult> ExportUserAsync([FromBody] TaktUserQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _userService.ExportUserAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    #region 统计分析

    /// <summary>
    /// 统计用户总数
    /// </summary>
    /// <returns>用户总数</returns>
    [HttpGet("stats/count")]
    [TaktPermission("identity:user:list", "统计用户总数")]
    public async Task<ActionResult<long>> GetUserCountAsync()
    {
        var count = await _userService.GetUserCountAsync();
        return Ok(count);
    }

    #endregion
}
