// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Tenant
// 文件名称：TaktUserTenant.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户租户关联实体，定义用户与租户的多对多关系
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt用户租户关联实体
/// </summary>
/// <remarks>
/// 重要说明 - ConfigId 与 TenantId 的区别：
/// 
/// 1. ConfigId（继承自 TaktEntityBase，string类型）：
///    - 用途：数据隔离，表示 TaktUserTenant 表本身存储在哪個租户库
///    - 值：通常固定为 "0"（主库），因为用户-租户关联表统一存储在主库
///    - 作用：在单库模式下，通过此字段过滤数据；在多库模式下，用于确定查询哪个数据库
/// 
/// 2. TenantId（long类型）：
///    - 用途：业务关系，表示用户属于哪个租户
///    - 值：关联到 TaktTenant.Id（主键），通过此字段可以查询到租户的 ConfigId
///    - 作用：建立用户与租户的多对多关系，用于权限验证
/// 
/// 关系说明：
/// - ConfigId 表示"表存储位置"（数据隔离层）
/// - TenantId 表示"业务关联关系"（业务逻辑层）
/// - 两者不冲突：ConfigId 是表级别的，TenantId 是记录级别的业务关系
/// 
/// 查询逻辑：
/// - 通过 ConfigId 确定查询哪个数据库（主库 "0"）
/// - 通过 TenantId 关联到 TaktTenant.Id，再通过 TaktTenant.ConfigId 获取租户的配置ID
/// </remarks>
[SugarTable("takt_identity_user_tenant", "用户租户关联表")]
[SugarIndex("ix_takt_identity_user_tenant_user_id_tenant_id", nameof(UserId), OrderByType.Asc, nameof(TenantId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_user_tenant_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_tenant_tenant_id", nameof(TenantId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_tenant_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_tenant_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktUserTenant : TaktEntityBase
{
    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 租户ID（关联到 TaktTenant.Id，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TenantId { get; set; }
}
