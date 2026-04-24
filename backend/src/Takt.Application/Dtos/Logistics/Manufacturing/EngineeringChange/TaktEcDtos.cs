// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：设变主表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变主表Dto
/// </summary>
public partial class TaktEcDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDto()
    {
        PlantCode = string.Empty;
        EcnNo = string.Empty;
    }

    /// <summary>
    /// 设变主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }
    /// <summary>
    /// 发行日期
    /// </summary>
    public DateTime? EcnIssueDate { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
    /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }
    /// <summary>
    /// 设变详情
    /// </summary>
    public string? EcnDetailText { get; set; }
    /// <summary>
    /// 负责人
    /// </summary>
    public string? EcnLeader { get; set; }
    /// <summary>
    /// 损失金额
    /// </summary>
    public decimal? EcnLossAmount { get; set; }
    /// <summary>
    /// 区分
    /// </summary>
    public string? EcnDistinction { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }
    /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 工作流状态
    /// </summary>
    public string? FlowInstanceStatus { get; set; }

    /// <summary>
    /// 设变明细ID列表
    /// </summary>
    public List<long>? EcnDetailIds { get; set; }

    /// <summary>
    /// 设变附件列表（一个设变可对应多个附件）
    /// </summary>
    public List<long>? AttachmentIds { get; set; }
}

/// <summary>
/// 设变主表查询DTO
/// </summary>
public partial class TaktEcQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 设变主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 设变单号
    /// </summary>
    public string? EcnNo { get; set; }
    /// <summary>
    /// 发行日期
    /// </summary>
    public DateTime? EcnIssueDate { get; set; }

    /// <summary>
    /// 发行日期开始时间
    /// </summary>
    public DateTime? EcnIssueDateStart { get; set; }
    /// <summary>
    /// 发行日期结束时间
    /// </summary>
    public DateTime? EcnIssueDateEnd { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }
    /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }
    /// <summary>
    /// 设变详情
    /// </summary>
    public string? EcnDetailText { get; set; }
    /// <summary>
    /// 负责人
    /// </summary>
    public string? EcnLeader { get; set; }
    /// <summary>
    /// 损失金额
    /// </summary>
    public decimal? EcnLossAmount { get; set; }
    /// <summary>
    /// 区分
    /// </summary>
    public string? EcnDistinction { get; set; }
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
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

    /// <summary>
    /// 录入日期开始时间
    /// </summary>
    public DateTime? EcnEntryDateStart { get; set; }
    /// <summary>
    /// 录入日期结束时间
    /// </summary>
    public DateTime? EcnEntryDateEnd { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 工作流状态
    /// </summary>
    public string? FlowInstanceStatus { get; set; }

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
/// Takt创建设变主表DTO
/// </summary>
public partial class TaktEcCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcCreateDto()
    {
        PlantCode = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 发行日期
    /// </summary>
    public DateTime? EcnIssueDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 设变详情
    /// </summary>
    public string? EcnDetailText { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? EcnLeader { get; set; }

        /// <summary>
    /// 损失金额
    /// </summary>
    public decimal? EcnLossAmount { get; set; }

        /// <summary>
    /// 区分
    /// </summary>
    public string? EcnDistinction { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 工作流状态
    /// </summary>
    public string? FlowInstanceStatus { get; set; }

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
/// Takt更新设变主表DTO
/// </summary>
public partial class TaktEcUpdateDto : TaktEcCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcUpdateDto()
    {
    }

        /// <summary>
    /// 设变主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcId { get; set; }
}

/// <summary>
/// 设变主表状态DTO
/// </summary>
public partial class TaktEcStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcStatusDto()
    {
    }

        /// <summary>
    /// 设变主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 设变主表工作流状态DTO
/// </summary>
public partial class TaktEcFlowInstanceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcFlowInstanceStatusDto()
    {
    }

        /// <summary>
    /// 设变主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcId { get; set; }

    /// <summary>
    /// 工作流状态（0=禁用，1=启用）
    /// </summary>
    public int FlowInstanceStatus { get; set; }
}

/// <summary>
/// 设变主表导入模板DTO
/// </summary>
public partial class TaktEcTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcTemplateDto()
    {
        PlantCode = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 发行日期
    /// </summary>
    public DateTime? EcnIssueDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 设变详情
    /// </summary>
    public string? EcnDetailText { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? EcnLeader { get; set; }

        /// <summary>
    /// 损失金额
    /// </summary>
    public decimal? EcnLossAmount { get; set; }

        /// <summary>
    /// 区分
    /// </summary>
    public string? EcnDistinction { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 工作流状态
    /// </summary>
    public string? FlowInstanceStatus { get; set; }

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
/// 设变主表导入DTO
/// </summary>
public partial class TaktEcImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcImportDto()
    {
        PlantCode = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 发行日期
    /// </summary>
    public DateTime? EcnIssueDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 设变详情
    /// </summary>
    public string? EcnDetailText { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? EcnLeader { get; set; }

        /// <summary>
    /// 损失金额
    /// </summary>
    public decimal? EcnLossAmount { get; set; }

        /// <summary>
    /// 区分
    /// </summary>
    public string? EcnDistinction { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 工作流状态
    /// </summary>
    public string? FlowInstanceStatus { get; set; }

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
/// 设变主表导出DTO
/// </summary>
public partial class TaktEcExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 发行日期
    /// </summary>
    public DateTime? EcnIssueDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 设变详情
    /// </summary>
    public string? EcnDetailText { get; set; }

        /// <summary>
    /// 负责人
    /// </summary>
    public string? EcnLeader { get; set; }

        /// <summary>
    /// 损失金额
    /// </summary>
    public decimal? EcnLossAmount { get; set; }

        /// <summary>
    /// 区分
    /// </summary>
    public string? EcnDistinction { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 工作流状态
    /// </summary>
    public string? FlowInstanceStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}