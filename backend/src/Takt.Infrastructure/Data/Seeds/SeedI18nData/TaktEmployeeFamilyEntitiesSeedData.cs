// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktEmployeeFamilyEntitiesSeedData.cs
// 创建时间：2026-04-14
// 功能描述：TaktEmployeeFamily 实体字段翻译种子数据，entity.employeefamily / entity.employeefamily.xxx，zh-CN 与 ColumnDescription 对齐
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// TaktEmployeeFamily 实体翻译种子数据。严格按 TaktEmployeeFamily.cs 属性顺序，ResourceKey = entity.employeefamily.属性名小写，zh-CN = ColumnDescription。
/// </summary>
public class TaktEmployeeFamilyEntitiesSeedData : ITaktSeedData
{
    public int Order => 48;

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
            foreach (var translation in GetAllEmployeeFamilyEntityTranslations())
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

    private static List<TaktTranslation> GetAllEmployeeFamilyEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // _self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily._self", TranslationValue = "أسرة الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily._self", TranslationValue = "Employee Family", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily._self", TranslationValue = "Familia del empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily._self", TranslationValue = "Famille de l'employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily._self", TranslationValue = "従業員家族", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily._self", TranslationValue = "직원 가족", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily._self", TranslationValue = "Семья сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily._self", TranslationValue = "员工家庭成员", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily._self", TranslationValue = "員工家庭成員", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // employeeid → 员工ID
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "معرف الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "Employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "ID del empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "ID employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "従業員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "직원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "ID сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.employeeid", TranslationValue = "員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // membername → 成员姓名
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.membername", TranslationValue = "اسم العضو", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.membername", TranslationValue = "Member Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.membername", TranslationValue = "Nombre del familiar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.membername", TranslationValue = "Nom du membre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.membername", TranslationValue = "家族姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.membername", TranslationValue = "가족 구성원 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.membername", TranslationValue = "Имя члена семьи", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.membername", TranslationValue = "成员姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.membername", TranslationValue = "成員姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // relationtype → 关系类型
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "نوع العلاقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "Relation Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "Tipo de relación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "Type de relation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "続柄", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "관계 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "Тип родства", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "关系类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.relationtype", TranslationValue = "關係類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // phonenumber → 联系电话
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "رقم الهاتف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "Phone Number", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "Teléfono", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "Téléphone", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "連絡先電話", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "연락처", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "Телефон", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "联系电话", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.phonenumber", TranslationValue = "聯絡電話", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // workunit → 工作单位
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "جهة العمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "Work Unit", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "Unidad de trabajo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "Unité de travail", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "勤務先", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "근무 기관", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "Место работы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "工作单位", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.workunit", TranslationValue = "工作單位", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // jobtitle → 职务
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "الوظيفة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "Job Title", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "Cargo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "Fonction", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "職務", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "직무", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "Должность", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "职务", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.jobtitle", TranslationValue = "職務", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // birthdate → 出生日期
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "تاريخ الميلاد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "Birth Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "Fecha de nacimiento", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "Date de naissance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "生年月日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "생년월일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "Дата рождения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "出生日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.birthdate", TranslationValue = "出生日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // isemergencycontact → 是否紧急联系人
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "جهة اتصال طارئة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "Is Emergency Contact", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "Es contacto de emergencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "Contact d'urgence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "緊急連絡先", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "긴급 연락처 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "Контакт для экстренной связи", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "是否紧急联系人", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeefamily.isemergencycontact", TranslationValue = "是否緊急聯絡人", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
