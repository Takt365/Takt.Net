// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Code.Generator
// 文件名称：TaktGenTableColumnValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：GenTableColumn DTO 验证器（根据实体 TaktGenTableColumn 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Code.Generator;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Code.Generator;

/// <summary>
/// GenTableColumn创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Code.Generator.TaktGenTableColumn"/> 字段对齐）。
/// </summary>
public class TaktGenTableColumnCreateDtoValidator : AbstractValidator<TaktGenTableColumnCreateDto>
{
    public TaktGenTableColumnCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DatabaseColumnName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentablecolumn.databasecolumnname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentablecolumn.databasecolumnname", 1, 200));

        RuleFor(x => x.ColumnComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.columncomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ColumnComment));

        RuleFor(x => x.DatabaseDataType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentablecolumn.databasedatatype"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentablecolumn.databasedatatype", 1, 100));

        RuleFor(x => x.CsharpDataType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentablecolumn.csharpdatatype"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentablecolumn.csharpdatatype", 1, 100));

        RuleFor(x => x.CsharpColumnName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentablecolumn.csharpcolumnname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentablecolumn.csharpcolumnname", 1, 100));

        RuleFor(x => x.IsPk)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.ispk"));

        RuleFor(x => x.IsIncrement)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.isincrement"));

        RuleFor(x => x.IsRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.isrequired"));

        RuleFor(x => x.IsCreate)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.iscreate"));

        RuleFor(x => x.IsUpdate)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.isupdate"));

        RuleFor(x => x.IsUnique)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.isunique"));

        RuleFor(x => x.IsList)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.islist"));

        RuleFor(x => x.IsExport)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.isexport"));

        RuleFor(x => x.IsSort)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.issort"));

        RuleFor(x => x.IsQuery)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentablecolumn.isquery"));

        RuleFor(x => x.QueryType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentablecolumn.querytype"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentablecolumn.querytype", 1, 20));

        RuleFor(x => x.HtmlType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentablecolumn.htmltype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentablecolumn.htmltype", 1, 50));

        RuleFor(x => x.DictType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.dicttype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictType));
    }
}

/// <summary>
/// GenTableColumn更新 DTO 验证器。
/// </summary>
public class TaktGenTableColumnUpdateDtoValidator : AbstractValidator<TaktGenTableColumnUpdateDto>
{
    public TaktGenTableColumnUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktGenTableColumnCreateDtoValidator(localizer));

        RuleFor(x => x.GenTableColumnId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.gentablecolumn.gentablecolumnid"));

        RuleFor(x => x.DatabaseColumnName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.databasecolumnname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DatabaseColumnName));

        RuleFor(x => x.ColumnComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.columncomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ColumnComment));

        RuleFor(x => x.DatabaseDataType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.databasedatatype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DatabaseDataType));

        RuleFor(x => x.CsharpDataType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.csharpdatatype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CsharpDataType));

        RuleFor(x => x.CsharpColumnName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.csharpcolumnname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CsharpColumnName));

        RuleFor(x => x.QueryType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.querytype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.QueryType));

        RuleFor(x => x.HtmlType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.htmltype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HtmlType));

        RuleFor(x => x.DictType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentablecolumn.dicttype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictType));
    }
}
