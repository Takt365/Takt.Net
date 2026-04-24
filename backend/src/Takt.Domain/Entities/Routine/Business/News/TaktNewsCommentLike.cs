// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.News
// 文件名称：TaktNewsCommentLike.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt新闻评论点赞记录实体，记录用户点赞评论的情况
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.News;

/// <summary>
/// Takt新闻评论点赞记录实体
/// </summary>
[SugarTable("takt_routine_news_comment_like", "新闻评论点赞记录表")]
[SugarIndex("ix_takt_routine_news_comment_like_comment_id_user_id", nameof(CommentId), OrderByType.Asc, nameof(UserId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_news_comment_like_comment_id", nameof(CommentId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_like_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_like_like_time", nameof(LikeTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_news_comment_like_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_comment_like_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktNewsCommentLike : TaktEntityBase
{
    /// <summary>
    /// 评论ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "comment_id", ColumnDescription = "评论ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CommentId { get; set; }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户姓名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 点赞时间
    /// </summary>
    [SugarColumn(ColumnName = "like_time", ColumnDescription = "点赞时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime LikeTime { get; set; } = DateTime.Now;
}
