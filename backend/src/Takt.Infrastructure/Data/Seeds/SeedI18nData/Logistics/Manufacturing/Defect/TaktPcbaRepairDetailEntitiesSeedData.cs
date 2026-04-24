// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPcbaRepairDetailEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPcbaRepairDetail 实体字段翻译种子数据（自动生成）
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
/// TaktPcbaRepairDetail 实体翻译种子数据（自动生成，与 TaktPcbaRepairDetail.cs 属性一一对应）
/// </summary>
public class TaktPcbaRepairDetailEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktPcbaRepairDetailEntityTranslations();

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
    /// 获取所有 TaktPcbaRepairDetail 实体名称及字段翻译（自动生成，与 TaktPcbaRepairDetail.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPcbaRepairDetailEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.pcbarepairdetail（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail._self", TranslationValue = "PCBA改修明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail._self", TranslationValue = "PCBA改修明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail._self", TranslationValue = "PCBA改修明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail._self", TranslationValue = "PCBA改修明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail._self", TranslationValue = "PCBA改修明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail._self", TranslationValue = "PCBA改修明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.pcbarepairid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.pcbarepairid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.pcbarepairid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.pcbarepairid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.pcbarepairid", TranslationValue = "PCBA改修ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.pcbarepairid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.pcbarepairid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.pcbaboardtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.prodactualqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.prodactualqty", TranslationValue = "生产实绩", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.prodactualqty", TranslationValue = "生产实绩", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.prodactualqty", TranslationValue = "生产实绩", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.prodactualqty", TranslationValue = "生产实绩", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.prodactualqty", TranslationValue = "生产实绩", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.prodactualqty", TranslationValue = "生产实绩", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.prodline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.cardno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.cardno", TranslationValue = "卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.cardno", TranslationValue = "卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.cardno", TranslationValue = "卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.cardno", TranslationValue = "卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.cardno", TranslationValue = "卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.cardno", TranslationValue = "卡号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.defectsymptom
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.defectsymptom", TranslationValue = "不良症状", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.defectengineering
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.defectengineering", TranslationValue = "检出工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.defectengineering", TranslationValue = "检出工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.defectengineering", TranslationValue = "检出工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.defectengineering", TranslationValue = "检出工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.defectengineering", TranslationValue = "检出工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.defectengineering", TranslationValue = "检出工程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.defectreason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.defectreason", TranslationValue = "不良原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.defectqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.defectqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.defectqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.defectqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.defectqty", TranslationValue = "不良数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.defectqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.defectqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.defectresponsibility
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.defectresponsibility", TranslationValue = "责任归属", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.defectresponsibility", TranslationValue = "责任归属", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.defectresponsibility", TranslationValue = "责任归属", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.defectresponsibility", TranslationValue = "责任归属", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.defectresponsibility", TranslationValue = "责任归属", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.defectresponsibility", TranslationValue = "责任归属", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.defectnature
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.defectnature", TranslationValue = "不良性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.defectnature", TranslationValue = "不良性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.defectnature", TranslationValue = "不良性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.defectnature", TranslationValue = "不良性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.defectnature", TranslationValue = "不良性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.defectnature", TranslationValue = "不良性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbarepairdetail.repairoperator
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbarepairdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbarepairdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbarepairdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbarepairdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbarepairdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbarepairdetail.repairoperator", TranslationValue = "修理员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
