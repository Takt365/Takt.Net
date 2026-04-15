// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Validation
// 文件名称：TaktValidationMessages.cs
// 功能描述：FluentValidation 提示：validation.* 与 entity.* 均按 Frontend 从库读取后 string.Format 拼接（与 TaktValidationSeedData 一致）。无翻译或 localizer 为 null 时不兜底，直接返回资源键。
// ========================================

using Takt.Domain.Interfaces;

namespace Takt.WebApi.Validation;

/// <summary>
/// 验证文案组合：字段名与模板均为 <c>Frontend</c>（<c>GetString(entityKey, Frontend)</c> + <c>GetString(validation.*, Frontend)</c>）。
/// 无翻译或 <c>localizer</c> 为 null 时返回对应资源键，不使用第二套中文或其它文案兜底。
/// </summary>
public static class TaktValidationMessages
{
    private const string Frontend = "Frontend";

    private static string EntityLabel(ITaktLocalizer? localizer, string entityKey)
    {
        if (localizer == null) return entityKey;
        var v = localizer.GetString(entityKey, Frontend);
        return string.IsNullOrWhiteSpace(v) || v == entityKey ? entityKey : v;
    }

    private static string FormatTemplate(ITaktLocalizer? localizer, string templateKey, params object[] args)
    {
        if (localizer == null)
            return templateKey;

        var t = localizer.GetString(templateKey, Frontend);
        if (string.IsNullOrWhiteSpace(t) || t == templateKey)
            return templateKey;

        try
        {
            return args.Length == 0 ? t : string.Format(t, args);
        }
        catch
        {
            return templateKey;
        }
    }

    /// <summary>模板 <c>validation.required</c>，占位符 <c>{0}</c> 为字段名。</summary>
    public static string Required(ITaktLocalizer? localizer, string entityKey) =>
        FormatTemplate(localizer, "validation.required", EntityLabel(localizer, entityKey));

    /// <summary>模板 <c>validation.lengthBetween</c>，<c>{0}</c> 字段名，<c>{1}</c><c>{2}</c> 最小/最大长度。</summary>
    public static string LengthBetween(ITaktLocalizer? localizer, string entityKey, int min, int max) =>
        FormatTemplate(localizer, "validation.lengthBetween", EntityLabel(localizer, entityKey), min, max);

    /// <summary>模板 <c>validation.lengthMin</c>，<c>{0}</c> 字段名，<c>{1}</c> 最小长度。</summary>
    public static string LengthMin(ITaktLocalizer? localizer, string entityKey, int min) =>
        FormatTemplate(localizer, "validation.lengthMin", EntityLabel(localizer, entityKey), min);

    /// <summary>模板 <c>validation.lengthMax</c>，<c>{0}</c> 字段名，<c>{1}</c> 最大长度。</summary>
    public static string LengthMax(ITaktLocalizer? localizer, string entityKey, int max) =>
        FormatTemplate(localizer, "validation.lengthMax", EntityLabel(localizer, entityKey), max);

    /// <summary>模板 <c>validation.formatInvalid</c>，<c>{0}</c> 为字段名。</summary>
    public static string FormatInvalid(ITaktLocalizer? localizer, string entityKey) =>
        FormatTemplate(localizer, "validation.formatInvalid", EntityLabel(localizer, entityKey));

    /// <summary>模板 <c>validation.idCardInvalid</c>，<c>{0}</c> 为证件字段名（含校验失败语义）。</summary>
    public static string IdCardInvalid(ITaktLocalizer? localizer, string entityKey) =>
        FormatTemplate(localizer, "validation.idCardInvalid", EntityLabel(localizer, entityKey));

    /// <summary>模板 <c>validation.patternUsername</c>，含用户名规则说明。</summary>
    public static string PatternUserName(ITaktLocalizer? localizer, string entityKey) =>
        FormatTemplate(localizer, "validation.patternUsername", EntityLabel(localizer, entityKey));

    /// <summary>模板 <c>validation.patternPasswordStrong</c>，含强密码规则说明。</summary>
    public static string PatternPasswordStrong(ITaktLocalizer? localizer, string entityKey) =>
        FormatTemplate(localizer, "validation.patternPasswordStrong", EntityLabel(localizer, entityKey));

    /// <summary>模板 <c>validation.notEqualFields</c>，<c>{0}</c><c>{1}</c> 为两个字段名（均来自 entity 键）。</summary>
    public static string NotEqualFields(ITaktLocalizer? localizer, string entityKeyA, string entityKeyB) =>
        FormatTemplate(localizer, "validation.notEqualFields",
            EntityLabel(localizer, entityKeyA), EntityLabel(localizer, entityKeyB));

    /// <summary>模板 <c>validation.endBeforeStart</c>：<c>{0}</c> 为结束时间字段名，<c>{1}</c> 为开始时间字段名（结束不得早于开始）。</summary>
    public static string EndBeforeStart(ITaktLocalizer? localizer, string endEntityKey, string startEntityKey) =>
        FormatTemplate(localizer, "validation.endBeforeStart",
            EntityLabel(localizer, endEntityKey), EntityLabel(localizer, startEntityKey));

    /// <summary>
    /// 模板键为 <c>validation.pattern*</c>（与 <c>TaktRegexHelper</c> 中 Regex 字段对应），占位符 <c>{0}</c> 为字段名（entity 键解析）。
    /// </summary>
    public static string Pattern(ITaktLocalizer? localizer, string validationTemplateKey, string entityKey) =>
        FormatTemplate(localizer, validationTemplateKey, EntityLabel(localizer, entityKey));
}
