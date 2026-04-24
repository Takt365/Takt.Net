// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：物料清单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料清单表Dto
/// </summary>
public partial class TaktBillOfMaterialDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialDto()
    {
        BomCode = string.Empty;
        BomName = string.Empty;
        ParentMaterialCode = string.Empty;
        ParentMaterialName = string.Empty;
        BomVersion = string.Empty;
        ParentMaterialUnit = string.Empty;
    }

    /// <summary>
    /// 物料清单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BillOfMaterialId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }
    /// <summary>
    /// BOM名称
    /// </summary>
    public string BomName { get; set; }
    /// <summary>
    /// 父物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentMaterialId { get; set; }
    /// <summary>
    /// 父物料编码
    /// </summary>
    public string ParentMaterialCode { get; set; }
    /// <summary>
    /// 父物料名称
    /// </summary>
    public string ParentMaterialName { get; set; }
    /// <summary>
    /// BOM版本号
    /// </summary>
    public string BomVersion { get; set; }
    /// <summary>
    /// BOM类型
    /// </summary>
    public int BomType { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
    /// <summary>
    /// 父物料单位
    /// </summary>
    public string ParentMaterialUnit { get; set; }
    /// <summary>
    /// 父物料数量
    /// </summary>
    public decimal ParentMaterialQuantity { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }
    /// <summary>
    /// BOM状态
    /// </summary>
    public int BomStatus { get; set; }
    /// <summary>
    /// BOM描述
    /// </summary>
    public string? BomDescription { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 物料清单表查询DTO
/// </summary>
public partial class TaktBillOfMaterialQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 物料清单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BillOfMaterialId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// BOM编码
    /// </summary>
    public string? BomCode { get; set; }
    /// <summary>
    /// BOM名称
    /// </summary>
    public string? BomName { get; set; }
    /// <summary>
    /// 父物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentMaterialId { get; set; }
    /// <summary>
    /// 父物料编码
    /// </summary>
    public string? ParentMaterialCode { get; set; }
    /// <summary>
    /// 父物料名称
    /// </summary>
    public string? ParentMaterialName { get; set; }
    /// <summary>
    /// BOM版本号
    /// </summary>
    public string? BomVersion { get; set; }
    /// <summary>
    /// BOM类型
    /// </summary>
    public int? BomType { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? EffectiveDateStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? EffectiveDateEnd { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 失效日期开始时间
    /// </summary>
    public DateTime? ExpiryDateStart { get; set; }
    /// <summary>
    /// 失效日期结束时间
    /// </summary>
    public DateTime? ExpiryDateEnd { get; set; }
    /// <summary>
    /// 父物料单位
    /// </summary>
    public string? ParentMaterialUnit { get; set; }
    /// <summary>
    /// 父物料数量
    /// </summary>
    public decimal? ParentMaterialQuantity { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public int? IsEnabled { get; set; }
    /// <summary>
    /// BOM状态
    /// </summary>
    public int? BomStatus { get; set; }
    /// <summary>
    /// BOM描述
    /// </summary>
    public string? BomDescription { get; set; }
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
/// Takt创建物料清单表DTO
/// </summary>
public partial class TaktBillOfMaterialCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialCreateDto()
    {
        BomCode = string.Empty;
        BomName = string.Empty;
        ParentMaterialCode = string.Empty;
        ParentMaterialName = string.Empty;
        BomVersion = string.Empty;
        ParentMaterialUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// BOM名称
    /// </summary>
    public string BomName { get; set; }

        /// <summary>
    /// 父物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentMaterialId { get; set; }

        /// <summary>
    /// 父物料编码
    /// </summary>
    public string ParentMaterialCode { get; set; }

        /// <summary>
    /// 父物料名称
    /// </summary>
    public string ParentMaterialName { get; set; }

        /// <summary>
    /// BOM版本号
    /// </summary>
    public string BomVersion { get; set; }

        /// <summary>
    /// BOM类型
    /// </summary>
    public int BomType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 父物料单位
    /// </summary>
    public string ParentMaterialUnit { get; set; }

        /// <summary>
    /// 父物料数量
    /// </summary>
    public decimal ParentMaterialQuantity { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// BOM状态
    /// </summary>
    public int BomStatus { get; set; }

        /// <summary>
    /// BOM描述
    /// </summary>
    public string? BomDescription { get; set; }

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
/// Takt更新物料清单表DTO
/// </summary>
public partial class TaktBillOfMaterialUpdateDto : TaktBillOfMaterialCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialUpdateDto()
    {
    }

        /// <summary>
    /// 物料清单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BillOfMaterialId { get; set; }
}

/// <summary>
/// 物料清单表BOM状态DTO
/// </summary>
public partial class TaktBillOfMaterialBomStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialBomStatusDto()
    {
    }

        /// <summary>
    /// 物料清单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BillOfMaterialId { get; set; }

    /// <summary>
    /// BOM状态（0=禁用，1=启用）
    /// </summary>
    public int BomStatus { get; set; }
}

/// <summary>
/// 物料清单表导入模板DTO
/// </summary>
public partial class TaktBillOfMaterialTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialTemplateDto()
    {
        BomCode = string.Empty;
        BomName = string.Empty;
        ParentMaterialCode = string.Empty;
        ParentMaterialName = string.Empty;
        BomVersion = string.Empty;
        ParentMaterialUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// BOM名称
    /// </summary>
    public string BomName { get; set; }

        /// <summary>
    /// 父物料ID
    /// </summary>
    public long ParentMaterialId { get; set; }

        /// <summary>
    /// 父物料编码
    /// </summary>
    public string ParentMaterialCode { get; set; }

        /// <summary>
    /// 父物料名称
    /// </summary>
    public string ParentMaterialName { get; set; }

        /// <summary>
    /// BOM版本号
    /// </summary>
    public string BomVersion { get; set; }

        /// <summary>
    /// BOM类型
    /// </summary>
    public int BomType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 父物料单位
    /// </summary>
    public string ParentMaterialUnit { get; set; }

        /// <summary>
    /// 父物料数量
    /// </summary>
    public decimal ParentMaterialQuantity { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// BOM状态
    /// </summary>
    public int BomStatus { get; set; }

        /// <summary>
    /// BOM描述
    /// </summary>
    public string? BomDescription { get; set; }

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
/// 物料清单表导入DTO
/// </summary>
public partial class TaktBillOfMaterialImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialImportDto()
    {
        BomCode = string.Empty;
        BomName = string.Empty;
        ParentMaterialCode = string.Empty;
        ParentMaterialName = string.Empty;
        BomVersion = string.Empty;
        ParentMaterialUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// BOM名称
    /// </summary>
    public string BomName { get; set; }

        /// <summary>
    /// 父物料ID
    /// </summary>
    public long ParentMaterialId { get; set; }

        /// <summary>
    /// 父物料编码
    /// </summary>
    public string ParentMaterialCode { get; set; }

        /// <summary>
    /// 父物料名称
    /// </summary>
    public string ParentMaterialName { get; set; }

        /// <summary>
    /// BOM版本号
    /// </summary>
    public string BomVersion { get; set; }

        /// <summary>
    /// BOM类型
    /// </summary>
    public int BomType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 父物料单位
    /// </summary>
    public string ParentMaterialUnit { get; set; }

        /// <summary>
    /// 父物料数量
    /// </summary>
    public decimal ParentMaterialQuantity { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// BOM状态
    /// </summary>
    public int BomStatus { get; set; }

        /// <summary>
    /// BOM描述
    /// </summary>
    public string? BomDescription { get; set; }

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
/// 物料清单表导出DTO
/// </summary>
public partial class TaktBillOfMaterialExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialExportDto()
    {
        CreatedAt = DateTime.Now;
        BomCode = string.Empty;
        BomName = string.Empty;
        ParentMaterialCode = string.Empty;
        ParentMaterialName = string.Empty;
        BomVersion = string.Empty;
        ParentMaterialUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// BOM编码
    /// </summary>
    public string BomCode { get; set; }

        /// <summary>
    /// BOM名称
    /// </summary>
    public string BomName { get; set; }

        /// <summary>
    /// 父物料ID
    /// </summary>
    public long ParentMaterialId { get; set; }

        /// <summary>
    /// 父物料编码
    /// </summary>
    public string ParentMaterialCode { get; set; }

        /// <summary>
    /// 父物料名称
    /// </summary>
    public string ParentMaterialName { get; set; }

        /// <summary>
    /// BOM版本号
    /// </summary>
    public string BomVersion { get; set; }

        /// <summary>
    /// BOM类型
    /// </summary>
    public int BomType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 父物料单位
    /// </summary>
    public string ParentMaterialUnit { get; set; }

        /// <summary>
    /// 父物料数量
    /// </summary>
    public decimal ParentMaterialQuantity { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

        /// <summary>
    /// BOM状态
    /// </summary>
    public int BomStatus { get; set; }

        /// <summary>
    /// BOM描述
    /// </summary>
    public string? BomDescription { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}