// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktSamplingSchemeDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：抽样方案表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 抽样方案表Dto
/// </summary>
public partial class TaktSamplingSchemeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeDto()
    {
        PlantCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        SamplingSchemeName = string.Empty;
    }

    /// <summary>
    /// 抽样方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SamplingSchemeId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }
    /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string SamplingSchemeName { get; set; }
    /// <summary>
    /// 抽样方案类型
    /// </summary>
    public int SamplingSchemeType { get; set; }
    /// <summary>
    /// 抽样标准
    /// </summary>
    public int SamplingStandard { get; set; }
    /// <summary>
    /// 检验水平
    /// </summary>
    public int InspectionLevel { get; set; }
    /// <summary>
    /// AQL值
    /// </summary>
    public decimal AqlValue { get; set; }
    /// <summary>
    /// 批量范围最小值
    /// </summary>
    public int LotSizeMin { get; set; }
    /// <summary>
    /// 批量范围最大值
    /// </summary>
    public int LotSizeMax { get; set; }
    /// <summary>
    /// 样本量
    /// </summary>
    public int SampleSize { get; set; }
    /// <summary>
    /// 接收数
    /// </summary>
    public int AcceptanceNumber { get; set; }
    /// <summary>
    /// 拒收数
    /// </summary>
    public int RejectionNumber { get; set; }
    /// <summary>
    /// 检验严格度
    /// </summary>
    public int InspectionStrictness { get; set; }
    /// <summary>
    /// 是否支持转移规则
    /// </summary>
    public int IsTransferRuleEnabled { get; set; }
    /// <summary>
    /// 转移规则配置
    /// </summary>
    public string? TransferRuleConfig { get; set; }
    /// <summary>
    /// 抽样方案状态
    /// </summary>
    public int SamplingSchemeStatus { get; set; }
    /// <summary>
    /// 抽样方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

    /// <summary>
    /// 检验标准列表（主子表关系）（外键在子表 TaktInspectionStandardDto.SamplingSchemeCode）
    /// </summary>
    public List<TaktInspectionStandardDto>? InspectionStandards { get; set; }
}

