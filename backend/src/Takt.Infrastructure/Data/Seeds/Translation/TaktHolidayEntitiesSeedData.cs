// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktHolidayEntitiesSeedData.cs
// 功能描述：TaktHoliday 实体字段翻译种子数据，与 TaktUserEntitiesSeedData 风格一致，entity.holiday / entity.holiday.xxx，每个字段 9 种语言
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
/// TaktHoliday 实体翻译种子数据（与 TaktHoliday.cs 属性一一对应）
/// </summary>
public class TaktHolidayEntitiesSeedData : ITaktSeedData
{
    public int Order => 12;

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
            var allTranslations = GetAllHolidayEntityTranslations();

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
    /// 获取所有 TaktHoliday 实体名称及字段翻译（ResourceKey 风格 entity.holiday / entity.holiday.xxx，与 TaktHoliday.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllHolidayEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.holiday._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday._self", TranslationValue = "العطلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday._self", TranslationValue = "Holiday", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday._self", TranslationValue = "Festivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday._self", TranslationValue = "Jour férié", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday._self", TranslationValue = "祝日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday._self", TranslationValue = "휴일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday._self", TranslationValue = "Праздник", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday._self", TranslationValue = "假日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday._self", TranslationValue = "假日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.keyword（搜索关键词）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.keyword", TranslationValue = "اسم العطلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.keyword", TranslationValue = "Holiday name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.keyword", TranslationValue = "Nombre del festivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.keyword", TranslationValue = "Nom du jour férié", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.keyword", TranslationValue = "祝日名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.keyword", TranslationValue = "휴일명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.keyword", TranslationValue = "Название праздника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.keyword", TranslationValue = "假日名称、地区", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.keyword", TranslationValue = "假日名稱、地區", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.region
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.region", TranslationValue = "المنطقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.region", TranslationValue = "Region", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.region", TranslationValue = "Región", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.region", TranslationValue = "Région", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.region", TranslationValue = "地域", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.region", TranslationValue = "지역", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.region", TranslationValue = "Регион", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.region", TranslationValue = "地区", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.region", TranslationValue = "地區", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.holidayname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.holidayname", TranslationValue = "اسم العطلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.holidayname", TranslationValue = "Holiday Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.holidayname", TranslationValue = "Nombre del festivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.holidayname", TranslationValue = "Nom du jour férié", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.holidayname", TranslationValue = "祝日名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.holidayname", TranslationValue = "휴일명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.holidayname", TranslationValue = "Название праздника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.holidayname", TranslationValue = "假日名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.holidayname", TranslationValue = "假日名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.holidaytype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "نوع العطلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "Holiday Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "Tipo de festivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "Type de jour férié", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "祝日タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "휴일 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "Тип праздника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "假日类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.holidaytype", TranslationValue = "假日類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.startdate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.startdate", TranslationValue = "تاريخ البداية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.startdate", TranslationValue = "Start Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.startdate", TranslationValue = "Fecha de inicio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.startdate", TranslationValue = "Date de début", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.startdate", TranslationValue = "開始日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.startdate", TranslationValue = "시작일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.startdate", TranslationValue = "Дата начала", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.startdate", TranslationValue = "开始日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.startdate", TranslationValue = "開始日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.enddate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.enddate", TranslationValue = "تاريخ النهاية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.enddate", TranslationValue = "End Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.enddate", TranslationValue = "Fecha de fin", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.enddate", TranslationValue = "Date de fin", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.enddate", TranslationValue = "終了日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.enddate", TranslationValue = "종료일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.enddate", TranslationValue = "Дата окончания", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.enddate", TranslationValue = "结束日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.enddate", TranslationValue = "結束日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.isworkingday
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "يوم عمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "Working Day", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "Día laborable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "Jour ouvrable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "稼働日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "근무일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "Рабочий день", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "是否工作日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.isworkingday", TranslationValue = "是否工作日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.holidaygreeting
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "تحية العطلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "Holiday Greeting", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "Saludo festivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "Message de fête", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "祝日挨拶", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "휴일 인사말", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "Праздничное приветствие", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "假日问候语", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.holidaygreeting", TranslationValue = "假日問候語", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.holidayquote
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "اقتباس العطلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "Holiday Quote", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "Cita festiva", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "Citation de fête", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "祝日引用", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "휴일 인용", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "Праздничная цитата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "假日引用", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.holidayquote", TranslationValue = "假日引用", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.holiday.holidaytheme
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "سمة العطلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "Holiday Theme", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "Tema del festivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "Thème du jour férié", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "祝日テーマ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "휴일 테마", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "Тема праздника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "假日主题", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.holiday.holidaytheme", TranslationValue = "假日主題", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
