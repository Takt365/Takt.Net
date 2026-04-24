// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.News
// 文件名称：TaktNewsCommentDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：新闻评论表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.News;

/// <summary>
/// 新闻评论表Dto
/// </summary>
public partial class TaktNewsCommentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentDto()
    {
        UserName = string.Empty;
        CommentContent = string.Empty;
    }

    /// <summary>
    /// 新闻评论表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsCommentId { get; set; }

    /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }
    /// <summary>
    /// 父评论ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }
    /// <summary>
    /// 评论人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }
    /// <summary>
    /// 评论人姓名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 评论人头像URL
    /// </summary>
    public string? UserAvatar { get; set; }
    /// <summary>
    /// 被回复人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ReplyToUserId { get; set; }
    /// <summary>
    /// 被回复人姓名
    /// </summary>
    public string? ReplyToUserName { get; set; }
    /// <summary>
    /// 评论内容
    /// </summary>
    public string CommentContent { get; set; }
    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime CommentTime { get; set; }
    /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }
    /// <summary>
    /// 回复次数
    /// </summary>
    public int ReplyCount { get; set; }
    /// <summary>
    /// 评论层级
    /// </summary>
    public int CommentLevel { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 审核状态
    /// </summary>
    public string? ApprovalStatus { get; set; }
    /// <summary>
    /// 评论状态
    /// </summary>
    public int CommentStatus { get; set; }
}

/// <summary>
/// 新闻评论表树形DTO
/// </summary>
public partial class TaktNewsCommentTreeDto : TaktNewsCommentDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentTreeDto()
    {
        Children = new List<TaktNewsCommentTreeDto>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktNewsCommentTreeDto> Children { get; set; }
}

/// <summary>
/// 新闻评论表查询DTO
/// </summary>
public partial class TaktNewsCommentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 新闻评论表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsCommentId { get; set; }

    /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? NewsId { get; set; }
    /// <summary>
    /// 父评论ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }
    /// <summary>
    /// 评论人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }
    /// <summary>
    /// 评论人姓名
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// 评论人头像URL
    /// </summary>
    public string? UserAvatar { get; set; }
    /// <summary>
    /// 被回复人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ReplyToUserId { get; set; }
    /// <summary>
    /// 被回复人姓名
    /// </summary>
    public string? ReplyToUserName { get; set; }
    /// <summary>
    /// 评论内容
    /// </summary>
    public string? CommentContent { get; set; }
    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime? CommentTime { get; set; }

    /// <summary>
    /// 评论时间开始时间
    /// </summary>
    public DateTime? CommentTimeStart { get; set; }
    /// <summary>
    /// 评论时间结束时间
    /// </summary>
    public DateTime? CommentTimeEnd { get; set; }
    /// <summary>
    /// 点赞次数
    /// </summary>
    public int? LikeCount { get; set; }
    /// <summary>
    /// 回复次数
    /// </summary>
    public int? ReplyCount { get; set; }
    /// <summary>
    /// 评论层级
    /// </summary>
    public int? CommentLevel { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 审核状态
    /// </summary>
    public string? ApprovalStatus { get; set; }
    /// <summary>
    /// 评论状态
    /// </summary>
    public int? CommentStatus { get; set; }

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
/// Takt创建新闻评论表DTO
/// </summary>
public partial class TaktNewsCommentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentCreateDto()
    {
        UserName = string.Empty;
        CommentContent = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }

        /// <summary>
    /// 父评论ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

        /// <summary>
    /// 评论人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

        /// <summary>
    /// 评论人姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 评论人头像URL
    /// </summary>
    public string? UserAvatar { get; set; }

        /// <summary>
    /// 被回复人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ReplyToUserId { get; set; }

        /// <summary>
    /// 被回复人姓名
    /// </summary>
    public string? ReplyToUserName { get; set; }

        /// <summary>
    /// 评论内容
    /// </summary>
    public string CommentContent { get; set; }

        /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime CommentTime { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 回复次数
    /// </summary>
    public int ReplyCount { get; set; }

        /// <summary>
    /// 评论层级
    /// </summary>
    public int CommentLevel { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public string? ApprovalStatus { get; set; }

        /// <summary>
    /// 评论状态
    /// </summary>
    public int CommentStatus { get; set; }

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
/// Takt更新新闻评论表DTO
/// </summary>
public partial class TaktNewsCommentUpdateDto : TaktNewsCommentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentUpdateDto()
    {
    }

        /// <summary>
    /// 新闻评论表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsCommentId { get; set; }
}

/// <summary>
/// 新闻评论表审核状态DTO
/// </summary>
public partial class TaktNewsCommentApprovalStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentApprovalStatusDto()
    {
    }

        /// <summary>
    /// 新闻评论表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsCommentId { get; set; }

    /// <summary>
    /// 审核状态（0=禁用，1=启用）
    /// </summary>
    public int ApprovalStatus { get; set; }
}

/// <summary>
/// 新闻评论表评论状态DTO
/// </summary>
public partial class TaktNewsCommentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentStatusDto()
    {
    }

        /// <summary>
    /// 新闻评论表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsCommentId { get; set; }

    /// <summary>
    /// 评论状态（0=禁用，1=启用）
    /// </summary>
    public int CommentStatus { get; set; }
}

