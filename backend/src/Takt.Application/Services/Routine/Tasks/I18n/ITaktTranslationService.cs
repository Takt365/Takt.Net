// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.I18n
// 文件名称：ITaktTranslationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：翻译表应用服务接口（主子表），定义Translation管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.I18n;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.I18n;

/// <summary>
/// 翻译表应用服务接口（主子表）
/// </summary>
public interface ITaktTranslationService
{
    /// <summary>
    /// 获取翻译表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTranslationDto>> GetTranslationListAsync(TaktTranslationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取翻译表（包含子表数据）
    /// </summary>
    /// <param name="id">翻译表ID</param>
    /// <returns>翻译表DTO</returns>
    Task<TaktTranslationDto?> GetTranslationByIdAsync(long id);

    /// <summary>
    /// 获取翻译表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>翻译表选项列表</returns>
    Task<List<TaktSelectOption>> GetTranslationOptionsAsync();

    /// <summary>
    /// 创建翻译表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建翻译表DTO</param>
    /// <returns>翻译表DTO</returns>
    Task<TaktTranslationDto> CreateTranslationAsync(TaktTranslationCreateDto dto);

    /// <summary>
    /// 更新翻译表（包含子表数据）
    /// </summary>
    /// <param name="id">翻译表ID</param>
    /// <param name="dto">更新翻译表DTO</param>
    /// <returns>翻译表DTO</returns>
    Task<TaktTranslationDto> UpdateTranslationAsync(long id, TaktTranslationUpdateDto dto);

    /// <summary>
    /// 删除翻译表(Translation)（级联删除子表）
    /// </summary>
    /// <param name="id">翻译表(Translation)ID</param>
    /// <returns>任务</returns>
    Task DeleteTranslationByIdAsync(long id);

    /// <summary>
    /// 批量删除翻译表(Translation)（级联删除子表）
    /// </summary>
    /// <param name="ids">翻译表(Translation)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTranslationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新翻译表(Translation)排序
    /// </summary>
    /// <param name="dto">翻译表(Translation)排序DTO</param>
    /// <returns>翻译表(Translation)DTO</returns>
    Task<TaktTranslationDto> UpdateTranslationSortAsync(TaktTranslationSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTranslationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入翻译表(Translation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTranslationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出翻译表(Translation)
    /// </summary>
    /// <param name="query">翻译表(Translation)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTranslationAsync(TaktTranslationQueryDto query, string? sheetName, string? fileName);
}

