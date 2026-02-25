// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Organization
// 文件名称：ITaktDeptService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门应用服务接口，定义部门管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// Takt部门应用服务接口.
/// </summary>
public interface ITaktDeptService
{
    /// <summary>
    /// 获取部门列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktDeptDto>> GetListAsync(TaktDeptQueryDto queryDto);

    /// <summary>
    /// 根据ID获取部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>部门DTO</returns>
    Task<TaktDeptDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取部门树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>部门树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync();

    /// <summary>
    /// 获取部门树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门树形列表</returns>
    Task<List<TaktDeptTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取部门子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门子节点列表</returns>
    Task<List<TaktDeptDto>> GetChildrenAsync(long parentId, bool includeDisabled = false);

    /// <summary>
    /// 创建部门
    /// </summary>
    /// <param name="dto">创建部门DTO</param>
    /// <returns>部门DTO</returns>
    Task<TaktDeptDto> CreateAsync(TaktDeptCreateDto dto);

    /// <summary>
    /// 更新部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <param name="dto">更新部门DTO</param>
    /// <returns>部门DTO</returns>
    Task<TaktDeptDto> UpdateAsync(long id, TaktDeptUpdateDto dto);

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除部门
    /// </summary>
    /// <param name="ids">部门ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新部门状态
    /// </summary>
    /// <param name="dto">部门状态DTO</param>
    /// <returns>部门DTO</returns>
    Task<TaktDeptDto> UpdateStatusAsync(TaktDeptStatusDto dto);

    /// <summary>
    /// 获取部门用户列表
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <returns>部门用户列表</returns>
    Task<List<TaktUserDeptDto>> GetUserDeptIdsAsync(long deptId);

    /// <summary>
    /// 分配用户部门
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="userIds">用户ID数组</param>
    /// <returns>是否分配成功</returns>
    Task<bool> AssignUserDeptsAsync(long deptId, long[] userIds);

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>角色部门分页列表</returns>
    Task<TaktPagedResult<TaktRoleDeptDto>> GetRoleDeptIdsAsync(long deptId, TaktRoleQueryDto query);

    /// <summary>
    /// 分配角色部门
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="roleIds">角色ID数组</param>
    /// <returns>是否分配成功</returns>
    Task<bool> AssignRoleDeptsAsync(long deptId, long[] roleIds);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入部门
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出部门
    /// </summary>
    /// <param name="query">部门查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktDeptQueryDto query, string? sheetName, string? fileName);
}