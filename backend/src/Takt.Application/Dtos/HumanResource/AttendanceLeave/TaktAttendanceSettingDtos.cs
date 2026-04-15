// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSettingDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设置 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktHolidayDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设置 DTO（列表/详情）
/// </summary>
public class TaktAttendanceSettingDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingDto()
    {
        SettingCode = string.Empty;
        SettingName = string.Empty;
        WorkStartTime = "09:00";
        WorkEndTime = "18:00";
        ConfigId = "0";
    }

    /// <summary>
    /// 考勤设置ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SettingId { get; set; }

    /// <summary>
    /// 方案编码
    /// </summary>
    public string SettingCode { get; set; }

    /// <summary>
    /// 方案名称
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 标准上班时间（HH:mm）
    /// </summary>
    public string WorkStartTime { get; set; }

    /// <summary>
    /// 标准下班时间（HH:mm）
    /// </summary>
    public string WorkEndTime { get; set; }

    /// <summary>
    /// 迟到宽限（分钟）
    /// </summary>
    public int LateGraceMinutes { get; set; }

    /// <summary>
    /// 早退宽限（分钟）
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

    /// <summary>
    /// 是否默认方案（0=否 1=是）
    /// </summary>
    public int IsDefault { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 考勤设置查询 DTO
/// </summary>
public class TaktAttendanceSettingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingQueryDto()
    {
    }

    /// <summary>
    /// 方案编码（模糊）
    /// </summary>
    public string? SettingCode { get; set; }

    /// <summary>
    /// 方案名称（模糊）
    /// </summary>
    public string? SettingName { get; set; }
}

/// <summary>
/// 创建考勤设置 DTO
/// </summary>
public class TaktAttendanceSettingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingCreateDto()
    {
        SettingCode = string.Empty;
        SettingName = string.Empty;
        WorkStartTime = "09:00";
        WorkEndTime = "18:00";
    }

    /// <summary>
    /// 方案编码
    /// </summary>
    public string SettingCode { get; set; }

    /// <summary>
    /// 方案名称
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 标准上班时间（HH:mm）
    /// </summary>
    public string WorkStartTime { get; set; }

    /// <summary>
    /// 标准下班时间（HH:mm）
    /// </summary>
    public string WorkEndTime { get; set; }

    /// <summary>
    /// 迟到宽限（分钟）
    /// </summary>
    public int LateGraceMinutes { get; set; }

    /// <summary>
    /// 早退宽限（分钟）
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

    /// <summary>
    /// 是否默认方案（0=否 1=是）
    /// </summary>
    public int IsDefault { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新考勤设置 DTO
/// </summary>
public class TaktAttendanceSettingUpdateDto : TaktAttendanceSettingCreateDto
{
    /// <summary>
    /// 考勤设置ID（适配字段）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SettingId { get; set; }
}

/// <summary>
/// 考勤设置导入模板 DTO（Excel 列）
/// </summary>
public class TaktAttendanceSettingTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingTemplateDto()
    {
        SettingCode = string.Empty;
        SettingName = string.Empty;
        WorkStartTime = "09:00";
        WorkEndTime = "18:00";
        Remark = string.Empty;
    }

    /// <summary>
    /// 方案编码
    /// </summary>
    public string SettingCode { get; set; }

    /// <summary>
    /// 方案名称
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 标准上班时间（HH:mm）
    /// </summary>
    public string WorkStartTime { get; set; }

    /// <summary>
    /// 标准下班时间（HH:mm）
    /// </summary>
    public string WorkEndTime { get; set; }

    /// <summary>
    /// 迟到宽限（分钟）
    /// </summary>
    public int LateGraceMinutes { get; set; }

    /// <summary>
    /// 早退宽限（分钟）
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

    /// <summary>
    /// 是否默认（0=否 1=是）
    /// </summary>
    public int IsDefault { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤设置导入行 DTO
/// </summary>
public class TaktAttendanceSettingImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingImportDto()
    {
        SettingCode = string.Empty;
        SettingName = string.Empty;
        WorkStartTime = "09:00";
        WorkEndTime = "18:00";
        Remark = string.Empty;
    }

    /// <summary>
    /// 方案编码
    /// </summary>
    public string SettingCode { get; set; }

    /// <summary>
    /// 方案名称
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 标准上班时间（HH:mm）
    /// </summary>
    public string WorkStartTime { get; set; }

    /// <summary>
    /// 标准下班时间（HH:mm）
    /// </summary>
    public string WorkEndTime { get; set; }

    /// <summary>
    /// 迟到宽限（分钟）
    /// </summary>
    public int LateGraceMinutes { get; set; }

    /// <summary>
    /// 早退宽限（分钟）
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

    /// <summary>
    /// 是否默认（0=否 1=是）
    /// </summary>
    public int IsDefault { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤设置导出 DTO
/// </summary>
public class TaktAttendanceSettingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingExportDto()
    {
        SettingCode = string.Empty;
        SettingName = string.Empty;
        WorkStartTime = string.Empty;
        WorkEndTime = string.Empty;
    }

    /// <summary>
    /// 方案编码
    /// </summary>
    public string SettingCode { get; set; }

    /// <summary>
    /// 方案名称
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 标准上班时间
    /// </summary>
    public string WorkStartTime { get; set; }

    /// <summary>
    /// 标准下班时间
    /// </summary>
    public string WorkEndTime { get; set; }

    /// <summary>
    /// 迟到宽限（分钟）
    /// </summary>
    public int LateGraceMinutes { get; set; }

    /// <summary>
    /// 早退宽限（分钟）
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

    /// <summary>
    /// 是否默认
    /// </summary>
    public int IsDefault { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
