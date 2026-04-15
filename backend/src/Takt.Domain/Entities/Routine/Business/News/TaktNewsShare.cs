// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.News
// 文件名称：TaktNewsShare.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt新闻分享记录实体，记录用户分享新闻的情况
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.News;

/// <summary>
/// Takt新闻分享记录实体
/// </summary>
[SugarTable("takt_routine_news_share", "新闻分享记录表")]
[SugarIndex("ix_takt_routine_news_share_news_id", nameof(NewsId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_share_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_share_share_time", nameof(ShareTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_news_share_share_platform", nameof(SharePlatform), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_share_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_share_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktNewsShare : TaktEntityBase
{
    /// <summary>
    /// 新闻ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "news_id", ColumnDescription = "新闻ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long NewsId { get; set; }

    /// <summary>
    /// 分享人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "分享人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 分享人姓名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "分享人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 分享平台（0=内部分享，1=微信，2=微博，3=QQ，4=链接复制，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "share_platform", ColumnDescription = "分享平台", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SharePlatform { get; set; } = 0;

    /// <summary>
    /// 分享时间
    /// </summary>
    [SugarColumn(ColumnName = "share_time", ColumnDescription = "分享时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ShareTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 分享备注
    /// </summary>
    [SugarColumn(ColumnName = "share_remark", ColumnDescription = "分享备注", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ShareRemark { get; set; }
}
