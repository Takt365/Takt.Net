// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.News
// 文件名称：ITaktNewsService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：新闻表应用服务接口（主子表），定义News管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.News;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.News;

/// <summary>
/// 新闻表应用服务接口（主子表）
/// </summary>
public interface ITaktNewsService
{
    /// <summary>
    /// 获取新闻表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktNewsDto>> GetNewsListAsync(TaktNewsQueryDto queryDto);

    /// <summary>
    /// 根据ID获取新闻表（包含子表数据）
    /// </summary>
    /// <param name="id">新闻表ID</param>
    /// <returns>新闻表DTO</returns>
    Task<TaktNewsDto?> GetNewsByIdAsync(long id);

    /// <summary>
    /// 获取新闻表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>新闻表选项列表</returns>
    Task<List<TaktSelectOption>> GetNewsOptionsAsync();

    /// <summary>
    /// 创建新闻表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建新闻表DTO</param>
    /// <returns>新闻表DTO</returns>
    Task<TaktNewsDto> CreateNewsAsync(TaktNewsCreateDto dto);

    /// <summary>
    /// 更新新闻表（包含子表数据）
    /// </summary>
    /// <param name="id">新闻表ID</param>
    /// <param name="dto">更新新闻表DTO</param>
    /// <returns>新闻表DTO</returns>
    Task<TaktNewsDto> UpdateNewsAsync(long id, TaktNewsUpdateDto dto);

    /// <summary>
    /// 删除新闻表(News)（级联删除子表）
    /// </summary>
    /// <param name="id">新闻表(News)ID</param>
    /// <returns>任务</returns>
    Task DeleteNewsByIdAsync(long id);

    /// <summary>
    /// 批量删除新闻表(News)（级联删除子表）
    /// </summary>
    /// <param name="ids">新闻表(News)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteNewsBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新新闻表(News)Status
    /// </summary>
    /// <param name="dto">新闻表(News)StatusDTO</param>
    /// <returns>新闻表(News)DTO</returns>
    Task<TaktNewsDto> UpdateNewsStatusAsync(TaktNewsStatusDto dto);

    /// <summary>
    /// 更新新闻表(News)排序
    /// </summary>
    /// <param name="dto">新闻表(News)排序DTO</param>
    /// <returns>新闻表(News)DTO</returns>
    Task<TaktNewsDto> UpdateNewsSortAsync(TaktNewsSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetNewsTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入新闻表(News)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportNewsAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出新闻表(News)
    /// </summary>
    /// <param name="query">新闻表(News)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportNewsAsync(TaktNewsQueryDto query, string? sheetName, string? fileName);
}

