// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPerformancePlanEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPerformancePlan 实体字段翻译种子数据（自动生成）
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
/// TaktPerformancePlan 实体翻译种子数据（自动生成，与 TaktPerformancePlan.cs 属性一一对应）
/// </summary>
public class TaktPerformancePlanEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktPerformancePlanEntityTranslations();

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
    /// 获取所有 TaktPerformancePlan 实体名称及字段翻译（自动生成，与 TaktPerformancePlan.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPerformancePlanEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.performanceplan（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan._self", TranslationValue = "绩效方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan._self", TranslationValue = "绩效方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan._self", TranslationValue = "绩效方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan._self", TranslationValue = "绩效方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan._self", TranslationValue = "绩效方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan._self", TranslationValue = "绩效方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.performanceplan.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.code", TranslationValue = "方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.code", TranslationValue = "方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.code", TranslationValue = "方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.code", TranslationValue = "方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.code", TranslationValue = "方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.code", TranslationValue = "方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.performanceplan.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.name", TranslationValue = "方案名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.performanceplan.applicabledepartment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.performanceplan.applicableposition
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.performanceplan.applicablelevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.applicablelevel", TranslationValue = "适用职级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.performanceplan.cycletype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.cycletype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.cycletype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.cycletype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.cycletype", TranslationValue = "考核周期类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.cycletype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.cycletype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.performanceplan.scoringstandard
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.performanceplan.selfevaluationweight
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.selfevaluationweight", TranslationValue = "自评权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.selfevaluationweight", TranslationValue = "自评权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.selfevaluationweight", TranslationValue = "自评权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.selfevaluationweight", TranslationValue = "自评权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.selfevaluationweight", TranslationValue = "自评权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.selfevaluationweight", TranslationValue = "自评权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.performanceplan.supervisorweight
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.supervisorweight", TranslationValue = "主管评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.supervisorweight", TranslationValue = "主管评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.supervisorweight", TranslationValue = "主管评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.supervisorweight", TranslationValue = "主管评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.supervisorweight", TranslationValue = "主管评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.supervisorweight", TranslationValue = "主管评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.performanceplan.peerweight
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.peerweight", TranslationValue = "同事评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.peerweight", TranslationValue = "同事评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.peerweight", TranslationValue = "同事评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.peerweight", TranslationValue = "同事评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.peerweight", TranslationValue = "同事评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.peerweight", TranslationValue = "同事评分权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.performanceplan.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.description", TranslationValue = "方案说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.description", TranslationValue = "方案说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.description", TranslationValue = "方案说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.description", TranslationValue = "方案说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.description", TranslationValue = "方案说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.description", TranslationValue = "方案说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.performanceplan.effectivedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.effectivedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.effectivedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.effectivedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.performanceplan.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.performanceplan.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.performanceplan.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.performanceplan.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.performanceplan.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.performanceplan.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.performanceplan.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
        };
    }
}
