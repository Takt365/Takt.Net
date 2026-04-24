// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Code.Generator
// 文件名称：TaktGenTableValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：GenTable DTO 验证器（根据实体 TaktGenTable 自动生成）
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
/// GenTable创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Code.Generator.TaktGenTable"/> 字段对齐）。
/// </summary>
public class TaktGenTableCreateDtoValidator : AbstractValidator<TaktGenTableCreateDto>
{
    public TaktGenTableCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DataSource)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.datasource", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSource));

        RuleFor(x => x.TableName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.tablename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.tablename", 1, 200));

        RuleFor(x => x.TableComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.tablecomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.TableComment));

        RuleFor(x => x.SubTableName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.subtablename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SubTableName));

        RuleFor(x => x.SubTableFkName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.subtablefkname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SubTableFkName));

        RuleFor(x => x.TreeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.treecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TreeCode));

        RuleFor(x => x.TreeParentCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.treeparentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TreeParentCode));

        RuleFor(x => x.TreeName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.treename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TreeName));

        RuleFor(x => x.InDatabase)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.indatabase"));

        RuleFor(x => x.GenTemplateCategory)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.gentemplatecategory"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.gentemplatecategory", 1, 50));

        RuleFor(x => x.GenModuleName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genmodulename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GenModuleName));

        RuleFor(x => x.GenBusinessName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.genbusinessname"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.genbusinessname", 1, 50));

        RuleFor(x => x.GenFunctionName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genfunctionname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GenFunctionName));

        RuleFor(x => x.PermsPrefix)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.permsprefix"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.permsprefix", 1, 100));

        RuleFor(x => x.MenuButtonGroup)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.menubuttongroup", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuButtonGroup));

        RuleFor(x => x.NamePrefix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.nameprefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NamePrefix));

        RuleFor(x => x.EntityNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.entitynamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EntityNamespace));

        RuleFor(x => x.EntityClassName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.entityclassname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.entityclassname", 1, 100));

        RuleFor(x => x.DtoNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.dtonamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DtoNamespace));

        RuleFor(x => x.DtoClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.dtoclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DtoClassName));

        RuleFor(x => x.ServiceNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.servicenamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ServiceNamespace));

        RuleFor(x => x.IServiceClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.iserviceclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IServiceClassName));

        RuleFor(x => x.ServiceClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.serviceclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ServiceClassName));

        RuleFor(x => x.ControllerNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.controllernamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ControllerNamespace));

        RuleFor(x => x.ControllerClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.controllerclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ControllerClassName));

        RuleFor(x => x.IsRepository)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.isrepository"));

        RuleFor(x => x.RepositoryInterfaceNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.repositoryinterfacenamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RepositoryInterfaceNamespace));

        RuleFor(x => x.IRepositoryClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.irepositoryclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IRepositoryClassName));

        RuleFor(x => x.RepositoryNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.repositorynamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RepositoryNamespace));

        RuleFor(x => x.RepositoryClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.repositoryclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RepositoryClassName));

        RuleFor(x => x.GenFunction)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genfunction", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.GenFunction));

        RuleFor(x => x.GenMethod)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.genmethod"));

        RuleFor(x => x.GenPath)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.genpath"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.genpath", 1, 500));

        RuleFor(x => x.IsGenMenu)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.isgenmenu"));

        RuleFor(x => x.IsGenTranslation)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.isgentranslation"));

        RuleFor(x => x.SortField)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.sortfield"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.sortfield", 1, 100));

        RuleFor(x => x.SortType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.sorttype"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.sorttype", 1, 10));

        RuleFor(x => x.FrontUi)
            .InclusiveBetween(1, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.frontui"));

        RuleFor(x => x.FrontFormLayout)
            .InclusiveBetween(12, 24)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.frontformlayout"));

        RuleFor(x => x.FrontBtnStyle)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.frontbtnstyle"));

        RuleFor(x => x.IsGenCode)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.isgencode"));

        RuleFor(x => x.IsUseTabs)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.gentable.isusetabs"));

        RuleFor(x => x.GenAuthor)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.genauthor"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.gentable.genauthor", 1, 50));

        RuleFor(x => x.OtherGenOptions)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.othergenoptions", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OtherGenOptions));
    }
}

/// <summary>
/// GenTable更新 DTO 验证器。
/// </summary>
public class TaktGenTableUpdateDtoValidator : AbstractValidator<TaktGenTableUpdateDto>
{
    public TaktGenTableUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktGenTableCreateDtoValidator(localizer));

        RuleFor(x => x.GenTableId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.gentable.gentableid"));

        RuleFor(x => x.DataSource)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.datasource", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSource));

        RuleFor(x => x.TableName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.tablename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TableName));

        RuleFor(x => x.TableComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.tablecomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.TableComment));

        RuleFor(x => x.SubTableName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.subtablename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SubTableName));

        RuleFor(x => x.SubTableFkName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.subtablefkname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SubTableFkName));

        RuleFor(x => x.TreeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.treecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TreeCode));

        RuleFor(x => x.TreeParentCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.treeparentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TreeParentCode));

        RuleFor(x => x.TreeName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.treename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TreeName));

        RuleFor(x => x.GenTemplateCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.gentemplatecategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GenTemplateCategory));

        RuleFor(x => x.GenModuleName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genmodulename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GenModuleName));

        RuleFor(x => x.GenBusinessName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genbusinessname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GenBusinessName));

        RuleFor(x => x.GenFunctionName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genfunctionname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GenFunctionName));

        RuleFor(x => x.PermsPrefix)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.permsprefix", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PermsPrefix));

        RuleFor(x => x.MenuButtonGroup)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.menubuttongroup", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuButtonGroup));

        RuleFor(x => x.NamePrefix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.nameprefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NamePrefix));

        RuleFor(x => x.EntityNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.entitynamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EntityNamespace));

        RuleFor(x => x.EntityClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.entityclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EntityClassName));

        RuleFor(x => x.DtoNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.dtonamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DtoNamespace));

        RuleFor(x => x.DtoClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.dtoclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DtoClassName));

        RuleFor(x => x.ServiceNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.servicenamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ServiceNamespace));

        RuleFor(x => x.IServiceClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.iserviceclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IServiceClassName));

        RuleFor(x => x.ServiceClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.serviceclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ServiceClassName));

        RuleFor(x => x.ControllerNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.controllernamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ControllerNamespace));

        RuleFor(x => x.ControllerClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.controllerclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ControllerClassName));

        RuleFor(x => x.RepositoryInterfaceNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.repositoryinterfacenamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RepositoryInterfaceNamespace));

        RuleFor(x => x.IRepositoryClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.irepositoryclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IRepositoryClassName));

        RuleFor(x => x.RepositoryNamespace)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.repositorynamespace", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RepositoryNamespace));

        RuleFor(x => x.RepositoryClassName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.repositoryclassname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RepositoryClassName));

        RuleFor(x => x.GenFunction)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genfunction", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.GenFunction));

        RuleFor(x => x.GenPath)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genpath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.GenPath));

        RuleFor(x => x.SortField)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.sortfield", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SortField));

        RuleFor(x => x.SortType)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.sorttype", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.SortType));

        RuleFor(x => x.GenAuthor)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.genauthor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GenAuthor));

        RuleFor(x => x.OtherGenOptions)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.gentable.othergenoptions", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OtherGenOptions));
    }
}
