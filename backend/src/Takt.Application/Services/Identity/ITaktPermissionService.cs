// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktPermissionService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt权限应用服务接口，定义权限管理的业务操作
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt权限应用服务接口
/// </summary>
public interface ITaktPermissionService
{
    /// <summary>
    /// 获取权限列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPermissionDto>> GetListAsync(TaktPermissionQueryDto queryDto);

    /// <summary>
    /// 根据ID获取权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <returns>权限DTO</returns>
    Task<TaktPermissionDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取权限选项列表（用于下拉框等）
    /// </summary>
    /// <returns>权限选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建权限
    /// </summary>
    /// <param name="dto">创建权限DTO</param>
    /// <returns>权限DTO</returns>
    Task<TaktPermissionDto> CreateAsync(TaktPermissionCreateDto dto);

    /// <summary>
    /// 更新权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <param name="dto">更新权限DTO</param>
    /// <returns>权限DTO</returns>
    Task<TaktPermissionDto> UpdateAsync(long id, TaktPermissionUpdateDto dto);

    /// <summary>
    /// 删除权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除权限
    /// </summary>
    /// <param name="ids">权限ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新权限状态
    /// </summary>
    /// <param name="dto">权限状态DTO</param>
    /// <returns>权限DTO</returns>
    Task<TaktPermissionDto> UpdateStatusAsync(TaktPermissionStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入权限
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出权限
    /// </summary>
    /// <param name="query">权限查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktPermissionQueryDto query, string? sheetName, string? fileName);
}
