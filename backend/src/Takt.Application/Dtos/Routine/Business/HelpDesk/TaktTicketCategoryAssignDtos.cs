// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.HelpDesk
// 文件名称：TaktTicketCategoryAssignDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：工单分类默认处理人表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// 工单分类默认处理人表Dto
/// </summary>
public partial class TaktTicketCategoryAssignDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignDto()
    {
        CategoryCode = string.Empty;
    }

    /// <summary>
    /// 工单分类默认处理人表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketCategoryAssignId { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string CategoryCode { get; set; }
    /// <summary>
    /// 默认处理人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssigneeId { get; set; }
    /// <summary>
    /// 默认处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 工单分类默认处理人表查询DTO
/// </summary>
public partial class TaktTicketCategoryAssignQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工单分类默认处理人表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketCategoryAssignId { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }
    /// <summary>
    /// 默认处理人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }
    /// <summary>
    /// 默认处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建工单分类默认处理人表DTO
/// </summary>
public partial class TaktTicketCategoryAssignCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignCreateDto()
    {
        CategoryCode = string.Empty;
    }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string CategoryCode { get; set; }

        /// <summary>
    /// 默认处理人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssigneeId { get; set; }

        /// <summary>
    /// 默认处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新工单分类默认处理人表DTO
/// </summary>
public partial class TaktTicketCategoryAssignUpdateDto : TaktTicketCategoryAssignCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignUpdateDto()
    {
    }

        /// <summary>
    /// 工单分类默认处理人表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketCategoryAssignId { get; set; }
}

/// <summary>
/// 工单分类默认处理人表导入模板DTO
/// </summary>
public partial class TaktTicketCategoryAssignTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignTemplateDto()
    {
        CategoryCode = string.Empty;
    }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string CategoryCode { get; set; }

        /// <summary>
    /// 默认处理人ID
    /// </summary>
    public long AssigneeId { get; set; }

        /// <summary>
    /// 默认处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 工单分类默认处理人表导入DTO
/// </summary>
public partial class TaktTicketCategoryAssignImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignImportDto()
    {
        CategoryCode = string.Empty;
    }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string CategoryCode { get; set; }

        /// <summary>
    /// 默认处理人ID
    /// </summary>
    public long AssigneeId { get; set; }

        /// <summary>
    /// 默认处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 工单分类默认处理人表导出DTO
/// </summary>
public partial class TaktTicketCategoryAssignExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignExportDto()
    {
        CreatedAt = DateTime.Now;
        CategoryCode = string.Empty;
    }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string CategoryCode { get; set; }

        /// <summary>
    /// 默认处理人ID
    /// </summary>
    public long AssigneeId { get; set; }

        /// <summary>
    /// 默认处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}