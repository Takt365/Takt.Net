// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPlantEntitiesSeedData.cs
// 创建时间：2025-02-15
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktPlant 实体字段翻译种子数据，与 TaktPlant.cs 属性一一对应，entity.plant / entity.plant.xxx，每个字段 9 种语言
// 执行库：ConfigId=4（Routine，翻译表在此库）。由 TaktEntityDatabaseMapping 按类名 *EntitiesSeedData 归入 4，与 PlantSeedData(3) 区分。
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
/// TaktPlant 实体翻译种子数据（与 TaktPlant.cs 属性一一对应，参照 TaktUserEntitiesSeedData）。翻译种子始终在 ConfigId=4（Routine）执行。
/// </summary>
public class TaktPlantEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在 TaktCompanyEntitiesSeedData 之后）
    /// </summary>
    public int Order => 56;

    /// <summary>
    /// 初始化工厂实体翻译种子数据
    /// </summary>
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
            var allTranslations = GetAllPlantEntityTranslations();

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
                else
                {
                    if (existing.TranslationValue != translation.TranslationValue)
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
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有 TaktPlant 实体名称及字段翻译（ResourceKey 拆分风格 entity.plant / entity.plant.xxx，与 TaktPlant.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllPlantEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.plant._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant._self", TranslationValue = "معلومات المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant._self", TranslationValue = "Plant", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant._self", TranslationValue = "Planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant._self", TranslationValue = "Usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant._self", TranslationValue = "工場", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant._self", TranslationValue = "공장", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant._self", TranslationValue = "Завод", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant._self", TranslationValue = "工厂", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant._self", TranslationValue = "工廠", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantcode", TranslationValue = "رمز المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantcode", TranslationValue = "Plant Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantcode", TranslationValue = "Código de planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantcode", TranslationValue = "Code usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantcode", TranslationValue = "工場コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantcode", TranslationValue = "공장 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantcode", TranslationValue = "Код завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantcode", TranslationValue = "工廠代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantname", TranslationValue = "اسم المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantname", TranslationValue = "Plant Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantname", TranslationValue = "Nombre de planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantname", TranslationValue = "Nom de l'usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantname", TranslationValue = "工場名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantname", TranslationValue = "공장명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantname", TranslationValue = "Название завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantname", TranslationValue = "工厂名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantname", TranslationValue = "工廠名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantname2
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantname2", TranslationValue = "اسم المصنع 2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantname2", TranslationValue = "Plant Name 2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantname2", TranslationValue = "Nombre de planta 2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantname2", TranslationValue = "Nom usine 2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantname2", TranslationValue = "工場名2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantname2", TranslationValue = "공장명2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantname2", TranslationValue = "Название завода 2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantname2", TranslationValue = "工厂名称2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantname2", TranslationValue = "工廠名稱2", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.shortname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.shortname", TranslationValue = "الاسم المختصر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.shortname", TranslationValue = "Short Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.shortname", TranslationValue = "Nombre corto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.shortname", TranslationValue = "Nom abrégé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.shortname", TranslationValue = "略称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.shortname", TranslationValue = "약칭", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.shortname", TranslationValue = "Краткое название", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.shortname", TranslationValue = "简称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.shortname", TranslationValue = "簡稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.region
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.region", TranslationValue = "المنطقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.region", TranslationValue = "Region", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.region", TranslationValue = "Región", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.region", TranslationValue = "Région", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.region", TranslationValue = "地区", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.region", TranslationValue = "지역", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.region", TranslationValue = "Регион", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.region", TranslationValue = "地区", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.region", TranslationValue = "地區", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.province
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.province", TranslationValue = "المحافظة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.province", TranslationValue = "Province", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.province", TranslationValue = "Provincia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.province", TranslationValue = "Province", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.province", TranslationValue = "省", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.province", TranslationValue = "성", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.province", TranslationValue = "Провинция", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.province", TranslationValue = "省", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.province", TranslationValue = "省", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.city
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.city", TranslationValue = "المدينة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.city", TranslationValue = "City", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.city", TranslationValue = "Ciudad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.city", TranslationValue = "Ville", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.city", TranslationValue = "市", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.city", TranslationValue = "시", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.city", TranslationValue = "Город", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.city", TranslationValue = "市", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.city", TranslationValue = "市", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.county
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.county", TranslationValue = "المقاطعة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.county", TranslationValue = "County", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.county", TranslationValue = "Condado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.county", TranslationValue = "Comté", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.county", TranslationValue = "县", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.county", TranslationValue = "군", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.county", TranslationValue = "Уезд", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.county", TranslationValue = "县", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.county", TranslationValue = "縣", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.townstreet
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.townstreet", TranslationValue = "الشارع/البلدية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.townstreet", TranslationValue = "Town/Street", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.townstreet", TranslationValue = "Pueblo/Calle", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.townstreet", TranslationValue = "Ville/Rue", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.townstreet", TranslationValue = "镇街", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.townstreet", TranslationValue = "읍·동", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.townstreet", TranslationValue = "Улица", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.townstreet", TranslationValue = "镇街", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.townstreet", TranslationValue = "鎮街", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.village
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.village", TranslationValue = "القرية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.village", TranslationValue = "Village", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.village", TranslationValue = "Pueblo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.village", TranslationValue = "Village", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.village", TranslationValue = "村庄", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.village", TranslationValue = "마을", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.village", TranslationValue = "Деревня", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.village", TranslationValue = "村庄", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.village", TranslationValue = "村莊", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.address
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.address", TranslationValue = "العنوان الكامل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.address", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.address", TranslationValue = "Dirección", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.address", TranslationValue = "Adresse", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.address", TranslationValue = "完整地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.address", TranslationValue = "전체 주소", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.address", TranslationValue = "Полный адрес", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.address", TranslationValue = "完整地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.address", TranslationValue = "完整地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.localcurrency
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.localcurrency", TranslationValue = "العملة المحلية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.localcurrency", TranslationValue = "Local Currency", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.localcurrency", TranslationValue = "Moneda local", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.localcurrency", TranslationValue = "Devise locale", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.localcurrency", TranslationValue = "本位币", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.localcurrency", TranslationValue = "기준 통화", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.localcurrency", TranslationValue = "Локальная валюта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.localcurrency", TranslationValue = "本位币", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.localcurrency", TranslationValue = "本位幣", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.languagecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.languagecode", TranslationValue = "رمز اللغة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.languagecode", TranslationValue = "Language Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.languagecode", TranslationValue = "Código de idioma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.languagecode", TranslationValue = "Code langue", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.languagecode", TranslationValue = "语言代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.languagecode", TranslationValue = "언어 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.languagecode", TranslationValue = "Код языка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.languagecode", TranslationValue = "语言代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.languagecode", TranslationValue = "語言代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.chartofaccounts
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "دليل الحسابات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "Chart of Accounts", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "Plan de cuentas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "Plan comptable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "会计科目表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "회계과목표", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "План счетов", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "会计科目表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.chartofaccounts", TranslationValue = "會計科目表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.controllingarea
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.controllingarea", TranslationValue = "نطاق التحكم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.controllingarea", TranslationValue = "Controlling Area", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.controllingarea", TranslationValue = "Área de control", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.controllingarea", TranslationValue = "Zone de contrôle", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.controllingarea", TranslationValue = "控制范围", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.controllingarea", TranslationValue = "관리 영역", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.controllingarea", TranslationValue = "Область контроля", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.controllingarea", TranslationValue = "控制范围", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.controllingarea", TranslationValue = "控制範圍", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.salesorg
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.salesorg", TranslationValue = "منظمة المبيعات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.salesorg", TranslationValue = "Sales Organization", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.salesorg", TranslationValue = "Organización de ventas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.salesorg", TranslationValue = "Organisation ventes", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.salesorg", TranslationValue = "销售组织", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.salesorg", TranslationValue = "영업 조직", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.salesorg", TranslationValue = "Организация продаж", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.salesorg", TranslationValue = "销售组织", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.salesorg", TranslationValue = "銷售組織", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantphone
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantphone", TranslationValue = "هاتف المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantphone", TranslationValue = "Plant Phone", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantphone", TranslationValue = "Teléfono de planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantphone", TranslationValue = "Téléphone usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantphone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantphone", TranslationValue = "공장 전화", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantphone", TranslationValue = "Телефон завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantphone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantphone", TranslationValue = "工廠電話", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantemail
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantemail", TranslationValue = "البريد الإلكتروني للمصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantemail", TranslationValue = "Plant Email", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantemail", TranslationValue = "Correo de planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantemail", TranslationValue = "E-mail usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantemail", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantemail", TranslationValue = "공장 이메일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantemail", TranslationValue = "Email завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantemail", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantemail", TranslationValue = "工廠郵箱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantfax
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantfax", TranslationValue = "فاكس المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantfax", TranslationValue = "Plant Fax", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantfax", TranslationValue = "Fax de planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantfax", TranslationValue = "Fax usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantfax", TranslationValue = "工厂传真", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantfax", TranslationValue = "공장 팩스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantfax", TranslationValue = "Факс завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantfax", TranslationValue = "工厂传真", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantfax", TranslationValue = "工廠傳真", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantwebsite
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "موقع المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "Plant Website", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "Sitio web de planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "Site web usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "工厂网站", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "공장 웹사이트", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "Сайт завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "工厂网站", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantwebsite", TranslationValue = "工廠網站", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.socialaccounts
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "حسابات التواصل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "Social Accounts", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "Cuentas sociales", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "Comptes sociaux", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "社交账号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "소셜 계정", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "Социальные аккаунты", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "社交账号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.socialaccounts", TranslationValue = "社交帳號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.unifiedsocialcreditcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "رمز الائتمان الاجتماعي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "Unified Social Credit Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "Código de crédito social", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "Code crédit social unifié", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "统一社会信用代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "통합사회신용코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "ЕГРЮЛ/ИНН", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "统一社会信用代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.unifiedsocialcreditcode", TranslationValue = "統一社會信用代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.taxregistrationnumber
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "رقم التسجيل الضريبي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "Tax Registration Number", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "Número de registro fiscal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "Numéro enregistrement fiscal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "세무등록번호", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "ИНН", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.taxregistrationnumber", TranslationValue = "稅務登記號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.vatregistrationnumber
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "رقم تسجيل ضريبة القيمة المضافة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "VAT Registration Number", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "Número IVA", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "Numéro TVA", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "增值税登记号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "부가세등록번호", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "НДС номер", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "增值税登记号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.vatregistrationnumber", TranslationValue = "增值稅登記號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.legalrepresentative
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "الممثل القانوني", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "Legal Representative", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "Representante legal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "Représentant légal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "법정대표인", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "Юридический представитель", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.industrytype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.industrytype", TranslationValue = "نوع الصناعة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.industrytype", TranslationValue = "Industry Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.industrytype", TranslationValue = "Tipo de industria", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.industrytype", TranslationValue = "Type d'industrie", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.industrytype", TranslationValue = "行业类别", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.industrytype", TranslationValue = "업종 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.industrytype", TranslationValue = "Тип отрасли", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.industrytype", TranslationValue = "行业类别", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.industrytype", TranslationValue = "行業類別", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.enterpriseregistrationtype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "نوع تسجيل المؤسسة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "Enterprise Registration Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "Tipo de registro empresarial", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "Type d'enregistrement", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "기업등록유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "Тип регистрации", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.enterpriseregistrationtype", TranslationValue = "企業性質", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.enterprisesize
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "حجم المؤسسة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "Enterprise Size", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "Tamaño de empresa", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "Taille entreprise", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "기업 규모", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "Размер предприятия", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.enterprisesize", TranslationValue = "企業規模", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.registeredcapital
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "رأس المال المسجل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "Registered Capital", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "Capital registrado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "Capital enregistré", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "등록 자본", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "Уставный капитал", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.registeredcapital", TranslationValue = "註冊資本", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.establishmentdate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "تاريخ التأسيس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "Establishment Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "Fecha de fundación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "Date de création", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "成立日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "설립일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "Дата основания", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "成立日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.establishmentdate", TranslationValue = "成立日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.dissolutiondate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "تاريخ الحل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "Dissolution Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "Fecha de disolución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "Date de dissolution", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "解散日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "해산일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "Дата ликвидации", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "解散日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.dissolutiondate", TranslationValue = "解散日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.plantstatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.plantstatus", TranslationValue = "حالة المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.plantstatus", TranslationValue = "Plant Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.plantstatus", TranslationValue = "Estado de planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.plantstatus", TranslationValue = "État usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.plantstatus", TranslationValue = "工厂状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.plantstatus", TranslationValue = "공장 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.plantstatus", TranslationValue = "Статус завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.plantstatus", TranslationValue = "工厂状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.plantstatus", TranslationValue = "工廠狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.plant.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.plant.ordernum", TranslationValue = "رقم الترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.ordernum", TranslationValue = "Order Number", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.plant.ordernum", TranslationValue = "Número de orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.plant.ordernum", TranslationValue = "Numéro d'ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.ordernum", TranslationValue = "정렬 번호", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.plant.ordernum", TranslationValue = "Порядковый номер", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
