// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCustomerComplaintHandlingEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktCustomerComplaintHandling 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality.Complaint;

/// <summary>
/// TaktCustomerComplaintHandling 实体翻译种子数据（自动生成，与 TaktCustomerComplaintHandling.cs 属性一一对应）
/// </summary>
public class TaktCustomerComplaintHandlingEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktCustomerComplaintHandlingEntityTranslations();

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
    /// 获取所有 TaktCustomerComplaintHandling 实体名称及字段翻译（自动生成，与 TaktCustomerComplaintHandling.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktCustomerComplaintHandlingEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.customercomplainthandling（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling._self", TranslationValue = "客诉处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling._self", TranslationValue = "客诉处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling._self", TranslationValue = "客诉处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling._self", TranslationValue = "客诉处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling._self", TranslationValue = "客诉处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling._self", TranslationValue = "客诉处理记录", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.customercomplainthandling.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.code", TranslationValue = "客诉处理记录编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.code", TranslationValue = "客诉处理记录编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.code", TranslationValue = "客诉处理记录编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.code", TranslationValue = "客诉处理记录编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.code", TranslationValue = "客诉处理记录编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.code", TranslationValue = "客诉处理记录编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.customercomplainthandling.complaintid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.complaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.complaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.complaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.complaintid", TranslationValue = "客诉ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.complaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.complaintid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.customercomplainthandling.complaintno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.complaintno", TranslationValue = "客诉单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.complaintno", TranslationValue = "客诉单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.complaintno", TranslationValue = "客诉单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.complaintno", TranslationValue = "客诉单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.complaintno", TranslationValue = "客诉单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.complaintno", TranslationValue = "客诉单号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.customercomplainthandling.complaintitemid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.complaintitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.complaintitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.complaintitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.complaintitemid", TranslationValue = "客诉明细ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.complaintitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.complaintitemid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.customercomplainthandling.stage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.stage", TranslationValue = "处理阶段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.stage", TranslationValue = "处理阶段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.stage", TranslationValue = "处理阶段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.stage", TranslationValue = "处理阶段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.stage", TranslationValue = "处理阶段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.stage", TranslationValue = "处理阶段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.customercomplainthandling.method
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.method", TranslationValue = "处理方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.customercomplainthandling.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.description", TranslationValue = "处理说明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.customercomplainthandling.causeanalysis
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.causeanalysis", TranslationValue = "原因分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.causeanalysis", TranslationValue = "原因分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.causeanalysis", TranslationValue = "原因分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.causeanalysis", TranslationValue = "原因分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.causeanalysis", TranslationValue = "原因分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.causeanalysis", TranslationValue = "原因分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.customercomplainthandling.correctiveaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.correctiveaction", TranslationValue = "纠正措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.customercomplainthandling.preventiveaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.preventiveaction", TranslationValue = "预防措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.preventiveaction", TranslationValue = "预防措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.preventiveaction", TranslationValue = "预防措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.preventiveaction", TranslationValue = "预防措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.preventiveaction", TranslationValue = "预防措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.preventiveaction", TranslationValue = "预防措施", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.customercomplainthandling.responsibledept
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.responsibledept", TranslationValue = "责任部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.customercomplainthandling.responsibleby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.responsibleby", TranslationValue = "责任人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.customercomplainthandling.handlerby
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.handlerby", TranslationValue = "处理人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.customercomplainthandling.time
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.time", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.time", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.time", TranslationValue = "处理时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.time", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.customercomplainthandling.plannedcompletiondate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.plannedcompletiondate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.plannedcompletiondate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.plannedcompletiondate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.plannedcompletiondate", TranslationValue = "计划完成日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.plannedcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.plannedcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.customercomplainthandling.actualcompletiondate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.actualcompletiondate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.actualcompletiondate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.actualcompletiondate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.actualcompletiondate", TranslationValue = "实际完成日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.actualcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.actualcompletiondate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.customercomplainthandling.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.status", TranslationValue = "处理状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.customercomplainthandling.cost
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.cost", TranslationValue = "处理成本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.cost", TranslationValue = "处理成本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.cost", TranslationValue = "处理成本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.cost", TranslationValue = "处理成本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.cost", TranslationValue = "处理成本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.cost", TranslationValue = "处理成本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.customercomplainthandling.customerfeedback
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.customerfeedback", TranslationValue = "客户反馈", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.customercomplainthandling.customersatisfaction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.customersatisfaction", TranslationValue = "客户满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.customersatisfaction", TranslationValue = "客户满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.customersatisfaction", TranslationValue = "客户满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.customersatisfaction", TranslationValue = "客户满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.customersatisfaction", TranslationValue = "客户满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.customersatisfaction", TranslationValue = "客户满意度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.customercomplainthandling.attachmentpaths
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.customercomplainthandling.attachmentpaths", TranslationValue = "附件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.customercomplainthandling.attachmentpaths", TranslationValue = "附件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.customercomplainthandling.attachmentpaths", TranslationValue = "附件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.customercomplainthandling.attachmentpaths", TranslationValue = "附件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.customercomplainthandling.attachmentpaths", TranslationValue = "附件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.customercomplainthandling.attachmentpaths", TranslationValue = "附件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
        };
    }
}
