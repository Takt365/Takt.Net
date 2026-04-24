// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPurchaseOrderItemEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPurchaseOrderItem 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Materials;

/// <summary>
/// TaktPurchaseOrderItem 实体翻译种子数据（自动生成，与 TaktPurchaseOrderItem.cs 属性一一对应）
/// </summary>
public class TaktPurchaseOrderItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktPurchaseOrderItemEntityTranslations();

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
    /// 获取所有 TaktPurchaseOrderItem 实体名称及字段翻译（自动生成，与 TaktPurchaseOrderItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPurchaseOrderItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.purchaseorderitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem._self", TranslationValue = "采购订单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem._self", TranslationValue = "采购订单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem._self", TranslationValue = "采购订单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem._self", TranslationValue = "采购订单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem._self", TranslationValue = "采购订单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem._self", TranslationValue = "采购订单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.orderid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.orderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.orderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.orderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.orderid", TranslationValue = "订单ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.orderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.orderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.ordercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.ordercode", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.ordercode", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.ordercode", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.ordercode", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.ordercode", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.ordercode", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.materialid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.materialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.materialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.materialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.materialid", TranslationValue = "物料ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.materialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.materialid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.materialname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.materialname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.materialname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.materialname", TranslationValue = "物料名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.materialspecification
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.purchaseunit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.purchaseunit", TranslationValue = "采购单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.purchaseunit", TranslationValue = "采购单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.purchaseunit", TranslationValue = "采购单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.purchaseunit", TranslationValue = "采购单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.purchaseunit", TranslationValue = "采购单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.purchaseunit", TranslationValue = "采购单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.orderquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.orderquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.orderquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.orderquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.orderquantity", TranslationValue = "订购数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.orderquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.orderquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.receivedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.receivedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.receivedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.receivedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.receivedquantity", TranslationValue = "已入库数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.receivedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.receivedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.unitprice
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.unitprice", TranslationValue = "单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.unitprice", TranslationValue = "单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.unitprice", TranslationValue = "单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.unitprice", TranslationValue = "单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.unitprice", TranslationValue = "单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.unitprice", TranslationValue = "单价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.discountrate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.discountrate", TranslationValue = "折扣率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.discountrate", TranslationValue = "折扣率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.discountrate", TranslationValue = "折扣率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.discountrate", TranslationValue = "折扣率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.discountrate", TranslationValue = "折扣率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.discountrate", TranslationValue = "折扣率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.discountamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.discountamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.discountamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.discountamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.discountamount", TranslationValue = "折扣金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.discountamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.discountamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.taxrate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.taxrate", TranslationValue = "税费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.taxrate", TranslationValue = "税费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.taxrate", TranslationValue = "税费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.taxrate", TranslationValue = "税费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.taxrate", TranslationValue = "税费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.taxrate", TranslationValue = "税费率", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.taxamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.subtotalamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.subtotalamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.subtotalamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.subtotalamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.subtotalamount", TranslationValue = "小计金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.subtotalamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.subtotalamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.purchaseorderitem.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.purchaseorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.purchaseorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.purchaseorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.purchaseorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.purchaseorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.purchaseorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
