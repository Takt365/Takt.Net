// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：班次信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 班次信息表Dto
/// </summary>
public partial class TaktWorkShiftDto : TaktDtosEntityBase
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
    }

    /// <summary>
    /// 班次信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long WorkShiftId { get; set; }

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
    public int SortOrder { get; set; }
}

/// <summary>
/// 班次信息表查询DTO
/// </summary>
public partial class TaktWorkShiftQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 班次信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long WorkShiftId { get; set; }

    /// <summary>
    /// 班次编码
    /// </summary>
    public string? ShiftCode { get; set; }
    /// <summary>
    /// 班次名称
    /// </summary>
    public string? ShiftName { get; set; }
    /// <summary>
    /// 开始时间
    /// </summary>
    public string? StartTime { get; set; }
    /// <summary>
    /// 结束时间
    /// </summary>
    public string? EndTime { get; set; }
    /// <summary>
    /// 是否跨日
    /// </summary>
    public int? CrossMidnight { get; set; }
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
/// Takt创建班次信息表DTO
/// </summary>
public partial class TaktWorkShiftCreateDto
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
/// Takt更新班次信息表DTO
/// </summary>
public partial class TaktWorkShiftUpdateDto : TaktWorkShiftCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftUpdateDto()
    {
    }

        /// <summary>
    /// 班次信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long WorkShiftId { get; set; }
}

/// <summary>
/// 班次信息表导入模板DTO
/// </summary>
public partial class TaktWorkShiftTemplateDto
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
/// 班次信息表导入DTO
/// </summary>
public partial class TaktWorkShiftImportDto
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
/// 班次信息表导出DTO
/// </summary>
public partial class TaktWorkShiftExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftExportDto()
    {
        CreatedAt = DateTime.Now;
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
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}