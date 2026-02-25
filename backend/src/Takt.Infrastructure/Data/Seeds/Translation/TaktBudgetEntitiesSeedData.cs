// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktBudgetEntitiesSeedData.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktBudget 实体翻译种子数据，entity.budget / entity.budget.xxx，与 TaktBudget.cs 一一对应，9 种语言
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// TaktBudget 实体翻译种子数据（与 TaktBudget.cs 属性一一对应）
/// </summary>
public class TaktBudgetEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 60;

    /// <summary>
    /// 初始化种子数据
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
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetBudgetEntityTranslations();

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
                else
                {
                    if (existing.TranslationValue != translation.TranslationValue)
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
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    private static List<TaktTranslation> GetBudgetEntityTranslations()
    {
        var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
        var list = new List<TaktTranslation>();

        // entity.budget._self
        var self = new Dictionary<string, string> { ["ar-SA"] = "الميزانية", ["en-US"] = "Budget", ["es-ES"] = "Presupuesto", ["fr-FR"] = "Budget", ["ja-JP"] = "予算", ["ko-KR"] = "예산", ["ru-RU"] = "Бюджет", ["zh-CN"] = "预算", ["zh-TW"] = "預算" };
        foreach (var c in cultures)
            list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget._self", TranslationValue = self[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.budget.companycode
        var companycode = new Dictionary<string, string> { ["ar-SA"] = "رمز الشركة", ["en-US"] = "Company Code", ["es-ES"] = "Código Empresa", ["fr-FR"] = "Code Société", ["ja-JP"] = "会社コード", ["ko-KR"] = "회사코드", ["ru-RU"] = "Код компании", ["zh-CN"] = "公司代码", ["zh-TW"] = "公司代碼" };
        foreach (var c in cultures)
            list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.companycode", TranslationValue = companycode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.budget.fiscalyear
        var fiscalyear = new Dictionary<string, string> { ["ar-SA"] = "السنة المالية", ["en-US"] = "Fiscal Year", ["es-ES"] = "Año fiscal", ["fr-FR"] = "Année fiscale", ["ja-JP"] = "会計年度", ["ko-KR"] = "회계연도", ["ru-RU"] = "Финансовый год", ["zh-CN"] = "会计年度", ["zh-TW"] = "會計年度" };
        foreach (var c in cultures)
            list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.fiscalyear", TranslationValue = fiscalyear[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.budget.costcenterid / costcentercode / costelementid / costelementcode
        var costcenterid = new Dictionary<string, string> { ["ar-SA"] = "معرف مركز التكلفة", ["en-US"] = "Cost Center ID", ["es-ES"] = "ID Centro Coste", ["fr-FR"] = "ID Centre Coût", ["ja-JP"] = "コストセンターID", ["ko-KR"] = "원가센터 ID", ["ru-RU"] = "ID центра затрат", ["zh-CN"] = "成本中心ID", ["zh-TW"] = "成本中心ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.costcenterid", TranslationValue = costcenterid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var costcentercode = new Dictionary<string, string> { ["ar-SA"] = "رمز مركز التكلفة", ["en-US"] = "Cost Center Code", ["es-ES"] = "Código Centro Coste", ["fr-FR"] = "Code Centre Coût", ["ja-JP"] = "コストセンターコード", ["ko-KR"] = "원가센터코드", ["ru-RU"] = "Код центра затрат", ["zh-CN"] = "成本中心编码", ["zh-TW"] = "成本中心編碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.costcentercode", TranslationValue = costcentercode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var costelementid = new Dictionary<string, string> { ["ar-SA"] = "معرف عنصر التكلفة", ["en-US"] = "Cost Element ID", ["es-ES"] = "ID Elemento Coste", ["fr-FR"] = "ID Élément Coût", ["ja-JP"] = "原価要素ID", ["ko-KR"] = "원가요소 ID", ["ru-RU"] = "ID элемента затрат", ["zh-CN"] = "成本要素ID", ["zh-TW"] = "成本要素ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.costelementid", TranslationValue = costelementid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var costelementcode = new Dictionary<string, string> { ["ar-SA"] = "رمز عنصر التكلفة", ["en-US"] = "Cost Element Code", ["es-ES"] = "Código Elemento Coste", ["fr-FR"] = "Code Élément Coût", ["ja-JP"] = "原価要素コード", ["ko-KR"] = "원가요소코드", ["ru-RU"] = "Код элемента затрат", ["zh-CN"] = "成本要素编码", ["zh-TW"] = "成本要素編碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.costelementcode", TranslationValue = costelementcode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.budget.budgetamount / usedamount
        var budgetamount = new Dictionary<string, string> { ["ar-SA"] = "مبلغ الميزانية", ["en-US"] = "Budget Amount", ["es-ES"] = "Importe Presupuesto", ["fr-FR"] = "Montant Budget", ["ja-JP"] = "予算金額", ["ko-KR"] = "예산금액", ["ru-RU"] = "Сумма бюджета", ["zh-CN"] = "预算金额", ["zh-TW"] = "預算金額" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.budgetamount", TranslationValue = budgetamount[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var usedamount = new Dictionary<string, string> { ["ar-SA"] = "المبلغ المستخدم", ["en-US"] = "Used Amount", ["es-ES"] = "Importe Utilizado", ["fr-FR"] = "Montant Utilisé", ["ja-JP"] = "使用金額", ["ko-KR"] = "사용금액", ["ru-RU"] = "Использованная сумма", ["zh-CN"] = "已用金额", ["zh-TW"] = "已用金額" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.usedamount", TranslationValue = usedamount[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.budget.instanceid / budgetstatus
        var instanceid = new Dictionary<string, string> { ["ar-SA"] = "معرف المثيل", ["en-US"] = "Instance ID", ["es-ES"] = "ID Instancia", ["fr-FR"] = "ID Instance", ["ja-JP"] = "インスタンスID", ["ko-KR"] = "인스턴스 ID", ["ru-RU"] = "ID экземпляра", ["zh-CN"] = "工作流实例ID", ["zh-TW"] = "工作流實例ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.instanceid", TranslationValue = instanceid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var budgetstatus = new Dictionary<string, string> { ["ar-SA"] = "حالة الميزانية", ["en-US"] = "Budget Status", ["es-ES"] = "Estado Presupuesto", ["fr-FR"] = "Statut Budget", ["ja-JP"] = "予算状態", ["ko-KR"] = "예산상태", ["ru-RU"] = "Статус бюджета", ["zh-CN"] = "预算状态", ["zh-TW"] = "預算狀態" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.budgetstatus", TranslationValue = budgetstatus[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.budget.plantid / plantcode
        var plantid = new Dictionary<string, string> { ["ar-SA"] = "معرف المصنع", ["en-US"] = "Plant ID", ["es-ES"] = "ID Planta", ["fr-FR"] = "ID Usine", ["ja-JP"] = "工場ID", ["ko-KR"] = "공장 ID", ["ru-RU"] = "ID завода", ["zh-CN"] = "工厂ID", ["zh-TW"] = "工廠ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.plantid", TranslationValue = plantid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var plantcode = new Dictionary<string, string> { ["ar-SA"] = "رمز المصنع", ["en-US"] = "Plant Code", ["es-ES"] = "Código Planta", ["fr-FR"] = "Code Usine", ["ja-JP"] = "工場コード", ["ko-KR"] = "공장코드", ["ru-RU"] = "Код завода", ["zh-CN"] = "工厂代码", ["zh-TW"] = "工廠代碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.budget.plantcode", TranslationValue = plantcode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        return list;
    }
}
