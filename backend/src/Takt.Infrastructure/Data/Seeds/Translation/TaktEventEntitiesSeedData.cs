// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktEventEntitiesSeedData.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktEvent 实体字段翻译种子数据，entity.event / entity.event.xxx，与 TaktEvent.cs 一一对应，9 种语言
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
/// TaktEvent 实体翻译种子数据（与 TaktEvent.cs 属性一一对应）
/// </summary>
public class TaktEventEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 59;

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
            var allTranslations = GetAllEventEntityTranslations();

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
    /// 获取所有 TaktEvent 实体名称及字段翻译（ResourceKey 风格 entity.event / entity.event.xxx，与 TaktEvent.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllEventEntityTranslations()
    {
        var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
        var list = new List<TaktTranslation>();

        // entity.event._self（活动组织，英文用 Event）
        var selfTranslations = new Dictionary<string, string>
        {
            ["ar-SA"] = "الفعالية", ["en-US"] = "Event", ["es-ES"] = "Evento", ["fr-FR"] = "Événement",
            ["ja-JP"] = "活動", ["ko-KR"] = "행사", ["ru-RU"] = "Мероприятие", ["zh-CN"] = "活动组织", ["zh-TW"] = "活動組織"
        };
        foreach (var c in cultures)
            list.Add(new TaktTranslation { CultureCode = c, ResourceKey = "entity.event._self", TranslationValue = selfTranslations[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        // entity.event.* 字段
        var keys = new (string Key, Dictionary<string, string> Values)[]
        {
            ("entity.event.companycode", new Dictionary<string, string> { ["ar-SA"] = "رمز الشركة", ["en-US"] = "Company Code", ["es-ES"] = "Código de empresa", ["fr-FR"] = "Code société", ["ja-JP"] = "会社コード", ["ko-KR"] = "회사 코드", ["ru-RU"] = "Код компании", ["zh-CN"] = "公司代码", ["zh-TW"] = "公司代碼" }),
            ("entity.event.plantcode", new Dictionary<string, string> { ["ar-SA"] = "رمز المصنع", ["en-US"] = "Plant Code", ["es-ES"] = "Código de planta", ["fr-FR"] = "Code usine", ["ja-JP"] = "工場コード", ["ko-KR"] = "공장 코드", ["ru-RU"] = "Код завода", ["zh-CN"] = "工厂代码", ["zh-TW"] = "工廠代碼" }),
            ("entity.event.eventcode", new Dictionary<string, string> { ["ar-SA"] = "رمز الفعالية", ["en-US"] = "Event Code", ["es-ES"] = "Código de evento", ["fr-FR"] = "Code événement", ["ja-JP"] = "活動コード", ["ko-KR"] = "행사 코드", ["ru-RU"] = "Код мероприятия", ["zh-CN"] = "活动编码", ["zh-TW"] = "活動編碼" }),
            ("entity.event.eventname", new Dictionary<string, string> { ["ar-SA"] = "اسم الفعالية", ["en-US"] = "Event Name", ["es-ES"] = "Nombre del evento", ["fr-FR"] = "Nom de l'événement", ["ja-JP"] = "活動名", ["ko-KR"] = "행사명", ["ru-RU"] = "Название мероприятия", ["zh-CN"] = "活动名称", ["zh-TW"] = "活動名稱" }),
            ("entity.event.eventtype", new Dictionary<string, string> { ["ar-SA"] = "نوع الفعالية", ["en-US"] = "Event Type", ["es-ES"] = "Tipo de evento", ["fr-FR"] = "Type d'événement", ["ja-JP"] = "活動種別", ["ko-KR"] = "행사 유형", ["ru-RU"] = "Тип мероприятия", ["zh-CN"] = "活动类型", ["zh-TW"] = "活動類型" }),
            ("entity.event.starttime", new Dictionary<string, string> { ["ar-SA"] = "وقت البدء", ["en-US"] = "Start Time", ["es-ES"] = "Hora de inicio", ["fr-FR"] = "Heure de début", ["ja-JP"] = "開始時間", ["ko-KR"] = "시작 시간", ["ru-RU"] = "Время начала", ["zh-CN"] = "开始时间", ["zh-TW"] = "開始時間" }),
            ("entity.event.endtime", new Dictionary<string, string> { ["ar-SA"] = "وقت الانتهاء", ["en-US"] = "End Time", ["es-ES"] = "Hora de fin", ["fr-FR"] = "Heure de fin", ["ja-JP"] = "終了時間", ["ko-KR"] = "종료 시간", ["ru-RU"] = "Время окончания", ["zh-CN"] = "结束时间", ["zh-TW"] = "結束時間" }),
            ("entity.event.location", new Dictionary<string, string> { ["ar-SA"] = "الموقع", ["en-US"] = "Location", ["es-ES"] = "Ubicación", ["fr-FR"] = "Lieu", ["ja-JP"] = "場所", ["ko-KR"] = "장소", ["ru-RU"] = "Место", ["zh-CN"] = "活动地点", ["zh-TW"] = "活動地點" }),
            ("entity.event.organizername", new Dictionary<string, string> { ["ar-SA"] = "المنظم", ["en-US"] = "Organizer", ["es-ES"] = "Organizador", ["fr-FR"] = "Organisateur", ["ja-JP"] = "主催者", ["ko-KR"] = "주최자", ["ru-RU"] = "Организатор", ["zh-CN"] = "组织人", ["zh-TW"] = "組織人" }),
            ("entity.event.deptname", new Dictionary<string, string> { ["ar-SA"] = "القسم", ["en-US"] = "Department", ["es-ES"] = "Departamento", ["fr-FR"] = "Département", ["ja-JP"] = "部門", ["ko-KR"] = "부서", ["ru-RU"] = "Отдел", ["zh-CN"] = "组织部门", ["zh-TW"] = "組織部門" }),
            ("entity.event.eventstatus", new Dictionary<string, string> { ["ar-SA"] = "حالة الفعالية", ["en-US"] = "Event Status", ["es-ES"] = "Estado del evento", ["fr-FR"] = "État de l'événement", ["ja-JP"] = "活動状態", ["ko-KR"] = "행사 상태", ["ru-RU"] = "Статус мероприятия", ["zh-CN"] = "活动状态", ["zh-TW"] = "活動狀態" }),
            ("entity.event.eventcontent", new Dictionary<string, string> { ["ar-SA"] = "محتوى الفعالية", ["en-US"] = "Event Content", ["es-ES"] = "Contenido del evento", ["fr-FR"] = "Contenu de l'événement", ["ja-JP"] = "活動内容", ["ko-KR"] = "행사 내용", ["ru-RU"] = "Содержание мероприятия", ["zh-CN"] = "活动内容", ["zh-TW"] = "活動內容" }),
            ("entity.event.participantsummary", new Dictionary<string, string> { ["ar-SA"] = "ملخص المشاركين", ["en-US"] = "Participants", ["es-ES"] = "Participantes", ["fr-FR"] = "Participants", ["ja-JP"] = "参加者", ["ko-KR"] = "참가자", ["ru-RU"] = "Участники", ["zh-CN"] = "参与人摘要", ["zh-TW"] = "參與人摘要" }),
            ("entity.event.ordernum", new Dictionary<string, string> { ["ar-SA"] = "الترتيب", ["en-US"] = "Sort Order", ["es-ES"] = "Orden", ["fr-FR"] = "Ordre", ["ja-JP"] = "並び順", ["ko-KR"] = "정렬 순서", ["ru-RU"] = "Порядок", ["zh-CN"] = "排序号", ["zh-TW"] = "排序號" })
        };

        foreach (var (key, values) in keys)
            foreach (var c in cultures)
                list.Add(new TaktTranslation { CultureCode = c, ResourceKey = key, TranslationValue = values[c], ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 });

        return list;
    }
}
