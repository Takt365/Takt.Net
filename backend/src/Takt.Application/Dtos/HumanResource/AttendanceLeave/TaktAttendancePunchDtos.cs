// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunchDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：打卡记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 打卡记录表Dto
/// </summary>
public partial class TaktAttendancePunchDto : TaktDtosEntityBase
{
    /// <summary>
    /// 打卡记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendancePunchId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime PunchTime { get; set; }
    /// <summary>
    /// 打卡类型
    /// </summary>
    public int PunchType { get; set; }
    /// <summary>
    /// 打卡来源
    /// </summary>
    public int PunchSource { get; set; }
    /// <summary>
    /// 打卡地点
    /// </summary>
    public string? PunchAddress { get; set; }
}

/// <summary>
/// 打卡记录表查询DTO
/// </summary>
public partial class TaktAttendancePunchQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 打卡记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendancePunchId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime? PunchTime { get; set; }

    /// <summary>
    /// 打卡时间开始时间
    /// </summary>
    public DateTime? PunchTimeStart { get; set; }
    /// <summary>
    /// 打卡时间结束时间
    /// </summary>
    public DateTime? PunchTimeEnd { get; set; }
    /// <summary>
    /// 打卡类型
    /// </summary>
    public int? PunchType { get; set; }
    /// <summary>
    /// 打卡来源
    /// </summary>
    public int? PunchSource { get; set; }
    /// <summary>
    /// 打卡地点
    /// </summary>
    public string? PunchAddress { get; set; }

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
/// Takt创建打卡记录表DTO
/// </summary>
public partial class TaktAttendancePunchCreateDto
{
        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime PunchTime { get; set; }

        /// <summary>
    /// 打卡类型
    /// </summary>
    public int PunchType { get; set; }

        /// <summary>
    /// 打卡来源
    /// </summary>
    public int PunchSource { get; set; }

        /// <summary>
    /// 打卡地点
    /// </summary>
    public string? PunchAddress { get; set; }

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
/// Takt更新打卡记录表DTO
/// </summary>
public partial class TaktAttendancePunchUpdateDto : TaktAttendancePunchCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchUpdateDto()
    {
    }

        /// <summary>
    /// 打卡记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendancePunchId { get; set; }
}

/// <summary>
/// 打卡记录表导入模板DTO
/// </summary>
public partial class TaktAttendancePunchTemplateDto
{
        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime PunchTime { get; set; }

        /// <summary>
    /// 打卡类型
    /// </summary>
    public int PunchType { get; set; }

        /// <summary>
    /// 打卡来源
    /// </summary>
    public int PunchSource { get; set; }

        /// <summary>
    /// 打卡地点
    /// </summary>
    public string? PunchAddress { get; set; }

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
/// 打卡记录表导入DTO
/// </summary>
public partial class TaktAttendancePunchImportDto
{
        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime PunchTime { get; set; }

        /// <summary>
    /// 打卡类型
    /// </summary>
    public int PunchType { get; set; }

        /// <summary>
    /// 打卡来源
    /// </summary>
    public int PunchSource { get; set; }

        /// <summary>
    /// 打卡地点
    /// </summary>
    public string? PunchAddress { get; set; }

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
/// 打卡记录表导出DTO
/// </summary>
public partial class TaktAttendancePunchExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime PunchTime { get; set; }

        /// <summary>
    /// 打卡类型
    /// </summary>
    public int PunchType { get; set; }

        /// <summary>
    /// 打卡来源
    /// </summary>
    public int PunchSource { get; set; }

        /// <summary>
    /// 打卡地点
    /// </summary>
    public string? PunchAddress { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}