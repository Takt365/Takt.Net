// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktAccountingTitleEntitiesSeedData.cs
// 创建时间：2025-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktAccountingTitle 实体字段翻译种子数据，entity.accountingtitle / entity.accountingtitle.xxx，zh-CN 与 ColumnDescription 对齐
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
/// TaktAccountingTitle 实体翻译种子数据。严格按 TaktAccountingTitle.cs 实体属性顺序，ResourceKey = entity.accountingtitle.属性名小写，每 key 9 种语言，zh-CN = ColumnDescription。
/// </summary>
public class TaktAccountingTitleEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 52;

    /// <summary>
    /// 初始化会计科目实体翻译种子数据
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
            var languages = await languageRepository.FindAsync(l =>
                l.LanguageStatus == 0 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllAccountingTitleEntityTranslations();

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
                else if (existing.TranslationValue != translation.TranslationValue
                         || existing.ResourceType != translation.ResourceType
                         || existing.ResourceGroup != translation.ResourceGroup)
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
    /// 获取所有 TaktAccountingTitle 实体名称及字段翻译（严格按 TaktAccountingTitle 实体属性顺序，ResourceKey = entity.accountingtitle.属性名小写）
    /// </summary>
    private static List<TaktTranslation> GetAllAccountingTitleEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // _self（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle._self", TranslationValue = "حساب محاسبي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle._self", TranslationValue = "Accounting Title", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle._self", TranslationValue = "Cuenta contable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle._self", TranslationValue = "Compte comptable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle._self", TranslationValue = "会计科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle._self", TranslationValue = "회계 과목", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle._self", TranslationValue = "Бухгалтерский счёт", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle._self", TranslationValue = "会计科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle._self", TranslationValue = "會計科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // CompanyCode → entity.accountingtitle.companycode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "رمز الشركة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "Company Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "Código de empresa", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "Code société", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "회사 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "Код компании", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.companycode", TranslationValue = "公司代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // TitleCode → entity.accountingtitle.titlecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "رمز الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "Account Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "Código de cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "Code du compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "科目编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "계정 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "Код счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "科目编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.titlecode", TranslationValue = "科目編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // TitleName → entity.accountingtitle.titlename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "اسم الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "Account Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "Nombre de la cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "Nom du compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "科目名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "계정 명칭", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "Наименование счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "科目名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.titlename", TranslationValue = "科目名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // ParentId → entity.accountingtitle.parentid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "المعرف الأب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "Parent ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "ID padre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "ID parent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "父级ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "상위 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "Родительский ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "父级ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.parentid", TranslationValue = "父級ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // TitleType → entity.accountingtitle.titletype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "نوع الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "Account Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "Tipo de cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "Type de compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "科目类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "계정 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "Тип счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "科目类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.titletype", TranslationValue = "科目類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // BalanceDirection → entity.accountingtitle.balancedirection
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "اتجاه الرصيد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "Balance Direction", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "Sentido del saldo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "Sens du solde", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "余额方向", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "잔액 방향", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "Направление сальдо", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "余额方向", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.balancedirection", TranslationValue = "餘額方向", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // TitleLevel → entity.accountingtitle.titlelevel
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "مستوى الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "Account Level", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "Nivel de cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "Niveau du compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "科目层级", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "계정 레벨", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "Уровень счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "科目层级", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.titlelevel", TranslationValue = "科目層級", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IsLeaf → entity.accountingtitle.isleaf
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "هل هو حساب فرعي نهائي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "Is Leaf Account", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "Es cuenta final", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "Est un compte feuille", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "是否末级科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "말단 계정 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "Конечный ли это счёт", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "是否末级科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.isleaf", TranslationValue = "是否末級科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IsAuxiliary → entity.accountingtitle.isauxiliary
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "هل هو حساب مساعد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "Is Auxiliary Accounting", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "Si es cuenta auxiliar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "Comptabilité auxiliaire ou non", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "是否辅助核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "보조 회계 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "Ведётся ли аналитический учёт", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "是否辅助核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.isauxiliary", TranslationValue = "是否輔助核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // AuxiliaryType → entity.accountingtitle.auxiliarytype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "نوع الحساب المساعد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "Auxiliary Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "Tipo auxiliar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "Type auxiliaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "辅助核算类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "보조 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "Тип аналитики", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "辅助核算类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.auxiliarytype", TranslationValue = "輔助核算類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IsQuantity → entity.accountingtitle.isquantity
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "هل هو حساب كمي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "Is Quantity Accounting", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "Si es contabilidad por cantidades", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "Comptabilité quantitative ou non", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "是否数量核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "수량 회계 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "Ведётся ли количественный учёт", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "是否数量核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.isquantity", TranslationValue = "是否數量核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IsCurrency → entity.accountingtitle.iscurrency
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "هل هو حساب عملة أجنبية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "Is Foreign Currency Accounting", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "Si es contabilidad en moneda extranjera", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "Comptabilité en devise ou non", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "是否外币核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "외화 회계 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "Ведётся ли учёт в иностранной валюте", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "是否外币核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.iscurrency", TranslationValue = "是否外幣核算", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IsCash → entity.accountingtitle.iscash
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "هل هو حساب نقدي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "Is Cash Account", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "Si es cuenta de efectivo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "Compte de trésorerie ou non", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "是否现金科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "현금 계정 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "Является ли кассовым счётом", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "是否现金科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.iscash", TranslationValue = "是否現金科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // IsBank → entity.accountingtitle.isbank
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "هل هو حساب بنكي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "Is Bank Account", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "Si es cuenta bancaria", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "Compte bancaire ou non", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "是否银行科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "은행 계정 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "Является ли банковским счётом", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "是否银行科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.isbank", TranslationValue = "是否銀行科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // RelatedPlant → entity.accountingtitle.relatedplant
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "المصنع المرتبط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "Related Plant", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "Planta relacionada", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "Usine associée", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "관련 공장", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "Связанный завод", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "关联工厂", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.relatedplant", TranslationValue = "關聯工廠", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // TitleStatus → entity.accountingtitle.titlestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "حالة الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "Account Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "Estado de la cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "Statut du compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "科目状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "계정 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "Статус счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "科目状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.titlestatus", TranslationValue = "科目狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // OrderNum → entity.accountingtitle.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accountingtitle.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}

