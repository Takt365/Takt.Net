// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Tenant
// 文件名称：TaktTenant.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户实体，定义租户领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt租户实体
/// </summary>
[SugarTable("takt_identity_tenant", "租户表")]
[SugarIndex("ix_takt_identity_tenant_tenant_code", nameof(TenantCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_tenant_config_id", nameof(ConfigId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_tenant_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_tenant_tenant_status", nameof(TenantStatus), OrderByType.Asc)]
public class TaktTenant : TaktEntityBase
{
    /// <summary>
    /// 租户名称
    /// </summary>
    [SugarColumn(ColumnName = "tenant_name", ColumnDescription = "租户名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string TenantName { get; set; } = string.Empty;

    /// <summary>
    /// 租户编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "tenant_code", ColumnDescription = "租户编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string TenantCode { get; set; } = string.Empty;

    /// <summary>
    /// 订阅开始时间（默认为当前时间）
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "订阅开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 订阅结束时间（默认为9999/12/31，表示长期有效）
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "订阅结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; } = new DateTime(9999, 12, 31);

    /// <summary>
    /// 租户状态（1=启用，0=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "tenant_status", ColumnDescription = "租户状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int TenantStatus { get; set; } = 1;
}