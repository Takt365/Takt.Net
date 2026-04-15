// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Organization
// 文件名称：TaktRoleDeptDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色部门关联DTO，定义角色与部门的关联关系数据传输对象（用于获取角色部门列表）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// Takt角色部门关联DTO（用于获取角色部门列表，即根据角色ID获取该角色的部门列表）
/// </summary>
public class TaktRoleDeptDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleDeptDto()
    {
        ConfigId = "0";
        RoleName = string.Empty;
        RoleCode = string.Empty;
        DeptName = string.Empty;
        DeptCode = string.Empty;
    }

    /// <summary>
    /// 角色部门关联ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RoleDeptId { get; set; }

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
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }
}
