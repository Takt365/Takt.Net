// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Logistics.Maintenance
// 文件名称：TaktEquipmentDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂设备DTO，包含工厂设备相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Maintenance;

/// <summary>
/// Takt工厂设备DTO
/// </summary>
public class TaktEquipmentDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentDto()
    {
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 设备ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }

    /// <summary>
    /// 设备编码（唯一索引）
    /// </summary>
    public string EquipmentCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

    /// <summary>
    /// 设备类别ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentCategoryId { get; set; }

    /// <summary>
    /// 设备类别名称
    /// </summary>
    public string? EquipmentCategoryName { get; set; }

    /// <summary>
    /// 设备类型（0=生产设备，1=检测设备，2=辅助设备，3=办公设备，4=其他设备）
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
    /// 供应商ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SupplierId { get; set; }

    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }

    /// <summary>
    /// 序列号/出厂编号
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// 所属车间ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? WorkshopId { get; set; }

    /// <summary>
    /// 所属车间名称
    /// </summary>
    public string? WorkshopName { get; set; }

    /// <summary>
    /// 所属产线ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ProductionLineId { get; set; }

    /// <summary>
    /// 所属产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

    /// <summary>
    /// 所属工位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? WorkstationId { get; set; }

    /// <summary>
    /// 所属工位名称
    /// </summary>
    public string? WorkstationName { get; set; }

    /// <summary>
    /// 所属部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 设备位置（详细位置描述）
    /// </summary>
    public string? EquipmentLocation { get; set; }

    /// <summary>
    /// 负责人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsibleUserId { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ResponsibleUserName { get; set; }

    /// <summary>
    /// 操作人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? OperatorId { get; set; }

    /// <summary>
    /// 操作人姓名
    /// </summary>
    public string? OperatorName { get; set; }

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
    /// 设备原值（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; }

    /// <summary>
    /// 设备技术参数（JSON格式，存储设备技术参数配置）
    /// </summary>
    public string? TechnicalParameters { get; set; }

    /// <summary>
    /// 设备图片（JSON格式，存储设备图片URL列表）
    /// </summary>
    public string? EquipmentImages { get; set; }

    /// <summary>
    /// 设备文档（JSON格式，存储设备文档ID列表）
    /// </summary>
    public string? EquipmentDocuments { get; set; }

    /// <summary>
    /// 是否关键设备（0=否，1=是）
    /// </summary>
    public int IsCritical { get; set; }

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    public int IsEnabled { get; set; }

    /// <summary>
    /// 设备状态（0=运行中，1=停机，2=维修中，3=故障，4=待报废，5=已报废）
    /// </summary>
    public int EquipmentStatus { get; set; }

    /// <summary>
    /// 最后维护日期
    /// </summary>
    public DateTime? LastMaintenanceDate { get; set; }

    /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }

    /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; }

    /// <summary>
    /// 累计运行时间（小时）
    /// </summary>
    public decimal TotalRunningHours { get; set; }

    /// <summary>
    /// 累计停机时间（小时）
    /// </summary>
    public decimal TotalDowntimeHours { get; set; }
}

/// <summary>
/// Takt工厂设备查询DTO
/// </summary>
public class TaktEquipmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在设备编码、设备名称中模糊查询

    /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }

    /// <summary>
    /// 设备类别ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EquipmentCategoryId { get; set; }

    /// <summary>
    /// 设备类型（0=生产设备，1=检测设备，2=辅助设备，3=办公设备，4=其他设备）
    /// </summary>
    public int? EquipmentType { get; set; }

    /// <summary>
    /// 所属车间ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? WorkshopId { get; set; }

    /// <summary>
    /// 所属产线ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ProductionLineId { get; set; }

    /// <summary>
    /// 所属部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    public int? IsEnabled { get; set; }

    /// <summary>
    /// 设备状态（0=运行中，1=停机，2=维修中，3=故障，4=待报废，5=已报废）
    /// </summary>
    public int? EquipmentStatus { get; set; }
}

