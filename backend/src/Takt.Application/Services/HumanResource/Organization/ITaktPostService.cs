// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Organization
// 文件名称：ITaktPostService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt岗位应用服务接口，定义岗位管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// Takt岗位应用服务接口
/// </summary>
public interface ITaktPostService
{
    /// <summary>
    /// 获取岗位列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPostDto>> GetListAsync(TaktPostQueryDto queryDto);

    /// <summary>
    /// 根据ID获取岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <returns>岗位DTO</returns>
    Task<TaktPostDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取岗位选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建岗位
    /// </summary>
    /// <param name="dto">创建岗位DTO</param>
    /// <returns>岗位DTO</returns>
    Task<TaktPostDto> CreateAsync(TaktPostCreateDto dto);

    /// <summary>
    /// 更新岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <param name="dto">更新岗位DTO</param>
    /// <returns>岗位DTO</returns>
    Task<TaktPostDto> UpdateAsync(long id, TaktPostUpdateDto dto);

    /// <summary>
    /// 删除岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除岗位
    /// </summary>
    /// <param name="ids">岗位ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新岗位状态
    /// </summary>
    /// <param name="dto">岗位状态DTO</param>
    /// <returns>岗位DTO</returns>
    Task<TaktPostDto> UpdateStatusAsync(TaktPostStatusDto dto);

    /// <summary>
    /// 获取岗位用户列表
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <returns>岗位用户列表</returns>
    Task<List<TaktUserPostDto>> GetUserPostIdsAsync(long postId);

    /// <summary>
    /// 分配用户岗位
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <param name="userIds">用户ID集合</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignUserPostsAsync(long postId, long[] userIds);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入岗位
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出岗位
    /// </summary>
    /// <param name="query">岗位查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktPostQueryDto query, string? sheetName, string? fileName);
}