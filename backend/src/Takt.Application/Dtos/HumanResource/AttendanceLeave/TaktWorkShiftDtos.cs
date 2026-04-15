// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：班次 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktHolidayDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 班次 DTO（列表/详情）
/// </summary>
public class TaktWorkShiftDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftDto()
    {
        ShiftCode = string.Empty;
        ShiftName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 班次ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftId { get; set; }

    /// <summary>
    /// 班次编码
    /// </summary>
    public string ShiftCode { get; set; }

    /// <summary>
    /// 班次名称
    /// </summary>
    public string ShiftName { get; set; }

    /// <summary>
    /// 当班开始时间（HH:mm）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 当班结束时间（HH:mm）
    /// </summary>
    public string EndTime { get; set; }

    /// <summary>
    /// 是否跨自然日（0=否 1=是）
    /// </summary>
    public int CrossMidnight { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 班次查询 DTO
/// </summary>
public class TaktWorkShiftQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftQueryDto()
    {
    }

    /// <summary>
    /// 班次编码（模糊）
    /// </summary>
    public string? ShiftCode { get; set; }

    /// <summary>
    /// 班次名称（模糊）
    /// </summary>
    public string? ShiftName { get; set; }
}

/// <summary>
/// 创建班次 DTO
/// </summary>
public class TaktWorkShiftCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftCreateDto()
    {
        ShiftCode = string.Empty;
        ShiftName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
    }

    /// <summary>
    /// 班次编码
    /// </summary>
    public string ShiftCode { get; set; }

    /// <summary>
    /// 班次名称
    /// </summary>
    public string ShiftName { get; set; }

    /// <summary>
    /// 当班开始时间（HH:mm）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 当班结束时间（HH:mm）
    /// </summary>
    public string EndTime { get; set; }

    /// <summary>
    /// 是否跨自然日（0=否 1=是）
    /// </summary>
    public int CrossMidnight { get; set; }

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
/// 更新班次 DTO
/// </summary>
public class TaktWorkShiftUpdateDto : TaktWorkShiftCreateDto
{
    /// <summary>
    /// 班次ID（适配字段）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftId { get; set; }
}

/// <summary>
/// 班次导入模板 DTO
/// </summary>
public class TaktWorkShiftTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftTemplateDto()
    {
        ShiftCode = string.Empty;
        ShiftName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 班次编码
    /// </summary>
    public string ShiftCode { get; set; }

    /// <summary>
    /// 班次名称
    /// </summary>
    public string ShiftName { get; set; }

    /// <summary>
    /// 开始时间（HH:mm）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（HH:mm）
    /// </summary>
    public string EndTime { get; set; }

    /// <summary>
    /// 是否跨日（0=否 1=是）
    /// </summary>
    public int CrossMidnight { get; set; }

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
/// 班次导入行 DTO
/// </summary>
public class TaktWorkShiftImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftImportDto()
    {
        ShiftCode = string.Empty;
        ShiftName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 班次编码
    /// </summary>
    public string ShiftCode { get; set; }

    /// <summary>
    /// 班次名称
    /// </summary>
    public string ShiftName { get; set; }

    /// <summary>
    /// 开始时间（HH:mm）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（HH:mm）
    /// </summary>
    public string EndTime { get; set; }

    /// <summary>
    /// 是否跨日（0=否 1=是）
    /// </summary>
    public int CrossMidnight { get; set; }

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
/// 班次导出 DTO
/// </summary>
public class TaktWorkShiftExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftExportDto()
    {
        ShiftCode = string.Empty;
        ShiftName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
    }

    /// <summary>
    /// 班次编码
    /// </summary>
    public string ShiftCode { get; set; }

    /// <summary>
    /// 班次名称
    /// </summary>
    public string ShiftName { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; }

    /// <summary>
    /// 是否跨日
    /// </summary>
    public int CrossMidnight { get; set; }

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
