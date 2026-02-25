// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktRoleMenuDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色菜单关联DTO，定义角色与菜单的关联关系数据传输对象（用于获取角色菜单列表）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt角色菜单关联DTO（用于获取角色菜单列表，即根据角色ID获取该角色的菜单列表）
/// </summary>
public class TaktRoleMenuDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleMenuDto()
    {
        ConfigId = "0";
        RoleName = string.Empty;
        RoleCode = string.Empty;
        MenuName = string.Empty;
        MenuCode = string.Empty;
    }

    /// <summary>
    /// 角色菜单关联ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleMenuId { get; set; }

    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 菜单ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; }

    /// <summary>
    /// 菜单路径
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 菜单类型（0=目录，1=菜单，2=按钮）
    /// </summary>
    public int MenuType { get; set; }

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
