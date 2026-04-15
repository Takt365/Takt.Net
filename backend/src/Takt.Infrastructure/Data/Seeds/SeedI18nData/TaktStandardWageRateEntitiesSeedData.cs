// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktStandardWageRateEntitiesSeedData.cs
// 创建时间：2025-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktStandardWageRate 实体字段翻译种子数据，entity.standardwagerate / entity.standardwagerate.xxx，zh-CN 与 ColumnDescription 对齐
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// TaktStandardWageRate 实体翻译种子数据。严格按 TaktStandardWageRate.cs 实体属性顺序，ResourceKey = entity.standardwagerate.属性名小写，每 key 9 种语言，zh-CN = ColumnDescription。
/// </summary>
public class TaktStandardWageRateEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 50;

    /// <summary>
    /// 初始化标准工资率实体翻译种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l =>
                l.LanguageStatus == 0 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllStandardWageRateEntityTranslations();

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
                        OrderNum = translation.OrderNum,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
                else if (existing.TranslationValue != translation.TranslationValue
                         || existing.ResourceType != translation.ResourceType
                         || existing.ResourceGroup != translation.ResourceGroup)
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
    /// 获取所有 TaktStandardWageRate 实体名称及字段翻译（严格按 TaktStandardWageRate 实体属性顺序，ResourceKey = entity.standardwagerate.属性名小写）
    /// </summary>
    private static List<TaktTranslation> GetAllStandardWageRateEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // _self（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate._self", TranslationValue = "معدل الأجر القياسي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate._self", TranslationValue = "Standard Wage Rate", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate._self", TranslationValue = "Tasa salarial estándar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate._self", TranslationValue = "Taux de salaire standard", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate._self", TranslationValue = "標準賃率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate._self", TranslationValue = "표준 임금율", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate._self", TranslationValue = "Стандартная ставка зарплаты", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate._self", TranslationValue = "标准工资率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate._self", TranslationValue = "標準工資率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CompanyCode → entity.standardwagerate.companycode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "رمز الشركة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "Company Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "Código de empresa", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "Code société", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "会社コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "회사 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "Код компании", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.companycode", TranslationValue = "公司代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // YearMonth → entity.standardwagerate.yearmonth
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "السنة والشهر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "Year-Month", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "Año-mes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "Année-mois", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "연월", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "Год-месяц", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.yearmonth", TranslationValue = "年月", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // WorkingDays → entity.standardwagerate.workingdays
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "أيام العمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "Working Days", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "Días laborables", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "Jours ouvrés", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "稼働日数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "근무 일수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "Рабочие дни", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.workingdays", TranslationValue = "工作天數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // SalesAmount → entity.standardwagerate.salesamount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "قيمة المبيعات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "Sales Amount", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "Importe de ventas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "Montant des ventes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "売上高", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "매출액", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "Сумма продаж", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "销售额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.salesamount", TranslationValue = "銷售額", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // DirectLaborCount → entity.standardwagerate.directlaborcount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "عدد العمال المباشرين", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "Direct Labor Count", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "Número de personal directo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "Effectif direct", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "직접 인원수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "Число прямых работников", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directlaborcount", TranslationValue = "直接人數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // DirectLaborWage → entity.standardwagerate.directlaborwage
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "أجر العمال المباشرين", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "Direct Labor Wage", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "Salario directo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "Salaire direct", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "직접 임금", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "Прямая зарплата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工资", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directlaborwage", TranslationValue = "直接工資", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // DirectOvertimeHours → entity.standardwagerate.directovertimehours
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "ساعات العمل الإضافي المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "Direct Overtime Hours", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "Horas extras directas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "Heures supplémentaires directes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "직접 초과근무 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "Часы прямых сверхурочных", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directovertimehours", TranslationValue = "直接加班小時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // DirectOvertimeTotal → entity.standardwagerate.directovertimetotal
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "إجمالي أجر العمل الإضافي المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "Direct Overtime Total", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "Total horas extras directas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "Total heures sup. directes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "직접 초과근무 총액", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "Сумма прямых сверхурочных", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directovertimetotal", TranslationValue = "直接加班總額", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // DirectWageRate → entity.standardwagerate.directwagerate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "معدل الأجر المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "Direct Wage Rate", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "Tasa salarial directa", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "Taux de salaire direct", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "직접 임금율", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "Ставка прямой зарплаты", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.directwagerate", TranslationValue = "直接工資率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IndirectLaborCount → entity.standardwagerate.indirectlaborcount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "عدد العمال غير المباشرين", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "Indirect Labor Count", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "Número de personal indirecto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "Effectif indirect", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "간접 인원수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "Число косвенных работников", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "间接人数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectlaborcount", TranslationValue = "間接人數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IndirectLaborWage → entity.standardwagerate.indirectlaborwage
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "أجر العمال غير المباشرين", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "Indirect Labor Wage", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "Salario indirecto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "Salaire indirect", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "간접 임금", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "Косвенная зарплата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "间接工资", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectlaborwage", TranslationValue = "間接工資", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IndirectOvertimeHours → entity.standardwagerate.indirectovertimehours
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "ساعات العمل الإضافي غير المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "Indirect Overtime Hours", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "Horas extras indirectas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "Heures supplémentaires indirectes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "간접 초과근무 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "Часы косвенных сверхурочных", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "间接加班小时", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectovertimehours", TranslationValue = "間接加班小時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IndirectOvertimeTotal → entity.standardwagerate.indirectovertimetotal
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "إجمالي أجر العمل الإضافي غير المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "Indirect Overtime Total", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "Total horas extras indirectas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "Total heures sup. indirectes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "간접 초과근무 총액", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "Сумма косвенных сверхурочных", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "间接加班总额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectovertimetotal", TranslationValue = "間接加班總額", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IndirectWageRate → entity.standardwagerate.indirectwagerate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "معدل الأجر غير المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "Indirect Wage Rate", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "Tasa salarial indirecta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "Taux de salaire indirect", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "간접 임금율", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "Ставка косвенной зарплаты", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "间接工资率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.indirectwagerate", TranslationValue = "間接工資率", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // RelatedPlant → entity.standardwagerate.relatedplant
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "المصنع المرتبط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "Related Plant", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "Planta relacionada", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "Usine associée", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "関連工場", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "관련 공장", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "Связанный завод", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.standardwagerate.relatedplant", TranslationValue = "關聯工廠", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}

