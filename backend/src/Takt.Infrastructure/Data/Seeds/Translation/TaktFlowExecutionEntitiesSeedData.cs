// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFlowExecutionEntitiesSeedData.cs
// 功能描述：TaktFlowExecution（工作流流转历史）实体翻译种子，entity.flowhistory 兼容前端，与 TaktFlowExecution 属性名小写一一对应
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
/// TaktFlowExecution（工作流流转历史）实体翻译种子数据（ResourceKey 仍为 entity.flowhistory 以兼容前端）
/// </summary>
public class TaktFlowExecutionEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 54;

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
            var allTranslations = GetFlowExecutionEntityTranslations();

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
    /// 获取 TaktFlowExecution 实体字段翻译（ResourceKey 保持 entity.flowhistory 以兼容前端）
    /// </summary>
    private static List<TaktTranslation> GetFlowExecutionEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.flowhistory._self
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory._self", TranslationValue = "تاريخ انتقال العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory._self", TranslationValue = "Flow History", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory._self", TranslationValue = "Historial flujo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory._self", TranslationValue = "Historique flux", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory._self", TranslationValue = "フロー履歴", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory._self", TranslationValue = "플로우 이력", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory._self", TranslationValue = "История процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory._self", TranslationValue = "流程流转历史", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory._self", TranslationValue = "流程流轉歷史", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.instanceid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "معرف المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "Instance ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "ID instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "ID instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "インスタンスID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "인스턴스 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "ID экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "流程实例ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.instanceid", TranslationValue = "流程實例ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.instancecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "رمز المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "Instance Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "Código instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "Code instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "インスタンスコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "인스턴스 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "Код экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "流程实例编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.instancecode", TranslationValue = "流程實例編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.processkey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "مفتاح العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "Process Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "Clave proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "Clé processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "プロセスキー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "프로세스 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "Ключ процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.processname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.processname", TranslationValue = "اسم العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.processname", TranslationValue = "Process Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.processname", TranslationValue = "Nombre proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.processname", TranslationValue = "Nom processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.processname", TranslationValue = "プロセス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.processname", TranslationValue = "프로세스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.processname", TranslationValue = "Название процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.processname", TranslationValue = "流程名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.processname", TranslationValue = "流程名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.fromnodeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "معرف العقدة المصدر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "From Node ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "ID nodo origen", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "ID nœud source", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "元ノードID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "시작 노드 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "ID узла откуда", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "源节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.fromnodeid", TranslationValue = "源節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.fromnodename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "اسم العقدة المصدر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "From Node Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "Nombre nodo origen", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "Nom nœud source", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "元ノード名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "시작 노드명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "Название узла откуда", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "源节点名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.fromnodename", TranslationValue = "源節點名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.tonodeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "معرف العقدة الهدف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "To Node ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "ID nodo destino", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "ID nœud cible", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "先ノードID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "도착 노드 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "ID узла куда", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "目标节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.tonodeid", TranslationValue = "目標節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.tonodename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "اسم العقدة الهدف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "To Node Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "Nombre nodo destino", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "Nom nœud cible", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "先ノード名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "도착 노드명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "Название узла куда", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "目标节点名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.tonodename", TranslationValue = "目標節點名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.transitiontype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "نوع الانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "Transition Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "Tipo transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "Type transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "遷移タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "전환 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "Тип перехода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "流转类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitiontype", TranslationValue = "流轉類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.transitiontime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "وقت الانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "Transition Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "Hora transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "Heure transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "遷移日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "전환 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "Время перехода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "流转时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitiontime", TranslationValue = "流轉時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.transitionusername
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "اسم من قام بالانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "Transition User Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "Usuario transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "Utilisateur transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "遷移者名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "전환 사용자명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "Имя переведшего", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "流转人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitionusername", TranslationValue = "流轉人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.transitioncomment
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "تعليق الانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "Transition Comment", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "Comentario transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "Commentaire transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "遷移コメント", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "전환 의견", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "Комментарий перехода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "流转意见", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitioncomment", TranslationValue = "流轉意見", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.elapsedmilliseconds
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "الوقت المنقضي (مللي ثانية)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "Elapsed (ms)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "Tiempo transcurrido (ms)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "Écoulé (ms)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "経過時間(ミリ秒)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "소요 시간(ms)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "Затрачено (мс)", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "流转耗时（毫秒）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.elapsedmilliseconds", TranslationValue = "流轉耗時（毫秒）", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            // entity.flowhistory.fromnodetype / tonodetype / isfinish / transitionuserid / transitiondeptid / transitiondeptname / transitionattachments
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "نوع العقدة المصدر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "From Node Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "Tipo nodo origen", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "Type nœud source", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "元ノードタイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "시작 노드 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "Тип узла откуда", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "源节点类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.fromnodetype", TranslationValue = "源節點類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "نوع العقدة الهدف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "To Node Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "Tipo nodo destino", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "Type nœud cible", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "先ノードタイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "도착 노드 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "Тип узла куда", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "目标节点类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.tonodetype", TranslationValue = "目標節點類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "منتهي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "Is Finish", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "Finalizado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "Terminé", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "終了済み", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "완료 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "Завершено", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "是否已结束", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.isfinish", TranslationValue = "是否已結束", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "معرف من قام بالانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "Transition User ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "ID usuario transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "ID utilisateur transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "遷移者ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "전환 사용자 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "ID переведшего", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "流转人ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitionuserid", TranslationValue = "流轉人ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "معرف قسم الانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "Transition Dept ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "ID depto. transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "ID dépt. transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "遷移部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "전환 부서 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "ID отдела перевода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "流转部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitiondeptid", TranslationValue = "流轉部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "اسم قسم الانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "Transition Dept Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "Nombre depto. transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "Nom dépt. transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "遷移部門名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "전환 부서명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "Название отдела перевода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "流转部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitiondeptname", TranslationValue = "流轉部門名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "مرفقات الانتقال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "Transition Attachments", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "Adjuntos transición", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "Pièces jointes transition", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "遷移添付", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "전환 첨부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "Вложения перехода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "流转附件", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowhistory.transitionattachments", TranslationValue = "流轉附件", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
        };
    }
}
