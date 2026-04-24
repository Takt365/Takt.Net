// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.News
// 文件名称：TaktNewsComment.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt新闻评论实体，支持多级回复
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.News;

/// <summary>
/// Takt新闻评论实体
/// </summary>
[SugarTable("takt_routine_news_comment", "新闻评论表")]
[SugarIndex("ix_takt_routine_news_comment_news_id", nameof(NewsId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_comment_time", nameof(CommentTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_news_comment_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_approval_status", nameof(ApprovalStatus), OrderByType.Asc)]
public class TaktNewsComment : TaktEntityBase
{
    /// <summary>
    /// 新闻ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "news_id", ColumnDescription = "新闻ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long NewsId { get; set; }

    /// <summary>
    /// 父评论ID（0表示顶级评论，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父评论ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 评论人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "评论人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 评论人姓名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "评论人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 评论人头像URL
    /// </summary>
    [SugarColumn(ColumnName = "user_avatar", ColumnDescription = "评论人头像URL", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? UserAvatar { get; set; }

    /// <summary>
    /// 被回复人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "reply_to_user_id", ColumnDescription = "被回复人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ReplyToUserId { get; set; }

    /// <summary>
    /// 被回复人姓名
    /// </summary>
    [SugarColumn(ColumnName = "reply_to_user_name", ColumnDescription = "被回复人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ReplyToUserName { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [SugarColumn(ColumnName = "comment_content", ColumnDescription = "评论内容", ColumnDataType = "nvarchar", Length = 2000, IsNullable = false)]
    public string CommentContent { get; set; } = string.Empty;

    /// <summary>
    /// 评论时间
    /// </summary>
    [SugarColumn(ColumnName = "comment_time", ColumnDescription = "评论时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime CommentTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 点赞次数
    /// </summary>
    [SugarColumn(ColumnName = "like_count", ColumnDescription = "点赞次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LikeCount { get; set; } = 0;

    /// <summary>
    /// 回复次数（子评论数量）
    /// </summary>
    [SugarColumn(ColumnName = "reply_count", ColumnDescription = "回复次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReplyCount { get; set; } = 0;

    /// <summary>
    /// 评论层级（0=顶级评论，1=一级回复，2=二级回复，最多支持3级）
    /// </summary>
    [SugarColumn(ColumnName = "comment_level", ColumnDescription = "评论层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CommentLevel { get; set; } = 0;

    /// <summary>
    /// 流程实例ID（关联工作流，评论审核流程）
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 审核状态（如：待审、已通过、已驳回）
    /// </summary>
    [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审核状态", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? ApprovalStatus { get; set; }

    /// <summary>
    /// 评论状态（0=正常，1=已隐藏；与审核通过后展示与否相关）
    /// </summary>
    [SugarColumn(ColumnName = "comment_status", ColumnDescription = "评论状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CommentStatus { get; set; } = 0;
}
