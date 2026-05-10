// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQualityIssueMeetingEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktQualityIssueMeeting 实体字段翻译种子数据（自动生成）
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
/// TaktQualityIssueMeeting 实体翻译种子数据（自动生成，与 TaktQualityIssueMeeting.cs 属性一一对应）
/// </summary>
public class TaktQualityIssueMeetingEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktQualityIssueMeetingEntityTranslations();

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
    /// 获取所有 TaktQualityIssueMeeting 实体名称及字段翻译（自动生成，与 TaktQualityIssueMeeting.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktQualityIssueMeetingEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.qualityissuemeeting（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting._self", TranslationValue = "质量问题会议调查试验费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting._self", TranslationValue = "质量问题会议调查试验费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting._self", TranslationValue = "质量问题会议调查试验费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting._self", TranslationValue = "质量问题会议调查试验费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting._self", TranslationValue = "质量问题会议调查试验费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting._self", TranslationValue = "质量问题会议调查试验费用明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.qualityissuemeeting.qualityissueid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.qualityissueid", TranslationValue = "品质问题主表ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.qualityissueid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.qualityissuemeeting.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.qualityissuemeeting.directmanpowercostperminute
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.directmanpowercostperminute", TranslationValue = "直接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.directmanpowercostperminute", TranslationValue = "直接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.directmanpowercostperminute", TranslationValue = "直接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.directmanpowercostperminute", TranslationValue = "直接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.directmanpowercostperminute", TranslationValue = "直接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.directmanpowercostperminute", TranslationValue = "直接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.qualityissuemeeting.indirectmanpowercostperminute
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.indirectmanpowercostperminute", TranslationValue = "间接人员费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.qualityissuemeeting.investigationcontent
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.investigationcontent", TranslationValue = "讨论调查试验内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.investigationcontent", TranslationValue = "讨论调查试验内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.investigationcontent", TranslationValue = "讨论调查试验内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.investigationcontent", TranslationValue = "讨论调查试验内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.investigationcontent", TranslationValue = "讨论调查试验内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.investigationcontent", TranslationValue = "讨论调查试验内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.qualityissuemeeting.investigationcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.investigationcost", TranslationValue = "讨论调查试验费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.investigationcost", TranslationValue = "讨论调查试验费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.investigationcost", TranslationValue = "讨论调查试验费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.investigationcost", TranslationValue = "讨论调查试验费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.investigationcost", TranslationValue = "讨论调查试验费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.investigationcost", TranslationValue = "讨论调查试验费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.qualityissuemeeting.timeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.timeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.timeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.timeminutes", TranslationValue = "检讨会使用时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.timeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.qualityissuemeeting.directparticipantcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.directparticipantcount", TranslationValue = "直接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.directparticipantcount", TranslationValue = "直接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.directparticipantcount", TranslationValue = "直接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.directparticipantcount", TranslationValue = "直接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.directparticipantcount", TranslationValue = "直接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.directparticipantcount", TranslationValue = "直接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.qualityissuemeeting.indirectparticipantcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.indirectparticipantcount", TranslationValue = "间接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.indirectparticipantcount", TranslationValue = "间接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.indirectparticipantcount", TranslationValue = "间接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.indirectparticipantcount", TranslationValue = "间接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.indirectparticipantcount", TranslationValue = "间接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.indirectparticipantcount", TranslationValue = "间接人员参加人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.qualityissuemeeting.investigationworktimeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.investigationworktimeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.investigationworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.investigationworktimeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.investigationworktimeminutes", TranslationValue = "调查评价试验工作时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.investigationworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.investigationworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.qualityissuemeeting.travelcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.travelcost", TranslationValue = "交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.travelcost", TranslationValue = "交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.travelcost", TranslationValue = "交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.travelcost", TranslationValue = "交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.travelcost", TranslationValue = "交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.travelcost", TranslationValue = "交通费旅费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.qualityissuemeeting.otherexpenses
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.otherexpenses", TranslationValue = "其他费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.qualityissuemeeting.otherworktimeminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.otherworktimeminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.otherworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.otherworktimeminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.otherworktimeminutes", TranslationValue = "其他作业时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.otherworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.otherworktimeminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.qualityissuemeeting.otherapparatuscost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.otherapparatuscost", TranslationValue = "其他设备工程搬运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.otherapparatuscost", TranslationValue = "其他设备工程搬运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.otherapparatuscost", TranslationValue = "其他设备工程搬运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.otherapparatuscost", TranslationValue = "其他设备工程搬运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.otherapparatuscost", TranslationValue = "其他设备工程搬运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.otherapparatuscost", TranslationValue = "其他设备工程搬运费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.qualityissuemeeting.recorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.qualityissuemeeting.recorder", TranslationValue = "品质问题对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.qualityissuemeeting.recorder", TranslationValue = "品质问题对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.qualityissuemeeting.recorder", TranslationValue = "品质问题对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.qualityissuemeeting.recorder", TranslationValue = "品质问题对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.qualityissuemeeting.recorder", TranslationValue = "品质问题对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.qualityissuemeeting.recorder", TranslationValue = "品质问题对应记录者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
        };
    }
}
