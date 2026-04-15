// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktKnowledgeChangeLogDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt知识库变更日志DTO，用于查询与展示修改历史
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================


// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktKnowledgeChangeLogDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt知识库变更日志DTO，用于查询与展示修改历史
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// Takt知识库变更日志DTO
/// </summary>
public class TaktKnowledgeChangeLogDto : TaktDtoBase
{
    /// <summary>
    /// 日志ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ChangeLogId { get; set; }

    /// <summary>
    /// 知识ID（关联知识库表，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }

    /// <summary>
    /// 知识标题（冗余展示）
    /// </summary>
    public string? KnowledgeTitle { get; set; }

    /// <summary>
    /// 变更类型（0=创建，1=更新，2=删除）
    /// </summary>
    public int ChangeType { get; set; }

    /// <summary>
    /// 修改内容摘要（简短描述本次变更，便于列表展示）
    /// </summary>
    public string? ChangeSummary { get; set; }

    /// <summary>
    /// 变更字段名
    /// </summary>
    public string? ChangeField { get; set; }

    /// <summary>
    /// 原值
    /// </summary>
    public string? OldValue { get; set; }

    /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }

    /// <summary>
    /// 变更原因或备注（变更时间、变更人由基类 CreatedAt/CreatedById/CreatedBy 表示）
    /// </summary>
    public string? ChangeReason { get; set; }

    /// <summary>
    /// 变更时知识版本号
    /// </summary>
    public int? VersionAtChange { get; set; }
}

/// <summary>
/// Takt知识库变更日志查询DTO
/// </summary>
public class TaktKnowledgeChangeLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 知识ID（按知识条目查其全部变更历史）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }

    /// <summary>
    /// 变更类型（0=创建，1=更新，2=删除）
    /// </summary>
    public int? ChangeType { get; set; }

    /// <summary>
    /// 变更人ID（按创建人筛选，即基类 CreatedById）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }

    /// <summary>
    /// 变更时间起（按创建时间筛选，即基类 CreatedAt）
    /// </summary>
    public DateTime? CreatedAtFrom { get; set; }

    /// <summary>
    /// 变更时间止
    /// </summary>
    public DateTime? CreatedAtTo { get; set; }
}
