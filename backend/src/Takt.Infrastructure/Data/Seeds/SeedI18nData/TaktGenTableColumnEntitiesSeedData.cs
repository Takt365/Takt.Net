// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktGenTableColumnEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktGenTableColumn 实体字段翻译种子数据，与 TaktUserEntitiesSeedData 风格一致，entity.gentablecolumn / entity.gentablecolumn.xxx，每个字段 9 种语言
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
/// TaktGenTableColumn 实体翻译种子数据（与 TaktGenTableColumn.cs 属性一一对应）
/// </summary>
public class TaktGenTableColumnEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在 TaktGenTableEntitiesSeedData 之后）
    /// </summary>
    public int Order => 22;

    /// <summary>
    /// 初始化代码生成字段配置实体翻译种子数据
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
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllGenTableColumnEntityTranslations();

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
    /// 获取所有 TaktGenTableColumn 实体名称及字段翻译（ResourceKey 拆分风格 entity.gentablecolumn / entity.gentablecolumn.xxx，与 TaktGenTableColumn.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllGenTableColumnEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.gentablecolumn（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "عمود جدول التوليد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "Code Gen Column", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "Columna de generación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "Colonne de génération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "コード生成カラム", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "코드 생성 컬럼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "Колонка генерации", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "代码生成字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn._self", TranslationValue = "代碼生成字段配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.tableid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "معرف الجدول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "Table ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "ID de tabla", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "ID de table", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "テーブルID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "테이블 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "ID таблицы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "表ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.tableid", TranslationValue = "表ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.databasecolumnname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "اسم العمود", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "Column Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "Nombre de columna", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "Nom de colonne", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "列名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "컬럼 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "Имя столбца", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "列名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.databasecolumnname", TranslationValue = "列名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.columncomment
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "وصف العمود", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "Column Comment", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "Comentario de columna", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "Commentaire de colonne", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "列コメント", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "컬럼 설명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "Комментарий столбца", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "列描述", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.columncomment", TranslationValue = "列描述", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.databasedatatype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "نوع البيانات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "Data Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "Tipo de datos", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "Type de données", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "データ型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "데이터 형식", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "Тип данных", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "数据类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.databasedatatype", TranslationValue = "數據類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.csharpdatatype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "نوع C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "C# Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "Tipo C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "Type C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "C#型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "C# 타입", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "Тип C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "C#类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.csharpdatatype", TranslationValue = "C#類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.csharpcolumnname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "اسم عمود C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "C# Column Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "Nombre columna C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "Nom colonne C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "C#列名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "C# 컬럼명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "Имя столбца C#", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "C#列名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.csharpcolumnname", TranslationValue = "C#列名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.length
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "طول البيانات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "Length", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "Longitud", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "Longueur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "データ長", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "길이", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "Длина", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "数据长度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.length", TranslationValue = "數據長度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.decimaldigits
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "دقة البيانات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "Decimal Digits", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "Decimales", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "Décimales", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "小数点桁数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "소수 자리", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "Знаков после запятой", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "数据精度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.decimaldigits", TranslationValue = "數據精度", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.ispk
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "مفتاح أساسي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "Primary Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "Clave primaria", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "Clé primaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "主キー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "기본 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "Первичный ключ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "是否主键", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.ispk", TranslationValue = "是否主鍵", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.isincrement
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "تلقائي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "Auto Increment", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "Autoincremento", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "Auto-incrément", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "自動増分", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "자동 증가", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "Автоинкремент", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "是否自增", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.isincrement", TranslationValue = "是否自增", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.isrequired
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "مطلوب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "Required", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "Obligatorio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "Requis", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "必須", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "필수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "Обязательно", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "是否必填", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.isrequired", TranslationValue = "是否必填", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.iscreate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "حقل إدراج", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "Insert Field", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "Campo de inserción", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "Champ d'insertion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "新增項目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "추가 필드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "Поле вставки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "是否新增", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.iscreate", TranslationValue = "是否新增", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.isupdate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "حقل تحديث", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "Update Field", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "Campo de actualización", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "Champ de mise à jour", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "更新項目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "수정 필드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "Поле обновления", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "是否更新", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.isupdate", TranslationValue = "是否更新", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.islist
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "عمود القائمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "List Column", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "Columna de lista", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "Colonne liste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "リスト列", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "목록 컬럼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "Столбец списка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "是否列表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.islist", TranslationValue = "是否列表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.isexport
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "تصدير", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "Export", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "Exportar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "Exporter", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "エクスポート", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "내보내기", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "Экспорт", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "是否导出", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.isexport", TranslationValue = "是否導出", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.issort
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "Sort", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "Ordenar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "Tri", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "ソート", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "Сортировка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "是否排序", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.issort", TranslationValue = "是否排序", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.isquery
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "حقل استعلام", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "Query Field", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "Campo de consulta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "Champ de requête", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "検索項目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "조회 필드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "Поле запроса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "是否查询", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.isquery", TranslationValue = "是否查詢", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.querytype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "طريقة الاستعلام", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "Query Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "Tipo de consulta", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "Type de requête", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "検索方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "조회 방식", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "Тип запроса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "查询方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.querytype", TranslationValue = "查詢方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.htmltype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "نوع العرض", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "Display Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "Tipo de visualización", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "Type d'affichage", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "表示タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "표시 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "Тип отображения", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "显示类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.htmltype", TranslationValue = "顯示類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.dicttype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "نوع القاموس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "Dict Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "Tipo de diccionario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "Type de dictionnaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "辞書タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "딕셔너리 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "Тип словаря", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "字典类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.dicttype", TranslationValue = "字典類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentablecolumn.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "排序序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentablecolumn.ordernum", TranslationValue = "排序序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
