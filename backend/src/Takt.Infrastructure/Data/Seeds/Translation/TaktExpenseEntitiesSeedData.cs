// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktExpenseEntitiesSeedData.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktExpense 实体翻译种子数据，entity.expense / entity.expense.xxx，与 TaktExpense.cs 一一对应，9 种语言
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
/// TaktExpense 实体翻译种子数据（与 TaktExpense.cs 属性一一对应）
/// </summary>
public class TaktExpenseEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 61;

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
            var allTranslations = GetExpenseEntityTranslations();

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

    private static List<TaktTranslation> GetExpenseEntityTranslations()
    {
        var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
        var list = new List<TaktTranslation>();

        // entity.expense._self
        var self = new Dictionary<string, string> { ["ar-SA"] = "المصروفات", ["en-US"] = "Expense", ["es-ES"] = "Gasto", ["fr-FR"] = "Dépense", ["ja-JP"] = "費用", ["ko-KR"] = "비용", ["ru-RU"] = "Расход", ["zh-CN"] = "费用", ["zh-TW"] = "費用" };
        foreach (var c in cultures)
            list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense._self", TranslationValue = self[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.expense.companycode / expensecode
        var companycode = new Dictionary<string, string> { ["ar-SA"] = "رمز الشركة", ["en-US"] = "Company Code", ["es-ES"] = "Código Empresa", ["fr-FR"] = "Code Société", ["ja-JP"] = "会社コード", ["ko-KR"] = "회사코드", ["ru-RU"] = "Код компании", ["zh-CN"] = "公司代码", ["zh-TW"] = "公司代碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.companycode", TranslationValue = companycode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var expensecode = new Dictionary<string, string> { ["ar-SA"] = "رمز المصروفات", ["en-US"] = "Expense Code", ["es-ES"] = "Código Gasto", ["fr-FR"] = "Code Dépense", ["ja-JP"] = "費用コード", ["ko-KR"] = "비용코드", ["ru-RU"] = "Код расхода", ["zh-CN"] = "费用单编码", ["zh-TW"] = "費用單編碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.expensecode", TranslationValue = expensecode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // costcenterid / costcentercode / costelementid / costelementcode
        var costcenterid = new Dictionary<string, string> { ["ar-SA"] = "معرف مركز التكلفة", ["en-US"] = "Cost Center ID", ["es-ES"] = "ID Centro Coste", ["fr-FR"] = "ID Centre Coût", ["ja-JP"] = "コストセンターID", ["ko-KR"] = "원가센터 ID", ["ru-RU"] = "ID центра затрат", ["zh-CN"] = "成本中心ID", ["zh-TW"] = "成本中心ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.costcenterid", TranslationValue = costcenterid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var costcentercode = new Dictionary<string, string> { ["ar-SA"] = "رمز مركز التكلفة", ["en-US"] = "Cost Center Code", ["es-ES"] = "Código Centro Coste", ["fr-FR"] = "Code Centre Coût", ["ja-JP"] = "コストセンターコード", ["ko-KR"] = "원가센터코드", ["ru-RU"] = "Код центра затрат", ["zh-CN"] = "成本中心编码", ["zh-TW"] = "成本中心編碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.costcentercode", TranslationValue = costcentercode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var costelementid = new Dictionary<string, string> { ["ar-SA"] = "معرف عنصر التكلفة", ["en-US"] = "Cost Element ID", ["es-ES"] = "ID Elemento Coste", ["fr-FR"] = "ID Élément Coût", ["ja-JP"] = "原価要素ID", ["ko-KR"] = "원가요소 ID", ["ru-RU"] = "ID элемента затрат", ["zh-CN"] = "成本要素ID", ["zh-TW"] = "成本要素ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.costelementid", TranslationValue = costelementid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var costelementcode = new Dictionary<string, string> { ["ar-SA"] = "رمز عنصر التكلفة", ["en-US"] = "Cost Element Code", ["es-ES"] = "Código Elemento Coste", ["fr-FR"] = "Code Élément Coût", ["ja-JP"] = "原価要素コード", ["ko-KR"] = "원가요소코드", ["ru-RU"] = "Код элемента затрат", ["zh-CN"] = "成本要素编码", ["zh-TW"] = "成本要素編碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.costelementcode", TranslationValue = costelementcode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // amount / currency / expensedate / fiscalyear / fiscalmonth / expensetype
        var amount = new Dictionary<string, string> { ["ar-SA"] = "المبلغ", ["en-US"] = "Amount", ["es-ES"] = "Importe", ["fr-FR"] = "Montant", ["ja-JP"] = "金額", ["ko-KR"] = "금액", ["ru-RU"] = "Сумма", ["zh-CN"] = "费用金额", ["zh-TW"] = "費用金額" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.amount", TranslationValue = amount[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var currency = new Dictionary<string, string> { ["ar-SA"] = "العملة", ["en-US"] = "Currency", ["es-ES"] = "Moneda", ["fr-FR"] = "Devise", ["ja-JP"] = "通貨", ["ko-KR"] = "통화", ["ru-RU"] = "Валюта", ["zh-CN"] = "币种", ["zh-TW"] = "幣種" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.currency", TranslationValue = currency[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var expensedate = new Dictionary<string, string> { ["ar-SA"] = "تاريخ المصروفات", ["en-US"] = "Expense Date", ["es-ES"] = "Fecha Gasto", ["fr-FR"] = "Date Dépense", ["ja-JP"] = "費用日", ["ko-KR"] = "비용일", ["ru-RU"] = "Дата расхода", ["zh-CN"] = "费用日期", ["zh-TW"] = "費用日期" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.expensedate", TranslationValue = expensedate[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var fiscalyear = new Dictionary<string, string> { ["ar-SA"] = "السنة المالية", ["en-US"] = "Fiscal Year", ["es-ES"] = "Año fiscal", ["fr-FR"] = "Année fiscale", ["ja-JP"] = "会計年度", ["ko-KR"] = "회계연도", ["ru-RU"] = "Финансовый год", ["zh-CN"] = "会计年度", ["zh-TW"] = "會計年度" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.fiscalyear", TranslationValue = fiscalyear[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var fiscalmonth = new Dictionary<string, string> { ["ar-SA"] = "الشهر المالي", ["en-US"] = "Fiscal Month", ["es-ES"] = "Mes fiscal", ["fr-FR"] = "Mois fiscal", ["ja-JP"] = "会計月", ["ko-KR"] = "회계월", ["ru-RU"] = "Финансовый месяц", ["zh-CN"] = "会计月份", ["zh-TW"] = "會計月份" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.fiscalmonth", TranslationValue = fiscalmonth[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var expensetype = new Dictionary<string, string> { ["ar-SA"] = "نوع المصروفات", ["en-US"] = "Expense Type", ["es-ES"] = "Tipo Gasto", ["fr-FR"] = "Type Dépense", ["ja-JP"] = "費用タイプ", ["ko-KR"] = "비용유형", ["ru-RU"] = "Тип расхода", ["zh-CN"] = "费用类型", ["zh-TW"] = "費用類型" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.expensetype", TranslationValue = expensetype[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // instanceid / expensestatus / applicantid / applicantname
        var instanceid = new Dictionary<string, string> { ["ar-SA"] = "معرف المثيل", ["en-US"] = "Instance ID", ["es-ES"] = "ID Instancia", ["fr-FR"] = "ID Instance", ["ja-JP"] = "インスタンスID", ["ko-KR"] = "인스턴스 ID", ["ru-RU"] = "ID экземпляра", ["zh-CN"] = "工作流实例ID", ["zh-TW"] = "工作流實例ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.instanceid", TranslationValue = instanceid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var expensestatus = new Dictionary<string, string> { ["ar-SA"] = "حالة المصروفات", ["en-US"] = "Expense Status", ["es-ES"] = "Estado Gasto", ["fr-FR"] = "Statut Dépense", ["ja-JP"] = "費用状態", ["ko-KR"] = "비용상태", ["ru-RU"] = "Статус расхода", ["zh-CN"] = "费用状态", ["zh-TW"] = "費用狀態" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.expensestatus", TranslationValue = expensestatus[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var applicantid = new Dictionary<string, string> { ["ar-SA"] = "معرف مقدم الطلب", ["en-US"] = "Applicant ID", ["es-ES"] = "ID Solicitante", ["fr-FR"] = "ID Demandeur", ["ja-JP"] = "申請者ID", ["ko-KR"] = "신청자 ID", ["ru-RU"] = "ID заявителя", ["zh-CN"] = "申请人ID", ["zh-TW"] = "申請人ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.applicantid", TranslationValue = applicantid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var applicantname = new Dictionary<string, string> { ["ar-SA"] = "اسم مقدم الطلب", ["en-US"] = "Applicant Name", ["es-ES"] = "Nombre Solicitante", ["fr-FR"] = "Nom Demandeur", ["ja-JP"] = "申請者名", ["ko-KR"] = "신청자명", ["ru-RU"] = "Имя заявителя", ["zh-CN"] = "申请人姓名", ["zh-TW"] = "申請人姓名" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.applicantname", TranslationValue = applicantname[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // plantid / plantcode
        var plantid = new Dictionary<string, string> { ["ar-SA"] = "معرف المصنع", ["en-US"] = "Plant ID", ["es-ES"] = "ID Planta", ["fr-FR"] = "ID Usine", ["ja-JP"] = "工場ID", ["ko-KR"] = "공장 ID", ["ru-RU"] = "ID завода", ["zh-CN"] = "工厂ID", ["zh-TW"] = "工廠ID" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.plantid", TranslationValue = plantid[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });
        var plantcode = new Dictionary<string, string> { ["ar-SA"] = "رمز المصنع", ["en-US"] = "Plant Code", ["es-ES"] = "Código Planta", ["fr-FR"] = "Code Usine", ["ja-JP"] = "工場コード", ["ko-KR"] = "공장코드", ["ru-RU"] = "Код завода", ["zh-CN"] = "工厂代码", ["zh-TW"] = "工廠代碼" };
        foreach (var c in cultures) list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.expense.plantcode", TranslationValue = plantcode[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        return list;
    }
}
