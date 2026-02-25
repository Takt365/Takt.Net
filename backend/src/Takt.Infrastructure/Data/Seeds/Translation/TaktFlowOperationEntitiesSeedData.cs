// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFlowOperationEntitiesSeedData.cs
// 功能描述：TaktFlowOperation 实体翻译种子，entity.flowoperation，与 TaktFlowOperation.cs 属性名小写一一对应，9 种语言
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
/// TaktFlowOperation 实体翻译种子数据（与 TaktFlowOperation.cs 属性一一对应）
/// </summary>
public class TaktFlowOperationEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 55;

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
            var allTranslations = GetFlowOperationEntityTranslations();

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
    /// 获取 TaktFlowOperation 实体字段翻译（ResourceKey 与 TaktFlowOperation.cs 属性名小写一一对应）
    /// </summary>
    private static List<TaktTranslation> GetFlowOperationEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.flowoperation._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation._self", TranslationValue = "سجل عملية التدفق", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation._self", TranslationValue = "Flow Operation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation._self", TranslationValue = "Operación flujo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation._self", TranslationValue = "Opération flux", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation._self", TranslationValue = "フロー操作履歴", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation._self", TranslationValue = "플로우 작업 이력", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation._self", TranslationValue = "Операция процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation._self", TranslationValue = "流程操作历史", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation._self", TranslationValue = "流程操作歷史", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.instanceid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "معرف المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "Instance ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "ID instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "ID instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "インスタンスID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "인스턴스 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "ID экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "流程实例ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.instanceid", TranslationValue = "流程實例ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.instancecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "رمز المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "Instance Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "Código instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "Code instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "インスタンスコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "인스턴스 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "Код экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "流程实例编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.instancecode", TranslationValue = "流程實例編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.processkey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "مفتاح العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "Process Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "Clave proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "Clé processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "プロセスキー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "프로세스 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "Ключ процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.processname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.processname", TranslationValue = "اسم العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.processname", TranslationValue = "Process Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.processname", TranslationValue = "Nombre proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.processname", TranslationValue = "Nom processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.processname", TranslationValue = "プロセス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.processname", TranslationValue = "프로세스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.processname", TranslationValue = "Название процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.processname", TranslationValue = "流程名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.processname", TranslationValue = "流程名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.operationtype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "نوع العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "Operation Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "Tipo operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "Type opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "操作タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "작업 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "Тип операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "操作类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operationtype", TranslationValue = "操作類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.operationtime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "وقت العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "Operation Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "Hora operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "Heure opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "操作日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "작업 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "Время операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "操作时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operationtime", TranslationValue = "操作時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.operatorname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "اسم المشغل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "Operator Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "Nombre operador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "Nom opérateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "操作者名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "작업자명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "Имя оператора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "操作人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operatorname", TranslationValue = "操作人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.operatorid / operatordeptid / operatordeptname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "معرف المشغل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "Operator ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "ID operador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "ID opérateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "操作者ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "작업자 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "ID оператора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "操作人ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operatorid", TranslationValue = "操作人ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "معرف قسم المشغل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "Operator Dept ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "ID depto. operador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "ID dépt. opérateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "操作部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "작업 부서 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "ID отдела оператора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "操作部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operatordeptid", TranslationValue = "操作部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "اسم قسم المشغل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "Operator Dept Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "Nombre depto. operador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "Nom dépt. opérateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "操作部門名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "작업 부서명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "Название отдела оператора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "操作部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operatordeptname", TranslationValue = "操作部門名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.nodeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "معرف العقدة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "Node ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "ID nodo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "ID nœud", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "ノードID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "노드 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "ID узла", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "操作节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.nodeid", TranslationValue = "操作節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.nodename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "اسم العقدة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "Node Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "Nombre nodo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "Nom nœud", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "ノード名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "노드명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "Название узла", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "操作节点名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.nodename", TranslationValue = "操作節點名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.operationcontent
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "محتوى العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "Operation Content", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "Contenido operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "Contenu opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "操作内容", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "작업 내용", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "Содержимое операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "操作内容", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operationcontent", TranslationValue = "操作內容", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.operationcomment
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "تعليق العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "Operation Comment", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "Comentario operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "Commentaire opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "操作コメント", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "작업 의견", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "Комментарий операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "操作意见", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operationcomment", TranslationValue = "操作意見", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.beforestatus / afterstatus / operationip / operationdevice / errormessage
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "الحالة قبل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "Before Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "Estado anterior", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "État avant", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "操作前状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "조작 전 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "Состояние до", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "操作前状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.beforestatus", TranslationValue = "操作前狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "الحالة بعد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "After Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "Estado posterior", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "État après", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "操作後状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "조작 후 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "Состояние после", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "操作后状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.afterstatus", TranslationValue = "操作後狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "IP العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "Operation IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "IP operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "IP opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "操作IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "작업 IP", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "IP операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "操作IP地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operationip", TranslationValue = "操作IP地址", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "جهاز العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "Operation Device", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "Dispositivo operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "Appareil opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "操作デバイス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "작업 기기", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "Устройство операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "操作设备信息", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operationdevice", TranslationValue = "操作設備資訊", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "رسالة الخطأ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "Error Message", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "Mensaje error", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "Message erreur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "エラーメッセージ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "오류 메시지", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "Сообщение об ошибке", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "错误信息", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.errormessage", TranslationValue = "錯誤資訊", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowoperation.operationresult
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "نتيجة العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "Operation Result", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "Resultado operación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "Résultat opération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "操作結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "작업 결과", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "Результат операции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "操作结果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowoperation.operationresult", TranslationValue = "操作結果", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
        };
    }
}
