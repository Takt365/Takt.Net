// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectDetailDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：组立不良明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Defect;

/// <summary>
/// 组立不良明细表Dto
/// </summary>
public partial class TaktAssyDefectDetailDto : TaktDtosEntityBase
{
    /// <summary>
    /// 组立不良明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyDefectDetailId { get; set; }

    /// <summary>
    /// 组立不良ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyDefectId { get; set; }
    /// <summary>
    /// 不良区分
    /// </summary>
    public string? DefectCategory { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }
    /// <summary>
    /// 累计不良
    /// </summary>
    public decimal CumulativeDefectQty { get; set; }
    /// <summary>
    /// 随机卡号
    /// </summary>
    public string? RandomCardNo { get; set; }
    /// <summary>
    /// 发生工程
    /// </summary>
    public string? OccurrenceEngineering { get; set; }
    /// <summary>
    /// 测试步骤
    /// </summary>
    public string? TestStep { get; set; }
    /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }
    /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }
    /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }
    /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 组立不良明细表查询DTO
/// </summary>
public partial class TaktAssyDefectDetailQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyDefectDetailQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 组立不良明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyDefectDetailId { get; set; }

    /// <summary>
    /// 组立不良ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssyDefectId { get; set; }
    /// <summary>
    /// 不良区分
    /// </summary>
    public string? DefectCategory { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public decimal? DefectQty { get; set; }
    /// <summary>
    /// 累计不良
    /// </summary>
    public decimal? CumulativeDefectQty { get; set; }
    /// <summary>
    /// 随机卡号
    /// </summary>
    public string? RandomCardNo { get; set; }
    /// <summary>
    /// 发生工程
    /// </summary>
    public string? OccurrenceEngineering { get; set; }
    /// <summary>
    /// 测试步骤
    /// </summary>
    public string? TestStep { get; set; }
    /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }
    /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }
    /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }
    /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }
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
/// Takt创建组立不良明细表DTO
/// </summary>
public partial class TaktAssyDefectDetailCreateDto
{
        /// <summary>
    /// 组立不良ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyDefectId { get; set; }

        /// <summary>
    /// 不良区分
    /// </summary>
    public string? DefectCategory { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 累计不良
    /// </summary>
    public decimal CumulativeDefectQty { get; set; }

        /// <summary>
    /// 随机卡号
    /// </summary>
    public string? RandomCardNo { get; set; }

        /// <summary>
    /// 发生工程
    /// </summary>
    public string? OccurrenceEngineering { get; set; }

        /// <summary>
    /// 测试步骤
    /// </summary>
    public string? TestStep { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

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
/// Takt更新组立不良明细表DTO
/// </summary>
public partial class TaktAssyDefectDetailUpdateDto : TaktAssyDefectDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyDefectDetailUpdateDto()
    {
    }

        /// <summary>
    /// 组立不良明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyDefectDetailId { get; set; }
}

/// <summary>
/// 组立不良明细表状态DTO
/// </summary>
public partial class TaktAssyDefectDetailStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyDefectDetailStatusDto()
    {
    }

        /// <summary>
    /// 组立不良明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyDefectDetailId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 组立不良明细表导入模板DTO
/// </summary>
public partial class TaktAssyDefectDetailTemplateDto
{
        /// <summary>
    /// 组立不良ID
    /// </summary>
    public long AssyDefectId { get; set; }

        /// <summary>
    /// 不良区分
    /// </summary>
    public string? DefectCategory { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 累计不良
    /// </summary>
    public decimal CumulativeDefectQty { get; set; }

        /// <summary>
    /// 随机卡号
    /// </summary>
    public string? RandomCardNo { get; set; }

        /// <summary>
    /// 发生工程
    /// </summary>
    public string? OccurrenceEngineering { get; set; }

        /// <summary>
    /// 测试步骤
    /// </summary>
    public string? TestStep { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

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
/// 组立不良明细表导入DTO
/// </summary>
public partial class TaktAssyDefectDetailImportDto
{
        /// <summary>
    /// 组立不良ID
    /// </summary>
    public long AssyDefectId { get; set; }

        /// <summary>
    /// 不良区分
    /// </summary>
    public string? DefectCategory { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 累计不良
    /// </summary>
    public decimal CumulativeDefectQty { get; set; }

        /// <summary>
    /// 随机卡号
    /// </summary>
    public string? RandomCardNo { get; set; }

        /// <summary>
    /// 发生工程
    /// </summary>
    public string? OccurrenceEngineering { get; set; }

        /// <summary>
    /// 测试步骤
    /// </summary>
    public string? TestStep { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

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
/// 组立不良明细表导出DTO
/// </summary>
public partial class TaktAssyDefectDetailExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyDefectDetailExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 组立不良ID
    /// </summary>
    public long AssyDefectId { get; set; }

        /// <summary>
    /// 不良区分
    /// </summary>
    public string? DefectCategory { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 累计不良
    /// </summary>
    public decimal CumulativeDefectQty { get; set; }

        /// <summary>
    /// 随机卡号
    /// </summary>
    public string? RandomCardNo { get; set; }

        /// <summary>
    /// 发生工程
    /// </summary>
    public string? OccurrenceEngineering { get; set; }

        /// <summary>
    /// 测试步骤
    /// </summary>
    public string? TestStep { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 不良个所
    /// </summary>
    public string? DefectLocation { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}