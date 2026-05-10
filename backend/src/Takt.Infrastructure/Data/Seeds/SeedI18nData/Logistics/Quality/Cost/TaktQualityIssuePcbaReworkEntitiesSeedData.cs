// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQualityIssuePcbaReworkEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktQualityIssuePcbaRework 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Cost;

/// <summary>
/// TaktQualityIssuePcbaRework 实体翻译种子数据（自动生成，与 TaktQualityIssuePcbaRework.cs 属性一一对应）
/// </summary>
public class TaktQualityIssuePcbaReworkEntitiesSeedData : ITaktSeedData
{
    public int Order => 999;

    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 1 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktQualityIssuePcbaReworkEntityTranslations();

            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId))
                    continue;

                var existing = await translationRepository.GetAsync(t =>
                    t.ResourceKey == translation.ResourceKey &&
                    t.CultureCode == translation.CultureCode &&
                    t.IsDeleted == 0);

                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = translation.CultureCode,
                        ResourceKey = translation.ResourceKey,
                        TranslationValue = translation.TranslationValue,
                        ResourceType = translation.ResourceType,
                        ResourceGroup = translation.ResourceGroup,
                        SortOrder = translation.SortOrder,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
                else if (existing.TranslationValue != translation.TranslationValue)
                {
                    existing.LanguageId = languageId;
                    existing.TranslationValue = translation.TranslationValue;
                    existing.ResourceType = translation.ResourceType;
                    existing.ResourceGroup = translation.ResourceGroup;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
                else if (existing.LanguageId != languageId)
                {
                    existing.LanguageId = languageId;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
            }
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有 TaktQualityIssuePcbaRework 实体名称及字段翻译（自动生成，与 TaktQualityIssuePcbaRework.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktQualityIssuePcbaReworkEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.qualityissuepcbarework（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework._self", TranslationValue = "质量问题PCBA不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework._self", TranslationValue = "质量问题PCBA不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework._self", TranslationValue = "质量问题PCBA不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework._self", TranslationValue = "质量问题PCBA不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework._self", TranslationValue = "质量问题PCBA不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework._self", TranslationValue = "质量问题PCBA不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.qualityissuepcbarework.qualityissueid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.qualityissueid", TranslationValue = "品质问题主表ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.qualityissuepcbarework.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.qualityissuepcbarework.pcbadefectparts
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbadefectparts", TranslationValue = "PCBA不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbadefectparts", TranslationValue = "PCBA不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbadefectparts", TranslationValue = "PCBA不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbadefectparts", TranslationValue = "PCBA不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbadefectparts", TranslationValue = "PCBA不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbadefectparts", TranslationValue = "PCBA不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.qualityissuepcbarework.cost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.cost", TranslationValue = "PCBA选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.cost", TranslationValue = "PCBA选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.cost", TranslationValue = "PCBA选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.cost", TranslationValue = "PCBA选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.cost", TranslationValue = "PCBA选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.cost", TranslationValue = "PCBA选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.qualityissuepcbarework.timeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.timeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.timeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.timeminutes", TranslationValue = "PCBA选别改修时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.qualityissuepcbarework.pcbareinspectiontimeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbareinspectiontimeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbareinspectiontimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbareinspectiontimeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbareinspectiontimeminutes", TranslationValue = "PCBA再检查时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbareinspectiontimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbareinspectiontimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.qualityissuepcbarework.pcbatravelcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbatravelcost", TranslationValue = "PCBA交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbatravelcost", TranslationValue = "PCBA交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbatravelcost", TranslationValue = "PCBA交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbatravelcost", TranslationValue = "PCBA交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbatravelcost", TranslationValue = "PCBA交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbatravelcost", TranslationValue = "PCBA交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.qualityissuepcbarework.pcbawarehousecost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbawarehousecost", TranslationValue = "PCBA仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbawarehousecost", TranslationValue = "PCBA仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbawarehousecost", TranslationValue = "PCBA仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbawarehousecost", TranslationValue = "PCBA仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbawarehousecost", TranslationValue = "PCBA仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbawarehousecost", TranslationValue = "PCBA仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.qualityissuepcbarework.pcbaotherexpenses
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses", TranslationValue = "PCBA选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses", TranslationValue = "PCBA选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses", TranslationValue = "PCBA选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses", TranslationValue = "PCBA选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses", TranslationValue = "PCBA选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses", TranslationValue = "PCBA选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.qualityissuepcbarework.note
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.note", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.note", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.note", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.note", TranslationValue = "PCBA选别改修备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.note", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.note", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.qualityissuepcbarework.pcbascrapcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbascrapcost", TranslationValue = "PCBA向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbascrapcost", TranslationValue = "PCBA向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbascrapcost", TranslationValue = "PCBA向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbascrapcost", TranslationValue = "PCBA向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbascrapcost", TranslationValue = "PCBA向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbascrapcost", TranslationValue = "PCBA向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.qualityissuepcbarework.pcbacustomername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbacustomername", TranslationValue = "PCBA顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbacustomername", TranslationValue = "PCBA顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbacustomername", TranslationValue = "PCBA顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbacustomername", TranslationValue = "PCBA顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbacustomername", TranslationValue = "PCBA顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbacustomername", TranslationValue = "PCBA顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.qualityissuepcbarework.pcbadebitnoteno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbadebitnoteno", TranslationValue = "PCBA Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbadebitnoteno", TranslationValue = "PCBA Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbadebitnoteno", TranslationValue = "PCBA Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbadebitnoteno", TranslationValue = "PCBA Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbadebitnoteno", TranslationValue = "PCBA Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbadebitnoteno", TranslationValue = "PCBA Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.qualityissuepcbarework.pcbaotherexpenses2
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses2", TranslationValue = "PCBA其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses2", TranslationValue = "PCBA其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses2", TranslationValue = "PCBA其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses2", TranslationValue = "PCBA其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses2", TranslationValue = "PCBA其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbaotherexpenses2", TranslationValue = "PCBA其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.qualityissuepcbarework.pcbanote
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbanote", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbanote", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbanote", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbanote", TranslationValue = "PCBA备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbanote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbanote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.qualityissuepcbarework.pcbarecorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuepcbarework.pcbarecorder", TranslationValue = "PCBA不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuepcbarework.pcbarecorder", TranslationValue = "PCBA不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuepcbarework.pcbarecorder", TranslationValue = "PCBA不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuepcbarework.pcbarecorder", TranslationValue = "PCBA不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuepcbarework.pcbarecorder", TranslationValue = "PCBA不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuepcbarework.pcbarecorder", TranslationValue = "PCBA不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
        };
    }
}
