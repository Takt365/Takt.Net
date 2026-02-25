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
using Takt.Domain.Entities.Routine.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds;

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

        // dept.head_office [室] 总公司 TEAC
        AddDeptTranslations("HEAD_OFFICE", new Dictionary<string, string>
        {
            { "en-US", "TEAC" }, { "zh-CN", "TEAC" }, { "zh-TW", "TEAC" },
            { "ja-JP", "TEAC" }, { "ko-KR", "TEAC" },
            { "ar-SA", "TEAC" }, { "es-ES", "TEAC" }, { "fr-FR", "TEAC" }, { "ru-RU", "TEAC" }
        });

        // dept.branch_office [室] 分公司 DTA
        AddDeptTranslations("BRANCH_OFFICE", new Dictionary<string, string>
        {
            { "en-US", "DTA" }, { "zh-CN", "DTA" }, { "zh-TW", "DTA" },
            { "ja-JP", "DTA" }, { "ko-KR", "DTA" },
            { "ar-SA", "DTA" }, { "es-ES", "DTA" }, { "fr-FR", "DTA" }, { "ru-RU", "DTA" }
        });

        // dept.general_office [室] 总经室
        AddDeptTranslations("GENERAL_OFFICE", new Dictionary<string, string>
        {
            { "en-US", "General Office" }, { "zh-CN", "总经室" }, { "zh-TW", "總經室" },
            { "ja-JP", "総経室" }, { "ko-KR", "총경실" },
            { "ar-SA", "المكتب العام" }, { "es-ES", "Oficina General" }, { "fr-FR", "Bureau Général" }, { "ru-RU", "Генеральный офис" }
        });

        // dept.business_promotion_headquarters [本部] 事业推进本部
        AddDeptTranslations("BUSINESS_PROMOTION_HEADQUARTERS", new Dictionary<string, string>
        {
            { "en-US", "Business Promotion Headquarters" }, { "zh-CN", "事业推进本部" }, { "zh-TW", "事業推進本部" },
            { "ja-JP", "事業推進本部" }, { "ko-KR", "사업 추진 본부" },
            { "ar-SA", "مقر تعزيز الأعمال" }, { "es-ES", "Sede de Promoción de Negocios" }, { "fr-FR", "Siège de Promotion des Affaires" }, { "ru-RU", "Штаб-квартира по продвижению бизнеса" }
        });

        // dept.manufacturing_improvement_headquarters [本部] 生产改善推进本部
        AddDeptTranslations("MANUFACTURING_IMPROVEMENT_HEADQUARTERS", new Dictionary<string, string>
        {
            { "en-US", "Production Improvement Headquarters" }, { "zh-CN", "生产改善推进本部" }, { "zh-TW", "生產改善推進本部" },
            { "ja-JP", "生産改善推進本部" }, { "ko-KR", "생산 개선 추진 본부" },
            { "ar-SA", "مقر تحسين الإنتاج" }, { "es-ES", "Sede de Mejora de Producción" }, { "fr-FR", "Siège d'Amélioration de la Production" }, { "ru-RU", "Штаб-квартира по улучшению производства" }
        });

        // dept.finance_dept [部] 财务部
        AddDeptTranslations("FINANCE_DEPT", new Dictionary<string, string>
        {
            { "en-US", "Finance" }, { "zh-CN", "财务部" }, { "zh-TW", "財務部" },
            { "ja-JP", "財務部" }, { "ko-KR", "재무부" },
            { "ar-SA", "المالية" }, { "es-ES", "Finanzas" }, { "fr-FR", "Finance" }, { "ru-RU", "Финансы" }
        });

        // dept.finance_section [课] 财务课
        AddDeptTranslations("FINANCE_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Finance Section" }, { "zh-CN", "财务课" }, { "zh-TW", "財務課" },
            { "ja-JP", "財務課" }, { "ko-KR", "재무과" },
            { "ar-SA", "قسم المالية" }, { "es-ES", "Sección de Finanzas" }, { "fr-FR", "Section Finance" }, { "ru-RU", "Финансовый отдел" }
        });

        // dept.it_dept [部] IT部
        AddDeptTranslations("IT_DEPT", new Dictionary<string, string>
        {
            { "en-US", "IT Department" }, { "zh-CN", "IT部" }, { "zh-TW", "IT部" },
            { "ja-JP", "IT部門" }, { "ko-KR", "IT부서" },
            { "ar-SA", "قسم تقنية المعلومات" }, { "es-ES", "Departamento IT" }, { "fr-FR", "Service IT" }, { "ru-RU", "ИТ-отдел" }
        });

        // dept.computer_section [课] 电脑课
        AddDeptTranslations("COMPUTER_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Computer Section" }, { "zh-CN", "电脑课" }, { "zh-TW", "電腦課" },
            { "ja-JP", "コンピューター課" }, { "ko-KR", "컴퓨터과" },
            { "ar-SA", "قسم الحاسوب" }, { "es-ES", "Sección de Computadoras" }, { "fr-FR", "Section Informatique" }, { "ru-RU", "Компьютерный отдел" }
        });

        // dept.general_affairs_dept [部] 总务部
        AddDeptTranslations("GENERAL_AFFAIRS_DEPT", new Dictionary<string, string>
        {
            { "en-US", "General Affairs" }, { "zh-CN", "总务部" }, { "zh-TW", "總務部" },
            { "ja-JP", "総務部" }, { "ko-KR", "총무부" },
            { "ar-SA", "الشؤون العامة" }, { "es-ES", "Asuntos Generales" }, { "fr-FR", "Affaires Générales" }, { "ru-RU", "Общие вопросы" }
        });

        // dept.general_affairs_section [课] 总务课
        AddDeptTranslations("GENERAL_AFFAIRS_SECTION", new Dictionary<string, string>
        {
            { "en-US", "General Affairs Section" }, { "zh-CN", "总务课" }, { "zh-TW", "總務課" },
            { "ja-JP", "総務課" }, { "ko-KR", "총무과" },
            { "ar-SA", "قسم الشؤون العامة" }, { "es-ES", "Sección de Asuntos Generales" }, { "fr-FR", "Section des Affaires Générales" }, { "ru-RU", "Отдел общих вопросов" }
        });

        // dept.management_dept [部] 管理部
        AddDeptTranslations("MANAGEMENT_DEPT", new Dictionary<string, string>
        {
            { "en-US", "Management" }, { "zh-CN", "管理部" }, { "zh-TW", "管理部" },
            { "ja-JP", "管理部" }, { "ko-KR", "관리부" },
            { "ar-SA", "الإدارة" }, { "es-ES", "Gestión" }, { "fr-FR", "Gestion" }, { "ru-RU", "Управление" }
        });

        // dept.customs_section [课] 报关课
        AddDeptTranslations("CUSTOMS_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Customs Section" }, { "zh-CN", "报关课" }, { "zh-TW", "報關課" },
            { "ja-JP", "通関課" }, { "ko-KR", "통관과" },
            { "ar-SA", "قسم الجمارك" }, { "es-ES", "Sección de Aduanas" }, { "fr-FR", "Section des Douanes" }, { "ru-RU", "Таможенный отдел" }
        });

        // dept.manufacturing_control_section [课] 生管课
        AddDeptTranslations("MANUFACTURING_CONTROL_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Production Control Section" }, { "zh-CN", "生管课" }, { "zh-TW", "生管課" },
            { "ja-JP", "生管課" }, { "ko-KR", "생관과" },
            { "ar-SA", "قسم مراقبة الإنتاج" }, { "es-ES", "Sección de Control de Producción" }, { "fr-FR", "Section de Contrôle de Production" }, { "ru-RU", "Отдел контроля производства" }
        });

        // dept.materials_management_section [课] 部管课（Materials Management）
        AddDeptTranslations("MATERIALS_MANAGEMENT_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Materials Management Section" }, { "zh-CN", "部管课" }, { "zh-TW", "部管課" },
            { "ja-JP", "部管課" }, { "ko-KR", "부관과" },
            { "ar-SA", "قسم إدارة المواد" }, { "es-ES", "Sección de Gestión de Materiales" }, { "fr-FR", "Section Gestion des Matériaux" }, { "ru-RU", "Отдел управления материалами" }
        });

        // dept.procurement_dept [部] 资材部（下级 PURCHASING_SECTION）
        AddDeptTranslations("PROCUREMENT_DEPT", new Dictionary<string, string>
        {
            { "en-US", "Procurement Department" }, { "zh-CN", "资材部" }, { "zh-TW", "資材部" },
            { "ja-JP", "資材部" }, { "ko-KR", "자재부" },
            { "ar-SA", "المواد" }, { "es-ES", "Materiales" }, { "fr-FR", "Matériaux" }, { "ru-RU", "Материалы" }
        });

        // dept.purchasing_section [课] 采购课
        AddDeptTranslations("PURCHASING_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Purchasing Section" }, { "zh-CN", "采购课" }, { "zh-TW", "採購課" },
            { "ja-JP", "購買課" }, { "ko-KR", "구매과" },
            { "ar-SA", "قسم المشتريات" }, { "es-ES", "Sección de Compras" }, { "fr-FR", "Section des Achats" }, { "ru-RU", "Отдел закупок" }
        });

        // dept.technology_dept [部] 技术部
        AddDeptTranslations("TECHNOLOGY_DEPT", new Dictionary<string, string>
        {
            { "en-US", "Technology" }, { "zh-CN", "技术部" }, { "zh-TW", "技術部" },
            { "ja-JP", "技術部" }, { "ko-KR", "기술부" },
            { "ar-SA", "التكنولوجيا" }, { "es-ES", "Tecnología" }, { "fr-FR", "Technologie" }, { "ru-RU", "Технология" }
        });

        // dept.technology_section [课] 技术课
        AddDeptTranslations("TECHNOLOGY_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Technology Section" }, { "zh-CN", "技术课" }, { "zh-TW", "技術課" },
            { "ja-JP", "技術課" }, { "ko-KR", "기술과" },
            { "ar-SA", "قسم التكنولوجيا" }, { "es-ES", "Sección de Tecnología" }, { "fr-FR", "Section Technologie" }, { "ru-RU", "Технологический отдел" }
        });

        // dept.manufacturing_dept [部] 生产部
        AddDeptTranslations("MANUFACTURING_DEPT", new Dictionary<string, string>
        {
            { "en-US", "Production" }, { "zh-CN", "生产部" }, { "zh-TW", "生產部" },
            { "ja-JP", "生産部" }, { "ko-KR", "생산부" },
            { "ar-SA", "الإنتاج" }, { "es-ES", "Producción" }, { "fr-FR", "Production" }, { "ru-RU", "Производство" }
        });

        // dept.manufacturing_section_1 [课] 制造一课
        AddDeptTranslations("MANUFACTURING_SECTION_1", new Dictionary<string, string>
        {
            { "en-US", "Manufacturing Section 1" }, { "zh-CN", "制造一课" }, { "zh-TW", "製造一課" },
            { "ja-JP", "製造一課" }, { "ko-KR", "제조1과" },
            { "ar-SA", "قسم التصنيع 1" }, { "es-ES", "Sección de Fabricación 1" }, { "fr-FR", "Section de Fabrication 1" }, { "ru-RU", "Производственный отдел 1" }
        });

        // dept.manufacturing_section_2 [课] 制造二课
        AddDeptTranslations("MANUFACTURING_SECTION_2", new Dictionary<string, string>
        {
            { "en-US", "Manufacturing Section 2" }, { "zh-CN", "制造二课" }, { "zh-TW", "製造二課" },
            { "ja-JP", "製造二課" }, { "ko-KR", "제조2과" },
            { "ar-SA", "قسم التصنيع 2" }, { "es-ES", "Sección de Fabricación 2" }, { "fr-FR", "Section de Fabrication 2" }, { "ru-RU", "Производственный отдел 2" }
        });

        // dept.manufacturing_technology_section [课] 制造技术课
        AddDeptTranslations("MANUFACTURING_TECHNOLOGY_SECTION", new Dictionary<string, string>
        {
            { "en-US", "Manufacturing Technology Section" }, { "zh-CN", "制造技术课" }, { "zh-TW", "製造技術課" },
            { "ja-JP", "製造技術課" }, { "ko-KR", "제조기술과" },
            { "ar-SA", "قسم تكنولوجيا التصنيع" }, { "es-ES", "Sección de Tecnología de Fabricación" }, { "fr-FR", "Section Technologie de Fabrication" }, { "ru-RU", "Отдел производственных технологий" }
        });

        // dept.quality_dept [部] 品保部
        AddDeptTranslations("QUALITY_DEPT", new Dictionary<string, string>
        {
            { "en-US", "Quality" }, { "zh-CN", "品保部" }, { "zh-TW", "品保部" },
            { "ja-JP", "品質保証部" }, { "ko-KR", "품질보증부" },
            { "ar-SA", "الجودة" }, { "es-ES", "Calidad" }, { "fr-FR", "Qualité" }, { "ru-RU", "Качество" }
        });

        // dept.incoming_quality_control_section [课] 受检课（受検課）Incoming Quality Control
        AddDeptTranslations("INCOMING_QUALITY_CONTROL_SECTION", new Dictionary<string, string>
        {
            { "en-US", "IQC" }, { "zh-CN", "受检课" }, { "zh-TW", "受檢課" },
            { "ja-JP", "受検課" }, { "ko-KR", "수검과" },
            { "ar-SA", "قسم التفتيش" }, { "es-ES", "Sección de Inspección" }, { "fr-FR", "Section d'Inspection" }, { "ru-RU", "Отдел инспекции" }
        });

        // dept.quality_assurance_section [课] 品管课 Quality Assurance
        AddDeptTranslations("QUALITY_ASSURANCE_SECTION", new Dictionary<string, string>
        {
            { "en-US", "QA" }, { "zh-CN", "品管课" }, { "zh-TW", "品管課" },
            { "ja-JP", "品質管理課" }, { "ko-KR", "품질관리과" },
            { "ar-SA", "قسم مراقبة الجودة" }, { "es-ES", "Sección de Control de Calidad" }, { "fr-FR", "Section Contrôle Qualité" }, { "ru-RU", "Отдел контроля качества" }
        });

        return list;
    }
}
