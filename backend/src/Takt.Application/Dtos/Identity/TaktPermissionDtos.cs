// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktPermissionDtos.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt权限DTO，包含权限相关的数据传输对象（查询、创建、更新、导出、模板、导入）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt权限DTO
/// </summary>
public class TaktPermissionDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPermissionDto()
    {
        PermissionCode = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 权限ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PermissionId { get; set; }

    /// <summary>
    /// 权限标识（唯一）
    /// </summary>
    public string PermissionCode { get; set; }

    /// <summary>
    /// 权限名称（展示用）
    /// </summary>
    public string? PermissionName { get; set; }

    /// <summary>
    /// 模块（如 identity、routine）
    /// </summary>
    public string? Module { get; set; }

    /// <summary>
    /// 关联菜单ID（可选，序列化为string以避免前端精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? MenuId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    public int PermissionStatus { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
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
    /// 更新人ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
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
}

/// <summary>
/// Takt权限查询DTO
/// </summary>
public class TaktPermissionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 权限标识（模糊）
    /// </summary>
    public string? PermissionCode { get; set; }

    /// <summary>
    /// 权限名称（模糊）
    /// </summary>
    public string? PermissionName { get; set; }

    /// <summary>
    /// 模块
    /// </summary>
    public string? Module { get; set; }

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    public int? PermissionStatus { get; set; }
}

/// <summary>
/// Takt创建权限DTO
/// </summary>
public class TaktPermissionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPermissionCreateDto()
    {
        PermissionCode = string.Empty;
    }

    /// <summary>
    /// 权限标识（唯一）
    /// </summary>
    public string PermissionCode { get; set; }

    /// <summary>
    /// 权限名称（展示用）
    /// </summary>
    public string? PermissionName { get; set; }

    /// <summary>
    /// 模块（如 identity、routine）
    /// </summary>
    public string? Module { get; set; }

    /// <summary>
    /// 关联菜单ID（可选）
    /// </summary>
    public long? MenuId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;
}

/// <summary>
/// Takt更新权限DTO
/// </summary>
public class TaktPermissionUpdateDto : TaktPermissionCreateDto
{
    /// <summary>
    /// 权限ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PermissionId { get; set; }

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    public int PermissionStatus { get; set; }
}

/// <summary>
/// Takt权限状态DTO
/// </summary>
public class TaktPermissionStatusDto
{
    /// <summary>
    /// 权限ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PermissionId { get; set; }

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    public int PermissionStatus { get; set; }
}

/// <summary>
/// Takt权限导出DTO
/// </summary>
public class TaktPermissionExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPermissionExportDto()
    {
        PermissionCode = string.Empty;
        PermissionName = string.Empty;
        Module = string.Empty;
    }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string PermissionCode { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; }

    /// <summary>
    /// 模块
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    public int PermissionStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// Takt权限导入模板DTO
/// </summary>
public class TaktPermissionTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPermissionTemplateDto()
    {
        PermissionCode = string.Empty;
        PermissionName = string.Empty;
        Module = string.Empty;
    }

    /// <summary>
    /// 权限标识（唯一）
    /// </summary>
    public string PermissionCode { get; set; }

    /// <summary>
    /// 权限名称（展示用）
    /// </summary>
    public string PermissionName { get; set; }

    /// <summary>
    /// 模块（如 identity、routine）
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// 关联菜单ID（可选，可空）
    /// </summary>
    public long? MenuId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    public int PermissionStatus { get; set; }
}

/// <summary>
/// Takt权限导入DTO
/// </summary>
public class TaktPermissionImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPermissionImportDto()
    {
        PermissionCode = string.Empty;
        PermissionName = string.Empty;
        Module = string.Empty;
    }

    /// <summary>
    /// 权限标识（唯一）
    /// </summary>
    public string PermissionCode { get; set; }

    /// <summary>
    /// 权限名称（展示用）
    /// </summary>
    public string PermissionName { get; set; }

    /// <summary>
    /// 模块（如 identity、routine）
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// 关联菜单ID（可选）
    /// </summary>
    public long? MenuId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    public int PermissionStatus { get; set; }
}
