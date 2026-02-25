// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktOperLogEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktOperLog 实体字段翻译种子数据，与 TaktMenuEntitiesSeedData 风格一致，entity.operlog / entity.operlog.xxx，每个字段 9 种语言
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
/// TaktOperLog 实体翻译种子数据（与 TaktOperLog.cs 属性一一对应）
/// </summary>
public class TaktOperLogEntitiesSeedData : ITaktSeedData
{
    public int Order => 32;

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
            var allTranslations = GetAllOperLogEntityTranslations();

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
    /// 获取所有 TaktOperLog 实体名称及字段翻译（ResourceKey 拆分风格 entity.operlog / entity.operlog.xxx，与 TaktOperLog.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllOperLogEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.operlog（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog._self", TranslationValue = "سجل العمليات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog._self", TranslationValue = "Operation Log", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog._self", TranslationValue = "Registro de operaciones", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog._self", TranslationValue = "Journal des opérations", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog._self", TranslationValue = "操作ログ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog._self", TranslationValue = "작업 로그", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog._self", TranslationValue = "Журнал операций", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog._self", TranslationValue = "操作日志", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog._self", TranslationValue = "操作日誌", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.operlog.username / opermodule / opertype / opermethod / requestmethod / operurl / requestparam / jsonresult / operstatus / errormsg / operip / operlocation / opercountry / operprovince / opercity / operisp / opertime / costtime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.username", TranslationValue = "اسم المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.username", TranslationValue = "User Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.username", TranslationValue = "Usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.username", TranslationValue = "Utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.username", TranslationValue = "ユーザー名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.username", TranslationValue = "사용자 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.username", TranslationValue = "Имя пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.username", TranslationValue = "用戶名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.opermodule", TranslationValue = "وحدة العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.opermodule", TranslationValue = "Oper Module", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.opermodule", TranslationValue = "Módulo de operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.opermodule", TranslationValue = "Module opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.opermodule", TranslationValue = "操作モジュール", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.opermodule", TranslationValue = "작업 모듈", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.opermodule", TranslationValue = "Модуль операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.opermodule", TranslationValue = "操作模块", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.opermodule", TranslationValue = "操作模組", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.opertype", TranslationValue = "نوع العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.opertype", TranslationValue = "Oper Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.opertype", TranslationValue = "Tipo de operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.opertype", TranslationValue = "Type d'opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.opertype", TranslationValue = "操作タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.opertype", TranslationValue = "작업 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.opertype", TranslationValue = "Тип операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.opertype", TranslationValue = "操作类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.opertype", TranslationValue = "操作類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.opermethod", TranslationValue = "طريقة العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.opermethod", TranslationValue = "Oper Method", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.opermethod", TranslationValue = "Método de operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.opermethod", TranslationValue = "Méthode opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.opermethod", TranslationValue = "操作方法", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.opermethod", TranslationValue = "작업 방법", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.opermethod", TranslationValue = "Метод операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.opermethod", TranslationValue = "操作方法", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.opermethod", TranslationValue = "操作方法", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "طريقة الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "Request Method", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "Método de solicitud", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "Méthode requête", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "リクエスト方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "요청 방식", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "Метод запроса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "请求方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.requestmethod", TranslationValue = "請求方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.operurl", TranslationValue = "رابط العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.operurl", TranslationValue = "Oper URL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.operurl", TranslationValue = "URL de operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.operurl", TranslationValue = "URL opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.operurl", TranslationValue = "操作URL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.operurl", TranslationValue = "작업 URL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.operurl", TranslationValue = "URL операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.operurl", TranslationValue = "操作URL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.operurl", TranslationValue = "操作URL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.requestparam", TranslationValue = "معلمات الطلب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.requestparam", TranslationValue = "Request Param", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.requestparam", TranslationValue = "Parámetros de solicitud", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.requestparam", TranslationValue = "Param. requête", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.requestparam", TranslationValue = "リクエストパラメータ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.requestparam", TranslationValue = "요청 매개변수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.requestparam", TranslationValue = "Параметры запроса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.requestparam", TranslationValue = "请求参数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.requestparam", TranslationValue = "請求參數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "النتيجة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "JSON Result", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "Resultado JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "Résultat JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "返却結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "반환 결과", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "Результат JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "返回结果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.jsonresult", TranslationValue = "返回結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.operstatus", TranslationValue = "حالة العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.operstatus", TranslationValue = "Oper Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.operstatus", TranslationValue = "Estado de operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.operstatus", TranslationValue = "État opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.operstatus", TranslationValue = "操作状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.operstatus", TranslationValue = "작업 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.operstatus", TranslationValue = "Статус операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.operstatus", TranslationValue = "操作状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.operstatus", TranslationValue = "操作狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.errormsg", TranslationValue = "رسالة الخطأ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.errormsg", TranslationValue = "Error Message", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.errormsg", TranslationValue = "Mensaje de error", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.errormsg", TranslationValue = "Message d'erreur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.errormsg", TranslationValue = "エラーメッセージ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.errormsg", TranslationValue = "오류 메시지", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.errormsg", TranslationValue = "Сообщение об ошибке", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.errormsg", TranslationValue = "错误消息", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.errormsg", TranslationValue = "錯誤消息", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.operip", TranslationValue = "IP العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.operip", TranslationValue = "Oper IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.operip", TranslationValue = "IP de operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.operip", TranslationValue = "IP opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.operip", TranslationValue = "操作IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.operip", TranslationValue = "작업 IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.operip", TranslationValue = "IP операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.operip", TranslationValue = "操作IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.operip", TranslationValue = "操作IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.operlocation", TranslationValue = "موقع العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.operlocation", TranslationValue = "Oper Location", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.operlocation", TranslationValue = "Ubicación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.operlocation", TranslationValue = "Lieu opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.operlocation", TranslationValue = "操作場所", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.operlocation", TranslationValue = "작업 위치", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.operlocation", TranslationValue = "Место операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.operlocation", TranslationValue = "操作地点", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.operlocation", TranslationValue = "操作地點", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.opercountry", TranslationValue = "الدولة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.opercountry", TranslationValue = "Country", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.opercountry", TranslationValue = "País", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.opercountry", TranslationValue = "Pays", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.opercountry", TranslationValue = "国", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.opercountry", TranslationValue = "국가", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.opercountry", TranslationValue = "Страна", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.opercountry", TranslationValue = "操作国家", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.opercountry", TranslationValue = "操作國家", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.operprovince", TranslationValue = "المحافظة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.operprovince", TranslationValue = "Province", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.operprovince", TranslationValue = "Provincia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.operprovince", TranslationValue = "Province", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.operprovince", TranslationValue = "省", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.operprovince", TranslationValue = "지방", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.operprovince", TranslationValue = "Регион", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.operprovince", TranslationValue = "操作省份", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.operprovince", TranslationValue = "操作省份", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.opercity", TranslationValue = "المدينة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.opercity", TranslationValue = "City", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.opercity", TranslationValue = "Ciudad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.opercity", TranslationValue = "Ville", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.opercity", TranslationValue = "市区町村", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.opercity", TranslationValue = "도시", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.opercity", TranslationValue = "Город", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.opercity", TranslationValue = "操作城市", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.opercity", TranslationValue = "操作城市", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.operisp", TranslationValue = "مزود الخدمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.operisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.operisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.operisp", TranslationValue = "FAI", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.operisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.operisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.operisp", TranslationValue = "Провайдер", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.operisp", TranslationValue = "操作ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.operisp", TranslationValue = "操作ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.opertime", TranslationValue = "وقت العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.opertime", TranslationValue = "Oper Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.opertime", TranslationValue = "Hora de operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.opertime", TranslationValue = "Heure opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.opertime", TranslationValue = "操作日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.opertime", TranslationValue = "작업 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.opertime", TranslationValue = "Время операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.opertime", TranslationValue = "操作时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.opertime", TranslationValue = "操作時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.operlog.costtime", TranslationValue = "الوقت المستغرق", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.operlog.costtime", TranslationValue = "Cost Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.operlog.costtime", TranslationValue = "Tiempo de ejecución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.operlog.costtime", TranslationValue = "Temps d'exécution", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.operlog.costtime", TranslationValue = "実行時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.operlog.costtime", TranslationValue = "실행 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.operlog.costtime", TranslationValue = "Время выполнения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.operlog.costtime", TranslationValue = "执行耗时", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.operlog.costtime", TranslationValue = "執行耗時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
