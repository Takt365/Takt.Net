// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPcbaInspectionDetailEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPcbaInspectionDetail 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Manufacturing.Defect;

/// <summary>
/// TaktPcbaInspectionDetail 实体翻译种子数据（自动生成，与 TaktPcbaInspectionDetail.cs 属性一一对应）
/// </summary>
public class TaktPcbaInspectionDetailEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktPcbaInspectionDetailEntityTranslations();

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
    /// 获取所有 TaktPcbaInspectionDetail 实体名称及字段翻译（自动生成，与 TaktPcbaInspectionDetail.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPcbaInspectionDetailEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.pcbainspectiondetail（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail._self", TranslationValue = "PCBA检查明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail._self", TranslationValue = "PCBA检查明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail._self", TranslationValue = "PCBA检查明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail._self", TranslationValue = "PCBA检查明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail._self", TranslationValue = "PCBA检查明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail._self", TranslationValue = "PCBA检查明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.pcbainspectionid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.pcbainspectionid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.pcbainspectionid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.pcbainspectionid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.pcbainspectionid", TranslationValue = "PCBA检查ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.pcbainspectionid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.pcbainspectionid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.pcbaboardtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.pcbaboardtype", TranslationValue = "PCBA板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.visualinspectionline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.visualinspectionline", TranslationValue = "目视线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.visualinspectionline", TranslationValue = "目视线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.visualinspectionline", TranslationValue = "目视线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.visualinspectionline", TranslationValue = "目视线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.visualinspectionline", TranslationValue = "目视线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.visualinspectionline", TranslationValue = "目视线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.aoiline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.aoiline", TranslationValue = "AOI线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.aoiline", TranslationValue = "AOI线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.aoiline", TranslationValue = "AOI线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.aoiline", TranslationValue = "AOI线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.aoiline", TranslationValue = "AOI线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.aoiline", TranslationValue = "AOI线别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.bsideassemblydate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.bsideassemblydate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.bsideassemblydate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.bsideassemblydate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.bsideassemblydate", TranslationValue = "B面实装日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.bsideassemblydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.bsideassemblydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.tsideassemblydate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.tsideassemblydate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.tsideassemblydate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.tsideassemblydate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.tsideassemblydate", TranslationValue = "T面实装日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.tsideassemblydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.tsideassemblydate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.shiftno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.shiftno", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.shiftno", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.shiftno", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.shiftno", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.shiftno", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.shiftno", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.inspectorname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.inspectorname", TranslationValue = "检查员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.inspectorname", TranslationValue = "检查员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.inspectorname", TranslationValue = "检查员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.inspectorname", TranslationValue = "检查员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.inspectorname", TranslationValue = "检查员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.inspectorname", TranslationValue = "检查员", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.dailycompletedqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.dailycompletedqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.dailycompletedqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.dailycompletedqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.dailycompletedqty", TranslationValue = "当日完成数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.dailycompletedqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.dailycompletedqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.inspectionqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.inspectionqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.inspectionqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.inspectionqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.inspectionqty", TranslationValue = "检查数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.inspectionqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.inspectionqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.inspectionstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.inspectionstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.inspectionstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.inspectionstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.inspectionstatus", TranslationValue = "检查状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.inspectionstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.inspectionstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.prodline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.prodline", TranslationValue = "生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.inspectionworkhours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.inspectionworkhours", TranslationValue = "检查工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.inspectionworkhours", TranslationValue = "检查工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.inspectionworkhours", TranslationValue = "检查工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.inspectionworkhours", TranslationValue = "检查工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.inspectionworkhours", TranslationValue = "检查工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.inspectionworkhours", TranslationValue = "检查工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.aoiworkhours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.aoiworkhours", TranslationValue = "AOI工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.aoiworkhours", TranslationValue = "AOI工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.aoiworkhours", TranslationValue = "AOI工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.aoiworkhours", TranslationValue = "AOI工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.aoiworkhours", TranslationValue = "AOI工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.aoiworkhours", TranslationValue = "AOI工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.defectqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.defectqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.defectqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.defectqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.defectqty", TranslationValue = "不良数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.defectqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.defectqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.handplacement
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.handplacement", TranslationValue = "手贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.handplacement", TranslationValue = "手贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.handplacement", TranslationValue = "手贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.handplacement", TranslationValue = "手贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.handplacement", TranslationValue = "手贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.handplacement", TranslationValue = "手贴", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.serialnumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.serialnumber", TranslationValue = "流水号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.serialnumber", TranslationValue = "流水号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.serialnumber", TranslationValue = "流水号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.serialnumber", TranslationValue = "流水号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.serialnumber", TranslationValue = "流水号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.serialnumber", TranslationValue = "流水号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.content
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.content", TranslationValue = "内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.content", TranslationValue = "内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.content", TranslationValue = "内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.content", TranslationValue = "内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.content", TranslationValue = "内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.content", TranslationValue = "内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.defectlocation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.defectlocation", TranslationValue = "不良个所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbainspectiondetail.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbainspectiondetail.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbainspectiondetail.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbainspectiondetail.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbainspectiondetail.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbainspectiondetail.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbainspectiondetail.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
