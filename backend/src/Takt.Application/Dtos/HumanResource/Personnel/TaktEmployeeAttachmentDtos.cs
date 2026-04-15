// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeAttachmentDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工附件DTO，包含员工附件相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工附件DTO
/// </summary>
public class TaktEmployeeAttachmentDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FilePath = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 附件ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AttachmentId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 文件ID（关联TaktFile）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他）
    /// </summary>
    public int AttachmentType { get; set; }

    /// <summary>
    /// 附件描述
    /// </summary>
    public string? AttachmentDescription { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt员工附件查询DTO
/// </summary>
public class TaktEmployeeAttachmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentQueryDto()
    {
    }

    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 文件ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FileId { get; set; }

    /// <summary>
    /// 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他；null 表示全部）
    /// </summary>
    public int? AttachmentType { get; set; }
}

/// <summary>
/// Takt创建员工附件DTO
/// </summary>
public class TaktEmployeeAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentCreateDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FilePath = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 文件ID（关联TaktFile）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他）
    /// </summary>
    public int AttachmentType { get; set; }

    /// <summary>
    /// 附件描述
    /// </summary>
    public string? AttachmentDescription { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt更新员工附件DTO
/// </summary>
public class TaktEmployeeAttachmentUpdateDto : TaktEmployeeAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentUpdateDto()
    {
    }

    /// <summary>
    /// 附件ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AttachmentId { get; set; }
}

/// <summary>
/// Takt员工附件导入模板DTO
/// </summary>
public class TaktEmployeeAttachmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentTemplateDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FilePath = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他）
    /// </summary>
    public int AttachmentType { get; set; }

    /// <summary>
    /// 附件描述
    /// </summary>
    public string? AttachmentDescription { get; set; }

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
/// Takt员工附件导入DTO
/// </summary>
public class TaktEmployeeAttachmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentImportDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FilePath = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他）
    /// </summary>
    public int AttachmentType { get; set; }

    /// <summary>
    /// 附件描述
    /// </summary>
    public string? AttachmentDescription { get; set; }

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
/// Takt员工附件导出DTO
/// </summary>
public class TaktEmployeeAttachmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentExportDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FilePath = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 附件ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AttachmentId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他）
    /// </summary>
    public int AttachmentType { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
