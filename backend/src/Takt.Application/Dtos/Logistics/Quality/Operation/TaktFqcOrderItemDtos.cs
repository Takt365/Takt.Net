// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderItemDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：出货检验单明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 出货检验单明细表Dto
/// </summary>
public partial class TaktFqcOrderItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
    }

    /// <summary>
    /// 出货检验单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderItemId { get; set; } = 0;

    /// <summary>
    /// FQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderId { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 检验项目编码
    /// </summary>
    public string ItemCode { get; set; }
    /// <summary>
    /// 检验项目名称
    /// </summary>
    public string ItemName { get; set; }
    /// <summary>
    /// 检验项目类型
    /// </summary>
    public int ItemType { get; set; }
    /// <summary>
    /// 检验标准值
    /// </summary>
    public string? StandardValue { get; set; }
    /// <summary>
    /// 检验上限值
    /// </summary>
    public string? UpperLimit { get; set; }
    /// <summary>
    /// 检验下限值
    /// </summary>
    public string? LowerLimit { get; set; }
    /// <summary>
    /// 检验工具
    /// </summary>
    public string? InspectionTool { get; set; }
    /// <summary>
    /// 检验方法
    /// </summary>
    public string? InspectionMethod { get; set; }
    /// <summary>
    /// 实际检验值
    /// </summary>
    public string? ActualValue { get; set; }
    /// <summary>
    /// 检验结果
    /// </summary>
    public int InspectionResult { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }
    /// <summary>
    /// 不良描述
    /// </summary>
    public string? DefectDescription { get; set; }
    /// <summary>
    /// 检验员
    /// </summary>
    public string? InspectorBy { get; set; }
    /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? InspectionTime { get; set; }
    /// <summary>
    /// 检验图片
    /// </summary>
    public string? InspectionImages { get; set; }

    /// <summary>
    /// FQC检验单（主表）
    /// </summary>
    public TaktFqcOrderDto? Order { get; set; }

    /// <summary>
    /// 不良处理记录列表（主子表关系）（外键在子表 TaktFqcDefectHandlingDto.FqcOrderItemId）
    /// </summary>
    public List<TaktFqcDefectHandlingDto>? DefectHandlings { get; set; }
}

