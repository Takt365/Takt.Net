// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktAttendanceDeviceEntitiesSeedData.cs
// 功能描述：TaktAttendanceDevice 实体字段翻译种子数据，与 TaktRoleDeptEntitiesSeedData 风格一致，与 TaktAttendanceDevice.cs 一一对应。
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
/// TaktAttendanceDevice 实体翻译种子数据（考勤设备）。
/// </summary>
public class TaktAttendanceDeviceEntitiesSeedData : ITaktSeedData
{
    public int Order => 207;

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
            var allTranslations = GetAllAttendanceDeviceEntityTranslations();

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

    private static List<TaktTranslation> GetAllAttendanceDeviceEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.attendancedevice._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice._self", TranslationValue = "جهاز الحضور", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice._self", TranslationValue = "Attendance device", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice._self", TranslationValue = "Dispositivo de asistencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice._self", TranslationValue = "Appareil de pointage", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice._self", TranslationValue = "勤怠端末", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice._self", TranslationValue = "근태 단말", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice._self", TranslationValue = "Устройство учёта времени", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice._self", TranslationValue = "考勤设备", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice._self", TranslationValue = "考勤設備", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.keyword
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "رمز الجهاز، الاسم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "Device code, name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "Código y nombre del dispositivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "Code et nom de l’appareil", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "端末コード、名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "단말 코드, 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "Код и название устройства", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "设备编码、名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.keyword", TranslationValue = "設備編碼、名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.devicecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "رمز الجهاز", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "Device code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "Código del dispositivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "Code appareil", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "端末コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "단말 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "Код устройства", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.devicecode", TranslationValue = "設備編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.devicename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "اسم الجهاز", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "Device name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "Nombre del dispositivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "Nom de l’appareil", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "端末名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "단말 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "Название устройства", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "设备名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.devicename", TranslationValue = "設備名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.devicetype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "نوع الجهاز", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "Device type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "Tipo de dispositivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "Type d’appareil", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "端末種別", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "단말 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "Тип устройства", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "设备类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.devicetype", TranslationValue = "設備類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.manufacturer
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "الشركة المصنعة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "Manufacturer", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "Fabricante", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "Fabricant", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "メーカー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "제조사", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "Производитель", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "设备厂商", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.manufacturer", TranslationValue = "設備廠商", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.ipaddress
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "عنوان IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "IP address", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "Dirección IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "Adresse IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "IPアドレス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "IP 주소", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "IP-адрес", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "IP地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.ipaddress", TranslationValue = "IP位址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.port
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.port", TranslationValue = "المنفذ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.port", TranslationValue = "Port", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.port", TranslationValue = "Puerto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.port", TranslationValue = "Port", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.port", TranslationValue = "ポート", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.port", TranslationValue = "포트", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.port", TranslationValue = "Порт", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.port", TranslationValue = "端口", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.port", TranslationValue = "埠號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.devicemodel
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "طراز الجهاز", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "Device model", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "Modelo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "Modèle", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "型番", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "모델", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "Модель", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "设备型号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.devicemodel", TranslationValue = "設備型號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.apisecret
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "مفتاح الوصول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "API secret", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "Secreto API", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "Secret API", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "APIシークレット", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "API 비밀키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "Секрет API", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "接入密钥", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.apisecret", TranslationValue = "接入金鑰", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.configjson
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "إعداد JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "Config JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "JSON de configuración", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "JSON de configuration", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "設定JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "설정 JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "JSON настроек", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "设备配置JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.configjson", TranslationValue = "設備設定 JSON", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.devicestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "حالة الجهاز", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "Device status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "Estado del dispositivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "État de l’appareil", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "端末状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "단말 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "Статус устройства", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "设备状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.devicestatus", TranslationValue = "設備狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.ispushenabled
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "تفعيل الدفع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "Push enabled", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "Push habilitado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "Push activé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "プッシュ受信", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "푸시 수신", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "Приём push", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "启用推送", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.ispushenabled", TranslationValue = "啟用推送", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.lastpullat
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "آخر سحب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "Last pull at", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "Última descarga", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "Dernier tirage", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "最終取得時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "마지막 가져오기", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "Последняя выгрузка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "上次拉取时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.lastpullat", TranslationValue = "上次拉取時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.attendancedevice.lastpushat
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "آخر دفع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "Last push at", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "Último push", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "Dernier push", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "最終受信時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "마지막 푸시", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "Последний push", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "上次推送时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.attendancedevice.lastpushat", TranslationValue = "上次推送時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
