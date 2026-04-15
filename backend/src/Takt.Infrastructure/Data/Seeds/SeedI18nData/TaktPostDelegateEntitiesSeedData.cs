// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktPostDelegateEntitiesSeedData.cs
// 功能描述：TaktPostDelegate 实体字段翻译种子数据，与 TaktRoleDeptEntitiesSeedData 风格一致（每键 9 语内联），与 TaktPostDelegate.cs 一一对应。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// TaktPostDelegate 实体翻译种子数据（岗位代理规则）。
/// </summary>
public class TaktPostDelegateEntitiesSeedData : ITaktSeedData
{
    public int Order => 200;

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
            var allTranslations = GetAllPostDelegateEntityTranslations();

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

    private static List<TaktTranslation> GetAllPostDelegateEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.postdelegate._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate._self", TranslationValue = "تفويض المنصب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate._self", TranslationValue = "Post delegation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate._self", TranslationValue = "Delegación de puesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate._self", TranslationValue = "Délégation de poste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate._self", TranslationValue = "職位代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate._self", TranslationValue = "직위 대리", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate._self", TranslationValue = "Делегирование должности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate._self", TranslationValue = "岗位代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate._self", TranslationValue = "崗位代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.postdelegate.keyword
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "معرف المنصب، الوكيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "Post ID, delegate", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "ID de puesto, delegado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "ID de poste, délégué", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "職位ID、代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "직위 ID, 대리", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "Должность, делегат", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "岗位ID、代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate.keyword", TranslationValue = "崗位ID、代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.postdelegate.postid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate.postid", TranslationValue = "معرف المنصب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate.postid", TranslationValue = "Post ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate.postid", TranslationValue = "ID del puesto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate.postid", TranslationValue = "ID du poste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate.postid", TranslationValue = "職位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate.postid", TranslationValue = "직위 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate.postid", TranslationValue = "ID должности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate.postid", TranslationValue = "岗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate.postid", TranslationValue = "崗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.postdelegate.delegatemode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "وضع التفويض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "Delegate mode", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "Modo de delegación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "Mode de délégation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "代理モード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "대리 모드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "Режим делегирования", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "代理模式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate.delegatemode", TranslationValue = "代理模式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.postdelegate.delegateemployeeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "معرف الموظف المفوض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "Delegate employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "ID empleado delegado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "ID employé délégué", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "代理先社員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "대리 사원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "ID сотрудника-делегата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "被代理员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate.delegateemployeeid", TranslationValue = "被代理員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.postdelegate.delegatedeptid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "معرف القسم المفوض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "Delegate department ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "ID departamento delegado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "ID département délégué", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "代理先部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "대리 부서 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "ID отдела-делегата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "被引用部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate.delegatedeptid", TranslationValue = "被引用部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.postdelegate.delegatepostid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "معرف المنصب المفوض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "Delegate post ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "ID puesto delegado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "ID poste délégué", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "代理先職位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "대리 직위 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "ID должности-делегата", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "被引用岗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate.delegatepostid", TranslationValue = "被引用崗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.postdelegate.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.postdelegate.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
