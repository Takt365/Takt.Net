// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.I18n.TransposedEngine
// 文件名称：TaktI18nEngineService.cs
// 创建时间：2026-05-04
// 创建人：Takt365(Cursor AI)
// 功能描述：国际化转置引擎服务，提供翻译转置查询和批量更新功能
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using OfficeOpenXml;
using SqlSugar;
using Takt.Application.Dtos.Routine.Tasks.I18n;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.I18n.TransposedEngine;

/// <summary>
/// 国际化转置引擎服务
/// </summary>
public class TaktTransposedEngineService : TaktServiceBase, ITaktTransposedEngineService
{
    private readonly ITaktRepository<TaktTranslation> _translationRepository;
    private readonly ITaktRepository<TaktLanguage> _languageRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="languageRepository">语言仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTransposedEngineService(
        ITaktRepository<TaktTranslation> translationRepository,
        ITaktRepository<TaktLanguage> languageRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _translationRepository = translationRepository;
        _languageRepository = languageRepository;
    }

    /// <summary>
    /// 获取转置后的翻译列表（按资源键分组，各语言为列）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>转置后的分页结果</returns>
    public async Task<TaktPagedResult<TaktTranslationTransposedDto>> GetTransposedTranslationsAsync(TaktTranslationQueryDto queryDto)
    {
        // 获取所有启用的语言（按排序）
        var languages = await _languageRepository.FindAsync(x => x.LanguageStatus == 1);
        var cultureCodeOrder = languages.OrderBy(x => x.SortOrder).Select(x => x.CultureCode).ToList();

        // 使用查询表达式获取所有翻译数据
        var allTranslations = await _translationRepository.FindAsync(QueryExpression(queryDto));

        // 按资源键分组
        var groupedByResourceKey = allTranslations
            .GroupBy(x => new { x.ResourceKey, x.ResourceType, x.ResourceGroup, x.SortOrder })
            .OrderBy(g => g.Key.SortOrder)
            .ThenBy(g => g.Key.ResourceKey);

        // 获取总数（用于分页）
        var totalCount = groupedByResourceKey.Count();

        // 分页
        var pagedGroups = groupedByResourceKey
            .Skip((queryDto.PageIndex - 1) * queryDto.PageSize)
            .Take(queryDto.PageSize)
            .ToList();

        // 转换为转置DTO
        var transposedList = new List<TaktTranslationTransposedDto>();
        foreach (var group in pagedGroups)
        {
            var transposed = new TaktTranslationTransposedDto
            {
                ResourceKey = group.Key.ResourceKey,
                ResourceType = group.Key.ResourceType,
                ResourceGroup = group.Key.ResourceGroup,
                SortOrder = group.Key.SortOrder,
                Translations = new Dictionary<string, string>()
            };

            // 填充各语言的翻译值
            foreach (var cultureCode in cultureCodeOrder)
            {
                var translation = group.FirstOrDefault(x => x.CultureCode == cultureCode);
                transposed.Translations[cultureCode] = translation?.TranslationValue ?? string.Empty;
            }

            transposedList.Add(transposed);
        }

        // 构建并返回分页结果
        return TaktPagedResult<TaktTranslationTransposedDto>.Create(
            transposedList,
            totalCount,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 批量更新翻译（转置模式）
    /// </summary>
    /// <param name="translations">转置后的翻译列表</param>
    /// <returns>更新成功的数量</returns>
    public async Task<int> BatchUpdateTranslationsAsync(List<TaktTranslationTransposedDto> translations)
    {
        if (translations == null || translations.Count == 0)
        {
            return 0;
        }

        var updateCount = 0;

        // 获取所有语言
        var languages = await _languageRepository.FindAsync(x => x.LanguageStatus == 1);
        var cultureCodes = languages.Select(x => x.CultureCode).ToList();

        foreach (var transposed in translations)
        {
            foreach (var kvp in transposed.Translations)
            {
                var cultureCode = kvp.Key;
                var translationValue = kvp.Value;

                // 跳过不存在的语言
                if (!cultureCodes.Contains(cultureCode))
                {
                    continue;
                }

                // 查找现有翻译
                var existing = await _translationRepository.FindAsync(x =>
                    x.ResourceKey == transposed.ResourceKey &&
                    x.CultureCode == cultureCode &&
                    x.IsDeleted == 0);

                if (existing.Count > 0)
                {
                    // 更新现有翻译
                    var translation = existing.First();
                    translation.TranslationValue = translationValue;
                    await _translationRepository.UpdateAsync(translation);
                    updateCount++;
                }
                else
                {
                    // 创建新翻译
                    var language = languages.FirstOrDefault(x => x.CultureCode == cultureCode);
                    if (language == null)
                    {
                        continue;
                    }

                    var newTranslation = new TaktTranslation
                    {
                        LanguageId = language.Id,
                        CultureCode = cultureCode,
                        ResourceKey = transposed.ResourceKey,
                        TranslationValue = translationValue,
                        ResourceType = transposed.ResourceType,
                        ResourceGroup = transposed.ResourceGroup,
                        SortOrder = transposed.SortOrder
                    };

                    await _translationRepository.CreateAsync(newTranslation);
                    updateCount++;
                }
            }
        }

        return updateCount;
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTranslation, bool>> QueryExpression(TaktTranslationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktTranslation>();

        // 关键词搜索（资源键或翻译值）
        if (!string.IsNullOrWhiteSpace(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ResourceKey!.Contains(queryDto.KeyWords) ||
                x.TranslationValue!.Contains(queryDto.KeyWords)
            );
        }

        // 语言ID过滤
        if (queryDto?.LanguageId.HasValue == true)
        {
            exp = exp.And(x => x.LanguageId == queryDto.LanguageId);
        }

        // 语言编码过滤
        if (!string.IsNullOrWhiteSpace(queryDto?.CultureCode))
        {
            exp = exp.And(x => x.CultureCode == queryDto.CultureCode);
        }

        // 资源键过滤
        if (!string.IsNullOrWhiteSpace(queryDto?.ResourceKey))
        {
            exp = exp.And(x => x.ResourceKey == queryDto.ResourceKey);
        }

        // 翻译值过滤
        if (!string.IsNullOrWhiteSpace(queryDto?.TranslationValue))
        {
            exp = exp.And(x => x.TranslationValue!.Contains(queryDto.TranslationValue));
        }

        // 资源类型过滤
        if (!string.IsNullOrWhiteSpace(queryDto?.ResourceType))
        {
            exp = exp.And(x => x.ResourceType == queryDto.ResourceType);
        }

        // 资源分组过滤
        if (!string.IsNullOrWhiteSpace(queryDto?.ResourceGroup))
        {
            exp = exp.And(x => x.ResourceGroup == queryDto.ResourceGroup);
        }

        // 排除已删除的记录
        exp = exp.And(x => x.IsDeleted == 0);

        return exp.ToExpression();
    }

    /// <summary>
    /// 根据资源键获取转置后的翻译
    /// </summary>
    /// <param name="resourceKey">资源键</param>
    /// <returns>转置后的翻译DTO</returns>
    public async Task<TaktTranslationTransposedDto?> GetTransposedTranslationByKeyAsync(string resourceKey)
    {
        if (string.IsNullOrWhiteSpace(resourceKey))
        {
            return null;
        }

        // 获取所有启用的语言（按排序）
        var languages = await _languageRepository.FindAsync(x => x.LanguageStatus == 1);
        var cultureCodeOrder = languages.OrderBy(x => x.SortOrder).Select(x => x.CultureCode).ToList();

        // 获取该资源键的所有翻译
        var translations = await _translationRepository.FindAsync(x =>
            x.ResourceKey == resourceKey &&
            x.IsDeleted == 0);

        if (translations == null || translations.Count == 0)
        {
            return null;
        }

        // 构建转置 DTO
        var first = translations.First();
        var transposed = new TaktTranslationTransposedDto
        {
            ResourceKey = first.ResourceKey,
            ResourceType = first.ResourceType,
            ResourceGroup = first.ResourceGroup,
            SortOrder = first.SortOrder,
            Translations = new Dictionary<string, string>()
        };

        // 填充各语言的翻译值
        foreach (var cultureCode in cultureCodeOrder)
        {
            var translation = translations.FirstOrDefault(x => x.CultureCode == cultureCode);
            transposed.Translations[cultureCode] = translation?.TranslationValue ?? string.Empty;
        }

        return transposed;
    }

    /// <summary>
    /// 创建翻译（转置模式，同时创建多个语言的翻译）
    /// </summary>
    /// <param name="transposedDto">转置后的翻译DTO</param>
    /// <returns>创建的翻译数量</returns>
    public async Task<int> CreateTransposedTranslationAsync(TaktTranslationTransposedDto transposedDto)
    {
        if (transposedDto == null || string.IsNullOrWhiteSpace(transposedDto.ResourceKey))
        {
            return 0;
        }

        var createCount = 0;
        var languages = await _languageRepository.FindAsync(x => x.LanguageStatus == 1);

        foreach (var kvp in transposedDto.Translations)
        {
            var cultureCode = kvp.Key;
            var translationValue = kvp.Value;

            // 跳过空值
            if (string.IsNullOrWhiteSpace(translationValue))
            {
                continue;
            }

            var language = languages.FirstOrDefault(x => x.CultureCode == cultureCode);
            if (language == null)
            {
                continue;
            }

            // 检查是否已存在
            var existing = await _translationRepository.FindAsync(x =>
                x.ResourceKey == transposedDto.ResourceKey &&
                x.CultureCode == cultureCode &&
                x.IsDeleted == 0);

            if (existing.Count > 0)
            {
                continue; // 已存在，跳过
            }

            // 创建新翻译
            var newTranslation = new TaktTranslation
            {
                LanguageId = language.Id,
                CultureCode = cultureCode,
                ResourceKey = transposedDto.ResourceKey,
                TranslationValue = translationValue,
                ResourceType = transposedDto.ResourceType,
                ResourceGroup = transposedDto.ResourceGroup,
                SortOrder = transposedDto.SortOrder
            };

            await _translationRepository.CreateAsync(newTranslation);
            createCount++;
        }

        return createCount;
    }

    /// <summary>
    /// 更新翻译（转置模式，同时更新多个语言的翻译）
    /// </summary>
    /// <param name="transposedDto">转置后的翻译DTO</param>
    /// <returns>更新的翻译数量</returns>
    public async Task<int> UpdateTransposedTranslationAsync(TaktTranslationTransposedDto transposedDto)
    {
        if (transposedDto == null || string.IsNullOrWhiteSpace(transposedDto.ResourceKey))
        {
            return 0;
        }

        var updateCount = 0;

        foreach (var kvp in transposedDto.Translations)
        {
            var cultureCode = kvp.Key;
            var translationValue = kvp.Value;

            // 查找现有翻译
            var existing = await _translationRepository.FindAsync(x =>
                x.ResourceKey == transposedDto.ResourceKey &&
                x.CultureCode == cultureCode &&
                x.IsDeleted == 0);

            if (existing.Count > 0)
            {
                // 更新现有翻译
                var translation = existing.First();
                translation.TranslationValue = translationValue;
                await _translationRepository.UpdateAsync(translation);
                updateCount++;
            }
        }

        return updateCount;
    }

    /// <summary>
    /// 删除翻译（根据资源键删除所有语言的翻译）
    /// </summary>
    /// <param name="resourceKey">资源键</param>
    /// <returns>删除的数量</returns>
    public async Task<int> DeleteTransposedTranslationAsync(string resourceKey)
    {
        if (string.IsNullOrWhiteSpace(resourceKey))
        {
            return 0;
        }

        var translations = await _translationRepository.FindAsync(x =>
            x.ResourceKey == resourceKey &&
            x.IsDeleted == 0);

        if (translations == null || translations.Count == 0)
        {
            return 0;
        }

        // 软删除
        foreach (var translation in translations)
        {
            translation.IsDeleted = 1;
            await _translationRepository.UpdateAsync(translation);
        }

        return translations.Count;
    }

    /// <summary>
    /// 批量删除翻译（根据资源键列表删除）
    /// </summary>
    /// <param name="resourceKeys">资源键列表</param>
    /// <returns>删除的数量</returns>
    public async Task<int> BatchDeleteTranslationsAsync(List<string> resourceKeys)
    {
        if (resourceKeys == null || resourceKeys.Count == 0)
        {
            return 0;
        }

        var deleteCount = 0;
        foreach (var resourceKey in resourceKeys)
        {
            deleteCount += await DeleteTransposedTranslationAsync(resourceKey);
        }

        return deleteCount;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>模板文件字节数组</returns>
    public async Task<byte[]> GetImportTemplateAsync(string? sheetName = null, string? fileName = null)
    {
        // 获取所有启用的语言
        var languages = await _languageRepository.FindAsync(x => x.LanguageStatus == 1);
        var cultureCodeOrder = languages.OrderBy(x => x.SortOrder).Select(x => x.CultureCode).ToList();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add(sheetName ?? "Translations");

        // 设置表头
        var col = 1;
        worksheet.Cells[1, col++].Value = "ResourceKey";
        worksheet.Cells[1, col++].Value = "ResourceType";
        worksheet.Cells[1, col++].Value = "ResourceGroup";
        worksheet.Cells[1, col++].Value = "SortOrder";

        foreach (var cultureCode in cultureCodeOrder)
        {
            worksheet.Cells[1, col++].Value = cultureCode;
        }

        // 设置表头样式
        using (var range = worksheet.Cells[1, 1, 1, col - 1])
        {
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }

        return package.GetAsByteArray();
    }

    /// <summary>
    /// 导入翻译数据（转置模式）
    /// </summary>
    /// <param name="fileBytes">文件字节数组</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>导入结果（成功数、失败数、错误信息）</returns>
    public async Task<(int Success, int Fail, List<string> Errors)> ImportTranslationsAsync(byte[] fileBytes, string? sheetName = null)
    {
        var errors = new List<string>();
        var successCount = 0;
        var failCount = 0;

        try
        {
            using var package = new ExcelPackage(new MemoryStream(fileBytes));
            var worksheet = package.Workbook.Worksheets[sheetName ?? "Translations"];

            if (worksheet == null)
            {
                errors.Add("工作表不存在");
                return (0, 0, errors);
            }

            // 读取表头
            var headers = new Dictionary<int, string>();
            for (var col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                headers[col] = worksheet.Cells[1, col].Value?.ToString() ?? string.Empty;
            }

            // 获取语言列
            var languageColumns = headers
                .Where(h => !new[] { "ResourceKey", "ResourceType", "ResourceGroup", "SortOrder" }.Contains(h.Value))
                .ToDictionary(h => h.Key, h => h.Value);

            // 读取数据行
            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var resourceKey = worksheet.Cells[row, 1].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(resourceKey))
                    {
                        continue;
                    }

                    var resourceType = worksheet.Cells[row, 2].Value?.ToString() ?? string.Empty;
                    var resourceGroup = worksheet.Cells[row, 3].Value?.ToString();
                    var sortOrderStr = worksheet.Cells[row, 4].Value?.ToString();
                    int.TryParse(sortOrderStr, out var sortOrder);

                    // 创建或更新翻译
                    var transposedDto = new TaktTranslationTransposedDto
                    {
                        ResourceKey = resourceKey,
                        ResourceType = resourceType,
                        ResourceGroup = resourceGroup,
                        SortOrder = sortOrder,
                        Translations = new Dictionary<string, string>()
                    };

                    foreach (var langCol in languageColumns)
                    {
                        var value = worksheet.Cells[row, langCol.Key].Value?.ToString();
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            transposedDto.Translations[langCol.Value] = value;
                        }
                    }

                    // 先尝试更新，如果不存在则创建
                    var updateCount = await UpdateTransposedTranslationAsync(transposedDto);
                    if (updateCount == 0)
                    {
                        await CreateTransposedTranslationAsync(transposedDto);
                    }

                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errors.Add($"行 {row}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            errors.Add($"导入失败: {ex.Message}");
        }

        return (successCount, failCount, errors);
    }

    /// <summary>
    /// 导出翻译数据（转置模式）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>导出文件字节数组</returns>
    public async Task<byte[]> ExportTranslationsAsync(TaktTranslationQueryDto queryDto, string? sheetName = null, string? fileName = null)
    {
        // 获取转置后的数据（不分页，导出全部）
        queryDto.PageIndex = 1;
        queryDto.PageSize = int.MaxValue;
        var result = await GetTransposedTranslationsAsync(queryDto);

        // 获取所有启用的语言
        var languages = await _languageRepository.FindAsync(x => x.LanguageStatus == 1);
        var cultureCodeOrder = languages.OrderBy(x => x.SortOrder).Select(x => x.CultureCode).ToList();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add(sheetName ?? "Translations");

        // 设置表头
        var col = 1;
        worksheet.Cells[1, col++].Value = "ResourceKey";
        worksheet.Cells[1, col++].Value = "ResourceType";
        worksheet.Cells[1, col++].Value = "ResourceGroup";
        worksheet.Cells[1, col++].Value = "SortOrder";

        foreach (var cultureCode in cultureCodeOrder)
        {
            worksheet.Cells[1, col++].Value = cultureCode;
        }

        // 设置表头样式
        using (var range = worksheet.Cells[1, 1, 1, col - 1])
        {
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }

        // 填充数据
        var row = 2;
        foreach (var transposed in result.Data)
        {
            col = 1;
            worksheet.Cells[row, col++].Value = transposed.ResourceKey;
            worksheet.Cells[row, col++].Value = transposed.ResourceType;
            worksheet.Cells[row, col++].Value = transposed.ResourceGroup;
            worksheet.Cells[row, col++].Value = transposed.SortOrder;

            foreach (var cultureCode in cultureCodeOrder)
            {
                worksheet.Cells[row, col++].Value = transposed.Translations.TryGetValue(cultureCode, out var value) ? value : string.Empty;
            }

            row++;
        }

        // 自动调整列宽
        worksheet.Cells.AutoFitColumns();

        return package.GetAsByteArray();
    }

    /// <summary>
    /// 获取翻译选项列表（用于下拉框等）
    /// </summary>
    /// <returns>翻译选项列表</returns>
    public async Task<List<TaktSelectOption>> GetI8nOptionsAsync()
    {
        var translations = await _translationRepository.FindAsync(t => t.IsDeleted == 0);
        return translations
            .OrderBy(t => t.SortOrder)
            .ThenBy(t => t.CreatedAt)
            .Select(t => new TaktSelectOption
            {
                DictLabel = t.TranslationValue,
                DictValue = t.ResourceKey,
                ExtLabel = t.CultureCode,
                ExtValue = t.ResourceType,
                SortOrder = t.SortOrder
            })
            .ToList();
    }
}
