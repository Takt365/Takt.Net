// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCountersignEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktCountersign 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Accounting.Financial;

/// <summary>
/// TaktCountersign 实体翻译种子数据（自动生成，与 TaktCountersign.cs 属性一一对应）
/// </summary>
public class TaktCountersignEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktCountersignEntityTranslations();

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
    /// 获取所有 TaktCountersign 实体名称及字段翻译（自动生成，与 TaktCountersign.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktCountersignEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.countersign（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.countersign.companycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.companycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.companycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.companycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.countersign.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.code", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.code", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.code", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.code", TranslationValue = "会签编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.code", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.code", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.countersign.depts
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.depts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.depts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.depts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.depts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.depts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.depts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.countersign.financedept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.countersign.budgetreviewcomment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.countersign.executiveoffice
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.countersign.approvaldate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.approvaldate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.approvaldate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.approvaldate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.approvaldate", TranslationValue = "承认日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.approvaldate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.approvaldate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.countersign.applicationdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.applicationdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.applicationdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.applicationdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.applicationdate", TranslationValue = "申请日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.applicationdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.applicationdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.countersign.applicantid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.applicantid", TranslationValue = "申请人员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.countersign.applicantby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.countersign.applicationdept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.countersign.costbearerdept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.countersign.isbudget
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.isbudget", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.isbudget", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.isbudget", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.isbudget", TranslationValue = "是否有预算", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.isbudget", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.isbudget", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.countersign.budgetitem
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.countersign.budgetamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.budgetamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.budgetamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.budgetamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.budgetamount", TranslationValue = "预算金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.budgetamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.budgetamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.countersign.applicationamount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.applicationamount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.applicationamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.applicationamount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.applicationamount", TranslationValue = "申请金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.applicationamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.applicationamount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.countersign.title
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.title", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.title", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.title", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.title", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.title", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.title", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.countersign.applicationreason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.countersign.budgetusagedescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.countersign.targetandexpectedbenefit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.countersign.attachments
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.countersign.flowinstanceid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.flowinstanceid", TranslationValue = "流程实例ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.countersign.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersign.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersign.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersign.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersign.status", TranslationValue = "会签单状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersign.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.countersign.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
        };
    }
}
