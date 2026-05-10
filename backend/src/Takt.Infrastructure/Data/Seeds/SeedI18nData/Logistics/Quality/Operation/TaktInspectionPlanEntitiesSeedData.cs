// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktInspectionPlanEntitiesSeedData.cs
// 创建时间：2026-05-07
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktInspectionPlan 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality;

/// <summary>
/// TaktInspectionPlan 实体翻译种子数据（自动生成，与 TaktInspectionPlan.cs 属性一一对应）
/// </summary>
public class TaktInspectionPlanEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktInspectionPlanEntityTranslations();

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
    /// 获取所有 TaktInspectionPlan 实体名称及字段翻译（自动生成，与 TaktInspectionPlan.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktInspectionPlanEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.inspectionplan（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan._self", TranslationValue = "检验计划", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan._self", TranslationValue = "检验计划", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan._self", TranslationValue = "检验计划", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan._self", TranslationValue = "检验计划", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan._self", TranslationValue = "检验计划", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan._self", TranslationValue = "检验计划", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.inspectionplan.plancode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.plancode", TranslationValue = "检验计划编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.plancode", TranslationValue = "检验计划编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.plancode", TranslationValue = "检验计划编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.plancode", TranslationValue = "检验计划编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.plancode", TranslationValue = "检验计划编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.plancode", TranslationValue = "检验计划编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.inspectionplan.inspectiontype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.inspectiontype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.inspectiontype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.inspectiontype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.inspectiontype", TranslationValue = "检验类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.inspectiontype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.inspectiontype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.inspectionplan.sourcetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.sourcetype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.sourcetype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.sourcetype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.sourcetype", TranslationValue = "来源类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.sourcetype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.sourcetype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.inspectionplan.sourceid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.sourceid", TranslationValue = "来源ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.inspectionplan.sourcecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.inspectionplan.standardid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.standardid", TranslationValue = "检验标准ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.inspectionplan.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.inspectionplan.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.materialname", TranslationValue = "Name", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.materialname", TranslationValue = "名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.materialname", TranslationValue = "이름", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.materialname", TranslationValue = "物料名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.materialname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.materialname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.inspectionplan.batchno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.inspectionplan.plannedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.plannedquantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.plannedquantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.plannedquantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.plannedquantity", TranslationValue = "计划检验数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.plannedquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.plannedquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.inspectionplan.samplequantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.samplequantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.samplequantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.samplequantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.samplequantity", TranslationValue = "抽样数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.samplequantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.samplequantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.inspectionplan.planneddate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.planneddate", TranslationValue = "Date", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.planneddate", TranslationValue = "日付", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.planneddate", TranslationValue = "날짜", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.planneddate", TranslationValue = "计划检验日期", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.planneddate", TranslationValue = "日期", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.planneddate", TranslationValue = "日期", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.inspectionplan.inspectorid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.inspectorid", TranslationValue = "检验员ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.inspectionplan.inspectorname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.inspectionplan.planstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.planstatus", TranslationValue = "Status", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.planstatus", TranslationValue = "状態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.planstatus", TranslationValue = "상태", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.planstatus", TranslationValue = "检验计划状态", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.planstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.planstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.inspectionplan.plandescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionplan.plandescription", TranslationValue = "Description", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionplan.plandescription", TranslationValue = "説明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionplan.plandescription", TranslationValue = "설명", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionplan.plandescription", TranslationValue = "检验计划描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionplan.plandescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionplan.plandescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
        };
    }
}
