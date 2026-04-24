// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Maintenance
// 文件名称：TaktMaintenanceDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：设备维护记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Maintenance;

/// <summary>
/// 设备维护记录表Dto
/// </summary>
public partial class TaktMaintenanceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 设备维护记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaintenanceId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }
    /// <summary>
    /// 维护类型
    /// </summary>
    public int MaintenanceType { get; set; }
    /// <summary>
    /// 维护单位
    /// </summary>
    public string? MaintenanceCompany { get; set; }
    /// <summary>
    /// 维护技师
    /// </summary>
    public string? MaintenanceTechnician { get; set; }
    /// <summary>
    /// 维护日期
    /// </summary>
    public DateTime MaintenanceDate { get; set; }
    /// <summary>
    /// 维护开始时间
    /// </summary>
    public DateTime? MaintenanceStartTime { get; set; }
    /// <summary>
    /// 维护结束时间
    /// </summary>
    public DateTime? MaintenanceEndTime { get; set; }
    /// <summary>
    /// 维护内容
    /// </summary>
    public string? MaintenanceContent { get; set; }
    /// <summary>
    /// 故障描述
    /// </summary>
    public string? FaultDescription { get; set; }
    /// <summary>
    /// 处理方案
    /// </summary>
    public string? Solution { get; set; }
    /// <summary>
    /// 使用配件
    /// </summary>
    public string? UsedParts { get; set; }
    /// <summary>
    /// 维护费用
    /// </summary>
    public decimal MaintenanceCost { get; set; }
    /// <summary>
    /// 维护结果
    /// </summary>
    public int MaintenanceResult { get; set; }
    /// <summary>
    /// 维护状态
    /// </summary>
    public int MaintenanceStatus { get; set; }
    /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }
    /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; }
    /// <summary>
    /// 维护文档
    /// </summary>
    public string? MaintenanceDocuments { get; set; }
    /// <summary>
    /// 维护图片
    /// </summary>
    public string? MaintenanceImages { get; set; }
    /// <summary>
    /// 验收总结
    /// </summary>
    public string? AcceptedSummary { get; set; }
    /// <summary>
    /// 验收人
    /// </summary>
    public string? AcceptedBy { get; set; }
    /// <summary>
    /// 验收时间
    /// </summary>
    public DateTime? AcceptedAt { get; set; }
}

