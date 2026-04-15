// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDeptI18nSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门本地化种子数据，为 TaktDeptSeedData 中的部门编码创建翻译数据（ResourceKey = dept.{deptcode}，翻译键全部小写）
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
/// Takt部门本地化种子数据
/// </summary>
public class TaktDeptI18nSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（部门本地化应在部门、语言之后初始化）
    /// </summary>
    public int Order => 8;

    /// <summary>
    /// 初始化部门本地化种子数据
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
            var languages = await languageRepository.FindAsync(l =>
                l.LanguageStatus == 0 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllDeptTranslations();

            foreach (var t in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(t.CultureCode, out var languageId))
                    continue;

                var existing = await translationRepository.GetAsync(x =>
                    x.ResourceKey == t.ResourceKey &&
                    x.CultureCode == t.CultureCode &&
                    x.IsDeleted == 0);

                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = t.CultureCode,
                        ResourceKey = t.ResourceKey,
                        TranslationValue = t.TranslationValue,
                        ResourceType = t.ResourceType,
                        ResourceGroup = t.ResourceGroup,
                        OrderNum = t.OrderNum,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
                else
                {
                    if (existing.TranslationValue != t.TranslationValue ||
                        existing.ResourceType != t.ResourceType ||
                        existing.ResourceGroup != t.ResourceGroup)
                    {
                        existing.LanguageId = languageId;
                        existing.TranslationValue = t.TranslationValue;
                        existing.ResourceType = t.ResourceType;
                        existing.ResourceGroup = t.ResourceGroup;
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
    /// 获取所有部门翻译数据（与 TaktDeptSeedData 的 DeptCode 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllDeptTranslations()
    {
        const string resourceType = "Frontend";
        const string resourceGroup = "Dept";
        const int orderNum = 0;

        var list = new List<TaktTranslation>();

        // 辅助方法：添加单个部门的9种语言翻译。翻译键（ResourceKey）统一小写。
        void AddDeptTranslations(string deptCode, Dictionary<string, string> translations)
        {
            var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
            var resourceKey = $"dept.{deptCode.ToLowerInvariant()}".ToLowerInvariant();
            foreach (var culture in cultures)
            {
                if (translations.TryGetValue(culture, out var value))
                {
                    list.Add(new TaktTranslation
                    {
                        CultureCode = culture,
                        ResourceKey = resourceKey,
                        TranslationValue = value,
                        ResourceType = resourceType,
                        ResourceGroup = resourceGroup,
                        OrderNum = orderNum
                    });
                }
            }
        }

        void Add9(string code, string en, string zhcn, string zhtw, string ja, string ko, string ar, string es, string fr, string ru)
        {
            AddDeptTranslations(code, new Dictionary<string, string>
            {
                { "en-US", en }, { "zh-CN", zhcn }, { "zh-TW", zhtw },
                { "ja-JP", ja }, { "ko-KR", ko },
                { "ar-SA", ar }, { "es-ES", es }, { "fr-FR", fr }, { "ru-RU", ru }
            });
        }

        // dept.head_office 总公司 TEAC
        AddDeptTranslations("HEAD_OFFICE", new Dictionary<string, string>
        {
            { "en-US", "TEAC" }, { "zh-CN", "TEAC" }, { "zh-TW", "TEAC" },
            { "ja-JP", "TEAC" }, { "ko-KR", "TEAC" },
            { "ar-SA", "TEAC" }, { "es-ES", "TEAC" }, { "fr-FR", "TEAC" }, { "ru-RU", "TEAC" }
        });

        // —— DTA 组织（DeptCode 与 TaktDeptSeedData 一致）——
        Add9("D0000", "DTA", "DTA", "DTA", "DTA", "DTA", "DTA", "DTA", "DTA", "DTA");
        Add9("D1000", "General Manager's Room", "总经理室", "總經理室", "総経理室", "총경리실", "غرفة المدير العام", "Oficina del Director General", "Bureau du Directeur Général", "Кабинет генерального директора");
        Add9("D0100", "General Affairs Dept", "总务部", "總務部", "総務部", "총무부", "الشؤون العامة", "Asuntos Generales", "Affaires Générales", "Общий отдел");
        Add9("D0110", "General Affairs Section", "总务课", "總務課", "総務課", "총무과", "قسم الشؤون العامة", "Sección de Asuntos Generales", "Section Affaires Générales", "Отдел общих дел");
        Add9("D0200", "Finance Dept", "财务部", "財務部", "財務部", "재무부", "المالية", "Finanzas", "Finance", "Финансы");
        Add9("D0210", "Finance Section", "财务课", "財務課", "財務課", "재무과", "قسم المالية", "Sección de Finanzas", "Section Finance", "Финансовый отдел");
        Add9("D0300", "IT Dept", "IT部", "IT部", "IT部", "IT부", "تقنية المعلومات", "Departamento IT", "Service IT", "ИТ-отдел");
        Add9("D0310", "IT Section", "电脑课", "電腦課", "コンピューター課", "컴퓨터과", "قسم الحاسوب", "Sección de Informática", "Section Informatique", "Компьютерный отдел");
        Add9("D0400", "Management Dept", "管理部", "管理部", "管理部", "관리부", "الإدارة", "Gestión", "Gestion", "Управление");
        Add9("D0410", "Customhouse Section", "报关课", "報關課", "通関課", "통관과", "قسم الجمارك", "Sección de Aduanas", "Section Douanes", "Таможня");
        Add9("D0420", "Production Management Section", "生管课", "生管課", "生管課", "생관과", "قسم التخطيط", "Sección PMC", "Section PC", "Отдел ПУП");
        Add9("D0430", "Materials Control Section", "部管课", "部管課", "部管課", "부관과", "قسم المواد", "Sección de Materiales", "Section Matériaux", "Отдел материалов");
        Add9("D0500", "Materials Dept", "资材部", "資材部", "資材部", "자재부", "المواد", "Materiales", "Matériaux", "Снабжение");
        Add9("D0510", "Purchase Section", "采购课", "採購課", "購買課", "구매과", "قسم المشتريات", "Sección de Compras", "Section Achats", "Отдел закупок");
        Add9("D0600", "Production Dept", "生产部", "生產部", "生産部", "생산부", "الإنتاج", "Producción", "Production", "Производство");
        Add9("D0610", "Production Section 1", "制造1课", "製造1課", "製造1課", "제조1과", "التصنيع 1", "Sección Producción 1", "Section Production 1", "Участок 1");
        Add9("D0620", "Production Section 2", "制造2课", "製造2課", "製造2課", "제조2과", "التصنيع 2", "Sección Producción 2", "Section Production 2", "Участок 2");
        Add9("D0621", "SMT", "SMT", "SMT", "SMT", "SMT", "SMT", "SMT", "SMT", "SMT");
        Add9("D0622", "Automatic Insertion", "自插", "自插", "自挿入", "자동삽입", "إدراج تلقائي", "Inserción automática", "Insertion automatique", "Автовставка");
        Add9("D0623", "Revision", "修正", "修正", "修正", "수정", "تصحيح", "Corrección", "Correction", "Ремонт");
        Add9("D0624", "Manual Insertion", "手插", "手插", "手挿入", "수동삽입", "إدراج يدوي", "Inserción manual", "Insertion manuelle", "Ручная вставка");
        Add9("D0625", "Materials", "物料", "物料", "物料", "자재", "المواد", "Materiales", "Matériaux", "Материалы");
        Add9("D0626", "PS2 Indirect", "制造2课-间接", "製造2課-間接", "製造2課-間接", "제조2과-간접", "غير مباشر", "Indirecto", "Indirect", "Косвенный");
        Add9("D0630", "Production Engineering Section", "制造技术课", "製造技術課", "製造技術課", "제조기술과", "هندسة الإنتاج", "Sección Ingeniería", "Section Ingénierie", "Производственная инженерия");
        Add9("D0700", "Engineering Dept", "技术部", "技術部", "技術部", "기술부", "الهندسة", "Ingeniería", "Ingénierie", "Технический отдел");
        Add9("D0710", "Engineering Section", "技术课", "技術課", "技術課", "기술과", "قسم الهندسة", "Sección de Ingeniería", "Section Technique", "Техотдел");
        Add9("D0800", "Quality Assurance Dept", "品保部", "品保部", "品質保証部", "품질보증부", "الجودة", "Calidad", "Qualité", "ОТК");
        Add9("D0810", "IQC Section", "受检课", "受檢課", "受検課", "수검과", "الفحص الوارد", "IQC", "IQC", "Входной контроль");
        Add9("D0820", "QA Section", "品管课", "品管課", "品質管理課", "품질관리과", "مراقبة الجودة", "Control de Calidad", "Contrôle Qualité", "ОТК участок");
        Add9("D0900", "OEM Dept", "OEM部", "OEM部", "OEM部", "OEM부", "OEM", "OEM", "OEM", "OEM");
        Add9("D0910", "OEM QA Section", "OEM QA课", "OEM QA課", "OEM QA課", "OEM QA과", "OEM QA", "OEM QA", "OEM QA", "OEM ОТК");
        Add9("D0920", "OEM Management Section", "OEM管理课", "OEM管理課", "OEM管理課", "OEM 관리과", "OEM إدارة", "OEM Gestión", "OEM Gestion", "OEM управление");

        return list;
    }
}
