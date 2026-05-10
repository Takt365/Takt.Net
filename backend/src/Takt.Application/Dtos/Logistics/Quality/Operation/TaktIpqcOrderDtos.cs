// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：制程检验单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 制程检验单表Dto
/// </summary>
public partial class TaktIpqcOrderDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderDto()
    {
        OrderCode = string.Empty;
        SourceCode = string.Empty;
        StandardCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

    /// <summary>
    /// 制程检验单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderId { get; set; } = 0;

    /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }
    /// <summary>
    /// 来源单号
    /// </summary>
    public string SourceCode { get; set; }
    /// <summary>
    /// 检验计划编码
    /// </summary>
    public string? PlanCode { get; set; }
    /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }
    /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }
    /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }
    /// <summary>
    /// 抽样数量
    /// </summary>
    public int SampleQuantity { get; set; }
    /// <summary>
    /// 合格数量
    /// </summary>
    public int QualifiedQuantity { get; set; }
    /// <summary>
    /// 不合格数量
    /// </summary>
    public int UnqualifiedQuantity { get; set; }
    /// <summary>
    /// 检验结论
    /// </summary>
    public int InspectionConclusion { get; set; }
    /// <summary>
    /// 判定人
    /// </summary>
    public string? JudgeBy { get; set; }
    /// <summary>
    /// 判定时间
    /// </summary>
    public DateTime? JudgeTime { get; set; }
    /// <summary>
    /// 检验备注
    /// </summary>
    public string? InspectionRemark { get; set; }
    /// <summary>
    /// 检验单状态
    /// </summary>
    public int OrderStatus { get; set; }

    /// <summary>
    /// IPQC检验单明细列表（主子表关系）（外键在子表 TaktIpqcOrderItemDto.IpqcOrderId）
    /// </summary>
    public List<TaktIpqcOrderItemDto>? Items { get; set; }

    /// <summary>
    /// 变更日志列表（主子表关系）（外键在子表 TaktIpqcOrderChangeLogDto.IpqcOrderId）
    /// </summary>
    public List<TaktIpqcOrderChangeLogDto>? ChangeLogs { get; set; }
}

/// <summary>
/// 制程检验单表查询DTO
/// </summary>
public partial class TaktIpqcOrderQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 检验单编码
    /// </summary>
    public string? OrderCode { get; set; }
    /// <summary>
    /// 来源单号
    /// </summary>
    public string? SourceCode { get; set; }
    /// <summary>
    /// 检验计划编码
    /// </summary>
    public string? PlanCode { get; set; }
    /// <summary>
    /// 检验标准编码
    /// </summary>
    public string? StandardCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }
    /// <summary>
    /// 工序编码
    /// </summary>
    public string? ProcessCode { get; set; }
    /// <summary>
    /// 工序名称
    /// </summary>
    public string? ProcessName { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }
    /// <summary>
    /// 抽样数量
    /// </summary>
    public int? SampleQuantity { get; set; }
    /// <summary>
    /// 合格数量
    /// </summary>
    public int? QualifiedQuantity { get; set; }
    /// <summary>
    /// 不合格数量
    /// </summary>
    public int? UnqualifiedQuantity { get; set; }
    /// <summary>
    /// 检验结论
    /// </summary>
    public int? InspectionConclusion { get; set; }
    /// <summary>
    /// 判定人
    /// </summary>
    public string? JudgeBy { get; set; }
    /// <summary>
    /// 判定时间
    /// </summary>
    public DateTime? JudgeTime { get; set; }

    /// <summary>
    /// 判定时间开始时间
    /// </summary>
    public DateTime? JudgeTimeStart { get; set; }
    /// <summary>
    /// 判定时间结束时间
    /// </summary>
    public DateTime? JudgeTimeEnd { get; set; }
    /// <summary>
    /// 检验备注
    /// </summary>
    public string? InspectionRemark { get; set; }
    /// <summary>
    /// 检验单状态
    /// </summary>
    public int? OrderStatus { get; set; }

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
/// Takt创建制程检验单表DTO
/// </summary>
public partial class TaktIpqcOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderCreateDto()
    {
        OrderCode = string.Empty;
        SourceCode = string.Empty;
        StandardCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 来源单号
    /// </summary>
    public string SourceCode { get; set; }

        /// <summary>
    /// 检验计划编码
    /// </summary>
    public string? PlanCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样数量
    /// </summary>
    public int SampleQuantity { get; set; }

        /// <summary>
    /// 合格数量
    /// </summary>
    public int QualifiedQuantity { get; set; }

        /// <summary>
    /// 不合格数量
    /// </summary>
    public int UnqualifiedQuantity { get; set; }

        /// <summary>
    /// 检验结论
    /// </summary>
    public int InspectionConclusion { get; set; }

        /// <summary>
    /// 判定人
    /// </summary>
    public string? JudgeBy { get; set; }

        /// <summary>
    /// 判定时间
    /// </summary>
    public DateTime? JudgeTime { get; set; }

        /// <summary>
    /// 检验备注
    /// </summary>
    public string? InspectionRemark { get; set; }

        /// <summary>
    /// 检验单状态
    /// </summary>
    public int OrderStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// IPQC检验单明细列表（主子表关系）（外键在子表 TaktIpqcOrderItemCreateDto.IpqcOrderId）
    /// </summary>
    public List<TaktIpqcOrderItemCreateDto>? Items { get; set; }


    /// <summary>
    /// 变更日志列表（主子表关系）（外键在子表 TaktIpqcOrderChangeLogCreateDto.IpqcOrderId）
    /// </summary>
    public List<TaktIpqcOrderChangeLogCreateDto>? ChangeLogs { get; set; }

}

