// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktApsScheduleItemEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktApsScheduleItem 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Manufacturing.Scheduling;

/// <summary>
/// TaktApsScheduleItem 实体翻译种子数据（自动生成，与 TaktApsScheduleItem.cs 属性一一对应）
/// </summary>
public class TaktApsScheduleItemEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktApsScheduleItemEntityTranslations();

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
    /// 获取所有 TaktApsScheduleItem 实体名称及字段翻译（自动生成，与 TaktApsScheduleItem.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktApsScheduleItemEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.apsscheduleitem（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem._self", TranslationValue = "APS排程明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem._self", TranslationValue = "APS排程明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem._self", TranslationValue = "APS排程明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem._self", TranslationValue = "APS排程明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem._self", TranslationValue = "APS排程明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem._self", TranslationValue = "APS排程明细", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.apsscheduleitem.apsscheduleid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.apsscheduleid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.apsscheduleid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.apsscheduleid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.apsscheduleid", TranslationValue = "APS排程ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.apsscheduleid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.apsscheduleid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.apsscheduleitem.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.apsscheduleitem.workordercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.workordercode", TranslationValue = "生产工单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.workordercode", TranslationValue = "生产工单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.workordercode", TranslationValue = "生产工单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.workordercode", TranslationValue = "生产工单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.workordercode", TranslationValue = "生产工单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.workordercode", TranslationValue = "生产工单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.apsscheduleitem.workorderid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.workorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.workorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.workorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.workorderid", TranslationValue = "生产工单ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.workorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.workorderid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.apsscheduleitem.productcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.productcode", TranslationValue = "产品编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.productcode", TranslationValue = "产品编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.productcode", TranslationValue = "产品编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.productcode", TranslationValue = "产品编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.productcode", TranslationValue = "产品编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.productcode", TranslationValue = "产品编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.apsscheduleitem.productname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.productname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.productname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.productname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.productname", TranslationValue = "产品名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.productname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.productname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.apsscheduleitem.workcentercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.workcentercode", TranslationValue = "工作中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.workcentercode", TranslationValue = "工作中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.workcentercode", TranslationValue = "工作中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.workcentercode", TranslationValue = "工作中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.workcentercode", TranslationValue = "工作中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.workcentercode", TranslationValue = "工作中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.apsscheduleitem.workcentername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.workcentername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.workcentername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.workcentername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.workcentername", TranslationValue = "工作中心名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.workcentername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.workcentername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.apsscheduleitem.processcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.processcode", TranslationValue = "工序编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.apsscheduleitem.processname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.processname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.processname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.processname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.processname", TranslationValue = "工序名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.processname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.processname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.apsscheduleitem.processsequence
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.processsequence", TranslationValue = "工序序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.processsequence", TranslationValue = "工序序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.processsequence", TranslationValue = "工序序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.processsequence", TranslationValue = "工序序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.processsequence", TranslationValue = "工序序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.processsequence", TranslationValue = "工序序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.apsscheduleitem.processstandardst
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.processstandardst", TranslationValue = "工序标准ST值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.processstandardst", TranslationValue = "工序标准ST值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.processstandardst", TranslationValue = "工序标准ST值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.processstandardst", TranslationValue = "工序标准ST值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.processstandardst", TranslationValue = "工序标准ST值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.processstandardst", TranslationValue = "工序标准ST值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.apsscheduleitem.processstandardstunit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.processstandardstunit", TranslationValue = "工序标准ST单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.processstandardstunit", TranslationValue = "工序标准ST单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.processstandardstunit", TranslationValue = "工序标准ST单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.processstandardstunit", TranslationValue = "工序标准ST单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.processstandardstunit", TranslationValue = "工序标准ST单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.processstandardstunit", TranslationValue = "工序标准ST单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.apsscheduleitem.extraminutes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.extraminutes", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.extraminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.extraminutes", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.extraminutes", TranslationValue = "额外时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.extraminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.extraminutes", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.apsscheduleitem.planquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.planquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.planquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.planquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.planquantity", TranslationValue = "计划数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.planquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.planquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.apsscheduleitem.planstarttime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.planstarttime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.planstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.planstarttime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.planstarttime", TranslationValue = "计划开始时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.planstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.planstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.apsscheduleitem.planendtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.planendtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.planendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.planendtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.planendtime", TranslationValue = "计划结束时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.planendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.planendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.apsscheduleitem.actualstarttime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.actualstarttime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.actualstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.actualstarttime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.actualstarttime", TranslationValue = "实际开始时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.actualstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.actualstarttime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.apsscheduleitem.actualendtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.actualendtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.actualendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.actualendtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.actualendtime", TranslationValue = "实际结束时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.actualendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.actualendtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.apsscheduleitem.equipmentcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.equipmentcode", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.equipmentcode", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.equipmentcode", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.equipmentcode", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.equipmentcode", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.equipmentcode", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.apsscheduleitem.equipmentname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.equipmentname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.equipmentname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.equipmentname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.equipmentname", TranslationValue = "设备名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.equipmentname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.equipmentname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.apsscheduleitem.teamcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.teamcode", TranslationValue = "班组编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.teamcode", TranslationValue = "班组编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.teamcode", TranslationValue = "班组编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.teamcode", TranslationValue = "班组编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.teamcode", TranslationValue = "班组编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.teamcode", TranslationValue = "班组编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.apsscheduleitem.teamname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.teamname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.teamname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.teamname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.teamname", TranslationValue = "班组名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.teamname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.teamname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.apsscheduleitem.processstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.processstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.processstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.processstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.processstatus", TranslationValue = "工序状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.processstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.processstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.apsscheduleitem.priority
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.apsscheduleitem.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.apsscheduleitem.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.apsscheduleitem.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.apsscheduleitem.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.apsscheduleitem.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.apsscheduleitem.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
        };
    }
}
