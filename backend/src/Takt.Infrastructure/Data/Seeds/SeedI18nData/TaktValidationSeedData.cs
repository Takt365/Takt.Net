// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktValidationSeedData.cs
// 功能描述：通用验证文案模板种子（ResourceKey：validation.*，ResourceGroup：Validation）。含通用规则与 TaktRegexHelper 对应的 validation.pattern*（九语种逐条书写，与上文同风格）。ResourceType 使用 Frontend：与 entity.* 一致，供前端展示与后端 ITaktLocalizer 共用。另含 Excel 导入模板默认文件名后缀键 entity.template.name（ResourceGroup：Entity）。
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
/// 通用验证模板种子（与实体 I18n 种子相同：逐条 TaktTranslation、九语种横展）。
/// </summary>
public class TaktValidationSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在语言种子之后，可与用户实体翻译同级稍后执行）
    /// </summary>
    public int Order => 45;

    /// <summary>
    /// 初始化验证模板翻译种子数据
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
            var allTranslations = GetAllValidationTemplates();

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
                    var newTranslation = new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = translation.CultureCode,
                        ResourceKey = translation.ResourceKey,
                        TranslationValue = translation.TranslationValue,
                        ResourceType = translation.ResourceType,
                        ResourceGroup = translation.ResourceGroup,
                        OrderNum = translation.OrderNum,
                        IsDeleted = 0
                    };
                    await translationRepository.CreateAsync(newTranslation);
                    insertCount++;
                }
                else
                {
                    if (existing.TranslationValue != translation.TranslationValue ||
                        existing.ResourceType != translation.ResourceType ||
                        existing.ResourceGroup != translation.ResourceGroup ||
                        existing.OrderNum != translation.OrderNum)
                    {
                        existing.LanguageId = languageId;
                        existing.TranslationValue = translation.TranslationValue;
                        existing.ResourceType = translation.ResourceType;
                        existing.ResourceGroup = translation.ResourceGroup;
                        existing.OrderNum = translation.OrderNum;
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
    /// 获取所有验证模板翻译（ResourceType=Frontend；含通用规则与 TaktRegexHelper 对应的 validation.pattern*，九语种逐条书写；另含 entity.template.name 供 Excel 导入模板默认文件名后缀）
    /// </summary>
    private static List<TaktTranslation> GetAllValidationTemplates()
    {
        var list = new List<TaktTranslation>
        {
            // entity.template.name（Excel 导入模板文件基名后缀，与本地化 entity.xxx._self 直接拼接，如 zh-CN：用户表+模板=用户表模板）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.template.name", TranslationValue = "قالب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.template.name", TranslationValue = "Template", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.template.name", TranslationValue = "Plantilla", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.template.name", TranslationValue = "Modele", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.template.name", TranslationValue = "テンプレート", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.template.name", TranslationValue = "템플릿", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.template.name", TranslationValue = "Шаблон", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.template.name", TranslationValue = "模板", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.template.name", TranslationValue = "模板", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // validation.required
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.required", TranslationValue = "{0} مطلوب.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.required", TranslationValue = "{0} is required.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.required", TranslationValue = "{0} es obligatorio.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.required", TranslationValue = "{0} est requis.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.required", TranslationValue = "{0}は必須です。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.required", TranslationValue = "{0}은(는) 필수입니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.required", TranslationValue = "Поле «{0}» обязательно.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.required", TranslationValue = "{0}不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.required", TranslationValue = "{0}不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.lengthBetween
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.lengthBetween", TranslationValue = "يجب أن يتراوح طول {0} بين {1} و{2} حرفًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.lengthBetween", TranslationValue = "{0} must be between {1} and {2} characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.lengthBetween", TranslationValue = "La longitud de {0} debe estar entre {1} y {2} caracteres.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.lengthBetween", TranslationValue = "La longueur de {0} doit être comprise entre {1} et {2} caractères.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}の長さは{1}文字以上{2}文字以下である必要があります。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.lengthBetween", TranslationValue = "{0} 길이는 {1}자 이상 {2}자 이하여야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.lengthBetween", TranslationValue = "Длина поля «{0}» должна быть от {1} до {2} символов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}长度必须在{1}到{2}个字符之间", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}長度必須在{1}到{2}個字元之間", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.lengthMin
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.lengthMin", TranslationValue = "يجب ألا يقل طول {0} عن {1} حرفًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.lengthMin", TranslationValue = "{0} must be at least {1} characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.lengthMin", TranslationValue = "La longitud de {0} debe ser de al menos {1} caracteres.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.lengthMin", TranslationValue = "La longueur de {0} doit être d'au moins {1} caractères.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.lengthMin", TranslationValue = "{0}は{1}文字以上である必要があります。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.lengthMin", TranslationValue = "{0}은(는) 최소 {1}자 이상이어야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.lengthMin", TranslationValue = "Длина поля «{0}» — не менее {1} символов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.lengthMin", TranslationValue = "{0}长度不能少于{1}个字符", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.lengthMin", TranslationValue = "{0}長度不能少於{1}個字元", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.lengthMax
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.lengthMax", TranslationValue = "لا يجب أن يتجاوز طول {0} {1} حرفًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.lengthMax", TranslationValue = "{0} must not exceed {1} characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.lengthMax", TranslationValue = "La longitud de {0} no debe superar {1} caracteres.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.lengthMax", TranslationValue = "La longueur de {0} ne doit pas dépasser {1} caractères.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.lengthMax", TranslationValue = "{0}は{1}文字を超えてはなりません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.lengthMax", TranslationValue = "{0}은(는) {1}자를 넘을 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.lengthMax", TranslationValue = "Длина поля «{0}» не должна превышать {1} символов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.lengthMax", TranslationValue = "{0}长度不能超过{1}个字符", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.lengthMax", TranslationValue = "{0}長度不能超過{1}個字元", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.formatInvalid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.formatInvalid", TranslationValue = "تنسيق {0} غير صالح.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.formatInvalid", TranslationValue = "{0} is not in a valid format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.formatInvalid", TranslationValue = "El formato de {0} no es válido.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.formatInvalid", TranslationValue = "Le format de {0} est invalide.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}の形式が正しくありません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.formatInvalid", TranslationValue = "{0} 형식이 올바르지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.formatInvalid", TranslationValue = "Неверный формат поля «{0}».", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}格式不正确", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}格式不正確", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.idCardInvalid
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0} غير صالح أو فشل التحقق.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0} is invalid or failed verification.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0} no es válido o falló la verificación.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0} est invalide ou la vérification a échoué.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}が無効であるか、検証に失敗しました。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}이(가) 잘못되었거나 검증에 실패했습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.idCardInvalid", TranslationValue = "«{0}» недействителен или проверка не пройдена.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}格式不正确或校验失败", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}格式不正確或校驗失敗", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternUsername
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternUsername", TranslationValue = "{0}: يجب أن يبدأ بحرف صغير، وأحرف صغيرة وأرقام فقط، بين 4 و20.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternUsername", TranslationValue = "{0} is invalid (must start with a lowercase letter; only lowercase letters and digits; 4–20 characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternUsername", TranslationValue = "{0} no es válido (debe empezar con minúscula; solo minúsculas y dígitos; 4–20 caracteres).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternUsername", TranslationValue = "{0} est invalide (doit commencer par une minuscule ; uniquement minuscules et chiffres ; 4 à 20 caractères).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternUsername", TranslationValue = "{0}が無効です（先頭は小文字、小文字と数字のみ、4～20文字）。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternUsername", TranslationValue = "{0}이(가) 올바르지 않습니다(소문자로 시작, 소문자·숫자만, 4~20자).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternUsername", TranslationValue = "«{0}»: с маленькой буквы, только строчные буквы и цифры, 4–20 символов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternUsername", TranslationValue = "{0}格式不正确（小写字母开头，仅允许小写字母和数字，4-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternUsername", TranslationValue = "{0}格式不正確（小寫字母開頭，僅允許小寫字母與數字，4-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPasswordStrong
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}: من 8 إلى 20 مع حرف كبير وصغير ورقم ورمز خاص.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0} is invalid (8–20 characters; must include uppercase, lowercase, digit and special character).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0} no es válido (8–20 caracteres; mayúscula, minúscula, dígito y carácter especial).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0} est invalide (8 à 20 caractères ; majuscule, minuscule, chiffre et caractère spécial).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}が無効です（8～20文字、大文字・小文字・数字・記号を含む必要があります）。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}이(가) 올바르지 않습니다(8~20자, 대·소문자·숫자·특수문자 포함).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "«{0}»: 8–20 символов, заглавная, строчная, цифра и спецсимвол.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}格式不正确（8-20位，必须包含大小写字母、数字和特殊字符）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}格式不正確（8-20位，必須包含大小寫字母、數字與特殊字元）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.notEqualFields
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.notEqualFields", TranslationValue = "لا يجب أن يكون {0} مطابقًا لـ {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.notEqualFields", TranslationValue = "{0} must not be the same as {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.notEqualFields", TranslationValue = "{0} no debe ser igual que {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.notEqualFields", TranslationValue = "{0} ne doit pas être identique à {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}を{1}と同じにすることはできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}은(는) {1}과(와) 같을 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.notEqualFields", TranslationValue = "«{0}» не должно совпадать с «{1}».", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}不能与{1}相同", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}不能與{1}相同", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.endBeforeStart（结束时间不得早于开始时间）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.endBeforeStart", TranslationValue = "لا يمكن أن يكون {0} قبل {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0} cannot be earlier than {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0} no puede ser anterior a {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0} ne peut pas être antérieur à {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}を{1}より前にすることはできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}은(는) {1}보다 이전일 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.endBeforeStart", TranslationValue = "«{0}» не может быть раньше «{1}».", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}不能早于{1}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}不能早於{1}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternEmail（起：与 TaktRegexHelper 中 Regex 字段顺序一致；ar/es/fr/ja/ko/ru 暂用英文文案与历史种子一致）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternEmail", TranslationValue = "{0}不是有效的邮箱地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternEmail", TranslationValue = "{0}不是有效的郵箱地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPhoneCn
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0} is not a valid China mainland mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0} is not a valid China mainland mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0} is not a valid China mainland mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0} is not a valid China mainland mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0} is not a valid China mainland mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0} is not a valid China mainland mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0} is not a valid China mainland mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0}不是有效的中国大陆手机号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPhoneCn", TranslationValue = "{0}不是有效的中國大陸手機號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPhoneTw
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0} is not a valid Taiwan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0} is not a valid Taiwan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0} is not a valid Taiwan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0} is not a valid Taiwan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0} is not a valid Taiwan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0} is not a valid Taiwan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0} is not a valid Taiwan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0}不是有效的中国台湾手机号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPhoneTw", TranslationValue = "{0}不是有效的中國台灣手機號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPhoneHk
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0} is not a valid Hong Kong mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0} is not a valid Hong Kong mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0} is not a valid Hong Kong mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0} is not a valid Hong Kong mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0} is not a valid Hong Kong mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0} is not a valid Hong Kong mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0} is not a valid Hong Kong mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0}不是有效的中国香港手机号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPhoneHk", TranslationValue = "{0}不是有效的中國香港手機號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPhoneUs
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0} is not a valid US mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0} is not a valid US mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0} is not a valid US mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0} is not a valid US mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0} is not a valid US mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0} is not a valid US mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0} is not a valid US mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0}不是有效的美国手机号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPhoneUs", TranslationValue = "{0}不是有效的美國手機號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPhoneJp
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0} is not a valid Japan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0} is not a valid Japan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0} is not a valid Japan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0} is not a valid Japan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0} is not a valid Japan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0} is not a valid Japan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0} is not a valid Japan mobile number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0}不是有效的日本手机号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPhoneJp", TranslationValue = "{0}不是有效的日本手機號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPhone
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile number (CN/TW/HK/US/JP).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile number (CN/TW/HK/US/JP).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile number (CN/TW/HK/US/JP).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile number (CN/TW/HK/US/JP).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile number (CN/TW/HK/US/JP).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile number (CN/TW/HK/US/JP).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile number (CN/TW/HK/US/JP).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPhone", TranslationValue = "{0}不是有效的手机号码（支持大陆/台湾/香港/美国/日本）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPhone", TranslationValue = "{0}不是有效的手機號碼（支援大陸/台灣/香港/美國/日本）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternTelCn
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternTelCn", TranslationValue = "{0} is not a valid China mainland landline number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternTelCn", TranslationValue = "{0} is not a valid China mainland landline number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternTelCn", TranslationValue = "{0} is not a valid China mainland landline number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternTelCn", TranslationValue = "{0} is not a valid China mainland landline number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternTelCn", TranslationValue = "{0} is not a valid China mainland landline number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternTelCn", TranslationValue = "{0} is not a valid China mainland landline number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternTelCn", TranslationValue = "{0} is not a valid China mainland landline number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternTelCn", TranslationValue = "{0}不是有效的中国大陆固定电话", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternTelCn", TranslationValue = "{0}不是有效的中國大陸固定電話", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIdCard18
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0} is not a valid 18-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0} is not a valid 18-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0} is not a valid 18-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0} is not a valid 18-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0} is not a valid 18-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0} is not a valid 18-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0} is not a valid 18-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0}不是有效的18位中国身份证号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIdCard18", TranslationValue = "{0}不是有效的18位中國身份證號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIdCard15
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0} is not a valid 15-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0} is not a valid 15-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0} is not a valid 15-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0} is not a valid 15-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0} is not a valid 15-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0} is not a valid 15-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0} is not a valid 15-digit Chinese ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0}不是有效的15位中国身份证号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIdCard15", TranslationValue = "{0}不是有效的15位中國身份證號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIdCardTw
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0} is not a valid Taiwan ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0} is not a valid Taiwan ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0} is not a valid Taiwan ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0} is not a valid Taiwan ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0} is not a valid Taiwan ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0} is not a valid Taiwan ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0} is not a valid Taiwan ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0}不是有效的中国台湾身份证号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIdCardTw", TranslationValue = "{0}不是有效的中國台灣身份證號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIdCardHk
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0} is not a valid Hong Kong ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0} is not a valid Hong Kong ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0} is not a valid Hong Kong ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0} is not a valid Hong Kong ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0} is not a valid Hong Kong ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0} is not a valid Hong Kong ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0} is not a valid Hong Kong ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0}不是有效的中国香港身份证号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIdCardHk", TranslationValue = "{0}不是有效的中國香港身份證號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIdCardUs
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0} is not a valid US SSN.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0} is not a valid US SSN.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0} is not a valid US SSN.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0} is not a valid US SSN.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0} is not a valid US SSN.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0} is not a valid US SSN.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0} is not a valid US SSN.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0}不是有效的美国社会安全号码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIdCardUs", TranslationValue = "{0}不是有效的美國社會安全號碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIdCardJp
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0} is not a valid Japan My Number (12 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0} is not a valid Japan My Number (12 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0} is not a valid Japan My Number (12 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0} is not a valid Japan My Number (12 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0} is not a valid Japan My Number (12 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0} is not a valid Japan My Number (12 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0} is not a valid Japan My Number (12 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0}不是有效的日本个人番号（12位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIdCardJp", TranslationValue = "{0}不是有效的日本個人番號（12位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternUnifiedSocialCreditCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0} is not a valid unified social credit code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0} is not a valid unified social credit code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0} is not a valid unified social credit code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0} is not a valid unified social credit code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0} is not a valid unified social credit code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0} is not a valid unified social credit code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0} is not a valid unified social credit code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0}不是有效的统一社会信用代码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternUnifiedSocialCreditCode", TranslationValue = "{0}不是有效的統一社會信用代碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPostalCodeCn
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0} is not a valid China postal code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0} is not a valid China postal code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0} is not a valid China postal code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0} is not a valid China postal code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0} is not a valid China postal code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0} is not a valid China postal code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0} is not a valid China postal code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0}不是有效的中国邮政编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPostalCodeCn", TranslationValue = "{0}不是有效的中國郵政編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternUrl
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternUrl", TranslationValue = "{0}不是有效的网址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternUrl", TranslationValue = "{0}不是有效的網址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIpv4
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIpv4", TranslationValue = "{0} is not a valid IPv4 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIpv4", TranslationValue = "{0} is not a valid IPv4 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIpv4", TranslationValue = "{0} is not a valid IPv4 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIpv4", TranslationValue = "{0} is not a valid IPv4 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIpv4", TranslationValue = "{0} is not a valid IPv4 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIpv4", TranslationValue = "{0} is not a valid IPv4 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIpv4", TranslationValue = "{0} is not a valid IPv4 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIpv4", TranslationValue = "{0}不是有效的IPv4地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIpv4", TranslationValue = "{0}不是有效的IPv4地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternIpv6
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternIpv6", TranslationValue = "{0} is not a valid IPv6 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternIpv6", TranslationValue = "{0} is not a valid IPv6 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternIpv6", TranslationValue = "{0} is not a valid IPv6 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternIpv6", TranslationValue = "{0} is not a valid IPv6 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternIpv6", TranslationValue = "{0} is not a valid IPv6 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternIpv6", TranslationValue = "{0} is not a valid IPv6 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternIpv6", TranslationValue = "{0} is not a valid IPv6 address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternIpv6", TranslationValue = "{0}不是有效的IPv6地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternIpv6", TranslationValue = "{0}不是有效的IPv6地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternRealName
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternRealName", TranslationValue = "{0} is not a valid real name (2–50 Chinese characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternRealName", TranslationValue = "{0} is not a valid real name (2–50 Chinese characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternRealName", TranslationValue = "{0} is not a valid real name (2–50 Chinese characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternRealName", TranslationValue = "{0} is not a valid real name (2–50 Chinese characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternRealName", TranslationValue = "{0} is not a valid real name (2–50 Chinese characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternRealName", TranslationValue = "{0} is not a valid real name (2–50 Chinese characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternRealName", TranslationValue = "{0} is not a valid real name (2–50 Chinese characters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternRealName", TranslationValue = "{0}不是有效的实名（2-50个汉字）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternRealName", TranslationValue = "{0}不是有效的實名（2-50個漢字）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternFullName
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternFullName", TranslationValue = "{0} is not a valid full name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternFullName", TranslationValue = "{0} is not a valid full name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternFullName", TranslationValue = "{0} is not a valid full name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternFullName", TranslationValue = "{0} is not a valid full name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternFullName", TranslationValue = "{0} is not a valid full name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternFullName", TranslationValue = "{0} is not a valid full name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternFullName", TranslationValue = "{0} is not a valid full name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternFullName", TranslationValue = "{0}不是有效的姓名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternFullName", TranslationValue = "{0}不是有效的姓名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternNickName
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternNickName", TranslationValue = "{0} is not a valid nickname.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternNickName", TranslationValue = "{0} is not a valid nickname.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternNickName", TranslationValue = "{0} is not a valid nickname.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternNickName", TranslationValue = "{0} is not a valid nickname.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternNickName", TranslationValue = "{0} is not a valid nickname.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternNickName", TranslationValue = "{0} is not a valid nickname.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternNickName", TranslationValue = "{0} is not a valid nickname.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternNickName", TranslationValue = "{0}不是有效的昵称", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternNickName", TranslationValue = "{0}不是有效的暱稱", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternEnglishName
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0} is not a valid English name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0} is not a valid English name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0} is not a valid English name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0} is not a valid English name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0} is not a valid English name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0} is not a valid English name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0} is not a valid English name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0}不是有效的英文名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternEnglishName", TranslationValue = "{0}不是有效的英文名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternNameEn
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternNameEn", TranslationValue = "{0} is not a valid English-style name (initial cap).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternNameEn", TranslationValue = "{0} is not a valid English-style name (initial cap).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternNameEn", TranslationValue = "{0} is not a valid English-style name (initial cap).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternNameEn", TranslationValue = "{0} is not a valid English-style name (initial cap).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternNameEn", TranslationValue = "{0} is not a valid English-style name (initial cap).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternNameEn", TranslationValue = "{0} is not a valid English-style name (initial cap).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternNameEn", TranslationValue = "{0} is not a valid English-style name (initial cap).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternNameEn", TranslationValue = "{0}不是有效的英文格式姓名（首字母大写）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternNameEn", TranslationValue = "{0}不是有效的英文格式姓名（首字母大寫）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLastName
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLastName", TranslationValue = "{0} is not a valid last name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLastName", TranslationValue = "{0} is not a valid last name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLastName", TranslationValue = "{0} is not a valid last name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLastName", TranslationValue = "{0} is not a valid last name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLastName", TranslationValue = "{0} is not a valid last name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLastName", TranslationValue = "{0} is not a valid last name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLastName", TranslationValue = "{0} is not a valid last name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLastName", TranslationValue = "{0}不是有效的姓", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLastName", TranslationValue = "{0}不是有效的姓", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternFirstName
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternFirstName", TranslationValue = "{0} is not a valid first name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternFirstName", TranslationValue = "{0} is not a valid first name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternFirstName", TranslationValue = "{0} is not a valid first name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternFirstName", TranslationValue = "{0} is not a valid first name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternFirstName", TranslationValue = "{0} is not a valid first name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternFirstName", TranslationValue = "{0} is not a valid first name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternFirstName", TranslationValue = "{0} is not a valid first name.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternFirstName", TranslationValue = "{0}不是有效的名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternFirstName", TranslationValue = "{0}不是有效的名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternChinese
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternChinese", TranslationValue = "{0} must contain only Chinese characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternChinese", TranslationValue = "{0} must contain only Chinese characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternChinese", TranslationValue = "{0} must contain only Chinese characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternChinese", TranslationValue = "{0} must contain only Chinese characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternChinese", TranslationValue = "{0} must contain only Chinese characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternChinese", TranslationValue = "{0} must contain only Chinese characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternChinese", TranslationValue = "{0} must contain only Chinese characters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternChinese", TranslationValue = "{0}须为纯中文", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternChinese", TranslationValue = "{0}須為純中文", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternEnglish
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternEnglish", TranslationValue = "{0} must contain only English letters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternEnglish", TranslationValue = "{0} must contain only English letters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternEnglish", TranslationValue = "{0} must contain only English letters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternEnglish", TranslationValue = "{0} must contain only English letters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternEnglish", TranslationValue = "{0} must contain only English letters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternEnglish", TranslationValue = "{0} must contain only English letters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternEnglish", TranslationValue = "{0} must contain only English letters.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternEnglish", TranslationValue = "{0}须为纯英文字母", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternEnglish", TranslationValue = "{0}須為純英文字母", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternNumber
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternNumber", TranslationValue = "{0} must be digits only.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternNumber", TranslationValue = "{0} must be digits only.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternNumber", TranslationValue = "{0} must be digits only.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternNumber", TranslationValue = "{0} must be digits only.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternNumber", TranslationValue = "{0} must be digits only.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternNumber", TranslationValue = "{0} must be digits only.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternNumber", TranslationValue = "{0} must be digits only.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternNumber", TranslationValue = "{0}须为数字", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternNumber", TranslationValue = "{0}須為數字", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternInteger
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternInteger", TranslationValue = "{0} is not a valid integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternInteger", TranslationValue = "{0} is not a valid integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternInteger", TranslationValue = "{0} is not a valid integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternInteger", TranslationValue = "{0} is not a valid integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternInteger", TranslationValue = "{0} is not a valid integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternInteger", TranslationValue = "{0} is not a valid integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternInteger", TranslationValue = "{0} is not a valid integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternInteger", TranslationValue = "{0}不是有效的整数", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternInteger", TranslationValue = "{0}不是有效的整數", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPositiveInteger
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0} is not a valid positive integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0} is not a valid positive integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0} is not a valid positive integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0} is not a valid positive integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0} is not a valid positive integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0} is not a valid positive integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0} is not a valid positive integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0}不是有效的正整数", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPositiveInteger", TranslationValue = "{0}不是有效的正整數", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternNonNegativeInteger
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0} is not a valid non-negative integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0} is not a valid non-negative integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0} is not a valid non-negative integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0} is not a valid non-negative integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0} is not a valid non-negative integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0} is not a valid non-negative integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0} is not a valid non-negative integer.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0}不是有效的非负整数", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternNonNegativeInteger", TranslationValue = "{0}不是有效的非負整數", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternFloat
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternFloat", TranslationValue = "{0} is not a valid decimal number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternFloat", TranslationValue = "{0} is not a valid decimal number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternFloat", TranslationValue = "{0} is not a valid decimal number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternFloat", TranslationValue = "{0} is not a valid decimal number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternFloat", TranslationValue = "{0} is not a valid decimal number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternFloat", TranslationValue = "{0} is not a valid decimal number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternFloat", TranslationValue = "{0} is not a valid decimal number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternFloat", TranslationValue = "{0}不是有效的小数", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternFloat", TranslationValue = "{0}不是有效的小數", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPositiveFloat
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0} is not a valid positive decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0} is not a valid positive decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0} is not a valid positive decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0} is not a valid positive decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0} is not a valid positive decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0} is not a valid positive decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0} is not a valid positive decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0}不是有效的正小数", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPositiveFloat", TranslationValue = "{0}不是有效的正小數", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternNonNegativeFloat
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0} is not a valid non-negative decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0} is not a valid non-negative decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0} is not a valid non-negative decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0} is not a valid non-negative decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0} is not a valid non-negative decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0} is not a valid non-negative decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0} is not a valid non-negative decimal.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0}不是有效的非负小数", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternNonNegativeFloat", TranslationValue = "{0}不是有效的非負小數", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternDate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternDate", TranslationValue = "{0} is not a valid date (YYYY-MM-DD).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternDate", TranslationValue = "{0} is not a valid date (YYYY-MM-DD).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternDate", TranslationValue = "{0} is not a valid date (YYYY-MM-DD).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternDate", TranslationValue = "{0} is not a valid date (YYYY-MM-DD).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternDate", TranslationValue = "{0} is not a valid date (YYYY-MM-DD).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternDate", TranslationValue = "{0} is not a valid date (YYYY-MM-DD).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternDate", TranslationValue = "{0} is not a valid date (YYYY-MM-DD).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternDate", TranslationValue = "{0}不是有效的日期（YYYY-MM-DD）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternDate", TranslationValue = "{0}不是有效的日期（YYYY-MM-DD）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternTime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternTime", TranslationValue = "{0} is not a valid time (HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternTime", TranslationValue = "{0} is not a valid time (HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternTime", TranslationValue = "{0} is not a valid time (HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternTime", TranslationValue = "{0} is not a valid time (HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternTime", TranslationValue = "{0} is not a valid time (HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternTime", TranslationValue = "{0} is not a valid time (HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternTime", TranslationValue = "{0} is not a valid time (HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternTime", TranslationValue = "{0}不是有效的时间（HH:mm:ss）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternTime", TranslationValue = "{0}不是有效的時間（HH:mm:ss）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternDateTime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternDateTime", TranslationValue = "{0} is not a valid date-time (YYYY-MM-DD HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternDateTime", TranslationValue = "{0} is not a valid date-time (YYYY-MM-DD HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternDateTime", TranslationValue = "{0} is not a valid date-time (YYYY-MM-DD HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternDateTime", TranslationValue = "{0} is not a valid date-time (YYYY-MM-DD HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternDateTime", TranslationValue = "{0} is not a valid date-time (YYYY-MM-DD HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternDateTime", TranslationValue = "{0} is not a valid date-time (YYYY-MM-DD HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternDateTime", TranslationValue = "{0} is not a valid date-time (YYYY-MM-DD HH:mm:ss).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternDateTime", TranslationValue = "{0}不是有效的日期时间（YYYY-MM-DD HH:mm:ss）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternDateTime", TranslationValue = "{0}不是有效的日期時間（YYYY-MM-DD HH:mm:ss）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternBankCard
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternBankCard", TranslationValue = "{0} is not a valid bank card number (16–19 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternBankCard", TranslationValue = "{0} is not a valid bank card number (16–19 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternBankCard", TranslationValue = "{0} is not a valid bank card number (16–19 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternBankCard", TranslationValue = "{0} is not a valid bank card number (16–19 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternBankCard", TranslationValue = "{0} is not a valid bank card number (16–19 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternBankCard", TranslationValue = "{0} is not a valid bank card number (16–19 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternBankCard", TranslationValue = "{0} is not a valid bank card number (16–19 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternBankCard", TranslationValue = "{0}不是有效的银行卡号（16-19位数字）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternBankCard", TranslationValue = "{0}不是有效的銀行卡號（16-19位數字）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternQq
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternQq", TranslationValue = "{0} is not a valid QQ number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternQq", TranslationValue = "{0} is not a valid QQ number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternQq", TranslationValue = "{0} is not a valid QQ number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternQq", TranslationValue = "{0} is not a valid QQ number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternQq", TranslationValue = "{0} is not a valid QQ number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternQq", TranslationValue = "{0} is not a valid QQ number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternQq", TranslationValue = "{0} is not a valid QQ number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternQq", TranslationValue = "{0}不是有效的QQ号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternQq", TranslationValue = "{0}不是有效的QQ號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternWechat
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternWechat", TranslationValue = "{0} is not a valid WeChat ID.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternWechat", TranslationValue = "{0} is not a valid WeChat ID.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternWechat", TranslationValue = "{0} is not a valid WeChat ID.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternWechat", TranslationValue = "{0} is not a valid WeChat ID.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternWechat", TranslationValue = "{0} is not a valid WeChat ID.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternWechat", TranslationValue = "{0} is not a valid WeChat ID.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternWechat", TranslationValue = "{0} is not a valid WeChat ID.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternWechat", TranslationValue = "{0}不是有效的微信号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternWechat", TranslationValue = "{0}不是有效的微信號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLicensePlateCn
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0} is not a valid China mainland license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0} is not a valid China mainland license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0} is not a valid China mainland license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0} is not a valid China mainland license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0} is not a valid China mainland license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0} is not a valid China mainland license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0} is not a valid China mainland license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0}不是有效的中国大陆车牌号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLicensePlateCn", TranslationValue = "{0}不是有效的中國大陸車牌號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLicensePlateUs
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0} is not a valid US license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0} is not a valid US license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0} is not a valid US license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0} is not a valid US license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0} is not a valid US license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0} is not a valid US license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0} is not a valid US license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0}不是有效的美国车牌号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLicensePlateUs", TranslationValue = "{0}不是有效的美國車牌號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLicensePlateJp
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0} is not a valid Japan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0} is not a valid Japan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0} is not a valid Japan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0} is not a valid Japan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0} is not a valid Japan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0} is not a valid Japan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0} is not a valid Japan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0}不是有效的日本车牌号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLicensePlateJp", TranslationValue = "{0}不是有效的日本車牌號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLicensePlateTw
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0} is not a valid Taiwan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0} is not a valid Taiwan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0} is not a valid Taiwan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0} is not a valid Taiwan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0} is not a valid Taiwan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0} is not a valid Taiwan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0} is not a valid Taiwan license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0}不是有效的中国台湾车牌号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLicensePlateTw", TranslationValue = "{0}不是有效的中國台灣車牌號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLicensePlateHk
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0} is not a valid Hong Kong license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0} is not a valid Hong Kong license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0} is not a valid Hong Kong license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0} is not a valid Hong Kong license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0} is not a valid Hong Kong license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0} is not a valid Hong Kong license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0} is not a valid Hong Kong license plate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0}不是有效的中国香港车牌号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLicensePlateHk", TranslationValue = "{0}不是有效的中國香港車牌號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLicensePlate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0} is not a valid license plate (CN/US/JP/TW/HK).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0} is not a valid license plate (CN/US/JP/TW/HK).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0} is not a valid license plate (CN/US/JP/TW/HK).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0} is not a valid license plate (CN/US/JP/TW/HK).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0} is not a valid license plate (CN/US/JP/TW/HK).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0} is not a valid license plate (CN/US/JP/TW/HK).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0} is not a valid license plate (CN/US/JP/TW/HK).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0}不是有效的车牌号（大陆/美国/日本/台湾/香港）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLicensePlate", TranslationValue = "{0}不是有效的車牌號（大陸/美國/日本/台灣/香港）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternMacAddress
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0} is not a valid MAC address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0} is not a valid MAC address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0} is not a valid MAC address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0} is not a valid MAC address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0} is not a valid MAC address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0} is not a valid MAC address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0} is not a valid MAC address.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0}不是有效的MAC地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternMacAddress", TranslationValue = "{0}不是有效的MAC地址", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternHexColor
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternHexColor", TranslationValue = "{0} is not a valid hex color (#RGB or #RRGGBB).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternHexColor", TranslationValue = "{0} is not a valid hex color (#RGB or #RRGGBB).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternHexColor", TranslationValue = "{0} is not a valid hex color (#RGB or #RRGGBB).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternHexColor", TranslationValue = "{0} is not a valid hex color (#RGB or #RRGGBB).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternHexColor", TranslationValue = "{0} is not a valid hex color (#RGB or #RRGGBB).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternHexColor", TranslationValue = "{0} is not a valid hex color (#RGB or #RRGGBB).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternHexColor", TranslationValue = "{0} is not a valid hex color (#RGB or #RRGGBB).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternHexColor", TranslationValue = "{0}不是有效的十六进制颜色值", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternHexColor", TranslationValue = "{0}不是有效的十六進位顏色值", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternVersion
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternVersion", TranslationValue = "{0} is not a valid version (x.y.z).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternVersion", TranslationValue = "{0} is not a valid version (x.y.z).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternVersion", TranslationValue = "{0} is not a valid version (x.y.z).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternVersion", TranslationValue = "{0} is not a valid version (x.y.z).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternVersion", TranslationValue = "{0} is not a valid version (x.y.z).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternVersion", TranslationValue = "{0} is not a valid version (x.y.z).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternVersion", TranslationValue = "{0} is not a valid version (x.y.z).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternVersion", TranslationValue = "{0}不是有效的版本号（x.y.z）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternVersion", TranslationValue = "{0}不是有效的版本號（x.y.z）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternFileExtension
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0} is not a valid file extension.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0} is not a valid file extension.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0} is not a valid file extension.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0} is not a valid file extension.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0} is not a valid file extension.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0} is not a valid file extension.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0} is not a valid file extension.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0}不是有效的文件扩展名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternFileExtension", TranslationValue = "{0}不是有效的文件擴展名", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternCode", TranslationValue = "{0} is not a valid code (3–50 alphanumeric, _-).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternCode", TranslationValue = "{0} is not a valid code (3–50 alphanumeric, _-).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternCode", TranslationValue = "{0} is not a valid code (3–50 alphanumeric, _-).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternCode", TranslationValue = "{0} is not a valid code (3–50 alphanumeric, _-).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternCode", TranslationValue = "{0} is not a valid code (3–50 alphanumeric, _-).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternCode", TranslationValue = "{0} is not a valid code (3–50 alphanumeric, _-).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternCode", TranslationValue = "{0} is not a valid code (3–50 alphanumeric, _-).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternCode", TranslationValue = "{0}不是有效的编码（3-50位字母数字下划线横线）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternCode", TranslationValue = "{0}不是有效的編碼（3-50位字母數字下劃線橫線）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternMaterialCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0} is not a valid material code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0} is not a valid material code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0} is not a valid material code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0} is not a valid material code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0} is not a valid material code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0} is not a valid material code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0} is not a valid material code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0}不是有效的物料编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternMaterialCode", TranslationValue = "{0}不是有效的物料編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternDocumentCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0} is not a valid document code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0} is not a valid document code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0} is not a valid document code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0} is not a valid document code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0} is not a valid document code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0} is not a valid document code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0} is not a valid document code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0}不是有效的文档编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternDocumentCode", TranslationValue = "{0}不是有效的文檔編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternOrderCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0} is not a valid order code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0} is not a valid order code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0} is not a valid order code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0} is not a valid order code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0} is not a valid order code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0} is not a valid order code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0} is not a valid order code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0}不是有效的订单编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternOrderCode", TranslationValue = "{0}不是有效的訂單編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternBatchCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0} is not a valid batch code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0} is not a valid batch code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0} is not a valid batch code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0} is not a valid batch code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0} is not a valid batch code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0} is not a valid batch code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0} is not a valid batch code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0}不是有效的批次编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternBatchCode", TranslationValue = "{0}不是有效的批次編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternSerialNumber
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0} is not a valid serial number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0} is not a valid serial number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0} is not a valid serial number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0} is not a valid serial number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0} is not a valid serial number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0} is not a valid serial number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0} is not a valid serial number.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0}不是有效的序列号", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternSerialNumber", TranslationValue = "{0}不是有效的序號", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternCodeWithDate
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0} is not a valid date-prefixed code (YYYYMMDD+suffix).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0} is not a valid date-prefixed code (YYYYMMDD+suffix).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0} is not a valid date-prefixed code (YYYYMMDD+suffix).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0} is not a valid date-prefixed code (YYYYMMDD+suffix).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0} is not a valid date-prefixed code (YYYYMMDD+suffix).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0} is not a valid date-prefixed code (YYYYMMDD+suffix).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0} is not a valid date-prefixed code (YYYYMMDD+suffix).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0}不是有效的带日期编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternCodeWithDate", TranslationValue = "{0}不是有效的帶日期編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternCodeWithPrefix
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0} is not a valid prefix-separated code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0} is not a valid prefix-separated code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0} is not a valid prefix-separated code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0} is not a valid prefix-separated code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0} is not a valid prefix-separated code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0} is not a valid prefix-separated code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0} is not a valid prefix-separated code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0}不是有效的前缀编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternCodeWithPrefix", TranslationValue = "{0}不是有效的前綴編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternCodeWithSeparator
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0} is not a valid multi-segment code with separators.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0} is not a valid multi-segment code with separators.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0} is not a valid multi-segment code with separators.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0} is not a valid multi-segment code with separators.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0} is not a valid multi-segment code with separators.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0} is not a valid multi-segment code with separators.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0} is not a valid multi-segment code with separators.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0}不是有效的分段编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternCodeWithSeparator", TranslationValue = "{0}不是有效的分段編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternNumericCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0} is not a valid numeric code (3–20 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0} is not a valid numeric code (3–20 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0} is not a valid numeric code (3–20 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0} is not a valid numeric code (3–20 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0} is not a valid numeric code (3–20 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0} is not a valid numeric code (3–20 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0} is not a valid numeric code (3–20 digits).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0}不是有效的数字编码（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternNumericCode", TranslationValue = "{0}不是有效的數字編碼（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternAlphabeticCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0} is not a valid alphabetic code (3–20 letters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0} is not a valid alphabetic code (3–20 letters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0} is not a valid alphabetic code (3–20 letters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0} is not a valid alphabetic code (3–20 letters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0} is not a valid alphabetic code (3–20 letters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0} is not a valid alphabetic code (3–20 letters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0} is not a valid alphabetic code (3–20 letters).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0}不是有效的字母编码（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternAlphabeticCode", TranslationValue = "{0}不是有效的字母編碼（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternUppercaseCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0} is not a valid uppercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0} is not a valid uppercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0} is not a valid uppercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0} is not a valid uppercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0} is not a valid uppercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0} is not a valid uppercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0} is not a valid uppercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0}不是有效的大写编码（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternUppercaseCode", TranslationValue = "{0}不是有效的大寫編碼（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternLowercaseCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0} is not a valid lowercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0} is not a valid lowercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0} is not a valid lowercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0} is not a valid lowercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0} is not a valid lowercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0} is not a valid lowercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0} is not a valid lowercase code (3–20).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0}不是有效的小写编码（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternLowercaseCode", TranslationValue = "{0}不是有效的小寫編碼（3-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternMenuCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0} is not a valid menu code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0} is not a valid menu code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0} is not a valid menu code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0} is not a valid menu code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0} is not a valid menu code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0} is not a valid menu code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0} is not a valid menu code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0}不是有效的菜单编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternMenuCode", TranslationValue = "{0}不是有效的菜單編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternL10nKey
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0} is not a valid localization key.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0} is not a valid localization key.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0} is not a valid localization key.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0} is not a valid localization key.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0} is not a valid localization key.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0} is not a valid localization key.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0} is not a valid localization key.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0}不是有效的本地化键", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternL10nKey", TranslationValue = "{0}不是有效的本地化鍵", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternRoleCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0} is not a valid role code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0} is not a valid role code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0} is not a valid role code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0} is not a valid role code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0} is not a valid role code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0} is not a valid role code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0} is not a valid role code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0}不是有效的角色编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternRoleCode", TranslationValue = "{0}不是有效的角色編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternDeptCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0} is not a valid department code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0} is not a valid department code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0} is not a valid department code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0} is not a valid department code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0} is not a valid department code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0} is not a valid department code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0} is not a valid department code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0}不是有效的部门编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternDeptCode", TranslationValue = "{0}不是有效的部門編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPostCode
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPostCode", TranslationValue = "{0} is not a valid post (position) code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPostCode", TranslationValue = "{0} is not a valid post (position) code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPostCode", TranslationValue = "{0} is not a valid post (position) code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPostCode", TranslationValue = "{0} is not a valid post (position) code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPostCode", TranslationValue = "{0} is not a valid post (position) code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPostCode", TranslationValue = "{0} is not a valid post (position) code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPostCode", TranslationValue = "{0} is not a valid post (position) code.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPostCode", TranslationValue = "{0}不是有效的岗位编码", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPostCode", TranslationValue = "{0}不是有效的崗位編碼", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.importRowLimitExceeded
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "فشل الاستيراد: إجمالي السجلات في هذا الملف هو {0}، وتجاوز الحد {1}. يرجى تقسيم الملف وإعادة المحاولة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "Import failed: total rows in this import is {0}, exceeding the limit {1}. Please split the file and retry.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "Import failed: total rows in this import is {0}, exceeding the limit {1}. Please split the file and retry.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "Import failed: total rows in this import is {0}, exceeding the limit {1}. Please split the file and retry.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "インポート失敗: 今回のインポート総件数は{0}件で、上限{1}件を超えています。分割して再試行してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "가져오기 실패: 이번 가져오기 총 행 수는 {0}개로 제한 {1}개를 초과했습니다. 파일을 분할해 다시 시도하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "Ошибка импорта: количество строк в файле ({0}) превышает лимит {1}. Разделите файл и повторите попытку.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "导入失败：本次导入总记录数为{0}，超过上限{1}，请拆分后重试", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "導入失敗：本次導入總記錄數為{0}，超過上限{1}，請拆分後重試", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.excelHelperNotConfigured
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "لم يتم تهيئة تكوين Excel Helper. يرجى استدعاء TaktExcelHelper.Configure() عند بدء التطبيق.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel helper is not configured. Please call TaktExcelHelper.Configure() at application startup.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel helper is not configured. Please call TaktExcelHelper.Configure() at application startup.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel helper is not configured. Please call TaktExcelHelper.Configure() at application startup.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel Helper の設定が初期化されていません。アプリ起動時に TaktExcelHelper.Configure() を呼び出してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel Helper 설정이 초기화되지 않았습니다. 애플리케이션 시작 시 TaktExcelHelper.Configure()를 호출하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel helper is not configured. Please call TaktExcelHelper.Configure() at application startup.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel 帮助器配置未初始化，请在应用启动时调用 TaktExcelHelper.Configure()", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.excelHelperNotConfigured", TranslationValue = "Excel 輔助器配置未初始化，請在應用啟動時呼叫 TaktExcelHelper.Configure()", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.exportMultiSheetAtLeastOneSheet
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "يجب توفير ورقة واحدة على الأقل للتصدير.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "At least one sheet is required for export.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "At least one sheet is required for export.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "At least one sheet is required for export.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "エクスポートには少なくとも 1 つのシートが必要です。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "내보내기에는 최소 1개의 시트가 필요합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "Для экспорта требуется как минимум один лист.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "导出至少需要一个Sheet", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.exportMultiSheetAtLeastOneSheet", TranslationValue = "導出至少需要一個Sheet", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.exportMultiSheetNameRequired
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "اسم الورقة لا يمكن أن يكون فارغًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "Sheet name cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "Sheet name cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "Sheet name cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "シート名は空にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "시트 이름은 비워둘 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "Имя листа не может быть пустым.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "Sheet名称不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.exportMultiSheetNameRequired", TranslationValue = "Sheet名稱不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.exportMultiSheetDataRequired
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "بيانات الورقة '{0}' لا يمكن أن تكون فارغة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "Data for sheet '{0}' cannot be null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "Data for sheet '{0}' cannot be null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "Data for sheet '{0}' cannot be null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "シート '{0}' のデータは null にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "'{0}' 시트 데이터는 null일 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "Данные листа '{0}' не могут быть null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "Sheet“{0}”的数据不能为null", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.exportMultiSheetDataRequired", TranslationValue = "Sheet「{0}」的資料不能為null", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.excelSheetNotFoundOrEmpty
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "تعذر العثور على ورقة العمل '{0}' أو أنها فارغة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "Worksheet '{0}' was not found or is empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "Worksheet '{0}' was not found or is empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "Worksheet '{0}' was not found or is empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "ワークシート「{0}」が見つからないか、空です。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "워크시트 '{0}'을(를) 찾을 수 없거나 비어 있습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "Лист '{0}' не найден или пуст.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "找不到名为{0}的工作表或工作表为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.excelSheetNotFoundOrEmpty", TranslationValue = "找不到名為{0}的工作表或工作表為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.excelZipNoFiles
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "لا توجد ملفات قابلة للحزم.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "No files available to package.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "No files available to package.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "No files available to package.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "パッケージ化するファイルがありません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "압축할 파일이 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "Нет файлов для упаковки.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "没有可打包的文件", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.excelZipNoFiles", TranslationValue = "沒有可打包的文件", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.argumentNull
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.argumentNull", TranslationValue = "خطأ في المعلمة: القيمة '{0}' لا يمكن أن تكون فارغة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.argumentNull", TranslationValue = "Parameter error: '{0}' cannot be null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.argumentNull", TranslationValue = "Parameter error: '{0}' cannot be null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.argumentNull", TranslationValue = "Parameter error: '{0}' cannot be null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.argumentNull", TranslationValue = "パラメーターエラー: '{0}' は null にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.argumentNull", TranslationValue = "매개변수 오류: '{0}'은(는) null일 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.argumentNull", TranslationValue = "Ошибка параметра: '{0}' не может быть null.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.argumentNull", TranslationValue = "参数错误：{0}不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.argumentNull", TranslationValue = "參數錯誤：{0}不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.argumentError
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.argumentError", TranslationValue = "خطأ في المعلمة: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.argumentError", TranslationValue = "Parameter error: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.argumentError", TranslationValue = "Parameter error: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.argumentError", TranslationValue = "Parameter error: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.argumentError", TranslationValue = "パラメーターエラー: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.argumentError", TranslationValue = "매개변수 오류: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.argumentError", TranslationValue = "Ошибка параметра: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.argumentError", TranslationValue = "参数错误：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.argumentError", TranslationValue = "參數錯誤：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.unauthorizedAccess
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "وصول غير مصرح به.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "Unauthorized access.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "Unauthorized access.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "Unauthorized access.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "未認可のアクセスです。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "권한이 없는 접근입니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "Несанкционированный доступ.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "未授权访问", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.unauthorizedAccess", TranslationValue = "未授權存取", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.systemInternalError
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.systemInternalError", TranslationValue = "خطأ داخلي في النظام، يرجى المحاولة لاحقًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.systemInternalError", TranslationValue = "Internal server error, please try again later.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.systemInternalError", TranslationValue = "Internal server error, please try again later.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.systemInternalError", TranslationValue = "Internal server error, please try again later.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.systemInternalError", TranslationValue = "システム内部エラーです。しばらくしてから再試行してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.systemInternalError", TranslationValue = "시스템 내부 오류입니다. 잠시 후 다시 시도하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.systemInternalError", TranslationValue = "Внутренняя ошибка сервера, повторите попытку позже.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.systemInternalError", TranslationValue = "系统内部错误，请稍后重试", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.systemInternalError", TranslationValue = "系統內部錯誤，請稍後重試", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.businessError
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.businessError", TranslationValue = "فشل تنفيذ العملية التجارية.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.businessError", TranslationValue = "Business operation failed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.businessError", TranslationValue = "Business operation failed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.businessError", TranslationValue = "Business operation failed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.businessError", TranslationValue = "業務処理に失敗しました。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.businessError", TranslationValue = "업무 처리에 실패했습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.businessError", TranslationValue = "Ошибка выполнения бизнес-операции.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.businessError", TranslationValue = "业务处理失败", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.businessError", TranslationValue = "業務處理失敗", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.wordFilterTextRequired
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "النص لا يمكن أن يكون فارغًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "テキストは空にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "텍스트는 비워둘 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "Текст не может быть пустым.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "文本不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterTextRequired", TranslationValue = "文本不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.wordFilterWordsRequired
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "قائمة الكلمات الحساسة لا يمكن أن تكون فارغة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "Sensitive word list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "Sensitive word list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "Sensitive word list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "敏感語リストは空にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "민감어 목록은 비워둘 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "Список чувствительных слов не может быть пустым.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "敏感词列表不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterWordsRequired", TranslationValue = "敏感詞清單不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.wordFilterFileMustBeTxt
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "يجب أن يكون ملف القاموس بصيغة .txt.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "語彙ファイルは .txt 形式である必要があります。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "단어 라이브러리 파일은 .txt 형식이어야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "Файл словаря должен быть в формате .txt.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "词库文件必须是.txt格式", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterFileMustBeTxt", TranslationValue = "詞庫檔案必須是.txt格式", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.wordFilterCheckFailed / find / replace / highlight / stats / groups / list / add / remove / clear
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "فشل الفحص: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "Check failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "Check failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "Check failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "チェックに失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "검사 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "Проверка не выполнена: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "检查失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterCheckFailed", TranslationValue = "檢查失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "فشل البحث: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "Find failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "Find failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "Find failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "検索に失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "검색 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "Поиск не выполнен: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "查找失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterFindFailed", TranslationValue = "查找失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "فشل الاستبدال: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "Replace failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "Replace failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "Replace failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "置換に失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "치환 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "Замена не выполнена: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "替换失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterReplaceFailed", TranslationValue = "替換失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "فشل التمييز: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "Highlight failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "Highlight failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "Highlight failed: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "ハイライトに失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "하이라이트 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "Подсветка не выполнена: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "高亮失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterHighlightFailed", TranslationValue = "高亮失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "فشل الحصول على الإحصائيات: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "Failed to get stats: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "Failed to get stats: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "Failed to get stats: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "統計情報の取得に失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "통계 정보를 가져오지 못했습니다: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "Не удалось получить статистику: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "获取统计信息失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterStatsFailed", TranslationValue = "取得統計資訊失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "فشل الحصول على مجموعات القاموس: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "Failed to get word library groups: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "Failed to get word library groups: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "Failed to get word library groups: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "語彙ライブラリ一覧の取得に失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "단어 라이브러리 그룹 조회 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "Не удалось получить группы словаря: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "获取词库文件列表失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterGroupsFailed", TranslationValue = "取得詞庫檔案列表失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "فشل الحصول على قائمة الكلمات الحساسة: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "Failed to get sensitive words list: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "Failed to get sensitive words list: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "Failed to get sensitive words list: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "敏感語リストの取得に失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "민감어 목록 조회 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "Не удалось получить список чувствительных слов: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "获取敏感词列表失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterListFailed", TranslationValue = "取得敏感詞列表失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "تمت إضافة {0} كلمة حساسة بنجاح.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "Successfully added {0} sensitive words.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "Successfully added {0} sensitive words.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "Successfully added {0} sensitive words.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "敏感語を{0}件追加しました。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "민감어 {0}개를 추가했습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "Успешно добавлено {0} чувствительных слов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "成功添加 {0} 个敏感词", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterAddSuccess", TranslationValue = "成功新增 {0} 個敏感詞", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "فشل إضافة الكلمات الحساسة: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "Failed to add sensitive words: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "Failed to add sensitive words: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "Failed to add sensitive words: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "敏感語の追加に失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "민감어 추가 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "Не удалось добавить чувствительные слова: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "添加敏感词失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterAddFailed", TranslationValue = "新增敏感詞失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "تمت إزالة {0} كلمة حساسة بنجاح.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "Successfully removed {0} sensitive words.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "Successfully removed {0} sensitive words.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "Successfully removed {0} sensitive words.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "敏感語を{0}件削除しました。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "민감어 {0}개를 제거했습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "Успешно удалено {0} чувствительных слов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "成功移除 {0} 个敏感词", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterRemoveSuccess", TranslationValue = "成功移除 {0} 個敏感詞", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "فشل إزالة الكلمات الحساسة: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "Failed to remove sensitive words: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "Failed to remove sensitive words: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "Failed to remove sensitive words: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "敏感語の削除に失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "민감어 제거 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "Не удалось удалить чувствительные слова: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "移除敏感词失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterRemoveFailed", TranslationValue = "移除敏感詞失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "تم مسح مكتبة الكلمات الحساسة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "Sensitive word library has been cleared.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "Sensitive word library has been cleared.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "Sensitive word library has been cleared.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "敏感語ライブラリをクリアしました。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "민감어 라이브러리를 비웠습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "Библиотека чувствительных слов очищена.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "敏感词库已清空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterClearSuccess", TranslationValue = "敏感詞庫已清空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "فشل مسح مكتبة الكلمات الحساسة: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "Failed to clear sensitive word library: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "Failed to clear sensitive word library: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "Failed to clear sensitive word library: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "敏感語ライブラリのクリアに失敗しました: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "민감어 라이브러리 비우기 실패: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "Не удалось очистить библиотеку чувствительных слов: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "清空敏感词库失败：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.wordFilterClearFailed", TranslationValue = "清空敏感詞庫失敗：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.importExcelFileRequired / importExcelOnlyXlsxOrXls / idRouteMismatch / API 参数类
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "يرجى اختيار ملف Excel للاستيراد.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "Please select an Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "Please select an Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "Please select an Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "インポートする Excel ファイルを選択してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "가져올 Excel 파일을 선택하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "Выберите файл Excel для импорта.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "请选择要导入的Excel文件", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.importExcelFileRequired", TranslationValue = "請選擇要匯入的Excel檔案", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "يُسمح فقط بملفات Excel (.xlsx أو .xls).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "Excel ファイル（.xlsx または .xls）のみ対応しています。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "Excel 파일(.xlsx 또는 .xls)만 지원합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "Поддерживаются только файлы Excel (.xlsx или .xls).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "只支持Excel文件（.xlsx或.xls）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.importExcelOnlyXlsxOrXls", TranslationValue = "僅支援Excel檔案（.xlsx或.xls）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.idRouteMismatch", TranslationValue = "المعرف لا يتطابق مع المسار.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.idRouteMismatch", TranslationValue = "ID does not match the route.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.idRouteMismatch", TranslationValue = "ID does not match the route.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.idRouteMismatch", TranslationValue = "ID does not match the route.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.idRouteMismatch", TranslationValue = "IDがルートと一致しません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.idRouteMismatch", TranslationValue = "ID가 경로와 일치하지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.idRouteMismatch", TranslationValue = "Идентификатор не совпадает с маршрутом.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.idRouteMismatch", TranslationValue = "ID与路由不一致", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.idRouteMismatch", TranslationValue = "ID與路由不一致", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "يرجى تقديم قائمة معرفات المستخدمين للحذف.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "削除するユーザーIDのリストを指定してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "삭제할 사용자 ID 목록을 제공하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "Укажите список идентификаторов пользователей для удаления.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "请提供要删除的用户ID列表", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userIdsDeleteRequired", TranslationValue = "請提供要刪除的使用者ID清單", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // TaktUserService 业务键（Identity 用户）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "يُرجى اختيار موظف مرتبط من قائمة الموظفين.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "Please select a linked employee from the employee picker.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "Please select a linked employee from the employee picker.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "Please select a linked employee from the employee picker.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "従業員一覧から関連付ける従業員を選択してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "직원 목록에서 연결할 직원을 선택하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "Выберите связанного сотрудника из списка.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "请选择关联员工（从员工选项列表选择）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userSelectEmployeeRequired", TranslationValue = "請選擇關聯員工（從員工選項清單選擇）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "الموظف المحدد غير موجود أو تم حذفه.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "The selected employee does not exist or has been deleted.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "The selected employee does not exist or has been deleted.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "The selected employee does not exist or has been deleted.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "選択した従業員は存在しないか削除されています。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "선택한 직원이 없거나 삭제되었습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "Выбранный сотрудник не существует или удалён.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "所选员工不存在或已删除", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userEmployeeNotFoundOrDeleted", TranslationValue = "所選員工不存在或已刪除", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "هذا الموظف مرتبط بالفعل بمستخدم؛ مسموح بمستخدم واحد لكل موظف.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "This employee is already linked to a user. One employee may link to only one user.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "This employee is already linked to a user. One employee may link to only one user.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "This employee is already linked to a user. One employee may link to only one user.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "この従業員は既にユーザーに紐付いています。従業員とユーザーは一対一です。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "해당 직원은 이미 사용자에 연결되어 있습니다. 직원과 사용자는 일대일입니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "Сотрудник уже привязан к пользователю. Связь «один сотрудник — один пользователь».", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "该员工已关联用户，不允许重复关联（员工与用户必须一对一）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userEmployeeAlreadyLinked", TranslationValue = "該員工已關聯使用者，不允許重複關聯（員工與使用者必須一對一）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "هذا الموظف مرتبط بمستخدم آخر؛ مسموح بمستخدم واحد لكل موظف.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "This employee is already linked to another user. One employee may link to only one user.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "This employee is already linked to another user. One employee may link to only one user.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "This employee is already linked to another user. One employee may link to only one user.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "この従業員は別のユーザーに紐付いています。従業員とユーザーは一対一です。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "해당 직원은 다른 사용자에 연결되어 있습니다. 직원과 사용자는 일대일입니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "Сотрудник уже привязан к другому пользователю. Связь «один сотрудник — один пользователь».", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "该员工已关联其他用户，不允许重复关联（员工与用户必须一对一）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userEmployeeAlreadyLinkedOtherUser", TranslationValue = "該員工已關聯其他使用者，不允許重複關聯（員工與使用者必須一對一）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "يجب أن يربط المستخدم العادي موظفًا ذكرًا أو أنثى.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "Normal users must link an employee with gender male or female.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "Normal users must link an employee with gender male or female.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "Normal users must link an employee with gender male or female.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "一般ユーザーは性別が男性または女性の従業員を紐付ける必要があります。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "일반 사용자는 성별이 남성 또는 여성인 직원을 연결해야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "Обычный пользователь должен быть связан с сотрудником мужского или женского пола.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "普通用户须关联性别为男或女的员工档案", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userNormalMustLinkMaleOrFemaleEmployee", TranslationValue = "一般使用者須關聯性別為男或女的員工檔案", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "رمز الموظف للمستخدم العادي يجب أن يبدأ بالرقم 1 (ذكر).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "For normal users, the employee code must start with 1 (male).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "For normal users, the employee code must start with 1 (male).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "For normal users, the employee code must start with 1 (male).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "一般ユーザーの従業員コードは 1（男性）で始まる必要があります。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "일반 사용자의 직원 코드는 1(남성)로 시작해야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "Для обычных пользователей код сотрудника должен начинаться с цифры 1 (мужской пол).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "普通用户关联的员工编码须以数字 1 开头（男）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix1Male", TranslationValue = "一般使用者關聯的員工編碼須以數字 1 開頭（男）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "رمز الموظف للمستخدم العادي يجب أن يبدأ بالرقم 2 (أنثى).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "For normal users, the employee code must start with 2 (female).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "For normal users, the employee code must start with 2 (female).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "For normal users, the employee code must start with 2 (female).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "一般ユーザーの従業員コードは 2（女性）で始まる必要があります。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "일반 사용자의 직원 코드는 2(여성)로 시작해야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "Для обычных пользователей код сотрудника должен начинаться с цифры 2 (женский пол).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "普通用户关联的员工编码须以数字 2 开头（女）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userNormalEmployeeCodeMustStartPrefix2Female", TranslationValue = "一般使用者關聯的員工編碼須以數字 2 開頭（女）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "حساب المسؤول أو المسؤول الأعلى يجب أن يربط موظفًا يبدأ رمزه بالرقم 9.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "Admin or super admin accounts must link an employee whose code starts with 9.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "Admin or super admin accounts must link an employee whose code starts with 9.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "Admin or super admin accounts must link an employee whose code starts with 9.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "管理者またはスーパー管理者は、従業員番号が 9 で始まる従業員を紐付ける必要があります。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "관리자 또는 최고 관리자 계정은 직원 번호가 9로 시작하는 직원을 연결해야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "Учётная запись администратора должна быть связана с сотрудником, код которого начинается с 9.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "管理员或超级管理员账号须关联使用用户编号(系统)、且员工编码以 9 开头的职员", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userAdminMustLinkSystemEmployeeCode", TranslationValue = "管理員或超級管理員帳號須關聯使用用戶編號(系統)、且員工編號以 9 開頭的職員", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "بعض الأدوار غير موجودة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "Some roles do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "Some roles do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "Some roles do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "一部のロールが存在しません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "일부 역할이 존재하지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "Некоторые роли не существуют.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "部分角色不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.partialRolesNotFound", TranslationValue = "部分角色不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "بعض الأقسام غير موجودة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "Some departments do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "Some departments do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "Some departments do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "一部の部門が存在しません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "일부 부서가 존재하지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "Некоторые подразделения не существуют.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "部分部门不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.partialDeptsNotFound", TranslationValue = "部分部門不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "بعض المناصب غير موجودة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "Some posts do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "Some posts do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "Some posts do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "一部の職位が存在しません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "일부 직위가 존재하지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "Некоторые должности не существуют.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "部分岗位不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.partialPostsNotFound", TranslationValue = "部分崗位不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "بعض المستأجرين غير موجودين.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "Some tenants do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "Some tenants do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "Some tenants do not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "一部のテナントが存在しません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "일부 테넌트가 존재하지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "Некоторые арендаторы не существуют.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "部分租户不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.partialTenantsNotFound", TranslationValue = "部分租戶不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "اسم المستخدم يحتوي على كلمات حساسة: {0}. يُرجى التعديل والمحاولة مرة أخرى.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "Username contains sensitive words: {0}. Please change it and try again.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "Username contains sensitive words: {0}. Please change it and try again.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "Username contains sensitive words: {0}. Please change it and try again.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "ユーザー名に次の不適切な語が含まれます: {0}。修正してから再度お試しください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "사용자 이름에 민감한 단어가 포함되어 있습니다: {0}. 수정 후 다시 시도하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "Имя пользователя содержит запрещённые слова: {0}. Измените и повторите попытку.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "用户名包含敏感词：{0}，请修改后重试", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userNameContainsSensitiveWords", TranslationValue = "使用者名稱包含敏感詞：{0}，請修改後重試", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userNotFound", TranslationValue = "المستخدم غير موجود.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userNotFound", TranslationValue = "User does not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userNotFound", TranslationValue = "User does not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userNotFound", TranslationValue = "User does not exist.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userNotFound", TranslationValue = "ユーザーが存在しません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userNotFound", TranslationValue = "사용자가 존재하지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userNotFound", TranslationValue = "Пользователь не найден.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userNotFound", TranslationValue = "用户不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userNotFound", TranslationValue = "使用者不存在", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "لا يُسمح بتعديل مستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "Admin user cannot be modified.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "Admin user cannot be modified.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "Admin user cannot be modified.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "管理者ユーザーは変更できません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "관리자 사용자는 수정할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "Учётную запись администратора нельзя изменить.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "管理员用户不允许修改！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotUpdate", TranslationValue = "管理員使用者不允許修改！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "لا يُسمح بتعيين المستأجرين لمستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "Admin user cannot be assigned tenants.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "Admin user cannot be assigned tenants.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "Admin user cannot be assigned tenants.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "管理者ユーザーにテナントを割り当てられません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "관리자 사용자에게 테넌트를 할당할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "Администратору нельзя назначать арендаторов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "管理员用户不允许进行租户分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotAssignTenant", TranslationValue = "管理員使用者不允許進行租戶分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "لا يُسمح بحذف مستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "Admin user cannot be deleted.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "Admin user cannot be deleted.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "Admin user cannot be deleted.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "管理者ユーザーは削除できません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "관리자 사용자는 삭제할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "Учётную запись администратора нельзя удалить.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "管理员用户不允许删除！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotDelete", TranslationValue = "管理員使用者不允許刪除！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "لا يُسمح بحذف مستخدم المسؤول: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "Admin user cannot be deleted: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "Admin user cannot be deleted: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "Admin user cannot be deleted: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "管理者ユーザーは削除できません: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "관리자 사용자는 삭제할 수 없습니다: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "Учётную запись администратора нельзя удалить: {0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "管理员用户不允许删除：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotDeleteNamed", TranslationValue = "管理員使用者不允許刪除：{0}", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "لا يُسمح بتغيير حالة مستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "Admin user status cannot be changed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "Admin user status cannot be changed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "Admin user status cannot be changed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "管理者ユーザーの状態は変更できません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "관리자 사용자의 상태는 변경할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "Статус администратора нельзя изменить.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "管理员用户不允许修改状态！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotChangeStatus", TranslationValue = "管理員使用者不允許修改狀態！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "لا يمكن قفل المستخدم يدويًا؛ يتم القفل تلقائيًا عند تجاوز محاولات تسجيل الدخول الفاشلة للحد.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "Users cannot be locked manually; locking is applied when failed logins exceed the limit.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "Users cannot be locked manually; locking is applied when failed logins exceed the limit.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "Users cannot be locked manually; locking is applied when failed logins exceed the limit.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "手動でユーザーをロックできません。ログイン失敗回数が上限に達したときのみシステムがロックします。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "사용자를 수동으로 잠글 수 없습니다. 로그인 실패 횟수가 제한에 도달하면 시스템이 자동으로 잠급니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "Ручная блокировка пользователя недоступна; блокировка выполняется системой при превышении лимита неудачных входов.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "不能手动锁定用户，锁定只能由系统在登录失败次数达到限制时自动触发", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.userManualLockNotAllowed", TranslationValue = "不能手動鎖定使用者，鎖定僅能由系統在登入失敗次數達到限制時自動觸發", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "لا يُسمح بإعادة تعيين كلمة مرور مستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "Admin user password cannot be reset.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "Admin user password cannot be reset.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "Admin user password cannot be reset.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "管理者ユーザーのパスワードはリセットできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "관리자 사용자의 암호는 재설정할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "Пароль администратора нельзя сбросить.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "管理员用户不允许重置密码！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotResetPassword", TranslationValue = "管理員使用者不允許重設密碼！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "لا يُسمح بإلغاء قفل مستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "Admin user cannot be unlocked.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "Admin user cannot be unlocked.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "Admin user cannot be unlocked.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "管理者ユーザーのロックを解除できません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "관리자 사용자의 잠금을 해제할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "Администратора нельзя разблокировать.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "管理员用户不允许解锁！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotUnlock", TranslationValue = "管理員使用者不允許解除鎖定！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "لا يُسمح بتغيير صورة مستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "Admin user avatar cannot be changed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "Admin user avatar cannot be changed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "Admin user avatar cannot be changed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "管理者ユーザーのアバターは変更できません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "관리자 사용자의 아바타는 변경할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "Аватар администратора нельзя изменить.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "管理员用户不允许修改头像！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotChangeAvatar", TranslationValue = "管理員使用者不允許修改頭像！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "لا يُسمح بتعيين أدوار لمستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "Admin user roles cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "Admin user roles cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "Admin user roles cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "管理者ユーザーにロールを割り当てられません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "관리자 사용자에게 역할을 할당할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "Администратору нельзя назначать роли.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "管理员用户不允许进行角色分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotAssignRoles", TranslationValue = "管理員使用者不允許進行角色分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "لا يُسمح بتعيين أقسام لمستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "Admin user departments cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "Admin user departments cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "Admin user departments cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "管理者ユーザーに部門を割り当てられません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "관리자 사용자에게 부서를 할당할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "Администратору нельзя назначать подразделения.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "管理员用户不允许进行部门分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotAssignDepts", TranslationValue = "管理員使用者不允許進行部門分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "لا يُسمح بتعيين مناصب لمستخدم المسؤول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "Admin user posts cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "Admin user posts cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "Admin user posts cannot be assigned.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "管理者ユーザーに職位を割り当てられません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "관리자 사용자에게 직위를 할당할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "Администратору нельзя назначать должности.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "管理员用户不允许进行岗位分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.adminUserCannotAssignPosts", TranslationValue = "管理員使用者不允許進行崗位分配！", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "يرجى تقديم قائمة معرفات النماذج للحذف.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "Please provide the list of form IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "Please provide the list of form IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "Please provide the list of form IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "削除するフォームIDのリストを指定してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "삭제할 양식 ID 목록을 제공하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "Укажите список идентификаторов форм для удаления.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "请提供要删除的表单ID列表", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.flowFormIdsDeleteRequired", TranslationValue = "請提供要刪除的表單ID清單", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "يرجى تقديم قائمة معرفات المخططات للحذف.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "Please provide the list of scheme IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "Please provide the list of scheme IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "Please provide the list of scheme IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "削除するスキームIDのリストを指定してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "삭제할 스킴 ID 목록을 제공하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "Укажите список идентификаторов схем для удаления.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "请提供要删除的方案ID列表", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.flowSchemeIdsDeleteRequired", TranslationValue = "請提供要刪除的方案ID清單", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "يرجى تقديم InstanceCode أو FlowInstanceId.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "Please provide InstanceCode or FlowInstanceId.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "Please provide InstanceCode or FlowInstanceId.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "Please provide InstanceCode or FlowInstanceId.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "InstanceCode または FlowInstanceId を指定してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "InstanceCode 또는 FlowInstanceId를 제공하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "Укажите InstanceCode или FlowInstanceId.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "请提供 InstanceCode 或 FlowInstanceId", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.flowInstanceCodeOrIdRequired", TranslationValue = "請提供 InstanceCode 或 FlowInstanceId", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "يرجى تقديم قائمة معرفات المثيلات للحذف.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "Please provide the list of instance IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "Please provide the list of instance IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "Please provide the list of instance IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "削除するインスタンスIDのリストを指定してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "삭제할 인스턴스 ID 목록을 제공하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "Укажите список идентификаторов экземпляров для удаления.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "请提供要删除的实例ID列表", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.flowInstanceIdsDeleteRequired", TranslationValue = "請提供要刪除的實例ID清單", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "يرجى تمرير معامل الاستعلام processKey (مثل Leave أو Reimburse).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "Please pass the processKey query parameter (e.g. Leave, Reimburse).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "Please pass the processKey query parameter (e.g. Leave, Reimburse).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "Please pass the processKey query parameter (e.g. Leave, Reimburse).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "クエリパラメータ processKey を指定してください（例: Leave、Reimburse）。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "processKey 쿼리 매개변수를 전달하세요(예: Leave, Reimburse).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "Укажите параметр запроса processKey (например Leave, Reimburse).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "请传入 processKey 查询参数（如 Leave、Reimburse）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.processKeyQueryRequired", TranslationValue = "請傳入 processKey 查詢參數（如 Leave、Reimburse）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "يرجى تمرير processKey لتحديد سير العمل المراد التحقق منه.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "Please pass processKey to specify the workflow to validate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "Please pass processKey to specify the workflow to validate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "Please pass processKey to specify the workflow to validate.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "検証するワークフローを指定するため processKey を渡してください。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "검증할 워크플로를 지정하려면 processKey를 전달하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "Укажите processKey для выбора проверяемого процесса.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "请传入 processKey 查询参数指定要验证的流程（如 Leave、公文审批的流程 Key）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.processKeyQueryForValidationRequired", TranslationValue = "請傳入 processKey 查詢參數以指定要驗證的流程", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.configIdRequired", TranslationValue = "لا يمكن أن يكون configId فارغًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.configIdRequired", TranslationValue = "configId cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.configIdRequired", TranslationValue = "configId cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.configIdRequired", TranslationValue = "configId cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.configIdRequired", TranslationValue = "configId は空にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.configIdRequired", TranslationValue = "configId는 비워둘 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.configIdRequired", TranslationValue = "configId не может быть пустым.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.configIdRequired", TranslationValue = "configId 不能为空。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.configIdRequired", TranslationValue = "configId 不能為空。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "لا يمكن أن يكون configId أو tableName فارغًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId and tableName cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId and tableName cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId and tableName cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId と tableName は空にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId와 tableName은 비워둘 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId и tableName не могут быть пустыми.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId 与 tableName 不能为空。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.configIdAndTableNameRequired", TranslationValue = "configId 與 tableName 不能為空。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "ConfigId في إعداد الجدول فارغ ولا يمكن تحديد قاعدة البيانات.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "Table config ConfigId is empty; cannot determine target database.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "Table config ConfigId is empty; cannot determine target database.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "Table config ConfigId is empty; cannot determine target database.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "テーブル設定の ConfigId が空のため、対象データベースを特定できません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "테이블 설정의 ConfigId가 비어 있어 대상 DB를 결정할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "ConfigId в конфигурации таблицы пуст; невозможно определить БД.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "表配置的 ConfigId 为空，无法确定目标数据库。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.genTableConfigIdEmpty", TranslationValue = "表設定的 ConfigId 為空，無法決定目標資料庫。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "اسم فئة الكيان في إعداد الجدول فارغ ولا يمكن تهيئة الجدول.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "Table config entity class name is empty; cannot initialize table.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "Table config entity class name is empty; cannot initialize table.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "Table config entity class name is empty; cannot initialize table.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "テーブル設定のエンティティクラス名が空のため、テーブルを初期化できません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "테이블 설정의 엔티티 클래스명이 비어 있어 테이블을 초기화할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "Имя класса сущности в конфигурации таблицы пусто; нельзя инициализировать таблицу.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "表配置的实体类名为空，无法初始化数据表。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.genTableEntityClassNameEmpty", TranslationValue = "表設定的實體類別名稱為空，無法初始化資料表。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "لم يتم العثور على مجلد قوالب Generator.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "Generator template directory was not found.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "Generator template directory was not found.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "Generator template directory was not found.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "Generator テンプレートディレクトリが見つかりません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "Generator 템플릿 디렉터리를 찾을 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "Каталог шаблонов Generator не найден.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "未找到 Generator 模板目录。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.generatorTemplateDirectoryNotFound", TranslationValue = "找不到 Generator 範本目錄。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "لم يتم العثور على أي قوالب .sbn تحت مجلد Generator.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "No .sbn templates were found under the Generator directory.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "No .sbn templates were found under the Generator directory.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "No .sbn templates were found under the Generator directory.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "Generator ディレクトリに .sbn テンプレートがありません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "Generator 디렉터리에 .sbn 템플릿이 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "В каталоге Generator не найдено шаблонов .sbn.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "Generator 目录下未找到任何 .sbn 模板。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.generatorNoSbnTemplates", TranslationValue = "Generator 目錄下未找到任何 .sbn 範本。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "لم يتم إنشاء أي ملفات.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "No files were generated.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "No files were generated.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "No files were generated.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "生成されたファイルはありません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "생성된 파일이 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "Файлы не сгенерированы.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "未生成任何文件。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.generatorNoFilesGenerated", TranslationValue = "未產生任何檔案。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.fileRequired", TranslationValue = "الملف لا يمكن أن يكون فارغًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.fileRequired", TranslationValue = "File cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.fileRequired", TranslationValue = "File cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.fileRequired", TranslationValue = "File cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.fileRequired", TranslationValue = "ファイルは空にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.fileRequired", TranslationValue = "파일은 비워둘 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.fileRequired", TranslationValue = "Файл не может быть пустым.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.fileRequired", TranslationValue = "文件不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.fileRequired", TranslationValue = "檔案不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "مفتاح التخزين المؤقت لا يمكن أن يكون فارغًا.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "キャッシュキーは空にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "캐시 키는 비워둘 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "Ключ кэша не может быть пустым.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "缓存键不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.cacheKeyRequired", TranslationValue = "快取鍵不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "تمت الإزالة.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "削除しました。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "제거되었습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "Удалено.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "已移除", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.cacheRemoveSuccess", TranslationValue = "已移除", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "تم تسجيل الخروج بنجاح.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "Logged out successfully.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "Logged out successfully.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "Logged out successfully.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "ログアウトしました。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "로그아웃되었습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "Выход выполнен.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "登出成功", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.auth.logoutSuccess", TranslationValue = "登出成功", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.cacheStatsMemoryOnly / cacheStatsDisabledOrEmpty
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "المزود الحالي هو {0}؛ إحصائيات الذاكرة المؤقتة متاحة فقط لمزود Memory.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "Current provider is {0}; only Memory cache supports statistics.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "Current provider is {0}; only Memory cache supports statistics.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "Current provider is {0}; only Memory cache supports statistics.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "現在のプロバイダーは {0} です。統計は Memory キャッシュのみ対応です。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "현재 공급자는 {0}입니다. 통계는 Memory 캐시만 지원합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "Текущий провайдер: {0}; статистика доступна только для Memory.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "当前提供者为 {0}，仅 Memory 缓存支持统计。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.cacheStatsMemoryOnly", TranslationValue = "目前提供者為 {0}，僅 Memory 快取支援統計。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "الإحصائيات غير مفعّلة أو لا توجد بيانات.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "Statistics are not enabled or there is no data.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "Statistics are not enabled or there is no data.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "Statistics are not enabled or there is no data.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "統計が無効か、データがありません。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "통계가 비활성화되었거나 데이터가 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "Статистика отключена или данных нет.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "统计未启用或暂无数据。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.cacheStatsDisabledOrEmpty", TranslationValue = "統計未啟用或暫無資料。", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // validation.patternPermission
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "validation.patternPermission", TranslationValue = "{0} is not a valid permission string (module:resource:action).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPermission", TranslationValue = "{0} is not a valid permission string (module:resource:action).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "validation.patternPermission", TranslationValue = "{0} is not a valid permission string (module:resource:action).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "validation.patternPermission", TranslationValue = "{0} is not a valid permission string (module:resource:action).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPermission", TranslationValue = "{0} is not a valid permission string (module:resource:action).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPermission", TranslationValue = "{0} is not a valid permission string (module:resource:action).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "validation.patternPermission", TranslationValue = "{0} is not a valid permission string (module:resource:action).", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPermission", TranslationValue = "{0}不是有效的权限标识（module:resource:action）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPermission", TranslationValue = "{0}不是有效的權限標識（module:resource:action）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            // 前端可见业务消息本地化（控制器直返 + 业务异常）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "Please select the Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "Please select the Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "Please select the Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "Please select the Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "Please select the Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "Please select the Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "Please select the Excel file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "请选择要导入的Excel文件", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "请选择要导入的Excel文件", TranslationValue = "請選擇要導入的Excel檔案", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "只支持Excel文件（.xlsx或.xls）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "只支持Excel文件（.xlsx或.xls）", TranslationValue = "只支援Excel檔案（.xlsx或.xls）", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "Please provide the list of user IDs to delete.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "请提供要删除的用户ID列表", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "请提供要删除的用户ID列表", TranslationValue = "請提供要刪除的使用者ID清單", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "文本不能为空", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "文本不能为空", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "文本不能为空", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "文本不能为空", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "文本不能为空", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "文本不能为空", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "文本不能为空", TranslationValue = "Text cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "文本不能为空", TranslationValue = "文本不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "文本不能为空", TranslationValue = "文字不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "敏感词列表不能为空", TranslationValue = "Sensitive words list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "敏感词列表不能为空", TranslationValue = "Sensitive words list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "敏感词列表不能为空", TranslationValue = "Sensitive words list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "敏感词列表不能为空", TranslationValue = "Sensitive words list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "敏感词列表不能为空", TranslationValue = "Sensitive words list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "敏感词列表不能为空", TranslationValue = "Sensitive words list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "敏感词列表不能为空", TranslationValue = "Sensitive words list cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "敏感词列表不能为空", TranslationValue = "敏感词列表不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "敏感词列表不能为空", TranslationValue = "敏感詞清單不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "Word library file must be in .txt format.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "词库文件必须是.txt格式", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "词库文件必须是.txt格式", TranslationValue = "詞庫檔案必須是.txt格式", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "缓存键不能为空", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "缓存键不能为空", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "缓存键不能为空", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "缓存键不能为空", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "缓存键不能为空", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "缓存键不能为空", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "缓存键不能为空", TranslationValue = "Cache key cannot be empty.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "缓存键不能为空", TranslationValue = "缓存键不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "缓存键不能为空", TranslationValue = "快取鍵不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },

            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "已移除", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "已移除", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "已移除", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "已移除", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "已移除", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "已移除", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "已移除", TranslationValue = "Removed.", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "已移除", TranslationValue = "已移除", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "已移除", TranslationValue = "已移除", ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 }
        };

        AppendFrontendBusinessMessages(list);
        return list;
    }

    /// <summary>
    /// 追加应用层业务消息：ResourceKey 统一为 validation.*（九语种；ar/es/fr/ja/ko/ru 暂与 en-US 同源文案，避免中文作键）。
    /// 若主列表已存在同 ResourceKey 则跳过。
    /// </summary>
    private static void AppendFrontendBusinessMessages(List<TaktTranslation> list)
    {
        var existingKeys = list
            .Where(t => t.ResourceType == "Frontend" && t.ResourceGroup == "Validation")
            .Select(t => t.ResourceKey)
            .ToHashSet();

        void AddValidationKey(string resourceKey, string enUs, string zhCn, string zhTw)
        {
            if (existingKeys.Contains(resourceKey))
                return;

            list.Add(new TaktTranslation { CultureCode = "ar-SA", ResourceKey = resourceKey, TranslationValue = enUs, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "en-US", ResourceKey = resourceKey, TranslationValue = enUs, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "es-ES", ResourceKey = resourceKey, TranslationValue = enUs, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "fr-FR", ResourceKey = resourceKey, TranslationValue = enUs, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "ja-JP", ResourceKey = resourceKey, TranslationValue = enUs, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "ko-KR", ResourceKey = resourceKey, TranslationValue = enUs, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "ru-RU", ResourceKey = resourceKey, TranslationValue = enUs, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "zh-CN", ResourceKey = resourceKey, TranslationValue = zhCn, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            list.Add(new TaktTranslation { CultureCode = "zh-TW", ResourceKey = resourceKey, TranslationValue = zhTw, ResourceType = "Frontend", ResourceGroup = "Validation", OrderNum = 0 });
            existingKeys.Add(resourceKey);
        }

        AddValidationKey("validation.partialUsersNotFound", "Some users do not exist.", "部分用户不存在", "部分使用者不存在");
        AddValidationKey("validation.deptNotFound", "Department does not exist.", "部门不存在", "部門不存在");
        AddValidationKey("validation.postNotFound", "Post does not exist.", "岗位不存在", "崗位不存在");
        AddValidationKey("validation.employeeCareerNotFound", "Employee career information does not exist.", "员工职业信息不存在", "員工職業資訊不存在");
        AddValidationKey("validation.employeeAttachmentNotFound", "Employee attachment does not exist.", "员工附件不存在", "員工附件不存在");
        AddValidationKey("validation.employeeOnlyAdminCanCreateSystemCode", "Only administrators or super administrators can create employee profiles with user code (system, prefix 9).", "仅管理员或超级管理员可创建用户编号(系统)职员档案", "僅管理員或超級管理員可建立用戶編號(系統)職員檔案");
        AddValidationKey("validation.employeeNonSystemGenderRequired", "Non-system employees must specify gender as male (1) or female (2) to generate employee codes with prefix 1 or 2.", "非系统用户职员须指定性别为男(1)或女(2)，以便生成前缀 1/2 的员工编号", "非系統使用者職員須指定性別為男(1)或女(2)，以便產生前綴 1/2 的員工編號");
        AddValidationKey("validation.hrEmployeeNotFound", "Employee does not exist.", "员工不存在", "員工不存在");
        AddValidationKey("validation.employeeTransferNotFound", "Employee transfer does not exist.", "员工调动不存在", "員工調動不存在");
        AddValidationKey("validation.authInvalidCredentials", "Incorrect username or password.", "用户名或密码错误", "使用者名稱或密碼錯誤");
        AddValidationKey("validation.authUserDisabledOrLocked", "User has been disabled or locked.", "用户已被禁用或锁定", "使用者已被停用或鎖定");
        AddValidationKey("validation.authNotLoggedIn", "User is not logged in.", "用户未登录", "使用者未登入");
        AddValidationKey("validation.authInvalidUserId", "Invalid user ID.", "用户ID无效", "使用者ID無效");
        AddValidationKey("validation.authUsernamePasswordRequired", "Username and password cannot be empty.", "用户名和密码不能为空", "使用者名稱和密碼不能為空");
        AddValidationKey("validation.authUnsupportedGrantType", "Unsupported grant type.", "不支持的授权类型", "不支援的授權類型");
        AddValidationKey("validation.authRefreshSubjectInvalid", "Invalid user information in token.", "用户信息无效", "使用者資訊無效");
        AddValidationKey("validation.authRefreshUserInvalid", "User does not exist or is not enabled.", "用户不存在或未启用", "使用者不存在或未啟用");
        AddValidationKey("validation.authAccountLockedWithReason", "Account locked. Reason: {0}", "账户已被锁定，原因：{0}", "帳戶已被鎖定，原因：{0}");
        AddValidationKey("validation.authOpenIddictRequestMissing", "Unable to obtain OpenID Connect request.", "无法获取OpenID Connect请求", "無法取得 OpenID Connect 請求");
        AddValidationKey("validation.authRefreshPrincipalMissing", "Unable to obtain authentication principal.", "无法获取认证主体", "無法取得認證主體");
        AddValidationKey("validation.authUseConnectTokenForLogin", "Sign-in must use the /api/TaktAuth/connect/token endpoint.", "登录应直接调用 /api/TaktAuth/connect/token 端点", "登入請直接呼叫 /api/TaktAuth/connect/token 端點");
        AddValidationKey("validation.authUseConnectTokenForRefresh", "Token refresh must use the /api/TaktAuth/connect/token endpoint.", "刷新令牌应直接调用 /api/TaktAuth/connect/token 端点", "重新整理權杖請直接呼叫 /api/TaktAuth/connect/token 端點");
        AddValidationKey("validation.authLogUserNotFound", "User does not exist", "用户不存在", "使用者不存在");
        AddValidationKey("validation.authLogWrongPassword", "Incorrect password", "密码错误", "密碼錯誤");
        AddValidationKey("validation.authLogUserDisabled", "User disabled", "用户已被禁用", "使用者已被停用");
        AddValidationKey("validation.authLogUserLocked", "User locked", "用户已被锁定", "使用者已被鎖定");
        AddValidationKey("validation.authLoginSuccess", "Login succeeded.", "登录成功", "登入成功");
        AddValidationKey("validation.authUnknownUser", "Unknown user", "未知用户", "未知使用者");
        AddValidationKey("validation.recordNotFound", "Record does not exist.", "记录不存在", "記錄不存在");
        AddValidationKey("validation.loginLogStatusSuccess", "Success", "成功", "成功");
        AddValidationKey("validation.loginLogStatusFailure", "Failure", "失败", "失敗");
        AddValidationKey("validation.captchaIdRequired", "Captcha ID cannot be empty.", "验证码ID不能为空", "驗證碼ID不能為空");
        AddValidationKey("validation.captchaNotFoundOrExpired", "Captcha does not exist or has expired.", "验证码不存在或已过期", "驗證碼不存在或已過期");
        AddValidationKey("validation.captchaExpired", "Captcha has expired.", "验证码已过期", "驗證碼已過期");
        AddValidationKey("validation.captchaTooFastRetry", "Too fast. Please try again later.", "操作过快，请稍后重试", "操作過快，請稍後重試");
        AddValidationKey("validation.captchaUserInputEmpty", "User input cannot be empty.", "用户输入不能为空", "使用者輸入不能為空");
        AddValidationKey("validation.captchaNeedPositionTimeSpent", "Provide behavior data such as position and timeSpent.", "请提供 position、timeSpent 等行为数据", "請提供 position、timeSpent 等行為資料");
        AddValidationKey("validation.captchaNeedTimeSpent", "Provide timeSpent (duration).", "请提供 timeSpent 操作时长", "請提供 timeSpent 操作時長");
        AddValidationKey("validation.captchaTimeSpentInvalid", "Provide a valid timeSpent value.", "请提供有效的 timeSpent 操作时长", "請提供有效的 timeSpent 操作時長");
        AddValidationKey("validation.captchaSlideTooFast", "Too fast. Complete the slide at a normal speed.", "操作过快，请正常滑动完成验证", "操作過快，請以正常速度滑動完成驗證");
        AddValidationKey("validation.captchaInvalidUserInputFormat", "Invalid user input format.", "无效的用户输入格式", "無效的使用者輸入格式");
        AddValidationKey("validation.captchaVerifySuccess", "Verification succeeded.", "验证成功", "驗證成功");
        AddValidationKey("validation.captchaVerifyPositionMismatch", "Verification failed: position does not match.", "验证失败，位置不匹配", "驗證失敗，位置不符");
        AddValidationKey("validation.captchaVerifyBehaviorMismatch", "Verification failed: behavior does not meet requirements.", "验证失败，行为特征不符合要求", "驗證失敗，行為特徵不符合要求");
        AddValidationKey("validation.captchaVerifyProcessError", "An error occurred during verification.", "验证过程中发生错误", "驗證過程中發生錯誤");
        AddValidationKey("validation.captchaBehaviorDataInvalid", "Provide valid behavior data (e.g. position, timeSpent, mouseTrajectory).", "请提供有效的行为数据（如 position、timeSpent、mouseTrajectory）", "請提供有效的行為資料（如 position、timeSpent、mouseTrajectory）");
        AddValidationKey("validation.captchaBehaviorTooFast", "Too fast. Complete verification at a normal speed.", "操作过快，请正常完成验证", "操作過快，請以正常速度完成驗證");
        AddValidationKey("validation.captchaMouseTrajectoryRequired", "Provide mouseTrajectory (mouse path).", "请提供 mouseTrajectory 鼠标轨迹", "請提供 mouseTrajectory 滑鼠軌跡");
        AddValidationKey("validation.captchaTrajectoryInsufficient", "Not enough trajectory points. Slide normally to complete verification.", "鼠标轨迹数据不足，请正常滑动完成验证", "滑鼠軌跡資料不足，請正常滑動完成驗證");
        AddValidationKey("validation.captchaGenerateFailed", "Failed to generate captcha.", "生成验证码失败", "產生驗證碼失敗");
        AddValidationKey("validation.captchaVerifyRequestRequired", "Verify request cannot be empty.", "验证请求不能为空", "驗證請求不能為空");
        AddValidationKey("validation.captchaDisabledAutoPass", "Captcha is disabled; request allowed.", "验证码未启用，自动通过", "驗證碼未啟用，自動通過");
        AddValidationKey("validation.captchaVerifyEndpointFailed", "Captcha verification failed.", "验证验证码失败", "驗證驗證碼失敗");
        AddValidationKey("validation.plantNotFound", "Plant master data does not exist.", "工厂表不存在", "工廠主檔不存在");
        AddValidationKey("validation.genTableColumnConfigNotFound", "Code generation column configuration does not exist.", "代码生成字段配置不存在", "程式碼產生欄位設定不存在");
        AddValidationKey("validation.menuNotFound", "Menu does not exist.", "菜单不存在", "選單不存在");
        AddValidationKey("validation.menuStatusAllowedValues", "Menu status only supports 1=enabled, 0=disabled.", "菜单状态仅支持 1=启用，0=禁用", "選單狀態僅支援 1=啟用，0=停用");
        AddValidationKey("validation.partialMenusNotFound", "Some menus do not exist.", "部分菜单不存在", "部分選單不存在");
        AddValidationKey("validation.roleNotFound", "Role does not exist.", "角色不存在", "角色不存在");
        AddValidationKey("validation.roleSystemProtectedCannotModify", "Protected system roles (Administrator/Guest) cannot be modified.", "受保护系统角色（管理员/来宾）不允许修改！", "受保護系統角色（管理員/來賓）不允許修改！");
        AddValidationKey("validation.roleSystemProtectedCannotDelete", "Protected system roles (Administrator/Guest) cannot be deleted.", "受保护系统角色（管理员/来宾）不允许删除！", "受保護系統角色（管理員/來賓）不允許刪除！");
        AddValidationKey("validation.roleSystemProtectedCannotChangeStatus", "Protected system roles (Administrator/Guest) cannot change status.", "受保护系统角色（管理员/来宾）不允许修改状态！", "受保護系統角色（管理員/來賓）不允許修改狀態！");
        AddValidationKey("validation.roleStatusAllowedValues", "Role status only supports 1=enabled, 0=disabled.", "角色状态仅支持 1=启用，0=禁用", "角色狀態僅支援 1=啟用，0=停用");
        AddValidationKey("validation.adminRoleCannotAssignMenus", "Administrator roles cannot assign menus.", "管理员角色不允许进行菜单分配！", "管理員角色不允許進行選單分配！");
        AddValidationKey("validation.adminRoleCannotAssignUsers", "Administrator roles cannot assign users.", "管理员角色不允许进行用户分配！", "管理員角色不允許進行使用者分配！");
        AddValidationKey("validation.adminRoleCannotAssignDepts", "Administrator roles cannot assign departments.", "管理员角色不允许进行部门分配！", "管理員角色不允許進行部門分配！");
        AddValidationKey("validation.tenantNotFound", "Tenant does not exist.", "租户不存在", "租戶不存在");
        AddValidationKey("validation.tenantStatusAllowedValues", "Tenant status only supports 1=enabled, 0=disabled.", "租户状态仅支持 1=启用，0=禁用", "租戶狀態僅支援 1=啟用，0=停用");
        AddValidationKey("validation.i18nTranslationNotFound", "Translation does not exist.", "翻译不存在", "翻譯不存在");
        AddValidationKey("validation.i18nLanguageNotFound", "Language does not exist.", "语言不存在", "語言不存在");
        AddValidationKey("validation.holidayNotFound", "Holiday does not exist.", "假日不存在", "假日不存在");
        AddValidationKey("validation.attendanceSettingNotFound", "Attendance setting does not exist.", "考勤设置不存在", "考勤設定不存在");
        AddValidationKey("validation.attendanceSettingCodeDuplicate", "Attendance setting code already exists.", "考勤方案编码已存在", "考勤方案編碼已存在");
        AddValidationKey("validation.workShiftNotFound", "Work shift does not exist.", "班次不存在", "班次不存在");
        AddValidationKey("validation.workShiftCodeDuplicate", "Work shift code already exists.", "班次编码已存在", "班次編碼已存在");
        AddValidationKey("validation.importRowAttendanceSettingCodeRequired", "Row {0}: Setting code is required.", "第{0}行：方案编码不能为空", "第{0}行：方案編碼不能為空");
        AddValidationKey("validation.importRowAttendanceSettingNameRequired", "Row {0}: Setting name is required.", "第{0}行：方案名称不能为空", "第{0}行：方案名稱不能為空");
        AddValidationKey("validation.importRowAttendanceSettingDuplicateCode", "Row {0}: Duplicate setting code in file or database.", "第{0}行：方案编码与库内或文件内重复", "第{0}行：方案編碼與庫內或檔案內重複");
        AddValidationKey("validation.importRowWorkShiftCodeRequired", "Row {0}: Shift code is required.", "第{0}行：班次编码不能为空", "第{0}行：班次編碼不能為空");
        AddValidationKey("validation.importRowWorkShiftNameRequired", "Row {0}: Shift name is required.", "第{0}行：班次名称不能为空", "第{0}行：班次名稱不能為空");
        AddValidationKey("validation.importRowWorkShiftDuplicateCode", "Row {0}: Duplicate shift code in file or database.", "第{0}行：班次编码与库内或文件内重复", "第{0}行：班次編碼與庫內或檔案內重複");
        AddValidationKey("validation.attendanceDeviceNotFound", "Attendance device does not exist.", "考勤设备不存在", "考勤設備不存在");
        AddValidationKey("validation.attendanceDeviceCodeDuplicate", "Device code already exists.", "设备编码已存在", "設備編碼已存在");
        AddValidationKey("validation.attendanceSourceNotFound", "Attendance source record does not exist.", "考勤源记录不存在", "考勤源記錄不存在");
        AddValidationKey("validation.attendanceResultNotFound", "Attendance result does not exist.", "考勤结果不存在", "考勤結果不存在");
        AddValidationKey("validation.attendanceResultDuplicateEmployeeDate", "Attendance result already exists for this employee and date.", "该员工该考勤日期的结果已存在", "該員工該考勤日期的結果已存在");
        AddValidationKey("validation.attendanceExceptionNotFound", "Attendance exception does not exist.", "考勤异常不存在", "考勤異常不存在");
        AddValidationKey("validation.attendanceCorrectionNotFound", "Attendance correction does not exist.", "补卡记录不存在", "補卡記錄不存在");
        AddValidationKey("validation.attendancePunchNotFound", "Attendance punch does not exist.", "打卡记录不存在", "打卡記錄不存在");
        AddValidationKey("validation.shiftScheduleNotFound", "Shift schedule does not exist.", "排班记录不存在", "排班記錄不存在");
        AddValidationKey("validation.shiftScheduleDuplicateEmployeeDate", "Shift schedule already exists for this employee and date.", "该员工该日期的排班已存在", "該員工該日期的排班已存在");
        AddValidationKey("validation.overtimeNotFound", "Overtime record does not exist.", "加班记录不存在", "加班記錄不存在");
        AddValidationKey("validation.importRowAttendanceDeviceCodeRequired", "Row {0}: Device code is required.", "第{0}行：设备编码不能为空", "第{0}行：設備編碼不能為空");
        AddValidationKey("validation.importRowAttendanceDeviceNameRequired", "Row {0}: Device name is required.", "第{0}行：设备名称不能为空", "第{0}行：設備名稱不能為空");
        AddValidationKey("validation.importRowAttendanceDeviceDuplicateCode", "Row {0}: Duplicate device code in file or database.", "第{0}行：设备编码与库内或文件内重复", "第{0}行：設備編碼與庫內或檔案內重複");
        AddValidationKey("validation.importRowAttendanceSourceDeviceCodeRequired", "Row {0}: Device code is required.", "第{0}行：设备编码不能为空", "第{0}行：設備編碼不能為空");
        AddValidationKey("validation.importRowAttendanceSourceDeviceNotFound", "Row {0}: Device code not found.", "第{0}行：设备编码不存在", "第{0}行：設備編碼不存在");
        AddValidationKey("validation.importRowAttendanceSourceEnrollRequired", "Row {0}: Enroll number is required.", "第{0}行：登记号不能为空", "第{0}行：登記號不能為空");
        AddValidationKey("validation.importRowAttendanceResultEmployeeRequired", "Row {0}: Employee ID is required.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowAttendanceResultDuplicateEmployeeDate", "Row {0}: Duplicate employee and attendance date.", "第{0}行：员工与考勤日期重复", "第{0}行：員工與考勤日期重複");
        AddValidationKey("validation.importRowAttendanceExceptionEmployeeRequired", "Row {0}: Employee ID is required.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowAttendanceExceptionSummaryRequired", "Row {0}: Summary is required.", "第{0}行：说明不能为空", "第{0}行：說明不能為空");
        AddValidationKey("validation.importRowAttendanceCorrectionEmployeeRequired", "Row {0}: Employee ID is required.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowAttendanceCorrectionReasonRequired", "Row {0}: Reason is required.", "第{0}行：原因不能为空", "第{0}行：原因不能為空");
        AddValidationKey("validation.importRowAttendancePunchEmployeeRequired", "Row {0}: Employee ID is required.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowAttendancePunchTimeRequired", "Row {0}: Punch time is required.", "第{0}行：打卡时间不能为空", "第{0}行：打卡時間不能為空");
        AddValidationKey("validation.importRowOvertimeEmployeeRequired", "Row {0}: Employee ID is required.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowShiftScheduleEmployeeRequired", "Row {0}: Employee ID is required.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowShiftScheduleShiftCodeRequired", "Row {0}: Shift code is required.", "第{0}行：班次编码不能为空", "第{0}行：班次編碼不能為空");
        AddValidationKey("validation.importRowShiftScheduleShiftNotFound", "Row {0}: Shift code not found.", "第{0}行：班次编码不存在", "第{0}行：班次編碼不存在");
        AddValidationKey("validation.importRowShiftScheduleDuplicateEmployeeDate", "Row {0}: Duplicate employee and schedule date.", "第{0}行：员工与排班日期重复", "第{0}行：員工與排班日期重複");
        AddValidationKey("validation.dictDataNotFound", "Dictionary data does not exist.", "字典数据不存在", "字典資料不存在");
        AddValidationKey("validation.dictTypeSqlCodeRequired", "When data source is SQL query, dictionary type code cannot be empty.", "数据源为SQL查询时，字典类型编码不能为空", "資料來源為SQL查詢時，字典類型編碼不能為空");
        AddValidationKey("validation.dictTypeSqlScriptRequired", "When data source is SQL query, SQL script cannot be empty.", "数据源为SQL查询时，SQL脚本不能为空", "資料來源為SQL查詢時，SQL腳本不能為空");
        AddValidationKey("validation.dictTypeNotFound", "Dictionary type does not exist.", "字典类型不存在", "字典類型不存在");
        AddValidationKey("validation.dictTypeSqlConfigIdRequired", "When data source is SQL query, database config ID cannot be empty.", "数据源为SQL查询时，数据库配置ID不能为空", "資料來源為SQL查詢時，資料庫設定ID不能為空");
        AddValidationKey("validation.storedFileNotFound", "File does not exist.", "文件不存在", "檔案不存在");
        AddValidationKey("validation.storedFilePathNotFound", "File path does not exist.", "文件路径不存在", "檔案路徑不存在");
        AddValidationKey("validation.fileDownloadOssNotImplemented", "OSS object storage download is not implemented.", "OSS对象存储下载功能未实现", "OSS 物件儲存下載功能尚未實作");
        AddValidationKey("validation.fileDownloadFtpNotImplemented", "FTP storage download is not implemented.", "FTP存储下载功能未实现", "FTP 儲存下載功能尚未實作");
        AddValidationKey("validation.fileStreamEmpty", "File stream is empty.", "文件流为空", "檔案串流為空");
        AddValidationKey("validation.filePathEmpty", "File path is empty.", "文件路径为空", "檔案路徑為空");
        AddValidationKey("validation.webHostEnvironmentNotInjected", "IWebHostEnvironment is not injected; cannot read relative path files.", "IWebHostEnvironment 未注入，无法读取相对路径文件", "IWebHostEnvironment 未注入，無法讀取相對路徑檔案");
        AddValidationKey("validation.leaveRecordNotFound", "Leave record does not exist.", "请假记录不存在", "請假記錄不存在");
        AddValidationKey("validation.leaveFlowInstanceMismatch", "The process instance does not match the leave record.", "流程实例与请假记录不匹配", "流程實例與請假記錄不匹配");
        AddValidationKey("validation.loginRequiredFirst", "Please log in first.", "请先登录", "請先登入");
        AddValidationKey("validation.numberingRuleNotFound", "Numbering rule does not exist.", "编码规则不存在", "編碼規則不存在");
        AddValidationKey("validation.genTableConfigNotFound", "Code generation table configuration does not exist.", "代码生成表配置不存在", "程式碼產生表設定不存在");
        AddValidationKey("validation.announcementNotFound", "Announcement does not exist.", "公告不存在", "公告不存在");
        AddValidationKey("validation.purchasePriceNotFound", "Purchase price does not exist.", "采购价格不存在", "採購價格不存在");
        AddValidationKey("validation.ruleCodeRequired", "Rule code cannot be empty.", "规则编码不能为空", "規則編碼不能為空");
        AddValidationKey("validation.settingNotFound", "Setting does not exist.", "设置不存在", "設定不存在");
        AddValidationKey("validation.siteMessageNotFound", "Message does not exist.", "消息不存在", "訊息不存在");
        AddValidationKey("validation.purchaseOrderCodeRequired", "Order code cannot be empty.", "订单编码不能为空", "訂單編碼不能為空");
        AddValidationKey("validation.purchaseOrderNotFound", "Purchase order does not exist.", "采购订单不存在", "採購訂單不存在");
        AddValidationKey("validation.purchaseOrderApprovedCannotModify", "Order has been approved and cannot be modified.", "订单已审核，不允许修改", "訂單已審核，不允許修改");
        AddValidationKey("validation.purchaseOrderApprovedCannotDelete", "Order has been approved and cannot be deleted.", "订单已审核，不允许删除", "訂單已審核，不允許刪除");
        AddValidationKey("validation.costCenterNotFound", "Cost center does not exist.", "成本中心不存在", "成本中心不存在");
        AddValidationKey("validation.costCenterHasChildrenCannotDelete", "Sub cost centers exist; cannot delete.", "存在子成本中心，无法删除", "存在子成本中心，無法刪除");
        AddValidationKey("validation.accountingTitleNotFound", "Accounting title does not exist.", "会计科目不存在", "會計科目不存在");
        AddValidationKey("validation.accountingTitleHasChildrenCannotDelete", "Sub accounts exist; cannot delete.", "存在子科目，无法删除", "存在子科目，無法刪除");

        AddValidationKey("validation.purchaseOrderBatchApprovedCannotDelete", "The following orders are approved and cannot be deleted: {0}", "以下订单已审核，不允许删除：{0}", "以下訂單已審核，不允許刪除：{0}");
        AddValidationKey("validation.numberingRuleEnabledNotFound", "No enabled numbering rule found for code: {0}", "未找到启用的编码规则：{0}", "未找到啟用的編碼規則：{0}");
        AddValidationKey("validation.numberingRuleEnabledNotFoundWithOrg", "No enabled numbering rule for code: {0} (company: {1}, department: {2}).", "未找到启用的编码规则：{0}（公司：{1}，部门：{2}）", "未找到啟用的編碼規則：{0}（公司：{1}，部門：{2}）");
        AddValidationKey("validation.leaveDuplicateForEmployeeDateType", "A leave record already exists for this employee on {0} with type {1}.", "该员工在 {0} 的 {1} 请假记录已存在", "該員工在 {0} 的 {1} 請假記錄已存在");
        AddValidationKey("validation.fileStorageTypeUnknown", "Unknown storage type: {0}", "未知的存储类型: {0}", "未知的儲存類型: {0}");
        AddValidationKey("validation.storedFileNotFoundWithPath", "File does not exist: {0}", "文件不存在: {0}", "檔案不存在: {0}");
        AddValidationKey("validation.dictTypeSqlCodePrefixInvalid", "When data source is SQL query, dictionary type code must start with 'sql_'. Current code: {0}", "数据源为SQL查询时，字典类型编码必须以 'sql_' 开头，当前编码：{0}", "資料來源為SQL查詢時，字典類型編碼必須以 'sql_' 開頭，目前編碼：{0}");
        AddValidationKey("validation.dictTypeNotFoundByCode", "Dictionary type {0} does not exist.", "字典类型 {0} 不存在", "字典類型 {0} 不存在");
        AddValidationKey("validation.i18nLanguageNotFoundByCode", "Language {0} does not exist.", "语言 {0} 不存在", "語言 {0} 不存在");
        AddValidationKey("validation.adminUserCannotAssignTenantNamed", "Administrator users ({0}) cannot be assigned to tenants.", "管理员用户（{0}）不允许进行租户分配！", "管理員使用者（{0}）不允許進行租戶分配！");
        AddValidationKey("validation.roleSystemProtectedCannotDeleteNamed", "Protected system roles (Administrator/Guest) cannot be deleted: {0}", "受保护系统角色（管理员/来宾）不允许删除：{0}", "受保護系統角色（管理員/來賓）不允許刪除：{0}");
        AddValidationKey("validation.costCenterBulkHasChildrenCannotDelete", "The following cost centers have children and cannot be deleted: {0}", "以下成本中心存在子成本中心，无法删除：{0}", "以下成本中心存在子成本中心，無法刪除：{0}");
        AddValidationKey("validation.accountingTitleBulkHasChildrenCannotDelete", "The following titles have sub-accounts and cannot be deleted: {0}", "以下科目存在子科目，无法删除：{0}", "以下科目存在子科目，無法刪除：{0}");

        // Excel 导入（各服务 Import*，validation.import*）
        AddValidationKey("validation.importExcelNoData", "No data in the Excel file.", "Excel文件中没有数据", "Excel檔案中沒有資料");
        AddValidationKey("validation.importProcessFailedWithReason", "Import process error: {0}", "导入过程发生错误：{0}", "匯入過程發生錯誤：{0}");
        AddValidationKey("validation.importBatchInsertFailed", "Records {0}–{1}: batch insert failed: {2}", "第{0}～{1}条批量插入失败：{2}", "第{0}～{1}筆批次插入失敗：{2}");
        AddValidationKey("validation.importRecordCountExceedsLimit", "Import failed: total rows {0} exceed limit {1}. Split the file and retry.", "导入失败：本次导入总记录数为{0}，超过上限{1}，请拆分后重试", "匯入失敗：本次匯入總筆數為{0}，超過上限{1}，請拆分後重試");
        AddValidationKey("validation.importFileExceedsMaxRows", "A single import allows at most {0} rows; this file has {1}. Reduce rows or import in batches.", "单次导入最多{0}条记录，当前文件有{1}条，请分批导入或减少单次导入行数", "單次匯入最多{0}筆記錄，目前檔案有{1}筆，請分批匯入或減少單次匯入行數");
        AddValidationKey("validation.importRowUnhandledException", "Row {0}: {1}", "第{0}行：{1}", "第{0}行：{1}");
        AddValidationKey("validation.importRowFailedWithReason", "Row {0}: Import failed - {1}", "第{0}行：导入失败 - {1}", "第{0}行：匯入失敗 - {1}");
        AddValidationKey("validation.importRowFlowProcessKeyRequired", "Row {0}: Process key cannot be empty.", "第{0}行：流程Key不能为空", "第{0}行：流程Key不能為空");
        AddValidationKey("validation.importRowFlowProcessKeyExists", "Row {0}: Process key {1} already exists.", "第{0}行：流程Key {1} 已存在", "第{0}行：流程Key {1} 已存在");
        AddValidationKey("validation.importRowFlowFormCodeRequired", "Row {0}: Form code cannot be empty.", "第{0}行：表单编码不能为空", "第{0}行：表單編碼不能為空");
        AddValidationKey("validation.importRowFlowFormCodeExists", "Row {0}: Form code {1} already exists.", "第{0}行：表单编码 {1} 已存在", "第{0}行：表單編碼 {1} 已存在");
        AddValidationKey("validation.importRowAccountingTitleCodeRequired", "Row {0}: Title code cannot be empty.", "第{0}行：科目编码不能为空", "第{0}行：科目編碼不能為空");
        AddValidationKey("validation.importRowAccountingTitleNameRequired", "Row {0}: Title name cannot be empty.", "第{0}行：科目名称不能为空", "第{0}行：科目名稱不能為空");
        AddValidationKey("validation.importRowAccountingTitleCodeExists", "Row {0}: Title code {1} already exists.", "第{0}行：科目编码 {1} 已存在", "第{0}行：科目編碼 {1} 已存在");
        AddValidationKey("validation.importRowCostCenterCodeRequired", "Row {0}: Cost center code cannot be empty.", "第{0}行：成本中心编码不能为空", "第{0}行：成本中心編碼不能為空");
        AddValidationKey("validation.importRowCostCenterNameRequired", "Row {0}: Cost center name cannot be empty.", "第{0}行：成本中心名称不能为空", "第{0}行：成本中心名稱不能為空");
        AddValidationKey("validation.importRowCostCenterCodeExists", "Row {0}: Cost center code {1} already exists.", "第{0}行：成本中心编码 {1} 已存在", "第{0}行：成本中心編碼 {1} 已存在");
        AddValidationKey("validation.importRowRoleNameRequired", "Row {0}: Role name cannot be empty.", "第{0}行：角色名称不能为空", "第{0}行：角色名稱不能為空");
        AddValidationKey("validation.importRowRoleCodeRequired", "Row {0}: Role code cannot be empty.", "第{0}行：角色编码不能为空", "第{0}行：角色編碼不能為空");
        AddValidationKey("validation.importRowRoleNameCodeDuplicate", "Row {0}: Role name and code combination already exists.", "第{0}行：角色名称+角色编码组合已存在", "第{0}行：角色名稱+角色編碼組合已存在");
        AddValidationKey("validation.importRowTenantNameRequired", "Row {0}: Tenant name cannot be empty.", "第{0}行：租户名称不能为空", "第{0}行：租戶名稱不能為空");
        AddValidationKey("validation.importRowTenantCodeRequired", "Row {0}: Tenant code cannot be empty.", "第{0}行：租户编码不能为空", "第{0}行：租戶編碼不能為空");
        AddValidationKey("validation.importRowTenantConfigIdRequired", "Row {0}: Config ID cannot be empty.", "第{0}行：配置ID不能为空", "第{0}行：設定ID不能為空");
        AddValidationKey("validation.importRowTenantNameCodeDuplicate", "Row {0}: Tenant name and code combination already exists.", "第{0}行：租户名称+租户编码组合已存在", "第{0}行：租戶名稱+租戶編碼組合已存在");
        AddValidationKey("validation.importRowTranslationResourceKeyRequired", "Row {0}: Resource key cannot be empty.", "第{0}行：资源键不能为空", "第{0}行：資源鍵不能為空");
        AddValidationKey("validation.importRowTranslationCultureRequired", "Row {0}: Culture code cannot be empty.", "第{0}行：语言编码不能为空", "第{0}行：語言編碼不能為空");
        AddValidationKey("validation.importRowTranslationValueRequired", "Row {0}: Translation value cannot be empty.", "第{0}行：翻译值不能为空", "第{0}行：翻譯值不能為空");
        AddValidationKey("validation.importRowTranslationResourceTypeRequired", "Row {0}: Resource type cannot be empty.", "第{0}行：资源类型不能为空", "第{0}行：資源類型不能為空");
        AddValidationKey("validation.importRowTranslationResourceTypeInvalid", "Row {0}: Resource type must be Frontend or Backend.", "第{0}行：资源类型只能是 Frontend 或 Backend", "第{0}行：資源類型只能是 Frontend 或 Backend");
        AddValidationKey("validation.importRowTranslationLanguageNotFound", "Row {0}: Language {1} does not exist.", "第{0}行：语言 {1} 不存在", "第{0}行：語言 {1} 不存在");
        AddValidationKey("validation.importRowTranslationDuplicateComposite", "Row {0}: Language ID, culture, resource key and type combination already exists.", "第{0}行：语言ID+语言编码+资源键+资源类型组合已存在", "第{0}行：語言ID+語言編碼+資源鍵+資源類型組合已存在");
        AddValidationKey("validation.importRowDictDataTypeCodeRequired", "Row {0}: Dictionary type code cannot be empty.", "第{0}行：字典类型编码不能为空", "第{0}行：字典類型編碼不能為空");
        AddValidationKey("validation.importRowDictDataLabelRequired", "Row {0}: Dictionary label cannot be empty.", "第{0}行：字典标签不能为空", "第{0}行：字典標籤不能為空");
        AddValidationKey("validation.importRowDictDataValueRequired", "Row {0}: Dictionary value cannot be empty.", "第{0}行：字典值不能为空", "第{0}行：字典值不能為空");
        AddValidationKey("validation.importRowDictDataTypeNotFound", "Row {0}: Dictionary type {1} does not exist.", "第{0}行：字典类型 {1} 不存在", "第{0}行：字典類型 {1} 不存在");
        AddValidationKey("validation.importRowDictDataDuplicateComposite", "Row {0}: Dictionary type ID, type code and label combination already exists.", "第{0}行：字典类型ID+字典类型编码+字典标签组合已存在", "第{0}行：字典類型ID+字典類型編碼+字典標籤組合已存在");
        AddValidationKey("validation.importRowDictTypeCodeRequired", "Row {0}: Dictionary type code cannot be empty.", "第{0}行：字典类型编码不能为空", "第{0}行：字典類型編碼不能為空");
        AddValidationKey("validation.importRowDictTypeNameRequired", "Row {0}: Dictionary type name cannot be empty.", "第{0}行：字典类型名称不能为空", "第{0}行：字典類型名稱不能為空");
        AddValidationKey("validation.importRowDictTypeSqlCodeRequired", "Row {0}: When data source is SQL, dictionary type code cannot be empty.", "第{0}行：数据源为SQL查询时，字典类型编码不能为空", "第{0}行：資料來源為SQL查詢時，字典類型編碼不能為空");
        AddValidationKey("validation.importRowDictTypeSqlCodePrefixInvalid", "Row {0}: When data source is SQL, type code must start with 'sql_'. Current: {1}", "第{0}行：数据源为SQL查询时，字典类型编码必须以 'sql_' 开头，当前编码：{1}", "第{0}行：資料來源為SQL查詢時，字典類型編碼必須以 'sql_' 開頭，目前編碼：{1}");
        AddValidationKey("validation.importRowDictTypeSqlScriptRequired", "Row {0}: When data source is SQL, SQL script cannot be empty.", "第{0}行：数据源为SQL查询时，SQL脚本不能为空", "第{0}行：資料來源為SQL查詢時，SQL腳本不能為空");
        AddValidationKey("validation.importRowDictTypeSqlConfigIdRequired", "Row {0}: When data source is SQL, database config ID cannot be empty.", "第{0}行：数据源为SQL查询时，数据库配置ID不能为空", "第{0}行：資料來源為SQL查詢時，資料庫設定ID不能為空");
        AddValidationKey("validation.importRowDictTypeCodeNameDuplicate", "Row {0}: Dictionary type code and name combination already exists.", "第{0}行：字典类型编码+类型名称组合已存在", "第{0}行：字典類型編碼+類型名稱組合已存在");
        AddValidationKey("validation.importRowLeaveEmployeeIdRequired", "Row {0}: Employee ID cannot be empty.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowLeaveTypeRequired", "Row {0}: Leave type cannot be empty.", "第{0}行：请假类型不能为空", "第{0}行：請假類型不能為空");
        AddValidationKey("validation.importRowLeaveEndBeforeStart", "Row {0}: End date cannot be earlier than start date.", "第{0}行：结束日期不能早于开始日期", "第{0}行：結束日期不能早於開始日期");
        AddValidationKey("validation.importRowLeaveDuplicate", "Row {0}: A leave record already exists for this employee on {1} with type {2}.", "第{0}行：该员工在 {1} 的 {2} 请假记录已存在", "第{0}行：該員工在 {1} 的 {2} 請假記錄已存在");
        AddValidationKey("validation.importRowLeaveDuplicateInFile", "Row {0}: Duplicate leave in this file for employee on {1} with type {2}.", "第{0}行：该员工在 {1} 的 {2} 请假记录已存在（本文件重复）", "第{0}行：該員工在 {1} 的 {2} 請假記錄已存在（本檔案重複）");
        AddValidationKey("validation.importRowEmployeeGenderInvalid", "Row {0}: Gender must be 1 (male) or 2 (female) to generate employee code.", "第{0}行：性别须为男(1)或女(2)，以便生成员工编号", "第{0}行：性別須為男(1)或女(2)，以便產生員工編號");
        AddValidationKey("validation.importRowEmployeeCodeIdDuplicate", "Row {0}: Employee code and ID card combination already exists.", "第{0}行：员工编码+身份证号组合已存在", "第{0}行：員工編碼+身分證號組合已存在");
        AddValidationKey("validation.importRowEmployeeCodeIdDuplicateInFile", "Row {0}: Duplicate employee code and ID card in this file.", "第{0}行：员工编码+身份证号组合已存在（本文件重复）", "第{0}行：員工編碼+身分證號組合已存在（本檔案重複）");
        AddValidationKey("validation.importRowPurchaseOrderCodeRequired", "Row {0}: Order code cannot be empty.", "第{0}行：订单编码不能为空", "第{0}行：訂單編碼不能為空");
        AddValidationKey("validation.importRowPurchaseSupplierCodeRequired", "Row {0}: Supplier code cannot be empty.", "第{0}行：供应商编码不能为空", "第{0}行：供應商編碼不能為空");
        AddValidationKey("validation.importRowPurchaseSupplierNameRequired", "Row {0}: Supplier name cannot be empty.", "第{0}行：供应商名称不能为空", "第{0}行：供應商名稱不能為空");
        AddValidationKey("validation.importRowPurchaseOrderCodeExists", "Row {0}: Order code {1} already exists.", "第{0}行：订单编码 {1} 已存在", "第{0}行：訂單編碼 {1} 已存在");
        AddValidationKey("validation.importRowSettingKeyRequired", "Row {0}: Setting key cannot be empty.", "第{0}行：设置键不能为空", "第{0}行：設定鍵不能為空");
        AddValidationKey("validation.importRowSettingKeyExists", "Row {0}: Setting key {1} already exists.", "第{0}行：设置键 {1} 已存在", "第{0}行：設定鍵 {1} 已存在");
        AddValidationKey("validation.importRowSettingValueExists", "Row {0}: Setting value {1} already exists.", "第{0}行：设置值 {1} 已存在", "第{0}行：設定值 {1} 已存在");
        AddValidationKey("validation.importRowSettingNameExists", "Row {0}: Setting name {1} already exists.", "第{0}行：设置名称 {1} 已存在", "第{0}行：設定名稱 {1} 已存在");
        AddValidationKey("validation.importRowSettingGroupExists", "Row {0}: Setting group {1} already exists.", "第{0}行：设置分组 {1} 已存在", "第{0}行：設定分組 {1} 已存在");
        AddValidationKey("validation.importRowPurchasePriceSupplierCodeRequired", "Row {0}: Supplier code cannot be empty.", "第{0}行：供应商编码不能为空", "第{0}行：供應商編碼不能為空");
        AddValidationKey("validation.importRowHolidayRegionRequired", "Row {0}: Region cannot be empty.", "第{0}行：地区不能为空", "第{0}行：地區不能為空");
        AddValidationKey("validation.importRowHolidayNameRequired", "Row {0}: Holiday name cannot be empty.", "第{0}行：假日名称不能为空", "第{0}行：假日名稱不能為空");
        AddValidationKey("validation.importRowHolidayDuplicateComposite", "Row {0}: Region, name, type and start date combination already exists.", "第{0}行：地区+假日名称+假日类型+假日开始日期组合已存在", "第{0}行：地區+假日名稱+假日類型+假日開始日期組合已存在");
        AddValidationKey("validation.importRowLanguageNameRequired", "Row {0}: Language name cannot be empty.", "第{0}行：语言名称不能为空", "第{0}行：語言名稱不能為空");
        AddValidationKey("validation.importRowLanguageCodeRequired", "Row {0}: Language code cannot be empty.", "第{0}行：语言编码不能为空", "第{0}行：語言編碼不能為空");
        AddValidationKey("validation.importRowLanguageNativeNameRequired", "Row {0}: Native name cannot be empty.", "第{0}行：本地化名称不能为空", "第{0}行：本地化名稱不能為空");
        AddValidationKey("validation.importRowLanguageNameCodeDuplicate", "Row {0}: Language name and code combination already exists.", "第{0}行：语言名称+语言编码组合已存在", "第{0}行：語言名稱+語言編碼組合已存在");
        AddValidationKey("validation.importRowMenuNameRequired", "Row {0}: Menu name cannot be empty.", "第{0}行：菜单名称不能为空", "第{0}行：選單名稱不能為空");
        AddValidationKey("validation.importRowMenuCodeRequired", "Row {0}: Menu code cannot be empty.", "第{0}行：菜单编码不能为空", "第{0}行：選單編碼不能為空");
        AddValidationKey("validation.importRowMenuDuplicateComposite", "Row {0}: Menu name, code and type combination already exists.", "第{0}行：菜单名称+菜单编码+菜单类型组合已存在", "第{0}行：選單名稱+選單編碼+選單類型組合已存在");
        AddValidationKey("validation.importRowPostNameRequired", "Row {0}: Post name cannot be empty.", "第{0}行：岗位名称不能为空", "第{0}行：崗位名稱不能為空");
        AddValidationKey("validation.importRowPostCodeRequired", "Row {0}: Post code cannot be empty.", "第{0}行：岗位编码不能为空", "第{0}行：崗位編碼不能為空");
        AddValidationKey("validation.importRowPostDuplicateComposite", "Row {0}: Post name, code, category and level combination already exists.", "第{0}行：岗位名称+岗位编码+岗位类别+岗位级别组合已存在", "第{0}行：崗位名稱+崗位編碼+崗位類別+崗位級別組合已存在");
        AddValidationKey("validation.importRowDeptNameRequired", "Row {0}: Department name cannot be empty.", "第{0}行：部门名称不能为空", "第{0}行：部門名稱不能為空");
        AddValidationKey("validation.importRowDeptCodeRequired", "Row {0}: Department code cannot be empty.", "第{0}行：部门编码不能为空", "第{0}行：部門編碼不能為空");
        AddValidationKey("validation.importRowDeptDuplicateComposite", "Row {0}: Department name, code and type combination already exists.", "第{0}行：部门名称+部门编码+部门类型组合已存在", "第{0}行：部門名稱+部門編碼+部門類型組合已存在");
        AddValidationKey("validation.importRowDeptHeadIdRequired", "Row {0}: Head employee ID is required.", "第{0}行：部门负责人员工ID不能为空", "第{0}行：部門負責人員工ID不能為空");
        AddValidationKey("validation.importRowDeptHeadEmployeeInvalid", "Row {0}: Head employee ID is invalid or employee not found.", "第{0}行：部门负责人员工ID无效或员工不存在", "第{0}行：部門負責人員工ID無效或員工不存在");
        AddValidationKey("validation.importRowDeptCostCenterInvalid", "Row {0}: Cost center code is invalid or not found.", "第{0}行：成本中心编码无效或不存在", "第{0}行：成本中心編碼無效或不存在");
        AddValidationKey("validation.deptCostCenterNotFound", "Cost center does not exist or has been deleted.", "成本中心不存在或已删除", "成本中心不存在或已刪除");
        AddValidationKey("validation.deptDelegateEmployeesInvalid", "One or more delegate employees do not exist.", "部分代理人员工不存在", "部分代理人人員工不存在");
        AddValidationKey("validation.importRowUserNameRequired", "Row {0}: User name cannot be empty.", "第{0}行：用户名不能为空", "第{0}行：使用者名稱不能為空");
        AddValidationKey("validation.importRowUserDuplicateComposite", "Row {0}: User name, type, email and phone combination already exists.", "第{0}行：用户名+用户类型+邮箱+手机组合已存在", "第{0}行：使用者名稱+使用者類型+信箱+手機組合已存在");
        AddValidationKey("validation.importRowUserDuplicateCompositeInFile", "Row {0}: Duplicate user combination in this file.", "第{0}行：用户名+用户类型+邮箱+手机组合已存在（本文件重复）", "第{0}行：使用者名稱+使用者類型+信箱+手機組合已存在（本檔案重複）");
        AddValidationKey("validation.importRowEmployeeAttachmentEmployeeIdRequired", "Row {0}: Employee ID cannot be empty.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowEmployeeAttachmentDuplicateComposite", "Row {0}: Employee ID, file code and file name combination already exists.", "第{0}行：员工ID+文件编码+文件名称组合已存在", "第{0}行：員工ID+檔案編碼+檔案名稱組合已存在");
        AddValidationKey("validation.importRowEmployeeCareerEmployeeIdRequired", "Row {0}: Employee ID cannot be empty.", "第{0}行：员工ID不能为空", "第{0}行：員工ID不能為空");
        AddValidationKey("validation.importRowEmployeeCareerDeptIdRequired", "Row {0}: Department ID cannot be empty.", "第{0}行：部门ID不能为空", "第{0}行：部門ID不能為空");
        AddValidationKey("validation.importPlantUniqueCombinationExists", "Plant unique field combination already exists.", "工厂表唯一字段组合已存在", "工廠主檔唯一欄位組合已存在");

        // 工作流（TaktFlow*Service）
        AddValidationKey("validation.flowSchemeNotFound", "Flow scheme does not exist.", "流程方案不存在", "流程方案不存在");
        AddValidationKey("validation.flowProcessContentRootMustBeJsonObject", "ProcessContent root must be a JSON object.", "流程内容 ProcessContent 根须为 JSON 对象", "流程內容 ProcessContent 根須為 JSON 物件");
        AddValidationKey("validation.flowProcessContentMustContainNodesArray", "ProcessContent must contain a \"nodes\" array.", "流程内容须包含 nodes 数组", "流程內容須包含 nodes 陣列");
        AddValidationKey("validation.flowProcessContentMustContainEdgesArray", "ProcessContent must contain an \"edges\" array.", "流程内容须包含 edges 数组", "流程內容須包含 edges 陣列");
        AddValidationKey("validation.flowTreeMustBeJsonObject", "flowTree must be a JSON object.", "flowTree 须为 JSON 对象", "flowTree 須為 JSON 物件");
        AddValidationKey("validation.flowTreeRootNodeTypeMustBeStarter", "flowTree root nodeType must be 1 (starter). Please save again in the flow designer.", "flowTree 根节点 nodeType 须为 1（发起人），请在流程设计器中重新保存", "flowTree 根節點 nodeType 須為 1（發起人），請在流程設計器中重新儲存");
        AddValidationKey("validation.flowProcessContentInvalidJson", "ProcessContent is not valid JSON and cannot be saved: {0}", "流程内容不是合法 JSON，无法保存：{0}", "流程內容不是合法 JSON，無法儲存：{0}");
        AddValidationKey("validation.flowFormCodeAlreadyExists", "Form code {0} already exists.", "表单编码 {0} 已存在", "表單編碼 {0} 已存在");
        AddValidationKey("validation.flowFormCodeUsedByOther", "Form code {0} is already used by another form.", "表单编码 {0} 已被其他表单使用", "表單編碼 {0} 已被其他表單使用");
        AddValidationKey("validation.flowFormNotFound", "Flow form does not exist.", "流程表单不存在", "流程表單不存在");
        AddValidationKey("validation.flowInstanceNotFound", "Flow instance does not exist.", "流程实例不存在", "流程實例不存在");
        AddValidationKey("validation.flowPublishedSchemeNotFoundByProcessKey", "No published flow scheme found for process key: {0}", "未找到已发布的流程方案：{0}", "未找到已發佈的流程方案：{0}");
        AddValidationKey("validation.flowSchemeDefinitionNotFound", "Flow scheme definition does not exist.", "流程定义不存在", "流程定義不存在");
        AddValidationKey("validation.flowSchemeContentEmpty", "Flow scheme definition content is empty.", "流程定义内容为空", "流程定義內容為空");
        AddValidationKey("validation.flowSchemeMissingStartNode", "Flow scheme definition is missing the start node.", "流程定义缺少开始节点", "流程定義缺少開始節點");
        AddValidationKey("validation.flowOnlyRunningOrDraftUpdatable", "Only running or draft instances can be updated.", "仅运行中或草稿状态的流程可更新", "僅執行中或草稿狀態的流程可更新");
        AddValidationKey("validation.flowOnlyStarterCanUpdate", "Only the starter can update the instance.", "仅发起人可更新流程", "僅發起人可更新流程");
        AddValidationKey("validation.flowOnlyRunningCanUndoApproval", "Only running instances can undo approval.", "仅运行中的流程可撤销审批", "僅執行中的流程可撤銷審批");
        AddValidationKey("validation.flowOnlyPreviousApproverCanUndo", "Only the previous approver can undo.", "仅上一审批人可撤销审批", "僅上一審批人可撤銷審批");
        AddValidationKey("validation.flowNoTransitionHistoryCannotUndo", "No transition history; cannot undo.", "无流转记录，无法撤销", "無流轉記錄，無法撤銷");
        AddValidationKey("validation.flowCannotDetermineReturnNode", "Cannot determine the return node.", "无法确定退回节点", "無法確定退回節點");
        AddValidationKey("validation.flowReturnNodeNotFound", "Return node does not exist.", "退回节点不存在", "退回節點不存在");
        AddValidationKey("validation.flowOnlyStarterCanDeleteInstance", "Only the starter can delete the instance. Instance {0} was not started by you.", "仅发起人可删除实例，实例 {0} 非您发起", "僅發起人可刪除實例，實例 {0} 非您發起");
        AddValidationKey("validation.flowOnlyDraftCanStartFromDraft", "Only draft instances can be started from draft.", "仅草稿状态的实例可从草稿启动", "僅草稿狀態的實例可從草稿啟動");
        AddValidationKey("validation.flowOnlyStarterCanStartFromDraft", "Only the starter can start from draft.", "仅发起人可从草稿启动", "僅發起人可從草稿啟動");
        AddValidationKey("validation.flowEndedOrWithdrawn", "The flow has ended or been withdrawn.", "流程已结束或已撤回", "流程已結束或已撤回");
        AddValidationKey("validation.flowNoPermissionForTask", "You are not allowed to handle this task.", "无权处理该待办", "無權處理該待辦");
        AddValidationKey("validation.flowCurrentNodeInvalid", "Current node is invalid.", "当前节点无效", "目前節點無效");
        AddValidationKey("validation.flowOnlyStarterCanWithdraw", "Only the starter can withdraw.", "仅发起人可撤回", "僅發起人可撤回");
        AddValidationKey("validation.flowEndedCannotWithdraw", "The flow has ended; cannot withdraw.", "流程已结束，无法撤回", "流程已結束，無法撤回");
        AddValidationKey("validation.flowOnlyRunningCanSuspend", "Only running instances can be suspended.", "仅运行中的流程可挂起", "僅執行中的流程可掛起");
        AddValidationKey("validation.flowAlreadySuspended", "The flow is already suspended.", "流程已挂起", "流程已掛起");
        AddValidationKey("validation.flowOnlySuspendedCanResume", "Only suspended instances can be resumed.", "仅已挂起的流程可恢复", "僅已掛起的流程可恢復");
        AddValidationKey("validation.flowRunningOrSuspendedCanTerminate", "Only running or suspended instances can be terminated.", "仅运行中或已挂起的流程可终止", "僅執行中或已掛起的流程可終止");
        AddValidationKey("validation.flowOnlyRunningCanReassign", "Only running instances can be reassigned.", "仅运行中的流程可转办", "僅執行中的流程可轉辦");
        AddValidationKey("validation.flowNoPermissionToReassign", "You are not allowed to reassign this task.", "无权转办该待办", "無權轉辦該待辦");
        AddValidationKey("validation.flowCountersignApproverRequired", "Specify at least one countersign approver.", "请指定至少一名加签审批人", "請指定至少一名加簽審批人");
        AddValidationKey("validation.flowOnlyRunningCanAddSign", "Only running instances can add approvers.", "仅运行中的流程可加签", "僅執行中的流程可加簽");
        AddValidationKey("validation.flowNoPermissionToAddSign", "You are not allowed to add approvers.", "无权加签", "無權加簽");
        AddValidationKey("validation.flowOnlyRunningCanRemoveSign", "Only running instances can remove approvers.", "仅运行中的流程可减签", "僅執行中的流程可減簽");
        AddValidationKey("validation.flowNoPermissionToRemoveSign", "You are not allowed to remove approvers.", "无权减签", "無權減簽");
        AddValidationKey("validation.flowAddApproverNotFound", "Add-approver record does not exist.", "加签记录不存在", "加簽記錄不存在");
        AddValidationKey("validation.flowCountersignRecordWrongInstance", "This add-approver record does not belong to the current instance.", "该加签记录不属于当前流程实例", "該加簽記錄不屬於目前流程實例");
        AddValidationKey("validation.flowProcessedCountersignCannotRemove", "Processed add-approver records cannot be removed.", "已处理的加签不可移除", "已處理的加簽不可移除");
    }
}
