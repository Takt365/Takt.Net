// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktOnlineEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktOnline 实体字段翻译种子数据，与 TaktMenuEntitiesSeedData 风格一致，entity.online / entity.online.xxx，每个字段 9 种语言
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
/// TaktOnline 实体翻译种子数据（与 TaktOnline.cs 属性一一对应）
/// </summary>
public class TaktOnlineEntitiesSeedData : ITaktSeedData
{
    public int Order => 29;

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
            var allTranslations = GetAllOnlineEntityTranslations();

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
    /// 获取所有 TaktOnline 实体名称及字段翻译（ResourceKey 拆分风格 entity.online / entity.online.xxx，与 TaktOnline.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllOnlineEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.online（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online._self", TranslationValue = "المستخدمون عبر الإنترنت", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online._self", TranslationValue = "Online User", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online._self", TranslationValue = "Usuario en línea", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online._self", TranslationValue = "Utilisateur en ligne", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online._self", TranslationValue = "オンラインユーザー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online._self", TranslationValue = "온라인 사용자", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online._self", TranslationValue = "Пользователь в сети", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online._self", TranslationValue = "在线用户", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online._self", TranslationValue = "在線用戶", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.online.connectionid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectionid", TranslationValue = "معرف الاتصال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectionid", TranslationValue = "Connection ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectionid", TranslationValue = "ID de conexión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectionid", TranslationValue = "ID connexion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectionid", TranslationValue = "接続ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectionid", TranslationValue = "연결 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectionid", TranslationValue = "ID подключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectionid", TranslationValue = "SignalR连接ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectionid", TranslationValue = "SignalR連接ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.online.username
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.username", TranslationValue = "اسم المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.username", TranslationValue = "User Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.username", TranslationValue = "Nombre de usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.username", TranslationValue = "Nom d'utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.username", TranslationValue = "ユーザー名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.username", TranslationValue = "사용자 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.username", TranslationValue = "Имя пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.username", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.username", TranslationValue = "用戶名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.online.userid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.userid", TranslationValue = "معرف المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.userid", TranslationValue = "User ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.userid", TranslationValue = "ID de usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.userid", TranslationValue = "ID utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.userid", TranslationValue = "ユーザーID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.userid", TranslationValue = "사용자 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.userid", TranslationValue = "ID пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.userid", TranslationValue = "用户ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.userid", TranslationValue = "用戶ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.online.onlinestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.onlinestatus", TranslationValue = "حالة الاتصال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.onlinestatus", TranslationValue = "Online Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.onlinestatus", TranslationValue = "Estado en línea", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.onlinestatus", TranslationValue = "État en ligne", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.onlinestatus", TranslationValue = "オンライン状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.onlinestatus", TranslationValue = "온라인 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.onlinestatus", TranslationValue = "Статус в сети", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.onlinestatus", TranslationValue = "在线状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.onlinestatus", TranslationValue = "在線狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.online.connectip
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectip", TranslationValue = "عنوان IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectip", TranslationValue = "Connect IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectip", TranslationValue = "IP de conexión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectip", TranslationValue = "IP de connexion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectip", TranslationValue = "接続IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectip", TranslationValue = "연결 IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectip", TranslationValue = "IP подключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectip", TranslationValue = "连接IP地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectip", TranslationValue = "連接IP地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.online.connectlocation
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectlocation", TranslationValue = "موقع الاتصال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectlocation", TranslationValue = "Connect Location", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectlocation", TranslationValue = "Ubicación de conexión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectlocation", TranslationValue = "Lieu de connexion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectlocation", TranslationValue = "接続場所", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectlocation", TranslationValue = "연결 위치", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectlocation", TranslationValue = "Место подключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectlocation", TranslationValue = "连接地点", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectlocation", TranslationValue = "連接地點", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.online.connectcountry / connectprovince / connectcity / connectisp / useragent / devicetype / browsertype / operatingsystem / connecttime / lastactivetime / disconnecttime / connectionduration
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectcountry", TranslationValue = "الدولة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectcountry", TranslationValue = "Country", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectcountry", TranslationValue = "País", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectcountry", TranslationValue = "Pays", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectcountry", TranslationValue = "国", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectcountry", TranslationValue = "국가", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectcountry", TranslationValue = "Страна", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectcountry", TranslationValue = "连接国家", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectcountry", TranslationValue = "連接國家", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectprovince", TranslationValue = "المحافظة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectprovince", TranslationValue = "Province", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectprovince", TranslationValue = "Provincia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectprovince", TranslationValue = "Province", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectprovince", TranslationValue = "省", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectprovince", TranslationValue = "지방", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectprovince", TranslationValue = "Регион", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectprovince", TranslationValue = "连接省份", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectprovince", TranslationValue = "連接省份", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectcity", TranslationValue = "المدينة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectcity", TranslationValue = "City", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectcity", TranslationValue = "Ciudad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectcity", TranslationValue = "Ville", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectcity", TranslationValue = "市区町村", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectcity", TranslationValue = "도시", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectcity", TranslationValue = "Город", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectcity", TranslationValue = "连接城市", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectcity", TranslationValue = "連接城市", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectisp", TranslationValue = "مزود الخدمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectisp", TranslationValue = "FAI", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectisp", TranslationValue = "ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectisp", TranslationValue = "Провайдер", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectisp", TranslationValue = "连接ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectisp", TranslationValue = "連接ISP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.useragent", TranslationValue = "User-Agent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.devicetype", TranslationValue = "نوع الجهاز", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.devicetype", TranslationValue = "Device Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.devicetype", TranslationValue = "Tipo de dispositivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.devicetype", TranslationValue = "Type d'appareil", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.devicetype", TranslationValue = "デバイスタイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.devicetype", TranslationValue = "기기 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.devicetype", TranslationValue = "Тип устройства", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.devicetype", TranslationValue = "设备类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.devicetype", TranslationValue = "設備類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.browsertype", TranslationValue = "نوع المتصفح", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.browsertype", TranslationValue = "Browser Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.browsertype", TranslationValue = "Tipo de navegador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.browsertype", TranslationValue = "Type de navigateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.browsertype", TranslationValue = "ブラウザタイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.browsertype", TranslationValue = "브라우저 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.browsertype", TranslationValue = "Тип браузера", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.browsertype", TranslationValue = "浏览器类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.browsertype", TranslationValue = "瀏覽器類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.operatingsystem", TranslationValue = "نظام التشغيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.operatingsystem", TranslationValue = "Operating System", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.operatingsystem", TranslationValue = "Sistema operativo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.operatingsystem", TranslationValue = "Système d'exploitation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.operatingsystem", TranslationValue = "OS", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.operatingsystem", TranslationValue = "운영 체제", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.operatingsystem", TranslationValue = "ОС", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.operatingsystem", TranslationValue = "操作系统", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.operatingsystem", TranslationValue = "操作系統", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connecttime", TranslationValue = "وقت الاتصال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connecttime", TranslationValue = "Connect Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connecttime", TranslationValue = "Hora de conexión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connecttime", TranslationValue = "Heure connexion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connecttime", TranslationValue = "接続日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connecttime", TranslationValue = "연결 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connecttime", TranslationValue = "Время подключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connecttime", TranslationValue = "连接时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connecttime", TranslationValue = "連接時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.lastactivetime", TranslationValue = "آخر نشاط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.lastactivetime", TranslationValue = "Last Active", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.lastactivetime", TranslationValue = "Última actividad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.lastactivetime", TranslationValue = "Dernière activité", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.lastactivetime", TranslationValue = "最終活動", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.lastactivetime", TranslationValue = "마지막 활동", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.lastactivetime", TranslationValue = "Последняя активность", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.lastactivetime", TranslationValue = "最后活动时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.lastactivetime", TranslationValue = "最後活動時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.disconnecttime", TranslationValue = "وقت قطع الاتصال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.disconnecttime", TranslationValue = "Disconnect Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.disconnecttime", TranslationValue = "Hora de desconexión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.disconnecttime", TranslationValue = "Heure déconnexion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.disconnecttime", TranslationValue = "切断日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.disconnecttime", TranslationValue = "연결 해제 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.disconnecttime", TranslationValue = "Время отключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.disconnecttime", TranslationValue = "断开时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.disconnecttime", TranslationValue = "斷開時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.online.connectionduration", TranslationValue = "مدة الاتصال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.online.connectionduration", TranslationValue = "Connection Duration", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.online.connectionduration", TranslationValue = "Duración de conexión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.online.connectionduration", TranslationValue = "Durée connexion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.online.connectionduration", TranslationValue = "接続時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.online.connectionduration", TranslationValue = "연결 지속 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.online.connectionduration", TranslationValue = "Длительность подключения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.online.connectionduration", TranslationValue = "连接时长", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.online.connectionduration", TranslationValue = "連接時長", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
