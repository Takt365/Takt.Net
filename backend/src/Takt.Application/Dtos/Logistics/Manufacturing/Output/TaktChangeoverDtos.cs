// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktChangeoverDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：切换记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 切换记录表Dto
/// </summary>
public partial class TaktChangeoverDto : TaktDtosEntityBase
{
    /// <summary>
    /// 切换记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ChangeoverId { get; set; } = 0;

    /// <summary>
    /// 生产工厂
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 生产类别
    /// </summary>
    public string? ProductionCategory { get; set; }
    /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProductionDate { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }
    /// <summary>
    /// 读取SOP时间
    /// </summary>
    public decimal ReadSopTime { get; set; }
    /// <summary>
    /// 人数
    /// </summary>
    public int PersonCount { get; set; }
    /// <summary>
    /// SOP总时间
    /// </summary>
    public decimal TotalSopTime { get; set; }
    /// <summary>
    /// 切换次数
    /// </summary>
    public int ChangeoverCount { get; set; }
    /// <summary>
    /// 切换时间
    /// </summary>
    public decimal ChangeoverTime { get; set; }
    /// <summary>
    /// 切换总时间
    /// </summary>
    public decimal TotalChangeoverTime { get; set; }
}

/// <summary>
/// 切换记录表查询DTO
/// </summary>
public partial class TaktChangeoverQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktChangeoverQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 生产工厂
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 生产类别
    /// </summary>
    public string? ProductionCategory { get; set; }
    /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime? ProductionDate { get; set; }

    /// <summary>
    /// 生产日期开始时间
    /// </summary>
    public DateTime? ProductionDateStart { get; set; }
    /// <summary>
    /// 生产日期结束时间
    /// </summary>
    public DateTime? ProductionDateEnd { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }
    /// <summary>
    /// 读取SOP时间
    /// </summary>
    public decimal? ReadSopTime { get; set; }
    /// <summary>
    /// 人数
    /// </summary>
    public int? PersonCount { get; set; }
    /// <summary>
    /// SOP总时间
    /// </summary>
    public decimal? TotalSopTime { get; set; }
    /// <summary>
    /// 切换次数
    /// </summary>
    public int? ChangeoverCount { get; set; }
    /// <summary>
    /// 切换时间
    /// </summary>
    public decimal? ChangeoverTime { get; set; }
    /// <summary>
    /// 切换总时间
    /// </summary>
    public decimal? TotalChangeoverTime { get; set; }

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
/// Takt创建切换记录表DTO
/// </summary>
public partial class TaktChangeoverCreateDto
{
        /// <summary>
    /// 生产工厂
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string? ProductionCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProductionDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 读取SOP时间
    /// </summary>
    public decimal ReadSopTime { get; set; }

        /// <summary>
    /// 人数
    /// </summary>
    public int PersonCount { get; set; }

        /// <summary>
    /// SOP总时间
    /// </summary>
    public decimal TotalSopTime { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int ChangeoverCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal ChangeoverTime { get; set; }

        /// <summary>
    /// 切换总时间
    /// </summary>
    public decimal TotalChangeoverTime { get; set; }

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
/// Takt更新切换记录表DTO
/// </summary>
public partial class TaktChangeoverUpdateDto : TaktChangeoverCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktChangeoverUpdateDto()
    {
    }

        /// <summary>
    /// 切换记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ChangeoverId { get; set; } = 0;
}

/// <summary>
/// 切换记录表导入模板DTO
/// </summary>
public partial class TaktChangeoverTemplateDto
{
        /// <summary>
    /// 生产工厂
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string? ProductionCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProductionDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 读取SOP时间
    /// </summary>
    public decimal ReadSopTime { get; set; }

        /// <summary>
    /// 人数
    /// </summary>
    public int PersonCount { get; set; }

        /// <summary>
    /// SOP总时间
    /// </summary>
    public decimal TotalSopTime { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int ChangeoverCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal ChangeoverTime { get; set; }

        /// <summary>
    /// 切换总时间
    /// </summary>
    public decimal TotalChangeoverTime { get; set; }

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
/// 切换记录表导入DTO
/// </summary>
public partial class TaktChangeoverImportDto
{
        /// <summary>
    /// 生产工厂
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string? ProductionCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProductionDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 读取SOP时间
    /// </summary>
    public decimal ReadSopTime { get; set; }

        /// <summary>
    /// 人数
    /// </summary>
    public int PersonCount { get; set; }

        /// <summary>
    /// SOP总时间
    /// </summary>
    public decimal TotalSopTime { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int ChangeoverCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal ChangeoverTime { get; set; }

        /// <summary>
    /// 切换总时间
    /// </summary>
    public decimal TotalChangeoverTime { get; set; }

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
/// 切换记录表导出DTO
/// </summary>
public partial class TaktChangeoverExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktChangeoverExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 生产工厂
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string? ProductionCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProductionDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 读取SOP时间
    /// </summary>
    public decimal ReadSopTime { get; set; }

        /// <summary>
    /// 人数
    /// </summary>
    public int PersonCount { get; set; }

        /// <summary>
    /// SOP总时间
    /// </summary>
    public decimal TotalSopTime { get; set; }

        /// <summary>
    /// 切换次数
    /// </summary>
    public int ChangeoverCount { get; set; }

        /// <summary>
    /// 切换时间
    /// </summary>
    public decimal ChangeoverTime { get; set; }

        /// <summary>
    /// 切换总时间
    /// </summary>
    public decimal TotalChangeoverTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}