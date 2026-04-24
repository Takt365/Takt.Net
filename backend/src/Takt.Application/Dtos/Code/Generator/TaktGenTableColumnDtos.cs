// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Code.Generator
// 文件名称：TaktGenTableColumnDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：代码生成字段配置表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Code.Generator;

/// <summary>
/// 代码生成字段配置表Dto
/// </summary>
public partial class TaktGenTableColumnDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnDto()
    {
        DatabaseColumnName = string.Empty;
        DatabaseDataType = string.Empty;
        CsharpDataType = string.Empty;
        CsharpColumnName = string.Empty;
        QueryType = string.Empty;
        HtmlType = string.Empty;
    }

    /// <summary>
    /// 代码生成字段配置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long GenTableColumnId { get; set; }

    /// <summary>
    /// 表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TableId { get; set; }
    /// <summary>
    /// 列名称
    /// </summary>
    public string DatabaseColumnName { get; set; }
    /// <summary>
    /// 列描述
    /// </summary>
    public string? ColumnComment { get; set; }
    /// <summary>
    /// 数据类型
    /// </summary>
    public string DatabaseDataType { get; set; }
    /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpDataType { get; set; }
    /// <summary>
    /// C#列名
    /// </summary>
    public string CsharpColumnName { get; set; }
    /// <summary>
    /// 数据长度
    /// </summary>
    public int Length { get; set; }
    /// <summary>
    /// 数据精度
    /// </summary>
    public int DecimalDigits { get; set; }
    /// <summary>
    /// 是否主键
    /// </summary>
    public int IsPk { get; set; }
    /// <summary>
    /// 是否自增
    /// </summary>
    public int IsIncrement { get; set; }
    /// <summary>
    /// 是否必填
    /// </summary>
    public int IsRequired { get; set; }
    /// <summary>
    /// 是否新增
    /// </summary>
    public int IsCreate { get; set; }
    /// <summary>
    /// 是否更新
    /// </summary>
    public int IsUpdate { get; set; }
    /// <summary>
    /// 是否查重
    /// </summary>
    public int IsUnique { get; set; }
    /// <summary>
    /// 是否列表
    /// </summary>
    public int IsList { get; set; }
    /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }
    /// <summary>
    /// 是否排序
    /// </summary>
    public int IsSort { get; set; }
    /// <summary>
    /// 是否查询
    /// </summary>
    public int IsQuery { get; set; }
    /// <summary>
    /// 查询方式
    /// </summary>
    public string QueryType { get; set; }
    /// <summary>
    /// 显示类型
    /// </summary>
    public string HtmlType { get; set; }
    /// <summary>
    /// 字典类型
    /// </summary>
    public string? DictType { get; set; }
    /// <summary>
    /// 排序序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 代码生成字段配置表查询DTO
