// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Tenant
// 文件名称：TaktUserTenantDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户租户关联DTO，定义用户与租户的关联关系数据传输对象（用于获取租户用户列表）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt用户租户关联DTO（用于获取租户用户列表，即根据租户ID获取该租户下的用户列表）
/// </summary>
public class TaktUserTenantDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserTenantDto()
    {
        ConfigId = "0";
        UserName = string.Empty;
        RealName = string.Empty;
        TenantName = string.Empty;
        TenantCode = string.Empty;
        TenantConfigId = "0";
    }

    /// <summary>
    /// 用户租户关联ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserTenantId { get; set; }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 租户ID（关联到 TaktTenant.Id，序列化为string以避免Javascript精度问题）
    /// </summary>
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
    /// 租户配置ID（从 TaktTenant.ConfigId 获取，用于多租户数据隔离和数据库切换）
    /// </summary>
    public string TenantConfigId { get; set; }

    /// <summary>
    /// 租户状态（0=启用，1=禁用）
    /// </summary>
    public int TenantStatus { get; set; }

    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

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
