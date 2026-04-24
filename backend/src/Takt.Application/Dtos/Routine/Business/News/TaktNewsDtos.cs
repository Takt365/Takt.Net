// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.News
// 文件名称：TaktNewsDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：新闻表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.News;

/// <summary>
/// 新闻表Dto
/// </summary>
public partial class TaktNewsDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsDto()
    {
        NewsCode = string.Empty;
        NewsTitle = string.Empty;
        NewsContent = string.Empty;
        PublisherName = string.Empty;
    }

    /// <summary>
    /// 新闻表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }

    /// <summary>
    /// 新闻编码
    /// </summary>
    public string NewsCode { get; set; }
    /// <summary>
    /// 新闻分类
    /// </summary>
    public int NewsCategory { get; set; }
    /// <summary>
    /// 新闻标题
    /// </summary>
    public string NewsTitle { get; set; }
    /// <summary>
    /// 新闻摘要
    /// </summary>
    public string? NewsSummary { get; set; }
    /// <summary>
    /// 新闻内容
    /// </summary>
    public string NewsContent { get; set; }
    /// <summary>
    /// 新闻封面图片URL
    /// </summary>
    public string? NewsCoverImage { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }
    /// <summary>
    /// 是否推荐
    /// </summary>
    public int IsRecommended { get; set; }
    /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }
    /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }
    /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }
    /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }
    /// <summary>
    /// 评论次数
    /// </summary>
    public int CommentCount { get; set; }
    /// <summary>
    /// 收藏次数
    /// </summary>
    public int FavoriteCount { get; set; }
    /// <summary>
    /// 分享次数
    /// </summary>
    public int ShareCount { get; set; }
    /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 发布部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PublisherId { get; set; }
    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 新闻状态
    /// </summary>
    public int NewsStatus { get; set; }
}

