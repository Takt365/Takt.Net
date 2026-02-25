// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowScheme.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt流程方案实体，定义工作流流程方案领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// 流程方案实体（一条流程模板记录，存流程定义与配置）
/// </summary>
/// <remarks>
/// 术语：流程方案 = 本实体/表记录；流程定义 = BPMN 2.0 XML（仅存于 BpmnXml 字段）。执行引擎只根据 BpmnXml 解析构建图，无 BpmnXml 时用内置默认图；ProcessJson 为历史遗留字段，不参与执行。
/// ProcessKey 对应 BPM definition key，ProcessVersion 对应 version。ProcessStatus：0=草稿，1=已发布，2=已停用。
/// </remarks>
[SugarTable("takt_workflow_scheme", "流程方案表")]
[SugarIndex("ix_takt_workflow_scheme_process_key", nameof(ProcessKey), OrderByType.Asc, true)]
[SugarIndex("ix_takt_workflow_scheme_process_category", nameof(ProcessCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_scheme_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_scheme_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_scheme_process_status", nameof(ProcessStatus), OrderByType.Asc)]
public class TaktFlowScheme : TaktEntityBase
{
    /// <summary>
    /// 流程Key（BPM Process Definition Key；唯一索引，用于标识流程类型）
    /// </summary>
    [SugarColumn(ColumnName = "process_key", ColumnDescription = "流程Key", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称（BPM Process Definition Name）
    /// </summary>
    [SugarColumn(ColumnName = "process_name", ColumnDescription = "流程名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程分类（BPM 分类；0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    [SugarColumn(ColumnName = "process_category", ColumnDescription = "流程分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ProcessCategory { get; set; } = 0;

    /// <summary>
    /// 流程版本号（BPM Process Definition Version）
    /// </summary>
    [SugarColumn(ColumnName = "process_version", ColumnDescription = "流程版本号", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int ProcessVersion { get; set; } = 1;

    /// <summary>
    /// 流程描述
    /// </summary>
    [SugarColumn(ColumnName = "process_description", ColumnDescription = "流程描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单ID（BPM 启动/任务表单引用；序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "form_id", ColumnDescription = "流程表单ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码（BPM Form Key）
    /// </summary>
    [SugarColumn(ColumnName = "form_code", ColumnDescription = "流程表单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? FormCode { get; set; }

    /// <summary>
    /// 流程定义（BPMN 2.0 XML，设计期唯一标准；引擎仅解析本字段构建可执行图，符合 BPM 规范）
    /// </summary>
    [SugarColumn(ColumnName = "bpmn_xml", ColumnDescription = "BPMN流程定义", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? BpmnXml { get; set; }

    /// <summary>
    /// 流程图缓存（JSON，遗留/可选；不作为 BPM 规范定义，引擎不以本字段构建流程）
    /// </summary>
    [SugarColumn(ColumnName = "process_json", ColumnDescription = "流程JSON缓存", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ProcessJson { get; set; }

    /// <summary>
    /// 流程图标
    /// </summary>
    [SugarColumn(ColumnName = "process_icon", ColumnDescription = "流程图标", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ProcessIcon { get; set; }

    /// <summary>
    /// 是否支持挂起（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_suspendable", ColumnDescription = "是否支持挂起", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsSuspendable { get; set; } = 1;

    /// <summary>
    /// 是否支持撤回（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_revocable", ColumnDescription = "是否支持撤回", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsRevocable { get; set; } = 1;

    /// <summary>
    /// 是否支持转办（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_transferable", ColumnDescription = "是否支持转办", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsTransferable { get; set; } = 1;

    /// <summary>
    /// 是否支持加签（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_addsignable", ColumnDescription = "是否支持加签", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsAddsignable { get; set; } = 0;

    /// <summary>
    /// 是否支持减签（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_reduce_signable", ColumnDescription = "是否支持减签", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsReduceSignable { get; set; } = 0;

    /// <summary>
    /// 是否支持退回（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_returnable", ColumnDescription = "是否支持退回", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsReturnable { get; set; } = 1;

    /// <summary>
    /// 是否自动完成（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_auto_complete", ColumnDescription = "是否自动完成", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsAutoComplete { get; set; } = 0;

    /// <summary>
    /// 超时配置（JSON格式，存储超时时间、超时处理方式等）
    /// </summary>
    [SugarColumn(ColumnName = "timeout_config", ColumnDescription = "超时配置", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? TimeoutConfig { get; set; }

    /// <summary>
    /// 通知配置（JSON格式，存储通知方式、通知对象等）
    /// </summary>
    [SugarColumn(ColumnName = "notification_config", ColumnDescription = "通知配置", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? NotificationConfig { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 流程状态（BPM 部署状态：0=Draft 草稿，1=Deployed 已发布，2=Retired 已停用）
    /// </summary>
    [SugarColumn(ColumnName = "process_status", ColumnDescription = "流程状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ProcessStatus { get; set; } = 0;
}
