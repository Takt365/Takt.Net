// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Announcement
// 文件名称：TaktAnnouncement.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公告实体，定义公告领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Business.Announcement;

/// <summary>
/// Takt公告实体
/// </summary>
[SugarTable("takt_routine_announcement", "公告表")]
[SugarIndex("ix_takt_routine_announcement_announcement_code", nameof(AnnouncementCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_announcement_announcement_type", nameof(AnnouncementType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_announcement_publish_time", nameof(PublishTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_announcement_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_announcement_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_announcement_announcement_status", nameof(AnnouncementStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_announcement_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
public class TaktAnnouncement : TaktEntityBase
{
    /// <summary>
    /// 公告编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "announcement_code", ColumnDescription = "公告编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string AnnouncementCode { get; set; } = string.Empty;

    /// <summary>
    /// 公告标题
    /// </summary>
    [SugarColumn(ColumnName = "announcement_title", ColumnDescription = "公告标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string AnnouncementTitle { get; set; } = string.Empty;

    /// <summary>
    /// 公告类型（0=通知，1=公告，2=新闻，3=活动）
    /// </summary>
    [SugarColumn(ColumnName = "announcement_type", ColumnDescription = "公告类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AnnouncementType { get; set; } = 0;

    /// <summary>
    /// 公告内容
    /// </summary>
    [SugarColumn(ColumnName = "announcement_content", ColumnDescription = "公告内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = false)]
    public string AnnouncementContent { get; set; } = string.Empty;

    /// <summary>
    /// 附件列表 JSON。格式：[{ "FileId": 0, "FileName": "", "FilePath": "", "FileSize": 0, "FileType": "", "FileExtension": "", "OrderNum": 0 }]
    /// </summary>
    [SugarColumn(ColumnName = "attachments_json", ColumnDescription = "附件列表JSON", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? AttachmentsJson { get; set; }

    /// <summary>
    /// 阅读记录 JSON。格式：[{ "UserId": 0, "UserName": "", "ReadTime": "", "ReadDuration": 0 }]；阅读人数多时建议仅保留近期或汇总
    /// </summary>
    [SugarColumn(ColumnName = "read_records_json", ColumnDescription = "阅读记录JSON", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ReadRecordsJson { get; set; }

    /// <summary>
    /// 阅读次数
    /// </summary>
    [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReadCount { get; set; } = 0;

    /// <summary>
    /// 发布范围（0=全部，1=指定部门，2=指定用户，3=指定角色）
    /// </summary>
    [SugarColumn(ColumnName = "publish_scope", ColumnDescription = "发布范围", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PublishScope { get; set; } = 0;

    /// <summary>
    /// 发布范围配置（JSON格式，存储部门ID列表、用户ID列表或角色ID列表）
    /// </summary>
    [SugarColumn(ColumnName = "publish_scope_config", ColumnDescription = "发布范围配置", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? PublishScopeConfig { get; set; }

    /// <summary>
    /// 是否置顶（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_top", ColumnDescription = "是否置顶", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsTop { get; set; } = 0;

    /// <summary>
    /// 是否紧急（0=一般，1=紧急，2=非常紧急）
    /// </summary>
    [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsUrgent { get; set; } = 0;

    /// <summary>
    /// 生效时间
    /// </summary>
    [SugarColumn(ColumnName = "effective_time", ColumnDescription = "生效时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EffectiveTime { get; set; }

    /// <summary>
    /// 失效时间
    /// </summary>
    [SugarColumn(ColumnName = "expire_time", ColumnDescription = "失效时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 流程实例ID（关联工作流，如发布审批流程；流程侧 BusinessType=Announcement、BusinessKey=本表 Id）
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 发布部门ID
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "发布部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 发布部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "发布部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DeptName { get; set; }

    /// <summary>
    /// 发布人ID
    /// </summary>
    [SugarColumn(ColumnName = "publisher_id", ColumnDescription = "发布人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PublisherId { get; set; }

    /// <summary>
    /// 发布人姓名
    /// </summary>
    [SugarColumn(ColumnName = "publisher_name", ColumnDescription = "发布人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string PublisherName { get; set; } = string.Empty;

    /// <summary>
    /// 发布时间
    /// </summary>
    [SugarColumn(ColumnName = "publish_time", ColumnDescription = "发布时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期）
    /// </summary>
    [SugarColumn(ColumnName = "announcement_status", ColumnDescription = "公告状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AnnouncementStatus { get; set; } = 0;
}
