// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFqcDefectHandlingEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktFqcDefectHandling 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Operation;

/// <summary>
/// TaktFqcDefectHandling 实体翻译种子数据（自动生成，与 TaktFqcDefectHandling.cs 属性一一对应）
/// </summary>
public class TaktFqcDefectHandlingEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktFqcDefectHandlingEntityTranslations();

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
    /// 获取所有 TaktFqcDefectHandling 实体名称及字段翻译（自动生成，与 TaktFqcDefectHandling.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktFqcDefectHandlingEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.fqcdefecthandling（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling._self", TranslationValue = "出货检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling._self", TranslationValue = "出货检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling._self", TranslationValue = "出货检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling._self", TranslationValue = "出货检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling._self", TranslationValue = "出货检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling._self", TranslationValue = "出货检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.fqcdefecthandling.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.code", TranslationValue = "不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.code", TranslationValue = "不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.code", TranslationValue = "不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.code", TranslationValue = "不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.code", TranslationValue = "不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.code", TranslationValue = "不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.fqcdefecthandling.fqcorderitemid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.fqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.fqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.fqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.fqcorderitemid", TranslationValue = "FQC检验单明细ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.fqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.fqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.fqcdefecthandling.ordercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.ordercode", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.ordercode", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.ordercode", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.ordercode", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.ordercode", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.ordercode", TranslationValue = "检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.fqcdefecthandling.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.fqcdefecthandling.defecttype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.defecttype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.defecttype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.defecttype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.defecttype", TranslationValue = "不良类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.defecttype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.defecttype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.fqcdefecthandling.defectcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.fqcdefecthandling.defectdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.defectdescription", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.defectdescription", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.defectdescription", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.defectdescription", TranslationValue = "不良现象描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.defectdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.defectdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.fqcdefecthandling.defectquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.defectquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.defectquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.defectquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.defectquantity", TranslationValue = "不良数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.defectquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.defectquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.fqcdefecthandling.method
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.fqcdefecthandling.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.fqcdefecthandling.responsibledept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.fqcdefecthandling.responsibleby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.fqcdefecthandling.handlerby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.fqcdefecthandling.time
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.time", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.time", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.time", TranslationValue = "处理时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.fqcdefecthandling.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.status", TranslationValue = "处理状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.fqcdefecthandling.correctiveaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.fqcdefecthandling.defectimages
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.fqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
        };
    }
}
