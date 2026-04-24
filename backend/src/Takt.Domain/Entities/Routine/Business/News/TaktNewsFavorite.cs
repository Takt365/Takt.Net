// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.News
// 文件名称：TaktNewsFavorite.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt新闻收藏记录实体，记录用户收藏新闻的情况
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.News;

/// <summary>
/// Takt新闻收藏记录实体
/// </summary>
[SugarTable("takt_routine_news_favorite", "新闻收藏记录表")]
[SugarIndex("ix_takt_routine_news_favorite_news_id_user_id", nameof(NewsId), OrderByType.Asc, nameof(UserId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_news_favorite_news_id", nameof(NewsId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_favorite_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_favorite_favorite_time", nameof(FavoriteTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_news_favorite_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_favorite_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktNewsFavorite : TaktEntityBase
{
    /// <summary>
    /// 新闻ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "news_id", ColumnDescription = "新闻ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long NewsId { get; set; }

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
    /// 收藏时间
    /// </summary>
    [SugarColumn(ColumnName = "favorite_time", ColumnDescription = "收藏时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime FavoriteTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 收藏标签（用户自定义标签，多个标签用逗号分隔）
    /// </summary>
    [SugarColumn(ColumnName = "favorite_tags", ColumnDescription = "收藏标签", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? FavoriteTags { get; set; }
}
