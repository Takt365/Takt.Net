// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSalesOrderEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktSalesOrder 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Sales;

/// <summary>
/// TaktSalesOrder 实体翻译种子数据（自动生成，与 TaktSalesOrder.cs 属性一一对应）
/// </summary>
public class TaktSalesOrderEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktSalesOrderEntityTranslations();

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
    /// 获取所有 TaktSalesOrder 实体名称及字段翻译（自动生成，与 TaktSalesOrder.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktSalesOrderEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.salesorder（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder._self", TranslationValue = "销售订单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder._self", TranslationValue = "销售订单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder._self", TranslationValue = "销售订单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder._self", TranslationValue = "销售订单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder._self", TranslationValue = "销售订单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder._self", TranslationValue = "销售订单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.salesorder.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.plantcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.plantcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.plantcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.salesorder.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.code", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.code", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.code", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.code", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.code", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.code", TranslationValue = "订单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.salesorder.customercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.salesorder.customername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.customername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.customername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.customername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.customername", TranslationValue = "客户名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.customername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.customername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.salesorder.date
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.date", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.date", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.date", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.date", TranslationValue = "订单日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.salesorder.requireddeliverydate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.requireddeliverydate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.requireddeliverydate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.requireddeliverydate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.requireddeliverydate", TranslationValue = "要求交货日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.requireddeliverydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.requireddeliverydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.salesorder.actualdeliverydate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.actualdeliverydate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.actualdeliverydate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.actualdeliverydate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.actualdeliverydate", TranslationValue = "实际交货日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.actualdeliverydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.actualdeliverydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.salesorder.salesby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.salesby", TranslationValue = "销售员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.salesby", TranslationValue = "销售员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.salesby", TranslationValue = "销售员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.salesby", TranslationValue = "销售员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.salesby", TranslationValue = "销售员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.salesby", TranslationValue = "销售员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.salesorder.totalquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.totalquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.totalquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.totalquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.totalquantity", TranslationValue = "订单总数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.totalquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.totalquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.salesorder.totalamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.totalamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.totalamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.totalamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.totalamount", TranslationValue = "订单总金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.totalamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.totalamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.salesorder.discountamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.discountamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.discountamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.discountamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.discountamount", TranslationValue = "折扣金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.discountamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.discountamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.salesorder.taxamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.taxamount", TranslationValue = "税费", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.salesorder.actualamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.actualamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.actualamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.actualamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.actualamount", TranslationValue = "订单实付金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.actualamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.actualamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.salesorder.shippedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.shippedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.shippedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.shippedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.shippedquantity", TranslationValue = "已发货数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.shippedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.shippedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.salesorder.shippedamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.shippedamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.shippedamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.shippedamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.shippedamount", TranslationValue = "已发货金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.shippedamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.shippedamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.salesorder.receivedamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.receivedamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.receivedamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.receivedamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.receivedamount", TranslationValue = "已收款金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.receivedamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.receivedamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.salesorder.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.status", TranslationValue = "订单状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.salesorder.deliverystatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.deliverystatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.deliverystatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.deliverystatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.deliverystatus", TranslationValue = "交货状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.deliverystatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.deliverystatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.salesorder.deliverymethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.deliverymethod", TranslationValue = "交货方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.deliverymethod", TranslationValue = "交货方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.deliverymethod", TranslationValue = "交货方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.deliverymethod", TranslationValue = "交货方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.deliverymethod", TranslationValue = "交货方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.deliverymethod", TranslationValue = "交货方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.salesorder.paymentmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.paymentmethod", TranslationValue = "收款方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.paymentmethod", TranslationValue = "收款方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.paymentmethod", TranslationValue = "收款方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.paymentmethod", TranslationValue = "收款方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.paymentmethod", TranslationValue = "收款方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.paymentmethod", TranslationValue = "收款方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.salesorder.deliveryaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salesorder.deliveryaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salesorder.deliveryaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salesorder.deliveryaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salesorder.deliveryaddress", TranslationValue = "交货地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salesorder.deliveryaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salesorder.deliveryaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
        };
    }
}
