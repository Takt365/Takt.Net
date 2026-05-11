// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktKanbanSchemeEntitiesSeedData.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktKanbanScheme 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Statistics.Kanban;

/// <summary>
/// TaktKanbanScheme 实体翻译种子数据（自动生成，与 TaktKanbanScheme.cs 属性一一对应）
/// </summary>
public class TaktKanbanSchemeEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktKanbanSchemeEntityTranslations();

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
    /// 获取所有 TaktKanbanScheme 实体名称及字段翻译（自动生成，与 TaktKanbanScheme.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktKanbanSchemeEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.kanbanscheme（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme._self", TranslationValue = "看板方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme._self", TranslationValue = "看板方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme._self", TranslationValue = "看板方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme._self", TranslationValue = "看板方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme._self", TranslationValue = "看板方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme._self", TranslationValue = "看板方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.kanbanscheme.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.code", TranslationValue = "看板方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.code", TranslationValue = "看板方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.code", TranslationValue = "看板方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.code", TranslationValue = "看板方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.code", TranslationValue = "看板方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.code", TranslationValue = "看板方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.kanbanscheme.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.name", TranslationValue = "看板方案名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.kanbanscheme.kanbantype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.kanbantype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.kanbantype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.kanbantype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.kanbantype", TranslationValue = "看板类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.kanbantype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.kanbantype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.kanbanscheme.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.description", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.description", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.description", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.description", TranslationValue = "看板描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.description", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.description", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.kanbanscheme.datasourceconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.datasourceconfig", TranslationValue = "数据源配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.datasourceconfig", TranslationValue = "数据源配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.datasourceconfig", TranslationValue = "数据源配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.datasourceconfig", TranslationValue = "数据源配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.datasourceconfig", TranslationValue = "数据源配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.datasourceconfig", TranslationValue = "数据源配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.kanbanscheme.layoutconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.layoutconfig", TranslationValue = "布局配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.layoutconfig", TranslationValue = "布局配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.layoutconfig", TranslationValue = "布局配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.layoutconfig", TranslationValue = "布局配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.layoutconfig", TranslationValue = "布局配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.layoutconfig", TranslationValue = "布局配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.kanbanscheme.componentconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.componentconfig", TranslationValue = "组件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.componentconfig", TranslationValue = "组件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.componentconfig", TranslationValue = "组件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.componentconfig", TranslationValue = "组件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.componentconfig", TranslationValue = "组件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.componentconfig", TranslationValue = "组件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.kanbanscheme.refreshstrategy
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.refreshstrategy", TranslationValue = "刷新策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.refreshstrategy", TranslationValue = "刷新策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.refreshstrategy", TranslationValue = "刷新策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.refreshstrategy", TranslationValue = "刷新策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.refreshstrategy", TranslationValue = "刷新策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.refreshstrategy", TranslationValue = "刷新策略", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.kanbanscheme.refreshinterval
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.refreshinterval", TranslationValue = "刷新间隔", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.refreshinterval", TranslationValue = "刷新间隔", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.refreshinterval", TranslationValue = "刷新间隔", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.refreshinterval", TranslationValue = "刷新间隔", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.refreshinterval", TranslationValue = "刷新间隔", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.refreshinterval", TranslationValue = "刷新间隔", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.kanbanscheme.themestyle
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.themestyle", TranslationValue = "主题风格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.themestyle", TranslationValue = "主题风格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.themestyle", TranslationValue = "主题风格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.themestyle", TranslationValue = "主题风格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.themestyle", TranslationValue = "主题风格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.themestyle", TranslationValue = "主题风格", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.kanbanscheme.isfullscreen
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.isfullscreen", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.isfullscreen", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.isfullscreen", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.isfullscreen", TranslationValue = "是否全屏显示", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.isfullscreen", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.isfullscreen", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.kanbanscheme.enablealert
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.enablealert", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.enablealert", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.enablealert", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.enablealert", TranslationValue = "是否启用告警", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.enablealert", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.enablealert", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.kanbanscheme.alertconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.alertconfig", TranslationValue = "告警配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.alertconfig", TranslationValue = "告警配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.alertconfig", TranslationValue = "告警配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.alertconfig", TranslationValue = "告警配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.alertconfig", TranslationValue = "告警配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.alertconfig", TranslationValue = "告警配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.kanbanscheme.filterconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.filterconfig", TranslationValue = "过滤条件配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.kanbanscheme.sortconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.sortconfig", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.sortconfig", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.sortconfig", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.sortconfig", TranslationValue = "排序配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.sortconfig", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.sortconfig", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.kanbanscheme.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.plantcode", TranslationValue = "应用工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.plantcode", TranslationValue = "应用工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.plantcode", TranslationValue = "应用工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.plantcode", TranslationValue = "应用工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.plantcode", TranslationValue = "应用工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.plantcode", TranslationValue = "应用工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.kanbanscheme.workshopcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.workshopcode", TranslationValue = "应用车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.workshopcode", TranslationValue = "应用车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.workshopcode", TranslationValue = "应用车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.workshopcode", TranslationValue = "应用车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.workshopcode", TranslationValue = "应用车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.workshopcode", TranslationValue = "应用车间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.kanbanscheme.linecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.linecode", TranslationValue = "应用生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.linecode", TranslationValue = "应用生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.linecode", TranslationValue = "应用生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.linecode", TranslationValue = "应用生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.linecode", TranslationValue = "应用生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.linecode", TranslationValue = "应用生产线", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.kanbanscheme.displayorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.displayorder", TranslationValue = "显示顺序", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.displayorder", TranslationValue = "显示顺序", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.displayorder", TranslationValue = "显示顺序", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.displayorder", TranslationValue = "显示顺序", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.displayorder", TranslationValue = "显示顺序", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.displayorder", TranslationValue = "显示顺序", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.kanbanscheme.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.status", TranslationValue = "状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.kanbanscheme.ispublic
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.ispublic", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.ispublic", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.ispublic", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.ispublic", TranslationValue = "是否公开", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.ispublic", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.ispublic", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.kanbanscheme.creatorids
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.creatorids", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.creatorids", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.creatorids", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.creatorids", TranslationValue = "创建人ID列表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.creatorids", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.creatorids", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.kanbanscheme.accessconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.kanbanscheme.accessconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.kanbanscheme.accessconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.kanbanscheme.accessconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.kanbanscheme.accessconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.kanbanscheme.accessconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.kanbanscheme.accessconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
        };
    }
}
