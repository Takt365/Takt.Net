// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktRoleService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色应用服务接口，定义角色管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt角色应用服务接口
/// </summary>
public interface ITaktRoleService
{
    /// <summary>
    /// 获取角色列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktRoleDto>> GetRoleListAsync(TaktRoleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>角色DTO</returns>
    Task<TaktRoleDto?> GetRoleByIdAsync(long id);

    /// <summary>
    /// 获取角色选项列表（用于下拉框等）
    /// </summary>
    /// <returns>角色选项列表</returns>
    Task<List<TaktSelectOption>> GetRoleOptionsAsync();

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="dto">创建角色DTO</param>
    /// <returns>角色DTO</returns>
    Task<TaktRoleDto> CreateRoleAsync(TaktRoleCreateDto dto);

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <param name="dto">更新角色DTO</param>
    /// <returns>角色DTO</returns>
    Task<TaktRoleDto> UpdateRoleAsync(long id, TaktRoleUpdateDto dto);

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>任务</returns>
    Task DeleteRoleByIdAsync(long id);

    /// <summary>
    /// 批量删除角色
    /// </summary>
    /// <param name="ids">角色ID列表</param>
    /// <returns>任务</returns>
    Task DeleteRoleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="dto">角色状态DTO</param>
    /// <returns>角色DTO</returns>
    Task<TaktRoleDto> UpdateRoleStatusAsync(TaktRoleStatusDto dto);

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色部门列表</returns>
    Task<List<TaktRoleDeptDto>> GetRoleDeptIdsAsync(long roleId);

    /// <summary>
    /// 获取角色菜单列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色菜单列表</returns>
    Task<List<TaktRoleMenuDto>> GetRoleMenuIdsAsync(long roleId);

    /// <summary>
    /// 分配角色菜单
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="menuIds">菜单ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignRoleMenusAsync(long roleId, long[] menuIds);

    /// <summary>
    /// 分配角色用户
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignRoleUsersAsync(long roleId, long[] userIds);

    /// <summary>
    /// 分配角色部门
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="deptIds">部门ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignRoleDeptsAsync(long roleId, long[] deptIds);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetRoleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入角色
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportRoleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出角色
    /// </summary>
    /// <param name="query">角色查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportRoleAsync(TaktRoleQueryDto query, string? sheetName, string? fileName);
}