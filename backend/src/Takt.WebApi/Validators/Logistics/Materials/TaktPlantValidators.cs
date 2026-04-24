// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPlantValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Plant DTO 验证器（根据实体 TaktPlant 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Materials;

/// <summary>
/// Plant创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPlant"/> 字段对齐）。
/// </summary>
public class TaktPlantCreateDtoValidator : AbstractValidator<TaktPlantCreateDto>
{
    public TaktPlantCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.plant.plantcode"))
            .Length(1, 4).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.plant.plantcode", 1, 4));

        RuleFor(x => x.PlantName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.plant.plantname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.plant.plantname", 1, 200));

        RuleFor(x => x.PlantShortName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantShortName));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.PlantAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantAddress));

        RuleFor(x => x.PlantPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantPhone));

        RuleFor(x => x.PlantEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantEmail));

        RuleFor(x => x.PlantManager)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantmanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedCompany)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.relatedcompany", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedCompany));

        RuleFor(x => x.PlantStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plant.plantstatus"));
    }
}

/// <summary>
/// Plant更新 DTO 验证器。
/// </summary>
public class TaktPlantUpdateDtoValidator : AbstractValidator<TaktPlantUpdateDto>
{
    public TaktPlantUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPlantCreateDtoValidator(localizer));

        RuleFor(x => x.PlantId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.plant.plantid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(4).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantcode", 4))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.PlantName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantName));

        RuleFor(x => x.PlantShortName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantShortName));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.PlantAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantAddress));

        RuleFor(x => x.PlantPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantPhone));

        RuleFor(x => x.PlantEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantEmail));

        RuleFor(x => x.PlantManager)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.plantmanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedCompany)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plant.relatedcompany", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedCompany));
    }
}
