// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Accounting.Financial
// 文件名称：TaktCompany.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公司实体，定义公司领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Financial;

/// <summary>
/// Takt公司实体
/// </summary>
[SugarTable("takt_accounting_financial_company", "公司表")]
[SugarIndex("ix_takt_accounting_financial_company_company_code", nameof(CompanyCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_financial_company_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_company_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_company_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_company_company_status", nameof(CompanyStatus), OrderByType.Asc)]
public class TaktCompany : TaktEntityBase
{
    /// <summary>
    /// 公司代码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    [SugarColumn(ColumnName = "company_name", ColumnDescription = "公司名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称2（名称补充）
    /// </summary>
    [SugarColumn(ColumnName = "company_name2", ColumnDescription = "公司名称2", ColumnDataType = "nvarchar", Length = 30, IsNullable = true)]
    public string? CompanyName2 { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "short_name", ColumnDescription = "简称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ShortName { get; set; }

    /// <summary>
    /// 地区（行政区划最上级，覆盖国家/地区，因部分区域不被承认为国家）
    /// </summary>
    [SugarColumn(ColumnName = "region", ColumnDescription = "地区", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Region { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    [SugarColumn(ColumnName = "province", ColumnDescription = "省", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    [SugarColumn(ColumnName = "city", ColumnDescription = "市", ColumnDataType = "nvarchar", Length = 25, IsNullable = true)]
    public string? City { get; set; }

    /// <summary>
    /// 县
    /// </summary>
    [SugarColumn(ColumnName = "county", ColumnDescription = "县", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? County { get; set; }

    /// <summary>
    /// 镇街
    /// </summary>
    [SugarColumn(ColumnName = "town_street", ColumnDescription = "镇街", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? TownStreet { get; set; }

    /// <summary>
    /// 村庄
    /// </summary>
    [SugarColumn(ColumnName = "village", ColumnDescription = "村庄", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? Village { get; set; }

    /// <summary>
    /// 完整地址
    /// </summary>
    [SugarColumn(ColumnName = "address", ColumnDescription = "完整地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Address { get; set; }

    /// <summary>
    /// 本位币（如 CNY、USD）
    /// </summary>
    [SugarColumn(ColumnName = "local_currency", ColumnDescription = "本位币", ColumnDataType = "nvarchar", Length = 5, IsNullable = true)]
    public string? LocalCurrency { get; set; }

    /// <summary>
    /// 语言代码（如 1=中文）
    /// </summary>
    [SugarColumn(ColumnName = "language_code", ColumnDescription = "语言代码", ColumnDataType = "nvarchar", Length = 1, IsNullable = true)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// 会计科目表
    /// </summary>
    [SugarColumn(ColumnName = "chart_of_accounts", ColumnDescription = "会计科目表", ColumnDataType = "nvarchar", Length = 4, IsNullable = false, DefaultValue = "takt")]
    public string ChartOfAccounts { get; set; } = "takt";

    /// <summary>
    /// 控制范围
    /// </summary>
    [SugarColumn(ColumnName = "controlling_area", ColumnDescription = "控制范围", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? ControllingArea { get; set; }

    /// <summary>
    /// 采购组织
    /// </summary>
    [SugarColumn(ColumnName = "purchasing_org", ColumnDescription = "采购组织", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PurchasingOrg { get; set; }

    /// <summary>
    /// 公司电话
    /// </summary>
    [SugarColumn(ColumnName = "company_phone", ColumnDescription = "公司电话", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyPhone { get; set; }

    /// <summary>
    /// 公司邮箱
    /// </summary>
    [SugarColumn(ColumnName = "company_email", ColumnDescription = "公司邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? CompanyEmail { get; set; }

    /// <summary>
    /// 公司传真
    /// </summary>
    [SugarColumn(ColumnName = "company_fax", ColumnDescription = "公司传真", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyFax { get; set; }

    /// <summary>
    /// 公司网站
    /// </summary>
    [SugarColumn(ColumnName = "company_website", ColumnDescription = "公司网站", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? CompanyWebsite { get; set; }

    /// <summary>
    /// 社交账号（JSON 格式，如 [{"type":"wechat","account":"xxx"},{"type":"weibo","account":"yyy"}]）
    /// </summary>
    [SugarColumn(ColumnName = "social_accounts", ColumnDescription = "社交账号", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? SocialAccounts { get; set; }

    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    [SugarColumn(ColumnName = "unified_social_credit_code", ColumnDescription = "统一社会信用代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? UnifiedSocialCreditCode { get; set; }

    /// <summary>
    /// 税务登记号
    /// </summary>
    [SugarColumn(ColumnName = "tax_registration_number", ColumnDescription = "税务登记号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? TaxRegistrationNumber { get; set; }

    /// <summary>
    /// 增值税登记号
    /// </summary>
    [SugarColumn(ColumnName = "vat_registration_number", ColumnDescription = "增值税登记号", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? VatRegistrationNumber { get; set; }

    /// <summary>
    /// 法定代表人
    /// </summary>
    [SugarColumn(ColumnName = "legal_representative", ColumnDescription = "法定代表人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? LegalRepresentative { get; set; }

    /// <summary>
    /// 行业类别（《国民经济行业分类》GB/T 4754—2017 门类，字典 acct_industry_type，DictValue：A～T）
    /// </summary>
    [SugarColumn(ColumnName = "industry_type", ColumnDescription = "行业类别", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? IndustryType { get; set; }

    /// <summary>
    /// 企业性质（《关于划分企业登记注册类型的规定》国统字〔2011〕86号，字典 acct_enterprise_registration_type，DictValue：110/120/150 等）
    /// </summary>
    [SugarColumn(ColumnName = "enterprise_registration_type", ColumnDescription = "企业性质", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? EnterpriseRegistrationType { get; set; }

    /// <summary>
    /// 企业规模（《统计上大中小微型企业划分办法(2017)》，字典 acct_enterprise_size，0=大型，1=中型，2=小型，3=微型）
    /// </summary>
    [SugarColumn(ColumnName = "enterprise_size", ColumnDescription = "企业规模", ColumnDataType = "int", IsNullable = true)]
    public int? EnterpriseSize { get; set; }

    /// <summary>
    /// 注册资本（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "registered_capital", ColumnDescription = "注册资本", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal RegisteredCapital { get; set; } = 0;

    /// <summary>
    /// 成立日期
    /// </summary>
    [SugarColumn(ColumnName = "establishment_date", ColumnDescription = "成立日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EstablishmentDate { get; set; }

    /// <summary>
    /// 解散日期
    /// </summary>
    [SugarColumn(ColumnName = "dissolution_date", ColumnDescription = "解散日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DissolutionDate { get; set; }

    /// <summary>
    /// 公司状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "company_status", ColumnDescription = "公司状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CompanyStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 工厂ID（关联 TaktPlant.Id；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "plant_id", ColumnDescription = "工厂ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PlantId { get; set; }

    /// <summary>
    /// 工厂代码（冗余，便于列表展示；来源于 TaktPlant.PlantCode）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }
}
