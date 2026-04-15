// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktKnowledgeDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt知识库DTO，包含知识库相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================


// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktKnowledgeDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt知识库DTO，包含知识库相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// Takt知识库DTO
/// </summary>
public class TaktKnowledgeDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeDto()
    {
        Title = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 知识ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }

    /// <summary>
    /// 知识标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 知识内容（富文本/HTML）
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 知识摘要（简短描述，列表/搜索展示）
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    /// 标签（逗号分隔或JSON数组存储）
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// 知识状态（0=草稿，1=已发布，2=已下架）
    /// </summary>
    public int KnowledgeStatus { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// 有用评价数（用户点“有帮助”次数）
    /// </summary>
    public int HelpfulCount { get; set; }

    /// <summary>
    /// 无帮助评价数（用户点“无帮助”次数）
    /// </summary>
    public int UnhelpfulCount { get; set; }
}

/// <summary>
/// Takt知识库查询DTO
/// </summary>
public class TaktKnowledgeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 知识标题（模糊）
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    /// 知识状态（0=草稿，1=已发布，2=已下架）
    /// </summary>
    public int? KnowledgeStatus { get; set; }

    /// <summary>
    /// 标签（模糊匹配）
    /// </summary>
    public string? Tags { get; set; }
}

/// <summary>
/// Takt创建知识库DTO
/// </summary>
public class TaktKnowledgeCreateDto
{
    /// <summary>
    /// 知识标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 知识内容（富文本/HTML）
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 知识摘要（简短描述）
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
    /// 知识状态（0=草稿，1=已发布，2=已下架）
    /// </summary>
    public int KnowledgeStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新知识库DTO
/// </summary>
public class TaktKnowledgeUpdateDto : TaktKnowledgeCreateDto
{
    /// <summary>
    /// 知识ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }
}
