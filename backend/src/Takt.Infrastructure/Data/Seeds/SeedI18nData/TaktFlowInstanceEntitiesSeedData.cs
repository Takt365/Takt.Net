// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktFlowInstanceEntitiesSeedData.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktFlowInstance 实体字段翻译种子数据，entity.flowinstance / entity.flowinstance.xxx
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
/// TaktFlowInstance 实体翻译种子数据（与 TaktFlowInstance.cs 实体字段完全一致：_self + 26 个属性，每 key 9 种语言，ResourceKey 与属性名一致且小写）
/// </summary>
public class TaktFlowInstanceEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在 TaktFlowFormEntitiesSeedData 之后）
    /// </summary>
    public int Order => 42;

    /// <summary>
    /// 初始化流程实例实体翻译种子数据
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
            var allTranslations = GetAllFlowInstanceEntityTranslations();

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
    /// 获取所有 TaktFlowInstance 实体名称及字段翻译（与 TaktFlowInstance.cs 实体字段完全一致：_self + 26 个属性，每 key 9 种语言，ResourceKey 小写与属性名一致）
    /// </summary>
    private static List<TaktTranslation> GetAllFlowInstanceEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.flowinstance._self（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance._self", TranslationValue = "مثيل سير العمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance._self", TranslationValue = "flow instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance._self", TranslationValue = "instancia de flujo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance._self", TranslationValue = "instance de flux", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance._self", TranslationValue = "フローインスタンス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance._self", TranslationValue = "플로우 인스턴스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance._self", TranslationValue = "экземпляр процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance._self", TranslationValue = "流程实例", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance._self", TranslationValue = "流程實例", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.instancecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "رمز المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "instance code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "código de instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "code d'instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "インスタンスコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "인스턴스 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "код экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "实例编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.instancecode", TranslationValue = "實例編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.processkey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "مفتاح العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "process key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "clave del proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "clé du processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "プロセスキー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "프로세스 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "ключ процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.processkey", TranslationValue = "流程Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.schemeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "معرف المخطط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "scheme id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "id del esquema", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "id du schéma", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "スキームid", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "스키마 id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "id схемы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "流程方案ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.schemeid", TranslationValue = "流程方案ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.processname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.processname", TranslationValue = "اسم العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.processname", TranslationValue = "process name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.processname", TranslationValue = "nombre del proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.processname", TranslationValue = "nom du processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.processname", TranslationValue = "プロセス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.processname", TranslationValue = "프로세스 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.processname", TranslationValue = "название процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.processname", TranslationValue = "流程名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.processname", TranslationValue = "流程名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.businesskey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "المفتاح التجاري", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "business key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "clave de negocio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "clé métier", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "業務キー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "비즈니스 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "бизнес-ключ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "业务主键", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.businesskey", TranslationValue = "業務主鍵", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.businesstype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "نوع العمل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "business type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "tipo de negocio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "type métier", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "業務タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "비즈니스 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "тип бизнеса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "业务类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.businesstype", TranslationValue = "業務類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.startuserid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "معرف المستخدم البادئ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "start user id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "id de usuario iniciador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "id utilisateur initiateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "起動ユーザーid", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "시작 사용자 id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "id инициатора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "启动人ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.startuserid", TranslationValue = "啟動人ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.startusername
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "اسم المستخدم البادئ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "start user name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "nombre del usuario iniciador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "nom de l'utilisateur initiateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "起動ユーザー名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "시작 사용자 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "имя инициатора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "启动人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.startusername", TranslationValue = "啟動人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.startdeptid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "معرف القسم البادئ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "start dept id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "id del departamento iniciador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "id du département initiateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "起動部門id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "시작 부서 id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "id отдела инициатора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "启动部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.startdeptid", TranslationValue = "啟動部門ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.startdeptname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "اسم القسم البادئ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "start dept name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "nombre del departamento iniciador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "nom du département initiateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "起動部門名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "시작 부서 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "название отдела инициатора", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "启动部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.startdeptname", TranslationValue = "啟動部門名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.starttime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "وقت البدء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "start time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "hora de inicio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "heure de début", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "開始時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "시작 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "время начала", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "启动时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.starttime", TranslationValue = "啟動時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.endtime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "وقت الانتهاء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "end time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "hora de fin", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "heure de fin", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "終了時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "종료 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "время окончания", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "结束时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.endtime", TranslationValue = "結束時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.currentnodeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "معرف العقدة الحالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "current node id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "id del nodo actual", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "id du nœud actuel", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "現在ノードid", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "현재 노드 id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "id текущего узла", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "当前节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.currentnodeid", TranslationValue = "當前節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.currentnodename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "اسم العقدة الحالية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "current node name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "nombre del nodo actual", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "nom du nœud actuel", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "現在ノード名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "현재 노드 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "имя текущего узла", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "当前节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.currentnodename", TranslationValue = "當前節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.activityname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "اسم النشاط", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "activity name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "nombre de la actividad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "nom de l'activité", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "アクティビティ名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "활동 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "название активности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "当前节点名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.activityname", TranslationValue = "當前節點名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.previousnodeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "معرف العقدة السابقة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "previous node id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "id del nodo anterior", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "id du nœud précédent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "前ノードid", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "이전 노드 id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "id предыдущего узла", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "上一节点ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.previousnodeid", TranslationValue = "上一節點ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.makerlist
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "قائمة المنفذين", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "maker list", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "lista de ejecutores", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "liste des exécutants", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "実行者リスト", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "실행자 목록", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "список исполнителей", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "当前节点执行人ID列表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.makerlist", TranslationValue = "當前節點執行人ID列表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.frmdata
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "بيانات النموذج", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "frm data", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "datos del formulario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "données du formulaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "フォームデータ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "폼 데이터", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "данные формы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "表单数据", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.frmdata", TranslationValue = "表單數據", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.instancestatus
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "حالة المثيل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "instance status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "estado de la instancia", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "statut de l'instance", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "インスタンス状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "인스턴스 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "статус экземпляра", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "实例状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.instancestatus", TranslationValue = "實例狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.issuspended
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "موقوف أم لا", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "is suspended", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "está suspendido", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "est suspendu", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "保留フラグ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "일시 중지 여부", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "приостановлен", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "是否挂起", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.issuspended", TranslationValue = "是否掛起", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.suspendtime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "وقت التعليق", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "suspend time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "hora de suspensión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "heure de suspension", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "保留時刻", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "일시 중지 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "время приостановки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "挂起时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.suspendtime", TranslationValue = "掛起時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.suspendreason
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "سبب التعليق", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "suspend reason", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "motivo de suspensión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "raison de la suspension", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "保留理由", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "일시 중지 사유", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "причина приостановки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "挂起原因", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.suspendreason", TranslationValue = "掛起原因", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.priority
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.priority", TranslationValue = "الأولوية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.priority", TranslationValue = "priority", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.priority", TranslationValue = "prioridad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.priority", TranslationValue = "priorité", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.priority", TranslationValue = "優先度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.priority", TranslationValue = "우선순위", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.priority", TranslationValue = "приоритет", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.priority", TranslationValue = "优先级", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.priority", TranslationValue = "優先級", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.processtitle
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "عنوان العملية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "process title", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "título del proceso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "titre du processus", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "プロセスタイトル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "프로세스 제목", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "заголовок процесса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "流程标题", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.processtitle", TranslationValue = "流程標題", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.formid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.formid", TranslationValue = "معرف النموذج", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.formid", TranslationValue = "form id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.formid", TranslationValue = "id del formulario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.formid", TranslationValue = "id du formulaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.formid", TranslationValue = "フォームid", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.formid", TranslationValue = "양식 id", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.formid", TranslationValue = "id формы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.formid", TranslationValue = "流程表单ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.formid", TranslationValue = "流程表單ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.flowinstance.formcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "رمز النموذج", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "form code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "código del formulario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "code du formulaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "フォームコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "양식 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "код формы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "流程表单编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.flowinstance.formcode", TranslationValue = "流程表單編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
