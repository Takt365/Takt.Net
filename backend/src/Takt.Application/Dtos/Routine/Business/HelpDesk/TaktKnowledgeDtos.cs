// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：知识库表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// 知识库表Dto
/// </summary>
public partial class TaktKnowledgeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeDto()
    {
        Title = string.Empty;
    }

    /// <summary>
    /// 知识库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }

    /// <summary>
    /// 知识标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 知识内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 知识摘要
    /// </summary>
    public string? Summary { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get; set; }
    /// <summary>
    /// 知识状态
    /// </summary>
    public int KnowledgeStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewCount { get; set; }
    /// <summary>
    /// 有用评价数
    /// </summary>
    public int HelpfulCount { get; set; }
    /// <summary>
    /// 无帮助评价数
    /// </summary>
    public int UnhelpfulCount { get; set; }
    /// <summary>
    /// 是否已发布
    /// </summary>
    public int IsPublished { get; set; }
    /// <summary>
    /// 版本号
    /// </summary>
    public int Version { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishedAt { get; set; }
    /// <summary>
    /// 最后修订时间
    /// </summary>
    public DateTime? RevisedAt { get; set; }
}

/// <summary>
/// 知识库表查询DTO
/// </summary>
public partial class TaktKnowledgeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 知识库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }

    /// <summary>
    /// 知识标题
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// 知识内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 知识摘要
    /// </summary>
    public string? Summary { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get; set; }
    /// <summary>
    /// 知识状态
    /// </summary>
    public int? KnowledgeStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 浏览次数
    /// </summary>
    public int? ViewCount { get; set; }
    /// <summary>
    /// 有用评价数
    /// </summary>
    public int? HelpfulCount { get; set; }
    /// <summary>
    /// 无帮助评价数
    /// </summary>
    public int? UnhelpfulCount { get; set; }
    /// <summary>
    /// 是否已发布
    /// </summary>
    public int? IsPublished { get; set; }
    /// <summary>
    /// 版本号
    /// </summary>
    public int? Version { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// 发布时间开始时间
    /// </summary>
    public DateTime? PublishedAtStart { get; set; }
    /// <summary>
    /// 发布时间结束时间
    /// </summary>
    public DateTime? PublishedAtEnd { get; set; }
    /// <summary>
    /// 最后修订时间
    /// </summary>
    public DateTime? RevisedAt { get; set; }

    /// <summary>
    /// 最后修订时间开始时间
    /// </summary>
    public DateTime? RevisedAtStart { get; set; }
    /// <summary>
    /// 最后修订时间结束时间
    /// </summary>
    public DateTime? RevisedAtEnd { get; set; }

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
/// Takt创建知识库表DTO
/// </summary>
public partial class TaktKnowledgeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeCreateDto()
    {
        Title = string.Empty;
    }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 知识内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 知识摘要
    /// </summary>
    public string? Summary { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get; set; }

        /// <summary>
    /// 知识状态
    /// </summary>
    public int KnowledgeStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewCount { get; set; }

        /// <summary>
    /// 有用评价数
    /// </summary>
    public int HelpfulCount { get; set; }

        /// <summary>
    /// 无帮助评价数
    /// </summary>
    public int UnhelpfulCount { get; set; }

        /// <summary>
    /// 是否已发布
    /// </summary>
    public int IsPublished { get; set; }

        /// <summary>
    /// 版本号
    /// </summary>
    public int Version { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishedAt { get; set; }

        /// <summary>
    /// 最后修订时间
    /// </summary>
    public DateTime? RevisedAt { get; set; }

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
/// Takt更新知识库表DTO
/// </summary>
public partial class TaktKnowledgeUpdateDto : TaktKnowledgeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeUpdateDto()
    {
    }

        /// <summary>
    /// 知识库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }
}

/// <summary>
/// 知识库表知识状态DTO
/// </summary>
public partial class TaktKnowledgeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeStatusDto()
    {
    }

        /// <summary>
    /// 知识库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }

    /// <summary>
    /// 知识状态（0=禁用，1=启用）
    /// </summary>
    public int KnowledgeStatus { get; set; }
}

/// <summary>
/// 知识库表导入模板DTO
/// </summary>
public partial class TaktKnowledgeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeTemplateDto()
    {
        Title = string.Empty;
    }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 知识内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 知识摘要
    /// </summary>
    public string? Summary { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get; set; }

        /// <summary>
    /// 知识状态
    /// </summary>
    public int KnowledgeStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewCount { get; set; }

        /// <summary>
    /// 有用评价数
    /// </summary>
    public int HelpfulCount { get; set; }

        /// <summary>
    /// 无帮助评价数
    /// </summary>
    public int UnhelpfulCount { get; set; }

        /// <summary>
    /// 是否已发布
    /// </summary>
    public int IsPublished { get; set; }

        /// <summary>
    /// 版本号
    /// </summary>
    public int Version { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishedAt { get; set; }

        /// <summary>
    /// 最后修订时间
    /// </summary>
    public DateTime? RevisedAt { get; set; }

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
/// 知识库表导入DTO
/// </summary>
public partial class TaktKnowledgeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeImportDto()
    {
        Title = string.Empty;
    }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 知识内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 知识摘要
    /// </summary>
    public string? Summary { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get; set; }

        /// <summary>
    /// 知识状态
    /// </summary>
    public int KnowledgeStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewCount { get; set; }

        /// <summary>
    /// 有用评价数
    /// </summary>
    public int HelpfulCount { get; set; }

        /// <summary>
    /// 无帮助评价数
    /// </summary>
    public int UnhelpfulCount { get; set; }

        /// <summary>
    /// 是否已发布
    /// </summary>
    public int IsPublished { get; set; }

        /// <summary>
    /// 版本号
    /// </summary>
    public int Version { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishedAt { get; set; }

        /// <summary>
    /// 最后修订时间
    /// </summary>
    public DateTime? RevisedAt { get; set; }

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
/// 知识库表导出DTO
/// </summary>
public partial class TaktKnowledgeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeExportDto()
    {
        CreatedAt = DateTime.Now;
        Title = string.Empty;
    }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 知识内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 知识摘要
    /// </summary>
    public string? Summary { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get; set; }

        /// <summary>
    /// 知识状态
    /// </summary>
    public int KnowledgeStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewCount { get; set; }

        /// <summary>
    /// 有用评价数
    /// </summary>
    public int HelpfulCount { get; set; }

        /// <summary>
    /// 无帮助评价数
    /// </summary>
    public int UnhelpfulCount { get; set; }

        /// <summary>
    /// 是否已发布
    /// </summary>
    public int IsPublished { get; set; }

        /// <summary>
    /// 版本号
    /// </summary>
    public int Version { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishedAt { get; set; }

        /// <summary>
    /// 最后修订时间
    /// </summary>
    public DateTime? RevisedAt { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}