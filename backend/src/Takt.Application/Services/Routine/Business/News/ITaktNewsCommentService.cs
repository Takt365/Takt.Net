// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.News
// 文件名称：ITaktNewsCommentService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：新闻评论表应用服务接口（主子表），定义NewsComment管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.News;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.News;

/// <summary>
/// 新闻评论表应用服务接口（主子表）
/// </summary>
public interface ITaktNewsCommentService
{
    /// <summary>
    /// 获取新闻评论表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktNewsCommentDto>> GetNewsCommentListAsync(TaktNewsCommentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取新闻评论表（包含子表数据）
    /// </summary>
    /// <param name="id">新闻评论表ID</param>
    /// <returns>新闻评论表DTO</returns>
    Task<TaktNewsCommentDto?> GetNewsCommentByIdAsync(long id);

    /// <summary>
    /// 获取新闻评论表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>新闻评论表选项列表</returns>
    Task<List<TaktSelectOption>> GetNewsCommentOptionsAsync();

    // ==================== 树形服务 ====================

    /// <summary>
    /// 获取NewsComment树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>NewsComment树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetNewsCommentTreeOptionsAsync();

    /// <summary>
    /// 获取NewsComment树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的新闻评论表（默认false）</param>
    /// <returns>NewsComment树形列表</returns>
    Task<List<TaktNewsCommentTreeDto>> GetNewsCommentTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取NewsComment子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的新闻评论表（默认false）</param>
    /// <returns>NewsComment子节点列表</returns>
    Task<List<TaktNewsCommentDto>> GetNewsCommentChildrenAsync(long parentId, bool includeDisabled = false);

    // ==================== 树形服务 ====================

    /// <summary>
    /// 创建新闻评论表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建新闻评论表DTO</param>
    /// <returns>新闻评论表DTO</returns>
    Task<TaktNewsCommentDto> CreateNewsCommentAsync(TaktNewsCommentCreateDto dto);

    /// <summary>
    /// 更新新闻评论表（包含子表数据）
    /// </summary>
    /// <param name="id">新闻评论表ID</param>
    /// <param name="dto">更新新闻评论表DTO</param>
    /// <returns>新闻评论表DTO</returns>
    Task<TaktNewsCommentDto> UpdateNewsCommentAsync(long id, TaktNewsCommentUpdateDto dto);

    /// <summary>
    /// 删除新闻评论表(NewsComment)（级联删除子表）
    /// </summary>
    /// <param name="id">新闻评论表(NewsComment)ID</param>
    /// <returns>任务</returns>
    Task DeleteNewsCommentByIdAsync(long id);

    /// <summary>
    /// 批量删除新闻评论表(NewsComment)（级联删除子表）
    /// </summary>
    /// <param name="ids">新闻评论表(NewsComment)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteNewsCommentBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新新闻评论表(NewsComment)ApprovalStatus
    /// </summary>
    /// <param name="dto">新闻评论表(NewsComment)ApprovalStatusDTO</param>
    /// <returns>新闻评论表(NewsComment)DTO</returns>
    Task<TaktNewsCommentDto> UpdateNewsCommentApprovalStatusAsync(TaktNewsCommentApprovalStatusDto dto);

    /// <summary>
    /// 更新新闻评论表(NewsComment)CommentStatus
    /// </summary>
    /// <param name="dto">新闻评论表(NewsComment)CommentStatusDTO</param>
    /// <returns>新闻评论表(NewsComment)DTO</returns>
    Task<TaktNewsCommentDto> UpdateNewsCommentCommentStatusAsync(TaktNewsCommentCommentStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetNewsCommentTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入新闻评论表(NewsComment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportNewsCommentAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出新闻评论表(NewsComment)
    /// </summary>
    /// <param name="query">新闻评论表(NewsComment)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportNewsCommentAsync(TaktNewsCommentQueryDto query, string? sheetName, string? fileName);
}

