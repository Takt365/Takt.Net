// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCompanyEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktCompany 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Accounting.Financial;

/// <summary>
/// TaktCompany 实体翻译种子数据（自动生成，与 TaktCompany.cs 属性一一对应）
/// </summary>
public class TaktCompanyEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktCompanyEntityTranslations();

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
    /// 获取所有 TaktCompany 实体名称及字段翻译（自动生成，与 TaktCompany.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktCompanyEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.company（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company._self", TranslationValue = "公司信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company._self", TranslationValue = "公司信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company._self", TranslationValue = "公司信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company._self", TranslationValue = "公司信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company._self", TranslationValue = "公司信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company._self", TranslationValue = "公司信息", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companycode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companycode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companycode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companycode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companycode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companyname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companyname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companyname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companyname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companyname", TranslationValue = "公司名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companyname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companyname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companyshortname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companyshortname", TranslationValue = "公司简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companyshortname", TranslationValue = "公司简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companyshortname", TranslationValue = "公司简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companyshortname", TranslationValue = "公司简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companyshortname", TranslationValue = "公司简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companyshortname", TranslationValue = "公司简称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.registrationregion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.registrationregion", TranslationValue = "注册地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.registrationprovince
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.registrationprovince", TranslationValue = "注册地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.registrationcity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.registrationcity", TranslationValue = "注册地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.registrationaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.registrationaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.registrationaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.registrationaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.registrationaddress", TranslationValue = "注册地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.registrationaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.registrationaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.businessregion
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.businessregion", TranslationValue = "经营地区-国家", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.businessprovince
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.businessprovince", TranslationValue = "经营地区-省", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.businesscity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.businesscity", TranslationValue = "经营地区-市", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.businessaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.businessaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.businessaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.businessaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.businessaddress", TranslationValue = "经营地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.businessaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.businessaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companyphone
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companyphone", TranslationValue = "公司电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companyphone", TranslationValue = "公司电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companyphone", TranslationValue = "公司电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companyphone", TranslationValue = "公司电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companyphone", TranslationValue = "公司电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companyphone", TranslationValue = "公司电话", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companyemail
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companyemail", TranslationValue = "公司邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companyemail", TranslationValue = "公司邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companyemail", TranslationValue = "公司邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companyemail", TranslationValue = "公司邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companyemail", TranslationValue = "公司邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companyemail", TranslationValue = "公司邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companyfax
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companyfax", TranslationValue = "公司传真", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companyfax", TranslationValue = "公司传真", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companyfax", TranslationValue = "公司传真", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companyfax", TranslationValue = "公司传真", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companyfax", TranslationValue = "公司传真", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companyfax", TranslationValue = "公司传真", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companywebsite
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companywebsite", TranslationValue = "公司网站", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companywebsite", TranslationValue = "公司网站", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companywebsite", TranslationValue = "公司网站", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companywebsite", TranslationValue = "公司网站", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companywebsite", TranslationValue = "公司网站", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companywebsite", TranslationValue = "公司网站", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.unifiedsocialcreditcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.unifiedsocialcreditcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.unifiedsocialcreditcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.unifiedsocialcreditcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.unifiedsocialcreditcode", TranslationValue = "统一社会信用代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.unifiedsocialcreditcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.unifiedsocialcreditcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.taxregistrationnumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.taxregistrationnumber", TranslationValue = "税务登记号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.legalrepresentative
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.legalrepresentative", TranslationValue = "法定代表人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companymanager
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companymanager", TranslationValue = "公司负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companymanager", TranslationValue = "公司负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companymanager", TranslationValue = "公司负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companymanager", TranslationValue = "公司负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companymanager", TranslationValue = "公司负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companymanager", TranslationValue = "公司负责人", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.registeredcapital
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.registeredcapital", TranslationValue = "注册资本", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.establishmentdate
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.establishmentdate", TranslationValue = "Date", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.establishmentdate", TranslationValue = "日付", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.establishmentdate", TranslationValue = "날짜", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.establishmentdate", TranslationValue = "成立日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.establishmentdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.establishmentdate", TranslationValue = "日期", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.enterprisenature
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.enterprisenature", TranslationValue = "企业性质", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.industryattribute
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.industryattribute", TranslationValue = "行业属性", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.enterprisescale
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.enterprisescale", TranslationValue = "企业规模", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.businessscope
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.businessscope", TranslationValue = "经营范围", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.relatedplant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.companystatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.companystatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.companystatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.companystatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.companystatus", TranslationValue = "公司状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.companystatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.companystatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.company.SortOrder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.company.SortOrder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.company.SortOrder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.company.SortOrder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.company.SortOrder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.company.SortOrder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.company.SortOrder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
