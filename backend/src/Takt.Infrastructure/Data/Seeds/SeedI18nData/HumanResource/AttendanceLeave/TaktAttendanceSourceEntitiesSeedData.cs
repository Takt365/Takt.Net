// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktAttendanceSourceEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktAttendanceSource 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.AttendanceLeave;

/// <summary>
/// TaktAttendanceSource 实体翻译种子数据（自动生成，与 TaktAttendanceSource.cs 属性一一对应）
/// </summary>
public class TaktAttendanceSourceEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktAttendanceSourceEntityTranslations();

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
    /// 获取所有 TaktAttendanceSource 实体名称及字段翻译（自动生成，与 TaktAttendanceSource.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktAttendanceSourceEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.attendancesource（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource._self", TranslationValue = "考勤源记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource._self", TranslationValue = "考勤源记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource._self", TranslationValue = "考勤源记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource._self", TranslationValue = "考勤源记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource._self", TranslationValue = "考勤源记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource._self", TranslationValue = "考勤源记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.deviceid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.deviceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.deviceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.deviceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.deviceid", TranslationValue = "设备ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.deviceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.deviceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.employeeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.enrollnumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.enrollnumber", TranslationValue = "登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.enrollnumber", TranslationValue = "登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.enrollnumber", TranslationValue = "登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.enrollnumber", TranslationValue = "登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.enrollnumber", TranslationValue = "登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.enrollnumber", TranslationValue = "登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.rawpunchtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.rawpunchtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.rawpunchtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.rawpunchtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.rawpunchtime", TranslationValue = "原始打卡时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.rawpunchtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.rawpunchtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.verifymode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.verifymode", TranslationValue = "验证方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.verifymode", TranslationValue = "验证方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.verifymode", TranslationValue = "验证方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.verifymode", TranslationValue = "验证方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.verifymode", TranslationValue = "验证方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.verifymode", TranslationValue = "验证方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.externalrecordkey
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.externalrecordkey", TranslationValue = "外部记录键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.externalrecordkey", TranslationValue = "外部记录键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.externalrecordkey", TranslationValue = "外部记录键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.externalrecordkey", TranslationValue = "外部记录键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.externalrecordkey", TranslationValue = "外部记录键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.externalrecordkey", TranslationValue = "外部记录键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.downloadbatchno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.downloadbatchno", TranslationValue = "下载批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.downloadbatchno", TranslationValue = "下载批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.downloadbatchno", TranslationValue = "下载批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.downloadbatchno", TranslationValue = "下载批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.downloadbatchno", TranslationValue = "下载批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.downloadbatchno", TranslationValue = "下载批次", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.attendancesource.rawpayloadjson
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesource.rawpayloadjson", TranslationValue = "原始JSON", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesource.rawpayloadjson", TranslationValue = "原始JSON", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesource.rawpayloadjson", TranslationValue = "原始JSON", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesource.rawpayloadjson", TranslationValue = "原始JSON", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesource.rawpayloadjson", TranslationValue = "原始JSON", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.attendancesource.rawpayloadjson", TranslationValue = "原始JSON", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
