// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Employee DTO 验证器（根据实体 TaktEmployee 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// Employee创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployee"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeCreateDtoValidator : AbstractValidator<TaktEmployeeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EmployeeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employee.employeecode"))
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employee.employeecode", 50));

        RuleFor(x => x.RealName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employee.realname"))
            .Must(TaktRegexHelper.IsValidFullName).WithMessage(_validationMessages.FormatInvalid("entity.employee.realname"));

        RuleFor(x => x.FormerName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.formername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FormerName));

        RuleFor(x => x.FullName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.fullname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FullName));

        RuleFor(x => x.NativeName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.nativename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativeName));

        RuleFor(x => x.DisplayName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.displayname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        RuleFor(x => x.Gender)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.employee.gender"));

        RuleFor(x => x.IdCard)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employee.idcard"))
            .Must(TaktRegexHelper.IsValidIdCard).WithMessage(_validationMessages.IdCardInvalid("entity.employee.idcard"));

        RuleFor(x => x.Phone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(_validationMessages.Pattern("validation.patternPhone", "entity.employee.phone"))
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.employee.phone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Email)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(_validationMessages.Pattern("validation.patternEmail", "entity.employee.email"))
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.email", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Avatar)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employee.avatar", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Avatar));

        RuleFor(x => x.Nationality)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.employee.nationality"));

        RuleFor(x => x.Political)
            .InclusiveBetween(0, 12)
            .WithMessage(_validationMessages.FormatInvalid("entity.employee.political"));

        RuleFor(x => x.Marital)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.employee.marital"));

        RuleFor(x => x.NativePlace)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.nativeplace", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativePlace));

        RuleFor(x => x.CurrentAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employee.currentaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentAddress));

        RuleFor(x => x.RegisteredAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employee.registeredaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegisteredAddress));

        RuleFor(x => x.EmployeeStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.employee.employeestatus"));
    }
}

/// <summary>
/// Employee更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeUpdateDtoValidator : AbstractValidator<TaktEmployeeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employee.employeeid"));

        RuleFor(x => x.FormerName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.formername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FormerName));

        RuleFor(x => x.FullName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.fullname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FullName));

        RuleFor(x => x.NativeName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.nativename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativeName));

        RuleFor(x => x.DisplayName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.displayname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        RuleFor(x => x.Phone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(_validationMessages.Pattern("validation.patternPhone", "entity.employee.phone"))
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.employee.phone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Email)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(_validationMessages.Pattern("validation.patternEmail", "entity.employee.email"))
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.email", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Avatar)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employee.avatar", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Avatar));

        RuleFor(x => x.NativePlace)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employee.nativeplace", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativePlace));

        RuleFor(x => x.CurrentAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employee.currentaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentAddress));

        RuleFor(x => x.RegisteredAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employee.registeredaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegisteredAddress));
    }
}
