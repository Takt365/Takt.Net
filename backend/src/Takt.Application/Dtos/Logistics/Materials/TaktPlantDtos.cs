// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPlantDtos.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂DTO，包含工厂相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// Takt工厂DTO
/// </summary>
public class TaktPlantDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        ChartOfAccounts = "takt";
        ConfigId = "0";
    }

    /// <summary>
    /// 工厂ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PlantId { get; set; }

    /// <summary>
    /// 工厂代码（唯一索引）
    /// </summary>
    public string PlantCode { get; set; }

    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }

    /// <summary>
    /// 工厂名称2（名称补充）
    /// </summary>
    public string? PlantName2 { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// 地区（行政区划最上级）
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 县
    /// </summary>
    public string? County { get; set; }

    /// <summary>
    /// 镇街
    /// </summary>
    public string? TownStreet { get; set; }

    /// <summary>
    /// 村庄
    /// </summary>
    public string? Village { get; set; }

    /// <summary>
    /// 完整地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 本位币（如 CNY、USD）
    /// </summary>
    public string? LocalCurrency { get; set; }

    /// <summary>
    /// 语言代码（如 1=中文）
    /// </summary>
    public string? LanguageCode { get; set; }

    /// <summary>
    /// 会计科目表
    /// </summary>
    public string ChartOfAccounts { get; set; }

    /// <summary>
    /// 控制范围
    /// </summary>
    public string? ControllingArea { get; set; }

    /// <summary>
    /// 销售组织
    /// </summary>
    public string? SalesOrg { get; set; }

    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }

    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }

    /// <summary>
    /// 工厂传真
    /// </summary>
    public string? PlantFax { get; set; }

    /// <summary>
    /// 工厂网站
    /// </summary>
    public string? PlantWebsite { get; set; }

    /// <summary>
    /// 社交账号（JSON 格式，如 [{"type":"wechat","account":"xxx"}]）
    /// </summary>
    public string? SocialAccounts { get; set; }

    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

    /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }

    /// <summary>
    /// 增值税登记号
    /// </summary>
    public string? VatRegistrationNumber { get; set; }

    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

    /// <summary>
    /// 行业类别（字典 acct_industry_type）
    /// </summary>
    public string? IndustryType { get; set; }

    /// <summary>
    /// 企业性质（字典 acct_enterprise_registration_type）
    /// </summary>
    public string? EnterpriseRegistrationType { get; set; }

    /// <summary>
    /// 企业规模（字典 acct_enterprise_size，0=大型，1=中型，2=小型，3=微型）
    /// </summary>
    public int? EnterpriseSize { get; set; }

    /// <summary>
    /// 注册资本（精确到分）
    /// </summary>
    public decimal RegisteredCapital { get; set; }

    /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }

    /// <summary>
    /// 解散日期
    /// </summary>
    public DateTime? DissolutionDate { get; set; }

    /// <summary>
    /// 工厂状态（0=启用，1=禁用）
    /// </summary>
    public int PlantStatus { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON（与实体基类一致）
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt工厂查询DTO
/// </summary>
public class TaktPlantQueryDto : Takt.Shared.Models.TaktPagedQuery
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 工厂名称
    /// </summary>
    public string? PlantName { get; set; }

    /// <summary>
    /// 工厂状态（0=启用，1=禁用）
    /// </summary>
    public int? PlantStatus { get; set; }
}