/// <summary>
/// 新闻评论表导入模板DTO
/// </summary>
public partial class TaktNewsCommentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentTemplateDto()
    {
        UserName = string.Empty;
        CommentContent = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

        /// <summary>
    /// 父评论ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 评论人ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 评论人姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 评论人头像URL
    /// </summary>
    public string? UserAvatar { get; set; }

        /// <summary>
    /// 被回复人ID
    /// </summary>
    public long? ReplyToUserId { get; set; }

        /// <summary>
    /// 被回复人姓名
    /// </summary>
    public string? ReplyToUserName { get; set; }

        /// <summary>
    /// 评论内容
    /// </summary>
    public string CommentContent { get; set; }

        /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime CommentTime { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 回复次数
    /// </summary>
    public int ReplyCount { get; set; }

        /// <summary>
    /// 评论层级
    /// </summary>
    public int CommentLevel { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public string? ApprovalStatus { get; set; }

        /// <summary>
    /// 评论状态
    /// </summary>
    public int CommentStatus { get; set; }

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
/// 新闻评论表导入DTO
/// </summary>
public partial class TaktNewsCommentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentImportDto()
    {
        UserName = string.Empty;
        CommentContent = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

        /// <summary>
    /// 父评论ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 评论人ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 评论人姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 评论人头像URL
    /// </summary>
    public string? UserAvatar { get; set; }

        /// <summary>
    /// 被回复人ID
    /// </summary>
    public long? ReplyToUserId { get; set; }

        /// <summary>
    /// 被回复人姓名
    /// </summary>
    public string? ReplyToUserName { get; set; }

        /// <summary>
    /// 评论内容
    /// </summary>
    public string CommentContent { get; set; }

        /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime CommentTime { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 回复次数
    /// </summary>
    public int ReplyCount { get; set; }

        /// <summary>
    /// 评论层级
    /// </summary>
    public int CommentLevel { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public string? ApprovalStatus { get; set; }

        /// <summary>
    /// 评论状态
    /// </summary>
    public int CommentStatus { get; set; }

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
/// 新闻评论表导出DTO
/// </summary>
public partial class TaktNewsCommentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentExportDto()
    {
        CreatedAt = DateTime.Now;
        UserName = string.Empty;
        CommentContent = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

        /// <summary>
    /// 父评论ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 评论人ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 评论人姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 评论人头像URL
    /// </summary>
    public string? UserAvatar { get; set; }

        /// <summary>
    /// 被回复人ID
    /// </summary>
    public long? ReplyToUserId { get; set; }

        /// <summary>
    /// 被回复人姓名
    /// </summary>
    public string? ReplyToUserName { get; set; }

        /// <summary>
    /// 评论内容
    /// </summary>
    public string CommentContent { get; set; }

        /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime CommentTime { get; set; }

        /// <summary>
    /// 点赞次数
    /// </summary>
    public int LikeCount { get; set; }

        /// <summary>
    /// 回复次数
    /// </summary>
    public int ReplyCount { get; set; }

        /// <summary>
    /// 评论层级
    /// </summary>
    public int CommentLevel { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public string? ApprovalStatus { get; set; }

        /// <summary>
    /// 评论状态
    /// </summary>
    public int CommentStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}