/// <summary>
/// 抽样方案表查询DTO
/// </summary>
public partial class TaktSamplingSchemeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }
    /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string? SamplingSchemeName { get; set; }
    /// <summary>
    /// 抽样方案类型
    /// </summary>
    public int? SamplingSchemeType { get; set; }
    /// <summary>
    /// 抽样标准
    /// </summary>
    public int? SamplingStandard { get; set; }
    /// <summary>
    /// 检验水平
    /// </summary>
    public int? InspectionLevel { get; set; }
    /// <summary>
    /// AQL值
    /// </summary>
    public decimal? AqlValue { get; set; }
    /// <summary>
    /// 批量范围最小值
    /// </summary>
    public int? LotSizeMin { get; set; }
    /// <summary>
    /// 批量范围最大值
    /// </summary>
    public int? LotSizeMax { get; set; }
    /// <summary>
    /// 样本量
    /// </summary>
    public int? SampleSize { get; set; }
    /// <summary>
    /// 接收数
    /// </summary>
    public int? AcceptanceNumber { get; set; }
    /// <summary>
    /// 拒收数
    /// </summary>
    public int? RejectionNumber { get; set; }
    /// <summary>
    /// 检验严格度
    /// </summary>
    public int? InspectionStrictness { get; set; }
    /// <summary>
    /// 是否支持转移规则
    /// </summary>
    public int? IsTransferRuleEnabled { get; set; }
    /// <summary>
    /// 转移规则配置
    /// </summary>
    public string? TransferRuleConfig { get; set; }
    /// <summary>
    /// 抽样方案状态
    /// </summary>
    public int? SamplingSchemeStatus { get; set; }
    /// <summary>
    /// 抽样方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

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
/// Takt创建抽样方案表DTO
/// </summary>
public partial class TaktSamplingSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeCreateDto()
    {
        PlantCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        SamplingSchemeName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string SamplingSchemeName { get; set; }

        /// <summary>
    /// 抽样方案类型
    /// </summary>
    public int SamplingSchemeType { get; set; }

        /// <summary>
    /// 抽样标准
    /// </summary>
    public int SamplingStandard { get; set; }

        /// <summary>
    /// 检验水平
    /// </summary>
    public int InspectionLevel { get; set; }

        /// <summary>
    /// AQL值
    /// </summary>
    public decimal AqlValue { get; set; }

        /// <summary>
    /// 批量范围最小值
    /// </summary>
    public int LotSizeMin { get; set; }

        /// <summary>
    /// 批量范围最大值
    /// </summary>
    public int LotSizeMax { get; set; }

        /// <summary>
    /// 样本量
    /// </summary>
    public int SampleSize { get; set; }

        /// <summary>
    /// 接收数
    /// </summary>
    public int AcceptanceNumber { get; set; }

        /// <summary>
    /// 拒收数
    /// </summary>
    public int RejectionNumber { get; set; }

        /// <summary>
    /// 检验严格度
    /// </summary>
    public int InspectionStrictness { get; set; }

        /// <summary>
    /// 是否支持转移规则
    /// </summary>
    public int IsTransferRuleEnabled { get; set; }

        /// <summary>
    /// 转移规则配置
    /// </summary>
    public string? TransferRuleConfig { get; set; }

        /// <summary>
    /// 抽样方案状态
    /// </summary>
    public int SamplingSchemeStatus { get; set; }

        /// <summary>
    /// 抽样方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 检验标准列表（主子表关系）（外键在子表 TaktInspectionStandardCreateDto.SamplingSchemeCode）
    /// </summary>
    public List<TaktInspectionStandardCreateDto>? InspectionStandards { get; set; }

}

/// <summary>
/// Takt更新抽样方案表DTO
/// </summary>
public partial class TaktSamplingSchemeUpdateDto : TaktSamplingSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeUpdateDto()
    {
    }

        /// <summary>
    /// 抽样方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SamplingSchemeId { get; set; } = 0;
}

/// <summary>
/// 抽样方案表抽样方案状态DTO
/// </summary>
public partial class TaktSamplingSchemeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeStatusDto()
    {
    }

        /// <summary>
    /// 抽样方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SamplingSchemeId { get; set; } = 0;

    /// <summary>
    /// 抽样方案状态（0=禁用，1=启用）
    /// </summary>
    public int SamplingSchemeStatus { get; set; }
}

