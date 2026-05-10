// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktTrainingCourseEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktTrainingCourse 实体字段翻译种子数据（自动生成）
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
/// TaktTrainingCourse 实体翻译种子数据（自动生成，与 TaktTrainingCourse.cs 属性一一对应）
/// </summary>
public class TaktTrainingCourseEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktTrainingCourseEntityTranslations();

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
    /// 获取所有 TaktTrainingCourse 实体名称及字段翻译（自动生成，与 TaktTrainingCourse.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktTrainingCourseEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.trainingcourse（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse._self", TranslationValue = "培训课程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse._self", TranslationValue = "培训课程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse._self", TranslationValue = "培训课程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse._self", TranslationValue = "培训课程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse._self", TranslationValue = "培训课程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse._self", TranslationValue = "培训课程", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.trainingcourse.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.code", TranslationValue = "课程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.code", TranslationValue = "课程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.code", TranslationValue = "课程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.code", TranslationValue = "课程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.code", TranslationValue = "课程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.code", TranslationValue = "课程编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.trainingcourse.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.name", TranslationValue = "课程名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.trainingcourse.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.type", TranslationValue = "课程类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.trainingcourse.level
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.level", TranslationValue = "课程级别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.level", TranslationValue = "课程级别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.level", TranslationValue = "课程级别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.level", TranslationValue = "课程级别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.level", TranslationValue = "课程级别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.level", TranslationValue = "课程级别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.trainingcourse.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.description", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.description", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.description", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.description", TranslationValue = "课程描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.description", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.description", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.trainingcourse.objectives
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.objectives", TranslationValue = "课程目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.objectives", TranslationValue = "课程目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.objectives", TranslationValue = "课程目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.objectives", TranslationValue = "课程目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.objectives", TranslationValue = "课程目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.objectives", TranslationValue = "课程目标", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.trainingcourse.applicabledepartment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.applicabledepartment", TranslationValue = "适用部门", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.trainingcourse.applicableposition
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.applicableposition", TranslationValue = "适用岗位", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.trainingcourse.traininghours
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.traininghours", TranslationValue = "培训时长", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.trainingcourse.trainingdays
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.trainingdays", TranslationValue = "培训天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.trainingdays", TranslationValue = "培训天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.trainingdays", TranslationValue = "培训天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.trainingdays", TranslationValue = "培训天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.trainingdays", TranslationValue = "培训天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.trainingdays", TranslationValue = "培训天数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.trainingcourse.maininstructor
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.maininstructor", TranslationValue = "主讲讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.maininstructor", TranslationValue = "主讲讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.maininstructor", TranslationValue = "主讲讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.maininstructor", TranslationValue = "主讲讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.maininstructor", TranslationValue = "主讲讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.maininstructor", TranslationValue = "主讲讲师", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.trainingcourse.trainingmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.trainingmethod", TranslationValue = "培训方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.trainingmethod", TranslationValue = "培训方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.trainingmethod", TranslationValue = "培训方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.trainingmethod", TranslationValue = "培训方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.trainingmethod", TranslationValue = "培训方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.trainingmethod", TranslationValue = "培训方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.trainingcourse.assessmentmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.assessmentmethod", TranslationValue = "考核方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.assessmentmethod", TranslationValue = "考核方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.assessmentmethod", TranslationValue = "考核方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.assessmentmethod", TranslationValue = "考核方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.assessmentmethod", TranslationValue = "考核方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.assessmentmethod", TranslationValue = "考核方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.trainingcourse.passingscore
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.passingscore", TranslationValue = "及格分数线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.passingscore", TranslationValue = "及格分数线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.passingscore", TranslationValue = "及格分数线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.passingscore", TranslationValue = "及格分数线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.passingscore", TranslationValue = "及格分数线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.passingscore", TranslationValue = "及格分数线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.trainingcourse.iscertification
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.iscertification", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.iscertification", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.iscertification", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.iscertification", TranslationValue = "是否颁发证书", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.iscertification", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.iscertification", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.trainingcourse.outline
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.outline", TranslationValue = "课程大纲", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.outline", TranslationValue = "课程大纲", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.outline", TranslationValue = "课程大纲", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.outline", TranslationValue = "课程大纲", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.outline", TranslationValue = "课程大纲", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.outline", TranslationValue = "课程大纲", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.trainingcourse.materiallist
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.materiallist", TranslationValue = "培训材料清单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.materiallist", TranslationValue = "培训材料清单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.materiallist", TranslationValue = "培训材料清单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.materiallist", TranslationValue = "培训材料清单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.materiallist", TranslationValue = "培训材料清单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.materiallist", TranslationValue = "培训材料清单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.trainingcourse.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.trainingcourse.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.trainingcourse.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.trainingcourse.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.trainingcourse.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.trainingcourse.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.trainingcourse.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.trainingcourse.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
        };
    }
}