/// </summary>
public partial class TaktGenTableColumnQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 代码生成字段配置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long GenTableColumnId { get; set; }

    /// <summary>
    /// 表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TableId { get; set; }
    /// <summary>
    /// 列名称
    /// </summary>
    public string? DatabaseColumnName { get; set; }
    /// <summary>
    /// 列描述
    /// </summary>
    public string? ColumnComment { get; set; }
    /// <summary>
    /// 数据类型
    /// </summary>
    public string? DatabaseDataType { get; set; }
    /// <summary>
    /// C#类型
    /// </summary>
    public string? CsharpDataType { get; set; }
    /// <summary>
    /// C#列名
    /// </summary>
    public string? CsharpColumnName { get; set; }
    /// <summary>
    /// 数据长度
    /// </summary>
    public int? Length { get; set; }
    /// <summary>
    /// 数据精度
    /// </summary>
    public int? DecimalDigits { get; set; }
    /// <summary>
    /// 是否主键
    /// </summary>
    public int? IsPk { get; set; }
    /// <summary>
    /// 是否自增
    /// </summary>
    public int? IsIncrement { get; set; }
    /// <summary>
    /// 是否必填
    /// </summary>
    public int? IsRequired { get; set; }
    /// <summary>
    /// 是否新增
    /// </summary>
    public int? IsCreate { get; set; }
    /// <summary>
    /// 是否更新
    /// </summary>
    public int? IsUpdate { get; set; }
    /// <summary>
    /// 是否查重
    /// </summary>
    public int? IsUnique { get; set; }
    /// <summary>
    /// 是否列表
    /// </summary>
    public int? IsList { get; set; }
    /// <summary>
    /// 是否导出
    /// </summary>
    public int? IsExport { get; set; }
    /// <summary>
    /// 是否排序
    /// </summary>
    public int? IsSort { get; set; }
    /// <summary>
    /// 是否查询
    /// </summary>
    public int? IsQuery { get; set; }
    /// <summary>
    /// 查询方式
    /// </summary>
    public string? QueryType { get; set; }
    /// <summary>
    /// 显示类型
    /// </summary>
    public string? HtmlType { get; set; }
    /// <summary>
    /// 字典类型
    /// </summary>
    public string? DictType { get; set; }
    /// <summary>
    /// 排序序号
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
/// Takt创建代码生成字段配置表DTO
/// </summary>
public partial class TaktGenTableColumnCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnCreateDto()
    {
        DatabaseColumnName = string.Empty;
        DatabaseDataType = string.Empty;
        CsharpDataType = string.Empty;
        CsharpColumnName = string.Empty;
        QueryType = string.Empty;
        HtmlType = string.Empty;
    }

        /// <summary>
    /// 表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TableId { get; set; }

        /// <summary>
    /// 列名称
    /// </summary>
    public string DatabaseColumnName { get; set; }

        /// <summary>
    /// 列描述
    /// </summary>
    public string? ColumnComment { get; set; }

        /// <summary>
    /// 数据类型
    /// </summary>
    public string DatabaseDataType { get; set; }

        /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpDataType { get; set; }

        /// <summary>
    /// C#列名
    /// </summary>
    public string CsharpColumnName { get; set; }

        /// <summary>
    /// 数据长度
    /// </summary>
    public int Length { get; set; }

        /// <summary>
    /// 数据精度
    /// </summary>
    public int DecimalDigits { get; set; }

        /// <summary>
    /// 是否主键
    /// </summary>
    public int IsPk { get; set; }

        /// <summary>
    /// 是否自增
    /// </summary>
    public int IsIncrement { get; set; }

        /// <summary>
    /// 是否必填
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否新增
    /// </summary>
    public int IsCreate { get; set; }

        /// <summary>
    /// 是否更新
    /// </summary>
    public int IsUpdate { get; set; }

        /// <summary>
    /// 是否查重
    /// </summary>
    public int IsUnique { get; set; }

        /// <summary>
    /// 是否列表
    /// </summary>
    public int IsList { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 是否排序
    /// </summary>
    public int IsSort { get; set; }

        /// <summary>
    /// 是否查询
    /// </summary>
    public int IsQuery { get; set; }

        /// <summary>
    /// 查询方式
    /// </summary>
    public string QueryType { get; set; }

        /// <summary>
    /// 显示类型
    /// </summary>
    public string HtmlType { get; set; }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string? DictType { get; set; }

        /// <summary>
    /// 排序序号
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
/// Takt更新代码生成字段配置表DTO
/// </summary>
public partial class TaktGenTableColumnUpdateDto : TaktGenTableColumnCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnUpdateDto()
    {
    }

        /// <summary>
    /// 代码生成字段配置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long GenTableColumnId { get; set; }
}

