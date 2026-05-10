// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktControllingDtos.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：成本管控专用DTO集合，包含有效期更新等业务专用DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

// ==================== 成本中心有效期 ====================

/// <summary>
/// 成本中心有效期更新DTO
/// </summary>
public partial class TaktCostCenterValidityDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterValidityDto()
    {
    }

    /// <summary>
    /// 成本中心ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostCenterId { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }
}

// ==================== 成本要素有效期 ====================

/// <summary>
/// 成本要素有效期更新DTO
/// </summary>
public partial class TaktCostElementValidityDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementValidityDto()
    {
    }

    /// <summary>
    /// 成本要素ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }
}

// ==================== 利润中心有效期 ====================

/// <summary>
/// 利润中心有效期更新DTO
/// </summary>
public partial class TaktProfitCenterValidityDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterValidityDto()
    {
    }

    /// <summary>
    /// 利润中心ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProfitCenterId { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }
}