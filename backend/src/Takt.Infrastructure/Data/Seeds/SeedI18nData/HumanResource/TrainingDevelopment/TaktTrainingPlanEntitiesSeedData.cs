// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktTrainingPlanEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktTrainingPlan 实体字段翻译种子数据（自动生成）
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
/// TaktTrainingPlan 实体翻译种子数据（自动生成，与 TaktTrainingPlan.cs 属性一一对应）
/// </summary>
public class TaktTrainingPlanEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktTrainingPlanEntityTranslations();

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
    /// 获取所有 TaktTrainingPlan 实体名称及字段翻译（自动生成，与 TaktTrainingPlan.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktTrainingPlanEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.trainingplan（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan._self", TranslationValue = "培训计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan._self", TranslationValue = "培训计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan._self", TranslationValue = "培训计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan._self", TranslationValue = "培训计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan._self", TranslationValue = "培训计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan._self", TranslationValue = "培训计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.trainingplan.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.code", TranslationValue = "计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.code", TranslationValue = "计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.code", TranslationValue = "计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.code", TranslationValue = "计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.code", TranslationValue = "计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.code", TranslationValue = "计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.trainingplan.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.name", TranslationValue = "计划名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.trainingplan.year
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.year", TranslationValue = "计划年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.year", TranslationValue = "计划年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.year", TranslationValue = "计划年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.year", TranslationValue = "计划年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.year", TranslationValue = "计划年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.year", TranslationValue = "计划年度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.trainingplan.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.type", TranslationValue = "计划类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.trainingplan.applicabledepartment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.trainingplan.applicableposition
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.trainingplan.applicablelevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.trainingplan.startdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.startdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.startdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.startdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.startdate", TranslationValue = "计划开始日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.startdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.startdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.trainingplan.enddate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.enddate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.enddate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.enddate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.enddate", TranslationValue = "计划结束日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.enddate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.enddate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.trainingplan.trainingobjectives
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.trainingobjectives", TranslationValue = "培训目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.trainingobjectives", TranslationValue = "培训目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.trainingobjectives", TranslationValue = "培训目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.trainingobjectives", TranslationValue = "培训目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.trainingobjectives", TranslationValue = "培训目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.trainingobjectives", TranslationValue = "培训目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.trainingplan.nedheadcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.nedheadcount", TranslationValue = "计划培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.nedheadcount", TranslationValue = "计划培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.nedheadcount", TranslationValue = "计划培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.nedheadcount", TranslationValue = "计划培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.nedheadcount", TranslationValue = "计划培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.nedheadcount", TranslationValue = "计划培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.trainingplan.actualheadcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.actualheadcount", TranslationValue = "实际培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.actualheadcount", TranslationValue = "实际培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.actualheadcount", TranslationValue = "实际培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.actualheadcount", TranslationValue = "实际培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.actualheadcount", TranslationValue = "实际培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.actualheadcount", TranslationValue = "实际培训人数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.trainingplan.nedtotalhours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.nedtotalhours", TranslationValue = "计划总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.nedtotalhours", TranslationValue = "计划总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.nedtotalhours", TranslationValue = "计划总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.nedtotalhours", TranslationValue = "计划总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.nedtotalhours", TranslationValue = "计划总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.nedtotalhours", TranslationValue = "计划总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.trainingplan.actualtotalhours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.actualtotalhours", TranslationValue = "实际总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.actualtotalhours", TranslationValue = "实际总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.actualtotalhours", TranslationValue = "实际总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.actualtotalhours", TranslationValue = "实际总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.actualtotalhours", TranslationValue = "实际总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.actualtotalhours", TranslationValue = "实际总课时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.trainingplan.trainingbudget
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.trainingbudget", TranslationValue = "培训预算", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.trainingbudget", TranslationValue = "培训预算", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.trainingbudget", TranslationValue = "培训预算", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.trainingbudget", TranslationValue = "培训预算", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.trainingbudget", TranslationValue = "培训预算", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.trainingbudget", TranslationValue = "培训预算", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.trainingplan.actualcost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.actualcost", TranslationValue = "实际花费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.actualcost", TranslationValue = "实际花费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.actualcost", TranslationValue = "实际花费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.actualcost", TranslationValue = "实际花费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.actualcost", TranslationValue = "实际花费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.actualcost", TranslationValue = "实际花费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.trainingplan.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.description", TranslationValue = "计划说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.description", TranslationValue = "计划说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.description", TranslationValue = "计划说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.description", TranslationValue = "计划说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.description", TranslationValue = "计划说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.description", TranslationValue = "计划说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.trainingplan.approverid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.approverid", TranslationValue = "审批人ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.trainingplan.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingplan.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingplan.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingplan.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingplan.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingplan.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingplan.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
        };
    }
}