/// <summary>
/// 抽样方案表导入模板DTO
/// </summary>
public partial class TaktSamplingSchemeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeTemplateDto()
    {
        PlantCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        SamplingSchemeName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string SamplingSchemeName { get; set; }

        /// <summary>
    /// 抽样方案类型
    /// </summary>
    public int SamplingSchemeType { get; set; }

        /// <summary>
    /// 抽样标准
    /// </summary>
    public int SamplingStandard { get; set; }

        /// <summary>
    /// 检验水平
    /// </summary>
    public int InspectionLevel { get; set; }

        /// <summary>
    /// AQL值
    /// </summary>
    public decimal AqlValue { get; set; }

        /// <summary>
    /// 批量范围最小值
    /// </summary>
    public int LotSizeMin { get; set; }

        /// <summary>
    /// 批量范围最大值
    /// </summary>
    public int LotSizeMax { get; set; }

        /// <summary>
    /// 样本量
    /// </summary>
    public int SampleSize { get; set; }

        /// <summary>
    /// 接收数
    /// </summary>
    public int AcceptanceNumber { get; set; }

        /// <summary>
    /// 拒收数
    /// </summary>
    public int RejectionNumber { get; set; }

        /// <summary>
    /// 检验严格度
    /// </summary>
    public int InspectionStrictness { get; set; }

        /// <summary>
    /// 是否支持转移规则
    /// </summary>
    public int IsTransferRuleEnabled { get; set; }

        /// <summary>
    /// 转移规则配置
    /// </summary>
    public string? TransferRuleConfig { get; set; }

        /// <summary>
    /// 抽样方案状态
    /// </summary>
    public int SamplingSchemeStatus { get; set; }

        /// <summary>
    /// 抽样方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

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
/// 抽样方案表导入DTO
/// </summary>
public partial class TaktSamplingSchemeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeImportDto()
    {
        PlantCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        SamplingSchemeName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string SamplingSchemeName { get; set; }

        /// <summary>
    /// 抽样方案类型
    /// </summary>
    public int SamplingSchemeType { get; set; }

        /// <summary>
    /// 抽样标准
    /// </summary>
    public int SamplingStandard { get; set; }

        /// <summary>
    /// 检验水平
    /// </summary>
    public int InspectionLevel { get; set; }

        /// <summary>
    /// AQL值
    /// </summary>
    public decimal AqlValue { get; set; }

        /// <summary>
    /// 批量范围最小值
    /// </summary>
    public int LotSizeMin { get; set; }

        /// <summary>
    /// 批量范围最大值
    /// </summary>
    public int LotSizeMax { get; set; }

        /// <summary>
    /// 样本量
    /// </summary>
    public int SampleSize { get; set; }

        /// <summary>
    /// 接收数
    /// </summary>
    public int AcceptanceNumber { get; set; }

        /// <summary>
    /// 拒收数
    /// </summary>
    public int RejectionNumber { get; set; }

        /// <summary>
    /// 检验严格度
    /// </summary>
    public int InspectionStrictness { get; set; }

        /// <summary>
    /// 是否支持转移规则
    /// </summary>
    public int IsTransferRuleEnabled { get; set; }

        /// <summary>
    /// 转移规则配置
    /// </summary>
    public string? TransferRuleConfig { get; set; }

        /// <summary>
    /// 抽样方案状态
    /// </summary>
    public int SamplingSchemeStatus { get; set; }

        /// <summary>
    /// 抽样方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

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
/// 抽样方案表导出DTO
/// </summary>
public partial class TaktSamplingSchemeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemeExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        SamplingSchemeCode = string.Empty;
        SamplingSchemeName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string SamplingSchemeName { get; set; }

        /// <summary>
    /// 抽样方案类型
    /// </summary>
    public int SamplingSchemeType { get; set; }

        /// <summary>
    /// 抽样标准
    /// </summary>
    public int SamplingStandard { get; set; }

        /// <summary>
    /// 检验水平
    /// </summary>
    public int InspectionLevel { get; set; }

        /// <summary>
    /// AQL值
    /// </summary>
    public decimal AqlValue { get; set; }

        /// <summary>
    /// 批量范围最小值
    /// </summary>
    public int LotSizeMin { get; set; }

        /// <summary>
    /// 批量范围最大值
    /// </summary>
    public int LotSizeMax { get; set; }

        /// <summary>
    /// 样本量
    /// </summary>
    public int SampleSize { get; set; }

        /// <summary>
    /// 接收数
    /// </summary>
    public int AcceptanceNumber { get; set; }

        /// <summary>
    /// 拒收数
    /// </summary>
    public int RejectionNumber { get; set; }

        /// <summary>
    /// 检验严格度
    /// </summary>
    public int InspectionStrictness { get; set; }

        /// <summary>
    /// 是否支持转移规则
    /// </summary>
    public int IsTransferRuleEnabled { get; set; }

        /// <summary>
    /// 转移规则配置
    /// </summary>
    public string? TransferRuleConfig { get; set; }

        /// <summary>
    /// 抽样方案状态
    /// </summary>
    public int SamplingSchemeStatus { get; set; }

        /// <summary>
    /// 抽样方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}