// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：品质业务主表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 品质业务主表Dto
/// </summary>
public partial class TaktQualityOperationDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationDto()
    {
        PlantCode = string.Empty;
        OperationNo = string.Empty;
        OperationMonth = string.Empty;
        CostCurrency = string.Empty;
    }

    /// <summary>
    /// 品质业务主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 品质业务编号
    /// </summary>
    public string OperationNo { get; set; }
    /// <summary>
    /// 业务年月
    /// </summary>
    public string OperationMonth { get; set; }
    /// <summary>
    /// 顾客名
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Debit Note No
    /// </summary>
    public string? DebitNoteNo { get; set; }
    /// <summary>
    /// 记录者
    /// </summary>
    public string? Recorder { get; set; }
    /// <summary>
    /// 质量总成本
    /// </summary>
    public decimal TotalQualityCost { get; set; }
    /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 来料检验费用明细列表（外键在子表 TaktQualityOperationIncomingDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationIncomingDto>? IncomingItems { get; set; }

    /// <summary>
    /// 初期/定期检定费用明细列表（外键在子表 TaktQualityOperationFirstArticleDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationFirstArticleDto>? FirstArticleItems { get; set; }

    /// <summary>
    /// 设备校正费用明细列表（外键在子表 TaktQualityOperationCalibrationDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationCalibrationDto>? CalibrationItems { get; set; }

    /// <summary>
    /// 其他通常业务费用明细列表（外键在子表 TaktQualityOperationOtherDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationOtherDto>? OtherItems { get; set; }

    /// <summary>
    /// 出货检验费用明细列表（外键在子表 TaktQualityOperationOutgoingDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationOutgoingDto>? OutgoingItems { get; set; }

    /// <summary>
    /// 信赖性评价/ORT费用明细列表（外键在子表 TaktQualityOperationReliabilityDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationReliabilityDto>? ReliabilityItems { get; set; }

    /// <summary>
    /// 顾客品质要求对应费用明细列表（外键在子表 TaktQualityOperationCustomerResponseDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationCustomerResponseDto>? CustomerResponseItems { get; set; }
}

/// <summary>
/// 品质业务主表查询DTO
/// </summary>
public partial class TaktQualityOperationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 品质业务编号
    /// </summary>
    public string? OperationNo { get; set; }
    /// <summary>
    /// 业务年月
    /// </summary>
    public string? OperationMonth { get; set; }
    /// <summary>
    /// 顾客名
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Debit Note No
    /// </summary>
    public string? DebitNoteNo { get; set; }
    /// <summary>
    /// 记录者
    /// </summary>
    public string? Recorder { get; set; }
    /// <summary>
    /// 质量总成本
    /// </summary>
    public decimal? TotalQualityCost { get; set; }
    /// <summary>
    /// 成本币种
    /// </summary>
    public string? CostCurrency { get; set; }

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
/// Takt创建品质业务主表DTO
/// </summary>
public partial class TaktQualityOperationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationCreateDto()
    {
        PlantCode = string.Empty;
        OperationNo = string.Empty;
        OperationMonth = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 品质业务编号
    /// </summary>
    public string OperationNo { get; set; }

        /// <summary>
    /// 业务年月
    /// </summary>
    public string OperationMonth { get; set; }

        /// <summary>
    /// 顾客名
    /// </summary>
    public string? CustomerName { get; set; }

        /// <summary>
    /// Debit Note No
    /// </summary>
    public string? DebitNoteNo { get; set; }

        /// <summary>
    /// 记录者
    /// </summary>
    public string? Recorder { get; set; }

        /// <summary>
    /// 质量总成本
    /// </summary>
    public decimal TotalQualityCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 来料检验费用明细列表（外键在子表 TaktQualityOperationIncomingCreateDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationIncomingCreateDto>? IncomingItems { get; set; }


    /// <summary>
    /// 初期/定期检定费用明细列表（外键在子表 TaktQualityOperationFirstArticleCreateDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationFirstArticleCreateDto>? FirstArticleItems { get; set; }


    /// <summary>
    /// 设备校正费用明细列表（外键在子表 TaktQualityOperationCalibrationCreateDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationCalibrationCreateDto>? CalibrationItems { get; set; }


    /// <summary>
    /// 其他通常业务费用明细列表（外键在子表 TaktQualityOperationOtherCreateDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationOtherCreateDto>? OtherItems { get; set; }


    /// <summary>
    /// 出货检验费用明细列表（外键在子表 TaktQualityOperationOutgoingCreateDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationOutgoingCreateDto>? OutgoingItems { get; set; }


    /// <summary>
    /// 信赖性评价/ORT费用明细列表（外键在子表 TaktQualityOperationReliabilityCreateDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationReliabilityCreateDto>? ReliabilityItems { get; set; }


    /// <summary>
    /// 顾客品质要求对应费用明细列表（外键在子表 TaktQualityOperationCustomerResponseCreateDto.QualityOperationId）
    /// </summary>
    public List<TaktQualityOperationCustomerResponseCreateDto>? CustomerResponseItems { get; set; }

}

