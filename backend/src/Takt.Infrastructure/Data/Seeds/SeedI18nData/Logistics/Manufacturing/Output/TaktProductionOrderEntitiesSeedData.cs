// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktProductionOrderEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktProductionOrder 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Manufacturing.Output;

/// <summary>
/// TaktProductionOrder 实体翻译种子数据（自动生成，与 TaktProductionOrder.cs 属性一一对应）
/// </summary>
public class TaktProductionOrderEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktProductionOrderEntityTranslations();

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
    /// 获取所有 TaktProductionOrder 实体名称及字段翻译（自动生成，与 TaktProductionOrder.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktProductionOrderEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.productionorder（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder._self", TranslationValue = "生产工单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder._self", TranslationValue = "生产工单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder._self", TranslationValue = "生产工单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder._self", TranslationValue = "生产工单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder._self", TranslationValue = "生产工单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder._self", TranslationValue = "生产工单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.plantcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.plantcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.plantcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.prodordertype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.prodordertype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.prodordertype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.prodordertype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.prodordertype", TranslationValue = "生产工单类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.prodordertype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.prodordertype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.prodordercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.prodordercode", TranslationValue = "生产工单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.prodordercode", TranslationValue = "生产工单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.prodordercode", TranslationValue = "生产工单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.prodordercode", TranslationValue = "生产工单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.prodordercode", TranslationValue = "生产工单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.prodordercode", TranslationValue = "生产工单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.prodorderqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.prodorderqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.prodorderqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.prodorderqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.prodorderqty", TranslationValue = "生产工单数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.prodorderqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.prodorderqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.producedqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.producedqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.producedqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.producedqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.producedqty", TranslationValue = "已生产数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.producedqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.producedqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.unitofmeasure
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.unitofmeasure", TranslationValue = "计量单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.unitofmeasure", TranslationValue = "计量单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.unitofmeasure", TranslationValue = "计量单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.unitofmeasure", TranslationValue = "计量单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.unitofmeasure", TranslationValue = "计量单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.unitofmeasure", TranslationValue = "计量单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.actualstartdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.actualstartdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.actualstartdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.actualstartdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.actualstartdate", TranslationValue = "实际开始日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.actualstartdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.actualstartdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.actualenddate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.actualenddate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.actualenddate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.actualenddate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.actualenddate", TranslationValue = "实际完成日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.actualenddate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.actualenddate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.priority
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.workcenter
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.workcenter", TranslationValue = "工作中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.workcenter", TranslationValue = "工作中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.workcenter", TranslationValue = "工作中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.workcenter", TranslationValue = "工作中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.workcenter", TranslationValue = "工作中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.workcenter", TranslationValue = "工作中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.prodline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.prodbatch
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.prodbatch", TranslationValue = "生产批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.prodbatch", TranslationValue = "生产批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.prodbatch", TranslationValue = "生产批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.prodbatch", TranslationValue = "生产批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.prodbatch", TranslationValue = "生产批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.prodbatch", TranslationValue = "生产批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.serialno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.routingcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.routingcode", TranslationValue = "工艺路线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.routingcode", TranslationValue = "工艺路线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.routingcode", TranslationValue = "工艺路线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.routingcode", TranslationValue = "工艺路线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.routingcode", TranslationValue = "工艺路线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.routingcode", TranslationValue = "工艺路线编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.productionorder.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.productionorder.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.productionorder.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.productionorder.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.productionorder.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.productionorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.productionorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
