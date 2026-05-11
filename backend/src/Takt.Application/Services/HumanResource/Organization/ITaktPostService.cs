// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：ITaktPostService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：岗位信息表应用服务接口（主子表），定义Post管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 岗位信息表应用服务接口（主子表）
/// </summary>
public interface ITaktPostService
{
    /// <summary>
    /// 获取岗位信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPostDto>> GetPostListAsync(TaktPostQueryDto queryDto);

    /// <summary>
    /// 根据ID获取岗位信息表（包含子表数据）
    /// </summary>
    /// <param name="id">岗位信息表ID</param>
    /// <returns>岗位信息表DTO</returns>
    Task<TaktPostDto?> GetPostByIdAsync(long id);

    /// <summary>
    /// 获取岗位信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetPostOptionsAsync();

    /// <summary>
    /// 创建岗位信息表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建岗位信息表DTO</param>
    /// <returns>岗位信息表DTO</returns>
    Task<TaktPostDto> CreatePostAsync(TaktPostCreateDto dto);

    /// <summary>
    /// 更新岗位信息表（包含子表数据）
    /// </summary>
    /// <param name="id">岗位信息表ID</param>
    /// <param name="dto">更新岗位信息表DTO</param>
    /// <returns>岗位信息表DTO</returns>
    Task<TaktPostDto> UpdatePostAsync(long id, TaktPostUpdateDto dto);

    /// <summary>
    /// 删除岗位信息表(Post)（级联删除子表）
    /// </summary>
    /// <param name="id">岗位信息表(Post)ID</param>
    /// <returns>任务</returns>
    Task DeletePostByIdAsync(long id);

    /// <summary>
    /// 批量删除岗位信息表(Post)（级联删除子表）
    /// </summary>
    /// <param name="ids">岗位信息表(Post)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePostBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新岗位信息表(Post)Status
    /// </summary>
    /// <param name="dto">岗位信息表(Post)StatusDTO</param>
    /// <returns>岗位信息表(Post)DTO</returns>
    Task<TaktPostDto> UpdatePostStatusAsync(TaktPostStatusDto dto);

    /// <summary>
    /// 更新岗位信息表(Post)排序
    /// </summary>
    /// <param name="dto">岗位信息表(Post)排序DTO</param>
    /// <returns>岗位信息表(Post)DTO</returns>
    Task<TaktPostDto> UpdatePostSortAsync(TaktPostSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPostTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入岗位信息表(Post)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPostAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出岗位信息表(Post)
    /// </summary>
    /// <param name="query">岗位信息表(Post)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPostAsync(TaktPostQueryDto query, string? sheetName, string? fileName);
}

