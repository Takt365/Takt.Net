// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDictDataEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktDictData 实体字段翻译种子数据，与 TaktMenuEntitiesSeedData 风格一致，entity.dictdata / entity.dictdata.xxx，每个字段 9 种语言
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
/// TaktDictData 实体翻译种子数据（与 TaktDictData.cs 属性一一对应）
/// </summary>
public class TaktDictDataEntitiesSeedData : ITaktSeedData
{
    public int Order => 23;

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
            var allTranslations = GetAllDictDataEntityTranslations();

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
    /// 获取所有 TaktDictData 实体名称及字段翻译（ResourceKey 拆分风格 entity.dictdata / entity.dictdata.xxx，与 TaktDictData.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllDictDataEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.dictdata（实体名称）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata._self", TranslationValue = "بيانات القاموس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata._self", TranslationValue = "Dict Data", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata._self", TranslationValue = "Datos de diccionario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata._self", TranslationValue = "Données dictionnaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata._self", TranslationValue = "辞書データ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata._self", TranslationValue = "딕셔너리 데이터", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata._self", TranslationValue = "Данные словаря", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata._self", TranslationValue = "字典数据", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata._self", TranslationValue = "字典數據", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.dicttypeid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "معرف نوع القاموس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "Dict Type ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "ID tipo diccionario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "ID type dictionnaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "辞書タイプID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "딕셔너리 유형 ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "ID типа словаря", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "字典类型ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.dicttypeid", TranslationValue = "字典類型ID", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.dicttypecode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "رمز نوع القاموس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "Dict Type Code", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "Código tipo diccionario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "Code type dictionnaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "辞書タイプコード", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "딕셔너리 유형 코드", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "Код типа словаря", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "字典类型编码", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.dicttypecode", TranslationValue = "字典類型編碼", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.dictlabel
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "تسمية القاموس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "Dict Label", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "Etiqueta diccionario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "Libellé dictionnaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "辞書ラベル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "딕셔너리 라벨", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "Метка словаря", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "字典标签", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.dictlabel", TranslationValue = "字典標籤", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.dictl10nkey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "مفتاح الترجمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "L10n Key", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "Clave L10n", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "Clé L10n", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "L10nキー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "L10n 키", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "Ключ L10n", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "字典本地化键", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.dictl10nkey", TranslationValue = "字典本地化鍵", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.dictvalue
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "قيمة القاموس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "Dict Value", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "Valor diccionario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "Valeur dictionnaire", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "辞書値", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "딕셔너리 값", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "Значение словаря", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "字典值", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.dictvalue", TranslationValue = "字典值", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.cssclass
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "فئة CSS", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "CSS Class", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "Clase CSS", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "Classe CSS", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "CSSクラス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "CSS 클래스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "CSS класс", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "CSS类名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.cssclass", TranslationValue = "CSS類名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.listclass
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.listclass", TranslationValue = "فئة القائمة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.listclass", TranslationValue = "List Class", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.listclass", TranslationValue = "Clase lista", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.listclass", TranslationValue = "Classe liste", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.listclass", TranslationValue = "リストクラス", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.listclass", TranslationValue = "목록 클래스", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.listclass", TranslationValue = "Класс списка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.listclass", TranslationValue = "列表类名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.listclass", TranslationValue = "列表類名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.extlabel
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "تسمية موسعة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "Ext Label", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "Etiqueta ext", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "Libellé ext", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "拡張ラベル", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "확장 라벨", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "Расширенная метка", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "扩展标签", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.extlabel", TranslationValue = "擴展標籤", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.extvalue
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "قيمة موسعة", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "Ext Value", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "Valor ext", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "Valeur ext", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "拡張値", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "확장 값", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "Расширенное значение", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "扩展值", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.extvalue", TranslationValue = "擴展值", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.dictdata.ordernum
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "ترتيب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "Orden", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "Ordre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "Порядок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.dictdata.ordernum", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 }
        };
    }
}
