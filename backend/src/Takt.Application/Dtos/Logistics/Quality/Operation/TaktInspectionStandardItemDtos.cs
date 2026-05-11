// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardItemDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：检验标准明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 检验标准明细表Dto
/// </summary>
public partial class TaktInspectionStandardItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
        DefectLevel = string.Empty;
        StandardValue = string.Empty;
        UpperLimit = string.Empty;
        LowerLimit = string.Empty;
        InspectionTool = string.Empty;
        InspectionMethodDescription = string.Empty;
        AcceptanceCriteria = string.Empty;
        RejectionCriteria = string.Empty;
    }

    /// <summary>
    /// 检验标准明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InspectionStandardItemId { get; set; } = 0;

    /// <summary>
    /// 检验标准ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InspectionStandardId { get; set; }
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
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }
    /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMode { get; set; }
    /// <summary>
    /// 检验标准值
    /// </summary>
    public string StandardValue { get; set; }
    /// <summary>
    /// 检验上限值
    /// </summary>
    public string UpperLimit { get; set; }
    /// <summary>
    /// 检验下限值
    /// </summary>
    public string LowerLimit { get; set; }
    /// <summary>
    /// 检验工具
    /// </summary>
    public string InspectionTool { get; set; }
    /// <summary>
    /// 检验方法说明
    /// </summary>
    public string InspectionMethodDescription { get; set; }
    /// <summary>
    /// 接收标准(AC值)
    /// </summary>
    public string AcceptanceCriteria { get; set; }
    /// <summary>
    /// 拒收标准(RE值)
    /// </summary>
    public string RejectionCriteria { get; set; }
    /// <summary>
    /// 是否合格判定项目
    /// </summary>
    public int IsQualifiedBasis { get; set; }

    /// <summary>
    /// 检验标准（主表）
    /// </summary>
    public TaktInspectionStandardDto? Standard { get; set; }
}

/// <summary>
/// 检验标准明细表查询DTO
/// </summary>
public partial class TaktInspectionStandardItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 检验标准ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? InspectionStandardId { get; set; }
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
    /// 缺点等级
    /// </summary>
    public string? DefectLevel { get; set; }
    /// <summary>
    /// 检验方式
    /// </summary>
    public int? InspectionMode { get; set; }
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
    /// 检验方法说明
    /// </summary>
    public string? InspectionMethodDescription { get; set; }
    /// <summary>
    /// 接收标准(AC值)
    /// </summary>
    public string? AcceptanceCriteria { get; set; }
    /// <summary>
    /// 拒收标准(RE值)
    /// </summary>
    public string? RejectionCriteria { get; set; }
    /// <summary>
    /// 是否合格判定项目
    /// </summary>
    public int? IsQualifiedBasis { get; set; }

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
/// Takt创建检验标准明细表DTO
/// </summary>
public partial class TaktInspectionStandardItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemCreateDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
        DefectLevel = string.Empty;
        StandardValue = string.Empty;
        UpperLimit = string.Empty;
        LowerLimit = string.Empty;
        InspectionTool = string.Empty;
        InspectionMethodDescription = string.Empty;
        AcceptanceCriteria = string.Empty;
        RejectionCriteria = string.Empty;
    }

        /// <summary>
    /// 检验标准ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InspectionStandardId { get; set; }

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
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMode { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string InspectionTool { get; set; }

        /// <summary>
    /// 检验方法说明
    /// </summary>
    public string InspectionMethodDescription { get; set; }

        /// <summary>
    /// 接收标准(AC值)
    /// </summary>
    public string AcceptanceCriteria { get; set; }

        /// <summary>
    /// 拒收标准(RE值)
    /// </summary>
    public string RejectionCriteria { get; set; }

        /// <summary>
    /// 是否合格判定项目
    /// </summary>
    public int IsQualifiedBasis { get; set; }

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
/// Takt更新检验标准明细表DTO
/// </summary>
public partial class TaktInspectionStandardItemUpdateDto : TaktInspectionStandardItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemUpdateDto()
    {
    }

        /// <summary>
    /// 检验标准明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InspectionStandardItemId { get; set; } = 0;
}

