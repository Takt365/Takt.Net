// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDocumentEntitiesSeedData.cs
// 创建时间：2025-02-25
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心（DocsCenter）实体翻译种子：TaktDocument、TaktDocumentReceipt、TaktDocumentHistory，entity.document / entity.documentreceipt / entity.documenthistory，9 种语言
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
/// 文控中心实体翻译种子数据（TaktDocument、TaktDocumentReceipt、TaktDocumentHistory 与实体属性一一对应）
/// </summary>
public class TaktDocumentEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllDocsCenterEntityTranslations();

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
    /// 获取文控中心所有实体名称及字段翻译（entity.document / entity.documentreceipt / entity.documenthistory 及各自属性）
    /// </summary>
    private static List<TaktTranslation> GetAllDocsCenterEntityTranslations()
    {
        var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };

        var entries = new List<(string Key, string ar, string en, string es, string fr, string ja, string ko, string ru, string zhCN, string zhTW)>
        {
            // ----- TaktDocument -----
            ("entity.document._self", "وثيقة", "Document", "Documento", "Document", "文書", "문서", "Документ", "文档", "文檔"),
            ("entity.document.companycode", "رمز الشركة", "Company Code", "Código de empresa", "Code société", "会社コード", "회사 코드", "Код компании", "公司代码", "公司代碼"),
            ("entity.document.plantcode", "رمز المصنع", "Plant Code", "Código de planta", "Code usine", "工場コード", "공장 코드", "Код завода", "工厂代码", "工廠代碼"),
            ("entity.document.documentcode", "رمز الوثيقة", "Document Code", "Código del documento", "Code document", "文書コード", "문서 코드", "Код документа", "文档编码", "文檔編碼"),
            ("entity.document.documenttitle", "عنوان الوثيقة", "Document Title", "Título del documento", "Titre du document", "文書タイトル", "문서 제목", "Название документа", "文档标题", "文檔標題"),
            ("entity.document.documenttype", "نوع الوثيقة", "Document Type", "Tipo de documento", "Type de document", "文書種別", "문서 유형", "Тип документа", "文档类型", "文檔類型"),
            ("entity.document.documentversion", "إصدار الوثيقة", "Document Version", "Versión del documento", "Version du document", "文書バージョン", "문서 버전", "Версия документа", "文档版本号", "文檔版本號"),
            ("entity.document.instanceid", "معرف مثيل سير العمل", "Workflow Instance ID", "ID de instancia de flujo", "ID instance workflow", "ワークフローインスタンスID", "워크플로 인스턴스 ID", "ID экземпляра процесса", "工作流实例ID", "工作流實例ID"),
            ("entity.document.documentstatus", "حالة الوثيقة", "Document Status", "Estado del documento", "État du document", "文書状態", "문서 상태", "Статус документа", "文档状态", "文檔狀態"),
            ("entity.document.applicantid", "معرف مقدم الطلب", "Applicant ID", "ID del solicitante", "ID demandeur", "申請者ID", "신청자 ID", "ID заявителя", "申请人ID", "申請人ID"),
            ("entity.document.applicantname", "اسم مقدم الطلب", "Applicant Name", "Nombre del solicitante", "Nom du demandeur", "申請者名", "신청자명", "Имя заявителя", "申请人姓名", "申請人姓名"),
            ("entity.document.applicantdeptid", "معرف قسم مقدم الطلب", "Applicant Dept ID", "ID del departamento del solicitante", "ID département demandeur", "申請部門ID", "신청 부서 ID", "ID отдела заявителя", "申请部门ID", "申請部門ID"),
            ("entity.document.applicantdeptname", "اسم قسم مقدم الطلب", "Applicant Dept Name", "Nombre del departamento del solicitante", "Nom du département demandeur", "申請部門名", "신청 부서명", "Название отдела заявителя", "申请部门名称", "申請部門名稱"),
            ("entity.document.applytime", "وقت التقديم", "Apply Time", "Fecha de solicitud", "Date de demande", "申請日時", "신청 일시", "Время подачи", "申请时间", "申請時間"),
            ("entity.document.approvedtime", "وقت الموافقة", "Approved Time", "Fecha de aprobación", "Date d'approbation", "承認日時", "승인 일시", "Время утверждения", "批准时间", "批准時間"),
            ("entity.document.fileid", "معرف الملف", "File ID", "ID del archivo", "ID fichier", "ファイルID", "파일 ID", "ID файла", "关联文件ID", "關聯檔案ID"),
            ("entity.document.direction", "اتجاه الإصدار/الاستلام", "Direction", "Dirección (emisión/recepción)", "Sens (émission/réception)", "収発方向", "수발 방향", "Направление", "收发文方向", "收發文方向"),
            ("entity.document.documentcategory", "نوع الوثيقة", "Document Category", "Categoría del documento", "Catégorie du document", "文種", "문서 유형", "Категория документа", "文种", "文種"),
            ("entity.document.lifecyclestage", "مرحلة دورة الحياة", "Lifecycle Stage", "Etapa del ciclo de vida", "Étape du cycle de vie", "ライフサイクル段階", "생명주기 단계", "Этап жизненного цикла", "生命周期阶段", "生命週期階段"),
            ("entity.document.retentionyears", "مدة الحفظ (سنوات)", "Retention Years", "Años de retención", "Durée de conservation (années)", "保管期間（年）", "보관 기간(년)", "Срок хранения (лет)", "保管期限年", "保管期限年"),
            ("entity.document.effectivetime", "وقت السريان", "Effective Time", "Fecha de vigencia", "Date d'effet", "発効日時", "시행 일시", "Время вступления в силу", "生效时间", "生效時間"),
            ("entity.document.archivetime", "وقت الأرشفة", "Archive Time", "Fecha de archivo", "Date d'archivage", "アーカイブ日時", "보관 일시", "Время архивации", "归档时间", "歸檔時間"),
            ("entity.document.obsoletetime", "وقت الإلغاء", "Obsolete Time", "Fecha de obsolescencia", "Date d'obsoletion", "廃止日時", "폐기 일시", "Время устаревания", "作废时间", "作廢時間"),

            // ----- TaktDocumentReceipt -----
            ("entity.documentreceipt._self", "إيصال الوثيقة", "Document Receipt", "Recibo de documento", "Réception de document", "文書受領", "문서 수령", "Получение документа", "文档签收", "文檔簽收"),
            ("entity.documentreceipt.documentid", "معرف الوثيقة", "Document ID", "ID del documento", "ID document", "文書ID", "문서 ID", "ID документа", "文档ID", "文檔ID"),
            ("entity.documentreceipt.receipttype", "نوع الاستلام", "Receipt Type", "Tipo de recibo", "Type de réception", "受領種別", "수령 유형", "Тип получения", "签收类型", "簽收類型"),
            ("entity.documentreceipt.signstatus", "حالة التوقيع", "Sign Status", "Estado de firma", "État de signature", "署名状態", "서명 상태", "Статус подписи", "签收状态", "簽收狀態"),
            ("entity.documentreceipt.comment", "ملاحظة الاستلام", "Comment", "Comentario", "Commentaire", "受領備考", "수령 비고", "Комментарий", "签收备注", "簽收備註"),

            // ----- TaktDocumentHistory -----
            ("entity.documenthistory._self", "سجل تغيير الوثيقة", "Document History", "Historial del documento", "Historique du document", "文書変更履歴", "문서 변경 이력", "История документа", "文档变更记录", "文檔變更記錄"),
            ("entity.documenthistory.documentid", "معرف الوثيقة", "Document ID", "ID del documento", "ID document", "文書ID", "문서 ID", "ID документа", "文档ID", "文檔ID"),
            ("entity.documenthistory.documentcode", "رمز الوثيقة", "Document Code", "Código del documento", "Code document", "文書コード", "문서 코드", "Код документа", "文档编码", "文檔編碼"),
            ("entity.documenthistory.changefield", "الحقل المُغيَّر", "Change Field", "Campo modificado", "Champ modifié", "変更項目", "변경 항목", "Изменённое поле", "变更字段名", "變更欄位名"),
            ("entity.documenthistory.oldvalue", "القيمة القديمة", "Old Value", "Valor anterior", "Ancienne valeur", "変更前値", "변경 전 값", "Старое значение", "原值", "原值"),
            ("entity.documenthistory.newvalue", "القيمة الجديدة", "New Value", "Valor nuevo", "Nouvelle valeur", "変更後値", "변경 후 값", "Новое значение", "新值", "新值"),
            ("entity.documenthistory.changereason", "سبب التغيير", "Change Reason", "Motivo del cambio", "Raison du changement", "変更理由", "변경 사유", "Причина изменения", "变更原因", "變更原因"),
        };

        var list = new List<TaktTranslation>();
        foreach (var e in entries)
        {
            var values = new[] { e.ar, e.en, e.es, e.fr, e.ja, e.ko, e.ru, e.zhCN, e.zhTW };
            for (var i = 0; i < cultures.Length; i++)
            {
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
        }

        return list;
    }
}
