// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFiscalYearEntitiesSeedData.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktFiscalYear 实体翻译种子数据，entity.fiscalyear / entity.fiscalyear.xxx，与 TaktFiscalYear.cs 一一对应，9 种语言
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
/// TaktFiscalYear 实体翻译种子数据（与 TaktFiscalYear.cs 属性一一对应）
/// </summary>
public class TaktFiscalYearEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetFiscalYearEntityTranslations();

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
    /// 获取 TaktFiscalYear 实体字段翻译（ResourceKey 与 TaktFiscalYear.cs 属性名小写一一对应）
    /// </summary>
    private static List<TaktTranslation> GetFiscalYearEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.fiscalyear._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear._self", TranslationValue = "السنة المالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear._self", TranslationValue = "Fiscal Year", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear._self", TranslationValue = "Año fiscal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear._self", TranslationValue = "Année fiscale", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear._self", TranslationValue = "会計年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear._self", TranslationValue = "회계연도", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear._self", TranslationValue = "Финансовый год", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear._self", TranslationValue = "会计年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear._self", TranslationValue = "會計年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.fiscalyear
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "السنة المالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "Fiscal Year", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "Año fiscal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "Année", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "회계연도", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "Год", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "会计年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.fiscalyear", TranslationValue = "會計年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.fiscalyearname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "اسم السنة المالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "Fiscal Year Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "Nombre año fiscal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "Nom année fiscale", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "会計年度名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "회계연도명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "Название года", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "会计年度名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.fiscalyearname", TranslationValue = "會計年度名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.startdate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "تاريخ البدء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "Start Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "Fecha inicio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "Date début", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "開始日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "시작일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "Дата начала", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "开始日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.startdate", TranslationValue = "開始日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.enddate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "تاريخ الانتهاء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "End Date", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "Fecha fin", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "Date fin", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "終了日", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "종료일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "Дата окончания", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "结束日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.enddate", TranslationValue = "結束日期", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.fiscalstatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "حالة السنة المالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "Fiscal Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "Estado año fiscal", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "Statut année fiscale", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "会計年度状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "회계연도 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "Статус года", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "会计年度状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.fiscalstatus", TranslationValue = "會計年度狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.iscurrent
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "السنة الحالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "Current Year", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "Año actual", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "Année en cours", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "当年", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "당해 연도", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "Текущий год", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "是否当前年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.iscurrent", TranslationValue = "是否當前年度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "Sort Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "정렬순서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.fiscalyear.plantid / entity.fiscalyear.plantcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "معرف المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "Plant ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "ID Planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "ID Usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "工場ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "공장 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "ID завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "工厂ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.plantid", TranslationValue = "工廠ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "رمز المصنع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "Plant Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "Código Planta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "Code Usine", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "工場コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "공장코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "Код завода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.fiscalyear.plantcode", TranslationValue = "工廠代碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
        };
    }
}