/// <summary>
/// 设备维护记录表查询DTO
/// </summary>
public partial class TaktMaintenanceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMaintenanceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 设备维护记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaintenanceId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EquipmentId { get; set; }
    /// <summary>
    /// 维护类型
    /// </summary>
    public int? MaintenanceType { get; set; }
    /// <summary>
    /// 维护单位
    /// </summary>
    public string? MaintenanceCompany { get; set; }
    /// <summary>
    /// 维护技师
    /// </summary>
    public string? MaintenanceTechnician { get; set; }
    /// <summary>
    /// 维护日期
    /// </summary>
    public DateTime? MaintenanceDate { get; set; }

    /// <summary>
    /// 维护日期开始时间
    /// </summary>
    public DateTime? MaintenanceDateStart { get; set; }
    /// <summary>
    /// 维护日期结束时间
    /// </summary>
    public DateTime? MaintenanceDateEnd { get; set; }
    /// <summary>
    /// 维护开始时间
    /// </summary>
    public DateTime? MaintenanceStartTime { get; set; }

    /// <summary>
    /// 维护开始时间开始时间
    /// </summary>
    public DateTime? MaintenanceStartTimeStart { get; set; }
    /// <summary>
    /// 维护开始时间结束时间
    /// </summary>
    public DateTime? MaintenanceStartTimeEnd { get; set; }
    /// <summary>
    /// 维护结束时间
    /// </summary>
    public DateTime? MaintenanceEndTime { get; set; }

    /// <summary>
    /// 维护结束时间开始时间
    /// </summary>
    public DateTime? MaintenanceEndTimeStart { get; set; }
    /// <summary>
    /// 维护结束时间结束时间
    /// </summary>
    public DateTime? MaintenanceEndTimeEnd { get; set; }
    /// <summary>
    /// 维护内容
    /// </summary>
    public string? MaintenanceContent { get; set; }
    /// <summary>
    /// 故障描述
    /// </summary>
    public string? FaultDescription { get; set; }
    /// <summary>
    /// 处理方案
    /// </summary>
    public string? Solution { get; set; }
    /// <summary>
    /// 使用配件
    /// </summary>
    public string? UsedParts { get; set; }
    /// <summary>
    /// 维护费用
    /// </summary>
    public decimal? MaintenanceCost { get; set; }
    /// <summary>
    /// 维护结果
    /// </summary>
    public int? MaintenanceResult { get; set; }
    /// <summary>
    /// 维护状态
    /// </summary>
    public int? MaintenanceStatus { get; set; }
    /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }

    /// <summary>
    /// 下次维护日期开始时间
    /// </summary>
    public DateTime? NextMaintenanceDateStart { get; set; }
    /// <summary>
    /// 下次维护日期结束时间
    /// </summary>
    public DateTime? NextMaintenanceDateEnd { get; set; }
    /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int? MaintenanceCycleDays { get; set; }
    /// <summary>
    /// 维护文档
    /// </summary>
    public string? MaintenanceDocuments { get; set; }
    /// <summary>
    /// 维护图片
    /// </summary>
    public string? MaintenanceImages { get; set; }
    /// <summary>
    /// 验收总结
    /// </summary>
    public string? AcceptedSummary { get; set; }
    /// <summary>
    /// 验收人
    /// </summary>
    public string? AcceptedBy { get; set; }
    /// <summary>
    /// 验收时间
    /// </summary>
    public DateTime? AcceptedAt { get; set; }

    /// <summary>
    /// 验收时间开始时间
    /// </summary>
    public DateTime? AcceptedAtStart { get; set; }
    /// <summary>
    /// 验收时间结束时间
    /// </summary>
    public DateTime? AcceptedAtEnd { get; set; }

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
/// Takt创建设备维护记录表DTO
/// </summary>
public partial class TaktMaintenanceCreateDto
{
        /// <summary>
    /// 设备ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentId { get; set; }

        /// <summary>
    /// 维护类型
    /// </summary>
    public int MaintenanceType { get; set; }

        /// <summary>
    /// 维护单位
    /// </summary>
    public string? MaintenanceCompany { get; set; }

        /// <summary>
    /// 维护技师
    /// </summary>
    public string? MaintenanceTechnician { get; set; }

        /// <summary>
    /// 维护日期
    /// </summary>
    public DateTime MaintenanceDate { get; set; }

        /// <summary>
    /// 维护开始时间
    /// </summary>
    public DateTime? MaintenanceStartTime { get; set; }

        /// <summary>
    /// 维护结束时间
    /// </summary>
    public DateTime? MaintenanceEndTime { get; set; }

        /// <summary>
    /// 维护内容
    /// </summary>
    public string? MaintenanceContent { get; set; }

        /// <summary>
    /// 故障描述
    /// </summary>
    public string? FaultDescription { get; set; }

        /// <summary>
    /// 处理方案
    /// </summary>
    public string? Solution { get; set; }

        /// <summary>
    /// 使用配件
    /// </summary>
    public string? UsedParts { get; set; }

        /// <summary>
    /// 维护费用
    /// </summary>
    public decimal MaintenanceCost { get; set; }

        /// <summary>
    /// 维护结果
    /// </summary>
    public int MaintenanceResult { get; set; }

        /// <summary>
    /// 维护状态
    /// </summary>
    public int MaintenanceStatus { get; set; }

