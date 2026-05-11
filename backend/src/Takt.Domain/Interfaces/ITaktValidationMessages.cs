// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Interfaces
// 文件名称：ITaktValidationMessages.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：验证消息接口（定义验证消息格式化契约）
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Domain.Interfaces;

/// <summary>
/// 验证消息接口（定义验证消息格式化契约）
/// </summary>
public interface ITaktValidationMessages
{
    /// <summary>模板 <c>validation.required</c>，占位符 <c>{0}</c> 为字段名。</summary>
    string Required(string entityKey);

    /// <summary>模板 <c>validation.lengthBetween</c>，<c>{0}</c> 字段名，<c>{1}</c><c>{2}</c> 最小/最大长度。</summary>
    string LengthBetween(string entityKey, int min, int max);

    /// <summary>模板 <c>validation.lengthMin</c>，<c>{0}</c> 字段名，<c>{1}</c> 最小长度。</summary>
    string LengthMin(string entityKey, int min);

    /// <summary>模板 <c>validation.lengthMax</c>，<c>{0}</c> 字段名，<c>{1}</c> 最大长度。</summary>
    string LengthMax(string entityKey, int max);

    /// <summary>模板 <c>validation.formatInvalid</c>，<c>{0}</c> 为字段名。</summary>
    string FormatInvalid(string entityKey);

    /// <summary>模板 <c>validation.idCardInvalid</c>，<c>{0}</c> 为证件字段名（含校验失败语义）。</summary>
    string IdCardInvalid(string entityKey);

    /// <summary>模板 <c>validation.patternUsername</c>，含用户名规则说明。</summary>
    string PatternUserName(string entityKey);

    /// <summary>模板 <c>validation.patternPasswordStrong</c>，含强密码规则说明。</summary>
    string PatternPasswordStrong(string entityKey);

    /// <summary>模板 <c>validation.notEqualFields</c>，<c>{0}</c><c>{1}</c> 为两个字段名（均来自 entity 键）。</summary>
    string NotEqualFields(string entityKeyA, string entityKeyB);

    /// <summary>模板 <c>validation.endBeforeStart</c>：<c>{0}</c> 为结束时间字段名，<c>{1}</c> 为开始时间字段名（结束不得早于开始）。</summary>
    string EndBeforeStart(string endEntityKey, string startEntityKey);

    /// <summary>
    /// 模板键为 <c>validation.pattern*</c>（与 <c>TaktRegexHelper</c> 中 Regex 字段对应），占位符 <c>{0}</c> 为字段名（entity 键解析）。
    /// </summary>
    string Pattern(string validationTemplateKey, string entityKey);
}
