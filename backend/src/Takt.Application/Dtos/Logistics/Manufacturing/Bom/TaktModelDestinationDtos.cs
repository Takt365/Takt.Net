// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Bom
// 文件名称：TaktModelDestinationDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：型号目的地表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom;

/// <summary>
/// 型号目的地表Dto
/// </summary>
public partial class TaktModelDestinationDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationDto()
    {
        MaterialName = string.Empty;
        ModelName = string.Empty;
        DestinationName = string.Empty;
    }

    /// <summary>
    /// 型号目的地表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ModelDestinationId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }
    /// <summary>
    /// 机种名称
    /// </summary>
    public string ModelName { get; set; }
    /// <summary>
    /// 仕向地名称
    /// </summary>
    public string DestinationName { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 型号目的地表查询DTO
/// </summary>
public partial class TaktModelDestinationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 型号目的地表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ModelDestinationId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }
    /// <summary>
    /// 机种名称
    /// </summary>
    public string? ModelName { get; set; }
    /// <summary>
    /// 仕向地名称
    /// </summary>
    public string? DestinationName { get; set; }
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
/// Takt创建型号目的地表DTO
/// </summary>
public partial class TaktModelDestinationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationCreateDto()
    {
        MaterialName = string.Empty;
        ModelName = string.Empty;
        DestinationName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 机种名称
    /// </summary>
    public string ModelName { get; set; }

        /// <summary>
    /// 仕向地名称
    /// </summary>
    public string DestinationName { get; set; }

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
/// Takt更新型号目的地表DTO
/// </summary>
public partial class TaktModelDestinationUpdateDto : TaktModelDestinationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationUpdateDto()
    {
    }

        /// <summary>
    /// 型号目的地表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ModelDestinationId { get; set; }
}

/// <summary>
/// 型号目的地表导入模板DTO
/// </summary>
public partial class TaktModelDestinationTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationTemplateDto()
    {
        MaterialName = string.Empty;
        ModelName = string.Empty;
        DestinationName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 机种名称
    /// </summary>
    public string ModelName { get; set; }

        /// <summary>
    /// 仕向地名称
    /// </summary>
    public string DestinationName { get; set; }

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
/// 型号目的地表导入DTO
/// </summary>
public partial class TaktModelDestinationImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationImportDto()
    {
        MaterialName = string.Empty;
        ModelName = string.Empty;
        DestinationName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 机种名称
    /// </summary>
    public string ModelName { get; set; }

        /// <summary>
    /// 仕向地名称
    /// </summary>
    public string DestinationName { get; set; }

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
/// 型号目的地表导出DTO
/// </summary>
public partial class TaktModelDestinationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationExportDto()
    {
        CreatedAt = DateTime.Now;
        MaterialName = string.Empty;
        ModelName = string.Empty;
        DestinationName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 机种名称
    /// </summary>
    public string ModelName { get; set; }

        /// <summary>
    /// 仕向地名称
    /// </summary>
    public string DestinationName { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}