/// <summary>
/// 检验标准明细表导入模板DTO
/// </summary>
public partial class TaktInspectionStandardItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemTemplateDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
        DefectLevel = string.Empty;
        StandardValue = string.Empty;
        UpperLimit = string.Empty;
        LowerLimit = string.Empty;
        InspectionTool = string.Empty;
        InspectionMethodDescription = string.Empty;
        AcceptanceCriteria = string.Empty;
        RejectionCriteria = string.Empty;
    }

        /// <summary>
    /// 检验标准ID
    /// </summary>
    public long InspectionStandardId { get; set; }

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
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMode { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string InspectionTool { get; set; }

        /// <summary>
    /// 检验方法说明
    /// </summary>
    public string InspectionMethodDescription { get; set; }

        /// <summary>
    /// 接收标准(AC值)
    /// </summary>
    public string AcceptanceCriteria { get; set; }

        /// <summary>
    /// 拒收标准(RE值)
    /// </summary>
    public string RejectionCriteria { get; set; }

        /// <summary>
    /// 是否合格判定项目
    /// </summary>
    public int IsQualifiedBasis { get; set; }

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
/// 检验标准明细表导入DTO
/// </summary>
public partial class TaktInspectionStandardItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemImportDto()
    {
        ItemCode = string.Empty;
        ItemName = string.Empty;
        DefectLevel = string.Empty;
        StandardValue = string.Empty;
        UpperLimit = string.Empty;
        LowerLimit = string.Empty;
        InspectionTool = string.Empty;
        InspectionMethodDescription = string.Empty;
        AcceptanceCriteria = string.Empty;
        RejectionCriteria = string.Empty;
    }

        /// <summary>
    /// 检验标准ID
    /// </summary>
    public long InspectionStandardId { get; set; }

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
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMode { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string InspectionTool { get; set; }

        /// <summary>
    /// 检验方法说明
    /// </summary>
    public string InspectionMethodDescription { get; set; }

        /// <summary>
    /// 接收标准(AC值)
    /// </summary>
    public string AcceptanceCriteria { get; set; }

        /// <summary>
    /// 拒收标准(RE值)
    /// </summary>
    public string RejectionCriteria { get; set; }

        /// <summary>
    /// 是否合格判定项目
    /// </summary>
    public int IsQualifiedBasis { get; set; }

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
/// 检验标准明细表导出DTO
/// </summary>
public partial class TaktInspectionStandardItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemExportDto()
    {
        CreatedAt = DateTime.Now;
        ItemCode = string.Empty;
        ItemName = string.Empty;
        DefectLevel = string.Empty;
        StandardValue = string.Empty;
        UpperLimit = string.Empty;
        LowerLimit = string.Empty;
        InspectionTool = string.Empty;
        InspectionMethodDescription = string.Empty;
        AcceptanceCriteria = string.Empty;
        RejectionCriteria = string.Empty;
    }

        /// <summary>
    /// 检验标准ID
    /// </summary>
    public long InspectionStandardId { get; set; }

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
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 检验方式
    /// </summary>
    public int InspectionMode { get; set; }

        /// <summary>
    /// 检验标准值
    /// </summary>
    public string StandardValue { get; set; }

        /// <summary>
    /// 检验上限值
    /// </summary>
    public string UpperLimit { get; set; }

        /// <summary>
    /// 检验下限值
    /// </summary>
    public string LowerLimit { get; set; }

        /// <summary>
    /// 检验工具
    /// </summary>
    public string InspectionTool { get; set; }

        /// <summary>
    /// 检验方法说明
    /// </summary>
    public string InspectionMethodDescription { get; set; }

        /// <summary>
    /// 接收标准(AC值)
    /// </summary>
    public string AcceptanceCriteria { get; set; }

        /// <summary>
    /// 拒收标准(RE值)
    /// </summary>
    public string RejectionCriteria { get; set; }

        /// <summary>
    /// 是否合格判定项目
    /// </summary>
    public int IsQualifiedBasis { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}