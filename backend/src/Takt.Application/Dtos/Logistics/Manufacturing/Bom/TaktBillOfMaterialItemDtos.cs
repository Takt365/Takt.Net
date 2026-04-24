// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialItemDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：物料清单明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料清单明细表Dto
/// </summary>
public partial class TaktBillOfMaterialItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemDto()
    {
        BomCode = string.Empty;
        ChildMaterialCode = string.Empty;
        ChildMaterialName = string.Empty;
        ChildMaterialUnit = string.Empty;
    }

    /// <summary>
    /// 物料清单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BillOfMaterialItemId { get; set; }

    /// <summary>
    /// BOM ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BomId { get; set; }
    /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }
    /// <summary>
    /// 子物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ChildMaterialId { get; set; }
    /// <summary>
    /// 子物料编码
    /// </summary>
    public string ChildMaterialCode { get; set; }
    /// <summary>
    /// 子物料名称
    /// </summary>
    public string ChildMaterialName { get; set; }
    /// <summary>
    /// 子物料规格
    /// </summary>
    public string? ChildMaterialSpecification { get; set; }
    /// <summary>
    /// 用量
    /// </summary>
    public decimal UsageQuantity { get; set; }
    /// <summary>
    /// 子物料单位
    /// </summary>
    public string ChildMaterialUnit { get; set; }
    /// <summary>
    /// 损耗率
    /// </summary>
    public decimal ScrapRate { get; set; }
    /// <summary>
    /// 实际用量
    /// </summary>
    public decimal ActualUsageQuantity { get; set; }
    /// <summary>
    /// 是否替代料
    /// </summary>
    public int IsSubstitute { get; set; }
    /// <summary>
    /// 替代优先级
    /// </summary>
    public int SubstitutePriority { get; set; }
    /// <summary>
    /// 是否必选
    /// </summary>
    public int IsRequired { get; set; }
    /// <summary>
    /// 是否虚拟件
    /// </summary>
    public int IsPhantom { get; set; }
    /// <summary>
    /// 是否关键件
    /// </summary>
    public int IsCritical { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
}

/// <summary>
/// 物料清单明细表查询DTO
/// </summary>
public partial class TaktBillOfMaterialItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 物料清单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BillOfMaterialItemId { get; set; }

    /// <summary>
    /// BOM ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? BomId { get; set; }
    /// <summary>
    /// BOM编码
    /// </summary>
    public string? BomCode { get; set; }
    /// <summary>
    /// 子物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ChildMaterialId { get; set; }
    /// <summary>
    /// 子物料编码
    /// </summary>
    public string? ChildMaterialCode { get; set; }
    /// <summary>
    /// 子物料名称
    /// </summary>
    public string? ChildMaterialName { get; set; }
    /// <summary>
    /// 子物料规格
    /// </summary>
    public string? ChildMaterialSpecification { get; set; }
    /// <summary>
    /// 用量
    /// </summary>
    public decimal? UsageQuantity { get; set; }
    /// <summary>
    /// 子物料单位
    /// </summary>
    public string? ChildMaterialUnit { get; set; }
    /// <summary>
    /// 损耗率
    /// </summary>
    public decimal? ScrapRate { get; set; }
    /// <summary>
    /// 实际用量
    /// </summary>
    public decimal? ActualUsageQuantity { get; set; }
    /// <summary>
    /// 是否替代料
    /// </summary>
    public int? IsSubstitute { get; set; }
    /// <summary>
    /// 替代优先级
    /// </summary>
    public int? SubstitutePriority { get; set; }
    /// <summary>
    /// 是否必选
    /// </summary>
    public int? IsRequired { get; set; }
    /// <summary>
    /// 是否虚拟件
    /// </summary>
    public int? IsPhantom { get; set; }
    /// <summary>
    /// 是否关键件
    /// </summary>
    public int? IsCritical { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }

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
/// Takt创建物料清单明细表DTO
/// </summary>
public partial class TaktBillOfMaterialItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemCreateDto()
    {
        BomCode = string.Empty;
        ChildMaterialCode = string.Empty;
        ChildMaterialName = string.Empty;
        ChildMaterialUnit = string.Empty;
    }

        /// <summary>
    /// BOM ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BomId { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// 子物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ChildMaterialId { get; set; }

        /// <summary>
    /// 子物料编码
    /// </summary>
    public string ChildMaterialCode { get; set; }

        /// <summary>
    /// 子物料名称
    /// </summary>
    public string ChildMaterialName { get; set; }

        /// <summary>
    /// 子物料规格
    /// </summary>
    public string? ChildMaterialSpecification { get; set; }

        /// <summary>
    /// 用量
    /// </summary>
    public decimal UsageQuantity { get; set; }

        /// <summary>
    /// 子物料单位
    /// </summary>
    public string ChildMaterialUnit { get; set; }

        /// <summary>
    /// 损耗率
    /// </summary>
    public decimal ScrapRate { get; set; }

        /// <summary>
    /// 实际用量
    /// </summary>
    public decimal ActualUsageQuantity { get; set; }

        /// <summary>
    /// 是否替代料
    /// </summary>
    public int IsSubstitute { get; set; }

        /// <summary>
    /// 替代优先级
    /// </summary>
    public int SubstitutePriority { get; set; }

        /// <summary>
    /// 是否必选
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否虚拟件
    /// </summary>
    public int IsPhantom { get; set; }

        /// <summary>
    /// 是否关键件
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
/// Takt更新物料清单明细表DTO
/// </summary>
public partial class TaktBillOfMaterialItemUpdateDto : TaktBillOfMaterialItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemUpdateDto()
    {
    }

        /// <summary>
    /// 物料清单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BillOfMaterialItemId { get; set; }
}

