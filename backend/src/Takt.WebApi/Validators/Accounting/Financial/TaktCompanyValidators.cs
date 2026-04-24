// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Financial
// 文件名称：TaktCompanyValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Company DTO 验证器（根据实体 TaktCompany 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Accounting.Financial;

/// <summary>
/// Company创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktCompany"/> 字段对齐）。
/// </summary>
public class TaktCompanyCreateDtoValidator : AbstractValidator<TaktCompanyCreateDto>
{
    public TaktCompanyCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.company.companycode"))
            .Length(1, 4).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.company.companycode", 1, 4));

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.company.companyname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.company.companyname", 1, 200));

        RuleFor(x => x.CompanyShortName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyShortName));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.CompanyPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyPhone));

        RuleFor(x => x.CompanyEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyEmail));

        RuleFor(x => x.CompanyFax)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyfax", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyFax));

        RuleFor(x => x.CompanyWebsite)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companywebsite", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyWebsite));

        RuleFor(x => x.UnifiedSocialCreditCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.unifiedsocialcreditcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.UnifiedSocialCreditCode));

        RuleFor(x => x.TaxRegistrationNumber)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.taxregistrationnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TaxRegistrationNumber));

        RuleFor(x => x.LegalRepresentative)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.legalrepresentative", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LegalRepresentative));

        RuleFor(x => x.CompanyManager)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companymanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.CompanyStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.company.companystatus"));
    }
}

/// <summary>
/// Company更新 DTO 验证器。
/// </summary>
public class TaktCompanyUpdateDtoValidator : AbstractValidator<TaktCompanyUpdateDto>
{
    public TaktCompanyUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktCompanyCreateDtoValidator(localizer));

        RuleFor(x => x.CompanyId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.company.companyid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(4).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companycode", 4))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.CompanyName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyName));

        RuleFor(x => x.CompanyShortName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyShortName));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.CompanyPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyPhone));

        RuleFor(x => x.CompanyEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyEmail));

        RuleFor(x => x.CompanyFax)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companyfax", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyFax));

        RuleFor(x => x.CompanyWebsite)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companywebsite", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyWebsite));

        RuleFor(x => x.UnifiedSocialCreditCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.unifiedsocialcreditcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.UnifiedSocialCreditCode));

        RuleFor(x => x.TaxRegistrationNumber)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.taxregistrationnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TaxRegistrationNumber));

        RuleFor(x => x.LegalRepresentative)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.legalrepresentative", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LegalRepresentative));

        RuleFor(x => x.CompanyManager)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.companymanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.company.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
