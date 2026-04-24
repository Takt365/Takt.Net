// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Logging
// 文件名称：TaktAopLogDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：差异日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// 差异日志表Dto
/// </summary>
public partial class TaktAopLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogDto()
    {
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
    }

    /// <summary>
    /// 差异日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AopLogId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; }
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }
    /// <summary>
    /// 主键ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }
    /// <summary>
    /// 修改前数据
    /// </summary>
    public string? BeforeData { get; set; }
    /// <summary>
    /// 修改后数据
    /// </summary>
    public string? AfterData { get; set; }
    /// <summary>
    /// 差异内容
    /// </summary>
    public string? DiffData { get; set; }
    /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }
}

/// <summary>
/// 差异日志表查询DTO
/// </summary>
public partial class TaktAopLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 差异日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AopLogId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }
    /// <summary>
    /// 表名
    /// </summary>
    public string? TableName { get; set; }
    /// <summary>
    /// 主键ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }
    /// <summary>
    /// 修改前数据
    /// </summary>
    public string? BeforeData { get; set; }
    /// <summary>
    /// 修改后数据
    /// </summary>
    public string? AfterData { get; set; }
    /// <summary>
    /// 差异内容
    /// </summary>
    public string? DiffData { get; set; }
    /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime? OperTime { get; set; }

    /// <summary>
    /// 操作时间开始时间
    /// </summary>
    public DateTime? OperTimeStart { get; set; }
    /// <summary>
    /// 操作时间结束时间
    /// </summary>
    public DateTime? OperTimeEnd { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public int? CostTime { get; set; }

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
/// Takt创建差异日志表DTO
/// </summary>
public partial class TaktAopLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogCreateDto()
    {
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; }

        /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 主键ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }

        /// <summary>
    /// 修改前数据
    /// </summary>
    public string? BeforeData { get; set; }

        /// <summary>
    /// 修改后数据
    /// </summary>
    public string? AfterData { get; set; }

        /// <summary>
    /// 差异内容
    /// </summary>
    public string? DiffData { get; set; }

        /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

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
/// Takt更新差异日志表DTO
/// </summary>
public partial class TaktAopLogUpdateDto : TaktAopLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogUpdateDto()
    {
    }

        /// <summary>
    /// 差异日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AopLogId { get; set; }
}

/// <summary>
/// 差异日志表导入模板DTO
/// </summary>
public partial class TaktAopLogTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogTemplateDto()
    {
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; }

        /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 主键ID
    /// </summary>
    public long? PrimaryKeyId { get; set; }

        /// <summary>
    /// 修改前数据
    /// </summary>
    public string? BeforeData { get; set; }

        /// <summary>
    /// 修改后数据
    /// </summary>
    public string? AfterData { get; set; }

        /// <summary>
    /// 差异内容
    /// </summary>
    public string? DiffData { get; set; }

        /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

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
/// 差异日志表导入DTO
/// </summary>
public partial class TaktAopLogImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogImportDto()
    {
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; }

        /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 主键ID
    /// </summary>
    public long? PrimaryKeyId { get; set; }

        /// <summary>
    /// 修改前数据
    /// </summary>
    public string? BeforeData { get; set; }

        /// <summary>
    /// 修改后数据
    /// </summary>
    public string? AfterData { get; set; }

        /// <summary>
    /// 差异内容
    /// </summary>
    public string? DiffData { get; set; }

        /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

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
/// 差异日志表导出DTO
/// </summary>
public partial class TaktAopLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogExportDto()
    {
        CreatedAt = DateTime.Now;
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; }

        /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 主键ID
    /// </summary>
    public long? PrimaryKeyId { get; set; }

        /// <summary>
    /// 修改前数据
    /// </summary>
    public string? BeforeData { get; set; }

        /// <summary>
    /// 修改后数据
    /// </summary>
    public string? AfterData { get; set; }

        /// <summary>
    /// 差异内容
    /// </summary>
    public string? DiffData { get; set; }

        /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}