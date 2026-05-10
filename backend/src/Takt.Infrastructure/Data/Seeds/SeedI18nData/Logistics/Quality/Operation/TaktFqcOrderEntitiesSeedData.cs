// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFqcOrderEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktFqcOrder 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Operation;

/// <summary>
/// TaktFqcOrder 实体翻译种子数据（自动生成，与 TaktFqcOrder.cs 属性一一对应）
/// </summary>
public class TaktFqcOrderEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktFqcOrderEntityTranslations();

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
    /// 获取所有 TaktFqcOrder 实体名称及字段翻译（自动生成，与 TaktFqcOrder.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktFqcOrderEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.fqcorder（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder._self", TranslationValue = "出货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder._self", TranslationValue = "出货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder._self", TranslationValue = "出货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder._self", TranslationValue = "出货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder._self", TranslationValue = "出货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder._self", TranslationValue = "出货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.fqcorder.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.fqcorder.sourcecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.fqcorder.plancode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.fqcorder.standardcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.fqcorder.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.fqcorder.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.materialname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.materialname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.materialname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.materialname", TranslationValue = "物料名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.fqcorder.batchno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.fqcorder.customercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.fqcorder.customername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.customername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.customername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.customername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.customername", TranslationValue = "客户名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.customername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.customername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.fqcorder.outgoingquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.outgoingquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.outgoingquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.outgoingquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.outgoingquantity", TranslationValue = "出货数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.outgoingquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.outgoingquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.fqcorder.deliveryordercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.deliveryordercode", TranslationValue = "出货单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.deliveryordercode", TranslationValue = "出货单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.deliveryordercode", TranslationValue = "出货单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.deliveryordercode", TranslationValue = "出货单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.deliveryordercode", TranslationValue = "出货单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.deliveryordercode", TranslationValue = "出货单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.fqcorder.samplingschemecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.fqcorder.samplequantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.samplequantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.samplequantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.samplequantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.samplequantity", TranslationValue = "抽样数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.samplequantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.samplequantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.fqcorder.qualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.qualifiedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.qualifiedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.qualifiedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.qualifiedquantity", TranslationValue = "合格数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.qualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.qualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.fqcorder.unqualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.unqualifiedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.unqualifiedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.unqualifiedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.unqualifiedquantity", TranslationValue = "不合格数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.fqcorder.inspectionconclusion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.fqcorder.judgeby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.fqcorder.judgetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.judgetime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.judgetime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.judgetime", TranslationValue = "判定时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.fqcorder.inspectionremark
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.inspectionremark", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.inspectionremark", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.inspectionremark", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.inspectionremark", TranslationValue = "检验备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.inspectionremark", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.inspectionremark", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.fqcorder.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcorder.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcorder.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcorder.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcorder.status", TranslationValue = "检验单状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
        };
    }
}
