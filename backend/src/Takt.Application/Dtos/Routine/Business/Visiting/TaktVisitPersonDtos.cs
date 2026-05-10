// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.Visiting
// 文件名称：TaktVisitPersonDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：参访人员表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Business.Visiting;

/// <summary>
/// 参访人员表Dto
/// </summary>
public partial class TaktVisitPersonDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonDto()
    {
        Department = string.Empty;
        JobTitle = string.Empty;
        PersonName = string.Empty;
    }

    /// <summary>
    /// 参访人员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VisitPersonId { get; set; } = 0;

    /// <summary>
    /// 参访记录ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VisitId { get; set; }
    /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; }
    /// <summary>
    /// 职称
    /// </summary>
    public string JobTitle { get; set; }
    /// <summary>
    /// 参访人员
    /// </summary>
    public string PersonName { get; set; }

    /// <summary>
    /// 参访记录（主表）
    /// </summary>
    public TaktVisitDto? Visit { get; set; }
}

/// <summary>
/// 参访人员表查询DTO
/// </summary>
public partial class TaktVisitPersonQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 参访记录ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? VisitId { get; set; }
    /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }
    /// <summary>
    /// 职称
    /// </summary>
    public string? JobTitle { get; set; }
    /// <summary>
    /// 参访人员
    /// </summary>
    public string? PersonName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
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
/// Takt创建参访人员表DTO
/// </summary>
public partial class TaktVisitPersonCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonCreateDto()
    {
        Department = string.Empty;
        JobTitle = string.Empty;
        PersonName = string.Empty;
    }

        /// <summary>
    /// 参访记录ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string JobTitle { get; set; }

        /// <summary>
    /// 参访人员
    /// </summary>
    public string PersonName { get; set; }

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
/// Takt更新参访人员表DTO
/// </summary>
public partial class TaktVisitPersonUpdateDto : TaktVisitPersonCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonUpdateDto()
    {
    }

        /// <summary>
    /// 参访人员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VisitPersonId { get; set; } = 0;
}

/// <summary>
/// 参访人员表导入模板DTO
/// </summary>
public partial class TaktVisitPersonTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonTemplateDto()
    {
        Department = string.Empty;
        JobTitle = string.Empty;
        PersonName = string.Empty;
    }

        /// <summary>
    /// 参访记录ID
    /// </summary>
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string JobTitle { get; set; }

        /// <summary>
    /// 参访人员
    /// </summary>
    public string PersonName { get; set; }

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
/// 参访人员表导入DTO
/// </summary>
public partial class TaktVisitPersonImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonImportDto()
    {
        Department = string.Empty;
        JobTitle = string.Empty;
        PersonName = string.Empty;
    }

        /// <summary>
    /// 参访记录ID
    /// </summary>
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string JobTitle { get; set; }

        /// <summary>
    /// 参访人员
    /// </summary>
    public string PersonName { get; set; }

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
/// 参访人员表导出DTO
/// </summary>
public partial class TaktVisitPersonExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonExportDto()
    {
        CreatedAt = DateTime.Now;
        Department = string.Empty;
        JobTitle = string.Empty;
        PersonName = string.Empty;
    }

        /// <summary>
    /// 参访记录ID
    /// </summary>
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string JobTitle { get; set; }

        /// <summary>
    /// 参访人员
    /// </summary>
    public string PersonName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}