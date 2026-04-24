// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktRoleDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：角色信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 角色信息表Dto
/// </summary>
public partial class TaktRoleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
    }

    /// <summary>
    /// 角色信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
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
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }
    /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }
    /// <summary>
    /// 角色状态
    /// </summary>
    public int RoleStatus { get; set; }
}

/// <summary>
/// 角色信息表查询DTO
/// </summary>
public partial class TaktRoleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 角色信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string? RoleName { get; set; }
    /// <summary>
    /// 角色编码
    /// </summary>
    public string? RoleCode { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 数据范围
    /// </summary>
    public int? DataScope { get; set; }
    /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }
    /// <summary>
    /// 角色状态
    /// </summary>
    public int? RoleStatus { get; set; }

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
/// Takt创建角色信息表DTO
/// </summary>
public partial class TaktRoleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleCreateDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
    }

        /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }

        /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 角色状态
    /// </summary>
    public int RoleStatus { get; set; }

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
/// Takt更新角色信息表DTO
/// </summary>
public partial class TaktRoleUpdateDto : TaktRoleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleUpdateDto()
    {
    }

        /// <summary>
    /// 角色信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }
}

/// <summary>
/// 角色信息表角色状态DTO
/// </summary>
public partial class TaktRoleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleStatusDto()
    {
    }

        /// <summary>
    /// 角色信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 角色状态（0=禁用，1=启用）
    /// </summary>
    public int RoleStatus { get; set; }
}

/// <summary>
/// 角色信息表导入模板DTO
/// </summary>
public partial class TaktRoleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleTemplateDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
    }

        /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }

        /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 角色状态
    /// </summary>
    public int RoleStatus { get; set; }

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
/// 角色信息表导入DTO
/// </summary>
public partial class TaktRoleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleImportDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
    }

        /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }

        /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 角色状态
    /// </summary>
    public int RoleStatus { get; set; }

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
/// 角色信息表导出DTO
/// </summary>
public partial class TaktRoleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleExportDto()
    {
        CreatedAt = DateTime.Now;
        RoleName = string.Empty;
        RoleCode = string.Empty;
    }

        /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }

        /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 角色状态
    /// </summary>
    public int RoleStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}