/// <summary>
/// 代码生成字段配置表导入模板DTO
/// </summary>
public partial class TaktGenTableColumnTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnTemplateDto()
    {
        DatabaseColumnName = string.Empty;
        DatabaseDataType = string.Empty;
        CsharpDataType = string.Empty;
        CsharpColumnName = string.Empty;
        QueryType = string.Empty;
        HtmlType = string.Empty;
    }

        /// <summary>
    /// 表ID
    /// </summary>
    public long TableId { get; set; }

        /// <summary>
    /// 列名称
    /// </summary>
    public string DatabaseColumnName { get; set; }

        /// <summary>
    /// 列描述
    /// </summary>
    public string? ColumnComment { get; set; }

        /// <summary>
    /// 数据类型
    /// </summary>
    public string DatabaseDataType { get; set; }

        /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpDataType { get; set; }

        /// <summary>
    /// C#列名
    /// </summary>
    public string CsharpColumnName { get; set; }

        /// <summary>
    /// 数据长度
    /// </summary>
    public int Length { get; set; }

        /// <summary>
    /// 数据精度
    /// </summary>
    public int DecimalDigits { get; set; }

        /// <summary>
    /// 是否主键
    /// </summary>
    public int IsPk { get; set; }

        /// <summary>
    /// 是否自增
    /// </summary>
    public int IsIncrement { get; set; }

        /// <summary>
    /// 是否必填
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否新增
    /// </summary>
    public int IsCreate { get; set; }

        /// <summary>
    /// 是否更新
    /// </summary>
    public int IsUpdate { get; set; }

        /// <summary>
    /// 是否查重
    /// </summary>
    public int IsUnique { get; set; }

        /// <summary>
    /// 是否列表
    /// </summary>
    public int IsList { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 是否排序
    /// </summary>
    public int IsSort { get; set; }

        /// <summary>
    /// 是否查询
    /// </summary>
    public int IsQuery { get; set; }

        /// <summary>
    /// 查询方式
    /// </summary>
    public string QueryType { get; set; }

        /// <summary>
    /// 显示类型
    /// </summary>
    public string HtmlType { get; set; }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string? DictType { get; set; }

        /// <summary>
    /// 排序序号
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
/// 代码生成字段配置表导入DTO
/// </summary>
public partial class TaktGenTableColumnImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnImportDto()
    {
        DatabaseColumnName = string.Empty;
        DatabaseDataType = string.Empty;
        CsharpDataType = string.Empty;
        CsharpColumnName = string.Empty;
        QueryType = string.Empty;
        HtmlType = string.Empty;
    }

        /// <summary>
    /// 表ID
    /// </summary>
    public long TableId { get; set; }

        /// <summary>
    /// 列名称
    /// </summary>
    public string DatabaseColumnName { get; set; }

        /// <summary>
    /// 列描述
    /// </summary>
    public string? ColumnComment { get; set; }

        /// <summary>
    /// 数据类型
    /// </summary>
    public string DatabaseDataType { get; set; }

        /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpDataType { get; set; }

        /// <summary>
    /// C#列名
    /// </summary>
    public string CsharpColumnName { get; set; }

        /// <summary>
    /// 数据长度
    /// </summary>
    public int Length { get; set; }

        /// <summary>
    /// 数据精度
    /// </summary>
    public int DecimalDigits { get; set; }

        /// <summary>
    /// 是否主键
    /// </summary>
    public int IsPk { get; set; }

        /// <summary>
    /// 是否自增
    /// </summary>
    public int IsIncrement { get; set; }

        /// <summary>
    /// 是否必填
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否新增
    /// </summary>
    public int IsCreate { get; set; }

        /// <summary>
    /// 是否更新
    /// </summary>
    public int IsUpdate { get; set; }

        /// <summary>
    /// 是否查重
    /// </summary>
    public int IsUnique { get; set; }

        /// <summary>
    /// 是否列表
    /// </summary>
    public int IsList { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 是否排序
    /// </summary>
    public int IsSort { get; set; }

        /// <summary>
    /// 是否查询
    /// </summary>
    public int IsQuery { get; set; }

        /// <summary>
    /// 查询方式
    /// </summary>
    public string QueryType { get; set; }

        /// <summary>
    /// 显示类型
    /// </summary>
    public string HtmlType { get; set; }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string? DictType { get; set; }

        /// <summary>
    /// 排序序号
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
/// 代码生成字段配置表导出DTO
/// </summary>
public partial class TaktGenTableColumnExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnExportDto()
    {
        CreatedAt = DateTime.Now;
        DatabaseColumnName = string.Empty;
        DatabaseDataType = string.Empty;
        CsharpDataType = string.Empty;
        CsharpColumnName = string.Empty;
        QueryType = string.Empty;
        HtmlType = string.Empty;
    }

        /// <summary>
    /// 表ID
    /// </summary>
    public long TableId { get; set; }

        /// <summary>
    /// 列名称
    /// </summary>
    public string DatabaseColumnName { get; set; }

        /// <summary>
    /// 列描述
    /// </summary>
    public string? ColumnComment { get; set; }

        /// <summary>
    /// 数据类型
    /// </summary>
    public string DatabaseDataType { get; set; }

        /// <summary>
    /// C#类型
    /// </summary>
    public string CsharpDataType { get; set; }

        /// <summary>
    /// C#列名
    /// </summary>
    public string CsharpColumnName { get; set; }

        /// <summary>
    /// 数据长度
    /// </summary>
    public int Length { get; set; }

        /// <summary>
    /// 数据精度
    /// </summary>
    public int DecimalDigits { get; set; }

        /// <summary>
    /// 是否主键
    /// </summary>
    public int IsPk { get; set; }

        /// <summary>
    /// 是否自增
    /// </summary>
    public int IsIncrement { get; set; }

        /// <summary>
    /// 是否必填
    /// </summary>
    public int IsRequired { get; set; }

        /// <summary>
    /// 是否新增
    /// </summary>
    public int IsCreate { get; set; }

        /// <summary>
    /// 是否更新
    /// </summary>
    public int IsUpdate { get; set; }

        /// <summary>
    /// 是否查重
    /// </summary>
    public int IsUnique { get; set; }

        /// <summary>
    /// 是否列表
    /// </summary>
    public int IsList { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 是否排序
    /// </summary>
    public int IsSort { get; set; }

        /// <summary>
    /// 是否查询
    /// </summary>
    public int IsQuery { get; set; }

        /// <summary>
    /// 查询方式
    /// </summary>
    public string QueryType { get; set; }

        /// <summary>
    /// 显示类型
    /// </summary>
    public string HtmlType { get; set; }

        /// <summary>
    /// 字典类型
    /// </summary>
    public string? DictType { get; set; }

        /// <summary>
    /// 排序序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}