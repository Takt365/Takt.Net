// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDictI18nSeedData.cs
// 创建时间：2026-04-07
// 创建人：Takt365(Cursor AI)
// 功能描述：基于 DictL10nKey 初始化字典翻译，覆盖所有字典项与所有启用语言
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.Dict;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// 字典翻译种子数据（按 DictL10nKey 生成 translation）
/// </summary>
public class TaktDictI18nSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（字典数据之后）
    /// </summary>
    public int Order => 102;

    /// <summary>
    /// 初始化字典翻译种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();
        var dictDataRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDictData>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
            {
                return (0, 0);
            }

            var dictDataList = await dictDataRepository.FindAsync(d =>
                d.IsDeleted == 0 &&
                d.DictL10nKey != null &&
                d.DictL10nKey != string.Empty);

            if (dictDataList == null || dictDataList.Count == 0)
            {
                return (0, 0);
            }

            // 按键去重，避免重复键产生重复翻译写入。
            var dictItems = dictDataList
                .OrderBy(d => d.SortOrder)
                .ThenBy(d => d.Id)
                .GroupBy(d => d.DictL10nKey!)
                .Select(g => g.First())
                .ToList();

            foreach (var language in languages)
            {
                foreach (var dictItem in dictItems)
                {
                    var resourceKey = dictItem.DictL10nKey!;
                    var translationValue = dictItem.DictLabel;

                    var existing = await translationRepository.GetAsync(t =>
                        t.ResourceKey == resourceKey &&
                        t.CultureCode == language.CultureCode &&
                        t.IsDeleted == 0);

                    if (existing == null)
                    {
                        await translationRepository.CreateAsync(new TaktTranslation
                        {
                            LanguageId = language.Id,
                            CultureCode = language.CultureCode,
                            ResourceKey = resourceKey,
                            TranslationValue = translationValue,
                            ResourceType = "Frontend",
                            ResourceGroup = "Dict",
                            SortOrder = dictItem.SortOrder,
                            IsDeleted = 0
                        });
                        insertCount++;
                    }
                    else if (existing.TranslationValue != translationValue ||
                             existing.ResourceType != "Frontend" ||
                             existing.ResourceGroup != "Dict" ||
                             existing.SortOrder != dictItem.SortOrder ||
                             existing.LanguageId != language.Id)
                    {
                        existing.LanguageId = language.Id;
                        existing.TranslationValue = translationValue;
                        existing.ResourceType = "Frontend";
                        existing.ResourceGroup = "Dict";
                        existing.SortOrder = dictItem.SortOrder;
                        await translationRepository.UpdateAsync(existing);
                        updateCount++;
                    }
                }
            }
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }
}
