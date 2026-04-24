// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Maintenance
// 文件名称：TaktEquipmentDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：工厂设备表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Maintenance;

/// <summary>
/// 工厂设备表Dto
/// </summary>
public partial class TaktEquipmentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentDto()
    {
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
    }

    /// <summary>
    /// 工厂设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }

    /// <summary>
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }
    /// <summary>
    /// 设备型号
    /// </summary>
    public string? EquipmentModel { get; set; }
    /// <summary>
    /// 设备规格
    /// </summary>
    public string? EquipmentSpecification { get; set; }
    /// <summary>
    /// 设备品牌
    /// </summary>
    public string? EquipmentBrand { get; set; }
    /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }
    /// <summary>
    /// 经销商
    /// </summary>
    public string? DealerBy { get; set; }
    /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNumber { get; set; }
    /// <summary>
    /// 所属车间
    /// </summary>
    public string? WorkshopBy { get; set; }
    /// <summary>
    /// 所属产线
    /// </summary>
    public string? ProductionLineBy { get; set; }
    /// <summary>
    /// 所属工位
    /// </summary>
    public string? WorkstationBy { get; set; }
    /// <summary>
    /// 所属部门
    /// </summary>
    public string? DeptBy { get; set; }
    /// <summary>
    /// 设备位置
    /// </summary>
    public string? EquipmentLocation { get; set; }
    /// <summary>
    /// 负责人
    /// </summary>
    public string? ResponsibleUserBy { get; set; }
    /// <summary>
    /// 操作人
    /// </summary>
    public string? OperatorBy { get; set; }
    /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }
    /// <summary>
    /// 安装日期
    /// </summary>
    public DateTime? InstallationDate { get; set; }
    /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }
    /// <summary>
    /// 保修开始日期
    /// </summary>
    public DateTime? WarrantyStartDate { get; set; }
    /// <summary>
    /// 保修结束日期
    /// </summary>
    public DateTime? WarrantyEndDate { get; set; }
    /// <summary>
    /// 设备原值
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; }
    /// <summary>
    /// 设备技术参数
    /// </summary>
    public string? TechnicalParameters { get; set; }
    /// <summary>
    /// 设备图片
    /// </summary>
    public string? EquipmentImages { get; set; }
    /// <summary>
    /// 设备文档
    /// </summary>
    public string? EquipmentDocuments { get; set; }
    /// <summary>
    /// 是否关键设备
    /// </summary>
    public int IsCritical { get; set; }
    /// <summary>
    /// 保修状态
    /// </summary>
    public int WarrantyStatus { get; set; }
    /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

    /// <summary>
    /// 维护记录列表（外键：子表 TaktMaintenance.EquipmentId 关联本表 Id）
    /// </summary>
    public List<long>? MaintenanceRecordIds { get; set; }
}

