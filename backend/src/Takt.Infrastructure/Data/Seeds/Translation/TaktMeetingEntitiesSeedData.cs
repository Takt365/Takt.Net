// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMeetingEntitiesSeedData.cs
// 创建时间：2025-02-25
// 创建人：Takt365(Cursor AI)
// 功能描述：会议（Meeting）实体翻译种子：TaktMeeting、TaktMeetingApply、TaktMeetingMinutes，entity.meeting / entity.meetingapply / entity.meetingminutes，9 种语言
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
/// 会议模块实体翻译种子数据（TaktMeeting、TaktMeetingApply、TaktMeetingMinutes 与实体属性一一对应）
/// </summary>
public class TaktMeetingEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 61;

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0, updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0) return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllMeetingEntityTranslations();

            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId)) continue;

                var existing = await translationRepository.GetAsync(t =>
                    t.ResourceKey == translation.ResourceKey &&
                    t.CultureCode == translation.CultureCode && t.IsDeleted == 0);

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

    private static List<TaktTranslation> GetAllMeetingEntityTranslations()
    {
        var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
        var entries = new List<(string Key, string ar, string en, string es, string fr, string ja, string ko, string ru, string zhCN, string zhTW)>
        {
            // ----- TaktMeeting -----
            ("entity.meeting._self", "اجتماع", "Meeting", "Reunión", "Réunion", "会議", "회의", "Встреча", "会议", "會議"),
            ("entity.meeting.companycode", "رمز الشركة", "Company Code", "Código de empresa", "Code société", "会社コード", "회사 코드", "Код компании", "公司代码", "公司代碼"),
            ("entity.meeting.plantcode", "رمز المصنع", "Plant Code", "Código de planta", "Code usine", "工場コード", "공장 코드", "Код завода", "工厂代码", "工廠代碼"),
            ("entity.meeting.meetingtitle", "موضوع الاجتماع", "Meeting Title", "Título de reunión", "Titre réunion", "会議タイトル", "회의 제목", "Тема встречи", "会议主题", "會議主題"),
            ("entity.meeting.meetingtype", "نوع الاجتماع", "Meeting Type", "Tipo de reunión", "Type de réunion", "会議種別", "회의 유형", "Тип встречи", "会议类型", "會議類型"),
            ("entity.meeting.starttime", "وقت البدء", "Start Time", "Hora de inicio", "Heure de début", "開始日時", "시작 일시", "Время начала", "会议开始时间", "會議開始時間"),
            ("entity.meeting.endtime", "وقت الانتهاء", "End Time", "Hora de fin", "Heure de fin", "終了日時", "종료 일시", "Время окончания", "会议结束时间", "會議結束時間"),
            ("entity.meeting.isallday", "طوال اليوم", "All Day", "Todo el día", "Toute la journée", "終日", "종일", "Весь день", "是否全天", "是否全天"),
            ("entity.meeting.location", "مكان الاجتماع", "Location", "Lugar", "Lieu", "会議場所", "회의 장소", "Место проведения", "会议地点", "會議地點"),
            ("entity.meeting.organizerid", "معرف المنظم", "Organizer ID", "ID del organizador", "ID organisateur", "主催者ID", "주최자 ID", "ID организатора", "组织人ID", "組織人ID"),
            ("entity.meeting.organizername", "اسم المنظم", "Organizer Name", "Nombre del organizador", "Nom de l'organisateur", "主催者名", "주최자명", "Имя организатора", "组织人姓名", "組織人姓名"),
            ("entity.meeting.participantsummary", "ملخص الحضور", "Participant Summary", "Resumen de participantes", "Résumé participants", "参加者一覧", "참석자 요약", "Участники", "参与人摘要", "參與人摘要"),
            ("entity.meeting.remindminutes", "دقائق التذكير", "Remind Minutes", "Minutos de aviso", "Minutes de rappel", "リマインド（分）", "알림(분)", "Напоминание (мин)", "提前提醒分钟数", "提前提醒分鐘數"),
            ("entity.meeting.meetingstatus", "حالة الاجتماع", "Meeting Status", "Estado de reunión", "État réunion", "会議状態", "회의 상태", "Статус встречи", "会议状态", "會議狀態"),
            ("entity.meeting.meetingagenda", "جدول الأعمال", "Meeting Agenda", "Orden del día", "Ordre du jour", "会議アジェンダ", "회의 안건", "Повестка дня", "会议议程摘要", "會議議程摘要"),
            ("entity.meeting.ordernum", "الترتيب", "Sort Order", "Orden", "Ordre", "並び順", "정렬 순서", "Порядок", "排序号", "排序號"),

            // ----- TaktMeetingApply -----
            ("entity.meetingapply._self", "طلب اجتماع", "Meeting Apply", "Solicitud de reunión", "Demande de réunion", "会議申請", "회의 신청", "Заявка на встречу", "会议申请", "會議申請"),
            ("entity.meetingapply.companycode", "رمز الشركة", "Company Code", "Código de empresa", "Code société", "会社コード", "회사 코드", "Код компании", "公司代码", "公司代碼"),
            ("entity.meetingapply.plantcode", "رمز المصنع", "Plant Code", "Código de planta", "Code usine", "工場コード", "공장 코드", "Код завода", "工厂代码", "工廠代碼"),
            ("entity.meetingapply.applycode", "رمز الطلب", "Apply Code", "Código de solicitud", "Code demande", "申請番号", "신청 번호", "Код заявки", "申请单编码", "申請單編碼"),
            ("entity.meetingapply.instanceid", "معرف مثيل سير العمل", "Workflow Instance ID", "ID de instancia", "ID instance workflow", "ワークフローインスタンスID", "워크플로 인스턴스 ID", "ID экземпляра процесса", "工作流实例ID", "工作流實例ID"),
            ("entity.meetingapply.applystatus", "حالة الطلب", "Apply Status", "Estado de solicitud", "État demande", "申請状態", "신청 상태", "Статус заявки", "申请状态", "申請狀態"),
            ("entity.meetingapply.applicantid", "معرف مقدم الطلب", "Applicant ID", "ID del solicitante", "ID demandeur", "申請者ID", "신청자 ID", "ID заявителя", "申请人ID", "申請人ID"),
            ("entity.meetingapply.applicantname", "اسم مقدم الطلب", "Applicant Name", "Nombre del solicitante", "Nom du demandeur", "申請者名", "신청자명", "Имя заявителя", "申请人姓名", "申請人姓名"),
            ("entity.meetingapply.applicantdeptid", "معرف قسم مقدم الطلب", "Applicant Dept ID", "ID del departamento", "ID département demandeur", "申請部門ID", "신청 부서 ID", "ID отдела заявителя", "申请部门ID", "申請部門ID"),
            ("entity.meetingapply.applicantdeptname", "اسم قسم مقدم الطلب", "Applicant Dept Name", "Nombre del departamento", "Nom du département", "申請部門名", "신청 부서명", "Название отдела", "申请部门名称", "申請部門名稱"),
            ("entity.meetingapply.applytime", "وقت التقديم", "Apply Time", "Fecha de solicitud", "Date de demande", "申請日時", "신청 일시", "Время подачи", "申请时间", "申請時間"),
            ("entity.meetingapply.meetingtitle", "موضوع الاجتماع", "Meeting Title", "Título de reunión", "Titre réunion", "会議タイトル", "회의 제목", "Тема встречи", "会议主题", "會議主題"),
            ("entity.meetingapply.meetingtype", "نوع الاجتماع", "Meeting Type", "Tipo de reunión", "Type de réunion", "会議種別", "회의 유형", "Тип встречи", "会议类型", "會議類型"),
            ("entity.meetingapply.starttime", "وقت البدء", "Start Time", "Hora de inicio", "Heure de début", "開始日時", "시작 일시", "Время начала", "会议开始时间", "會議開始時間"),
            ("entity.meetingapply.endtime", "وقت الانتهاء", "End Time", "Hora de fin", "Heure de fin", "終了日時", "종료 일시", "Время окончания", "会议结束时间", "會議結束時間"),
            ("entity.meetingapply.isallday", "طوال اليوم", "All Day", "Todo el día", "Toute la journée", "終日", "종일", "Весь день", "是否全天", "是否全天"),
            ("entity.meetingapply.location", "مكان الاجتماع", "Location", "Lugar", "Lieu", "会議場所", "회의 장소", "Место проведения", "会议地点", "會議地點"),
            ("entity.meetingapply.participantsummary", "ملخص الحضور", "Participant Summary", "Resumen de participantes", "Résumé participants", "参加者一覧", "참석자 요약", "Участники", "参与人摘要", "參與人摘要"),
            ("entity.meetingapply.remindminutes", "دقائق التذكير", "Remind Minutes", "Minutos de aviso", "Minutes de rappel", "リマインド（分）", "알림(분)", "Напоминание (мин)", "提前提醒分钟数", "提前提醒分鐘數"),
            ("entity.meetingapply.meetingagenda", "جدول الأعمال", "Meeting Agenda", "Orden del día", "Ordre du jour", "会議アジェンダ", "회의 안건", "Повестка дня", "会议议程摘要", "會議議程摘要"),
            ("entity.meetingapply.meetingid", "معرف الاجتماع", "Meeting ID", "ID de reunión", "ID réunion", "会議ID", "회의 ID", "ID встречи", "会议实体ID", "會議實體ID"),

            // ----- TaktMeetingMinutes -----
            ("entity.meetingminutes._self", "محضر الاجتماع", "Meeting Minutes", "Acta de reunión", "Compte-rendu", "会議議事録", "회의록", "Протокол встречи", "会议纪要", "會議紀要"),
            ("entity.meetingminutes.companycode", "رمز الشركة", "Company Code", "Código de empresa", "Code société", "会社コード", "회사 코드", "Код компании", "公司代码", "公司代碼"),
            ("entity.meetingminutes.plantcode", "رمز المصنع", "Plant Code", "Código de planta", "Code usine", "工場コード", "공장 코드", "Код завода", "工厂代码", "工廠代碼"),
            ("entity.meetingminutes.meetingid", "معرف الاجتماع", "Meeting ID", "ID de reunión", "ID réunion", "会議ID", "회의 ID", "ID встречи", "会议申请ID", "會議申請ID"),
            ("entity.meetingminutes.minutestitle", "عنوان المحضر", "Minutes Title", "Título del acta", "Titre du CR", "議事録タイトル", "회의록 제목", "Заголовок протокола", "纪要标题", "紀要標題"),
            ("entity.meetingminutes.meetingtime", "وقت الاجتماع", "Meeting Time", "Fecha de reunión", "Date de réunion", "会議日時", "회의 일시", "Время встречи", "会议时间", "會議時間"),
            ("entity.meetingminutes.attendeesummary", "ملخص الحضور", "Attendee Summary", "Resumen de asistentes", "Résumé des présents", "参加者一覧", "참석자 요약", "Список участников", "参与人摘要", "參與人摘要"),
            ("entity.meetingminutes.recorderid", "معرف المسجل", "Recorder ID", "ID del redactor", "ID rédacteur", "記録者ID", "기록자 ID", "ID секретаря", "记录人ID", "記錄人ID"),
            ("entity.meetingminutes.recordername", "اسم المسجل", "Recorder Name", "Nombre del redactor", "Nom du rédacteur", "記録者名", "기록자명", "Имя секретаря", "记录人姓名", "記錄人姓名"),
            ("entity.meetingminutes.minutescontent", "محتوى المحضر", "Minutes Content", "Contenido del acta", "Contenu du CR", "議事録本文", "회의록 내용", "Содержание протокола", "纪要内容", "紀要內容"),
            ("entity.meetingminutes.conclusions", "القرارات", "Conclusions", "Conclusiones", "Conclusions", "決議事項", "결의 사항", "Решения", "决议摘要", "決議摘要"),
            ("entity.meetingminutes.actionitems", "المهام المعلقة", "Action Items", "Tareas pendientes", "Actions à faire", "アクション項目", "액션 항목", "Поручения", "待办事项摘要", "待辦事項摘要"),
            ("entity.meetingminutes.ordernum", "الترتيب", "Sort Order", "Orden", "Ordre", "並び順", "정렬 순서", "Порядок", "排序号", "排序號"),
        };

        var list = new List<TaktTranslation>();
        foreach (var e in entries)
        {
            var values = new[] { e.ar, e.en, e.es, e.fr, e.ja, e.ko, e.ru, e.zhCN, e.zhTW };
            for (var i = 0; i < cultures.Length; i++)
                list.Add(new TaktTranslation
                {
                    CultureCode = cultures[i],
                    ResourceKey = e.Key,
                    TranslationValue = values[i],
                    ResourceType = "Frontend",
                    ResourceGroup = "Entity",
                    OrderNum = 0
                });
        }
        return list;
    }
}
