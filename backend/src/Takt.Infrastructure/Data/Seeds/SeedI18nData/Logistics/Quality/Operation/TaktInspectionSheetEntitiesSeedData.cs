// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktInspectionSheetEntitiesSeedData.cs
// 创建时间：2026-05-07
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktInspectionSheet 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality;

/// <summary>
/// TaktInspectionSheet 实体翻译种子数据（自动生成，与 TaktInspectionSheet.cs 属性一一对应）
/// </summary>
public class TaktInspectionSheetEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktInspectionSheetEntityTranslations();

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
    /// 获取所有 TaktInspectionSheet 实体名称及字段翻译（自动生成，与 TaktInspectionSheet.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktInspectionSheetEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.inspectionsheet（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet._self", TranslationValue = "检验单", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet._self", TranslationValue = "检验单", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet._self", TranslationValue = "检验单", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet._self", TranslationValue = "检验单", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet._self", TranslationValue = "检验单", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet._self", TranslationValue = "检验单", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.inspectionsheet.sheetcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.sheetcode", TranslationValue = "检验单编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.sheetcode", TranslationValue = "检验单编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.sheetcode", TranslationValue = "检验单编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.sheetcode", TranslationValue = "检验单编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.sheetcode", TranslationValue = "检验单编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.sheetcode", TranslationValue = "检验单编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.inspectionsheet.inspectiontype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.inspectiontype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.inspectiontype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.inspectiontype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.inspectiontype", TranslationValue = "检验类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.inspectiontype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.inspectiontype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.inspectionsheet.sourcetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.sourcetype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.sourcetype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.sourcetype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.sourcetype", TranslationValue = "来源类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.sourcetype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.sourcetype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.inspectionsheet.sourceid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.sourceid", TranslationValue = "来源ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.sourceid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.inspectionsheet.sourcecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.sourcecode", TranslationValue = "来源单号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.inspectionsheet.planid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.planid", TranslationValue = "检验计划ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.inspectionsheet.standardid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.standardid", TranslationValue = "检验标准ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.standardid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.inspectionsheet.materialcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.materialcode", TranslationValue = "物料编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.inspectionsheet.materialname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.materialname", TranslationValue = "Name", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.materialname", TranslationValue = "名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.materialname", TranslationValue = "이름", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.materialname", TranslationValue = "物料名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.materialname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.materialname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.inspectionsheet.batchno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.batchno", TranslationValue = "批次号", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.inspectionsheet.suppliercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.suppliercode", TranslationValue = "供应商编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.suppliercode", TranslationValue = "供应商编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.suppliercode", TranslationValue = "供应商编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.suppliercode", TranslationValue = "供应商编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.suppliercode", TranslationValue = "供应商编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.suppliercode", TranslationValue = "供应商编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.inspectionsheet.suppliername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.suppliername", TranslationValue = "Name", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.suppliername", TranslationValue = "名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.suppliername", TranslationValue = "이름", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.suppliername", TranslationValue = "供应商名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.suppliername", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.suppliername", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.inspectionsheet.incomingquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.incomingquantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.incomingquantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.incomingquantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.incomingquantity", TranslationValue = "来料数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.incomingquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.incomingquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.inspectionsheet.processcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.processcode", TranslationValue = "工序编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.processcode", TranslationValue = "工序编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.processcode", TranslationValue = "工序编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.processcode", TranslationValue = "工序编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.processcode", TranslationValue = "工序编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.processcode", TranslationValue = "工序编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.inspectionsheet.processname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.processname", TranslationValue = "Name", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.processname", TranslationValue = "名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.processname", TranslationValue = "이름", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.processname", TranslationValue = "工序名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.processname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.processname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.inspectionsheet.samplingschemeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.samplingschemeid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.samplingschemeid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.samplingschemeid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.samplingschemeid", TranslationValue = "抽样方案ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.samplingschemeid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.samplingschemeid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.inspectionsheet.samplequantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.samplequantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.samplequantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.samplequantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.samplequantity", TranslationValue = "抽样数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.samplequantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.samplequantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.inspectionsheet.qualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.qualifiedquantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.qualifiedquantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.qualifiedquantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.qualifiedquantity", TranslationValue = "合格数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.qualifiedquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.qualifiedquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.inspectionsheet.unqualifiedquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.unqualifiedquantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.unqualifiedquantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.unqualifiedquantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.unqualifiedquantity", TranslationValue = "不合格数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.unqualifiedquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.inspectionsheet.inspectionconclusion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.inspectionconclusion", TranslationValue = "检验结论", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.inspectionsheet.inspectorid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.inspectorid", TranslationValue = "检验员ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.inspectionsheet.inspectorname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.inspectionsheet.inspectiontime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.inspectiontime", TranslationValue = "Time", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.inspectiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.inspectiontime", TranslationValue = "시간", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.inspectiontime", TranslationValue = "检验时间", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.inspectiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.inspectiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.inspectionsheet.judgeuserid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.judgeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.judgeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.judgeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.judgeuserid", TranslationValue = "判定人ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.judgeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.judgeuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 26 },

            // entity.inspectionsheet.judgeusername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.judgeusername", TranslationValue = "判定人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.judgeusername", TranslationValue = "判定人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.judgeusername", TranslationValue = "判定人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.judgeusername", TranslationValue = "判定人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.judgeusername", TranslationValue = "判定人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 27 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.judgeusername", TranslationValue = "判定人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 27 },

            // entity.inspectionsheet.judgetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.judgetime", TranslationValue = "Time", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.judgetime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.judgetime", TranslationValue = "시간", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.judgetime", TranslationValue = "判定时间", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.judgetime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 28 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.judgetime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 28 },

            // entity.inspectionsheet.inspectionremark
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.inspectionremark", TranslationValue = "Remark", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.inspectionremark", TranslationValue = "備考", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.inspectionremark", TranslationValue = "비고", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.inspectionremark", TranslationValue = "检验备注", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.inspectionremark", TranslationValue = "備註", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 29 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.inspectionremark", TranslationValue = "備註", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 29 },

            // entity.inspectionsheet.sheetstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionsheet.sheetstatus", TranslationValue = "Status", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionsheet.sheetstatus", TranslationValue = "状態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionsheet.sheetstatus", TranslationValue = "상태", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionsheet.sheetstatus", TranslationValue = "检验单状态", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionsheet.sheetstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 30 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionsheet.sheetstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 30 },
        };
    }
}
