// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktApsScheduleEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktApsSchedule 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Manufacturing.Scheduling;

/// <summary>
/// TaktApsSchedule 实体翻译种子数据（自动生成，与 TaktApsSchedule.cs 属性一一对应）
/// </summary>
public class TaktApsScheduleEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktApsScheduleEntityTranslations();

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
    /// 获取所有 TaktApsSchedule 实体名称及字段翻译（自动生成，与 TaktApsSchedule.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktApsScheduleEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.apsschedule（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule._self", TranslationValue = "APS排程主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule._self", TranslationValue = "APS排程主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule._self", TranslationValue = "APS排程主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule._self", TranslationValue = "APS排程主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule._self", TranslationValue = "APS排程主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule._self", TranslationValue = "APS排程主", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.apsschedule.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.code", TranslationValue = "排程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.code", TranslationValue = "排程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.code", TranslationValue = "排程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.code", TranslationValue = "排程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.code", TranslationValue = "排程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.code", TranslationValue = "排程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.apsschedule.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.name", TranslationValue = "排程名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.apsschedule.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.type", TranslationValue = "排程类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.apsschedule.plandate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.plandate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.plandate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.plandate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.plandate", TranslationValue = "计划日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.plandate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.plandate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.apsschedule.planstarttime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.planstarttime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.planstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.planstarttime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.planstarttime", TranslationValue = "计划开始时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.planstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.planstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.apsschedule.planendtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.planendtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.planendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.planendtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.planendtime", TranslationValue = "计划结束时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.planendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.planendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.apsschedule.plancycle
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.plancycle", TranslationValue = "计划周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.plancycle", TranslationValue = "计划周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.plancycle", TranslationValue = "计划周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.plancycle", TranslationValue = "计划周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.plancycle", TranslationValue = "计划周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.plancycle", TranslationValue = "计划周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.apsschedule.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.plantcode", TranslationValue = "工厂编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.plantcode", TranslationValue = "工厂编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.plantcode", TranslationValue = "工厂编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.plantcode", TranslationValue = "工厂编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.plantcode", TranslationValue = "工厂编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.plantcode", TranslationValue = "工厂编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.apsschedule.plantname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.plantname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.plantname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.plantname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.plantname", TranslationValue = "工厂名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.plantname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.plantname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.apsschedule.workshopcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.workshopcode", TranslationValue = "车间编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.workshopcode", TranslationValue = "车间编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.workshopcode", TranslationValue = "车间编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.workshopcode", TranslationValue = "车间编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.workshopcode", TranslationValue = "车间编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.workshopcode", TranslationValue = "车间编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.apsschedule.workshopname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.workshopname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.workshopname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.workshopname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.workshopname", TranslationValue = "车间名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.workshopname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.workshopname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.apsschedule.productionlinecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.productionlinecode", TranslationValue = "生产线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.productionlinecode", TranslationValue = "生产线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.productionlinecode", TranslationValue = "生产线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.productionlinecode", TranslationValue = "生产线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.productionlinecode", TranslationValue = "生产线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.productionlinecode", TranslationValue = "生产线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.apsschedule.productionlinename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.productionlinename", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.productionlinename", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.productionlinename", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.productionlinename", TranslationValue = "生产线名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.productionlinename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.productionlinename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.apsschedule.strategy
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.strategy", TranslationValue = "排程策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.strategy", TranslationValue = "排程策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.strategy", TranslationValue = "排程策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.strategy", TranslationValue = "排程策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.strategy", TranslationValue = "排程策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.strategy", TranslationValue = "排程策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.apsschedule.algorithm
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.algorithm", TranslationValue = "排程算法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.algorithm", TranslationValue = "排程算法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.algorithm", TranslationValue = "排程算法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.algorithm", TranslationValue = "排程算法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.algorithm", TranslationValue = "排程算法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.algorithm", TranslationValue = "排程算法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.apsschedule.optimizationobjective
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.optimizationobjective", TranslationValue = "优化目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.optimizationobjective", TranslationValue = "优化目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.optimizationobjective", TranslationValue = "优化目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.optimizationobjective", TranslationValue = "优化目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.optimizationobjective", TranslationValue = "优化目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.optimizationobjective", TranslationValue = "优化目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.apsschedule.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.status", TranslationValue = "排程状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.apsschedule.plannerid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.plannerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.plannerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.plannerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.plannerid", TranslationValue = "计划员ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.plannerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.plannerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.apsschedule.plannername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.plannername", TranslationValue = "计划员姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.plannername", TranslationValue = "计划员姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.plannername", TranslationValue = "计划员姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.plannername", TranslationValue = "计划员姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.plannername", TranslationValue = "计划员姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.plannername", TranslationValue = "计划员姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.apsschedule.publishtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.publishtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.publishtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.publishtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.publishtime", TranslationValue = "发布时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.publishtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.publishtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.apsschedule.publishuserid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.publishuserid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.publishuserid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.publishuserid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.publishuserid", TranslationValue = "发布人ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.publishuserid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.publishuserid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.apsschedule.publishusername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.publishusername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.publishusername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.publishusername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.publishusername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.publishusername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.publishusername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.apsschedule.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsschedule.description", TranslationValue = "排程说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsschedule.description", TranslationValue = "排程说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsschedule.description", TranslationValue = "排程说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsschedule.description", TranslationValue = "排程说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsschedule.description", TranslationValue = "排程说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsschedule.description", TranslationValue = "排程说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
        };
    }
}
