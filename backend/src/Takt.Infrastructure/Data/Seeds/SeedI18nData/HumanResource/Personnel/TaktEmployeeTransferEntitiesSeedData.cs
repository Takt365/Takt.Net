// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktEmployeeTransferEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktEmployeeTransfer 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.Personnel;

/// <summary>
/// TaktEmployeeTransfer 实体翻译种子数据（自动生成，与 TaktEmployeeTransfer.cs 属性一一对应）
/// </summary>
public class TaktEmployeeTransferEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktEmployeeTransferEntityTranslations();

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
    /// 获取所有 TaktEmployeeTransfer 实体名称及字段翻译（自动生成，与 TaktEmployeeTransfer.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktEmployeeTransferEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.employeetransfer（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer._self", TranslationValue = "员工调动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer._self", TranslationValue = "员工调动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer._self", TranslationValue = "员工调动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer._self", TranslationValue = "员工调动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer._self", TranslationValue = "员工调动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer._self", TranslationValue = "员工调动", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.employeetransfer.employeeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.employeetransfer.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.linenumber", TranslationValue = "项号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.employeetransfer.applicantid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.applicantid", TranslationValue = "申请人员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.applicantid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.employeetransfer.applicantby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.applicantby", TranslationValue = "申请人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.employeetransfer.applicationdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.applicationdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.applicationdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.applicationdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.applicationdate", TranslationValue = "申请日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.applicationdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.applicationdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.employeetransfer.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.type", TranslationValue = "调动类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.employeetransfer.fromdeptid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.fromdeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.fromdeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.fromdeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.fromdeptid", TranslationValue = "原部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.fromdeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.fromdeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.employeetransfer.fromdeptname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.fromdeptname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.fromdeptname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.fromdeptname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.fromdeptname", TranslationValue = "原部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.fromdeptname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.fromdeptname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.employeetransfer.frompostid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.frompostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.frompostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.frompostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.frompostid", TranslationValue = "原岗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.frompostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.frompostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.employeetransfer.frompostname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.frompostname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.frompostname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.frompostname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.frompostname", TranslationValue = "原岗位名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.frompostname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.frompostname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.employeetransfer.todeptid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.todeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.todeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.todeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.todeptid", TranslationValue = "目标部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.todeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.todeptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.employeetransfer.todeptname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.todeptname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.todeptname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.todeptname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.todeptname", TranslationValue = "目标部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.todeptname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.todeptname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.employeetransfer.topostid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.topostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.topostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.topostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.topostid", TranslationValue = "目标岗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.topostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.topostid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.employeetransfer.topostname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.topostname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.topostname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.topostname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.topostname", TranslationValue = "目标岗位名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.topostname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.topostname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.employeetransfer.effectivedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.effectivedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.effectivedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.effectivedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.effectivedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.employeetransfer.reason
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.reason", TranslationValue = "申请事由", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.reason", TranslationValue = "申请事由", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.reason", TranslationValue = "申请事由", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.reason", TranslationValue = "申请事由", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.reason", TranslationValue = "申请事由", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.reason", TranslationValue = "申请事由", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.employeetransfer.approverid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.approverid", TranslationValue = "审批人员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.approverid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.employeetransfer.approverby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.approverby", TranslationValue = "审批人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.approverby", TranslationValue = "审批人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.approverby", TranslationValue = "审批人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.approverby", TranslationValue = "审批人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.approverby", TranslationValue = "审批人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.approverby", TranslationValue = "审批人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.employeetransfer.approvetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.approvetime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.approvetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.approvetime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.approvetime", TranslationValue = "审批时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.approvetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.approvetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.employeetransfer.approvecomment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.approvecomment", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.approvecomment", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.approvecomment", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.approvecomment", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.approvecomment", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.approvecomment", TranslationValue = "审批意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.employeetransfer.handlingid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.handlingid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.handlingid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.handlingid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.handlingid", TranslationValue = "经办人员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.handlingid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.handlingid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.employeetransfer.handlingby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.handlingby", TranslationValue = "经办人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.handlingby", TranslationValue = "经办人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.handlingby", TranslationValue = "经办人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.handlingby", TranslationValue = "经办人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.handlingby", TranslationValue = "经办人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.handlingby", TranslationValue = "经办人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.employeetransfer.handlingtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.handlingtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.handlingtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.handlingtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.handlingtime", TranslationValue = "经办时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.handlingtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.handlingtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.employeetransfer.handlingcomment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.handlingcomment", TranslationValue = "Remark", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.handlingcomment", TranslationValue = "備考", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.handlingcomment", TranslationValue = "비고", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.handlingcomment", TranslationValue = "经办备注", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.handlingcomment", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.handlingcomment", TranslationValue = "備註", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.employeetransfer.flowinstanceid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.flowinstanceid", TranslationValue = "流程实例ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },

            // entity.employeetransfer.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeetransfer.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeetransfer.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeetransfer.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeetransfer.status", TranslationValue = "调动状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeetransfer.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employeetransfer.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
        };
    }
}
