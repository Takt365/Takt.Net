using FluentValidation;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Organization;

/// <summary>
/// 岗位创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktPost"/> 及 TaktRegexHelper.PostCode 对齐）。
/// </summary>
public class TaktPostCreateDtoValidator : AbstractValidator<TaktPostCreateDto>
{
    public TaktPostCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PostName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.post.name"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.post.name", 1, 100));

        RuleFor(x => x.PostCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.post.code"))
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.PostCode, v))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPostCode", "entity.post.code"));

        RuleFor(x => x.DeptId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.post.deptid"));

        RuleFor(x => x.PostCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.category", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PostCategory));

        RuleFor(x => x.PostLevel)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.post.level"));

        RuleFor(x => x.PostDuty)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.duty", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PostDuty));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.post.datascope"));

        When(x => x.DataScope == 4, () =>
        {
            RuleFor(x => x.CustomScope)
                .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.post.customscope"))
                .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.customscope", 2000));
        });
    }
}

/// <summary>
/// 岗位更新 DTO 验证器。
/// </summary>
public class TaktPostUpdateDtoValidator : AbstractValidator<TaktPostUpdateDto>
{
    public TaktPostUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPostCreateDtoValidator(localizer));

        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.post.postid"));
    }
}

/// <summary>
/// 岗位状态 DTO 验证器。
/// </summary>
public class TaktPostStatusDtoValidator : AbstractValidator<TaktPostStatusDto>
{
    public TaktPostStatusDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.post.postid"));

        RuleFor(x => x.PostStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.post.status"));
    }
}

/// <summary>
/// 岗位分配用户 DTO 验证器。
/// </summary>
public class TaktPostAssignUsersDtoValidator : AbstractValidator<TaktPostAssignUsersDto>
{
    public TaktPostAssignUsersDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.post.postid"));
    }
}
