// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Dict
// 文件名称：TaktDictTypeDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：字典类型表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.Dict;

/// <summary>
/// 字典类型表Dto
/// </summary>
public partial class TaktDictTypeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataConfigId = string.Empty;
    }

    /// <summary>
    /// 字典类型表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string DictTypeCode { get; set; }
    /// <summary>
    /// 类型名称
    /// </summary>
    public string DictTypeName { get; set; }
    /// <summary>
    /// 数据源
    /// </summary>
    public int DataSource { get; set; }
    /// <summary>
    /// 数据库配置ID
    /// </summary>
    public string DataConfigId { get; set; }
    /// <summary>
    /// SQL脚本
    /// </summary>
    public string? SqlScript { get; set; }
    /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 类型状态
    /// </summary>
    public int DictTypeStatus { get; set; }

    /// <summary>
    /// 字典数据列表（外键：子表 TaktDictData.DictTypeId 关联本表 Id）
    /// </summary>
    public List<long>? DictDataList { get; set; }
}

/// <summary>
/// 字典类型表查询DTO
/// </summary>
public partial class TaktDictTypeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 字典类型表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string? DictTypeCode { get; set; }
    /// <summary>
    /// 类型名称
    /// </summary>
    public string? DictTypeName { get; set; }
    /// <summary>
    /// 数据源
    /// </summary>
    public int? DataSource { get; set; }
    /// <summary>
    /// 数据库配置ID
    /// </summary>
    public string? DataConfigId { get; set; }
    /// <summary>
    /// SQL脚本
    /// </summary>
    public string? SqlScript { get; set; }
    /// <summary>
    /// 是否内置
    /// </summary>
    public int? IsBuiltIn { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 类型状态
    /// </summary>
    public int? DictTypeStatus { get; set; }

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
/// Takt创建字典类型表DTO
/// </summary>
public partial class TaktDictTypeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeCreateDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataConfigId = string.Empty;
    }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 类型名称
    /// </summary>
    public string DictTypeName { get; set; }

        /// <summary>
    /// 数据源
    /// </summary>
    public int DataSource { get; set; }

        /// <summary>
    /// 数据库配置ID
    /// </summary>
    public string DataConfigId { get; set; }

        /// <summary>
    /// SQL脚本
    /// </summary>
    public string? SqlScript { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 类型状态
    /// </summary>
    public int DictTypeStatus { get; set; }

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
/// Takt更新字典类型表DTO
/// </summary>
public partial class TaktDictTypeUpdateDto : TaktDictTypeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeUpdateDto()
    {
    }

        /// <summary>
    /// 字典类型表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }
}

/// <summary>
/// 字典类型表类型状态DTO
/// </summary>
public partial class TaktDictTypeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeStatusDto()
    {
    }

        /// <summary>
    /// 字典类型表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 类型状态（0=禁用，1=启用）
    /// </summary>
    public int DictTypeStatus { get; set; }
}

/// <summary>
/// 字典类型表导入模板DTO
/// </summary>
public partial class TaktDictTypeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeTemplateDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataConfigId = string.Empty;
    }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 类型名称
    /// </summary>
    public string DictTypeName { get; set; }

        /// <summary>
    /// 数据源
    /// </summary>
    public int DataSource { get; set; }

        /// <summary>
    /// 数据库配置ID
    /// </summary>
    public string DataConfigId { get; set; }

        /// <summary>
    /// SQL脚本
    /// </summary>
    public string? SqlScript { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 类型状态
    /// </summary>
    public int DictTypeStatus { get; set; }

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
/// 字典类型表导入DTO
/// </summary>
public partial class TaktDictTypeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeImportDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataConfigId = string.Empty;
    }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 类型名称
    /// </summary>
    public string DictTypeName { get; set; }

        /// <summary>
    /// 数据源
    /// </summary>
    public int DataSource { get; set; }

        /// <summary>
    /// 数据库配置ID
    /// </summary>
    public string DataConfigId { get; set; }

        /// <summary>
    /// SQL脚本
    /// </summary>
    public string? SqlScript { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 类型状态
    /// </summary>
    public int DictTypeStatus { get; set; }

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
/// 字典类型表导出DTO
/// </summary>
public partial class TaktDictTypeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeExportDto()
    {
        CreatedAt = DateTime.Now;
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataConfigId = string.Empty;
    }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 类型名称
    /// </summary>
    public string DictTypeName { get; set; }

        /// <summary>
    /// 数据源
    /// </summary>
    public int DataSource { get; set; }

        /// <summary>
    /// 数据库配置ID
    /// </summary>
    public string DataConfigId { get; set; }

        /// <summary>
    /// SQL脚本
    /// </summary>
    public string? SqlScript { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 类型状态
    /// </summary>
    public int DictTypeStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}