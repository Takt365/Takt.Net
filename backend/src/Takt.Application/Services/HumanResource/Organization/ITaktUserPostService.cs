// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：ITaktUserPostService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：岗位用户关联表应用服务接口，定义UserPost管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 岗位用户关联表应用服务接口
/// </summary>
public interface ITaktUserPostService
{
    /// <summary>
    /// 获取岗位用户关联表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktUserPostDto>> GetUserPostListAsync(TaktUserPostQueryDto queryDto);

    /// <summary>
    /// 根据ID获取岗位用户关联表
    /// </summary>
    /// <param name="id">岗位用户关联表ID</param>
    /// <returns>岗位用户关联表DTO</returns>
    Task<TaktUserPostDto?> GetUserPostByIdAsync(long id);

    /// <summary>
    /// 获取岗位用户关联表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位用户关联表选项列表</returns>
    Task<List<TaktSelectOption>> GetUserPostOptionsAsync();

    /// <summary>
    /// 创建岗位用户关联表
    /// </summary>
    /// <param name="dto">创建岗位用户关联表DTO</param>
    /// <returns>岗位用户关联表DTO</returns>
    Task<TaktUserPostDto> CreateUserPostAsync(TaktUserPostCreateDto dto);

    /// <summary>
    /// 更新岗位用户关联表
    /// </summary>
    /// <param name="id">岗位用户关联表ID</param>
    /// <param name="dto">更新岗位用户关联表DTO</param>
    /// <returns>岗位用户关联表DTO</returns>
    Task<TaktUserPostDto> UpdateUserPostAsync(long id, TaktUserPostUpdateDto dto);

    /// <summary>
    /// 删除岗位用户关联表(UserPost)
    /// </summary>
    /// <param name="id">岗位用户关联表(UserPost)ID</param>
    /// <returns>任务</returns>
    Task DeleteUserPostByIdAsync(long id);

    /// <summary>
    /// 批量删除岗位用户关联表(UserPost)
    /// </summary>
    /// <param name="ids">岗位用户关联表(UserPost)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteUserPostBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetUserPostTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入岗位用户关联表(UserPost)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportUserPostAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出岗位用户关联表(UserPost)
    /// </summary>
    /// <param name="query">岗位用户关联表(UserPost)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportUserPostAsync(TaktUserPostQueryDto query, string? sheetName, string? fileName);
}

