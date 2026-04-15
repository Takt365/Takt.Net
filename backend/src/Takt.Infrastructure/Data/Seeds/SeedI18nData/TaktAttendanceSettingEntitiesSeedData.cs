// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktAttendanceSettingEntitiesSeedData.cs
// 功能描述：TaktAttendanceSetting 实体字段翻译种子数据，与 TaktRoleDeptEntitiesSeedData 风格一致，与 TaktAttendanceSetting.cs 一一对应。
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
/// TaktAttendanceSetting 实体翻译种子数据（考勤规则/标准上下班）。
/// </summary>
public class TaktAttendanceSettingEntitiesSeedData : ITaktSeedData
{
    public int Order => 206;

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
            var allTranslations = GetAllAttendanceSettingEntityTranslations();

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

    private static List<TaktTranslation> GetAllAttendanceSettingEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.attendancesetting._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting._self", TranslationValue = "إعداد الحضور", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting._self", TranslationValue = "Attendance setting", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting._self", TranslationValue = "Configuración de asistencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting._self", TranslationValue = "Paramètre de présence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting._self", TranslationValue = "勤怠設定", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting._self", TranslationValue = "근태 설정", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting._self", TranslationValue = "Настройка учёта времени", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting._self", TranslationValue = "考勤设置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting._self", TranslationValue = "考勤設定", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.keyword
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "رمز المخطط، الاسم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "Setting code, name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "Código y nombre del esquema", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "Code et nom du schéma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "方案コード、名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "방안 코드, 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "Код и название схемы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "方案编码、名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.keyword", TranslationValue = "方案編碼、名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.settingcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "رمز المخطط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "Setting code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "Código del esquema", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "Code du schéma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "方案コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "방안 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "Код схемы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.settingcode", TranslationValue = "方案編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.settingname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "اسم المخطط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "Setting name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "Nombre del esquema", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "Nom du schéma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "方案名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "방안 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "Название схемы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "方案名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.settingname", TranslationValue = "方案名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.workstarttime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "وقت بدء العمل القياسي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "Standard work start", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "Inicio laboral estándar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "Début de travail standard", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "標準出勤時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "표준 출근 시각", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "Стандартное начало работы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "标准上班时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.workstarttime", TranslationValue = "標準上班時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.workendtime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "وقت انتهاء العمل القياسي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "Standard work end", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "Fin laboral estándar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "Fin de travail standard", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "標準退勤時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "표준 퇴근 시각", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "Стандартное окончание работы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "标准下班时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.workendtime", TranslationValue = "標準下班時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.lategraceminutes
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "دقائق السماح بالتأخير", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "Late grace (minutes)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "Tolerancia de retraso (min)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "Tolérance retard (min)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "遅刻猶予（分）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "지각 유예(분)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "Допуск опоздания (мин)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "迟到宽限（分钟）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.lategraceminutes", TranslationValue = "遲到寬限（分鐘）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.earlyleavegraceminutes
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "دقائق السماح بالمغادرة المبكرة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "Early leave grace (minutes)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "Tolerancia salida anticipada (min)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "Tolérance départ anticipé (min)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "早退猶予（分）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "조퇴 유예(분)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "Допуск раннего ухода (мин)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "早退宽限（分钟）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.earlyleavegraceminutes", TranslationValue = "早退寬限（分鐘）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.isdefault
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "افتراضي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "Default", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "Predeterminado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "Par défaut", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "既定", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "기본", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "По умолчанию", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "是否默认", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.isdefault", TranslationValue = "是否預設", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancesetting.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancesetting.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
