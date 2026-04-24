// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos
// 文件名称：TaktDtoBase.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt DTO 基类，包含审计等通用字段（租户配置ID、扩展字段JSON、备注、CreatedById/CreatedBy/CreatedAt、UpdatedById/UpdatedBy/UpdatedAt、软删除、DeletedById/DeletedBy/DeletedAt）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Constants;

namespace Takt.Application.Dtos;

/// <summary>
/// Takt DTO 基类（主 DTO 继承此类可复用审计等通用字段）
/// </summary>
public abstract class TaktDtosEntityBase
{
    /// <summary>
    /// 主键ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long Id { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID（非空；种子等无当前用户时仓储填 <see cref="TaktAppConstants.InitUserId"/>）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreatedById { get; set; }

    /// <summary>
    /// 创建人（用户名，非空；种子等无当前用户时仓储填 <see cref="TaktAppConstants.InitUserName"/>）
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间（非空）
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更新人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
     [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdatedById { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeletedById { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}
