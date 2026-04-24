// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktAssyDefectDetailEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktAssyDefectDetail 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Manufacturing.Defect;

/// <summary>
/// TaktAssyDefectDetail 实体翻译种子数据（自动生成，与 TaktAssyDefectDetail.cs 属性一一对应）
/// </summary>
public class TaktAssyDefectDetailEntitiesSeedData : ITaktSeedData
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
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktAssyDefectDetailEntityTranslations();

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
    /// 获取所有 TaktAssyDefectDetail 实体名称及字段翻译（自动生成，与 TaktAssyDefectDetail.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktAssyDefectDetailEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.assydefectdetail（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail._self", TranslationValue = "组立不良明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail._self", TranslationValue = "组立不良明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail._self", TranslationValue = "组立不良明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail._self", TranslationValue = "组立不良明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail._self", TranslationValue = "组立不良明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail._self", TranslationValue = "组立不良明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.assydefectid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.assydefectid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.assydefectid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.assydefectid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.assydefectid", TranslationValue = "组立不良ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.assydefectid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.assydefectid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.defectcategory
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.defectcategory", TranslationValue = "不良区分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.defectcategory", TranslationValue = "不良区分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.defectcategory", TranslationValue = "不良区分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.defectcategory", TranslationValue = "不良区分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.defectcategory", TranslationValue = "不良区分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.defectcategory", TranslationValue = "不良区分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.defectqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.defectqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.defectqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.defectqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.defectqty", TranslationValue = "不良数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.defectqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.defectqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.cumulativedefectqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.cumulativedefectqty", TranslationValue = "累计不良", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.cumulativedefectqty", TranslationValue = "累计不良", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.cumulativedefectqty", TranslationValue = "累计不良", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.cumulativedefectqty", TranslationValue = "累计不良", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.cumulativedefectqty", TranslationValue = "累计不良", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.cumulativedefectqty", TranslationValue = "累计不良", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.randomcardno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.randomcardno", TranslationValue = "随机卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.randomcardno", TranslationValue = "随机卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.randomcardno", TranslationValue = "随机卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.randomcardno", TranslationValue = "随机卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.randomcardno", TranslationValue = "随机卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.randomcardno", TranslationValue = "随机卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.occurrenceengineering
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.occurrenceengineering", TranslationValue = "发生工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.occurrenceengineering", TranslationValue = "发生工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.occurrenceengineering", TranslationValue = "发生工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.occurrenceengineering", TranslationValue = "发生工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.occurrenceengineering", TranslationValue = "发生工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.occurrenceengineering", TranslationValue = "发生工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.teststep
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.teststep", TranslationValue = "测试步骤", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.teststep", TranslationValue = "测试步骤", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.teststep", TranslationValue = "测试步骤", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.teststep", TranslationValue = "测试步骤", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.teststep", TranslationValue = "测试步骤", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.teststep", TranslationValue = "测试步骤", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.defectsymptom
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.defectlocation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.defectreason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.repairoperator
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.assydefectdetail.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.assydefectdetail.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.assydefectdetail.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.assydefectdetail.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.assydefectdetail.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.assydefectdetail.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.assydefectdetail.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
