// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPlantMaterialEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktPlantMaterial 实体字段翻译种子数据（自动生成）
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
/// TaktPlantMaterial 实体翻译种子数据（自动生成，与 TaktPlantMaterial.cs 属性一一对应）
/// </summary>
public class TaktPlantMaterialEntitiesSeedData : ITaktSeedData
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
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktPlantMaterialEntityTranslations();

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
    /// 获取所有 TaktPlantMaterial 实体名称及字段翻译（自动生成，与 TaktPlantMaterial.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktPlantMaterialEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.plantmaterial（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial._self", TranslationValue = "工厂物料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial._self", TranslationValue = "工厂物料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial._self", TranslationValue = "工厂物料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial._self", TranslationValue = "工厂物料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial._self", TranslationValue = "工厂物料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial._self", TranslationValue = "工厂物料", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.plantcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.plantcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.plantcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialcode", TranslationValue = "物料编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialname", TranslationValue = "物料名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.industrysector
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.industrysector", TranslationValue = "行业领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.industrysector", TranslationValue = "行业领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.industrysector", TranslationValue = "行业领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.industrysector", TranslationValue = "行业领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.industrysector", TranslationValue = "行业领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.industrysector", TranslationValue = "行业领域", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialhierarchyid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialhierarchyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialhierarchyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialhierarchyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialhierarchyid", TranslationValue = "品目阶层ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialhierarchyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialhierarchyid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialhierarchyname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialhierarchyname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialhierarchyname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialhierarchyname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialhierarchyname", TranslationValue = "品目阶层名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialhierarchyname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialhierarchyname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialgroupcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialgroupcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialgroupcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialgroupcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialgroupcode", TranslationValue = "品目组代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialgroupcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialgroupcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialtype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialtype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialtype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialtype", TranslationValue = "物料类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialtype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialtype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialspecification
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialspecification", TranslationValue = "物料规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialmodel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialmodel", TranslationValue = "物料型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialmodel", TranslationValue = "物料型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialmodel", TranslationValue = "物料型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialmodel", TranslationValue = "物料型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialmodel", TranslationValue = "物料型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialmodel", TranslationValue = "物料型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialbrand
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialbrand", TranslationValue = "物料品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialbrand", TranslationValue = "物料品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialbrand", TranslationValue = "物料品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialbrand", TranslationValue = "物料品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialbrand", TranslationValue = "物料品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialbrand", TranslationValue = "物料品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.baseunit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.baseunit", TranslationValue = "基本单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.baseunit", TranslationValue = "基本单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.baseunit", TranslationValue = "基本单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.baseunit", TranslationValue = "基本单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.baseunit", TranslationValue = "基本单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.baseunit", TranslationValue = "基本单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.purchasegroup
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.purchasegroup", TranslationValue = "采购组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.purchasegroup", TranslationValue = "采购组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.purchasegroup", TranslationValue = "采购组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.purchasegroup", TranslationValue = "采购组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.purchasegroup", TranslationValue = "采购组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.purchasegroup", TranslationValue = "采购组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.purchasetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.purchasetype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.purchasetype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.purchasetype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.purchasetype", TranslationValue = "采购类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.purchasetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.purchasetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.specialprocurement
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.specialprocurement", TranslationValue = "特殊采购", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.specialprocurement", TranslationValue = "特殊采购", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.specialprocurement", TranslationValue = "特殊采购", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.specialprocurement", TranslationValue = "特殊采购", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.specialprocurement", TranslationValue = "特殊采购", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.specialprocurement", TranslationValue = "特殊采购", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.isbulk
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.isbulk", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.isbulk", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.isbulk", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.isbulk", TranslationValue = "是否散装", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.isbulk", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.isbulk", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.minorderquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.minorderquantity", TranslationValue = "最小起订量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.minorderquantity", TranslationValue = "最小起订量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.minorderquantity", TranslationValue = "最小起订量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.minorderquantity", TranslationValue = "最小起订量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.minorderquantity", TranslationValue = "最小起订量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.minorderquantity", TranslationValue = "最小起订量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.roundingvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.roundingvalue", TranslationValue = "舍入值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.roundingvalue", TranslationValue = "舍入值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.roundingvalue", TranslationValue = "舍入值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.roundingvalue", TranslationValue = "舍入值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.roundingvalue", TranslationValue = "舍入值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.roundingvalue", TranslationValue = "舍入值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.planneddeliverytimedays
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.planneddeliverytimedays", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.planneddeliverytimedays", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.planneddeliverytimedays", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.planneddeliverytimedays", TranslationValue = "计划交货时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.planneddeliverytimedays", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.planneddeliverytimedays", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.inhouseproductiondays
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.inhouseproductiondays", TranslationValue = "自制生产天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.inhouseproductiondays", TranslationValue = "自制生产天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.inhouseproductiondays", TranslationValue = "自制生产天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.inhouseproductiondays", TranslationValue = "自制生产天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.inhouseproductiondays", TranslationValue = "自制生产天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.inhouseproductiondays", TranslationValue = "自制生产天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.suppliername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.suppliername", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.suppliername", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.suppliername", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.suppliername", TranslationValue = "供应商名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.suppliername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.suppliername", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.manufacturer
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.manufacturerpartnumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.manufacturerpartnumber", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.manufacturerpartnumber", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.manufacturerpartnumber", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.manufacturerpartnumber", TranslationValue = "制造商零件编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.manufacturerpartnumber", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.manufacturerpartnumber", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.currencycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.currencycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.currencycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.currencycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.currencycode", TranslationValue = "币种代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.currencycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.currencycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.pricecontrol
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.pricecontrol", TranslationValue = "Price", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.pricecontrol", TranslationValue = "価格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.pricecontrol", TranslationValue = "가격", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.pricecontrol", TranslationValue = "价格控制", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.pricecontrol", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.pricecontrol", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.priceunit
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.priceunit", TranslationValue = "Price", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.priceunit", TranslationValue = "価格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.priceunit", TranslationValue = "가격", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.priceunit", TranslationValue = "价格单位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.priceunit", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.priceunit", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.valuationcategory
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.valuationcategory", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.valuationcategory", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.valuationcategory", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.valuationcategory", TranslationValue = "评估类别代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.valuationcategory", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.valuationcategory", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.differencecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.differencecode", TranslationValue = "差异码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.differencecode", TranslationValue = "差异码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.differencecode", TranslationValue = "差异码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.differencecode", TranslationValue = "差异码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.differencecode", TranslationValue = "差异码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.differencecode", TranslationValue = "差异码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.profitcenter
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.profitcenter", TranslationValue = "利润中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.profitcenter", TranslationValue = "利润中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.profitcenter", TranslationValue = "利润中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.profitcenter", TranslationValue = "利润中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.profitcenter", TranslationValue = "利润中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.profitcenter", TranslationValue = "利润中心", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.latestpurchaseprice
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.latestpurchaseprice", TranslationValue = "最新采购价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.latestpurchaseprice", TranslationValue = "最新采购价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.latestpurchaseprice", TranslationValue = "最新采购价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.latestpurchaseprice", TranslationValue = "最新采购价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.latestpurchaseprice", TranslationValue = "最新采购价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.latestpurchaseprice", TranslationValue = "最新采购价", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.salesprice
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.salesprice", TranslationValue = "Price", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.salesprice", TranslationValue = "価格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.salesprice", TranslationValue = "가격", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.salesprice", TranslationValue = "销售价格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.salesprice", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.salesprice", TranslationValue = "價格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.safetystock
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.safetystock", TranslationValue = "安全库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.safetystock", TranslationValue = "安全库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.safetystock", TranslationValue = "安全库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.safetystock", TranslationValue = "安全库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.safetystock", TranslationValue = "安全库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.safetystock", TranslationValue = "安全库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.maxstock
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.maxstock", TranslationValue = "最大库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.maxstock", TranslationValue = "最大库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.maxstock", TranslationValue = "最大库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.maxstock", TranslationValue = "最大库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.maxstock", TranslationValue = "最大库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.maxstock", TranslationValue = "最大库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.minstock
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.minstock", TranslationValue = "最小库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.minstock", TranslationValue = "最小库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.minstock", TranslationValue = "最小库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.minstock", TranslationValue = "最小库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.minstock", TranslationValue = "最小库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.minstock", TranslationValue = "最小库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.currentstock
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.currentstock", TranslationValue = "当前库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.currentstock", TranslationValue = "当前库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.currentstock", TranslationValue = "当前库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.currentstock", TranslationValue = "当前库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.currentstock", TranslationValue = "当前库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.currentstock", TranslationValue = "当前库存", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.productionlocation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.productionlocation", TranslationValue = "生产地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.productionlocation", TranslationValue = "生产地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.productionlocation", TranslationValue = "生产地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.productionlocation", TranslationValue = "生产地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.productionlocation", TranslationValue = "生产地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.productionlocation", TranslationValue = "生产地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.purchasinglocation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.purchasinglocation", TranslationValue = "采购地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.purchasinglocation", TranslationValue = "采购地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.purchasinglocation", TranslationValue = "采购地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.purchasinglocation", TranslationValue = "采购地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.purchasinglocation", TranslationValue = "采购地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.purchasinglocation", TranslationValue = "采购地点", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.inspectionrequired
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.inspectionrequired", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.inspectionrequired", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.inspectionrequired", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.inspectionrequired", TranslationValue = "是否检验", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.inspectionrequired", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.inspectionrequired", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.isbatch
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.isbatch", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.isbatch", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.isbatch", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.isbatch", TranslationValue = "是否批次管理", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.isbatch", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.isbatch", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.isexpiry
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.isexpiry", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.isexpiry", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.isexpiry", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.isexpiry", TranslationValue = "是否保质期管理", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.isexpiry", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.isexpiry", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.expirydays
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.expirydays", TranslationValue = "保质期天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.expirydays", TranslationValue = "保质期天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.expirydays", TranslationValue = "保质期天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.expirydays", TranslationValue = "保质期天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.expirydays", TranslationValue = "保质期天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.expirydays", TranslationValue = "保质期天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialstatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialstatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialstatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialstatus", TranslationValue = "物料状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialstatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialattributes
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialattributes", TranslationValue = "物料属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialattributes", TranslationValue = "物料属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialattributes", TranslationValue = "物料属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialattributes", TranslationValue = "物料属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialattributes", TranslationValue = "物料属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialattributes", TranslationValue = "物料属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.materialdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.materialdescription", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.materialdescription", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.materialdescription", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.materialdescription", TranslationValue = "物料描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.materialdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.materialdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.isendoflife
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.isendoflife", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.isendoflife", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.isendoflife", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.isendoflife", TranslationValue = "停产状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.isendoflife", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.isendoflife", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.plantmaterial.endoflifedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.plantmaterial.endoflifedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.plantmaterial.endoflifedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.plantmaterial.endoflifedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.plantmaterial.endoflifedate", TranslationValue = "停产日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.plantmaterial.endoflifedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.plantmaterial.endoflifedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