/// <summary>
/// Takt更新品质业务主表DTO
/// </summary>
public partial class TaktQualityOperationUpdateDto : TaktQualityOperationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationUpdateDto()
    {
    }

        /// <summary>
    /// 品质业务主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationId { get; set; } = 0;
}

/// <summary>
/// 品质业务主表导入模板DTO
/// </summary>
public partial class TaktQualityOperationTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationTemplateDto()
    {
        PlantCode = string.Empty;
        OperationNo = string.Empty;
        OperationMonth = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 品质业务编号
    /// </summary>
    public string OperationNo { get; set; }

        /// <summary>
    /// 业务年月
    /// </summary>
    public string OperationMonth { get; set; }

        /// <summary>
    /// 顾客名
    /// </summary>
    public string? CustomerName { get; set; }

        /// <summary>
    /// Debit Note No
    /// </summary>
    public string? DebitNoteNo { get; set; }

        /// <summary>
    /// 记录者
    /// </summary>
    public string? Recorder { get; set; }

        /// <summary>
    /// 质量总成本
    /// </summary>
    public decimal TotalQualityCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

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
/// 品质业务主表导入DTO
/// </summary>
public partial class TaktQualityOperationImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationImportDto()
    {
        PlantCode = string.Empty;
        OperationNo = string.Empty;
        OperationMonth = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 品质业务编号
    /// </summary>
    public string OperationNo { get; set; }

        /// <summary>
    /// 业务年月
    /// </summary>
    public string OperationMonth { get; set; }

        /// <summary>
    /// 顾客名
    /// </summary>
    public string? CustomerName { get; set; }

        /// <summary>
    /// Debit Note No
    /// </summary>
    public string? DebitNoteNo { get; set; }

        /// <summary>
    /// 记录者
    /// </summary>
    public string? Recorder { get; set; }

        /// <summary>
    /// 质量总成本
    /// </summary>
    public decimal TotalQualityCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

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
/// 品质业务主表导出DTO
/// </summary>
public partial class TaktQualityOperationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        OperationNo = string.Empty;
        OperationMonth = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 品质业务编号
    /// </summary>
    public string OperationNo { get; set; }

        /// <summary>
    /// 业务年月
    /// </summary>
    public string OperationMonth { get; set; }

        /// <summary>
    /// 顾客名
    /// </summary>
    public string? CustomerName { get; set; }

        /// <summary>
    /// Debit Note No
    /// </summary>
    public string? DebitNoteNo { get; set; }

        /// <summary>
    /// 记录者
    /// </summary>
    public string? Recorder { get; set; }

        /// <summary>
    /// 质量总成本
    /// </summary>
    public decimal TotalQualityCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}