/// <summary>
/// 物料清单明细表导入模板DTO
/// </summary>
public partial class TaktBillOfMaterialItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemTemplateDto()
    {
        BomCode = string.Empty;
        ChildMaterialCode = string.Empty;
        ChildMaterialName = string.Empty;
        ChildMaterialUnit = string.Empty;
    }

        /// <summary>
    /// BOM ID
    /// </summary>
    public long BomId { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// 子物料ID
    /// </summary>
    public long ChildMaterialId { get; set; }

        /// <summary>
    /// 子物料编码
    /// </summary>
    public string ChildMaterialCode { get; set; }

        /// <summary>
    /// 子物料名称
    /// </summary>
    public string ChildMaterialName { get; set; }

        /// <summary>
    /// 子物料规格
    /// </summary>
    public string? ChildMaterialSpecification { get; set; }

        /// <summary>
    /// 用量
    /// </summary>
    public decimal UsageQuantity { get; set; }

        /// <summary>
    /// 子物料单位
    /// </summary>
    public string ChildMaterialUnit { get; set; }

        /// <summary>
    /// 损耗率
    /// </summary>
    public decimal ScrapRate { get; set; }

        /// <summary>
    /// 实际用量
    /// </summary>
    public decimal ActualUsageQuantity { get; set; }

        /// <summary>
    /// 是否替代料
    /// </summary>
    public int IsSubstitute { get; set; }

        /// <summary>
    /// 替代优先级
    /// </summary>
    public int SubstitutePriority { get; set; }

        /// <summary>
    /// 是否必选
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否虚拟件
    /// </summary>
    public int IsPhantom { get; set; }

        /// <summary>
    /// 是否关键件
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
/// 物料清单明细表导入DTO
/// </summary>
public partial class TaktBillOfMaterialItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemImportDto()
    {
        BomCode = string.Empty;
        ChildMaterialCode = string.Empty;
        ChildMaterialName = string.Empty;
        ChildMaterialUnit = string.Empty;
    }

        /// <summary>
    /// BOM ID
    /// </summary>
    public long BomId { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// 子物料ID
    /// </summary>
    public long ChildMaterialId { get; set; }

        /// <summary>
    /// 子物料编码
    /// </summary>
    public string ChildMaterialCode { get; set; }

        /// <summary>
    /// 子物料名称
    /// </summary>
    public string ChildMaterialName { get; set; }

        /// <summary>
    /// 子物料规格
    /// </summary>
    public string? ChildMaterialSpecification { get; set; }

        /// <summary>
    /// 用量
    /// </summary>
    public decimal UsageQuantity { get; set; }

        /// <summary>
    /// 子物料单位
    /// </summary>
    public string ChildMaterialUnit { get; set; }

        /// <summary>
    /// 损耗率
    /// </summary>
    public decimal ScrapRate { get; set; }

        /// <summary>
    /// 实际用量
    /// </summary>
    public decimal ActualUsageQuantity { get; set; }

        /// <summary>
    /// 是否替代料
    /// </summary>
    public int IsSubstitute { get; set; }

        /// <summary>
    /// 替代优先级
    /// </summary>
    public int SubstitutePriority { get; set; }

        /// <summary>
    /// 是否必选
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否虚拟件
    /// </summary>
    public int IsPhantom { get; set; }

        /// <summary>
    /// 是否关键件
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
/// 物料清单明细表导出DTO
/// </summary>
public partial class TaktBillOfMaterialItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialItemExportDto()
    {
        CreatedAt = DateTime.Now;
        BomCode = string.Empty;
        ChildMaterialCode = string.Empty;
        ChildMaterialName = string.Empty;
        ChildMaterialUnit = string.Empty;
    }

        /// <summary>
    /// BOM ID
    /// </summary>
    public long BomId { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// 子物料ID
    /// </summary>
    public long ChildMaterialId { get; set; }

        /// <summary>
    /// 子物料编码
    /// </summary>
    public string ChildMaterialCode { get; set; }

        /// <summary>
    /// 子物料名称
    /// </summary>
    public string ChildMaterialName { get; set; }

        /// <summary>
    /// 子物料规格
    /// </summary>
    public string? ChildMaterialSpecification { get; set; }

        /// <summary>
    /// 用量
    /// </summary>
    public decimal UsageQuantity { get; set; }

        /// <summary>
    /// 子物料单位
    /// </summary>
    public string ChildMaterialUnit { get; set; }

        /// <summary>
    /// 损耗率
    /// </summary>
    public decimal ScrapRate { get; set; }

        /// <summary>
    /// 实际用量
    /// </summary>
    public decimal ActualUsageQuantity { get; set; }

        /// <summary>
    /// 是否替代料
    /// </summary>
    public int IsSubstitute { get; set; }

        /// <summary>
    /// 替代优先级
    /// </summary>
    public int SubstitutePriority { get; set; }

        /// <summary>
    /// 是否必选
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否虚拟件
    /// </summary>
    public int IsPhantom { get; set; }

        /// <summary>
    /// 是否关键件
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}