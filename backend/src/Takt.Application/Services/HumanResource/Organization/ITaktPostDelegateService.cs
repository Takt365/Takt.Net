// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：ITaktPostDelegateService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：岗位代理表应用服务接口，定义PostDelegate管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 岗位代理表应用服务接口
/// </summary>
public interface ITaktPostDelegateService
{
    /// <summary>
    /// 获取岗位代理表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPostDelegateDto>> GetPostDelegateListAsync(TaktPostDelegateQueryDto queryDto);

    /// <summary>
    /// 根据ID获取岗位代理表
    /// </summary>
    /// <param name="id">岗位代理表ID</param>
    /// <returns>岗位代理表DTO</returns>
    Task<TaktPostDelegateDto?> GetPostDelegateByIdAsync(long id);

    /// <summary>
    /// 获取岗位代理表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位代理表选项列表</returns>
    Task<List<TaktSelectOption>> GetPostDelegateOptionsAsync();

    /// <summary>
    /// 创建岗位代理表
    /// </summary>
    /// <param name="dto">创建岗位代理表DTO</param>
    /// <returns>岗位代理表DTO</returns>
    Task<TaktPostDelegateDto> CreatePostDelegateAsync(TaktPostDelegateCreateDto dto);

    /// <summary>
    /// 更新岗位代理表
    /// </summary>
    /// <param name="id">岗位代理表ID</param>
    /// <param name="dto">更新岗位代理表DTO</param>
    /// <returns>岗位代理表DTO</returns>
    Task<TaktPostDelegateDto> UpdatePostDelegateAsync(long id, TaktPostDelegateUpdateDto dto);

    /// <summary>
    /// 删除岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="id">岗位代理表(PostDelegate)ID</param>
    /// <returns>任务</returns>
    Task DeletePostDelegateByIdAsync(long id);

    /// <summary>
    /// 批量删除岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="ids">岗位代理表(PostDelegate)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePostDelegateBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新岗位代理表(PostDelegate)排序
    /// </summary>
    /// <param name="dto">岗位代理表(PostDelegate)排序DTO</param>
    /// <returns>岗位代理表(PostDelegate)DTO</returns>
    Task<TaktPostDelegateDto> UpdatePostDelegateSortAsync(TaktPostDelegateSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPostDelegateTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPostDelegateAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="query">岗位代理表(PostDelegate)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPostDelegateAsync(TaktPostDelegateQueryDto query, string? sheetName, string? fileName);
}

