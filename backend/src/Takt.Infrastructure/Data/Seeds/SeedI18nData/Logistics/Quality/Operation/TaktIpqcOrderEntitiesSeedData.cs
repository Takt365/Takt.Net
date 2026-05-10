// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktIpqcOrderEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktIpqcOrder 实体字段翻译种子数据（自动生成）
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
/// TaktIpqcOrder 实体翻译种子数据（自动生成，与 TaktIpqcOrder.cs 属性一一对应）
/// </summary>
public class TaktIpqcOrderEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktIpqcOrderEntityTranslations();

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
    /// 获取所有 TaktIpqcOrder 实体名称及字段翻译（自动生成，与 TaktIpqcOrder.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktIpqcOrderEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.ipqcorder（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder._self", TranslationValue = "制程检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder._self", TranslationValue = "制程检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder._self", TranslationValue = "制程检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder._self", TranslationValue = "制程检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder._self", TranslationValue = "制程检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder._self", TranslationValue = "制程检验单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.ipqcorder.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.code", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.ipqcorder.sourcecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.sourcecode", TranslationValue = "来源单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.ipqcorder.plancode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.plancode", TranslationValue = "检验计划编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.ipqcorder.standardcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.standardcode", TranslationValue = "检验标准编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.ipqcorder.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.ipqcorder.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.materialname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.materialname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.materialname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.materialname", TranslationValue = "物料名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.ipqcorder.batchno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.batchno", TranslationValue = "批次号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.ipqcorder.processcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.ipqcorder.processname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.processname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.processname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.processname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.processname", TranslationValue = "工序名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.processname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.processname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.ipqcorder.samplingschemecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.samplingschemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.ipqcorder.samplequantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.samplequantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.samplequantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.samplequantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.samplequantity", TranslationValue = "抽样数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.samplequantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.samplequantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.ipqcorder.qualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.qualifiedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.qualifiedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.qualifiedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.qualifiedquantity", TranslationValue = "合格数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.qualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.qualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.ipqcorder.unqualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.unqualifiedquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.unqualifiedquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.unqualifiedquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.unqualifiedquantity", TranslationValue = "不合格数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.ipqcorder.inspectionconclusion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.ipqcorder.judgeby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.judgeby", TranslationValue = "判定人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.ipqcorder.judgetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.judgetime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.judgetime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.judgetime", TranslationValue = "判定时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.judgetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.ipqcorder.inspectionremark
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.inspectionremark", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.inspectionremark", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.inspectionremark", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.inspectionremark", TranslationValue = "检验备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.inspectionremark", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.inspectionremark", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.ipqcorder.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorder.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorder.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorder.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorder.status", TranslationValue = "检验单状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorder.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
        };
    }
}
