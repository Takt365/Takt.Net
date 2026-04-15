// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktEmployeeSkillEntitiesSeedData.cs
// 创建时间：2026-04-14
// 功能描述：TaktEmployeeSkill 实体字段翻译种子数据，entity.employeeskill / entity.employeeskill.xxx，zh-CN 与 ColumnDescription 对齐
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// TaktEmployeeSkill 实体翻译种子数据。严格按 TaktEmployeeSkill.cs 属性顺序，ResourceKey = entity.employeeskill.属性名小写，zh-CN = ColumnDescription。
/// </summary>
public class TaktEmployeeSkillEntitiesSeedData : ITaktSeedData
{
    public int Order => 50;

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
            foreach (var translation in GetAllEmployeeSkillEntityTranslations())
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId)) continue;
                var existing = await translationRepository.GetAsync(t => t.ResourceKey == translation.ResourceKey && t.CultureCode == translation.CultureCode && t.IsDeleted == 0);
                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation { LanguageId = languageId, CultureCode = translation.CultureCode, ResourceKey = translation.ResourceKey, TranslationValue = translation.TranslationValue, ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0, IsDeleted = 0 });
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

    private static List<TaktTranslation> GetAllEmployeeSkillEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // _self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill._self", TranslationValue = "مهارة الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill._self", TranslationValue = "Employee Skill", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill._self", TranslationValue = "Habilidad del empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill._self", TranslationValue = "Compétence de l'employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill._self", TranslationValue = "従業員技能", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill._self", TranslationValue = "직원 기술", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill._self", TranslationValue = "Навык сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill._self", TranslationValue = "员工业务技能", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill._self", TranslationValue = "員工業務技能", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // employeeid → 员工ID
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "معرف الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "Employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "ID del empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "ID employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "従業員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "직원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "ID сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill.employeeid", TranslationValue = "員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // skillname → 技能名称
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "اسم المهارة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "Skill Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "Nombre de habilidad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "Nom de compétence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "スキル名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "기술명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "Название навыка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "技能名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill.skillname", TranslationValue = "技能名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // skilllevel → 技能等级
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "مستوى المهارة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "Skill Level", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "Nivel de habilidad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "Niveau de compétence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "スキルレベル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "기술 수준", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "Уровень навыка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "技能等级", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill.skilllevel", TranslationValue = "技能等級", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // certificatename → 证书名称
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "اسم الشهادة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "Certificate Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "Nombre del certificado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "Nom du certificat", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "資格名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "자격증명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "Название сертификата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "证书名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill.certificatename", TranslationValue = "證書名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // certificateno → 证书编号
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "رقم الشهادة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "Certificate No", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "N.º de certificado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "Numéro de certificat", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "証書番号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "증서 번호", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "Номер сертификата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "证书编号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill.certificateno", TranslationValue = "證書編號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // obtaineddate → 获得日期
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "تاريخ الحصول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "Obtained Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "Fecha de obtención", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "Date d'obtention", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "取得日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "취득일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "Дата получения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "获得日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill.obtaineddate", TranslationValue = "獲得日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // expirydate → 到期日期
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "تاريخ الانتهاء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "Expiry Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "Fecha de vencimiento", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "Date d'expiration", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "有効期限", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "만료일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "Срок действия", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "到期日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeeskill.expirydate", TranslationValue = "到期日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
