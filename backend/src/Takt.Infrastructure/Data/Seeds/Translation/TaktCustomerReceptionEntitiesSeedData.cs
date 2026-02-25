// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktCustomerReceptionEntitiesSeedData.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktCustomerReception 实体字段翻译种子数据，entity.customerreception / entity.customerreception.xxx，与 TaktCustomerReception.cs 一一对应，9 种语言
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
/// TaktCustomerReception 实体翻译种子数据（与 TaktCustomerReception.cs 属性一一对应）
/// </summary>
public class TaktCustomerReceptionEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 60;

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
            var allTranslations = GetAllCustomerReceptionEntityTranslations();

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
    /// 获取所有 TaktCustomerReception 实体名称及字段翻译（ResourceKey 风格 entity.customerreception / entity.customerreception.xxx，与 TaktCustomerReception.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllCustomerReceptionEntityTranslations()
    {
        var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
        var list = new List<TaktTranslation>();

        // entity.customerreception._self
        var selfTranslations = new Dictionary<string, string>
        {
            ["ar-SA"] = "استقبال العملاء", ["en-US"] = "Customer Reception", ["es-ES"] = "Recepción de clientes", ["fr-FR"] = "Réception client",
            ["ja-JP"] = "客先接待", ["ko-KR"] = "고객 접대", ["ru-RU"] = "Приём клиентов", ["zh-CN"] = "客户接待", ["zh-TW"] = "客戶接待"
        };
        foreach (var c in cultures)
            list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.customerreception._self", TranslationValue = selfTranslations[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.customerreception.companycode / plantcode / receptioncode / customername / customercompany / visittime / visitpurpose / receptiondeptname / receptionusername / receptionstatus / contactinfo / visitorcount
        var keys = new (string Key, Dictionary<string, string> Values)[]
        {
            ("entity.customerreception.companycode", new Dictionary<string, string> { ["ar-SA"] = "رمز الشركة", ["en-US"] = "Company Code", ["es-ES"] = "Código de empresa", ["fr-FR"] = "Code société", ["ja-JP"] = "会社コード", ["ko-KR"] = "회사 코드", ["ru-RU"] = "Код компании", ["zh-CN"] = "公司代码", ["zh-TW"] = "公司代碼" }),
            ("entity.customerreception.plantcode", new Dictionary<string, string> { ["ar-SA"] = "رمز المصنع", ["en-US"] = "Plant Code", ["es-ES"] = "Código de planta", ["fr-FR"] = "Code usine", ["ja-JP"] = "工場コード", ["ko-KR"] = "공장 코드", ["ru-RU"] = "Код завода", ["zh-CN"] = "工厂代码", ["zh-TW"] = "工廠代碼" }),
            ("entity.customerreception.receptioncode", new Dictionary<string, string> { ["ar-SA"] = "رمز الاستقبال", ["en-US"] = "Reception Code", ["es-ES"] = "Código de recepción", ["fr-FR"] = "Code réception", ["ja-JP"] = "接待番号", ["ko-KR"] = "접대 코드", ["ru-RU"] = "Код приёма", ["zh-CN"] = "接待单编码", ["zh-TW"] = "接待單編碼" }),
            ("entity.customerreception.customername", new Dictionary<string, string> { ["ar-SA"] = "اسم العميل", ["en-US"] = "Customer Name", ["es-ES"] = "Nombre del cliente", ["fr-FR"] = "Nom du client", ["ja-JP"] = "客先名", ["ko-KR"] = "고객명", ["ru-RU"] = "Имя клиента", ["zh-CN"] = "来访客户名称", ["zh-TW"] = "來訪客戶名稱" }),
            ("entity.customerreception.customercompany", new Dictionary<string, string> { ["ar-SA"] = "شركة الزائر", ["en-US"] = "Customer Company", ["es-ES"] = "Empresa del cliente", ["fr-FR"] = "Société du client", ["ja-JP"] = "来訪先", ["ko-KR"] = "방문사", ["ru-RU"] = "Компания клиента", ["zh-CN"] = "来访单位", ["zh-TW"] = "來訪單位" }),
            ("entity.customerreception.visittime", new Dictionary<string, string> { ["ar-SA"] = "وقت الزيارة", ["en-US"] = "Visit Time", ["es-ES"] = "Hora de visita", ["fr-FR"] = "Heure de visite", ["ja-JP"] = "来訪日時", ["ko-KR"] = "방문 시간", ["ru-RU"] = "Время визита", ["zh-CN"] = "来访时间", ["zh-TW"] = "來訪時間" }),
            ("entity.customerreception.visitpurpose", new Dictionary<string, string> { ["ar-SA"] = "غرض الزيارة", ["en-US"] = "Visit Purpose", ["es-ES"] = "Objetivo de la visita", ["fr-FR"] = "Objet de la visite", ["ja-JP"] = "来訪目的", ["ko-KR"] = "방문 목적", ["ru-RU"] = "Цель визита", ["zh-CN"] = "来访事由", ["zh-TW"] = "來訪事由" }),
            ("entity.customerreception.receptiondeptname", new Dictionary<string, string> { ["ar-SA"] = "قسم الاستقبال", ["en-US"] = "Reception Department", ["es-ES"] = "Departamento de recepción", ["fr-FR"] = "Département réception", ["ja-JP"] = "接待部門", ["ko-KR"] = "접대 부서", ["ru-RU"] = "Отдел приёма", ["zh-CN"] = "接待部门", ["zh-TW"] = "接待部門" }),
            ("entity.customerreception.receptionusername", new Dictionary<string, string> { ["ar-SA"] = "المستقبل", ["en-US"] = "Receptionist", ["es-ES"] = "Receptor", ["fr-FR"] = "Réceptionniste", ["ja-JP"] = "接待者", ["ko-KR"] = "접대자", ["ru-RU"] = "Принимающий", ["zh-CN"] = "接待人", ["zh-TW"] = "接待人" }),
            ("entity.customerreception.receptionstatus", new Dictionary<string, string> { ["ar-SA"] = "حالة الاستقبال", ["en-US"] = "Reception Status", ["es-ES"] = "Estado de recepción", ["fr-FR"] = "État de la réception", ["ja-JP"] = "接待状態", ["ko-KR"] = "접대 상태", ["ru-RU"] = "Статус приёма", ["zh-CN"] = "接待状态", ["zh-TW"] = "接待狀態" }),
            ("entity.customerreception.contactinfo", new Dictionary<string, string> { ["ar-SA"] = "جهة الاتصال", ["en-US"] = "Contact Info", ["es-ES"] = "Contacto", ["fr-FR"] = "Coordonnées", ["ja-JP"] = "連絡先", ["ko-KR"] = "연락처", ["ru-RU"] = "Контакт", ["zh-CN"] = "联系人/电话", ["zh-TW"] = "聯繫人/電話" }),
            ("entity.customerreception.visitorcount", new Dictionary<string, string> { ["ar-SA"] = "عدد الزوار", ["en-US"] = "Visitor Count", ["es-ES"] = "Número de visitantes", ["fr-FR"] = "Nombre de visiteurs", ["ja-JP"] = "来訪人数", ["ko-KR"] = "방문 인원", ["ru-RU"] = "Количество посетителей", ["zh-CN"] = "来访人数", ["zh-TW"] = "來訪人數" })
        };

        foreach (var (key, values) in keys)
            foreach (var c in cultures)
                list.Add(new TaktTranslation { CultureCode = c, ResourceKey = key, TranslationValue = values[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        return list;
    }
}
