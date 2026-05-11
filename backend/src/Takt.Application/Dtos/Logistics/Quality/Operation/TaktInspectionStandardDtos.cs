// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：检验标准表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 检验标准表Dto
/// </summary>
public partial class TaktInspectionStandardDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardDto()
    {
        StandardCode = string.Empty;
        StandardName = string.Empty;
        MaterialCategoryCode = string.Empty;
        MaterialCategoryName = string.Empty;
    }

    /// <summary>
    /// 检验标准表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InspectionStandardId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }
    /// <summary>
    /// 检验标准名称
    /// </summary>
    public string StandardName { get; set; }
    /// <summary>
    /// 检验类型
    /// </summary>
    public int InspectionType { get; set; }
    /// <summary>
    /// 物料类别编码
    /// </summary>
    public string MaterialCategoryCode { get; set; }
    /// <summary>
    /// 物料类别名称
    /// </summary>
    public string MaterialCategoryName { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }
    /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string? SamplingSchemeName { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }
    /// <summary>
    /// 检验标准状态
    /// </summary>
    public int StandardStatus { get; set; }
    /// <summary>
    /// 检验标准描述
    /// </summary>
    public string? StandardDescription { get; set; }

    /// <summary>
    /// 检验标准明细列表（主子表关系）（外键在子表 TaktInspectionStandardItemDto.InspectionStandardId）
    /// </summary>
    public List<TaktInspectionStandardItemDto>? Items { get; set; }
}

/// <summary>
/// 检验标准表查询DTO
/// </summary>
public partial class TaktInspectionStandardQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 检验标准编码
    /// </summary>
    public string? StandardCode { get; set; }
    /// <summary>
    /// 检验标准名称
    /// </summary>
    public string? StandardName { get; set; }
    /// <summary>
    /// 检验类型
    /// </summary>
    public int? InspectionType { get; set; }
    /// <summary>
    /// 物料类别编码
    /// </summary>
    public string? MaterialCategoryCode { get; set; }
    /// <summary>
    /// 物料类别名称
    /// </summary>
    public string? MaterialCategoryName { get; set; }
    /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }
    /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string? SamplingSchemeName { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public int? IsEnabled { get; set; }
    /// <summary>
    /// 检验标准状态
    /// </summary>
    public int? StandardStatus { get; set; }
    /// <summary>
    /// 检验标准描述
    /// </summary>
    public string? StandardDescription { get; set; }

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
/// Takt创建检验标准表DTO
/// </summary>
public partial class TaktInspectionStandardCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardCreateDto()
    {
        StandardCode = string.Empty;
        StandardName = string.Empty;
        MaterialCategoryCode = string.Empty;
        MaterialCategoryName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 检验标准名称
    /// </summary>
    public string StandardName { get; set; }

        /// <summary>
    /// 检验类型
    /// </summary>
    public int InspectionType { get; set; }

        /// <summary>
    /// 物料类别编码
    /// </summary>
    public string MaterialCategoryCode { get; set; }

        /// <summary>
    /// 物料类别名称
    /// </summary>
    public string MaterialCategoryName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string? SamplingSchemeName { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// 检验标准状态
    /// </summary>
    public int StandardStatus { get; set; }

        /// <summary>
    /// 检验标准描述
    /// </summary>
    public string? StandardDescription { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 检验标准明细列表（主子表关系）（外键在子表 TaktInspectionStandardItemCreateDto.InspectionStandardId）
    /// </summary>
    public List<TaktInspectionStandardItemCreateDto>? Items { get; set; }

}

/// <summary>
/// Takt更新检验标准表DTO
/// </summary>
public partial class TaktInspectionStandardUpdateDto : TaktInspectionStandardCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardUpdateDto()
    {
    }

        /// <summary>
    /// 检验标准表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InspectionStandardId { get; set; } = 0;
}

/// <summary>
/// 检验标准表检验标准状态DTO
/// </summary>
public partial class TaktInspectionStandardStandardStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardStandardStatusDto()
    {
    }

        /// <summary>
    /// 检验标准表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InspectionStandardId { get; set; } = 0;

    /// <summary>
    /// 检验标准状态（0=禁用，1=启用）
    /// </summary>
    public int StandardStatus { get; set; }
}

/// <summary>
/// 检验标准表导入模板DTO
/// </summary>
public partial class TaktInspectionStandardTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardTemplateDto()
    {
        StandardCode = string.Empty;
        StandardName = string.Empty;
        MaterialCategoryCode = string.Empty;
        MaterialCategoryName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 检验标准名称
    /// </summary>
    public string StandardName { get; set; }

        /// <summary>
    /// 检验类型
    /// </summary>
    public int InspectionType { get; set; }

        /// <summary>
    /// 物料类别编码
    /// </summary>
    public string MaterialCategoryCode { get; set; }

        /// <summary>
    /// 物料类别名称
    /// </summary>
    public string MaterialCategoryName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string? SamplingSchemeName { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// 检验标准状态
    /// </summary>
    public int StandardStatus { get; set; }

        /// <summary>
    /// 检验标准描述
    /// </summary>
    public string? StandardDescription { get; set; }

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
/// 检验标准表导入DTO
/// </summary>
public partial class TaktInspectionStandardImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardImportDto()
    {
        StandardCode = string.Empty;
        StandardName = string.Empty;
        MaterialCategoryCode = string.Empty;
        MaterialCategoryName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 检验标准名称
    /// </summary>
    public string StandardName { get; set; }

        /// <summary>
    /// 检验类型
    /// </summary>
    public int InspectionType { get; set; }

        /// <summary>
    /// 物料类别编码
    /// </summary>
    public string MaterialCategoryCode { get; set; }

        /// <summary>
    /// 物料类别名称
    /// </summary>
    public string MaterialCategoryName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string? SamplingSchemeName { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// 检验标准状态
    /// </summary>
    public int StandardStatus { get; set; }

        /// <summary>
    /// 检验标准描述
    /// </summary>
    public string? StandardDescription { get; set; }

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
/// 检验标准表导出DTO
/// </summary>
public partial class TaktInspectionStandardExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardExportDto()
    {
        CreatedAt = DateTime.Now;
        StandardCode = string.Empty;
        StandardName = string.Empty;
        MaterialCategoryCode = string.Empty;
        MaterialCategoryName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 检验标准编码
    /// </summary>
    public string StandardCode { get; set; }

        /// <summary>
    /// 检验标准名称
    /// </summary>
    public string StandardName { get; set; }

        /// <summary>
    /// 检验类型
    /// </summary>
    public int InspectionType { get; set; }

        /// <summary>
    /// 物料类别编码
    /// </summary>
    public string MaterialCategoryCode { get; set; }

        /// <summary>
    /// 物料类别名称
    /// </summary>
    public string MaterialCategoryName { get; set; }

        /// <summary>
    /// 抽样方案编码
    /// </summary>
    public string? SamplingSchemeCode { get; set; }

        /// <summary>
    /// 抽样方案名称
    /// </summary>
    public string? SamplingSchemeName { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// 检验标准状态
    /// </summary>
    public int StandardStatus { get; set; }

        /// <summary>
    /// 检验标准描述
    /// </summary>
    public string? StandardDescription { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}