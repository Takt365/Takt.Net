// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Announcement
// 文件名称：TaktAnnouncementDtos.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公告DTO，包含公告、附件、阅读记录相关的数据传输对象
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Announcement;

/// <summary>
/// Takt公告DTO
/// </summary>
public class TaktAnnouncementDto
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
        ConfigId = "4";
    }

    /// <summary>
    /// 公告ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 公告编码
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
    /// 发布范围配置（JSON格式）
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

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 扩展字段JSON（与实体基类一致）
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 更新人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt公告查询DTO
/// </summary>
public class TaktAnnouncementQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 公告类型
    /// </summary>
    public int? AnnouncementType { get; set; }

    /// <summary>
    /// 公告状态
    /// </summary>
    public int? AnnouncementStatus { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public int? IsTop { get; set; }
}

/// <summary>
/// Takt公告创建DTO
/// </summary>
public class TaktAnnouncementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementCreateDto()
    {
        AnnouncementTitle = string.Empty;
        AnnouncementContent = string.Empty;
    }

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
    /// 发布范围（0=全部，1=指定部门，2=指定用户，3=指定角色）
    /// </summary>
    public int PublishScope { get; set; }

    /// <summary>
    /// 发布范围配置（JSON格式）
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
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

    /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 附件列表（创建时可选上传的附件）
    /// </summary>
    public List<TaktAnnouncementAttachmentCreateDto>? Attachments { get; set; }
}

/// <summary>
/// Takt公告更新DTO
/// </summary>
public class TaktAnnouncementUpdateDto : TaktAnnouncementCreateDto
{
    /// <summary>
    /// 公告ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }
}

/// <summary>
/// Takt公告状态DTO（发布/撤回等）
/// </summary>
public class TaktAnnouncementStatusDto
{
    /// <summary>
    /// 公告ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期）
    /// </summary>
    public int AnnouncementStatus { get; set; }
}

/// <summary>
/// Takt公告附件DTO
/// </summary>
public class TaktAnnouncementAttachmentDto
{
    /// <summary>
    /// 附件ID（适配字段）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttachmentId { get; set; }

    /// <summary>
    /// 公告ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt公告附件创建DTO
/// </summary>
public class TaktAnnouncementAttachmentCreateDto
{
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt公告阅读记录DTO
/// </summary>
public class TaktAnnouncementReadDto
{
    /// <summary>
    /// 阅读记录ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReadId { get; set; }

    /// <summary>
    /// 公告ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户姓名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime ReadTime { get; set; }

    /// <summary>
    /// 阅读时长（秒）
    /// </summary>
    public int ReadDuration { get; set; }
}
