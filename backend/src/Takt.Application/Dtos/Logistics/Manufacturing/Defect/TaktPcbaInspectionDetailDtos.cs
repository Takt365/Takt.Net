// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaInspectionDetailDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：PCBA检查明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA检查明细表Dto
/// </summary>
public partial class TaktPcbaInspectionDetailDto : TaktDtosEntityBase
{
    /// <summary>
    /// PCBA检查明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaInspectionDetailId { get; set; }

    /// <summary>
    /// PCBA检查ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaInspectionId { get; set; }
    /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }
    /// <summary>
    /// 目视线别
    /// </summary>
    public string? VisualInspectionLine { get; set; }
    /// <summary>
    /// AOI线别
    /// </summary>
    public string? AoiLine { get; set; }
    /// <summary>
    /// B面实装日期
    /// </summary>
    public DateTime? BSideAssemblyDate { get; set; }
    /// <summary>
    /// T面实装日期
    /// </summary>
    public DateTime? TSideAssemblyDate { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }
    /// <summary>
    /// 检查员
    /// </summary>
    public string? InspectorName { get; set; }
    /// <summary>
    /// 当日完成数量
    /// </summary>
    public decimal DailyCompletedQty { get; set; }
    /// <summary>
    /// 检查数量
    /// </summary>
    public decimal InspectionQty { get; set; }
    /// <summary>
    /// 检查状态
    /// </summary>
    public string? InspectionStatus { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }
    /// <summary>
    /// 检查工数
    /// </summary>
    public decimal InspectionWorkHours { get; set; }
    /// <summary>
    /// AOI工数
    /// </summary>
    public decimal AoiWorkHours { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }
    /// <summary>
    /// 手贴
    /// </summary>
    public string? HandPlacement { get; set; }
    /// <summary>
    /// 流水号
    /// </summary>
    public string? SerialNumber { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// PCBA检查明细表查询DTO
/// </summary>
public partial class TaktPcbaInspectionDetailQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaInspectionDetailQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// PCBA检查明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaInspectionDetailId { get; set; }

    /// <summary>
    /// PCBA检查ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PcbaInspectionId { get; set; }
    /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }
    /// <summary>
    /// 目视线别
    /// </summary>
    public string? VisualInspectionLine { get; set; }
    /// <summary>
    /// AOI线别
    /// </summary>
    public string? AoiLine { get; set; }
    /// <summary>
    /// B面实装日期
    /// </summary>
    public DateTime? BSideAssemblyDate { get; set; }

    /// <summary>
    /// B面实装日期开始时间
    /// </summary>
    public DateTime? BSideAssemblyDateStart { get; set; }
    /// <summary>
    /// B面实装日期结束时间
    /// </summary>
    public DateTime? BSideAssemblyDateEnd { get; set; }
    /// <summary>
    /// T面实装日期
    /// </summary>
    public DateTime? TSideAssemblyDate { get; set; }

    /// <summary>
    /// T面实装日期开始时间
    /// </summary>
    public DateTime? TSideAssemblyDateStart { get; set; }
    /// <summary>
    /// T面实装日期结束时间
    /// </summary>
    public DateTime? TSideAssemblyDateEnd { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }
    /// <summary>
    /// 检查员
    /// </summary>
    public string? InspectorName { get; set; }
    /// <summary>
    /// 当日完成数量
    /// </summary>
    public decimal? DailyCompletedQty { get; set; }
    /// <summary>
    /// 检查数量
    /// </summary>
    public decimal? InspectionQty { get; set; }
    /// <summary>
    /// 检查状态
    /// </summary>
    public string? InspectionStatus { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }
    /// <summary>
    /// 检查工数
    /// </summary>
    public decimal? InspectionWorkHours { get; set; }
    /// <summary>
    /// AOI工数
    /// </summary>
    public decimal? AoiWorkHours { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public decimal? DefectQty { get; set; }
    /// <summary>
    /// 手贴
    /// </summary>
    public string? HandPlacement { get; set; }
    /// <summary>
    /// 流水号
    /// </summary>
    public string? SerialNumber { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

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
/// Takt创建PCBA检查明细表DTO
/// </summary>
public partial class TaktPcbaInspectionDetailCreateDto
{
        /// <summary>
    /// PCBA检查ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaInspectionId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 目视线别
    /// </summary>
    public string? VisualInspectionLine { get; set; }

        /// <summary>
    /// AOI线别
    /// </summary>
    public string? AoiLine { get; set; }

        /// <summary>
    /// B面实装日期
    /// </summary>
    public DateTime? BSideAssemblyDate { get; set; }

        /// <summary>
    /// T面实装日期
    /// </summary>
    public DateTime? TSideAssemblyDate { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 检查员
    /// </summary>
    public string? InspectorName { get; set; }

        /// <summary>
    /// 当日完成数量
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 检查数量
    /// </summary>
    public decimal InspectionQty { get; set; }

        /// <summary>
    /// 检查状态
    /// </summary>
    public string? InspectionStatus { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 检查工数
    /// </summary>
    public decimal InspectionWorkHours { get; set; }

        /// <summary>
    /// AOI工数
    /// </summary>
    public decimal AoiWorkHours { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 手贴
    /// </summary>
    public string? HandPlacement { get; set; }

        /// <summary>
    /// 流水号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// Takt更新PCBA检查明细表DTO
/// </summary>
public partial class TaktPcbaInspectionDetailUpdateDto : TaktPcbaInspectionDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaInspectionDetailUpdateDto()
    {
    }

        /// <summary>
    /// PCBA检查明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaInspectionDetailId { get; set; }
}

/// <summary>
/// PCBA检查明细表检查状态DTO
/// </summary>
public partial class TaktPcbaInspectionDetailInspectionStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaInspectionDetailInspectionStatusDto()
    {
    }

        /// <summary>
    /// PCBA检查明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaInspectionDetailId { get; set; }

    /// <summary>
    /// 检查状态（0=禁用，1=启用）
    /// </summary>
    public int InspectionStatus { get; set; }
}

/// <summary>
/// PCBA检查明细表状态DTO
/// </summary>
public partial class TaktPcbaInspectionDetailStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaInspectionDetailStatusDto()
    {
    }

        /// <summary>
    /// PCBA检查明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaInspectionDetailId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// PCBA检查明细表导入模板DTO
/// </summary>
public partial class TaktPcbaInspectionDetailTemplateDto
{
        /// <summary>
    /// PCBA检查ID
    /// </summary>
    public long PcbaInspectionId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 目视线别
    /// </summary>
    public string? VisualInspectionLine { get; set; }

        /// <summary>
    /// AOI线别
    /// </summary>
    public string? AoiLine { get; set; }

        /// <summary>
    /// B面实装日期
    /// </summary>
    public DateTime? BSideAssemblyDate { get; set; }

        /// <summary>
    /// T面实装日期
    /// </summary>
    public DateTime? TSideAssemblyDate { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 检查员
    /// </summary>
    public string? InspectorName { get; set; }

        /// <summary>
    /// 当日完成数量
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 检查数量
    /// </summary>
    public decimal InspectionQty { get; set; }

        /// <summary>
    /// 检查状态
    /// </summary>
    public string? InspectionStatus { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 检查工数
    /// </summary>
    public decimal InspectionWorkHours { get; set; }

        /// <summary>
    /// AOI工数
    /// </summary>
    public decimal AoiWorkHours { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 手贴
    /// </summary>
    public string? HandPlacement { get; set; }

        /// <summary>
    /// 流水号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// PCBA检查明细表导入DTO
/// </summary>
public partial class TaktPcbaInspectionDetailImportDto
{
        /// <summary>
    /// PCBA检查ID
    /// </summary>
    public long PcbaInspectionId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 目视线别
    /// </summary>
    public string? VisualInspectionLine { get; set; }

        /// <summary>
    /// AOI线别
    /// </summary>
    public string? AoiLine { get; set; }

        /// <summary>
    /// B面实装日期
    /// </summary>
    public DateTime? BSideAssemblyDate { get; set; }

        /// <summary>
    /// T面实装日期
    /// </summary>
    public DateTime? TSideAssemblyDate { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 检查员
    /// </summary>
    public string? InspectorName { get; set; }

        /// <summary>
    /// 当日完成数量
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 检查数量
    /// </summary>
    public decimal InspectionQty { get; set; }

        /// <summary>
    /// 检查状态
    /// </summary>
    public string? InspectionStatus { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 检查工数
    /// </summary>
    public decimal InspectionWorkHours { get; set; }

        /// <summary>
    /// AOI工数
    /// </summary>
    public decimal AoiWorkHours { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 手贴
    /// </summary>
    public string? HandPlacement { get; set; }

        /// <summary>
    /// 流水号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// PCBA检查明细表导出DTO
/// </summary>
public partial class TaktPcbaInspectionDetailExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaInspectionDetailExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// PCBA检查ID
    /// </summary>
    public long PcbaInspectionId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 目视线别
    /// </summary>
    public string? VisualInspectionLine { get; set; }

        /// <summary>
    /// AOI线别
    /// </summary>
    public string? AoiLine { get; set; }

        /// <summary>
    /// B面实装日期
    /// </summary>
    public DateTime? BSideAssemblyDate { get; set; }

        /// <summary>
    /// T面实装日期
    /// </summary>
    public DateTime? TSideAssemblyDate { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 检查员
    /// </summary>
    public string? InspectorName { get; set; }

        /// <summary>
    /// 当日完成数量
    /// </summary>
    public decimal DailyCompletedQty { get; set; }

        /// <summary>
    /// 检查数量
    /// </summary>
    public decimal InspectionQty { get; set; }

        /// <summary>
    /// 检查状态
    /// </summary>
    public string? InspectionStatus { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 检查工数
    /// </summary>
    public decimal InspectionWorkHours { get; set; }

        /// <summary>
    /// AOI工数
    /// </summary>
    public decimal AoiWorkHours { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 手贴
    /// </summary>
    public string? HandPlacement { get; set; }

        /// <summary>
    /// 流水号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}