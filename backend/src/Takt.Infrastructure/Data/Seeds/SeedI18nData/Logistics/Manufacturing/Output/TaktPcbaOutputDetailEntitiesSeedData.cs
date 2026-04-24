// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPcbaOutputDetailEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPcbaOutputDetail 实体字段翻译种子数据（自动生成）
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
/// TaktPcbaOutputDetail 实体翻译种子数据（自动生成，与 TaktPcbaOutputDetail.cs 属性一一对应）
/// </summary>
public class TaktPcbaOutputDetailEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktPcbaOutputDetailEntityTranslations();

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
    /// 获取所有 TaktPcbaOutputDetail 实体名称及字段翻译（自动生成，与 TaktPcbaOutputDetail.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPcbaOutputDetailEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.pcbaoutputdetail（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail._self", TranslationValue = "PCBA日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail._self", TranslationValue = "PCBA日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail._self", TranslationValue = "PCBA日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail._self", TranslationValue = "PCBA日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail._self", TranslationValue = "PCBA日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail._self", TranslationValue = "PCBA日报明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.pcbaoutputid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.pcbaoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.pcbaoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.pcbaoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.pcbaoutputid", TranslationValue = "PCBA日报ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.pcbaoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.pcbaoutputid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.timeperiod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.timeperiod", TranslationValue = "生产时段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.shiftno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.shiftno", TranslationValue = "班组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.shiftno", TranslationValue = "班组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.shiftno", TranslationValue = "班组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.shiftno", TranslationValue = "班组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.shiftno", TranslationValue = "班组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.shiftno", TranslationValue = "班组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.pcbboardtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.pcbboardtype", TranslationValue = "PCB板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.pcbboardtype", TranslationValue = "PCB板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.pcbboardtype", TranslationValue = "PCB板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.pcbboardtype", TranslationValue = "PCB板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.pcbboardtype", TranslationValue = "PCB板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.pcbboardtype", TranslationValue = "PCB板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.panelside
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.panelside", TranslationValue = "面板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.panelside", TranslationValue = "面板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.panelside", TranslationValue = "面板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.panelside", TranslationValue = "面板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.panelside", TranslationValue = "面板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.panelside", TranslationValue = "面板别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.batchqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.batchqty", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.batchqty", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.batchqty", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.batchqty", TranslationValue = "批次数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.batchqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.batchqty", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.dailycompletedqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.dailycompletedqty", TranslationValue = "当日完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.dailycompletedqty", TranslationValue = "当日完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.dailycompletedqty", TranslationValue = "当日完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.dailycompletedqty", TranslationValue = "当日完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.dailycompletedqty", TranslationValue = "当日完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.dailycompletedqty", TranslationValue = "当日完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.totalcompletedqty
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.totalcompletedqty", TranslationValue = "累计完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.totalcompletedqty", TranslationValue = "累计完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.totalcompletedqty", TranslationValue = "累计完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.totalcompletedqty", TranslationValue = "累计完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.totalcompletedqty", TranslationValue = "累计完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.totalcompletedqty", TranslationValue = "累计完成数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.completedstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.completedstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.completedstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.completedstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.completedstatus", TranslationValue = "完成状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.completedstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.completedstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.serialno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.serialno", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.defectcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.defectcount", TranslationValue = "不良台数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.defectcount", TranslationValue = "不良台数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.defectcount", TranslationValue = "不良台数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.defectcount", TranslationValue = "不良台数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.defectcount", TranslationValue = "不良台数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.defectcount", TranslationValue = "不良台数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.inputminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.inputminutes", TranslationValue = "投入工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.inputminutes", TranslationValue = "投入工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.inputminutes", TranslationValue = "投入工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.inputminutes", TranslationValue = "投入工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.inputminutes", TranslationValue = "投入工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.inputminutes", TranslationValue = "投入工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.repairminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.repairminutes", TranslationValue = "修工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.repairminutes", TranslationValue = "修工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.repairminutes", TranslationValue = "修工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.repairminutes", TranslationValue = "修工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.repairminutes", TranslationValue = "修工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.repairminutes", TranslationValue = "修工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.switchcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.switchcount", TranslationValue = "切换次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.switchcount", TranslationValue = "切换次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.switchcount", TranslationValue = "切换次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.switchcount", TranslationValue = "切换次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.switchcount", TranslationValue = "切换次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.switchcount", TranslationValue = "切换次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.switchtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.switchtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.switchtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.switchtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.switchtime", TranslationValue = "切换时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.switchtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.switchtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.stoptime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.stoptime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.stoptime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.stoptime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.stoptime", TranslationValue = "切停机时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.stoptime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.stoptime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.totalminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.totalminutes", TranslationValue = "总工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.totalminutes", TranslationValue = "总工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.totalminutes", TranslationValue = "总工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.totalminutes", TranslationValue = "总工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.totalminutes", TranslationValue = "总工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.totalminutes", TranslationValue = "总工数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.unachievedreason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.unachievedreason", TranslationValue = "未达成原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.pcbaoutputdetail.unachieveddescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.pcbaoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.pcbaoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.pcbaoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.pcbaoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.pcbaoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.pcbaoutputdetail.unachieveddescription", TranslationValue = "未达成说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
