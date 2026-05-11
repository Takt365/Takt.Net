// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCustomerSatisfactionSurveyItemEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktCustomerSatisfactionSurveyItem 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Complaint;

/// <summary>
/// TaktCustomerSatisfactionSurveyItem 实体翻译种子数据（自动生成，与 TaktCustomerSatisfactionSurveyItem.cs 属性一一对应）
/// </summary>
public class TaktCustomerSatisfactionSurveyItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktCustomerSatisfactionSurveyItemEntityTranslations();

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
    /// 获取所有 TaktCustomerSatisfactionSurveyItem 实体名称及字段翻译（自动生成，与 TaktCustomerSatisfactionSurveyItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktCustomerSatisfactionSurveyItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.customersatisfactionsurveyitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem._self", TranslationValue = "客户满意度调查项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem._self", TranslationValue = "客户满意度调查项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem._self", TranslationValue = "客户满意度调查项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem._self", TranslationValue = "客户满意度调查项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem._self", TranslationValue = "客户满意度调查项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem._self", TranslationValue = "客户满意度调查项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.customersatisfactionsurveyitem.surveyid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.surveyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.surveyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.surveyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.surveyid", TranslationValue = "调查表ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.surveyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.surveyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.customersatisfactionsurveyitem.customersatisfactionsurveycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", TranslationValue = "调查表编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.customersatisfactionsurveyitem.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.customersatisfactionsurveyitem.categorytype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.categorytype", TranslationValue = "调查类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.categorytype", TranslationValue = "调查类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.categorytype", TranslationValue = "调查类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.categorytype", TranslationValue = "调查类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.categorytype", TranslationValue = "调查类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.categorytype", TranslationValue = "调查类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.customersatisfactionsurveyitem.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.name", TranslationValue = "调查项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.name", TranslationValue = "调查项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.name", TranslationValue = "调查项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.name", TranslationValue = "调查项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.name", TranslationValue = "调查项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.name", TranslationValue = "调查项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.customersatisfactionsurveyitem.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.customersatisfactionsurveyitem.weight
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.customersatisfactionsurveyitem.score
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.customersatisfactionsurveyitem.satisfactionlevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.satisfactionlevel", TranslationValue = "满意度等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.satisfactionlevel", TranslationValue = "满意度等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.satisfactionlevel", TranslationValue = "满意度等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.satisfactionlevel", TranslationValue = "满意度等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.satisfactionlevel", TranslationValue = "满意度等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.satisfactionlevel", TranslationValue = "满意度等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.customersatisfactionsurveyitem.customerfeedback
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.customersatisfactionsurveyitem.improvementsuggestion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.improvementsuggestion", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.improvementsuggestion", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.improvementsuggestion", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.improvementsuggestion", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.improvementsuggestion", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.improvementsuggestion", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.customersatisfactionsurveyitem.followupaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.followupaction", TranslationValue = "跟进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.followupaction", TranslationValue = "跟进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.followupaction", TranslationValue = "跟进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.followupaction", TranslationValue = "跟进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.followupaction", TranslationValue = "跟进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.followupaction", TranslationValue = "跟进措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.customersatisfactionsurveyitem.followupstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.followupstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.followupstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.followupstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.followupstatus", TranslationValue = "跟进状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.followupstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.followupstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.customersatisfactionsurveyitem.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurveyitem.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurveyitem.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurveyitem.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurveyitem.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurveyitem.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurveyitem.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
        };
    }
}
