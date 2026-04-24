// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQuartzLogEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktQuartzLog 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Statistics.Logging;

/// <summary>
/// TaktQuartzLog 实体翻译种子数据（自动生成，与 TaktQuartzLog.cs 属性一一对应）
/// </summary>
public class TaktQuartzLogEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktQuartzLogEntityTranslations();

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
    /// 获取所有 TaktQuartzLog 实体名称及字段翻译（自动生成，与 TaktQuartzLog.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktQuartzLogEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.quartzlog（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog._self", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog._self", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog._self", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog._self", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog._self", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog._self", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.username
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.jobname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "任务名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.jobgroup
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任务组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任务组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任务组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任务组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任务组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任务组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.triggername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "触发器名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.triggergroup
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "触发器组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "触发器组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "触发器组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "触发器组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "触发器组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "触发器组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.executestatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "执行状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.executeresult
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "执行结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "执行结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "执行结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "执行结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "执行结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "执行结果", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.errormsg
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.executetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "执行时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.costtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.jobdata
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任务参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任务参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任务参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任务参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任务参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任务参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.nextfiretime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "下一次执行时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.quartzlog.previousfiretime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "上一次执行时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