        /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; }

        /// <summary>
    /// 维护文档
    /// </summary>
    public string? MaintenanceDocuments { get; set; }

        /// <summary>
    /// 维护图片
    /// </summary>
    public string? MaintenanceImages { get; set; }

        /// <summary>
    /// 验收总结
    /// </summary>
    public string? AcceptedSummary { get; set; }

        /// <summary>
    /// 验收人
    /// </summary>
    public string? AcceptedBy { get; set; }

        /// <summary>
    /// 验收时间
    /// </summary>
    public DateTime? AcceptedAt { get; set; }

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
/// Takt更新设备维护记录表DTO
/// </summary>
public partial class TaktMaintenanceUpdateDto : TaktMaintenanceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMaintenanceUpdateDto()
    {
    }

        /// <summary>
    /// 设备维护记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaintenanceId { get; set; }
}

/// <summary>
/// 设备维护记录表维护状态DTO
/// </summary>
public partial class TaktMaintenanceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMaintenanceStatusDto()
    {
    }

        /// <summary>
    /// 设备维护记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaintenanceId { get; set; }

    /// <summary>
    /// 维护状态（0=禁用，1=启用）
    /// </summary>
    public int MaintenanceStatus { get; set; }
}

/// <summary>
/// 设备维护记录表导入模板DTO
/// </summary>
public partial class TaktMaintenanceTemplateDto
{
        /// <summary>
    /// 设备ID
    /// </summary>
    public long EquipmentId { get; set; }

        /// <summary>
    /// 维护类型
    /// </summary>
    public int MaintenanceType { get; set; }

        /// <summary>
    /// 维护单位
    /// </summary>
    public string? MaintenanceCompany { get; set; }

        /// <summary>
    /// 维护技师
    /// </summary>
    public string? MaintenanceTechnician { get; set; }

        /// <summary>
    /// 维护日期
    /// </summary>
    public DateTime MaintenanceDate { get; set; }

        /// <summary>
    /// 维护开始时间
    /// </summary>
    public DateTime? MaintenanceStartTime { get; set; }

        /// <summary>
    /// 维护结束时间
    /// </summary>
    public DateTime? MaintenanceEndTime { get; set; }

        /// <summary>
    /// 维护内容
    /// </summary>
    public string? MaintenanceContent { get; set; }

        /// <summary>
    /// 故障描述
    /// </summary>
    public string? FaultDescription { get; set; }

        /// <summary>
    /// 处理方案
    /// </summary>
    public string? Solution { get; set; }

        /// <summary>
    /// 使用配件
    /// </summary>
    public string? UsedParts { get; set; }

        /// <summary>
    /// 维护费用
    /// </summary>
    public decimal MaintenanceCost { get; set; }

        /// <summary>
    /// 维护结果
    /// </summary>
    public int MaintenanceResult { get; set; }

        /// <summary>
    /// 维护状态
    /// </summary>
    public int MaintenanceStatus { get; set; }

        /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; }

        /// <summary>
    /// 维护文档
    /// </summary>
    public string? MaintenanceDocuments { get; set; }

        /// <summary>
    /// 维护图片
    /// </summary>
    public string? MaintenanceImages { get; set; }

        /// <summary>
    /// 验收总结
    /// </summary>
    public string? AcceptedSummary { get; set; }

        /// <summary>
    /// 验收人
    /// </summary>
    public string? AcceptedBy { get; set; }

        /// <summary>
    /// 验收时间
    /// </summary>
    public DateTime? AcceptedAt { get; set; }

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
/// 设备维护记录表导入DTO
/// </summary>
public partial class TaktMaintenanceImportDto
{
        /// <summary>
    /// 设备ID
    /// </summary>
    public long EquipmentId { get; set; }

        /// <summary>
    /// 维护类型
    /// </summary>
    public int MaintenanceType { get; set; }

        /// <summary>
    /// 维护单位
    /// </summary>
    public string? MaintenanceCompany { get; set; }

        /// <summary>
    /// 维护技师
    /// </summary>
    public string? MaintenanceTechnician { get; set; }

        /// <summary>
    /// 维护日期
    /// </summary>
    public DateTime MaintenanceDate { get; set; }

