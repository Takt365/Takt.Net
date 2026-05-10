// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDefectHandlingEntitiesSeedData.cs
// 创建时间：2026-05-07
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktDefectHandling 实体字段翻译种子数据（自动生成）
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
/// TaktDefectHandling 实体翻译种子数据（自动生成，与 TaktDefectHandling.cs 属性一一对应）
/// </summary>
public class TaktDefectHandlingEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktDefectHandlingEntityTranslations();

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
    /// 获取所有 TaktDefectHandling 实体名称及字段翻译（自动生成，与 TaktDefectHandling.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktDefectHandlingEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.defecthandling（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling._self", TranslationValue = "不良处理记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling._self", TranslationValue = "不良处理记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling._self", TranslationValue = "不良处理记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling._self", TranslationValue = "不良处理记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling._self", TranslationValue = "不良处理记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling._self", TranslationValue = "不良处理记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.defecthandling.handlingcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.handlingcode", TranslationValue = "不良处理编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.handlingcode", TranslationValue = "不良处理编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.handlingcode", TranslationValue = "不良处理编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.handlingcode", TranslationValue = "不良处理编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.handlingcode", TranslationValue = "不良处理编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.handlingcode", TranslationValue = "不良处理编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.defecthandling.executionid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.executionid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.executionid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.executionid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.executionid", TranslationValue = "检验执行ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.executionid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.executionid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.defecthandling.sheetitemid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.sheetitemid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.sheetitemid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.sheetitemid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.sheetitemid", TranslationValue = "检验单明细ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.sheetitemid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.sheetitemid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.defecthandling.defecttype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.defecttype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.defecttype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.defecttype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.defecttype", TranslationValue = "不良类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.defecttype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.defecttype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.defecthandling.defectcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.defectcode", TranslationValue = "不良现象编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.defecthandling.defectdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.defectdescription", TranslationValue = "Description", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.defectdescription", TranslationValue = "説明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.defectdescription", TranslationValue = "설명", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.defectdescription", TranslationValue = "不良现象描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.defectdescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.defectdescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.defecthandling.defectquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.defectquantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.defectquantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.defectquantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.defectquantity", TranslationValue = "不良数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.defectquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.defectquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.defecthandling.handlingmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.handlingmethod", TranslationValue = "处理方式", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.handlingmethod", TranslationValue = "处理方式", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.handlingmethod", TranslationValue = "处理方式", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.handlingmethod", TranslationValue = "处理方式", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.handlingmethod", TranslationValue = "处理方式", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.handlingmethod", TranslationValue = "处理方式", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.defecthandling.handlingdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.handlingdescription", TranslationValue = "处理说明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.handlingdescription", TranslationValue = "处理说明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.handlingdescription", TranslationValue = "处理说明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.handlingdescription", TranslationValue = "处理说明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.handlingdescription", TranslationValue = "处理说明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.handlingdescription", TranslationValue = "处理说明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.defecthandling.responsibledept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.defecthandling.responsibleuserid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.responsibleuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.responsibleuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.responsibleuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.responsibleuserid", TranslationValue = "责任人ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.responsibleuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.responsibleuserid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.defecthandling.responsibleusername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.responsibleusername", TranslationValue = "责任人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.responsibleusername", TranslationValue = "责任人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.responsibleusername", TranslationValue = "责任人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.responsibleusername", TranslationValue = "责任人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.responsibleusername", TranslationValue = "责任人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.responsibleusername", TranslationValue = "责任人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.defecthandling.handlerid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.handlerid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.handlerid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.handlerid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.handlerid", TranslationValue = "处理人ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.handlerid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.handlerid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.defecthandling.handlername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.handlername", TranslationValue = "处理人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.handlername", TranslationValue = "处理人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.handlername", TranslationValue = "处理人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.handlername", TranslationValue = "处理人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.handlername", TranslationValue = "处理人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.handlername", TranslationValue = "处理人姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.defecthandling.handlingtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.handlingtime", TranslationValue = "Time", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.handlingtime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.handlingtime", TranslationValue = "시간", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.handlingtime", TranslationValue = "处理时间", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.handlingtime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.handlingtime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.defecthandling.handlingstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.handlingstatus", TranslationValue = "Status", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.handlingstatus", TranslationValue = "状態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.handlingstatus", TranslationValue = "상태", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.handlingstatus", TranslationValue = "处理状态", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.handlingstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.handlingstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.defecthandling.correctiveaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.defecthandling.defectimages
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.defecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.defecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.defecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.defecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.defecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.defecthandling.defectimages", TranslationValue = "不良图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
        };
    }
}
