// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktBankDtos.cs
// 功能描述：Takt银行DTO，包含银行相关的数据传输对象（查询、创建、更新、状态、导入、导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// Takt银行DTO
/// </summary>
public class TaktBankDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBankDto()
    {
        CompanyCode = string.Empty;
        BankCode = string.Empty;
        BankName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 银行ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long BankId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

    /// <summary>
    /// 银行编码
    /// </summary>
    public string BankCode { get; set; }

    /// <summary>
    /// 银行名称
    /// </summary>
    public string BankName { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// Swift代码/联行号
    /// </summary>
    public string? SwiftCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    public int BankStatus { get; set; }

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
/// Takt银行查询DTO
/// </summary>
public class TaktBankQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 银行编码
    /// </summary>
    public string? BankCode { get; set; }

    /// <summary>
    /// 银行名称
    /// </summary>
    public string? BankName { get; set; }

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    public int? BankStatus { get; set; }
}

/// <summary>
/// Takt创建银行DTO
/// </summary>
public class TaktBankCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBankCreateDto()
    {
        CompanyCode = string.Empty;
        BankCode = string.Empty;
        BankName = string.Empty;
    }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

    /// <summary>
    /// 银行编码
    /// </summary>
    public string BankCode { get; set; }

    /// <summary>
    /// 银行名称
    /// </summary>
    public string BankName { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// Swift代码/联行号
    /// </summary>
    public string? SwiftCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    public int BankStatus { get; set; }
}

/// <summary>
/// Takt更新银行DTO
/// </summary>
public class TaktBankUpdateDto : TaktBankCreateDto
{
    /// <summary>
    /// 银行ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long BankId { get; set; }
}

/// <summary>
/// Takt银行状态DTO
/// </summary>
public class TaktBankStatusDto
{
    /// <summary>
    /// 银行ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long BankId { get; set; }

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    public int BankStatus { get; set; }
}

/// <summary>
/// Takt银行导入模板DTO
/// </summary>
public class TaktBankTemplateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行编码
    /// </summary>
    public string BankCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行名称
    /// </summary>
    public string BankName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// Swift代码/联行号
    /// </summary>
    public string? SwiftCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    public int BankStatus { get; set; }
}

/// <summary>
/// Takt银行导入DTO
/// </summary>
public class TaktBankImportDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行编码
    /// </summary>
    public string BankCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行名称
    /// </summary>
    public string BankName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// Swift代码/联行号
    /// </summary>
    public string? SwiftCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    public int BankStatus { get; set; }
}

/// <summary>
/// Takt银行导出DTO
/// </summary>
public class TaktBankExportDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行编码
    /// </summary>
    public string BankCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行名称
    /// </summary>
    public string BankName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    public string? ShortName { get; set; }

    /// <summary>
    /// Swift代码/联行号
    /// </summary>
    public string? SwiftCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    public string BankStatus { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
