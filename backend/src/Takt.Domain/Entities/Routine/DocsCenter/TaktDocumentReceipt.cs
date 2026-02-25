// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.DocsCenter
// 文件名称：TaktDocumentReceipt.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心 · 文档签收实体，记录收文签收、分发签收等，支持 OA 收发文流程中的“谁在何时签收”
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.DocsCenter;

/// <summary>
/// 文控中心 · 文档签收实体
/// </summary>
/// <remarks>用于收文流程的“登记-拟办-批办-承办-签收-归档”及发文流程的“分发签收”。签收类型：0=收文签收，1=分发签收。签收时间、签收人ID、签收人姓名由基类 CreateTime、CreateId、CreateBy 表示。</remarks>
[SugarTable("takt_routine_docscenter_document_receipt", "文档签收表")]
[SugarIndex("ix_takt_routine_docscenter_doc_receipt_document_id", nameof(DocumentId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_doc_receipt_create_id", nameof(CreateId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_doc_receipt_create_time", nameof(CreateTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_docscenter_doc_receipt_sign_status", nameof(SignStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_doc_receipt_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_docscenter_doc_receipt_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktDocumentReceipt : TaktEntityBase
{
    /// <summary>
    /// 关联文档ID（对应 TaktDocument.Id；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "document_id", ColumnDescription = "文档ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DocumentId { get; set; }

    /// <summary>
    /// 签收类型（0=收文签收，1=分发签收）
    /// </summary>
    [SugarColumn(ColumnName = "receipt_type", ColumnDescription = "签收类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReceiptType { get; set; } = 0;

    /// <summary>
    /// 签收状态（0=待签收，1=已签收）
    /// </summary>
    [SugarColumn(ColumnName = "sign_status", ColumnDescription = "签收状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SignStatus { get; set; } = 0;

    /// <summary>
    /// 签收备注
    /// </summary>
    [SugarColumn(ColumnName = "comment", ColumnDescription = "签收备注", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Comment { get; set; }
}
