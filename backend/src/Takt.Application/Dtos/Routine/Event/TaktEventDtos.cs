// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Event
// 文件名称：TaktEventDtos.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：活动组织（Event）DTO，包含查询、创建、更新、导出
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Event;

/// <summary>
/// 活动组织 DTO
/// </summary>
public class TaktEventDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEventDto()
    {
        EventCode = string.Empty;
        EventName = string.Empty;
        OrganizerName = string.Empty;
        ConfigId = "4";
    }

    /// <summary>
    /// 活动ID（适配字段，序列化为 string 以避免前端精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EventId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 活动编码
    /// </summary>
    public string EventCode { get; set; }

    /// <summary>
    /// 活动名称
    /// </summary>
    public string EventName { get; set; }

    /// <summary>
    /// 活动类型（0=培训，1=团建，2=会议活动，3=庆典，4=其他）
    /// </summary>
    public int EventType { get; set; }

    /// <summary>
    /// 活动开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 活动结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 活动地点
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 组织人ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OrganizerId { get; set; }

    /// <summary>
    /// 组织人姓名
    /// </summary>
    public string OrganizerName { get; set; }

    /// <summary>
    /// 组织部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 组织部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 活动状态（0=草稿，1=已发布，2=进行中，3=已结束，4=已取消）
    /// </summary>
    public int EventStatus { get; set; }

    /// <summary>
    /// 活动内容/描述
    /// </summary>
    public string? EventContent { get; set; }

    /// <summary>
    /// 参与人摘要
    /// </summary>
    public string? ParticipantSummary { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// 活动组织查询 DTO
/// </summary>
public class TaktEventQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 活动类型（0=培训，1=团建，2=会议活动，3=庆典，4=其他）
    /// </summary>
    public int? EventType { get; set; }

    /// <summary>
    /// 活动状态（0=草稿，1=已发布，2=进行中，3=已结束，4=已取消）
    /// </summary>
    public int? EventStatus { get; set; }
}

/// <summary>
/// 活动组织创建 DTO
/// </summary>
public class TaktEventCreateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 活动名称
    /// </summary>
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// 活动类型（0=培训，1=团建，2=会议活动，3=庆典，4=其他）
    /// </summary>
    public int EventType { get; set; }

    /// <summary>
    /// 活动开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 活动结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 活动地点
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 组织部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 组织部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 活动状态（0=草稿，1=已发布，2=进行中，3=已结束，4=已取消）
    /// </summary>
    public int EventStatus { get; set; }

    /// <summary>
    /// 活动内容/描述
    /// </summary>
    public string? EventContent { get; set; }

    /// <summary>
    /// 参与人摘要
    /// </summary>
    public string? ParticipantSummary { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 活动组织更新 DTO
/// </summary>
public class TaktEventUpdateDto : TaktEventCreateDto
{
    /// <summary>
    /// 活动ID（适配字段）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EventId { get; set; }
}

/// <summary>
/// 活动组织导出 DTO
/// </summary>
public class TaktEventExportDto
{
    /// <summary>
    /// 活动编码
    /// </summary>
    public string EventCode { get; set; } = string.Empty;

    /// <summary>
    /// 活动名称
    /// </summary>
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// 活动类型（0=培训，1=团建，2=会议活动，3=庆典，4=其他）
    /// </summary>
    public int EventType { get; set; }

    /// <summary>
    /// 活动开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 活动结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 活动地点
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 组织人姓名
    /// </summary>
    public string OrganizerName { get; set; } = string.Empty;

    /// <summary>
    /// 组织部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 活动状态（0=草稿，1=已发布，2=进行中，3=已结束，4=已取消）
    /// </summary>
    public int EventStatus { get; set; }

    /// <summary>
    /// 参与人摘要
    /// </summary>
    public string? ParticipantSummary { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
