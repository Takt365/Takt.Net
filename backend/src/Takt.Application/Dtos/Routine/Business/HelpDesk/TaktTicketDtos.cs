// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktTicketDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工单DTO，包含工单相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================


// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktTicketDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工单DTO，包含工单相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// Takt工单DTO
/// </summary>
public class TaktTicketDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketDto()
    {
        TicketNo = string.Empty;
        Title = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 工单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }

    /// <summary>
    /// 工单编号（唯一）
    /// </summary>
    public string TicketNo { get; set; }

    /// <summary>
    /// 工单标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 工单内容描述
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 工单状态（0=待处理，1=处理中，2=已解决，3=已关闭）
    /// </summary>
    public int TicketStatus { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    /// 工单来源（0=门户网站，1=邮件，2=电话，3=API接入）
    /// </summary>
    public int TicketSource { get; set; }

    /// <summary>
    /// 提交人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SubmitterId { get; set; }

    /// <summary>
    /// 提交人姓名
    /// </summary>
    public string? SubmitterName { get; set; }

    /// <summary>
    /// 处理人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }

    /// <summary>
    /// 处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

    /// <summary>
    /// 关联知识ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }

    /// <summary>
    /// 父工单ID（为空表示顶级工单；非空表示该工单为子工单，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentTicketId { get; set; }

    /// <summary>
    /// 首次响应时间（支持人员首次回复用户的时间，SLA/OLA 时间追踪）
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }

    /// <summary>
    /// 首次响应期限（根据 SLA 计算出的首次响应截止时间）
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }

    /// <summary>
    /// 解决时间（问题被标记为已解决的时间）
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

    /// <summary>
    /// 解决期限（根据 SLA 计算出的解决截止时间）
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }

    /// <summary>
    /// 关闭时间（工单最终关闭的时间）
    /// </summary>
    public DateTime? ClosedAt { get; set; }
}

/// <summary>
/// Takt工单查询DTO
/// </summary>
public class TaktTicketQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }

    /// <summary>
    /// 工单标题（模糊）
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 工单状态（0=待处理，1=处理中，2=已解决，3=已关闭）
    /// </summary>
    public int? TicketStatus { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    /// 工单来源（0=门户网站，1=邮件，2=电话，3=API接入）
    /// </summary>
    public int? TicketSource { get; set; }

    /// <summary>
    /// 提交人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SubmitterId { get; set; }

    /// <summary>
    /// 处理人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }

    /// <summary>
    /// 父工单ID（查询子工单时传入；为空可查顶级工单）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentTicketId { get; set; }

    /// <summary>
    /// 关联知识ID（按关联知识库文章筛选工单）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }

    /// <summary>
    /// 首次响应期限起（SLA 时间追踪筛选，查询在此期限前的工单等）
    /// </summary>
    public DateTime? FirstResponseDueByFrom { get; set; }

    /// <summary>
    /// 首次响应期限止
    /// </summary>
    public DateTime? FirstResponseDueByTo { get; set; }

    /// <summary>
    /// 解决期限起
    /// </summary>
    public DateTime? ResolutionDueByFrom { get; set; }

    /// <summary>
    /// 解决期限止
    /// </summary>
    public DateTime? ResolutionDueByTo { get; set; }
}

/// <summary>
/// Takt创建工单DTO
/// </summary>
public class TaktTicketCreateDto
{
    /// <summary>
    /// 工单标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 工单内容描述
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    public int Priority { get; set; } = 1;

    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    /// 工单来源（0=门户网站，1=邮件，2=电话，3=API接入）
    /// </summary>
    public int TicketSource { get; set; } = 0;

    /// <summary>
    /// 父工单ID（创建子工单时传入；不传或为空表示顶级工单）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentTicketId { get; set; }

    /// <summary>
    /// 关联知识ID（关联知识库文章）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新工单DTO
/// </summary>
public class TaktTicketUpdateDto : TaktTicketCreateDto
{
    /// <summary>
    /// 工单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }

    /// <summary>
    /// 工单状态（0=待处理，1=处理中，2=已解决，3=已关闭）
    /// </summary>
    public int? TicketStatus { get; set; }

    /// <summary>
    /// 处理人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }

    /// <summary>
    /// 首次响应时间（支持首次回复时由系统或接口写入）
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }

    /// <summary>
    /// 首次响应期限（根据 SLA 计算后可写入）
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }

    /// <summary>
    /// 解决时间（标记已解决时由系统或接口写入）
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

    /// <summary>
    /// 解决期限（根据 SLA 计算后可写入）
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }

    /// <summary>
    /// 关闭时间（工单关闭时由系统或接口写入）
    /// </summary>
    public DateTime? ClosedAt { get; set; }
}
