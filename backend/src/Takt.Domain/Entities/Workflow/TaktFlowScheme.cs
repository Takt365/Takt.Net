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
/// Takt流程方案实体（流程定义）
/// </summary>
/// <remarks>
/// <para>
/// 关联：1 方案 → N 实例（Instance.SchemeId）；方案 → 0..1 表单（FormId/FormCode 关联 TaktFlowForm）。
/// 发布后（ProcessStatus=1）可被启动生成实例。
/// </para>
/// <para>
/// <b>SchemeContent</b> 与前端流程设计器 <c>frontend/takt.antd/src/components/business/takt-flow-antflow-designer</c>（<c>index.vue</c> 中
/// <c>parseProcessContent</c> / <c>toProcessContent</c>）及流程引擎解析使用<strong>同一套 JSON</strong>，推荐持久化形态为：
/// </para>
/// <list type="bullet">
/// <item><description>
/// 根对象为 <c>{ "nodes": [...], "edges": [...], "flowTree": { ... } }</c>（属性名均为 camelCase）。
/// <c>nodes</c> / <c>edges</c> 供引擎执行；节点字段与 <c>takt-flow-tree-graph-convert.ts</c> 中 <c>GraphNode</c>、<c>GraphEdge</c> 对齐（如 <c>id</c>、<c>type</c>、<c>assigneeType</c>、<c>from</c>、<c>to</c>、<c>condition</c> 等）。
/// </description></item>
/// <item><description>
/// <c>flowTree</c> 为 <c>takt-flow-tree.ts</c> 中 <c>FlowTreeNode</c> 树（<c>nodeType</c> 1～7：发起人、网关、条件、审批、抄送、并行等）。
/// 当 <c>flowTree</c> 存在且根节点 <c>nodeType == 1</c>（发起人）时，设计器以 <c>flowTree</c> 为权威画布结构；否则由 <c>nodes</c>/<c>edges</c> 经 <c>graphToTree</c> 还原，网关汇合可能与仅保存图时不一致，故<strong>建议始终写入 flowTree</strong>（与种子数据一致）。
/// </description></item>
/// <item><description>
/// 设计器输出仍包含 <c>nodes</c>/<c>edges</c> 以便引擎消费；表单绑定见 <c>scheme-form.vue</c> 中 <c>v-model="form.SchemeContent"</c>。
/// </description></item>
/// </list>
/// </remarks>
[SugarTable("takt_workflow_scheme", "流程方案表")]
[SugarIndex("ix_takt_workflow_scheme_scheme_key", nameof(SchemeKey), OrderByType.Asc, true)]
[SugarIndex("ix_takt_workflow_scheme_scheme_category", nameof(SchemeCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_scheme_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_scheme_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_scheme_scheme_status", nameof(SchemeStatus), OrderByType.Asc)]
public class TaktFlowScheme : TaktEntityBase
{
    /// <summary>
    /// 方案Key（唯一索引，用于标识流程类型）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_key", ColumnDescription = "方案Key", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string SchemeKey { get; set; } = string.Empty;

    /// <summary>
    /// 方案名称
    /// </summary>
    [SugarColumn(ColumnName = "scheme_name", ColumnDescription = "方案名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string SchemeName { get; set; } = string.Empty;

    /// <summary>
    /// 方案分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_category", ColumnDescription = "方案分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SchemeCategory { get; set; } = 0;

    /// <summary>
    /// 方案版本号
    /// </summary>
    [SugarColumn(ColumnName = "scheme_version", ColumnDescription = "方案版本号", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int SchemeVersion { get; set; } = 1;

    /// <summary>
    /// 方案描述
    /// </summary>
    [SugarColumn(ColumnName = "scheme_description", ColumnDescription = "方案描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? SchemeDescription { get; set; }

    /// <summary>
    /// 方案表单ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "form_id", ColumnDescription = "方案表单ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 方案表单编码
    /// </summary>
    [SugarColumn(ColumnName = "form_code", ColumnDescription = "方案表单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? FormCode { get; set; }

    /// <summary>
    /// 方案内容（JSON：nodes、edges、可选 flowTree；与 takt-flow-antflow-designer 及引擎一致，见类备注）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_content", ColumnDescription = "方案内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? SchemeContent { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 方案状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_status", ColumnDescription = "方案状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SchemeStatus { get; set; } = 0;
}
