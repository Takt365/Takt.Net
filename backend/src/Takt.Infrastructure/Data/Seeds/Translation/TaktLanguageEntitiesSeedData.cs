// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktLanguageEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktLanguage 实体字段翻译种子数据，与 TaktMenuEntitiesSeedData 风格一致，entity.language / entity.language.xxx，每个字段 9 种语言
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
/// TaktLanguage 实体翻译种子数据（与 TaktLanguage.cs 属性一一对应）
/// </summary>
public class TaktLanguageEntitiesSeedData : ITaktSeedData
{
    public int Order => 25;

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
            var allTranslations = GetAllLanguageEntityTranslations();

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
    /// 获取所有 TaktLanguage 实体名称及字段翻译（ResourceKey 拆分风格 entity.language / entity.language.xxx，与 TaktLanguage.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllLanguageEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.language（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language._self", TranslationValue = "اللغة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language._self", TranslationValue = "Language", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language._self", TranslationValue = "Idioma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language._self", TranslationValue = "Langue", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language._self", TranslationValue = "言語", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language._self", TranslationValue = "언어", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language._self", TranslationValue = "Язык", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language._self", TranslationValue = "语言", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language._self", TranslationValue = "語言", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.name
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.name", TranslationValue = "اسم اللغة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.name", TranslationValue = "Language Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.name", TranslationValue = "Nombre del idioma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.name", TranslationValue = "Nom de la langue", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.name", TranslationValue = "言語名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.name", TranslationValue = "언어 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.name", TranslationValue = "Название языка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.name", TranslationValue = "语言名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.name", TranslationValue = "語言名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.culturecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.culturecode", TranslationValue = "رمز اللغة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.culturecode", TranslationValue = "Culture Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.culturecode", TranslationValue = "Código de idioma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.culturecode", TranslationValue = "Code culture", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.culturecode", TranslationValue = "言語コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.culturecode", TranslationValue = "언어 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.culturecode", TranslationValue = "Код языка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.culturecode", TranslationValue = "语言编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.culturecode", TranslationValue = "語言編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.nativename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.nativename", TranslationValue = "الاسم المحلي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.nativename", TranslationValue = "Native Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.nativename", TranslationValue = "Nombre nativo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.nativename", TranslationValue = "Nom natif", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.nativename", TranslationValue = "現地語名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.nativename", TranslationValue = "현지어 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.nativename", TranslationValue = "Локальное название", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.nativename", TranslationValue = "本地化名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.nativename", TranslationValue = "本地化名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.icon
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.icon", TranslationValue = "أيقونة اللغة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.icon", TranslationValue = "Language Icon", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.icon", TranslationValue = "Icono de idioma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.icon", TranslationValue = "Icône langue", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.icon", TranslationValue = "言語アイコン", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.icon", TranslationValue = "언어 아이콘", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.icon", TranslationValue = "Иконка языка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.icon", TranslationValue = "语言图标", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.icon", TranslationValue = "語言圖標", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.isdefault
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.isdefault", TranslationValue = "الافتراضي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.isdefault", TranslationValue = "Default", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.isdefault", TranslationValue = "Predeterminado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.isdefault", TranslationValue = "Par défaut", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.isdefault", TranslationValue = "デフォルト", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.isdefault", TranslationValue = "기본", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.isdefault", TranslationValue = "По умолчанию", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.isdefault", TranslationValue = "是否默认语言", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.isdefault", TranslationValue = "是否默認語言", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.isrtl
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.isrtl", TranslationValue = "RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.isrtl", TranslationValue = "RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.isrtl", TranslationValue = "RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.isrtl", TranslationValue = "RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.isrtl", TranslationValue = "RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.isrtl", TranslationValue = "RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.isrtl", TranslationValue = "RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.isrtl", TranslationValue = "是否启用RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.isrtl", TranslationValue = "是否啟用RTL", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.ordernum", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.ordernum", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.language.status
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.language.status", TranslationValue = "حالة اللغة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.language.status", TranslationValue = "Language Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.language.status", TranslationValue = "Estado del idioma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.language.status", TranslationValue = "État langue", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.language.status", TranslationValue = "言語状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.language.status", TranslationValue = "언어 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.language.status", TranslationValue = "Статус языка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.language.status", TranslationValue = "语言状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.language.status", TranslationValue = "語言狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
