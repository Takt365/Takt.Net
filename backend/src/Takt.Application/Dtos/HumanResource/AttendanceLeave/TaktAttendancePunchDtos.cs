// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunchDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：打卡记录 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 打卡记录 DTO（列表/详情）
/// </summary>
public class TaktAttendancePunchDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchDto()
    {
        ConfigId = "0";
    }

    /// <summary>
    /// 打卡记录 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PunchId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime PunchTime { get; set; }

    /// <summary>
    /// 打卡类型（1=上班 2=下班 3=外勤，与实体注释一致）
    /// </summary>
    public int PunchType { get; set; }

    /// <summary>
    /// 打卡来源（0=后台录入 1=移动端 2=导入）
    /// </summary>
    public int PunchSource { get; set; }

    /// <summary>
    /// 打卡地点或说明
    /// </summary>
    public string? PunchAddress { get; set; }
}

/// <summary>
/// 打卡记录分页查询 DTO
/// </summary>
public class TaktAttendancePunchQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchQueryDto()
    {
    }

    /// <summary>
    /// 员工 ID（精确）
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 打卡类型
    /// </summary>
    public int? PunchType { get; set; }

    /// <summary>
    /// 打卡时间起（含）
    /// </summary>
    public DateTime? PunchTimeFrom { get; set; }

    /// <summary>
    /// 打卡时间止（含）
    /// </summary>
    public DateTime? PunchTimeTo { get; set; }
}

/// <summary>
/// 创建打卡记录 DTO
/// </summary>
public class TaktAttendancePunchCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchCreateDto()
    {
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 打卡时间
    /// </summary>
    public DateTime PunchTime { get; set; }

    /// <summary>
    /// 打卡类型（默认 1=上班）
    /// </summary>
    public int PunchType { get; set; } = 1;

    /// <summary>
    /// 打卡来源
    /// </summary>
    public int PunchSource { get; set; }

    /// <summary>
    /// 打卡地点或说明
    /// </summary>
    public string? PunchAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新打卡记录 DTO
/// </summary>
public class TaktAttendancePunchUpdateDto : TaktAttendancePunchCreateDto
{
    /// <summary>
    /// 打卡记录 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PunchId { get; set; }
}

/// <summary>
/// 打卡记录导出 DTO（Excel 列）
/// </summary>
public class TaktAttendancePunchExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchExportDto()
    {
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
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
    /// 打卡地点或说明
    /// </summary>
    public string? PunchAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// 打卡记录导入模板 DTO（Excel 列）
/// </summary>
public class TaktAttendancePunchTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchTemplateDto()
    {
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
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
    /// 打卡地点或说明
    /// </summary>
    public string? PunchAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 打卡记录导入行 DTO（与模板列一致）
/// </summary>
public class TaktAttendancePunchImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchImportDto()
    {
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
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
    /// 打卡地点或说明
    /// </summary>
    public string? PunchAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}
