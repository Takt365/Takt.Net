// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSupplierEvaluationItemEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktSupplierEvaluationItem 实体字段翻译种子数据（自动生成）
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
/// TaktSupplierEvaluationItem 实体翻译种子数据（自动生成，与 TaktSupplierEvaluationItem.cs 属性一一对应）
/// </summary>
public class TaktSupplierEvaluationItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktSupplierEvaluationItemEntityTranslations();

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
    /// 获取所有 TaktSupplierEvaluationItem 实体名称及字段翻译（自动生成，与 TaktSupplierEvaluationItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktSupplierEvaluationItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.supplierevaluationitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem._self", TranslationValue = "供应商评价考核项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem._self", TranslationValue = "供应商评价考核项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem._self", TranslationValue = "供应商评价考核项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem._self", TranslationValue = "供应商评价考核项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem._self", TranslationValue = "供应商评价考核项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem._self", TranslationValue = "供应商评价考核项目明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.supplierevaluationitem.evaluationid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.evaluationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.evaluationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.evaluationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.evaluationid", TranslationValue = "评价表ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.evaluationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.evaluationid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.supplierevaluationitem.supplierevaluationcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.supplierevaluationcode", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.supplierevaluationcode", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.supplierevaluationcode", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.supplierevaluationcode", TranslationValue = "评价表编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.supplierevaluationcode", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.supplierevaluationcode", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.supplierevaluationitem.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.supplierevaluationitem.categorytype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.categorytype", TranslationValue = "评价类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.categorytype", TranslationValue = "评价类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.categorytype", TranslationValue = "评价类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.categorytype", TranslationValue = "评价类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.categorytype", TranslationValue = "评价类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.categorytype", TranslationValue = "评价类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.supplierevaluationitem.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.name", TranslationValue = "评价项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.name", TranslationValue = "评价项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.name", TranslationValue = "评价项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.name", TranslationValue = "评价项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.name", TranslationValue = "评价项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.name", TranslationValue = "评价项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.supplierevaluationitem.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.description", TranslationValue = "项目说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.supplierevaluationitem.weight
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.weight", TranslationValue = "权重", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.supplierevaluationitem.scoringstandard
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.scoringstandard", TranslationValue = "评分标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.supplierevaluationitem.score
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.score", TranslationValue = "评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.supplierevaluationitem.ratinglevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.ratinglevel", TranslationValue = "评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.ratinglevel", TranslationValue = "评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.ratinglevel", TranslationValue = "评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.ratinglevel", TranslationValue = "评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.ratinglevel", TranslationValue = "评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.ratinglevel", TranslationValue = "评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.supplierevaluationitem.evaluationcomment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.evaluationcomment", TranslationValue = "评价说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.evaluationcomment", TranslationValue = "评价说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.evaluationcomment", TranslationValue = "评价说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.evaluationcomment", TranslationValue = "评价说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.evaluationcomment", TranslationValue = "评价说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.evaluationcomment", TranslationValue = "评价说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.supplierevaluationitem.existingissues
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.existingissues", TranslationValue = "存在问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.existingissues", TranslationValue = "存在问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.existingissues", TranslationValue = "存在问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.existingissues", TranslationValue = "存在问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.existingissues", TranslationValue = "存在问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.existingissues", TranslationValue = "存在问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.supplierevaluationitem.improvementrequirement
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.improvementrequirement", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.improvementrequirement", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.improvementrequirement", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.improvementrequirement", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.improvementrequirement", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.improvementrequirement", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.supplierevaluationitem.rectificationrequired
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.rectificationrequired", TranslationValue = "整改要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.rectificationrequired", TranslationValue = "整改要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.rectificationrequired", TranslationValue = "整改要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.rectificationrequired", TranslationValue = "整改要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.rectificationrequired", TranslationValue = "整改要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.rectificationrequired", TranslationValue = "整改要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.supplierevaluationitem.rectificationdeadline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.supplierevaluationitem.rectificationstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.rectificationstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.rectificationstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.rectificationstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.rectificationstatus", TranslationValue = "整改状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.rectificationstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.rectificationstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.supplierevaluationitem.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluationitem.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluationitem.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluationitem.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluationitem.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluationitem.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluationitem.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
        };
    }
}
