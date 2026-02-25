// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.DocsCenter
// 文件名称：TaktDocumentDtos.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心文档 DTO，包含查询、创建、更新、状态等数据传输对象
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.DocsCenter;

/// <summary>
/// 文控中心文档 DTO
/// </summary>
public class TaktDocumentDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDocumentDto()
    {
        DocumentCode = string.Empty;
        DocumentTitle = string.Empty;
        ApplicantName = string.Empty;
        ConfigId = "4";
    }

    /// <summary>
    /// 文档ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DocumentId { get; set; }

    /// <summary>
    /// 文档编码（唯一索引）
    /// </summary>
    public string DocumentCode { get; set; }

    /// <summary>
    /// 文档标题
    /// </summary>
    public string DocumentTitle { get; set; }

    /// <summary>
    /// 文档类型（0=发布，1=变更，2=废止，3=其他）
    /// </summary>
    public int DocumentType { get; set; }

    /// <summary>
    /// 文档版本号
    /// </summary>
    public string? DocumentVersion { get; set; }

    /// <summary>
    /// 关联工作流实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 文档状态（0=草稿，1=审批中，2=已批准，3=已驳回，4=已发布，5=已废止）
    /// </summary>
    public int DocumentStatus { get; set; }

    /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApplicantId { get; set; }

    /// <summary>
    /// 申请人姓名
    /// </summary>
    public string ApplicantName { get; set; }

    /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }

    /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }

    /// <summary>
    /// 申请时间
    /// </summary>
    public DateTime? ApplyTime { get; set; }

    /// <summary>
    /// 批准时间
    /// </summary>
    public DateTime? ApprovedTime { get; set; }

    /// <summary>
    /// 关联文件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FileId { get; set; }

    /// <summary>
    /// 收发文方向（0=发文，1=收文）
    /// </summary>
    public int Direction { get; set; }

    /// <summary>
    /// 文种（0=通知，1=报告，2=制度，3=规定，4=其他）
    /// </summary>
    public int DocumentCategory { get; set; }

    /// <summary>
    /// 生命周期阶段（0=创建，1=审批，2=发布，3=使用，4=变更，5=归档，6=销毁）
    /// </summary>
    public int LifecycleStage { get; set; }

    /// <summary>
    /// 保管期限（年）
    /// </summary>
    public int? RetentionYears { get; set; }

    /// <summary>
    /// 生效时间
    /// </summary>
    public DateTime? EffectiveTime { get; set; }

    /// <summary>
    /// 归档时间
    /// </summary>
    public DateTime? ArchiveTime { get; set; }

    /// <summary>
    /// 作废时间
    /// </summary>
    public DateTime? ObsoleteTime { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID
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
/// 文控中心文档查询 DTO
/// </summary>
public class TaktDocumentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 文档编码
    /// </summary>
    public string? DocumentCode { get; set; }

    /// <summary>
    /// 文档标题
    /// </summary>
    public string? DocumentTitle { get; set; }

    /// <summary>
    /// 文档类型（0=发布，1=变更，2=废止，3=其他）
    /// </summary>
    public int? DocumentType { get; set; }

    /// <summary>
    /// 文档状态（0=草稿，1=审批中，2=已批准，3=已驳回，4=已发布，5=已废止）
    /// </summary>
    public int? DocumentStatus { get; set; }

    /// <summary>
    /// 收发文方向（0=发文，1=收文）
    /// </summary>
    public int? Direction { get; set; }

    /// <summary>
    /// 生命周期阶段
    /// </summary>
    public int? LifecycleStage { get; set; }
}

/// <summary>
/// 创建文控中心文档 DTO
/// </summary>
public class TaktDocumentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDocumentCreateDto()
    {
        DocumentCode = string.Empty;
        DocumentTitle = string.Empty;
        ApplicantName = string.Empty;
    }

    /// <summary>
    /// 文档编码（唯一索引）
    /// </summary>
    public string DocumentCode { get; set; }

    /// <summary>
    /// 文档标题
    /// </summary>
    public string DocumentTitle { get; set; }

    /// <summary>
    /// 文档类型（0=发布，1=变更，2=废止，3=其他）
    /// </summary>
    public int DocumentType { get; set; }

    /// <summary>
    /// 文档版本号
    /// </summary>
    public string? DocumentVersion { get; set; }

    /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApplicantId { get; set; }

    /// <summary>
    /// 申请人姓名
    /// </summary>
    public string ApplicantName { get; set; }

    /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }

    /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }

    /// <summary>
    /// 关联文件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FileId { get; set; }

    /// <summary>
    /// 收发文方向（0=发文，1=收文）
    /// </summary>
    public int Direction { get; set; }

    /// <summary>
    /// 文种（0=通知，1=报告，2=制度，3=规定，4=其他）
    /// </summary>
    public int DocumentCategory { get; set; }

    /// <summary>
    /// 生命周期阶段
    /// </summary>
    public int LifecycleStage { get; set; }

    /// <summary>
    /// 保管期限（年）
    /// </summary>
    public int? RetentionYears { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新文控中心文档 DTO
/// </summary>
public class TaktDocumentUpdateDto : TaktDocumentCreateDto
{
    /// <summary>
    /// 文档ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DocumentId { get; set; }
}

/// <summary>
/// 文控中心文档状态 DTO
/// </summary>
public class TaktDocumentStatusDto
{
    /// <summary>
    /// 文档ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DocumentId { get; set; }

    /// <summary>
    /// 文档状态（0=草稿，1=审批中，2=已批准，3=已驳回，4=已发布，5=已废止）
    /// </summary>
    public int DocumentStatus { get; set; }
}
