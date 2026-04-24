// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktTenantDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：租户信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 租户信息表Dto
/// </summary>
public partial class TaktTenantDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
    }

    /// <summary>
    /// 租户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TenantId { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }
    /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; }
    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime SubscriptionStartTime { get; set; }
    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime SubscriptionEndTime { get; set; }
    /// <summary>
    /// 租户状态
    /// </summary>
    public int TenantStatus { get; set; }
}

/// <summary>
/// 租户信息表查询DTO
/// </summary>
public partial class TaktTenantQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 租户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TenantId { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? TenantName { get; set; }
    /// <summary>
    /// 租户编码
    /// </summary>
    public string? TenantCode { get; set; }
    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime? SubscriptionStartTime { get; set; }

    /// <summary>
    /// 订阅开始时间开始时间
    /// </summary>
    public DateTime? SubscriptionStartTimeStart { get; set; }
    /// <summary>
    /// 订阅开始时间结束时间
    /// </summary>
    public DateTime? SubscriptionStartTimeEnd { get; set; }
    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime? SubscriptionEndTime { get; set; }

    /// <summary>
    /// 订阅结束时间开始时间
    /// </summary>
    public DateTime? SubscriptionEndTimeStart { get; set; }
    /// <summary>
    /// 订阅结束时间结束时间
    /// </summary>
    public DateTime? SubscriptionEndTimeEnd { get; set; }
    /// <summary>
    /// 租户状态
    /// </summary>
    public int? TenantStatus { get; set; }

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
/// Takt创建租户信息表DTO
/// </summary>
public partial class TaktTenantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantCreateDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
    }

        /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }

        /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; }

        /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime SubscriptionStartTime { get; set; }

        /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime SubscriptionEndTime { get; set; }

        /// <summary>
    /// 租户状态
    /// </summary>
    public int TenantStatus { get; set; }

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
/// Takt更新租户信息表DTO
/// </summary>
public partial class TaktTenantUpdateDto : TaktTenantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantUpdateDto()
    {
    }

        /// <summary>
    /// 租户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TenantId { get; set; }
}

/// <summary>
/// 租户信息表租户状态DTO
/// </summary>
public partial class TaktTenantStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantStatusDto()
    {
    }

        /// <summary>
    /// 租户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TenantId { get; set; }

    /// <summary>
    /// 租户状态（0=禁用，1=启用）
    /// </summary>
    public int TenantStatus { get; set; }
}

/// <summary>
/// 租户信息表导入模板DTO
/// </summary>
public partial class TaktTenantTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantTemplateDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
    }

        /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }

        /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; }

        /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime SubscriptionStartTime { get; set; }

        /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime SubscriptionEndTime { get; set; }

        /// <summary>
    /// 租户状态
    /// </summary>
    public int TenantStatus { get; set; }

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
/// 租户信息表导入DTO
/// </summary>
public partial class TaktTenantImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantImportDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
    }

        /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }

        /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; }

        /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime SubscriptionStartTime { get; set; }

        /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime SubscriptionEndTime { get; set; }

        /// <summary>
    /// 租户状态
    /// </summary>
    public int TenantStatus { get; set; }

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
/// 租户信息表导出DTO
/// </summary>
public partial class TaktTenantExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantExportDto()
    {
        CreatedAt = DateTime.Now;
        TenantName = string.Empty;
        TenantCode = string.Empty;
    }

        /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; }

        /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; }

        /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime SubscriptionStartTime { get; set; }

        /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime SubscriptionEndTime { get; set; }

        /// <summary>
    /// 租户状态
    /// </summary>
    public int TenantStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}