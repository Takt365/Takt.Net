// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.I18n.TransposedEngine
// 文件名称：ITaktI18nEngineService.cs
// 创建时间：2026-05-04
// 创建人：Takt365
// 功能描述：国际化转置引擎服务接口，提供翻译转置查询和批量更新功能
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.I18n;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.I18n.TransposedEngine;

/// <summary>
/// 国际化转置引擎服务接口
/// </summary>
public interface ITaktTransposedEngineService
{
    /// <summary>
    /// 获取转置后的翻译列表（按资源键分组，各语言为列）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>转置后的分页结果</returns>
    Task<TaktPagedResult<TaktTranslationTransposedDto>> GetTransposedTranslationsAsync(TaktTranslationQueryDto queryDto);

    /// <summary>
    /// 根据资源键获取转置后的翻译
    /// </summary>
    /// <param name="resourceKey">资源键</param>
    /// <returns>转置后的翻译DTO</returns>
    Task<TaktTranslationTransposedDto?> GetTransposedTranslationByKeyAsync(string resourceKey);

    /// <summary>
    /// 创建翻译（转置模式，同时创建多个语言的翻译）
    /// </summary>
    /// <param name="transposedDto">转置后的翻译DTO</param>
    /// <returns>创建的翻译数量</returns>
    Task<int> CreateTransposedTranslationAsync(TaktTranslationTransposedDto transposedDto);

    /// <summary>
    /// 更新翻译（转置模式，同时更新多个语言的翻译）
    /// </summary>
    /// <param name="transposedDto">转置后的翻译DTO</param>
    /// <returns>更新的翻译数量</returns>
    Task<int> UpdateTransposedTranslationAsync(TaktTranslationTransposedDto transposedDto);

    /// <summary>
    /// 删除翻译（根据资源键删除所有语言的翻译）
    /// </summary>
    /// <param name="resourceKey">资源键</param>
    /// <returns>删除的数量</returns>
    Task<int> DeleteTransposedTranslationAsync(string resourceKey);

    /// <summary>
    /// 批量删除翻译（根据资源键列表删除）
    /// </summary>
    /// <param name="resourceKeys">资源键列表</param>
    /// <returns>删除的数量</returns>
    Task<int> BatchDeleteTranslationsAsync(List<string> resourceKeys);

    /// <summary>
    /// 批量更新翻译（转置模式）
    /// </summary>
    /// <param name="translations">转置后的翻译列表</param>
    /// <returns>更新成功的数量</returns>
    Task<int> BatchUpdateTranslationsAsync(List<TaktTranslationTransposedDto> translations);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>模板文件字节数组</returns>
    Task<byte[]> GetImportTemplateAsync(string? sheetName = null, string? fileName = null);

    /// <summary>
    /// 导入翻译数据（转置模式）
    /// </summary>
    /// <param name="fileBytes">文件字节数组</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>导入结果（成功数、失败数、错误信息）</returns>
    Task<(int Success, int Fail, List<string> Errors)> ImportTranslationsAsync(byte[] fileBytes, string? sheetName = null);

    /// <summary>
    /// 导出翻译数据（转置模式）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>导出文件字节数组</returns>
    Task<byte[]> ExportTranslationsAsync(TaktTranslationQueryDto queryDto, string? sheetName = null, string? fileName = null);

    /// <summary>
    /// 获取翻译选项列表（用于下拉框等）
    /// </summary>
    /// <returns>翻译选项列表</returns>
    Task<List<Takt.Shared.Models.TaktSelectOption>> GetI8nOptionsAsync();
}
