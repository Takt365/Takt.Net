// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktFinancialDtos.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：财务专用DTO集合，包含有效期更新等业务专用DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

// ==================== 会计科目有效期 ====================

/// <summary>
/// 会计科目有效期更新DTO
/// </summary>
public partial class TaktAccountingTitleValidityDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleValidityDto()
    {
    }

    /// <summary>
    /// 会计科目ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AccountingTitleId { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }
}

// ==================== 固定资产日期更新 ====================

/// <summary>
/// 固定资产购买日期更新DTO
/// </summary>
public partial class TaktAssetPurchaseDateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetPurchaseDateDto()
    {
    }

    /// <summary>
    /// 固定资产ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime PurchaseDate { get; set; }
}

/// <summary>
/// 固定资产报废日期更新DTO
/// </summary>
public partial class TaktAssetScrapDateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetScrapDateDto()
    {
    }

    /// <summary>
    /// 固定资产ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 报废日期
    /// </summary>
    public DateTime ScrapDate { get; set; }
}

/// <summary>
/// 固定资产处置日期更新DTO
/// </summary>
public partial class TaktAssetDisposalDateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetDisposalDateDto()
    {
    }

    /// <summary>
    /// 固定资产ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 处置日期
    /// </summary>
    public DateTime DisposalDate { get; set; }
}

/// <summary>
/// 固定资产启用日期更新DTO
/// </summary>
public partial class TaktAssetStartDateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetStartDateDto()
    {
    }

    /// <summary>
    /// 固定资产ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime StartDate { get; set; }
}

// ==================== 固定资产配置更新 ====================

/// <summary>
/// 固定资产折旧方法更新DTO
/// </summary>
public partial class TaktAssetDepreciationMethodDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetDepreciationMethodDto()
    {
    }

    /// <summary>
    /// 固定资产ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }
}

/// <summary>
/// 固定资产位置更新DTO
/// </summary>
public partial class TaktAssetLocationDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetLocationDto()
    {
        Location = string.Empty;
    }

    /// <summary>
    /// 固定资产ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 资产位置
    /// </summary>
    public string Location { get; set; }
}