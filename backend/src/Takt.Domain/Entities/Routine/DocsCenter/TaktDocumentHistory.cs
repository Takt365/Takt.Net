// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.DocsCenter
// 文件名称：TaktDocumentHistory.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心 · 文档变更记录实体，记录文档字段级变更；变更时间/变更人由基类审计字段 CreateTime/CreateId/CreateBy 表示
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.DocsCenter;

/// <summary>
/// 文控中心 · 文档变更记录实体
/// </summary>
/// <remarks>记录文档字段级变更，便于审计与追溯。变更时间、变更人ID、变更人姓名由基类 TaktEntityBase 的 CreateTime、CreateId、CreateBy 表示，不重复冗余。</remarks>
[SugarTable("takt_routine_docscenter_document_history", "文档变更记录表")]
[SugarIndex("ix_takt_routine_docscenter_document_history_document_id", nameof(DocumentId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_history_create_time", nameof(CreateTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_docscenter_document_history_create_id", nameof(CreateId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_history_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_document_history_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktDocumentHistory : TaktEntityBase
{
    /// <summary>
    /// 文档ID（关联文档表，序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "document_id", ColumnDescription = "文档ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DocumentId { get; set; }

    /// <summary>
    /// 文档编码
    /// </summary>
    [SugarColumn(ColumnName = "document_code", ColumnDescription = "文档编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string DocumentCode { get; set; } = string.Empty;

    /// <summary>
    /// 变更字段名
    /// </summary>
    [SugarColumn(ColumnName = "change_field", ColumnDescription = "变更字段名", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ChangeField { get; set; } = string.Empty;

    /// <summary>
    /// 原值（JSON 格式存储，支持复杂类型）
    /// </summary>
    [SugarColumn(ColumnName = "old_value", ColumnDescription = "原值", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? OldValue { get; set; }

    /// <summary>
    /// 新值（JSON 格式存储，支持复杂类型）
    /// </summary>
    [SugarColumn(ColumnName = "new_value", ColumnDescription = "新值", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? NewValue { get; set; }

    /// <summary>
    /// 变更原因
    /// </summary>
    [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ChangeReason { get; set; }
}
