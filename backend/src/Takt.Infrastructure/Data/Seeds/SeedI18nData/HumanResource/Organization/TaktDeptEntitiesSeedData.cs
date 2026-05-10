// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDeptEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktDept 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.Organization;

/// <summary>
/// TaktDept 实体翻译种子数据（自动生成，与 TaktDept.cs 属性一一对应）
/// </summary>
public class TaktDeptEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktDeptEntityTranslations();

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
    /// 获取所有 TaktDept 实体名称及字段翻译（自动生成，与 TaktDept.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktDeptEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.dept（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept._self", TranslationValue = "部门信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept._self", TranslationValue = "部门信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept._self", TranslationValue = "部门信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept._self", TranslationValue = "部门信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept._self", TranslationValue = "部门信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept._self", TranslationValue = "部门信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.dept.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.name", TranslationValue = "部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.dept.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.code", TranslationValue = "部门编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.code", TranslationValue = "部门编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.code", TranslationValue = "部门编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.code", TranslationValue = "部门编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.code", TranslationValue = "部门编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.code", TranslationValue = "部门编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.dept.parentid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.parentid", TranslationValue = "父级ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.parentid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.dept.headid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.headid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.headid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.headid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.headid", TranslationValue = "部门负责人员工ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.headid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.headid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.dept.costcentercode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.costcentercode", TranslationValue = "成本中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.costcentercode", TranslationValue = "成本中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.costcentercode", TranslationValue = "成本中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.costcentercode", TranslationValue = "成本中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.costcentercode", TranslationValue = "成本中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.costcentercode", TranslationValue = "成本中心编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.dept.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.type", TranslationValue = "部门类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.dept.phone
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.phone", TranslationValue = "部门电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.phone", TranslationValue = "部门电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.phone", TranslationValue = "部门电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.phone", TranslationValue = "部门电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.phone", TranslationValue = "部门电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.phone", TranslationValue = "部门电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.dept.mail
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.mail", TranslationValue = "部门邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.mail", TranslationValue = "部门邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.mail", TranslationValue = "部门邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.mail", TranslationValue = "部门邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.mail", TranslationValue = "部门邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.mail", TranslationValue = "部门邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.dept.addr
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.addr", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.addr", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.addr", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.addr", TranslationValue = "部门地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.addr", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.addr", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.dept.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.dept.datascope
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.datascope", TranslationValue = "数据范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.datascope", TranslationValue = "数据范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.datascope", TranslationValue = "数据范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.datascope", TranslationValue = "数据范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.datascope", TranslationValue = "数据范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.datascope", TranslationValue = "数据范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.dept.customscope
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.customscope", TranslationValue = "自定义范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.customscope", TranslationValue = "自定义范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.customscope", TranslationValue = "自定义范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.customscope", TranslationValue = "自定义范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.customscope", TranslationValue = "自定义范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.customscope", TranslationValue = "自定义范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.dept.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dept.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dept.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dept.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dept.status", TranslationValue = "部门状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dept.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.dept.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
        };
    }
}
