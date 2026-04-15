// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Organization
// 文件名称：TaktUserDeptDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户部门关联DTO，定义用户与部门的关联关系数据传输对象（用于获取部门用户列表）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// Takt用户部门关联DTO（用于获取部门用户列表，即根据部门ID获取该部门下的用户列表）
/// </summary>
public class TaktUserDeptDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserDeptDto()
    {
        ConfigId = "0";
        DeptName = string.Empty;
        DeptCode = string.Empty;
        UserName = string.Empty;
        RealName = string.Empty;
    }

    /// <summary>
    /// 用户部门关联ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserDeptId { get; set; }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户真实姓名
    /// </summary>
    public string RealName { get; set; }

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
