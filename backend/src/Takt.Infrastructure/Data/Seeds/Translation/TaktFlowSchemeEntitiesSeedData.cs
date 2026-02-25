// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFlowSchemeEntitiesSeedData.cs
// 功能描述：TaktFlowScheme 实体翻译种子，entity.flowscheme，与 TaktFlowScheme.cs 属性名小写一一对应，9 种语言
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
/// TaktFlowScheme 实体翻译种子数据（与 TaktFlowScheme.cs 属性一一对应）
/// </summary>
public class TaktFlowSchemeEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 52;

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
            var allTranslations = GetFlowSchemeEntityTranslations();

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
    /// 获取 TaktFlowScheme 实体字段翻译（ResourceKey 与 TaktFlowScheme.cs 属性名小写一一对应）
    /// </summary>
    private static List<TaktTranslation> GetFlowSchemeEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.flowscheme._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme._self", TranslationValue = "مخطط العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme._self", TranslationValue = "Flow Scheme", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme._self", TranslationValue = "Esquema de flujo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme._self", TranslationValue = "Schéma de flux", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme._self", TranslationValue = "フロー定義", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme._self", TranslationValue = "플로우 정의", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme._self", TranslationValue = "Схема процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme._self", TranslationValue = "流程方案", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme._self", TranslationValue = "流程方案", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processkey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "مفتاح العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "Process Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "Clave proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "Clé processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "プロセスキー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "프로세스 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "Ключ процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processname", TranslationValue = "اسم العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processname", TranslationValue = "Process Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processname", TranslationValue = "Nombre proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processname", TranslationValue = "Nom processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processname", TranslationValue = "プロセス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processname", TranslationValue = "프로세스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processname", TranslationValue = "Название процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processname", TranslationValue = "流程名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processname", TranslationValue = "流程名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processcategory
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "تصنيف العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "Process Category", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "Categoría proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "Catégorie processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "プロセス分類", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "프로세스 분류", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "Категория процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "流程分类", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processcategory", TranslationValue = "流程分類", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processversion
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "إصدار العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "Process Version", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "Versión proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "Version processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "プロセスバージョン", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "프로세스 버전", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "Версия процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "流程版本号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processversion", TranslationValue = "流程版本號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processdescription
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "وصف العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "Process Description", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "Descripción proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "Description processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "プロセス説明", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "프로세스 설명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "Описание процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "流程描述", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processdescription", TranslationValue = "流程描述", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.formid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.formid", TranslationValue = "معرف النموذج", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.formid", TranslationValue = "Form ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.formid", TranslationValue = "ID formulario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.formid", TranslationValue = "ID formulaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.formid", TranslationValue = "フォームID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.formid", TranslationValue = "폼 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.formid", TranslationValue = "ID формы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.formid", TranslationValue = "流程表单ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.formid", TranslationValue = "流程表單ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.formcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "رمز النموذج", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "Form Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "Código formulario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "Code formulaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "フォームコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "폼 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "Код формы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "流程表单编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.formcode", TranslationValue = "流程表單編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processicon
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "أيقونة العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "Process Icon", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "Icono proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "Icône processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "プロセスアイコン", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "프로세스 아이콘", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "Иконка процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "流程图标", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processicon", TranslationValue = "流程圖標", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.issuspendable
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "يدعم الإيقاف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "Suspendable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "Suspendible", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "Suspendable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "一時停止可", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "일시중지 가능", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "Приостановка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "是否支持挂起", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.issuspendable", TranslationValue = "是否支援掛起", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.isrevocable
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "يدعم السحب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "Revocable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "Revocable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "Révocable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "取下可", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "회수 가능", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "Отзыв", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "是否支持撤回", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.isrevocable", TranslationValue = "是否支援撤回", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.istransferable
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "يدعم التحويل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "Transferable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "Transferible", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "Transférable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "転送可", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "이관 가능", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "Передача", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "是否支持转办", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.istransferable", TranslationValue = "是否支援轉辦", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.isreturnable
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "يدعم الإرجاع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "Returnable", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "Devolución", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "Retour", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "差戻可", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "반려 가능", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "Возврат", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "是否支持退回", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.isreturnable", TranslationValue = "是否支援退回", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "Sort Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "정렬순서", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processstatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "حالة العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "Process Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "Estado proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "Statut processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "プロセス状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "프로세스 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "Статус процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "流程状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processstatus", TranslationValue = "流程狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.bpmnxml
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "تعريف BPMN", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "BPMN Definition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "Definición BPMN", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "Définition BPMN", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "BPMN定義", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "BPMN 정의", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "Определение BPMN", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "BPMN流程定义", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.bpmnxml", TranslationValue = "BPMN流程定義", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.processjson
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "كاش الرسم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "Process JSON Cache", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "Caché JSON proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "Cache JSON processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "流程JSONキャッシュ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "프로세스 JSON 캐시", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "Кэш JSON процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "流程JSON缓存", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.processjson", TranslationValue = "流程JSON緩存", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowscheme.isaddsignable / isreducesignable / isautocomplete / timeoutconfig / notificationconfig
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "يدعم التوقيع الإضافي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "Add Sign", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "Firma adicional", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "Signature additionnelle", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "加签", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "추가 서명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "Доп. подпись", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "是否支持加签", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.isaddsignable", TranslationValue = "是否支援加簽", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "يدعم تقليل التوقيع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "Reduce Sign", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "Reducir firma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "Réduire signature", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "減签", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "서명 감소", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "Уменьшить подпись", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "是否支持减签", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.isreducesignable", TranslationValue = "是否支援減簽", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "إكمال تلقائي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "Auto Complete", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "Completar automático", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "Complétion auto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "自動完了", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "자동 완료", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "Автозавершение", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "是否自动完成", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.isautocomplete", TranslationValue = "是否自動完成", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "إعدادات المهلة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "Timeout Config", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "Config. tiempo límite", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "Config. délai", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "タイムアウト設定", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "타임아웃 설정", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "Настройка таймаута", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "超时配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.timeoutconfig", TranslationValue = "逾時配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "إعدادات الإشعار", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "Notification Config", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "Config. notificación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "Config. notification", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "通知設定", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "알림 설정", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "Настройка уведомлений", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "通知配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowscheme.notificationconfig", TranslationValue = "通知配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
        };
    }
}
