// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputDetailDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：PCBA日报明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// PCBA日报明细表Dto
/// </summary>
public partial class TaktPcbaOutputDetailDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailDto()
    {
        TimePeriod = string.Empty;
        PcbBoardType = string.Empty;
        PanelSide = string.Empty;
        CompletedStatus = string.Empty;
        SerialNo = string.Empty;
    }

    /// <summary>
    /// PCBA日报明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaOutputDetailId { get; set; }

    /// <summary>
    /// PCBA日报ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaOutputId { get; set; }
    /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }
    /// <summary>
    /// 班组
    /// </summary>
    public int ShiftNo { get; set; }
    /// <summary>
    /// PCB板别
    /// </summary>
    public string PcbBoardType { get; set; }
    /// <summary>
    /// 面板别
    /// </summary>
    public string PanelSide { get; set; }
    /// <summary>
    /// 批次数量
    /// </summary>
    public decimal BatchQty { get; set; }
    /// <summary>
    /// 当日完成数
    /// </summary>
    public decimal DailyCompletedQty { get; set; }
    /// <summary>
    /// 累计完成数
    /// </summary>
    public decimal TotalCompletedQty { get; set; }
    /// <summary>
    /// 完成状态
    /// </summary>
    public string CompletedStatus { get; set; }
    /// <summary>
    /// 序列号
    /// </summary>
    public string SerialNo { get; set; }
    /// <summary>
    /// 不良台数
    /// </summary>
    public int DefectCount { get; set; }
    /// <summary>
    /// 投入工数
    /// </summary>
    public decimal InputMinutes { get; set; }
    /// <summary>
    /// 修工数
    /// </summary>
    public decimal RepairMinutes { get; set; }
    /// <summary>
    /// 切换次数
    /// </summary>
    public int SwitchCount { get; set; }
    /// <summary>
    /// 切换时间
    /// </summary>
    public decimal SwitchTime { get; set; }
    /// <summary>
    /// 切停机时间
    /// </summary>
    public decimal StopTime { get; set; }
    /// <summary>
    /// 总工数
    /// </summary>
    public decimal TotalMinutes { get; set; }
    /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }
    /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }
}

/// <summary>
/// PCBA日报明细表查询DTO
/// </summary>
public partial class TaktPcbaOutputDetailQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// PCBA日报明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaOutputDetailId { get; set; }

    /// <summary>
    /// PCBA日报ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PcbaOutputId { get; set; }
    /// <summary>
    /// 生产时段
    /// </summary>
    public string? TimePeriod { get; set; }
    /// <summary>
    /// 班组
    /// </summary>
    public int? ShiftNo { get; set; }
    /// <summary>
    /// PCB板别
    /// </summary>
    public string? PcbBoardType { get; set; }
    /// <summary>
    /// 面板别
    /// </summary>
    public string? PanelSide { get; set; }
    /// <summary>
    /// 批次数量
    /// </summary>
    public decimal? BatchQty { get; set; }
    /// <summary>
    /// 当日完成数
    /// </summary>
    public decimal? DailyCompletedQty { get; set; }
    /// <summary>
    /// 累计完成数
    /// </summary>
    public decimal? TotalCompletedQty { get; set; }
    /// <summary>
    /// 完成状态
    /// </summary>
    public string? CompletedStatus { get; set; }
    /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNo { get; set; }
    /// <summary>
    /// 不良台数
    /// </summary>
    public int? DefectCount { get; set; }
    /// <summary>
    /// 投入工数
    /// </summary>
    public decimal? InputMinutes { get; set; }
    /// <summary>
    /// 修工数
    /// </summary>
    public decimal? RepairMinutes { get; set; }
    /// <summary>
    /// 切换次数
    /// </summary>
    public int? SwitchCount { get; set; }
    /// <summary>
    /// 切换时间
    /// </summary>
    public decimal? SwitchTime { get; set; }
    /// <summary>
    /// 切停机时间
    /// </summary>
    public decimal? StopTime { get; set; }
    /// <summary>
    /// 总工数
    /// </summary>
    public decimal? TotalMinutes { get; set; }
    /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }
    /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

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
/// Takt创建PCBA日报明细表DTO
/// </summary>
public partial class TaktPcbaOutputDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailCreateDto()
    {
        TimePeriod = string.Empty;
        PcbBoardType = string.Empty;
        PanelSide = string.Empty;
        CompletedStatus = string.Empty;
        SerialNo = string.Empty;
    }

        /// <summary>
    /// PCBA日报ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 班组
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// PCB板别
    /// </summary>
    public string PcbBoardType { get; set; }

        /// <summary>
    /// 面板别
    /// </summary>
    public string PanelSide { get; set; }

        /// <summary>
    /// 批次数量
    /// </summary>
    public decimal BatchQty { get; set; }

        /// <summary>
    /// 当日完成数
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 累计完成数
    /// </summary>
    public decimal TotalCompletedQty { get; set; }

        /// <summary>
    /// 完成状态
    /// </summary>
    public string CompletedStatus { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string SerialNo { get; set; }

        /// <summary>
    /// 不良台数
    /// </summary>
    public int DefectCount { get; set; }

        /// <summary>
    /// 投入工数
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 修工数
    /// </summary>
    public decimal RepairMinutes { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int SwitchCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal SwitchTime { get; set; }

        /// <summary>
    /// 切停机时间
    /// </summary>
    public decimal StopTime { get; set; }

        /// <summary>
    /// 总工数
    /// </summary>
    public decimal TotalMinutes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

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
/// Takt更新PCBA日报明细表DTO
/// </summary>
public partial class TaktPcbaOutputDetailUpdateDto : TaktPcbaOutputDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailUpdateDto()
    {
    }

        /// <summary>
    /// PCBA日报明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaOutputDetailId { get; set; }
}

/// <summary>
/// PCBA日报明细表完成状态DTO
/// </summary>
public partial class TaktPcbaOutputDetailCompletedStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailCompletedStatusDto()
    {
    }

        /// <summary>
    /// PCBA日报明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaOutputDetailId { get; set; }

    /// <summary>
    /// 完成状态（0=禁用，1=启用）
    /// </summary>
    public int CompletedStatus { get; set; }
}

