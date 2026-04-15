// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktRoleDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色DTO，包含角色相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt角色DTO
/// </summary>
public class TaktRoleDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 角色ID（适配字段，序列化为string以避免Javascript精度问题）
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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔）
    /// </summary>
    public string? CustomScope { get; set; }

    /// <summary>
    /// 角色状态（1=启用，0=禁用）
    /// </summary>
    public int RoleStatus { get; set; }

    /// <summary>
    /// 菜单ID列表
    /// </summary>
    public List<long>? MenuIds { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    public List<long>? UserIds { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    public List<long>? DeptIds { get; set; }
}

/// <summary>
/// Takt角色查询DTO
/// </summary>
public class TaktRoleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在角色名称、角色编码中模糊查询

    /// <summary>
    /// 角色名称
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    /// 角色编码
    /// </summary>
    public string? RoleCode { get; set; }

    /// <summary>
    /// 角色状态（1=启用，0=禁用）
    /// </summary>
    public int? RoleStatus { get; set; }
}

/// <summary>
/// Takt创建角色DTO
/// </summary>
public class TaktRoleCreateDto
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
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; } = 0;

    /// <summary>
    /// 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔）
    /// </summary>
    public string? CustomScope { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 菜单ID列表
    /// </summary>
    public List<long>? MenuIds { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    public List<long>? UserIds { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    public List<long>? DeptIds { get; set; }
}

/// <summary>
/// Takt更新角色DTO
/// </summary>
public class TaktRoleUpdateDto : TaktRoleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleUpdateDto()
    {
    }

    /// <summary>
    /// 角色ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }
}

/// <summary>
/// Takt角色状态DTO
/// </summary>
public class TaktRoleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleStatusDto()
    {
    }

    /// <summary>
    /// 角色ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 角色状态（1=启用，0=禁用）
    /// </summary>
    public int RoleStatus { get; set; }
}

/// <summary>
/// Takt角色分配菜单DTO
/// </summary>
public class TaktRoleAssignMenusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleAssignMenusDto()
    {
        MenuIds = new List<long>();
    }

    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 菜单ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> MenuIds { get; set; }
}

/// <summary>
/// Takt角色分配部门DTO
/// </summary>
public class TaktRoleAssignDeptsDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleAssignDeptsDto()
    {
        DeptIds = new List<long>();
    }

    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> DeptIds { get; set; }
}

/// <summary>
/// Takt角色导入模板DTO
/// </summary>
public class TaktRoleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleTemplateDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
        Remark = string.Empty;
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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 角色状态（1=启用，0=禁用）
    /// </summary>
    public int RoleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt角色导入DTO
/// </summary>
public class TaktRoleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleImportDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
        Remark = string.Empty;
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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 角色状态（1=启用，0=禁用）
    /// </summary>
    public int RoleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt角色导出DTO
/// </summary>
public class TaktRoleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleExportDto()
    {
        RoleName = string.Empty;
        RoleCode = string.Empty;
        DataScope = string.Empty;
        RoleStatus = 1;
        CreatedAt = DateTime.Now;
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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围
    /// </summary>
    public string DataScope { get; set; }

    /// <summary>
    /// 角色状态（1=启用，0=禁用）
    /// </summary>
    public int RoleStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}