// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeTransferDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工调动表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工调动表Dto
/// </summary>
public partial class TaktEmployeeTransferDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferDto()
    {
        FromDeptName = string.Empty;
        ToDeptName = string.Empty;
    }

    /// <summary>
    /// 员工调动表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeTransferId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 调动类型
    /// </summary>
    public int TransferType { get; set; }
    /// <summary>
    /// 原部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FromDeptId { get; set; }
    /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }
    /// <summary>
    /// 原岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromPostId { get; set; }
    /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }
    /// <summary>
    /// 目标部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ToDeptId { get; set; }
    /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }
    /// <summary>
    /// 目标岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 调动状态
    /// </summary>
    public int TransferStatus { get; set; }
}

/// <summary>
/// 员工调动表查询DTO
/// </summary>
public partial class TaktEmployeeTransferQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工调动表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeTransferId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 调动类型
    /// </summary>
    public int? TransferType { get; set; }
    /// <summary>
    /// 原部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromDeptId { get; set; }
    /// <summary>
    /// 原部门名称
    /// </summary>
    public string? FromDeptName { get; set; }
    /// <summary>
    /// 原岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromPostId { get; set; }
    /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }
    /// <summary>
    /// 目标部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ToDeptId { get; set; }
    /// <summary>
    /// 目标部门名称
    /// </summary>
    public string? ToDeptName { get; set; }
    /// <summary>
    /// 目标岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 生效日期开始时间
    /// </summary>
    public DateTime? EffectiveDateStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? EffectiveDateEnd { get; set; }
    /// <summary>
    /// 申请事由
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 调动状态
    /// </summary>
    public int? TransferStatus { get; set; }

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
/// Takt创建员工调动表DTO
/// </summary>
public partial class TaktEmployeeTransferCreateDto
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调动类型
    /// </summary>
    public int TransferType { get; set; }

        /// <summary>
    /// 原部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FromDeptId { get; set; }

        /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }

        /// <summary>
    /// 原岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromPostId { get; set; }

        /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }

        /// <summary>
    /// 目标部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ToDeptId { get; set; }

        /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }

        /// <summary>
    /// 目标岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 调动状态
    /// </summary>
    public int TransferStatus { get; set; }

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
/// Takt更新员工调动表DTO
/// </summary>
public partial class TaktEmployeeTransferUpdateDto : TaktEmployeeTransferCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferUpdateDto()
    {
    }

        /// <summary>
    /// 员工调动表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeTransferId { get; set; }
}

/// <summary>
/// 员工调动表调动状态DTO
/// </summary>
public partial class TaktEmployeeTransferStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferStatusDto()
    {
    }

        /// <summary>
    /// 员工调动表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeTransferId { get; set; }

    /// <summary>
    /// 调动状态（0=禁用，1=启用）
    /// </summary>
    public int TransferStatus { get; set; }
}

/// <summary>
/// 员工调动表导入模板DTO
/// </summary>
public partial class TaktEmployeeTransferTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferTemplateDto()
    {
        FromDeptName = string.Empty;
        ToDeptName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调动类型
    /// </summary>
    public int TransferType { get; set; }

        /// <summary>
    /// 原部门ID
    /// </summary>
    public long FromDeptId { get; set; }

        /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }

        /// <summary>
    /// 原岗位ID
    /// </summary>
    public long? FromPostId { get; set; }

        /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }

        /// <summary>
    /// 目标部门ID
    /// </summary>
    public long ToDeptId { get; set; }

        /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }

        /// <summary>
    /// 目标岗位ID
    /// </summary>
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
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 调动状态
    /// </summary>
    public int TransferStatus { get; set; }

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
/// 员工调动表导入DTO
/// </summary>
public partial class TaktEmployeeTransferImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferImportDto()
    {
        FromDeptName = string.Empty;
        ToDeptName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调动类型
    /// </summary>
    public int TransferType { get; set; }

        /// <summary>
    /// 原部门ID
    /// </summary>
    public long FromDeptId { get; set; }

        /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }

        /// <summary>
    /// 原岗位ID
    /// </summary>
    public long? FromPostId { get; set; }

        /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }

        /// <summary>
    /// 目标部门ID
    /// </summary>
    public long ToDeptId { get; set; }

        /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }

        /// <summary>
    /// 目标岗位ID
    /// </summary>
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
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 调动状态
    /// </summary>
    public int TransferStatus { get; set; }

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
/// 员工调动表导出DTO
/// </summary>
public partial class TaktEmployeeTransferExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTransferExportDto()
    {
        CreatedAt = DateTime.Now;
        FromDeptName = string.Empty;
        ToDeptName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调动类型
    /// </summary>
    public int TransferType { get; set; }

        /// <summary>
    /// 原部门ID
    /// </summary>
    public long FromDeptId { get; set; }

        /// <summary>
    /// 原部门名称
    /// </summary>
    public string FromDeptName { get; set; }

        /// <summary>
    /// 原岗位ID
    /// </summary>
    public long? FromPostId { get; set; }

        /// <summary>
    /// 原岗位名称
    /// </summary>
    public string? FromPostName { get; set; }

        /// <summary>
    /// 目标部门ID
    /// </summary>
    public long ToDeptId { get; set; }

        /// <summary>
    /// 目标部门名称
    /// </summary>
    public string ToDeptName { get; set; }

        /// <summary>
    /// 目标岗位ID
    /// </summary>
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
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 调动状态
    /// </summary>
    public int TransferStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}