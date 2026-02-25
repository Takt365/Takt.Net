// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Tenant
// 文件名称：TaktTenantDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户DTO，包含租户相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt租户DTO
/// </summary>
public class TaktTenantDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 租户ID（适配字段，序列化为string以避免Javascript精度问题）
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
    /// 租户配置ID（ConfigId，用于多租户数据隔离和数据库切换）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 租户状态（0=启用，1=禁用）
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
/// Takt租户查询DTO
/// </summary>
public class TaktTenantQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在租户名称、租户编码中模糊查询

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? TenantName { get; set; }

    /// <summary>
    /// 租户编码
    /// </summary>
    public string? TenantCode { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string? ConfigId { get; set; }

    /// <summary>
    /// 租户状态（0=启用，1=禁用）
    /// </summary>
    public int? TenantStatus { get; set; }
}

/// <summary>
/// Takt创建租户DTO
/// </summary>
public class TaktTenantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantCreateDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string TenantName { get; set; } = string.Empty;

    /// <summary>
    /// 租户编码
    /// </summary>
    public string TenantCode { get; set; } = string.Empty;

    /// <summary>
    /// 租户配置ID（ConfigId，用于多租户数据隔离和数据库切换）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新租户DTO
/// </summary>
public class TaktTenantUpdateDto : TaktTenantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantUpdateDto()
    {
    }

    /// <summary>
    /// 租户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TenantId { get; set; }
}

/// <summary>
/// Takt租户状态DTO
/// </summary>
public class TaktTenantStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantStatusDto()
    {
    }

    /// <summary>
    /// 租户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TenantId { get; set; }

    /// <summary>
    /// 租户状态（0=启用，1=禁用）
    /// </summary>
    public int TenantStatus { get; set; }
}

/// <summary>
/// Takt租户导入模板DTO
/// </summary>
public class TaktTenantTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantTemplateDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ConfigId = "0";
        TenantStatus = 0;
        CreateTime = DateTime.Now;
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
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 租户状态（0=启用，1=禁用）
    /// </summary>
    public int TenantStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// Takt租户导入DTO
/// </summary>
public class TaktTenantImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantImportDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ConfigId = "0";
        TenantStatus = 0;
        CreateTime = DateTime.Now;
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
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 租户状态（0=启用，1=禁用）
    /// </summary>
    public int TenantStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// Takt租户导出DTO
/// </summary>
public class TaktTenantExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTenantExportDto()
    {
        TenantName = string.Empty;
        TenantCode = string.Empty;
        ConfigId = "0";
        TenantStatus = 0;
        CreateTime = DateTime.Now;
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
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 订阅开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 订阅结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 租户状态（0=启用，1=禁用）
    /// </summary>
    public int TenantStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
