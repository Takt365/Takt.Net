// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowForm.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt流程表单实体，定义工作流表单领域模型；含 OpenAuth Form 迁移用字段（Fields/ContentParse/DataSource/DeptId）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// Takt流程表单实体（BPM 表单定义）
/// </summary>
/// <remarks>
/// 对应 BPM 中的表单定义 (Form Definition) / 启动表单、用户任务表单 (Start Form / User Task Form)。
/// FormCode 作为表单唯一键供流程定义引用；非空字段：FormCode、FormName、FormCategory、FormType、FormVersion、OrderNum、FormStatus、DeptId（默认 0）；可空：FormTemplate、Fields、ContentParse、DataSource。
/// </remarks>
[SugarTable("takt_workflow_form", "流程表单表")]
[SugarIndex("ix_takt_workflow_form_form_code", nameof(FormCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_workflow_form_form_category", nameof(FormCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_form_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_form_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_form_form_status", nameof(FormStatus), OrderByType.Asc)]
public class TaktFlowForm : TaktEntityBase
{
    /// <summary>
    /// 表单编码（BPM Form Key，唯一索引，供流程定义引用）
    /// </summary>
    [SugarColumn(ColumnName = "form_code", ColumnDescription = "表单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string FormCode { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称（BPM Form Name，非空）
    /// </summary>
    [SugarColumn(ColumnName = "form_name", ColumnDescription = "表单名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类（BPM 分类；0=通用表单，1=业务表单，2=系统表单，非空，默认 0）
    /// </summary>
    [SugarColumn(ColumnName = "form_category", ColumnDescription = "表单分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormCategory { get; set; } = 0;

    /// <summary>
    /// 表单类型（0=动态表单，1=静态表单，非空，默认 0）
    /// </summary>
    [SugarColumn(ColumnName = "form_type", ColumnDescription = "表单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormType { get; set; } = 0;

    /// <summary>
    /// 表单模板（HTML/JSON，可空）
    /// </summary>
    [SugarColumn(ColumnName = "form_template", ColumnDescription = "表单模板", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 字段个数（OpenAuth 迁移，可空）
    /// </summary>
    [SugarColumn(ColumnName = "fields", ColumnDescription = "字段个数", ColumnDataType = "int", IsNullable = true)]
    public int? Fields { get; set; }

    /// <summary>
    /// 表单控件位置模板（OpenAuth 迁移，可空）
    /// </summary>
    [SugarColumn(ColumnName = "content_parse", ColumnDescription = "控件位置模板", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ContentParse { get; set; }

    /// <summary>
    /// 数据源（如：Takt_Identity_Dev:0，可空）
    /// </summary>
    [SugarColumn(ColumnName = "data_source", ColumnDescription = "数据源", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DataSource { get; set; }

    /// <summary>
    /// 部门ID（非空，0 表示未指定；OpenAuth 迁移时 OrgId 可解析为 long 写入）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 表单版本号（非空，默认 1.0.0）
    /// </summary>
    [SugarColumn(ColumnName = "form_version", ColumnDescription = "表单版本号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "1.0.0")]
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 排序号（非空，默认 0，越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 表单状态（BPM 发布状态：0=草稿，1=已发布，2=已停用，非空，默认 0）
    /// </summary>
    [SugarColumn(ColumnName = "form_status", ColumnDescription = "表单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormStatus { get; set; } = 0;
}
