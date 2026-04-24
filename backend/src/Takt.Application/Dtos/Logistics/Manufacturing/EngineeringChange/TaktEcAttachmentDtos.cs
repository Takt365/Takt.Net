// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcAttachmentDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：设变附件表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变附件表Dto
/// </summary>
public partial class TaktEcAttachmentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentDto()
    {
        AccessUrl = string.Empty;
    }

    /// <summary>
    /// 设变附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcAttachmentId { get; set; }

    /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnId { get; set; }
    /// <summary>
    /// 文件类别
    /// </summary>
    public string? AttachmentType { get; set; }
    /// <summary>
    /// 文件编号
    /// </summary>
    public string? DocNo { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// 访问地址
    /// </summary>
    public string AccessUrl { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 设变附件表查询DTO
/// </summary>
public partial class TaktEcAttachmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 设变附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcAttachmentId { get; set; }

    /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EcnId { get; set; }
    /// <summary>
    /// 文件类别
    /// </summary>
    public string? AttachmentType { get; set; }
    /// <summary>
    /// 文件编号
    /// </summary>
    public string? DocNo { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// 访问地址
    /// </summary>
    public string? AccessUrl { get; set; }
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
/// Takt创建设变附件表DTO
/// </summary>
public partial class TaktEcAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentCreateDto()
    {
        AccessUrl = string.Empty;
    }

        /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnId { get; set; }

        /// <summary>
    /// 文件类别
    /// </summary>
    public string? AttachmentType { get; set; }

        /// <summary>
    /// 文件编号
    /// </summary>
    public string? DocNo { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }

        /// <summary>
    /// 访问地址
    /// </summary>
    public string AccessUrl { get; set; }

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
/// Takt更新设变附件表DTO
/// </summary>
public partial class TaktEcAttachmentUpdateDto : TaktEcAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentUpdateDto()
    {
    }

        /// <summary>
    /// 设变附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcAttachmentId { get; set; }
}

/// <summary>
/// 设变附件表导入模板DTO
/// </summary>
public partial class TaktEcAttachmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentTemplateDto()
    {
        AccessUrl = string.Empty;
    }

        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 文件类别
    /// </summary>
    public string? AttachmentType { get; set; }

        /// <summary>
    /// 文件编号
    /// </summary>
    public string? DocNo { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }

        /// <summary>
    /// 访问地址
    /// </summary>
    public string AccessUrl { get; set; }

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
/// 设变附件表导入DTO
/// </summary>
public partial class TaktEcAttachmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentImportDto()
    {
        AccessUrl = string.Empty;
    }

        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 文件类别
    /// </summary>
    public string? AttachmentType { get; set; }

        /// <summary>
    /// 文件编号
    /// </summary>
    public string? DocNo { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }

        /// <summary>
    /// 访问地址
    /// </summary>
    public string AccessUrl { get; set; }

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
/// 设变附件表导出DTO
/// </summary>
public partial class TaktEcAttachmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentExportDto()
    {
        CreatedAt = DateTime.Now;
        AccessUrl = string.Empty;
    }

        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 文件类别
    /// </summary>
    public string? AttachmentType { get; set; }

        /// <summary>
    /// 文件编号
    /// </summary>
    public string? DocNo { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }

        /// <summary>
    /// 访问地址
    /// </summary>
    public string AccessUrl { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}