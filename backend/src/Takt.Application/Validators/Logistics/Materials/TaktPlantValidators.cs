// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Materials
// 文件名称：TaktPlantValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Plant DTO 验证器（根据实体 TaktPlant 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Validators.Logistics.Materials;

/// <summary>
/// Plant创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPlant"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPlantCreateDtoValidator : AbstractValidator<TaktPlantCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPlantCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.plant.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.plant.plantcode"));

        RuleFor(x => x.PlantName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.plant.plantname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.plant.plantname", 1, 200));

        RuleFor(x => x.PlantShortName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plant.plantshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantShortName));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.plant.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.plant.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.PlantAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.plant.plantaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantAddress));

        RuleFor(x => x.PlantPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.plantphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantPhone));

        RuleFor(x => x.PlantEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plant.plantemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantEmail));

        RuleFor(x => x.PlantManager)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.plantmanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plant.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.plant.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedCompany)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.relatedcompany", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedCompany));

        RuleFor(x => x.PlantStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.plant.plantstatus"));
    }
}

/// <summary>
/// Plant更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPlantCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PlantId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPlantUpdateDtoValidator : AbstractValidator<TaktPlantUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPlantUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPlantCreateDtoValidator(validationMessages));

        RuleFor(x => x.PlantId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.plant.plantid"));

        RuleFor(x => x.PlantName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plant.plantname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantName));

        RuleFor(x => x.PlantShortName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plant.plantshortname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantShortName));

        RuleFor(x => x.RegistrationAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.plant.registrationaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationAddress));

        RuleFor(x => x.RegistrationRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.registrationregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationRegion));

        RuleFor(x => x.RegistrationProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.registrationprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationProvince));

        RuleFor(x => x.RegistrationCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.registrationcity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationCity));

        RuleFor(x => x.BusinessRegion)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.businessregion", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessRegion));

        RuleFor(x => x.BusinessProvince)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.businessprovince", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessProvince));

        RuleFor(x => x.BusinessCity)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.businesscity", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessCity));

        RuleFor(x => x.BusinessAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.plant.businessaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessAddress));

        RuleFor(x => x.PlantAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.plant.plantaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantAddress));

        RuleFor(x => x.PlantPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.plantphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantPhone));

        RuleFor(x => x.PlantEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plant.plantemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantEmail));

        RuleFor(x => x.PlantManager)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.plantmanager", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantManager));

        RuleFor(x => x.EnterpriseNature)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.enterprisenature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseNature));

        RuleFor(x => x.IndustryAttribute)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plant.industryattribute", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustryAttribute));

        RuleFor(x => x.EnterpriseScale)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.enterprisescale", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EnterpriseScale));

        RuleFor(x => x.BusinessScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.plant.businessscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessScope));

        RuleFor(x => x.RelatedCompany)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plant.relatedcompany", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedCompany));
    }
}
