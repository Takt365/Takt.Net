// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSkillAssessmentEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktSkillAssessment 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.TrainingDevelopment;

/// <summary>
/// TaktSkillAssessment 实体翻译种子数据（自动生成，与 TaktSkillAssessment.cs 属性一一对应）
/// </summary>
public class TaktSkillAssessmentEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktSkillAssessmentEntityTranslations();

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
    /// 获取所有 TaktSkillAssessment 实体名称及字段翻译（自动生成，与 TaktSkillAssessment.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktSkillAssessmentEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.skillassessment（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment._self", TranslationValue = "技能评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment._self", TranslationValue = "技能评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment._self", TranslationValue = "技能评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment._self", TranslationValue = "技能评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment._self", TranslationValue = "技能评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment._self", TranslationValue = "技能评估", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.skillassessment.employeeid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.employeeid", TranslationValue = "员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.employeeid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.skillassessment.skillcategory
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.skillcategory", TranslationValue = "技能类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.skillcategory", TranslationValue = "技能类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.skillcategory", TranslationValue = "技能类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.skillcategory", TranslationValue = "技能类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.skillcategory", TranslationValue = "技能类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.skillcategory", TranslationValue = "技能类别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.skillassessment.skillname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.skillname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.skillname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.skillname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.skillname", TranslationValue = "技能名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.skillname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.skillname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.skillassessment.skilldescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.skilldescription", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.skilldescription", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.skilldescription", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.skilldescription", TranslationValue = "技能描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.skilldescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.skilldescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.skillassessment.date
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.date", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.date", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.date", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.date", TranslationValue = "评估日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.date", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.skillassessment.method
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.method", TranslationValue = "评估方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.method", TranslationValue = "评估方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.method", TranslationValue = "评估方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.method", TranslationValue = "评估方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.method", TranslationValue = "评估方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.method", TranslationValue = "评估方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.skillassessment.score
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.score", TranslationValue = "评估得分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.score", TranslationValue = "评估得分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.score", TranslationValue = "评估得分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.score", TranslationValue = "评估得分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.score", TranslationValue = "评估得分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.score", TranslationValue = "评估得分", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.skillassessment.skilllevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.skilllevel", TranslationValue = "技能等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.skilllevel", TranslationValue = "技能等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.skilllevel", TranslationValue = "技能等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.skilllevel", TranslationValue = "技能等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.skilllevel", TranslationValue = "技能等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.skilllevel", TranslationValue = "技能等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.skillassessment.previouslevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.previouslevel", TranslationValue = "评估前等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.previouslevel", TranslationValue = "评估前等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.previouslevel", TranslationValue = "评估前等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.previouslevel", TranslationValue = "评估前等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.previouslevel", TranslationValue = "评估前等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.previouslevel", TranslationValue = "评估前等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.skillassessment.newlevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.newlevel", TranslationValue = "评估后等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.newlevel", TranslationValue = "评估后等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.newlevel", TranslationValue = "评估后等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.newlevel", TranslationValue = "评估后等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.newlevel", TranslationValue = "评估后等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.newlevel", TranslationValue = "评估后等级", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.skillassessment.ispassed
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.ispassed", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.ispassed", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.ispassed", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.ispassed", TranslationValue = "是否通过", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.ispassed", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.ispassed", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.skillassessment.certificateno
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.certificateno", TranslationValue = "Number", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.certificateno", TranslationValue = "番号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.certificateno", TranslationValue = "번호", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.certificateno", TranslationValue = "证书编号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.certificateno", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.certificateno", TranslationValue = "編號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.skillassessment.certificateexpirydate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.certificateexpirydate", TranslationValue = "证书有效期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.certificateexpirydate", TranslationValue = "证书有效期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.certificateexpirydate", TranslationValue = "证书有效期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.certificateexpirydate", TranslationValue = "证书有效期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.certificateexpirydate", TranslationValue = "证书有效期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.certificateexpirydate", TranslationValue = "证书有效期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.skillassessment.assessorid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.assessorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.assessorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.assessorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.assessorid", TranslationValue = "评估人ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.assessorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.assessorid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.skillassessment.comments
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.comments", TranslationValue = "评估评语", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.comments", TranslationValue = "评估评语", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.comments", TranslationValue = "评估评语", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.comments", TranslationValue = "评估评语", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.comments", TranslationValue = "评估评语", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.comments", TranslationValue = "评估评语", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.skillassessment.strengthsanalysis
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.strengthsanalysis", TranslationValue = "优势分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.strengthsanalysis", TranslationValue = "优势分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.strengthsanalysis", TranslationValue = "优势分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.strengthsanalysis", TranslationValue = "优势分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.strengthsanalysis", TranslationValue = "优势分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.strengthsanalysis", TranslationValue = "优势分析", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.skillassessment.improvementsuggestions
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.improvementsuggestions", TranslationValue = "改进建议", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.skillassessment.nextassessmentdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.nextassessmentdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.nextassessmentdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.nextassessmentdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.nextassessmentdate", TranslationValue = "下次评估日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.nextassessmentdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.nextassessmentdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.skillassessment.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.skillassessment.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.skillassessment.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.skillassessment.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.skillassessment.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.skillassessment.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.skillassessment.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
        };
    }
}
