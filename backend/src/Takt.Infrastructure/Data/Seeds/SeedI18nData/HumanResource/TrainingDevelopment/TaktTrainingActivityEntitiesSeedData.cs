// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktTrainingActivityEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktTrainingActivity 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.TrainingDevelopment;

/// <summary>
/// TaktTrainingActivity 实体翻译种子数据（自动生成，与 TaktTrainingActivity.cs 属性一一对应）
/// </summary>
public class TaktTrainingActivityEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktTrainingActivityEntityTranslations();

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
    /// 获取所有 TaktTrainingActivity 实体名称及字段翻译（自动生成，与 TaktTrainingActivity.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktTrainingActivityEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.trainingactivity（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity._self", TranslationValue = "培训活动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity._self", TranslationValue = "培训活动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity._self", TranslationValue = "培训活动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity._self", TranslationValue = "培训活动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity._self", TranslationValue = "培训活动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity._self", TranslationValue = "培训活动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.trainingactivity.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.code", TranslationValue = "活动编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.code", TranslationValue = "活动编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.code", TranslationValue = "活动编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.code", TranslationValue = "活动编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.code", TranslationValue = "活动编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.code", TranslationValue = "活动编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.trainingactivity.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.name", TranslationValue = "活动名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.trainingactivity.trainingcourseid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.trainingcourseid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.trainingcourseid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.trainingcourseid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.trainingcourseid", TranslationValue = "培训课程ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.trainingcourseid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.trainingcourseid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.trainingactivity.trainingplanid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.trainingplanid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.trainingplanid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.trainingplanid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.trainingplanid", TranslationValue = "培训计划ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.trainingplanid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.trainingplanid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.trainingactivity.trainingdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.trainingdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.trainingdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.trainingdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.trainingdate", TranslationValue = "培训日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.trainingdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.trainingdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.trainingactivity.starttime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.starttime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.starttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.starttime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.starttime", TranslationValue = "开始时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.starttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.starttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.trainingactivity.endtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.endtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.endtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.endtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.endtime", TranslationValue = "结束时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.endtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.endtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.trainingactivity.traininglocation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.traininglocation", TranslationValue = "培训地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.traininglocation", TranslationValue = "培训地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.traininglocation", TranslationValue = "培训地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.traininglocation", TranslationValue = "培训地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.traininglocation", TranslationValue = "培训地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.traininglocation", TranslationValue = "培训地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.trainingactivity.instructor
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.instructor", TranslationValue = "培训讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.instructor", TranslationValue = "培训讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.instructor", TranslationValue = "培训讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.instructor", TranslationValue = "培训讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.instructor", TranslationValue = "培训讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.instructor", TranslationValue = "培训讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.trainingactivity.plannedattendees
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.plannedattendees", TranslationValue = "计划人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.plannedattendees", TranslationValue = "计划人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.plannedattendees", TranslationValue = "计划人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.plannedattendees", TranslationValue = "计划人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.plannedattendees", TranslationValue = "计划人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.plannedattendees", TranslationValue = "计划人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.trainingactivity.actualattendees
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.actualattendees", TranslationValue = "实际签到人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.actualattendees", TranslationValue = "实际签到人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.actualattendees", TranslationValue = "实际签到人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.actualattendees", TranslationValue = "实际签到人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.actualattendees", TranslationValue = "实际签到人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.actualattendees", TranslationValue = "实际签到人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.trainingactivity.traininghours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.trainingactivity.trainingcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.trainingcost", TranslationValue = "培训费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.trainingcost", TranslationValue = "培训费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.trainingcost", TranslationValue = "培训费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.trainingcost", TranslationValue = "培训费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.trainingcost", TranslationValue = "培训费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.trainingcost", TranslationValue = "培训费用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.trainingactivity.contentsummary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.contentsummary", TranslationValue = "培训内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.contentsummary", TranslationValue = "培训内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.contentsummary", TranslationValue = "培训内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.contentsummary", TranslationValue = "培训内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.contentsummary", TranslationValue = "培训内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.contentsummary", TranslationValue = "培训内容摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.trainingactivity.trainingmaterials
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.trainingmaterials", TranslationValue = "培训材料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.trainingmaterials", TranslationValue = "培训材料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.trainingmaterials", TranslationValue = "培训材料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.trainingmaterials", TranslationValue = "培训材料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.trainingmaterials", TranslationValue = "培训材料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.trainingmaterials", TranslationValue = "培训材料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.trainingactivity.effectivenessevaluation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.effectivenessevaluation", TranslationValue = "培训效果评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.effectivenessevaluation", TranslationValue = "培训效果评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.effectivenessevaluation", TranslationValue = "培训效果评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.effectivenessevaluation", TranslationValue = "培训效果评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.effectivenessevaluation", TranslationValue = "培训效果评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.effectivenessevaluation", TranslationValue = "培训效果评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.trainingactivity.participantfeedback
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.participantfeedback", TranslationValue = "学员反馈意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.participantfeedback", TranslationValue = "学员反馈意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.participantfeedback", TranslationValue = "学员反馈意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.participantfeedback", TranslationValue = "学员反馈意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.participantfeedback", TranslationValue = "学员反馈意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.participantfeedback", TranslationValue = "学员反馈意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.trainingactivity.improvementsuggestions
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.trainingactivity.organizerid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.organizerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.organizerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.organizerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.organizerid", TranslationValue = "组织者ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.organizerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.organizerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.trainingactivity.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingactivity.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingactivity.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingactivity.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingactivity.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingactivity.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingactivity.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
        };
    }
}
