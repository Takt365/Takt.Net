// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFlowInstanceEntitiesSeedData.cs
// 功能描述：TaktFlowInstance 实体翻译种子，entity.flowinstance，与 TaktFlowInstance.cs 属性名小写一一对应，9 种语言
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
/// TaktFlowInstance 实体翻译种子数据（与 TaktFlowInstance.cs 属性一一对应）
/// </summary>
public class TaktFlowInstanceEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 56;

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
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
            var allTranslations = GetFlowInstanceEntityTranslations();

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
    /// 获取 TaktFlowInstance 实体字段翻译（ResourceKey 与 TaktFlowInstance.cs 属性名小写一一对应）
    /// </summary>
    private static List<TaktTranslation> GetFlowInstanceEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.flowinstance._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance._self", TranslationValue = "مثيل العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance._self", TranslationValue = "Flow Instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance._self", TranslationValue = "Instancia flujo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance._self", TranslationValue = "Instance flux", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance._self", TranslationValue = "フローインスタンス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance._self", TranslationValue = "플로우 인스턴스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance._self", TranslationValue = "Экземпляр процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance._self", TranslationValue = "流程实例", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance._self", TranslationValue = "流程實例", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.instancecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "رمز المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "Instance Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "Código instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "Code instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "インスタンスコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "인스턴스 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "Код экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "实例编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "實例編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.processkey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "مفتاح العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "Process Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "Clave proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "Clé processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "プロセスキー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "프로세스 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "Ключ процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.schemeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "معرف المخطط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "Scheme ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "ID esquema", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "ID schéma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "スキームID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "스키마 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "ID схемы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "流程方案ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "流程方案ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.businesskey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "المفتاح التجاري", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "Business Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "Clave negocio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "Clé métier", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "業務キー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "비즈니스 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "Бизнес-ключ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "业务主键", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "業務主鍵", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.deptid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "معرف القسم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "Dept ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "ID departamento", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "ID département", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "부서 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "ID отдела", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.deptid", TranslationValue = "部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.currentnodeid / previousnodeid / priority
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "معرف العقدة الحالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "Current Node ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "ID nodo actual", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "ID nœud actuel", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "現在ノードID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "현재 노드 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "ID текущего узла", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "当前节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "當前節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "معرف العقدة السابقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "Previous Node ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "ID nodo anterior", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "ID nœud précédent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "前ノードID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "이전 노드 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "ID предыдущего узла", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "上一节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "上一節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.priority", TranslationValue = "الأولوية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.priority", TranslationValue = "Priority", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.priority", TranslationValue = "Prioridad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.priority", TranslationValue = "Priorité", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.priority", TranslationValue = "優先度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.priority", TranslationValue = "우선순위", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.priority", TranslationValue = "Приоритет", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.priority", TranslationValue = "等级", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.priority", TranslationValue = "等級", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.currentnodename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "اسم العقدة الحالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "Current Node Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "Nombre nodo actual", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "Nom nœud actuel", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "現在ノード名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "현재 노드명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "Текущий узел", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "当前节点名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "當前節點名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.instancestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "حالة المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "Instance Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "Estado instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "Statut instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "インスタンス状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "인스턴스 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "Статус экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "实例状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "實例狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowinstance.processtitle
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "عنوان العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "Process Title", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "Título proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "Titre processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "プロセスタイトル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "프로세스 제목", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "Заголовок процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "流程标题", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "流程標題", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
        };
    }
}
