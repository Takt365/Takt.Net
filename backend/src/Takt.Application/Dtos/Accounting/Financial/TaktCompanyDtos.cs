// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktCompanyDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：公司信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// 公司信息表Dto
/// </summary>
public partial class TaktCompanyDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyDto()
    {
        CompanyCode = string.Empty;
        CompanyName = string.Empty;
    }

    /// <summary>
    /// 公司信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CompanyId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }
    /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }
    /// <summary>
    /// 公司简称
    /// </summary>
    public string? CompanyShortName { get; set; }
    /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }
    /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }
    /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }
    /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }
    /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }
    /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }
    /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }
    /// <summary>
    /// 公司电话
    /// </summary>
    public string? CompanyPhone { get; set; }
    /// <summary>
    /// 公司邮箱
    /// </summary>
    public string? CompanyEmail { get; set; }
    /// <summary>
    /// 公司传真
    /// </summary>
    public string? CompanyFax { get; set; }
    /// <summary>
    /// 公司网站
    /// </summary>
    public string? CompanyWebsite { get; set; }
    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }
    /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }
    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }
    /// <summary>
    /// 公司负责人
    /// </summary>
    public string? CompanyManager { get; set; }
    /// <summary>
    /// 注册资本
    /// </summary>
    public decimal RegisteredCapital { get; set; }
    /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }
    /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }
    /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }
    /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 公司状态
    /// </summary>
    public int CompanyStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 公司信息表查询DTO
/// </summary>
public partial class TaktCompanyQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 公司信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CompanyId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }
    /// <summary>
    /// 公司简称
    /// </summary>
    public string? CompanyShortName { get; set; }
    /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }
    /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }
    /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }
    /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }
    /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }
    /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }
    /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }
    /// <summary>
    /// 公司电话
    /// </summary>
    public string? CompanyPhone { get; set; }
    /// <summary>
    /// 公司邮箱
    /// </summary>
    public string? CompanyEmail { get; set; }
    /// <summary>
    /// 公司传真
    /// </summary>
    public string? CompanyFax { get; set; }
    /// <summary>
    /// 公司网站
    /// </summary>
    public string? CompanyWebsite { get; set; }
    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }
    /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }
    /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }
    /// <summary>
    /// 公司负责人
    /// </summary>
    public string? CompanyManager { get; set; }
    /// <summary>
    /// 注册资本
    /// </summary>
    public decimal? RegisteredCapital { get; set; }
    /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }

    /// <summary>
    /// 成立日期开始时间
    /// </summary>
    public DateTime? EstablishmentDateStart { get; set; }
    /// <summary>
    /// 成立日期结束时间
    /// </summary>
    public DateTime? EstablishmentDateEnd { get; set; }
    /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }
    /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }
    /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 公司状态
    /// </summary>
    public int? CompanyStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建公司信息表DTO
/// </summary>
public partial class TaktCompanyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyCreateDto()
    {
        CompanyCode = string.Empty;
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 公司简称
    /// </summary>
    public string? CompanyShortName { get; set; }

        /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }

        /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }

        /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }

        /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }

        /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }

        /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }

        /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }

        /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }

        /// <summary>
    /// 公司电话
    /// </summary>
    public string? CompanyPhone { get; set; }

        /// <summary>
    /// 公司邮箱
    /// </summary>
    public string? CompanyEmail { get; set; }

        /// <summary>
    /// 公司传真
    /// </summary>
    public string? CompanyFax { get; set; }

        /// <summary>
    /// 公司网站
    /// </summary>
    public string? CompanyWebsite { get; set; }

        /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

        /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }

        /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

        /// <summary>
    /// 公司负责人
    /// </summary>
    public string? CompanyManager { get; set; }

        /// <summary>
    /// 注册资本
    /// </summary>
    public decimal RegisteredCapital { get; set; }

        /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }

        /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }

        /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }

        /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }

        /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 公司状态
    /// </summary>
    public int CompanyStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新公司信息表DTO
/// </summary>
public partial class TaktCompanyUpdateDto : TaktCompanyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyUpdateDto()
    {
    }

        /// <summary>
    /// 公司信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CompanyId { get; set; }
}