/// <summary>
/// 出货检验单明细表查询DTO
/// </summary>
public partial class TaktFqcOrderItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// FQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FqcOrderId { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 检验项目编码
    /// </summary>
    public string? ItemCode { get; set; }
    /// <summary>
    /// 检验项目名称
    /// </summary>
    public string? ItemName { get; set; }
    /// <summary>
    /// 检验项目类型
    /// </summary>
    public int? ItemType { get; set; }
    /// <summary>
    /// 检验标准值
    /// </summary>
    public string? StandardValue { get; set; }
    /// <summary>
    /// 检验上限值
    /// </summary>
    public string? UpperLimit { get; set; }
    /// <summary>
    /// 检验下限值
    /// </summary>
    public string? LowerLimit { get; set; }
    /// <summary>
    /// 检验工具
    /// </summary>
    public string? InspectionTool { get; set; }
    /// <summary>
    /// 检验方法
    /// </summary>
    public string? InspectionMethod { get; set; }
    /// <summary>
    /// 实际检验值
    /// </summary>
    public string? ActualValue { get; set; }
    /// <summary>
    /// 检验结果
    /// </summary>
    public int? InspectionResult { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public int? DefectQuantity { get; set; }
    /// <summary>
    /// 不良描述
    /// </summary>
    public string? DefectDescription { get; set; }
    /// <summary>
    /// 检验员
    /// </summary>
    public string? InspectorBy { get; set; }
    /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? InspectionTime { get; set; }

    /// <summary>
    /// 检验时间开始时间
    /// </summary>
    public DateTime? InspectionTimeStart { get; set; }
    /// <summary>
    /// 检验时间结束时间
    /// </summary>
    public DateTime? InspectionTimeEnd { get; set; }
    /// <summary>
    /// 检验图片
    /// </summary>
    public string? InspectionImages { get; set; }

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
/// Takt创建出货检验单明细表DTO
/// </summary>
public partial class TaktFqcOrderItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemCreateDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// FQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 检验项目编码
    /// </summary>
    public string ItemCode { get; set; }

        /// <summary>
    /// 检验项目名称
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 检验项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string? StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string? UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string? LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string? InspectionTool { get; set; }

        /// <summary>
    /// 检验方法
    /// </summary>
    public string? InspectionMethod { get; set; }

        /// <summary>
    /// 实际检验值
    /// </summary>
    public string? ActualValue { get; set; }

        /// <summary>
    /// 检验结果
    /// </summary>
    public int InspectionResult { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良描述
    /// </summary>
    public string? DefectDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string? InspectorBy { get; set; }

        /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? InspectionTime { get; set; }

        /// <summary>
    /// 检验图片
    /// </summary>
    public string? InspectionImages { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 不良处理记录列表（主子表关系）（外键在子表 TaktFqcDefectHandlingCreateDto.FqcOrderItemId）
    /// </summary>
    public List<TaktFqcDefectHandlingCreateDto>? DefectHandlings { get; set; }

}

/// <summary>
/// Takt更新出货检验单明细表DTO
/// </summary>
public partial class TaktFqcOrderItemUpdateDto : TaktFqcOrderItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemUpdateDto()
    {
    }

        /// <summary>
    /// 出货检验单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderItemId { get; set; } = 0;
}

/// <summary>
/// 出货检验单明细表导入模板DTO
/// </summary>
public partial class TaktFqcOrderItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemTemplateDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// FQC检验单ID
    /// </summary>
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 检验项目编码
    /// </summary>
    public string ItemCode { get; set; }

        /// <summary>
    /// 检验项目名称
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 检验项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string? StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string? UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string? LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string? InspectionTool { get; set; }

        /// <summary>
    /// 检验方法
    /// </summary>
    public string? InspectionMethod { get; set; }

        /// <summary>
    /// 实际检验值
    /// </summary>
    public string? ActualValue { get; set; }

        /// <summary>
    /// 检验结果
    /// </summary>
    public int InspectionResult { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良描述
    /// </summary>
    public string? DefectDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string? InspectorBy { get; set; }

        /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? InspectionTime { get; set; }

        /// <summary>
    /// 检验图片
    /// </summary>
    public string? InspectionImages { get; set; }

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
/// 出货检验单明细表导入DTO
/// </summary>
public partial class TaktFqcOrderItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemImportDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// FQC检验单ID
    /// </summary>
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 检验项目编码
    /// </summary>
    public string ItemCode { get; set; }

        /// <summary>
    /// 检验项目名称
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 检验项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string? StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string? UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string? LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string? InspectionTool { get; set; }

        /// <summary>
    /// 检验方法
    /// </summary>
    public string? InspectionMethod { get; set; }

        /// <summary>
    /// 实际检验值
    /// </summary>
    public string? ActualValue { get; set; }

        /// <summary>
    /// 检验结果
    /// </summary>
    public int InspectionResult { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良描述
    /// </summary>
    public string? DefectDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string? InspectorBy { get; set; }

        /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? InspectionTime { get; set; }

        /// <summary>
    /// 检验图片
    /// </summary>
    public string? InspectionImages { get; set; }

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
/// 出货检验单明细表导出DTO
/// </summary>
public partial class TaktFqcOrderItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemExportDto()
    {
        CreatedAt = DateTime.Now;
        ItemCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// FQC检验单ID
    /// </summary>
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 检验项目编码
    /// </summary>
    public string ItemCode { get; set; }

        /// <summary>
    /// 检验项目名称
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 检验项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string? StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string? UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string? LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string? InspectionTool { get; set; }

        /// <summary>
    /// 检验方法
    /// </summary>
    public string? InspectionMethod { get; set; }

        /// <summary>
    /// 实际检验值
    /// </summary>
    public string? ActualValue { get; set; }

        /// <summary>
    /// 检验结果
    /// </summary>
    public int InspectionResult { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良描述
    /// </summary>
    public string? DefectDescription { get; set; }

        /// <summary>
    /// 检验员
    /// </summary>
    public string? InspectorBy { get; set; }

        /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? InspectionTime { get; set; }

        /// <summary>
    /// 检验图片
    /// </summary>
    public string? InspectionImages { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}