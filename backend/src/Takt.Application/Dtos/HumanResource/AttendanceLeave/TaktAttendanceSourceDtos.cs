// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourceDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录 DTO（设备下载原始行），包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤源记录 DTO（列表/详情；设备原始打卡行）
/// </summary>
public class TaktAttendanceSourceDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceDto()
    {
        EnrollNumber = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 源记录 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SourceId { get; set; }

    /// <summary>
    /// 设备 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeviceId { get; set; }

    /// <summary>
    /// 设备编码（列表展示用，非表字段，服务层按 DeviceId 填充）
    /// </summary>
    public string? DeviceCode { get; set; }

    /// <summary>
    /// 员工 ID（已解析时填写；未解析可为 0）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号（机内用户号）
    /// </summary>
    public string EnrollNumber { get; set; }

    /// <summary>
    /// 设备原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

    /// <summary>
    /// 验证方式（0=未知 1=指纹 2=人脸 3=密码 4=卡）
    /// </summary>
    public int VerifyMode { get; set; }

    /// <summary>
    /// 设备侧记录唯一键/指纹（用于去重，可为空）
    /// </summary>
    public string? ExternalRecordKey { get; set; }

    /// <summary>
    /// 下载批次号
    /// </summary>
    public string? DownloadBatchNo { get; set; }

    /// <summary>
    /// 原始报文 JSON（可选）
    /// </summary>
    public string? RawPayloadJson { get; set; }
}

/// <summary>
/// 考勤源记录分页查询 DTO
/// </summary>
public class TaktAttendanceSourceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceQueryDto()
    {
    }

    /// <summary>
    /// 设备 ID（精确）
    /// </summary>
    public long? DeviceId { get; set; }

    /// <summary>
    /// 员工 ID（精确）
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 原始打卡时间起（含）
    /// </summary>
    public DateTime? RawPunchTimeFrom { get; set; }

    /// <summary>
    /// 原始打卡时间止（含）
    /// </summary>
    public DateTime? RawPunchTimeTo { get; set; }
}

/// <summary>
/// 创建考勤源记录 DTO
/// </summary>
public class TaktAttendanceSourceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceCreateDto()
    {
        EnrollNumber = string.Empty;
    }

    /// <summary>
    /// 设备 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeviceId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号（机内用户号）
    /// </summary>
    public string EnrollNumber { get; set; }

    /// <summary>
    /// 设备原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

    /// <summary>
    /// 验证方式（0=未知 1=指纹 2=人脸 3=密码 4=卡）
    /// </summary>
    public int VerifyMode { get; set; }

    /// <summary>
    /// 设备侧记录唯一键/指纹（可选）
    /// </summary>
    public string? ExternalRecordKey { get; set; }

    /// <summary>
    /// 下载批次号（可选）
    /// </summary>
    public string? DownloadBatchNo { get; set; }

    /// <summary>
    /// 原始报文 JSON（可选）
    /// </summary>
    public string? RawPayloadJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新考勤源记录 DTO
/// </summary>
public class TaktAttendanceSourceUpdateDto : TaktAttendanceSourceCreateDto
{
    /// <summary>
    /// 源记录 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SourceId { get; set; }
}

/// <summary>
/// 考勤源记录导入模板 DTO（Excel 列；设备以编码解析为设备 ID）
/// </summary>
public class TaktAttendanceSourceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceTemplateDto()
    {
        DeviceCode = string.Empty;
        EnrollNumber = string.Empty;
    }

    /// <summary>
    /// 设备编码（与考勤设备表 device_code 对应）
    /// </summary>
    public string DeviceCode { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号
    /// </summary>
    public string EnrollNumber { get; set; }

    /// <summary>
    /// 设备原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

    /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

    /// <summary>
    /// 外部记录键（可选）
    /// </summary>
    public string? ExternalRecordKey { get; set; }

    /// <summary>
    /// 下载批次号（可选）
    /// </summary>
    public string? DownloadBatchNo { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤源记录导入行 DTO（与模板列一致）
/// </summary>
public class TaktAttendanceSourceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceImportDto()
    {
        DeviceCode = string.Empty;
        EnrollNumber = string.Empty;
    }

    /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号
    /// </summary>
    public string EnrollNumber { get; set; }

    /// <summary>
    /// 设备原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

    /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

    /// <summary>
    /// 外部记录键（可选）
    /// </summary>
    public string? ExternalRecordKey { get; set; }

    /// <summary>
    /// 下载批次号（可选）
    /// </summary>
    public string? DownloadBatchNo { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤源记录导出 DTO（Excel 列）
/// </summary>
public class TaktAttendanceSourceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceExportDto()
    {
        DeviceCode = string.Empty;
        EnrollNumber = string.Empty;
    }

    /// <summary>
    /// 设备编码（导出展示）
    /// </summary>
    public string DeviceCode { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号
    /// </summary>
    public string EnrollNumber { get; set; }

    /// <summary>
    /// 设备原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

    /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

    /// <summary>
    /// 外部记录键（可选）
    /// </summary>
    public string? ExternalRecordKey { get; set; }

    /// <summary>
    /// 下载批次号（可选）
    /// </summary>
    public string? DownloadBatchNo { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
