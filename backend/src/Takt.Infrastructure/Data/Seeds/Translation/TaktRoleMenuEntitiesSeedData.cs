// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktRoleMenuEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktRoleMenu 实体字段翻译种子数据，与 TaktUserEntitiesSeedData 风格一致，entity.rolemenu / entity.rolemenu.xxx，每个字段 9 种语言
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// TaktRoleMenu 实体翻译种子数据（与 TaktRoleMenu.cs 属性一一对应）
/// </summary>
public class TaktRoleMenuEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在 TaktDeptEntitiesSeedData 之后）
    /// </summary>
    public int Order => 14;

    /// <summary>
    /// 初始化角色菜单关联实体翻译种子数据
    /// </summary>
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
            var allTranslations = GetAllRoleMenuEntityTranslations();

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
                        OrderNum = translation.OrderNum,
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
    /// 获取所有 TaktRoleMenu 实体名称及字段翻译（ResourceKey 拆分风格 entity.rolemenu / entity.rolemenu.xxx，与 TaktRoleMenu.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllRoleMenuEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.rolemenu（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.rolemenu._self", TranslationValue = "ارتباط دور القائمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.rolemenu._self", TranslationValue = "Role-Menu", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.rolemenu._self", TranslationValue = "Rol-Menú", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.rolemenu._self", TranslationValue = "Rôle-Menu", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.rolemenu._self", TranslationValue = "ロールメニュー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.rolemenu._self", TranslationValue = "역할-메뉴", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.rolemenu._self", TranslationValue = "Роль-Меню", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.rolemenu._self", TranslationValue = "角色菜单关联", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.rolemenu._self", TranslationValue = "角色選單關聯", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.rolemenu.roleid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "معرف الدور", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "Role ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "ID del rol", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "ID du rôle", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "ロールID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "역할 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "ID роли", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "角色ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.rolemenu.roleid", TranslationValue = "角色ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.rolemenu.menuid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "معرف القائمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "Menu ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "ID del menú", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "ID du menu", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "メニューID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "메뉴 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "ID меню", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "菜单ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.rolemenu.menuid", TranslationValue = "選單ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
