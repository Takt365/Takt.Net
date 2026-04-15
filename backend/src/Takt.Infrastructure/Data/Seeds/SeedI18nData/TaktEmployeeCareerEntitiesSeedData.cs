// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktEmployeeCareerEntitiesSeedData.cs
// 创建时间：2025-03-16
// 功能描述：TaktEmployeeCareer 实体字段翻译种子数据，entity.employeecareer / entity.employeecareer.xxx，zh-CN 与 ColumnDescription 对齐
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// TaktEmployeeCareer 实体翻译种子数据。严格按 TaktEmployeeCareer.cs 属性顺序，ResourceKey = entity.employeecareer.属性名小写，zh-CN = ColumnDescription。
/// </summary>
public class TaktEmployeeCareerEntitiesSeedData : ITaktSeedData
{
    public int Order => 45;

    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();
        int insertCount = 0, updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;
        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0) return (0, 0);
            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            foreach (var translation in GetAllEmployeeCareerEntityTranslations())
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId)) continue;
                var existing = await translationRepository.GetAsync(t => t.ResourceKey == translation.ResourceKey && t.CultureCode == translation.CultureCode && t.IsDeleted == 0);
                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation { LanguageId = languageId, CultureCode = translation.CultureCode, ResourceKey = translation.ResourceKey, TranslationValue = translation.TranslationValue, ResourceType = translation.ResourceType, ResourceGroup = translation.ResourceGroup, OrderNum = translation.OrderNum, IsDeleted = 0 });
                    insertCount++;
                }
                else if (existing.TranslationValue != translation.TranslationValue || existing.ResourceType != translation.ResourceType || existing.ResourceGroup != translation.ResourceGroup)
                {
                    existing.LanguageId = languageId; existing.TranslationValue = translation.TranslationValue; existing.ResourceType = translation.ResourceType; existing.ResourceGroup = translation.ResourceGroup;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
                else if (existing.LanguageId != languageId) { existing.LanguageId = languageId; await translationRepository.UpdateAsync(existing); updateCount++; }
            }
        }
        finally { TaktTenantContext.CurrentConfigId = originalConfigId; }
        return (insertCount, updateCount);
    }

    private static List<TaktTranslation> GetAllEmployeeCareerEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // _self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer._self", TranslationValue = "المهنة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer._self", TranslationValue = "Employee Career", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer._self", TranslationValue = "Carrera del empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer._self", TranslationValue = "Carrière employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer._self", TranslationValue = "従業員キャリア", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer._self", TranslationValue = "직원 경력", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer._self", TranslationValue = "Карьера сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer._self", TranslationValue = "员工职业信息", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer._self", TranslationValue = "員工職業資訊", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // employeeid → 员工ID
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "معرف الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "Employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "ID del empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "ID employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "従業員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "직원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "ID сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.employeeid", TranslationValue = "員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // deptid → 部门ID
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "معرف القسم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "Dept ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "ID del departamento", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "ID du département", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "부서 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "ID отдела", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.deptid", TranslationValue = "部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // deptname → 部门名称
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "اسم القسم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "Dept Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "Nombre del departamento", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "Nom du département", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "部門名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "부서명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "Название отдела", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.deptname", TranslationValue = "部門名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // postid → 岗位ID
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.postid", TranslationValue = "معرف المنصب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.postid", TranslationValue = "Post ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.postid", TranslationValue = "ID del puesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.postid", TranslationValue = "ID du poste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.postid", TranslationValue = "職位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.postid", TranslationValue = "직위 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.postid", TranslationValue = "ID должности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.postid", TranslationValue = "岗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.postid", TranslationValue = "崗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // postname → 岗位名称
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.postname", TranslationValue = "اسم المنصب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.postname", TranslationValue = "Post Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.postname", TranslationValue = "Nombre del puesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.postname", TranslationValue = "Nom du poste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.postname", TranslationValue = "職位名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.postname", TranslationValue = "직위명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.postname", TranslationValue = "Название должности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.postname", TranslationValue = "岗位名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.postname", TranslationValue = "崗位名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // joblevel → 职级
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "المستوى الوظيفي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "Job Level", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "Nivel del puesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "Niveau du poste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "職級", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "직급", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "Уровень должности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "职级", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.joblevel", TranslationValue = "職級", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // jobtitle → 职位
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "المسمى الوظيفي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "Job Title", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "Título del puesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "Intitulé du poste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "職位", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "직함", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "Должность", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "职位", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.jobtitle", TranslationValue = "職位", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // joindate → 入职日期
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "تاريخ الانضمام", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "Join Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "Fecha de incorporación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "Date d'entrée", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "入社日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "입사일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "Дата приёма", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "入职日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.joindate", TranslationValue = "入職日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // regularizationdate → 转正日期
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "تاريخ التأهيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "Regularization Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "Fecha de regularización", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "Date de titularisation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "正社員転換日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "정규직 전환일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "Дата перевода в штат", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "转正日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.regularizationdate", TranslationValue = "轉正日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // leavedate → 离职日期
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "تاريخ المغادرة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "Leave Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "Fecha de baja", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "Date de départ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "退職日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "퇴사일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "Дата увольнения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "离职日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.leavedate", TranslationValue = "離職日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // workyears → 工作年限
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "سنوات الخبرة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "Work Years", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "Años de experiencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "Années d'ancienneté", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "勤続年数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "근속 연수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "Стаж работы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "工作年限", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.workyears", TranslationValue = "工作年限", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // worklocation → 工作地点
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "مكان العمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "Work Location", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "Lugar de trabajo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "Lieu de travail", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "勤務地", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "근무지", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "Место работы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "工作地点", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.worklocation", TranslationValue = "工作地點", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // worknature → 工作性质
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "طبيعة العمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "Work Nature", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "Naturaleza del trabajo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "Nature du travail", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "勤務形態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "근무 성격", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "Характер работы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "工作性质", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.worknature", TranslationValue = "工作性質", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // employmenttype → 用工形式
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "نوع التوظيف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "Employment Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "Tipo de empleo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "Type d'emploi", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "雇用形態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "고용 형태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "Тип занятости", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "用工形式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.employmenttype", TranslationValue = "用工形式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // isprimary → 是否主职
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "الوظيفة الرئيسية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "Is Primary", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "Es principal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "Principal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "主職フラグ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "주직 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "Основная должность", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "是否主职", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.isprimary", TranslationValue = "是否主職", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // directmanagerid → 直接上级员工ID
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "معرف المدير المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "Direct Manager ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "ID del supervisor directo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "ID du responsable direct", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "直属上長ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "직속 상사 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "ID непосредственного руководителя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "直接上级员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.directmanagerid", TranslationValue = "直接上級員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // directmanagername → 直接上级姓名
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "اسم المدير المباشر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "Direct Manager Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "Nombre del supervisor directo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "Nom du responsable direct", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "直属上長名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "직속 상사 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "Имя непосредственного руководителя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "直接上级姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeecareer.directmanagername", TranslationValue = "直接上級姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
