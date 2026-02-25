// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQuartzLogEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktQuartzLog 实体字段翻译种子数据，与 TaktMenuEntitiesSeedData 风格一致，entity.quartzlog / entity.quartzlog.xxx，每个字段 9 种语言
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
/// TaktQuartzLog 实体翻译种子数据（与 TaktQuartzLog.cs 属性一一对应）
/// </summary>
public class TaktQuartzLogEntitiesSeedData : ITaktSeedData
{
    public int Order => 33;

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
            var allTranslations = GetAllQuartzLogEntityTranslations();

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

    /// <summary>
    /// 获取所有 TaktQuartzLog 实体名称及字段翻译（ResourceKey 拆分风格 entity.quartzlog / entity.quartzlog.xxx，与 TaktQuartzLog.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllQuartzLogEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.quartzlog（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog._self", TranslationValue = "سجل المهمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog._self", TranslationValue = "Task Log", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog._self", TranslationValue = "Registro de tareas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog._self", TranslationValue = "Journal des tâches", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog._self", TranslationValue = "タスクログ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog._self", TranslationValue = "작업 로그", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog._self", TranslationValue = "Журнал заданий", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog._self", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog._self", TranslationValue = "任務日誌", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.quartzlog.username / jobname / jobgroup / triggername / triggergroup / executestatus / executeresult / errormsg / executetime / costtime / jobdata / nextfiretime / previousfiretime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.username", TranslationValue = "اسم المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.username", TranslationValue = "User Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.username", TranslationValue = "Usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.username", TranslationValue = "Utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.username", TranslationValue = "ユーザー名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.username", TranslationValue = "사용자 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.username", TranslationValue = "Имя пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.username", TranslationValue = "用戶名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "اسم المهمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "Job Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "Nombre del trabajo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "Nom du job", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "ジョブ名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "작업 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "Имя задания", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "任务名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.jobname", TranslationValue = "任務名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "مجموعة المهمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "Job Group", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "Grupo de trabajo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "Groupe job", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "ジョブグループ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "작업 그룹", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "Группа задания", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任务组", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.jobgroup", TranslationValue = "任務組", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "اسم المشغل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "Trigger Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "Nombre del disparador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "Nom du déclencheur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "トリガー名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "트리거 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "Имя триггера", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "触发器名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.triggername", TranslationValue = "觸發器名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "مجموعة المشغل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "Trigger Group", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "Grupo de disparador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "Groupe déclencheur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "トリガーグループ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "트리거 그룹", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "Группа триггера", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "触发器组", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.triggergroup", TranslationValue = "觸發器組", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "حالة التنفيذ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "Execute Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "Estado de ejecución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "État d'exécution", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "実行状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "실행 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "Статус выполнения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "执行状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.executestatus", TranslationValue = "執行狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "نتيجة التنفيذ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "Execute Result", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "Resultado de ejecución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "Résultat d'exécution", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "実行結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "실행 결과", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "Результат выполнения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "执行结果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.executeresult", TranslationValue = "執行結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "رسالة الخطأ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "Error Message", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "Mensaje de error", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "Message d'erreur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "エラーメッセージ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "오류 메시지", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "Сообщение об ошибке", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.errormsg", TranslationValue = "錯誤消息", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "وقت التنفيذ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "Execute Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "Hora de ejecución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "Heure d'exécution", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "実行日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "실행 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "Время выполнения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "执行时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.executetime", TranslationValue = "執行時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "الوقت المستغرق", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "Cost Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "Tiempo de ejecución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "Temps d'exécution", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "実行時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "실행 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "Время выполнения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.costtime", TranslationValue = "執行耗時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "بيانات المهمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "Job Data", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "Datos del trabajo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "Données du job", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "ジョブパラメータ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "작업 매개변수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "Данные задания", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任务参数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.jobdata", TranslationValue = "任務參數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "الوقت التالي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "Next Fire Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "Próxima ejecución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "Prochaine exécution", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "次回実行日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "다음 실행 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "След. выполнение", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "下一次执行时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.nextfiretime", TranslationValue = "下一次執行時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "الوقت السابق", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "Previous Fire Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "Ejecución anterior", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "Exécution précédente", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "前回実行日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "이전 실행 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "Пред. выполнение", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "上一次执行时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.quartzlog.previousfiretime", TranslationValue = "上一次執行時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
