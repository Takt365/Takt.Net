// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktRoleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：角色信息表应用服务接口，定义Role管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 角色信息表应用服务接口
/// </summary>
public interface ITaktRoleService
{
    /// <summary>
    /// 获取角色信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktRoleDto>> GetRoleListAsync(TaktRoleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取角色信息表
    /// </summary>
    /// <param name="id">角色信息表ID</param>
    /// <returns>角色信息表DTO</returns>
    Task<TaktRoleDto?> GetRoleByIdAsync(long id);

    /// <summary>
    /// 获取角色信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>角色信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetRoleOptionsAsync();

    /// <summary>
    /// 创建角色信息表
    /// </summary>
    /// <param name="dto">创建角色信息表DTO</param>
    /// <returns>角色信息表DTO</returns>
    Task<TaktRoleDto> CreateRoleAsync(TaktRoleCreateDto dto);

    /// <summary>
    /// 更新角色信息表
    /// </summary>
    /// <param name="id">角色信息表ID</param>
    /// <param name="dto">更新角色信息表DTO</param>
    /// <returns>角色信息表DTO</returns>
    Task<TaktRoleDto> UpdateRoleAsync(long id, TaktRoleUpdateDto dto);

    /// <summary>
    /// 删除角色信息表(Role)
    /// </summary>
    /// <param name="id">角色信息表(Role)ID</param>
    /// <returns>任务</returns>
    Task DeleteRoleByIdAsync(long id);

    /// <summary>
    /// 批量删除角色信息表(Role)
    /// </summary>
    /// <param name="ids">角色信息表(Role)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteRoleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新角色信息表(Role)Status
    /// </summary>
    /// <param name="dto">角色信息表(Role)StatusDTO</param>
    /// <returns>角色信息表(Role)DTO</returns>
    Task<TaktRoleDto> UpdateRoleStatusAsync(TaktRoleStatusDto dto);

    /// <summary>
    /// 更新角色信息表(Role)排序
    /// </summary>
    /// <param name="dto">角色信息表(Role)排序DTO</param>
    /// <returns>角色信息表(Role)DTO</returns>
    Task<TaktRoleDto> UpdateRoleSortAsync(TaktRoleSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetRoleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入角色信息表(Role)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportRoleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出角色信息表(Role)
    /// </summary>
    /// <param name="query">角色信息表(Role)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportRoleAsync(TaktRoleQueryDto query, string? sheetName, string? fileName);
}

