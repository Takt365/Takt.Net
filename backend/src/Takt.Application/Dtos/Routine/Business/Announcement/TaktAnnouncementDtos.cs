// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.Announcement
// 文件名称：TaktAnnouncementDtos.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公告通知DTO，包含公告相关的数据传输对象（查询、创建、更新、导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.Announcement;

/// <summary>
/// Takt公告通知DTO
/// </summary>
public class TaktAnnouncementDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementDto()
    {
        AnnouncementCode = string.Empty;
        AnnouncementTitle = string.Empty;
        AnnouncementContent = string.Empty;
        PublisherName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 公告ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 公告编码（唯一）
    /// </summary>
    public string AnnouncementCode { get; set; }

    /// <summary>
    /// 公告标题
    /// </summary>
    public string AnnouncementTitle { get; set; }

    /// <summary>
    /// 公告内容
    /// </summary>
    public string AnnouncementContent { get; set; }

    /// <summary>
    /// 公告类型（0=通知，1=公告，2=新闻，3=活动）
    /// </summary>
    public int AnnouncementType { get; set; }

    /// <summary>
    /// 发布人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PublisherId { get; set; }

    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

    /// <summary>
    /// 发布部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 发布范围（0=全部，1=指定部门，2=指定用户，3=指定角色）
    /// </summary>
    public int PublishScope { get; set; }

    /// <summary>
    /// 发布范围配置（JSON）
    /// </summary>
    public string? PublishScopeConfig { get; set; }

    /// <summary>
    /// 是否置顶（0=否，1=是）
    /// </summary>
    public int IsTop { get; set; }

    /// <summary>
    /// 是否紧急（0=一般，1=紧急，2=非常紧急）
    /// </summary>
    public int IsUrgent { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

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
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期）
    /// </summary>
    public int AnnouncementStatus { get; set; }
}

/// <summary>
/// Takt公告通知查询DTO
/// </summary>
public class TaktAnnouncementQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在公告编码、公告标题中模糊查询

    /// <summary>
    /// 公告编码
    /// </summary>
    public string? AnnouncementCode { get; set; }

    /// <summary>
    /// 公告标题
    /// </summary>
    public string? AnnouncementTitle { get; set; }

    /// <summary>
    /// 公告类型（0=通知，1=公告，2=新闻，3=活动）
    /// </summary>
    public int? AnnouncementType { get; set; }

    /// <summary>
    /// 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期）
    /// </summary>
    public int? AnnouncementStatus { get; set; }
}

/// <summary>
/// Takt创建公告通知DTO
/// </summary>
public class TaktAnnouncementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementCreateDto()
    {
        AnnouncementCode = string.Empty;
        AnnouncementTitle = string.Empty;
        AnnouncementContent = string.Empty;
    }

    /// <summary>
    /// 公告编码（唯一）
    /// </summary>
    public string AnnouncementCode { get; set; } = string.Empty;

    /// <summary>
    /// 公告标题
    /// </summary>
    public string AnnouncementTitle { get; set; } = string.Empty;

    /// <summary>
    /// 公告内容
    /// </summary>
    public string AnnouncementContent { get; set; } = string.Empty;

    /// <summary>
    /// 公告类型（0=通知，1=公告，2=新闻，3=活动）
    /// </summary>
    public int AnnouncementType { get; set; }

    /// <summary>
    /// 发布部门ID（可选）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 发布部门名称（可选）
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 发布范围（0=全部，1=指定部门，2=指定用户，3=指定角色）
    /// </summary>
    public int PublishScope { get; set; } = 0;

    /// <summary>
    /// 发布范围配置（JSON，可选）
    /// </summary>
    public string? PublishScopeConfig { get; set; }

    /// <summary>
    /// 是否置顶（0=否，1=是）
    /// </summary>
    public int IsTop { get; set; } = 0;

    /// <summary>
    /// 是否紧急（0=一般，1=紧急，2=非常紧急）
    /// </summary>
    public int IsUrgent { get; set; } = 0;

    /// <summary>
    /// 生效时间（可选）
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

    /// <summary>
    /// 失效时间（可选）
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新公告通知DTO
/// </summary>
public class TaktAnnouncementUpdateDto : TaktAnnouncementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementUpdateDto()
    {
    }

    /// <summary>
    /// 公告ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }
}

/// <summary>
/// Takt公告通知状态DTO
/// </summary>
public class TaktAnnouncementStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementStatusDto()
    {
    }

    /// <summary>
    /// 公告ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期）
    /// </summary>
    public int AnnouncementStatus { get; set; }
}

/// <summary>
/// Takt公告通知导出DTO
/// </summary>
public class TaktAnnouncementExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementExportDto()
    {
        AnnouncementCode = string.Empty;
        AnnouncementTitle = string.Empty;
        PublisherName = string.Empty;
        AnnouncementStatus = 0;
        ReadCount = 0;
        OrderNum = 0;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 公告编码
    /// </summary>
    public string AnnouncementCode { get; set; }

    /// <summary>
    /// 公告标题
    /// </summary>
    public string AnnouncementTitle { get; set; }

    /// <summary>
    /// 公告类型（0=通知，1=公告，2=新闻，3=活动）
    /// </summary>
    public int AnnouncementType { get; set; }

    /// <summary>
    /// 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期）
    /// </summary>
    public int AnnouncementStatus { get; set; }

    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

    /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

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
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