/// <summary>
/// Takt更新制程检验单表DTO
/// </summary>
public partial class TaktIpqcOrderUpdateDto : TaktIpqcOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderUpdateDto()
    {
    }

        /// <summary>
    /// 制程检验单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderId { get; set; } = 0;
}

/// <summary>
/// 制程检验单表检验单状态DTO
/// </summary>
public partial class TaktIpqcOrderOrderStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderOrderStatusDto()
    {
    }

        /// <summary>
    /// 制程检验单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderId { get; set; } = 0;

    /// <summary>
    /// 检验单状态（0=禁用，1=启用）
    /// </summary>
    public int OrderStatus { get; set; }
}

/// <summary>
/// 制程检验单表导入模板DTO
/// </summary>
public partial class TaktIpqcOrderTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderTemplateDto()
    {
        OrderCode = string.Empty;
        SourceCode = string.Empty;
        StandardCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 来源单号
    /// </summary>
    public string SourceCode { get; set; }

        /// <summary>
    /// 检验计划编码
    /// </summary>
    public string? PlanCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样数量
    /// </summary>
    public int SampleQuantity { get; set; }

        /// <summary>
    /// 合格数量
    /// </summary>
    public int QualifiedQuantity { get; set; }

        /// <summary>
    /// 不合格数量
    /// </summary>
    public int UnqualifiedQuantity { get; set; }

        /// <summary>
    /// 检验结论
    /// </summary>
    public int InspectionConclusion { get; set; }

        /// <summary>
    /// 判定人
    /// </summary>
    public string? JudgeBy { get; set; }

        /// <summary>
    /// 判定时间
    /// </summary>
    public DateTime? JudgeTime { get; set; }

        /// <summary>
    /// 检验备注
    /// </summary>
    public string? InspectionRemark { get; set; }

        /// <summary>
    /// 检验单状态
    /// </summary>
    public int OrderStatus { get; set; }

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
/// 制程检验单表导入DTO
/// </summary>
public partial class TaktIpqcOrderImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderImportDto()
    {
        OrderCode = string.Empty;
        SourceCode = string.Empty;
        StandardCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 来源单号
    /// </summary>
    public string SourceCode { get; set; }

        /// <summary>
    /// 检验计划编码
    /// </summary>
    public string? PlanCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样数量
    /// </summary>
    public int SampleQuantity { get; set; }

        /// <summary>
    /// 合格数量
    /// </summary>
    public int QualifiedQuantity { get; set; }

        /// <summary>
    /// 不合格数量
    /// </summary>
    public int UnqualifiedQuantity { get; set; }

        /// <summary>
    /// 检验结论
    /// </summary>
    public int InspectionConclusion { get; set; }

        /// <summary>
    /// 判定人
    /// </summary>
    public string? JudgeBy { get; set; }

        /// <summary>
    /// 判定时间
    /// </summary>
    public DateTime? JudgeTime { get; set; }

        /// <summary>
    /// 检验备注
    /// </summary>
    public string? InspectionRemark { get; set; }

        /// <summary>
    /// 检验单状态
    /// </summary>
    public int OrderStatus { get; set; }

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
/// 制程检验单表导出DTO
/// </summary>
public partial class TaktIpqcOrderExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderExportDto()
    {
        CreatedAt = DateTime.Now;
        OrderCode = string.Empty;
        SourceCode = string.Empty;
        StandardCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 来源单号
    /// </summary>
    public string SourceCode { get; set; }

        /// <summary>
    /// 检验计划编码
    /// </summary>
    public string? PlanCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样数量
    /// </summary>
    public int SampleQuantity { get; set; }

        /// <summary>
    /// 合格数量
    /// </summary>
    public int QualifiedQuantity { get; set; }

        /// <summary>
    /// 不合格数量
    /// </summary>
    public int UnqualifiedQuantity { get; set; }

        /// <summary>
    /// 检验结论
    /// </summary>
    public int InspectionConclusion { get; set; }

        /// <summary>
    /// 判定人
    /// </summary>
    public string? JudgeBy { get; set; }

        /// <summary>
    /// 判定时间
    /// </summary>
    public DateTime? JudgeTime { get; set; }

        /// <summary>
    /// 检验备注
    /// </summary>
    public string? InspectionRemark { get; set; }

        /// <summary>
    /// 检验单状态
    /// </summary>
    public int OrderStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}