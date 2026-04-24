// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktProductionTeamDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：生产班组表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 生产班组表Dto
/// </summary>
public partial class TaktProductionTeamDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamDto()
    {
        PlantCode = string.Empty;
        TeamCode = string.Empty;
        TeamName = string.Empty;
    }

    /// <summary>
    /// 生产班组表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionTeamId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 班组编码
    /// </summary>
    public string TeamCode { get; set; }
    /// <summary>
    /// 班组名称
    /// </summary>
    public string TeamName { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }
    /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }
    /// <summary>
    /// 班组长员工Id
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TeamLeaderId { get; set; }
    /// <summary>
    /// 班组长姓名
    /// </summary>
    public string? TeamLeaderName { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }
    /// <summary>
    /// 启用状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 生产班组表查询DTO
/// </summary>
public partial class TaktProductionTeamQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 生产班组表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionTeamId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 班组编码
    /// </summary>
    public string? TeamCode { get; set; }
    /// <summary>
    /// 班组名称
    /// </summary>
    public string? TeamName { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }
    /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }
    /// <summary>
    /// 班组长员工Id
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TeamLeaderId { get; set; }
    /// <summary>
    /// 班组长姓名
    /// </summary>
    public string? TeamLeaderName { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }
    /// <summary>
    /// 启用状态
    /// </summary>
    public int? Status { get; set; }

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
/// Takt创建生产班组表DTO
/// </summary>
public partial class TaktProductionTeamCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamCreateDto()
    {
        PlantCode = string.Empty;
        TeamCode = string.Empty;
        TeamName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string TeamName { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班组长员工Id
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TeamLeaderId { get; set; }

        /// <summary>
    /// 班组长姓名
    /// </summary>
    public string? TeamLeaderName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }

        /// <summary>
    /// 启用状态
    /// </summary>
    public int Status { get; set; }

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
/// Takt更新生产班组表DTO
/// </summary>
public partial class TaktProductionTeamUpdateDto : TaktProductionTeamCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamUpdateDto()
    {
    }

        /// <summary>
    /// 生产班组表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionTeamId { get; set; }
}

/// <summary>
/// 生产班组表启用状态DTO
/// </summary>
public partial class TaktProductionTeamStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamStatusDto()
    {
    }

        /// <summary>
    /// 生产班组表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionTeamId { get; set; }

    /// <summary>
    /// 启用状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 生产班组表导入模板DTO
/// </summary>
public partial class TaktProductionTeamTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamTemplateDto()
    {
        PlantCode = string.Empty;
        TeamCode = string.Empty;
        TeamName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string TeamName { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班组长员工Id
    /// </summary>
    public long? TeamLeaderId { get; set; }

        /// <summary>
    /// 班组长姓名
    /// </summary>
    public string? TeamLeaderName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }

        /// <summary>
    /// 启用状态
    /// </summary>
    public int Status { get; set; }

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
/// 生产班组表导入DTO
/// </summary>
public partial class TaktProductionTeamImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamImportDto()
    {
        PlantCode = string.Empty;
        TeamCode = string.Empty;
        TeamName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string TeamName { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班组长员工Id
    /// </summary>
    public long? TeamLeaderId { get; set; }

        /// <summary>
    /// 班组长姓名
    /// </summary>
    public string? TeamLeaderName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }

        /// <summary>
    /// 启用状态
    /// </summary>
    public int Status { get; set; }

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
/// 生产班组表导出DTO
/// </summary>
public partial class TaktProductionTeamExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        TeamCode = string.Empty;
        TeamName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string TeamName { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班组长员工Id
    /// </summary>
    public long? TeamLeaderId { get; set; }

        /// <summary>
    /// 班组长姓名
    /// </summary>
    public string? TeamLeaderName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }

        /// <summary>
    /// 启用状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}