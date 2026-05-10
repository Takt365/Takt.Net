// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQualityIssueAssyReworkEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktQualityIssueAssyRework 实体字段翻译种子数据（自动生成）
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
/// TaktQualityIssueAssyRework 实体翻译种子数据（自动生成，与 TaktQualityIssueAssyRework.cs 属性一一对应）
/// </summary>
public class TaktQualityIssueAssyReworkEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktQualityIssueAssyReworkEntityTranslations();

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
    /// 获取所有 TaktQualityIssueAssyRework 实体名称及字段翻译（自动生成，与 TaktQualityIssueAssyRework.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktQualityIssueAssyReworkEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.qualityissueassyrework（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework._self", TranslationValue = "质量问题组装不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework._self", TranslationValue = "质量问题组装不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework._self", TranslationValue = "质量问题组装不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework._self", TranslationValue = "质量问题组装不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework._self", TranslationValue = "质量问题组装不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework._self", TranslationValue = "质量问题组装不良改修费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.qualityissueassyrework.qualityissueid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.qualityissueid", TranslationValue = "品质问题主表ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.qualityissueassyrework.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.qualityissueassyrework.assydefectparts
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assydefectparts", TranslationValue = "组装不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assydefectparts", TranslationValue = "组装不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assydefectparts", TranslationValue = "组装不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assydefectparts", TranslationValue = "组装不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assydefectparts", TranslationValue = "组装不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assydefectparts", TranslationValue = "组装不良内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.qualityissueassyrework.cost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.cost", TranslationValue = "组装选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.cost", TranslationValue = "组装选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.cost", TranslationValue = "组装选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.cost", TranslationValue = "组装选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.cost", TranslationValue = "组装选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.cost", TranslationValue = "组装选别改修费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.qualityissueassyrework.timeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.timeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.timeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.timeminutes", TranslationValue = "组装选别改修时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.qualityissueassyrework.assyreinspectiontimeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assyreinspectiontimeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assyreinspectiontimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assyreinspectiontimeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assyreinspectiontimeminutes", TranslationValue = "组装再检查时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assyreinspectiontimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assyreinspectiontimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.qualityissueassyrework.assytravelcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assytravelcost", TranslationValue = "组装交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assytravelcost", TranslationValue = "组装交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assytravelcost", TranslationValue = "组装交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assytravelcost", TranslationValue = "组装交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assytravelcost", TranslationValue = "组装交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assytravelcost", TranslationValue = "组装交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.qualityissueassyrework.assywarehousecost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assywarehousecost", TranslationValue = "组装仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assywarehousecost", TranslationValue = "组装仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assywarehousecost", TranslationValue = "组装仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assywarehousecost", TranslationValue = "组装仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assywarehousecost", TranslationValue = "组装仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assywarehousecost", TranslationValue = "组装仓库管理费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.qualityissueassyrework.assyotherexpenses
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses", TranslationValue = "组装选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses", TranslationValue = "组装选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses", TranslationValue = "组装选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses", TranslationValue = "组装选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses", TranslationValue = "组装选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses", TranslationValue = "组装选别改修其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.qualityissueassyrework.note
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.note", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.note", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.note", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.note", TranslationValue = "组装选别改修备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.note", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.note", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.qualityissueassyrework.assyscrapcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assyscrapcost", TranslationValue = "组装向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assyscrapcost", TranslationValue = "组装向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assyscrapcost", TranslationValue = "组装向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assyscrapcost", TranslationValue = "组装向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assyscrapcost", TranslationValue = "组装向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assyscrapcost", TranslationValue = "组装向顾客费用请求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.qualityissueassyrework.assycustomername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assycustomername", TranslationValue = "组装顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assycustomername", TranslationValue = "组装顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assycustomername", TranslationValue = "组装顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assycustomername", TranslationValue = "组装顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assycustomername", TranslationValue = "组装顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assycustomername", TranslationValue = "组装顾客名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.qualityissueassyrework.assydebitnoteno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assydebitnoteno", TranslationValue = "组装 Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assydebitnoteno", TranslationValue = "组装 Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assydebitnoteno", TranslationValue = "组装 Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assydebitnoteno", TranslationValue = "组装 Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assydebitnoteno", TranslationValue = "组装 Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assydebitnoteno", TranslationValue = "组装 Debit Note No", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.qualityissueassyrework.assyotherexpenses2
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses2", TranslationValue = "组装其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses2", TranslationValue = "组装其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses2", TranslationValue = "组装其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses2", TranslationValue = "组装其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses2", TranslationValue = "组装其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assyotherexpenses2", TranslationValue = "组装其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.qualityissueassyrework.assynote
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assynote", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assynote", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assynote", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assynote", TranslationValue = "组装备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assynote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assynote", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.qualityissueassyrework.assyrecorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissueassyrework.assyrecorder", TranslationValue = "组装不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissueassyrework.assyrecorder", TranslationValue = "组装不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissueassyrework.assyrecorder", TranslationValue = "组装不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissueassyrework.assyrecorder", TranslationValue = "组装不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissueassyrework.assyrecorder", TranslationValue = "组装不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissueassyrework.assyrecorder", TranslationValue = "组装不良改修对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
        };
    }
}
