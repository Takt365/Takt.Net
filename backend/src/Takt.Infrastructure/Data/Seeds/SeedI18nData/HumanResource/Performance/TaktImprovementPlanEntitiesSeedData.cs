// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktImprovementPlanEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktImprovementPlan 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.Performance;

/// <summary>
/// TaktImprovementPlan 实体翻译种子数据（自动生成，与 TaktImprovementPlan.cs 属性一一对应）
/// </summary>
public class TaktImprovementPlanEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktImprovementPlanEntityTranslations();

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
    /// 获取所有 TaktImprovementPlan 实体名称及字段翻译（自动生成，与 TaktImprovementPlan.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktImprovementPlanEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.improvementplan（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan._self", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan._self", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan._self", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan._self", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan._self", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan._self", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.improvementplan.employeeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.improvementplan.performancereviewid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.performancereviewid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.performancereviewid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.performancereviewid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.performancereviewid", TranslationValue = "绩效评审ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.performancereviewid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.performancereviewid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.improvementplan.title
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.title", TranslationValue = "改进计划标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.title", TranslationValue = "改进计划标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.title", TranslationValue = "改进计划标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.title", TranslationValue = "改进计划标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.title", TranslationValue = "改进计划标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.title", TranslationValue = "改进计划标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.improvementplan.improvementarea
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.improvementarea", TranslationValue = "改进领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.improvementarea", TranslationValue = "改进领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.improvementarea", TranslationValue = "改进领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.improvementarea", TranslationValue = "改进领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.improvementarea", TranslationValue = "改进领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.improvementarea", TranslationValue = "改进领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.improvementplan.currentsituation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.currentsituation", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.currentsituation", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.currentsituation", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.currentsituation", TranslationValue = "当前状况描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.currentsituation", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.currentsituation", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.improvementplan.improvementgoal
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.improvementgoal", TranslationValue = "改进目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.improvementgoal", TranslationValue = "改进目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.improvementgoal", TranslationValue = "改进目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.improvementgoal", TranslationValue = "改进目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.improvementgoal", TranslationValue = "改进目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.improvementgoal", TranslationValue = "改进目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.improvementplan.improvementactions
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.improvementactions", TranslationValue = "改进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.improvementactions", TranslationValue = "改进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.improvementactions", TranslationValue = "改进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.improvementactions", TranslationValue = "改进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.improvementactions", TranslationValue = "改进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.improvementactions", TranslationValue = "改进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.improvementplan.requiredresources
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.requiredresources", TranslationValue = "所需资源支持", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.requiredresources", TranslationValue = "所需资源支持", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.requiredresources", TranslationValue = "所需资源支持", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.requiredresources", TranslationValue = "所需资源支持", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.requiredresources", TranslationValue = "所需资源支持", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.requiredresources", TranslationValue = "所需资源支持", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.improvementplan.date
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.date", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.date", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.date", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.date", TranslationValue = "计划制定日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.improvementplan.startdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.startdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.startdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.startdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.startdate", TranslationValue = "开始日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.startdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.startdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.improvementplan.targetcompletiondate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.targetcompletiondate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.targetcompletiondate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.targetcompletiondate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.targetcompletiondate", TranslationValue = "目标完成日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.targetcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.targetcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.improvementplan.actualcompletiondate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.actualcompletiondate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.actualcompletiondate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.actualcompletiondate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.actualcompletiondate", TranslationValue = "实际完成日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.actualcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.actualcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.improvementplan.progresspercentage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.progresspercentage", TranslationValue = "进度百分比", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.progresspercentage", TranslationValue = "进度百分比", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.progresspercentage", TranslationValue = "进度百分比", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.progresspercentage", TranslationValue = "进度百分比", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.progresspercentage", TranslationValue = "进度百分比", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.progresspercentage", TranslationValue = "进度百分比", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.improvementplan.midtermcheckdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.midtermcheckdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.midtermcheckdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.midtermcheckdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.midtermcheckdate", TranslationValue = "中期检查日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.midtermcheckdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.midtermcheckdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.improvementplan.midtermcheckresult
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.midtermcheckresult", TranslationValue = "中期检查结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.midtermcheckresult", TranslationValue = "中期检查结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.midtermcheckresult", TranslationValue = "中期检查结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.midtermcheckresult", TranslationValue = "中期检查结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.midtermcheckresult", TranslationValue = "中期检查结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.midtermcheckresult", TranslationValue = "中期检查结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.improvementplan.resultdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.resultdescription", TranslationValue = "改进结果说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.resultdescription", TranslationValue = "改进结果说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.resultdescription", TranslationValue = "改进结果说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.resultdescription", TranslationValue = "改进结果说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.resultdescription", TranslationValue = "改进结果说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.resultdescription", TranslationValue = "改进结果说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.improvementplan.mentorid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.mentorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.mentorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.mentorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.mentorid", TranslationValue = "指导老师ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.mentorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.mentorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.improvementplan.approverid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.approverid", TranslationValue = "审批人ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.improvementplan.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.improvementplan.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.improvementplan.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.improvementplan.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.improvementplan.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.improvementplan.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.improvementplan.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
        };
    }
}
