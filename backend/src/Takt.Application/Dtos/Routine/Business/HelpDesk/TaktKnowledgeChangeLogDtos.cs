// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeChangeLogDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：知识库变更日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// 知识库变更日志表Dto
/// </summary>
public partial class TaktKnowledgeChangeLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 知识库变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeChangeLogId { get; set; }

    /// <summary>
    /// 知识ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }
    /// <summary>
    /// 知识标题
    /// </summary>
    public string? KnowledgeTitle { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }
    /// <summary>
    /// 修改内容摘要
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
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
    /// <summary>
    /// 变更时版本号
    /// </summary>
    public int? VersionAtChange { get; set; }
}

/// <summary>
/// 知识库变更日志表查询DTO
/// </summary>
public partial class TaktKnowledgeChangeLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeChangeLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 知识库变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeChangeLogId { get; set; }

    /// <summary>
    /// 知识ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }
    /// <summary>
    /// 知识标题
    /// </summary>
    public string? KnowledgeTitle { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int? ChangeType { get; set; }
    /// <summary>
    /// 修改内容摘要
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
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
    /// <summary>
    /// 变更时版本号
    /// </summary>
    public int? VersionAtChange { get; set; }

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
/// Takt创建知识库变更日志表DTO
/// </summary>
public partial class TaktKnowledgeChangeLogCreateDto
{
        /// <summary>
    /// 知识ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeId { get; set; }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string? KnowledgeTitle { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改内容摘要
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
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更时版本号
    /// </summary>
    public int? VersionAtChange { get; set; }

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
/// Takt更新知识库变更日志表DTO
/// </summary>
public partial class TaktKnowledgeChangeLogUpdateDto : TaktKnowledgeChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeChangeLogUpdateDto()
    {
    }

        /// <summary>
    /// 知识库变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KnowledgeChangeLogId { get; set; }
}

/// <summary>
/// 知识库变更日志表导入模板DTO
/// </summary>
public partial class TaktKnowledgeChangeLogTemplateDto
{
        /// <summary>
    /// 知识ID
    /// </summary>
    public long KnowledgeId { get; set; }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string? KnowledgeTitle { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改内容摘要
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
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更时版本号
    /// </summary>
    public int? VersionAtChange { get; set; }

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
/// 知识库变更日志表导入DTO
/// </summary>
public partial class TaktKnowledgeChangeLogImportDto
{
        /// <summary>
    /// 知识ID
    /// </summary>
    public long KnowledgeId { get; set; }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string? KnowledgeTitle { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改内容摘要
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
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更时版本号
    /// </summary>
    public int? VersionAtChange { get; set; }

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
/// 知识库变更日志表导出DTO
/// </summary>
public partial class TaktKnowledgeChangeLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeChangeLogExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 知识ID
    /// </summary>
    public long KnowledgeId { get; set; }

        /// <summary>
    /// 知识标题
    /// </summary>
    public string? KnowledgeTitle { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改内容摘要
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
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更时版本号
    /// </summary>
    public int? VersionAtChange { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}