// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource
// 文件名称：TaktEmployeeAttachment.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工附件实体，定义员工附件领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工附件实体
/// </summary>
[SugarTable("takt_humanresource_employee_attachment", "员工附件表")]
[SugarIndex("ix_takt_humanresource_employee_attachment_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_attachment_file_id", nameof(FileId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_attachment_attachment_type", nameof(AttachmentType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_attachment_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_attachment_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeAttachment : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 文件ID（关联TaktFile，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "file_id", ColumnDescription = "文件ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    [SugarColumn(ColumnName = "file_code", ColumnDescription = "文件编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string FileCode { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径
    /// </summary>
    [SugarColumn(ColumnName = "file_path", ColumnDescription = "文件路径", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    [SugarColumn(ColumnName = "file_size", ColumnDescription = "文件大小（字节）", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long FileSize { get; set; } = 0;

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    [SugarColumn(ColumnName = "file_type", ColumnDescription = "文件类型", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? FileType { get; set; }

    /// <summary>
    /// 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "attachment_type", ColumnDescription = "附件类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
    public int AttachmentType { get; set; } = 5;

    /// <summary>
    /// 附件描述
    /// </summary>
    [SugarColumn(ColumnName = "attachment_description", ColumnDescription = "附件描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? AttachmentDescription { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
