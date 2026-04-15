// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.HumanResource.Organization
// 文件名称：TaktEmployeePostDto.cs
// 功能描述：Takt员工岗位关联DTO（人事侧），用于根据员工ID获取该员工的岗位列表
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// Takt员工岗位关联DTO（人事：员工档案与岗位，用于获取员工的岗位列表）
/// </summary>
public class TaktEmployeePostDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeePostDto()
    {
        ConfigId = "0";
        EmployeeName = string.Empty;
        EmployeeCode = string.Empty;
        PostName = string.Empty;
        PostCode = string.Empty;
    }

    /// <summary>
    /// 员工岗位关联ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeePostId { get; set; }

    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 员工姓名
    /// </summary>
    public string EmployeeName { get; set; }

    /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

    /// <summary>
    /// 岗位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; }

    /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; }
}
