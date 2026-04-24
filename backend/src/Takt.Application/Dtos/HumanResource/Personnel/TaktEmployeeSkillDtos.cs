// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeSkillDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工业务技能表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工业务技能表Dto
/// </summary>
public partial class TaktEmployeeSkillDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillDto()
    {
        SkillName = string.Empty;
    }

    /// <summary>
    /// 员工业务技能表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeSkillId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }
    /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }
    /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }
    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }
    /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }
    /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
}

/// <summary>
/// 员工业务技能表查询DTO
/// </summary>
public partial class TaktEmployeeSkillQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工业务技能表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeSkillId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 技能名称
    /// </summary>
    public string? SkillName { get; set; }
    /// <summary>
    /// 技能等级
    /// </summary>
    public int? SkillLevel { get; set; }
    /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }
    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }
    /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }

    /// <summary>
    /// 获得日期开始时间
    /// </summary>
    public DateTime? ObtainedDateStart { get; set; }
    /// <summary>
    /// 获得日期结束时间
    /// </summary>
    public DateTime? ObtainedDateEnd { get; set; }
    /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 到期日期开始时间
    /// </summary>
    public DateTime? ExpiryDateStart { get; set; }
    /// <summary>
    /// 到期日期结束时间
    /// </summary>
    public DateTime? ExpiryDateEnd { get; set; }

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
/// Takt创建员工业务技能表DTO
/// </summary>
public partial class TaktEmployeeSkillCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillCreateDto()
    {
        SkillName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }

        /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

        /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

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
/// Takt更新员工业务技能表DTO
/// </summary>
public partial class TaktEmployeeSkillUpdateDto : TaktEmployeeSkillCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillUpdateDto()
    {
    }

        /// <summary>
    /// 员工业务技能表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeSkillId { get; set; }
}

/// <summary>
/// 员工业务技能表导入模板DTO
/// </summary>
public partial class TaktEmployeeSkillTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillTemplateDto()
    {
        SkillName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }

        /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

        /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

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
/// 员工业务技能表导入DTO
/// </summary>
public partial class TaktEmployeeSkillImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillImportDto()
    {
        SkillName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }

        /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

        /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

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
/// 员工业务技能表导出DTO
/// </summary>
public partial class TaktEmployeeSkillExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillExportDto()
    {
        CreatedAt = DateTime.Now;
        SkillName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }

        /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

        /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}