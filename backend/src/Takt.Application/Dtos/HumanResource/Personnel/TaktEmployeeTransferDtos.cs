// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeTransferDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工调动DTO（转岗/调岗），包含员工调动相关的数据传输对象（查询、创建、更新、状态、导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工调动DTO（转岗/调岗）
/// </summary>
public class TaktEmployeeTransferDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferDto()
    {
        FromDeptName = string.Empty;
        ToDeptName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 调动ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TransferId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 调动类型（0=转岗 1=调岗）
    /// </summary>
    public int TransferType { get; set; }

    /// <summary>
    /// 原部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FromDeptId { get; set; }

    /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }

    /// <summary>
    /// 原岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FromPostId { get; set; }

    /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }

    /// <summary>
    /// 目标部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ToDeptId { get; set; }

    /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }

    /// <summary>
    /// 目标岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ToPostId { get; set; }

    /// <summary>
    /// 目标岗位名称
    /// </summary>
    public string? ToPostName { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 申请事由
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 调动状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    public int TransferStatus { get; set; }
}

/// <summary>
/// Takt员工调动查询DTO
/// </summary>
public class TaktEmployeeTransferQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferQueryDto()
    {
    }

    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 调动类型（0=转岗 1=调岗；null 表示全部）
    /// </summary>
    public int? TransferType { get; set; }

    /// <summary>
    /// 调动状态（0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回；null 表示全部）
    /// </summary>
    public int? TransferStatus { get; set; }

    /// <summary>
    /// 生效日期起（闭区间）
    /// </summary>
    public DateTime? EffectiveDateFrom { get; set; }

    /// <summary>
    /// 生效日期止（闭区间）
    /// </summary>
    public DateTime? EffectiveDateTo { get; set; }
}

/// <summary>
/// Takt创建员工调动DTO
/// </summary>
public class TaktEmployeeTransferCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferCreateDto()
    {
        FromDeptName = string.Empty;
        ToDeptName = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 调动类型（0=转岗 1=调岗）
    /// </summary>
    public int TransferType { get; set; }

    /// <summary>
    /// 原部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FromDeptId { get; set; }

    /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }

    /// <summary>
    /// 原岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FromPostId { get; set; }

    /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }

    /// <summary>
    /// 目标部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ToDeptId { get; set; }

    /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }

    /// <summary>
    /// 目标岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ToPostId { get; set; }

    /// <summary>
    /// 目标岗位名称
    /// </summary>
    public string? ToPostName { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 申请事由
    /// </summary>
    public string? Reason { get; set; }
}

/// <summary>
/// Takt更新员工调动DTO
/// </summary>
public class TaktEmployeeTransferUpdateDto : TaktEmployeeTransferCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferUpdateDto()
    {
    }

    /// <summary>
    /// 调动ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TransferId { get; set; }
}

/// <summary>
/// Takt员工调动状态DTO（流程回调更新用）
/// </summary>
public class TaktEmployeeTransferStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferStatusDto()
    {
    }

    /// <summary>
    /// 调动ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TransferId { get; set; }

    /// <summary>
    /// 调动状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    public int TransferStatus { get; set; }
}

/// <summary>
/// Takt员工调动导出DTO
/// </summary>
public class TaktEmployeeTransferExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferExportDto()
    {
        FromDeptName = string.Empty;
        ToDeptName = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 调动ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TransferId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 调动类型（0=转岗 1=调岗）
    /// </summary>
    public int TransferType { get; set; }

    /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }

    /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }

    /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }

    /// <summary>
    /// 目标岗位名称
    /// </summary>
    public string? ToPostName { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 调动状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    public int TransferStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
