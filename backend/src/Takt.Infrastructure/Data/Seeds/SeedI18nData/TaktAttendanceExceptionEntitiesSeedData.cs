// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktAttendanceExceptionEntitiesSeedData.cs
// 功能描述：TaktAttendanceException 实体字段翻译种子数据，与 TaktRoleDeptEntitiesSeedData 风格一致，与 TaktAttendanceException.cs 一一对应。
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
/// TaktAttendanceException 实体翻译种子数据（考勤异常）。
/// </summary>
public class TaktAttendanceExceptionEntitiesSeedData : ITaktSeedData
{
    public int Order => 210;

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
            var allTranslations = GetAllAttendanceExceptionEntityTranslations();

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

    private static List<TaktTranslation> GetAllAttendanceExceptionEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.attendanceexception._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceexception._self", TranslationValue = "استثناء الحضور", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceexception._self", TranslationValue = "Attendance exception", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceexception._self", TranslationValue = "Excepción de asistencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceexception._self", TranslationValue = "Anomalie de présence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceexception._self", TranslationValue = "勤怠異常", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceexception._self", TranslationValue = "근태 예외", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceexception._self", TranslationValue = "Исключение по учёту времени", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceexception._self", TranslationValue = "考勤异常", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceexception._self", TranslationValue = "考勤異常", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceexception.keyword
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "الموظف، التاريخ، الملخص", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "Employee, date, summary", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "Empleado, fecha, resumen", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "Employé, date, résumé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "社員、日付、概要", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "사원, 날짜, 요약", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "Сотрудник, дата, описание", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "员工、日期、说明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceexception.keyword", TranslationValue = "員工、日期、說明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceexception.employeeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "معرف الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "Employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "ID empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "ID employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "社員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "사원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "ID сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceexception.employeeid", TranslationValue = "員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceexception.exceptiondate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "تاريخ الاستثناء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "Exception date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "Fecha de excepción", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "Date d’anomalie", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "異常日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "예외일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "Дата исключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "异常日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceexception.exceptiondate", TranslationValue = "異常日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceexception.exceptiontype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "نوع الاستثناء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "Exception type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "Tipo de excepción", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "Type d’anomalie", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "異常種別", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "예외 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "Тип исключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "异常类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceexception.exceptiontype", TranslationValue = "異常類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceexception.summary
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "الملخص", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "Summary", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "Resumen", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "Résumé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "Описание", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "说明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceexception.summary", TranslationValue = "說明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceexception.handlestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "حالة المعالجة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "Handle status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "Estado de gestión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "Statut de traitement", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "処理状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "처리 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "Статус обработки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "处理状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceexception.handlestatus", TranslationValue = "處理狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