/// <summary>
/// 公司信息表公司状态DTO
/// </summary>
public partial class TaktCompanyStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyStatusDto()
    {
    }

        /// <summary>
    /// 公司信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CompanyId { get; set; }

    /// <summary>
    /// 公司状态（0=禁用，1=启用）
    /// </summary>
    public int CompanyStatus { get; set; }
}

/// <summary>
/// 公司信息表导入模板DTO
/// </summary>
public partial class TaktCompanyTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyTemplateDto()
    {
        CompanyCode = string.Empty;
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 公司简称
    /// </summary>
    public string? CompanyShortName { get; set; }

        /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }

        /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }

        /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }

        /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }

        /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }

        /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }

        /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }

        /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }

        /// <summary>
    /// 公司电话
    /// </summary>
    public string? CompanyPhone { get; set; }

        /// <summary>
    /// 公司邮箱
    /// </summary>
    public string? CompanyEmail { get; set; }

        /// <summary>
    /// 公司传真
    /// </summary>
    public string? CompanyFax { get; set; }

        /// <summary>
    /// 公司网站
    /// </summary>
    public string? CompanyWebsite { get; set; }

        /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

        /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }

        /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

        /// <summary>
    /// 公司负责人
    /// </summary>
    public string? CompanyManager { get; set; }

        /// <summary>
    /// 注册资本
    /// </summary>
    public decimal RegisteredCapital { get; set; }

        /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }

        /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }

        /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }

        /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }

        /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 公司状态
    /// </summary>
    public int CompanyStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 公司信息表导入DTO
/// </summary>
public partial class TaktCompanyImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyImportDto()
    {
        CompanyCode = string.Empty;
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 公司简称
    /// </summary>
    public string? CompanyShortName { get; set; }

        /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }

        /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }

        /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }

        /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }

        /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }

        /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }

        /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }

        /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }

        /// <summary>
    /// 公司电话
    /// </summary>
    public string? CompanyPhone { get; set; }

        /// <summary>
    /// 公司邮箱
    /// </summary>
    public string? CompanyEmail { get; set; }

        /// <summary>
    /// 公司传真
    /// </summary>
    public string? CompanyFax { get; set; }

        /// <summary>
    /// 公司网站
    /// </summary>
    public string? CompanyWebsite { get; set; }

        /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

        /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }

        /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

        /// <summary>
    /// 公司负责人
    /// </summary>
    public string? CompanyManager { get; set; }

        /// <summary>
    /// 注册资本
    /// </summary>
    public decimal RegisteredCapital { get; set; }

        /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }

        /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }

        /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }

        /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }

        /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 公司状态
    /// </summary>
    public int CompanyStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 公司信息表导出DTO
/// </summary>
public partial class TaktCompanyExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanyExportDto()
    {
        CreatedAt = DateTime.Now;
        CompanyCode = string.Empty;
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 公司简称
    /// </summary>
    public string? CompanyShortName { get; set; }

        /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }

        /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }

        /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }

        /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }

        /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }

        /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }

        /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }

        /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }

        /// <summary>
    /// 公司电话
    /// </summary>
    public string? CompanyPhone { get; set; }

        /// <summary>
    /// 公司邮箱
    /// </summary>
    public string? CompanyEmail { get; set; }

        /// <summary>
    /// 公司传真
    /// </summary>
    public string? CompanyFax { get; set; }

        /// <summary>
    /// 公司网站
    /// </summary>
    public string? CompanyWebsite { get; set; }

        /// <summary>
    /// 统一社会信用代码
    /// </summary>
    public string? UnifiedSocialCreditCode { get; set; }

        /// <summary>
    /// 税务登记号
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }

        /// <summary>
    /// 法定代表人
    /// </summary>
    public string? LegalRepresentative { get; set; }

        /// <summary>
    /// 公司负责人
    /// </summary>
    public string? CompanyManager { get; set; }

        /// <summary>
    /// 注册资本
    /// </summary>
    public decimal RegisteredCapital { get; set; }

        /// <summary>
    /// 成立日期
    /// </summary>
    public DateTime? EstablishmentDate { get; set; }

        /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }

        /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }

        /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }

        /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 公司状态
    /// </summary>
    public int CompanyStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}