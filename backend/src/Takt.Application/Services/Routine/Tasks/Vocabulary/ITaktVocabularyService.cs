// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.Vocabulary
// 文件名称：ITaktVocabularyService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：敏感词表应用服务接口，定义Vocabulary管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Vocabulary;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Vocabulary;

/// <summary>
/// 敏感词表应用服务接口
/// </summary>
public interface ITaktVocabularyService
{
    /// <summary>
    /// 获取敏感词表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktVocabularyDto>> GetVocabularyListAsync(TaktVocabularyQueryDto queryDto);

    /// <summary>
    /// 根据ID获取敏感词表
    /// </summary>
    /// <param name="id">敏感词表ID</param>
    /// <returns>敏感词表DTO</returns>
    Task<TaktVocabularyDto?> GetVocabularyByIdAsync(long id);

    /// <summary>
    /// 获取敏感词表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>敏感词表选项列表</returns>
    Task<List<TaktSelectOption>> GetVocabularyOptionsAsync();

    /// <summary>
    /// 创建敏感词表
    /// </summary>
    /// <param name="dto">创建敏感词表DTO</param>
    /// <returns>敏感词表DTO</returns>
    Task<TaktVocabularyDto> CreateVocabularyAsync(TaktVocabularyCreateDto dto);

    /// <summary>
    /// 更新敏感词表
    /// </summary>
    /// <param name="id">敏感词表ID</param>
    /// <param name="dto">更新敏感词表DTO</param>
    /// <returns>敏感词表DTO</returns>
    Task<TaktVocabularyDto> UpdateVocabularyAsync(long id, TaktVocabularyUpdateDto dto);

    /// <summary>
    /// 删除敏感词表(Vocabulary)
    /// </summary>
    /// <param name="id">敏感词表(Vocabulary)ID</param>
    /// <returns>任务</returns>
    Task DeleteVocabularyByIdAsync(long id);

    /// <summary>
    /// 批量删除敏感词表(Vocabulary)
    /// </summary>
    /// <param name="ids">敏感词表(Vocabulary)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteVocabularyBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新敏感词表(Vocabulary)Status
    /// </summary>
    /// <param name="dto">敏感词表(Vocabulary)StatusDTO</param>
    /// <returns>敏感词表(Vocabulary)DTO</returns>
    Task<TaktVocabularyDto> UpdateVocabularyStatusAsync(TaktVocabularyStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetVocabularyTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入敏感词表(Vocabulary)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportVocabularyAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出敏感词表(Vocabulary)
    /// </summary>
    /// <param name="query">敏感词表(Vocabulary)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportVocabularyAsync(TaktVocabularyQueryDto query, string? sheetName, string? fileName);
}