        /// <summary>
    /// 维护开始时间
    /// </summary>
    public DateTime? MaintenanceStartTime { get; set; }

        /// <summary>
    /// 维护结束时间
    /// </summary>
    public DateTime? MaintenanceEndTime { get; set; }

        /// <summary>
    /// 维护内容
    /// </summary>
    public string? MaintenanceContent { get; set; }

        /// <summary>
    /// 故障描述
    /// </summary>
    public string? FaultDescription { get; set; }

        /// <summary>
    /// 处理方案
    /// </summary>
    public string? Solution { get; set; }

        /// <summary>
    /// 使用配件
    /// </summary>
    public string? UsedParts { get; set; }

        /// <summary>
    /// 维护费用
    /// </summary>
    public decimal MaintenanceCost { get; set; }

        /// <summary>
    /// 维护结果
    /// </summary>
    public int MaintenanceResult { get; set; }

        /// <summary>
    /// 维护状态
    /// </summary>
    public int MaintenanceStatus { get; set; }

        /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; }

        /// <summary>
    /// 维护文档
    /// </summary>
    public string? MaintenanceDocuments { get; set; }

        /// <summary>
    /// 维护图片
    /// </summary>
    public string? MaintenanceImages { get; set; }

        /// <summary>
    /// 验收总结
    /// </summary>
    public string? AcceptedSummary { get; set; }

        /// <summary>
    /// 验收人
    /// </summary>
    public string? AcceptedBy { get; set; }

        /// <summary>
    /// 验收时间
    /// </summary>
    public DateTime? AcceptedAt { get; set; }

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
/// 设备维护记录表导出DTO
/// </summary>
public partial class TaktMaintenanceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMaintenanceExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 设备ID
    /// </summary>
    public long EquipmentId { get; set; }

        /// <summary>
    /// 维护类型
    /// </summary>
    public int MaintenanceType { get; set; }

        /// <summary>
    /// 维护单位
    /// </summary>
    public string? MaintenanceCompany { get; set; }

        /// <summary>
    /// 维护技师
    /// </summary>
    public string? MaintenanceTechnician { get; set; }

        /// <summary>
    /// 维护日期
    /// </summary>
    public DateTime MaintenanceDate { get; set; }

        /// <summary>
    /// 维护开始时间
    /// </summary>
    public DateTime? MaintenanceStartTime { get; set; }

        /// <summary>
    /// 维护结束时间
    /// </summary>
    public DateTime? MaintenanceEndTime { get; set; }

        /// <summary>
    /// 维护内容
    /// </summary>
    public string? MaintenanceContent { get; set; }

        /// <summary>
    /// 故障描述
    /// </summary>
    public string? FaultDescription { get; set; }

        /// <summary>
    /// 处理方案
    /// </summary>
    public string? Solution { get; set; }

        /// <summary>
    /// 使用配件
    /// </summary>
    public string? UsedParts { get; set; }

        /// <summary>
    /// 维护费用
    /// </summary>
    public decimal MaintenanceCost { get; set; }

        /// <summary>
    /// 维护结果
    /// </summary>
    public int MaintenanceResult { get; set; }

        /// <summary>
    /// 维护状态
    /// </summary>
    public int MaintenanceStatus { get; set; }

        /// <summary>
    /// 下次维护日期
    /// </summary>
    public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>
    /// 维护周期（天）
    /// </summary>
    public int MaintenanceCycleDays { get; set; }

        /// <summary>
    /// 维护文档
    /// </summary>
    public string? MaintenanceDocuments { get; set; }

        /// <summary>
    /// 维护图片
    /// </summary>
    public string? MaintenanceImages { get; set; }

        /// <summary>
    /// 验收总结
    /// </summary>
    public string? AcceptedSummary { get; set; }

        /// <summary>
    /// 验收人
    /// </summary>
    public string? AcceptedBy { get; set; }

        /// <summary>
    /// 验收时间
    /// </summary>
    public DateTime? AcceptedAt { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}