/// <summary>
/// Takt创建工厂DTO
/// </summary>
public class TaktPlantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantCreateDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        ChartOfAccounts = "takt";
    }

    /// <summary>
    /// 工厂代码（唯一索引）
    /// </summary>
    public string PlantCode { get; set; }

    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }

    /// <summary>
    /// 工厂名称2（名称补充）
    /// </summary>
    public string? PlantName2 { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// 地区（行政区划最上级）
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 县
    /// </summary>
    public string? County { get; set; }

    /// <summary>
    /// 镇街
    /// </summary>
    public string? TownStreet { get; set; }

    /// <summary>
    /// 村庄
    /// </summary>
    public string? Village { get; set; }

    /// <summary>
    /// 完整地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 本位币（如 CNY、USD）
    /// </summary>
    public string? LocalCurrency { get; set; }

    /// <summary>
    /// 语言代码（如 1=中文）
    /// </summary>
    public string? LanguageCode { get; set; }

    /// <summary>
    /// 会计科目表
    /// </summary>
    public string ChartOfAccounts { get; set; }

    /// <summary>
    /// 控制范围
    /// </summary>
    public string? ControllingArea { get; set; }

    /// <summary>
    /// 销售组织
    /// </summary>
    public string? SalesOrg { get; set; }

    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }

    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }

    /// <summary>
    /// 工厂传真
    /// </summary>
    public string? PlantFax { get; set; }

    /// <summary>
    /// 工厂网站
    /// </summary>
    public string? PlantWebsite { get; set; }

    /// <summary>
    /// 社交账号（JSON 格式，如 [{"type":"wechat","account":"xxx"}]）
    /// </summary>
    public string? SocialAccounts { get; set; }

    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

    /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }

    /// <summary>
    /// 增值税登记号
    /// </summary>
    public string? VatRegistrationNumber { get; set; }

    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

    /// <summary>
    /// 行业类别（字典 acct_industry_type）
    /// </summary>
    public string? IndustryType { get; set; }

    /// <summary>
    /// 企业性质（字典 acct_enterprise_registration_type）
    /// </summary>
    public string? EnterpriseRegistrationType { get; set; }

    /// <summary>
    /// 企业规模（字典 acct_enterprise_size，0=大型，1=中型，2=小型，3=微型）
    /// </summary>
    public int? EnterpriseSize { get; set; }

    /// <summary>
    /// 注册资本（精确到分）
    /// </summary>
    public decimal RegisteredCapital { get; set; }

    /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }

    /// <summary>
    /// 解散日期
    /// </summary>
    public DateTime? DissolutionDate { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新工厂DTO
/// </summary>
public class TaktPlantUpdateDto : TaktPlantCreateDto
{
    /// <summary>
    /// 工厂ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PlantId { get; set; }
}

/// <summary>
/// Takt工厂状态DTO
/// </summary>
public class TaktPlantStatusDto
{
    /// <summary>
    /// 工厂ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PlantId { get; set; }

    /// <summary>
    /// 工厂状态（0=启用，1=禁用）
    /// </summary>
    public int PlantStatus { get; set; }
}

/// <summary>
/// Takt工厂导入模板DTO
/// </summary>
public class TaktPlantTemplateDto
{
    /// <summary>
    /// 工厂代码（唯一）
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// 地区
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 完整地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 本位币
    /// </summary>
    public string? LocalCurrency { get; set; }

    /// <summary>
    /// 销售组织
    /// </summary>
    public string? SalesOrg { get; set; }

    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }

    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }

    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 工厂状态（0=启用，1=禁用）
    /// </summary>
    public int PlantStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt工厂导入DTO
/// </summary>
public class TaktPlantImportDto
{
    /// <summary>
    /// 工厂代码（唯一）
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// 地区
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 完整地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 本位币
    /// </summary>
    public string? LocalCurrency { get; set; }

    /// <summary>
    /// 销售组织
    /// </summary>
    public string? SalesOrg { get; set; }

    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }

    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }

    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 工厂状态（0=启用，1=禁用）
    /// </summary>
    public int PlantStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt工厂导出DTO
/// </summary>
public class TaktPlantExportDto
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// 地区
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 完整地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 本位币
    /// </summary>
    public string? LocalCurrency { get; set; }

    /// <summary>
    /// 销售组织
    /// </summary>
    public string? SalesOrg { get; set; }

    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }

    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }

    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 工厂状态
    /// </summary>
    public string PlantStatus { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
