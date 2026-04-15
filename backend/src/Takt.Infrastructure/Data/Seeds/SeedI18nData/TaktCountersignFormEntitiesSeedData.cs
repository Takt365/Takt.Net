// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktCountersignFormEntitiesSeedData.cs
// 创建时间：2025-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktCountersignForm 实体字段翻译种子数据，entity.countersignform / entity.countersignform.xxx，zh-CN 与 ColumnDescription 对齐
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
/// TaktCountersignForm 实体翻译种子数据。严格按 TaktCountersignForm.cs 实体属性顺序，ResourceKey = entity.countersignform.属性名小写，每 key 9 种语言，zh-CN = ColumnDescription。
/// </summary>
public class TaktCountersignFormEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 54;

    /// <summary>
    /// 初始化会签单实体翻译种子数据
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
            var allTranslations = GetAllCountersignFormEntityTranslations();

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
    /// 获取所有 TaktCountersignForm 实体名称及字段翻译（严格按 TaktCountersignForm 实体属性顺序，ResourceKey = entity.countersignform.属性名小写）
    /// </summary>
    private static List<TaktTranslation> GetAllCountersignFormEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // _self（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform._self", TranslationValue = "نموذج الموافقة المالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform._self", TranslationValue = "Countersign Form", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform._self", TranslationValue = "Formulario de co-firma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform._self", TranslationValue = "Formulaire de co‑signature", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform._self", TranslationValue = "합의서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform._self", TranslationValue = "Лист согласования", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform._self", TranslationValue = "会签单", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform._self", TranslationValue = "會簽單", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CompanyCode → entity.countersignform.companycode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.companycode", TranslationValue = "رمز الشركة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.companycode", TranslationValue = "Company Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.companycode", TranslationValue = "Código de empresa", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.companycode", TranslationValue = "Code société", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.companycode", TranslationValue = "회사 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.companycode", TranslationValue = "Код компании", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.companycode", TranslationValue = "公司代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CountersignCode → entity.countersignform.countersigncode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "رقم نموذج الموافقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "Countersign Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "Número de co-firma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "N° de co‑signature", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "会签编号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "합의 번호", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "Номер листа согласования", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "会签编号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.countersigncode", TranslationValue = "會簽編號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CountersignDepts → entity.countersignform.countersigndepts
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "أقسام الموافقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "Countersign Departments", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "Departamentos que co-firman", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "Départements co‑signataires", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "합의 부서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "Согласующие подразделения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "会签部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.countersigndepts", TranslationValue = "會簽部門", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // FinanceDept → entity.countersignform.financedept
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.financedept", TranslationValue = "قسم المالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.financedept", TranslationValue = "Finance Department", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.financedept", TranslationValue = "Departamento de finanzas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.financedept", TranslationValue = "Service financier", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.financedept", TranslationValue = "재무 부서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.financedept", TranslationValue = "Финансовый отдел", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.financedept", TranslationValue = "财务部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.financedept", TranslationValue = "財務部門", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // BudgetReviewComment → entity.countersignform.budgetreviewcomment
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "ملاحظات مراجعة الميزانية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "Budget Review Comment", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "Comentario de revisión del presupuesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "Avis de révision budgétaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "예산 심사 의견", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "Комментарий по проверке бюджета", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "预算审核意见", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.budgetreviewcomment", TranslationValue = "預算審核意見", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ExecutiveOffice → entity.countersignform.executiveoffice
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "مكتب الإدارة العليا", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "Executive Office", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "Oficina ejecutiva", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "Bureau exécutif", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "총경실", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "Исполнительный офис", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "总经室", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.executiveoffice", TranslationValue = "總經室", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ApprovalDate → entity.countersignform.approvaldate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "تاريخ الموافقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "Approval Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "Fecha de aprobación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "Date d'approbation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "承认日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "승인 일자", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "Дата утверждения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "承认日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.approvaldate", TranslationValue = "承認日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ApplicationDate → entity.countersignform.applicationdate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "تاريخ الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "Application Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "Fecha de solicitud", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "Date de la demande", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "申请日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "신청 일자", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "Дата заявки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "申请日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.applicationdate", TranslationValue = "申請日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // EmployeeId → entity.countersignform.employeeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "معرف الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "Employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "ID del empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "ID de l'employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "申请人员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "신청자 직원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "ID сотрудника-заявителя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "申请人员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.employeeid", TranslationValue = "申請人員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ApplicantName → entity.countersignform.applicantname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "اسم مقدم الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "Applicant Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "Nombre del solicitante", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "Nom du demandeur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "申请者名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "신청자명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "ФИО заявителя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "申请者名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.applicantname", TranslationValue = "申請者名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ApplicationDept → entity.countersignform.applicationdept
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "قسم مقدم الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "Application Dept.", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "Departamento solicitante", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "Département demandeur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "신청 부서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "Подразделение-заявитель", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "申请部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.applicationdept", TranslationValue = "申請部門", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CostBearerDept → entity.countersignform.costbearerdept
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "قسم تحمل التكاليف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "Cost Bearer Dept.", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "Departamento que soporta el costo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "Département support de coût", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "비용 부담 부서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "Подразделение-носитель затрат", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "经费负担部门", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.costbearerdept", TranslationValue = "經費負擔部門", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IsBudget → entity.countersignform.isbudget
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "هل يوجد ميزانية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "Has Budget", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "Tiene presupuesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "Avec budget ou non", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "是否有预算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "예산 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "Есть ли бюджет", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "是否有预算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.isbudget", TranslationValue = "是否有預算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // BudgetItem → entity.countersignform.budgetitem
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "بند الميزانية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "Budget Item", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "Partida presupuestaria", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "Poste budgétaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "예산 항목", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "Статья бюджета", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "预算项目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.budgetitem", TranslationValue = "預算項目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // BudgetAmount → entity.countersignform.budgetamount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "مبلغ الميزانية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "Budget Amount", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "Importe presupuestado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "Montant budgété", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "预算金额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "예산 금액", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "Сумма бюджета", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "预算金额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.budgetamount", TranslationValue = "預算金額", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ApplicationAmount → entity.countersignform.applicationamount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "مبلغ الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "Application Amount", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "Importe solicitado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "Montant demandé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "申请金额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "신청 금액", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "Запрашиваемая сумма", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "申请金额", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.applicationamount", TranslationValue = "申請金額", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CountersignTitle → entity.countersignform.countersigntitle
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "عنوان الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "Countersign Title", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "Título de la co-firma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "Titre de la co‑signature", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "제목", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "Заголовок листа согласования", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "标题", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.countersigntitle", TranslationValue = "標題", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ApplicationReason → entity.countersignform.applicationreason
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "سبب الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "Application Reason", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "Motivo de la solicitud", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "Motif de la demande", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "신청 사유", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "Причина заявки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "申请原因", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.applicationreason", TranslationValue = "申請原因", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // BudgetUsageDescription → entity.countersignform.budgetusagedescription
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "وصف استخدام الميزانية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "Budget Usage Description", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "Descripción del uso del presupuesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "Description de l'utilisation du budget", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "예산 사용 설명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "Описание использования бюджета", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "预算使用说明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.budgetusagedescription", TranslationValue = "預算使用說明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // TargetAndExpectedBenefit → entity.countersignform.targetandexpectedbenefit
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "الأهداف والفوائد المتوقعة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "Target & Expected Benefit", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "Objetivo y beneficio esperado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "Objectif et bénéfice attendu", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "목표 및 기대 효과", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "Цели и ожидаемый эффект", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "目标与预期效益", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.targetandexpectedbenefit", TranslationValue = "目標與預期效益", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // Attachments → entity.countersignform.attachments
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.attachments", TranslationValue = "المرفقات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.attachments", TranslationValue = "Attachments", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.attachments", TranslationValue = "Adjuntos", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.attachments", TranslationValue = "Pièces jointes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.attachments", TranslationValue = "첨부 파일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.attachments", TranslationValue = "Вложения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.attachments", TranslationValue = "附件", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // FlowInstanceId → entity.countersignform.flowinstanceid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "معرف عملية الموافقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "Flow Instance ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "ID de instancia de flujo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "ID d'instance de flux", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "流程实例ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "프로세스 인스턴스 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "ID экземпляра процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "流程实例ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.flowinstanceid", TranslationValue = "流程實例ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CountersignStatus → entity.countersignform.countersignstatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "حالة نموذج الموافقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "Countersign Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "Estado de la co-firma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "Statut de la co‑signature", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "会签单状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "합의서 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "Статус листа согласования", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "会签单状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.countersignform.countersignstatus", TranslationValue = "會簽單狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}

