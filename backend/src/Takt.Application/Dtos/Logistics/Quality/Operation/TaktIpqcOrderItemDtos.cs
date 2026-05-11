// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderItemDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：制程检验单明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 制程检验单明细表Dto
/// </summary>
public partial class TaktIpqcOrderItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemDto()
    {
        IpqcOrderCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        StandardCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        InspectorBy = string.Empty;
    }

    /// <summary>
    /// 制程检验单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderItemId { get; set; } = 0;

    /// <summary>
    /// IPQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderId { get; set; }
    /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
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
    /// 生产数量
    /// </summary>
    public decimal ProductionQuantity { get; set; }
    /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }
    /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMethod { get; set; }
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
    /// 验退数量
    /// </summary>
    public decimal InspectionReturnQuantity { get; set; }
    /// <summary>
    /// 判定状态
    /// </summary>
    public int JudgeStatus { get; set; }
    /// <summary>
    /// 抽检序列号
    /// </summary>
    public string? SampleSerialNo { get; set; }
    /// <summary>
    /// 检验说明
    /// </summary>
    public string? InspectionDescription { get; set; }
    /// <summary>
    /// 检验员
    /// </summary>
    public string InspectorBy { get; set; }
    /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime InspectionDate { get; set; }

    /// <summary>
    /// IPQC检验单（主表）
    /// </summary>
    public TaktIpqcOrderDto? Order { get; set; }

    /// <summary>
    /// 不良处理记录列表（主子表关系）（外键在子表 TaktIpqcDefectHandlingDto.IpqcOrderItemId）
    /// </summary>
    public List<TaktIpqcDefectHandlingDto>? DefectHandlings { get; set; }
}

/// <summary>
/// 制程检验单明细表查询DTO
/// </summary>
public partial class TaktIpqcOrderItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// IPQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? IpqcOrderId { get; set; }
    /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string? IpqcOrderCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }
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
    /// 生产数量
    /// </summary>
    public decimal? ProductionQuantity { get; set; }
    /// <summary>
    /// 检验标准编码
    /// </summary>
    public string? StandardCode { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }
    /// <summary>
    /// 检验方式
    /// </summary>
    public int? InspectionMethod { get; set; }
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
    /// 验退数量
    /// </summary>
    public decimal? InspectionReturnQuantity { get; set; }
    /// <summary>
    /// 判定状态
    /// </summary>
    public int? JudgeStatus { get; set; }
    /// <summary>
    /// 抽检序列号
    /// </summary>
    public string? SampleSerialNo { get; set; }
    /// <summary>
    /// 检验说明
    /// </summary>
    public string? InspectionDescription { get; set; }
    /// <summary>
    /// 检验员
    /// </summary>
    public string? InspectorBy { get; set; }
    /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime? InspectionDate { get; set; }

    /// <summary>
    /// 检验日期开始时间
    /// </summary>
    public DateTime? InspectionDateStart { get; set; }
    /// <summary>
    /// 检验日期结束时间
    /// </summary>
    public DateTime? InspectionDateEnd { get; set; }

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
/// Takt创建制程检验单明细表DTO
/// </summary>
public partial class TaktIpqcOrderItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemCreateDto()
    {
        IpqcOrderCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        StandardCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        InspectorBy = string.Empty;
    }

        /// <summary>
    /// IPQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
    /// 生产数量
    /// </summary>
    public decimal ProductionQuantity { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMethod { get; set; }

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
    /// 验退数量
    /// </summary>
    public decimal InspectionReturnQuantity { get; set; }

        /// <summary>
    /// 判定状态
    /// </summary>
    public int JudgeStatus { get; set; }

        /// <summary>
    /// 抽检序列号
    /// </summary>
    public string? SampleSerialNo { get; set; }

        /// <summary>
    /// 检验说明
    /// </summary>
    public string? InspectionDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string InspectorBy { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime InspectionDate { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 不良处理记录列表（主子表关系）（外键在子表 TaktIpqcDefectHandlingCreateDto.IpqcOrderItemId）
    /// </summary>
    public List<TaktIpqcDefectHandlingCreateDto>? DefectHandlings { get; set; }

}

/// <summary>
/// Takt更新制程检验单明细表DTO
/// </summary>
public partial class TaktIpqcOrderItemUpdateDto : TaktIpqcOrderItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemUpdateDto()
    {
    }

        /// <summary>
    /// 制程检验单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderItemId { get; set; } = 0;
}

/// <summary>
/// 制程检验单明细表判定状态DTO
/// </summary>
public partial class TaktIpqcOrderItemJudgeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemJudgeStatusDto()
    {
    }

        /// <summary>
    /// 制程检验单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderItemId { get; set; } = 0;

    /// <summary>
    /// 判定状态（0=禁用，1=启用）
    /// </summary>
    public int JudgeStatus { get; set; }
}

/// <summary>
/// 制程检验单明细表导入模板DTO
/// </summary>
public partial class TaktIpqcOrderItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemTemplateDto()
    {
        IpqcOrderCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        StandardCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        InspectorBy = string.Empty;
    }

        /// <summary>
    /// IPQC检验单ID
    /// </summary>
    public long IpqcOrderId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
    /// 生产数量
    /// </summary>
    public decimal ProductionQuantity { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMethod { get; set; }

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
    /// 验退数量
    /// </summary>
    public decimal InspectionReturnQuantity { get; set; }

        /// <summary>
    /// 判定状态
    /// </summary>
    public int JudgeStatus { get; set; }

        /// <summary>
    /// 抽检序列号
    /// </summary>
    public string? SampleSerialNo { get; set; }

        /// <summary>
    /// 检验说明
    /// </summary>
    public string? InspectionDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string InspectorBy { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime InspectionDate { get; set; }

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
/// 制程检验单明细表导入DTO
/// </summary>
public partial class TaktIpqcOrderItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemImportDto()
    {
        IpqcOrderCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        StandardCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        InspectorBy = string.Empty;
    }

        /// <summary>
    /// IPQC检验单ID
    /// </summary>
    public long IpqcOrderId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
    /// 生产数量
    /// </summary>
    public decimal ProductionQuantity { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMethod { get; set; }

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
    /// 验退数量
    /// </summary>
    public decimal InspectionReturnQuantity { get; set; }

        /// <summary>
    /// 判定状态
    /// </summary>
    public int JudgeStatus { get; set; }

        /// <summary>
    /// 抽检序列号
    /// </summary>
    public string? SampleSerialNo { get; set; }

        /// <summary>
    /// 检验说明
    /// </summary>
    public string? InspectionDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string InspectorBy { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime InspectionDate { get; set; }

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
/// 制程检验单明细表导出DTO
/// </summary>
public partial class TaktIpqcOrderItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemExportDto()
    {
        CreatedAt = DateTime.Now;
        IpqcOrderCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        StandardCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        InspectorBy = string.Empty;
    }

        /// <summary>
    /// IPQC检验单ID
    /// </summary>
    public long IpqcOrderId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
    /// 生产数量
    /// </summary>
    public decimal ProductionQuantity { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMethod { get; set; }

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
    /// 验退数量
    /// </summary>
    public decimal InspectionReturnQuantity { get; set; }

        /// <summary>
    /// 判定状态
    /// </summary>
    public int JudgeStatus { get; set; }

        /// <summary>
    /// 抽检序列号
    /// </summary>
    public string? SampleSerialNo { get; set; }

        /// <summary>
    /// 检验说明
    /// </summary>
    public string? InspectionDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string InspectorBy { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime InspectionDate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}