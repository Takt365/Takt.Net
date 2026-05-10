// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktIpqcOrderItemEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktIpqcOrderItem 实体字段翻译种子数据（自动生成）
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
/// TaktIpqcOrderItem 实体翻译种子数据（自动生成，与 TaktIpqcOrderItem.cs 属性一一对应）
/// </summary>
public class TaktIpqcOrderItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktIpqcOrderItemEntityTranslations();

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
    /// 获取所有 TaktIpqcOrderItem 实体名称及字段翻译（自动生成，与 TaktIpqcOrderItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktIpqcOrderItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.ipqcorderitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem._self", TranslationValue = "制程检验单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem._self", TranslationValue = "制程检验单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem._self", TranslationValue = "制程检验单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem._self", TranslationValue = "制程检验单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem._self", TranslationValue = "制程检验单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem._self", TranslationValue = "制程检验单明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.ipqcorderitem.ipqcorderid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.ipqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.ipqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.ipqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.ipqcorderid", TranslationValue = "IPQC检验单ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.ipqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.ipqcorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.ipqcorderitem.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.linenumber", TranslationValue = "行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.ipqcorderitem.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.code", TranslationValue = "检验项目编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.code", TranslationValue = "检验项目编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.code", TranslationValue = "检验项目编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.code", TranslationValue = "检验项目编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.code", TranslationValue = "检验项目编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.code", TranslationValue = "检验项目编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.ipqcorderitem.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.name", TranslationValue = "检验项目名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.ipqcorderitem.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.type", TranslationValue = "检验项目类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.ipqcorderitem.standardvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.ipqcorderitem.upperlimit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.ipqcorderitem.lowerlimit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.ipqcorderitem.inspectiontool
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.ipqcorderitem.inspectionmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.ipqcorderitem.actualvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.ipqcorderitem.inspectionresult
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.ipqcorderitem.defectquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.defectquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.defectquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.defectquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.defectquantity", TranslationValue = "不良数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.defectquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.defectquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.ipqcorderitem.defectdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.defectdescription", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.defectdescription", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.defectdescription", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.defectdescription", TranslationValue = "不良描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.defectdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.defectdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.ipqcorderitem.inspectorby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.inspectorby", TranslationValue = "检验员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.inspectorby", TranslationValue = "检验员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.inspectorby", TranslationValue = "检验员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.inspectorby", TranslationValue = "检验员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.inspectorby", TranslationValue = "检验员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.inspectorby", TranslationValue = "检验员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.ipqcorderitem.inspectiontime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.inspectiontime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.inspectiontime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.inspectiontime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.inspectiontime", TranslationValue = "检验时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.inspectiontime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.inspectiontime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.ipqcorderitem.inspectionimages
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcorderitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcorderitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcorderitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcorderitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcorderitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcorderitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
        };
    }
}