/// <summary>
/// Takt创建工厂设备DTO
/// </summary>
public class TaktEquipmentCreateDto
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
    /// 设备编码（唯一索引）
    /// </summary>
    public string EquipmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; } = string.Empty;

    /// <summary>
    /// 设备类别ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentCategoryId { get; set; }

    /// <summary>
    /// 设备类别名称
    /// </summary>
    public string? EquipmentCategoryName { get; set; }

    /// <summary>
    /// 设备类型（0=生产设备，1=检测设备，2=辅助设备，3=办公设备，4=其他设备）
    /// </summary>
    public int EquipmentType { get; set; } = 0;

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
    /// 供应商ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SupplierId { get; set; }

    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }

    /// <summary>
    /// 序列号/出厂编号
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// 所属车间ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? WorkshopId { get; set; }

    /// <summary>
    /// 所属车间名称
    /// </summary>
    public string? WorkshopName { get; set; }

    /// <summary>
    /// 所属产线ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ProductionLineId { get; set; }

    /// <summary>
    /// 所属产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

    /// <summary>
    /// 所属工位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? WorkstationId { get; set; }

    /// <summary>
    /// 所属工位名称
    /// </summary>
    public string? WorkstationName { get; set; }

    /// <summary>
    /// 所属部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 设备位置（详细位置描述）
    /// </summary>
    public string? EquipmentLocation { get; set; }

    /// <summary>
    /// 负责人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsibleUserId { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ResponsibleUserName { get; set; }

    /// <summary>
    /// 操作人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? OperatorId { get; set; }

    /// <summary>
    /// 操作人姓名
    /// </summary>
    public string? OperatorName { get; set; }

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
    /// 设备原值（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; } = 0;

    /// <summary>
    /// 设备技术参数（JSON格式，存储设备技术参数配置）
    /// </summary>
    public string? TechnicalParameters { get; set; }

    /// <summary>
    /// 设备图片（JSON格式，存储设备图片URL列表）
    /// </summary>
    public string? EquipmentImages { get; set; }

    /// <summary>
    /// 设备文档（JSON格式，存储设备文档ID列表）
    /// </summary>
    public string? EquipmentDocuments { get; set; }

    /// <summary>
    /// 是否关键设备（0=否，1=是）
    /// </summary>
    public int IsCritical { get; set; } = 0;

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    public int IsEnabled { get; set; } = 1;

    /// <summary>
    /// 设备状态（0=运行中，1=停机，2=维修中，3=故障，4=待报废，5=已报废）
    /// </summary>
    public int EquipmentStatus { get; set; } = 0;

    /// <summary>
    /// 最后维护日期
    /// </summary>
    public DateTime? LastMaintenanceDate { get; set; }

    /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }

    /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; } = 0;

    /// <summary>
    /// 累计运行时间（小时）
    /// </summary>
    public decimal TotalRunningHours { get; set; } = 0;

    /// <summary>
    /// 累计停机时间（小时）
    /// </summary>
    public decimal TotalDowntimeHours { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新工厂设备DTO
/// </summary>
public class TaktEquipmentUpdateDto : TaktEquipmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentUpdateDto()
    {
    }

    /// <summary>
    /// 设备ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }
}

/// <summary>
/// Takt工厂设备状态DTO
/// </summary>
public class TaktEquipmentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentStatusDto()
    {
    }

    /// <summary>
    /// 设备ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }

    /// <summary>
    /// 设备状态（0=运行中，1=停机，2=维修中，3=故障，4=待报废，5=已报废）
    /// </summary>
    public int EquipmentStatus { get; set; }
}

/// <summary>
/// Takt工厂设备导入模板DTO
/// </summary>
public class TaktEquipmentTemplateDto
{
    /// <summary>
    /// 设备编码（唯一索引）
    /// </summary>
    public string EquipmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; } = string.Empty;

    /// <summary>
    /// 设备类别ID
    /// </summary>
    public string EquipmentCategoryId { get; set; } = string.Empty;

    /// <summary>
    /// 设备类型（0=生产设备，1=检测设备，2=辅助设备，3=办公设备，4=其他设备）
    /// </summary>
    public int EquipmentType { get; set; } = 0;

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
    /// 供应商ID
    /// </summary>
    public string? SupplierId { get; set; }

    /// <summary>
    /// 序列号/出厂编号
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// 所属车间ID
    /// </summary>
    public string? WorkshopId { get; set; }

    /// <summary>
    /// 所属产线ID
    /// </summary>
    public string? ProductionLineId { get; set; }

    /// <summary>
    /// 所属工位ID
    /// </summary>
    public string? WorkstationId { get; set; }

    /// <summary>
    /// 所属部门ID
    /// </summary>
    public string? DeptId { get; set; }

    /// <summary>
    /// 设备位置
    /// </summary>
    public string? EquipmentLocation { get; set; }

    /// <summary>
    /// 负责人ID
    /// </summary>
    public string? ResponsibleUserId { get; set; }

    /// <summary>
    /// 操作人ID
    /// </summary>
    public string? OperatorId { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    public string? PurchaseDate { get; set; }

    /// <summary>
    /// 安装日期
    /// </summary>
    public string? InstallationDate { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    /// 保修开始日期
    /// </summary>
    public string? WarrantyStartDate { get; set; }

    /// <summary>
    /// 保修结束日期
    /// </summary>
    public string? WarrantyEndDate { get; set; }

    /// <summary>
    /// 设备原值
    /// </summary>
    public decimal EquipmentOriginalValue { get; set; } = 0;

    /// <summary>
    /// 是否关键设备（0=否，1=是）
    /// </summary>
    public int IsCritical { get; set; } = 0;

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    public int IsEnabled { get; set; } = 1;

    /// <summary>
    /// 设备状态（0=运行中，1=停机，2=维修中，3=故障，4=待报废，5=已报废）
    /// </summary>
    public int EquipmentStatus { get; set; } = 0;

    /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt工厂设备导入DTO
/// </summary>
public class TaktEquipmentImportDto : TaktEquipmentTemplateDto
{
}

/// <summary>
/// Takt工厂设备导出DTO
/// </summary>
public class TaktEquipmentExportDto : TaktEquipmentDto
{
}
