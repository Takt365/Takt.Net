// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：ITaktKnowledgeChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：知识库变更日志表应用服务接口（主子表），定义KnowledgeChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 知识库变更日志表应用服务接口（主子表）
/// </summary>
public interface ITaktKnowledgeChangeLogService
{
    /// <summary>
    /// 获取知识库变更日志表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktKnowledgeChangeLogDto>> GetKnowledgeChangeLogListAsync(TaktKnowledgeChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取知识库变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">知识库变更日志表ID</param>
    /// <returns>知识库变更日志表DTO</returns>
    Task<TaktKnowledgeChangeLogDto?> GetKnowledgeChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取知识库变更日志表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>知识库变更日志表选项列表</returns>
    Task<List<TaktSelectOption>> GetKnowledgeChangeLogOptionsAsync();

    /// <summary>
    /// 创建知识库变更日志表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建知识库变更日志表DTO</param>
    /// <returns>知识库变更日志表DTO</returns>
    Task<TaktKnowledgeChangeLogDto> CreateKnowledgeChangeLogAsync(TaktKnowledgeChangeLogCreateDto dto);

    /// <summary>
    /// 更新知识库变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">知识库变更日志表ID</param>
    /// <param name="dto">更新知识库变更日志表DTO</param>
    /// <returns>知识库变更日志表DTO</returns>
    Task<TaktKnowledgeChangeLogDto> UpdateKnowledgeChangeLogAsync(long id, TaktKnowledgeChangeLogUpdateDto dto);

    /// <summary>
    /// 删除知识库变更日志表(KnowledgeChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="id">知识库变更日志表(KnowledgeChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteKnowledgeChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除知识库变更日志表(KnowledgeChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="ids">知识库变更日志表(KnowledgeChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteKnowledgeChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetKnowledgeChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportKnowledgeChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="query">知识库变更日志表(KnowledgeChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportKnowledgeChangeLogAsync(TaktKnowledgeChangeLogQueryDto query, string? sheetName, string? fileName);
}

