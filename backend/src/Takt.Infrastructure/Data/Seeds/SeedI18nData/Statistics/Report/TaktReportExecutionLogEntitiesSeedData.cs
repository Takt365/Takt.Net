// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktReportExecutionLogEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktReportExecutionLog 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Statistics.Report;

/// <summary>
/// TaktReportExecutionLog 实体翻译种子数据（自动生成，与 TaktReportExecutionLog.cs 属性一一对应）
/// </summary>
public class TaktReportExecutionLogEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktReportExecutionLogEntityTranslations();

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
    /// 获取所有 TaktReportExecutionLog 实体名称及字段翻译（自动生成，与 TaktReportExecutionLog.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktReportExecutionLogEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.reportexecutionlog（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog._self", TranslationValue = "报表执行日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog._self", TranslationValue = "报表执行日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog._self", TranslationValue = "报表执行日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog._self", TranslationValue = "报表执行日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog._self", TranslationValue = "报表执行日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog._self", TranslationValue = "报表执行日志", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.reportexecutionlog.reportid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.reportid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.reportid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.reportid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.reportid", TranslationValue = "报表定义ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.reportid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.reportid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.reportexecutionlog.userid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.userid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.userid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.userid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.userid", TranslationValue = "用户ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.userid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.userid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.reportexecutionlog.executiontime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.executiontime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.executiontime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.executiontime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.executiontime", TranslationValue = "执行时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.executiontime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.executiontime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.reportexecutionlog.variantname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.variantname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.variantname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.variantname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.variantname", TranslationValue = "变式名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.variantname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.variantname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.reportexecutionlog.selectionparameters
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.selectionparameters", TranslationValue = "选择屏幕参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.selectionparameters", TranslationValue = "选择屏幕参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.selectionparameters", TranslationValue = "选择屏幕参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.selectionparameters", TranslationValue = "选择屏幕参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.selectionparameters", TranslationValue = "选择屏幕参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.selectionparameters", TranslationValue = "选择屏幕参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.reportexecutionlog.layoutvariant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.layoutvariant", TranslationValue = "布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.layoutvariant", TranslationValue = "布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.layoutvariant", TranslationValue = "布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.layoutvariant", TranslationValue = "布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.layoutvariant", TranslationValue = "布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.layoutvariant", TranslationValue = "布局变式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.reportexecutionlog.executiontype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.executiontype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.executiontype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.executiontype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.executiontype", TranslationValue = "执行类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.executiontype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.executiontype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.reportexecutionlog.backgroundjobname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.backgroundjobname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.backgroundjobname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.backgroundjobname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.backgroundjobname", TranslationValue = "后台作业名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.backgroundjobname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.backgroundjobname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.reportexecutionlog.backgroundjobcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.backgroundjobcount", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.backgroundjobcount", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.backgroundjobcount", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.backgroundjobcount", TranslationValue = "后台作业编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.backgroundjobcount", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.backgroundjobcount", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.reportexecutionlog.executiondurationms
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.executiondurationms", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.executiondurationms", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.executiondurationms", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.executiondurationms", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.executiondurationms", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.executiondurationms", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.reportexecutionlog.rowcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.rowcount", TranslationValue = "返回数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.rowcount", TranslationValue = "返回数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.rowcount", TranslationValue = "返回数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.rowcount", TranslationValue = "返回数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.rowcount", TranslationValue = "返回数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.rowcount", TranslationValue = "返回数据行数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.reportexecutionlog.issuccess
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.issuccess", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.issuccess", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.issuccess", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.issuccess", TranslationValue = "是否成功", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.issuccess", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.issuccess", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.reportexecutionlog.errormessage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.errormessage", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.errormessage", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.errormessage", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.errormessage", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.errormessage", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.errormessage", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.reportexecutionlog.messagetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.messagetype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.messagetype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.messagetype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.messagetype", TranslationValue = "消息类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.messagetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.messagetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.reportexecutionlog.messagenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.messagenumber", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.messagenumber", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.messagenumber", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.messagenumber", TranslationValue = "消息编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.messagenumber", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.messagenumber", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.reportexecutionlog.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.plantcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.plantcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.plantcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.reportexecutionlog.companycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.companycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.companycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.companycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.reportexecutionlog.clientip
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.clientip", TranslationValue = "客户端IP", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.clientip", TranslationValue = "客户端IP", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.clientip", TranslationValue = "客户端IP", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.clientip", TranslationValue = "客户端IP", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.clientip", TranslationValue = "客户端IP", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.clientip", TranslationValue = "客户端IP", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.reportexecutionlog.terminalname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.terminalname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.terminalname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.terminalname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.terminalname", TranslationValue = "终端名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.terminalname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.terminalname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.reportexecutionlog.outputtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.outputtype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.outputtype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.outputtype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.outputtype", TranslationValue = "输出类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.outputtype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.outputtype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.reportexecutionlog.spoolrequestno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.spoolrequestno", TranslationValue = "Spool请求号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.spoolrequestno", TranslationValue = "Spool请求号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.spoolrequestno", TranslationValue = "Spool请求号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.spoolrequestno", TranslationValue = "Spool请求号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.spoolrequestno", TranslationValue = "Spool请求号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.spoolrequestno", TranslationValue = "Spool请求号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.reportexecutionlog.isexport
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.isexport", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.isexport", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.isexport", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.isexport", TranslationValue = "是否导出", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.isexport", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.isexport", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.reportexecutionlog.exportformat
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.exportformat", TranslationValue = "导出格式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.exportformat", TranslationValue = "导出格式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.exportformat", TranslationValue = "导出格式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.exportformat", TranslationValue = "导出格式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.exportformat", TranslationValue = "导出格式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.exportformat", TranslationValue = "导出格式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.reportexecutionlog.exportfilepath
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.exportfilepath", TranslationValue = "导出文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.exportfilepath", TranslationValue = "导出文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.exportfilepath", TranslationValue = "导出文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.exportfilepath", TranslationValue = "导出文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.exportfilepath", TranslationValue = "导出文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.exportfilepath", TranslationValue = "导出文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.reportexecutionlog.isdownloaded
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.isdownloaded", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.isdownloaded", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.isdownloaded", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.isdownloaded", TranslationValue = "是否下载", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.isdownloaded", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.isdownloaded", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },

            // entity.reportexecutionlog.downloadtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.reportexecutionlog.downloadtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.reportexecutionlog.downloadtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.reportexecutionlog.downloadtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.reportexecutionlog.downloadtime", TranslationValue = "下载时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.reportexecutionlog.downloadtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.reportexecutionlog.downloadtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
        };
    }
}
