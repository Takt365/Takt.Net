// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Financial
// 文件名称：TaktCompanyValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Company DTO 验证器（根据实体 TaktCompany 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;

namespace Takt.Application.Validators.Accounting.Financial;

/// <summary>
/// Company创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktCompany"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCompanyCreateDtoValidator : AbstractValidator<TaktCompanyCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCompanyCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.company.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.company.companycode"));

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.company.companyname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.company.companyname", 1, 200));

        RuleFor(x => x.CompanyShortName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.company.companyshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyShortName));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.company.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.company.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.CompanyPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.companyphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyPhone));

        RuleFor(x => x.CompanyEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.company.companyemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyEmail));

        RuleFor(x => x.CompanyFax)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.companyfax", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyFax));

        RuleFor(x => x.CompanyWebsite)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.company.companywebsite", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyWebsite));

        RuleFor(x => x.UnifiedSocialCreditCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.unifiedsocialcreditcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.UnifiedSocialCreditCode));

        RuleFor(x => x.TaxRegistrationNumber)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.taxregistrationnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TaxRegistrationNumber));

        RuleFor(x => x.LegalRepresentative)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.legalrepresentative", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LegalRepresentative));

        RuleFor(x => x.CompanyManager)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.companymanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.company.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.company.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.CompanyStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.company.companystatus"));
    }
}

/// <summary>
/// Company更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCompanyCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CompanyId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCompanyUpdateDtoValidator : AbstractValidator<TaktCompanyUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCompanyUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCompanyCreateDtoValidator(validationMessages));

        RuleFor(x => x.CompanyId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.company.companyid"));

        RuleFor(x => x.CompanyName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.company.companyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyName));

        RuleFor(x => x.CompanyShortName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.company.companyshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyShortName));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.company.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.company.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.CompanyPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.companyphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyPhone));

        RuleFor(x => x.CompanyEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.company.companyemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyEmail));

        RuleFor(x => x.CompanyFax)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.companyfax", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyFax));

        RuleFor(x => x.CompanyWebsite)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.company.companywebsite", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyWebsite));

        RuleFor(x => x.UnifiedSocialCreditCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.unifiedsocialcreditcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.UnifiedSocialCreditCode));

        RuleFor(x => x.TaxRegistrationNumber)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.taxregistrationnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TaxRegistrationNumber));

        RuleFor(x => x.LegalRepresentative)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.legalrepresentative", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LegalRepresentative));

        RuleFor(x => x.CompanyManager)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.companymanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.company.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.company.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.company.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
