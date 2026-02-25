// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktAccountTitleEntitiesSeedData.cs
// 功能描述：TaktAccountTitle 实体翻译种子，entity.accounttitle，与 TaktAccountTitle.cs 属性一一对应，9 种语言
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
/// TaktAccountTitle 实体翻译种子数据（与 TaktAccountTitle.cs 属性一一对应）
/// </summary>
public class TaktAccountTitleEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 53;

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者（用于获取仓储）</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
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
            var allTranslations = GetAccountTitleEntityTranslations();

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
                else
                {
                    if (existing.TranslationValue != translation.TranslationValue)
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
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取 TaktAccountTitle 实体字段翻译（ResourceKey 与 TaktAccountTitle.cs 属性名小写一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAccountTitleEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.accounttitle._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle._self", TranslationValue = "الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle._self", TranslationValue = "Account Title", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle._self", TranslationValue = "Plan Contable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle._self", TranslationValue = "Compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle._self", TranslationValue = "会計科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle._self", TranslationValue = "계정과목", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle._self", TranslationValue = "Счёт", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle._self", TranslationValue = "会计科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle._self", TranslationValue = "會計科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.companycode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "رمز الشركة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "Company Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "Código Empresa", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "Code Société", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "会社コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "회사코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "Код компании", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "公司代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.companycode", TranslationValue = "公司代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.titlecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "رمز الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "Title Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "Código Cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "Code Compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "科目コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "과목코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "Код счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "科目编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.titlecode", TranslationValue = "科目編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.titlename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "اسم الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "Title Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "Nombre Cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "Nom Compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "科目名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "과목명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "Название счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "科目名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.titlename", TranslationValue = "科目名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.parentid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "المعرف الأب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "Parent ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "ID padre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "ID parent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "親ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "부모 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "ID родителя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "父级ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.parentid", TranslationValue = "父級ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.titletype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "نوع الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "Title Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "Tipo Cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "Type Compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "科目タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "과목유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "Тип счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "科目类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.titletype", TranslationValue = "科目類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.balancedirection
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "اتجاه الرصيد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "Balance Direction", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "Dirección saldo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "Sens solde", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "残高方向", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "잔액방향", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "Направление остатка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "余额方向", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.balancedirection", TranslationValue = "餘額方向", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "Sort Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "정렬순서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.effectivedate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "تاريخ السريان", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "Effective Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "Fecha vigencia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "Date d'effet", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "生效日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "시행일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "Дата вступления", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.effectivedate", TranslationValue = "生效日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.expirydate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "تاريخ الانتهاء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "Expiry Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "Fecha caducidad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "Date d'expiration", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "失効日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "만료일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "Дата истечения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "失效日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.expirydate", TranslationValue = "失效日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.isreconciliationaccount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "حساب تسوية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "Reconciliation Account", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "Cuenta conciliación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "Compte de rapprochement", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "統括科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "조정계정", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "Счёт согласования", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "是否统驭科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.isreconciliationaccount", TranslationValue = "是否統馭科目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.titlestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "حالة الحساب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "Title Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "Estado Cuenta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "Statut Compte", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "科目状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "과목상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "Статус счёта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "科目状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.titlestatus", TranslationValue = "科目狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.accounttitle.plantid / entity.accounttitle.plantcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "معرف المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "Plant ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "ID Planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "ID Usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "工場ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "공장 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "ID завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "工厂ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.plantid", TranslationValue = "工廠ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "رمز المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "Plant Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "Código Planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "Code Usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "工場コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "공장코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "Код завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.accounttitle.plantcode", TranslationValue = "工廠代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
        };
    }
}
