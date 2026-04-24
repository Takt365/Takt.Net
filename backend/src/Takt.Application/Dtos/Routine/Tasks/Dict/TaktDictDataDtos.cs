// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Dict
// 文件名称：TaktDictDataDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：字典数据表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.Dict;

/// <summary>
/// 字典数据表Dto
/// </summary>
public partial class TaktDictDataDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataDto()
    {
        DictTypeCode = string.Empty;
        DictLabel = string.Empty;
        DictValue = string.Empty;
    }

    /// <summary>
    /// 字典数据表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictDataId { get; set; }

    /// <summary>
    /// 字典类型ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }
    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; }
    /// <summary>
    /// 字典标签
    /// </summary>
    public string DictLabel { get; set; }
    /// <summary>
    /// 字典本地化键
    /// </summary>
    public string? DictL10nKey { get; set; }
    /// <summary>
    /// 字典标签
    /// </summary>
    public string DictValue { get; set; }
    /// <summary>
    /// CSS类名
    /// </summary>
    public int CssClass { get; set; }
    /// <summary>
    /// 列表类名
    /// </summary>
    public int ListClass { get; set; }
    /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }
    /// <summary>
    /// 扩展值
    /// </summary>
    public string? ExtValue { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 字典数据表查询DTO
/// </summary>
public partial class TaktDictDataQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 字典数据表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictDataId { get; set; }

    /// <summary>
    /// 字典类型ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DictTypeId { get; set; }
    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string? DictTypeCode { get; set; }
    /// <summary>
    /// 字典标签
    /// </summary>
    public string? DictLabel { get; set; }
    /// <summary>
    /// 字典本地化键
    /// </summary>
    public string? DictL10nKey { get; set; }
    /// <summary>
    /// 字典标签
    /// </summary>
    public string? DictValue { get; set; }
    /// <summary>
    /// CSS类名
    /// </summary>
    public int? CssClass { get; set; }
    /// <summary>
    /// 列表类名
    /// </summary>
    public int? ListClass { get; set; }
    /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }
    /// <summary>
    /// 扩展值
    /// </summary>
    public string? ExtValue { get; set; }
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
/// Takt创建字典数据表DTO
/// </summary>
public partial class TaktDictDataCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataCreateDto()
    {
        DictTypeCode = string.Empty;
        DictLabel = string.Empty;
        DictValue = string.Empty;
    }

        /// <summary>
    /// 字典类型ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

        /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictLabel { get; set; }

        /// <summary>
    /// 字典本地化键
    /// </summary>
    public string? DictL10nKey { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictValue { get; set; }

        /// <summary>
    /// CSS类名
    /// </summary>
    public int CssClass { get; set; }

        /// <summary>
    /// 列表类名
    /// </summary>
    public int ListClass { get; set; }

        /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }

        /// <summary>
    /// 扩展值
    /// </summary>
    public string? ExtValue { get; set; }

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
/// Takt更新字典数据表DTO
/// </summary>
public partial class TaktDictDataUpdateDto : TaktDictDataCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataUpdateDto()
    {
    }

        /// <summary>
    /// 字典数据表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictDataId { get; set; }
}

/// <summary>
/// 字典数据表导入模板DTO
/// </summary>
public partial class TaktDictDataTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataTemplateDto()
    {
        DictTypeCode = string.Empty;
        DictLabel = string.Empty;
        DictValue = string.Empty;
    }

        /// <summary>
    /// 字典类型ID
    /// </summary>
    public long DictTypeId { get; set; }

        /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictLabel { get; set; }

        /// <summary>
    /// 字典本地化键
    /// </summary>
    public string? DictL10nKey { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictValue { get; set; }

        /// <summary>
    /// CSS类名
    /// </summary>
    public int CssClass { get; set; }

        /// <summary>
    /// 列表类名
    /// </summary>
    public int ListClass { get; set; }

        /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }

        /// <summary>
    /// 扩展值
    /// </summary>
    public string? ExtValue { get; set; }

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
/// 字典数据表导入DTO
/// </summary>
public partial class TaktDictDataImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataImportDto()
    {
        DictTypeCode = string.Empty;
        DictLabel = string.Empty;
        DictValue = string.Empty;
    }

        /// <summary>
    /// 字典类型ID
    /// </summary>
    public long DictTypeId { get; set; }

        /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictLabel { get; set; }

        /// <summary>
    /// 字典本地化键
    /// </summary>
    public string? DictL10nKey { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictValue { get; set; }

        /// <summary>
    /// CSS类名
    /// </summary>
    public int CssClass { get; set; }

        /// <summary>
    /// 列表类名
    /// </summary>
    public int ListClass { get; set; }

        /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }

        /// <summary>
    /// 扩展值
    /// </summary>
    public string? ExtValue { get; set; }

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
/// 字典数据表导出DTO
/// </summary>
public partial class TaktDictDataExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataExportDto()
    {
        CreatedAt = DateTime.Now;
        DictTypeCode = string.Empty;
        DictLabel = string.Empty;
        DictValue = string.Empty;
    }

        /// <summary>
    /// 字典类型ID
    /// </summary>
    public long DictTypeId { get; set; }

        /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictLabel { get; set; }

        /// <summary>
    /// 字典本地化键
    /// </summary>
    public string? DictL10nKey { get; set; }

        /// <summary>
    /// 字典标签
    /// </summary>
    public string DictValue { get; set; }

        /// <summary>
    /// CSS类名
    /// </summary>
    public int CssClass { get; set; }

        /// <summary>
    /// 列表类名
    /// </summary>
    public int ListClass { get; set; }

        /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }

        /// <summary>
    /// 扩展值
    /// </summary>
    public string? ExtValue { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}