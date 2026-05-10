// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktInspectionSheetItemEntitiesSeedData.cs
// 创建时间：2026-05-07
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktInspectionSheetItem 实体字段翻译种子数据（自动生成）
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
/// TaktInspectionSheetItem 实体翻译种子数据（自动生成，与 TaktInspectionSheetItem.cs 属性一一对应）
/// </summary>
public class TaktInspectionSheetItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktInspectionSheetItemEntityTranslations();

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
    /// 获取所有 TaktInspectionSheetItem 实体名称及字段翻译（自动生成，与 TaktInspectionSheetItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktInspectionSheetItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.inspectionsheetitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem._self", TranslationValue = "检验单明细", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem._self", TranslationValue = "检验单明细", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem._self", TranslationValue = "检验单明细", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem._self", TranslationValue = "检验单明细", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem._self", TranslationValue = "检验单明细", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem._self", TranslationValue = "检验单明细", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.inspectionsheetitem.sheetid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.sheetid", TranslationValue = "检验单ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.inspectionsheetitem.itemcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.inspectionsheetitem.itemname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.itemname", TranslationValue = "Name", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.itemname", TranslationValue = "名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.itemname", TranslationValue = "이름", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.itemname", TranslationValue = "检验项目名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.itemname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.itemname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.inspectionsheetitem.itemtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.itemtype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.itemtype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.itemtype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.itemtype", TranslationValue = "检验项目类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.itemtype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.itemtype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.inspectionsheetitem.standardvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.standardvalue", TranslationValue = "检验标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.inspectionsheetitem.upperlimit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.upperlimit", TranslationValue = "检验上限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.inspectionsheetitem.lowerlimit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.lowerlimit", TranslationValue = "检验下限值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.inspectionsheetitem.inspectiontool
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.inspectiontool", TranslationValue = "检验工具", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.inspectionsheetitem.inspectionmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.inspectionsheetitem.actualvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.inspectionsheetitem.inspectionresult
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.inspectionsheetitem.defectquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.defectquantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.defectquantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.defectquantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.defectquantity", TranslationValue = "不良数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.defectquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.defectquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.inspectionsheetitem.defectdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.defectdescription", TranslationValue = "Description", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.defectdescription", TranslationValue = "説明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.defectdescription", TranslationValue = "설명", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.defectdescription", TranslationValue = "不良描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.defectdescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.defectdescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.inspectionsheetitem.inspectorid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.inspectorid", TranslationValue = "检验员ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.inspectionsheetitem.inspectorname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.inspectionsheetitem.inspectiontime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.inspectiontime", TranslationValue = "Time", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.inspectiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.inspectiontime", TranslationValue = "시간", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.inspectiontime", TranslationValue = "检验时间", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.inspectiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.inspectiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.inspectionsheetitem.inspectionimages
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheetitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheetitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheetitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheetitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheetitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheetitem.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
        };
    }
}
