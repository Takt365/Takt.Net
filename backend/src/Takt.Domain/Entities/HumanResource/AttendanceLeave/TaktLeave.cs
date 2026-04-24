// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktLeave.cs
// 功能描述：请假实体，与工作流请假流程关联；流程 BusinessType=Leave、BusinessKey=本表 Id。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 请假实体（考勤请假）。FlowInstanceId 存流程实例 Id，由业务方在发起流程后写入；流程引擎不识别本表，BusinessKey/BusinessType 与“请假”的对应关系由调用方（请假业务模块）约定并实现。
/// </summary>
[SugarTable("takt_humanresource_leave", "请假信息表")]
[SugarIndex("ix_takt_humanresource_leave_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_leave_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_leave_leave_status", nameof(LeaveStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_leave_start_date", nameof(StartDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_leave_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_leave_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktLeave : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（与请假表单 leaveType 一致：affair=事假, sick=病假, annual=年假 等）
    /// </summary>
    [SugarColumn(ColumnName = "leave_type", ColumnDescription = "请假类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string LeaveType { get; set; } = string.Empty;

    /// <summary>
    /// 开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "结束日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 请假事由
    /// </summary>
    [SugarColumn(ColumnName = "reason", ColumnDescription = "请假事由", ColumnDataType = "nvarchar", Length = 500, IsNullable = false, DefaultValue = "")]
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// 证明附件 JSON。上传证明文件（如住院 PDF）后，将文件服务返回的信息按条存入数组。
    /// 单条格式（与 TaktFile 对齐，用于展示与下载）：FileId、FileCode、FileName、FileOriginalName（上传时文件名，如「住院证明.pdf」）、FilePath、FileSize、FileType（如 application/pdf）、FileExtension（如 .pdf）、AccessUrl（可选，下载/预览地址）；业务扩展：AttachmentType（0=病假证明 1=事假证明 2=其他）、AttachmentDescription（如「住院证明」）、SortOrder。
    /// 示例（一条住院 PDF）：[{ "FileId": 123, "FileCode": "xxx", "FileName": "xxx.pdf", "FileOriginalName": "住院证明.pdf", "FilePath": "/uploads/2025/xx/xxx.pdf", "FileSize": 102400, "FileType": "application/pdf", "FileExtension": ".pdf", "AccessUrl": "/api/files/xxx/download", "AttachmentType": 0, "AttachmentDescription": "住院证明", "SortOrder": 0 }]
    /// </summary>
    [SugarColumn(ColumnName = "proof_attachments_json", ColumnDescription = "证明附件JSON", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ProofAttachmentsJson { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 请假状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    [SugarColumn(ColumnName = "leave_status", ColumnDescription = "请假状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int LeaveStatus { get; set; } = 1;
}
