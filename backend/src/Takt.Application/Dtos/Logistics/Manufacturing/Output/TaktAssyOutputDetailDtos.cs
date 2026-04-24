// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputDetailDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：组立日报明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 组立日报明细表Dto
/// </summary>
public partial class TaktAssyOutputDetailDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailDto()
    {
        TimePeriod = string.Empty;
    }

    /// <summary>
    /// 组立日报明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputDetailId { get; set; }

    /// <summary>
    /// 组立日报ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputId { get; set; }
    /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }
    /// <summary>
    /// 实际生产数量
    /// </summary>
    public decimal ProdActualQty { get; set; }
    /// <summary>
    /// 停线时间
    /// </summary>
    public int DowntimeMinutes { get; set; }
    /// <summary>
    /// 停线原因
    /// </summary>
    public string? DowntimeReason { get; set; }
    /// <summary>
    /// 停线说明
    /// </summary>
    public string? DowntimeDescription { get; set; }
    /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }
    /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }
    /// <summary>
    /// 投入工时
    /// </summary>
    public decimal InputMinutes { get; set; }
    /// <summary>
    /// 生产工时
    /// </summary>
    public decimal ProdMinutes { get; set; }
    /// <summary>
    /// 实际工时
    /// </summary>
    public decimal ActualMinutes { get; set; }
    /// <summary>
    /// 达成率
    /// </summary>
    public decimal AchievementRate { get; set; }
}

/// <summary>
/// 组立日报明细表查询DTO
/// </summary>
public partial class TaktAssyOutputDetailQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 组立日报明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputDetailId { get; set; }

    /// <summary>
    /// 组立日报ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssyOutputId { get; set; }
    /// <summary>
    /// 生产时段
    /// </summary>
    public string? TimePeriod { get; set; }
    /// <summary>
    /// 实际生产数量
    /// </summary>
    public decimal? ProdActualQty { get; set; }
    /// <summary>
    /// 停线时间
    /// </summary>
    public int? DowntimeMinutes { get; set; }
    /// <summary>
    /// 停线原因
    /// </summary>
    public string? DowntimeReason { get; set; }
    /// <summary>
    /// 停线说明
    /// </summary>
    public string? DowntimeDescription { get; set; }
    /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }
    /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }
    /// <summary>
    /// 投入工时
    /// </summary>
    public decimal? InputMinutes { get; set; }
    /// <summary>
    /// 生产工时
    /// </summary>
    public decimal? ProdMinutes { get; set; }
    /// <summary>
    /// 实际工时
    /// </summary>
    public decimal? ActualMinutes { get; set; }
    /// <summary>
    /// 达成率
    /// </summary>
    public decimal? AchievementRate { get; set; }

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
/// Takt创建组立日报明细表DTO
/// </summary>
public partial class TaktAssyOutputDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailCreateDto()
    {
        TimePeriod = string.Empty;
    }

        /// <summary>
    /// 组立日报ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 实际生产数量
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 停线时间
    /// </summary>
    public int DowntimeMinutes { get; set; }

        /// <summary>
    /// 停线原因
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 停线说明
    /// </summary>
    public string? DowntimeDescription { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

        /// <summary>
    /// 投入工时
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 生产工时
    /// </summary>
    public decimal ProdMinutes { get; set; }

        /// <summary>
    /// 实际工时
    /// </summary>
    public decimal ActualMinutes { get; set; }

        /// <summary>
    /// 达成率
    /// </summary>
    public decimal AchievementRate { get; set; }

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
/// Takt更新组立日报明细表DTO
/// </summary>
public partial class TaktAssyOutputDetailUpdateDto : TaktAssyOutputDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailUpdateDto()
    {
    }

        /// <summary>
    /// 组立日报明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputDetailId { get; set; }
}

/// <summary>
/// 组立日报明细表导入模板DTO
/// </summary>
public partial class TaktAssyOutputDetailTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailTemplateDto()
    {
        TimePeriod = string.Empty;
    }

        /// <summary>
    /// 组立日报ID
    /// </summary>
    public long AssyOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 实际生产数量
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 停线时间
    /// </summary>
    public int DowntimeMinutes { get; set; }

        /// <summary>
    /// 停线原因
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 停线说明
    /// </summary>
    public string? DowntimeDescription { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

        /// <summary>
    /// 投入工时
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 生产工时
    /// </summary>
    public decimal ProdMinutes { get; set; }

        /// <summary>
    /// 实际工时
    /// </summary>
    public decimal ActualMinutes { get; set; }

        /// <summary>
    /// 达成率
    /// </summary>
    public decimal AchievementRate { get; set; }

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
/// 组立日报明细表导入DTO
/// </summary>
public partial class TaktAssyOutputDetailImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailImportDto()
    {
        TimePeriod = string.Empty;
    }

        /// <summary>
    /// 组立日报ID
    /// </summary>
    public long AssyOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 实际生产数量
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 停线时间
    /// </summary>
    public int DowntimeMinutes { get; set; }

        /// <summary>
    /// 停线原因
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 停线说明
    /// </summary>
    public string? DowntimeDescription { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

        /// <summary>
    /// 投入工时
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 生产工时
    /// </summary>
    public decimal ProdMinutes { get; set; }

        /// <summary>
    /// 实际工时
    /// </summary>
    public decimal ActualMinutes { get; set; }

        /// <summary>
    /// 达成率
    /// </summary>
    public decimal AchievementRate { get; set; }

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
/// 组立日报明细表导出DTO
/// </summary>
public partial class TaktAssyOutputDetailExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailExportDto()
    {
        CreatedAt = DateTime.Now;
        TimePeriod = string.Empty;
    }

        /// <summary>
    /// 组立日报ID
    /// </summary>
    public long AssyOutputId { get; set; }

        /// <summary>
    /// 生产时段
    /// </summary>
    public string TimePeriod { get; set; }

        /// <summary>
    /// 实际生产数量
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 停线时间
    /// </summary>
    public int DowntimeMinutes { get; set; }

        /// <summary>
    /// 停线原因
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 停线说明
    /// </summary>
    public string? DowntimeDescription { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string? UnachievedReason { get; set; }

        /// <summary>
    /// 未达成说明
    /// </summary>
    public string? UnachievedDescription { get; set; }

        /// <summary>
    /// 投入工时
    /// </summary>
    public decimal InputMinutes { get; set; }

        /// <summary>
    /// 生产工时
    /// </summary>
    public decimal ProdMinutes { get; set; }

        /// <summary>
    /// 实际工时
    /// </summary>
    public decimal ActualMinutes { get; set; }

        /// <summary>
    /// 达成率
    /// </summary>
    public decimal AchievementRate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}