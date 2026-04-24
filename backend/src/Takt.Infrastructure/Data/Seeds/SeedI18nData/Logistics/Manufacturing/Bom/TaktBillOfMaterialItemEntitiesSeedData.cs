// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktBillOfMaterialItemEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktBillOfMaterialItem 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Manufacturing.Bom;

/// <summary>
/// TaktBillOfMaterialItem 实体翻译种子数据（自动生成，与 TaktBillOfMaterialItem.cs 属性一一对应）
/// </summary>
public class TaktBillOfMaterialItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktBillOfMaterialItemEntityTranslations();

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
    /// 获取所有 TaktBillOfMaterialItem 实体名称及字段翻译（自动生成，与 TaktBillOfMaterialItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktBillOfMaterialItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.billofmaterialitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem._self", TranslationValue = "物料清单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem._self", TranslationValue = "物料清单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem._self", TranslationValue = "物料清单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem._self", TranslationValue = "物料清单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem._self", TranslationValue = "物料清单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem._self", TranslationValue = "物料清单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.bomid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.bomid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.bomid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.bomid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.bomid", TranslationValue = "BOM ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.bomid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.bomid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.bomcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.bomcode", TranslationValue = "BOM编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.bomcode", TranslationValue = "BOM编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.bomcode", TranslationValue = "BOM编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.bomcode", TranslationValue = "BOM编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.bomcode", TranslationValue = "BOM编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.bomcode", TranslationValue = "BOM编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.childmaterialid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.childmaterialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.childmaterialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.childmaterialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.childmaterialid", TranslationValue = "子物料ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.childmaterialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.childmaterialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.childmaterialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.childmaterialcode", TranslationValue = "子物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.childmaterialcode", TranslationValue = "子物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.childmaterialcode", TranslationValue = "子物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.childmaterialcode", TranslationValue = "子物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.childmaterialcode", TranslationValue = "子物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.childmaterialcode", TranslationValue = "子物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.childmaterialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.childmaterialname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.childmaterialname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.childmaterialname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.childmaterialname", TranslationValue = "子物料名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.childmaterialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.childmaterialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.childmaterialspecification
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.childmaterialspecification", TranslationValue = "子物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.childmaterialspecification", TranslationValue = "子物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.childmaterialspecification", TranslationValue = "子物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.childmaterialspecification", TranslationValue = "子物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.childmaterialspecification", TranslationValue = "子物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.childmaterialspecification", TranslationValue = "子物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.usagequantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.usagequantity", TranslationValue = "用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.usagequantity", TranslationValue = "用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.usagequantity", TranslationValue = "用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.usagequantity", TranslationValue = "用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.usagequantity", TranslationValue = "用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.usagequantity", TranslationValue = "用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.childmaterialunit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.childmaterialunit", TranslationValue = "子物料单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.childmaterialunit", TranslationValue = "子物料单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.childmaterialunit", TranslationValue = "子物料单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.childmaterialunit", TranslationValue = "子物料单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.childmaterialunit", TranslationValue = "子物料单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.childmaterialunit", TranslationValue = "子物料单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.scraprate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.scraprate", TranslationValue = "损耗率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.scraprate", TranslationValue = "损耗率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.scraprate", TranslationValue = "损耗率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.scraprate", TranslationValue = "损耗率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.scraprate", TranslationValue = "损耗率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.scraprate", TranslationValue = "损耗率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.actualusagequantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.actualusagequantity", TranslationValue = "实际用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.actualusagequantity", TranslationValue = "实际用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.actualusagequantity", TranslationValue = "实际用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.actualusagequantity", TranslationValue = "实际用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.actualusagequantity", TranslationValue = "实际用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.actualusagequantity", TranslationValue = "实际用量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.issubstitute
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.issubstitute", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.issubstitute", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.issubstitute", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.issubstitute", TranslationValue = "是否替代料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.issubstitute", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.issubstitute", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.substitutepriority
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.substitutepriority", TranslationValue = "替代优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.substitutepriority", TranslationValue = "替代优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.substitutepriority", TranslationValue = "替代优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.substitutepriority", TranslationValue = "替代优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.substitutepriority", TranslationValue = "替代优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.substitutepriority", TranslationValue = "替代优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.isrequired
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.isrequired", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.isrequired", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.isrequired", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.isrequired", TranslationValue = "是否必选", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.isrequired", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.isrequired", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.isphantom
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.isphantom", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.isphantom", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.isphantom", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.isphantom", TranslationValue = "是否虚拟件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.isphantom", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.isphantom", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.iscritical
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.iscritical", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.iscritical", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.iscritical", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.iscritical", TranslationValue = "是否关键件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.iscritical", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.iscritical", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.billofmaterialitem.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.billofmaterialitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.billofmaterialitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.billofmaterialitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.billofmaterialitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.billofmaterialitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.billofmaterialitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