/// <summary>
/// PCBA日报明细表导入模板DTO
/// </summary>
public partial class TaktPcbaOutputDetailTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailTemplateDto()
    {
        TimePeriod = string.Empty;
        PcbBoardType = string.Empty;
        PanelSide = string.Empty;
        CompletedStatus = string.Empty;
        SerialNo = string.Empty;
    }

        /// <summary>
    /// PCBA日报ID
    /// </summary>
    public long PcbaOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 班组
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// PCB板别
    /// </summary>
    public string PcbBoardType { get; set; }

        /// <summary>
    /// 面板别
    /// </summary>
    public string PanelSide { get; set; }

        /// <summary>
    /// 批次数量
    /// </summary>
    public decimal BatchQty { get; set; }

        /// <summary>
    /// 当日完成数
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 累计完成数
    /// </summary>
    public decimal TotalCompletedQty { get; set; }

        /// <summary>
    /// 完成状态
    /// </summary>
    public string CompletedStatus { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string SerialNo { get; set; }

        /// <summary>
    /// 不良台数
    /// </summary>
    public int DefectCount { get; set; }

        /// <summary>
    /// 投入工数
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 修工数
    /// </summary>
    public decimal RepairMinutes { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int SwitchCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal SwitchTime { get; set; }

        /// <summary>
    /// 切停机时间
    /// </summary>
    public decimal StopTime { get; set; }

        /// <summary>
    /// 总工数
    /// </summary>
    public decimal TotalMinutes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

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
/// PCBA日报明细表导入DTO
/// </summary>
public partial class TaktPcbaOutputDetailImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailImportDto()
    {
        TimePeriod = string.Empty;
        PcbBoardType = string.Empty;
        PanelSide = string.Empty;
        CompletedStatus = string.Empty;
        SerialNo = string.Empty;
    }

        /// <summary>
    /// PCBA日报ID
    /// </summary>
    public long PcbaOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 班组
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// PCB板别
    /// </summary>
    public string PcbBoardType { get; set; }

        /// <summary>
    /// 面板别
    /// </summary>
    public string PanelSide { get; set; }

        /// <summary>
    /// 批次数量
    /// </summary>
    public decimal BatchQty { get; set; }

        /// <summary>
    /// 当日完成数
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 累计完成数
    /// </summary>
    public decimal TotalCompletedQty { get; set; }

        /// <summary>
    /// 完成状态
    /// </summary>
    public string CompletedStatus { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string SerialNo { get; set; }

        /// <summary>
    /// 不良台数
    /// </summary>
    public int DefectCount { get; set; }

        /// <summary>
    /// 投入工数
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 修工数
    /// </summary>
    public decimal RepairMinutes { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int SwitchCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal SwitchTime { get; set; }

        /// <summary>
    /// 切停机时间
    /// </summary>
    public decimal StopTime { get; set; }

        /// <summary>
    /// 总工数
    /// </summary>
    public decimal TotalMinutes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

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
/// PCBA日报明细表导出DTO
/// </summary>
public partial class TaktPcbaOutputDetailExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaOutputDetailExportDto()
    {
        CreatedAt = DateTime.Now;
        TimePeriod = string.Empty;
        PcbBoardType = string.Empty;
        PanelSide = string.Empty;
        CompletedStatus = string.Empty;
        SerialNo = string.Empty;
    }

        /// <summary>
    /// PCBA日报ID
    /// </summary>
    public long PcbaOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 班组
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// PCB板别
    /// </summary>
    public string PcbBoardType { get; set; }

        /// <summary>
    /// 面板别
    /// </summary>
    public string PanelSide { get; set; }

        /// <summary>
    /// 批次数量
    /// </summary>
    public decimal BatchQty { get; set; }

        /// <summary>
    /// 当日完成数
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 累计完成数
    /// </summary>
    public decimal TotalCompletedQty { get; set; }

        /// <summary>
    /// 完成状态
    /// </summary>
    public string CompletedStatus { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string SerialNo { get; set; }

        /// <summary>
    /// 不良台数
    /// </summary>
    public int DefectCount { get; set; }

        /// <summary>
    /// 投入工数
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 修工数
    /// </summary>
    public decimal RepairMinutes { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int SwitchCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal SwitchTime { get; set; }

        /// <summary>
    /// 切停机时间
    /// </summary>
    public decimal StopTime { get; set; }

        /// <summary>
    /// 总工数
    /// </summary>
    public decimal TotalMinutes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}