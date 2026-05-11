// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Code.Generator
// 文件名称：TaktGenTableColumnValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：GenTableColumn DTO 验证器（根据实体 TaktGenTableColumn 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Code.Generator;

namespace Takt.Application.Validators.Code.Generator;

/// <summary>
/// GenTableColumn创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Code.Generator.TaktGenTableColumn"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktGenTableColumnCreateDtoValidator : AbstractValidator<TaktGenTableColumnCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktGenTableColumnCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.DatabaseColumnName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.gentablecolumn.databasecolumnname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.gentablecolumn.databasecolumnname", 1, 200));

        RuleFor(x => x.ColumnComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.columncomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ColumnComment));

        RuleFor(x => x.DatabaseDataType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.gentablecolumn.databasedatatype"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.gentablecolumn.databasedatatype", 1, 100));

        RuleFor(x => x.CsharpDataType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.gentablecolumn.csharpdatatype"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.gentablecolumn.csharpdatatype", 1, 100));

        RuleFor(x => x.CsharpColumnName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.gentablecolumn.csharpcolumnname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.gentablecolumn.csharpcolumnname", 1, 100));

        RuleFor(x => x.IsPk)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.ispk"));

        RuleFor(x => x.IsIncrement)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.isincrement"));

        RuleFor(x => x.IsRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.isrequired"));

        RuleFor(x => x.IsCreate)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.iscreate"));

        RuleFor(x => x.IsUpdate)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.isupdate"));

        RuleFor(x => x.IsUnique)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.isunique"));

        RuleFor(x => x.IsList)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.islist"));

        RuleFor(x => x.IsExport)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.isexport"));

        RuleFor(x => x.IsSort)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.issort"));

        RuleFor(x => x.IsQuery)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.gentablecolumn.isquery"));

        RuleFor(x => x.QueryType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.gentablecolumn.querytype"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.gentablecolumn.querytype", 1, 20));

        RuleFor(x => x.HtmlType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.gentablecolumn.htmltype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.gentablecolumn.htmltype", 1, 50));

        RuleFor(x => x.DictType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.dicttype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictType));
    }
}

/// <summary>
/// GenTableColumn更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktGenTableColumnCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>GenTableColumnId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktGenTableColumnUpdateDtoValidator : AbstractValidator<TaktGenTableColumnUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktGenTableColumnUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktGenTableColumnCreateDtoValidator(validationMessages));

        RuleFor(x => x.GenTableColumnId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.gentablecolumn.gentablecolumnid"));

        RuleFor(x => x.DatabaseColumnName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.databasecolumnname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DatabaseColumnName));

        RuleFor(x => x.ColumnComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.columncomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ColumnComment));

        RuleFor(x => x.DatabaseDataType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.databasedatatype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DatabaseDataType));

        RuleFor(x => x.CsharpDataType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.csharpdatatype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CsharpDataType));

        RuleFor(x => x.CsharpColumnName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.csharpcolumnname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CsharpColumnName));

        RuleFor(x => x.QueryType)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.querytype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.QueryType));

        RuleFor(x => x.HtmlType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.htmltype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HtmlType));

        RuleFor(x => x.DictType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.gentablecolumn.dicttype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictType));
    }
}
