// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSupplierEvaluationEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktSupplierEvaluation 实体字段翻译种子数据（自动生成）
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
/// TaktSupplierEvaluation 实体翻译种子数据（自动生成，与 TaktSupplierEvaluation.cs 属性一一对应）
/// </summary>
public class TaktSupplierEvaluationEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktSupplierEvaluationEntityTranslations();

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
    /// 获取所有 TaktSupplierEvaluation 实体名称及字段翻译（自动生成，与 TaktSupplierEvaluation.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktSupplierEvaluationEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.supplierevaluation（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation._self", TranslationValue = "供应商评价考核", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation._self", TranslationValue = "供应商评价考核", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation._self", TranslationValue = "供应商评价考核", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation._self", TranslationValue = "供应商评价考核", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation._self", TranslationValue = "供应商评价考核", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation._self", TranslationValue = "供应商评价考核", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.supplierevaluation.companycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.companycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.companycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.companycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.supplierevaluation.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.code", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.code", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.code", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.code", TranslationValue = "评价表编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.code", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.code", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.supplierevaluation.supplierid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.supplierid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.supplierid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.supplierid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.supplierid", TranslationValue = "供应商ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.supplierid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.supplierid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.supplierevaluation.suppliername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.suppliername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.suppliername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.suppliername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.suppliername", TranslationValue = "供应商名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.suppliername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.suppliername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.supplierevaluation.suppliercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.suppliercode", TranslationValue = "供应商编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.supplierevaluation.date
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.date", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.date", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.date", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.date", TranslationValue = "评价日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.supplierevaluation.period
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.period", TranslationValue = "评价周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.period", TranslationValue = "评价周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.period", TranslationValue = "评价周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.period", TranslationValue = "评价周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.period", TranslationValue = "评价周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.period", TranslationValue = "评价周期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.supplierevaluation.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.type", TranslationValue = "评价类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.supplierevaluation.evaluatorby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.evaluatorby", TranslationValue = "评价人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.evaluatorby", TranslationValue = "评价人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.evaluatorby", TranslationValue = "评价人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.evaluatorby", TranslationValue = "评价人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.evaluatorby", TranslationValue = "评价人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.evaluatorby", TranslationValue = "评价人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.supplierevaluation.dept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.dept", TranslationValue = "评价部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.dept", TranslationValue = "评价部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.dept", TranslationValue = "评价部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.dept", TranslationValue = "评价部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.dept", TranslationValue = "评价部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.dept", TranslationValue = "评价部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.supplierevaluation.overallrating
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.overallrating", TranslationValue = "总体评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.overallrating", TranslationValue = "总体评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.overallrating", TranslationValue = "总体评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.overallrating", TranslationValue = "总体评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.overallrating", TranslationValue = "总体评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.overallrating", TranslationValue = "总体评级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.supplierevaluation.totalscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.totalscore", TranslationValue = "综合评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.supplierevaluation.qualityscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.qualityscore", TranslationValue = "质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.qualityscore", TranslationValue = "质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.qualityscore", TranslationValue = "质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.qualityscore", TranslationValue = "质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.qualityscore", TranslationValue = "质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.qualityscore", TranslationValue = "质量评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.supplierevaluation.deliveryscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.deliveryscore", TranslationValue = "交付评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.deliveryscore", TranslationValue = "交付评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.deliveryscore", TranslationValue = "交付评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.deliveryscore", TranslationValue = "交付评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.deliveryscore", TranslationValue = "交付评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.deliveryscore", TranslationValue = "交付评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.supplierevaluation.pricescore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.pricescore", TranslationValue = "Price", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.pricescore", TranslationValue = "価格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.pricescore", TranslationValue = "가격", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.pricescore", TranslationValue = "价格评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.pricescore", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.pricescore", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.supplierevaluation.servicescore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.servicescore", TranslationValue = "服务评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.servicescore", TranslationValue = "服务评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.servicescore", TranslationValue = "服务评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.servicescore", TranslationValue = "服务评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.servicescore", TranslationValue = "服务评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.servicescore", TranslationValue = "服务评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.supplierevaluation.technicalscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.technicalscore", TranslationValue = "技术能力评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.technicalscore", TranslationValue = "技术能力评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.technicalscore", TranslationValue = "技术能力评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.technicalscore", TranslationValue = "技术能力评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.technicalscore", TranslationValue = "技术能力评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.technicalscore", TranslationValue = "技术能力评分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.supplierevaluation.mainstrengths
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.mainstrengths", TranslationValue = "主要优点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.mainstrengths", TranslationValue = "主要优点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.mainstrengths", TranslationValue = "主要优点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.mainstrengths", TranslationValue = "主要优点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.mainstrengths", TranslationValue = "主要优点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.mainstrengths", TranslationValue = "主要优点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.supplierevaluation.mainissues
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.mainissues", TranslationValue = "主要问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.mainissues", TranslationValue = "主要问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.mainissues", TranslationValue = "主要问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.mainissues", TranslationValue = "主要问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.mainissues", TranslationValue = "主要问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.mainissues", TranslationValue = "主要问题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.supplierevaluation.improvementrequirements
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.improvementrequirements", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.improvementrequirements", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.improvementrequirements", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.improvementrequirements", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.improvementrequirements", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.improvementrequirements", TranslationValue = "改进要求", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.supplierevaluation.conclusion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.conclusion", TranslationValue = "考核结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.conclusion", TranslationValue = "考核结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.conclusion", TranslationValue = "考核结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.conclusion", TranslationValue = "考核结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.conclusion", TranslationValue = "考核结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.conclusion", TranslationValue = "考核结论", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.supplierevaluation.rectificationdeadline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.rectificationdeadline", TranslationValue = "整改期限", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.supplierevaluation.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.status", TranslationValue = "评价状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.supplierevaluation.rectificationstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.rectificationstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.rectificationstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.rectificationstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.rectificationstatus", TranslationValue = "整改跟进状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.rectificationstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.rectificationstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.supplierevaluation.relatedplant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },

            // entity.supplierevaluation.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.supplierevaluation.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.supplierevaluation.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.supplierevaluation.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.supplierevaluation.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.supplierevaluation.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.supplierevaluation.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
        };
    }
}
