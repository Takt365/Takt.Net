// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCustomerSatisfactionSurveyEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktCustomerSatisfactionSurvey 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Complaint;

/// <summary>
/// TaktCustomerSatisfactionSurvey 实体翻译种子数据（自动生成，与 TaktCustomerSatisfactionSurvey.cs 属性一一对应）
/// </summary>
public class TaktCustomerSatisfactionSurveyEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktCustomerSatisfactionSurveyEntityTranslations();

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
    /// 获取所有 TaktCustomerSatisfactionSurvey 实体名称及字段翻译（自动生成，与 TaktCustomerSatisfactionSurvey.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktCustomerSatisfactionSurveyEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.customersatisfactionsurvey（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey._self", TranslationValue = "客户满意度调查", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey._self", TranslationValue = "客户满意度调查", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey._self", TranslationValue = "客户满意度调查", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey._self", TranslationValue = "客户满意度调查", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey._self", TranslationValue = "客户满意度调查", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey._self", TranslationValue = "客户满意度调查", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.customersatisfactionsurvey.companycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.companycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.companycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.companycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.customersatisfactionsurvey.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.code", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.code", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.code", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.code", TranslationValue = "调查表编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.code", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.code", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.customersatisfactionsurvey.customerid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.customerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.customerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.customerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.customerid", TranslationValue = "客户ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.customerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.customerid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.customersatisfactionsurvey.customername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.customername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.customername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.customername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.customername", TranslationValue = "客户名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.customername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.customername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.customersatisfactionsurvey.customercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.customercode", TranslationValue = "客户编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.customersatisfactionsurvey.date
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.date", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.date", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.date", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.date", TranslationValue = "调查日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.customersatisfactionsurvey.method
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.method", TranslationValue = "调查方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.method", TranslationValue = "调查方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.method", TranslationValue = "调查方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.method", TranslationValue = "调查方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.method", TranslationValue = "调查方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.method", TranslationValue = "调查方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.customersatisfactionsurvey.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.type", TranslationValue = "调查类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.customersatisfactionsurvey.period
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.period", TranslationValue = "调查周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.period", TranslationValue = "调查周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.period", TranslationValue = "调查周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.period", TranslationValue = "调查周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.period", TranslationValue = "调查周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.period", TranslationValue = "调查周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.customersatisfactionsurvey.orby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.orby", TranslationValue = "调查人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.orby", TranslationValue = "调查人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.orby", TranslationValue = "调查人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.orby", TranslationValue = "调查人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.orby", TranslationValue = "调查人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.orby", TranslationValue = "调查人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.customersatisfactionsurvey.customercontact
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.customercontact", TranslationValue = "客户联系人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.customercontact", TranslationValue = "客户联系人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.customercontact", TranslationValue = "客户联系人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.customercontact", TranslationValue = "客户联系人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.customercontact", TranslationValue = "客户联系人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.customercontact", TranslationValue = "客户联系人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.customersatisfactionsurvey.customerphone
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.customerphone", TranslationValue = "客户联系电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.customerphone", TranslationValue = "客户联系电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.customerphone", TranslationValue = "客户联系电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.customerphone", TranslationValue = "客户联系电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.customerphone", TranslationValue = "客户联系电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.customerphone", TranslationValue = "客户联系电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.customersatisfactionsurvey.overallsatisfaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.overallsatisfaction", TranslationValue = "整体满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.overallsatisfaction", TranslationValue = "整体满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.overallsatisfaction", TranslationValue = "整体满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.overallsatisfaction", TranslationValue = "整体满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.overallsatisfaction", TranslationValue = "整体满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.overallsatisfaction", TranslationValue = "整体满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.customersatisfactionsurvey.totalscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.customersatisfactionsurvey.qualityscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.qualityscore", TranslationValue = "产品质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.qualityscore", TranslationValue = "产品质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.qualityscore", TranslationValue = "产品质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.qualityscore", TranslationValue = "产品质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.qualityscore", TranslationValue = "产品质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.qualityscore", TranslationValue = "产品质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.customersatisfactionsurvey.deliveryscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.deliveryscore", TranslationValue = "交付准时率评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.deliveryscore", TranslationValue = "交付准时率评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.deliveryscore", TranslationValue = "交付准时率评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.deliveryscore", TranslationValue = "交付准时率评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.deliveryscore", TranslationValue = "交付准时率评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.deliveryscore", TranslationValue = "交付准时率评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.customersatisfactionsurvey.servicescore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.servicescore", TranslationValue = "服务质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.servicescore", TranslationValue = "服务质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.servicescore", TranslationValue = "服务质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.servicescore", TranslationValue = "服务质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.servicescore", TranslationValue = "服务质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.servicescore", TranslationValue = "服务质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.customersatisfactionsurvey.pricescore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.pricescore", TranslationValue = "Price", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.pricescore", TranslationValue = "価格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.pricescore", TranslationValue = "가격", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.pricescore", TranslationValue = "价格竞争力评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.pricescore", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.pricescore", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.customersatisfactionsurvey.technicalscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.technicalscore", TranslationValue = "技术支持评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.technicalscore", TranslationValue = "技术支持评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.technicalscore", TranslationValue = "技术支持评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.technicalscore", TranslationValue = "技术支持评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.technicalscore", TranslationValue = "技术支持评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.technicalscore", TranslationValue = "技术支持评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.customersatisfactionsurvey.customerpraise
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.customerpraise", TranslationValue = "客户主要表扬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.customerpraise", TranslationValue = "客户主要表扬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.customerpraise", TranslationValue = "客户主要表扬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.customerpraise", TranslationValue = "客户主要表扬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.customerpraise", TranslationValue = "客户主要表扬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.customerpraise", TranslationValue = "客户主要表扬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.customersatisfactionsurvey.customerfeedback
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.customerfeedback", TranslationValue = "客户意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.customerfeedback", TranslationValue = "客户意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.customerfeedback", TranslationValue = "客户意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.customerfeedback", TranslationValue = "客户意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.customerfeedback", TranslationValue = "客户意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.customerfeedback", TranslationValue = "客户意见", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.customersatisfactionsurvey.improvementplan
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.improvementplan", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.improvementplan", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.improvementplan", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.improvementplan", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.improvementplan", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.improvementplan", TranslationValue = "改进计划", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.customersatisfactionsurvey.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.status", TranslationValue = "调查状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.customersatisfactionsurvey.followupstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.followupstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.followupstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.followupstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.followupstatus", TranslationValue = "跟进状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.followupstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.followupstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.customersatisfactionsurvey.relatedcomplaintid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.relatedcomplaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.relatedcomplaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.relatedcomplaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.relatedcomplaintid", TranslationValue = "关联客诉ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.relatedcomplaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.relatedcomplaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },

            // entity.customersatisfactionsurvey.relatedplant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },

            // entity.customersatisfactionsurvey.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customersatisfactionsurvey.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customersatisfactionsurvey.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customersatisfactionsurvey.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customersatisfactionsurvey.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customersatisfactionsurvey.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customersatisfactionsurvey.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
        };
    }
}
