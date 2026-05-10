// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSalaryAdjustmentEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktSalaryAdjustment 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.CompensationBenefits;

/// <summary>
/// TaktSalaryAdjustment 实体翻译种子数据（自动生成，与 TaktSalaryAdjustment.cs 属性一一对应）
/// </summary>
public class TaktSalaryAdjustmentEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktSalaryAdjustmentEntityTranslations();

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
    /// 获取所有 TaktSalaryAdjustment 实体名称及字段翻译（自动生成，与 TaktSalaryAdjustment.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktSalaryAdjustmentEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.salaryadjustment（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment._self", TranslationValue = "薪资调整", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment._self", TranslationValue = "薪资调整", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment._self", TranslationValue = "薪资调整", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment._self", TranslationValue = "薪资调整", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment._self", TranslationValue = "薪资调整", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment._self", TranslationValue = "薪资调整", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.salaryadjustment.employeeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.salaryadjustment.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.type", TranslationValue = "调整类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.salaryadjustment.date
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.date", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.date", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.date", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.date", TranslationValue = "调整日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.salaryadjustment.previoussalary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.previoussalary", TranslationValue = "调整前薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.previoussalary", TranslationValue = "调整前薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.previoussalary", TranslationValue = "调整前薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.previoussalary", TranslationValue = "调整前薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.previoussalary", TranslationValue = "调整前薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.previoussalary", TranslationValue = "调整前薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.salaryadjustment.newsalary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.newsalary", TranslationValue = "调整后薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.newsalary", TranslationValue = "调整后薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.newsalary", TranslationValue = "调整后薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.newsalary", TranslationValue = "调整后薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.newsalary", TranslationValue = "调整后薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.newsalary", TranslationValue = "调整后薪资", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.salaryadjustment.amount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.amount", TranslationValue = "Amount", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.amount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.amount", TranslationValue = "금액", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.amount", TranslationValue = "调整金额", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.amount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.amount", TranslationValue = "金額", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.salaryadjustment.percentage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.percentage", TranslationValue = "调整比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.percentage", TranslationValue = "调整比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.percentage", TranslationValue = "调整比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.percentage", TranslationValue = "调整比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.percentage", TranslationValue = "调整比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.percentage", TranslationValue = "调整比例", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.salaryadjustment.reason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.reason", TranslationValue = "调薪原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.reason", TranslationValue = "调薪原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.reason", TranslationValue = "调薪原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.reason", TranslationValue = "调薪原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.reason", TranslationValue = "调薪原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.reason", TranslationValue = "调薪原因", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.salaryadjustment.previoussalarylevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.previoussalarylevel", TranslationValue = "调整前薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.previoussalarylevel", TranslationValue = "调整前薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.previoussalarylevel", TranslationValue = "调整前薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.previoussalarylevel", TranslationValue = "调整前薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.previoussalarylevel", TranslationValue = "调整前薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.previoussalarylevel", TranslationValue = "调整前薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.salaryadjustment.newsalarylevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.newsalarylevel", TranslationValue = "调整后薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.newsalarylevel", TranslationValue = "调整后薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.newsalarylevel", TranslationValue = "调整后薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.newsalarylevel", TranslationValue = "调整后薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.newsalarylevel", TranslationValue = "调整后薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.newsalarylevel", TranslationValue = "调整后薪资等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.salaryadjustment.approverid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.approverid", TranslationValue = "审批人ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.salaryadjustment.approvaldate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.approvaldate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.approvaldate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.approvaldate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.approvaldate", TranslationValue = "审批日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.approvaldate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.approvaldate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.salaryadjustment.approvalcomments
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.approvalcomments", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.approvalcomments", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.approvalcomments", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.approvalcomments", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.approvalcomments", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.approvalcomments", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.salaryadjustment.effectivedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.effectivedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.effectivedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.effectivedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.salaryadjustment.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.salaryadjustment.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.salaryadjustment.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.salaryadjustment.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.salaryadjustment.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.salaryadjustment.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.salaryadjustment.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
        };
    }
}
