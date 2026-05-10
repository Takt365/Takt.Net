// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedI18nData
// 文件名称：TaktValidationsSeedData.cs
// 功能描述：FluentValidation验证文案种子 - 基于TaktValidationMessages.cs实际使用的方法生成。
//           包含：required、lengthBetween/Min/Max、formatInvalid、idCardInvalid、patternUsername/PasswordStrong、notEqualFields、endBeforeStart
//           以及TaktRegexHelper对应的pattern*验证模板（精简通用版）。
//           所有翻译ResourceType=Frontend，供前端展示与后端ITaktLocalizer共用。
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
/// FluentValidation验证文案种子 - 基于TaktValidationMessages.cs实际使用的方法
/// 与generate_validators.cjs脚本生成的验证器完全对齐
/// </summary>
public class TaktValidationSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在语言种子之后）
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
                l.LanguageStatus == 1 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetTranslationTemplates();

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
                        SortOrder = translation.SortOrder,
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
                        existing.SortOrder != translation.SortOrder)
                    {
                        existing.LanguageId = languageId;
                        existing.TranslationValue = translation.TranslationValue;
                        existing.ResourceType = translation.ResourceType;
                        existing.ResourceGroup = translation.ResourceGroup;
                        existing.SortOrder = translation.SortOrder;
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
    /// 获取所有验证模板翻译 - 与TaktValidationMessages.cs和generate_validators.cjs完全对齐
    /// </summary>
    private static List<TaktTranslation> GetTranslationTemplates() => new()
    {
        // ==================== 一、基础验证（TaktValidationMessages.cs实际使用） ====================
        
        // 1.1 validation.required - 必填验证
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.required", TranslationValue = "{0} is required.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 1 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.required", TranslationValue = "{0}不能为空", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 1 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.required", TranslationValue = "{0}不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 1 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.required", TranslationValue = "{0}不能為空", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 1 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.required", TranslationValue = "{0}は必須です。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 1 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.required", TranslationValue = "{0}은(는) 필수 항목입니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 1 },

        // 1.2 validation.lengthBetween - 长度范围验证
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.lengthBetween", TranslationValue = "{0} must be between {1} and {2} characters.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}长度必须在{1}到{2}个字符之间", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}長度必須在{1}到{2}個字元之間", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}長度必須在{1}到{2}個字元之間", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}は{1}〜{2}文字で入力してください。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.lengthBetween", TranslationValue = "{0}은(는) {1}~{2}자여야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },

        // 1.3 validation.lengthMin - 最小长度验证
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.lengthMin", TranslationValue = "{0} must be at least {1} characters.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.lengthMin", TranslationValue = "{0}至少需要{1}个字符", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.lengthMin", TranslationValue = "{0}至少需要{1}個字元", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.lengthMin", TranslationValue = "{0}至少需要{1}個字元", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.lengthMin", TranslationValue = "{0}は{1}文字以上で入力してください。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.lengthMin", TranslationValue = "{0}은(는) 최소 {1}자 이상이어야 합니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },

        // 1.4 validation.lengthMax - 最大长度验证
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.lengthMax", TranslationValue = "{0} cannot exceed {1} characters.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.lengthMax", TranslationValue = "{0}不能超过{1}个字符", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.lengthMax", TranslationValue = "{0}不能超過{1}個字元", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.lengthMax", TranslationValue = "{0}不能超過{1}個字元", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.lengthMax", TranslationValue = "{0}は{1}文字以内で入力してください。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.lengthMax", TranslationValue = "{0}은(는) {1}자를 초과할 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 2 },

        // 1.5 validation.formatInvalid - 格式无效（通用，用于枚举值验证等）
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.formatInvalid", TranslationValue = "{0} is invalid.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}格式不正确", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}格式不正確", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}格式不正確", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}が無効です。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.formatInvalid", TranslationValue = "{0}이(가) 올바르지 않습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },

        // 1.6 validation.idCardInvalid - 身份证无效
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0} is not a valid ID card number.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}不是有效的身份证号", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}不是有效的身分證號", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}不是有效的身分證號", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}は有効な身分証番号ではありません。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.idCardInvalid", TranslationValue = "{0}은(는) 유효한 신분증 번호가 아닙니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 3 },

        // 1.7 validation.patternUsername - 用户名格式
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternUsername", TranslationValue = "{0} is invalid (must start with a lowercase letter; only lowercase letters and digits; 4–20 characters).", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternUsername", TranslationValue = "{0}格式不正确（小写字母开头，仅允许小写字母和数字，4-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternUsername", TranslationValue = "{0}格式不正確（小寫字母開頭，僅允許小寫字母與數字，4-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.patternUsername", TranslationValue = "{0}格式不正確（小寫字母開頭，僅允許小寫字母與數字，4-20位）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternUsername", TranslationValue = "{0}が無効です（先頭は小文字、小文字と数字のみ、4～20文字）。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternUsername", TranslationValue = "{0}이(가) 올바르지 않습니다(소문자로 시작, 소문자·숫자만, 4~20자).", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },

        // 1.8 validation.patternPasswordStrong - 强密码格式
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0} is invalid (8–20 characters; must include uppercase, lowercase, digit and special character).", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}格式不正确（8-20位，必须包含大小写字母、数字和特殊字符）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}格式不正確（8-20位，必須包含大小寫字母、數字與特殊字元）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}格式不正確（8-20位，必須包含大小寫字母、數字與特殊字元）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}が無効です（8～20文字、大文字・小文字・数字・記号を含む必要があります）。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPasswordStrong", TranslationValue = "{0}이(가) 올바르지 않습니다(8~20자, 대·소문자·숫자·특수문자 포함).", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 4 },

        // 1.9 validation.notEqualFields - 两字段不相等（如确认密码≠密码）
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.notEqualFields", TranslationValue = "{0} and {1} must not be equal.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}和{1}不能相同", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}和{1}不能相同", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}和{1}不能相同", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}と{1}は同じ値にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.notEqualFields", TranslationValue = "{0}과(와) {1}은(는) 같을 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },

        // 1.10 validation.endBeforeStart - 结束时间不早于开始时间
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0} cannot be earlier than {1}.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}不能早于{1}", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}不能早於{1}", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}不能早於{1}", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}は{1}より前の日時にできません。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.endBeforeStart", TranslationValue = "{0}은(는) {1}보다 이전일 수 없습니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 5 },

        // ==================== 二、TaktRegexHelper对应的pattern*验证（generate_validators.cjs自动识别） ====================
        
        // 2.1 联系方式类（通用，不区分地区）
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternEmail", TranslationValue = "{0} is not a valid email address.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternEmail", TranslationValue = "{0}不是有效的邮箱地址", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternEmail", TranslationValue = "{0}不是有效的郵箱地址", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.patternEmail", TranslationValue = "{0}不是有效的郵箱地址", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternEmail", TranslationValue = "{0}は有効なメールアドレスではありません。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternEmail", TranslationValue = "{0}은(는) 유효한 이메일 주소가 아닙니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },

        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternPhone", TranslationValue = "{0} is not a valid mobile phone number.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternPhone", TranslationValue = "{0}不是有效的手机号", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternPhone", TranslationValue = "{0}不是有效的手機號", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.patternPhone", TranslationValue = "{0}不是有效的手機號", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternPhone", TranslationValue = "{0}は有効な携帯電話番号ではありません。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternPhone", TranslationValue = "{0}은(는) 유효한 휴대폰 번호가 아닙니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 10 },

        // 2.2 其他常用格式（generate_validators.cjs可能识别的）
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.patternUrl", TranslationValue = "{0} is not a valid URL.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 11 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.patternUrl", TranslationValue = "{0}不是有效的URL地址", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 11 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.patternUrl", TranslationValue = "{0}不是有效的URL位址", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 11 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.patternUrl", TranslationValue = "{0}不是有效的URL位址", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 11 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.patternUrl", TranslationValue = "{0}は有効なURLではありません。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 11 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.patternUrl", TranslationValue = "{0}은(는) 유효한 URL이 아닙니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 11 },

        // ==================== 三、Excel导入验证（项目实际使用） ====================
        
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.importFileRequired", TranslationValue = "Please upload a file to import.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.importFileRequired", TranslationValue = "请上传要导入的文件", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.importFileRequired", TranslationValue = "請上傳要導入的文件", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.importFileRequired", TranslationValue = "請上傳要導入的文件", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.importFileRequired", TranslationValue = "インポートするファイルをアップロードしてください。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.importFileRequired", TranslationValue = "가져올 파일을 업로드하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },

        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.excelFileOnly", TranslationValue = "Only Excel files (.xlsx or .xls) are supported.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.excelFileOnly", TranslationValue = "仅支持Excel文件（.xlsx或.xls）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.excelFileOnly", TranslationValue = "僅支援Excel文件（.xlsx或.xls）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.excelFileOnly", TranslationValue = "僅支援Excel檔案（.xlsx或.xls）", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.excelFileOnly", TranslationValue = "Excelファイル（.xlsxまたは.xls）のみサポートされています。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.excelFileOnly", TranslationValue = "Excel 파일(.xlsx 또는 .xls)만 지원됩니다.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },

        new TaktTranslation { CultureCode = "en-US", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "Import failed: total rows is {0}, exceeding the limit {1}. Please split the file.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "导入失败：总记录数{0}超过上限{1}，请拆分文件", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "導入失敗：總記錄數{0}超過上限{1}，請拆分文件", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "導入失敗：總記錄數{0}超過上限{1}，請拆分文件", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "インポート失敗：総件数{0}が上限{1}を超えています。ファイルを分割してください。", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "validation.importRowLimitExceeded", TranslationValue = "가져오기 실패: 총 {0}행이 제한 {1}행을 초과했습니다. 파일을 분할하세요.", ResourceType = "Frontend", ResourceGroup = "Validation", SortOrder = 20 },

        // ==================== 四、模板文件 ====================
        
        new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.template.name", TranslationValue = "Template", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 100 },
        new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.template.name", TranslationValue = "模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 100 },
        new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.template.name", TranslationValue = "模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 100 },
        new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.template.name", TranslationValue = "模板", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 100 },
        new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.template.name", TranslationValue = "テンプレート", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 100 },
        new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.template.name", TranslationValue = "템플릿", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 100 }
    };
}
