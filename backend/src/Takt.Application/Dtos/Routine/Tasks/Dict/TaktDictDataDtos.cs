// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.Dict
// 文件名称：TaktDictDataDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典数据DTO，包含字典数据相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================


// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.Dict
// 文件名称：TaktDictDataDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典数据DTO，包含字典数据相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Tasks.Dict;

/// <summary>
/// Takt字典数据DTO
/// </summary>
public class TaktDictDataDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataDto()
    {
        DictLabel = string.Empty;
        DictValue = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 字典数据ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictDataId { get; set; }

    /// <summary>
    /// 字典类型ID（外键，关联 TaktDictType.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典标签（在同一个字典类型下唯一）
    /// </summary>
    public string DictLabel { get; set; }

    /// <summary>
    /// 字典本地化键（用于多语言翻译）
    /// </summary>
    public string? DictL10nKey { get; set; }

    /// <summary>
    /// 字典值（显示值）
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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt字典数据查询DTO
/// </summary>
public class TaktDictDataQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在字典标签、字典值中模糊查询

    /// <summary>
    /// 字典类型ID（外键，关联字典类型）
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
    /// 字典值
    /// </summary>
    public string? DictValue { get; set; }
}

/// <summary>
/// Takt创建字典数据DTO
/// </summary>
public class TaktDictDataCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataCreateDto()
    {
        DictLabel = string.Empty;
        DictValue = string.Empty;
    }

    /// <summary>
    /// 字典类型ID（外键，关联 TaktDictType.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典标签（在同一个字典类型下唯一）
    /// </summary>
    public string DictLabel { get; set; } = string.Empty;

    /// <summary>
    /// 字典本地化键（用于多语言翻译）
    /// </summary>
    public string? DictL10nKey { get; set; }

    /// <summary>
    /// 字典值（显示值）
    /// </summary>
    public string DictValue { get; set; } = string.Empty;

    /// <summary>
    /// CSS类名
    /// </summary>
    public int CssClass { get; set; } = 0;

    /// <summary>
    /// 列表类名
    /// </summary>
    public int ListClass { get; set; } = 0;

    /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }

    /// <summary>
    /// 扩展值
    /// </summary>
    public string? ExtValue { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新字典数据DTO
/// </summary>
public class TaktDictDataUpdateDto : TaktDictDataCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataUpdateDto()
    {
    }

    /// <summary>
    /// 字典数据ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictDataId { get; set; }
}

/// <summary>
/// Takt字典数据导入模板DTO
/// </summary>
public class TaktDictDataTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataTemplateDto()
    {
        DictLabel = string.Empty;
        DictValue = string.Empty;
        ExtLabel = string.Empty;
        ExtValue = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 字典类型ID（外键，关联 TaktDictType.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典标签（在同一个字典类型下唯一）
    /// </summary>
    public string DictLabel { get; set; }

    /// <summary>
    /// 字典本地化键（用于多语言翻译）
    /// </summary>
    public string? DictL10nKey { get; set; }

    /// <summary>
    /// 字典值（显示值）
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
    public string ExtLabel { get; set; }

    /// <summary>
    /// 扩展值
    /// </summary>
    public string ExtValue { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt字典数据导入DTO
/// </summary>
public class TaktDictDataImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataImportDto()
    {
        DictLabel = string.Empty;
        DictValue = string.Empty;
        ExtLabel = string.Empty;
        ExtValue = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 字典类型ID（外键，关联 TaktDictType.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string DictTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典标签（在同一个字典类型下唯一）
    /// </summary>
    public string DictLabel { get; set; }

    /// <summary>
    /// 字典本地化键（用于多语言翻译）
    /// </summary>
    public string? DictL10nKey { get; set; }

    /// <summary>
    /// 字典值（显示值）
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
    public string ExtLabel { get; set; }

    /// <summary>
    /// 扩展值
    /// </summary>
    public string ExtValue { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt字典数据导出DTO
/// </summary>
public class TaktDictDataExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDataExportDto()
    {
        DictLabel = string.Empty;
        DictValue = string.Empty;
        ExtLabel = string.Empty;
        ExtValue = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 字典标签（在同一个字典类型下唯一）
    /// </summary>
    public string DictLabel { get; set; }

    /// <summary>
    /// 字典值（显示值）
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
    public string ExtLabel { get; set; }

    /// <summary>
    /// 扩展值
    /// </summary>
    public string ExtValue { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}