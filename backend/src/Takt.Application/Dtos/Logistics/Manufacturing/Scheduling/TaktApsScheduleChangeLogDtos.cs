// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleChangeLogDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：APS排程变更日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程变更日志表Dto
/// </summary>
public partial class TaktApsScheduleChangeLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// APS排程变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleChangeLogId { get; set; } = 0;

    /// <summary>
    /// APS排程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleId { get; set; }
    /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
    /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }
    /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

    /// <summary>
    /// APS排程主表（主表）
    /// </summary>
    public TaktApsScheduleDto? Schedule { get; set; }
}

/// <summary>
/// APS排程变更日志表查询DTO
/// </summary>
public partial class TaktApsScheduleChangeLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleChangeLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// APS排程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApsScheduleId { get; set; }
    /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int? ChangeType { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
    /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }
    /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime? ChangeTime { get; set; }

    /// <summary>
    /// 变更时间开始时间
    /// </summary>
    public DateTime? ChangeTimeStart { get; set; }
    /// <summary>
    /// 变更时间结束时间
    /// </summary>
    public DateTime? ChangeTimeEnd { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
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
/// Takt创建APS排程变更日志表DTO
/// </summary>
public partial class TaktApsScheduleChangeLogCreateDto
{
        /// <summary>
    /// APS排程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

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
/// Takt更新APS排程变更日志表DTO
/// </summary>
public partial class TaktApsScheduleChangeLogUpdateDto : TaktApsScheduleChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleChangeLogUpdateDto()
    {
    }

        /// <summary>
    /// APS排程变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleChangeLogId { get; set; } = 0;
}

/// <summary>
/// APS排程变更日志表导入模板DTO
/// </summary>
public partial class TaktApsScheduleChangeLogTemplateDto
{
        /// <summary>
    /// APS排程ID
    /// </summary>
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

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
/// APS排程变更日志表导入DTO
/// </summary>
public partial class TaktApsScheduleChangeLogImportDto
{
        /// <summary>
    /// APS排程ID
    /// </summary>
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

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
/// APS排程变更日志表导出DTO
/// </summary>
public partial class TaktApsScheduleChangeLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleChangeLogExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// APS排程ID
    /// </summary>
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}