/// <summary>
/// 新闻表查询DTO
/// </summary>
public partial class TaktNewsQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 新闻表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }

    /// <summary>
    /// 新闻编码
    /// </summary>
    public string? NewsCode { get; set; }
    /// <summary>
    /// 新闻分类
    /// </summary>
    public int? NewsCategory { get; set; }
    /// <summary>
    /// 新闻标题
    /// </summary>
    public string? NewsTitle { get; set; }
    /// <summary>
    /// 新闻摘要
    /// </summary>
    public string? NewsSummary { get; set; }
    /// <summary>
    /// 新闻内容
    /// </summary>
    public string? NewsContent { get; set; }
    /// <summary>
    /// 新闻封面图片URL
    /// </summary>
    public string? NewsCoverImage { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int? IsTop { get; set; }
    /// <summary>
    /// 是否推荐
    /// </summary>
    public int? IsRecommended { get; set; }
    /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

    /// <summary>
    /// 生效时间开始时间
    /// </summary>
    public DateTime? EffectiveTimeStart { get; set; }
    /// <summary>
    /// 生效时间结束时间
    /// </summary>
    public DateTime? EffectiveTimeEnd { get; set; }
    /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 失效时间开始时间
    /// </summary>
    public DateTime? ExpireTimeStart { get; set; }
    /// <summary>
    /// 失效时间结束时间
    /// </summary>
    public DateTime? ExpireTimeEnd { get; set; }
    /// <summary>
    /// 阅读次数
    /// </summary>
    public int? ReadCount { get; set; }
    /// <summary>
    /// 点赞次数
    /// </summary>
    public int? LikeCount { get; set; }
    /// <summary>
    /// 评论次数
    /// </summary>
    public int? CommentCount { get; set; }
    /// <summary>
    /// 收藏次数
    /// </summary>
    public int? FavoriteCount { get; set; }
    /// <summary>
    /// 分享次数
    /// </summary>
    public int? ShareCount { get; set; }
    /// <summary>
    /// 附件数量
    /// </summary>
    public int? AttachmentCount { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 发布部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PublisherId { get; set; }
    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublisherName { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 发布时间开始时间
    /// </summary>
    public DateTime? PublishTimeStart { get; set; }
    /// <summary>
    /// 发布时间结束时间
    /// </summary>
    public DateTime? PublishTimeEnd { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 新闻状态
    /// </summary>
    public int? NewsStatus { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建新闻表DTO
/// </summary>
public partial class TaktNewsCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCreateDto()
    {
        NewsCode = string.Empty;
        NewsTitle = string.Empty;
        NewsContent = string.Empty;
        PublisherName = string.Empty;
    }

        /// <summary>
    /// 新闻编码
    /// </summary>
    public string NewsCode { get; set; }

        /// <summary>
    /// 新闻分类
    /// </summary>
    public int NewsCategory { get; set; }

        /// <summary>
    /// 新闻标题
    /// </summary>
    public string NewsTitle { get; set; }

        /// <summary>
    /// 新闻摘要
    /// </summary>
    public string? NewsSummary { get; set; }

        /// <summary>
    /// 新闻内容
    /// </summary>
    public string NewsContent { get; set; }

        /// <summary>
    /// 新闻封面图片URL
    /// </summary>
    public string? NewsCoverImage { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否推荐
    /// </summary>
    public int IsRecommended { get; set; }

        /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

        /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 评论次数
    /// </summary>
    public int CommentCount { get; set; }

        /// <summary>
    /// 收藏次数
    /// </summary>
    public int FavoriteCount { get; set; }

        /// <summary>
    /// 分享次数
    /// </summary>
    public int ShareCount { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 新闻状态
    /// </summary>
    public int NewsStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新新闻表DTO
/// </summary>
public partial class TaktNewsUpdateDto : TaktNewsCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsUpdateDto()
    {
    }

        /// <summary>
    /// 新闻表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }
}

/// <summary>
/// 新闻表新闻状态DTO
/// </summary>
public partial class TaktNewsStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsStatusDto()
    {
    }

        /// <summary>
    /// 新闻表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }

    /// <summary>
    /// 新闻状态（0=禁用，1=启用）
    /// </summary>
    public int NewsStatus { get; set; }
}

/// <summary>
/// 新闻表导入模板DTO
/// </summary>
public partial class TaktNewsTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsTemplateDto()
    {
        NewsCode = string.Empty;
        NewsTitle = string.Empty;
        NewsContent = string.Empty;
        PublisherName = string.Empty;
    }

        /// <summary>
    /// 新闻编码
    /// </summary>
    public string NewsCode { get; set; }

        /// <summary>
    /// 新闻分类
    /// </summary>
    public int NewsCategory { get; set; }

        /// <summary>
    /// 新闻标题
    /// </summary>
    public string NewsTitle { get; set; }

        /// <summary>
    /// 新闻摘要
    /// </summary>
    public string? NewsSummary { get; set; }

        /// <summary>
    /// 新闻内容
    /// </summary>
    public string NewsContent { get; set; }

        /// <summary>
    /// 新闻封面图片URL
    /// </summary>
    public string? NewsCoverImage { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否推荐
    /// </summary>
    public int IsRecommended { get; set; }

        /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

        /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 评论次数
    /// </summary>
    public int CommentCount { get; set; }

        /// <summary>
    /// 收藏次数
    /// </summary>
    public int FavoriteCount { get; set; }

        /// <summary>
    /// 分享次数
    /// </summary>
    public int ShareCount { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 新闻状态
    /// </summary>
    public int NewsStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 新闻表导入DTO
/// </summary>
public partial class TaktNewsImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsImportDto()
    {
        NewsCode = string.Empty;
        NewsTitle = string.Empty;
        NewsContent = string.Empty;
        PublisherName = string.Empty;
    }

        /// <summary>
    /// 新闻编码
    /// </summary>
    public string NewsCode { get; set; }

        /// <summary>
    /// 新闻分类
    /// </summary>
    public int NewsCategory { get; set; }

        /// <summary>
    /// 新闻标题
    /// </summary>
    public string NewsTitle { get; set; }

        /// <summary>
    /// 新闻摘要
    /// </summary>
    public string? NewsSummary { get; set; }

        /// <summary>
    /// 新闻内容
    /// </summary>
    public string NewsContent { get; set; }

        /// <summary>
    /// 新闻封面图片URL
    /// </summary>
    public string? NewsCoverImage { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否推荐
    /// </summary>
    public int IsRecommended { get; set; }

        /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

        /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 评论次数
    /// </summary>
    public int CommentCount { get; set; }

        /// <summary>
    /// 收藏次数
    /// </summary>
    public int FavoriteCount { get; set; }

        /// <summary>
    /// 分享次数
    /// </summary>
    public int ShareCount { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 新闻状态
    /// </summary>
    public int NewsStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 新闻表导出DTO
/// </summary>
public partial class TaktNewsExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsExportDto()
    {
        CreatedAt = DateTime.Now;
        NewsCode = string.Empty;
        NewsTitle = string.Empty;
        NewsContent = string.Empty;
        PublisherName = string.Empty;
    }

        /// <summary>
    /// 新闻编码
    /// </summary>
    public string NewsCode { get; set; }

        /// <summary>
    /// 新闻分类
    /// </summary>
    public int NewsCategory { get; set; }

        /// <summary>
    /// 新闻标题
    /// </summary>
    public string NewsTitle { get; set; }

        /// <summary>
    /// 新闻摘要
    /// </summary>
    public string? NewsSummary { get; set; }

        /// <summary>
    /// 新闻内容
    /// </summary>
    public string NewsContent { get; set; }

        /// <summary>
    /// 新闻封面图片URL
    /// </summary>
    public string? NewsCoverImage { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否推荐
    /// </summary>
    public int IsRecommended { get; set; }

        /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

        /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 评论次数
    /// </summary>
    public int CommentCount { get; set; }

        /// <summary>
    /// 收藏次数
    /// </summary>
    public int FavoriteCount { get; set; }

        /// <summary>
    /// 分享次数
    /// </summary>
    public int ShareCount { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 新闻状态
    /// </summary>
    public int NewsStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}