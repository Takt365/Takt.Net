// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowForm.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt流程表单实体，定义工作流表单领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// Takt流程表单实体（表单定义，与流程方案解耦）
/// </summary>
/// <remarks>
/// 关联：表单被方案引用（TaktFlowScheme.FormId/FormCode）；一个表单可被多个方案复用。实例启动时可快照 FormId/FormCode 到 TaktFlowInstance。
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
    /// 表单编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "form_code", ColumnDescription = "表单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string FormCode { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    [SugarColumn(ColumnName = "form_name", ColumnDescription = "表单名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类（0=通用表单，1=业务表单，2=系统表单）
    /// </summary>
    [SugarColumn(ColumnName = "form_category", ColumnDescription = "表单分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormCategory { get; set; } = 0;

    /// <summary>
    /// 表单类型（0=动态表单，1=静态表单，2=自定义表单）
    /// </summary>
    [SugarColumn(ColumnName = "form_type", ColumnDescription = "表单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormType { get; set; } = 0;

    /// <summary>
    /// 表单配置（JSON格式，存储表单设计配置、字段定义等）
    /// </summary>
    [SugarColumn(ColumnName = "form_config", ColumnDescription = "表单配置", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? FormConfig { get; set; }

    /// <summary>
    /// 表单模板（HTML模板或JSON模板）
    /// </summary>
    [SugarColumn(ColumnName = "form_template", ColumnDescription = "表单模板", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    [SugarColumn(ColumnName = "form_version", ColumnDescription = "表单版本号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "1.0.0")]
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 是否启用数据源（0=否，1=是）。启用时表示表单绑定数据表，由 RelatedDataBaseName 指定来源。
    /// </summary>
    [SugarColumn(ColumnName = "is_datasource", ColumnDescription = "是否启用数据源", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsDatasource { get; set; } = 0;

    /// <summary>
    /// 关联数据库名（选中的数据库）：从 appsettings.dbConfigs 的 Conn 解析的数据库名（如 Takt_Identity_Dev），开发/生产环境不同，不允许纯数字。
    /// </summary>
    [SugarColumn(ColumnName = "related_data_base_name", ColumnDescription = "关联数据库名", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? RelatedDataBaseName { get; set; }

    /// <summary>
    /// 关联表名：通过 RelatedDataBaseName 选项选中的表名，对应 GetTables(configId) 返回的 TableName。
    /// </summary>
    [SugarColumn(ColumnName = "related_table_name", ColumnDescription = "关联表名", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? RelatedTableName { get; set; }

    /// <summary>
    /// 关联表单字段（JSON 数组）：通过 RelatedTableName 选项选中的、要显示在表表单中的列名，如 ["id","name","code"]，对应 GetColumns 返回的 DbColumnName。
    /// </summary>
    [SugarColumn(ColumnName = "related_form_field", ColumnDescription = "关联表单字段", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? RelatedFormField { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    [SugarColumn(ColumnName = "form_status", ColumnDescription = "表单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormStatus { get; set; } = 0;
}
