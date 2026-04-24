// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：补卡记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 补卡记录表Dto
/// </summary>
public partial class TaktAttendanceCorrectionDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 补卡记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceCorrectionId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }
    /// <summary>
    /// 补卡类型
    /// </summary>
    public int CorrectionKind { get; set; }
    /// <summary>
    /// 申请打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }
    /// <summary>
    /// 原因
    /// </summary>
    public string Reason { get; set; }
    /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }
}

/// <summary>
/// 补卡记录表查询DTO
/// </summary>
public partial class TaktAttendanceCorrectionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 补卡记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceCorrectionId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 归属日期
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// 归属日期开始时间
    /// </summary>
    public DateTime? TargetDateStart { get; set; }
    /// <summary>
    /// 归属日期结束时间
    /// </summary>
    public DateTime? TargetDateEnd { get; set; }
    /// <summary>
    /// 补卡类型
    /// </summary>
    public int? CorrectionKind { get; set; }
    /// <summary>
    /// 申请打卡时间
    /// </summary>
    public DateTime? RequestPunchTime { get; set; }

    /// <summary>
    /// 申请打卡时间开始时间
    /// </summary>
    public DateTime? RequestPunchTimeStart { get; set; }
    /// <summary>
    /// 申请打卡时间结束时间
    /// </summary>
    public DateTime? RequestPunchTimeEnd { get; set; }
    /// <summary>
    /// 原因
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 审批状态
    /// </summary>
    public int? ApprovalStatus { get; set; }

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
/// Takt创建补卡记录表DTO
/// </summary>
public partial class TaktAttendanceCorrectionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionCreateDto()
    {
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }

        /// <summary>
    /// 补卡类型
    /// </summary>
    public int CorrectionKind { get; set; }

        /// <summary>
    /// 申请打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

        /// <summary>
    /// 原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }

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
/// Takt更新补卡记录表DTO
/// </summary>
public partial class TaktAttendanceCorrectionUpdateDto : TaktAttendanceCorrectionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionUpdateDto()
    {
    }

        /// <summary>
    /// 补卡记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceCorrectionId { get; set; }
}

/// <summary>
/// 补卡记录表审批状态DTO
/// </summary>
public partial class TaktAttendanceCorrectionApprovalStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionApprovalStatusDto()
    {
    }

        /// <summary>
    /// 补卡记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceCorrectionId { get; set; }

    /// <summary>
    /// 审批状态（0=禁用，1=启用）
    /// </summary>
    public int ApprovalStatus { get; set; }
}

/// <summary>
/// 补卡记录表导入模板DTO
/// </summary>
public partial class TaktAttendanceCorrectionTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionTemplateDto()
    {
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }

        /// <summary>
    /// 补卡类型
    /// </summary>
    public int CorrectionKind { get; set; }

        /// <summary>
    /// 申请打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

        /// <summary>
    /// 原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }

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
/// 补卡记录表导入DTO
/// </summary>
public partial class TaktAttendanceCorrectionImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionImportDto()
    {
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }

        /// <summary>
    /// 补卡类型
    /// </summary>
    public int CorrectionKind { get; set; }

        /// <summary>
    /// 申请打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

        /// <summary>
    /// 原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }

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
/// 补卡记录表导出DTO
/// </summary>
public partial class TaktAttendanceCorrectionExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionExportDto()
    {
        CreatedAt = DateTime.Now;
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }

        /// <summary>
    /// 补卡类型
    /// </summary>
    public int CorrectionKind { get; set; }

        /// <summary>
    /// 申请打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

        /// <summary>
    /// 原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}