// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcAttachment.cs
// 功能描述：设变附件实体，一个设变可对应多条附件；类别：联络、EPP、FPP、外部联络、TCJ 等。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变附件实体。文件类别：Liaison/EPP/FPP/ExternalLiaison/TCJ 等；文件编号为联络编号等。
/// </summary>
[SugarTable("takt_logistics_manufacturing_ecn_attachment", "设变附件表")]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_attachment_ecn_id", nameof(EcnId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_attachment_attachment_type", nameof(AttachmentType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_attachment_doc_no", nameof(DocNo), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_attachment_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_attachment_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEcAttachment : TaktEntityBase
{
    /// <summary>
    /// 设变主表ID
    /// </summary>
    [SugarColumn(ColumnName = "ecn_id", ColumnDescription = "设变ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EcnId { get; set; }

    /// <summary>
    /// 文件类别：Liaison=联络, EPP, FPP, ExternalLiaison=外部联络, TCJ 等
    /// </summary>
    [SugarColumn(ColumnName = "attachment_type", ColumnDescription = "文件类别", ColumnDataType = "nvarchar", Length = 30, IsNullable = true)]
    public string? AttachmentType { get; set; }

    /// <summary>
    /// 文件编号（如联络编号等）
    /// </summary>
    [SugarColumn(ColumnName = "doc_no", ColumnDescription = "文件编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DocNo { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? FileName { get; set; }

    /// <summary>
    /// 访问地址（URL）
    /// </summary>
    [SugarColumn(ColumnName = "access_url", ColumnDescription = "访问地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
    public string AccessUrl { get; set; } = string.Empty;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 设变主表（多对一）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(EcnId))]
    public TaktEc? Ecn { get; set; }
}
