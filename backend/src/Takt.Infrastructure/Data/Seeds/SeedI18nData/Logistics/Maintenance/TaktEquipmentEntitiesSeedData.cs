// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktEquipmentEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktEquipment 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Maintenance;

/// <summary>
/// TaktEquipment 实体翻译种子数据（自动生成，与 TaktEquipment.cs 属性一一对应）
/// </summary>
public class TaktEquipmentEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktEquipmentEntityTranslations();

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
    /// 获取所有 TaktEquipment 实体名称及字段翻译（自动生成，与 TaktEquipment.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktEquipmentEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.equipment（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment._self", TranslationValue = "工厂设备", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment._self", TranslationValue = "工厂设备", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment._self", TranslationValue = "工厂设备", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment._self", TranslationValue = "工厂设备", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment._self", TranslationValue = "工厂设备", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment._self", TranslationValue = "工厂设备", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.equipment.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.plantcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.plantcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.plantcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.equipment.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.code", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.code", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.code", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.code", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.code", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.code", TranslationValue = "设备编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.equipment.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.name", TranslationValue = "设备名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.equipment.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.type", TranslationValue = "设备类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.equipment.model
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.model", TranslationValue = "设备型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.model", TranslationValue = "设备型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.model", TranslationValue = "设备型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.model", TranslationValue = "设备型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.model", TranslationValue = "设备型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.model", TranslationValue = "设备型号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.equipment.specification
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.specification", TranslationValue = "设备规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.specification", TranslationValue = "设备规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.specification", TranslationValue = "设备规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.specification", TranslationValue = "设备规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.specification", TranslationValue = "设备规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.specification", TranslationValue = "设备规格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.equipment.brand
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.brand", TranslationValue = "设备品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.brand", TranslationValue = "设备品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.brand", TranslationValue = "设备品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.brand", TranslationValue = "设备品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.brand", TranslationValue = "设备品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.brand", TranslationValue = "设备品牌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.equipment.manufacturer
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.manufacturer", TranslationValue = "制造商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.equipment.dealerby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.dealerby", TranslationValue = "经销商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.dealerby", TranslationValue = "经销商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.dealerby", TranslationValue = "经销商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.dealerby", TranslationValue = "经销商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.dealerby", TranslationValue = "经销商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.dealerby", TranslationValue = "经销商", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.equipment.serialnumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.serialnumber", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.serialnumber", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.serialnumber", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.serialnumber", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.serialnumber", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.serialnumber", TranslationValue = "序列号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.equipment.workshopby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.workshopby", TranslationValue = "所属车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.workshopby", TranslationValue = "所属车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.workshopby", TranslationValue = "所属车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.workshopby", TranslationValue = "所属车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.workshopby", TranslationValue = "所属车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.workshopby", TranslationValue = "所属车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.equipment.productionlineby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.productionlineby", TranslationValue = "所属产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.productionlineby", TranslationValue = "所属产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.productionlineby", TranslationValue = "所属产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.productionlineby", TranslationValue = "所属产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.productionlineby", TranslationValue = "所属产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.productionlineby", TranslationValue = "所属产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.equipment.workstationby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.workstationby", TranslationValue = "所属工位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.workstationby", TranslationValue = "所属工位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.workstationby", TranslationValue = "所属工位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.workstationby", TranslationValue = "所属工位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.workstationby", TranslationValue = "所属工位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.workstationby", TranslationValue = "所属工位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.equipment.deptby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.deptby", TranslationValue = "所属部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.deptby", TranslationValue = "所属部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.deptby", TranslationValue = "所属部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.deptby", TranslationValue = "所属部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.deptby", TranslationValue = "所属部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.deptby", TranslationValue = "所属部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.equipment.location
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.location", TranslationValue = "设备位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.location", TranslationValue = "设备位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.location", TranslationValue = "设备位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.location", TranslationValue = "设备位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.location", TranslationValue = "设备位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.location", TranslationValue = "设备位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.equipment.responsibleuserby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.responsibleuserby", TranslationValue = "负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.responsibleuserby", TranslationValue = "负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.responsibleuserby", TranslationValue = "负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.responsibleuserby", TranslationValue = "负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.responsibleuserby", TranslationValue = "负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.responsibleuserby", TranslationValue = "负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.equipment.operatorby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.operatorby", TranslationValue = "操作人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.operatorby", TranslationValue = "操作人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.operatorby", TranslationValue = "操作人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.operatorby", TranslationValue = "操作人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.operatorby", TranslationValue = "操作人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.operatorby", TranslationValue = "操作人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.equipment.purchasedate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.purchasedate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.purchasedate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.purchasedate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.purchasedate", TranslationValue = "购买日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.purchasedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.purchasedate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.equipment.installationdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.installationdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.installationdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.installationdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.installationdate", TranslationValue = "安装日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.installationdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.installationdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.equipment.startdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.startdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.startdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.startdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.startdate", TranslationValue = "启用日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.startdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.startdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.equipment.warrantystartdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.warrantystartdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.warrantystartdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.warrantystartdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.warrantystartdate", TranslationValue = "保修开始日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.warrantystartdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.warrantystartdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.equipment.warrantyenddate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.warrantyenddate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.warrantyenddate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.warrantyenddate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.warrantyenddate", TranslationValue = "保修结束日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.warrantyenddate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.warrantyenddate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.equipment.originalvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.originalvalue", TranslationValue = "设备原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.originalvalue", TranslationValue = "设备原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.originalvalue", TranslationValue = "设备原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.originalvalue", TranslationValue = "设备原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.originalvalue", TranslationValue = "设备原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.originalvalue", TranslationValue = "设备原值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.equipment.technicalparameters
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.technicalparameters", TranslationValue = "设备技术参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.technicalparameters", TranslationValue = "设备技术参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.technicalparameters", TranslationValue = "设备技术参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.technicalparameters", TranslationValue = "设备技术参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.technicalparameters", TranslationValue = "设备技术参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.technicalparameters", TranslationValue = "设备技术参数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.equipment.images
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.images", TranslationValue = "设备图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.images", TranslationValue = "设备图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.images", TranslationValue = "设备图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.images", TranslationValue = "设备图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.images", TranslationValue = "设备图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.images", TranslationValue = "设备图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 27 },

            // entity.equipment.documents
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.documents", TranslationValue = "设备文档", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.documents", TranslationValue = "设备文档", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.documents", TranslationValue = "设备文档", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.documents", TranslationValue = "设备文档", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.documents", TranslationValue = "设备文档", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.documents", TranslationValue = "设备文档", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 28 },

            // entity.equipment.iscritical
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.iscritical", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.iscritical", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.iscritical", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.iscritical", TranslationValue = "是否关键设备", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.iscritical", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.iscritical", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 29 },

            // entity.equipment.warrantystatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.warrantystatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.warrantystatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.warrantystatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.warrantystatus", TranslationValue = "保修状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.warrantystatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.warrantystatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 30 },

            // entity.equipment.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.equipment.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.equipment.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.equipment.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.equipment.status", TranslationValue = "设备状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.equipment.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.equipment.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 31 },
        };
    }
}
