// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktEmployeeEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktEmployee 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.HumanResource.Personnel;

/// <summary>
/// TaktEmployee 实体翻译种子数据（自动生成，与 TaktEmployee.cs 属性一一对应）
/// </summary>
public class TaktEmployeeEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktEmployeeEntityTranslations();

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
    /// 获取所有 TaktEmployee 实体名称及字段翻译（自动生成，与 TaktEmployee.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktEmployeeEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.employee（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee._self", TranslationValue = "员工信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee._self", TranslationValue = "员工信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee._self", TranslationValue = "员工信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee._self", TranslationValue = "员工信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee._self", TranslationValue = "员工信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee._self", TranslationValue = "员工信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.employeecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.employeecode", TranslationValue = "员工编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.employeecode", TranslationValue = "员工编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.employeecode", TranslationValue = "员工编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.employeecode", TranslationValue = "员工编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.employeecode", TranslationValue = "员工编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.employeecode", TranslationValue = "员工编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.realname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.realname", TranslationValue = "实名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.realname", TranslationValue = "实名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.realname", TranslationValue = "实名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.realname", TranslationValue = "实名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.realname", TranslationValue = "实名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.realname", TranslationValue = "实名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.formername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.formername", TranslationValue = "曾用名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.formername", TranslationValue = "曾用名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.formername", TranslationValue = "曾用名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.formername", TranslationValue = "曾用名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.formername", TranslationValue = "曾用名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.formername", TranslationValue = "曾用名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.fullname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.nativename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.nativename", TranslationValue = "本地化姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.nativename", TranslationValue = "本地化姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.nativename", TranslationValue = "本地化姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.nativename", TranslationValue = "本地化姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.nativename", TranslationValue = "本地化姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.nativename", TranslationValue = "本地化姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.displayname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.displayname", TranslationValue = "显示名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.displayname", TranslationValue = "显示名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.displayname", TranslationValue = "显示名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.displayname", TranslationValue = "显示名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.displayname", TranslationValue = "显示名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.displayname", TranslationValue = "显示名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.gender
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.gender", TranslationValue = "性别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.gender", TranslationValue = "性别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.gender", TranslationValue = "性别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.gender", TranslationValue = "性别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.gender", TranslationValue = "性别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.gender", TranslationValue = "性别", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.birthdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.birthdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.birthdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.birthdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.birthdate", TranslationValue = "出生日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.birthdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.birthdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.age
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.age", TranslationValue = "年龄", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.age", TranslationValue = "年龄", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.age", TranslationValue = "年龄", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.age", TranslationValue = "年龄", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.age", TranslationValue = "年龄", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.age", TranslationValue = "年龄", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.idcard
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.idcard", TranslationValue = "身份证号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.idcard", TranslationValue = "身份证号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.idcard", TranslationValue = "身份证号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.idcard", TranslationValue = "身份证号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.idcard", TranslationValue = "身份证号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.idcard", TranslationValue = "身份证号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.phone
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.phone", TranslationValue = "手机号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.phone", TranslationValue = "手机号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.phone", TranslationValue = "手机号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.phone", TranslationValue = "手机号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.phone", TranslationValue = "手机号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.phone", TranslationValue = "手机号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.email
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.email", TranslationValue = "邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.email", TranslationValue = "邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.email", TranslationValue = "邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.email", TranslationValue = "邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.email", TranslationValue = "邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.email", TranslationValue = "邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.avatar
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.avatar", TranslationValue = "头像", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.avatar", TranslationValue = "头像", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.avatar", TranslationValue = "头像", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.avatar", TranslationValue = "头像", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.avatar", TranslationValue = "头像", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.avatar", TranslationValue = "头像", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.nationality
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.nationality", TranslationValue = "民族", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.nationality", TranslationValue = "民族", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.nationality", TranslationValue = "民族", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.nationality", TranslationValue = "民族", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.nationality", TranslationValue = "民族", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.nationality", TranslationValue = "民族", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.politicalstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.politicalstatus", TranslationValue = "政治面貌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.politicalstatus", TranslationValue = "政治面貌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.politicalstatus", TranslationValue = "政治面貌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.politicalstatus", TranslationValue = "政治面貌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.politicalstatus", TranslationValue = "政治面貌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.politicalstatus", TranslationValue = "政治面貌", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.maritalstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.maritalstatus", TranslationValue = "婚姻状况", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.maritalstatus", TranslationValue = "婚姻状况", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.maritalstatus", TranslationValue = "婚姻状况", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.maritalstatus", TranslationValue = "婚姻状况", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.maritalstatus", TranslationValue = "婚姻状况", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.maritalstatus", TranslationValue = "婚姻状况", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.nativeplace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.nativeplace", TranslationValue = "籍贯", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.nativeplace", TranslationValue = "籍贯", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.nativeplace", TranslationValue = "籍贯", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.nativeplace", TranslationValue = "籍贯", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.nativeplace", TranslationValue = "籍贯", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.nativeplace", TranslationValue = "籍贯", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.currentaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.currentaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.currentaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.currentaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.currentaddress", TranslationValue = "现居住地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.currentaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.currentaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.registeredaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.registeredaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.registeredaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.registeredaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.registeredaddress", TranslationValue = "户籍地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.registeredaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.registeredaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.employee.employeestatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.employee.employeestatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.employee.employeestatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.employee.employeestatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.employee.employeestatus", TranslationValue = "员工状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.employee.employeestatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.employee.employeestatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
