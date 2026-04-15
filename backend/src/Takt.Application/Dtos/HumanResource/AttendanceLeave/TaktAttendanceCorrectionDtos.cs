// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：补卡管理 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 补卡管理 DTO（列表/详情）
/// </summary>
public class TaktAttendanceCorrectionDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionDto()
    {
        Reason = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 补卡记录 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CorrectionId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 补卡归属日期（日期部分有效）
    /// </summary>
    public DateTime TargetDate { get; set; }

    /// <summary>
    /// 补卡类型（1=上班 2=下班）
    /// </summary>
    public int CorrectionKind { get; set; }

    /// <summary>
    /// 申请补录的打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

    /// <summary>
    /// 申请原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 审批状态（0=草稿 1=待审 2=通过 3=驳回）
    /// </summary>
    public int ApprovalStatus { get; set; }
}

/// <summary>
/// 补卡管理分页查询 DTO。
/// 继承的 <see cref="TaktPagedQuery.KeyWords"/> 在应用服务查询表达式中用于匹配申请原因、备注。
/// </summary>
public class TaktAttendanceCorrectionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionQueryDto()
    {
    }

    /// <summary>
    /// 员工 ID（精确）
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 归属日期起（含，按日期部分比较）
    /// </summary>
    public DateTime? TargetDateFrom { get; set; }

    /// <summary>
    /// 归属日期止（含，按日期部分比较）
    /// </summary>
    public DateTime? TargetDateTo { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    public int? ApprovalStatus { get; set; }
}

/// <summary>
/// 创建补卡 DTO
/// </summary>
public class TaktAttendanceCorrectionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionCreateDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 补卡归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }

    /// <summary>
    /// 补卡类型（1=上班 2=下班）
    /// </summary>
    public int CorrectionKind { get; set; }

    /// <summary>
    /// 申请补录的打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

    /// <summary>
    /// 申请原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新补卡 DTO
/// </summary>
public class TaktAttendanceCorrectionUpdateDto : TaktAttendanceCorrectionCreateDto
{
    /// <summary>
    /// 补卡记录 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CorrectionId { get; set; }
}

/// <summary>
/// 补卡导入模板 DTO（Excel 列）
/// </summary>
public class TaktAttendanceCorrectionTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionTemplateDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 补卡归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }

    /// <summary>
    /// 补卡类型
    /// </summary>
    public int CorrectionKind { get; set; }

    /// <summary>
    /// 申请补录的打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

    /// <summary>
    /// 申请原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 补卡导入行 DTO（与模板列一致）
/// </summary>
public class TaktAttendanceCorrectionImportDto : TaktAttendanceCorrectionTemplateDto
{
}

/// <summary>
/// 补卡导出 DTO（Excel 列）
/// </summary>
public class TaktAttendanceCorrectionExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceCorrectionExportDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 补卡归属日期
    /// </summary>
    public DateTime TargetDate { get; set; }

    /// <summary>
    /// 补卡类型
    /// </summary>
    public int CorrectionKind { get; set; }

    /// <summary>
    /// 申请补录的打卡时间
    /// </summary>
    public DateTime RequestPunchTime { get; set; }

    /// <summary>
    /// 申请原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    public int ApprovalStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