/// <summary>
/// 工厂设备表查询DTO
/// </summary>
public partial class TaktEquipmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }

    /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public int? EquipmentType { get; set; }
    /// <summary>
    /// 设备型号
    /// </summary>
    public string? EquipmentModel { get; set; }
    /// <summary>
    /// 设备规格
    /// </summary>
    public string? EquipmentSpecification { get; set; }
    /// <summary>
    /// 设备品牌
    /// </summary>
    public string? EquipmentBrand { get; set; }
    /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }
    /// <summary>
    /// 经销商
    /// </summary>
    public string? DealerBy { get; set; }
    /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNumber { get; set; }
    /// <summary>
    /// 所属车间
    /// </summary>
    public string? WorkshopBy { get; set; }
    /// <summary>
    /// 所属产线
    /// </summary>
    public string? ProductionLineBy { get; set; }
    /// <summary>
    /// 所属工位
    /// </summary>
    public string? WorkstationBy { get; set; }
    /// <summary>
    /// 所属部门
    /// </summary>
    public string? DeptBy { get; set; }
    /// <summary>
    /// 设备位置
    /// </summary>
    public string? EquipmentLocation { get; set; }
    /// <summary>
    /// 负责人
    /// </summary>
    public string? ResponsibleUserBy { get; set; }
    /// <summary>
    /// 操作人
    /// </summary>
    public string? OperatorBy { get; set; }
    /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// 购买日期开始时间
    /// </summary>
    public DateTime? PurchaseDateStart { get; set; }
    /// <summary>
    /// 购买日期结束时间
    /// </summary>
    public DateTime? PurchaseDateEnd { get; set; }
    /// <summary>
    /// 安装日期
    /// </summary>
    public DateTime? InstallationDate { get; set; }

    /// <summary>
    /// 安装日期开始时间
    /// </summary>
    public DateTime? InstallationDateStart { get; set; }
    /// <summary>
    /// 安装日期结束时间
    /// </summary>
    public DateTime? InstallationDateEnd { get; set; }
    /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 启用日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 启用日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 保修开始日期
    /// </summary>
    public DateTime? WarrantyStartDate { get; set; }

    /// <summary>
    /// 保修开始日期开始时间
    /// </summary>
    public DateTime? WarrantyStartDateStart { get; set; }
    /// <summary>
    /// 保修开始日期结束时间
    /// </summary>
    public DateTime? WarrantyStartDateEnd { get; set; }
    /// <summary>
    /// 保修结束日期
    /// </summary>
    public DateTime? WarrantyEndDate { get; set; }

    /// <summary>
    /// 保修结束日期开始时间
    /// </summary>
    public DateTime? WarrantyEndDateStart { get; set; }
    /// <summary>
    /// 保修结束日期结束时间
    /// </summary>
    public DateTime? WarrantyEndDateEnd { get; set; }
    /// <summary>
    /// 设备原值
    /// </summary>
    public decimal? EquipmentOriginalValue { get; set; }
    /// <summary>
    /// 设备技术参数
    /// </summary>
    public string? TechnicalParameters { get; set; }
    /// <summary>
    /// 设备图片
    /// </summary>
    public string? EquipmentImages { get; set; }
    /// <summary>
    /// 设备文档
    /// </summary>
    public string? EquipmentDocuments { get; set; }
    /// <summary>
    /// 是否关键设备
    /// </summary>
    public int? IsCritical { get; set; }
    /// <summary>
    /// 保修状态
    /// </summary>
    public int? WarrantyStatus { get; set; }
    /// <summary>
    /// 设备状态
    /// </summary>
    public int? EquipmentStatus { get; set; }

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
/// Takt创建工厂设备表DTO
/// </summary>
public partial class TaktEquipmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentCreateDto()
    {
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 设备型号
    /// </summary>
    public string? EquipmentModel { get; set; }

        /// <summary>
    /// 设备规格
    /// </summary>
    public string? EquipmentSpecification { get; set; }

        /// <summary>
    /// 设备品牌
    /// </summary>
    public string? EquipmentBrand { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 经销商
    /// </summary>
    public string? DealerBy { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 所属车间
    /// </summary>
    public string? WorkshopBy { get; set; }

        /// <summary>
    /// 所属产线
    /// </summary>
    public string? ProductionLineBy { get; set; }

        /// <summary>
    /// 所属工位
    /// </summary>
    public string? WorkstationBy { get; set; }

        /// <summary>
    /// 所属部门
    /// </summary>
    public string? DeptBy { get; set; }

        /// <summary>
    /// 设备位置
    /// </summary>
    public string? EquipmentLocation { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? ResponsibleUserBy { get; set; }

        /// <summary>
    /// 操作人
    /// </summary>
    public string? OperatorBy { get; set; }

        /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

        /// <summary>
    /// 安装日期
    /// </summary>
    public DateTime? InstallationDate { get; set; }

        /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 保修开始日期
    /// </summary>
    public DateTime? WarrantyStartDate { get; set; }

        /// <summary>
    /// 保修结束日期
    /// </summary>
    public DateTime? WarrantyEndDate { get; set; }

        /// <summary>
    /// 设备原值
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; }

        /// <summary>
    /// 设备技术参数
    /// </summary>
    public string? TechnicalParameters { get; set; }

        /// <summary>
    /// 设备图片
    /// </summary>
    public string? EquipmentImages { get; set; }

        /// <summary>
    /// 设备文档
    /// </summary>
    public string? EquipmentDocuments { get; set; }

        /// <summary>
    /// 是否关键设备
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 保修状态
    /// </summary>
    public int WarrantyStatus { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

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
/// Takt更新工厂设备表DTO
/// </summary>
public partial class TaktEquipmentUpdateDto : TaktEquipmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentUpdateDto()
    {
    }

        /// <summary>
    /// 工厂设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }
}

/// <summary>
/// 工厂设备表保修状态DTO
/// </summary>
public partial class TaktEquipmentWarrantyStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentWarrantyStatusDto()
    {
    }

        /// <summary>
    /// 工厂设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }

    /// <summary>
    /// 保修状态（0=禁用，1=启用）
    /// </summary>
    public int WarrantyStatus { get; set; }
}

/// <summary>
/// 工厂设备表设备状态DTO
/// </summary>
public partial class TaktEquipmentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentStatusDto()
    {
    }

        /// <summary>
    /// 工厂设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }

    /// <summary>
    /// 设备状态（0=禁用，1=启用）
    /// </summary>
    public int EquipmentStatus { get; set; }
}

/// <summary>
/// 工厂设备表导入模板DTO
/// </summary>
public partial class TaktEquipmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentTemplateDto()
    {
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 设备型号
    /// </summary>
    public string? EquipmentModel { get; set; }

        /// <summary>
    /// 设备规格
    /// </summary>
    public string? EquipmentSpecification { get; set; }

        /// <summary>
    /// 设备品牌
    /// </summary>
    public string? EquipmentBrand { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 经销商
    /// </summary>
    public string? DealerBy { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 所属车间
    /// </summary>
    public string? WorkshopBy { get; set; }

        /// <summary>
    /// 所属产线
    /// </summary>
    public string? ProductionLineBy { get; set; }

        /// <summary>
    /// 所属工位
    /// </summary>
    public string? WorkstationBy { get; set; }

        /// <summary>
    /// 所属部门
    /// </summary>
    public string? DeptBy { get; set; }

        /// <summary>
    /// 设备位置
    /// </summary>
    public string? EquipmentLocation { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? ResponsibleUserBy { get; set; }

        /// <summary>
    /// 操作人
    /// </summary>
    public string? OperatorBy { get; set; }

        /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

        /// <summary>
    /// 安装日期
    /// </summary>
    public DateTime? InstallationDate { get; set; }

        /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 保修开始日期
    /// </summary>
    public DateTime? WarrantyStartDate { get; set; }

        /// <summary>
    /// 保修结束日期
    /// </summary>
    public DateTime? WarrantyEndDate { get; set; }

        /// <summary>
    /// 设备原值
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; }

        /// <summary>
    /// 设备技术参数
    /// </summary>
    public string? TechnicalParameters { get; set; }

        /// <summary>
    /// 设备图片
    /// </summary>
    public string? EquipmentImages { get; set; }

        /// <summary>
    /// 设备文档
    /// </summary>
    public string? EquipmentDocuments { get; set; }

        /// <summary>
    /// 是否关键设备
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 保修状态
    /// </summary>
    public int WarrantyStatus { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

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
/// 工厂设备表导入DTO
/// </summary>
public partial class TaktEquipmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentImportDto()
    {
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 设备型号
    /// </summary>
    public string? EquipmentModel { get; set; }

        /// <summary>
    /// 设备规格
    /// </summary>
    public string? EquipmentSpecification { get; set; }

        /// <summary>
    /// 设备品牌
    /// </summary>
    public string? EquipmentBrand { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 经销商
    /// </summary>
    public string? DealerBy { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 所属车间
    /// </summary>
    public string? WorkshopBy { get; set; }

        /// <summary>
    /// 所属产线
    /// </summary>
    public string? ProductionLineBy { get; set; }

        /// <summary>
    /// 所属工位
    /// </summary>
    public string? WorkstationBy { get; set; }

        /// <summary>
    /// 所属部门
    /// </summary>
    public string? DeptBy { get; set; }

        /// <summary>
    /// 设备位置
    /// </summary>
    public string? EquipmentLocation { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? ResponsibleUserBy { get; set; }

        /// <summary>
    /// 操作人
    /// </summary>
    public string? OperatorBy { get; set; }

        /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

        /// <summary>
    /// 安装日期
    /// </summary>
    public DateTime? InstallationDate { get; set; }

        /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 保修开始日期
    /// </summary>
    public DateTime? WarrantyStartDate { get; set; }

        /// <summary>
    /// 保修结束日期
    /// </summary>
    public DateTime? WarrantyEndDate { get; set; }

        /// <summary>
    /// 设备原值
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; }

        /// <summary>
    /// 设备技术参数
    /// </summary>
    public string? TechnicalParameters { get; set; }

        /// <summary>
    /// 设备图片
    /// </summary>
    public string? EquipmentImages { get; set; }

        /// <summary>
    /// 设备文档
    /// </summary>
    public string? EquipmentDocuments { get; set; }

        /// <summary>
    /// 是否关键设备
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 保修状态
    /// </summary>
    public int WarrantyStatus { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

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
/// 工厂设备表导出DTO
/// </summary>
public partial class TaktEquipmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentExportDto()
    {
        CreatedAt = DateTime.Now;
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 设备型号
    /// </summary>
    public string? EquipmentModel { get; set; }

        /// <summary>
    /// 设备规格
    /// </summary>
    public string? EquipmentSpecification { get; set; }

        /// <summary>
    /// 设备品牌
    /// </summary>
    public string? EquipmentBrand { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 经销商
    /// </summary>
    public string? DealerBy { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNumber { get; set; }

        /// <summary>
    /// 所属车间
    /// </summary>
    public string? WorkshopBy { get; set; }

        /// <summary>
    /// 所属产线
    /// </summary>
    public string? ProductionLineBy { get; set; }

        /// <summary>
    /// 所属工位
    /// </summary>
    public string? WorkstationBy { get; set; }

        /// <summary>
    /// 所属部门
    /// </summary>
    public string? DeptBy { get; set; }

        /// <summary>
    /// 设备位置
    /// </summary>
    public string? EquipmentLocation { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? ResponsibleUserBy { get; set; }

        /// <summary>
    /// 操作人
    /// </summary>
    public string? OperatorBy { get; set; }

        /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

        /// <summary>
    /// 安装日期
    /// </summary>
    public DateTime? InstallationDate { get; set; }

        /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 保修开始日期
    /// </summary>
    public DateTime? WarrantyStartDate { get; set; }

        /// <summary>
    /// 保修结束日期
    /// </summary>
    public DateTime? WarrantyEndDate { get; set; }

        /// <summary>
    /// 设备原值
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; }

        /// <summary>
    /// 设备技术参数
    /// </summary>
    public string? TechnicalParameters { get; set; }

        /// <summary>
    /// 设备图片
    /// </summary>
    public string? EquipmentImages { get; set; }

        /// <summary>
    /// 设备文档
    /// </summary>
    public string? EquipmentDocuments { get; set; }

        /// <summary>
    /// 是否关键设备
    /// </summary>
    public int IsCritical { get; set; }

        /// <summary>
    /// 保修状态
    /// </summary>
    public int WarrantyStatus { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}