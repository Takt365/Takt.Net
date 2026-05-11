// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Validation
// 文件名称：TaktValidationMessages.cs
// 创建时间：2026-04-17
// 创建人：Takt365(Cursor AI)
// 功能描述：FluentValidation 提示文案组装工具，validation.* 与 entity.* 均按 Frontend 从库读取并 string.Format 拼接；无翻译或 localizer 为 null 时返回资源键。
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Interfaces;

namespace Takt.Infrastructure.Validation;

/// <summary>
/// 验证文案组合：字段名与模板均为 <c>Frontend</c>（<c>GetString(entityKey, Frontend)</c> + <c>GetString(validation.*, Frontend)</c>）。
/// 无翻译或 <c>localizer</c> 为 null 时返回对应资源键，不使用第二套中文或其它文案兜底。
/// </summary>
public class TaktValidationMessages : ITaktValidationMessages
{
    private readonly ITaktLocalizer _localizer;
    private const string Frontend = "Frontend";

    public TaktValidationMessages(ITaktLocalizer localizer)
    {
        _localizer = localizer;
    }

    private string EntityLabel(string entityKey)
    {
        if (_localizer == null) return entityKey;
        var v = _localizer.GetString(entityKey, Frontend);
        return string.IsNullOrWhiteSpace(v) || v == entityKey ? entityKey : v;
    }

    private string FormatTemplate(string templateKey, params object[] args)
    {
        if (_localizer == null)
            return templateKey;

        var t = _localizer.GetString(templateKey, Frontend);
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
    public string Required(string entityKey) =>
        FormatTemplate("validation.required", EntityLabel(entityKey));

    /// <summary>模板 <c>validation.lengthBetween</c>，<c>{0}</c> 字段名，<c>{1}</c><c>{2}</c> 最小/最大长度。</summary>
    public string LengthBetween(string entityKey, int min, int max) =>
        FormatTemplate("validation.lengthBetween", EntityLabel(entityKey), min, max);

    /// <summary>模板 <c>validation.lengthMin</c>，<c>{0}</c> 字段名，<c>{1}</c> 最小长度。</summary>
    public string LengthMin(string entityKey, int min) =>
        FormatTemplate("validation.lengthMin", EntityLabel(entityKey), min);

    /// <summary>模板 <c>validation.lengthMax</c>，<c>{0}</c> 字段名，<c>{1}</c> 最大长度。</summary>
    public string LengthMax(string entityKey, int max) =>
        FormatTemplate("validation.lengthMax", EntityLabel(entityKey), max);

    /// <summary>模板 <c>validation.formatInvalid</c>，<c>{0}</c> 为字段名。</summary>
    public string FormatInvalid(string entityKey) =>
        FormatTemplate("validation.formatInvalid", EntityLabel(entityKey));

    /// <summary>模板 <c>validation.idCardInvalid</c>，<c>{0}</c> 为证件字段名（含校验失败语义）。</summary>
    public string IdCardInvalid(string entityKey) =>
        FormatTemplate("validation.idCardInvalid", EntityLabel(entityKey));

    /// <summary>模板 <c>validation.patternUsername</c>，含用户名规则说明。</summary>
    public string PatternUserName(string entityKey) =>
        FormatTemplate("validation.patternUsername", EntityLabel(entityKey));

    /// <summary>模板 <c>validation.patternPasswordStrong</c>，含强密码规则说明。</summary>
    public string PatternPasswordStrong(string entityKey) =>
        FormatTemplate("validation.patternPasswordStrong", EntityLabel(entityKey));

    /// <summary>模板 <c>validation.notEqualFields</c>，<c>{0}</c><c>{1}</c> 为两个字段名（均来自 entity 键）。</summary>
    public string NotEqualFields(string entityKeyA, string entityKeyB) =>
        FormatTemplate("validation.notEqualFields",
            EntityLabel(entityKeyA), EntityLabel(entityKeyB));

    /// <summary>模板 <c>validation.endBeforeStart</c>：<c>{0}</c> 为结束时间字段名，<c>{1}</c> 为开始时间字段名（结束不得早于开始）。</summary>
    public string EndBeforeStart(string endEntityKey, string startEntityKey) =>
        FormatTemplate("validation.endBeforeStart",
            EntityLabel(endEntityKey), EntityLabel(startEntityKey));

    /// <summary>
    /// 模板键为 <c>validation.pattern*</c>（与 <c>TaktRegexHelper</c> 中 Regex 字段对应），占位符 <c>{0}</c> 为字段名（entity 键解析）。
    /// </summary>
    public string Pattern(string validationTemplateKey, string entityKey) =>
        FormatTemplate(validationTemplateKey, EntityLabel(entityKey));
}
