// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktCostElementDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：成本要素表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// 成本要素表Dto
/// </summary>
public partial class TaktCostElementDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementDto()
    {
        CostElementCode = string.Empty;
        CostElementName = string.Empty;
    }

    /// <summary>
    /// 成本要素表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; }
    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; }
    /// <summary>
    /// 成本要素类型
    /// </summary>
    public int CostElementType { get; set; }
    /// <summary>
    /// 成本要素类别
    /// </summary>
    public int CostElementCategory { get; set; }
    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }
    /// <summary>
    /// 成本要素层级
    /// </summary>
    public int CostElementLevel { get; set; }
    /// <summary>
    /// 成本要素状态
    /// </summary>
    public int CostElementStatus { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 成本要素表树形DTO
/// </summary>
public partial class TaktCostElementTreeDto : TaktCostElementDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementTreeDto()
    {
        Children = new List<TaktCostElementTreeDto>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktCostElementTreeDto> Children { get; set; }
}

/// <summary>
/// 成本要素表查询DTO
/// </summary>
public partial class TaktCostElementQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 成本要素表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string? CostElementCode { get; set; }
    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string? CostElementName { get; set; }
    /// <summary>
    /// 成本要素类型
    /// </summary>
    public int? CostElementType { get; set; }
    /// <summary>
    /// 成本要素类别
    /// </summary>
    public int? CostElementCategory { get; set; }
    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }
    /// <summary>
    /// 成本要素层级
    /// </summary>
    public int? CostElementLevel { get; set; }
    /// <summary>
    /// 成本要素状态
    /// </summary>
    public int? CostElementStatus { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? ValidFromStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? ValidFromEnd { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// 失效日期开始时间
    /// </summary>
    public DateTime? ValidToStart { get; set; }
    /// <summary>
    /// 失效日期结束时间
    /// </summary>
    public DateTime? ValidToEnd { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建成本要素表DTO
/// </summary>
public partial class TaktCostElementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementCreateDto()
    {
        CostElementCode = string.Empty;
        CostElementName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; }

        /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; }

        /// <summary>
    /// 成本要素类型
    /// </summary>
    public int CostElementType { get; set; }

        /// <summary>
    /// 成本要素类别
    /// </summary>
    public int CostElementCategory { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

        /// <summary>
    /// 成本要素层级
    /// </summary>
    public int CostElementLevel { get; set; }

        /// <summary>
    /// 成本要素状态
    /// </summary>
    public int CostElementStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新成本要素表DTO
/// </summary>
public partial class TaktCostElementUpdateDto : TaktCostElementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementUpdateDto()
    {
    }

        /// <summary>
    /// 成本要素表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }
}

/// <summary>
/// 成本要素表成本要素状态DTO
/// </summary>
public partial class TaktCostElementStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementStatusDto()
    {
    }

        /// <summary>
    /// 成本要素表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }

    /// <summary>
    /// 成本要素状态（0=禁用，1=启用）
    /// </summary>
    public int CostElementStatus { get; set; }
}

/// <summary>
/// 成本要素表导入模板DTO
/// </summary>
public partial class TaktCostElementTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementTemplateDto()
    {
        CostElementCode = string.Empty;
        CostElementName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; }

        /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; }

        /// <summary>
    /// 成本要素类型
    /// </summary>
    public int CostElementType { get; set; }

        /// <summary>
    /// 成本要素类别
    /// </summary>
    public int CostElementCategory { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 成本要素层级
    /// </summary>
    public int CostElementLevel { get; set; }

        /// <summary>
    /// 成本要素状态
    /// </summary>
    public int CostElementStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 成本要素表导入DTO
/// </summary>
public partial class TaktCostElementImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementImportDto()
    {
        CostElementCode = string.Empty;
        CostElementName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; }

        /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; }

        /// <summary>
    /// 成本要素类型
    /// </summary>
    public int CostElementType { get; set; }

        /// <summary>
    /// 成本要素类别
    /// </summary>
    public int CostElementCategory { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 成本要素层级
    /// </summary>
    public int CostElementLevel { get; set; }

        /// <summary>
    /// 成本要素状态
    /// </summary>
    public int CostElementStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 成本要素表导出DTO
/// </summary>
public partial class TaktCostElementExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementExportDto()
    {
        CreatedAt = DateTime.Now;
        CostElementCode = string.Empty;
        CostElementName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; }

        /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; }

        /// <summary>
    /// 成本要素类型
    /// </summary>
    public int CostElementType { get; set; }

        /// <summary>
    /// 成本要素类别
    /// </summary>
    public int CostElementCategory { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 成本要素层级
    /// </summary>
    public int CostElementLevel { get; set; }

        /// <summary>
    /// 成本要素状态
    /// </summary>
    public int CostElementStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}