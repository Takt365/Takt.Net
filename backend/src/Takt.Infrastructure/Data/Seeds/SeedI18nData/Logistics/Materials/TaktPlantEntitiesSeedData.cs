// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPlantEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPlant 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Materials;

/// <summary>
/// TaktPlant 实体翻译种子数据（自动生成，与 TaktPlant.cs 属性一一对应）
/// </summary>
public class TaktPlantEntitiesSeedData : ITaktSeedData
{
    public int Order => 999;

    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 1 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktPlantEntityTranslations();

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
                        SortOrder = translation.SortOrder,
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
    /// 获取所有 TaktPlant 实体名称及字段翻译（自动生成，与 TaktPlant.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPlantEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.plant（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant._self", TranslationValue = "工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant._self", TranslationValue = "工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant._self", TranslationValue = "工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant._self", TranslationValue = "工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant._self", TranslationValue = "工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant._self", TranslationValue = "工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.plant.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.code", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.code", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.code", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.code", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.code", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.code", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.plant.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.name", TranslationValue = "工厂名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.plant.shortname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.shortname", TranslationValue = "工厂简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.shortname", TranslationValue = "工厂简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.shortname", TranslationValue = "工厂简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.shortname", TranslationValue = "工厂简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.shortname", TranslationValue = "工厂简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.shortname", TranslationValue = "工厂简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.plant.registrationaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.registrationaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.registrationaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.registrationaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.registrationaddress", TranslationValue = "注册地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.registrationaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.registrationaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.plant.registrationregion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.plant.registrationprovince
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.plant.registrationcity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.plant.businessregion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.plant.businessprovince
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.plant.businesscity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.plant.businessaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.businessaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.businessaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.businessaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.businessaddress", TranslationValue = "经营地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.businessaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.businessaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.plant.address
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.address", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.address", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.address", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.address", TranslationValue = "工厂地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.address", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.address", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.plant.phone
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.phone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.phone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.phone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.phone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.phone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.phone", TranslationValue = "工厂电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.plant.email
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.email", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.email", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.email", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.email", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.email", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.email", TranslationValue = "工厂邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.plant.manager
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.manager", TranslationValue = "工厂负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.manager", TranslationValue = "工厂负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.manager", TranslationValue = "工厂负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.manager", TranslationValue = "工厂负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.manager", TranslationValue = "工厂负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.manager", TranslationValue = "工厂负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.plant.enterprisenature
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.plant.industryattribute
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.plant.enterprisescale
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.plant.businessscope
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.plant.relatedcompany
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.relatedcompany", TranslationValue = "关联公司", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.relatedcompany", TranslationValue = "关联公司", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.relatedcompany", TranslationValue = "关联公司", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.relatedcompany", TranslationValue = "关联公司", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.relatedcompany", TranslationValue = "关联公司", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.relatedcompany", TranslationValue = "关联公司", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.plant.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.status", TranslationValue = "工厂状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.plant.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plant.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plant.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plant.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plant.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plant.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plant.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
        };
    }
}
