// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.News
// 文件名称：TaktNews.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt新闻实体，定义新闻领域模型（包含社交属性）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.News;

/// <summary>
/// Takt新闻实体
/// </summary>
[SugarTable("takt_routine_news", "新闻表")]
[SugarIndex("ix_takt_routine_news_news_code", nameof(NewsCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_news_news_category", nameof(NewsCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_publish_time", nameof(PublishTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_news_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_news_status", nameof(NewsStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_news_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
public class TaktNews : TaktEntityBase
{
    /// <summary>
    /// 新闻编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "news_code", ColumnDescription = "新闻编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string NewsCode { get; set; } = string.Empty;

    /// <summary>
    /// 新闻分类（0=公司新闻，1=行业动态，2=技术分享，3=产品发布，4=活动资讯，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "news_category", ColumnDescription = "新闻分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int NewsCategory { get; set; } = 0;

    /// <summary>
    /// 新闻标题
    /// </summary>
    [SugarColumn(ColumnName = "news_title", ColumnDescription = "新闻标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string NewsTitle { get; set; } = string.Empty;

    /// <summary>
    /// 新闻摘要
    /// </summary>
    [SugarColumn(ColumnName = "news_summary", ColumnDescription = "新闻摘要", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? NewsSummary { get; set; }

    /// <summary>
    /// 新闻内容
    /// </summary>
    [SugarColumn(ColumnName = "news_content", ColumnDescription = "新闻内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = false)]
    public string NewsContent { get; set; } = string.Empty;

    /// <summary>
    /// 新闻封面图片URL
    /// </summary>
    [SugarColumn(ColumnName = "news_cover_image", ColumnDescription = "新闻封面图片URL", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? NewsCoverImage { get; set; }

    /// <summary>
    /// 是否置顶（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_top", ColumnDescription = "是否置顶", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsTop { get; set; } = 0;

    /// <summary>
    /// 是否推荐（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_recommended", ColumnDescription = "是否推荐", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsRecommended { get; set; } = 0;

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
    /// 阅读次数
    /// </summary>
    [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReadCount { get; set; } = 0;

    /// <summary>
    /// 点赞次数
    /// </summary>
    [SugarColumn(ColumnName = "like_count", ColumnDescription = "点赞次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LikeCount { get; set; } = 0;

    /// <summary>
    /// 评论次数
    /// </summary>
    [SugarColumn(ColumnName = "comment_count", ColumnDescription = "评论次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CommentCount { get; set; } = 0;

    /// <summary>
    /// 收藏次数
    /// </summary>
    [SugarColumn(ColumnName = "favorite_count", ColumnDescription = "收藏次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FavoriteCount { get; set; } = 0;

    /// <summary>
    /// 分享次数
    /// </summary>
    [SugarColumn(ColumnName = "share_count", ColumnDescription = "分享次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ShareCount { get; set; } = 0;

    /// <summary>
    /// 附件数量
    /// </summary>
    [SugarColumn(ColumnName = "attachment_count", ColumnDescription = "附件数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AttachmentCount { get; set; } = 0;

    /// <summary>
    /// 流程实例ID（关联工作流，如发布审批流程；流程侧 BusinessType=News、BusinessKey=本表 Id）
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
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 新闻状态（0=草稿，1=已发布，2=已撤回，3=已过期）
    /// </summary>
    [SugarColumn(ColumnName = "news_status", ColumnDescription = "新闻状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int NewsStatus { get; set; } = 0;


}
