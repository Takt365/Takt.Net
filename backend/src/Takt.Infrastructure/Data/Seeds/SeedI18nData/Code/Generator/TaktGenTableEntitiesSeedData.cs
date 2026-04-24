// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktGenTableEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktGenTable 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Code.Generator;

/// <summary>
/// TaktGenTable 实体翻译种子数据（自动生成，与 TaktGenTable.cs 属性一一对应）
/// </summary>
public class TaktGenTableEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktGenTableEntityTranslations();

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
    /// 获取所有 TaktGenTable 实体名称及字段翻译（自动生成，与 TaktGenTable.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktGenTableEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.gentable（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable._self", TranslationValue = "代码生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable._self", TranslationValue = "代码生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable._self", TranslationValue = "代码生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable._self", TranslationValue = "代码生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable._self", TranslationValue = "代码生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable._self", TranslationValue = "代码生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.datasource
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.datasource", TranslationValue = "数据源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.datasource", TranslationValue = "数据源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.datasource", TranslationValue = "数据源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.datasource", TranslationValue = "数据源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.datasource", TranslationValue = "数据源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.datasource", TranslationValue = "数据源", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.tablename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.tablename", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.tablename", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.tablename", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.tablename", TranslationValue = "表名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.tablename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.tablename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.tablecomment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "表描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.subtablename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.subtablename", TranslationValue = "关联父表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.subtablename", TranslationValue = "关联父表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.subtablename", TranslationValue = "关联父表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.subtablename", TranslationValue = "关联父表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.subtablename", TranslationValue = "关联父表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.subtablename", TranslationValue = "关联父表", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.subtablefkname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "关联外键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "关联外键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "关联外键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "关联外键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "关联外键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "关联外键", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.treecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.treecode", TranslationValue = "树编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.treecode", TranslationValue = "树编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.treecode", TranslationValue = "树编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.treecode", TranslationValue = "树编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.treecode", TranslationValue = "树编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.treecode", TranslationValue = "树编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.treeparentcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "树父编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "树父编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "树父编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "树父编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "树父编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "树父编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.treename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.treename", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.treename", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.treename", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.treename", TranslationValue = "树名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.treename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.treename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.indatabase
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.indatabase", TranslationValue = "库表标识", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.indatabase", TranslationValue = "库表标识", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.indatabase", TranslationValue = "库表标识", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.indatabase", TranslationValue = "库表标识", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.indatabase", TranslationValue = "库表标识", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.indatabase", TranslationValue = "库表标识", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.gentemplatecategory
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.gentemplatecategory", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.gentemplatecategory", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.gentemplatecategory", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.gentemplatecategory", TranslationValue = "生成模板类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.gentemplatecategory", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.gentemplatecategory", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.genmodulename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模块名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模块名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模块名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模块名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模块名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模块名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.genbusinessname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "业务名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "业务名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "业务名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "业务名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "业务名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "业务名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.genfunctionname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.permsprefix
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "权限前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "权限前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "权限前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "权限前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "权限前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "权限前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.menubuttongroup
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.menubuttongroup", TranslationValue = "菜单权限组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.menubuttongroup", TranslationValue = "菜单权限组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.menubuttongroup", TranslationValue = "菜单权限组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.menubuttongroup", TranslationValue = "菜单权限组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.menubuttongroup", TranslationValue = "菜单权限组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.menubuttongroup", TranslationValue = "菜单权限组", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.nameprefix
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空间前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空间前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空间前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空间前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空间前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空间前缀", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.entitynamespace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "实体命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "实体命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "实体命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "实体命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "实体命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "实体命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.entityclassname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "实体类名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.dtonamespace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "传输对象Dto命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "传输对象Dto命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "传输对象Dto命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "传输对象Dto命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "传输对象Dto命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "传输对象Dto命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.dtoclassname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "传输对象Dto类名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "传输对象Dto类名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "传输对象Dto类名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "传输对象Dto类名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "传输对象Dto类名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "传输对象Dto类名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.servicenamespace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服务命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服务命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服务命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服务命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服务命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服务命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.iserviceclassname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "服务接口类名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.serviceclassname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "服务类名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.controllernamespace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.controllerclassname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "控制器类名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.isrepository
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isrepository", TranslationValue = "仓储层", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isrepository", TranslationValue = "仓储层", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isrepository", TranslationValue = "仓储层", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isrepository", TranslationValue = "仓储层", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isrepository", TranslationValue = "仓储层", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.isrepository", TranslationValue = "仓储层", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.repositoryinterfacenamespace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "仓储接口命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "仓储接口命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "仓储接口命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "仓储接口命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "仓储接口命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "仓储接口命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.irepositoryclassname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "仓储接口类名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.repositorynamespace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "仓储命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "仓储命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "仓储命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "仓储命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "仓储命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "仓储命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.repositoryclassname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "仓储类名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.genfunction
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.genmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.genpath
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.isgenmenu
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成菜单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成菜单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成菜单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成菜单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成菜单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成菜单", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.parentmenuid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "上级菜单ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.isgentranslation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻译", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻译", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻译", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻译", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻译", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻译", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.sortfield
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.sortfield", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.sortfield", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.sortfield", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.sortfield", TranslationValue = "排序字段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.sortfield", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.sortfield", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.sorttype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.sorttype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.sorttype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.sorttype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.sorttype", TranslationValue = "排序类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.sorttype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.sorttype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.frontui
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.frontui", TranslationValue = "前端UI框架", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.frontui", TranslationValue = "前端UI框架", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.frontui", TranslationValue = "前端UI框架", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.frontui", TranslationValue = "前端UI框架", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.frontui", TranslationValue = "前端UI框架", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.frontui", TranslationValue = "前端UI框架", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.frontformlayout
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.frontformlayout", TranslationValue = "前端表单布局", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.frontformlayout", TranslationValue = "前端表单布局", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.frontformlayout", TranslationValue = "前端表单布局", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.frontformlayout", TranslationValue = "前端表单布局", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.frontformlayout", TranslationValue = "前端表单布局", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.frontformlayout", TranslationValue = "前端表单布局", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.frontbtnstyle
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.frontbtnstyle", TranslationValue = "前端按钮样式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.frontbtnstyle", TranslationValue = "前端按钮样式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.frontbtnstyle", TranslationValue = "前端按钮样式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.frontbtnstyle", TranslationValue = "前端按钮样式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.frontbtnstyle", TranslationValue = "前端按钮样式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.frontbtnstyle", TranslationValue = "前端按钮样式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.isgencode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isgencode", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isgencode", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isgencode", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isgencode", TranslationValue = "是否生成", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isgencode", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.isgencode", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.gencodecount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "代码生成次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.isusetabs
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.tabsfieldcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs标签字段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs标签字段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs标签字段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs标签字段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs标签字段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs标签字段", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.genauthor
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.gentable.othergenoptions
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.othergenoptions", TranslationValue = "其他生成选项", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.othergenoptions", TranslationValue = "其他生成选项", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.othergenoptions", TranslationValue = "其他生成选项", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.othergenoptions", TranslationValue = "其他生成选项", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.othergenoptions", TranslationValue = "其他生成选项", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.gentable.othergenoptions", TranslationValue = "其他生成选项", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
