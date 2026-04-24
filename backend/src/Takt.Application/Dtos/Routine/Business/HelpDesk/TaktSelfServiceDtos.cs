// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.HelpDesk
// 文件名称：TaktSelfServiceDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：自助服务表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// 自助服务表Dto
/// </summary>
public partial class TaktSelfServiceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceDto()
    {
        ServiceName = string.Empty;
    }

    /// <summary>
    /// 自助服务表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SelfServiceId { get; set; }

    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }
    /// <summary>
    /// 服务类型
    /// </summary>
    public int ServiceType { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 链接或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }
    /// <summary>
    /// 图标URL
    /// </summary>
    public string? IconUrl { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int SelfServiceStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 自助服务表查询DTO
/// </summary>
public partial class TaktSelfServiceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 自助服务表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SelfServiceId { get; set; }

    /// <summary>
    /// 服务名称
    /// </summary>
    public string? ServiceName { get; set; }
    /// <summary>
    /// 服务类型
    /// </summary>
    public int? ServiceType { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 链接或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }
    /// <summary>
    /// 图标URL
    /// </summary>
    public string? IconUrl { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? SelfServiceStatus { get; set; }
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
/// Takt创建自助服务表DTO
/// </summary>
public partial class TaktSelfServiceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceCreateDto()
    {
        ServiceName = string.Empty;
    }

        /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }

        /// <summary>
    /// 服务类型
    /// </summary>
    public int ServiceType { get; set; }

        /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

        /// <summary>
    /// 链接或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }

        /// <summary>
    /// 图标URL
    /// </summary>
    public string? IconUrl { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int SelfServiceStatus { get; set; }

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
/// Takt更新自助服务表DTO
/// </summary>
public partial class TaktSelfServiceUpdateDto : TaktSelfServiceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceUpdateDto()
    {
    }

        /// <summary>
    /// 自助服务表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SelfServiceId { get; set; }
}

/// <summary>
/// 自助服务表状态DTO
/// </summary>
public partial class TaktSelfServiceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceStatusDto()
    {
    }

        /// <summary>
    /// 自助服务表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SelfServiceId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int SelfServiceStatus { get; set; }
}

/// <summary>
/// 自助服务表导入模板DTO
/// </summary>
public partial class TaktSelfServiceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceTemplateDto()
    {
        ServiceName = string.Empty;
    }

        /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }

        /// <summary>
    /// 服务类型
    /// </summary>
    public int ServiceType { get; set; }

        /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

        /// <summary>
    /// 链接或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }

        /// <summary>
    /// 图标URL
    /// </summary>
    public string? IconUrl { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int SelfServiceStatus { get; set; }

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
/// 自助服务表导入DTO
/// </summary>
public partial class TaktSelfServiceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceImportDto()
    {
        ServiceName = string.Empty;
    }

        /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }

        /// <summary>
    /// 服务类型
    /// </summary>
    public int ServiceType { get; set; }

        /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

        /// <summary>
    /// 链接或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }

        /// <summary>
    /// 图标URL
    /// </summary>
    public string? IconUrl { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int SelfServiceStatus { get; set; }

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
/// 自助服务表导出DTO
/// </summary>
public partial class TaktSelfServiceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceExportDto()
    {
        CreatedAt = DateTime.Now;
        ServiceName = string.Empty;
    }

        /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }

        /// <summary>
    /// 服务类型
    /// </summary>
    public int ServiceType { get; set; }

        /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

        /// <summary>
    /// 链接或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }

        /// <summary>
    /// 图标URL
    /// </summary>
    public string? IconUrl { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int SelfServiceStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}