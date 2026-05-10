// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktIqcOrderEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktIqcOrder 实体字段翻译种子数据（自动生成）
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
/// TaktIqcOrder 实体翻译种子数据（自动生成，与 TaktIqcOrder.cs 属性一一对应）
/// </summary>
public class TaktIqcOrderEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktIqcOrderEntityTranslations();

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
    /// 获取所有 TaktIqcOrder 实体名称及字段翻译（自动生成，与 TaktIqcOrder.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktIqcOrderEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.iqcorder（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder._self", TranslationValue = "进货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder._self", TranslationValue = "进货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder._self", TranslationValue = "进货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder._self", TranslationValue = "进货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder._self", TranslationValue = "进货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder._self", TranslationValue = "进货检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.iqcorder.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.iqcorder.sourcecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.iqcorder.plancode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.iqcorder.standardcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.iqcorder.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.iqcorder.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.materialname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.materialname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.materialname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.materialname", TranslationValue = "物料名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.iqcorder.batchno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.iqcorder.suppliercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.iqcorder.suppliername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.suppliername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.suppliername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.suppliername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.suppliername", TranslationValue = "供应商名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.suppliername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.suppliername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.iqcorder.incomingquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.incomingquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.incomingquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.incomingquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.incomingquantity", TranslationValue = "来料数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.incomingquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.incomingquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.iqcorder.samplingschemecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.iqcorder.samplequantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.samplequantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.samplequantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.samplequantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.samplequantity", TranslationValue = "抽样数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.samplequantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.samplequantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.iqcorder.qualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.qualifiedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.qualifiedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.qualifiedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.qualifiedquantity", TranslationValue = "合格数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.qualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.qualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.iqcorder.unqualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.unqualifiedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.unqualifiedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.unqualifiedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.unqualifiedquantity", TranslationValue = "不合格数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.iqcorder.inspectionconclusion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.iqcorder.judgeby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.iqcorder.judgetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.judgetime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.judgetime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.judgetime", TranslationValue = "判定时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.iqcorder.inspectionremark
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.inspectionremark", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.inspectionremark", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.inspectionremark", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.inspectionremark", TranslationValue = "检验备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.inspectionremark", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.inspectionremark", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.iqcorder.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.iqcorder.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.iqcorder.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.iqcorder.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.iqcorder.status", TranslationValue = "检验单状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.iqcorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.iqcorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
        };
    }
}
