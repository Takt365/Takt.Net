// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.DocsCenter
// 文件名称：TaktDocument.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心 · 文档实体，文件控制需经工作流审批（关联流程实例）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.DocsCenter;

/// <summary>
/// 文控中心 · 文档实体
/// </summary>
/// <remarks>
/// 支持 OA 发文（拟稿-核稿-会签-签发-编号-分发-归档）与收文（登记-拟办-批办-承办-签收-归档）及 ISO 全生命周期（创建-审批-发布-使用-变更-归档-销毁）。
/// 审批通过 InstanceId 关联 TaktFlowInstance；历史记录见 TaktDocumentHistory，签收见 TaktDocumentReceipt。
/// </remarks>
[SugarTable("takt_routine_docscenter_document", "文档表")]
[SugarIndex("ix_takt_routine_docscenter_document_code", nameof(DocumentCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_docscenter_document_status", nameof(DocumentStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_apply_time", nameof(ApplyTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_docscenter_document_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_direction", nameof(Direction), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_lifecycle_stage", nameof(LifecycleStage), OrderByType.Asc)]
public class TaktDocument : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码（关联工厂主数据 TaktPlant.PlantCode；冗余便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 文档编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "document_code", ColumnDescription = "文档编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string DocumentCode { get; set; } = string.Empty;

    /// <summary>
    /// 文档标题
    /// </summary>
    [SugarColumn(ColumnName = "document_title", ColumnDescription = "文档标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string DocumentTitle { get; set; } = string.Empty;

    /// <summary>
    /// 文档类型（0=发布，1=变更，2=废止，3=其他）
    /// </summary>
    [SugarColumn(ColumnName = "document_type", ColumnDescription = "文档类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DocumentType { get; set; } = 0;

    /// <summary>
    /// 文档版本号
    /// </summary>
    [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DocumentVersion { get; set; }

    /// <summary>
    /// 关联工作流实例ID（对应 TaktFlowInstance.Id，0=未关联；流程处理见 TaktFlowInstanceService：发起时按 ProcessKey+BusinessKey 回写本字段，结束时按本字段查找并更新状态；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 文档状态（0=草稿，1=审批中，2=已批准，3=已驳回，4=已发布，5=已废止；与工作流审批流程匹配）
    /// </summary>
    [SugarColumn(ColumnName = "document_status", ColumnDescription = "文档状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DocumentStatus { get; set; } = 0;

    /// <summary>
    /// 申请人ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApplicantId { get; set; }

    /// <summary>
    /// 申请人姓名
    /// </summary>
    [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ApplicantName { get; set; } = string.Empty;

    /// <summary>
    /// 申请部门ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_id", ColumnDescription = "申请部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }

    /// <summary>
    /// 申请部门名称
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_name", ColumnDescription = "申请部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ApplicantDeptName { get; set; }

    /// <summary>
    /// 申请时间
    /// </summary>
    [SugarColumn(ColumnName = "apply_time", ColumnDescription = "申请时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApplyTime { get; set; }

    /// <summary>
    /// 批准时间
    /// </summary>
    [SugarColumn(ColumnName = "approved_time", ColumnDescription = "批准时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApprovedTime { get; set; }

    /// <summary>
    /// 关联文件ID（对应 TaktFile.Id，可选；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "file_id", ColumnDescription = "关联文件ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FileId { get; set; }

    // ----- 发文/收文与全生命周期（ISO 管控） -----

    /// <summary>
    /// 收发文方向（0=发文，1=收文）
    /// </summary>
    [SugarColumn(ColumnName = "direction", ColumnDescription = "收发文方向", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Direction { get; set; } = 0;

    /// <summary>
    /// 文种（如通知、报告、制度等；可对接字典，0=通知，1=报告，2=制度，3=规定，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "document_category", ColumnDescription = "文种", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DocumentCategory { get; set; } = 0;

    /// <summary>
    /// 生命周期阶段（0=创建，1=审批，2=发布，3=使用，4=变更，5=归档，6=销毁；与 TaktDocumentLifecycleStage 一致）
    /// </summary>
    [SugarColumn(ColumnName = "lifecycle_stage", ColumnDescription = "生命周期阶段", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LifecycleStage { get; set; } = 0;

    /// <summary>
    /// 保管期限（年；归档后按此期限管理，到期可审批销毁）
    /// </summary>
    [SugarColumn(ColumnName = "retention_years", ColumnDescription = "保管期限年", ColumnDataType = "int", IsNullable = true)]
    public int? RetentionYears { get; set; }

    /// <summary>
    /// 生效时间（发布后生效）
    /// </summary>
    [SugarColumn(ColumnName = "effective_time", ColumnDescription = "生效时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EffectiveTime { get; set; }

    /// <summary>
    /// 归档时间
    /// </summary>
    [SugarColumn(ColumnName = "archive_time", ColumnDescription = "归档时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ArchiveTime { get; set; }

    /// <summary>
    /// 作废时间（废止时写入）
    /// </summary>
    [SugarColumn(ColumnName = "obsolete_time", ColumnDescription = "作废时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ObsoleteTime { get; set; }
}
