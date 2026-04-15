// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktAttendanceResultEntitiesSeedData.cs
// 功能描述：TaktAttendanceResult 实体字段翻译种子数据，与 TaktRoleDeptEntitiesSeedData 风格一致，与 TaktAttendanceResult.cs 一一对应。
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
/// TaktAttendanceResult 实体翻译种子数据（考勤日结结果）。
/// </summary>
public class TaktAttendanceResultEntitiesSeedData : ITaktSeedData
{
    public int Order => 211;

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
            var allTranslations = GetAllAttendanceResultEntityTranslations();

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

    private static List<TaktTranslation> GetAllAttendanceResultEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.attendanceresult._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult._self", TranslationValue = "نتيجة الحضور اليومية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult._self", TranslationValue = "Attendance result", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult._self", TranslationValue = "Resultado de asistencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult._self", TranslationValue = "Résultat de présence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult._self", TranslationValue = "勤怠日次結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult._self", TranslationValue = "근태 일일 결과", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult._self", TranslationValue = "Итог учёта за день", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult._self", TranslationValue = "考勤日结结果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult._self", TranslationValue = "考勤日結結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.keyword
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "الموظف، التاريخ، الحالة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "Employee, date, status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "Empleado, fecha, estado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "Employé, date, statut", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "社員、日付、状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "사원, 날짜, 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "Сотрудник, дата, статус", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "员工、日期、状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.keyword", TranslationValue = "員工、日期、狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.employeeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "معرف الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "Employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "ID empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "ID employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "社員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "사원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "ID сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.employeeid", TranslationValue = "員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.attendancedate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "تاريخ الحضور", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "Attendance date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "Fecha de asistencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "Date de présence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "勤怠日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "근태일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "Дата учёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "考勤日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.attendancedate", TranslationValue = "考勤日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.shiftscheduleid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "معرف الجدول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "Shift schedule ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "ID planificación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "ID planning", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "シフト予定ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "근무 일정 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "ID расписания смен", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "排班ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.shiftscheduleid", TranslationValue = "排班ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.attendancestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "حالة الحضور", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "Attendance status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "Estado de asistencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "Statut de présence", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "出勤状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "근태 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "Статус присутствия", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "出勤状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.attendancestatus", TranslationValue = "出勤狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.firstintime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "أول وقت دخول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "First in time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "Primera entrada", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "Première entrée", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "出勤打刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "첫 출근", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "Первый приход", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "上班打卡", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.firstintime", TranslationValue = "上班打卡", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.lastouttime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "آخر وقت خروج", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "Last out time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "Última salida", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "Dernière sortie", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "退勤打刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "마지막 퇴근", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "Последний уход", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "下班打卡", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.lastouttime", TranslationValue = "下班打卡", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.workminutes
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "دقائق العمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "Work minutes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "Minutos trabajados", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "Minutes travaillées", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "出勤分", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "근무 분", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "Минуты работы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "出勤分钟", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.workminutes", TranslationValue = "出勤分鐘", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendanceresult.calculatedat
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "وقت الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "Calculated at", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "Calculado el", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "Calculé le", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "集計時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "계산 시각", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "Время расчёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "计算时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendanceresult.calculatedat", TranslationValue = "計算時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
