// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktIpqcDefectHandlingEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktIpqcDefectHandling 实体字段翻译种子数据（自动生成）
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
/// TaktIpqcDefectHandling 实体翻译种子数据（自动生成，与 TaktIpqcDefectHandling.cs 属性一一对应）
/// </summary>
public class TaktIpqcDefectHandlingEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktIpqcDefectHandlingEntityTranslations();

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
    /// 获取所有 TaktIpqcDefectHandling 实体名称及字段翻译（自动生成，与 TaktIpqcDefectHandling.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktIpqcDefectHandlingEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.ipqcdefecthandling（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling._self", TranslationValue = "制程检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling._self", TranslationValue = "制程检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling._self", TranslationValue = "制程检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling._self", TranslationValue = "制程检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling._self", TranslationValue = "制程检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling._self", TranslationValue = "制程检验不良处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.ipqcdefecthandling.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.code", TranslationValue = "IPQC不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.code", TranslationValue = "IPQC不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.code", TranslationValue = "IPQC不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.code", TranslationValue = "IPQC不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.code", TranslationValue = "IPQC不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.code", TranslationValue = "IPQC不良处理编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.ipqcdefecthandling.ipqcorderitemid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.ipqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.ipqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.ipqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.ipqcorderitemid", TranslationValue = "IPQC检验单明细ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.ipqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.ipqcorderitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.ipqcdefecthandling.ipqcordercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.ipqcordercode", TranslationValue = "IPQC检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.ipqcordercode", TranslationValue = "IPQC检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.ipqcordercode", TranslationValue = "IPQC检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.ipqcordercode", TranslationValue = "IPQC检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.ipqcordercode", TranslationValue = "IPQC检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.ipqcordercode", TranslationValue = "IPQC检验单编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.ipqcdefecthandling.linenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.linenumber", TranslationValue = "检验单行号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.ipqcdefecthandling.defecttype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.defecttype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.defecttype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.defecttype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.defecttype", TranslationValue = "不良类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.defecttype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.defecttype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.ipqcdefecthandling.defectcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.ipqcdefecthandling.defectdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.defectdescription", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.defectdescription", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.defectdescription", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.defectdescription", TranslationValue = "不良现象描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.defectdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.defectdescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.ipqcdefecthandling.defectquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.defectquantity", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.defectquantity", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.defectquantity", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.defectquantity", TranslationValue = "不良数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.defectquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.defectquantity", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.ipqcdefecthandling.method
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.ipqcdefecthandling.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.ipqcdefecthandling.responsibledept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.ipqcdefecthandling.responsibleby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.ipqcdefecthandling.handlerby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.ipqcdefecthandling.time
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.time", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.time", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.time", TranslationValue = "处理时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.ipqcdefecthandling.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.status", TranslationValue = "处理状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.ipqcdefecthandling.correctiveaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.ipqcdefecthandling.defectimages
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.ipqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.ipqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.ipqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.ipqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.ipqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.ipqcdefecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
        };
    }
}
