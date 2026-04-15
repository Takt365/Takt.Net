// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktGenTableEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktGenTable 实体字段翻译种子数据，与 TaktUserEntitiesSeedData 风格一致，entity.gentable / entity.gentable.xxx，每个字段 9 种语言
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
/// TaktGenTable 实体翻译种子数据（与 TaktGenTable.cs 属性一一对应）
/// </summary>
public class TaktGenTableEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在 TaktUserPostEntitiesSeedData 之后）
    /// </summary>
    public int Order => 21;

    /// <summary>
    /// 初始化代码生成表配置实体翻译种子数据
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
            var allTranslations = GetAllGenTableEntityTranslations();

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
    /// 获取所有 TaktGenTable 实体名称及字段翻译（ResourceKey 拆分风格 entity.gentable / entity.gentable.xxx，与 TaktGenTable.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllGenTableEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.gentable（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable._self", TranslationValue = "جدول تكوين الكود", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable._self", TranslationValue = "Code Gen Table", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable._self", TranslationValue = "Tabla de generación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable._self", TranslationValue = "Table de génération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable._self", TranslationValue = "コード生成テーブル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable._self", TranslationValue = "코드 생성 테이블", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable._self", TranslationValue = "Таблица генерации", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable._self", TranslationValue = "代码生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable._self", TranslationValue = "代碼生成表配置", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.datasource
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.datasource", TranslationValue = "مصدر البيانات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.datasource", TranslationValue = "Data Source", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.datasource", TranslationValue = "Origen de datos", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.datasource", TranslationValue = "Source de données", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.datasource", TranslationValue = "データソース", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.datasource", TranslationValue = "데이터 소스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.datasource", TranslationValue = "Источник данных", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.datasource", TranslationValue = "数据源", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.datasource", TranslationValue = "數據源", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.tablename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.tablename", TranslationValue = "اسم الجدول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.tablename", TranslationValue = "Table Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.tablename", TranslationValue = "Nombre de tabla", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.tablename", TranslationValue = "Nom de table", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.tablename", TranslationValue = "テーブル名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.tablename", TranslationValue = "테이블 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.tablename", TranslationValue = "Имя таблицы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.tablename", TranslationValue = "表名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.tablename", TranslationValue = "表名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.tablecomment
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "وصف الجدول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "Table Comment", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "Comentario de tabla", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "Commentaire de table", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "テーブルコメント", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "테이블 설명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "Комментарий таблицы", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "表描述", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.tablecomment", TranslationValue = "表描述", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.subtablename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.subtablename", TranslationValue = "جدول الأب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.subtablename", TranslationValue = "Sub Table Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.subtablename", TranslationValue = "Tabla padre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.subtablename", TranslationValue = "Table parente", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.subtablename", TranslationValue = "親テーブル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.subtablename", TranslationValue = "부모 테이블", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.subtablename", TranslationValue = "Родительская таблица", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.subtablename", TranslationValue = "关联父表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.subtablename", TranslationValue = "關聯父表", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.subtablefkname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "المفتاح الأجنبي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "Foreign Key Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "Clave foránea", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "Clé étrangère", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "外部キー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "외래 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "Внешний ключ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "关联外键", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.subtablefkname", TranslationValue = "關聯外鍵", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.treecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.treecode", TranslationValue = "رمز الشجرة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.treecode", TranslationValue = "Tree Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.treecode", TranslationValue = "Código de árbol", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.treecode", TranslationValue = "Code arbre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.treecode", TranslationValue = "ツリーコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.treecode", TranslationValue = "트리 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.treecode", TranslationValue = "Код дерева", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.treecode", TranslationValue = "树编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.treecode", TranslationValue = "樹編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.treeparentcode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "رمز الأب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "Tree Parent Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "Código padre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "Code parent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "親コード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "부모 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "Код родителя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "树父编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.treeparentcode", TranslationValue = "樹父編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.treename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.treename", TranslationValue = "اسم الشجرة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.treename", TranslationValue = "Tree Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.treename", TranslationValue = "Nombre del árbol", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.treename", TranslationValue = "Nom de l'arbre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.treename", TranslationValue = "ツリー名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.treename", TranslationValue = "트리 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.treename", TranslationValue = "Имя дерева", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.treename", TranslationValue = "树名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.treename", TranslationValue = "樹名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.indatabase
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.indatabase", TranslationValue = "جدول قاعدة البيانات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.indatabase", TranslationValue = "In Database", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.indatabase", TranslationValue = "En base de datos", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.indatabase", TranslationValue = "En base de données", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.indatabase", TranslationValue = "DBテーブル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.indatabase", TranslationValue = "DB 테이블", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.indatabase", TranslationValue = "В базе данных", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.indatabase", TranslationValue = "库表标识", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.indatabase", TranslationValue = "庫表標識", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.gentemplate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "نوع القالب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "Gen Template", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "Tipo de plantilla", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "Type de modèle", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "生成タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "생성 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "Тип шаблона", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "生成类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.gentemplate", TranslationValue = "生成類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.genmodulename
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "اسم الوحدة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "Module Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "Nombre del módulo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "Nom du module", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "モジュール名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "모듈 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "Имя модуля", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模块名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genmodulename", TranslationValue = "模組名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.genbusinessname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "اسم الأعمال", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "Business Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "Nombre del negocio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "Nom métier", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "業務名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "비즈니스 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "Имя бизнеса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "业务名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genbusinessname", TranslationValue = "業務名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.genfunctionname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "اسم الوظيفة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "Function Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "Nombre de función", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "Nom de la fonction", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "機能名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "기능 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "Имя функции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genfunctionname", TranslationValue = "功能名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.entityclassname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "اسم فئة الكيان", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "Entity Class Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "Nombre de clase entidad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "Nom de classe d'entité", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "エンティティクラス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "엔티티 클래스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "Имя класса сущности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "实体类名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.entityclassname", TranslationValue = "實體類名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.genauthor
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.genauthor", TranslationValue = "المؤلف", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genauthor", TranslationValue = "Author", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.genauthor", TranslationValue = "Autor", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.genauthor", TranslationValue = "Auteur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genauthor", TranslationValue = "작성자", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.genauthor", TranslationValue = "Автор", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genauthor", TranslationValue = "作者", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.permsprefix
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "بادئة الصلاحية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "Permission Prefix", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "Prefijo de permiso", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "Préfixe permission", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "権限プレフィックス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "권한 접두사", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "Префикс прав", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "权限前缀", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.permsprefix", TranslationValue = "權限前綴", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.nameprefix
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "بادئة الاسم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "Name Prefix", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "Prefijo de nombre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "Préfixe nom", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "名前プレフィックス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "이름 접두사", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "Префикс имени", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空间前缀", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.nameprefix", TranslationValue = "命名空間前綴", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.entitynamespace
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "مساحة الكيان", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "Entity Namespace", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "Espacio de nombres entidad", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "Espace de noms entité", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "エンティティ名前空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "엔티티 네임스페이스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "Пространство имён сущности", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "实体命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.entitynamespace", TranslationValue = "實體命名空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.dtonamespace
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "مساحة Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "Dto Namespace", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "Espacio de nombres Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "Espace de noms Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "Dto名前空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "Dto 네임스페이스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "Пространство имён Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "传输对象Dto命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.dtonamespace", TranslationValue = "傳輸對象Dto命名空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.dtoclassname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "فئة Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "Dto Class Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "Nombre clase Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "Nom classe Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "Dtoクラス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "Dto 클래스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "Имя класса Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "传输对象Dto类名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.dtoclassname", TranslationValue = "傳輸對象Dto類名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.dtocategory
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "فئة Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "Dto Category", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "Categoría Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "Catégorie Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "Dtoカテゴリ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "Dto 카테고리", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "Категория Dto", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "传输对象Dto类别", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.dtocategory", TranslationValue = "傳輸對象Dto類別", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.servicenamespace
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "مساحة الخدمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "Service Namespace", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "Espacio de nombres servicio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "Espace de noms service", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "サービス名前空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "서비스 네임스페이스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "Пространство имён сервиса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服务命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.servicenamespace", TranslationValue = "服務命名空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.iserviceclassname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "واجهة الخدمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "Service Interface Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "Nombre interfaz servicio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "Nom interface service", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "サービスインターフェース名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "서비스 인터페이스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "Имя интерфейса сервиса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "服务接口类名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.iserviceclassname", TranslationValue = "服務接口類名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.serviceclassname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "فئة الخدمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "Service Class Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "Nombre clase servicio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "Nom classe service", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "サービスクラス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "서비스 클래스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "Имя класса сервиса", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "服务类名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.serviceclassname", TranslationValue = "服務類名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.controllernamespace
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "مساحة المتحكم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "Controller Namespace", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "Espacio de nombres controlador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "Espace de noms contrôleur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "コントローラ名前空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "컨트롤러 네임스페이스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "Пространство имён контроллера", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.controllernamespace", TranslationValue = "控制器命名空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.controllerclassname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "فئة المتحكم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "Controller Class Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "Nombre clase controlador", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "Nom classe contrôleur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "コントローラクラス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "컨트롤러 클래스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "Имя класса контроллера", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "控制器类名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.controllerclassname", TranslationValue = "控制器類名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.isrepository
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.isrepository", TranslationValue = "طبقة المستودع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isrepository", TranslationValue = "Repository Layer", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.isrepository", TranslationValue = "Capa repositorio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.isrepository", TranslationValue = "Couche dépôt", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isrepository", TranslationValue = "リポジトリ層", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isrepository", TranslationValue = "저장소 계층", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.isrepository", TranslationValue = "Слой репозитория", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isrepository", TranslationValue = "仓储层", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isrepository", TranslationValue = "倉儲層", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.repositoryinterfacenamespace
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "واجهة المستودع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "Repository Interface Namespace", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "Espacio interfaz repositorio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "Espace interface dépôt", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "リポジトリインターフェース名前空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "저장소 인터페이스 네임스페이스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "Пространство интерфейса репозитория", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "仓储接口命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.repositoryinterfacenamespace", TranslationValue = "倉儲接口命名空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.irepositoryclassname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "واجهة المستودع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "Repository Interface Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "Nombre interfaz repositorio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "Nom interface dépôt", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "リポジトリインターフェース名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "저장소 인터페이스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "Имя интерфейса репозитория", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "仓储接口类名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.irepositoryclassname", TranslationValue = "倉儲接口類名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.repositorynamespace
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "مساحة المستودع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "Repository Namespace", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "Espacio de nombres repositorio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "Espace de noms dépôt", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "リポジトリ名前空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "저장소 네임스페이스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "Пространство имён репозитория", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "仓储命名空间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.repositorynamespace", TranslationValue = "倉儲命名空間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.repositoryclassname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "فئة المستودع", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "Repository Class Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "Nombre clase repositorio", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "Nom classe dépôt", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "リポジトリクラス名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "저장소 클래스명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "Имя класса репозитория", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "仓储类名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.repositoryclassname", TranslationValue = "倉儲類名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.genfunction
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.genfunction", TranslationValue = "وظائف التوليد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genfunction", TranslationValue = "Gen Function", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.genfunction", TranslationValue = "Funciones de generación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.genfunction", TranslationValue = "Fonctions de génération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成機能", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genfunction", TranslationValue = "생성 기능", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.genfunction", TranslationValue = "Функции генерации", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genfunction", TranslationValue = "生成功能", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.genmethod
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.genmethod", TranslationValue = "طريقة التوليد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genmethod", TranslationValue = "Gen Method", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.genmethod", TranslationValue = "Método de generación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.genmethod", TranslationValue = "Méthode de génération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genmethod", TranslationValue = "생성 방식", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.genmethod", TranslationValue = "Способ генерации", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genmethod", TranslationValue = "生成方式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.genpath
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.genpath", TranslationValue = "مسار التوليد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.genpath", TranslationValue = "Gen Path", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.genpath", TranslationValue = "Ruta de generación", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.genpath", TranslationValue = "Chemin de génération", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成パス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.genpath", TranslationValue = "생성 경로", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.genpath", TranslationValue = "Путь генерации", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路径", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.genpath", TranslationValue = "生成路徑", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.isgenmenu
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "توليد القائمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "Gen Menu", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "Generar menú", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "Générer menu", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "メニュー生成", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "메뉴 생성", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "Генерация меню", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成菜单", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isgenmenu", TranslationValue = "生成選單", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.parentmenuid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "معرف القائمة الأب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "Parent Menu ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID menú padre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID menu parent", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "親メニューID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "부모 메뉴 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "ID родительского меню", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "上级菜单ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.parentmenuid", TranslationValue = "上級選單ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.isgentranslation
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "توليد الترجمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "Gen Translation", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "Generar traducción", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "Générer traduction", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "翻訳生成", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "번역 생성", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "Генерация перевода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻译", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isgentranslation", TranslationValue = "生成翻譯", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.sorttype
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.sorttype", TranslationValue = "نوع الترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.sorttype", TranslationValue = "Sort Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.sorttype", TranslationValue = "Tipo de orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.sorttype", TranslationValue = "Type de tri", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.sorttype", TranslationValue = "並び順タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.sorttype", TranslationValue = "정렬 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.sorttype", TranslationValue = "Тип сортировки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.sorttype", TranslationValue = "排序类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.sorttype", TranslationValue = "排序類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.sortfield
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.sortfield", TranslationValue = "حقل الترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.sortfield", TranslationValue = "Sort Field", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.sortfield", TranslationValue = "Campo de orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.sortfield", TranslationValue = "Champ de tri", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.sortfield", TranslationValue = "ソート項目", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.sortfield", TranslationValue = "정렬 필드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.sortfield", TranslationValue = "Поле сортировки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.sortfield", TranslationValue = "排序字段", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.sortfield", TranslationValue = "排序字段", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.fronttemplate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "قالب الواجهة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "Front Template", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "Plantilla frontend", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "Modèle front", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "フロントテンプレート", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "프론트 템플릿", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "Шаблон фронта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "前端模板", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.fronttemplate", TranslationValue = "前端模板", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.frontstyle
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "نمط الواجهة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "Front Style", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "Estilo frontend", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "Style front", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "フロントスタイル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "프론트 스타일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "Стиль фронта", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "前端样式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.frontstyle", TranslationValue = "前端樣式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.btnstyle
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "نمط الزر", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "Button Style", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "Estilo de botón", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "Style bouton", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "ボタンスタイル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "버튼 스타일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "Стиль кнопки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "按钮样式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.btnstyle", TranslationValue = "按鈕樣式", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.isgencode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.isgencode", TranslationValue = "توليد الكود", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isgencode", TranslationValue = "Gen Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.isgencode", TranslationValue = "Generar código", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.isgencode", TranslationValue = "Générer code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isgencode", TranslationValue = "コード生成", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isgencode", TranslationValue = "코드 생성", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.isgencode", TranslationValue = "Генерация кода", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isgencode", TranslationValue = "是否生成", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isgencode", TranslationValue = "是否生成", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.gencodecount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "مرات التوليد", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "Gen Code Count", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "Veces generado", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "Nombre de générations", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "生成回数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "생성 횟수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "Количество генераций", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "代码生成次数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.gencodecount", TranslationValue = "代碼生成次數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.isusetabs
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "استخدام التبويبات", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "Use Tabs", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "Usar pestañas", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "Utiliser onglets", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "タブ使用", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "탭 사용", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "Использовать вкладки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.isusetabs", TranslationValue = "使用tabs", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.tabsfieldcount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "عدد حقول التبويب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "Tabs Field Count", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "Campos por pestaña", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "Champs par onglet", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "タブ内項目数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "탭 필드 수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "Кол-во полей вкладки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs标签字段", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.tabsfieldcount", TranslationValue = "tabs標籤字段", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.gentable.options
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.gentable.options", TranslationValue = "خيارات أخرى", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.gentable.options", TranslationValue = "Options", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.gentable.options", TranslationValue = "Otras opciones", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.gentable.options", TranslationValue = "Autres options", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.gentable.options", TranslationValue = "その他オプション", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.gentable.options", TranslationValue = "기타 옵션", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.gentable.options", TranslationValue = "Прочие опции", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.gentable.options", TranslationValue = "其他生成选项", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.gentable.options", TranslationValue = "其他生成選項", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
