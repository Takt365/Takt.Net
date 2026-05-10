// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：ITaktKnowledgeService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：知识库表应用服务接口（主子表），定义Knowledge管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 知识库表应用服务接口（主子表）
/// </summary>
public interface ITaktKnowledgeService
{
    /// <summary>
    /// 获取知识库表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktKnowledgeDto>> GetKnowledgeListAsync(TaktKnowledgeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取知识库表（包含子表数据）
    /// </summary>
    /// <param name="id">知识库表ID</param>
    /// <returns>知识库表DTO</returns>
    Task<TaktKnowledgeDto?> GetKnowledgeByIdAsync(long id);

    /// <summary>
    /// 获取知识库表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>知识库表选项列表</returns>
    Task<List<TaktSelectOption>> GetKnowledgeOptionsAsync();

    /// <summary>
    /// 创建知识库表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建知识库表DTO</param>
    /// <returns>知识库表DTO</returns>
    Task<TaktKnowledgeDto> CreateKnowledgeAsync(TaktKnowledgeCreateDto dto);

    /// <summary>
    /// 更新知识库表（包含子表数据）
    /// </summary>
    /// <param name="id">知识库表ID</param>
    /// <param name="dto">更新知识库表DTO</param>
    /// <returns>知识库表DTO</returns>
    Task<TaktKnowledgeDto> UpdateKnowledgeAsync(long id, TaktKnowledgeUpdateDto dto);

    /// <summary>
    /// 删除知识库表(Knowledge)（级联删除子表）
    /// </summary>
    /// <param name="id">知识库表(Knowledge)ID</param>
    /// <returns>任务</returns>
    Task DeleteKnowledgeByIdAsync(long id);

    /// <summary>
    /// 批量删除知识库表(Knowledge)（级联删除子表）
    /// </summary>
    /// <param name="ids">知识库表(Knowledge)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteKnowledgeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新知识库表(Knowledge)Status
    /// </summary>
    /// <param name="dto">知识库表(Knowledge)StatusDTO</param>
    /// <returns>知识库表(Knowledge)DTO</returns>
    Task<TaktKnowledgeDto> UpdateKnowledgeStatusAsync(TaktKnowledgeStatusDto dto);

    /// <summary>
    /// 更新知识库表(Knowledge)排序
    /// </summary>
    /// <param name="dto">知识库表(Knowledge)排序DTO</param>
    /// <returns>知识库表(Knowledge)DTO</returns>
    Task<TaktKnowledgeDto> UpdateKnowledgeSortAsync(TaktKnowledgeSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetKnowledgeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入知识库表(Knowledge)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportKnowledgeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出知识库表(Knowledge)
    /// </summary>
    /// <param name="query">知识库表(Knowledge)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportKnowledgeAsync(TaktKnowledgeQueryDto query, string? sheetName, string? fileName);
}

