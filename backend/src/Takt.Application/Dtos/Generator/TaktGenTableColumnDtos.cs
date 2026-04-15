// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Generator
// 文件名称：TaktGenTableColumnDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成字段配置DTO，包含代码生成字段配置相关的数据传输对象
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Generator;

/// <summary>
/// Takt代码生成字段配置DTO
/// </summary>
public class TaktGenTableColumnDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnDto()
    {
        DatabaseColumnName = string.Empty;
        DatabaseDataType = "nvarchar";
        CsharpDataType = "string";
        CsharpColumnName = string.Empty;
        QueryType = "LIKE";
        HtmlType = "input";
        ConfigId = "0";
    }

    /// <summary>
    /// 字段ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ColumnId { get; set; }

    /// <summary>
    /// 表ID（关联代码生成表配置，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TableId { get; set; }

    /// <summary>
    /// 数据库列名称（数据库字段名，使用下划线命名法snake_case，如：column_name、user_name）
    /// </summary>
    public string DatabaseColumnName { get; set; }

    /// <summary>
    /// 列描述（字段注释）
    /// </summary>
    public string? ColumnComment { get; set; }

    /// <summary>
    /// 数据库数据类型（如：varchar、int、datetime、decimal等）
    /// </summary>
    public string DatabaseDataType { get; set; } = "nvarchar";

    /// <summary>
    /// C#类型（对应C#数据类型，如：string、int、long、DateTime、decimal、bool、Guid等）
    /// </summary>
    public string CsharpDataType { get; set; } = "string";

    /// <summary>
    /// C#列名（C#属性名，首字母大写，帕斯卡命名法）
    /// </summary>
    public string CsharpColumnName { get; set; } = string.Empty;

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int Length { get; set; } = 0;

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int DecimalDigits { get; set; } = 0;

    /// <summary>
    /// 是否主键（1=是，0=否）
    /// </summary>
    public int IsPk { get; set; }

    /// <summary>
    /// 是否自增（1=是，0=否）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否必填（1=是，0=否）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否为新增字段（1=是，0=否）
    /// </summary>
    public int IsCreate { get; set; }

    /// <summary>
    /// 是否更新字段（1=是，0=否）
    /// </summary>
    public int IsUpdate { get; set; }

    /// <summary>
    /// 是否查重字段（1=是，0=否）
    /// </summary>
    public int IsUnique { get; set; }

    /// <summary>
    /// 是否列表字段（1=是，0=否）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否导出字段（1=是，0=否）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 是否排序字段（1=是，0=否）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否查询字段（1=是，0=否）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（EQ=等于，NE=不等于，GT=大于，LT=小于，LIKE=模糊，BETWEEN=范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 显示类型（input=输入框，textarea=文本域，select=下拉框，checkbox=复选框，radio=单选框，date=日期控件，time=时间控件，image=图片上传，file=文件上传，slider=滑块，switch=开关，editor=富文本编辑器）
    /// </summary>
    public string HtmlType { get; set; }

    /// <summary>
    /// 字典类型（关联数据字典）
    /// </summary>
    public string? DictType { get; set; }

    /// <summary>
    /// 排序序号
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt代码生成字段配置查询DTO
/// </summary>
public class TaktGenTableColumnQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在数据库列名称、C#列名中模糊查询

    /// <summary>
    /// 表ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TableId { get; set; }

    /// <summary>
    /// 数据库列名称（数据库字段名，使用下划线命名法snake_case，如：column_name、user_name）
    /// </summary>
    public string? DatabaseColumnName { get; set; }

    /// <summary>
    /// 是否主键（1=是，0=否）
    /// </summary>
    public int? IsPk { get; set; }

    /// <summary>
    /// 是否查询字段（1=是，0=否）
    /// </summary>
    public int? IsQuery { get; set; }

    /// <summary>
    /// 是否查重字段（1=是，0=否）
    /// </summary>
    public int? IsUnique { get; set; }
}

/// <summary>
/// Takt创建代码生成字段配置DTO
/// </summary>
public class TaktGenTableColumnCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnCreateDto()
    {
        DatabaseColumnName = string.Empty;
        DatabaseDataType = "nvarchar";
        CsharpDataType = "string";
        CsharpColumnName = string.Empty;
        QueryType = "LIKE";
        HtmlType = "input";
    }

    /// <summary>
    /// 表ID（关联代码生成表配置，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TableId { get; set; }

    /// <summary>
    /// 数据库列名称（数据库字段名，使用下划线命名法snake_case，如：column_name、user_name）
    /// </summary>
    public string DatabaseColumnName { get; set; } = string.Empty;

    /// <summary>
    /// 列描述（字段注释）
    /// </summary>
    public string? ColumnComment { get; set; }

    /// <summary>
    /// 数据库数据类型（如：varchar、int、datetime、decimal等）
    /// </summary>
    public string DatabaseDataType { get; set; } = "nvarchar";

    /// <summary>
    /// C#类型（对应C#数据类型，如：string、int、long、DateTime、decimal、bool、Guid等）
    /// </summary>
    public string CsharpDataType { get; set; } = "string";

    /// <summary>
    /// C#列名（C#属性名，首字母大写，帕斯卡命名法）
    /// </summary>
    public string CsharpColumnName { get; set; } = string.Empty;

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int Length { get; set; } = 0;

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int DecimalDigits { get; set; } = 0;

    /// <summary>
    /// 是否主键（1=是，0=否）
    /// </summary>
    public int IsPk { get; set; } = 0;

    /// <summary>
    /// 是否自增（1=是，0=否）
    /// </summary>
    public int IsIncrement { get; set; } = 0;

    /// <summary>
    /// 是否必填（1=是，0=否）
    /// </summary>
    public int IsRequired { get; set; } = 1;

    /// <summary>
    /// 是否为新增字段（1=是，0=否）
    /// </summary>
    public int IsCreate { get; set; } = 1;

    /// <summary>
    /// 是否更新字段（1=是，0=否）
    /// </summary>
    public int IsUpdate { get; set; } = 1;

    /// <summary>
    /// 是否查重字段（1=是，0=否）
    /// </summary>
    public int IsUnique { get; set; } = 0;

    /// <summary>
    /// 是否列表字段（1=是，0=否）
    /// </summary>
    public int IsList { get; set; } = 1;

    /// <summary>
    /// 是否导出字段（1=是，0=否）
    /// </summary>
    public int IsExport { get; set; } = 1;

    /// <summary>
    /// 是否排序字段（1=是，0=否）
    /// </summary>
    public int IsSort { get; set; } = 0;

    /// <summary>
    /// 是否查询字段（1=是，0=否）
    /// </summary>
    public int IsQuery { get; set; } = 0;

    /// <summary>
    /// 查询方式（EQ=等于，NE=不等于，GT=大于，LT=小于，LIKE=模糊，BETWEEN=范围）
    /// </summary>
    public string QueryType { get; set; } = "LIKE";

    /// <summary>
    /// 显示类型（input=输入框，textarea=文本域，select=下拉框，checkbox=复选框，radio=单选框，date=日期控件，time=时间控件，image=图片上传，file=文件上传，slider=滑块，switch=开关，editor=富文本编辑器）
    /// </summary>
    public string HtmlType { get; set; } = "input";

    /// <summary>
    /// 字典类型（关联数据字典）
    /// </summary>
    public string? DictType { get; set; }

    /// <summary>
    /// 排序序号
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新代码生成字段配置DTO
/// </summary>
public class TaktGenTableColumnUpdateDto : TaktGenTableColumnCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnUpdateDto()
    {
    }

    /// <summary>
    /// 字段ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ColumnId { get; set; }
}
