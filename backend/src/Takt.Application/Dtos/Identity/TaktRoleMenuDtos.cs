// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktRoleMenuDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：角色菜单关联表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 角色菜单关联表Dto
/// </summary>
public partial class TaktRoleMenuDto : TaktDtosEntityBase
{
    /// <summary>
    /// 角色菜单关联表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleMenuId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }
    /// <summary>
    /// 菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }
}

/// <summary>
/// 角色菜单关联表查询DTO
/// </summary>
public partial class TaktRoleMenuQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleMenuQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 角色菜单关联表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleMenuId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RoleId { get; set; }
    /// <summary>
    /// 菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MenuId { get; set; }

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
/// Takt创建角色菜单关联表DTO
/// </summary>
public partial class TaktRoleMenuCreateDto
{
        /// <summary>
    /// 角色ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

        /// <summary>
    /// 菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

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
/// Takt更新角色菜单关联表DTO
/// </summary>
public partial class TaktRoleMenuUpdateDto : TaktRoleMenuCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleMenuUpdateDto()
    {
    }

        /// <summary>
    /// 角色菜单关联表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleMenuId { get; set; }
}

/// <summary>
/// 角色菜单关联表导入模板DTO
/// </summary>
public partial class TaktRoleMenuTemplateDto
{
        /// <summary>
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

        /// <summary>
    /// 菜单ID
    /// </summary>
    public long MenuId { get; set; }

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
/// 角色菜单关联表导入DTO
/// </summary>
public partial class TaktRoleMenuImportDto
{
        /// <summary>
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

        /// <summary>
    /// 菜单ID
    /// </summary>
    public long MenuId { get; set; }

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
/// 角色菜单关联表导出DTO
/// </summary>
public partial class TaktRoleMenuExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleMenuExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

        /// <summary>
    /// 菜单ID
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}