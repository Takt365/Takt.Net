// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSettingDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：考勤设置表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设置表Dto
/// </summary>
public partial class TaktAttendanceSettingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingDto()
    {
        SettingCode = string.Empty;
        SettingName = string.Empty;
        WorkStartTime = string.Empty;
        WorkEndTime = string.Empty;
    }

    /// <summary>
    /// 考勤设置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceSettingId { get; set; }

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
    /// 迟到宽限分钟
    /// </summary>
    public int LateGraceMinutes { get; set; }
    /// <summary>
    /// 早退宽限分钟
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }
    /// <summary>
    /// 是否默认
    /// </summary>
    public int IsDefault { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 考勤设置表查询DTO
/// </summary>
public partial class TaktAttendanceSettingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 考勤设置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceSettingId { get; set; }

    /// <summary>
    /// 方案编码
    /// </summary>
    public string? SettingCode { get; set; }
    /// <summary>
    /// 方案名称
    /// </summary>
    public string? SettingName { get; set; }
    /// <summary>
    /// 标准上班时间
    /// </summary>
    public string? WorkStartTime { get; set; }
    /// <summary>
    /// 标准下班时间
    /// </summary>
    public string? WorkEndTime { get; set; }
    /// <summary>
    /// 迟到宽限分钟
    /// </summary>
    public int? LateGraceMinutes { get; set; }
    /// <summary>
    /// 早退宽限分钟
    /// </summary>
    public int? EarlyLeaveGraceMinutes { get; set; }
    /// <summary>
    /// 是否默认
    /// </summary>
    public int? IsDefault { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建考勤设置表DTO
/// </summary>
public partial class TaktAttendanceSettingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingCreateDto()
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
    /// 迟到宽限分钟
    /// </summary>
    public int LateGraceMinutes { get; set; }

        /// <summary>
    /// 早退宽限分钟
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

        /// <summary>
    /// 是否默认
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新考勤设置表DTO
/// </summary>
public partial class TaktAttendanceSettingUpdateDto : TaktAttendanceSettingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingUpdateDto()
    {
    }

        /// <summary>
    /// 考勤设置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceSettingId { get; set; }
}

/// <summary>
/// 考勤设置表导入模板DTO
/// </summary>
public partial class TaktAttendanceSettingTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingTemplateDto()
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
    /// 迟到宽限分钟
    /// </summary>
    public int LateGraceMinutes { get; set; }

        /// <summary>
    /// 早退宽限分钟
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

        /// <summary>
    /// 是否默认
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 考勤设置表导入DTO
/// </summary>
public partial class TaktAttendanceSettingImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingImportDto()
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
    /// 迟到宽限分钟
    /// </summary>
    public int LateGraceMinutes { get; set; }

        /// <summary>
    /// 早退宽限分钟
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

        /// <summary>
    /// 是否默认
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 考勤设置表导出DTO
/// </summary>
public partial class TaktAttendanceSettingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingExportDto()
    {
        CreatedAt = DateTime.Now;
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
    /// 迟到宽限分钟
    /// </summary>
    public int LateGraceMinutes { get; set; }

        /// <summary>
    /// 早退宽限分钟
    /// </summary>
    public int EarlyLeaveGraceMinutes { get; set; }

        /// <summary>
    /// 是否默认
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}