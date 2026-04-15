// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktEmployeeDelegateEntitiesSeedData.cs
// 功能描述：TaktEmployeeDelegate 实体字段翻译种子数据，与 TaktPostDelegateEntitiesSeedData 风格一致（每键 9 语内联），与 TaktEmployeeDelegate.cs 一一对应。
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
/// TaktEmployeeDelegate 实体翻译种子数据（员工维度代理规则）。
/// </summary>
public class TaktEmployeeDelegateEntitiesSeedData : ITaktSeedData
{
    public int Order => 215;

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
            var allTranslations = GetAllEmployeeDelegateEntityTranslations();

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

    private static List<TaktTranslation> GetAllEmployeeDelegateEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.employeedelegate._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate._self", TranslationValue = "تفويض الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate._self", TranslationValue = "Employee delegation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate._self", TranslationValue = "Delegación de empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate._self", TranslationValue = "Délégation employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate._self", TranslationValue = "社員代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate._self", TranslationValue = "사원 대리", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate._self", TranslationValue = "Делегирование сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate._self", TranslationValue = "员工代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate._self", TranslationValue = "員工代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.employeedelegate.keyword
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "معرف الموظف، الوكيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "Employee ID, delegate", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "ID empleado, delegado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "ID employé, délégué", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "社員ID、代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "사원 ID, 대리", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "Сотрудник, делегат", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "员工ID、代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate.keyword", TranslationValue = "員工ID、代理", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.employeedelegate.employeeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "معرف الموظف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "Employee ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "ID empleado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "ID employé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "社員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "사원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "ID сотрудника", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "所属员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate.employeeid", TranslationValue = "所屬員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.employeedelegate.delegatemode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "وضع التفويض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "Delegate mode", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "Modo de delegación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "Mode de délégation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "代理モード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "대리 모드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "Режим делегирования", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "代理模式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate.delegatemode", TranslationValue = "代理模式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.employeedelegate.delegateemployeeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "معرف الموظف المفوض (مباشر)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "Delegate employee ID (direct)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "ID empleado delegado (directo)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "ID employé délégué (direct)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "直接代理先社員ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "직접 대리 사원 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "ID делегата (прямой)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "直接代理员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate.delegateemployeeid", TranslationValue = "直接代理員工ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.employeedelegate.delegatedeptid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "معرف القسم المفوض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "Delegate department ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "ID departamento delegado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "ID département délégué", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "部門ルール参照部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "부서 규칙 참조 부서 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "ID отдела (правило)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "部门规则引用部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate.delegatedeptid", TranslationValue = "部門規則引用部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.employeedelegate.delegatepostid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "معرف المنصب المفوض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "Delegate post ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "ID puesto delegado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "ID poste délégué", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "職位ルール参照職位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "직위 규칙 참조 직위 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "ID должности (правило)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "岗位规则引用岗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate.delegatepostid", TranslationValue = "崗位規則引用崗位ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.employeedelegate.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employeedelegate.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
