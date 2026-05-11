// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.News
// 文件名称：ITaktNewsLikeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：新闻点赞记录表应用服务接口（主子表），定义NewsLike管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.News;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.News;

/// <summary>
/// 新闻点赞记录表应用服务接口（主子表）
/// </summary>
public interface ITaktNewsLikeService
{
    /// <summary>
    /// 获取新闻点赞记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktNewsLikeDto>> GetNewsLikeListAsync(TaktNewsLikeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取新闻点赞记录表（包含子表数据）
    /// </summary>
    /// <param name="id">新闻点赞记录表ID</param>
    /// <returns>新闻点赞记录表DTO</returns>
    Task<TaktNewsLikeDto?> GetNewsLikeByIdAsync(long id);

    /// <summary>
    /// 获取新闻点赞记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>新闻点赞记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetNewsLikeOptionsAsync();

    /// <summary>
    /// 创建新闻点赞记录表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建新闻点赞记录表DTO</param>
    /// <returns>新闻点赞记录表DTO</returns>
    Task<TaktNewsLikeDto> CreateNewsLikeAsync(TaktNewsLikeCreateDto dto);

    /// <summary>
    /// 更新新闻点赞记录表（包含子表数据）
    /// </summary>
    /// <param name="id">新闻点赞记录表ID</param>
    /// <param name="dto">更新新闻点赞记录表DTO</param>
    /// <returns>新闻点赞记录表DTO</returns>
    Task<TaktNewsLikeDto> UpdateNewsLikeAsync(long id, TaktNewsLikeUpdateDto dto);

    /// <summary>
    /// 删除新闻点赞记录表(NewsLike)（级联删除子表）
    /// </summary>
    /// <param name="id">新闻点赞记录表(NewsLike)ID</param>
    /// <returns>任务</returns>
    Task DeleteNewsLikeByIdAsync(long id);

    /// <summary>
    /// 批量删除新闻点赞记录表(NewsLike)（级联删除子表）
    /// </summary>
    /// <param name="ids">新闻点赞记录表(NewsLike)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteNewsLikeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetNewsLikeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportNewsLikeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="query">新闻点赞记录表(NewsLike)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportNewsLikeAsync(TaktNewsLikeQueryDto query, string? sheetName, string? fileName);
}

