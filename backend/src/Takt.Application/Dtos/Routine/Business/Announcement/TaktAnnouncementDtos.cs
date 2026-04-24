// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.Announcement
// 文件名称：TaktAnnouncementDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：公告表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.Announcement;

/// <summary>
/// 公告表Dto
/// </summary>
public partial class TaktAnnouncementDto : TaktDtosEntityBase
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
    }

    /// <summary>
    /// 公告表（适配字段，序列化为string以避免Javascript精度问题）
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
    /// 公告类型
    /// </summary>
    public int AnnouncementType { get; set; }
    /// <summary>
    /// 公告内容
    /// </summary>
    public string AnnouncementContent { get; set; }
    /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }
    /// <summary>
    /// 阅读记录JSON
    /// </summary>
    public string? ReadRecordsJson { get; set; }
    /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }
    /// <summary>
    /// 发布范围
    /// </summary>
    public int PublishScope { get; set; }
    /// <summary>
    /// 发布范围配置
    /// </summary>
    public string? PublishScopeConfig { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }
    /// <summary>
    /// 是否紧急
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
    public int SortOrder { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 发布部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PublisherId { get; set; }
    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }
    /// <summary>
    /// 公告状态
    /// </summary>
    public int AnnouncementStatus { get; set; }
}

/// <summary>
/// 公告表查询DTO
/// </summary>
public partial class TaktAnnouncementQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 公告表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 公告编码
    /// </summary>
    public string? AnnouncementCode { get; set; }
    /// <summary>
    /// 公告标题
    /// </summary>
    public string? AnnouncementTitle { get; set; }
    /// <summary>
    /// 公告类型
    /// </summary>
    public int? AnnouncementType { get; set; }
    /// <summary>
    /// 公告内容
    /// </summary>
    public string? AnnouncementContent { get; set; }
    /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }
    /// <summary>
    /// 阅读记录JSON
    /// </summary>
    public string? ReadRecordsJson { get; set; }
    /// <summary>
    /// 阅读次数
    /// </summary>
    public int? ReadCount { get; set; }
    /// <summary>
    /// 发布范围
    /// </summary>
    public int? PublishScope { get; set; }
    /// <summary>
    /// 发布范围配置
    /// </summary>
    public string? PublishScopeConfig { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public int? IsTop { get; set; }
    /// <summary>
    /// 是否紧急
    /// </summary>
    public int? IsUrgent { get; set; }
    /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

    /// <summary>
    /// 生效时间开始时间
    /// </summary>
    public DateTime? EffectiveTimeStart { get; set; }
    /// <summary>
    /// 生效时间结束时间
    /// </summary>
    public DateTime? EffectiveTimeEnd { get; set; }
    /// <summary>
    /// 失效时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 失效时间开始时间
    /// </summary>
    public DateTime? ExpireTimeStart { get; set; }
    /// <summary>
    /// 失效时间结束时间
    /// </summary>
    public DateTime? ExpireTimeEnd { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 发布部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PublisherId { get; set; }
    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublisherName { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 发布时间开始时间
    /// </summary>
    public DateTime? PublishTimeStart { get; set; }
    /// <summary>
    /// 发布时间结束时间
    /// </summary>
    public DateTime? PublishTimeEnd { get; set; }
    /// <summary>
    /// 公告状态
    /// </summary>
    public int? AnnouncementStatus { get; set; }

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
/// Takt创建公告表DTO
/// </summary>
public partial class TaktAnnouncementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementCreateDto()
    {
        AnnouncementCode = string.Empty;
        AnnouncementTitle = string.Empty;
        AnnouncementContent = string.Empty;
        PublisherName = string.Empty;
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
    /// 公告类型
    /// </summary>
    public int AnnouncementType { get; set; }

        /// <summary>
    /// 公告内容
    /// </summary>
    public string AnnouncementContent { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 阅读记录JSON
    /// </summary>
    public string? ReadRecordsJson { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 发布范围
    /// </summary>
    public int PublishScope { get; set; }

        /// <summary>
    /// 发布范围配置
    /// </summary>
    public string? PublishScopeConfig { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否紧急
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 公告状态
    /// </summary>
    public int AnnouncementStatus { get; set; }

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
/// Takt更新公告表DTO
/// </summary>
public partial class TaktAnnouncementUpdateDto : TaktAnnouncementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementUpdateDto()
    {
    }

        /// <summary>
    /// 公告表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }
}

/// <summary>
/// 公告表公告状态DTO
/// </summary>
public partial class TaktAnnouncementStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementStatusDto()
    {
    }

        /// <summary>
    /// 公告表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AnnouncementId { get; set; }

    /// <summary>
    /// 公告状态（0=禁用，1=启用）
    /// </summary>
    public int AnnouncementStatus { get; set; }
}

/// <summary>
/// 公告表导入模板DTO
/// </summary>
public partial class TaktAnnouncementTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementTemplateDto()
    {
        AnnouncementCode = string.Empty;
        AnnouncementTitle = string.Empty;
        AnnouncementContent = string.Empty;
        PublisherName = string.Empty;
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
    /// 公告类型
    /// </summary>
    public int AnnouncementType { get; set; }

        /// <summary>
    /// 公告内容
    /// </summary>
    public string AnnouncementContent { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 阅读记录JSON
    /// </summary>
    public string? ReadRecordsJson { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 发布范围
    /// </summary>
    public int PublishScope { get; set; }

        /// <summary>
    /// 发布范围配置
    /// </summary>
    public string? PublishScopeConfig { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否紧急
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 公告状态
    /// </summary>
    public int AnnouncementStatus { get; set; }

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
/// 公告表导入DTO
/// </summary>
public partial class TaktAnnouncementImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementImportDto()
    {
        AnnouncementCode = string.Empty;
        AnnouncementTitle = string.Empty;
        AnnouncementContent = string.Empty;
        PublisherName = string.Empty;
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
    /// 公告类型
    /// </summary>
    public int AnnouncementType { get; set; }

        /// <summary>
    /// 公告内容
    /// </summary>
    public string AnnouncementContent { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 阅读记录JSON
    /// </summary>
    public string? ReadRecordsJson { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 发布范围
    /// </summary>
    public int PublishScope { get; set; }

        /// <summary>
    /// 发布范围配置
    /// </summary>
    public string? PublishScopeConfig { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否紧急
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 公告状态
    /// </summary>
    public int AnnouncementStatus { get; set; }

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
/// 公告表导出DTO
/// </summary>
public partial class TaktAnnouncementExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementExportDto()
    {
        CreatedAt = DateTime.Now;
        AnnouncementCode = string.Empty;
        AnnouncementTitle = string.Empty;
        AnnouncementContent = string.Empty;
        PublisherName = string.Empty;
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
    /// 公告类型
    /// </summary>
    public int AnnouncementType { get; set; }

        /// <summary>
    /// 公告内容
    /// </summary>
    public string AnnouncementContent { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 阅读记录JSON
    /// </summary>
    public string? ReadRecordsJson { get; set; }

        /// <summary>
    /// 阅读次数
    /// </summary>
    public int ReadCount { get; set; }

        /// <summary>
    /// 发布范围
    /// </summary>
    public int PublishScope { get; set; }

        /// <summary>
    /// 发布范围配置
    /// </summary>
    public string? PublishScopeConfig { get; set; }

        /// <summary>
    /// 是否置顶
    /// </summary>
    public int IsTop { get; set; }

        /// <summary>
    /// 是否紧急
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 发布部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 发布部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long PublisherId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string PublisherName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 公告状态
    /// </summary>
    public int AnnouncementStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}