// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktRolePermissionDto.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色权限关联DTO，用于列表/详情展示（角色-权限通过角色编辑分配，无需模板/导入/导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt角色权限关联DTO（用于获取角色权限列表，即根据角色ID获取该角色下的权限列表，或列表展示关联记录）
/// </summary>
public class TaktRolePermissionDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRolePermissionDto()
    {
        ConfigId = "0";
        RoleName = string.Empty;
        RoleCode = string.Empty;
        PermissionCode = string.Empty;
        PermissionName = string.Empty;
    }

    /// <summary>
    /// 角色权限关联ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RolePermissionId { get; set; }

    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; }

    /// <summary>
    /// 权限ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PermissionId { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string PermissionCode { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; }

    /// <summary>
    /// 权限所属模块
    /// </summary>
    public string? Module { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 创建人ID（与实体基类一致）
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
    /// 更新人ID（与实体基类一致）
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

    /// <summary>
    /// 删除人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
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
