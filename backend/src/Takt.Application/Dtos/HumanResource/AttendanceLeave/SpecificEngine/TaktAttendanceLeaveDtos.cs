// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceLeaveDtos.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：考勤排班、考勤原始记录DTO业务扩展字段（partial class扩展）+ 特殊业务独立DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

#region TaktShiftSchedule DTO 扩展

/// <summary>
/// Takt排班导入DTO扩展
/// </summary>
public partial class TaktShiftScheduleImportDto
{
    /// <summary>
    /// 班次编码（用于导入时查找班次）
    /// </summary>
    public string? ShiftCode { get; set; }
}

/// <summary>
/// Takt排班导出DTO扩展
/// </summary>
public partial class TaktShiftScheduleExportDto
{
    /// <summary>
    /// 班次名称（业务展示字段）
    /// </summary>
    public string? ShiftName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt排班DTO扩展
/// </summary>
public partial class TaktShiftScheduleDto
{
    /// <summary>
    /// 班次名称（业务展示字段）
    /// </summary>
    public string? ShiftName { get; set; }
}

#endregion

#region TaktAttendanceSource DTO 扩展

/// <summary>
/// Takt考勤原始记录导入DTO扩展（用于包含设备编码等业务字段）
/// </summary>
public partial class TaktAttendanceSourceImportDto
{
    /// <summary>
    /// 设备编码（用于导入时查找设备）
    /// </summary>
    public string? DeviceCode { get; set; }
}

/// <summary>
/// Takt考勤原始记录导出DTO扩展（用于包含设备编码等业务字段）
/// </summary>
public partial class TaktAttendanceSourceExportDto
{
    /// <summary>
    /// 设备编码（业务展示字段）
    /// </summary>
    public string? DeviceCode { get; set; }
}

/// <summary>
/// Takt考勤原始记录DTO扩展（用于包含设备编码等业务字段）
/// </summary>
public partial class TaktAttendanceSourceDto
{
    /// <summary>
    /// 设备编码（业务展示字段）
    /// </summary>
    public string? DeviceCode { get; set; }
}

#endregion

#region 特殊业务独立 DTO（非 partial class 扩展）

/// <summary>
/// Takt请假提交结果 DTO（独立类，非扩展）
/// </summary>
public class TaktLeaveSubmitResultDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveSubmitResultDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = "Leave";
        SchemeName = string.Empty;
    }

    /// <summary>
    /// 请假主键
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 流程实例ID（与 TaktLeave.FlowInstanceId 一致）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; }


    /// <summary>
    /// 方案Key（Leave）
    /// </summary>
    public string SchemeKey { get; set; } = "Leave";

    /// <summary>
    /// 方案名称
    /// </summary>
    public string SchemeName { get; set; } = string.Empty;
}

/// <summary>
/// 拉取请求 DTO（独立类，非扩展）
/// </summary>
public class TaktAttendancePullRequestDto
{
    /// <summary>
    /// 设备ID（AttendanceDevice.Id）
    /// </summary>
    public long DeviceId { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime EndTime { get; set; }
}

/// <summary>
/// 推送请求 DTO（统一入口，独立类，非扩展）
/// </summary>
public class TaktAttendancePushRequestDto
{
    /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; } = string.Empty;

    /// <summary>
    /// 设备类型（如 DingTalk、Face、Fingerprint）
    /// </summary>
    public string DeviceType { get; set; } = string.Empty;

    /// <summary>
    /// 原始请求体（JSON）
    /// </summary>
    public string RawPayloadJson { get; set; } = string.Empty;

    /// <summary>
    /// 签名（预留）
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// 时间戳（Unix秒，可选）
    /// </summary>
    public long? Timestamp { get; set; }
}

/// <summary>
/// 标准化源记录行 DTO（设备适配器输出，独立类，非扩展）
/// </summary>
public class TaktAttendanceSourceIngestRowDto
{
    /// <summary>
    /// 员工ID（可选，未识别时为0）
    /// </summary>
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号
    /// </summary>
    public string EnrollNumber { get; set; } = string.Empty;

    /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

    /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

    /// <summary>
    /// 外部记录键（用于去重）
    /// </summary>
    public string? ExternalRecordKey { get; set; }

    /// <summary>
    /// 原始JSON
    /// </summary>
    public string? RawPayloadJson { get; set; }
}

/// <summary>
/// 设备在线状态 DTO（独立类，非扩展）
/// </summary>
public class TaktAttendanceDeviceOnlineStatusDto
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public long DeviceId { get; set; }

    /// <summary>
    /// 是否在线
    /// </summary>
    public bool IsOnline { get; set; }

    /// <summary>
    /// 状态说明
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// 拉取结果 DTO（独立类，非扩展）
/// </summary>
public class TaktAttendancePullResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 成功入库数量
    /// </summary>
    public int AcceptedCount { get; set; }

    /// <summary>
    /// 错误列表
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// 拉取成功后需写回设备 ConfigJson 的全文（可选；例如得力云综合签到的 next_id 游标）。
    /// </summary>
    public string? UpdatedDeviceConfigJson { get; set; }
}

/// <summary>
/// 推送处理结果 DTO（独立类，非扩展）
/// </summary>
public class TaktAttendancePushHandleResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 成功入库数量
    /// </summary>
    public int AcceptedCount { get; set; }

    /// <summary>
    /// 错误列表
    /// </summary>
    public List<string> Errors { get; set; } = new();
}

/// <summary>
/// 设备用户同步项 DTO（独立类，非扩展）
/// </summary>
public class TaktAttendanceDeviceUserSyncItemDto
{
    /// <summary>
    /// 关联员工 ID（可选）
    /// </summary>
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号（海康 employeeNo）
    /// </summary>
    public string EnrollNumber { get; set; } = string.Empty;

    /// <summary>
    /// 用户姓名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 卡号（可选）
    /// </summary>
    public string? CardNo { get; set; }

    /// <summary>
    /// 身份证号（可选）
    /// </summary>
    public string? CertificateNo { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 手机号（得力云员工 API 要求中国大陆 11 位时可在此传入；未传时由同步逻辑生成占位号，详见得力对接文档）。
    /// </summary>
    public string? Mobile { get; set; }
}

/// <summary>
/// 设备用户同步请求 DTO（独立类，非扩展）
/// </summary>
public class TaktAttendanceUserSyncRequestDto
{
    /// <summary>
    /// 待同步用户集合
    /// </summary>
    public List<TaktAttendanceDeviceUserSyncItemDto> Users { get; set; } = new();
}

/// <summary>
/// 设备用户同步结果 DTO（独立类，非扩展）
/// </summary>
public class TaktAttendanceUserSyncResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 成功同步数量
    /// </summary>
    public int SyncedCount { get; set; }

    /// <summary>
    /// 错误列表
    /// </summary>
    public List<string> Errors { get; set; } = new();
}

/// <summary>
/// Takt请假提交入参（新增请假并发起流程，独立类，非扩展）
/// </summary>
public class TaktLeaveSubmitDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveSubmitDto()
    {
        LeaveType = string.Empty;
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（affair/sick/annual 等）
    /// </summary>
    public string LeaveType { get; set; } = string.Empty;

    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 请假事由
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// 证明附件 JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

    /// <summary>
    /// 流程标题（可选，默认自动生成）
    /// </summary>
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 流程表单数据 JSON（可选）
    /// </summary>
    public string? FrmData { get; set; }
}

#endregion
